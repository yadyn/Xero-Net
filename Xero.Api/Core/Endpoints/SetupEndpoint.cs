using System.Net;
using System.Threading.Tasks;
using Xero.Api.Core.Model.Setup;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public class SetupEndpoint
    {
        private readonly XeroHttpClient _client;
        private readonly string _apiEndpointUrl;

        public SetupEndpoint(XeroHttpClient client) :
            this(client, "/api.xro/2.0/Setup")
        {
        }

        protected SetupEndpoint(XeroHttpClient client, string apiEndpointUrl)
        {
            _client = client;
            _apiEndpointUrl = apiEndpointUrl;
        }

        public Task<ImportSummary> UpdateAsync(Setup setup)
        {
            return HandleResponseAsync(_client
                .Client
                .PostAsync(_apiEndpointUrl, _client.XmlMapper.To(setup)));
        }

        public Task<ImportSummary> CreateAsync(Setup setup)
        {
            return HandleResponseAsync(_client
                .Client
                .PutAsync(_apiEndpointUrl, _client.XmlMapper.To(setup)));
        }

        private async Task<ImportSummary> HandleResponseAsync(Task<Infrastructure.Http.Response> responseTask)
        {
            var response = await responseTask;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return _client.JsonMapper.From<SetupResponse>(response.Body).ImportSummary;                
            }

            _client.HandleErrors(response);

            return null;
        }
    }
}
