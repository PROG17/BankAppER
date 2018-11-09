using BankAppER.Models;
using System.Linq;



namespace BankAppER.Business
{
    public interface IBankRepository
    {
        IQueryable<Customer> GetCustomers();

        Customer GetCustomerById(int id);

        IQueryable<Account> GetAccounts();

        Account GetAccountById(int id);

        // Operations
        bool Deposit(Transaction trans, Account account);    // Insättning

        bool Withdrawal(Transaction trans, Account account); // Uttag

    }
}
