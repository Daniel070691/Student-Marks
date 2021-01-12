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
    public class StudentsController : Controller
    {
        VarsityDB db = new VarsityDB();
        // GET: Department
        public ActionResult Index()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var courses = db.tblCourses.Include(x => x.tblFaculty).Where(x => x.stateid == 1)
    .OrderBy(x => x.tblFaculty.title).ThenBy(x => x.title);

            var viewModel = new StudentViewModel();
            //{
            //    Courses = new SelectList(courses, "tid", "title", "tblFaculty.title", null, null)
            //};

            var students = from s in db.tblStudents
                           where s.stateid == 1
                           select s;

            viewModel.StudentList = new List<Student>();
            foreach (var item in students)
            {
                var coursesBrigde = db.tblStudentCourses.Where(x => x.studentID == item.TID).ToList();

                var CoursesLinked = new List<StudentCourse>();
                foreach (var item2 in coursesBrigde)
                {
                    var course = db.tblCourses.Where(x => x.tid == item2.courseID).FirstOrDefault();
                    CoursesLinked.Add(new StudentCourse { TID = Convert.ToInt32(course.tid), Title = course.title });
                }

                viewModel.StudentList.Add(new Student { TID = Convert.ToInt32(item.TID), StudentNumber = item.studentNo, Name = item.Name, Surname = item.Surname, Email = item.email,  StudentCourseList = CoursesLinked, status = item.stateid });
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            var courses = db.tblCourses.Include(x => x.tblFaculty).Where(x => x.stateid == 1)
    .OrderBy(x => x.tblFaculty.title).ThenBy(x => x.title);

            var viewModel = new StudentViewModel()
            {
                CourseList = new SelectList(courses, "tid", "title", "tblFaculty.title", null, null)
            };

            return View(viewModel);
        }
        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Create(StudentViewModel viewModel, FormCollection form)
        {
            if (Session["uname"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            string sAlert = "";
            int intState = 1;
            string sName = Convert.ToString(form["val-Name"]).Trim();
            string sSurname = Convert.ToString(form["val-Surname"]).Trim();
            string sStudentNo = Convert.ToString(form["val-student-number"]).Trim();
            string sCellphone = Convert.ToString(form["val-cellphone"]).Trim();
            string sEmail = Convert.ToString(form["val-email"]).Trim();
            string sPassword = Convert.ToString(form["val-password"]).Trim();
            int sGender = Convert.ToInt32(form["val-gender"]);


            var newStudent = new tblStudent();
            newStudent.Name = sName;
            newStudent.Surname = sSurname;
            newStudent.studentNo = sStudentNo;
            newStudent.cellphone = sCellphone;
            newStudent.email = sEmail;
            newStudent.gender = sGender;
            newStudent.password = sPassword;
            newStudent.insertdate = DateTime.Now;
            newStudent.lastupdate = DateTime.Now;
            newStudent.createdby = Convert.ToInt32(Session["TID"]);
            newStudent.updatedby = Convert.ToInt32(Session["TID"]);


            intState = 1;
            sAlert = "You have successfully published " + sName;
            newStudent.stateid = intState;

            db.tblStudents.Add(newStudent);

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
                var newStudentCourseBrigde = new tblStudentCourse();

                newStudentCourseBrigde.courseID = Convert.ToInt64(item);
                newStudentCourseBrigde.studentID = newStudent.TID;


                db.tblStudentCourses.Add(newStudentCourseBrigde);

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

            var viewModel = new StudentViewModel();
            var tblStudent = await db.tblStudents.FindAsync(id);

            try
            {
                viewModel.Student = tblStudent;

                var courses = db.tblCourses.Include(x => x.tblFaculty).Where(x => x.stateid == 1)
       .OrderBy(x => x.tblFaculty.title).ThenBy(x => x.title);

                var BridgeList = db.tblStudentCourses.Where(x => x.studentID == id).ToList();

                var list = new List<int>();
                if (BridgeList != null)
                {
                    foreach (var item in BridgeList)
                    {
                        list.Add(Convert.ToInt32(item.courseID));
                    }
                }
                viewModel.courseids = list;
                viewModel.CourseList = new SelectList(courses, "tid", "title", "tblFaculty.title", null, null);

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(viewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(StudentViewModel viewModel, FormCollection form)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var intState = 1;

            if (!string.IsNullOrEmpty(Convert.ToString(form["val-email"])))
            {



                var mID = viewModel.Student.TID.ToString();
                long marID;
                bool res2 = long.TryParse(mID, out marID);

                var Student = db.tblStudents.FirstOrDefault(x => x.TID == marID);

                if (Student != null)
                {
                    var oldCourses = db.tblStudentCourses.Where(x => x.studentID == Student.TID).ToList();
                    foreach (var item in oldCourses)
                    {
                        db.tblStudentCourses.Remove(item);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }
                    Student.Name = Convert.ToString(form["val-Name"]);
                    Student.Surname = Convert.ToString(form["val-Surname"]);
                    Student.email = Convert.ToString(form["val-email"]);
                    Student.gender = Convert.ToInt32(form["val-gender"]);
                    Student.cellphone = Convert.ToString(form["val-cellphone"]).Trim();
                    Student.studentNo = Convert.ToString(form["val-student-number"]);
                    Student.password = Convert.ToString(form["val-password"]);
                    Student.lastupdate = DateTime.Now;
                    Student.updatedby = Convert.ToInt32(Session["TID"]);
                    Student.stateid = intState;

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        throw;
                    }
                    var sCourses = form.GetValues("val-courses[]");

                    foreach (var item in sCourses)
                    {
                        var newStudentCourseBrigde = new tblStudentCourse();

                        newStudentCourseBrigde.courseID = Convert.ToInt64(item);
                        newStudentCourseBrigde.studentID = Student.TID;


                        db.tblStudentCourses.Add(newStudentCourseBrigde);


                        try
                        {
                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }

                    return RedirectToAction("/Index/");

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

            var unitOfWorkPost = db.tblStudents.Find(id);

            if (unitOfWorkPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            string sAlert = String.Empty;

            unitOfWorkPost.stateid = 4;

            sAlert = "You have successfully deleted " + unitOfWorkPost.Name;

            await db.SaveChangesAsync();

            return RedirectToAction("/Index/");

        }
    }
}