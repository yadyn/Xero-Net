using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using Xero.Api.Core.Endpoints;
using Xero.Api.Core.Model;

namespace Xero.Api.Example.Mocks.ContactManagerAndTests
{
    [TestFixture]
    public class ContactManagerTests : ContactManagerTestBase
    {
        [TestFixtureSetUp]
        public void ContactManagerTestsSetUp()
        {
            SetUp();
        }

        [Test]
        public async Task ReturnsContactsFromTatooine()
        {
            var fromTatooine = new ContactManager(await ContactsEndpoint.FindAsync()).ContactsFromTatooine();

            Assert.True(fromTatooine.All(c => c.Addresses.First().Country == "Tatooine"));
            Assert.True(fromTatooine.Count() == 2);
        }

        [Test]
        public async Task ReturnsContactsFromDagobah()
        {
            var fromDagobah = new ContactManager(await ContactsEndpoint.FindAsync()).ContactsFromDagobah();

            Assert.True(fromDagobah.All(c => c.Addresses.First().Country == "Dagobah"));
            Assert.True(fromDagobah.First().Name == "Minch Yoda");
        }
    }

    public class ContactManagerTestBase
    {
        protected IContactsEndpoint ContactsEndpoint;

        protected void SetUp()
        {
            ContactsEndpoint = MockRepository.GenerateStub<IContactsEndpoint>();

            var contacts = new List<Contact>()
            {
                ContactMaker("Minch Yoda","Dagobah"),
                ContactMaker("Luke Skywalker","Tatooine"),
                ContactMaker("Obi Wan Kenobi","Tatooine"),
                ContactMaker("Darth Vader","The Death Star"),
            };

            ContactsEndpoint.Stub(a => a.FindAsync()).Return(Task.FromResult<IEnumerable<Contact>>(contacts));
        }

        private Contact ContactMaker(string name, string country)
        {
            return new Contact()
            {
                Name = name,
                Addresses = new List<Address>
                {
                    new Address()
                    {
                        Country = country
                    }
                }
            };
        }
    }
}
