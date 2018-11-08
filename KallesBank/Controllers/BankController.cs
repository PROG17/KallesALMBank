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
            return View("Transfer", model);
        }

        [HttpPost]
        public IActionResult Withdraw(TransferViewModel model)
        {
            return View("Transfer", model);
        }
    }
}