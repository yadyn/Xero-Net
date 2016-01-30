using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace CoreTests.Integration.PurchaseOrders
{
    public class Create : ApiWrapperTest
    {
        [Test]
        public async Task Create_minimal_draft_purchase_order()
        {
            var contactId = await GetFirstContactId();

            var purchaseOrder = await Api.PurchaseOrders.CreateAsync(
                new PurchaseOrder
                {
                    Date = DateTime.Today,
                    Contact = new Contact { Id = contactId }
                }
            );

            Assert.True(purchaseOrder.Id != Guid.Empty);
            Assert.True(purchaseOrder.Status == PurchaseOrderStatus.Draft);
        }

        [Test]
        public async Task Create_authorised_purchase_order()
        {
            var contactId = await GetFirstContactId();

            var purchaseOrder = await Api.PurchaseOrders.CreateAsync(
                new PurchaseOrder
                {
                    Status = PurchaseOrderStatus.Authorised,
                    Date = DateTime.Today,
                    Contact = new Contact{Id = contactId },
                    LineItems = new List<LineItem>()
                    {
                        new LineItem
                        {
                            Description = "An item I want to purchase",
                            UnitAmount = 1,
                            Quantity = 1,

                        }
                    }
                }
            );

            Assert.True(purchaseOrder.Id != Guid.Empty);
            Assert.True(purchaseOrder.Status == PurchaseOrderStatus.Authorised);
        }

        private async Task<Guid> GetFirstContactId()
        {
            return (await Api.Contacts.FindAsync()).First().Id;
        }
    }
}
