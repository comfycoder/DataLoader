using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace LargeBlobBuilder
{
    class Program
    {
        const string mockDataPath = @"C:\srcMM\DataLoader\MockData\";
        const string largeBlobPath = @"C:\srcMM\DataLoader\LargeBlobData\";

        static void Main(string[] args)
        {
            GenerateCompanyCsvFile();
            //GeneratePersonCsvFile();
            //GenerateBigTextFile();
        }

        private static void GenerateCompanyCsvFile()
        {
            // Load source csv file to be clones multiple times
            using (var sr = new StreamReader($"{mockDataPath}MOCK_COMPANY_DATA.csv"))
            using (var csvReader = new CsvReader(sr))
            {
                // Configure CSV reader options
                csvReader.Configuration.TrimOptions = TrimOptions.Trim; // Trim all whitespaces from fields
                csvReader.Configuration.Comment = '@'; // Set comment start character. Default is '#'
                csvReader.Configuration.AllowComments = true; // Allow comments in file
                csvReader.Configuration.IgnoreBlankLines = true; // Ignore blank lines in file
                csvReader.Configuration.Delimiter = ","; // Set the field delimiter character
                csvReader.Configuration.HasHeaderRecord = true; // Ensure a header row exists

                var list = new List<Company>();

                // Register data mapper
                csvReader.Configuration.RegisterClassMap<CompanyMap>();

                // Retrieve company list
                var records = csvReader.GetRecords<Company>();

                // Need to make a copy of the list
                // So that we can clone it multiple
                // time to create the desired blob size
                foreach (var item in records)
                {
                    Company company = MapToNewPerson(item);

                    list.Add(company);
                }

                var newList = new List<Company>();

                int maxIterations = 20;
                int numberRecordsGenerated = maxIterations * list.Count;

                // Iterate over the source list to 
                // make a larger blob output file
                for (int i = 0; i < maxIterations; i++)
                {
                    foreach (var item in list)
                    {
                        Company company = MapToNewPerson(item);

                        // Ensure the record has a unique id
                        company.CompanyId = Guid.NewGuid();

                        newList.Add(company);
                    }
                }

                using (var writer = new StreamWriter($"{largeBlobPath}COMPANY_BLOB_DATA_{numberRecordsGenerated}.csv"))
                using (var csv = new CsvWriter(writer))
                {
                    // Write the record list to a new csv file
                    csv.WriteRecords(newList);
                }
            }
        }

        // Creates a clone of the input company
        private static Company MapToNewPerson(Company item)
        {
            var company = new Company();

            company.CompanyId = item.CompanyId;
            company.CompanyName = item.CompanyName.Length <= 50 ? item.CompanyName : item.CompanyName.Substring(0, 50).Trim();
            company.Description = item.Description.Length <= 1000 ? item.Description : item.Description.Substring(0, 1000).Trim();
            company.EmailAddress = item.EmailAddress.Length <= 255 ? item.EmailAddress : item.EmailAddress.Substring(0, 255).Trim();
            company.StreetAddress = item.StreetAddress.Length <= 50 ? item.StreetAddress : item.StreetAddress.Substring(0, 50).Trim();
            company.City = item.City.Length <= 30 ? item.City : item.City.Substring(0, 30).Trim();
            company.State = item.State.Length <= 30 ? item.State: item.State.Substring(0, 30).Trim();
            company.PostalCode = item.PostalCode.Length <= 10 ? item.PostalCode : item.PostalCode.Substring(0, 10).Trim();
            company.AnimalName = item.AnimalName.Length <= 50 ? item.AnimalName : item.AnimalName.Substring(0, 50).Trim();
            company.AnimalScientificName = item.AnimalScientificName.Length <= 60 ? item.AnimalScientificName : item.AnimalScientificName.Substring(0, 60).Trim();
            company.AppName = item.AppName.Length <= 30 ? item.AppName : item.AppName.Substring(0, 30).Trim();
            company.BuzzWord = item.BuzzWord.Length <= 30 ? item.BuzzWord : item.BuzzWord.Substring(0, 30).Trim();
            company.CarMake = item.CarMake.Length <= 20 ? item.CarMake : item.CarMake.Substring(0, 20).Trim();
            company.CarModel = item.CarModel.Length <= 20 ? item.CarModel : item.CarModel.Substring(0, 20).Trim();
            company.CarVin = item.CarVin.Length <= 20 ? item.CarVin : item.CarVin.Substring(0, 20).Trim();
            company.CatchPhrase = item.CatchPhrase.Length <= 60 ? item.CatchPhrase : item.CatchPhrase.Substring(0, 60).Trim();
            company.DomainName = item.DomainName.Length <= 30 ? item.DomainName :item.DomainName.Substring(0, 30).Trim();
            company.ContactFirstName = item.ContactFirstName.Length <= 30 ? item.ContactFirstName : item.ContactFirstName.Substring(0, 30).Trim();
            company.ContactLastName = item.ContactLastName.Length <= 30 ? item.ContactLastName : item.ContactLastName.Substring(0, 30).Trim();
            company.JobTitle = item.JobTitle.Length <= 50 ? item.JobTitle : item.JobTitle.Substring(0, 50).Trim();
            company.Language = item.Language.Length <= 25 ? item.Language : item.Language.Substring(0, 25).Trim();
            company.Skill = item.Skill.Length <= 40 ? item.Skill : item.Skill.Substring(0, 40).Trim();
            company.Longitude = item.Longitude;
            company.Latitude = item.Latitude;
            company.Phone = item.Phone.Length <= 12 ? item.Phone : item.Phone.Substring(0, 12).Trim();
            company.Product = item.Product.Length <= 50 ? item.Product : item.Product.Substring(0, 50).Trim();
            company.TimeZone = item.TimeZone.Length <= 30 ? item.TimeZone : item.TimeZone.Substring(0, 30).Trim();
            company.Notes = item.Notes.Length <= 1000 ? item.Notes : item.Notes.Substring(0, 1000).Trim();

            return company;
        }

        private static void GeneratePersonCsvFile()
        {
            // Load source csv file to be clones multiple times
            using (var sr = new StreamReader($"{mockDataPath}MOCK_PERSON_DATA.csv"))
            using (var csvReader = new CsvReader(sr))
            {
                // Configure CSV reader options
                csvReader.Configuration.TrimOptions = TrimOptions.Trim; // Trim all whitespaces from fields
                csvReader.Configuration.Comment = '@'; // Set comment start character. Default is '#'
                csvReader.Configuration.AllowComments = true; // Allow comments in file
                csvReader.Configuration.IgnoreBlankLines = true; // Ignore blank lines in file
                csvReader.Configuration.Delimiter = ","; // Set the field delimiter character
                csvReader.Configuration.HasHeaderRecord = true; // Ensure a header row exists

                var list = new List<Person>();

                // Register data mapper
                csvReader.Configuration.RegisterClassMap<PersonMap>();

                // Retrieve person list
                var records = csvReader.GetRecords<Person>();

                // Need to make a copy of the list
                // So that we can clone it multiple
                // time to create the desired blob size
                foreach (var item in records)
                {
                    Person person = MapToNewPerson(item);

                    list.Add(person);
                }

                var newList = new List<Person>();

                int maxIterations = 1_000;
                int numberRecordsGenerated = maxIterations * list.Count;

                // Iterate over the source list to 
                // make a larger blob output file
                for (int i = 0; i < maxIterations; i++)
                {
                    foreach (var item in list)
                    {
                        Person person = MapToNewPerson(item);

                        // Ensure the record has a unique id
                        person.PersonId = Guid.NewGuid();

                        newList.Add(person);
                    }
                }

                using (var writer = new StreamWriter($"{largeBlobPath}LARGE_BLOB_DATA_{numberRecordsGenerated}.csv"))
                using (var csv = new CsvWriter(writer))
                {
                    // Write the record list to a new csv file
                    csv.WriteRecords(newList);
                }
            }
        }

        // Creates a clone of the input person
        private static Person MapToNewPerson(Person item)
        {
            var person = new Person();

            person.PersonId = Guid.NewGuid();
            person.FirstName = item.FirstName;
            person.LastName = item.LastName;
            person.EmailAddress = item.EmailAddress;

            return person;
        }

        // Generate a very large text file
        private static void GenerateBigTextFile()
        {
            using (var sw = new StreamWriter($"{largeBlobPath}BIG_BLOB.txt"))
            {
                for (int i = 0; i < 40_000_000; i++)
                {
                    sw.WriteLine("Some line we are not interested in processing");
                }

                sw.WriteLine("Data: 42");
            }
        }
    }
}
