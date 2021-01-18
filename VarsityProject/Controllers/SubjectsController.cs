using System;
using System.Collections;
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
    public class SubjectsController : Controller
    {

        VarsityDB db = new VarsityDB();
        // GET: Department
        public ActionResult Index()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var courses = db.tblCourses.Include(x => x.tblFaculty).Where(x => x.stateid == 1)
    .OrderBy(x => x.tblFaculty.title).ThenBy(x => x.title);

            var viewModel = new SubjectViewModel();
            //{
            //    Courses = new SelectList(courses, "tid", "title", "tblFaculty.title", null, null)
            //};
            
            var subjects = db.tblSubjects.Where(x => x.stateid == 1).ToList();

            viewModel.SubjectList = new List<Subject>();
            foreach (var item in subjects)
            {
                var coursesBrigde = db.tblCourseSubjects.Where(x => x.SubjectID == item.tid && x.stateid == 1).ToList();

                var CoursesLinked = new List<Course>();
                foreach (var item2 in coursesBrigde)
                {
                    var course = db.tblCourses.Where(x => x.tid == item2.CourseID).FirstOrDefault();
                    CoursesLinked.Add(new Course { TID = Convert.ToInt32(course.tid), Title = course.title });
                }

                viewModel.SubjectList.Add(new Subject { TID = Convert.ToInt32(item.tid), Title = item.title, subjectCode = item.subjectCode, CourseList = CoursesLinked });
            }

            return View(viewModel);
        }


        public JsonResult SubjectCourseList(string subjectID)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return Json(JsonRequestBehavior.AllowGet);

            var id = subjectID;
            long TID;
            bool res = long.TryParse(id, out TID);
            var BridgeList = db.tblCourseSubjects.Where(x => x.SubjectID == TID).ToList();

            if (BridgeList != null)
            {
                var results = new List<selectListItem>();
                foreach (var item in BridgeList)
                {
                    results.Add(new selectListItem { Value = Convert.ToInt32(item.TID), Text = item.tblCourse.title });
                }
                return Json(results, JsonRequestBehavior.AllowGet);
            }

            return Json(new { empty = "No Data Found under this department" });
        }
        
        public ActionResult Create()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            var courses = db.tblCourses.Include(x => x.tblFaculty).Where(x => x.stateid == 1)
    .OrderBy(x => x.tblFaculty.title).ThenBy(x => x.title);

            var viewModel = new SubjectViewModel()
            {
                CoursesList = new SelectList(courses, "tid", "title", "tblFaculty.title", null, null)
            };

            return View(viewModel);
        }


        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Create(SubjectViewModel viewModel, FormCollection form)
        {
            if (Session["uname"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            string sAlert = "";
            int intState = 1;
            string sName = Convert.ToString(form["val-title"]).Trim();
            string sSubjectCode = Convert.ToString(form["val-subject-code"]).Trim();


            var newSubject = new tblSubject();
            newSubject.title = sName;
            newSubject.subjectCode = sSubjectCode;
            newSubject.insertdate = DateTime.Now;
            newSubject.lastupdate = DateTime.Now;
            newSubject.createdby = Convert.ToInt32(Session["TID"]);
            newSubject.updatedby = Convert.ToInt32(Session["TID"]);


            intState = 1;
            sAlert = "You have successfully published " + sName;
            newSubject.stateid = intState;

            db.tblSubjects.Add(newSubject);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            var sCourses1 = form.GetValues("val-courses[]");

            foreach (var item in sCourses1)
            {
                var newSubjectCourseBrigde = new tblCourseSubject();

                newSubjectCourseBrigde.CourseID = Convert.ToInt64(item);
                newSubjectCourseBrigde.SubjectID = newSubject.tid;
                newSubjectCourseBrigde.insertdate = DateTime.Now;
                newSubjectCourseBrigde.lastupdate = DateTime.Now;
                newSubjectCourseBrigde.createdby = Convert.ToInt32(Session["TID"]);
                newSubjectCourseBrigde.updatedby = Convert.ToInt32(Session["TID"]);
                intState = 1;
                newSubjectCourseBrigde.stateid = intState;


                db.tblCourseSubjects.Add(newSubjectCourseBrigde);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }

            return RedirectToAction("/Index/");



        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = new SubjectViewModel();
            var tblSubject = await db.tblSubjects.FindAsync(id);

            try
            {
                viewModel.Subject = tblSubject;

                var courses = db.tblCourses.Include(x => x.tblFaculty).Where(x => x.stateid == 1)
        .OrderBy(x => x.tblFaculty.title).ThenBy(x => x.title);

                var BridgeList = db.tblCourseSubjects.Where(x => x.SubjectID == id).ToList();

                var list = new List<int>();
                if (BridgeList != null)
                {
                    foreach (var item in BridgeList)
                    {
                        list.Add(Convert.ToInt32(item.CourseID));
                    }
                }
                viewModel.courseids = list;
                viewModel.CoursesList = new SelectList(courses, "tid", "title", "tblFaculty.title", null, null);

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(viewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(SubjectViewModel viewModel, FormCollection form)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var intState = 1;

            if (!string.IsNullOrEmpty(Convert.ToString(form["val-title"])))
            {



                var mID = viewModel.Subject.tid.ToString();
                long marID;
                bool res2 = long.TryParse(mID, out marID);

                var Subject = db.tblSubjects.FirstOrDefault(x => x.tid == marID);

                if (Subject != null)
                {
                    var oldCourses = db.tblCourseSubjects.Where(x => x.SubjectID == Subject.tid).ToList();
                    foreach (var item in oldCourses)
                    {
                        db.tblCourseSubjects.Remove(item);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }
                    Subject.title = Convert.ToString(form["val-title"]);
                    Subject.subjectCode = Convert.ToString(form["val-subject-code"]);
                    Subject.lastupdate = DateTime.Now;
                    Subject.updatedby = Convert.ToInt32(Session["TID"]);
                    Subject.stateid = intState;

                    var sCourses1 = form.GetValues("val-courses[]");

                    foreach (var item in sCourses1)
                    {
                        var newSubjectCourseBrigde = new tblCourseSubject();

                        newSubjectCourseBrigde.CourseID = Convert.ToInt64(item);
                        newSubjectCourseBrigde.SubjectID = Subject.tid;
                        newSubjectCourseBrigde.insertdate = DateTime.Now;
                        newSubjectCourseBrigde.lastupdate = DateTime.Now;
                        newSubjectCourseBrigde.createdby = Convert.ToInt32(Session["TID"]);
                        newSubjectCourseBrigde.updatedby = Convert.ToInt32(Session["TID"]);
                        intState = 1;
                        newSubjectCourseBrigde.stateid = intState;


                        db.tblCourseSubjects.Add(newSubjectCourseBrigde);


                        try
                        {
                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

            var unitOfWorkPost = db.tblSubjects.Find(id);

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

            if (Faculties != null)
            {
                var results = new List<selectListItem>();
                foreach (var item in Faculties)
                {
                    results.Add(new selectListItem { Value = item.tid, Text = item.title });
                }
                return Json(results, JsonRequestBehavior.AllowGet);
            }

            return Json(new { empty = "No Data Found under this department" });
        }

    }

}
