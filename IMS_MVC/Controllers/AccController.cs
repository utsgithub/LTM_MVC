using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using IMS_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IMS_MVC.Controllers
{
    [Authorize(Roles = "Accountant")]
    public class AccController : Controller
    {
        private ExtraDbContext db = new ExtraDbContext();
        // GET: Acc
        public ActionResult index()
        {
            Response.Redirect("acc/acc_dashboard");
            return View();
        }

        public ActionResult acc_dashboard()
        {
            return View();
        }

        public ActionResult acc_list_report(int? type)
        {
            Debug.WriteLine("Debugging acc_list_report");

            if(type != null)
            {
                // if not null then run the reports
            }
            else
            {
                //return view();
            }

            List<IntInfo> interventions = db.IntInfos.ToList();

            var results =
                from i in interventions
                //where i.Status.Equals("Completed") //need this later
                group i by i.AspNetUserId
                into g
                select new { Id = g.Key, TotalLabour = g.Sum(i => i.SetLabour), TotalCost = g.Sum(i => i.SetCost) };

            foreach (var result in results)
            {
                Debug.WriteLine(result.Id + '\t' + result.TotalLabour + ", " + result.TotalCost);
            }

            return View();
        }
        //Todo: Connect User table
        //Todo: Connect District table
        public ActionResult Acc_List_Users()
        {
            List<ViewUser> model = new List<ViewUser>();
            ApplicationDbContext appdbcontext = new ApplicationDbContext();
            List<ApplicationUser> aspusers = appdbcontext.Users.ToList();
            List<User> users = db.Users.ToList();
            string rolesForUser;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            foreach (User user in users)
            {
                rolesForUser = userManager.GetRoles(user.AspNetUserId).First();
                if (rolesForUser != "Accountant")
                {
                    District Dis = db.Districts.Where(x => x.Id == user.DistrictId).First();
                    ApplicationUser appuser = aspusers.Where(x => x.Id == user.AspNetUserId).First();
                    ViewUser vuser = new ViewUser();
                    vuser.Id = user.Id;
                    vuser.DistrictName = Dis.DistrictName;
                    vuser.UserName = appuser.UserName;
                    vuser.Role = rolesForUser;
                    model.Add(vuser);
                }
            }
            return View(model);
        }

        public ActionResult acc_edit_district(int? id)
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
            ApplicationDbContext appdbcontext = new ApplicationDbContext();
            List<ApplicationUser> aspusers = appdbcontext.Users.ToList();
            string rolesForUser;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            rolesForUser = userManager.GetRoles(user.AspNetUserId).First();
            District Dis = db.Districts.Where(x => x.Id == user.DistrictId).First();
            ApplicationUser appuser = aspusers.Where(x => x.Id == user.AspNetUserId).First();
            ViewUser vuser = new ViewUser();
            vuser.Id = user.Id;
            vuser.DistrictId = user.DistrictId;
            vuser.DistrictName = Dis.DistrictName;
            vuser.UserName = appuser.UserName;
            vuser.Role = rolesForUser;

            List<District> dicts = db.Districts.ToList();
            IEnumerable<SelectListItem> items = 
                db.Districts.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DistrictName });


            ViewBag.DistrictsName = items.ToList();
            return View(vuser);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult acc_edit_district(int DistrictId, int Id)
        {
            db.Users.Single(x => x.Id == Id).DistrictId = DistrictId;
            db.SaveChanges();
            return RedirectToAction("Acc_List_Users");
        }
    }
}