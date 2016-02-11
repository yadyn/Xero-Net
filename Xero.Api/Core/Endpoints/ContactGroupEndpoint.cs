using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Model;
using Xero.Api.Core.Request;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface IContactGroupsEndpoint :
        IXeroUpdateEndpoint<ContactGroupsEndpoint, ContactGroup, ContactGroupsRequest, ContactGroupsResponse>
    {
        Task<IContactCollection> GetContactsAsync(Guid guid);
        Task<ContactGroup> AddAsync(ContactGroup contactGroup);
    }
    public class ContactGroupsEndpoint : XeroUpdateEndpoint<ContactGroupsEndpoint,ContactGroup,ContactGroupsRequest,ContactGroupsResponse>,
        IContactGroupsEndpoint
    {
        public ContactGroupsEndpoint(XeroHttpClient client) : base(client,"/api.xro/2.0/ContactGroups")
        {
        }

        //public IContactCollection this[Guid guid]
        //{
        //    get
        //    {
        //        var endpoint = string.Format("/api.xro/2.0/ContactGroups/{0}",guid);

        //        var group = HandleResponse(Client
        //            .Client
        //            .GetAsync(endpoint,null))
        //            .ContactGroups.SingleOrDefault();

        //        var collection = new ContactCollection(Client, group);

        //        return collection;
        //    }
        //}

        public async Task<IContactCollection> GetContactsAsync(Guid guid)
        {
            var endpoint = string.Format("/api.xro/2.0/ContactGroups/{0}", guid);

            var response = HandleResponse(await Client
                .Client
                .GetAsync(endpoint, null));

            var collection = new ContactCollection(Client, response.ContactGroups.SingleOrDefault());

            return collection;
        }

        public async Task<ContactGroup> AddAsync(ContactGroup contactGroup)
        {
            var endpoint = string.Format("/api.xro/2.0/ContactGroups");

            var result = HandleResponse(await Client
                .Client
                .PutAsync(endpoint, Client.XmlMapper.To(new List<ContactGroup> { contactGroup })));

            return result.ContactGroups.FirstOrDefault();
        }
        
        private ContactGroupsResponse HandleResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = Client.JsonMapper.From<ContactGroupsResponse>(response.Body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }
    }

    public interface IContactCollection :
        IXeroUpdateEndpoint<ContactGroupsEndpoint, ContactGroup, ContactGroupsRequest, ContactGroupsResponse>
    {
        Task ClearAsync();
        Task AddAsync(Contact contact);
        Task AddRangeAsync(List<Contact> contacts);
        Task RemoveAsync(Guid guid);
    }

    public class ContactCollection  : XeroUpdateEndpoint<ContactGroupsEndpoint, ContactGroup, ContactGroupsRequest, ContactGroupsResponse>, IContactCollection
    {
        private readonly ContactGroup _group;
        private readonly XeroHttpClient _client;


        public ContactCollection(XeroHttpClient client, ContactGroup group)
            : base(client, "/api.xro/2.0/ContactGroups")
        {
            _group = group;
            _client = client;
        }

        public async Task ClearAsync()
        {
            var endpoint = string.Format("/api.xro/2.0/ContactGroups/{0}/Contacts", _group.Id);

            var response = HandleResponse(await Client
                .Client
                .DeleteAsync(endpoint));
        }

        public Task AddAsync(Contact contact)
        {
            var contactList = new List<Contact>();

            contactList.Add(contact);

            return AssignContactsAsync(_group, contactList);
        }

        public Task AddRangeAsync(List<Contact> contacts)
        {
            return AssignContactsAsync(_group,contacts);
        }

        public Task RemoveAsync(Guid guid)
        {
            return UnAssignContactAsync(_group, guid);
        }

        private async Task UnAssignContactAsync(ContactGroup contactGroup, Guid contactId)
        {
            var endpoint = string.Format("/api.xro/2.0/ContactGroups/{0}/Contacts/{1}", contactGroup.Id, contactId);

            var groups = HandleResponse(await Client
                .Client
                .DeleteAsync(endpoint));
        }

        private async Task AssignContactsAsync(ContactGroup contactGroup, List<Contact> contacts)
        {
            var endpoint = string.Format("/api.xro/2.0/ContactGroups/{0}/Contacts", contactGroup.Id);

            var groups = HandleResponse(await _client
                .Client
                .PutAsync(endpoint, _client.XmlMapper.To(contacts)))
                .ContactGroups;
        }

        private ContactGroupsResponse HandleResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = Client.JsonMapper.From<ContactGroupsResponse>(response.Body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }
    }
}
