using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xero.Api.Payroll.Australia.Model;
using Xero.Api.Payroll.Australia.Model.Types;

namespace PayrollTests.AU.Integration.Employees
{
    [TestFixture]
    public class Update : EmployeesTest
    {
        [Test]
        public async Task update_employee_with_super_memberhsip()
        {
            var emp = await Given_an_employee(false);

            var updated_emp = await Api.UpdateAsync(new Employee
            {
                Id = emp.Id,
                SuperMemberships = new List<SuperMembership>
                {
                    new SuperMembership
                    {
                        SuperFundId = await super_fund_id(),
                        EmployeeNumber = 3424232
                    }
                }
                
            });
            Assert.IsTrue(updated_emp.Id != Guid.Empty);
        }

    }
}
