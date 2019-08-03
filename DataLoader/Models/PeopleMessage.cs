using System.Collections.Generic;

namespace DataLoader.Models
{
    public class PeopleMessage
    {
        public string BatchId { get; set; }

        public ICollection<Person> People { get; set; }

        public PeopleMessage()
        {
            this.People = new List<Person>();
        }
    }
}
