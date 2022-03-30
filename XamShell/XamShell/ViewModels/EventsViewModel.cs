using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;
using XamShell.Infrastructure.ExternalServices;

namespace XamShell.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        private readonly IRestService _restService;
        string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public EventsViewModel()
        {
            _restService = DependencyService.Get<RestService>();;
            Title = "My Events Page!";
        }

        public async Task GetData()
        {
            var data = await _restService.RefreshDataAsync();
            if (data != null) Title = data?.Name;
        }
    }
}