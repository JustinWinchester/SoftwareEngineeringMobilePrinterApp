using PrinterApp.ViewModels;
using PrinterApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PrinterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentPage
    {
        CategoryViewModel cvm;
//#pragma warning disable CS0051 // Inconsistent accessibility: parameter type 'Category' is less accessible than method 'CategoryView.CategoryView(Category)'
       // public class Category { };
        public CategoryView(Category category)
//#pragma warning restore CS0051 // Inconsistent accessibility: parameter type 'Category' is less accessible than method 'CategoryView.CategoryView(Category)'
        {
            InitializeComponent();
            cvm = new CategoryViewModel(category);
            this.BindingContext = cvm;
        }

     /*   public CategoryView()
        {
        }
     */
        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var selectedProduct = e.CurrentSelection.FirstOrDefault() as PrinterItem;
            if (selectedProduct == null)
                return;
            await Navigation.PushModalAsync(new ProductDetailsView(selectedProduct));
            ((CollectionView)sender).SelectedItem = null;

        }
    }
}