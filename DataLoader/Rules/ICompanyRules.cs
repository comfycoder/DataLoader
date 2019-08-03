using DataLoader.Models;

namespace DataLoader.Rules
{
    public interface ICompanyRules
    {
        void ApplyBusinessRules(Company item);
    }
}