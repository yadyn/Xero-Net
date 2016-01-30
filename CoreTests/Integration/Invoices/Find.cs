using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model.Types;

namespace CoreTests.Integration.Invoices
{
    [TestFixture]
    public class Find : InvoicesTest
    {
        [Test]
        public async Task find_by_page()
        {
            await Given_an_invoice();
            var invoices = await Api.Invoices.Page(1).FindAsync();
            
            Assert.Greater(invoices.Count(), 0);
        }

        [Test]
        public async Task find_by_id()
        {
            var expected  = (await Given_an_invoice()).Id;
            var id = (await Api.Invoices.FindAsync(expected)).Id;

            Assert.AreEqual(expected, id);
        }

        [Test]
        public async Task find_by_value()
        {
            await Given_an_invoice();
            var invoices = (await Api.Invoices
                .Where("Type == \"ACCREC\"")
                .FindAsync())
                .ToList();

            Assert.True(invoices.Any());
            Assert.True(invoices.All(p => p.Type == InvoiceType.AccountsReceivable));            
        }

        [Test]
        public async Task find_by_due_date()
        {
            await Given_an_invoice();

            var today = DateTime.UtcNow;

            var invoices = (await Api.Invoices
                .Where(string.Format("DueDate > DateTime({0},{1},{2})", today.Year, today.Month, today.Day ))
                .FindAsync())
                .ToList();

            Assert.True(invoices.Any());           
        }

        [Test]
        public async Task order_by_type()
        {
            var invoices = await Api.Invoices.OrderByDescending("Type").FindAsync();

            Assert.AreEqual(InvoiceType.AccountsReceivable, invoices.First().Type);
        }
    }
}
