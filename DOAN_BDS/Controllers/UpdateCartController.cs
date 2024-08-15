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
    public class UpdateCartController : Controller
    {
        private DOAN_BDSEntities db = new DOAN_BDSEntities();
        // GET: UpdateCart
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Upgrade(int? ID)
        {
            int id = (int)Session["ID"];
            User user = db.Users.Find(id);
            user.TrangThai = true;
            db.SaveChanges();
            return RedirectToAction("Checkout_Success", "UpdateCart");
        }

        public ActionResult Checkout_Success()
        {
            return View();
        }
    }
}