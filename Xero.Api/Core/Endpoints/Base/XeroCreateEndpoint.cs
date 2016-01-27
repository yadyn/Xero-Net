﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Infrastructure.Http;
using Xero.Api.Infrastructure.Interfaces;

namespace Xero.Api.Core.Endpoints.Base
{
    public abstract class XeroCreateEndpoint<T, TResult, TRequest, TResponse>
        : XeroReadEndpoint<T, TResult, TResponse>
        where T : XeroReadEndpoint<T, TResult, TResponse>
        where TResponse : IXeroResponse<TResult>, new()
        where TRequest : IXeroRequest<TResult>, new()
    {
        protected XeroCreateEndpoint(XeroHttpClient client, string apiEndpointUrl)
            : base(client, apiEndpointUrl)
        {
        }

        public Task<IEnumerable<TResult>> CreateAsync(IEnumerable<TResult> items)
        {
            var request = new TRequest();
            request.AddRange(items);

            return PutAsync(request);
        }

        public async Task<TResult> CreateAsync(TResult item)
        {
            return (await CreateAsync(new[] { item })).First();
        }

        public XeroCreateEndpoint<T, TResult, TRequest, TResponse> SummarizeErrors(bool summarize)
        {
            AddParameter("summarizeErrors", summarize);
            return this;
        }

        protected Task<IEnumerable<TResult>> PutAsync(TRequest data)
        {
            try
            {
                Client.Parameters = Parameters;
                return Client.PutAsync<TResult, TResponse>(ApiEndpointUrl, data);
            }
            finally
            {
                ClearQueryString();            
            }
        }        
    }
}
