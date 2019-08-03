using DataLoader.Models;
using System.Threading.Tasks;

namespace DataLoader.Services
{
    public interface IPersonRepository
    {
        bool RecordExists(Person person);
        void UpsertPerson(Person person);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}