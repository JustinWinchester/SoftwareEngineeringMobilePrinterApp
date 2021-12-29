using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PrinterApp.Model;
using PrinterApp.services;
using PrinterApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using PrinterApp.Services;
using PrinterApp.Admin.ViewModels;
using PrinterApp.Admin.Views;

namespace PrinterApp.ViewModels
{
    class ProductsViewModel : BaseViewModel
    {
        private string _UserName;
        public string UserName
        {
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserName;
            }
        }

        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            set
            {
                _UserCartItemsCount = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserCartItemsCount;
            }
        }

        private string _SearchText;
        public string SearchText
        {
            set
            {
                _SearchText = value;
                OnPropertyChanged();
            }

            get
            {
                return _SearchText;
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<PrinterItem> LatestItems { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command OrdersHistoryCommand { get; set; }
        public Command SearchViewCommand { get; set; }

        public Command AdminLoginCommand { get; set; }

        public ProductsViewModel()
        {
            var uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
                UserName = "Guest";
            else
                UserName = uname;

                        UserCartItemsCount = new CartItemService().GetUserCartCount();

            Categories = new ObservableCollection<Category>();
            LatestItems = new ObservableCollection<PrinterItem>();

            ViewCartCommand = new Command(async () => await ViewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
             OrdersHistoryCommand = new Command(async () => await OrderHistoryAsync());
             SearchViewCommand = new Command(async () => await SearchViewAsync());
            AdminLoginCommand = new Command(async () => await AdminLoginAsync());
            
            GetCategories();
            GetLatestItems();
        }
        
            private async Task SearchViewAsync()
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(
                    new SearchResultsView(SearchText));
            }
        
            private async Task OrderHistoryAsync()
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersHistoryView());
            }
        
        private async Task ViewCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private async Task LogoutAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
        }
        private async Task AdminLoginAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AdminLogin());
        }

        private async void GetCategories()
        {
            var data = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach (var item in data)
            {
                Categories.Add(item);
            }
        }

        private async void GetLatestItems()
        {
            var data = await new PrinterItemService().GetLatestPrinterItemsAsync();
            LatestItems.Clear();
            foreach (var item in data)
            {
                LatestItems.Add(item);
            }
        }
    }
}



