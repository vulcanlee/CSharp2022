using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class DynamicViewController : Controller
    {
        public ActionResult GetView(string id)
        {
            var person = new Person
            {
                Name = "Andy"
            };

            var foo = $"/View/DynamicView/{id}.cshtml";
            return View(id, person);
        }
    }
}