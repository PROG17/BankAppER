using BankAppER.Models;
using System.Collections.Generic;
using System.Linq;



namespace BankAppER.Business
{
    public interface IBankRepository
    {
        IQueryable<Customer> GetCustomers();

        Customer GetCustomerById(int id);

        IQueryable<Account> GetAccounts();

        Account GetAccountById(int id);

        List<Transaction> GetTransactions(int accountId);

        void AddTransaction(Transaction transaction);

        // Operations
        bool Deposit(Transaction trans, Account account);    // Insättning

        bool Withdrawal(Transaction trans, Account account); // Uttag

        bool Transfer(Account fromAccount, Account toAccount, decimal amount);

    }
}
