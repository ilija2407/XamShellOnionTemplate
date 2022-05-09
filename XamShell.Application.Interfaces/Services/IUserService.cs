using System.Collections.Generic;
using System.Threading.Tasks;
using XamShell.Application.Models.DTOs;

namespace XamShell.Application.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetItemsAsync();
        Task<int> SaveItemAsync(UserDto item);
        Task<UserDto> GetUserById(int id);
    }
}