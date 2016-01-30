using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Endpoints;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace CoreTests.Integration.TrackingCategories
{
    [TestFixture]
    public class ArchiveTrackingCategory : TrackingCategoriesTest
    {
        [Test]
        public async Task Can_archieve_tracking_category()
        {
            await Given_a_TrackingCategory_with_Options();

            await Given_approved_invoice_with_tracking_option();

            await Given_Tracking_Category_is_Archived();

            await Given_Invoice_is_voided();
            await Given_Tracking_Category_is_deleted();
        }
    }
}
