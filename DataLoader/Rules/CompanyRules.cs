using DataLoader.Models;
using System;

namespace DataLoader.Rules
{
    public class CompanyRules : ICompanyRules
    {
        // For demo purposes only
        Random _rnd = new Random();

        public CompanyRules()
        {

        }

        public void ApplyBusinessRules(Company item)
        {
            // Simulate a business rule that modified the record
            int number = _rnd.Next(1, 10);

            item.CompanyName = $"{item.CompanyName}-{number}";
        }
    }
}
