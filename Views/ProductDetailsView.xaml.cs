using System;
using System.Collections.Generic;

using PrinterApp.Model;
using PrinterApp.ViewModels;
using Xamarin.Forms;

namespace PrinterApp.Views
{
    public partial class ProductDetailsView : ContentPage
    {

        ProductDetailsViewModel pvm;
        public ProductDetailsView(PrinterItem printerItem)
        {
            InitializeComponent();
            pvm = new ProductDetailsViewModel(printerItem);
            this.BindingContext = pvm;
        }
        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}