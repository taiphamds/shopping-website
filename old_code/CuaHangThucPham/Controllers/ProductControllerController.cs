using System.Linq;
using System.Web.Mvc;
using CuaHangThucPham.Models;

namespace CuaHangThucPham.Controllers
{
    public class ProductController : Controller
    {
        private CuaHangTrucTuyenEntities3 db = new CuaHangTrucTuyenEntities3();

        public ActionResult Details(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult AddToCart(int productId)
        {
            var currentUser = Session["TtDangNhap"] as User;

            if (currentUser == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để sử dụng tính năng này." }, JsonRequestBehavior.AllowGet);
            }

            int userId = currentUser.UserId;

            using (var context = new CuaHangTrucTuyenEntities3())
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == productId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại." }, JsonRequestBehavior.AllowGet);
                }

                var existingCartItem = context.ShoppingCarts
                    .FirstOrDefault(c => c.UserID == userId && c.ProductID == productId);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += 1;
                }
                else
                {
                    var newCartItem = new ShoppingCart
                    {
                        UserID = userId,
                        ProductID = productId,
                        Quantity = 1
                    };
                    context.ShoppingCarts.Add(newCartItem);
                }

                context.SaveChanges();
            }

            return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng." }, JsonRequestBehavior.AllowGet);
        }

    }
}
