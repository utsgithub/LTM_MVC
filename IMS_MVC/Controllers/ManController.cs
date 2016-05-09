using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS_MVC.Controllers
{
    public class ManController : Controller
    {
        // GET: Man
        public ActionResult Index()
        {
            Response.Redirect("man/man_list_intervention");
            return View();
        }


        public ActionResult man_edit_intervention() {
            return View();
        }

        // Todo: propose;
        // Todo: UserID;
        public ActionResult man_list_intervention() {
            return View();
        }
    }
}