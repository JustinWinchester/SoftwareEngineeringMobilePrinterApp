using System;
using System.Collections.Generic;
using System.Text;

namespace PrinterApp.Model
{
    public class Category
    {
        /*private Category category;

        public Category()
        {
        }

        public Category(Category category)
        {
            this.category = category;
        } */

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPoster { get; set; }
        public string ImageUrl { get; set; }
    }
}
