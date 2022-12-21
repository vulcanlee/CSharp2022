using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DynamicViewContent.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            Random random = new Random();
            string viewIndex = random.Next(1, 2).ToString("D3");
            string viewName = $"~/Views/Dynamic/View{viewIndex}.cshtml";
            return View(viewName);
            //return View();
        }

        public ActionResult Dynamic(int id)
        {
            ViewBag.Message = "Your contact page.";

            string viewIndex = id.ToString("D3");
            string viewName = $"~/Views/Dynamic/View{viewIndex}.cshtml";
            return View(viewName);
            //return View();
        }
    }
}