using DataLoader.Models;

namespace DataLoader.Rules
{
    public interface IPersonRules
    {
        void ApplyBusinessRules(Person item);
    }
}