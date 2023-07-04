using EmaPosAdminProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmaPosAdminProject.ViewModel
{
    public class ProductImages
    {
        public int Id { get; set; }
        public List<product> ProductList { get; set; }
        public List<productImage> ProductImageList { get; set; }
        public List<productImage> ProdctImagess { get; set; }
        public List<category> CategoryList { get; set; }

        public  IEnumerable<product> Products { get; set; }

        public product Product { get; set; }
        public productImage ProductImage { get; set; }
        public category Category { get; set; }

        public string myCategory { get; set; }

        
    }
}