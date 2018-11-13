using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankAppER.Models
{
    public class TransferViewModel
    {
        
        [Required(ErrorMessage = "Please enter your ToAccount!")]
        public Account ToAccount { get; set; }

        [Required(ErrorMessage = "Please enter your FromAccount!")]
        public Account FromAccount { get; set; }

        [Range(1,Int32.MaxValue)]
        [Required(ErrorMessage = "Please enter an amount!")]
        public decimal Amount { get; set; }
    }
}
