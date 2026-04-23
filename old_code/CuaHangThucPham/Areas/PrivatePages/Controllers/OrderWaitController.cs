using CuaHangThucPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangThucPham.Areas.PrivatePages.Controllers
{
    public class OrderWaitController : Controller
    {
        private readonly CuaHangTrucTuyenEntities3 _context;

        public OrderWaitController()
        {
            _context = new CuaHangTrucTuyenEntities3(); // Khởi tạo context nếu không sử dụng Dependency Injection
        }

        [HttpGet]
        public ActionResult Index()
        {
            // Lấy danh sách đơn hàng từ cơ sở dữ liệu
            var orders = _context.Orders
                .Select(o => new CheckoutViewModel
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,  // Chú ý OrderDate là kiểu DateTime?
                    ShippingAddress = o.ShippingAddress,
                    ShippingCity = o.ShippingCity,
                    TotalAmount = o.TotalAmount,
                    Notes = o.Notes,
                    UserID = o.UserID.ToString(),  // Convert UserID to string
                    FirstName = o.FirstName,
                    LastName = o.LastName
                })
                .ToList();

            return View(orders);
        }
        [HttpGet]
        public ActionResult SusIndex()
        {


            return View(); // Trả về view SusIndex với danh sách đơn hàng đã giao thành công
        }

        // Action method cho đơn hàng bị hủy
        [HttpGet]
        public ActionResult ExIndex()
        {
            

            return View(); // Trả về view ExIndex với danh sách đơn hàng bị hủy
        }
    }
}