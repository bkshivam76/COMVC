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
    public class ServiceProjectController : BaseController
    {
        public DataTable dt_projectInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_projectInfoGrid_ServiceProj"); }
            set { SetBaseSession("dt_projectInfoGrid_ServiceProj", value); }
        }
        public DataTable dt_project_Users_Grid
        {
            get { return (DataTable)GetBaseSession("dt_projectUsersGrid_ServiceProj"); }
            set { SetBaseSession("dt_projectUsersGrid_ServiceProj", value); }
        }
        public DataTable dt_project_MappedSubject_Grid
        {
            get { return (DataTable)GetBaseSession("dt_project_MappedSubject_Grid_ServiceProj"); }
            set { SetBaseSession("dt_project_MappedSubject_Grid_ServiceProj", value); }
        }


        public DataTable dt_subjectMappingInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_subjectMappingInfoGrid_ServiceProj"); }
            set { SetBaseSession("dt_subjectMappingInfoGrid_ServiceProj", value); }
        }
        public DataTable dt_allResponsesInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_allResponsesInfoGrid_ServiceProj"); }
            set { SetBaseSession("dt_allResponsesInfoGrid_ServiceProj", value); }
        }
        public DataTable dt_allResponsesTimeInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_allResponsesTimeInfoGrid_ServiceProj"); }
            set { SetBaseSession("dt_allResponsesTimeInfoGrid_ServiceProj", value); }
        }

        public string SP_Session_Project_ID
        {
            get { return (string)GetBaseSession("SP_Session_Project_ID_ServiceProj"); }
            set { SetBaseSession("SP_Session_Project_ID_ServiceProj", value); }
        }

        #region Service_Proj_Info_Listing
        public ActionResult Frm_Service_Project_Info(string Screen=null)
        {
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ServiceProject).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            projectInfoGridData();
            ViewBag.Screen = Screen;
            ViewBag.CenterId = BASE._open_Cen_ID;
            ViewBag.FY = BASE._open_Year_ID;
            ViewBag.UserABId = null;
            ViewBag.UserId = 0;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();            

            return View(dt_projectInfoGrid);
        }
        public ActionResult Frm_Service_Project_Info_Grid(string command, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string Screen ="")
        {
            if (command == "REFRESH" || dt_projectInfoGrid == null)
            {
                projectInfoGridData();
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Screen = Screen;

            return View(dt_projectInfoGrid);
        }
        public ActionResult Frm_Service_Project_Info_DetailGrid(string ProjID, string command, int ShowHorizontalBar = 0, bool isNested = true, bool MultipleSelection = false, string Screen = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ProjID = ProjID;
            ViewBag.isNested = isNested;
            ViewBag.MultipleSelection = MultipleSelection;
            ViewBag.Screen = Screen;
            if (command == "REFRESH")
            {
                dt_project_Users_Grid = BASE._SerProj_DBOps.get_project_users_Info(ProjID);
            }
            return PartialView(dt_project_Users_Grid);
        }
        public ActionResult Frm_Service_Project_Mapped_Subject_Grid(string ProjID, string command, int ShowHorizontalBar = 0, bool isNested = true, bool MultipleSelection = false, string Screen = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ProjID = ProjID;
            ViewBag.isNested = isNested;
            ViewBag.MultipleSelection = MultipleSelection;
            ViewBag.Screen = Screen;
            if (command == "REFRESH")
            {
                dt_project_MappedSubject_Grid = BASE._SerProj_DBOps.get_project_users_Info(ProjID);
            }
            return PartialView("Frm_Service_Project_Info_DetailGrid", dt_project_MappedSubject_Grid);
        }
        public void projectInfoGridData()
        {
            dt_projectInfoGrid = BASE._SerProj_DBOps.get_projectInfo();
        }
        public ActionResult Frm_ServiceProject_MappedSubjects(string ProjectID, string ProjName, string PopupID = "", string Screen = "")
        {
            ViewBag.MultipleSelection = Screen == "SEND_NOTIFICATION" || Screen == "SCHEDULE_NOTIFICATION" ? true : false;
            ViewBag.isNested = false;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.ProjID = ProjectID;
            ViewBag.ProjName = ProjName;
            ViewBag.PopupID = string.IsNullOrWhiteSpace(PopupID) ? "Dynamic_Content_popup" : PopupID;
            ViewBag.Screen = Screen;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            dt_project_MappedSubject_Grid = BASE._SerProj_DBOps.get_project_users_Info(ProjectID);
            return View("Frm_ServiceProject_MappedSubjects", dt_project_MappedSubject_Grid);
        }
        public ActionResult Frm_Export_Options_MappedSubject()
        {
            return PartialView();
        }
        public ActionResult Frm_Export_Options()
        {
            return PartialView();
        }

        #endregion Service_Proj_Info_Listing

        #region Subject_Mapping

        public ActionResult Frm_Subject_Mapping_Info(string Project_ID, string Project_Name = "", string RegChartID = "", string Screen=null)
        {

            SP_Session_Project_ID = Project_ID;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Project_Name = Project_Name;
            ViewBag.RegChartID = RegChartID;
            ViewBag.Screen = Screen;

            subjectMappingInfoGridData(SP_Session_Project_ID);

            // int[] AlreadyMapped_UserIDs = dt_subjectMappingInfoGrid.AsEnumerable().Where(s => s.Field<string>("IS_MAPPED") == "ALREADY MAPPED").Select(s => s.Field<int>("USER_ID")).ToArray();
            string AlreadyMapped_UserIDs = String.Join("|", dt_subjectMappingInfoGrid.AsEnumerable().Where(s => s.Field<string>("IS_MAPPED") == "ALREADY MAPPED").Select(s => s.Field<int>("USER_ID")));
            ViewBag.AlreadyMapped_UserIDs = AlreadyMapped_UserIDs;

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View(dt_subjectMappingInfoGrid);
        }

        public ActionResult Frm_Subject_Mapping_Info_Grid(string command, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string Screen= null)
        {
            if (command == "REFRESH" || dt_subjectMappingInfoGrid == null)
            {
                subjectMappingInfoGridData(SP_Session_Project_ID);
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Screen = Screen;

            return View(dt_subjectMappingInfoGrid);
        }
        public void subjectMappingInfoGridData(string Project_ID)
        {
            dt_subjectMappingInfoGrid = BASE._SerProj_DBOps.get_subjectsForMapping(Project_ID);
        }
        public ActionResult Frm_Export_Options_SubjectMapping()
        {
            return PartialView();
        }

        public ActionResult Save_MapSubjectsToProject(string selectedUserIDs, string Project_ID, string RegChartID, string AlreadyMapped_Prev)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(SP_Session_Project_ID))
                {
                    jsonParam.message = "Project ID is Not Available";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                //if (string.IsNullOrWhiteSpace(selectedUserIDs))
                //{
                //    jsonParam.message = "User IDs are not Available";
                //    jsonParam.title = "Incomplete Information...";
                //    jsonParam.result = false;
                //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                //}

                string[] Array_UserIds = selectedUserIDs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                DataTable d1 = BASE._SerProj_DBOps.GetMappedUsersWithChartResponsesForProject(SP_Session_Project_ID, RegChartID);
                if (d1 != null && d1.Rows.Count > 0)
                {
                    for (int i = 0; i < d1.Rows.Count; i++)
                    {
                        if (Array_UserIds.Contains(d1.Rows[i]["CR_SERV_USER_ID"].ToString()) == false)
                        {
                            jsonParam.message = "User With ID " + d1.Rows[i]["CR_SERV_USER_ID"].ToString() + " Cannot Be UnMapped.<br> User Has Responses Against Form Linked With The Project";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                d1 = BASE._SerProj_DBOps.GetMappedUsersWithChartForProject(SP_Session_Project_ID);
                if (d1 != null && d1.Rows.Count > 0)
                {
                    for (int i = 0; i < d1.Rows.Count; i++)
                    {
                        if (Array_UserIds.Contains(d1.Rows[i]["SUCM_SERVICE_USER_ID"].ToString()) == false)
                        {
                            jsonParam.message = "User With ID " + d1.Rows[i]["SUCM_SERVICE_USER_ID"].ToString() + " Cannot Be UnMapped.<br> User Has Been Mapped To Form (" + d1.Rows[i]["CI_CHARTNAME"].ToString() + ") Linked With The Project";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (Array_UserIds.Count() == 0) //to unmap all mapped users
                {
                    if (BASE._SerProj_DBOps.Delete_subjectsMappedToProject(SP_Session_Project_ID))
                    {
                        jsonParam.message = "All Users Have Been Unmapped From The Project Successfully";
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (BASE._SerProj_DBOps.insert_subjectMappingToProject(selectedUserIDs, SP_Session_Project_ID))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Success..";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = true;
                        jsonParam.closeform = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                //if (BASE._SerProj_DBOps.Delete_subjectsMappedToProject(SP_Session_Project_ID))
                //{
                //    if (Array_UserIds != null && Array_UserIds.Length > 0)
                //    {
                //        for (int i = 0; i < Array_UserIds.Length; i++)
                //        {
                //            BASE._SerProj_DBOps.insert_subjectMappingToProject(Array_UserIds[i], SP_Session_Project_ID);
                //        }
                //    }
                //    jsonParam.message = Messages.SaveSuccess;
                //    jsonParam.title = "Success..";
                //    jsonParam.result = true;
                //    jsonParam.closeform = true;
                //    return Json(new
                //    {
                //        jsonParam
                //    }, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                //    jsonParam.message = Messages.SomeError;
                //    jsonParam.title = "Error..";
                //    jsonParam.result = true;
                //    jsonParam.closeform = true;
                //    return Json(new
                //    {
                //        jsonParam
                //    }, JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Subject_Mapping
        #region project creation
        [HttpGet]
        public ActionResult Frm_ServiceProject_Window(string Tag = "_New", string ProjectID = "", string screen="")
        {
            ServiceProject model = new ServiceProject();
            model.Status_ServiceProject = true;
            model.Rec_ID = ProjectID;
            ViewBag.Screen = screen;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag.StartsWith("_") ? Tag : "_" + Tag);
            model.TempActionMethod = model.Tag.ToString();
            if (Tag != "_New")
            {
                DataTable d1 = BASE._SerProj_DBOps.GetServiceProjectRecord(ProjectID);
                if (d1 != null && d1.Rows.Count > 0)
                {
                    model.Project_ServiceProject = d1.Rows[0]["SP_PROJ_NAME"].ToString();
                    if (Convert.IsDBNull(d1.Rows[0]["SP_PROJ_FR_DATE"]) == false)
                    {
                        model.FromDate_ServiceProject = Convert.ToDateTime(d1.Rows[0]["SP_PROJ_FR_DATE"]);
                    }
                    if (Convert.IsDBNull(d1.Rows[0]["SP_PROJ_TO_DATE"]) == false)
                    {
                        model.ToDate_ServiceProject = Convert.ToDateTime(d1.Rows[0]["SP_PROJ_TO_DATE"]);
                    }
                    model.AdminID_ServiceProject = d1.Rows[0]["SP_PROJ_ADMIN"].ToString();
                    model.Status_ServiceProject = Convert.ToBoolean(d1.Rows[0]["SP_PROJ_STATE"]);
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_ServiceProject_Window(ServiceProject model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit)
                {
                    if (BASE._SerProj_DBOps.CheckIfProjectNameIsUniqueInUID(model.Project_ServiceProject, model.Rec_ID) == false)
                    {
                        jsonParam.message = "Project Name Must Be Unique Across The UID";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "Project_ServiceProject";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Tag == Common.Navigation_Mode._Delete)
                {
                    DataTable d1 = BASE._SerProj_DBOps.GetServiceProjectLinked(model.Rec_ID);
                    if (d1 != null && d1.Rows.Count > 0)
                    {
                        jsonParam.message = "Project Name Linked With Service Report or Event or Service Forms Cannot Be Deleted<br><b>Linked With " + d1.Rows[0]["Type"].ToString() + ": </b>" + d1.Rows[0]["Name"].ToString();
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Tag == Common.Navigation_Mode._New)
                {
                    Param_Insert_Service_Project Inparam = new Param_Insert_Service_Project();
                    Inparam.Admin = model.AdminID_ServiceProject;
                    if (CommonFunctions.IsDate(model.FromDate_ServiceProject))
                    {
                        Inparam.FromDate = Convert.ToDateTime(model.FromDate_ServiceProject).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (CommonFunctions.IsDate(model.ToDate_ServiceProject))
                    {
                        Inparam.ToDate = Convert.ToDateTime(model.ToDate_ServiceProject).ToString(BASE._Server_Date_Format_Long);
                    }
                    Inparam.ProjName = model.Project_ServiceProject;
                    Inparam.Rec_ID = Guid.NewGuid().ToString();
                    Inparam.Status = model.Status_ServiceProject;
                    if (BASE._SerProj_DBOps.InsertServiceProject(Inparam))
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Information...";
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (model.Tag == Common.Navigation_Mode._Edit)
                {
                    Param_Update_Service_Project UPparam = new Param_Update_Service_Project();
                    UPparam.Admin = model.AdminID_ServiceProject;
                    if (CommonFunctions.IsDate(model.FromDate_ServiceProject))
                    {
                        UPparam.FromDate = Convert.ToDateTime(model.FromDate_ServiceProject).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (CommonFunctions.IsDate(model.ToDate_ServiceProject))
                    {
                        UPparam.ToDate = Convert.ToDateTime(model.ToDate_ServiceProject).ToString(BASE._Server_Date_Format_Long);
                    }
                    UPparam.ProjName = model.Project_ServiceProject;
                    UPparam.Rec_ID = model.Rec_ID;
                    UPparam.Status = model.Status_ServiceProject;
                    if (BASE._SerProj_DBOps.UpdateServiceProject(UPparam))
                    {
                        jsonParam.message = Messages.UpdateSuccess;
                        jsonParam.title = "Information...";
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (model.Tag == Common.Navigation_Mode._Delete)
                {
                    if (BASE._SerProj_DBOps.DeleteServiceProject(model.Rec_ID))
                    {
                        jsonParam.message = Messages.DeleteSuccess;
                        jsonParam.title = "Information...";
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Information...";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult LookUp_Get_AdminList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._SerProj_DBOps.GetProjectAdmins();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Name_ID(d1), loadOptions)), "application/json");
        }

        public ActionResult DeleteFullServiceProject(string Project_ID, string Project_Name)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE._SerProj_DBOps.DeleteFullServiceProject(Project_ID))
                {
                    jsonParam.message = "The project " + Project_Name  + " is successfully deleted.";
                    jsonParam.title = "Success!!";
                    jsonParam.result = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.message = Common_Lib.Messages.SomeError;
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Notification
        public ActionResult Frm_Project_Notification(string ProjectID, string ProjName, string SendNowOrLater=null)
        {
            ProjectNotification model = new ProjectNotification();
            model.ProjID = ProjectID;
            model.ProjName = ProjName;
            ViewBag.Screen = SendNowOrLater;
            return View(model);
        }
        public ActionResult Send_Notification(ProjectNotification model)
        {            
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(model.Title_ProjectNotification))
                {
                    jsonParam.message = "Title cannot be Blank !!!";
                    jsonParam.title = "Empty field!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.SubTitle_ProjectNotification))
                {
                    jsonParam.message = "Sub-Tilte cannot be Blank !!!";
                    jsonParam.title = "Empty field!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.ProjID))
                {
                    jsonParam.message = "Project ID cannot be Blank !!!";
                    jsonParam.title = "Empty field!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.SelectedUserRecID))
                {
                    jsonParam.message = "Please select atleast one user!!!";
                    jsonParam.title = "Invalid field data!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                Param_Insert_Notification Inparam = new Param_Insert_Notification();

                Inparam.Title = model.Title_ProjectNotification;
                Inparam.SubTitle = model.SubTitle_ProjectNotification;
                Inparam.URL = model.URL_ProjectNotification;
                Inparam.ImageURL = model.ImageURL_ProjectNotification;
                Inparam.ProjectID = model.ProjID;
                Inparam.ToUsers = model.SelectedUserRecID;
                
                string notificationID;

                DataTable dt_notificationID = BASE._SerProj_DBOps.InsertNotification(Inparam);
                if (dt_notificationID != null && dt_notificationID.Rows.Count > 0)
                {
                    notificationID = (dt_notificationID.Rows[0][0]).ToString();
                }
                else
                {
                    jsonParam.message = "Notification not Saved. Please Try Again !!";
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                int logN_Counter = 0;
                string logN_NotificationID = "";
                string logN_UserID = "";
                string logN_AndroidID = "";
                string logN_DeviceToken = "";
                string logN_Status = "";
                string logN_Remarks = "";
                string logN_SentTime = "";
                string logN_Rec_Add_By = "";

                Response_AndroidNotificationAPI response = new Response_AndroidNotificationAPI();

                DataTable dt_androidTokens = BASE._SerProj_DBOps.get_DeviceTokenOfUsers(model.SelectedUserRecID);

                if(dt_androidTokens != null && dt_androidTokens.Rows.Count > 0)
                {
                    for (int j = 0; j < dt_androidTokens.Rows.Count; j++)
                    {
                        response = sendPushNotification(dt_androidTokens.Rows[j]["DEVICE_TOKEN"].ToString(), model.URL_ProjectNotification, model.Title_ProjectNotification, model.SubTitle_ProjectNotification, model.ImageURL_ProjectNotification);

                        logN_Counter++;
                        logN_NotificationID = string.IsNullOrWhiteSpace(logN_NotificationID) ? notificationID : logN_NotificationID + "|" + notificationID;
                        logN_UserID = string.IsNullOrWhiteSpace(logN_UserID) ? dt_androidTokens.Rows[j]["USER_REC_ID"].ToString() : logN_UserID + "|" + dt_androidTokens.Rows[j]["USER_REC_ID"].ToString();
                        logN_AndroidID = string.IsNullOrWhiteSpace(logN_AndroidID) ? dt_androidTokens.Rows[j]["ANDROID_ID"].ToString() : logN_AndroidID + "|" + dt_androidTokens.Rows[j]["ANDROID_ID"].ToString();
                        logN_DeviceToken = string.IsNullOrWhiteSpace(logN_DeviceToken) ? dt_androidTokens.Rows[j]["DEVICE_TOKEN"].ToString() : logN_DeviceToken + "|" + dt_androidTokens.Rows[j]["DEVICE_TOKEN"].ToString();
                        logN_Status = string.IsNullOrWhiteSpace(logN_Status) ? response.status : logN_Status + "|" + response.status;
                        logN_Remarks = string.IsNullOrWhiteSpace(logN_Remarks) ? response.remarks : logN_Remarks + "|" + response.remarks;
                        logN_SentTime = string.IsNullOrWhiteSpace(logN_SentTime) ? DateTime.Now.ToString(BASE._Server_Date_Format_Long) : logN_SentTime + "|" + DateTime.Now.ToString(BASE._Server_Date_Format_Long);
                        logN_Rec_Add_By = string.IsNullOrWhiteSpace(logN_Rec_Add_By) ? BASE._open_User_ID : logN_Rec_Add_By + "|" + BASE._open_User_ID;

                        if (logN_Counter == 45 || (j + 1) == dt_androidTokens.Rows.Count)
                        {

                            bool androidNotificationLog = BASE._AndroidDBOps.insertAndroidNotificationLog(logN_NotificationID, logN_UserID, logN_AndroidID, logN_DeviceToken, logN_Status, logN_Remarks, logN_SentTime, logN_Rec_Add_By);

                            logN_Counter = 0;
                            logN_NotificationID = "";
                            logN_UserID = "";
                            logN_AndroidID = "";
                            logN_DeviceToken = "";
                            logN_Status = "";
                            logN_Remarks = "";
                            logN_SentTime = "";
                            logN_Rec_Add_By = "";
                        }

                    }

                    jsonParam.message = "Notification Sent Succesfully";
                    jsonParam.title = "Information";
                    jsonParam.result = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.message = "Notification is saved!! <br/> <br/> Notification not sent; either no users are registered on Android Application or there is internal Error. <br/> <br/> Please try agian later.";
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Send_ScheduledNotification()
        {
            DataSet ds = BASE._Schedule_DBOps.ExecuteScheduleInstanceQueue();

            DataTable dt_notifications = ds.Tables[0];
            DataTable dt_dayTime = ds.Tables.Count > 1 ? ds.Tables[1] : null;

            string log_Sch_Instance_ID = "";
            string log_Notification_ID = "";
            string log_Job_Type = "";
            string log_Chart_Instance_ID = "";
            string log_Rec_Add_By = "";
            int log_Counter = 0;
            string rec_add_by = string.IsNullOrWhiteSpace(BASE._open_User_ID) ? "BKGRND_JOB" : BASE._open_User_ID;


            if (dt_notifications != null && dt_notifications.Rows.Count > 0)
            {
                for (int i = 0; i < dt_notifications.Rows.Count; i++)
                {
                    string Title = dt_notifications.Rows[i]["TITLE"].ToString();
                    string Subtitle = dt_notifications.Rows[i]["SUBTITLE"].ToString();
                    string URL = dt_notifications.Rows[i]["URL"].ToString();
                    string Image = dt_notifications.Rows[i]["IMAGE"].ToString();
                    string Users = dt_notifications.Rows[i]["USERS"].ToString();
                    string notificationID = dt_notifications.Rows[i]["NOTIFICATION_ID"].ToString();
                    string scheduleInstanceID = dt_notifications.Rows[i]["SCH_INSTANCE_ID"].ToString();
                    bool Is_Instance_Notification = Convert.ToBoolean(dt_notifications.Rows[i]["IS_INSTANCE_NOTIFICATION"]);
                    string serviceModuleDomain = WebConfigurationManager.AppSettings["AndroidServiceModuleDomain"].ToString();
                    string URL_Form = "";

                    int logN_Counter = 0;
                    string logN_NotificationID = "";
                    string logN_UserID = "";
                    string logN_AndroidID = "";
                    string logN_DeviceToken = "";
                    string logN_Status = "";
                    string logN_Remarks = "";
                    string logN_SentTime = "";
                    string logN_Rec_Add_By = "";
                    Response_AndroidNotificationAPI response = new Response_AndroidNotificationAPI();

                    DataTable dt_androidTokens = BASE._SerProj_DBOps.get_DeviceTokenOfUsers(Users);

                    if (dt_androidTokens != null && dt_androidTokens.Rows.Count > 0)
                    {                        
                        for (int j = 0; j < dt_androidTokens.Rows.Count; j++)
                        {
                            if (Is_Instance_Notification)
                            {
                                URL_Form = serviceModuleDomain + URL + dt_androidTokens.Rows[j]["APPEND_USER_INFO_TO_FORM_LINK"].ToString();

                                response = sendPushNotification(dt_androidTokens.Rows[j]["DEVICE_TOKEN"].ToString(), URL_Form, Title, Subtitle, Image);

                                URL_Form = "";
                            }
                            else
                            {
                                response = sendPushNotification(dt_androidTokens.Rows[j]["DEVICE_TOKEN"].ToString(), URL, Title, Subtitle, Image);
                            }

                            logN_Counter++;
                            logN_NotificationID = string.IsNullOrWhiteSpace(logN_NotificationID) ? notificationID : logN_NotificationID + "|" + notificationID;
                            logN_UserID = string.IsNullOrWhiteSpace(logN_UserID)? dt_androidTokens.Rows[j]["USER_REC_ID"].ToString() : logN_UserID + "|" + dt_androidTokens.Rows[j]["USER_REC_ID"].ToString();
                            logN_AndroidID = string.IsNullOrWhiteSpace(logN_AndroidID) ? dt_androidTokens.Rows[j]["ANDROID_ID"].ToString() : logN_AndroidID + "|" + dt_androidTokens.Rows[j]["ANDROID_ID"].ToString();
                            logN_DeviceToken = string.IsNullOrWhiteSpace(logN_DeviceToken) ? dt_androidTokens.Rows[j]["DEVICE_TOKEN"].ToString() : logN_DeviceToken + "|" + dt_androidTokens.Rows[j]["DEVICE_TOKEN"].ToString();
                            logN_Status = string.IsNullOrWhiteSpace(logN_Status) ? response.status : logN_Status + "|" + response.status;
                            logN_Remarks = string.IsNullOrWhiteSpace(logN_Remarks) ? response.remarks : logN_Remarks + "|" + response.remarks;
                            logN_SentTime = string.IsNullOrWhiteSpace(logN_SentTime) ? DateTime.Now.ToString(BASE._Server_Date_Format_Long) : logN_SentTime + "|" + DateTime.Now.ToString(BASE._Server_Date_Format_Long);
                            logN_Rec_Add_By = string.IsNullOrWhiteSpace(logN_Rec_Add_By) ? rec_add_by : logN_Rec_Add_By + "|" + rec_add_by;                            

                            if (logN_Counter == 45 || (j + 1) == dt_androidTokens.Rows.Count)
                            {

                                bool androidNotificationLog = BASE._AndroidDBOps.insertAndroidNotificationLog(logN_NotificationID, logN_UserID, logN_AndroidID, logN_DeviceToken, logN_Status, logN_Remarks, logN_SentTime, logN_Rec_Add_By);

                                logN_Counter = 0;
                                logN_NotificationID = "";
                                logN_UserID = "";
                                logN_AndroidID = "";
                                logN_DeviceToken = "";
                                logN_Status = "";
                                logN_Remarks = "";
                                logN_SentTime = "";
                                logN_Rec_Add_By = "";
                            }

                        }
                    }



                    log_Sch_Instance_ID = string.IsNullOrWhiteSpace(log_Sch_Instance_ID) ? scheduleInstanceID : log_Sch_Instance_ID + "," + scheduleInstanceID;
                    log_Notification_ID = string.IsNullOrWhiteSpace(log_Notification_ID) ? notificationID : log_Notification_ID + "," + notificationID;
                    log_Job_Type = string.IsNullOrWhiteSpace(log_Job_Type) ? "NOTIFICATION" : log_Job_Type + "," + "NOTIFICATION";
                    log_Chart_Instance_ID = string.IsNullOrWhiteSpace(log_Chart_Instance_ID) ? "NULL" : log_Chart_Instance_ID + "," + "NULL";
                    log_Rec_Add_By = string.IsNullOrWhiteSpace(log_Rec_Add_By) ? rec_add_by : log_Rec_Add_By + "," + rec_add_by;
                    log_Counter = log_Counter + 1;

                    if (log_Counter == 200 || (i + 1) == dt_notifications.Rows.Count)
                    {
                        bool notificationLogged = BASE._Schedule_DBOps.InsertScheduleInstanceLog(log_Sch_Instance_ID, log_Notification_ID, log_Job_Type, log_Chart_Instance_ID, log_Rec_Add_By);

                        log_Sch_Instance_ID = "";
                        log_Notification_ID = "";
                        log_Job_Type = "";
                        log_Chart_Instance_ID = "";
                        log_Rec_Add_By = "";
                        log_Counter = 0;
                    }
                }
            }

            if (dt_dayTime != null && dt_dayTime.Rows.Count > 0)
            {
                string resultString = "";
                BASE._Notifications_DBOps.SendSMS(null, "CHART_INSTANCE_SMS", ref resultString, null, null, null, null, Convert.ToDateTime(dt_dayTime.Rows[0]["DAY_TIME"]).ToString(BASE._Server_Date_Format_Long));
            }

            return new EmptyResult();
        }

        public ActionResult emailValidator(string email_ID = "", bool reverify = false)
        {
            // THIS FUNCTION IS CREATED FOR TESTING OF THE EMAIL VAILDATION API.
            Return_EmailValidationStatus status = new Return_EmailValidationStatus();
            status =  BASE._Notifications_DBOps.getEmailValidationStatus_API(email_ID, reverify);

            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save_Map_Notification(ProjectNotification model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(model.Title_ProjectNotification))
                {
                    jsonParam.message = "Title cannot be Blank !!!";
                    jsonParam.title = "Empty field!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.SubTitle_ProjectNotification))
                {
                    jsonParam.message = "Sub-Tilte cannot be Blank !!!";
                    jsonParam.title = "Empty field!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.ProjID))
                {
                    jsonParam.message = "Project ID cannot be Blank !!!";
                    jsonParam.title = "Empty field!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (model.schedule_ID == 0)
                {
                    jsonParam.message = "Schedule ID cannot be Blank/Zero !!!";
                    jsonParam.title = "Invalid field data!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrWhiteSpace(model.SelectedUserRecID))
                {
                    jsonParam.message = "Please select atleast one user!!!";
                    jsonParam.title = "Invalid field data!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                Param_Insert_Notification Inparam = new Param_Insert_Notification();

                Inparam.Title = model.Title_ProjectNotification;
                Inparam.SubTitle = model.SubTitle_ProjectNotification;
                Inparam.URL = model.URL_ProjectNotification;
                Inparam.ImageURL = model.ImageURL_ProjectNotification;
                Inparam.ProjectID = model.ProjID;
                Inparam.ToUsers = model.SelectedUserRecID;

                string message;
                int notificationID;

                DataTable d1 = BASE._SerProj_DBOps.InsertNotification(Inparam);
                if (d1 != null && d1.Rows.Count > 0)
                {
                    notificationID = Convert.ToInt32(d1.Rows[0][0]);
                    message = "Notification Saved Successfully";
                }
                else
                {
                    jsonParam.message = "Notification not Saved. Please Try Again !!";
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                int scheduleInstanceID = 0;
                if (model.FromDate_ScheduleInstance != null && model.ToDate_ScheduleInstance != null)
                {
                    scheduleInstanceID = BASE._Schedule_DBOps.InsertScheduleInstance(model.schedule_ID, Convert.ToDateTime(model.FromDate_ScheduleInstance).ToString(BASE._Server_Date_Format_Short), Convert.ToDateTime(model.ToDate_ScheduleInstance).ToString(BASE._Server_Date_Format_Short));
                }
                else
                {
                    if (model.FromDate_ScheduleInstance == null && model.ToDate_ScheduleInstance == null)
                        scheduleInstanceID = BASE._Schedule_DBOps.InsertScheduleInstance(model.schedule_ID, null, null);
                    else if (model.FromDate_ScheduleInstance == null && model.ToDate_ScheduleInstance != null)
                        scheduleInstanceID = BASE._Schedule_DBOps.InsertScheduleInstance(model.schedule_ID, null, Convert.ToDateTime(model.ToDate_ScheduleInstance).ToString(BASE._Server_Date_Format_Short));
                    else if (model.FromDate_ScheduleInstance != null && model.ToDate_ScheduleInstance == null)
                        scheduleInstanceID = BASE._Schedule_DBOps.InsertScheduleInstance(model.schedule_ID, Convert.ToDateTime(model.FromDate_ScheduleInstance).ToString(BASE._Server_Date_Format_Short), null);
                }

                if (BASE._Schedule_DBOps.InsertMappingToScheduleInstance(scheduleInstanceID, notificationID, "NOTIFICATION"))
                {
                    jsonParam.message = message + " & Mapping of Notification To Scheduler Done.";
                    jsonParam.title = "Information";
                    jsonParam.result = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.message = message + " & Mapping of Notification To Scheduler Not Done. Please try the mapping again.";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LookUp_Get_ScheduleList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Schedule_DBOps.Get_Schedules();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_scheduleList(d1), loadOptions)), "application/json");
        }
        #endregion Notification
        #region All_Responses
        public ActionResult Frm_All_Responses_Info(string Project_ID, string Project_Name = "")
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Project_Name = Project_Name;
            ViewBag.Project_ID = Project_ID;

            allResponsesInfoGridData(Project_ID);

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View(dt_allResponsesInfoGrid);
        }

        public ActionResult Frm_All_Responses_Info_Grid(string ProjectID, string command, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            if (command == "REFRESH" || dt_allResponsesInfoGrid == null)
            {
                allResponsesInfoGridData(ProjectID);
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Project_ID = ProjectID;

            return View(dt_allResponsesInfoGrid);
        }
        public void allResponsesInfoGridData(string Project_ID)
        {
            dt_allResponsesInfoGrid = BASE._SerProj_DBOps.get_allResponsesForProject(Project_ID);
        }
        public ActionResult Frm_Export_Options_AllResponses()
        {
            return PartialView();
        }
        #endregion All_Responses

        #region All_Responses_Time
        public ActionResult Frm_All_Responses_Time_Info(string Project_ID, string Project_Name = "", string Screen = null)
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Project_Name = Project_Name;
            ViewBag.Project_ID = Project_ID;
            
            ViewBag.Screen = Screen;

            allResponsesTimeInfoGridData(Project_ID);

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View(dt_allResponsesTimeInfoGrid);
        }

        public ActionResult Frm_All_Responses_Time_Info_Grid(string ProjectID, string command, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string Form_ID = "", string UserId = "", string User_AB_ID = null)
        {         
            if (command == "REFRESH" || dt_allResponsesInfoGrid == null)
            {
                allResponsesTimeInfoGridData(ProjectID, Form_ID, UserId, User_AB_ID);
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Project_ID = ProjectID;
            ViewBag.Form_ID = Form_ID;
            ViewBag.Subject_ID = UserId;
            return View(dt_allResponsesTimeInfoGrid);
        }
        public void allResponsesTimeInfoGridData(string Project_ID, string Form_ID = "", string UserId = "", string User_AB_ID = null)
        {
            Form_ID = string.IsNullOrWhiteSpace(Form_ID) ? null : Form_ID;
            UserId = string.IsNullOrWhiteSpace(UserId) ? null : UserId;
            User_AB_ID = string.IsNullOrWhiteSpace(User_AB_ID) ? null : User_AB_ID;
            dt_allResponsesTimeInfoGrid = BASE._SerProj_DBOps.get_allResponsesTimeForProject(Project_ID, string.IsNullOrWhiteSpace(Form_ID) ? (int?)null : Convert.ToInt32(Form_ID), string.IsNullOrWhiteSpace(UserId) ? (int?)null : Convert.ToInt32(UserId), User_AB_ID);
        }
        public ActionResult Frm_Export_Options_AllResponsesTime()
        {
            return PartialView();
        }
        #endregion All_Responses_Time
        public void ServiceProject_UserRights()
        {
            ViewData["Service_Project_ExportRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_ServiceProject, "Export");
            ViewData["Service_Project_ListRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Facility_ServiceProject, "List");
            ViewData["Service_Project_AddRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceProject, "Add");
            ViewData["Service_Project_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceProject, "Update");
            ViewData["Service_Project_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceProject, "Delete");
            ViewData["Service_Project_ReportRight"] = CheckRights(BASE, ClientScreen.Facility_ServiceProject, "Report");
        }
        //To Unmap Subjects from Project. First it ill check if any Chart is Mapped with Subject. It will unmap it first then it will unmap Project.
        public JsonResult ServiceProject_DeleteSubject(string ProjectID, string UserAbId, string UserId)
        {
            string[] abId = UserAbId.Split(',');
            string[] userId = UserId.Split(',');
            bool result = false;
            Return_Json_Param jsonParam = new Return_Json_Param();
            for (int i = 0; i < abId.Length; i++)
            {
                try
                {
                    result = BASE._SerProj_DBOps.DeleteSubject(ProjectID, userId[i], abId[i]);
                    jsonParam.result = result;
                    if (!result)
                    {
                        jsonParam.message = "Failed to Delete Subject!!!";
                        jsonParam.title = "Deletion Failed...";
                        break;
                    }
                    jsonParam.message = "Subject Deleted Successfully!!!";
                    jsonParam.title = "Deletion Success...";
                }
                catch (Exception ex)
                {
                    jsonParam.message = ex.Message;
                    jsonParam.title = "Deletion Failed...";
                }
            }            
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public void SessionClear()
        {
            ClearBaseSession("_ServiceProj");
            //Baba: To Be Changed
            //Session.Remove("ServiceReportInfo_DetailGrid_Data");
        }
        public void SessionClear_Window()
        {
            //Baba: To be Checked
            ClearBaseSession("_ServiceProj_Window");
        }


    }
}
