using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public class AllocationsEndpoint
    {
        private readonly XeroHttpClient _client;
        
        public AllocationsEndpoint(XeroHttpClient client)
        {
            _client = client;
        }

        public async Task<Allocation> AddAsync(Allocation allocation)
        {
            var endpoint = string.Format("/api.xro/2.0/CreditNotes/{0}/Allocations", allocation.CreditNote.Id);

            return (Allocation)(await AddAsync(allocation, endpoint));
        }

        public async Task<CreditNoteAllocation> AddAsync(CreditNoteAllocation allocation)
        {
            var endpoint = string.Format("/api.xro/2.0/CreditNotes/{0}/Allocations", allocation.CreditNote.Id);

            return (CreditNoteAllocation)(await AddAsync(allocation, endpoint));
        }

        public async Task<PrepaymentAllocation> AddAsync(PrepaymentAllocation allocation)
        {
            var endpoint = string.Format("/api.xro/2.0/Prepayments/{0}/Allocations", allocation.Prepayment.Id);

            return (PrepaymentAllocation)(await AddAsync(allocation, endpoint));
        }

        public async Task<OverpaymentAllocation> AddAsync(OverpaymentAllocation allocation)
        {
            var endpoint = string.Format("/api.xro/2.0/Overpayments/{0}/Allocations", allocation.Overpayment.Id);

            return (OverpaymentAllocation)(await AddAsync(allocation, endpoint));
        }

        private AllocationsResponse<T> HandleResponse<T>(Infrastructure.Http.Response response)
            where T : AllocationBase
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = _client.JsonMapper.From<AllocationsResponse<T>>(response.Body);
                return result;
            }

            _client.HandleErrors(response);

            return null;
        }

        public async Task<AllocationBase> AddAsync<T>(T allocation, string endpoint)
            where T : AllocationBase
        {
            var response = HandleResponse<T>(await _client
                .Client
                .PutAsync(endpoint, _client.XmlMapper.To(new List<T> { allocation })));

            return response.Allocations.FirstOrDefault();
        }
    }

    public class AllocationsResponse<T> : XeroResponse<T>
    {
        public List<T> Allocations { get; set; }

        public override IList<T> Values
        {
            get { return Allocations; }
        }
    }     
}
