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
                _customers.Add(Customer.Deserialize(file.ReadLine()));
            }

            int accounts = int.Parse(file.ReadLine());
            for (var i = 0; i < accounts; i++)
            {
                _accounts.Add(Account.Deserialize(file.ReadLine()));
            }
        }
    }
}