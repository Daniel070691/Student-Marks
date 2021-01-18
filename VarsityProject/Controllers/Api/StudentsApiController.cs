using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VarsityProject.Models;
using VarsityProject.ViewModels;

namespace VarsityProject.Controllers.Api
{
    public class StudentsController : ApiController
    {
        VarsityDB db = new VarsityDB();

        [AcceptVerbs("GET")]
        public IHttpActionResult MyCourses(string studentID)
        {
            var stid = studentID;
            long studID;
            var res = long.TryParse(stid, out studID);
            var viewModel = new StudentViewModel();

            var courseBridge = db.tblStudentCourses.Where(x => x.studentID == studID).ToList();

            if (courseBridge == null)
                return BadRequest();

            var studentModel = new UserModel();
            
            var courseList = new List<StudentCourse>();
            foreach (var item in courseBridge)
            {
                var course = db.tblCourses.FirstOrDefault(x => x.tid == item.courseID && x.stateid == 1);
                if (course != null)
                {
                    var courseDetails = new StudentCourse();

                    courseDetails.courseTitle = course.title;
                    courseDetails.courseID = Convert.ToInt32(course.tid);
                    var subjectList = new List<StudentSubjectDTO>();

                    var courseSubjectBridgeList = db.tblCourseSubjects.Where(x => x.CourseID == course.tid).ToList();
                    if(courseSubjectBridgeList != null)
                    {
                        foreach (var courseSubjectBridge in courseSubjectBridgeList)
                        {

                            var subject = db.tblSubjects.FirstOrDefault(x => x.tid == courseSubjectBridge.SubjectID && x.stateid == 1);
                            if (subject != null)
                            {
                                var subjectDetails = new StudentSubjectDTO();

                                subjectDetails.subjectID = Convert.ToInt32(subject.tid);
                                subjectDetails.subjectTitle = subject.title;
                                
                                var activeTests = db.tblSubjectTests.Where(x => x.subjectid == subject.tid).ToList();
                                if (activeTests != null)
                                {
                                    var testMarks = new List<StudentTestDTO>();
                                    foreach (var test in activeTests)
                                    {
                                        var studentMarksBridge = db.tblStudentMarks.FirstOrDefault(x => x.StudentID == studID && x.subjectTestID == test.testid);
                                        if (studentMarksBridge != null)
                                        {
                                            var testInfo = db.tblTests.FirstOrDefault(x => x.TID == test.testid);
                                            var rTID = testInfo.TID.ToString();
                                            int resultTID;
                                            bool res4 = int.TryParse(rTID, out resultTID);

                                            testMarks.Add(new StudentTestDTO { testID = Convert.ToInt32(testInfo.TID), testTitle = testInfo.title, testMark = studentMarksBridge.Mark });
                                            
                                        }

                                    }

                                    subjectDetails.SubjectTests = testMarks;
                                }

                                subjectList.Add(new StudentSubjectDTO { subjectID = subjectDetails.subjectID, subjectTitle = subjectDetails.subjectTitle, SubjectTests = subjectDetails.SubjectTests });
                            }
                        }
                    }
                    courseList.Add(new StudentCourse { courseID = Convert.ToInt32(course.tid), courseTitle = course.title, CourseSubjectList = subjectList });
                }
            }
            

            viewModel.StudentCourseList = courseList;

            return Ok(viewModel);
        }



        [AcceptVerbs("GET")]
        public IHttpActionResult MySubject(string courseID)
        {
            var ctid = courseID;
            long coursID;
            var res = long.TryParse(ctid, out coursID);
            var viewModel = new StudentViewModel();

            var subjectBridge = db.tblCourseSubjects.Where(x => x.CourseID == coursID).ToList();

            if (subjectBridge == null)
                return BadRequest();

            var subjectList = new List<StudentSubjectDTO>();
            foreach (var item in subjectBridge)
            {
                var subject = db.tblSubjects.FirstOrDefault(x => x.tid == item.SubjectID && x.stateid == 1);
                if (subject != null)
                {
                    subjectList.Add(new StudentSubjectDTO { subjectID = Convert.ToInt32(subject.tid), subjectTitle = subject.title });
                }
            }
            viewModel.StudentSubjectList = subjectList;

            return Ok(viewModel);
        }


        [AcceptVerbs("GET")]
        public IHttpActionResult MyTest(string subjectID, string studentID)
        {
            var subtid = subjectID;
            long sbjID;
            var res = long.TryParse(subtid, out sbjID);


            var stuID = studentID;
            long stdntID;
            var res2 = long.TryParse(stuID, out stdntID);

            var viewModel = new StudentViewModel();

            var subject = db.tblSubjects.FirstOrDefault(x => x.tid == sbjID && x.stateid == 1);

            var activeTests = db.tblSubjectTests.Where(x => x.subjectid == subject.tid).ToList();
            if (activeTests != null)
            {
                var testMarks = new List<StudentTestDTO>();
                foreach (var item in activeTests)
                {
                    var studentMarksBridge = db.tblStudentMarks.FirstOrDefault(x => x.StudentID == stdntID && x.subjectTestID == item.testid);
                    if (studentMarksBridge != null)
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

            return Ok(viewModel);
        }
    }
}
