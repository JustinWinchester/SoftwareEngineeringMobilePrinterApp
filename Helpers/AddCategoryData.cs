using Firebase.Database;
using Firebase.Database.Query;
using PrinterApp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrinterApp.Helpers
{
    class AddCategoryData
    {
        public List<Category> Categories { get; set; }

        FirebaseClient client;
        public AddCategoryData()
        {
            client = new FirebaseClient("https://printerapp-9ae65.firebaseio.com/");
            Categories = new List<Category>()
            {
        new Category()
        {
                    CategoryID = 1,
                    CategoryName = "HPInkJet",
                    CategoryPoster = "hpld",
                    ImageUrl  = "pr2.png"
            },
             new Category()
             {
                 CategoryID = 2,
                 CategoryName = "EpsonInkJet",
                 CategoryPoster = "epsonl",
                 ImageUrl = "p1.jpg"
             },
              new Category()
              {
                  CategoryID = 3,
                  CategoryName = "Epson-AllNOne",
                  CategoryPoster = "epsonl",
                  ImageUrl = "p1.jpg"
              },
               new Category()
               {
                   CategoryID = 4,
                   CategoryName = "Samsung-AllNOne",
                   CategoryPoster = "samsungall",
                   ImageUrl = "pr2.png"
               },
                new Category()
                {
                    CategoryID = 5,
                    CategoryName = "Samsung-InkJet",
                    CategoryPoster = "sumn",
                    ImageUrl = "pr2.png"
                },
                 new Category()
                 {
                     CategoryID = 6,
                     CategoryName = "HPAll-In-One",
                     CategoryPoster = "hpld",
                     ImageUrl = "pr2.png"
                 },
                  new Category()
                {
                    CategoryID = 7,
                    CategoryName = "Black & White Printers",
                    CategoryPoster = "sumn",
                    ImageUrl = "pr2.png"
                },
                   new Category()
                {
                    CategoryID = 7,
                    CategoryName = "Black & White Printers",
                    CategoryPoster = "sumn",
                    ImageUrl = "pr2.png"
                },
                   new Category()
                {
                    CategoryID = 7,
                    CategoryName = "Black & White Printers",
                    CategoryPoster = "sumn",
                    ImageUrl = "pr2.png"
                }


                 };
        }
        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach (var category in Categories)
                {
                    await client.Child("Categories").PostAsync(new Category()
                    {
                        CategoryID = category.CategoryID,
                        CategoryName = category.CategoryName,
                        CategoryPoster = category.CategoryPoster,
                        ImageUrl = category.ImageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
