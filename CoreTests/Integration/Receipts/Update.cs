using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model.Status;

namespace CoreTests.Integration.Receipts
{
    [TestFixture]
    public class Update : ReceiptTest
    {
        [Test]
        public async Task delete_receipt()
        {
            var contact = Random.GetRandomString(10);
            var description = Random.GetRandomString(30);
            const ReceiptStatus expected = ReceiptStatus.Deleted;
            const decimal value = 13.8m;

            var receipt = await Given_a_receipt((await Api.Users.FindAsync()).First().Id, contact, description, value, "420");
            receipt.Status = expected;
            var deletedReceipt = Api.UpdateAsync(receipt);

            Assert.AreEqual(expected, deletedReceipt.Status);
        }
    }
}