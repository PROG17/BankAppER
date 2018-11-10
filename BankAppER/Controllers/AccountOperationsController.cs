using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppER.Business;
using BankAppER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BankAppER.Controllers
{
    public class AccountOperationsController : Controller
    {
        private readonly IBankRepository _bankRepo;

        public AccountOperationsController(IBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }


        // GET: AccountOperations
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountOperations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        public IActionResult CreateTransaction()
        {
            var model = new TransactionViewModel();
            // model.Message = "";

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WithdrawTransaction(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Message = "Error: Model is not valid";

                model.IsError = true;

                return View(nameof(CreateTransaction), model);
            }

            var account = _bankRepo.GetAccountById(model.AccountId);

            if (account == null)
            {
                model.Message = "Error: Account number does not exist";

                model.IsError = true;

                return View(nameof(CreateTransaction), model);
            }

            var currentTransaction = new Transaction()
            {
                Date = DateTime.Now,
                Amount = -model.Amount,
                AccountId = model.AccountId
            };

            var result = _bankRepo.Withdrawal(currentTransaction, account);

            if (result)
            {
                _bankRepo.AddTransaction(currentTransaction);

                model.Message = $"Success: Withdrawal performed. New balance after withdrawal {account.Balance}";

                model.IsError = false;
            }
            else
            {
                model.Message = $"Error: Withdrawal failed. It is greater than balance. Current balance is {account.Balance}";

                model.IsError = true;
            }

            model.Transactions = _bankRepo.GetTransactions(model.AccountId);

            return View(nameof(CreateTransaction), model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DepositTransaction(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Message = "Error: Model is not valid";

                model.IsError = true;

                return View(nameof(CreateTransaction), model);
            }

            var account = _bankRepo.GetAccountById(model.AccountId);

            if (account == null)
            {
                model.Message = "Error: Account number does not exist";

                model.IsError = true;

                return View(nameof(CreateTransaction), model);
            }

            var currentTransaction = new Transaction()
            {
                Date = DateTime.Now,
                Amount = model.Amount,
                AccountId = model.AccountId
            };

            var result = _bankRepo.Deposit(currentTransaction, account);

            if (result)
            {
                _bankRepo.AddTransaction(currentTransaction);

                model.Message = $"Success: Deposit performed. New balance after deposit {account.Balance:N2} SEK";

                model.IsError = false;
            }
            else
            {
                model.Message = $"Error: Deposit failed. Current balance is {account.Balance:N2} SEK";

                model.IsError = true;
            }

            model.Transactions = _bankRepo.GetTransactions(model.AccountId);

            return View(nameof(CreateTransaction), model);
        }















    }
}