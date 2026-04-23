using CuaHangThucPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangThucPham.Areas.PrivatePages.Controllers
{
    public class UserController : Controller
    {
        private readonly CuaHangTrucTuyenEntities3 _context;

        public UserController()
        {
            _context = new CuaHangTrucTuyenEntities3(); // Khởi tạo context
        }

        // GET: PrivatePages/User
        public ActionResult Index()
        {
            var users = _context.Users.ToList(); // Lấy danh sách người dùng từ cơ sở dữ liệu
            return View(users); // Trả về view Index với danh sách người dùng
        }

        // GET: PrivatePages/User/Details/5
        public ActionResult Details(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id); // Tìm người dùng theo ID
            if (user == null)
            {
                return HttpNotFound(); // Trả về lỗi 404 nếu người dùng không tìm thấy
            }
            return View(user); // Trả về view Details với thông tin người dùng
        }
    }
}