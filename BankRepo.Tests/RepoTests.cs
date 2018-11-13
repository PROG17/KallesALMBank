using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BankRepo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankRepo.Tests
{
    [TestClass]
    public class RepoTests
    {
        private IBankRepository _repo;

        [TestInitialize]
        public void TestInitialize()
        {
            _repo = new FileBankRepository();
        }

        [TestMethod]
        public void CustomersNotNull()
        {
            Assert.IsNotNull(_repo.Customers);
            CollectionAssert.AllItemsAreNotNull((ICollection)_repo.Customers);
        }

        [TestMethod]
        public void AccountsNotNull()
        {
            Assert.IsNotNull(_repo.Accounts);
            CollectionAssert.AllItemsAreNotNull((ICollection)_repo.Accounts);
        }

        [TestMethod]
        public void NoDuplicateAccounts()
        {
            // Act
            var duplicates = _repo.Accounts.GroupBy(k => k.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            Assert.AreEqual(0, duplicates.Count, $"Duplicate accounts: {string.Join(", ", duplicates.Select(g => g.Key))}");
        }

        [TestMethod]
        public void NoDuplicateCustomers()
        {
            // Act
            var duplicates = _repo.Customers.GroupBy(k => k.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            Assert.AreEqual(0, duplicates.Count, $"Duplicate customers: {string.Join(", ", duplicates.Select(g => g.Key))}");
        }

        [TestMethod]
        public void AccountsHaveCustomers()
        {
            foreach (Account account in _repo.Accounts)
            {
                // Act
                Customer customer = _repo.Customers.SingleOrDefault(c => c.Id == account.CustomerId);

                // Assert
                Assert.IsNotNull(customer);
            }
        }

        [TestMethod]
        public void AccountLookupForAllCustomers()
        {
            foreach (Customer customer in _repo.Customers)
            {
                // Act
                List<Account> accounts = _repo.GetAccountsFromCustomer(customer.Id);
                List<Account> expected = _repo.Accounts.Where(a => a.CustomerId == customer.Id).ToList();

                // Assert
                CollectionAssert.AreEquivalent(expected, accounts);
            }
        }

        [TestMethod]
        public void CustomerLookupForAllAccounts()
        {
            foreach (Account account in _repo.Accounts)
            {
                // Act
                Customer customer = _repo.GetCustomerFromAccount(account.Id);
                Customer expected = customer != null ? _repo.Customers.Single(c => c.Id == customer.Id) : null;

                // Assert
                Assert.IsNotNull(customer);
                Assert.AreEqual(expected, customer);
            }
        }

        [TestMethod]
        public void GetCustomerNonExisting()
        {
            Customer customer = _repo.GetCustomer(-10);

            Assert.IsNull(customer);
        }

        [TestMethod]
        public void GetCustomerExisting()
        {
            foreach (Customer expected in _repo.Customers)
            {
                Customer customer = _repo.GetCustomer(expected.Id);

                Assert.IsNotNull(customer);
                Assert.AreEqual(expected, customer);
            }
        }

        [TestMethod]
        public void GetAccountNonExisting()
        {
            Account account = _repo.GetAccount(-10);

            Assert.IsNull(account);
        }

        [TestMethod]
        public void GetAccountExisting()
        {
            foreach (Account expected in _repo.Accounts)
            {
                Account account = _repo.GetAccount(expected.Id);

                Assert.IsNotNull(account);
                Assert.AreEqual(expected, account);
            }
        }

        [TestMethod]
        public void CorrectBalanceAfterTransferAccount1()
        {
            // arrange
            Account account1 = _repo.GetAccount(13001);
            account1.Balance = 500;
            Account account2 = _repo.GetAccount(13002);
            decimal amount = 500;

            decimal expected = 0;

            // act
            _repo.Transfer(account1, account2, amount);

            // assert
            Assert.AreEqual(expected, account1.Balance);
        }

        [TestMethod]
        public void CorrectBalanceAfterTransferAccount2()
        {
            // arrange
            Account account1 = _repo.GetAccount(13001);
            account1.Balance = 500;
            Account account2 = _repo.GetAccount(13002);
            account2.Balance = 0;
            decimal amount = 500;

            decimal expected = 500;

            // act
            _repo.Transfer(account1, account2, amount);

            // assert
            Assert.AreEqual(expected, account2.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TransferInsufficient()
        {
            Account account1 = _repo.GetAccount(13001);
            account1.Balance = 0;
            Account account2 = _repo.GetAccount(13002);
            account2.Balance = 0;
            decimal amount = 500;
            
            _repo.Transfer(account1, account2, amount);
        }

    }
}