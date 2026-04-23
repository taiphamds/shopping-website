using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangThucPham.Models
{
    public class CheckoutViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }  // Ensure this matches the SQL OrderDate
        public string UserID { get; set; }  // Thêm thuộc tính này
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }  // Thêm thuộc tính này

    }

}