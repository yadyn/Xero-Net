using System.Linq;
using NUnit.Framework;
using Xero.Api.Core.Model.Types;

namespace CoreTests.Integration.Overpayments
{
    [TestFixture]
    public class Find : ApiWrapperTest
    {
        [Test]
        public void find_all()
        {
            var overpayments = Api.Overpayments.FindAsync();
            Assert.Greater(overpayments.Count(), 0);
        }

        [Test]
        public void find_all_receive_overpayments()
        {
            var overpayments = Api.Overpayments.Where("Type == \"RECEIVE-OVERPAYMENT\"").FindAsync();
            Assert.True(overpayments.All(p => p.Type == OverpaymentType.ReceiveOverpayment));
        }

        [Test]
        public void find_all_spend_overpayments()
        {
            var overpayments = Api.Overpayments.Where("Type == \"SPEND-OVERPAYMENT\"").FindAsync();
            Assert.True(overpayments.All(p => p.Type == OverpaymentType.SpendOverpayment));
        }
    }
}
