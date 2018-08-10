using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Me_dico.it.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error_500()
        {
            return View();
        }
    }
}