using System;
using BankRepo;
using BankRepo.Models;
using KallesBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace KallesBank.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public IActionResult Transfer(int? id)
        {
            return View(new TransferViewModel{Amount = 120, AccountId = id ?? 0});
        }

        [HttpPost]
        public IActionResult Deposit(TransferViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Transfer", model);

            Account account = _bankRepository.GetAccount(model.AccountId ?? 0);
            if (account is null)
            {
                ModelState.AddModelError(nameof(model.AccountId), $"Account #{model.AccountId} doesnt exist.");
                return View("Transfer", model);
            }

            try
            {
                account.Deposit(model.Amount);
                ViewData["TransferSuccess"] = "Deposit";
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(model.Amount), e.Message);
            }

            return View("Transfer", model);
        }

        [HttpPost]
        public IActionResult Withdraw(TransferViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Transfer", model);

            Account account = _bankRepository.GetAccount(model.AccountId ?? 0);
            if (account is null)
            {
                ModelState.AddModelError(nameof(model.AccountId), $"Account #{model.AccountId} doesnt exist.");
                return View("Transfer", model);
            }

            try
            {
                account.Withdrawl(model.Amount);
                ViewData["TransferSuccess"] = "Withdrawl";
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(model.Amount), e.Message);
            }

            return View("Transfer", model);
        }
    }
}