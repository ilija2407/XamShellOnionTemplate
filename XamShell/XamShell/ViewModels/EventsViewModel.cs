using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;
using XamShell.Domain.Models.Data;
using XamShell.Domain.Repositories;
using XamShell.Infrastructure.Data.Repositories;
using XamShell.Infrastructure.ExternalServices;

namespace XamShell.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        private readonly IRestService _restService;
        private readonly IUserRepository _userRepository;

        string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public EventsViewModel()
        {
            Title = "My Events Page!";
        }

        public async Task GetData()
        {
            //ToDo
        }

        public async Task SetData()
        {
            //ToDo
        }
    }
}