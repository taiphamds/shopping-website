using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using System.Data.Entity;

namespace CuaHangThucPham.Models
{
    public class Common
    {
        public static List<Product> getProducts()
        {
            List<Product> l = new List<Product>();
            DbContext cn = new DbContext("name =CuaHangTrucTuyenEntities3");
            l = cn.Set<Product>().ToList<Product>();
            return l;
        }
        public static List<Category> getCategories()
        {
            return new DbContext("name = CuaHangTrucTuyenEntities3").Set<Category>().ToList<Category>();
        }
    }
}

