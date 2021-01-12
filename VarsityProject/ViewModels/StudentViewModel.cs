using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            this.Student = new tblStudent();
        }
        public tblStudent Student { get; set; }
        public List<Student> StudentList { get; set; }
        public string error { get; set; }
        public IEnumerable<SelectListItem> statusFilter { get; set; }
        public string courseid { get; set; }
        public List<StudentCourse> StudentCourseList { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public selectedValues SelectedValues { get; set; }
        public List<int> courseids { get; set; }
    }

    public class Student
    {
        public int TID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
        public string Email { get; set; }
        public string Cell { get; set; }
        public int status { get; set; }
        public List<StudentCourse> StudentCourseList { get; set; }

    }
    public class StudentCourse
    {
        public int TID { get; set; }
        public string Title { get; set; }
    }

}