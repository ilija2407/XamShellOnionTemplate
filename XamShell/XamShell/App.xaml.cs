using System;
using System.Text.Json;
using AutoMapper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamShell.Application.Services;
using XamShell.Infrastructure.Data.Repositories;
using XamShell.Infrastructure.ExternalServices;
using XamShell.Infrastructure.Services;

namespace XamShell
{
    public partial class App : Xamarin.Forms.Application
    {
        public static string AppName = "XamShell";

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            string appDomain = "http://" + AppName.ToLowerInvariant() + "/";
            if (!uri.ToString().ToLowerInvariant().StartsWith(appDomain, StringComparison.Ordinal))
                return;

            string pageUrl = uri.ToString().Replace(appDomain, string.Empty).Trim();
            var parts = pageUrl.Split('?');
            //string page = parts[0]; for linking different pages 
            string pageParameter = parts[1].Replace("id=", string.Empty);
            var userService = DependencyService.Get<UserService>();
            var user = await userService.GetUserById(int.Parse(pageParameter));
            var data = JsonSerializer.Serialize(user);
            await Shell.Current.GoToAsync($"addUserPage?userDto={data}");

            base.OnAppLinkRequestReceived(uri);
        }
    }
}