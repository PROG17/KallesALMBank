using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BankRepo;

namespace KallesBank.Models.Bank
{
    public class BankViewModel
    {
        public List<CustomerViewModel> Customers { get; set; }

        public static BankViewModel FromRepository(IRepository repository)
        {
            List<CustomerViewModel> customers = repository.Customers.Select(Mapper.Map<CustomerViewModel>).ToList();

            foreach (CustomerViewModel customer in customers)
            {
                customer.Accounts = repository.GetCustomerAccounts(customer.Id)
                    .Select(Mapper.Map<AccountViewModel>)
                    .ToList();
            }

            return new BankViewModel
            {
                Customers = customers
            };
        }
    }
}