using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;
using VarsityProject.ViewModels;

namespace VarsityProject.Controllers
{
    public class FacultyController : Controller
    {

        VarsityDB db = new VarsityDB();
        // GET: Department
        public async Task<ActionResult> Index()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var viewModel = new FacultyViewModel
            {
                Departments = (from s in db.tblDepartments
                               where s.stateid == 1
                                 select new SelectListItem
                                 {
                                     Value = s.tid.ToString(),
                                     Text = s.Title
                                 })
            };
            var faculties = from s in db.tblFaculties
                              where s.stateid == 1
                              select s;
            viewModel.FacultyList = await faculties.ToListAsync();

            return View(viewModel);
        }

        //Create: Fundraising
        public ActionResult Create()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");
            var viewModel = new FacultyViewModel
            {
                Departments = (from s in db.tblDepartments
                               where s.stateid == 1
                               select new SelectListItem
                               {
                                   Value = s.tid.ToString(),
                                   Text = s.Title
                               })
            };

            ViewBag.departments = new SelectList(viewModel.Departments.Distinct(), "Value", "Text");

            return View(viewModel);
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Create(FacultyViewModel viewModel, FormCollection form)
        {
            if (Session["uname"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            string sAlert = "";
            int intState = 1;
            string sName = Convert.ToString(form["val-title"]).Trim();

            var departId = form["departments"];
            //int iPageID = Convert.ToInt32(form["pagecat"]);

            var newFaculty = new tblFaculty();
            newFaculty.title = sName;
            newFaculty.departmentID = Convert.ToInt32(departId);
            newFaculty.insertdate = DateTime.Now;
            newFaculty.lastupdate = DateTime.Now;
            newFaculty.createdby = Convert.ToInt32(Session["TID"]);
            newFaculty.updatedby = Convert.ToInt32(Session["TID"]);



            intState = 1;
            sAlert = "You have successfully published " + sName;
            newFaculty.stateid = intState;

            db.tblFaculties.Add(newFaculty);


            await db.SaveChangesAsync();


            return RedirectToAction("/Index/");



        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = new FacultyViewModel();
            var tblFaculty = await db.tblFaculties.FindAsync(id);

            try
            {
                viewModel.Faculty.title = tblFaculty.title;
                var tblDepart = (from a in db.tblDepartments
                                  where a.stateid == 1
                                  orderby a.tid descending
                                  select new { a.tid, a.Title }).ToList().Distinct().Select(g => new SelectListItem
                                  {
                                      Value = g.tid.ToString(),
                                      Text = g.Title
                                  });


                ViewBag.departments = new SelectList(tblDepart.Distinct(), "Value", "Text", tblFaculty.departmentID);
                viewModel.Faculty.tid = tblFaculty.tid;
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(viewModel);
        }


        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Edit(FacultyViewModel viewModel, FormCollection form)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var intState = 1;
            string sAlert = String.Empty;

            sAlert = "You have successfully published " + Convert.ToString(form["val-title"]);

            if (!string.IsNullOrEmpty(Convert.ToString(form["val-title"])))
            {



                var mID = viewModel.Faculty.tid.ToString();
                long marID;
                bool res2 = long.TryParse(mID, out marID);

                var myFaculty = db.tblFaculties.FirstOrDefault(x => x.tid == marID);

                if (myFaculty != null)
                {
                    myFaculty.title = Convert.ToString(form["val-title"]);
                    myFaculty.departmentID = Convert.ToInt32(form["departments"]);
                    myFaculty.lastupdate = DateTime.Now;
                    myFaculty.updatedby = Convert.ToInt32(Session["TID"]);
                    myFaculty.stateid = intState;
                    try
                    {

                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    return RedirectToAction("/");

                }

            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var unitOfWorkPost = db.tblFaculties.Find(id);

            if (unitOfWorkPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            string sAlert = String.Empty;

            unitOfWorkPost.stateid = 4;

            sAlert = "You have successfully deleted " + unitOfWorkPost.title;

            await db.SaveChangesAsync();

            return RedirectToAction("/");

        }
    }
}