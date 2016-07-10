using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Z2H.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var controller = RouteData.Values["controller"];
            var action = RouteData.Values["action"];
            var id = RouteData.Values["id"];
            ViewBag.routes = $"{controller}::{action}::{id}";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Zero to Hero MVC demo project.";

            return View();
        }
    }
}