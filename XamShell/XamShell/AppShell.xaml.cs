using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamShell.Views;

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
        
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
   
            Routes.Add("eventsPage", typeof(EventsPage));
            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}