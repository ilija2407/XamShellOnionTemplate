using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XamShell.Application.Models.DTOs;
using XamShell.Application.Services;
using XamShell.Domain.Models.Data;
using XamShell.Domain.Repositories;
using XamShell.Infrastructure.Data.Repositories;

namespace XamShell.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UserService()
        {
            _mapper = DependencyService.Get<IMapper>();
            _userRepository = DependencyService.Get<UserRepository>();
        }
        
        public async Task<List<UserDto>> GetItemsAsync()
        {
            var repositoryData = await _userRepository.GetItemsAsync();
            return _mapper.Map<List<UserDto>>(repositoryData);
        }

        public async Task<int> SaveItemAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await _userRepository.SaveItemAsync(user);
        }
        
        public async Task<UserDto> GetUserById(int id)
        {
            var repositoryData = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserDto>(repositoryData);
        }
    }
}