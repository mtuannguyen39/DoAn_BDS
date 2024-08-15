using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN_BDS.Models;

namespace DOAN_BDS.Controllers
{
    public class UsersController : Controller
    {
        private DOAN_BDSEntities db = new DOAN_BDSEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenDangNhap,MatKhau,HoTen,DienThoai")] User user)
        {
            if (ModelState.IsValid)
            {
                user.TrangThai = false; // gắn mặc định cho Trạng Thái là false
               
                var check = db.Users.Where(s => s.TenDangNhap == user.TenDangNhap).FirstOrDefault();
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("LoginCus");
                }
                else
                {
                    ViewBag.ErrorInfo = "Tài khoản đã tồn tại";
                    return View();
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenDangNhap,MatKhau,HoTen,DienThoai,DiaChi,QuyenHan,TrangThai")] User user)
        {
            if (ModelState.IsValid)
            {
                user.QuyenHan = "Người đăng tin";
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult EditAD(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAD([Bind(Include = "ID,TenDangNhap,MatKhau,HoTen,DienThoai,DiaChi,QuyenHan,TrangThai")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult LoginCus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAccount(User _user)
        {
            var check = db.Users.Where(s => s.TenDangNhap == _user.TenDangNhap && s.MatKhau == _user.MatKhau).FirstOrDefault();
            if(check == null)
            {
                ViewBag.ErrorInfo = "Không có KH này";
                return View("LoginCus");
            }
            else
            {
                // Có tồn tại KH 
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = check.ID;
                Session["TenDangNhap"] = check.TenDangNhap;
                Session["MatKhau"] = check.MatKhau;
                Session["HoTen"] = check.HoTen;
                Session["DienThoai"] = check.DienThoai;
                Session["DiaChi"] = check.DiaChi;
                Session["QuyenHan"] = check.QuyenHan;
                Session["TrangThai"] = check.TrangThai;
                // Quay lại trang mani với thông tin cần thiết
                return RedirectToAction("./main", "Home");
            }
        }

        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("./main", "Home"); 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
