using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PrinterApp.Admin.Views;
using PrinterApp.Serivces;
using Xamarin.Forms;

namespace PrinterApp.Admin.ViewModels
{
    public class AdminViewModel : BindableObject
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginAdmin { get; }
        public INavigation SharedNav { get; }
        public AdminViewModel(INavigation navigation)
        {
            SharedNav = navigation;
            LoginAdmin = new Command(async () => await PerformLogin());
        }
        private async Task PerformLogin()
        {
            var response = await ApiServices.ServiceClientInstance.LoginAdminUser(UserName, Password);

            if (response != null)
            {
                //await SharedNav.PushAsync(new NavigationPage(new AdminDashboard()));
                await Application.Current.MainPage.Navigation.PushModalAsync(new AdminMainPage());
                //await Navigation.PushModalAsync(new AdminDashboard());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Server Error", "Ok");
            }
        }
    }
}
