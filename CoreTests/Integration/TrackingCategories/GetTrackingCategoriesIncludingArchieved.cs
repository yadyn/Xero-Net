using System;
using System.Linq;
using NUnit.Framework;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using System.Collections.Generic;
using Xero.Api.Core.Model.Types;
using System.Threading.Tasks;

namespace CoreTests.Integration.TrackingCategories
{
    [TestFixture]
    public class GetTrackingCategoriesIncludingArchieved : TrackingCategoriesTest
    {
        [Test]
        public async Task Can_get_Tracking_Category_including_archieved()
        {
            await Given_a_TrackingCategory_with_Options();

            await Given_approved_invoice_with_tracking_option();

            await Given_Tracking_Category_is_Archived();

            await Given_GetAll_with_Archived();

            Then_Archieved_Tracking_Category_is_in_list();

            await Given_Invoice_is_voided();
            await Given_Tracking_Category_is_deleted();
        }
    }
}
