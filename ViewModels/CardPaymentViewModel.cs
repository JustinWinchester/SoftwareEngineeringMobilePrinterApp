using System;
using System.Collections.Generic;
using System.Text;
using PrinterApp.services;
using PrinterApp.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using PrinterApp.ViewModels;
using PrinterApp.Services;
using PrinterApp.Model;
using System.Collections.ObjectModel;

namespace PrinterApp.ViewModels
{
    class CardPaymentViewModel : BaseViewModel
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

        private bool _IsCartExists;
        public bool IsCartExists
        {
            set
            {
                _IsCartExists = value;
                OnPropertyChanged();
            }

            get
            {
                return _IsCartExists;
            }
        }

        public Command LogoutCommand { get; set; }
        public Command GotoCartCommand { get; set; }

       
        public Command LoginCommand { get; set; }
    public Command RegisterCommand { get; set; }
        public Command PlaceOrdersCommand { get; set; }
       
        public CardPaymentViewModel()
        {
            UserCartItemsCount = new CartItemService().GetUserCartCount();

            IsCartExists = (UserCartItemsCount > 0) ? true : false;

            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
            PlaceOrdersCommand = new Command(async () => await PlaceOrdersAsync());
            GotoCartCommand = new Command(async () => await GotoCartAsync());
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

        private async Task GotoCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private void RemoveItemsFromCart()
        {
            var cis = new CartItemService();
            cis.RemoveItemsFromCart();
        }

        private async Task PlaceOrdersAsync()
        {
            var id = await new OrderService().PlaceOrderAsync() as string;
            RemoveItemsFromCart();
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersView(id));

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
        public ObservableCollection<UserCartItem> CartItems { get; set; }

        private decimal _TotalCost;
        public decimal TotalCost
        {
            set
            {
                _TotalCost = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalCost;
            }
        }


        private void LoadItems()
        {
            var cn = DependencyService.Get<ISQLite>().GetConnection();
            var items = cn.Table<CartItem>().ToList();
            CartItems.Clear();
            foreach (var item in items)
            {
                CartItems.Add(new UserCartItem()
                {
                    CartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Cost = item.Price * item.Quantity
                });
                TotalCost += (item.Price * item.Quantity);
            }
        }
    }
}
    