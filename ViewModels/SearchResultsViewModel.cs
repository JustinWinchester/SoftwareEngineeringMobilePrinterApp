using PrinterApp.Model;
using PrinterApp.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrinterApp.ViewModels
{
    public class SearchResultsViewModel : BaseViewModel
    {
        public ObservableCollection<PrinterItem> PrinterItemsByQuery { get; set; }

        private int _TotalPrinterItems;
        public int TotalPrinterItems
        {
            set
            {
                _TotalPrinterItems = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalPrinterItems;
            }
        }

        public SearchResultsViewModel(string searchText)
        {
            PrinterItemsByQuery = new ObservableCollection<PrinterItem>();
            GetPrinterItemsByQuery(searchText);
        }

        private async void GetPrinterItemsByQuery(string searchText)
        {
            var data = await new PrinterItemService().GetPrinterItemsByQueryAsync(searchText);
            PrinterItemsByQuery.Clear();
            foreach (var item in data)
            {
                PrinterItemsByQuery.Add(item);
            }
            TotalPrinterItems = PrinterItemsByQuery.Count;
        }
    }
}
