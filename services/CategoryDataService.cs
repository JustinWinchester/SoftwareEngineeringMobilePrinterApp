using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using System.Linq;
using Firebase.Database.Query;
using System.Threading.Tasks;
using PrinterApp.Model;

namespace PrinterApp.services
{
    class CategoryDataService
    {
        FirebaseClient client;
        public CategoryDataService()
        {
            client = new FirebaseClient("https://printerapp-9ae65.firebaseio.com/");
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = (await client.Child("Categories")
                .OnceAsync<Category>())
                .Select(c => new Category
                {
                    CategoryID = c.Object.CategoryID,
                    CategoryName = c.Object.CategoryName,
                    CategoryPoster = c.Object.CategoryPoster,
                    ImageUrl = c.Object.ImageUrl
                }).ToList();
            return categories;
        }
    }

}
