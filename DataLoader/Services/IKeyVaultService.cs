using System.Threading.Tasks;

namespace DataLoader.Services
{
    public interface IKeyVaultService
    {
        Task<string> GetSecretAsync(string secretName);
    }
}