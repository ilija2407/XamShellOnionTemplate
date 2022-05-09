using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using MvvmHelpers;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Xamarin.Forms;
using XamShell.Application.Models.DTOs;
using XamShell.Application.Services;
using XamShell.Infrastructure.Services;

namespace XamShell.ViewModels
{
    public class AddUserViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IUserService _userService;
        private UserDto _user;
        private IAppLinkEntry appLink;
        
        public AddUserViewModel()
        {
            _userService = DependencyService.Get<UserService>();
            SaveUserCommand = new AsyncCommand(SaveUser, CanExecute);
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.Count > 0)
            {
                var name = HttpUtility.UrlDecode(query["userDto"]);
                User = JsonSerializer.Deserialize<UserDto>(name);
                Title = "Edit user";
            }
            else
            {
                User = new UserDto()
                {
                    Id = 0
                };
                Title = "Add user";
            }
        }

        public IAsyncCommand SaveUserCommand { get; }

        public UserDto User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private async Task SaveUser()
        {
            User.Id = await _userService.SaveItemAsync(User);
            appLink = GetAppLink(User);
            Xamarin.Forms.Application.Current.AppLinks.RegisterLink(appLink);
            await Shell.Current.GoToAsync("..",true);
        }

        AppLinkEntry GetAppLink(UserDto item)
        {
            var pageLink = new AppLinkEntry
            {
                Title = item.Name,
                AppLinkUri = new Uri($"http://{App.AppName}/AddUser?id={item.Id}", UriKind.RelativeOrAbsolute),
                IsLinkActive = true,
            };

            return pageLink;
        }

        async void DeleteUser()
        {
            Xamarin.Forms.Application.Current.AppLinks.DeregisterLink(appLink);
            await Shell.Current.GoToAsync("..",true);
        }
        
        private bool CanExecute(object arg)
        {
            return !IsBusy;
        }
    }
}
