using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Facility.Models;
//using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Utils.Extensions;
using DevExpress.Web.Mvc;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations.Attachments;
using static Common_Lib.DbOperations.ServiceProject;
using static Common_Lib.DbOperations.Notifications;
//using FirebaseAdmin;
//using FirebaseAdmin.Messaging;
//using Google.Apis.Auth.OAuth2;
using System.Net;
using System.Text;
using DevExpress.Data.Extensions;
using System.Web.Configuration;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class SparcProjectController : BaseController
    {
        public ActionResult GetForms( string Project_Id = null )
        {
            DataTable dt = BASE._SerProj_DBOps.Get_Forms_For_DD(Project_Id);
            return Content(JsonConvert.SerializeObject(dt), "application/json");
        }
        public ActionResult GetProjects()
        {
            DataTable dt = BASE._SerProj_DBOps.Get_ServiceProjects_For_DD();
            return Content(JsonConvert.SerializeObject(dt), "application/json");
        }
        public ActionResult GetSchedules()
        {
            DataTable d1 = BASE._Schedule_DBOps.Get_Schedules(true);
            return Content(JsonConvert.SerializeObject(d1), "application/json");
        }
        public ActionResult GetSubjectsUsingProjectOrForm(string Project_Id=null, string Form_ID=null)
        {
            DataTable d1 = null;
            if (string.IsNullOrWhiteSpace(Project_Id)==false && string.IsNullOrWhiteSpace(Form_ID))
            {
                 d1 = BASE._SerProj_DBOps.Get_Subjects_From_Project_Form_DD(Project_Id);
            }
            else if (!string.IsNullOrWhiteSpace(Form_ID))
            {
                d1 = BASE._SerProj_DBOps.Get_Subjects_From_Project_Form_DD(null, Convert.ToInt32(Form_ID));
            }
            return Content(JsonConvert.SerializeObject(d1), "application/json");
        }
        public ActionResult Frm_Sparc_Project_Info()
        {
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_SparcProject).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View();
        }
        public void SparcProject_UserRights()
        {
            ViewData["Sparc_Project_ExportRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_SparcProject, "Export");
            ViewData["Sparc_Project_ListRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_SparcProject, "List");
            ViewData["Sparc_Project_AddRight"] = CheckRights(BASE, ClientScreen.Facility_SparcProject, "Add");
            ViewData["Sparc_Project_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_SparcProject, "Update");
            ViewData["Sparc_Project_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_SparcProject, "Delete");
            ViewData["Sparc_Project_ReportRight"] = CheckRights(BASE, ClientScreen.Facility_SparcProject, "Report");
        }
        public void SessionClear()
        {
            ClearBaseSession("_SparcProj");
            //Baba: To Be Changed
            //Session.Remove("SparcReportInfo_DetailGrid_Data");
        }
        public void SessionClear_Window()
        {
            //Baba: To be Checked
            ClearBaseSession("_SparcProj_Window");
        }
        [HttpGet]
        public ActionResult Frm_MapFormtoProject()
        {
            SparcMappingModel model = new SparcMappingModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_MapFormtoProject(string ProjectIds, string ProjectNames, string FormId, string FormName, string FormProjectId)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            jsonParam.message = "";
            bool result = false;
            
            string[] ProjectIdArr = ProjectIds.Split(',');
            string[] ProjectNameArr = ProjectNames.Split(',');
            StringBuilder[] projNameArr = new StringBuilder[ProjectNameArr.Length];

            for (int i = 0; i < ProjectNameArr.Length; i++)
            {
                projNameArr[i] = new StringBuilder(ProjectNameArr[i]);
            }
            string successMessage ="";
            string failureMessage = "";
            string frmName = FormName.Split('(')[0];
            StringBuilder formName = new StringBuilder(frmName);
            for (int i = 0; i < projNameArr.Length; i++)
            {
                 
                try
                {
                    formName.Append("(").Append(projNameArr[i]).Append(")");
                    if (BASE._Form_dbops.CheckIfChartNameIsUniqueInUID(formName.ToString(), FormId) && (!FormProjectId.Equals(ProjectIdArr[i])))
                    {
                        result = BASE._SerProj_DBOps.UpdateChartProject(Convert.ToInt32(FormId), ProjectIdArr[i], formName.ToString());
                        jsonParam.result = result;
                        if (!result)
                        {
                            //failureMessage = failureMessage + " Failed to Map Form  - <b>" + formName.ToString() + "</b>  <b>" + projNameArr[i] + "</b>!!!<br>";
                            failureMessage = failureMessage + " Failed to Map Form  - <b>" + frmName + "</b>  <b>" + projNameArr[i] + "</b>!!!<br>";
                            jsonParam.title = "Mapping Failed...";
                            jsonParam.message = failureMessage + successMessage;
                        }
                        else
                        {
                            successMessage = successMessage + " Form -<b> " + frmName + " </b> to Project - <b>" + ProjectNameArr[i] + " </b> Mapped Successfully!!!<br>";
                            jsonParam.title = "Mapping Success...";
                        }
                    }
                    else
                    {
                        jsonParam.result = false;
                        failureMessage = failureMessage + " Form - <b>" + frmName + "</b> already mapped to Project - <b>" + projNameArr[i] + "</b>!!!<br>";
                        jsonParam.title = "Mapping Failed...";
                    }
                    formName = new StringBuilder(frmName);
                    jsonParam.message = failureMessage + successMessage;
                }
                catch (Exception e)
                {
                    jsonParam.title = "Mapping Failed...";
                    jsonParam.message = failureMessage + "<br>"+e.Message;
                }
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_MapFormtoSchedule()
        {
            SparcMappingModel model = new SparcMappingModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_MapFormtoSchedule(string ScheduleId, string FormIds, string FormStartDates, string FormEndDates, string FormsFrequencysPrevious/*, string InstancesCounts, string ResponsesCounts*/)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            bool result = false;
            string[] FormIdArr = FormIds.Split(',');
            string[] FormStartDatesArr = FormStartDates.Split(',');
            string[] FormEndDatesArr = FormEndDates.Split(',');
            string[] FormFrequencyPreviousArr = FormsFrequencysPrevious.Split(',');
            for (int i = 0; i < FormIdArr.Length; i++)
            {
                DateTime? startDate = null; // Nullable DateTime
                DateTime? endDate = null; // Nullable DateTime
                if (!string.IsNullOrWhiteSpace(FormStartDatesArr[i]))
                {
                    startDate = Convert.ToDateTime(FormStartDatesArr[i]);
                }
                if (!string.IsNullOrWhiteSpace(FormEndDatesArr[i]))
                {
                    endDate = Convert.ToDateTime(FormEndDatesArr[i]);
                }
                try
                {
                    result = BASE._SerProj_DBOps.UpdateFormFrequency(Convert.ToInt32(FormIdArr[i]), startDate, endDate, endDate, FormFrequencyPreviousArr[i], Convert.ToInt32(ScheduleId));
                    jsonParam.result = result;
                    if (!result)
                    {
                        jsonParam.message = "Failed to Map!!!";
                        jsonParam.title = "Mapping Failed...";
                        break;
                    }
                    else
                    {
                        jsonParam.message = "Form to Schedule Mapped Successfully!!!";
                        jsonParam.title = "Mapping Success...";
                    }
                }
                catch (Exception ex)
                {
                    jsonParam.result = false;
                    jsonParam.message = ex.Message;
                    jsonParam.title = "Mapping Failed...";
                }
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
    }
}