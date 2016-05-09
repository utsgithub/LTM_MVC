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

    public class EngController : Controller
    {
        private ExtraDbContext db = new ExtraDbContext();
        // GET: Eng
        public ActionResult Index()
        {
            Response.Redirect("eng/eng_dashboard");
            return View();
        }

        public ActionResult eng_create_client()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult eng_create_client([Bind(Include = "Id,Name,Descriptive,DistrictId")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("eng_list_client");
            }

            return View(client);
        }
        public ActionResult eng_create_intervention()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult eng_create_intervention([Bind(Include = "Id,IntTypeId,ClientId,SetLabour,SetCost,UserId,IntDate,Status,Comments,Reamaining,VisitDate,ApprovedByUserId")] IntInfo intInfo)
        {
            if (ModelState.IsValid)
            {
                db.IntInfos.Add(intInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(intInfo);
        }
        public ActionResult eng_dashboard()
        {
            return View();
        }
        public ActionResult eng_detail_client()
        {
            return View();
        }
        public ActionResult eng_detail_intervention()
        {
            return View();
        }
        public ActionResult eng_edit_intervention()
        {
            return View();
        }
        public ActionResult eng_list_client()
        {
            return View();
        }
        public ActionResult eng_list_intervention()
        {
            return View();
        }

        public ActionResult eng_list_int_via_client()
        {
            return View();
        }
    }
}