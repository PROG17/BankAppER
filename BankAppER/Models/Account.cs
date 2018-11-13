﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppER.Models
{
    public class Account
    {

        public Account()
        {
            Transactions = new List<Transaction>();
        }

        [DisplayName("Account number")]
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public int CustomerId { get; set; }

        // Nav
        // public Customer Customer { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
