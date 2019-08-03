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
            company.CompanyName = item.CompanyName;
            company.Description = item.Description;
            company.EmailAddress = item.EmailAddress;
            company.StreetAddress = item.StreetAddress;
            company.City = item.City;
            company.State = item.State;
            company.PostalCode = item.PostalCode;
            company.AnimalName = item.AnimalName;
            company.AnimalScientificName = item.AnimalScientificName;
            company.AppName = item.AppName;
            company.BuzzWord = item.BuzzWord;
            company.CarMake = item.CarMake;
            company.CarModel = item.CarModel;
            company.CarVin = item.CarVin;
            company.CatchPhrase = item.CatchPhrase;
            company.DomainName = item.DomainName;
            company.ContactFirstName = item.ContactFirstName;
            company.ContactLastName = item.ContactLastName;
            company.JobTitle = item.JobTitle;
            company.Language = item.Language;
            company.Skill = item.Skill;
            company.Longitude = item.Longitude;
            company.Latitude = item.Latitude;
            company.Phone = item.Phone;
            company.Product = item.Product;
            company.TimeZone = item.TimeZone;
            company.Notes = item.Notes;

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
