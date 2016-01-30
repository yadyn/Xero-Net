using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PayrollTests.US.Integration.Employees
{
    [TestFixture]
    public class Find : EmployeesTest
    {
        [Test]
        public async Task find_all_employees()
        {
            var emp = await Api.Employees.FindAsync();
            Assert.Greater(emp.Count(), 0);
        }

        [Test]
        public async Task find_by_page()
        {
            var emp = await Api.Employees.Page(1).FindAsync();
            Assert.Greater(emp.Count(), 0);
        }

        [Test]
        public async Task find_by_id()
        {
            var expected = (await Given_an_employee()).Id;
            var employee = await Api.Employees.FindAsync(expected);
            Assert.AreEqual(expected, employee.Id);
        }

        

        //[Test]
        //public void find_by_status()
        //{
        //    var terminated = given_terminated_employee();

        //    Assert.True(Api.Employees
        //        .Where("Status == \"TERMINATED\"")
        //        .Find()
        //        .Select(p => p.Id)
        //        .Any(p => p == terminated.Id));
        //}
    }
}
