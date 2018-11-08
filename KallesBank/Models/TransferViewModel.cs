using System;
using System.ComponentModel.DataAnnotations;
using BankRepo;
using BankRepo.Models;
using JetBrains.Annotations;

namespace KallesBank.Models
{
    public class TransferViewModel
    {
        [Display(Name = "Account")]
        [Required(ErrorMessage = "Please specify the target account.")]
        public int? AccountId { get; set; }

        [Display(Name = "Transfer amount")]
        [Required(ErrorMessage = "Please specify a transfer amount.")]
        [Range(0, double.PositiveInfinity, ErrorMessage = "Please only specify a positive amount.")]
        [DataType(DataType.Currency, ErrorMessage = "Please specify a valid amount.")]
        public decimal Amount { get; set; }
    }
}