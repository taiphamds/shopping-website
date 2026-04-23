using CuaHangThucPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangThucPham.Controllers
{
    public class OrderController : Controller
    {
        private readonly CuaHangTrucTuyenEntities3 _context;

        public OrderController()
        {
            _context = new CuaHangTrucTuyenEntities3(); // Khởi tạo context nếu không sử dụng Dependency Injection
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            var currentUser = Session["TtDangNhap"] as User;

            if (currentUser == null)
            {
                TempData["Message"] = "Bạn cần đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Index", "Login");
            }

            int userId = currentUser.UserId;

            var cartItems = _context.ShoppingCarts
                .Where(c => c.UserID == userId)
                .Select(c => new CartItemViewModel
                {
                    ProductID = c.ProductID.Value,
                    ProductName = c.Product.ProductName,
                    Quantity = c.Quantity.Value,
                    Price = c.Product.Price,
                    Total = c.Quantity.Value * c.Product.Price,
                    img = c.Product.img
                })
                .ToList();

            var model = new CheckoutViewModel
            {
                CartItems = cartItems,
                ShippingAddress= "", // Có thể lấy từ thông tin người dùng nếu cần
                ShippingCity = "",
                Notes = ""
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult PlaceOrder(CheckoutViewModel model)
        {
            var currentUser = Session["TtDangNhap"] as User;

            if (currentUser == null)
            {
                TempData["Message"] = "Bạn cần đăng nhập để đặt hàng.";
                return RedirectToAction("Index", "Login");
            }

            using (var context = new CuaHangTrucTuyenEntities3())
            {
                // Tạo đơn hàng mới
                var order = new Order
                {
                    UserID = currentUser.UserId,
                    OrderDate = DateTime.Now,
                    ShippingAddress = model.ShippingAddress,
                    ShippingCity = model.ShippingCity,
                    Status = "Pending",
                    Notes = model.Notes,
                    PhoneNumber = model.Phone,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    TotalAmount = model.TotalAmount
                };

                context.Orders.Add(order);
                context.SaveChanges();

                // Thêm chi tiết đơn hàng

                context.SaveChanges();

                // Xóa giỏ hàng của người dùng sau khi đặt hàng
                var cartItems = context.ShoppingCarts.Where(c => c.UserID == currentUser.UserId).ToList();
                context.ShoppingCarts.RemoveRange(cartItems);
                context.SaveChanges();
 
            }

            TempData["Message"] = "Đặt hàng thành công!";
            return RedirectToAction("OrderSuccess", "Shop");
        }
    }
}