using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IMS_MVC.Controllers
{
    [Authorize(Roles = "SiteEngineer")]
    public class EngController : Controller
    {
        private ExtraDbContext db = new ExtraDbContext();
        // GET: Eng
        public ActionResult Index()
        {
            return RedirectToAction("eng_dashboard");
        }

        public ActionResult eng_create_client()
        {
            string userid = User.Identity.GetUserId();
            User user = db.Users.Where(x => x.AspNetUserId == userid).First();
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName", user.DistrictId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult eng_create_client([Bind(Include = "Id,Name,Descriptive,DistrictId")] Client client)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                User user = db.Users.Where(x => x.AspNetUserId == userid).First();
                client.DistrictId = user.DistrictId;
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("eng_list_client");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName", client.DistrictId);
            return View(client);
        }

        public ActionResult eng_create_intervention(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntInfo intInfo = new IntInfo();
            intInfo.ClientId = id.GetValueOrDefault(0);
            ViewBag.IntTypeId = new SelectList(db.IntTypes, "Id", "Name");
            return View(intInfo);
        }

        // POST: IntInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult eng_create_intervention(int IntTypeId, int? SetLabour, int? SetCost, int ClientId)
        {
            // Todo: Default Labour and Cost
            int FinalLabour = SetLabour.GetValueOrDefault(0);
            int FinalCost = SetCost.GetValueOrDefault(0);
            if (FinalLabour == 0 || FinalCost == 0)
            {
                IntType intType = db.IntTypes.Where(x => x.Id == IntTypeId).First();
                if (FinalCost == 0) { FinalCost = intType.Cost; }
                if (FinalLabour == 0) { FinalLabour = intType.Labour; }
            }
            string currentUserId = User.Identity.GetUserId();
            User user = db.Users.Where(x => x.AspNetUserId == currentUserId).First();
            IntInfo intInfo = new IntInfo();
            intInfo.AspNetUserId = User.Identity.GetUserId();
            intInfo.ClientId = ClientId;
            intInfo.IntTypeId = IntTypeId;
            intInfo.SetLabour = FinalCost;
            intInfo.SetCost = FinalCost;
            intInfo.Status = "Proposed";
            intInfo.IntDate = DateTime.Now;
            intInfo.UserId = user.Id;
            if (ModelState.IsValid)
            {

                db.IntInfos.Add(intInfo);
                db.SaveChanges();
                return RedirectToAction("eng_list_int_via_client", new { id = intInfo.ClientId });
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", intInfo.ClientId);
            ViewBag.IntTypeId = new SelectList(db.IntTypes, "Id", "Name", intInfo.IntTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "AspNetUserId", intInfo.UserId);
            return View(intInfo);
        }

        public ActionResult eng_dashboard()
        {
            return View();
        }

        public ActionResult eng_detail_client(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        public ActionResult eng_detail_intervention(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntInfo intInfo = db.IntInfos.Find(id);
            if (intInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", intInfo.ClientId);
            ViewBag.IntTypeId = new SelectList(db.IntTypes, "Id", "Name", intInfo.IntTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "AspNetUserId", intInfo.UserId);
            return View(intInfo);
        }

        // POST: IntInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult eng_detail_intervention(IntInfo intInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("eng_detail_intervention", new { id = intInfo.Id });
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", intInfo.ClientId);
            ViewBag.IntTypeId = new SelectList(db.IntTypes, "Id", "Name", intInfo.IntTypeId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "AspNetUserId", intInfo.UserId);
            return View(intInfo);
        }
        public ActionResult eng_edit_intervention()
        {
            return View();
        }
        public ActionResult eng_list_client()
        {
            string userid = User.Identity.GetUserId();
            User user = db.Users.Where(x => x.AspNetUserId == userid).First();
            var clients = db.Clients.Include(c => c.District);
            return View(clients.Where(x => x.DistrictId == user.DistrictId).ToList());
        }
        public ActionResult eng_list_intervention()
        {
            var intInfos = db.IntInfos.Include(i => i.Client).Include(i => i.IntType).Include(i => i.User);
            if (intInfos == null)
            {
                return HttpNotFound();
            }
            string userID = User.Identity.GetUserId();
            return View(intInfos.Where(x => x.AspNetUserId == userID).ToList());
        }
        public ActionResult eng_list_int_via_client(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var intInfos = db.IntInfos.Include(i => i.Client).Include(i => i.IntType).Include(i => i.User);
            if (intInfos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ViewID = id;
            return View(intInfos.Where(x => x.ClientId == id).ToList());
        }
    }
}