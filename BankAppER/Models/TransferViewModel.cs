using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BankAppER.Models
{
    public class TransferViewModel
    {
       
        [Required(ErrorMessage = "Please enter your ToAccount!")]
        public Account ToAccount { get; set; }
        
        [Required(ErrorMessage = "Please enter your FromAccount!")]
        public Account FromAccount { get; set; }

        [Required(ErrorMessage = "Please enter an amount!")]
        public string Amount { get; set; }
    }
}
