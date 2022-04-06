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
            await ((EventsViewModel) BindingContext).SetData();
            await ((EventsViewModel) BindingContext).GetData();
        }
    }
}