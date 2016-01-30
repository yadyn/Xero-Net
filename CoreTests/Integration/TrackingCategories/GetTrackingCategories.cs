using System;
using System.Linq;
using NUnit.Framework;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreTests.Integration.TrackingCategories
{
    [TestFixture]
    public class GetTrackingCategories : TrackingCategoriesTest
    {
        [Test]
        public async Task Can_get_Tracking_Category()
        {
            await Given_two_TrackingCategorys();

            await Given_GetAll();

            List_contains_the_two_Tracking_Category();

            await Given_both_Tracking_Category_is_deleted();
        }
    }
}
