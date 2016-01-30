﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace CoreTests.Integration.Contacts
{
    [TestFixture]
    public class Create : ContactsTest
    {
        [Test]
        public async Task create_contact()
        {
            var contact = await Given_a_contact();

            Assert.IsTrue(contact.Name.StartsWith("Peter"));
        }

        [Test]
        public async Task create_many_contact()
        {
            var contacts = await Api.CreateAsync(new List<Contact>
            {
                new Contact
                {
                    Name = "John " + Random.GetRandomString(10)
                },
                new Contact
                {
                    Name = "Paul" + Random.GetRandomString(10)
                },
                new Contact
                {
                    Name = "George" + Random.GetRandomString(10)
                },
                new Contact
                {
                    Name = "Ringo" + Random.GetRandomString(10)
                }
            });

            Assert.AreEqual(4, contacts.Count());
        }

        [Test]
        public async Task create_complex_contact()
        {
            Api.Contacts.SummarizeErrors(true);

            var expectedAccountNumber = "AccountNumber" + Random.GetRandomString(10);


            var contact = await Api.CreateAsync(new Contact
            {
                Name = "24 locks " + Random.GetRandomString(10),
                FirstName = "Ben",
                LastName = "Bowden",
                EmailAddress = "ben.bowden@24locks.com",
                AccountNumber = expectedAccountNumber,

                ContactPersons = new List<ContactPerson>
                {
                    new ContactPerson
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        EmailAddress = "john.smith@24locks.com",
                        IncludeInEmails = true
                    }
                }
            });

            Assert.AreEqual("Ben", contact.FirstName);
            Assert.AreEqual("John", contact.ContactPersons[0].FirstName);
            Assert.AreEqual(expectedAccountNumber, contact.AccountNumber);
        }

        [Test]
        public async Task create_contact_with_tracking()
        {
            //Test will create a Tracking Category and use it if there are none available.
            var TCName = "Luke Skywalker" + Guid.NewGuid();
            var OptionName = "Yoda " + Guid.NewGuid();

            var trackingCat = await findOrCreateTC(TCName, OptionName);

            var contact = await Api.CreateAsync(new Contact
            {
                Name = "24 locks " + Random.GetRandomString(10),
                FirstName = "Ben",
                LastName = "Bowden",
                EmailAddress = "ben.bowden@24locks.com",
                PurchasesTrackingCategories = new List<PurchasesTrackingCategory>(){
                    new PurchasesTrackingCategory()
                    {
                        Name = trackingCat.Name,
                        Option = trackingCat.Options.FirstOrDefault().Name
                    }
                },
                ContactPersons = new List<ContactPerson>
                {
                    new ContactPerson
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        EmailAddress = "john.smith@24locks.com",
                        IncludeInEmails = true
                    }
                }
            });

            await deleteCreatedTC(trackingCat);

            Assert.AreEqual("Ben", contact.FirstName);
            Assert.AreEqual("John", contact.ContactPersons[0].FirstName);
            Assert.True(TCName == contact.PurchasesTrackingCategories.FirstOrDefault().Name || trackingCat.Name == contact.PurchasesTrackingCategories.FirstOrDefault().Name);
            Assert.True(OptionName == contact.PurchasesTrackingCategories.FirstOrDefault().Option || contact.PurchasesTrackingCategories.FirstOrDefault().Option == trackingCat.Options.FirstOrDefault().Name);
        }
    }
}
