using System.Linq;
using System.Web.Mvc;
using CuaHangThucPham.Models;

namespace CuaHangThucPham.Areas.PrivatePages.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CuaHangTrucTuyenEntities3 _context;

        public DashboardController()
        {
            _context = new CuaHangTrucTuyenEntities3(); // Khởi tạo context nếu không sử dụng Dependency Injection
        }

        [HttpGet]
        public ActionResult Index()
        {
            // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            var products = _context.Products
        .Join(_context.Categories,
              p => p.CategoryID,
              c => c.CategoryID,
              (p, c) => new CartItemViewModel
              {
                  ProductID = p.ProductID,
                  ProductName = p.ProductName,
                  Description = p.Description,
                  Price = p.Price,
                  Quantity = p.Quantity,
                  img = p.img,
                  CategoryName = c.CategoryName // Thêm CategoryName vào view model
              })
        .ToList();

            return View(products);
        }
    }
}