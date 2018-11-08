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

        internal static Customer Deserialize(string line)
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