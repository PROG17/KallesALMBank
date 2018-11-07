using System.Collections.Generic;

namespace BankRepo
{
    public interface IRepository
    {
        List<Account> Accounts { get; }
        List<Customer> Customers { get; }
    }
}