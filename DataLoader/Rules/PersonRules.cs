using DataLoader.Models;
using System;

namespace DataLoader.Rules
{
    public class PersonRules : IPersonRules
    {
        // For demo purposes only
        Random _rnd = new Random();

        public PersonRules()
        {

        }

        public void ApplyBusinessRules(Person item)
        {
            // Simulate a business rule that modified the record
            int number = _rnd.Next(1, 10);

            item.LastName = $"{item.LastName}-{number}";
        }
    }
}
