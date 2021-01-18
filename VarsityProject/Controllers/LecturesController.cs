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
    public class LecturesController : Controller
    {
        VarsityDB db = new VarsityDB();

        // GET: Department
        public async Task<ActionResult> Index()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var subjects = db.tblSubjects.Include(x => x.tblCourse).Where(x => x.stateid == 1)
    .OrderBy(x => x.tblCourse.title).ThenBy(x => x.title);

            var viewModel = new LectureViewModel();
            //{
            //    Courses = new SelectList(courses, "tid", "title", "tblFaculty.title", null, null)
            //};
            
            var lecturers = db.tblLecturers.Where(x => x.stateid == 1).ToList();

            viewModel.LecturerList = new List<Lecturer>();
            foreach (var item in lecturers)
            {
                var subjectBrigde = db.tblLectureSubjects.Where(x => x.LectureID == item.TID && x.stateid == 1).ToList();

                var SubjectsLinked = new List<LectureSubject>();
                foreach (var item2 in subjectBrigde)
                {
                    var subject = db.tblSubjects.Where(x => x.tid == item2.SubjectID).FirstOrDefault();
                    SubjectsLinked.Add(new LectureSubject { TID = Convert.ToInt32(subject.tid), Title = subject.title });
                }

                viewModel.LecturerList.Add(new Lecturer { TID = Convert.ToInt32(item.TID), Name = item.Name, Surname = item.Surname, Email = item.Email, EmployeeNumber = item.EmployeeNumber, LectureSubjectList = SubjectsLinked });
            }

            return View(viewModel);
        }

        //Create: Fundraising
        public ActionResult Create()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var subjects = db.tblSubjects.Include(x => x.tblCourse).Where(x => x.stateid == 1)
    .OrderBy(x => x.tblCourse.title).ThenBy(x => x.title);

            var viewModel = new LectureViewModel()
            {
                SubjectList = new SelectList(subjects, "tid", "title", "tblCourse.title", null, null)
            };

            return View(viewModel);
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Create(LectureViewModel viewModel, FormCollection form)
        {
            if (Session["uname"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            string sAlert = "";
            int intState = 1;
            string sName = Convert.ToString(form["val-Name"]).Trim();
            string sSurname = Convert.ToString(form["val-Surname"]).Trim();
            string sEmail = Convert.ToString(form["val-email"]).Trim();
            string sEmployeeNo = Convert.ToString(form["val-employee-number"]).Trim();
            string sPassword = Convert.ToString(form["val-password"]).Trim();
            int sGender = Convert.ToInt32(form["val-gender"]);


            var newEmployee = new tblLecturer();
            newEmployee.Name = sName;
            newEmployee.Surname = sSurname;
            newEmployee.EmployeeNumber = sEmployeeNo;
            newEmployee.Email = sEmail;
            newEmployee.Password = sPassword;
            newEmployee.insertdate = DateTime.Now;
            newEmployee.lastupdate = DateTime.Now;
            newEmployee.Gender = sGender;
            newEmployee.createdby = Convert.ToInt32(Session["TID"]);
            newEmployee.updatedby = Convert.ToInt32(Session["TID"]);


            intState = 1;
            sAlert = "You have successfully published " + sName;
            newEmployee.stateid = intState;

            db.tblLecturers.Add(newEmployee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            var sSubjects = form.GetValues("val-subject[]");

            foreach (var item in sSubjects)
            {
                var newLectureSubjectBrigde = new tblLectureSubject();

                newLectureSubjectBrigde.SubjectID = Convert.ToInt64(item);
                newLectureSubjectBrigde.LectureID = newEmployee.TID;
                newLectureSubjectBrigde.insertdate = DateTime.Now;
                newLectureSubjectBrigde.lastupdate = DateTime.Now;
                newLectureSubjectBrigde.createdby = Convert.ToInt32(Session["TID"]);
                newLectureSubjectBrigde.updatedby = Convert.ToInt32(Session["TID"]);
                intState = 1;
                newLectureSubjectBrigde.stateid = intState;


                db.tblLectureSubjects.Add(newLectureSubjectBrigde);

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

            var viewModel = new LectureViewModel();
            var tblLecture = await db.tblLecturers.FindAsync(id);

            try
            {
                viewModel.Lecture = tblLecture;

                var subjects = db.tblSubjects.Include(x => x.tblCourse).Where(x => x.stateid == 1)
        .OrderBy(x => x.tblCourse.title).ThenBy(x => x.title);

                var BridgeList = db.tblLectureSubjects.Where(x => x.LectureID == id).ToList();

                var list = new List<int>();
                if (BridgeList != null)
                {
                    foreach (var item in BridgeList)
                    {
                        list.Add(Convert.ToInt32(item.SubjectID));
                    }
                }
                viewModel.subjectids = list;
                viewModel.SubjectList = new SelectList(subjects, "tid", "title", "tblCourse.title", null, null);

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(LectureViewModel viewModel, FormCollection form)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var intState = 1;

            if (!string.IsNullOrEmpty(Convert.ToString(form["val-email"])))
            {



                var mID = viewModel.Lecture.TID.ToString();
                long marID;
                bool res2 = long.TryParse(mID, out marID);

                var Lecture = db.tblLecturers.FirstOrDefault(x => x.TID == marID);

                if (Lecture != null)
                {
                    var oldSubjects = db.tblLectureSubjects.Where(x => x.LectureID == Lecture.TID).ToList();
                    foreach (var item in oldSubjects)
                    {
                        db.tblLectureSubjects.Remove(item);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }
                    Lecture.Name = Convert.ToString(form["val-Name"]);
                    Lecture.Surname = Convert.ToString(form["val-Surname"]);
                    Lecture.Email = Convert.ToString(form["val-Email"]);
                    Lecture.Gender = Convert.ToInt32(form["val-gender"]);
                    Lecture.EmployeeNumber = Convert.ToString(form["val-employee-number"]);
                    Lecture.Password = Convert.ToString(form["val-password"]);
                    Lecture.lastupdate = DateTime.Now;
                    Lecture.updatedby = Convert.ToInt32(Session["TID"]);
                    Lecture.stateid = intState;

                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        throw;
                    }
                    var sSubjects = form.GetValues("val-subject[]");

                    foreach (var item in sSubjects)
                    {
                        var newLectureSubjectBrigde = new tblLectureSubject();

                        newLectureSubjectBrigde.SubjectID = Convert.ToInt64(item);
                        newLectureSubjectBrigde.LectureID = Lecture.TID;
                        newLectureSubjectBrigde.insertdate = DateTime.Now;
                        newLectureSubjectBrigde.lastupdate = DateTime.Now;
                        newLectureSubjectBrigde.createdby = Convert.ToInt32(Session["TID"]);
                        newLectureSubjectBrigde.updatedby = Convert.ToInt32(Session["TID"]);
                        intState = 1;
                        newLectureSubjectBrigde.stateid = intState;


                        db.tblLectureSubjects.Add(newLectureSubjectBrigde);


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

            var unitOfWorkPost = db.tblLecturers.Find(id);

            if (unitOfWorkPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            string sAlert = String.Empty;

            unitOfWorkPost.stateid = 4;

            sAlert = "You have successfully deleted " + unitOfWorkPost.Name;

            await db.SaveChangesAsync();

            return RedirectToAction("/Index/");

        }

        public JsonResult LecturerSubjectList(string lecturerID)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return Json(JsonRequestBehavior.AllowGet);

            var id = lecturerID;
            long TID;
            bool res = long.TryParse(id, out TID);
            var BridgeList = db.tblLectureSubjects.Where(x => x.LectureID == TID).ToList();

            if (BridgeList != null)
            {
                var results = new List<selectListItem>();
                foreach (var item in BridgeList)
                {
                    results.Add(new selectListItem { Value = Convert.ToInt32(item.TID), Text = item.tblSubject.title });
                }
                return Json(results, JsonRequestBehavior.AllowGet);
            }

            return Json(new { empty = "No Data Found under this Course" });
        }

        //Lecture section
        public ActionResult LectureSubjectIndex()
        {

            if (Session["names"] == null && (string)Session["role"] != "Lecture")
                return RedirectToAction("LectureLogin", "Admin");

            var mID = Session["TID"].ToString();
            long marID;
            bool res2 = long.TryParse(mID, out marID);

            var BridgeList = db.tblLectureSubjects.Where(x => x.LectureID == marID && x.stateid == 1).ToList();
            var SubjectsLinked = new List<LectureSubject>();

            foreach (var item in BridgeList)
            {
                var subject = db.tblSubjects.Where(x => x.tid == item.SubjectID).FirstOrDefault();

                SubjectsLinked.Add(new LectureSubject { TID = Convert.ToInt32(subject.tid), Title = subject.title });
            }

            var viewModel = new LectureViewModel();
            viewModel.LectureSubjectList = SubjectsLinked;
            return View(viewModel);
        }

        public ActionResult SubjectManagement(int? id)
        {

            if (Session["names"] == null && (string)Session["role"] != "Lecture")
                return RedirectToAction("LectureLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mID = Session["TID"].ToString();
            long marID;
            bool res2 = long.TryParse(mID, out marID);
            var viewModel = new LectureViewModel();
            viewModel.Lecture.TID = Convert.ToInt32(marID);

            viewModel.SubjectSelected = db.tblSubjects.FirstOrDefault(x => x.tid == id);
            if (Session["subjectID"] != null)
            {
                Session.Remove("subjectID");
                Session["subjectID"] = Convert.ToString(id);
            }
            else
            {
                Session["subjectID"] = Convert.ToString(id);
            }


            var sID = Session["subjectID"].ToString();
            long subID;
            bool res = long.TryParse(sID, out subID);
            var Bridge = db.tblLectureSubjects.FirstOrDefault(x => x.LectureID == marID && x.SubjectID == id && x.stateid == 1);
            if (Bridge != null)
            {
                var subjectTestBridge = db.tblSubjectTests.Where(x => x.subjectid == Bridge.SubjectID).ToList();
                var testList = new List<tblTest>();
                foreach (var item in subjectTestBridge)
                {
                    var test = db.tblTests.FirstOrDefault(x => x.TID == item.testid && x.stateid == 1);
                    testList.Add(test);
                }
                viewModel.LectureTestList = testList;
                var studentsList = db.tblSubjects.Where(x => x.stateid == 1).ToList();
                var subjectCoursesList = db.tblCourseSubjects.Where(x => x.SubjectID == id).ToList();
                var studentList = new List<SubjectStudent>();
                foreach (var item in subjectCoursesList)
                {
                    var studentCoursesList = db.tblStudentCourses.Where(x => x.courseID == item.CourseID).ToList();
                    foreach (var student in studentCoursesList)
                    {
                        var subjectStudent = db.tblStudents.FirstOrDefault(x => x.TID == student.studentID);

                        studentList.Add(new SubjectStudent { TID = Convert.ToInt32(subjectStudent.TID), Name = subjectStudent.Name, Surname = subjectStudent.Surname, StudentNo = subjectStudent.studentNo });
                    }

                }

                viewModel.SubjectSelected = db.tblSubjects.FirstOrDefault(x => x.tid == subID);
                viewModel.SubjectStudentList = studentList.GroupBy(x => x.TID).Select(grp => grp.First()).ToList();

            }
            return View(viewModel);
        }

        public ActionResult createTest(int? id)
        {

            if (Session["names"] == null && (string)Session["role"] != "Lecture")
                return RedirectToAction("LectureLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = new LectureViewModel();
            viewModel.SubjectSelected = db.tblSubjects.FirstOrDefault(x => x.tid == id);
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult createTest(LectureViewModel model, FormCollection form, int? id)
        {
            if (Session["names"] == null && (string)Session["role"] != "Lecture")
                return RedirectToAction("LectureLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mID = model.SubjectSelected.tid.ToString();
            long marID;
            bool res2 = long.TryParse(mID, out marID);

            var sTitle = Convert.ToString(form["val-title"]);
            var sDate = Convert.ToDateTime(form["val-default-date"]);

            var newTest = new tblTest();
            newTest.title = sTitle;
            newTest.scheduleDate = sDate;
            newTest.insertdate = DateTime.Now;
            newTest.lastupdate = DateTime.Now;
            newTest.createdby = Convert.ToInt32(Session["TID"]);
            newTest.stateid = 1;

            db.tblTests.Add(newTest);
            db.SaveChanges();

            var newSubjectTestBridge = new tblSubjectTest();

            newSubjectTestBridge.testid = newTest.TID;
            newSubjectTestBridge.subjectid = marID;
            newSubjectTestBridge.insertdate = DateTime.Now;
            newSubjectTestBridge.lastupdate = DateTime.Now;
            newSubjectTestBridge.stateid = 1;

            db.tblSubjectTests.Add(newSubjectTestBridge);
            db.SaveChanges();

            return RedirectToAction("SubjectManagement", "Lectures", new { id = model.SubjectSelected.tid });
        }

        public ActionResult TestDetails(int? id)
        {
            if (Session["names"] == null && (string)Session["role"] != "Lecture")
                return RedirectToAction("LectureLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (Session["subjectID"] == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            var tID = id.ToString();
            long testID;
            bool res = long.TryParse(tID, out testID);

            var sID = Session["subjectID"].ToString();
            long subID;
            bool res2 = long.TryParse(sID, out subID);

            var viewModel = new LectureViewModel();
            var Bridge = db.tblLectureSubjects.FirstOrDefault(x => x.SubjectID == subID && x.stateid == 1);
            if (Bridge != null)
            {
                var subjectTestBridge = db.tblSubjectTests.Where(x => x.subjectid == Bridge.SubjectID).ToList();
                var testList = new List<tblTest>();

                var test = db.tblTests.FirstOrDefault(x => x.TID == testID);

                viewModel.testDetail.Test = test;
                var subjectCoursesList = db.tblCourseSubjects.Where(x => x.SubjectID == id).ToList();
                var studentList = new List<SubjectStudent>();
                foreach (var item in subjectCoursesList)
                {
                    var studentCoursesList = db.tblStudentCourses.Where(x => x.courseID == item.CourseID).ToList();
                    foreach (var student in studentCoursesList)
                    {
                        var subjectStudent = db.tblStudents.FirstOrDefault(x => x.TID == student.studentID);
                        var testMarkBridge = db.tblStudentMarks.FirstOrDefault(x => x.StudentID == subjectStudent.TID && x.subjectTestID == test.TID);
                        
                        studentList.Add(new SubjectStudent { TID = Convert.ToInt32(subjectStudent.TID), Name = subjectStudent.Name, Surname = subjectStudent.Surname, StudentNo = subjectStudent.studentNo, testMark = testMarkBridge != null ? Convert.ToString(testMarkBridge.Mark) : ""  });
                    }

                }


                viewModel.SubjectStudentList = studentList.GroupBy(x => x.TID).Select(grp => grp.First()).ToList();
            }
            var subjectSelected = db.tblSubjects.FirstOrDefault(x => x.tid == subID);
            viewModel.SubjectSelected = subjectSelected;

            //string[,] array = new string[,](); 

            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> testDetails(FormCollection form, int? id)
        {
            if (Session["names"] == null && (string)Session["role"] != "Lecture")
                return RedirectToAction("LectureLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mID = id.ToString();
            long marID;
            bool res2 = long.TryParse(mID, out marID);

            var sTitle = Convert.ToString(form["val-title"]);
            var sDate = Convert.ToDateTime(form["xp-default-date"]);

            var Test = db.tblTests.FirstOrDefault(x => x.TID == marID);

            if (Test == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Test.title = sTitle;
            Test.scheduleDate = sDate;
            Test.lastupdate = DateTime.Now;
            Test.createdby = Convert.ToInt32(Session["TID"]);
            Test.stateid = 1;
            await db.SaveChangesAsync();

            return RedirectToAction("TestDetails", "Lectures", new { id = Test.TID });
        }

        [HttpPost]
        public async Task<ActionResult> SaveStudentMark(string studentID, string testID, string testMark)
        {

            var sID = studentID;
            long stuID;
            bool res = long.TryParse(sID, out stuID);


            var tID = testID;
            long tesID;
            bool res2 = long.TryParse(tID, out tesID);


            var mID = testMark;
            int mark;
            bool res3 = int.TryParse(mID, out mark);

            var MarkBridgeExist = db.tblStudentMarks.FirstOrDefault(x => x.StudentID == stuID && x.subjectTestID == tesID);
            if(MarkBridgeExist == null)
            { 
            var studentTestMark = new tblStudentMark();

            studentTestMark.subjectTestID = tesID;
            studentTestMark.StudentID = stuID;
            studentTestMark.Mark = mark;

            db.tblStudentMarks.Add(studentTestMark);
            }
            else
            {
                MarkBridgeExist.Mark = mark;
            }

            await db.SaveChangesAsync();

            var result = "saved";

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
