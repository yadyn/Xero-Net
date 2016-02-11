using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Model;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface IInboxEndpoint : IXeroUpdateEndpoint<InboxEndpoint, Model.Folder, FolderRequest, FolderResponse>
    {
        Task<FilesResponse> AddAsync(Model.File file, byte[] data);
        Task<FilesResponse> RemoveAsync(Guid fileid);
        Task<Folder> GetInboxFolderAsync();
    }

    public class InboxEndpoint : XeroUpdateEndpoint<InboxEndpoint,Model.Folder,FolderRequest,FolderResponse>, IInboxEndpoint
    {
        internal InboxEndpoint(XeroHttpClient client)
            : base(client, "files.xro/1.0/Inbox")
        {
            
        }

        private async Task<Guid> GetInboxIdAsync()
        {
            var endpoint = string.Format("files.xro/1.0/Inbox");

            var folder = HandleInboxResponse(await Client
                .Client
                .GetAsync(endpoint, null));

            return folder.Id;
        }

        //public Model.File this[Guid id]
        //{
        //    get
        //    {
        //        var result = FindAsync(id);
        //        return result;
        //    }
        //}

        public new async Task<Model.File> FindAsync(Guid fileId)
        {
            var response = HandleFileResponse(await Client
                .Client.GetAsync("files.xro/1.0/Files", ""));

            return response.Items.SingleOrDefault(i => i.Id == fileId);
        }

        public async Task<FilesResponse> AddAsync(Model.File file, byte[] data)
        {
            var inboxId = await GetInboxIdAsync();

            var response = HandleFileResponse(await Client
                .Client
                .PostMultipartFormAsync("files.xro/1.0/Files/" + inboxId, file.Mimetype , file.Name, file.Name, data));

            return response;
        }

        public async Task<FilesResponse> RemoveAsync(Guid fileid)
        {
            var response = HandleFileResponse(await Client
                .Client
                .DeleteAsync("files.xro/1.0/Files/" + fileid.ToString()));

            return response;
        }

        private FilesResponse HandleFileResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                var result = Client.JsonMapper.From<FilesResponse>(response.Body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }

        private InboxResponse HandleInboxResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = response.Body;

                var result = Client.JsonMapper.From<InboxResponse>(json);

                return result;
            }

            Client.HandleErrors(response);

            return null;
        }
      

        public async Task<Folder> GetInboxFolderAsync()
        {
            var endpoint = string.Format("files.xro/1.0/Inbox");

            var folders = HandleFoldersResponse(await Client
                .Client
                .GetAsync(endpoint, null));

            return folders
                .Select(i => new Folder()
                {
                    Id = i.Id,
                    Name = i.Name,
                    IsInbox = i.IsInbox,
                    FileCount = i.FileCount
                })
                .First();
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
}