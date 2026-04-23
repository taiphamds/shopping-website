using CuaHangThucPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangThucPham.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var products = Common.getProducts(); // Lấy danh sách sản phẩm từ Common.getProducts()
            return View(products); // Truyền danh sách sản phẩm vào View
        }



    }
}