using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankAppER.Models

{
    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            Transactions = new List<Transaction>();
        }

        [DisplayName("Account number")]
        public int AccountId { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter an amount greater than {1}")]
        public decimal Amount { get; set; }

        public string Message { get; set; }

        public bool IsError { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
