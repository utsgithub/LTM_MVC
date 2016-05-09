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
    public class IntInfoesController : Controller
    {
        private ExtraDbContext db = new ExtraDbContext();

        // GET: IntInfoes
        public ActionResult Index()
        {
            return View(db.IntInfos.ToList());
        }

        // GET: IntInfoes/Details/5
        public ActionResult Details(int? id)
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

        // GET: IntInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IntInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IntTypeId,ClientId,SetLabour,SetCost,UserId,IntDate,Status,Comments,Reamaining,VisitDate,ApprovedByUserId")] IntInfo intInfo)
        {
            if (ModelState.IsValid)
            {
                db.IntInfos.Add(intInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(intInfo);
        }

        // GET: IntInfoes/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: IntInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IntTypeId,ClientId,SetLabour,SetCost,UserId,IntDate,Status,Comments,Reamaining,VisitDate,ApprovedByUserId")] IntInfo intInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(intInfo);
        }

        // GET: IntInfoes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: IntInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IntInfo intInfo = db.IntInfos.Find(id);
            db.IntInfos.Remove(intInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
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
