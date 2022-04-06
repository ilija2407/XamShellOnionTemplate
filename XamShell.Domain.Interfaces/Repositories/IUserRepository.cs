using System.Collections.Generic;
using System.Threading.Tasks;
using XamShell.Domain.Models.Data;

namespace XamShell.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetItemsAsync();
        Task<int> SaveItemAsync(User item);
    }
}