using System;
using AutoMapper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamShell.Infrastructure.Data.Repositories;
using XamShell.Infrastructure.ExternalServices;
using XamShell.Infrastructure.Services;

namespace XamShell
{
    public partial class App : Xamarin.Forms.Application
    {
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
    }
}