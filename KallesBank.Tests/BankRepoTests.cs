using System.Linq;
using BankRepo;
using BankRepo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KallesBank.Tests
{
    [TestClass]
    public class BankRepoTests
    {
        private IRepository _repo;

        [TestInitialize]
        public void TestInitialize()
        {
            _repo = new FileRepository();

            Assert.IsNotNull(_repo.Customers);
            Assert.IsNotNull(_repo.Accounts);
            CollectionAssert.AllItemsAreNotNull(_repo.Customers);
            CollectionAssert.AllItemsAreNotNull(_repo.Accounts);
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
    }
}