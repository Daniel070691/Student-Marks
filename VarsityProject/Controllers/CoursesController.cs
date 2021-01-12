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
    public class CoursesController : Controller
    {
        VarsityDB db = new VarsityDB();
        // GET: Department
        public async Task<ActionResult> Index()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var viewModel = new CoursesViewModel
            {
                FacultyList = (from s in db.tblFaculties
                               where s.stateid == 1
                               select new SelectListItem
                               {
                                   Value = s.tid.ToString(),
                                   Text = s.title
                               })
            };

            var courses = from s in db.tblCourses
                          where s.stateid == 1
                          select s;
            viewModel.CourseList = await courses.ToListAsync();

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
            string sCourseCode = Convert.ToString(form["val-course-code"]).Trim();
            int sFacultyID = Convert.ToInt32(form["facultyDropdown"]);
            int sDuration = Convert.ToInt32(form["val-duration"]);

            var departId = form["departments"];
            //int iPageID = Convert.ToInt32(form["pagecat"]);

            var newCourse = new tblCourse();
            newCourse.title = sName;
            newCourse.courseCode = sCourseCode;
            newCourse.facultyID = sFacultyID;
            newCourse.insertdate = DateTime.Now;
            newCourse.lastupdate = DateTime.Now;
            newCourse.createdby = Convert.ToInt32(Session["TID"]);
            newCourse.updatedby = Convert.ToInt32(Session["TID"]);
            newCourse.duration = sDuration;


            intState = 1;
            sAlert = "You have successfully published " + sName;
            newCourse.stateid = intState;

            db.tblCourses.Add(newCourse);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }


            return RedirectToAction("/Index/");



        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = new CoursesViewModel();
            var tblCourse = await db.tblCourses.FindAsync(id);

            try
            {
                viewModel.Course = tblCourse;
                var tblDepart = (from a in db.tblDepartments
                                 where a.stateid == 1
                                 orderby a.tid descending
                                 select new { a.tid, a.Title }).ToList().Distinct().Select(g => new SelectListItem
                                 {
                                     Value = g.tid.ToString(),
                                     Text = g.Title
                                 });


                ViewBag.departments = new SelectList(tblDepart.Distinct(), "Value", "Text", tblCourse.tblFaculty.departmentID);
                ViewBag.departmentsId = tblCourse.tblFaculty.departmentID;
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(viewModel);
        }


        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Edit(CoursesViewModel viewModel, FormCollection form)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var intState = 1;
            string sAlert = String.Empty;

            sAlert = "You have successfully published " + Convert.ToString(form["val-title"]);

            if (!string.IsNullOrEmpty(Convert.ToString(form["val-title"])))
            {



                var mID = viewModel.Course.tid.ToString();
                long marID;
                bool res2 = long.TryParse(mID, out marID);

                var myCourse = db.tblCourses.FirstOrDefault(x => x.tid == marID);

                if (myCourse != null)
                {
                    myCourse.title = Convert.ToString(form["val-title"]);
                    myCourse.courseCode = Convert.ToString(form["val-course-code"]);
                    myCourse.facultyID = Convert.ToInt32(form["facultyDropdown"]);
                    myCourse.lastupdate = DateTime.Now;
                    myCourse.updatedby = Convert.ToInt32(Session["TID"]);
                    myCourse.stateid = intState;

                    var valuee = Convert.ToInt32(form["val-duration"]);
                    myCourse.duration = valuee;
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

            var unitOfWorkPost = db.tblCourses.Find(id);

            if (unitOfWorkPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            string sAlert = String.Empty;

            unitOfWorkPost.stateid = 4;

            sAlert = "You have successfully deleted " + unitOfWorkPost.title;

            await db.SaveChangesAsync();

            return RedirectToAction("/");

        }

        public JsonResult FilterFaculty(string departmentID)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return Json(JsonRequestBehavior.AllowGet);

           var id = departmentID;
            long TID;
            bool res = long.TryParse(id, out TID);
            var Faculties = db.tblFaculties.Where(x => x.departmentID == TID).ToList();

            if(Faculties != null)
            {
                var results = new List<selectListItem>(); 
                foreach (var item in Faculties)
                {
                    results.Add(new selectListItem { Value = item.tid, Text = item.title});
                }
                return Json(results, JsonRequestBehavior.AllowGet);
            }

            return Json(new { empty = "No Data Found under this department"});
        }

    }


}