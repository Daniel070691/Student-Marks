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
            

            var students = db.tblStudents.Where(x => x.stateid == 1).ToList();

            viewModel.StudentList = new List<Student>();
            foreach (var item in students)
            {
                var coursesBrigde = db.tblStudentCourses.Where(x => x.studentID == item.TID).ToList();

                var CoursesLinked = new List<StudentCourse>();
                foreach (var item2 in coursesBrigde)
                {
                    var course = db.tblCourses.Where(x => x.tid == item2.courseID).FirstOrDefault();
                    CoursesLinked.Add(new StudentCourse { courseID = Convert.ToInt32(course.tid), courseTitle = course.title });
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

        public ActionResult MyCourses()
        {

            if (Session["names"] == null && (string)Session["role"] != "Student")
                return RedirectToAction("StudentLogIn", "Admin");

            var studID = Session["TID"].ToString();
            long TID;
            bool res2 = long.TryParse(studID, out TID);
            var viewModel = new StudentViewModel();
            var courseBridge = db.tblStudentCourses.Where(x => x.studentID == TID).ToList();

            var courseList = new List<StudentCourse>();
            foreach (var item in courseBridge)
            {
                var course = db.tblCourses.FirstOrDefault(x => x.tid == item.courseID && x.stateid == 1);
                if(course != null)
                {
                    courseList.Add(new StudentCourse { courseID = Convert.ToInt32(course.tid), courseTitle = course.title });
                }
            }
            viewModel.StudentCourseList = courseList;

            return View(viewModel);
        }

        public ActionResult CourseDetails(int? id)
        {

            if (Session["names"] == null && (string)Session["role"] != "Student")
                return RedirectToAction("StudentLogIn", "Admin");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var studID = Session["TID"].ToString();
            long TID;
            bool res2 = long.TryParse(studID, out TID);

            var cID = id.ToString();
            long courseID;
            bool res = long.TryParse(cID, out courseID);

            if (Session["subjectID"] != null)
            {
                Session.Remove("subjectID");
                Session["subjectID"] = Convert.ToString(id);
            }
            else
            {
                Session["subjectID"] = Convert.ToString(id);
            }

            if (Session["courseID"] != null)
            {
                Session.Remove("courseID");
                Session["courseID"] = Convert.ToString(courseID);
            }
            else
            {
                Session["courseID"] = Convert.ToString(courseID);
            }
            var viewModel = new StudentViewModel();
            var courseBridge = db.tblCourseSubjects.Where(x => x.CourseID == courseID).ToList();
            var course = db.tblCourses.FirstOrDefault(x => x.tid == courseID);

            if(course == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 

            var selectedCourse = new StudentCourse();

            selectedCourse.courseID = Convert.ToInt32(course.tid);
            selectedCourse.courseTitle = course.title;

            var subjectList = new List<StudentSubjectDTO>();
            foreach (var item in courseBridge)
            {
                var subject = db.tblSubjects.FirstOrDefault(x => x.tid == item.SubjectID && x.stateid == 1);
                
                if (subject != null)
                {
                    subjectList.Add(new StudentSubjectDTO { subjectID = Convert.ToInt32(subject.tid), subjectTitle = subject.title });
                }
            }
            viewModel.StudentSubjectList = subjectList;
            viewModel.selectedCourse = selectedCourse;
            return View(viewModel);
        }

        public ActionResult SubjectTest(int? id)
        {
            if (Session["names"] == null && (string)Session["role"] != "Student")
                return RedirectToAction("StudentLogIn", "Admin");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            var studID = Session["TID"].ToString();
            long TID;
            bool res2 = long.TryParse(studID, out TID);

            var sID = id.ToString();
            long subID;
            bool res = long.TryParse(sID, out subID);

            var subject = db.tblSubjects.FirstOrDefault(x => x.tid == subID && x.stateid == 1);

            if (subject == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var selectedSubject = new StudentSubjectDTO();

            selectedSubject.subjectID = Convert.ToInt32(subject.tid);
            selectedSubject.subjectTitle = subject.title;
            



            var cID = Session["courseID"].ToString();
            long courseID;
            bool res3 = long.TryParse(cID, out courseID);

            var course = db.tblCourses.FirstOrDefault(x => x.tid == courseID);

            if (course == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            var viewModel = new StudentViewModel();

            var selectedCourse = new StudentCourse();

            selectedCourse.courseID = Convert.ToInt32(course.tid);
            selectedCourse.courseTitle = course.title;

            var activeTests = db.tblSubjectTests.Where(x => x.subjectid == selectedSubject.subjectID).ToList();
            if(activeTests != null)
            {
                var testMarks = new List<StudentTestDTO>();
                foreach (var item in activeTests)
                {
                    var studentMarksBridge = db.tblStudentMarks.FirstOrDefault(x => x.StudentID == TID && x.subjectTestID == item.testid);
                    if(studentMarksBridge != null)
                    {
                        var testInfo = db.tblTests.FirstOrDefault(x => x.TID == item.testid);
                        var rTID = testInfo.TID.ToString();
                        int resultTID;
                        bool res4 = int.TryParse(rTID, out resultTID);
                        testMarks.Add(new StudentTestDTO { testID = resultTID, testTitle = testInfo.title, testMark = studentMarksBridge.Mark });
                    }

                }

                viewModel.TestList = testMarks;
            }



            viewModel.StudentSubject = selectedSubject;
            viewModel.selectedCourse = selectedCourse;
            return View(viewModel);
        }
    }
}