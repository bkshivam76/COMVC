using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Help.Models;
using ConnectOneMVC.Areas.Stock.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.Common;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.Projects;

namespace ConnectOneMVC.Areas.Stock.Controllers
{
    public class ProjectScreenController : BaseController
    {
        // GET: Stock/ProjectScreen
        #region Global variables
        public List<Return_GetRecord_Project> ProjectData
        {
            get
            {
                return (List<Return_GetRecord_Project>)GetBaseSession("ProjectData_Proj");
            }
            set
            {
                SetBaseSession("ProjectData_Proj", value);
            }
        }
        public List<Return_GetRegister> ProjectRegister_Data
        {
            get
            {
                return (List<Return_GetRegister>)GetBaseSession("ProjectRegister_Data_ProjInfo");
            }
            set
            {
                SetBaseSession("ProjectRegister_Data_ProjInfo", value);
            }
        }
        public DateTime? PRFromDate
        {
            get
            {
                return (DateTime?)GetBaseSession("PRFromDate_ProjInfo");
            }
            set
            {
                SetBaseSession("PRFromDate_ProjInfo", value);
            }
        }
        public DateTime? PRToDate
        {
            get
            {
                return (DateTime?)GetBaseSession("PRToDate_ProjInfo");
            }
            set
            {
                SetBaseSession("PRToDate_ProjInfo", value);
            }
        }
        public List<Return_GetProjectEstimation> Estimation_Grid_Data_Project
        {
            get
            {
                return (List<Return_GetProjectEstimation>)GetBaseSession("Estimation_Grid_Data_Project_Proj");
            }
            set
            {
                SetBaseSession("Estimation_Grid_Data_Project_Proj", value);
            }
        }
        public List<Return_GetDocumentsGridData> Documents_Window_Grid_Data_Project
        {
            get
            {
                return (List<Return_GetDocumentsGridData>)GetBaseSession("Documents_Window_Grid_Data_Project_Proj");
            }
            set
            {
                SetBaseSession("Documents_Window_Grid_Data_Project_Proj", value);
            }
        }
        public ArrayList Project_Edit_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Project_Edit_Document_ID_Proj");
            }
            set
            {
                SetBaseSession("Project_Edit_Document_ID_Proj", value);
            }
        }
        public ArrayList Project_Delete_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Project_Delete_Document_ID_Proj");
            }
            set
            {
                SetBaseSession("Project_Delete_Document_ID_Proj", value);
            }
        }
        public ArrayList Project_Unlink_Document_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Project_Unlink_Document_ID_Proj");
            }
            set
            {
                SetBaseSession("Project_Unlink_Document_ID_Proj", value);
            }
        }
        public ArrayList Edit_ProjEstID
        {
            get
            {
                return (ArrayList)GetBaseSession("Edit_ProjEstID_Proj");
            }
            set
            {
                SetBaseSession("Edit_ProjEstID_Proj", value);
            }
        }
        public ArrayList Delete_ProjEstimation_ID
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_ProjEstimation_ID_Proj");
            }
            set
            {
                SetBaseSession("Delete_ProjEstimation_ID_Proj", value);
            }
        }
        public ArrayList Delete_Remarks_ID_Project
        {
            get
            {
                return (ArrayList)GetBaseSession("Delete_Remarks_ID_Project_Proj");
            }
            set
            {
                SetBaseSession("Delete_Remarks_ID_Project_Proj", value);
            }
        }
        public List<Return_GetProjectRemarks> Remarks_Grid_Data_project
        {
            get
            {
                return (List<Return_GetProjectRemarks>)GetBaseSession("Remarks_Grid_Data_project_Proj");
            }
            set
            {
                SetBaseSession("Remarks_Grid_Data_project_Proj", value);
            }
        }
        
        #endregion
        #region Grid
        public ActionResult Frm_Project_Register_Info()
        {            
            if (CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Stock_Project, "List"))
            {
                Project_user_rights();
                Sessionclear();
                String PeriodString = SetDate();
                ViewBag.DefualtDateString = PeriodString;
                ViewBag.ShowHorizontalBar = 0;
                ViewData["ProjectExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
                ViewData["ProjectExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
                ViewData["ProjectExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
                
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Stock_Project').hide();</script>");
            }
        }
        public ActionResult Frm_Job_Project_Register_Grid(string command, int ShowHorizontalBar = 0)
        {
            Project_user_rights();
            ViewData["ProjectExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ProjectExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ProjectExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();            
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (ProjectRegister_Data == null || command == "REFRESH")
            {
                List<Return_GetRegister> PR_Data = BASE._Projects_Dbops.GetRegister(Convert.ToDateTime(PRFromDate), Convert.ToDateTime(PRToDate));
                ProjectRegister_Data = PR_Data;
            }
            List<Return_GetRegister> PR_Data_Final = ProjectRegister_Data as List<Return_GetRegister>;
            return PartialView(PR_Data_Final);
        }
        public ActionResult ProjectRegisterCustomDataAction(int key = 0)
        {
            string itstr = "";
            if (key != 0)
            {
                var Final_Data = ProjectRegister_Data as List<Return_GetRegister>;
                var it = (Return_GetRegister)Final_Data.Where(f => f.ID == key).FirstOrDefault();
                if (it != null)
                {
                    itstr = it.ID + "![" + it.Rec_Add_On + "![" + it.Rec_Add_By + "![" + it.Rec_Edit_On + "![" + it.Rec_Edit_By + "![" + it.CurrUserRole;

                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        #endregion
        #region Period Selection
        public string SetDate()
        {
            string xmonth;
            int year;
            if ((DateTime.Now >= new DateTime(BASE._open_Year_Sdt.Year, 4, 1)) && (DateTime.Now <= new DateTime(BASE._open_Year_Edt.Year, 3, 31)))
            {
                xmonth = (DateTime.Now.ToString("MMM")).ToUpper();
                if (xmonth == "JAN" || xmonth == "FEB" || xmonth == "MAR")
                {
                    year = BASE._open_Year_Edt.Year;
                }
                else
                {
                    year = BASE._open_Year_Sdt.Year;
                }

                return xmonth + "-" + year;
            }
            else
            {
                xmonth = "MAR";
                year = BASE._open_Year_Edt.Year;
                return xmonth + "-" + year;
            }
        }
        public ActionResult LookUp_Get_ViewType_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {

            var bankdata = new List<SelectListItem>() /*{ new SelectListItem {Value="",Text="" }, new SelectListItem { Value = "", Text = "" } }*/;
            string xMonth = string.Empty;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {

                switch (I)
                {
                    case 1:
                        xMonth = "JAN";
                        break;
                    case 2:
                        xMonth = "FEB";
                        break;
                    case 3:
                        xMonth = "MAR";
                        break;
                    case 4:
                        xMonth = "APR";
                        break;
                    case 5:
                        xMonth = "MAY";
                        break;
                    case 6:
                        xMonth = "JUN";
                        break;
                    case 7:
                        xMonth = "JUL";
                        break;
                    case 8:
                        xMonth = "AUG";
                        break;
                    case 9:
                        xMonth = "SEP";
                        break;
                    case 10:
                        xMonth = "OCT";
                        break;
                    case 11:
                        xMonth = "NOV";
                        break;
                    case 12:
                        xMonth = "DEC";
                        break;
                    default:
                        xMonth = "";
                        break;
                }
                bankdata.Add(new SelectListItem { Value = xMonth + "-" + BASE._open_Year_Sdt.Year, Text = xMonth + "-" + BASE._open_Year_Sdt.Year });

            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {

                switch (I)
                {
                    case 1:
                        xMonth = "JAN";
                        break;
                    case 2:
                        xMonth = "FEB";
                        break;
                    case 3:
                        xMonth = "MAR";
                        break;
                    case 4:
                        xMonth = "APR";
                        break;
                    case 5:
                        xMonth = "MAY";
                        break;
                    case 6:
                        xMonth = "JUN";
                        break;
                    case 7:
                        xMonth = "JUL";
                        break;
                    case 8:
                        xMonth = "AUG";
                        break;
                    case 9:
                        xMonth = "SEP";
                        break;
                    case 10:
                        xMonth = "OCT";
                        break;
                    case 11:
                        xMonth = "NOV";
                        break;
                    case 12:
                        xMonth = "DEC";
                        break;
                    default:
                        xMonth = "";
                        break;
                }
                bankdata.Add(new SelectListItem { Value = xMonth + "-" + BASE._open_Year_Edt.Year, Text = xMonth + "-" + BASE._open_Year_Edt.Year });

            }
            bankdata.AddRange(new List<SelectListItem>() { new SelectListItem { Value = "1st Quarter", Text = "1st Quarter" },
                                                           new SelectListItem { Value = "2rd Quarter", Text = "2rd Quarter" },
                                                           new SelectListItem { Value = "3th Quarter", Text = "3th Quarter" },

                                                           new SelectListItem { Value = "4th Quarter", Text = "4th Quarter" } ,
                                                           new SelectListItem { Value = "1st Half Yearly", Text = "1st Half Yearly" } ,
                                                           new SelectListItem { Value = "2nd Half Yearly", Text = "2nd Half Yearly" } ,
                                                           new SelectListItem { Value = "Nine Months", Text = "Nine Months" } ,
                                                           new SelectListItem { Value = "Financial Year", Text = "Financial Year" } ,
                                                           new SelectListItem { Value = "Specific Period", Text = "Specific Period" },
             });
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(bankdata, loadOptions)), "application/json");
        }
        public ActionResult LookUp_ViewType_ChangeEvent(string Chaval)
        {
            ProjectRegister_Period model = GetPeriod(Chaval);
            PRFromDate = model.Pr_Fromdate;
            PRToDate = model.Pr_Todate;
            return Json(new
            {
                Message = model,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ProjectRegister_Period GetPeriod(string Chaval)
        {
            ProjectRegister_Period model = new ProjectRegister_Period();
            DateTime xFr_Date = DateTime.Now;
            DateTime xTo_Date = DateTime.Now;
            if (Chaval == "1st Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }
            else if (Chaval == "2rd Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }
            else if (Chaval == "3th Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }

            else if (Chaval == "4th Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = xFr_Date.AddMonths(3).AddDays(-1);
            }
            else if (Chaval == "1st Half Yearly")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(6).AddDays(-1);
            }
            else if (Chaval == "2nd Half Yearly")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = xFr_Date.AddMonths(6).AddDays(-1);
            }
            else if (Chaval == "Nine Months")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(9).AddDays(-1);
            }
            else if (Chaval == "Financial Year")
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
            }
            else
            {
                var Sel_Month = Chaval.Substring(0, 3).ToUpper();
                var SEL_MM = 0;
                switch (Sel_Month)
                {
                    case "JAN":
                        SEL_MM = 1;
                        break;
                    case "FEB":
                        SEL_MM = 2;
                        break;
                    case "MAR":
                        SEL_MM = 3;
                        break;
                    case "APR":
                        SEL_MM = 4;
                        break;
                    case "MAY":
                        SEL_MM = 5;
                        break;
                    case "JUN":
                        SEL_MM = 6;
                        break;
                    case "JUL":
                        SEL_MM = 7;
                        break;
                    case "AUG":
                        SEL_MM = 8;
                        break;
                    case "SEP":
                        SEL_MM = 9;
                        break;
                    case "OCT":
                        SEL_MM = 10;
                        break;
                    case "NOV":
                        SEL_MM = 11;
                        break;
                    case "DEC":
                        SEL_MM = 12;
                        break;
                    default:
                        SEL_MM = 0;
                        break;
                }
                xFr_Date = new DateTime(Convert.ToInt32(Chaval.Substring(4, 4)), SEL_MM, 1);
                xTo_Date = xFr_Date.AddMonths(1).AddDays(-1);
            }
            model.Pr_BE_View_Period = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd-MMM, yyyy");
            model.Pr_Fromdate = xFr_Date;
            model.Pr_Todate = xTo_Date;
            return model;
        }
        public ActionResult Frm_Change_Period_Screen()
        {
            ProjectRegister_Period model = new ProjectRegister_Period();
            model.Pr_PeriodSelection = "Specific Period";
            model.Pr_Todate = (DateTime)PRToDate;
            model.Pr_Fromdate = (DateTime)PRFromDate;
            model.Pr_BE_View_Period = "";
            model.Pr_Opendate = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
            model.Pr_Closedate = new DateTime(BASE._open_Year_Edt.Year, 3, 31);
            return View(model);
        }
        public ActionResult Frm_Change_Period_Screen_PR(ProjectRegister_Period model)
        {
            if (model.Pr_Todate < model.Pr_Fromdate)
            {
                return Json(new
                {
                    Message = "To Date Should Be Greater Than From Date..!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                PRToDate = model.Pr_Todate;
                PRFromDate = model.Pr_Fromdate;
                return Json(new
                {
                    Message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region NEVD
        public ActionResult DataNavigation(string ActionMethod = null, int REC_ID = 0)
        {

            var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
            var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();

            if (ActionMethod == "Edit" || ActionMethod == "Delete")
            {
                ProjectData = BASE._Projects_Dbops.GetRecord(REC_ID);
                var RequestedDate = ProjectData[0].Proj_Request_Date;
                if (BASE._open_User_Type == "CLIENT ROLE")
                {
                    if (AuditedPeriod.Rows.Count > 0)
                    {
                        if (RequestedDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && RequestedDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                message = "No Changes Are Allowed In Audited Period.Request Date should not be in Audited period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (SubmittedPeriod.Rows.Count > 0)
                    {
                        if (RequestedDate >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && RequestedDate <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                        {
                            return Json(new
                            {
                                message = "No Changes Are Allowed In Accounts Submitted Period.Request Date Should Not Be In Account Submission Period...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            if (ActionMethod == "Delete" || ActionMethod == "Cancel" || ActionMethod == "Reject")
            {
                return CheckJobPosted(ActionMethod, REC_ID);
            }
            if (ActionMethod == "Complete")
            {
                var count = BASE._Projects_Dbops.GetProject_Open_Jobs_Count(REC_ID);
                if (count > 0)
                {
                    return Json(new
                    {
                        message = "A Project Can Be Marked As Completed When All Jobs Under It Are Complete/Rejected Or Cancelled...!!",
                        result = false,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (ActionMethod == "Estimate Approval")
            {
                if (Estimation_Grid_Data_Project == null || ((List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project).Count == 0)
                { // mantis bug 716 fixed
                    var EstimationData = BASE._Projects_Dbops.GetProjectEstimation(REC_ID); //mantis bug 1294 fixed
                    if (EstimationData == null || ((List<Return_GetProjectEstimation>)EstimationData).Count == 0)
                    {
                        return Json(new
                        {
                            message = "A Project Need To Have Entry In Estimation Grid, To Be Submitted For Approval..!!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }//Mantis bug 0001169 fixed
            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckJobPosted(string ActionMethod = null, int REC_ID = 0)
        {
            var Count = BASE._Projects_Dbops.GetProject_Jobs_Count(REC_ID);
            if (Count > 0)
            {
                return Json(new
                {
                    message = "Project Used in other screens Cannot Be " + ActionMethod + "ed" + "!!",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_Project_NEVD(string ActionMethod = null, int REC_ID = 0, string PostSuccessFunction = null, string PopUpID = "Dynamic_Content_popup", string buttonID = "ProjectNew")
        {
            Project_user_rights();
            var k = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            string[] AM = { "New", "Edit", "View", "Delete" };
            for (k = 0; k < Rights.Length; k++)
            {
                if (!CheckRights(BASE, ClientScreen.Stock_Project, Rights[k]) && ActionMethod == AM[k])
                {
                    return Content("<script language='javascript' type='text/javascript'>$('#" + PopUpID + "').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
                }
            }

            Sessionclear();
            Model_Project_NEVD model = new Model_Project_NEVD();
            model.PR_PostSuccessFunction = PostSuccessFunction == null ? "OnProjectRequestAjaxSuccessForm" : PostSuccessFunction;
            model.PR_PopUpID = PopUpID == null ? "Dynamic_Content_popup" : PopUpID;
            var Navigation_Mode_tag = (Navigation_Mode)Enum.Parse(typeof(Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Navigation_Mode_tag;
            model.TempActionMethod = Navigation_Mode_tag.ToString();
            model.Project_OpenUserID = BASE._open_User_PersonnelID == null ? 0 : (int)BASE._open_User_PersonnelID;
            model.Project_Request_Date = DateTime.Now;
            if (ActionMethod == "New")
            {
                if (BASE._open_PAD_No_Main == "9000")
                {
                    ViewBag.SelectAssigneeMainDept = "Construction";
                    //model.Project_Assignee_Main_Dept_ID = new int[] { 14037 };//construction dept//Mantis bug 0000314 fixed
                    //model.Proj_Assignee_Main_Dept_ID = "14037";//Mantis bug 0000314 fixed
                }
                model.Project_Requestor_Main_Dept_ID = BASE._open_User_MainDeptID;

                if (BASE._open_User_MainDeptID == BASE._open_User_SubDeptID)
                {
                    model.Project_Requestor_Sub_Dept_ID = null;
                }//Mantis bug 0000316 fixed
                else
                {
                    model.Project_Requestor_Sub_Dept_ID = BASE._open_User_SubDeptID;
                }//Mantis bug 0000316 fixed

                //  model.Project_Type_ID = "CEF9FE6B-7BB5-4628-B972-CCF30FDCD574";// New Construction
            }
            else if (ActionMethod == "Edit" || ActionMethod == "View" || ActionMethod == "Delete")
            {
                ProjectData = BASE._Projects_Dbops.GetRecord(REC_ID);

                model.Project_Name = ProjectData[0].Proj_Name;
                model.Project_Request_Date = ProjectData[0].Proj_Request_Date;
                model.Project_Complex_ID = ProjectData[0].Proj_Complex_Id;
                model.Project_Type_ID = ProjectData[0].Proj_Type_ID;
                model.Project_Sanction_No = ProjectData[0].Proj_Sanction_No;
                model.Project_Sanction_Date = ProjectData[0].Proj_Sanction_Date;
                model.Project_Requestor_Main_Dept_ID = ProjectData[0].Proj_Requestor_Main_Dept;
                model.Project_Requestor_Sub_Dept_ID = ProjectData[0].Proj_Requestor_Sub_Dept;
                model.Project_Estimation_Date = ProjectData[0].Proj_Estimation_Date;                
                model.Project_Req_Finish_Date = ProjectData[0].Proj_Finish_Date;//Mantis bug 0000454 fixed                
                model.Project_Req_Start_Date = ProjectData[0].Proj_Start_Date;//Mantis bug 0000454 fixed
                model.Project_Engineer_ID = ProjectData[0].Proj_Engineer_ID;
                model.Project_Estimator_ID = ProjectData[0].Proj_Estimator_ID;
                model.Project_Summary = ProjectData[0].Proj_Summary;
                model.ProjectID = REC_ID;

                var AssigneeMainDeptId = ProjectData[0].AssigneeMainDept.Split(',');
                List<int> AssMaindeptID = new List<int>();
                for (int i = 0; i < AssigneeMainDeptId.Count(); i++)
                {
                    AssMaindeptID.Add(Convert.ToInt32(AssigneeMainDeptId[i]));
                }
                model.Project_Assignee_Main_Dept_ID = AssMaindeptID.ToArray();

                model.Proj_Assignee_Main_Dept_ID = ProjectData[0].AssigneeMainDept;//Mantis bug 0000314 fixed

                if (ProjectData[0].AssigneeSubDept != null)
                {
                    var AssigneeSubDeptId = ProjectData[0].AssigneeSubDept.Split(',');//Mantis bug 0000314 fixed
                    List<int> AssSubdeptID = new List<int>();
                    for (int j = 0; j < AssigneeSubDeptId.Count(); j++)
                    {
                        AssSubdeptID.Add(Convert.ToInt32(AssigneeSubDeptId[j]));
                    }
                    model.Project_Assignee_Sub_Dept_ID = AssSubdeptID.ToArray();
                    model.Proj_Assignee_Sub_Dept_ID = ProjectData[0].AssigneeSubDept;//Mantis bug 0000314 fixed
                }//Mantis bug 0000323 fixed
                else
                {
                    model.Project_Assignee_Sub_Dept_ID = null;
                    model.Proj_Assignee_Sub_Dept_ID = null;
                }//Mantis bug 0000323 fixed

            }
            return View(model);
        }//Mantis bug 0000314 fixed
        [HttpPost]
        public ActionResult Frm_Project_NEVD(Model_Project_NEVD model)
        {
            try
            {
                if (model.TempActionMethod == "_New" || model.TempActionMethod == "_Edit")
                {

                    if (model.Project_Request_Date < BASE._open_Year_Sdt || model.Project_Request_Date > BASE._open_Year_Edt)
                    {
                        return Json(new
                        {
                            message = "Project Request Date Date Should Be Within Open Financial Year ...!",
                            result = false,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    var AuditedPeriod = BASE._Projects_Dbops.GetYrAuditedPeriod();
                    var SubmittedPeriod = BASE._Projects_Dbops.GetYrAccountsSubmittedPeriod();
                    
                    if ((BASE._open_User_Type == "CLIENT ROLE" && model.TempActionMethod == "_New") || model.TempActionMethod == "_Edit")
                    {
                        if (AuditedPeriod != null)
                        {
                            if (AuditedPeriod.Rows.Count > 0)
                            {
                                if (model.Project_Request_Date >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Project_Request_Date <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Request Date should not be in Audited period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (SubmittedPeriod != null)
                        {
                            if (SubmittedPeriod.Rows.Count > 0)
                            {
                                if (model.Project_Request_Date >= Convert.ToDateTime(AuditedPeriod.Rows[0]["FROMDATE"]) && model.Project_Request_Date <= Convert.ToDateTime(AuditedPeriod.Rows[0]["TODATE"]))
                                {
                                    return Json(new
                                    {
                                        message = "Request Date Should Not Be In Account Submission Period...!",
                                        result = false,
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    var project_data = BASE._Projects_Dbops.GetRegister(Convert.ToDateTime(BASE._open_Year_Sdt), Convert.ToDateTime(BASE._open_Year_Edt));
                    for (int i = 0; i < project_data.Count; i++)
                    {
                        if ((project_data[i].Project_Name == model.Project_Name && model.TempActionMethod == "_New") || (project_data[i].Project_Name == model.Project_Name && project_data[i].ID != model.ProjectID && model.TempActionMethod == "_Edit"))
                        {
                            return Json(new
                            {
                                message = "Project name need to be unique across Institute...!",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Project_Sanction_Date != null)
                    {
                        if (model.Project_Sanction_Date <= model.Project_Request_Date)
                        {
                            return Json(new
                            {
                                message = "Sanction Date Should Be Greater Than Project Request Date",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((!string.IsNullOrEmpty(model.Project_Sanction_No)) && model.TempActionMethod == "_Edit")
                    {
                        Param_GetProjCnt_ForGivenSanctionNo_CurrInstt param = new Param_GetProjCnt_ForGivenSanctionNo_CurrInstt();
                        param.Project_ID = model.ProjectID;//Mantis bug 0000472 fixed
                        param.Sanction_No = model.Project_Sanction_No;//Mantis bug 0000472 fixed
                        var count = BASE._Projects_Dbops.GetProjCnt_ForGivenSanctionNo_CurrInstt(param);//Mantis bug 0000472 fixed
                        if (count > 0)//Mantis bug 0000462 fixed//Mantis bug 0000472 fixed
                        {
                            return Json(new
                            {
                                message = "Sanction number should be unique and should not be already used in any project in current institution",
                                result = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (string.IsNullOrWhiteSpace(model.Project_Name))
                    {
                        return Json(new
                        {
                            message = "Project Name cannot be Blank . . . !",
                            result = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        model.Project_Name = model.Project_Name.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }

                    if (model.Project_Your_Remarks != null)
                    {
                        model.Project_Your_Remarks = model.Project_Your_Remarks.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                    if (model.Project_Summary != null)
                    {
                        model.Project_Summary = model.Project_Summary.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                    }
                }
                if (model.TempActionMethod == "_New")
                {
                    Param_Insert_Project_Txn InParam = new Param_Insert_Project_Txn();
                    NewInsertDocuments(ref InParam, model);
                    NewInsertEstimate(ref InParam);
                    NewInsertRemainingData(ref InParam, model);
                    var prjID = BASE._Projects_Dbops.InsertProject(InParam);
                    if (prjID > 0)
                    {
                        if (!string.IsNullOrEmpty(model.PR_StatusButton))
                        {
                            string msg = "";
                            Param_Update_Project_Status param = new Param_Update_Project_Status();
                            param.ProjectID = prjID;
                            var _CurrUserRole = BASE._Projects_Dbops.GetRegister(Convert.ToDateTime(PRFromDate), Convert.ToDateTime(PRToDate)).FindAll(x => x.ID == prjID).FirstOrDefault().CurrUserRole;
                            string[] CurrUserRole = new string[] { "" };
                            if (_CurrUserRole != null) { CurrUserRole = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }
                            switch (model.PR_StatusButton)
                            {
                                case "Estimate Creation":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Assigned_for_Estimation_Creation", true);
                                    msg = "Status Changed To 'Assigned For Estimation Creation'..!!";
                                    break;
                                case "Estimate Approval":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Submitted_for_Estimate_Approval", true);
                                    msg = "Status Changed To 'Submitted for Estimate Approval'..!!";
                                    break;
                                case "Approve":
                                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                                    {
                                        param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Requested", true);
                                        msg = "Status Changed To 'Requested'..!!";
                                    }
                                    if (CurrUserRole.Contains("Approver"))
                                    {
                                        param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Approved", true);
                                        msg = "Status Changed To 'Approved'..!!";
                                    }
                                    break;
                            }
                            if (BASE._Projects_Dbops.UpdateProjectStatus(param))
                            {
                                return Json(new
                                {
                                    result = true,
                                    message = msg
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = Messages.SomeError
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new
                            {
                                result = true,
                                message = Messages.SaveSuccess
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.TempActionMethod == "_Edit")
                {
                    Param_Update_Project_Txn InParam = new Param_Update_Project_Txn();
                    EditIUDDocuments(ref InParam, model);
                    EditIUDEstimate(ref InParam);
                    EditIUDRemainingData(ref InParam, model);
                    if (BASE._Projects_Dbops.UpdateProject(InParam))
                    {
                        if (!string.IsNullOrEmpty(model.PR_StatusButton))
                        {
                            string msg = "";
                            Param_Update_Project_Status param = new Param_Update_Project_Status();
                            param.ProjectID = model.ProjectID;
                            var _CurrUserRole = BASE._Projects_Dbops.GetRegister(Convert.ToDateTime(PRFromDate), Convert.ToDateTime(PRToDate)).FindAll(x => x.ID == model.ProjectID).FirstOrDefault().CurrUserRole;
                            string[] CurrUserRole = new string[] { "" };
                            if (_CurrUserRole != null) { CurrUserRole = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }
                            ProjectData = BASE._Projects_Dbops.GetRecord(model.ProjectID);
                            var Status = ProjectData[0].Proj_Status;
                            switch (model.PR_StatusButton)
                            {
                                case "Reopen":
                                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                                    {
                                        if (Status == "Rejected")
                                        {
                                            param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "_New", true);
                                            msg = "Status Changed To 'New'..!!";
                                        }
                                    }
                                    else if (CurrUserRole.Contains("Requestor"))
                                    {
                                        param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "_New", true);
                                        msg = "Status Changed To 'New'..!!";
                                    }
                                    else if (Status == "Completed")
                                    {
                                        if (CurrUserRole.Contains("Approver"))
                                        {
                                            param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "In_Progress", true);
                                            msg = "Status Changed To 'In Progress'..!!";
                                        }
                                        else if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                                        {
                                            param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Assigned", true);
                                            msg = "Status Changed To 'Assigned'..!!";
                                        }
                                    }
                                    break;
                                case "Estimate Creation":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Assigned_for_Estimation_Creation", true);
                                    msg = "Status Changed To 'Assigned For Estimation Creation'..!!";
                                    break;
                                case "Estimate Approval":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Submitted_for_Estimate_Approval", true);
                                    msg = "Status Changed To 'Submitted for Estimate Approval'..!!";
                                    break;
                                case "Approve":
                                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                                    {
                                        param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Requested", true);
                                        msg = "Status Changed To 'Requested'..!!";
                                    }
                                    if (CurrUserRole.Contains("Approver"))
                                    {
                                        param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Approved", true);
                                        msg = "Status Changed To 'Approved'..!!";
                                    }
                                    break;
                                case "Complete":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Completed", true);
                                    param.ProjectCompletionDate = DateTime.Now;
                                    msg = "Status Changed To Completed..!!";
                                    break;
                                case "Cancel":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Cancelled", true);
                                    msg = "Status Changed To Cancelled..!!";
                                    break;
                                case "Reject":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Rejected", true);
                                    msg = "Status Changed To Rejected..!!";
                                    break;
                                case "Changes Recommended":
                                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Changes_Recommended", true);
                                    msg = "Status Changed To Changes Recommended...!!";
                                    break;
                            }
                            if (BASE._Projects_Dbops.UpdateProjectStatus(param))
                            {
                                return Json(new
                                {
                                    result = true,
                                    message = msg
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new
                                {
                                    result = false,
                                    message = Messages.SomeError
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        return Json(new { result = true, message = Messages.UpdateSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.TempActionMethod == "_Delete")
                {
                    if (!BASE._Projects_Dbops.DeleteProject(model.ProjectID))
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return Json(new { result = true, message = Messages.DeleteSuccess }, JsonRequestBehavior.AllowGet); }
                }
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckDocumentsLinked(int ProjectID)
        {
            var docdata = Documents_Window_Grid_Data_Project as List<Return_GetDocumentsGridData>;
            for (int i = 0; i < docdata.Count; i++)
            {
                if (!string.IsNullOrEmpty(docdata[i].ID))
                {
                    var screen = BASE._Projects_Dbops.GetAttachmentLinkScreen(ProjectID, docdata[i].ID);
                    if (!string.IsNullOrEmpty(screen))//0000236 bug fixed
                    {             
                        if (screen != "Projects")
                        {
                            return Json(new
                            {
                                result = false,
                                message = "There Are Documents That Cannot Be Deleted Because They Have Been Linked To Other Screens</br> Do You Want To Unlink It From Project..?"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                result = false,
                                message = "This Document Cannot Be Deleted Because It Has been Attached To Some Other Entry In " + screen + ".Do You Want To Unlink It From This Entry..?"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(new
            {
                result = false,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #region Param-Fill
        public void NewInsertDocuments(ref Param_Insert_Project_Txn InParam, Model_Project_NEVD model)
        {
            var DocumentsData = (List<Return_GetDocumentsGridData>)Documents_Window_Grid_Data_Project;
            if (DocumentsData != null)
            {
                var insertAttachments = new Parameter_Insert_Attachment[DocumentsData.Count()];
                for (int i = 0; i < DocumentsData.Count; i++)
                {
                    if (DocumentsData[i].ID == null)
                    {
                        var InEInfo = new Parameter_Insert_Attachment();
                        InEInfo.FileName = DocumentsData[i].File_Name;
                        InEInfo.Description = DocumentsData[i].Remarks;
                        InEInfo.NameID = DocumentsData[i].Document_Name_ID;
                        InEInfo.Ref_Screen = "Projects";
                        InEInfo.Ref_Rec_ID = model.ProjectID.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments[i] = InEInfo;
                    }
                }
                InParam.InAttachment = insertAttachments;
            }
        }
        public void NewInsertEstimate(ref Param_Insert_Project_Txn InParam)
        {
            var all_data_Of_Est_Grid = (List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project;
            var insertEstimations = new Param_Insert_Project_Estimation[all_data_Of_Est_Grid.Count()];
            for (int I = 0; I <= all_data_Of_Est_Grid.Count() - 1; I++)
            {
                if (all_data_Of_Est_Grid[I].Sr_No > 0)
                {
                    var InEstimaInfo = new Param_Insert_Project_Estimation();
                    InEstimaInfo.Description = all_data_Of_Est_Grid[I].Description.ToString();
                    InEstimaInfo.UnitID = all_data_Of_Est_Grid[I].UnitID;
                    InEstimaInfo.Estimated_Quantity = Convert.ToDecimal(all_data_Of_Est_Grid[I].Estimated_Qty);
                    InEstimaInfo.Estimated_Rate = Convert.ToDecimal(all_data_Of_Est_Grid[I].Rate);
                    InEstimaInfo.Estimated_Amount = Convert.ToDecimal(all_data_Of_Est_Grid[I].Total_Amount);
                    insertEstimations[I] = InEstimaInfo;
                }
            }
            InParam.InProjectEstimation = insertEstimations;
        }
        public void NewInsertRemainingData(ref Param_Insert_Project_Txn InParam, Model_Project_NEVD model)
        {
            //InParam.Project_Actual_Start_Date = model.Project_Actual_Start_Date;
            InParam.Project_Req_Start_Date = model.Project_Req_Start_Date;//Mantis bug 0000454 fixed
            InParam.Project_Estimator_ID = model.Project_Estimator_ID;
            InParam.Project_Engineer_ID = model.Project_Engineer_ID;

            if (model.Proj_Assignee_Sub_Dept_ID != null)
            {
                var AssigneeSubDeptId = Convert.ToString(model.Proj_Assignee_Sub_Dept_ID).Split(',');//Mantis bug 0000314 fixed
                List<int?> AssSubdeptID = new List<int?>();
                for (int i = 0; i < AssigneeSubDeptId.Count(); i++)
                {
                    AssSubdeptID.Add(Convert.ToInt32(AssigneeSubDeptId[i]));
                }
                InParam.Project_Assignee_Sub_Dept_ID = AssSubdeptID.ToArray();
            }//Mantis bug 0000323 fixed
            else
            {
                InParam.Project_Assignee_Sub_Dept_ID = null;
            }//Mantis bug 0000323 fixed


            var AssigneeMainDeptId = Convert.ToString(model.Proj_Assignee_Main_Dept_ID).Split(',');//Mantis bug 0000314 fixed
            List<int?> AssMaindeptID = new List<int?>();
            for (int j = 0; j < AssigneeMainDeptId.Count(); j++)//Mantis bug 0000314 fixed
            {
                AssMaindeptID.Add(Convert.ToInt32(AssigneeMainDeptId[j]));
            }
            InParam.Project_Assignee_Main_Dept_ID = AssMaindeptID.ToArray();

            InParam.Project_Summary = model.Project_Summary;
            InParam.Project_Estimation_Date = model.Project_Estimation_Date;
            InParam.Project_Req_Finish_Date = model.Project_Req_Finish_Date;
            InParam.Project_Req_Start_Date = model.Project_Req_Start_Date;
            InParam.Project_Requestor_Sub_Dept_ID = model.Project_Requestor_Sub_Dept_ID;
            InParam.Project_Requestor_Main_Dept_ID = model.Project_Requestor_Main_Dept_ID;
            InParam.Sanction_Date = model.Project_Sanction_Date;
            InParam.Santion_No = model.Project_Sanction_No;
            InParam.Project_Type_ID = model.Project_Type_ID;
            InParam.Project_Complex_ID = model.Project_Complex_ID;
            InParam.Project_Request_Date = (DateTime)model.Project_Request_Date;
            InParam.Project_Name = model.Project_Name;
            //InParam.Project_Actual_Finish_Date = model.Project_Actual_Finish_Date;
            InParam.Project_Req_Finish_Date = model.Project_Req_Finish_Date;//Mantis bug 0000454 fixed
            InParam.Remarks = model.Project_Your_Remarks;
        }//Mantis bug 0000314 fixed
        public void EditIUDDocuments(ref Param_Update_Project_Txn InParam, Model_Project_NEVD model)
        {
            var DocumentsData = (List<Return_GetDocumentsGridData>)Documents_Window_Grid_Data_Project;
            if (DocumentsData != null)
            {
                var insertAttachments = new List<Parameter_Insert_Attachment>();
                var updateattachment = new List<Parameter_Update_Attachment>();
                string[] doceditid = Project_Edit_Document_ID != null ? (string[])(Project_Edit_Document_ID as ArrayList).ToArray(typeof(string)) : null;
                for (int i = 0; i < DocumentsData.Count; i++)
                {
                    if (DocumentsData[i].ID == null)
                    {
                        var InEInfo = new Parameter_Insert_Attachment();
                        InEInfo.FileName = DocumentsData[i].File_Name;
                        InEInfo.Description = DocumentsData[i].Remarks;
                        InEInfo.NameID = DocumentsData[i].Document_Name_ID;
                        InEInfo.Ref_Screen = "Projects";
                        InEInfo.Ref_Rec_ID = model.ProjectID.ToString();
                        InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                        InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                        InEInfo.File = DocumentsData[i].File_Array;
                        InEInfo.RecID = System.Guid.NewGuid().ToString();
                        insertAttachments.Add(InEInfo);
                    }
                    else if (doceditid != null)
                    {
                        if (doceditid.Contains(DocumentsData[i].ID))
                        {
                            var InEInfo = new Parameter_Update_Attachment();
                            InEInfo.FileName = DocumentsData[i].File_Name;
                            InEInfo.Description = DocumentsData[i].Remarks;
                            InEInfo.CategoryID = DocumentsData[i].Document_Name_ID;
                            InEInfo.Ref_Screen = "Projects";
                            InEInfo.Ref_Rec_ID = model.ProjectID.ToString();
                            InEInfo.Applicable_From = Convert.ToDateTime(DocumentsData[i].Applicable_From);
                            InEInfo.Applicable_To = Convert.ToDateTime(DocumentsData[i].Applicable_To);
                            InEInfo.File = DocumentsData[i].File_Array;
                            InEInfo.RecID = DocumentsData[i].ID;
                            updateattachment.Add(InEInfo);
                        }
                    }
                }
                if (insertAttachments.Count > 0) { InParam.Added_Attachments = insertAttachments[0] == null ? null : insertAttachments.ToArray(); }
                else { InParam.Added_Attachments = insertAttachments.ToArray(); }
                if (updateattachment.Count > 0) { InParam.Updated_Attachments = updateattachment[0] == null ? null : updateattachment.ToArray(); }
                else { InParam.Updated_Attachments = updateattachment.ToArray(); }
            }
            var DocAllDeleteIds = Project_Delete_Document_ID as ArrayList;
            if (DocAllDeleteIds != null)
            {
                var deleteDocID = new Param_Deleted_Attachments[DocAllDeleteIds.Count];
                for (int i = 0; i <= DocAllDeleteIds.Count - 1; i++)
                {
                    var deleteDocIDInfo = new Param_Deleted_Attachments();
                    deleteDocIDInfo.Rec_ID = DocAllDeleteIds[i].ToString();
                    deleteDocID[i] = deleteDocIDInfo;
                }
                InParam.Deleted_ProjectAttachments = deleteDocID;
            }
            var DocAllunlinkIds = Project_Unlink_Document_ID as ArrayList;
            if (DocAllunlinkIds != null)
            {
                var unlinkDocID = new Param_Unlinked_Attachments[DocAllunlinkIds.Count];
                for (int i = 0; i <= DocAllunlinkIds.Count - 1; i++)
                {
                    var unlinkDocIDInfo = new Param_Unlinked_Attachments();
                    unlinkDocIDInfo.Rec_ID = DocAllunlinkIds[i].ToString();
                    unlinkDocID[i] = unlinkDocIDInfo;
                }
                InParam.Unlinked_ProjectAttachments = unlinkDocID;
            }
        }
        public void EditIUDEstimate(ref Param_Update_Project_Txn InParam)
        {
            var all_data_Of_Est_Grid = (List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project;
            if (all_data_Of_Est_Grid != null)
            {
                var insertEst = new List<Param_Insert_Project_Estimation>();
                var updatEst = new List<Param_Update_Project_Estimation>();
                int[] estEditID = Edit_ProjEstID != null ? (int[])(Edit_ProjEstID as ArrayList).ToArray(typeof(int)) : null;
                for (int i = 0; i < all_data_Of_Est_Grid.Count; i++)
                {
                    if (all_data_Of_Est_Grid[i].ID == 0)
                    {
                        var InEstimaInfo = new Param_Insert_Project_Estimation();
                        InEstimaInfo.Description = all_data_Of_Est_Grid[i].Description.ToString();
                        InEstimaInfo.UnitID = all_data_Of_Est_Grid[i].UnitID;
                        InEstimaInfo.Estimated_Quantity = Convert.ToDecimal(all_data_Of_Est_Grid[i].Estimated_Qty);
                        InEstimaInfo.Estimated_Rate = Convert.ToDecimal(all_data_Of_Est_Grid[i].Rate);
                        InEstimaInfo.Estimated_Amount = Convert.ToDecimal(all_data_Of_Est_Grid[i].Total_Amount);
                        insertEst.Add(InEstimaInfo);
                    }
                    else if (estEditID != null)
                    {
                        if (estEditID.Contains(all_data_Of_Est_Grid[i].ID))
                        {
                            var InEstimaInfo = new Param_Update_Project_Estimation();
                            InEstimaInfo.Description = all_data_Of_Est_Grid[i].Description.ToString();
                            InEstimaInfo.UnitID = all_data_Of_Est_Grid[i].UnitID;
                            InEstimaInfo.Estimated_Quantity = Convert.ToDecimal(all_data_Of_Est_Grid[i].Estimated_Qty);
                            InEstimaInfo.Estimated_Rate = Convert.ToDecimal(all_data_Of_Est_Grid[i].Rate);
                            InEstimaInfo.Estimated_Amount = Convert.ToDecimal(all_data_Of_Est_Grid[i].Total_Amount);
                            InEstimaInfo.Estimation_Rec_ID = all_data_Of_Est_Grid[i].ID;
                            updatEst.Add(InEstimaInfo);
                        }
                    }
                }
                if (insertEst.Count() > 0) { InParam.Added_ProjectEstimations = insertEst[0] == null ? null : insertEst.ToArray(); }
                else { InParam.Added_ProjectEstimations = insertEst.ToArray(); }
                if (updatEst.Count() > 0) { InParam.Updated_ProjectEstimations = updatEst[0] == null ? null : updatEst.ToArray(); }
                else { InParam.Updated_ProjectEstimations = updatEst.ToArray(); }

            }//Mantis bug 0000222 fixed
            var ESTAllDeleteIds = Delete_ProjEstimation_ID as ArrayList;
            if (ESTAllDeleteIds != null)
            {
                var deleteESTID = new Param_Deleted_Estimations[ESTAllDeleteIds.Count];
                for (int i = 0; i <= ESTAllDeleteIds.Count - 1; i++)
                {
                    var deleteESTIDInfo = new Param_Deleted_Estimations();
                    deleteESTIDInfo.Rec_ID = (int)ESTAllDeleteIds[i];
                    deleteESTID[i] = deleteESTIDInfo;
                }
                InParam.Deleted_ProjectEstimates = deleteESTID;
            }
        }
        public void EditIUDRemainingData(ref Param_Update_Project_Txn InParam, Model_Project_NEVD model)
        {
            //InParam.Project_Actual_Start_Date = model.Project_Actual_Start_Date;
            InParam.Project_Req_Start_Date = model.Project_Req_Start_Date;//Mantis bug 0000454 fixed
            InParam.Project_Estimator_ID = model.Project_Estimator_ID;
            InParam.Project_Engineer_ID = model.Project_Engineer_ID;

            if (model.Proj_Assignee_Sub_Dept_ID != null)
            {
                var AssigneeSubDeptId = model.Proj_Assignee_Sub_Dept_ID.Split(',');
                List<int?> AssSubdeptID = new List<int?>();
                for (int j = 0; j < AssigneeSubDeptId.Count(); j++)
                {
                    AssSubdeptID.Add(Convert.ToInt32(AssigneeSubDeptId[j]));
                }
                InParam.Project_Assignee_Sub_Dept_ID = AssSubdeptID.ToArray();
            }//Mantis bug 0000323 fixed
            else
            {
                InParam.Project_Assignee_Sub_Dept_ID = null;
            }//Mantis bug 0000323 fixed

            var AssigneeMainDeptId = model.Proj_Assignee_Main_Dept_ID.Split(',');
            List<int?> AssMaindeptID = new List<int?>();
            for (int j = 0; j < AssigneeMainDeptId.Count(); j++)
            {
                AssMaindeptID.Add(Convert.ToInt32(AssigneeMainDeptId[j]));
            }
            InParam.Project_Assignee_Main_Dept_ID = AssMaindeptID.ToArray();

            InParam.Project_Summary = model.Project_Summary;
            InParam.Project_Estimation_Date = model.Project_Estimation_Date;
            InParam.Project_Req_Finish_Date = model.Project_Req_Finish_Date;
            InParam.Project_Req_Start_Date = model.Project_Req_Start_Date;
            InParam.Project_Requestor_Sub_Dept_ID = model.Project_Requestor_Sub_Dept_ID;
            InParam.Project_Requestor_Main_Dept_ID = model.Project_Requestor_Main_Dept_ID;
            InParam.Sanction_Date = model.Project_Sanction_Date;
            InParam.Santion_No = model.Project_Sanction_No;
            InParam.Project_Type_ID = model.Project_Type_ID;
            InParam.Project_Complex_ID = model.Project_Complex_ID;
            InParam.Project_Request_Date = (DateTime)model.Project_Request_Date;
            InParam.Project_Name = model.Project_Name;
            //InParam.Project_Actual_Finish_Date = model.Project_Actual_Finish_Date;
            InParam.Project_Req_Finish_Date = model.Project_Req_Finish_Date;//Mantis bug 0000454 fixed
            InParam.Remarks = model.Project_Your_Remarks;
            InParam.ProjectID = model.ProjectID;
            var allData = Delete_Remarks_ID_Project as ArrayList;
            if (allData != null)
            {
                var deleteExist_RemarksID = new Param_Deleted_Remarks[allData.Count];
                for (int i = 0; i < allData.Count; i++)
                {
                    var deleteRemarksInfo = new Param_Deleted_Remarks();
                    deleteRemarksInfo.Rec_ID = Convert.ToInt32(allData[i]);
                    deleteExist_RemarksID[i] = deleteRemarksInfo;
                }
                InParam.Deleted_ProjectRemarks = deleteExist_RemarksID;
            }

        }//Mantis bug 0000314 fixed
        #endregion
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Stock_Project, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#ProjectRegister_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');$('#ProjectListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
        #region DropDown
        public ActionResult LookUp_GetComplexNames(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var complexData = BASE._Complexes_DBOps.GetList(BASE._open_PAD_No_Main, BASE._prev_Unaudited_YearID);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(complexData, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetReqMain_Depts(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockDeptStores.Return_GetStoreDept> MainDept = BASE._StockDeptStores_dbops.GetMainDeptList(ClientScreen.Stock_Project);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MainDept, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetAssigMain_Depts(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var MainDept = BASE._StockDeptStores_dbops.GetMainDeptList(ClientScreen.Stock_Project);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(MainDept, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetProjectType(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            List<Return_GetProjTypes> projTypes = BASE._Projects_Dbops.GetProjTypes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(projTypes, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetProjectEngineer(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            List<Return_GetProjectEnginners> projEngineer = BASE._Projects_Dbops.GetProjectEnginners();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(projEngineer, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetRequestSub_Dept(bool? IsVisible, DataSourceLoadOptions loadOptions, int? MainID)
        {
            var SubDept = BASE._StockDeptStores_dbops.GetSubDeptList(ClientScreen.Stock_Project, Convert.ToInt32(MainID));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SubDept, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetAssigneSubDept(bool? IsVisible, DataSourceLoadOptions loadOptions, string AssigMainID)
        {
            List<StockDeptStores.Return_GetStoreDept> SubDept = new List<StockDeptStores.Return_GetStoreDept>();
            List<StockDeptStores.Return_GetStoreDept> Finaldata = new List<StockDeptStores.Return_GetStoreDept>();
            if (!(AssigMainID == null || AssigMainID == "" || AssigMainID == "System.Int32[]"))
            {
                List<string> AssMainDeptID = AssigMainID.Split(',').ToList();
                for (int i = 0; i < AssMainDeptID.Count(); i++)
                {
                    SubDept = BASE._StockDeptStores_dbops.GetSubDeptList(ClientScreen.Stock_Project, Convert.ToInt32(AssMainDeptID[i]));
                    for (int j = 0; j < SubDept.Count(); j++)
                    {
                        StockDeptStores.Return_GetStoreDept newRow = new StockDeptStores.Return_GetStoreDept();
                        newRow.Center = SubDept[j].Center;
                        newRow.ID = SubDept[j].ID;
                        newRow.InCharge_ID = SubDept[j].InCharge_ID;
                        newRow.InCharge_Name = SubDept[j].InCharge_Name;
                        newRow.Name = SubDept[j].Name;
                        newRow.Type = SubDept[j].Type;
                        Finaldata.Add(newRow);
                    }
                }
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Finaldata, loadOptions)), "application/json");
        }//Mantis bug 0000314 fixed
        public ActionResult LookUp_GetProjectEstimator(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            var projEstimator = BASE._Projects_Dbops.GetProjectEstimators();

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(projEstimator, loadOptions)), "application/json");
        }
        #endregion
        #region InnerGrid
        #region Document
        public ActionResult DocumentsGridData(string ActionMethodName,string SessionGUID, int ProjectID = 0)
        {
            var docList = new List<Return_GetDocumentsGridData>();
            if (ActionMethodName == "New")
            {
                return PartialView(docList);
            }
            if (Documents_Window_Grid_Data_Project == null)
            {
                var docData = BASE._Projects_Dbops.GetProjectDocuments(ProjectID);
                if (docData != null)
                {
                    docList = docData;
                }
                Documents_Window_Grid_Data_Project = docList;
            }
            if (((List<Return_GetDocumentsGridData>)Documents_Window_Grid_Data_Project).Count > 0)
            {
                for (int i = 0; i < ((List<Return_GetDocumentsGridData>)Documents_Window_Grid_Data_Project).Count; i++)
                {
                    ((List<Return_GetDocumentsGridData>)Documents_Window_Grid_Data_Project)[i].Sr_No = i + 1;
                }
            }
            return PartialView(Documents_Window_Grid_Data_Project);
        }
        public ActionResult Frm_Project_Document()
        {
            Model_Attachment_Window model = (Model_Attachment_Window)GetBaseSession("Frm_Project_Document_AttachmentData");
            if (model.Help_REF_REC_ID != null)
            {
                try
                {
                    Parameter_Insert_Attachment InEInfo = new Parameter_Insert_Attachment();

                    InEInfo.FileName = model.Help_Document_FileName;
                    InEInfo.Description = model.Help_Document_Description;
                    InEInfo.NameID = model.Help_Document_NameID;
                    InEInfo.Ref_Screen = "Projects";
                    InEInfo.Ref_Rec_ID = model.Help_REF_REC_ID;
                    InEInfo.Applicable_From = Convert.ToDateTime(model.Help_Doc_From_Date);
                    InEInfo.Applicable_To = Convert.ToDateTime(model.Help_Doc_To_Date);
                    InEInfo.File = model.Help_filefield;
                    InEInfo.RecID = System.Guid.NewGuid().ToString();
                    if (BASE._Attachments_DBOps.Insert(InEInfo).Length > 0)
                    {
                        return Json(new { result = true, message = Messages.SaveSuccess }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                    return Json(new
                    {
                        Message = message,
                        result = false
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<Return_GetDocumentsGridData> gridRows = new List<Return_GetDocumentsGridData>();
                if (Documents_Window_Grid_Data_Project != null)
                {
                    gridRows = (List<Return_GetDocumentsGridData>)Documents_Window_Grid_Data_Project;
                }
                if (model.ActionMethod == "New")
                {
                    Return_GetDocumentsGridData grid = new Return_GetDocumentsGridData();
                    grid.Document_Type = model.Help_Document_CategoryID;
                    grid.Document_Name = model.Help_Document_Name;
                    grid.File_Name = model.Help_Document_FileName;
                    grid.Remarks = model.Help_Document_Description;
                    grid.File_Array = model.Help_filefield;
                    grid.Applicable_From = model.Help_Doc_From_Date;
                    grid.Applicable_To = model.Help_Doc_To_Date;
                    grid.Document_Name_ID = model.Help_Document_NameID;
                    grid.Added_On = DateTime.Now;
                    grid.Added_By = BASE._open_User_ID;
                    gridRows.Add(grid);
                }
                else if (model.ActionMethod == "Edit")
                {
                    var dataToEdit = gridRows.FirstOrDefault(x => x.Sr_No == model.Sr_no);
                    var editDocumentID = new ArrayList();
                    if (model.ID != null || model.ID != "0" || model.ID != "")
                    {
                        var editDocument = Project_Edit_Document_ID as ArrayList;
                        if (editDocument != null)
                        {
                            editDocument.Add(model.ID);
                            Project_Edit_Document_ID = editDocument;
                        }
                        else
                        {
                            editDocumentID.Add(model.ID);
                            Project_Edit_Document_ID = editDocumentID;
                        }
                    }
                    dataToEdit.File_Name = model.Help_Document_FileName;
                    dataToEdit.Document_Type = model.Help_Document_CategoryID;
                    dataToEdit.Document_Name = model.Help_Document_Name;
                    dataToEdit.Remarks = model.Help_Document_Description;
                    dataToEdit.File_Array = model.Help_filefield;
                    dataToEdit.Applicable_From = model.Help_Doc_From_Date;
                    dataToEdit.Applicable_To = model.Help_Doc_To_Date;
                    dataToEdit.Document_Name_ID = model.Help_Document_NameID;
                }
                Documents_Window_Grid_Data_Project = gridRows;
                return Json(new
                {
                    result = true,
                    message = "Saved Successfully"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PR_Documents_Attachment_LinkCheck(string DocId, int PRId)
        {
            var screen = BASE._Projects_Dbops.GetAttachmentLinkScreen(PRId, DocId);
            if (!string.IsNullOrEmpty(screen))
            {
                if (screen != "Projects")
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To " + screen + ".Do You Want To Unlink It From Projects..?"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = "This Document Cannot Be Deleted Because It Has been Attached To Some Other Entry In " + screen + ".Do You Want To Unlink It From This Entry..?"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_DocumentsDetail_Window_Delete_Grid_Record(string ActionMethod, string SrID = null, string Doc_ID = null)
        {
            var srid = Convert.ToInt32(SrID);
            List<Return_GetDocumentsGridData> allDocData = (List<Return_GetDocumentsGridData>)Documents_Window_Grid_Data_Project;
            var dataToDelete = allDocData != null ? allDocData.Where(x => x.Sr_No == srid).FirstOrDefault() : new Return_GetDocumentsGridData();
            if (allDocData != null)
            {
                allDocData.Remove(dataToDelete);
            }
            Documents_Window_Grid_Data_Project = allDocData;
            if (ActionMethod == "Delete")
            {
                if (Doc_ID != null || Doc_ID != "")
                {
                    var deleteDocumentID = new ArrayList();
                    var deleteDocument = Project_Delete_Document_ID as ArrayList;
                    if (deleteDocument != null)
                    {
                        deleteDocument.Add(Doc_ID);
                        Project_Delete_Document_ID = deleteDocument;
                    }
                    else
                    {
                        deleteDocumentID.Add(Doc_ID);
                        Project_Delete_Document_ID = deleteDocumentID;
                    }
                }
            }
            if (ActionMethod == "Unlink")
            {
                var unlinkDocumentID = new ArrayList();
                var unlinkDocument = Project_Unlink_Document_ID as ArrayList;
                if (unlinkDocument != null)
                {
                    unlinkDocument.Add(Doc_ID);
                    Project_Unlink_Document_ID = unlinkDocument;
                }
                else
                {
                    unlinkDocumentID.Add(Doc_ID);
                    Project_Unlink_Document_ID = unlinkDocumentID;
                }
            }

            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Estimation
        public ActionResult EstimationGridData(string ActionMethodName, int ProjectID = 0, int EstimatorID = 0)
        {
            ViewData["ProjectExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ProjectExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ProjectExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Estimation_HideColumn = true;
            if (ProjectID > 0)
            {
                var _CurrUserRole = BASE._Projects_Dbops.GetRegister(Convert.ToDateTime(PRFromDate), Convert.ToDateTime(PRToDate)).FindAll(x => x.ID == ProjectID).FirstOrDefault().CurrUserRole;
                string[] CurrUserRole = new string[] { "" };
                if (_CurrUserRole != null) { CurrUserRole = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }
                if (CurrUserRole.Contains("Approver") || CurrUserRole.Contains("Estimator"))
                {
                    ViewBag.Estimation_HideColumn = false;
                }
            }
            else
            {
                if (EstimatorID == BASE._open_User_PersonnelID || CheckRights(BASE, ClientScreen.Stock_Project, "Approve"))
                {
                    ViewBag.Estimation_HideColumn = false;
                }
            }
            var EstimList = new List<Return_GetProjectEstimation>();
            if (ActionMethodName == "New")
            {
                return PartialView(EstimList);
            }
            if (Estimation_Grid_Data_Project == null)
            {
                var EstimationData = BASE._Projects_Dbops.GetProjectEstimation(ProjectID);
                if (EstimationData != null)
                {
                    EstimList = EstimationData;
                }
                Estimation_Grid_Data_Project = EstimList;
            }
            if (((List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project).Count > 0)
            {
                for (int i = 0; i < ((List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project).Count; i++)
                {
                    ((List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project)[i].Sr_No = i + 1;
                }
            }
            return PartialView(Estimation_Grid_Data_Project);
        }
        public ActionResult Frm_Project_Request_Add_Estimate(string ActionMethod = null, string SrID = null)
        {
            Navigation_Mode Tag = (Navigation_Mode)Enum.Parse(typeof(Navigation_Mode), ActionMethod);
            Model_EstimationDetails model = new Model_EstimationDetails();
            model.Tag = Tag;
            if (ActionMethod == "_Edit" || ActionMethod == "_View")
            {
                var Sr = Convert.ToInt16(SrID);
                var all_data = (List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project;
                var dataToEdit = all_data != null ? all_data.Where(x => x.Sr_No == Sr).FirstOrDefault() : new Return_GetProjectEstimation();
                model.SR_NO = Sr;
                model.Est_Description = dataToEdit != null ? dataToEdit.Description : "";
                model.Estimated_Qty = Convert.ToDouble(dataToEdit.Estimated_Qty);
                model.Est_Unit = dataToEdit.UnitID;
                model.Est_UnitName = dataToEdit.Unit;
                model.Est_Rate = Convert.ToDouble(dataToEdit.Rate);
                model.Est_Amount = Convert.ToDouble(dataToEdit.Total_Amount);
                model.ID = dataToEdit.ID;
            }
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Project_Request_Add_Estimate(Model_EstimationDetails model)
        {

            if (model.Tag == Navigation_Mode._New | model.Tag == Navigation_Mode._Edit)
            {
                if (string.IsNullOrEmpty(model.Est_Description))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Description of work can not be Empty...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Est_Description = model.Est_Description.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|'); //Mantis bug 1359 fixed
                }

                if (string.IsNullOrEmpty(model.Estimated_Qty.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Estimation Qty can not be empty..!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.Est_Unit))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Estimation Unit can not be empty..!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.Est_Rate.ToString()))
                {
                    return Json(new
                    {
                        result = false,
                        message = "Estimation Rate per Unit can not be empty..!"
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Convert.ToInt32(model.Estimated_Qty) < 0)
                {
                    return Json(new
                    {
                        result = false,
                        message = "Estimation Qty can not be Negative...!"
                    }, JsonRequestBehavior.AllowGet);
                }
                //if (Convert.ToInt32(model.Est_Rate) < 0)
                //{

                //    return Json(new
                //    {
                //        result = false,
                //        message = "Estimation Rate per Unit can not be Negative...!"
                //    }, JsonRequestBehavior.AllowGet);
                //}
            }
            List<Return_GetProjectEstimation> gridRows = new List<Return_GetProjectEstimation>();
            if (Estimation_Grid_Data_Project != null)
            {
                gridRows = (List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project;
            }
            if (model.Tag == Navigation_Mode._New)
            {
                Return_GetProjectEstimation grid = new Return_GetProjectEstimation();

                grid.Description = model.Est_Description;
                grid.Estimated_Qty = Convert.ToDecimal(model.Estimated_Qty);
                grid.Unit = model.Est_UnitName;
                grid.Rate = Convert.ToDecimal(model.Est_Rate);
                grid.Total_Amount = Convert.ToDecimal(model.Est_Amount);
                grid.Unit = model.Est_UnitName;
                grid.UnitID = model.Est_Unit;
                gridRows.Add(grid);
            }
            else if (model.Tag == Navigation_Mode._Edit)
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr_No == model.SR_NO);
                dataToEdit.Description = model.Est_Description;
                dataToEdit.Estimated_Qty = Convert.ToDecimal(model.Estimated_Qty);
                dataToEdit.Unit = model.Est_UnitName; //mantis bug #1310
                dataToEdit.UnitID = model.Est_Unit;
                dataToEdit.Rate = Convert.ToDecimal(model.Est_Rate);
                dataToEdit.Total_Amount = Convert.ToDecimal(model.Est_Amount);
                if (model.ID != 0)
                {
                    var editEstID = new ArrayList();
                    var editEst = Edit_ProjEstID as ArrayList;
                    if (editEst != null)
                    {
                        editEst.Add(model.ID);
                        Edit_ProjEstID = editEst;
                    }
                    else
                    {
                        editEstID.Add(model.ID);
                        Edit_ProjEstID = editEstID;
                    }
                }
            }
            Estimation_Grid_Data_Project = gridRows;
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_EstimationDetail_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null)
        {
            var Sr = Convert.ToInt16(SrID);
            var allData = (List<Return_GetProjectEstimation>)Estimation_Grid_Data_Project;
            var dataToDelete = allData != null ? allData.Where(x => x.Sr_No == Sr).FirstOrDefault() : new Return_GetProjectEstimation();
            var ID = dataToDelete.ID;
            if (allData != null)
            {
                var deleteEstID = new ArrayList();
                if (ID != 0)
                {
                    var deleteItemEst = Delete_ProjEstimation_ID as ArrayList;
                    if (deleteItemEst != null)
                    {
                        deleteItemEst.Add(ID);
                        Delete_ProjEstimation_ID = deleteItemEst;
                    }
                    else
                    {
                        deleteEstID.Add(ID);
                        Delete_ProjEstimation_ID = deleteEstID;
                    }
                }
                allData.Remove(dataToDelete);
            }
            Estimation_Grid_Data_Project = allData;
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStockUnits(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.StockProfile.Return_GetUnits> GetUnits = BASE._Stock_Profile_DBOps.GetUnits();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetUnits, loadOptions)), "application/json");
        }//Mantis 0000297 bug fixed
        public ActionResult Frm_Export_Options_Estimation()
        {
            return View();
        }
        #endregion
        #region Remarks
        public ActionResult ExistingRemarksGridData(int ProjectID = 0)
        {
            if (Remarks_Grid_Data_project == null)
            {
                List<Return_GetProjectRemarks> RemarksData = BASE._Projects_Dbops.GetProjectRemarks(ProjectID);
                Remarks_Grid_Data_project = RemarksData;
            }
            if (((List<Return_GetProjectRemarks>)Remarks_Grid_Data_project).Count > 0)
            {
                for (int i = 0; i < ((List<Return_GetProjectRemarks>)Remarks_Grid_Data_project).Count; i++)
                {
                    ((List<Return_GetProjectRemarks>)Remarks_Grid_Data_project)[i].Sr_No = i + 1;
                }
            }
            return PartialView(Remarks_Grid_Data_project);
        }
        public ActionResult Frm_ViewRemarks(int SR = 0)
        {
            var all_data = (List<Return_GetProjectRemarks>)Remarks_Grid_Data_project;
            var dataToView = all_data != null ? all_data.Where(x => x.Sr_No == SR).FirstOrDefault() : new Return_GetProjectRemarks();
            ViewBag.ViewRemarks = dataToView.Remarks;
            return PartialView("Frm_ViewRemarks", ViewBag.ViewRemarks);
        }
        public ActionResult Frm_ExistingRemarks_Window_Delete_Grid_Record(string ActionMethod, int? SrID = null)
        {

            var Sr = Convert.ToInt16(SrID);
            var allData = (List<Return_GetProjectRemarks>)Remarks_Grid_Data_project;
            var dataToDelete = allData != null ? allData.Where(x => x.ID == Sr).FirstOrDefault() : new Return_GetProjectRemarks();
            if (BASE._open_User_ID != dataToDelete.Remarks_By)//Mantis bug 0000218 fixed
            {
                return Json(new { result = false, message = "A User Is Allowed To Delete His Own Remarks Only...!! " }, JsonRequestBehavior.AllowGet);
            }
            var id = dataToDelete.ID;
            if (id != 0)
            {
                allData.Remove(dataToDelete);
            }
            Remarks_Grid_Data_project = allData;
            var deleteRemarksID = new ArrayList();
            var deleteExistingRemarks = Delete_Remarks_ID_Project as ArrayList;
            if (deleteExistingRemarks != null)
            {
                deleteExistingRemarks.Add(id);
                Delete_Remarks_ID_Project = deleteExistingRemarks;
            }
            else
            {
                deleteRemarksID.Add(id);
                Delete_Remarks_ID_Project = deleteRemarksID;
            }
            return Json(new
            {
                result = true,
                message = ""
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion                                 
        #region Status
        public ActionResult StatusFlowCheck(string ActionMethod = null, int REC_ID = 0)
        {
            var _CurrUserRole = BASE._Projects_Dbops.GetRegister(Convert.ToDateTime(PRFromDate), Convert.ToDateTime(PRToDate)).FindAll(x => x.ID == REC_ID).FirstOrDefault().CurrUserRole;
            string[] CurrUserRole = new string[] { "" };
            if (_CurrUserRole != null) { CurrUserRole = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }
            ProjectData = BASE._Projects_Dbops.GetRecord(REC_ID);
            var Status = ProjectData[0].Proj_Status;
            string[] PossibleStatus = new string[] { "", "New", "Cancelled", "Rejected", "Changes Recommended", "Requested", "Assigned for Estimation Creation", "Submitted for Estimate Approval", "Approved", "Assigned", "In-Progress", "Completed" };
            string[] AllowedIfStatus;
            var msg = "";
            switch (ActionMethod)
            {
                case "Cancel":
                    if (CurrUserRole.Contains("Requestor"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[1], PossibleStatus[4], PossibleStatus[5], PossibleStatus[8] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor' Cannot Cancel The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else
                    {
                        msg = "Only Requestor Can Cancel The Project..!!";
                    }
                    break;
                case "Reopen":
                    if (CurrUserRole.Contains("Requestor"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[2], PossibleStatus[4] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor' Cannot Re-open The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[3] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Dept Main Incharge' Cannot Re-open The Project If Project Status Is " + Status + " ..!!";
                        }
                    }//Mantis bug 0000332 fixed
                    if (CurrUserRole.Contains("Approver") || CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        if (Status == PossibleStatus[11])
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Approver' Or 'Assignee Main Dept Incharge' Cannot Re-open The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else if (!(CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Dept Main Incharge")))
                    {
                        msg = "Only 'Requestor','Requestor Dept Main Incharge','Approver','Assignee Main Dept Incharge' Can Re-open The Project..!!";
                    }//Mantis bug 0000332 fixed
                    break;
                case "Reject":
                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[1], PossibleStatus[4], PossibleStatus[5] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Main Dept Incharge' Cannot Reject The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Approver"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[5], PossibleStatus[6], PossibleStatus[7], PossibleStatus[8] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Approver' Cannot Reject The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else if (!CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        msg = "Only 'Requestor Main Dept Incharge','Approver' Can Reject The Project..!!";
                    }
                    break;
                case "Changes Recommended":
                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[1], PossibleStatus[3], PossibleStatus[5] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Main Dept Incharge' Cannot Recommend Changes If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Approver"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[3], PossibleStatus[5], PossibleStatus[6], PossibleStatus[7], PossibleStatus[8] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Approver' Cannot Recommend Changes If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else if (!CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        msg = " Only 'Requestor Main Dept Incharge','Approver' Can Recommend Changes..!!";
                    }
                    break;
                case "Approve":
                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[1], PossibleStatus[3], PossibleStatus[4] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Main Dept Incharge' Cannot Approve The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Approver"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[3], PossibleStatus[4], PossibleStatus[5], PossibleStatus[6], PossibleStatus[7] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Approver' Cannot Approve The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else if (!CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        msg = " Only 'Requestor Main Dept Incharge','Approver' Can Approve The Project..!!";
                    }
                    break;
                case "Estimate Creation":
                    if (CurrUserRole.Contains("Approver"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[3], PossibleStatus[4], PossibleStatus[5], PossibleStatus[7], PossibleStatus[8] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Approver' Cannot Assign For Estimation Creation If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))//Mantis bug 0000429 fixed
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[5], PossibleStatus[7] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Assignee Dept Main Incharge' Cannot Assign For Estimation Creation If Project Status Is " + Status + " ..!!";
                        }
                    }//Mantis bug 0000429 fixed
                    else if (!CurrUserRole.Contains("Approver"))
                    {
                        msg = "Only 'Approver' & 'Assignee Dept Main Incharge' Can Assign For Estimation Creation..!!";
                    }//Mantis bug 0000429 fixed
                    break;
                case "Estimate Approval":
                    if (CurrUserRole.Contains("Estimator"))
                    {
                        if (Status == PossibleStatus[6])
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Estimator' Cannot Approve Estimate If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else
                    {
                        msg = "Only 'Estimator' Can Approve Estimate..!!";
                    }
                    break;
                case "Complete":
                    if (CurrUserRole.Contains("Approver") || CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[8], PossibleStatus[9], PossibleStatus[10] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Assignee Main Dept Incharge' Or 'Approver' Cannot Mark Project As Completed If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else
                    {
                        msg = "Only 'Assignee Main Dept Incharge','Approver' Can Mark Project As Completed..!!";
                    }
                    break;
                case "Edit":
                    if (CurrUserRole.Contains("Requestor"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[1], PossibleStatus[4] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor' Cannot Edit The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[1], PossibleStatus[4], PossibleStatus[5] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Requestor Main Dept Incharge' Cannot Edit The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Approver"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[5], PossibleStatus[7] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Approver' Cannot Edit The Project If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[5], PossibleStatus[6], PossibleStatus[7], PossibleStatus[8], PossibleStatus[9], PossibleStatus[10], PossibleStatus[11] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Assignee Dept Main Incharge' Cannot Edit The Project If Project Status Is " + Status + " ..!!";
                        }
                    }//Mantis bug 0000321 fixed
                    if (CurrUserRole.Contains("Estimator"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[6] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Estimator' Cannot Edit The Project If Project Status Is " + Status + " ..!!";
                        }
                    }//Mantis 0000293 bug fixed
                    else if (!(CurrUserRole.Contains("Requestor") || CurrUserRole.Contains("Requestor Dept Main Incharge") || CurrUserRole.Contains("Approver") || CurrUserRole.Contains("Assignee Dept Main Incharge")))//Mantis bug 0000321 fixed
                    {
                        msg = "Only 'Requestor','Requestor Main Dept Incharge','Approver','Assignee Dept Main Incharge','Estimator' Can Edit The Project..!!";//Mantis bug 0000321 fixed
                    }//Mantis 0000293 bug fixed
                    break;
                case "Delete":
                    if (Status == PossibleStatus[1])
                    {
                        return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "You Cannot Delete The Project If Project Status Is " + Status + " ..!!";
                    }
                    break;
                case "Project Estimation":
                    if (CurrUserRole.Contains("Estimator"))
                    {
                        if (Status == PossibleStatus[6])
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Estimator' Cannot Add/Update/Delete Project Estimation If Project Status Is " + Status + " ..!!";
                        }
                    }
                    if (CurrUserRole.Contains("Approver"))
                    {
                        AllowedIfStatus = new string[] { PossibleStatus[5], PossibleStatus[6], PossibleStatus[7], PossibleStatus[8], PossibleStatus[9], PossibleStatus[10] };
                        if (AllowedIfStatus.Contains(Status))
                        {
                            return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            msg = "'Approver' Cannot Add/Update/Delete Project Estimation If Project Status Is " + Status + " ..!!";
                        }
                    }
                    else if (!CurrUserRole.Contains("Estimator"))
                    {
                        msg = "Only 'Estimator','Approver' Can Add/Update/Delete Project Estimation..!!";
                    }
                    break;
                case "Document":
                    if (Status != PossibleStatus[11])
                    {
                        return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "You Cannot Attach Documents If Project Status Is " + Status + " ..!!";
                    }
                    break;
                case "AddJob"://Mantis bug 0000378 fixed
                case "QuickJob"://Mantis bug 0000378 fixed
                    AllowedIfStatus = new string[] { PossibleStatus[8], PossibleStatus[9], PossibleStatus[10] };
                    if (AllowedIfStatus.Contains(Status))
                    {
                        return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        msg = "You Cannot Add Job To Project <br> If Project Status Is <b>" + Status + "</b> ..!!";//Mantis bug 0000378 fixed
                    }
                    break;
            }
            if (msg.Length > 0)
            {
                return Json(new { message = msg, result = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult PR_StatusChange_Window(int ProjectId, string StatusButton)
        {
            PRUpDateStatus model = new PRUpDateStatus();
            model.PR_ID = ProjectId;
            model.PR_StatusType = StatusButton;
            model.PR_Completion_Date = DateTime.Now;
            return View(model);
        }
        [HttpPost]
        public ActionResult PR_StatusChange_Window(PRUpDateStatus model)
        {
            try
            {
                string msg = "";
                Param_Update_Project_Status param = new Param_Update_Project_Status();
                param.ProjectID = model.PR_ID;
                if (model.PR_StatusType == "Complete")
                {
                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Completed", true);
                    param.ProjectCompletionDate = model.PR_Completion_Date;
                    msg = "Status Changed To Completed..!!";
                }
                else if (model.PR_StatusType == "Cancel")
                {
                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Cancelled", true);
                    msg = "Status Changed To Cancelled..!!";
                }
                else if (model.PR_StatusType == "Reject")
                {
                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Rejected", true);
                    msg = "Status Changed To Rejected..!!";
                }
                else if (model.PR_StatusType == "Changes Recommended")
                {
                    param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Changes_Recommended", true);
                    msg = "Status Changed To Changes Recommended...!!";
                }
                if (BASE._Projects_Dbops.UpdateProjectStatus(param))
                {
                    if (model.PR_StatusType != "Complete")
                    {
                        Param_InsertProjectRemarks Inparam = new Param_InsertProjectRemarks();
                        Inparam.Project_ID = model.PR_ID;
                        Inparam.Remarks = model.PR_Status_Remark;
                        if (BASE._Projects_Dbops.InsertProjectRemarks(Inparam))
                        {
                            return Json(new
                            {
                                result = true,
                                message = msg
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            result = true,
                            message = msg
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    result = true,
                    message = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }//Mantis 0000233 bug fixed
        public ActionResult PR_StatusChange(int ProjectId, string StatusButton)
        {
            string msg = "";
            Param_Update_Project_Status param = new Param_Update_Project_Status();
            param.ProjectID = ProjectId;
            var _CurrUserRole = BASE._Projects_Dbops.GetRegister(Convert.ToDateTime(PRFromDate), Convert.ToDateTime(PRToDate)).FindAll(x => x.ID == ProjectId).FirstOrDefault().CurrUserRole;
            string[] CurrUserRole = new string[] { "" };
            if (_CurrUserRole != null) { CurrUserRole = _CurrUserRole.Split(',').Select(t => t.Trim()).ToArray(); }
            ProjectData = BASE._Projects_Dbops.GetRecord(ProjectId);
            var Status = ProjectData[0].Proj_Status;
            var Proj_Estimator = ProjectData[0].Proj_Estimator_ID;//Mantis bug 0000455 fixed
            try
            {
                switch (StatusButton)
                {
                    case "Reopen":
                        if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                        {
                            if (Status == "Rejected")
                            {
                                param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "_New", true);
                                msg = "Status Changed To 'New'..!!";
                            }
                        }
                        else if (CurrUserRole.Contains("Requestor"))
                        {
                            param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "_New", true);
                            msg = "Status Changed To 'New'..!!";
                        }
                        else if (Status == "Completed")
                        {
                            if (CurrUserRole.Contains("Approver"))
                            {
                                param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "In_Progress", true);
                                msg = "Status Changed To 'InProgress'..!!";
                            }
                            else if (CurrUserRole.Contains("Assignee Dept Main Incharge"))
                            {
                                param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Assigned", true);
                                msg = "Status Changed To 'Assigned'..!!";
                            }
                        }

                        break;
                    case "Estimate Creation":
                        if (Proj_Estimator == null)
                        {
                            return Json(new
                            {
                                result = true,
                                message = "Project Estimator is required to <b>'Assign for Estimation Creation'</b>,<br> Kindly Edit the project and add Project Estimator"
                            }, JsonRequestBehavior.AllowGet);
                        }//Mantis bug 0000455 fixed
                        param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Assigned_for_Estimation_Creation", true);
                        msg = "Status Changed To 'Assigned For Estimation Creation'..!!";
                        break;
                    case "Estimate Approval":
                        param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Submitted_for_Estimate_Approval", true);
                        msg = "Status Changed To 'Submitted for Estimate Approval'..!!";
                        break;
                    case "Approve":

                        if (CurrUserRole.Contains("Requestor Dept Main Incharge"))
                        {
                            param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Requested", true);
                            msg = "Status Changed To 'Requested'..!!";
                        }
                        if (CurrUserRole.Contains("Approver"))
                        {
                            param.UpdatedStatus = (Project_Status)Enum.Parse(typeof(Project_Status), "Approved", true);
                            msg = "Status Changed To 'Approved'..!!";
                        }
                        break;
                }
                if (BASE._Projects_Dbops.UpdateProjectStatus(param))
                {
                    return Json(new
                    {
                        result = true,
                        message = msg
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        result = false,
                        message = Messages.SomeError
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }//Mantis 0000233 bug fixed

        #endregion
        #region Misc
        public void Sessionclear()
        {
            Session.Remove("PRDocument");
            BASE._SessionDictionary.Remove("Frm_Project_Document_AttachmentData");
            BASE._SessionDictionary.Remove("PRDocument_AttachmentData");
            ClearBaseSession("_Proj");
        } //clears session variable on popup close
        public void InfoSessionclear()
        {
            ClearBaseSession("_ProjInfo");
        }
        
        public void Project_user_rights()
        {            
            ViewData["Project_AddRight"] = CheckRights(BASE, ClientScreen.Stock_Project, "Add");
            ViewData["Project_UpdateRight"] = CheckRights(BASE, ClientScreen.Stock_Project, "Update");
            ViewData["Project_ViewRight"] = CheckRights(BASE, ClientScreen.Stock_Project, "View");
            ViewData["Project_DeleteRight"] = CheckRights(BASE, ClientScreen.Stock_Project, "Delete");
            ViewData["Project_ExportRight"] = CheckRights(BASE, ClientScreen.Stock_Project, "Export");
            ViewData["Project_ApproveRight"] = CheckRights(BASE, ClientScreen.Stock_Project, "Approve");
            ViewData["Project_AddDeptRight"] = CheckRights(BASE, ClientScreen.Stock_Dept_Store_Master, "Add");            
            ViewData["Project_AddPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "Add");
            ViewData["Project_ViewPersonnelRight"] = CheckRights(BASE, ClientScreen.Stock_Personnel_Master, "View");
            ViewData["Project_AddJobRight"] = CheckRights(BASE, ClientScreen.Stock_Job, "Add");
            ViewData["Project_AddHelpAttachmentRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
        }
        #endregion
    }
}