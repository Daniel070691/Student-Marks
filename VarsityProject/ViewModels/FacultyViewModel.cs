using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class FacultyViewModel
    {
        public FacultyViewModel()
        {
            this.Faculty = new tblFaculty();
        }
        public tblFaculty Faculty { get; set; }
        public List<tblFaculty> FacultyList { get; set; }
        public string error { get; set; }
        public IEnumerable<SelectListItem> statusFilter { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
    }
}