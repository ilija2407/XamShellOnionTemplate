using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
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
        private ObservableCollection<UserDto> _users;

        public UsersViewModel()
        {
            _restService = DependencyService.Get<RestService>();
            _userService = DependencyService.Get<UserService>();
            AddUserCommand = new AsyncCommand(AddUser, CanExecute);
            Title = "Users Page!";
        }
        
        UserDto _selectedUser;
        public UserDto SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser == value) return;
                _selectedUser = value; 
                var data = JsonSerializer.Serialize(value);
                Shell.Current.GoToAsync($"addUserPage?userDto={data}");
            }
        }

        public IAsyncCommand AddUserCommand { get; }
        
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

        public ObservableCollection<UserDto> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }
        
        public async Task GetData()
        {
            var data = await _restService.RefreshDataAsync();
            if (data != null) User = $"User from the http get request: {data.Name}";
            
            var repositoryData = await _userService.GetItemsAsync();
            if (repositoryData != null)
            {
                if (data != null) DbUser = $"User from the database: {repositoryData.FirstOrDefault()?.Name}";
                Users = new ObservableCollection<UserDto>(repositoryData);
            }
        }

        public async Task SetUserData()
        {
            if (Users.Count == 0)
            {
                var user = new UserDto()
                {
                    Name = "Peter",
                    Age = 24,
                    Count = 2
                };

                await _userService.SaveItemAsync(user);
                await GetData();
            }
        }

        private async Task AddUser()
        {
            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync("addUserPage");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecute(object arg)
        {
            return !IsBusy;
        }
    }
}