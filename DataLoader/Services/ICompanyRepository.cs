using System.Threading.Tasks;
using DataLoader.Models;

namespace DataLoader.Services
{
    public interface ICompanyRepository
    {
        bool RecordExists(Company company);
        void UpsertPerson(Company company);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}