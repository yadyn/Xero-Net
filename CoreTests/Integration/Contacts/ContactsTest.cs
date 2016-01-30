using System;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace CoreTests.Integration.Contacts
{
    public abstract  class ContactsTest : ApiWrapperTest
    {
        private TrackingCategory trackingCat;
        private bool wasTCCreated = false;

        protected Task<Contact> Given_a_contact()
        {
            return Api.CreateAsync(new Contact
            {
                Name = "Peter " + Random.GetRandomString(10)
            });
        }

        protected async Task<TrackingCategory> findOrCreateTC(string OptionName, string TCName)
        {
            trackingCat = (await Api.TrackingCategories.GetAllAsync()).FirstOrDefault();
            if (trackingCat == null || trackingCat.Options.FirstOrDefault() == null)
            {
                var option1 = new Option()
                {
                    Id = Guid.Empty,
                    Name = OptionName,
                    Status = TrackingOptionStatus.Active
                };


                trackingCat = await Api.TrackingCategories.CreateAsync(new TrackingCategory()
                {
                    Name = TCName,
                    Status = TrackingCategoryStatus.Active
                });


                await (await Api.TrackingCategories.GetOptionsByIDAsync(trackingCat.Id)).AddAsync(option1);

                trackingCat = await Api.TrackingCategories.GetByIDAsync(trackingCat.Id);
                wasTCCreated = true;
            }
            return trackingCat;
        }

        protected async Task deleteCreatedTC(TrackingCategory tc)
        {
            if (wasTCCreated)
            {
                await Api.TrackingCategories.DeleteAsync(trackingCat);
                wasTCCreated = false;
            }
        }
    }
}
