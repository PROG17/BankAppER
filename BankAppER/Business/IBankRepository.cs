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
        bool Deposit(Transaction trans);    // Insättning

        bool Withdrawal(Transaction trans); // Uttag

    }
}
