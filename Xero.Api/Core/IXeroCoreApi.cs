using System.Collections.Generic;
using System.Threading.Tasks;
using Xero.Api.Core.Endpoints;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Setup;
using Organisation = Xero.Api.Core.Model.Organisation;

namespace Xero.Api.Core
{
    public interface IXeroCoreApi
    {
        IAccountsEndpoint Accounts { get; }
        AllocationsEndpoint Allocations { get; }
        AttachmentsEndpoint Attachments { get; }
        IBankTransactionsEndpoint BankTransactions { get; }
        IBankTransfersEndpoint BankTransfers { get; }
        IBrandingThemesEndpoint BrandingThemes { get; }
        IContactsEndpoint Contacts { get; }
        IContactGroupsEndpoint ContactGroups { get; }
        ICreditNotesEndpoint CreditNotes { get; }
        ICurrenciesEndpoint Currencies { get; set; }
        IEmployeesEndpoint Employees { get; }
        IExpenseClaimsEndpoint ExpenseClaims { get; }
        IFilesEndpoint Files { get; }
        IFoldersEndpoint Folders { get; }
        IInboxEndpoint Inbox { get; }
        IInvoicesEndpoint Invoices { get; }
        IItemsEndpoint Items { get; }
        IJournalsEndpoint Journals { get; }
        ILinkedTransactionsEndpoint LinkedTransactions { get; }
        IManualJournalsEndpoint ManualJournals { get; }
        IOverpaymentsEndpoint Overpayments { get; }
        IPaymentsEndpoint Payments { get; }
        PdfEndpoint PdfFiles { get; }
        IPrepaymentsEndpoint Prepayments { get; }
        IPurchaseOrdersEndpoint PurchaseOrders { get; }
        IReceiptsEndpoint Receipts { get; }
        IRepeatingInvoicesEndpoint RepeatingInvoices { get; }
        IReportsEndpoint Reports { get; }
        SetupEndpoint Setup { get; }
        ITaxRatesEndpoint TaxRates { get; }
        ITrackingCategoriesEndpoint TrackingCategories { get; }
        IUsersEndpoint Users { get; }
        Task<Organisation> GetDefaultOrganisationAsync();
        string BaseUri { get; }
        string UserAgent { get; set; }
        Task<IEnumerable<Invoice>> CreateAsync(IEnumerable<Invoice> items);
        Task<IEnumerable<Contact>> CreateAsync(IEnumerable<Contact> items);
        Task<IEnumerable<Account>> CreateAsync(IEnumerable<Account> items);
        Task<IEnumerable<Employee>> CreateAsync(IEnumerable<Employee> items);
        Task<IEnumerable<ExpenseClaim>> CreateAsync(IEnumerable<ExpenseClaim> items);
        Task<IEnumerable<Receipt>> CreateAsync(IEnumerable<Receipt> items);
        Task<IEnumerable<CreditNote>> CreateAsync(IEnumerable<CreditNote> items);
        Task<IEnumerable<Item>> CreateAsync(IEnumerable<Item> items);
        Task<IEnumerable<ManualJournal>> CreateAsync(IEnumerable<ManualJournal> items);
        Task<IEnumerable<Payment>> CreateAsync(IEnumerable<Payment> items);
        Task<IEnumerable<TaxRate>> CreateAsync(IEnumerable<TaxRate> items);
        Task<IEnumerable<BankTransaction>> CreateAsync(IEnumerable<BankTransaction> items);
        Task<IEnumerable<BankTransfer>> CreateAsync(IEnumerable<BankTransfer> items);
        Task<Invoice> CreateAsync(Invoice item);
        Task<Contact> CreateAsync(Contact item);
        Task<Account> CreateAsync(Account item);
        Task<Employee> CreateAsync(Employee item);
        Task<ExpenseClaim> CreateAsync(ExpenseClaim item);
        Task<Receipt> CreateAsync(Receipt item);
        Task<CreditNote> CreateAsync(CreditNote item);
        Task<Item> CreateAsync(Item item);
        Task<ManualJournal> CreateAsync(ManualJournal item);
        Task<Payment> CreateAsync(Payment item);
        Task<TaxRate> CreateAsync(TaxRate item);
        Task<BankTransaction> CreateAsync(BankTransaction item);
        Task<BankTransfer> CreateAsync(BankTransfer item);
        Task<ImportSummary> CreateAsync(Setup item);
        Task<Invoice> UpdateAsync(Invoice item);
        Task<Contact> UpdateAsync(Contact item);
        Task<ContactGroup> UpdateAsync(ContactGroup item);
        Task<Employee> UpdateAsync(Employee item);
        Task<ExpenseClaim> UpdateAsync(ExpenseClaim item);
        Task<Receipt> UpdateAsync(Receipt item);
        Task<CreditNote> UpdateAsync(CreditNote item);
        Task<Item> UpdateAsync(Item item);
        Task<ManualJournal> UpdateAsync(ManualJournal item);
        Task<BankTransaction> UpdateAsync(BankTransaction item);
        Task<BankTransfer> UpdateAsync(BankTransfer item);
        Task<TaxRate> UpdateAsync(TaxRate item);
        Task<ImportSummary> UpdateAsync(Setup item);
        Task<TrackingCategory> UpdateAsync(TrackingCategory item);
    }
}