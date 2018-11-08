using BankAppER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BankAppER.Business
{
    public class BankRepository : IBankRepository
    {

        public BankRepository()
        {
            InitializeCustomers();
        }

        private IQueryable<Account> Accounts { get; set; } = new List<Account>
        {
            new Account { Id = 1, Balance = 80000, CustomerId = 1 },
            new Account { Id = 2, Balance = 65000, CustomerId = 1 },
            new Account { Id = 3, Balance = 4000, CustomerId = 1 },
            new Account { Id = 4, Balance = 15000, CustomerId = 2 },
            new Account { Id = 5, Balance = 12300, CustomerId = 2 },
            new Account { Id = 6, Balance = 800, CustomerId = 2 },
            new Account { Id = 7, Balance = 15600, CustomerId = 2 },
            new Account { Id = 8, Balance = 9540, CustomerId = 3 },
            new Account { Id = 9, Balance = 32000, CustomerId = 3 },
            new Account { Id = 10, Balance = 190000, CustomerId = 3 }
        }.AsQueryable();

        private IQueryable<Customer> Customers { get; set; } = new List<Customer>
        {
            new Customer {Id = 1, Name = "Egidio R."},
            new Customer {Id = 2, Name = "Jens H."},
            new Customer {Id = 3, Name = "Karl F."}

        }.AsQueryable();

        private void InitializeCustomers()
        {
            Customers.ToArray()[0].CustomerAccounts = Accounts.Take(3).ToList();
            Customers.ToArray()[1].CustomerAccounts = Accounts.Skip(3).Take(4).ToList();
            Customers.ToArray()[2].CustomerAccounts = Accounts.Skip(7).Take(3).ToList();
        }


        public Account GetAccountById(int id) => Accounts.FirstOrDefault(c => c.Id == id);

        public IQueryable<Account> GetAccounts() => Accounts;

        public Customer GetCustomerById(int id) => Customers.FirstOrDefault(c => c.Id == id);

        public IQueryable<Customer> GetCustomers() => Customers;


        public bool Deposit(Transaction trans)
        {
            var account = GetAccountById(trans.AccountId);

            if (account == null)
                return false;

            trans.Balance = account.Balance + trans.Amount;

            account.Balance = trans.Balance;

            account.Transactions.Add(trans);

            return true;
        }


        public bool Withdrawal(Transaction trans)
        {
            var account = GetAccountById(trans.AccountId);

            if (account == null)
                return false;

            // Check if withdrawal amount is greater than Balance
            var newBalance = account.Balance - trans.Amount;

            if (newBalance < 0)
            {
                return false;
            }

            trans.Balance = newBalance;

            account.Balance = newBalance;

            account.Transactions.Add(trans);

            return true;
        }



           


        //// Deposit (insättning) is exactly the same method as Withdrawal!!
        //// Return value is false if any error
        //public bool Deposit()
        //{
        //    string msg = GenericTransaction(TransType.DEPOSIT);
        //    if (msg != "ok")
        //    {
        //        opErr.Msg(msg);
        //        return false;
        //    }
        //    return true;
        //}


        //// Withdrawal (uttag) is exactly the same method as Deposit()!!
        //// Return value is false if any error
        //public bool Withdrawal()
        //{
        //    string msg = GenericTransaction(TransType.WITHDRAWAL);
        //    if (msg != "ok")
        //    {
        //        opErr.Msg(msg);
        //        return false;
        //    }
        //    return true;
        //}


        //// Used for both deposit and withdrawal
        //// Arguments: transaction type and if Transfer use 'step=0' to perform withdrawal and step=1 to perform deposit
        //// return an error messsage or null
        //private string GenericTransaction(TransType type) 
        //{
        //    // Retrieve an account number via user input
        //    // Below - refactoring
        //    //string text1 = "";
        //    //string text2 = "";

        //    //switch (type)
        //    //{
        //    //    case TransType.DEPOSIT:
        //    //    case TransType.WITHDRAWAL:
        //    //        text1 = "Account number? ";
        //    //        break;
        //    //    case TransType.TRANSFER:
        //    //        text1 = "From account number? ";
        //    //        text2 = "To account number? ";
        //    //        break;
        //    //}


        //    var account = SearchByAccountNr("\nAccount number? ", out int notUsedHere);

        //    if (account == null)
        //    {
        //        return " *** Account number not found *** ";
        //        // opErr.Msg(" *** Account number not found *** ");
        //        // return false;     // return error to caller!
        //    }
        //    // Show selected account status
        //    Console.WriteLine("\n{0}: {1,16:N2} kr", account.AccNr, account.Balance);

        //    //if (account.Balance == 0)
        //    //{
        //    //    Console.WriteLine($"Account nr {account.AccNr} has NOT enough ");
        //    //}

        //    decimal unsignedAmount = -1m;
        //    while (!InputADecimal("Which amount? ", out unsignedAmount))
        //    {
        //        inputError.Msg(" Invalid choice");
        //        // inputError.Msg(" Invalid choice");
        //    }

        //    // Check if withdrawal  of amount which is over the account balance
        //    if (type == TransType.WITHDRAWAL)
        //    {
        //        if ((account.Balance - unsignedAmount) < -account.CreditLimit)
        //        {
        //            // Error! Not possible transaction
        //            Console.WriteLine($" *** Reached the credit Limit {account.CreditLimit} ***");
        //            return "*** Operation cannot be performed ***";
        //        }
        //        else if (account.Balance < unsignedAmount)
        //        {
        //            Console.WriteLine($" ** Account nr {account.AccNr} has NOT enough balance **");
        //            Console.WriteLine($"Applied credit limit {account.CreditLimit} with rate {account.CreditRate:P2}");
        //        }
        //        // Otherwise, I can proceed with the transaction
        //    }

        //    Console.WriteLine("** Operation is being processed... **");
        //    // Create a new transaction, update the amount, Balance,Date, accNr
        //    var newTrans = new Transaction(DateTime.Now);

        //    newTrans.AccountNr = account.AccNr;

        //    // I want to give a "sign" to amount so I can distiguish which type of transaction 
        //    // I did while retrieving the transactions file on a later stage

        //    newTrans.Amount = (type == TransType.DEPOSIT) ? unsignedAmount : -unsignedAmount;

        //    // Save the new transaction in related account AND on file 
        //    // via .Deposit() or .Withdrawal method

        //    bool result = true;
        //    if (type == TransType.DEPOSIT)
        //    {
        //        result = account.Deposit(newTrans);
        //    }
        //    else // Withdrawal
        //    {
        //        result = account.Withdrawal(newTrans, false);
        //    }
        //    if (result)
        //    {
        //        // Console.WriteLine("Account status after operation");

        //        Console.WriteLine("Account Nr           Balance                CreditLimit      Credit interest    Credit rate       Debit interest   Debit Rate");
        //        Console.WriteLine(account.ShowAccountAllInfo());

        //        // Console.WriteLine("\n{0}: {1,16:N2} kr", account.AccNr, account.Balance);
        //        return "ok";
        //    }
        //    else
        //    {
        //        return "*** Transaction could not be performed ***";
        //    }
        //}

    }
}
