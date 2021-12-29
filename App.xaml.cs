using PrinterApp.Admin.Views;
using PrinterApp.Admin.Views.AdminRegistration;
using PrinterApp.Model;
using PrinterApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace PrinterApp
{
    public partial class App : Application
    {
        public static bool isCartTableCreated = Preferences.Get("isCartItemTableCreated", false);
        public App()
        {
            Device.SetFlags(new string[] {
                "AppTheme_Experimental",
                "MediaElement_Experimental"
                });

            InitializeComponent();
          //  MainPage = new NavigationPage(new AdminDashboard());
            // MainPage = new NavigationPage(new AdminLogin());
            //MainPage = new MainPage();
            // MainPage = new NavigationPage(new LoginView());
            // MainPage = new NavigationPage(new SettingsPage());

            
            string uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
            {
                MainPage = new LoginView();
            }
            else
            {
                MainPage = new ProductsView();
            }
        }
            
        protected override void OnStart()
        {
            if(isCartTableCreated == false)
            {
                var cn = DependencyService.Get<ISQLite>().GetConnection();
                cn.CreateTable<CartItem>();
                cn.Close();
                Preferences.Set("isCartItemTableCreated", true);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {

        }
    }
}


