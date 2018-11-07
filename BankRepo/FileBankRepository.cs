﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BankRepo.Models;

namespace BankRepo
{
    public class FileBankRepository : IBankRepository
    {
        public List<Customer> Customers { get; }
        public List<Account> Accounts { get; }
        private readonly Lookup<int, Account> _accountsLookup;

        public FileBankRepository()
        {
            Customers = new List<Customer>();
            Accounts = new List<Account>();

            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                AppDomain.CurrentDomain.RelativeSearchPath ?? "");

            ReadFromFile(Path.Combine(folder, @"bankdata.txt"));
            
            _accountsLookup = (Lookup<int, Account>) 
                Accounts.ToLookup(a => Customers.Single(c => c.Id == a.CustomerId).Id, a => a);
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            return _accountsLookup[customerId].ToList();
        }

        private void ReadFromFile(string path)
        {
            var file = new StreamReader(path);

            int customers = int.Parse(file.ReadLine());
            for (var i = 0; i < customers; i++)
            {
                Customers.Add(ReadCustomer(file.ReadLine()));
            }

            int accounts = int.Parse(file.ReadLine());
            for (var i = 0; i < accounts; i++)
            {
                Accounts.Add(ReadAccount(file.ReadLine()));
            }
        }

        private static Customer ReadCustomer(string line)
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

        private static Account ReadAccount(string line)
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