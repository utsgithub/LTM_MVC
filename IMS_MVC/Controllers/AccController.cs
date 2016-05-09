using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS_MVC.Models;

namespace IMS_MVC.Controllers
{
    public class AccController : Controller
    {
        private ExtraDbContext db = new ExtraDbContext();
        // GET: Acc
        public ActionResult Acc_Dashboard()
        {
            return View();
        }

        //Todo: Connect User table
        //Todo: Connect District table
        public ActionResult Acc_List_Users() {
            return View(db.Users.Where(x=>x.UserType!="Accountant").ToList());
        }

        public ActionResult acc_edit_district(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult acc_edit_district([Bind(Include = "UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Acc_List_Users");
            }
            return View(user);
        }
    }
}