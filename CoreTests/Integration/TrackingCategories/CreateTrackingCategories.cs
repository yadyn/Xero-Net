using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace CoreTests.Integration.TrackingCategories
{
    [TestFixture]
    public class CreateTrackingCategories : TrackingCategoriesTest
    {
        [Test]
        public async Task Can_create_a_Tracking_Category()
        {
            await Given_a_TrackingCategory();

            await Given_Tracking_Category_is_deleted();
        }
    }
}
