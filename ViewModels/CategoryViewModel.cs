using System;
using System.Collections.ObjectModel;
using PrinterApp.Model;
using PrinterApp.services;
using PrinterApp.Services;

namespace PrinterApp.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
       // public class Category { public int CategoryID { get; internal set; } };
        private Category _SelectedCategory;
        public Category SelectedCategory
        {
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged();
            }

            get
            {
                return _SelectedCategory;
            }
        }

        
        public ObservableCollection<PrinterItem> PrinterItemsByCategory { get; set; }

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

        public CategoryViewModel(Category category)
        {
            SelectedCategory = category;
            PrinterItemsByCategory = new ObservableCollection<PrinterItem>();
            GetPrinterItems(category.CategoryID);
        }

        private async void GetPrinterItems(int categoryID)
        {
            var data = await new PrinterItemService().GetPrinterItemsByCategoryAsync(categoryID);
            PrinterItemsByCategory.Clear();
            foreach (var item in data)
            {
                PrinterItemsByCategory.Add(item);
            }
            TotalPrinterItems = PrinterItemsByCategory.Count;
        }
    }
}