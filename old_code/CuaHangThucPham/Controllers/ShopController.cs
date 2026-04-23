using CuaHangThucPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CuaHangThucPham.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index(int? categoryID, string sortOrder)
        {
            var products = Common.getProducts().ToList();

            // Filter by category
            if (categoryID.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryID.Value).ToList();
            }

            // Sort products based on sortOrder parameter
            switch (sortOrder)
            {
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.ProductID).ToList(); // Default sorting (optional)
                    break;
            }

            ViewBag.Categories = Common.getCategories(); // Get categories for the sidebar
            ViewBag.CurrentSort = sortOrder; // Pass sort order to view

            return View(products);
        }
        [HttpPost]
        public JsonResult AddToCart(int productId, int quantity)
        {
            var currentUser = Session["TtDangNhap"] as User;

            if (currentUser == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để sử dụng tính năng này." });
            }

            int userId = currentUser.UserId;

            using (var context = new CuaHangTrucTuyenEntities3())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == productId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại." });
                }

                var existingCartItem = context.ShoppingCarts
                    .FirstOrDefault(c => c.UserID == userId && c.ProductID == productId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    var newCartItem = new ShoppingCart
                    {
                        UserID = userId,
                        ProductID = productId,
                        Quantity = quantity
                    };
                    context.ShoppingCarts.Add(newCartItem);
                }

                context.SaveChanges();
            }

            return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng! :>" });
        }

        public ActionResult Cart()
        {
            var currentUser = Session["TtDangNhap"] as User;

            if (currentUser == null)
            {
                TempData["Message"] = "Bạn cần đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Index", "Login");
            }

            int userId = currentUser.UserId;

            using (var context = new CuaHangTrucTuyenEntities3())
            {
                var cartItems = context.ShoppingCarts
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

                return View(cartItems);
            }
        }
        public ActionResult RemoveFromCart(int productId)
        {
            var currentUser = Session["TtDangNhap"] as User;

            if (currentUser == null)
            {
                TempData["Message"] = "Bạn cần đăng nhập để xem giỏ hàng.";
                return RedirectToAction("Index", "Login");
            }

            int userId = currentUser.UserId;

            using (var context = new CuaHangTrucTuyenEntities3())
            {
                // Tìm item trong giỏ hàng của user
                var cartItem = context.ShoppingCarts.FirstOrDefault(c => c.UserID == userId && c.ProductID == productId);

                if (cartItem != null)
                {
                    // Xóa sản phẩm khỏi giỏ hàng
                    context.ShoppingCarts.Remove(cartItem);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public JsonResult UpdateCartItem(int productId, int quantity)
        {
            var currentUser = Session["TtDangNhap"] as User;

            if (currentUser == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để sử dụng tính năng này." });
            }

            int userId = currentUser.UserId;

            using (var context = new CuaHangTrucTuyenEntities3())
            {
                var cartItem = context.ShoppingCarts
                    .FirstOrDefault(c => c.UserID == userId && c.ProductID == productId);

                if (cartItem != null)
                {
                    if (quantity > 0)
                    {
                        cartItem.Quantity = quantity;
                    }
                    else
                    {
                        context.ShoppingCarts.Remove(cartItem);
                    }
                    context.SaveChanges();
                }

                return Json(new { success = true });
            }
        }

        public ActionResult OrderSuccess()
        {
            // Trả về view OrderSuccess
            return View();
        }

    }
}