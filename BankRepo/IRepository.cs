using System.Collections.Generic;
using BankRepo.Models;

namespace BankRepo
{
    public interface IRepository
    {
        List<Account> Accounts { get; }
        List<Customer> Customers { get; }
    }
}