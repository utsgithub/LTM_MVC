using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
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

            Response.Redirect("man/man_list_intervention");
            return View();
        }


        public ActionResult man_edit_intervention()
        {
            return View();
        }

        // Todo: propose;
        // Todo: UserID;
        public ActionResult man_list_intervention()
        {
            //Todo: User ID
            return View(db.IntInfos.Where(x => x.Status== "Proposed").ToList());
        }
    }
}