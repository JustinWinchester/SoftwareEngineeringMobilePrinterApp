using System;
using System.Collections.Generic;
using System.Text;
using PrinterApp.services;
using PrinterApp.Views;
 using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using PrinterApp.ViewModels;
using PrinterApp.Admin.Views;

namespace PrinterApp.Admin.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            set
            {
                this._Username = value;
                OnPropertyChanged();

            }
            get
            {
                return this._Username;
            }
        }
        private string _Password;
        public string Password
        {
            set
            {
                this._Password = value;
                OnPropertyChanged();

            }
            get
            {
                return this._Password;
            }
        }
        private bool _Isbusy;
        public bool Isbusy
        {
            set
            {
                this._Isbusy = value;
                OnPropertyChanged();

            }
            get
            {
                return this._Isbusy;
            }
        }
        private bool _Result;
        public bool Result
        {
            set
            {
                this._Result = value;
                OnPropertyChanged();

            }
            get
            {
                return this._Result;
            }
        }


        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public Command GotoAdminPage2Command { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            GotoAdminPage2Command = new Command(async () => await GotoAdminPage2Async());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task GotoAdminPage2Async()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AdminPage2());
        }

        private async Task RegisterCommandAsync()
        {
            if (Isbusy)
                return;
            try
            {
                Isbusy = true;
                var userService = new UserService();
                Result = await userService.RegisterUser(Username, Password);
                if (Result)
                    await Application.Current.MainPage.DisplayAlert("Success", "User Registered", "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "User already exist with that info", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                Isbusy = false;
            }

        }

        private async Task LoginCommandAsync()
        {
            if (Isbusy)
                return;
            try
            {
                Isbusy = true;
                var userService = new UserService();
                Result = await userService.LoginUser(Username, Password);
                if (Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid Username or Password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                Isbusy = false;
            }
             

        }
    }
}