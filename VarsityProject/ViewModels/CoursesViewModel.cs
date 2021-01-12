using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class CoursesViewModel
    {

        public CoursesViewModel()
        {
            this.Course = new tblCourse();
        }

        public tblCourse Course { get; set; }
        public List<tblCourse> CourseList { get; set; }
        public string error { get; set; }
        public IEnumerable<SelectListItem> statusFilter { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> FacultyList { get; set; }
    }
}