using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppER.Models
{
    public class BankRepository : IBankRepository
    {

        public BankRepository()
        {
            InitializeCustomers();
        }

        private IQueryable<Account> Accounts { get; set; } = new List<Account>
        {
            new Account { Id=1, Total= 80000 },
            new Account { Id=2, Total= 65000 },
            new Account { Id=3, Total= 4000 },
            new Account { Id=4, Total= 15000 },
            new Account { Id=5, Total= 12300 },
            new Account { Id=6, Total= 800 },
            new Account { Id=7, Total= 15600 },
            new Account { Id=8, Total= 9540 },
            new Account { Id=9, Total= 32000 },
            new Account { Id=10, Total= 190000 }
        }.AsQueryable();

        private IQueryable<Customer> Customers { get; set; } = new List<Customer>
        {
            new Customer {Id = 1, Name = "Egidio R."},
            new Customer {Id = 2, Name = "Jens H."},
            new Customer {Id = 3, Name = "Karl F."}

        }.AsQueryable();

        public void InitializeCustomers()
        {
            Customers.ToArray()[0].CustomerAccounts = Accounts.Take(3).ToList();
            Customers.ToArray()[1].CustomerAccounts = Accounts.Skip(3).Take(4).ToList();
            Customers.ToArray()[2].CustomerAccounts = Accounts.Skip(7).Take(3).ToList();
        }


        public Account GetAccountById(int id) => Accounts.FirstOrDefault(c => c.Id == id);

        public IQueryable<Account> GetAccounts() => Accounts;

        public Customer GetCustomerById(int id) => Customers.FirstOrDefault(c => c.Id == id);

        public IQueryable<Customer> GetCustomers() => Customers;
    }
}
