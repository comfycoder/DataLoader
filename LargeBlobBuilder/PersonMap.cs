using CsvHelper.Configuration;

namespace LargeBlobBuilder
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.PersonId).Name("PersonId");
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.LastName).Name("LastName");
            Map(m => m.EmailAddress).Name("EmailAddress");
        }
    }
}
