using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamShell.Infrastructure.ExternalServices;

namespace XamShell
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<RestService>();
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
    }
}