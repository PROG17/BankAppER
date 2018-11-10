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

        [Required(ErrorMessage = "Account number is required.")]
        [DisplayName("Account number")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
      //  [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public string Amount { get; set; }

        public string Message { get; set; }

        public bool IsError { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
