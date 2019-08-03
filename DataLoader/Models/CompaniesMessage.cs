using System.Collections.Generic;

namespace DataLoader.Models
{
    public class CompaniesMessage
    {
        public string BatchId { get; set; }

        public ICollection<Company> Companies { get; set; }

        public CompaniesMessage()
        {
            this.Companies = new List<Company>();
        }
    }
}
