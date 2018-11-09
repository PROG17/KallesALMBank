using System;
using BankRepo;
using BankRepo.Models;
using JetBrains.Annotations;
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
            if (ValidateAccount(model) is JsonResult json && json.Value is string error)
            {
                ModelState.AddModelError(nameof(model.AccountId), error);
            }

            if (!ModelState.IsValid)
                return View("Transfer", model);

            Account account = _bankRepository.GetAccount(model.AccountId ?? 0);

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
            if (ValidateAccount(model) is JsonResult json && json.Value is string error)
            {
                ModelState.AddModelError(nameof(model.AccountId), error);
            }

            if (!ModelState.IsValid)
                return View("Transfer", model);
            
            Account account = _bankRepository.GetAccount(model.AccountId ?? 0);

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

        [AcceptVerbs("Get", "Post", Route = "/api/[controller]/ValidateAccount")]
        public IActionResult ValidateAccount(TransferViewModel model)
        {
            if (model.AccountId is null)
            {
                return Json($"Please specify the target account.");
            }
            if (_bankRepository.GetAccount(model.AccountId.Value) is null)
            {
                return Json($"Account #{model.AccountId} doesnt exist.");
            }

            return Json(true);
        }
    }
}