using System.Globalization;

namespace BankRepo.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
    }
}