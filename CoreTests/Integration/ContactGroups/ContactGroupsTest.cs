using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xero.Api.Core.Model;

namespace CoreTests.Integration.ContactGroups
{
    public abstract class ContactGroupsTest : ApiWrapperTest
    {
        // need a contact in the system to use contact groups with.
        protected Contact Given_a_contact()
        {
            var contact = Api.CreateAsync(new Contact
            {
                Name = "Peter " + Guid.NewGuid().ToString("N")
            });
            return contact;
        }
        protected ContactGroup Given_a_contactgroup()
        {
            var group = Api.ContactGroups.AddAsync(new ContactGroup
            {
                Name = "Nice People " + Guid.NewGuid()

            });

            return group;
        }
        
      
    }
}

