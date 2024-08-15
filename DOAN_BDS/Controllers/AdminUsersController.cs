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
    public class AdminUsersController : Controller
    {
        private DOAN_BDSEntities db = new DOAN_BDSEntities();

        // GET: AdminUsers
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(AdminUser _user)
        {
            var check = db.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai Info";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"] = _user.ID;
                Session["NameUser"] = _user.NameUser;
                Session["PasswodUser"] = _user.PasswordUser;
                Session["RoleUser"] = check.RoleUser;
                //var ch = Session["chon"];
                //return RedirectToAction("ProductList", "Products");
                if (check.RoleUser.ToString() == "QLBDS")
                    return RedirectToAction("ADIndex", "Properties");

                else if (check.RoleUser.ToString() == "DangTin")
                    return RedirectToAction("Index", "POSTs");

                else if (check.RoleUser.ToString() == "taichinh")
                    return RedirectToAction("Index", "Transactions");

                else if (check.RoleUser.ToString() == "Admin")
                    return RedirectToAction("./viewAD", "AdminUsers");

                else if (check.RoleUser.ToString() == "QuanLy")
                    return RedirectToAction("./index", "Users");

                else
                    return RedirectToAction("Index", "LoginUser");
            }
        }

        public ActionResult LogOutUserAD()
        {
            Session.Remove("NameUser");
            return RedirectToAction("./index", "AdminUsers");
        }

        [HttpGet] 
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = db.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.AdminUsers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index", "AdminUsers");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exist";
                    return View();
                }
            }
            return View();
        }

        public ActionResult ViewAD()
        {
            return View();
        }

    }
}
