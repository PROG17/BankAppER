using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppER.Models
{
    public class Transaction
    {

        // Date identifies one transaction for a specific customer
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public int AccountId { get; set; }

        public Transaction()
        {

        }

        public Transaction(DateTime date, decimal amount, int accountId)
        {
            Date = date;
            Amount = amount;
            Balance = 0m;
            AccountId = accountId;
        }
        

    }
}
