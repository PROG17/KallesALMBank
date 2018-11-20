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
        public IActionResult Transfer(TransferViewModel model)
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
                switch (model.Submit)
                {
                    case TransferViewModel.SubmitType.Deposit:
                        account.Deposit(model.Amount);
                        break;

                    case TransferViewModel.SubmitType.Withdraw:
                        account.Withdrawl(model.Amount);
                        break;
                }

                ViewData["TransferSuccess"] = true;
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

        //Överföring mellan konton
        public IActionResult TransferBetweenAccounts()
        {
            return View(new TransferBetweenAccountsViewModel());
        }

        [HttpPost]
        public IActionResult TransferBetweenAccounts(TransferBetweenAccountsViewModel model)
        {
            if (ValidateAccounts(model) is JsonResult json && json.Value is string error)
            {
                ModelState.AddModelError(nameof(model.AccountId1), error);
            }

            if (!ModelState.IsValid)
                return View("TransferBetweenAccounts", model);

            Account account1 = _bankRepository.GetAccount(model.AccountId1 ?? 0);
            Account account2 = _bankRepository.GetAccount(model.AccountId2 ?? 0);

            try
            {
                account1.Transfer(account2, model.Amount);

                ViewData["TransferSuccess"] = true;
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(model.Amount), e.Message);
            }

            return View("TransferBetweenAccounts", model);
        }

        [AcceptVerbs("Get", "Post", Route = "/api/[controller]/ValidateAccounts")]
        public IActionResult ValidateAccounts(TransferBetweenAccountsViewModel model)
        {
            if (!(model.AccountId1 is null) && _bankRepository.GetAccount(model.AccountId1.Value) is null)
            {
                return Json($"Account #{model.AccountId1} doesnt exist.");
            }
            if (!(model.AccountId2 is null) && _bankRepository.GetAccount(model.AccountId2.Value) is null)
            {
                return Json($"Account #{model.AccountId2} doesnt exist.");
            }
            if (model.AccountId1 == model.AccountId2)
            {
                return Json($"Account can not transfer to itself.");
            }

            return Json(true);
        }
    }
}