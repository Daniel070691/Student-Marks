using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;
using VarsityProject.ViewModels;

namespace VarsityProject.Controllers.Web
{
    public class DepartmentController : Controller
    {
        VarsityDB db = new VarsityDB();
        // GET: Department
        public async Task<ActionResult> Index()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var viewModel = new DepartmentsViewModel
            {
                StatusFilters = (from s in db.tblStates
                                 select new SelectListItem
                                 {
                                     Value = s.TID.ToString(),
                                     Text = s.Heading
                                 })
            };
            var departments = from s in db.tblDepartments
                              where s.stateid == 1
                              select s;
            viewModel.DepartmentList = await departments.ToListAsync();

            return View(viewModel);
        }

        //Create: Fundraising
        public ActionResult Create()
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Create(DepartmentsViewModel viewModel, FormCollection form)
        {
            if (Session["uname"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            string sAlert = "";
            int intState = 1;
            string sName = Convert.ToString(form["val-title"]).Trim();
            string sDeptCode = Convert.ToString(form["val-deptCode"]).Trim();

            //int iPageID = Convert.ToInt32(form["pagecat"]);

            var newDepartment = new tblDepartment();
            newDepartment.Title = sName;
            newDepartment.departmentCode = sDeptCode;
            newDepartment.insertdate = DateTime.Now;
            newDepartment.lastupdate = DateTime.Now;
            newDepartment.insertedby = Convert.ToInt32(Session["TID"]);
            newDepartment.updatedby = Convert.ToInt32(Session["TID"]);



            intState = 1;
            sAlert = "You have successfully published " + sName;
            newDepartment.stateid = intState;

            db.tblDepartments.Add(newDepartment);


            await db.SaveChangesAsync();


            return RedirectToAction("/Index/");



        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = new DepartmentsViewModel();
            var tblDepartment = await db.tblDepartments.FindAsync(id);

            try
            {

                viewModel.Department.Title = tblDepartment.Title;
                viewModel.Department.departmentCode = tblDepartment.departmentCode;
                viewModel.Department.tid = tblDepartment.tid;
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(viewModel);
        }


        [HttpPost, ValidateInput(false)]
        public async Task<ActionResult> Edit(DepartmentsViewModel viewModel, FormCollection form)
        {
            if (Session["names"] == null && (string)Session["role"] != "Administrator")
                return RedirectToAction("AdministratorLogin", "Admin");

            var intState = 1;
            string sAlert = String.Empty;

            sAlert = "You have successfully published " + Convert.ToString(form["val-title"]);

            if (!string.IsNullOrEmpty(Convert.ToString(form["val-title"])))
            {



                var mID = viewModel.Department.tid.ToString();
                long marID;
                bool res2 = long.TryParse(mID, out marID);

                var myDepart = db.tblDepartments.FirstOrDefault(x => x.tid == marID);

                if (myDepart != null)
                {
                    myDepart.Title = Convert.ToString(form["val-title"]);
                    myDepart.departmentCode = Convert.ToString(form["val-deptCode"]);
                    myDepart.lastupdate = DateTime.Now;
                    myDepart.updatedby = Convert.ToInt32(Session["TID"]);
                    myDepart.stateid = intState;
                    try
                    {

                        await db.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    return RedirectToAction("/");

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

            var unitOfWorkPost = db.tblDepartments.Find(id);

            if (unitOfWorkPost == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            string sAlert = String.Empty;

            unitOfWorkPost.stateid = 4;

            sAlert = "You have successfully deleted " + unitOfWorkPost.Title;

            await db.SaveChangesAsync();

            return RedirectToAction("/");

        }
    }
}