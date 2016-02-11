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
    public interface ITrackingCategoriesEndpoint : IXeroUpdateEndpoint<TrackingCategoriesEndpoint, TrackingCategory, TrackingCategoriesRequest, TrackingCategoriesResponse>
    {
        Task<IOptionCollection> GetOptionsByIDAsync(Guid id);
        Task<List<TrackingCategory>> GetAllAsync();
        TrackingCategoriesEndpoint IncludeArchived(bool include);
        Task<TrackingCategory> GetByIDAsync(Guid id);
        Task<TrackingCategory> DeleteAsync(TrackingCategory trackingCategory);
        Task<Option> DeleteTrackingOptionAsync(TrackingCategory trackingCategory, Option option);
        Task<TrackingCategory> AddAsync(TrackingCategory trackingCategory);
    }

    public class TrackingCategoriesEndpoint : XeroUpdateEndpoint<TrackingCategoriesEndpoint, TrackingCategory, TrackingCategoriesRequest, TrackingCategoriesResponse>, ITrackingCategoriesEndpoint
    {
        public TrackingCategoriesEndpoint(XeroHttpClient client) :
            base(client, "/api.xro/2.0/TrackingCategories")
        {
        }

        public TrackingCategoriesEndpoint IncludeArchived(bool include)
        {
            if (include)
            {
                AddParameter("includeArchived", true);
            }

            return this;
        }

        //public IOptionCollection this[Guid id]
        //{
        //    get
        //    {
        //        var endpoint = string.Format("/api.xro/2.0/TrackingCategories/{0}", id);

        //        var trackingCat = HandleResponse(Client.Client.GetAsync(endpoint, null)).TrackingCategories.FirstOrDefault();

        //        var collection = new OptionCollection(Client, trackingCat);

        //        return collection;
        //    }
        //}

        public async Task<TrackingCategory> GetByIDAsync(Guid id)
        {
            var endpoint = string.Format("/api.xro/2.0/TrackingCategories/{0}", id);

            var trackingCat = HandleResponse(await Client.Client.GetAsync(endpoint, null)).TrackingCategories.FirstOrDefault();

            return trackingCat;
        }

        public async Task<IOptionCollection> GetOptionsByIDAsync(Guid id)
        {
            var endpoint = string.Format("/api.xro/2.0/TrackingCategories/{0}", id);

            var trackingCat = HandleResponse(await Client.Client.GetAsync(endpoint, null)).TrackingCategories.FirstOrDefault();

            var collection = new OptionCollection(Client, trackingCat);

            return collection;
        }

        public async Task<List<TrackingCategory>> GetAllAsync()
        {
            var endpoint = string.Format("/api.xro/2.0/TrackingCategories");

            List<TrackingCategory> trackingCats = (HandleResponse(await Client.Client.GetAsync(endpoint, QueryString))).TrackingCategories.ToList();

            return trackingCats;
        } 

        public async Task<TrackingCategory> AddAsync(TrackingCategory trackingCategory)
        {
            var endpoint = string.Format("/api.xro/2.0/TrackingCategories");

            var track = HandleResponse(await Client
                .Client
                .PutAsync(endpoint, Client.XmlMapper.To(new List<TrackingCategory> { trackingCategory })));

            return track.Values.FirstOrDefault();
        }

        public override async Task<TrackingCategory> UpdateAsync(TrackingCategory trackingCategory)
        {
            var endpoint = string.Format("/api.xro/2.0/TrackingCategories/{0}", trackingCategory.Id.ToString());

            trackingCategory.Options = null;

            var track = HandleResponse(await Client
                .Client
                .PostAsync(endpoint, Client.XmlMapper.To(new List<TrackingCategory> { trackingCategory })));

            return track.Values.FirstOrDefault();
        }

        public async Task<TrackingCategory> DeleteAsync(TrackingCategory trackingCategory)
        {
            var endpoint = string.Format("/api.xro/2.0/TrackingCategories/{0}", trackingCategory.Id);

            var track = HandleResponse(await Client
                .Client
                .DeleteAsync(endpoint));

            return track.Values.FirstOrDefault();
        }

        public async Task<Option> DeleteTrackingOptionAsync(TrackingCategory trackingCategory, Option option)
        {
            var endpoint = string.Format("/api.xro/2.0/TrackingCategories/{0}/Options/{1}", trackingCategory.Id, option.Id);

            var track = HandleOptionResponse(await Client
                .Client
                .DeleteAsync(endpoint));

            return track.Values.FirstOrDefault();
        }

        private TrackingCategoriesResponse HandleResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = Client.JsonMapper.From<TrackingCategoriesResponse>(response.Body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }

        private OptionsResponse HandleOptionResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = Client.JsonMapper.From<OptionsResponse>(response.Body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }
    }

    public interface IOptionCollection :
        IXeroUpdateEndpoint
            <TrackingCategoriesEndpoint, TrackingCategory, TrackingCategoriesRequest, TrackingCategoriesResponse>
    {
        Task<List<Option>> AddAsync(Option option);
        Task<List<Option>> AddAsync(List<Option> options);
        Task<Option> UpdateOptionAsync(Option option);
    }

    public class OptionCollection :
        XeroUpdateEndpoint<TrackingCategoriesEndpoint, TrackingCategory, TrackingCategoriesRequest, TrackingCategoriesResponse>, IOptionCollection
    {
        public TrackingCategory _trackingCat;
        private readonly XeroHttpClient _client;

        public OptionCollection(XeroHttpClient client, TrackingCategory trackingCat)
            : base(client, "/api.xro/2.0/TrackingCategories")
        {
            _trackingCat = trackingCat;
            _client = client;
        }

        public Task<List<Option>> AddAsync(Option option)
        {
            List<Option> options = new List<Option>();
 
            options.Add(option);

            return AssignOptionsAsync(_trackingCat, options);
        }

        public Task<List<Option>> AddAsync(List<Option> options)
        {
            return AssignOptionsAsync(_trackingCat, options);
        }

        private async Task<List<Option>> AssignOptionsAsync(TrackingCategory category, List<Option> options)
        {
            var endpoint = string.Format("/api.xro/2.0/trackingcategories/{0}/options", category.Id);

            var response = HandleResponse(await _client
                 .Client
                 .PutAsync(endpoint, _client.XmlMapper.To(options)));

            return response.Values.ToList();
        }

        private OptionsResponse HandleResponse(Infrastructure.Http.Response response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = Client.JsonMapper.From<OptionsResponse>(response.Body);
                return result;
            }

            Client.HandleErrors(response);

            return null;
        }

        public async Task<Option> UpdateOptionAsync(Option option)
        {
            var endpoint = string.Format("/api.xro/2.0/trackingcategories/{0}/options/{1}", _trackingCat.Id, option.Id);


            List<Option> Options = new List<Option>();
            Options.Add(option);
            
            var response = HandleResponse(await _client
                 .Client
                 .PostAsync(endpoint, _client.XmlMapper.To(Options)));

            return response.Options.FirstOrDefault();
        }
    }
}