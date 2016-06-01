﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using IMS_MVC.Models;
using System.Threading.Tasks;

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

            SendEmailNotification();

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

            //SendEmailNotification();

            return RedirectToAction("man_edit_intervention", new { id = id });
        }

        private void SendEmailNotification()
        {
            // In order for this to work with, Gmail must enable less secure apps settings
            // TODO: Need to figure out the design of the email body and also the number of emails
            // required for each of the users

            /** List of Emails Created so far **
            eng1.ims.monitor@gmail.com    
            man1.ims.monitor@gmail.com
            acc1.ims.monitor@gmail.com

            ## All use the same password as the intervention.monitor email **/
            MailMessage mail = new MailMessage();
            mail.To.Add("Ashfaqur.M.Rahman@student.uts.edu.au");
            mail.CC.Add("mahie.rahman@gmail.com");
            mail.CC.Add("intervention.monitor@gmail.com");
            mail.CC.Add("eng1.ims.monitor@gmail.com");
            mail.CC.Add("man1.ims.monitor@gmail.com");
            mail.CC.Add("acc1.ims.monitor@gmail.com");
            mail.From = new MailAddress("intervention.monitor@gmail.com");
            mail.Subject = "Intervention Monitor Test Email";
            mail.Body = "The body of the email will reside here";

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "intervention.monitor@gmail.com",
                    Password = "Intervention1!"
                };
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
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
