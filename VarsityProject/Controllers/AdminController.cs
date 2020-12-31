using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.ViewModels;
using VarsityProject.Models;

namespace VarsityProject.Controllers
{
    public class AdminController : Controller
    {
        VarsityDB db = new VarsityDB();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentLogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogIn(FormCollection fc)
        {
            var username = fc["val-email"].ToString();
            var password = fc["val-password"].ToString();

            var user = db.tblStudents.FirstOrDefault(x => x.email == username && x.password == password);

            if (user != null)
            {

                //// Lastest Login Date
                string sUserName = user.Name.ToString();
                if (!string.IsNullOrEmpty(sUserName))
                {
                    Session["names"] = user.Name + " " + user.Surname;
                    Session["role"] = "Student";
                    var name = Session["imageName"];
                    Session["TID"] = user.TID;
                }

                return RedirectToAction("Student", "Dashboard");
            }
            return RedirectToAction("StudentLogIn", "Admin");
        }

        public ActionResult LectureLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LectureLogin(FormCollection fc)
        {
            var username = fc["val-email"].ToString();
            var password = fc["val-password"].ToString();

            var user = db.tblLecturers.FirstOrDefault(x => x.Email == username && x.Password == password);

            if (user != null)
            {

                //// Lastest Login Date
                string sUserName = user.Name.ToString();
                if (!string.IsNullOrEmpty(sUserName))
                {
                    Session["names"] = user.Name + " " + user.Surname;
                    Session["role"] = "Lecturer";
                    var name = Session["imageName"];
                    Session["TID"] = user.TID;
                }

                return RedirectToAction("Administrator", "Dashboard");
            }
            return RedirectToAction("Lecturer", "Dashboard");
        }

        public ActionResult AdministratorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdministratorLogin(FormCollection fc, string flag)
        {
            var username = fc["val-email"].ToString();
            var password = fc["val-password"].ToString();

            var user = db.tblAdministrators.FirstOrDefault(x => x.Email == username && x.Password == password);

            if (user != null)
                {

                    //// Lastest Login Date
                    string sUserName = user.Name.ToString();
                    if (!string.IsNullOrEmpty(sUserName))
                    {
                        Session["names"] = user.Name + " " + user.Surname;
                        Session["role"] = "Administrator";
                        var name = Session["imageName"];
                        Session["TID"] = user.TID;
                    }

                    return RedirectToAction("Administrator", "Dashboard");
                }
            return RedirectToAction("AdministratorLogin","Admin");
        }
    }
}