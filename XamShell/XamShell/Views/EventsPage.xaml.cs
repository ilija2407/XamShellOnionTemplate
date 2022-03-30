using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamShell.ViewModels;

namespace XamShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((EventsViewModel) BindingContext).GetData();
        }
    }
}