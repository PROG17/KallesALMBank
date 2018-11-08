using System.Collections.Generic;
using System.Linq;
using BankRepo.Models;

namespace BankRepo
{
    public interface IBankRepository
    {
        IReadOnlyList<Customer> Customers { get; }
        IReadOnlyList<Account> Accounts { get; }
        List<Account> GetCustomerAccounts(int customerId);
    }
}