using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamShell.Application.Models.DTOs;
using XamShell.Domain.Models.Data;
using XamShell.Infrastructure.Data.Repositories;
using XamShell.Infrastructure.ExternalServices;
using XamShell.Infrastructure.Services;
using XamShell.Views;
using XamShell.Views.BadExamples;
using XamShell.Views.GoodExamples;

namespace XamShell
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public ICommand NavigateToEventsCommand => new Command(() =>
        {
            Current.GoToAsync("eventsPage");
            Current.FlyoutIsPresented = false;   //close the menu 
        });
        
        public ICommand NavigateToGoodExamplesCommand => new Command(() =>
        {
            Current.GoToAsync("goodTaskInvokePage");
            Current.FlyoutIsPresented = false;   //close the menu 
        });
        
        public ICommand NavigateToBadExamplesCommand => new Command(() =>
        {
            Current.GoToAsync("badTaskInvokePage");
            Current.FlyoutIsPresented = false;   //close the menu 
        });
        public AppShell()
        {
            InitializeComponent();
            RegisterDependencies();
            RegisterRoutes();
            SetDatabase();
            ConfigureMapper();
            BindingContext = this;
        }

        private void RegisterDependencies()
        {
            DependencyService.Register<RestService>();
            DependencyService.Register<UserService>();
            DependencyService.Register<UserRepository>();
        }
        
        void RegisterRoutes()
        {
            Routes.Add("eventsPage", typeof(EventsPage));
            Routes.Add("goodTaskInvokePage", typeof(GoodTaskInvokePage));
            Routes.Add("badTaskInvokePage", typeof(BadTaskInvokePage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        private async Task SetDatabase()
        {
            await UserRepository.Instance;
        }
        
        private void ConfigureMapper()
        {
            var configuration = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });
            
            DependencyService.RegisterSingleton(configuration.CreateMapper());

        }
    }
}