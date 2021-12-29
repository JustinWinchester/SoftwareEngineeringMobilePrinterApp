using PrinterApp.Helpers;
using PrinterApp.Tables.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrinterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardPayment : ContentPage
    {
        public CardPayment()
        {
            //InitializeComponent();
            // LabelName.Text = "Welcome " + Preferences.Get("Username", "Guest") + ",";
            InitializeComponent();
            //LabelName.Text = "Welcome " + Preferences.Get("Username", "Guest") + ",";


        }

        async void ButtonProducts_Clicked(System.Object sender, System.EventArgs e)
        {
            var apd = new AddPrinterItemData();
            await apd.AddPrinterItemAsync();



        }
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}