using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productApi.Models
{
    public partial class ProductClass
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public Nullable<double> productPrice { get; set; }
    }
}