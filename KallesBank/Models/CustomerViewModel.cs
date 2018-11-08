using System.Collections.Generic;
using System.Linq;
using BankRepo;
using BankRepo.Models;

namespace KallesBank.Models
{
    public class CustomerViewModel
    {
        public Customer Customer { get; set; }
        public List<Account> Accounts { get; set; }

        public static List<CustomerViewModel> GetAll(IBankRepository repo)
        {
            return repo.Customers.Select(c => new CustomerViewModel
            {
                Customer = c,
                Accounts = repo.GetCustomerAccounts(c.Id)
            }).ToList();
        }
    }
}