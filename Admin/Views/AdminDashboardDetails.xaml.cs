using System;
using System.Collections.Generic;
using PrinterApp.Admin.Models;
using PrinterApp.Admin.ViewModels;
using PrinterApp.Serivces;
using PrinterApp.Tables.Register;
using Xamarin.Forms;

namespace PrinterApp.Admin.Views
{
    public partial class AdminDashboardDetails : ContentPage
    {

        public AdminDashboardDetails(RegisterTable registerTable)
        {
            InitializeComponent();
            BindingContext = new AdminDashboardViewModel(registerTable);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Label lblClicked = (Label)sender;
            var item = (TapGestureRecognizer)lblClicked.GestureRecognizers[0];
            var tappedItem = item.CommandParameter as TasksModel;



        }
    }
}
