using PrinterApp.Model;
using Xamarin.Forms;

namespace PrinterApp.Views
{
    internal class CategoryView : Page
    {
        private Category category;

        public CategoryView(Category category)
        {
            this.category = category;
        }
    }
}