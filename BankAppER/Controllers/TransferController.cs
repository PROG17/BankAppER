﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppER.Business;
using BankAppER.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAppER.Controllers
{
    public class TransferController : Controller
    {
        private IBankRepository _bankRepository;

        public TransferController(IBankRepository bankRepo)
        {
            _bankRepository = bankRepo;
        }

        public IActionResult Transfer()
        {
            return View(new TransferViewModel());
        }

        [HttpPost]
        public IActionResult Transfer(TransferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fromAccount = _bankRepository.GetAccountById(model.FromAccount.Id);
                var toAccount = _bankRepository.GetAccountById(model.ToAccount.Id);

                if (fromAccount != null && toAccount != null &&
                    model.FromAccount.Id != model.ToAccount.Id)
                {
                    decimal amountInDecimal;
                    if (Decimal.TryParse(model.Amount,out amountInDecimal) || amountInDecimal > 0)
                    {
                        if (amountInDecimal > 0)
                        {
                            var result = _bankRepository.Transfer(fromAccount, toAccount, amountInDecimal);
                            if (result)
                            {
                                TempData["TransferSuccessMessage"] =
                                    $" {model.Amount} SEK was successfully transferred to Account: {model.ToAccount.Id}! " +
                                    $"New balance Account #{toAccount.Id}: {toAccount.Balance}, New balance Account #{fromAccount.Id}: {fromAccount.Balance}";
                                return RedirectToAction("Transfer");
                            }
                            TempData["InsufficientBalanceErrorMessage"] = $"FromAccount insufficient funds!";
                            return View(model);
                        }
                        
                    }
                    TempData["InvalidAmountFormatErrorMessage"] = $"Please enter an Amount in the correct format!. Furthermore, the amount cant be zero";
                    return View(model);

                }

                if (fromAccount == null)
                {
                    TempData["FromAccountNotFoundErrorMessage"] = $"FromAccount number: {model.FromAccount.Id} was not found!";
                }
                    
                else if (toAccount == null)
                    TempData["ToAccountNotFoundErrorMessage"] = $"ToAccount number: {model.ToAccount.Id} was not found!";
                if (model.FromAccount.Id == model.ToAccount.Id)
                {
                    TempData["InvalidAccountOperation"] = $"FromAccount and ToAccount cant be the same account!";

                }

                return RedirectToAction("Transfer");

            }

            return View(model);
        }
    }
}