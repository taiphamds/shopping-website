using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangThucPham.Models
{
    public class CartItemViewModel
    {
        public int ProductID { get; set; }
        public int OrderId{ get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string img { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } // Thêm CategoryName vào model

    }
}
