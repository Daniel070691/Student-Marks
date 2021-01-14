using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class LectureViewModel
    {
        public LectureViewModel()
        {
            this.Lecture = new tblLecturer();
            this.testDetail = new TestDetails();
        }
        public tblLecturer Lecture { get; set; }
        public List<Lecturer> LecturerList { get; set; }
        public string error { get; set; }
        public IEnumerable<SelectListItem> statusFilter { get; set; }
        public string subjectid { get; set; }
        public List<LectureSubject> LectureSubjectList { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }
        public selectedValues SelectedValues { get; set; }
        public List<int> subjectids { get; set; }
        public List<tblTest> LectureTestList { get; set; }
        public tblSubject SubjectSelected { get; set; }
        public TestDetails testDetail { get; set; }
        public List<SubjectStudent> SubjectStudentList { get; set; }
    }

    public class Lecturer
    {
        public int TID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
        public List<LectureSubject> LectureSubjectList { get; set; }

    }
    public class LectureSubject
    {
        public int TID { get; set; }
        public string Title { get; set; }
    }

    public class SubjectStudent
    {
        public int TID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNo { get; set; }
        public string testMark { get; set; }
    }

    public class TestDetails
    {
        public tblTest Test { get; set; }
        public List<SubjectStudent> StudentList { get; set; }
    }

}