using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class AdminViewModels
    {
        public tblLecturer Lecturer { get; set; }
        public List<tblLecturer> LecturerList { get; set; }


        public tblStudent Student { get; set; }
        public List<tblLecturer> StudentList { get; set; }


        public tblAdministrator Administrator { get; set; }
        public List<tblAdministrator> AdministratorList { get; set; }

        public IEnumerable<SelectListItem> StatusFilters { get; set; }
    }
}