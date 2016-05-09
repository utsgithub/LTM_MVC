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
    public class IntTypesController : Controller
    {
        private ExtraDbContext db = new ExtraDbContext();

        // GET: IntTypes
        public ActionResult Index()
        {
            return View(db.IntTypes.ToList());
        }

        // GET: IntTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntType intType = db.IntTypes.Find(id);
            if (intType == null)
            {
                return HttpNotFound();
            }
            return View(intType);
        }

        // GET: IntTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IntTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Labour,Cost")] IntType intType)
        {
            if (ModelState.IsValid)
            {
                db.IntTypes.Add(intType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(intType);
        }

        // GET: IntTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntType intType = db.IntTypes.Find(id);
            if (intType == null)
            {
                return HttpNotFound();
            }
            return View(intType);
        }

        // POST: IntTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Labour,Cost")] IntType intType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(intType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(intType);
        }

        // GET: IntTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntType intType = db.IntTypes.Find(id);
            if (intType == null)
            {
                return HttpNotFound();
            }
            return View(intType);
        }

        // POST: IntTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IntType intType = db.IntTypes.Find(id);
            db.IntTypes.Remove(intType);
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
