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
    [Authorize(Roles = "Manager")]
    public class ManController : Controller
    {
        private ExtraDbContext db = new ExtraDbContext();

        // GET: Man
        public ActionResult Index()
        {
            return RedirectToAction("man_list_intervention");
        }

        public ActionResult man_list_intervention()
        {
            var intInfos = db.IntInfos.Include(i => i.Client).Include(i => i.IntType).Include(i => i.User);
            return View(intInfos.Where(x=>x.Status== "Proposed").ToList());
        }

        public ActionResult eng_create_client()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "DistrictName");
            return View();
        }

        public ActionResult man_edit_intervention(int? id)
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
            return View(intInfo);
        }

        // POST: Man/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IntTypeId,ClientId,SetLabour,SetCost,AspNetUserId,IntDate,Status,Comments,Reamaining,VisitDate,ApprovedByUserId")] IntInfo intInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", intInfo.ClientId);
            ViewBag.IntTypeId = new SelectList(db.IntTypes, "Id", "Name", intInfo.IntTypeId);
            return View(intInfo);
        }

        public ActionResult man_status_approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.IntInfos.Single(x => x.Id == id).Status = "Approved";
            db.SaveChanges();
            return RedirectToAction("man_edit_intervention", new { id = id });
        }

        public ActionResult man_status_cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.IntInfos.Single(x => x.Id == id).Status = "Cancelled";
            db.SaveChanges();
            return RedirectToAction("man_edit_intervention", new { id = id });
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
