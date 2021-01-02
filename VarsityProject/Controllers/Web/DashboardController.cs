using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VarsityProject.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        // GET: Dashboard
        public ActionResult Administrator()
        {
            return View();
        }
        // GET: Dashboard
        public ActionResult Lecture()
        {
            return View();
        }
        // GET: Dashboard
        public ActionResult Student()
        {
            return View();
        }
    }
}