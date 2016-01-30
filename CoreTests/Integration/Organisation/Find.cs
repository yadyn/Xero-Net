using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model.Types;

namespace CoreTests.Integration.Organisation
{
    [TestFixture]
    public class Find : ApiWrapperTest
    {
        [Test]
        public async Task can_get_the_organisation_sales_tax_basis()
        {
            var organisation = await Api.GetDefaultOrganisationAsync();
            var test = organisation.SalesTaxBasisType;

            Assert.True(Enum.IsDefined(typeof(SalesTaxBasisType), test));
        }

        [Test]
        public async Task can_get_the_organisation_sales_tax_period()
        {
            var organisation = await Api.GetDefaultOrganisationAsync();
            var test = organisation.SalesTaxPeriod;

            Assert.True(Enum.IsDefined(typeof(SalesTaxPeriodType), test));
        }
    }
}
