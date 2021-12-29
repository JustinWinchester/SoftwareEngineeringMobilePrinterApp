using PrinterApp.Helpers;
using PrinterApp.Tables.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrinterApp.Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminMainPage : ContentPage
    {
        public AdminMainPage()
        {
            InitializeComponent();
        }

        async void ButtonProducts_Clicked(System.Object sender, System.EventArgs e)
        {
            var apd = new AddPrinterItemData();
            await apd.AddPrinterItemAsync();



        }
        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }

}