using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model.Status;
using Xero.Api.Infrastructure.ThirdParty.ServiceStack.Text;

namespace CoreTests.Integration.TrackingCategories
{
    [TestFixture]
    public class Delete : TrackingCategoriesTest
    {
        [Test]
        public async Task can_delete_a_Tracking_Category()
        {
            await Given_a_TrackingCategory();

            await Given_Tracking_Category_is_deleted();
        }

        [Test]
        public async Task can_delete_a_Tracking_Category_Option()
        {
            await Given_a_TrackingCategory_with_Options();

            await Given_Tracking_CategoryOption_is_deleted();


            await Given_Tracking_Category_is_deleted();
        }

    }
}
