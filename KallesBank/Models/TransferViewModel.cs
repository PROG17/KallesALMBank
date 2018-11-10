using System;
using System.ComponentModel.DataAnnotations;
using BankRepo;
using BankRepo.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace KallesBank.Models
{
    public class TransferViewModel
    {
        [Display(Name = "Account")]
        [Required(ErrorMessage = "Please specify the target account.")]
        [Remote(controller:"Bank", action:"ValidateAccount")]
        public int? AccountId { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please specify a transfer amount.")]
        [Range(double.Epsilon, double.PositiveInfinity, ErrorMessage = "Please only specify a positive amount.")]
        [DataType(DataType.Currency, ErrorMessage = "Please specify a valid amount.")]
        public decimal Amount { get; set; }

        public SubmitType Submit { get; set; }

        public enum SubmitType
        {
            Deposit,
            Withdraw
        }
    }
}