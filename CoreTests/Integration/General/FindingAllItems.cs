using NUnit.Framework;

namespace CoreTests.Integration.General
{
    [TestFixture]
    public class FindingAllItems : ApiWrapperTest
    {
        [Test]
        public void get_accounts()
        {
            Assert.DoesNotThrow(() => Api.Accounts.FindAsync());
        }

        [Test]
        public void get_bank_transactions()
        {
            Assert.DoesNotThrow(() => Api.BankTransactions.FindAsync());
        }

        [Test]
        public void get_bank_transfers()
        {
            Assert.DoesNotThrow(() => Api.BankTransfers.FindAsync());
        }

        [Test]
        public void get_branding_themes()
        {
            Assert.DoesNotThrow(() => Api.BrandingThemes.FindAsync());
        }

        [Test]
        public void get_contacts()
        {
            Assert.DoesNotThrow(() => Api.Contacts.FindAsync());
        }

        [Test]
        public void get_credit_notes()
        {
            Assert.DoesNotThrow(() => Api.CreditNotes.FindAsync());
        }

        [Test]
        public void get_currencies()
        {
            Assert.DoesNotThrow(() => Api.Currencies.FindAsync());
        }

        [Test]
        public void get_employees()
        {
            Assert.DoesNotThrow(() => Api.Employees.FindAsync());
        }

        [Test]
        public void get_expense_claims()
        {
            Assert.DoesNotThrow(() => Api.ExpenseClaims.FindAsync());
        }

        [Test]
        public void get_invoices()
        {
            Assert.DoesNotThrow(() => Api.Invoices.FindAsync());
        }

        [Test]
        public void get_items()
        {
            Assert.DoesNotThrow(() => Api.Items.FindAsync());
        }

        [Test]
        public void get_journals()
        {
            Assert.DoesNotThrow(() => Api.Journals.FindAsync());
        }

        [Test]
        public void get_manual_journals()
        {
            Assert.DoesNotThrow(() => Api.ManualJournals.FindAsync());
        }

        [Test]
        public void get_payments()
        {
            Assert.DoesNotThrow(() => Api.Payments.FindAsync());
        }

        [Test]
        public void get_receipts()
        {
            Assert.DoesNotThrow(() => Api.Receipts.FindAsync());
        }

        [Test]
        public void get_repeating_invoices()
        {
            Assert.DoesNotThrow(() => Api.RepeatingInvoices.FindAsync());
        }

        [Test]
        public void get_tax_rates()
        {
            Assert.DoesNotThrow(() => Api.TaxRates.FindAsync());
        }

        [Test]
        public void get_tracking_categories()
        {
            Assert.DoesNotThrow(() => Api.TrackingCategories.FindAsync());
        }

        [Test]
        public void get_users()
        {
            Assert.DoesNotThrow(() => Api.Users.FindAsync());
        }        
    }
}
