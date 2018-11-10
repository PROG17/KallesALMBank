using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BankRepo.Models;

namespace BankRepo
{
    public class FileBankRepository : BaseBankRepository
    {
        public FileBankRepository()
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                AppDomain.CurrentDomain.RelativeSearchPath ?? "");

            string path = Path.Combine(folder, @"bankdata.txt");

            var file = new StreamReader(path);

            int customers = int.Parse(file.ReadLine());
            for (var i = 0; i < customers; i++)
            {
                _customers.Add(DeserializeCustomer(file.ReadLine()));
            }

            int accounts = int.Parse(file.ReadLine());
            for (var i = 0; i < accounts; i++)
            {
                _accounts.Add(DeserializeAccount(file.ReadLine()));
            }
        }


        internal static Account DeserializeAccount(string line)
        {
            string[] parts = line.Split(';');
            return new Account
            {
                Id = int.Parse(parts[0], CultureInfo.InvariantCulture),
                CustomerId = int.Parse(parts[1], CultureInfo.InvariantCulture),
                Balance = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
            };
        }

        internal static Customer DeserializeCustomer(string line)
        {
            string[] parts = line.Split(';');
            return new Customer
            {
                Id = int.Parse(parts[0], CultureInfo.InvariantCulture),
                OrganizationId = parts[1],
                OrganizationName = parts[2],
                Address = parts[3],
                City = parts[4],
                Region = parts[5],
                PostCode = parts[6],
                Country = parts[7],
                Telephone = parts[8],
            };
        }
    }
}