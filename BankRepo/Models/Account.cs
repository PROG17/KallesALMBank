using System;
using System.Globalization;

namespace BankRepo.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive!", nameof(amount));

            Balance += amount;
        }

        public void Withdrawl(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive!", nameof(amount));
            if (amount > Balance)
                throw new InvalidOperationException("Amount must be greater than the account balance!");

            Balance -= amount;
        }

        internal static Account Deserialize(string line)
        {
            string[] parts = line.Split(';');
            return new Account
            {
                Id = int.Parse(parts[0], CultureInfo.InvariantCulture),
                CustomerId = int.Parse(parts[1], CultureInfo.InvariantCulture),
                Balance = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
            };
        }
    }
}