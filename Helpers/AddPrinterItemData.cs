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
    class AddPrinterItemData
    {
        FirebaseClient client;
        public List<PrinterItem> PrinterItems { get; set; }
        public AddPrinterItemData()
        {
            client = new FirebaseClient("https://printerapp-9ae65.firebaseio.com/");
            PrinterItems = new List<PrinterItem>()
            {
                new PrinterItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "hpink",
                    Name = "HPInkJetPrinter",
                    Description = "This Is An Awesome Inkjet Printer",
                    Rating = "4.9",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 105
                },
             new PrinterItem
                {
                    ProductID = 6,
                    CategoryID = 6,
                    ImageUrl =  "hplaser",
                    Name = "HPLaserPrinter",
                    Description = "This Is An Awesome allinone Printer",
                    Rating = "3.8",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 107
                },
              new PrinterItem
                {
                    ProductID = 3,
                    CategoryID = 2,
                    ImageUrl =  "epson1",
                    Name = "EpsonPrinter",
                    Description = "This Is An Awesome Epson Inkjet",
                    Rating = "4.5",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 95
                },
               new PrinterItem
                {
                    ProductID = 4,
                    CategoryID = 2,
                    ImageUrl =  "epsonl",
                    Name = "Epson2",
                    Description = "Epson Inkjet Printer",
                    Rating = "5.0",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 155
                },
                new PrinterItem
                {
                    ProductID = 5,
                    CategoryID = 3,
                    ImageUrl =  "epsonall",
                    Name = "EpsonAll",
                    Description = "Epson Printer:Full Capabilities",
                    Rating = "3.4",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 118
                },
                 new PrinterItem
                {
                    ProductID = 6,
                    CategoryID = 2,
                    ImageUrl =  "epsonlaser",
                    Name = "EpsonLaser",
                    Description = "Epson LaserPrinter",
                    Rating = "4.7",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 99
                },
                  new PrinterItem
                {
                    ProductID = 7,
                    CategoryID = 5,
                    ImageUrl =  "down",
                    Name = "SamSung",
                    Description = "This Is A Samsung Laser Printer",
                    Rating = "3.2",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 125
                },
                   new PrinterItem
                {
                    ProductID = 8,
                    CategoryID = 4,
                    ImageUrl =  "download3",
                    Name = "SamsungAll",
                    Description = "Samsung All In One Printer",
                    Rating = "3.3",
                    RatingDetail = "(Tech Reviewed)",
                    HomeSelected = "CompleteHeart",
                    Price = 130
                },

                    new PrinterItem
                {
                    ProductID = 11,
                    CategoryID = 7,
                    ImageUrl =  "blwite",
                    Name = "Epson-Black And White",
                    Description = "Epson LaserPrinter",
                    Rating = "4.7",
                    RatingDetail = "(Cheap Ink)",
                    HomeSelected = "CompleteHeart",
                    Price = 99
                },
                  new PrinterItem
                {
                    ProductID = 9,
                    CategoryID = 7,
                    ImageUrl =  "Lexmarkbnw",
                    Name = "Lexmark Black & White",
                    Description = "Lexmark b&w Printer",
                    Rating = "4.0",
                    RatingDetail = "(Vintage)",
                    HomeSelected = "CompleteHeart",
                    Price = 125
                },
                   new PrinterItem
                {
                    ProductID = 10,
                    CategoryID = 7,
                    ImageUrl =  "hpbnw",
                    Name = "HP Black & White",
                    Description = "HP b&w Printer",
                    Rating = "3.3",
                    RatingDetail = "(Great Quality)",
                    HomeSelected = "CompleteHeart",
                    Price = 130
                }



            };
        }
        public async Task AddPrinterItemAsync()
        {
            try
            {
                foreach (var item in PrinterItems)
                {
                    await client.Child("PrinterItems").PostAsync(new PrinterItem()
                    {
                        CategoryID = item.CategoryID,
                        ProductID = item.ProductID,
                        Description = item.Description,
                        HomeSelected = item.HomeSelected,
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                        Price = item.Price,
                        Rating = item.Rating,
                        RatingDetail = item.RatingDetail
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
