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
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Column("Description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Column("EmailAddress")]
        [StringLength(255)]
        public string EmailAddress { get; set; }

        [Column("StreetAddress")]
        [StringLength(50)]
        public string StreetAddress { get; set; }

        [Column("City")]
        [StringLength(30)]
        public string City { get; set; }

        [Column("State")]
        [StringLength(30)]
        public string State { get; set; }

        [Column("PostalCode")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Column("AnimalName")]
        [StringLength(50)]
        public string AnimalName { get; set; }

        [Column("AnimalScientificName")]
        [StringLength(60)]
        public string AnimalScientificName { get; set; }

        [Column("AppName")]
        [StringLength(30)]
        public string AppName { get; set; }

        [Column("BuzzWord")]
        [StringLength(30)]
        public string BuzzWord { get; set; }

        [Column("CarMake")]
        [StringLength(20)]
        public string CarMake { get; set; }

        [Column("CarModel")]
        [StringLength(20)]
        public string CarModel { get; set; }

        [Column("CarVin")]
        [StringLength(20)]
        public string CarVin { get; set; }

        [Column("CatchPhrase")]
        [StringLength(60)]
        public string CatchPhrase { get; set; }

        [Column("DomainName")]
        [StringLength(30)]
        public string DomainName { get; set; }

        [Column("ContactFirstName")]
        [StringLength(30)]
        public string ContactFirstName { get; set; }

        [Column("ContactLastName")]
        [StringLength(30)]
        public string ContactLastName { get; set; }

        [Column("JobTitle")]
        [StringLength(50)]
        public string JobTitle { get; set; }

        [Column("Language")]
        [StringLength(25)]
        public string Language { get; set; }

        [Column("Skill")]
        [StringLength(50)]
        public string Skill { get; set; }

        [Column("Longitude")]
        [StringLength(40)]
        public double Longitude { get; set; }

        [Column("Latitude")]
        [StringLength(50)]
        public double Latitude { get; set; }

        [Column("Phone")]
        [StringLength(12)]
        public string Phone { get; set; }

        [Column("Product")]
        [StringLength(50)]
        public string Product { get; set; }

        [Column("TimeZone")]
        [StringLength(30)]
        public string TimeZone { get; set; }

        [Column("Notes")]
        [StringLength(1000)]
        public string Notes { get; set; }
    }
}
