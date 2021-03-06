﻿using System;
using System.Net;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Model;
using Xero.Api.Core.Request;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface ILinkedTransactionsEndpoint : IXeroUpdateEndpoint<LinkedTransactionsEndpoint, LinkedTransaction, LinkedTransactionsRequest, LinkedTransactionsResponse>, IPageableEndpoint<ILinkedTransactionsEndpoint>
    {
        Task DeleteAsync(LinkedTransaction linkedTransaction);
        LinkedTransactionsEndpoint WhereSourceId(Guid sourceId);
        LinkedTransactionsEndpoint WhereContactId(Guid contactId);
        LinkedTransactionsEndpoint WhereTargetId(Guid targetId);
    }

    public class LinkedTransactionsEndpoint
        : XeroUpdateEndpoint<LinkedTransactionsEndpoint, LinkedTransaction, LinkedTransactionsRequest, LinkedTransactionsResponse>, ILinkedTransactionsEndpoint
    {
        internal LinkedTransactionsEndpoint(XeroHttpClient client) 
            : base(client, "/api.xro/2.0/LinkedTransactions")
        {
            Page(1);
        }

        public async Task DeleteAsync(LinkedTransaction linkedTransaction)
        {
            var endpoint = string.Format("/api.xro/2.0/LinkedTransactions/{0}", linkedTransaction.Id);

            HandleResponse(await Client
                .Client
                .DeleteAsync(endpoint));
        }

        public ILinkedTransactionsEndpoint Page(int page)
        {
            AddParameter("page", page);
            return this;
        }

        public LinkedTransactionsEndpoint WhereSourceId(Guid sourceId)
        {
            AddParameter("SourceTransactionID", sourceId.ToString());
            return this;
        }

        public LinkedTransactionsEndpoint WhereContactId(Guid contactId)
        {
            AddParameter("ContactID", contactId.ToString());
            return this;
        }

        public LinkedTransactionsEndpoint WhereTargetId(Guid targetId)
        {
            AddParameter("TargetTransactionID", targetId.ToString());
            return this;
        }

        public override void ClearQueryString()
        {
            base.ClearQueryString();
            Page(1);
        }

        private LinkedTransactionsResponse HandleResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = Client.JsonMapper.From<LinkedTransactionsResponse>(response.Body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }
    }
}
