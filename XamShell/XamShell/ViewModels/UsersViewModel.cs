using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;
using XamShell.Application.Models.DTOs;
using XamShell.Application.Services;
using XamShell.Infrastructure.ExternalServices;
using XamShell.Infrastructure.Services;

namespace XamShell.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly IRestService _restService;
        private readonly IUserService _userService;

        private string _user;
        private string _dbUser;
        
        public UsersViewModel()
        {
            _restService = DependencyService.Get<RestService>();
            _userService = DependencyService.Get<UserService>();
            Title = "Users Page!";
        }
       
        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        
        public string DbUser
        {
            get => _dbUser;
            set => SetProperty(ref _dbUser, value);
        }
        
        public async Task GetData()
        {

            var data = await _restService.RefreshDataAsync();
            if (data != null) User = $"User from the http get request: {data.Name}";
            
            var repositoryData = await _userService.GetItemsAsync();
            if (repositoryData != null)
            {
                if (data != null) DbUser = $"User from the database: {repositoryData.FirstOrDefault()?.Name}";
            }
        }

        public async Task SetUserData()
        {
            var repositoryData = await _userService.GetItemsAsync();

            if (repositoryData.Count == 0)
            {
                var user = new UserDto()
                {
                    Name = "Peter",
                    Age = 24,
                    Count = 2
                };

                await _userService.SaveItemAsync(user);
            }
        }
    }
}