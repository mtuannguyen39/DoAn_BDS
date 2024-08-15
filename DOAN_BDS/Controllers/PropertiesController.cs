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
    public class PropertiesController : Controller
    {
        private DOAN_BDSEntities db = new DOAN_BDSEntities();

        // GET: Properties
        public ActionResult Index(int? category, int? page, string SearchString, double min = double.MinValue, double max = double.MaxValue)
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
            if(min >= 0 && max > 0)
            {
                properties = db.Properties.OrderByDescending(x => x.Gia).Where(p => (float)p.Gia >= min && (float)p.Gia <= max);
            }

            // Khai báo mỗi trang 4 sản phẩm
            int pageSize = 8;
            // Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page,
            // còn nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            //Nếu page = null thì đặt lại page là 1.
            if (page == null) page = 1;

            // Trả về các BDS được phân trang theo kích thước và số trang.
            return View(properties.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ADIndex(int? category, int? page, string SearchString, double min = double.MinValue, double max = double.MaxValue)
        {
            // Tạo BDS và có tham chiếu đến category
            var properties = db.Properties.Include(p => p.Category);
            //tìm kiếm chuỗi truy vấn theo category
            if (category == null)
            {
                properties = db.Properties.OrderByDescending(x => x.TenBatDongSan);
            }
            else
            {
                properties = db.Properties.OrderByDescending(x => x.IDdanhmuc).Where(x => x.IDdanhmuc == category);
            }
            //tìm theo tên
            if (!String.IsNullOrEmpty(SearchString))
            {
                properties = properties.Where(s => s.TenBatDongSan.Contains(SearchString));
            }    
            if(min >= 0 && max > 0)
            {
                properties = db.Properties.OrderByDescending(x => x.Gia).Where(p => (float)p.Gia >= min && (float)p.Gia <= max);
            }
            return View(properties.ToList());
        }


        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            ViewBag.IDdanhmuc = new SelectList(db.Categories, "ID", "TenDanhMuc");
            ViewBag.NguoiDangTinID = new SelectList(db.Users, "ID", "TenDangNhap");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenBatDongSan,DiaChi,DienTich,Gia,SoPhongNgu,SoPhongTam,TienIch,MoTa,HinhAnh,NguoiDangTinID,IDdanhmuc,DVT")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Main", "Home");
            }

            ViewBag.IDdanhmuc = new SelectList(db.Categories, "ID", "TenDanhMuc", property.IDdanhmuc);
            ViewBag.NguoiDangTinID = new SelectList(db.Users, "ID", "TenDangNhap", property.NguoiDangTinID);
            return View(property);
        }

        public ActionResult CreateAD()
        {
            ViewBag.IDdanhmuc = new SelectList(db.Categories, "ID", "TenDanhMuc");
            ViewBag.NguoiDangTinID = new SelectList(db.Users, "ID", "TenDangNhap");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAD([Bind(Include = "ID,TenBatDongSan,DiaChi,DienTich,Gia,SoPhongNgu,SoPhongTam,TienIch,MoTa,HinhAnh,NguoiDangTinID,IDdanhmuc,DVT")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("ADIndex", "Properties");
            }

            ViewBag.IDdanhmuc = new SelectList(db.Categories, "ID", "TenDanhMuc", property.IDdanhmuc);
            ViewBag.NguoiDangTinID = new SelectList(db.Users, "ID", "TenDangNhap", property.NguoiDangTinID);
            return View(property);
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDdanhmuc = new SelectList(db.Categories, "ID", "TenDanhMuc", property.IDdanhmuc);
            ViewBag.NguoiDangTinID = new SelectList(db.Users, "ID", "TenDangNhap", property.NguoiDangTinID);
            return View(property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenBatDongSan,DiaChi,DienTich,Gia,SoPhongNgu,SoPhongTam,TienIch,MoTa,HinhAnh,NguoiDangTinID,IDdanhmuc,DVT")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ADIndex");
            }
            ViewBag.IDdanhmuc = new SelectList(db.Categories, "ID", "TenDanhMuc", property.IDdanhmuc);
            ViewBag.NguoiDangTinID = new SelectList(db.Users, "ID", "TenDangNhap", property.NguoiDangTinID);
            return View(property);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
            db.SaveChanges();
            return RedirectToAction("ADIndex");
        }

        public PartialViewResult CategoryPartial()
        {
            var cateList = db.Categories.ToList();
            return PartialView(cateList);
        }
        
        public ActionResult BDSLienQuan()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);    
        }
        public ActionResult ViewDetails(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }
    }
}
