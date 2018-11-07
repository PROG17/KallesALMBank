using System.Collections.Generic;
using System.Linq;
using BankRepo.Models;

namespace BankRepo
{
    public interface IRepository
    {
        List<Customer> Customers { get; }
        List<Account> Accounts { get; }
        List<Account> GetCustomerAccounts(int customerId);
    }
}