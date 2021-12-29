using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using PrinterApp.Model;
using Xamarin.Essentials;

namespace PrinterApp.Services
{
    public class OrderHistoryService
    {
        FirebaseClient client;
        List<OrdersHistory> UserOrders;

        public OrderHistoryService()
        {
            client = new FirebaseClient("https://printerapp-9ae65.firebaseio.com/");
            UserOrders = new List<OrdersHistory>();
        }

        public async Task<List<OrdersHistory>> GetOrderDetailsAsync()
        {
            var uname = Preferences.Get("Username", "Guest");

            var orders = (await client.Child("Orders")
                .OnceAsync<Order>())
                .Where(o => o.Object.Username.Equals(uname))
                .Select(o => new Order
                {
                    OrderId = o.Object.OrderId,
                    ReceiptId = o.Object.ReceiptId,
                    TotalCost = o.Object.TotalCost,
                }).ToList();

            foreach (var order in orders)
            {
                OrdersHistory uoh = new OrdersHistory();
                uoh.OrderId = order.OrderId;
                uoh.ReceiptId = order.ReceiptId;
                uoh.TotalCost = order.TotalCost;

                var orderDetails = (await client.Child("OrderDetails")
                .OnceAsync<OrderDetails>())
                .Where(o => o.Object.OrderId.Equals(order.OrderId))
                .Select(o => new OrderDetails
                {
                    OrderId = o.Object.OrderId,
                    OrderDetailId = o.Object.OrderDetailId,
                    ProductID = o.Object.ProductID,
                    ProductName = o.Object.ProductName,
                    Quantity = o.Object.Quantity,
                    Price = o.Object.Price
                }).ToList();

                uoh.AddRange(orderDetails);

                UserOrders.Add(uoh);
            }

            return UserOrders;
        }
    }
}

