using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Model;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface IFoldersEndpoint : IXeroUpdateEndpoint<FoldersEndpoint, Model.Folder, FolderRequest, FolderResponse>
    {
        Task<FoldersResponse[]> GetFoldersAsync();
        Task<FilePageResponse> AddAsync(string folderName);
        Task RemoveAsync(Guid id);
        Task<FoldersResponse> RenameAsync(Guid id, string name);
    }

    public class FoldersEndpoint : XeroUpdateEndpoint<FoldersEndpoint, Model.Folder, FolderRequest, FolderResponse>, IFoldersEndpoint
    {
        internal FoldersEndpoint(XeroHttpClient client)
            : base(client, "files.xro/1.0/Folders")
        {
        }

        public async Task<FoldersResponse[]> GetFoldersAsync()
        {
            var endpoint = string.Format("files.xro/1.0/Folders");

            var folder = HandleFoldersResponse(await Client
                .Client
                .GetAsync(endpoint, null));

            return folder;
        }

        public async Task<FilePageResponse> AddAsync(string folderName)
        {
            var endpoint = string.Format("files.xro/1.0/Folders");

            var result = HandleFolderResponse(await Client
                .Client
                .PostAsync(endpoint, Client.JsonMapper.To(new Folder() { Name = folderName }), contentType: "application/json"));

            return result;
        }

        public new async Task<IList<Model.Folder>> FindAsync()
        {
            var response = HandleFoldersResponse(await Client
                .Client.GetAsync("files.xro/1.0/Folders", ""));


            var resultingFolders = from i in response
                                   select new Folder() { Id = i.Id, Name = i.Name, IsInbox = i.IsInbox, FileCount = i.FileCount };

            return resultingFolders.ToList();
        }

        public async Task RemoveAsync(Guid id)
        {
            var response = HandleFolderResponse(await Client
                .Client
                .DeleteAsync("files.xro/1.0/Folders/" + id.ToString()));
        }

        public async Task<FoldersResponse> RenameAsync(Guid id, string name)
        {
            var response = HandleFoldersResponse(await Client.Client.PutAsync("files.xro/1.0/Folders/" + id, "{\"Name\":\"" + name + "\"}", "application/json"));
            return (response != null) ? response[0] : null;
        }

        private FilePageResponse HandleFolderResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                var json = response.Body;

                var result = Client.JsonMapper.From<FilePageResponse>(json);

                return result;
            }

            Client.HandleErrors(response);

            return null;
        }

        private FoldersResponse[] HandleFoldersResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = response.Body;

                var result = Client.JsonMapper.From<FoldersResponse[]>(json);

                return result;
            }

            Client.HandleErrors(response);

            return null;
        }
    }

    public class FolderResponse : XeroResponse<Model.Folder>
    {
        public override IList<Folder> Values
        {
            get { throw new System.NotImplementedException(); }
        }
    }

    public class FolderRequest : XeroRequest<Model.Folder>
    {
    }
}