using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class SubjectViewModel
    {
        public SubjectViewModel()
        {
            this.Subject = new tblSubject();
        }
        public tblSubject Subject { get; set; }
        public List<Subject> SubjectList { get; set; }
        public string error { get; set; }
        public IEnumerable<SelectListItem> statusFilter { get; set; }
        public string coursesid { get; set; }
        public List<Course> SubjectCourseList { get; set; }
        public IEnumerable<SelectListItem> CoursesList { get; set; }
        public selectedValues SelectedValues { get; set; }
        public List<int> courseids { get; set; }
    }

    public class Subject
    {
        public int TID { get; set; }
        public string Title { get; set; }
        public string subjectCode { get; set; }
        public List<Course> CourseList { get; set; }

    }
    public class Course
    {
        public int TID { get; set; }
        public string Title { get; set; }
    }


    public class selectListItem
    {
        public int Value { get; set; }
        public string Text { get; set; }

    }

    public class selectedValues
    {
        public int[] Value { get; set; }
    }

}