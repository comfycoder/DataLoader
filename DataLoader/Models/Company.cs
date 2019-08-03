using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLoader.Models
{
    [Table("Company", Schema = "dbo")]
    public class Company
    {
        public Company()
        {
        }

        [Key]
        [Column("CompanyId")]
        public Guid CompanyId { get; set; }

        [Column("CompanyName")]
        public string CompanyName { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("EmailAddress")]
        public string EmailAddress { get; set; }

        [Column("StreetAddress")]
        public string StreetAddress { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("State")]
        public string State { get; set; }

        [Column("PostalCode")]
        public int PostalCode { get; set; }

        [Column("AnimalName")]
        public string AnimalName { get; set; }

        [Column("AnimalScientificName")]
        public string AnimalScientificName { get; set; }

        [Column("AppName")]
        public string AppName { get; set; }

        [Column("BuzzWord")]
        public string BuzzWord { get; set; }

        [Column("CarMake")]
        public string CarMake { get; set; }

        [Column("CarModel")]
        public string CarModel { get; set; }

        [Column("CarVin")]
        public string CarVin { get; set; }

        [Column("CatchPhrase")]
        public string CatchPhrase { get; set; }

        [Column("DomainName")]
        public string DomainName { get; set; }

        [Column("ContactFirstName")]
        public string ContactFirstName { get; set; }

        [Column("ContactLastName")]
        public string ContactLastName { get; set; }

        [Column("JobTitle")]
        public string JobTitle { get; set; }

        [Column("Language")]
        public string Language { get; set; }

        [Column("Skill")]
        public string Skill { get; set; }

        [Column("Longitude")]
        public double Longitude { get; set; }

        [Column("Latitude")]
        public double Latitude { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Product")]
        public string Product { get; set; }

        [Column("TimeZone")]
        public string TimeZone { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }
    }
}
