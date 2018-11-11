using BankAppER.Business;
using BankAppER.Models;
using Microsoft.AspNetCore.Mvc;
using System;


namespace BankAppER.Controllers
{
    public class AccountOperationsController : Controller
    {
        private readonly IBankRepository _bankRepo;

        public AccountOperationsController(IBankRepository bankRepo)
        {
            _bankRepo = bankRepo;
        }



        public IActionResult CreateTransaction()
        {
            var model = new TransactionViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WithdrawTransaction(TransactionViewModel model)
        {
            if (!IsModelValid(model))
                return View(nameof(CreateTransaction), model);

            if (!IsAccountExisting(model, out Account account))
                return View(nameof(CreateTransaction), model);

            if (!IsAmountValid(model, out decimal amount))
                return View(nameof(CreateTransaction), model);

            var currentTransaction = new Transaction(DateTime.Now, -amount, model.AccountId);

            var result = _bankRepo.Withdrawal(currentTransaction, account);

            ValidateOperation(result, "withdrawal", currentTransaction, model);

            model.Transactions = _bankRepo.GetTransactions(model.AccountId);

            return View(nameof(CreateTransaction), model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DepositTransaction(TransactionViewModel model)
        {
            if (!IsModelValid(model))
                return View(nameof(CreateTransaction), model);

            if (!IsAccountExisting(model, out Account account))
                return View(nameof(CreateTransaction), model);

            if (!IsAmountValid(model, out decimal amount))
                return View(nameof(CreateTransaction), model);

            var currentTransaction = new Transaction(DateTime.Now, amount, model.AccountId);

            var result = _bankRepo.Deposit(currentTransaction, account);

            ValidateOperation(result, "deposit", currentTransaction, model);

            model.Transactions = _bankRepo.GetTransactions(model.AccountId);

            return View(nameof(CreateTransaction), model);
        }


        // --- Help methods ----

        private bool IsModelValid(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Message = "Error: Model is not valid";

                model.IsError = true;

                return false;
            }
            return true;
        }

        private bool IsAccountExisting(TransactionViewModel model, out Account account)
        {
            account = _bankRepo.GetAccountById(model.AccountId);

            if (account == null)
            {
                ModelState.AddModelError("AccountId", "Account number does not exist");

                model.IsError = true;

                return false;
            }
            return true;
        }


        private bool IsAmountValid(TransactionViewModel model, out decimal amount)
        {
            if (!(decimal.TryParse(model.Amount, out amount)) || amount <= 0)
            {
                ModelState.AddModelError("Amount", "Amount must be a number greater than 0 SEK");

                model.IsError = true;

                return false;
            }
            return true;
        }


        private void ValidateOperation(bool result, string operation, Transaction currentTransaction, TransactionViewModel model)
        {
            if (result)
            {
                _bankRepo.AddTransaction(currentTransaction);

                model.Message = $"Success: {operation} performed.";

                model.IsError = false;
            }
            else
            {
                ModelState.AddModelError("Amount", $"{operation} failed. Amount is greater than balance.");

                model.IsError = true;
            }
        }




    }
}