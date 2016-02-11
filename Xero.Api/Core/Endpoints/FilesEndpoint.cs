using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Request;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface IFilesEndpoint : IXeroUpdateEndpoint<FilesEndpoint, Model.File, FilesRequest, FilesResponse>
    {
        Task<Model.File> RenameAsync(Guid id, string name);
        Task<Model.File> MoveAsync(Guid id, Guid newFolder);
        Task<Model.File> AddAsync(Guid folderId, Model.File file, byte[] data);
        Task<Model.File> RemoveAsync(Guid fileid);
        Task<byte[]> GetContentAsync(Guid id, string contentType);
    }

    public class FilesEndpoint : XeroUpdateEndpoint<FilesEndpoint, Model.File, FilesRequest, FilesResponse>, IFilesEndpoint
    {

        internal FilesEndpoint(XeroHttpClient client)
            : base(client, "files.xro/1.0/Files")
        {

        }

        //public Model.File this[Guid id]
        //{
        //    get
        //    {
        //        var result = Find(id);
        //        return result;
        //    }
        //}

        public override async Task<IEnumerable<Model.File>> FindAsync()
        {
            var response = HandleFilesResponse(await Client
                .Client.GetAsync("files.xro/1.0/Files", ""));

            return response.Items;
        }

        public override async Task<Model.File> FindAsync(Guid fileId)
        {
            var response = HandleFilesResponse(await Client
                .Client.GetAsync("files.xro/1.0/Files", ""));

            return response.Items.SingleOrDefault(i => i.Id == fileId);
        }

        public async Task<Model.File> RenameAsync(Guid id, string name)
        {
            var response = HandleFileResponse(await Client
                .Client.PutAsync("files.xro/1.0/Files/" + id, "{\"Name\":\"" + name + "\"}", "application/json"));


            return response;
        }

        public async Task<Model.File> MoveAsync(Guid id, Guid newFolder)
        {
            var response = HandleFileResponse(await Client
                .Client.PutAsync("files.xro/1.0/Files/" + id, "{\"FolderId\":\"" + newFolder + "\"}", "application/json"));


            return response;
        }

        public async Task<Model.File> AddAsync(Guid folderId, Model.File file, byte[] data)
        {

            var response = HandleFileResponse(await Client
                .Client
                .PostMultipartFormAsync("files.xro/1.0/Files/" + folderId, file.Mimetype, file.Name, file.FileName, data));

            return response;
        }

        public async Task<Model.File> RemoveAsync(Guid fileid)
        {
            var response = HandleFileResponse(await Client
                .Client
                .DeleteAsync("files.xro/1.0/Files/" + fileid.ToString()));

            return response;
        }

        public async Task<byte[]> GetContentAsync(Guid id, string contentType)
        {
            var response = await Client.Client.GetRawAsync("files.xro/1.0/Files/" + id + "/Content", contentType, "");

            using (MemoryStream ms = new MemoryStream())
            {
                response.Stream.CopyTo(ms);

                return ms.ToArray();
            }

        }

        private Model.File HandleFileResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                var body = response.Body;

                var result = Client.JsonMapper.From<Model.File>(body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }

        private FilesResponse HandleFilesResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                var body = response.Body;

                var result = Client.JsonMapper.From<FilesResponse>(body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }
    }
}