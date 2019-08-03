using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LargeBlobBuilder
{
    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Map(m => m.CompanyId).Name("CompanyId");
            Map(m => m.CompanyName).Name("CompanyName");
            Map(m => m.Description).Name("Description");
            Map(m => m.EmailAddress).Name("EmailAddress");
            Map(m => m.StreetAddress).Name("StreetAddress");
            Map(m => m.City).Name("City");
            Map(m => m.State).Name("State");
            Map(m => m.PostalCode).Name("PostalCode");
            Map(m => m.AnimalName).Name("AnimalName");
            Map(m => m.AnimalScientificName).Name("AnimalScientificName");
            Map(m => m.AppName).Name("AppName");
            Map(m => m.BuzzWord).Name("BuzzWord");
            Map(m => m.CarMake).Name("CarMake");
            Map(m => m.CarModel).Name("CarModel");
            Map(m => m.CarVin).Name("CarVin");
            Map(m => m.CatchPhrase).Name("CatchPhrase");
            Map(m => m.DomainName).Name("DomainName");
            Map(m => m.ContactFirstName).Name("ContactFirstName");
            Map(m => m.ContactLastName).Name("ContactLastName");
            Map(m => m.JobTitle).Name("JobTitle");
            Map(m => m.Language).Name("Language");
            Map(m => m.Skill).Name("Skill");
            Map(m => m.Longitude).Name("Longitude");
            Map(m => m.Latitude).Name("Latitude");
            Map(m => m.Phone).Name("Phone");
            Map(m => m.Product).Name("Product");
            Map(m => m.TimeZone).Name("TimeZone");
            Map(m => m.Notes).Name("Notes");
        }
    }
}
