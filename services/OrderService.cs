using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

using PrinterApp.Model;
using PrinterApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrinterApp.services
{

    class OrderService
    {
        FirebaseClient client;
        public OrderService()
        {
            client = new FirebaseClient("https://printerapp-9ae65.firebaseio.com/");
        }

        public async Task<string> PlaceOrderAsync()
        {
            {
                var cn = DependencyService.Get<ISQLite>().GetConnection();
                var data = cn.Table<CartItem>().ToList();

                var orderId = Guid.NewGuid().ToString();
                var uname = Preferences.Get("Username", "Guest");
                decimal totalCost = 0;



                foreach (var item in data)
                {
                    OrderDetails od = new OrderDetails()
                    {
                        OrderId = orderId,
                        OrderDetailId = Guid.NewGuid().ToString(),
                        ProductID = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    totalCost += item.Price * item.Quantity;
                    await client.Child("OrderDetails").PostAsync(od);
                }

                await client.Child("Orders").PostAsync(
                    new Order()
                    {
                        OrderId = orderId,
                        Username = uname,
                        TotalCost = totalCost

                    });
                return orderId;
            }
        }
    }
}