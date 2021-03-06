﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppER.Models
{
    public class Customer
    {
        public Customer()
        {
            CustomerAccounts = new List<Account>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Account> CustomerAccounts { get; set; }
    }
}
