using Firebase.Database;
using PrinterApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace PrinterApp.services
{
    class PrinterItemService
    {
        FirebaseClient client;
        public PrinterItemService()
        {
            client = new FirebaseClient("https://printerapp-9ae65.firebaseio.com/");
        }
        public async Task<List<PrinterItem>> GetPrinterItemsAsync()
        {
            var products = (await client.Child("PrinterItems")
                .OnceAsync<PrinterItem>())
                .Select(f => new PrinterItem
                {
                    CategoryID = f.Object.CategoryID,
                    Description = f.Object.Description,
                    HomeSelected = f.Object.HomeSelected,
                    ImageUrl = f.Object.ImageUrl,
                    Name = f.Object.Name,
                    Price = f.Object.Price,
                    ProductID = f.Object.ProductID,
                    Rating = f.Object.Rating,
                    RatingDetail = f.Object.RatingDetail
                }).ToList();
            return products;
        }
        public async Task<ObservableCollection<PrinterItem>> GetPrinterItemsByCategoryAsync(int categoryID)
        {
            var printerItemsByCategory = new ObservableCollection<PrinterItem>();
            var items = (await GetPrinterItemsAsync()).Where(p => p.CategoryID == categoryID).ToList();
            foreach (var item in items)
            {
                printerItemsByCategory.Add(item);

            }
            return printerItemsByCategory;
        }
        public async Task<ObservableCollection<PrinterItem>> GetLatestPrinterItemsAsync()
        {
            var latestPrinterItems = new ObservableCollection<PrinterItem>();
            var items = (await GetPrinterItemsAsync()).OrderByDescending(f => f.ProductID).Take(3);
            foreach (var item in items)
            {
                latestPrinterItems.Add(item);

            }
            return latestPrinterItems;
        }
        public async Task<ObservableCollection<PrinterItem>> GetPrinterItemsByQueryAsync(string searchText)
        {
            var printerItemsByQuery = new ObservableCollection<PrinterItem>();
            var items = (await GetPrinterItemsAsync()).Where(p => p.Name.Contains(searchText)).ToList();
            foreach (var item in items)
            {
            printerItemsByQuery.Add(item);
            }
            return printerItemsByQuery;
        }
    }
}
