using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppER.Models
{
    public interface IBankRepository
    {
        IQueryable<Customer> GetCustomers();
        Customer GetCustomerById(int id);

        IQueryable<Account> GetAccounts();
        Account GetAccountById(int id);
    }
}
