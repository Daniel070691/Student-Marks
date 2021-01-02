using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;

namespace VarsityProject.ViewModels
{
    public class DepartmentsViewModel
    {
        public tblDepartment Department { get; set; }
        public List<tblDepartment> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> StatusFilters { get; set; }
    }
}