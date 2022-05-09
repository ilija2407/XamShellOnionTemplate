using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamShell.ViewModels;

namespace XamShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        public UsersPage()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((UsersViewModel) BindingContext).GetData();
            await ((UsersViewModel) BindingContext).SetUserData();
        }
    }
}