﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model;

namespace CoreTests.Integration.ContactGroups
{
    [TestFixture]
    public class Delete : ContactGroupsTest
    {
        [Test]
        public async Task Can_I_remove_the_contactgroup()
        {
            var contactgroup = await Given_a_contactgroup();

            contactgroup.Status = "DELETED";

            var group = await Api.UpdateAsync(contactgroup);

            Assert.IsTrue(group.Status == "DELETED" );
        }

        [Test]
        public async Task Can_I_empty_the_contactgroup_of_contacts()
        {
            var contactgroup = await Given_a_contactgroup();

            List<Contact> contacts = new List<Contact>();
            contacts.Add(await Given_a_contact());
            contacts.Add(await Given_a_contact());
            contacts.Add(await Given_a_contact());
            contacts.Add(await Given_a_contact());
            contacts.Add(await Given_a_contact());

            var contactCollection = await Api.ContactGroups.GetContactsAsync(contactgroup.Id);

            await contactCollection.AddRangeAsync(contacts);

            await contactCollection.ClearAsync();
        }
    }
}
