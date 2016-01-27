using System;
using System.Linq;
using Xero.Api.Payroll.Australia.Model;
using Xero.Api.Payroll.Australia.Model.Status;
using Xero.Api.Payroll.Australia.Model.Types;
using Xero.Api.Payroll.Common.Model;
using PayRun = Xero.Api.Payroll.Australia.Model.PayRun;

namespace PayrollTests.AU.Integration.TimeSheets
{
    public abstract class TimesheetTest : ApiWrapperTest
    {

        public Timesheet Given_a_timesheet()
        {
            return Api.CreateAsync(new Timesheet
            {
                EmployeeId = the_employee_id(),
                StartDate = timesheet_start_date(),
                EndDate = timesheet_start_date().AddDays(6),
                Status = TimesheetStatus.Draft
            });
        }




        public Guid the_employee_id()
        {
            var emp = Api.CreateAsync(new Employee
            {
                FirstName = "Jack",
                LastName = "Gray",
                PayrollCalendarID = employee_payroll_calendar_id(),
                OrdinaryEarningsRateID = earning_rates_id()
            });
            return emp.Id;
        }



        public Guid employee_payroll_calendar_id()
        {
            var payruns = Api.PayRuns.Where("PayRunStatus == \"DRAFT\"").FindAsync();
            if (payruns.Any())
            {
                return payruns.FirstOrDefault().PayrollCalendarId;
            }
            else
            {
                var payroll_calendar_id = Api.CreateAsync(new PayrollCalendar
                {
                    Name = "New Calendar",
                    CalendarType = CalendarType.Weekly,
                    StartDate = DateTime.Today,
                    PaymentDate = DateTime.Today.AddDays(14)
                }).Id;

                Api.CreateAsync(new PayRun
                  {
                      PayrollCalendarId = payroll_calendar_id
                  });
                return payroll_calendar_id;
            }
        }



        public Guid earning_rates_id()
        {
            var er = Api.PayItems.FindAsync();
            return er.FirstOrDefault().EarningsRates.FirstOrDefault().Id;
        }



        public DateTime timesheet_start_date()
        {
            return Api.PayrollCalendars.FindAsync(employee_payroll_calendar_id()).StartDate.GetValueOrDefault();
        }


    }
}
