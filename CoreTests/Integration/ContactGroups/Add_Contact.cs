using NUnit.Framework;
using System;
using Xero.Api.Infrastructure.Exceptions;
using System.Threading.Tasks;

namespace CoreTests.Integration.ContactGroups
{
    [TestFixture]
    public class Add_Contact : ContactGroupsTest
    {
        [Test]
        public async Task Can_I_add_a_contact_to_a_contactgroup()
        {
            var contactgroup = await Given_a_contactgroup();

            var contactGroups = await Api.ContactGroups.GetContactsAsync(contactgroup.Id);

            await contactGroups.AddAsync(await Given_a_contact());
        }

        [Test]
        public void But_not_with_a_group_like_this()
        {
            Assert.Throws<NotFoundException>(async () => await (await Api.ContactGroups.GetContactsAsync(Guid.Empty)).AddAsync(await Given_a_contact()));
        }
    }
}
