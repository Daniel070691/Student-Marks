using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VarsityProject.Models;
using VarsityProject.ViewModels;

namespace VarsityProject.Controllers
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

        //[HttpPost, ValidateInput(false)]
        //public async Task<ActionResult> Create(DepartmentsViewModel viewModel, FormCollection form)
        //{
        //    if (Session["uname"] == null && (string)Session["role"] != "Administrator")
        //        return RedirectToAction("AdministratorLogin", "Admin");
            

        //    string sAlert = "";
        //    int intState = 1;

        //    switch (Convert.ToString(form["btnSubmit"]))
        //    {
        //        case "Publish":
        //            intState = 1;
        //            sAlert = "You have successfully published " + viewModel.Department.Title;
        //            break;
        //        default:
        //            intState = 1;
        //            sAlert = "You have successfully published " + viewModel.Department.Title;
        //            break;
        //    }

        //    string sName = Convert.ToString(form["TeamMember.name"]).Trim();
        //    string sSurname = Convert.ToString(form["TeamMember.surname"]).Trim();
        //    string sContent = Convert.ToString(form["TeamMember.content"]);
        //    DateTime dtStartDate = viewModel.TeamMember.startdate;
        //    DateTime dtEndDate = viewModel.TeamMember.enddate;

        //    //int iPageID = Convert.ToInt32(form["pagecat"]);

        //    var newTeamMember = new tbl_TeamMembers();
        //    newTeamMember.name = sName;
        //    newTeamMember.surname = sSurname;
        //    newTeamMember.email = viewModel.TeamMember.email;
        //    newTeamMember.telephone = viewModel.TeamMember.telephone;
        //    newTeamMember.Positition = viewModel.TeamMember.Positition;
        //    newTeamMember.content = sContent;
        //    newTeamMember.insertdate = DateTime.Now;
        //    newTeamMember.stateid = intState;
        //    newTeamMember.startdate = dtStartDate;
        //    newTeamMember.enddate = dtEndDate;
        //    newTeamMember.lastupdate = DateTime.Now;
        //    newTeamMember.updatedby = Convert.ToInt32(Session["TID"]);
        //    newTeamMember.insertedBy = Convert.ToInt32(Session["TID"]);

        //    if (!string.IsNullOrEmpty(form["ID"]))
        //    {
        //        int intImageId = Convert.ToInt32(form["ID"]);
        //        newTeamMember.imageid = intImageId;
        //    }



        //    unitOfWork.tbl_TeamMembers.Add(newTeamMember);


        //    await unitOfWork.SaveChangesAsync();


        //    return RedirectToAction("/Index/").SetStatusMessage(sAlert, ExtensionMethods.NotificationType.success);



        //}

        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (Session["uname"] == null)
        //        return RedirectToAction("Index", "Admin");
        //    string username = User.Identity.Name;
        //    if (string.IsNullOrEmpty(username))
        //        return RedirectToAction("Index", "Admin");


        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    var viewModel = new TeamMembersViewModel();
        //    var tblTeamMember = await unitOfWork.tbl_TeamMembers.FindAsync(id);

        //    var partnerState = unitOfWork.tblStates.FirstOrDefault(t => t.TID == tblTeamMember.stateid);
        //    if (partnerState != null) ViewBag.PartnerStatus = partnerState.Heading;

        //    viewModel.StatusFilters = unitOfWork.tblStates.ToList().Select(x => new SelectListItem
        //    {
        //        Value = x.Acronym,
        //        Text = x.Heading
        //    }
        //        );

        //    if (tblTeamMember == null)
        //    {
        //        return HttpNotFound();
        //    }


        //    var marketstate = unitOfWork.tblStates.FirstOrDefault(t => t.TID == tblTeamMember.stateid);
        //    if (marketstate != null) ViewBag.PartnerStatus = marketstate.Heading;
        //    viewModel.TeamMember = tblTeamMember;

        //    return View(viewModel);
        //}


        //[HttpPost, ValidateInput(false)]
        //public async Task<ActionResult> Edit(TeamMembersViewModel viewModel, FormCollection form)
        //{
        //    if (Session["uname"] == null)
        //        return RedirectToAction("Index", "Admin");
        //    string username = User.Identity.Name;
        //    if (string.IsNullOrEmpty(username))
        //        return RedirectToAction("Index", "Admin");

        //    var intState = 1;
        //    string sAlert = String.Empty;
        //    switch (Convert.ToString(form["btnSubmit"]))
        //    {
        //        case "Publish":
        //            intState = 1;
        //            sAlert = "You have successfully published " + viewModel.TeamMember.name + " " + viewModel.TeamMember.surname;
        //            break;
        //        case "Unpublish":
        //            intState = 2;
        //            sAlert = "You have successfully unpublished " + viewModel.TeamMember.name + " " + viewModel.TeamMember.surname;
        //            break;
        //        case "Save as Draft":
        //            intState = 3;
        //            sAlert = "You have successfully saved " + viewModel.TeamMember.name + " " + viewModel.TeamMember.surname + "as a draft.";
        //            break;
        //    }

        //    var test = form["TeamMember.TID"];
        //    if (!string.IsNullOrEmpty(form["TeamMember.TID"]))
        //    {



        //        var mID = form["TeamMember.TID"];
        //        long marID;
        //        bool res2 = long.TryParse(mID, out marID);

        //        var myTeamMember = unitOfWork.tbl_TeamMembers.FirstOrDefault(x => x.TID == marID);


        //        //int groupIDsession = Convert.ToInt32(Session[General.sSessionSelectedGroupID].ToString());



        //        string strEditor = Convert.ToString(form["TeamMember.content"]);



        //        if (myTeamMember != null)
        //        {
        //            myTeamMember.name = viewModel.TeamMember.name;
        //            myTeamMember.surname = viewModel.TeamMember.surname;
        //            myTeamMember.email = viewModel.TeamMember.email;
        //            myTeamMember.Positition = viewModel.TeamMember.Positition;
        //            myTeamMember.telephone = viewModel.TeamMember.telephone;

        //            myTeamMember.content = strEditor;
        //            myTeamMember.Positition = viewModel.TeamMember.Positition;
        //            if (!string.IsNullOrEmpty(form["ID"]))
        //            {
        //                int intImageId = Convert.ToInt32(form["ID"]);
        //                myTeamMember.imageid = intImageId;
        //            }
        //            myTeamMember.startdate = viewModel.TeamMember.startdate;
        //            myTeamMember.enddate = viewModel.TeamMember.enddate;
        //            myTeamMember.lastupdate = DateTime.Now;
        //            myTeamMember.updatedby = Convert.ToInt32(Session["TID"].ToString());
        //            myTeamMember.stateid = intState;
        //            try
        //            {

        //                await unitOfWork.SaveChangesAsync();
        //            }
        //            catch (Exception ex)
        //            {

        //                throw;
        //            }
        //            return RedirectToAction("/").SetStatusMessage(sAlert, ExtensionMethods.NotificationType.success);

        //        }

        //    }
        //    return RedirectToAction("/").SetStatusMessage("Internal error, Please contact administrator.", ExtensionMethods.NotificationType.success);


        //}

        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (Session["uname"] == null)
        //        return RedirectToAction("Index", "Admin");
        //    string username = User.Identity.Name;
        //    if (string.IsNullOrEmpty(username))
        //        return RedirectToAction("Index", "Admin");

        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    var unitOfWorkPost = unitOfWork.tbl_TeamMembers.Find(id);

        //    if (unitOfWorkPost == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        //    string sAlert = String.Empty;

        //    unitOfWorkPost.stateid = 4;

        //    sAlert = "You have successfully deleted " + unitOfWorkPost.name + " " + unitOfWorkPost.surname;

        //    await unitOfWork.SaveChangesAsync();

        //    return RedirectToAction("/").SetStatusMessage(sAlert, ExtensionMethods.NotificationType.success);

        //}
    }
}