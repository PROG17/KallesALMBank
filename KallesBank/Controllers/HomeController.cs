using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BankRepo;
using Microsoft.AspNetCore.Mvc;
using KallesBank.Models;

namespace KallesBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBankRepository _bankRepository;

        public HomeController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public IActionResult Index()
        {
            List<CustomerViewModel> model = _bankRepository.Customers
                .Select(c => new CustomerViewModel {
                    Customer = c,
                    Accounts = _bankRepository.GetAccountsFromCustomer(c.Id)
                }).ToList();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
