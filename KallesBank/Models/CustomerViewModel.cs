using System.Collections.Generic;
using System.Linq;
using BankRepo;
using BankRepo.Models;
using JetBrains.Annotations;

namespace KallesBank.Models
{
    public class CustomerViewModel
    {
        [NotNull]
        public Customer Customer { get; set; }

        [NotNull, ItemNotNull]
        public List<Account> Accounts { get; set; }
    }
}