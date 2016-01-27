using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CoreTests.Integration.Reports
{
    [TestFixture]
    public class Find : ApiWrapperTest
    {

        [Test]
        public void find_all()
        {
            var reports = Api.Reports.FindAsync();
            Assert.IsNotNull(reports);
        }

        [Test]
        public void find_gst_report()
        {
            var reports = Api.Reports.FindAsync("BalanceSheet");
            Assert.IsNotNull(reports);
        }

        [Test]
        public void find_PL_report()
        {
            var reports = Api.Reports.GetProfitAndLossAsync(DateTime.Now.AddDays(-50));
            Assert.IsNotNull(reports);
        }

        [Test]
        public void find_BudgetSummary_report()
        {
            var reports = Api.Reports.GetBudgetSummaryAsync(DateTime.Now.AddDays(-50));
            Assert.IsNotNull(reports);
        }
    }
}
