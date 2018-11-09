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

       // public int Id { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public int AccountId { get; set; }

        // Nav
        //public Account Account { get; set; }

        //public string ShowOneTransaction() => $"  {AccountNr}             {Date.ToString("g")}           {Amount,17:N2} kr                        {Balance,17:N2} kr";


    }
}
