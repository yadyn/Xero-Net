using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Core.Endpoints;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Setup;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.RateLimiter;
using Xero.Api.Serialization;
using Organisation = Xero.Api.Core.Model.Organisation;

namespace Xero.Api.Core
{
    public class XeroCoreApi : XeroApi, IXeroCoreApi
    {
        private IOrganisationEndpoint OrganisationEndpoint { get; set; }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user,
            IJsonObjectMapper readMapper, IXmlObjectMapper writeMapper)
            : this(baseUri, auth, consumer, user, readMapper, writeMapper, null)
        {
        }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user, IJsonObjectMapper readMapper, IXmlObjectMapper writeMapper, IRateLimiter rateLimiter)
            : base(baseUri, auth, consumer, user, readMapper, writeMapper, rateLimiter)
        {
            Connect();
        }

        public XeroCoreApi(string baseUri, ICertificateAuthenticator auth, IConsumer consumer, IUser user,
            IJsonObjectMapper readMapper, IXmlObjectMapper writeMapper)
            : this(baseUri, auth, consumer, user, readMapper, writeMapper, null)
        {
        }

        public XeroCoreApi(string baseUri, ICertificateAuthenticator auth, IConsumer consumer, IUser user, IJsonObjectMapper readMapper, IXmlObjectMapper writeMapper, IRateLimiter rateLimiter)
            : base(baseUri, auth, consumer, user, readMapper, writeMapper, rateLimiter)
        {
            Connect();
        }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user)
            : this(baseUri, auth, consumer, user, null)
        {
        }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user, IRateLimiter rateLimiter)
            : this(baseUri, auth, consumer, user, new DefaultMapper(), new DefaultMapper())
        {
        }

        public XeroCoreApi(string baseUri, ICertificateAuthenticator auth, IConsumer consumer, IUser user)
            : this(baseUri, auth, consumer, user, null)
        {
        }

        public XeroCoreApi(string baseUri, ICertificateAuthenticator auth, IConsumer consumer, IUser user, IRateLimiter rateLimiter)
            : this(baseUri, auth, consumer, user, new DefaultMapper(), new DefaultMapper(), rateLimiter)
        {
        }

        public IAccountsEndpoint Accounts { get; private set; }
        public AllocationsEndpoint Allocations { get; private set; }
        public AttachmentsEndpoint Attachments { get; private set; }
        public IBankTransactionsEndpoint BankTransactions { get; private set; }
        public IBankTransfersEndpoint BankTransfers { get; private set; }
        public IBrandingThemesEndpoint BrandingThemes { get; private set; }
        public IContactsEndpoint Contacts { get; private set; }
        public IContactGroupsEndpoint ContactGroups { get; private set;}
        public ICreditNotesEndpoint CreditNotes { get; private set; }
        public ICurrenciesEndpoint Currencies { get; set; }
        public IEmployeesEndpoint Employees { get; private set; }
        public IExpenseClaimsEndpoint ExpenseClaims { get; private set; }
        public IFilesEndpoint Files { get; private set; }
        public IFoldersEndpoint Folders { get; private set; }
        public IInboxEndpoint Inbox { get; private set; }
        public IInvoicesEndpoint Invoices { get; private set; }
        public IItemsEndpoint Items { get; private set; }
        public IJournalsEndpoint Journals { get; protected set; }
        public ILinkedTransactionsEndpoint LinkedTransactions { get; private set; }
        public IManualJournalsEndpoint ManualJournals { get; private set; }
        public IOverpaymentsEndpoint Overpayments { get; private set; }
        public IPaymentsEndpoint Payments { get; private set; }
        public PdfEndpoint PdfFiles { get; private set; }
        public IPrepaymentsEndpoint Prepayments { get; private set; }
        public IPurchaseOrdersEndpoint PurchaseOrders { get; private set; }
        public IReceiptsEndpoint Receipts { get; private set; }
        public IRepeatingInvoicesEndpoint RepeatingInvoices { get; private set; }
        public IReportsEndpoint Reports { get; private set; }
        public SetupEndpoint Setup { get; private set; }
        public ITaxRatesEndpoint TaxRates { get; private set; }
        public ITrackingCategoriesEndpoint TrackingCategories { get; private set; }
        public IUsersEndpoint Users { get; private set; }
        

        private void Connect()
        {
            OrganisationEndpoint = new OrganisationEndpoint(Client);

            Accounts = new AccountsEndpoint(Client);
            Allocations = new AllocationsEndpoint(Client);
            Attachments = new AttachmentsEndpoint(Client);
            BankTransactions = new BankTransactionsEndpoint(Client);
            BankTransfers = new BankTransfersEndpoint(Client);
            BrandingThemes = new BrandingThemesEndpoint(Client);
            Contacts = new ContactsEndpoint(Client);
            ContactGroups = new ContactGroupsEndpoint(Client);
            CreditNotes = new CreditNotesEndpoint(Client);
            Currencies = new CurrenciesEndpoint(Client);
            Employees = new EmployeesEndpoint(Client);
            ExpenseClaims = new ExpenseClaimsEndpoint(Client);
            Files = new FilesEndpoint(Client);
            Folders = new FoldersEndpoint(Client);
            Inbox = new InboxEndpoint(Client);
            Invoices = new InvoicesEndpoint(Client);
            Items = new ItemsEndpoint(Client);
            Journals = new JournalsEndpoint(Client);
            LinkedTransactions = new LinkedTransactionsEndpoint(Client);
            ManualJournals = new ManualJournalsEndpoint(Client);
            Overpayments = new OverpaymentsEndpoint(Client);
            Payments = new PaymentsEndpoint(Client);
            PdfFiles = new PdfEndpoint(Client);
            Prepayments = new PrepaymentsEndpoint(Client);
            PurchaseOrders = new PurchaseOrdersEndpoint(Client);
            Receipts = new ReceiptsEndpoint(Client);
            RepeatingInvoices = new RepeatingInvoicesEndpoint(Client);
            Reports = new ReportsEndpoint(Client);
            Setup = new SetupEndpoint(Client);
            TaxRates = new TaxRatesEndpoint(Client);
            TrackingCategories = new TrackingCategoriesEndpoint(Client);
            Users = new UsersEndpoint(Client);
        }

        //public Organisation Organisation
        //{
        //    get
        //    {
        //        return OrganisationEndpoint.FindAsync().FirstOrDefault();
        //    }
        //}

        public async Task<Organisation> GetDefaultOrganisationAsync()
        {
            return (await OrganisationEndpoint.FindAsync()).FirstOrDefault();
        }

        public Task<IEnumerable<Invoice>> CreateAsync(IEnumerable<Invoice> items)
        {
            return Invoices.CreateAsync(items);
        }

        public Task<IEnumerable<Contact>> CreateAsync(IEnumerable<Contact> items)
        {
            return Contacts.CreateAsync(items);
        }
        
        public Task<IEnumerable<Account>> CreateAsync(IEnumerable<Account> items)
        {
            return Accounts.CreateAsync(items);
        }

        public Task<IEnumerable<Employee>> CreateAsync(IEnumerable<Employee> items)
        {
            return Employees.CreateAsync(items);
        }

        public Task<IEnumerable<ExpenseClaim>> CreateAsync(IEnumerable<ExpenseClaim> items)
        {
            return ExpenseClaims.CreateAsync(items);
        }

        public Task<IEnumerable<Receipt>> CreateAsync(IEnumerable<Receipt> items)
        {
            return Receipts.CreateAsync(items);
        }

        public Task<IEnumerable<CreditNote>> CreateAsync(IEnumerable<CreditNote> items)
        {
            return CreditNotes.CreateAsync(items);
        }

        public Task<IEnumerable<Item>> CreateAsync(IEnumerable<Item> items)
        {
            return Items.CreateAsync(items);
        }

        public Task<IEnumerable<ManualJournal>> CreateAsync(IEnumerable<ManualJournal> items)
        {
            return ManualJournals.CreateAsync(items);
        }

        public Task<IEnumerable<Payment>> CreateAsync(IEnumerable<Payment> items)
        {
            return Payments.CreateAsync(items);
        }

        public Task<IEnumerable<TaxRate>> CreateAsync(IEnumerable<TaxRate> items)
        {
            return TaxRates.CreateAsync(items);
        }

        public Task<IEnumerable<BankTransaction>> CreateAsync(IEnumerable<BankTransaction> items)
        {
            return BankTransactions.CreateAsync(items);
        }

        public Task<IEnumerable<BankTransfer>> CreateAsync(IEnumerable<BankTransfer> items)
        {
            return BankTransfers.CreateAsync(items);
        }

        public Task<Invoice> CreateAsync(Invoice item)
        {
            return Invoices.CreateAsync(item);
        }

        public Task<Contact> CreateAsync(Contact item)
        {
            return Contacts.CreateAsync(item);
        }

        public Task<Account> CreateAsync(Account item)
        {
            return Accounts.CreateAsync(item);
        }

        public Task<Employee> CreateAsync(Employee item)
        {
            return Employees.CreateAsync(item);
        }

        public Task<ExpenseClaim> CreateAsync(ExpenseClaim item)
        {
            return ExpenseClaims.CreateAsync(item);
        }

        public Task<Receipt> CreateAsync(Receipt item)
        {
            return Receipts.CreateAsync(item);
        }

        public Task<CreditNote> CreateAsync(CreditNote item)
        {
            return CreditNotes.CreateAsync(item);
        }

        public Task<Item> CreateAsync(Item item)
        {
            return Items.CreateAsync(item);
        }

        public Task<ManualJournal> CreateAsync(ManualJournal item)
        {
            return ManualJournals.CreateAsync(item);
        }

        public Task<Payment> CreateAsync(Payment item)
        {
            return Payments.CreateAsync(item);
        }

        public Task<TaxRate> CreateAsync(TaxRate item)
        {
            return TaxRates.CreateAsync(item);
        }

        public Task<BankTransaction> CreateAsync(BankTransaction item)
        {
            return BankTransactions.CreateAsync(item);
        }

        public Task<BankTransfer> CreateAsync(BankTransfer item)
        {
            return BankTransfers.CreateAsync(item);
        }

        public Task<ImportSummary> CreateAsync(Setup item)
        {
            return Setup.CreateAsync(item);
        }

        public Task<Invoice> UpdateAsync(Invoice item)
        {
            return Invoices.UpdateAsync(item);
        }

        public Task<Contact> UpdateAsync(Contact item)
        {
            return Contacts.UpdateAsync(item);
        }

        public Task<ContactGroup> UpdateAsync(ContactGroup item)
        {
            return ContactGroups.UpdateAsync(item);
        }

        public Task<Employee> UpdateAsync(Employee item)
        {
            return Employees.UpdateAsync(item);
        }

        public Task<ExpenseClaim> UpdateAsync(ExpenseClaim item)
        {
            return ExpenseClaims.UpdateAsync(item);
        }

        public Task<Receipt> UpdateAsync(Receipt item)
        {
            return Receipts.UpdateAsync(item);
        }

        public Task<CreditNote> UpdateAsync(CreditNote item)
        {
            return CreditNotes.UpdateAsync(item);
        }

        public Task<Item> UpdateAsync(Item item)
        {
            return Items.UpdateAsync(item);
        }

        public Task<ManualJournal> UpdateAsync(ManualJournal item)
        {
            return ManualJournals.UpdateAsync(item);
        }

        public Task<BankTransaction> UpdateAsync(BankTransaction item)
        {
            return BankTransactions.UpdateAsync(item);
        }

        public Task<BankTransfer> UpdateAsync(BankTransfer item)
        {
            return BankTransfers.UpdateAsync(item);
        }

        public Task<TaxRate> UpdateAsync(TaxRate item)
        {
            return TaxRates.UpdateAsync(item);
        }

        public Task<ImportSummary> UpdateAsync(Setup item)
        {
            return Setup.UpdateAsync(item);
        }

        public Task<TrackingCategory> UpdateAsync(TrackingCategory item)
        {
            return TrackingCategories.UpdateAsync(item);
        }
        
    }
}
