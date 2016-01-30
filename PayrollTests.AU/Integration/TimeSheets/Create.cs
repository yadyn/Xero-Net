using System;
using System.Collections.Generic;
using NUnit.Framework;
using Xero.Api.Payroll.Australia.Model.Status;
using Xero.Api.Payroll.Australia.Model;
using Xero.Api.Payroll.Common.Model;
using System.Threading.Tasks;

namespace PayrollTests.AU.Integration.TimeSheets
{
    [TestFixture]
    public class Create : TimesheetTest
    {
        [Test]
        public async Task create_timesheet()
        {
            var timesheet = await Api.CreateAsync(new Timesheet
            {
                EmployeeId = await the_employee_id(),
                StartDate = await timesheet_start_date(),
                EndDate = (await timesheet_start_date()).AddDays(6),
                Status = TimesheetStatus.Draft
            });
        }

        [Test]
        public async Task timesheet_with_lines()
        {
            var timesheet = await Api.CreateAsync(new Timesheet
            {
                EmployeeId = await the_employee_id(),
                StartDate = await timesheet_start_date(),
                EndDate = (await timesheet_start_date()).AddDays(6),
                Status = TimesheetStatus.Draft,
                TimesheetLines = new List<TimesheetLine>
                {
                    new TimesheetLine
                    {
                        EarningsRateId = await earning_rates_id(),
                        NumberOfUnits = new NumberOfUnits
                        {
                            7.5m, 7.5m, 7.5m, 7.5m, 7.5m, 0, 0
                        }
                    }
                }
            });
        }
    }
}
