using NUnit.Framework;
using System;
using System.Collections.Generic;
using Xero.Api.Core.Model;
using System.Threading.Tasks;

namespace CoreTests.Integration.ContactGroups
{
    [TestFixture]
    public class Update : ContactGroupsTest
    {
        [Test]
        public async Task Can_I_change_the_name_of_a_contactgroup()
        {
            var contactgroup = await Given_a_contactgroup();

            var newName = "Marketing Group" + Guid.NewGuid();

            contactgroup.Name = newName;
            
            var result = await Api.UpdateAsync(contactgroup);

            Assert.IsTrue(result.Name.StartsWith("Marketing Group"));
        }

        [Test]
        protected async Task Can_I_append_contacts_to_a_contactgroup()
        {
            var contactgroup = await Given_a_contactgroup();

            List<Contact> assign_1_contacts = new List<Contact>();

            assign_1_contacts.Add(await Given_a_contact());

            var contactCollection = await Api.ContactGroups.GetContactsAsync(contactgroup.Id);

            await contactCollection.AddRangeAsync(assign_1_contacts);

            List<Contact> assign_4_more_contacts = new List<Contact>();
            assign_4_more_contacts.Add(await Given_a_contact());
            assign_4_more_contacts.Add(await Given_a_contact());
            assign_4_more_contacts.Add(await Given_a_contact());
            assign_4_more_contacts.Add(await Given_a_contact());

            await contactCollection.AddRangeAsync(assign_4_more_contacts);
            
        }
    }
}
