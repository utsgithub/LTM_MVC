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
using System.Dynamic;
using System.Globalization;

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

        //TODO: Must Implement the status checks in each of the Linq queries for Completed interventions only
        public ActionResult acc_list_report(int? report)
        {
            List<IntInfo> interventions = db.IntInfos.ToList();

            switch (report)
            {
                case 1: // Report 1 = Total Costs by Engineer
                    var report1 =
                        from i in interventions
                        where i.Status.Equals("Completed")
                        group i by i.User.UserName
                        into g
                        select new
                        {
                            User = g.Key,
                            TotalLabour = g.Sum(i => i.SetLabour),
                            TotalCost = ((double)g.Sum(i => i.SetCost)).ToString("N", new CultureInfo("en-US"))
                        } into selection
                        orderby selection.User
                        select selection.ToExpando();
                    return View(report1);
                case 2: // Report 2 - Average Costs by Engineer
                    var report2 =
                        from i in interventions
                        where i.Status.Equals("Completed") 
                        group i by i.User.UserName
                        into g
                        select new
                        {
                            User = g.Key,
                            AvgLabour = Math.Round((double)g.Average(i => i.SetLabour)),
                            AvgCost = Math.Round((double)g.Average(i => i.SetCost))
                        } into selection
                        orderby selection.User
                        select selection.ToExpando();
                    return View(report2);
                case 3: // Report 3 - Costs by District
                    var report3 =
                        from i in interventions
                        where i.Status.Equals("Completed") 
                        group i by i.Client.District
                        into g
                        select new
                        {
                            District = g.Key.DistrictName,
                            TotalLabour = g.Sum(i => i.SetLabour),
                            TotalCost = ((double)g.Sum(i => i.SetCost)).ToString("N", new CultureInfo("en-US"))
                        }.ToExpando();

                    var GrandTotalLabour = interventions.Where(x => x.Status == "Completed").Sum(x => x.SetLabour); 
                    var GrandTotalCost = ((double)interventions.Where(x => x.Status == "Completed").Sum(x => x.SetCost)).ToString("N", new CultureInfo("en-US")); //Where(x => x.Status == "Completed").

                    List<dynamic> grouped = new List<dynamic>();
                    grouped.Add(report3);
                    grouped.Add(GrandTotalLabour);
                    grouped.Add(GrandTotalCost);

                    return View(grouped);
                case 4: // Report 4 - Monthly Costs for District

                    List<District> districts = db.Districts.ToList();
                    
                    return View(districts);
                case null:
                default: break;
            }

            return View();
        }


        public ActionResult LoadPartialView(int? district_id)
        {
            List<IntInfo> interventions = db.IntInfos.Where(x => x.Status == "Completed").ToList();
            var monthly_labour_cost = new List<Tuple<string, int, int>>();

            for (int i = 1; i <= 12; i++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                var month_district = interventions.Where(x => x.IntDate.GetValueOrDefault().Month == i && x.Client.DistrictId == district_id);

                if (month_district.Count() > 0)
                {
                    int total_labour = 0, total_cost = 0;

                    foreach (var info in month_district)
                    {
                        if (info.IntDate != null)
                        {
                            total_labour += info.SetLabour.Value;
                            total_cost += info.SetCost.Value;
                        }
                    }
                    monthly_labour_cost.Add(new Tuple<string, int, int>(monthName, total_labour, total_cost));
                }
                else
                {
                    monthly_labour_cost.Add(new Tuple<string, int, int>(monthName, 0, 0));
                }
            }

            return PartialView("_ViewMonthly", monthly_labour_cost);
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

    /// <summary>
    /// This static class is conveniently and effectively borrowed from this website and used 
    /// by the for building the reports.
    /// http://www.dotnetfunda.com/articles/show/2655/binding-views-with-anonymous-type-collection-in-aspnet-mvc
    /// </summary>
    public static class Extensions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }
    }
}