using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Core;

namespace Xero.Api.Example.Mocks
{
    internal class Lister
    {
        private readonly IXeroCoreApi _api;

        public Lister(IXeroCoreApi api)
        {
            _api = api;
        }

        public async Task List()
        {
            Console.WriteLine("Your organisation is called {0}", (await _api.GetDefaultOrganisationAsync()).LegalName);

            Console.WriteLine("There are {0} accounts", (await _api.Accounts.FindAsync()).Count());
            Console.WriteLine("There are {0} journal entries", (await _api.Journals.FindAsync()).Count());
            Console.WriteLine("There are {0} Items", (await _api.Items.FindAsync()).Count());

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private async Task<int> GetTotalContactCount()
        {
            int count = (await _api.Contacts.FindAsync()).Count();
            int total = count;
            int page = 2;

            while(count == 100)
            {
                count = (await _api.Contacts.Page(page++).FindAsync()).Count();
                total += count;
            }

            return total;
        }

        private async Task<int> GetTotalInvoiceCount()
        {
            int count = (await _api.Invoices.FindAsync()).Count();
            int total = count;
            int page = 2;

            while (count == 100)
            {
                count = (await _api.Invoices.Page(page++).FindAsync()).Count();
                total += count;
            }

            return total;
        }

        private void ListReports(IEnumerable<string> reports, string name)
        {
            var enumerable = reports as IList<string> ?? reports.ToList();
            Console.WriteLine("There are {0} {1} reports", enumerable.Count(), name);
                
            if (enumerable.Any())
            {
                Console.WriteLine("Named:");
                foreach (var r in enumerable)
                {
                    Console.WriteLine("\t{0}", r);
                }
            }
        }
    }
}