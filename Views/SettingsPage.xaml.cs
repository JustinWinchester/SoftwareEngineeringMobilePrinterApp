using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinterApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrinterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
        async void ButtonCategories_Clicked(System.Object sender, System.EventArgs e)
        {
            var acd = new AddCategoryData();
            await acd.AddCategoriesAsync();
        }
        async void ButtonProducts_Clicked(System.Object sender, System.EventArgs e)
        {
            var apd = new AddPrinterItemData();
            await apd.AddPrinterItemAsync();



        }
        void ButtonCart_Clicked(System.Object sender, System.EventArgs e)
        {
            var cct = new CreateCartTable();
            if (cct.CreateTable())
                DisplayAlert("Success", "Cart Table Created", "Ok");
            else
                DisplayAlert("Error", "Error while creating table", "Ok");
        }
      /*  void ButtonRegisterTable_Clicked(System.Object sender, System.EventArgs e)
        {
            var cct = new CreateRegisterTable();
            if (cct.CreateTable())
                DisplayAlert("Success", "Register Table Created", "Ok");
            else
                DisplayAlert("Error", "Error while creating table", "Ok");
        }*/

    }
}