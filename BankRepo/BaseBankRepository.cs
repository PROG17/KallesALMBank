using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankRepo.Models;

namespace BankRepo
{
    public class BaseBankRepository : IBankRepository
    {
        protected readonly List<Customer> _customers;
        protected readonly List<Account> _accounts;

        public IReadOnlyList<Customer> Customers => _customers;
        public IReadOnlyList<Account> Accounts => _accounts;

        public BaseBankRepository()
        {
            _customers = new List<Customer>();
            _accounts = new List<Account>();
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            Customer customer = GetCustomer(customerId);
            if (customer is null) return new List<Account>();

            return Accounts.Where(a => a.CustomerId == customerId).ToList();
        }
    }
}
