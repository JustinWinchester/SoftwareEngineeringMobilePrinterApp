using System;
using System.Collections.Generic;
using PrinterApp.Admin.ViewModels;
using PrinterApp.Admin.Views.AdminRegistration;
using Xamarin.Forms;

namespace PrinterApp.Admin.Views
{
    public partial class AdminLogin : ContentPage
    {
        public AdminLogin()
        {
            InitializeComponent();
            BindingContext = new AdminViewModel(Navigation);
        }

        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            //Navigation.PushAsync(new AdminRegistrationPage());
            //await Application.Current.MainPage.Navigation.
              await Navigation.PushModalAsync(new AdminRegistrationPage());
        }
    }
}
