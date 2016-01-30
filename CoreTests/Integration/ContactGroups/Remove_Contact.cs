using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CoreTests.Integration.ContactGroups
{
    [TestFixture]
    public class Remove_Contact : ContactGroupsTest
    {
        [Test]
        public async Task Can_remove_a_contact_from_a_contact_group()
        {
            var contactgroup = await Given_a_contactgroup();

            var contact = await Given_a_contact();

            var contactCollection = await Api.ContactGroups.GetContactsAsync(contactgroup.Id);

            await contactCollection.AddAsync(contact);

            await contactCollection.RemoveAsync(contact.Id);

        }
    }
}
