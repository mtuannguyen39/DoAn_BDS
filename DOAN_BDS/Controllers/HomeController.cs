using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN_BDS.Models;
using PagedList;


namespace DOAN_BDS.Controllers
{
    public class HomeController : Controller
    {
        private DOAN_BDSEntities db = new DOAN_BDSEntities();
        public ActionResult Main(int? category, int? page, string SearchString, double min = double.MinValue, double max = double.MaxValue)
        {
            // Tạo BDS và có tham chiếu đến category
            var properties = db.Properties.Include(p => p.Category);
            // tìm kiếm chuỗi truy vấn theo category
            if (category == null)
            {
                properties = db.Properties.OrderByDescending(x => x.TenBatDongSan);
            }
            else
            {
                properties = db.Properties.OrderByDescending(x => x.IDdanhmuc).Where(x => x.IDdanhmuc == category);
            }
            // tìm kiếm theo tên
            if (!String.IsNullOrEmpty(SearchString))
            {
                properties = properties.Where(s => s.TenBatDongSan.ToLower().Contains(SearchString));
            }
            if (min >= 0 && max > 0)
            {
                properties = db.Properties.OrderByDescending(x => x.Gia).Where(p => (float)p.Gia >= min && (float)p.Gia <= max);
            }

            // Khai báo mỗi trang 4 sản phẩm
            int pageSize = 4;
            // Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page,
            // còn nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //Nếu page = null thì đặt lại page là 1.
            if (page == null) page = 1;

            // Trả về các BDS được phân trang theo kích thước và số trang.
            return View(properties.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult CategoryPartial()
        {
            var cateList = db.Categories.ToList();
            return PartialView(cateList);
        }

    }
}