using System.Collections.Generic;
using System.Linq;
using BankRepo.Models;
using JetBrains.Annotations;

namespace BankRepo
{
    public interface IBankRepository
    {
        [NotNull, ItemNotNull]
        IReadOnlyList<Customer> Customers { get; }

        [NotNull, ItemNotNull]
        IReadOnlyList<Account> Accounts { get; }

        [NotNull, ItemNotNull]
        List<Account> GetAccountsFromCustomer(int customerId);

        [CanBeNull]
        Customer GetCustomerFromAccount(int accountId);

        [CanBeNull]
        Customer GetCustomer(int customerId);

        [CanBeNull]
        Account GetAccount(int accountId);

        [CanBeNull]
        void Transfer(int _accountId1, int _accountId2, decimal amount);
    }
}