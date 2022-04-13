using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Xamarin.Forms;

namespace XamShell.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
                ((AsyncCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }
        
        public IAsyncCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new AsyncCommand(DoLogin, CanExecute);
        }

        private async Task DoLogin()
        {
            try
            {
                IsBusy = true;
                await Shell.Current.GoToAsync($"//MainPageRoute");
                //await Task.Delay(5000);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private bool CanExecute(object arg)
        {
            return !IsBusy;
        }
    }
}