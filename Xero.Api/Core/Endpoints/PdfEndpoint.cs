using System;
using System.Net;
using System.Threading.Tasks;
using Xero.Api.Core.File;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public class PdfEndpoint
    {
        private XeroHttpClient Client { get; set; }

        public PdfEndpoint(XeroHttpClient client)
        {
            Client = client;
        }

        public async Task<BinaryFile> GetAsync(PdfEndpointType type, Guid parent)
        {
            var response = await Client.Client.GetRawAsync(string.Format("/api.xro/2.0/{0}/{1}", type, parent.ToString("D")), "application/pdf");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new BinaryFile(response.Stream, parent.ToString("D") + ".pdf", response.ContentType, response.ContentLength);
            }

            Client.HandleErrors(response);

            return null;
        }
    }
}
