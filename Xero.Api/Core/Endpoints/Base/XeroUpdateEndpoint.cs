using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Infrastructure.Http;
using Xero.Api.Infrastructure.Interfaces;

namespace Xero.Api.Core.Endpoints.Base
{
    public abstract class XeroUpdateEndpoint<T, TResult, TRequest, TResponse>
        : XeroCreateEndpoint<T, TResult, TRequest, TResponse>
        where T : XeroReadEndpoint<T, TResult, TResponse>
        where TResponse : IXeroResponse<TResult>, new()
        where TRequest : IXeroRequest<TResult>, new()
    {
        protected XeroUpdateEndpoint(XeroHttpClient client, string apiEndpointUrl)
            : base(client, apiEndpointUrl)
        {            
        }

        public Task<IEnumerable<TResult>> UpdateAsync(IEnumerable<TResult> items)
        {
            var request = new TRequest();
            request.AddRange(items);

            return PostAsync(request);
        }

        public async Task<TResult> UpdateAsync(TResult item)
        {
            return (await UpdateAsync(new[] { item })).First();
        }

        protected Task<IEnumerable<TResult>> PostAsync(TRequest data)
        {
            try
            {
                Client.Parameters = Parameters;
                return Client.PostAsync<TResult, TResponse>(ApiEndpointUrl, data);
            }
            finally
            {
                ClearQueryString();
            }
        }
    }
}
