using DataLoader.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataLoader.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _dbContext;

        public CompanyRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool RecordExists(Company company)
        {
            var exists = _dbContext.People.AsNoTracking()
               .Any(x => x.PersonId == company.CompanyId);

            return exists;
        }

        public void UpsertPerson(Company company)
        {
            if (RecordExists(company))
            {
                _dbContext.Companies.Update(company);
            }
            else
            {
                _dbContext.Companies.Add(company);
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
