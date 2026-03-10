using System;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Facility.Models;
using ConnectOneMVC.Helper;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using static Common_Lib.DbOperations.Forms;
using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Options.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Net;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;

namespace ConnectOneMVC.Areas.Facility.Controllers
{

    public class ChartController : BaseController
    {
        public DataTable dt_TaskRegisterInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_TaskRegisterInfoGrid_TaskRegister"); }
            set { SetBaseSession("dt_TaskRegisterInfoGrid_TaskRegister", value); }
        }
        public DataTable dt_chartInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartInfoGrid_ChartInfo"); }
            set { SetBaseSession("dt_chartInfoGrid_ChartInfo", value); }
        }

        public DataTable dt_chartResponsesInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponsesInfoGrid_ChartInfo"); }
            set { SetBaseSession("dt_chartResponsesInfoGrid_ChartInfo", value); }
        }
        public DataTable dt_chartResponsesMedicalInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponsesMedicalInfoGrid_ChartInfo"); }
            set { SetBaseSession("dt_chartResponsesMedicalInfoGrid_ChartInfo", value); }
        }
        public DataTable dt_chartResponsesummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartResponsesummaryGrid_ChartInfo"); }
            set { SetBaseSession("dt_chartResponsesummaryGrid_ChartInfo", value); }
        }
        public DataTable dt_chartAccommDetailsGrid
        {
            get { return (DataTable)GetBaseSession("dt_chartAccommDetailsGrid_ChartInfo"); }
            set { SetBaseSession("dt_chartAccommDetailsGrid_ChartInfo", value); }
        }
        
        public DataTable RoomNumbersdt
        {
            get { return (DataTable)GetBaseSession("dt_RoomNumbers_ChartInfo"); }
            set { SetBaseSession("dt_RoomNumbers_ChartInfo", value); }
        }
        public List<ChartVisibilityGridData> ChartVisiblityDetailList
        {
            get { return (List<ChartVisibilityGridData>)GetBaseSession("ChartVisiblityDetailList_ChartInfo"); }
            set { SetBaseSession("ChartVisiblityDetailList_ChartInfo", value); }
        }

        public List<AccommodationDetailedList> GetAccommodationDetailedList
        {
            get { return (List<AccommodationDetailedList>)GetBaseSession("GetAccommodationDetailedList_ChartAccommodation"); }
            set { SetBaseSession("GetAccommodationDetailedList_ChartAccommodation", value); }
        }

        public string Project_ID
        {
            get { return (string)GetBaseSession("Project_ID_ChartInfo"); }
            set { SetBaseSession("Project_ID_ChartInfo", value); }
        }
        public DataTable dt_ChartUserMappingInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_ChartUserMappingInfoGrid_ChartInfo"); }
            set { SetBaseSession("dt_ChartUserMappingInfoGrid_ChartInfo", value); }
        }
        public DataTable dt_ChartMappedUsersInfoGrid
        {
            get { return (DataTable)GetBaseSession("dt_ChartMappedUsersInfoGrid_ChartInfo"); }
            set { SetBaseSession("dt_ChartMappedUsersInfoGrid_ChartInfo", value); }
        }
        
        #region Chart_Info
        public ActionResult Frm_Chart_Info(string serviceProject_ID = null, string screen = "")
        {
            if (!(CheckRights(BASE, ClientScreen.Facility_ChartInfo, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Facility_ServiceReport').hide();</script>");
            }
            Chart_user_rights();
            Project_ID = serviceProject_ID;
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Screen = screen;
            ViewBag.CenId = BASE._open_Cen_ID;
            ViewBag.serviceProject_ID = serviceProject_ID;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (string.IsNullOrWhiteSpace(screen)==false && screen.Contains("Sparc_"))//to Fetch only First Instance
            {
                chartInfoGridData(Project_ID, false);
            }
            else
            {
                chartInfoGridData(Project_ID);
            }
            return View(dt_chartInfoGrid);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Frm_Chart_Info_Grid(string command, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Compact", bool AllInstances = true, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string Screen = "")
        {
            Chart_user_rights();
            if (command == "REFRESH" || dt_chartInfoGrid == null)
            {
                chartInfoGridData(Project_ID, AllInstances);
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.InstanceMode = AllInstances;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Screen = Screen;

            return View(dt_chartInfoGrid);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Frm_Chart_Sparc_Info_Grid(string command, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Compact", bool AllInstances = true, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string Screen = "")
        {
            if (command == "REFRESH" || dt_chartInfoGrid == null)
            {
                chartInfoGridData(Project_ID, AllInstances);
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.InstanceMode = AllInstances;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Screen = Screen;
            return View(dt_chartInfoGrid);
        }

        public void chartInfoGridData(string serviceProject_ID = null, bool allInstances = true)
        {
            dt_chartInfoGrid = BASE._Form_dbops.get_chartInfo(serviceProject_ID, allInstances);
        }
        public ActionResult Frm_Chart_Info_DetailGrid(string command, int CHART_INSTANCE_ID = 0, string regCenId = null, string summary_type = "CENTRE SUMMARY", int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Compact", string RowKeyToFocus = "", string Screen = "", string fromdate = null, string todate = null, string fromtime = null, string totime = null, string eventid = null)
        {
            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
            eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
            if (eventid == "null") { eventid = null; }
            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
            }
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["Layout"] = Layout;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Screen = Screen;
            ViewBag.chartInstanceID = CHART_INSTANCE_ID;
            ViewBag.summary_type = summary_type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            if (command == "REFRESH" || dt_chartResponsesummaryGrid == null)
            {
                chartInfoDetailGridData(CHART_INSTANCE_ID, summary_type, startdate, enddate, null, eventid);
            }
            //dt_chartResponsesummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(chartInstanceID), summary_type, startdate, enddate, null, eventid);
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            return PartialView(dt_chartResponsesummaryGrid);
        }
        public void chartInfoDetailGridData(int chartInstanceID = 0, string summary_type = "", DateTime? startdate = null, DateTime? enddate = null, string buildingId = null, string eventid = null)
        {
            dt_chartResponsesummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(chartInstanceID), summary_type, startdate, enddate, buildingId, eventid).Tables[0];
        }

        public ActionResult Frm_Export_Options()
        {
            return PartialView();
        }

        public ActionResult deleteChart(string chartID = "", string screen = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(chartID))
                {
                    jsonParam.message = "Value of Form ID is not received.";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                if (BASE._Form_dbops.IsChartHavingResponses(chartID) && string.IsNullOrWhiteSpace(screen) && screen.Contains("Sparc_Delete") == false)
                {
                    jsonParam.message = "This Form cannot be deleted because it has responses against it.";
                    jsonParam.title = "Not Allowed!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                if (BASE._Form_dbops.deleteChart(chartID))
                {
                    jsonParam.message = "The form is successfully deleted.";
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

        public ActionResult Frm_Create_Chart_Copy(string chartInstanceID = "", string chartName = "", string chartTitle = "", string chartDescription = "", string chartID = "")
        {
            createChartCopy model = new createChartCopy();
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                DataRow[] chartDetails = dt_chartInfoGrid.Select("CHART_INSTANCE_ID=" + Convert.ToInt32(chartInstanceID));

                //model.chartName = chartName + " - COPY";
                //model.chartTitle = chartTitle;
                //model.chartDescription = chartDescription;
                //model.chartID = chartID;

                model.chartName_createCopy = chartDetails[0]["Form"].ToString() + " - COPY";
                model.chartTitle_createCopy = chartDetails[0]["Form Title"].ToString();
                model.chartDescription_createCopy = chartDetails[0]["Form Description"].ToString();
                model.chartID = chartDetails[0]["CHART_ID"].ToString();
                model.Project_ID_createCopy = chartDetails[0]["Project_ID"].ToString();
                model.ServiceReport_Id = chartDetails[0]["SR_ID"].ToString();
                model.Event_Id = chartDetails[0]["Event_ID"].ToString();
                model.cenID_createCopy = BASE._open_Cen_ID;
                model.ServReport_Event_ID_createCopy = string.IsNullOrWhiteSpace(model.ServiceReport_Id) ? string.IsNullOrWhiteSpace(model.Event_Id) ? null : model.Event_Id : model.ServiceReport_Id;
                if (Convert.IsDBNull(chartDetails[0]["CI_FROM_DATE"]) == false)
                {
                    model.StartDate_createCopy = Convert.ToDateTime(chartDetails[0]["CI_FROM_DATE"]);
                }
                if (Convert.IsDBNull(chartDetails[0]["CI_TO_DATE"]) == false)
                {
                    model.EndDate_createCopy = Convert.ToDateTime(chartDetails[0]["CI_TO_DATE"]);
                }

                //if (projID.Length > 0 && chartDetails[0]["Purpose"].ToString() == "REGISTRATION" &&(string.IsNullOrWhiteSpace(sr_id) == true && string.IsNullOrWhiteSpace(event_id) == true))
                //{
                //    DataTable d1 = BASE._Form_dbops.GetRegistrationFormsCreatedInProjectOnly(projID);
                //    if (d1 != null && d1.Rows.Count > 0)
                //    {
                //        jsonParam.message = "Registration Form Created For Projects Cannot Be Copied<br>Only One Registration Form Can Exist For A Project If The Form Is Not Created For Service Report Or Event";
                //        jsonParam.title = "Information";                   
                //        jsonParam.result = false;
                //        return Json(new
                //        {
                //            jsonParam
                //        }, JsonRequestBehavior.AllowGet);
                //    }
                //}
                if (model.Project_ID_createCopy.Length > 0 && chartDetails[0]["Purpose"].ToString() == "BASIC DETAILS" && (string.IsNullOrWhiteSpace(model.ServiceReport_Id) == true && string.IsNullOrWhiteSpace(model.Event_Id) == true))
                {
                    DataTable d1 = BASE._Form_dbops.GetBasicDetailsFormsCreatedInProjectOnly(model.Project_ID_createCopy);
                    if (d1 != null && d1.Rows.Count > 0)
                    {
                        jsonParam.message = "Basic Details Form Created For Projects Cannot Be Copied<br>Only One Basic Details Form Can Exist For A Project If The Form Is Not Created For Service Report Or Event";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return View(model);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Frm_Create_Chart_Copy(createChartCopy model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(model.chartName_createCopy))
                {
                    jsonParam.message = "Please fill the Form Name.";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.result = false;
                    jsonParam.focusid = "chartName_createCopy";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrWhiteSpace(model.chartTitle_createCopy))
                {
                    jsonParam.message = "Please fill the Form Title.";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.result = false;
                    jsonParam.focusid = "chartTitle_createCopy";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (CommonFunctions.IsDate(model.StartDate_createCopy) && CommonFunctions.IsDate(model.EndDate_createCopy))
                {
                    if (model.StartDate_createCopy > model.EndDate_createCopy)
                    {
                        jsonParam.message = "Start Date Should Be Less Than Or Equal To End Date";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.result = false;
                        jsonParam.focusid = "EndDate_createCopy";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.cenID_createCopy == 0)
                {
                    jsonParam.message = "Centre Is Compulsory";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.result = false;
                    jsonParam.focusid = "cenID_createCopy";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

                }
                if (BASE._Form_dbops.CheckIfChartNameIsUniqueInUID(model.chartName_createCopy) == false)
                {
                    jsonParam.message = "There is already a form with this name. Please change the form name.";
                    jsonParam.title = "Alert!!";
                    jsonParam.result = false;
                    jsonParam.focusid = "chartName";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                Param_Create_Copy Inparam = new Param_Create_Copy();
                Inparam.cenid = model.cenID_createCopy;
                Inparam.ChartID = model.chartID;
                Inparam.chart_name = model.chartName_createCopy;
                Inparam.Description = model.chartDescription_createCopy.ReplacePwithDivTags();
                Inparam.eventID = model.Event_Id;
                Inparam.ProjectID = model.Project_ID_createCopy;
                Inparam.serviceReportID = model.ServiceReport_Id;
                Inparam.Title = model.chartTitle_createCopy.ReplacePwithDivTags();
                if (CommonFunctions.IsDate(model.StartDate_createCopy))
                {
                    Inparam.startDate = Convert.ToDateTime(model.StartDate_createCopy).ToString(BASE._Server_Date_Format_Long);
                }
                if (CommonFunctions.IsDate(model.EndDate_createCopy))
                {
                    Inparam.endDate = Convert.ToDateTime(model.EndDate_createCopy).ToString(BASE._Server_Date_Format_Long);
                }
                if (BASE._Form_dbops.createChartCopy(Inparam))
                {
                    jsonParam.message = "The form is successfully created.";
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
        public ActionResult Frm_Shift_Chart(string chartID, string chartName)
        {
            ShiftChart model = new ShiftChart();
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.chartName_shiftChart = chartName;
                model.chartId_shiftChart = chartID;
                model.cenId_shiftChart = BASE._open_Cen_ID;                
                return View(model);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Frm_Shift_Chart(ShiftChart model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.cenId_shiftChart == 0)
                {
                    jsonParam.message = "Centre Is Compulsory";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.result = false;
                    jsonParam.focusid = "cenID_createCopy";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

                }
                if (BASE._Form_dbops.CheckIfChartNameIsUniqueInUID(model.chartName_shiftChart, null, model.cenId_shiftChart) == false)
                {
                    jsonParam.message = "There is already a Form : <b>"+model.chartName_shiftChart+ "</b> in Centre : <b>" + model.cenName_shiftChart+" ("+model.cenUId_shiftChart+ ")</b> . Please change the Form Name.";
                    jsonParam.title = "Alert!!";
                    jsonParam.result = false;
                    jsonParam.focusid = "chartName";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                BASE._Form_dbops.ShiftFormToAnotherCentre(model.chartId_shiftChart, model.cenId_shiftChart);
                jsonParam.message = "The Form : <b>" + model.chartName_shiftChart + "</b> is Shifted to Centre : <b>" + model.cenName_shiftChart + " (" + model.cenUId_shiftChart + ")</b> successfully.";
                jsonParam.title = "Success!!";
                jsonParam.result = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
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

        #region Chart_Responses
        public ActionResult Frm_Chart_Responses_Info(string chartInstanceID = "", bool isApprovalRequired = false, string ServiceReportID = null, string EventID = null, 
            string formid = null, string regCenId = null, string regCentre = null,string registrationsTotalCount = null, string approvedResponseCount = null, 
            string rejectionResponseCount = null, bool showApproval = true, string QuestionFilter = "")
        {
            if (!(CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Facility_ServiceReport').hide();</script>");
            }
            Chart_Responses_Info_user_rights();
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.isApprovalRequired = isApprovalRequired;
            ViewBag.show_Approval = showApproval;
            ViewBag.ServiceReportID = ServiceReportID;
            ViewBag.ChartID_Responses = formid;
            ViewBag.EventID = EventID;
            ViewBag.CenId = BASE._open_Cen_ID;
            ViewBag.RegCenId = regCenId;
            ViewBag.RegCentre = string.IsNullOrWhiteSpace(regCentre) ? null : "[" + regCentre + "]";
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.RegistrationsTotalCount = registrationsTotalCount;
            ViewBag.ApprovedResponseCount = approvedResponseCount;
            ViewBag.RejectionResponseCount = rejectionResponseCount;
            ViewBag.Question_Filter = QuestionFilter;
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];           
            chartResponsesInfoGridData(chartInstanceID, string.IsNullOrWhiteSpace(regCenId) ? null : regCenId,false, QuestionFilter);

            if (dt_chartResponsesInfoGrid != null && dt_chartResponsesInfoGrid.Rows.Count > 0)
            {

                ViewBag.isApprovalRequired = Convert.ToBoolean(dt_chartResponsesInfoGrid.Rows[0]["ApprovalReq"]);
                ViewBag.ServiceReportID = dt_chartResponsesInfoGrid.Rows[0]["ServiceReportID"].ToString();
                ViewBag.DefaultStatus = dt_chartResponsesInfoGrid.AsEnumerable().Count(row => row.Field<string>("Status") == "Default");
                ViewBag.EventID = dt_chartResponsesInfoGrid.Rows[0]["EventID"].ToString();
                ViewBag.Purpose = dt_chartResponsesInfoGrid.Rows[0]["CHART_PURPOSE"].ToString();
            }
            return View(dt_chartResponsesInfoGrid);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Frm_Chart_Responses_Info_Grid(string command, bool isApprovalRequired = false, int ShowHorizontalBar = 1, string Chart_Instance_ID = "", 
            string Layout = null, bool VouchingMode = false, string ViewMode = "Compact", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", 
            string RowKeyToFocus = "", string ChartPurpose = "", string regCenId = null, string regCentre = null, string chartResponseStatus = null, bool showApproval = true,
            string QuestionFilter = "")
        {
            Chart_Responses_Info_user_rights();
            if (command == "REFRESH" || dt_chartResponsesInfoGrid == null)
            {
                chartResponsesInfoGridData(Chart_Instance_ID, regCenId, false, QuestionFilter);
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.isApprovalRequired = isApprovalRequired;
            ViewBag.show_Approval = showApproval;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.RegCentre = string.IsNullOrWhiteSpace(regCentre) ? null : "[" + regCentre + "]";
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Purpose = ChartPurpose;
            ViewBag.chartResponseStatus = chartResponseStatus;
            ViewBag.Question_Filter = QuestionFilter;
            return View(dt_chartResponsesInfoGrid);
        }
        public ActionResult Frm_Chart_MedicalResponses_Info(string chartInstanceID = "", bool isApprovalRequired = false, string ServiceReportID = null, string EventID = null, string formid = null, string regCenId = null, string regCentre = null)
        {
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.isApprovalRequired = isApprovalRequired;
            ViewBag.ServiceReportID = ServiceReportID;
            ViewBag.ChartID_Responses = formid;
            ViewBag.EventID = EventID;
            ViewBag.CenId = BASE._open_Cen_ID;
            ViewBag.RegCenId = regCenId;
            ViewBag.RegCentre = string.IsNullOrWhiteSpace(regCentre) ? null : "[" + regCentre + "]";
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            if (BASE._open_Cen_ID == 6980)
            //if (BASE._open_Cen_ID == 555 || BASE._open_Cen_ID == 4216)
            {
                chartResponsesInfoGridData(chartInstanceID, BASE._open_Cen_ID.ToString(), true);
                //chartResponsesInfoGridData(chartInstanceID, "6980", true);
            }            
            if (dt_chartResponsesMedicalInfoGrid != null && dt_chartResponsesMedicalInfoGrid.Rows.Count > 0)
            {
                ViewBag.isApprovalRequired = Convert.ToBoolean(dt_chartResponsesInfoGrid.Rows[0]["ApprovalReq"]);
                ViewBag.ServiceReportID = dt_chartResponsesInfoGrid.Rows[0]["ServiceReportID"].ToString();
                ViewBag.EventID = dt_chartResponsesInfoGrid.Rows[0]["EventID"].ToString();
                ViewBag.Purpose = dt_chartResponsesInfoGrid.Rows[0]["CHART_PURPOSE"].ToString();
            }
            return View(dt_chartResponsesMedicalInfoGrid);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Frm_Chart_MedicalResponses_Info_Grid(string command, bool isApprovalRequired = false, int ShowHorizontalBar = 1, string Chart_Instance_ID = "", string Layout = null, bool VouchingMode = false, string ViewMode = "Compact", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string ChartPurpose = "", string regCenId = null, string regCentre = null)
        {
            if (command == "REFRESH" || dt_chartResponsesInfoGrid == null)
            {
                chartResponsesInfoGridData(Chart_Instance_ID, regCenId, true);
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.isApprovalRequired = isApprovalRequired;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.RegCentre = regCenId;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.Purpose = ChartPurpose;
            return View(dt_chartResponsesMedicalInfoGrid);
        }
        public void chartResponsesInfoGridData(string chartInstanceID = null, string regCenId = null, Boolean isChartResponseMedicalView=false, string questionFilter="")
        {
            int typeCastedIntVal;
            if (isChartResponseMedicalView)
            {
                dt_chartResponsesMedicalInfoGrid = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID), Int32.Parse(regCenId), questionFilter);
                return;
            }
            if (string.IsNullOrWhiteSpace(regCenId))
            {
                dt_chartResponsesInfoGrid = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID),null,questionFilter);
            }
            else
            {                
                if (int.TryParse(regCenId, out typeCastedIntVal) && isChartResponseMedicalView==false)//If Centre is not in Form but trying to edit Response then it will give "false" as CenId
                {
                    dt_chartResponsesInfoGrid = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID), typeCastedIntVal, questionFilter);
                }
                else
                {
                   
                    dt_chartResponsesInfoGrid = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID), null, questionFilter);
                }
            }
        }
        public ActionResult Frm_Export_Options_Responses()
        {
            return PartialView();
        }
        public ActionResult Frm_Export_Options_Medical()
        {
            return PartialView();
        }
        #region Task Register
        public ActionResult Frm_TaskRegister_Info(string chartInstanceID = "139", bool isApprovalRequired = false, string ServiceReportID = null, string EventID = null)
        {
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.isApprovalRequired = isApprovalRequired;
            ViewBag.ServiceReportID = ServiceReportID;
            ViewBag.EventID = EventID;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            dt_TaskRegisterInfoGrid = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(chartInstanceID));
            if (dt_TaskRegisterInfoGrid != null && dt_TaskRegisterInfoGrid.Rows.Count > 0)
            {
                ViewBag.isApprovalRequired = Convert.ToBoolean(dt_TaskRegisterInfoGrid.Rows[0]["ApprovalReq"]);
                ViewBag.ServiceReportID = dt_TaskRegisterInfoGrid.Rows[0]["ServiceReportID"].ToString();
                ViewBag.EventID = dt_TaskRegisterInfoGrid.Rows[0]["EventID"].ToString();
            }
            return View(dt_TaskRegisterInfoGrid);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Frm_TaskRegister_Info_Grid(string command, bool isApprovalRequired = false, int ShowHorizontalBar = 1, string Chart_Instance_ID = "", string Layout = null, bool VouchingMode = false, string ViewMode = "Compact", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            if (command == "REFRESH" || dt_TaskRegisterInfoGrid == null)
            {
                dt_TaskRegisterInfoGrid = BASE._Form_dbops.get_chartResponses(Convert.ToInt32(Chart_Instance_ID));
            }

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.isApprovalRequired = isApprovalRequired;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;

            return View(dt_TaskRegisterInfoGrid);
        }
        public ActionResult Frm_Export_Options_TaskRegister()
        {
            return PartialView();
        }
        public void SessionClearTaskRegister()
        {
            ClearBaseSession("_TaskRegister");
        }
        #endregion
        public ActionResult AddGuideSessionID() {
            string GuideSessionID = Guid.NewGuid().ToString();
            BASE._Form_dbops.InsertGuideSession(GuideSessionID);
            return Json(new { GuideSessionID = GuideSessionID }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeChartResponseStatus(string status = "DEFAULT", string chartResponseID = "", string chartInstanceID = "", string responseReason = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                int Chart_Instance_ID = Convert.ToInt32(chartInstanceID);
                int recStatus = 1;
                if (status.ToUpper() == "APPROVE") { recStatus = 2; }
                if (status.ToUpper() == "DRAFT") { recStatus = 0; }
                if (status.ToUpper() == "REJECT") { recStatus = -1; }
                if (status.ToUpper() == "CANCEL") { recStatus = -2; }
                string[] All_ResponseID = chartResponseID.Split(',');
                for (int i = 0; i < All_ResponseID.Count(); i++)
                {                    
                    if (BASE._Form_dbops.updateChartResponseStatus(chartInstanceID, All_ResponseID[i], recStatus, responseReason))
                    {
                        if (status == "APPROVE" || status == "REJECT")
                        {
                            BASE._Notifications_DBOps.SendNotificationOnEvent(All_ResponseID[i], status, Chart_Instance_ID);
                            DataTable sms = BASE._Notifications_DBOps.GetSMSNotificationOnEvent(All_ResponseID[i], status, Chart_Instance_ID);
                            if (sms.Rows.Count > 0) 
                            {
                                if (string.IsNullOrWhiteSpace(sms.Rows[0]["MOBILE"].ToString()) == false) 
                                {
                                    string REMINDER_TYPE = "";
                                    string SMSSentStatusMessage = "";
                                    if (status == "APPROVE")
                                    {
                                        if (string.IsNullOrWhiteSpace(sms.Rows[0]["registration_no"].ToString()))
                                        {
                                            REMINDER_TYPE = "ChartApprove";
                                        }
                                        else
                                        {
                                            REMINDER_TYPE = "ChartApprove_Registration";
                                        }
                                    }
                                    else if (status == "REJECT")
                                    {
                                        REMINDER_TYPE = "ChartReject";
                                    }

                                    BASE._Notifications_DBOps.SendSMS(Chart_Instance_ID, REMINDER_TYPE, ref SMSSentStatusMessage, All_ResponseID[i], "", sms.Rows[0]["MOBILE"].ToString());
                                }
                            }
                            //ChatResponse_Success(status, Chart_Instance_ID, All_ResponseID[i]);
                        }
                        if (status.Equals("CANCEL") && string.IsNullOrWhiteSpace(responseReason)==false)
                        {
                            DataTable DT = BASE._Form_dbops.GetFormSubmissionConfirmation(Chart_Instance_ID, All_ResponseID[i]);
                            Chart dataParams = new Chart();
                            dataParams.visitor_mobileNo = DT.Rows[0]["Mobile"].ToString();
                            dataParams.visitor_emailID = DT.Rows[0]["Email"].ToString();
                            //dataParams.visitor_emailID = DT.Rows[0]["Email"].ToString();
                            dataParams.emailId_CC = DT.Rows[0]["CC_Email"].ToString();
                            dataParams.emailId_BCC = DT.Rows[0]["BCC_Email"].ToString();
                            dataParams.emailId_ReplyTo = DT.Rows[0]["ReplyTo_Email"].ToString();
                            DataTable Reg_slip = BASE._Form_dbops.GetFormRegistrationSlip(Chart_Instance_ID, All_ResponseID[i]);
                            dataParams.emailSubject = "Cancellation for "+Reg_slip.Rows[0]["CI_CHARTNAME"].ToString();
                            dataParams.emailerName = dataParams.formTitle;
                            dataParams.emailId_To = dataParams.visitor_emailID;
                            dataParams.formDescription = Reg_slip.Rows[0]["CI_DESCRIPTION"].ToString();
                            dataParams.formTitle = Reg_slip.Rows[0]["CI_CHARTNAME"].ToString();
                            string formattedWhatsapp = responseReason.Replace("\n", "%0a");
                            if (string.IsNullOrWhiteSpace(dataParams.visitor_mobileNo)==false)
                            {
                                BASE._Notifications_DBOps.InsertWhatsappQueue(dataParams.visitor_mobileNo, formattedWhatsapp, All_ResponseID[i], All_ResponseID[i], null, ConfigurationManager.AppSettings["DefaultWhatsAppSender"]);
                            }
                            if (string.IsNullOrWhiteSpace(dataParams.visitor_emailID)==false)
                            {
                                BASE._Notifications_DBOps.InsertEmailQueue(dataParams.visitor_emailID, dataParams.emailSubject, responseReason, dataParams.emailId_CC, dataParams.emailId_BCC, All_ResponseID[i], All_ResponseID[i], dataParams.emailId_ReplyTo, ConfigurationManager.AppSettings["SenderId"], ConfigurationManager.AppSettings["senderPassword"]);
                            }
                        }
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (status == "APPROVE")
                {
                    jsonParam.message = "The selected form(s) have been marked as <b> Accepted </b>!  <br> User(s) will be notified via Email,Whatsapp,SMS if Configured";
                }
                if (status == "REJECT")
                {
                    jsonParam.message = "The selected form(s) have been marked as <b> Rejected </b>! <br> User(s) will be notified via Email,Whatsapp,SMS if Configured";
                }
                if (status == "DRAFT")
                {
                    jsonParam.message = "The selected form(s) have been marked as <b> Draft </b>!";
                }
                if (status == "CANCEL")
                {
                    jsonParam.message = "The selected form(s) have been marked as <b> Cancelled </b>!";
                }
                jsonParam.title = "SUCCESS";
                jsonParam.result = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Chart_Response_AddRejectionReason(string ChartInstanceID = null, string ChartResponseID = null, string ChartResponseRejectionRemarks = "")
        {
            ViewBag.ChartInstanceID = ChartInstanceID;
            ViewBag.ChartResponseID = ChartResponseID;
            ViewBag.ChartResponseRejectionRemarks = ChartResponseRejectionRemarks;
            return View();
        }
        public ActionResult Frm_Chart_Response_AddApprovalReason(string ChartInstanceID = null, string ChartResponseID = null, string ChartResponseApproveRemarks = "")
        {
            ViewBag.ChartInstanceID = ChartInstanceID;
            ViewBag.ChartResponseID = ChartResponseID;
            ViewBag.ChartResponseApprovalRemarks = ChartResponseApproveRemarks;
            return View();
        }
        public ActionResult Frm_Chart_Response_AddCancelReason(string ChartInstanceID = null, string ChartResponseID = null, string ChartResponseCancelRemarks = "")
        {
            ViewBag.ChartInstanceID = ChartInstanceID;
            ViewBag.ChartResponseID = ChartResponseID;
            ViewBag.ChartResponseCancelRemarks = ChartResponseCancelRemarks;
            return View();
        }
        public ActionResult Frm_ChartResponse_AddRemarks(string ChartResponseID = null, string ChartResponseRemarks = "", string GridNametoRefreshAfterAdd = "ChartResponsesInfoGrid")
        {
            ViewBag.ChartResponseID = ChartResponseID;
            ViewBag.ChartResponseRemarks = ChartResponseRemarks;
            ViewBag.GridNameToRefresh = GridNametoRefreshAfterAdd;
            return View();

        }

        public ActionResult saveChartResponseRemarks(string ChartResponseID = null, string ChartResponseRemarks = "", string GridNametoRefreshAfterAdd = "ChartResponsesInfoGrid")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE._Form_dbops.updateChartResponseRemarks(ChartResponseID, ChartResponseRemarks))
                {
                    jsonParam.message = "The remarks for the selected response is updated successfully.";
                    jsonParam.title = "Success!!";
                    jsonParam.result = true;
                    jsonParam.flag = GridNametoRefreshAfterAdd;
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
        public ActionResult Frm_ChartResponse_MarkCategory(string ChartResponseID = null)
        {
            ViewBag.ChartResponseID = ChartResponseID;
            return View();
        }
        public ActionResult saveResponseCategory(string ChartResponseID = null, string Category=null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE._Form_dbops.MarkResponseCategory(ChartResponseID, Category))
                {
                    string EmailSendStatus = ""; string WhatsappSendStatus = "";
                    string[] ResponseID = ChartResponseID.Split(',');
                    for (int i = 0; i < ResponseID.Length; i++)
                    {
                        EmailSendStatus = BASE._Notifications_DBOps.SendNotificationOnEvent(ResponseID[i], "UPDATE_USER_CATEGORY_" + Category.ToUpper());
                        //EmailSendStatus = BASE._Notifications_DBOps.SendEmailOnNotificationEvent(ResponseID[i], "UPDATE_USER_CATEGORY_" + Category.ToUpper());
                        //WhatsappSendStatus = BASE._Notifications_DBOps.SendWhatsAppOnNotificationEvent(ResponseID[i], "UPDATE_USER_CATEGORY_" + Category.ToUpper());
                    }
                    jsonParam.message = EmailSendStatus + WhatsappSendStatus +  "<br>The Category For The Selected Response(s) Is Updated Successfully.";
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
        public ActionResult saveResponseHighlight(string ChartResponseID = null, string Highlight = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE._Form_dbops.HighlightResponse(ChartResponseID, Highlight))
                {
                    string[] ResponseIDs = ChartResponseID.Split(',');
                    for (int i = 0; i < ResponseIDs.Length; i++) 
                    {
                        var dataRow = dt_chartResponsesInfoGrid.Select("RESP_ID='"+ ResponseIDs[i] + "'");
                        if (dataRow != null && dataRow.Length > 0) 
                        {
                            dataRow[0]["Highlight"] = Highlight;
                        }
                    }
                    jsonParam.message = "Responses Highlighted Successfully";
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
        public ActionResult deleteChartResponse(string ChartResponseID = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (BASE._Form_dbops.deleteChartResponse(ChartResponseID))
                {
                    jsonParam.message = "The selected response is <b>DELETED</b> successfully.";
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

        public ActionResult Frm_ChartResponse_Preview(string ChartResponseID, string chartInstanceID)
        {
            previewResponse model = new previewResponse();

            DataRow[] chartDetails = dt_chartInfoGrid.Select("CHART_INSTANCE_ID=" + Convert.ToInt32(chartInstanceID));

            model.headerImage = Request.Url.Scheme + "://" + Request.Url.Authority + "/Attachments/" + chartDetails[0]["CHART_IMAGE"].ToString();
            model.formName = chartDetails[0]["Form"].ToString();
            model.formDescription = chartDetails[0]["Form Description"].ToString();

            model.responseRow = dt_chartResponsesInfoGrid.Select("RESP_ID='" + ChartResponseID + "'");

            return View(model);

            //string HTML_Elements = "";
            //foreach (DataColumn c in response[0].Table.Columns)
            //{
            //    string question = c.ColumnName;
            //    string answer = response[0][c.ColumnName].ToString();

            //    string HTML = "<div class='row section_FormResponse'>" +
            //                    "<div class='col -sm-12'>" +
            //                      "<h5>Q."+ question  + "</h5>" +
            //                      "<p><b>A.</b>" + answer + "</p>" +
            //                    "</div>" +
            //                 "</div>";

            //    HTML_Elements = HTML_Elements + HTML;
            //}

            //return View(HttpUtility.HtmlEncode(HTML_Elements));

        }

        public ActionResult ChatResponse_Success(string chartResponseStatus = "", int ChartInstanceId = 0, string ChartResponseId = null, bool AlreadyRegistered = false, bool FromEmail = false)
        {
            Chart dataParams = new Chart();

            dataParams.AlreadyRegistered = AlreadyRegistered;
            dataParams.FromEmail = FromEmail;
            DataTable DT = null;
            DT = BASE._Form_dbops.GetFormSubmissionConfirmation(ChartInstanceId, ChartResponseId);
            //ViewBag.c = "<p>Thank you for Registering for Non Residential Pass, Divya Basant International Cultural Fest. <br> <b>Your Registration No. is DB-NRP-99845</b><br> <small>You can print or save your registration slip below. We have sent you registration confirmation by email and SMS too.</small></p>";

            dataParams.header_image = GetAttachmentPath(ChartResponseId, DT.Rows[0]["header_image"].ToString(), true);
            dataParams.chartResponseStatus = chartResponseStatus.ToUpper();
            dataParams.confirmation_message = DT.Rows[0]["CONFIRMATION_MESSAGE"].ToString();
            dataParams.approval_message = DT.Rows[0]["APPROVAL_MESSAGE"].ToString();
            dataParams.rejection_message = DT.Rows[0]["REJECTION_MESSAGE"].ToString();
            dataParams.header_width = Convert.ToInt32(DT.Rows[0]["header_image_width"].ToString());
            dataParams.header_height = Convert.ToInt32(DT.Rows[0]["header_image_height"].ToString());
            dataParams.show_reg_slip = Convert.ToBoolean(DT.Rows[0]["SHOW_REGISTRATION_SLIP"]);
            dataParams.visitor_mobileNo = DT.Rows[0]["Mobile"].ToString();
            dataParams.visitor_emailID = DT.Rows[0]["Email"].ToString();
            //dataParams.visitor_emailID = DT.Rows[0]["Email"].ToString();
            dataParams.emailId_CC = DT.Rows[0]["CC_Email"].ToString();
            dataParams.emailId_BCC = DT.Rows[0]["BCC_Email"].ToString();
            dataParams.emailId_ReplyTo = DT.Rows[0]["ReplyTo_Email"].ToString();

            DataTable Reg_slip = BASE._Form_dbops.GetFormRegistrationSlip(ChartInstanceId, ChartResponseId);
            //For popup data
            dataParams.heading = Reg_slip.Rows[0]["Heading"].ToString();
            dataParams.event_name = Reg_slip.Rows[0]["Event_Name"].ToString();
            dataParams.thumbnail_image_path = GetAttachmentPath(ChartResponseId, Reg_slip.Rows[0]["Thumbnail_image_Path"].ToString(), true);
            dataParams.schedule_date = Reg_slip.Rows[0]["Schedule_Date"].ToString();
            dataParams.schedule_time = Reg_slip.Rows[0]["Schedule_Time"].ToString();
            dataParams.visitor_name = Reg_slip.Rows[0]["Visitor_Name"].ToString();
            dataParams.visitor_contact = Reg_slip.Rows[0]["Visitor_Contact"].ToString();
            dataParams.visitor_location = Reg_slip.Rows[0]["Visit_Location"].ToString();
            dataParams.helpline = Reg_slip.Rows[0]["Helpline"].ToString();
            dataParams.registration_no = Reg_slip.Rows[0]["Reg_No"].ToString();
            dataParams.formDescription = Reg_slip.Rows[0]["CI_DESCRIPTION"].ToString();
            dataParams.formTitle = Reg_slip.Rows[0]["CI_CHARTNAME"].ToString();
            dataParams.Organizer = Reg_slip.Rows[0]["Organizer"].ToString();
            dataParams.Reg_Slip_Instructions = Reg_slip.Rows[0]["Reg_Slip_Instructions"].ToString();
            dataParams.visitor_location_qrcode_url = Reg_slip.Rows[0]["visitor_location_qrcode_url"].ToString();
            dataParams.urlOrigin = Request.Url.Scheme + "://" + Request.Url.Authority;
            dataParams.ChartResponseId = ChartResponseId;
            dataParams.ChartInstanceId = ChartInstanceId;

            dataParams.Visitor_Name_Label = Reg_slip.Rows[0]["Visitor_Name_Label"].ToString();
            dataParams.Visitor_Details_Label = Reg_slip.Rows[0]["Visitor_Details_Label"].ToString();
            dataParams.Visitor_Location_Label = Reg_slip.Rows[0]["Visitor_Location_Label"].ToString();
            dataParams.Helpline_No_Label = Reg_slip.Rows[0]["Helpline_No_Label"].ToString();
            dataParams.Organizer_Label = Reg_slip.Rows[0]["Organizer_Label"].ToString();
            dataParams.Reg_Confirmation_Footer = Reg_slip.Rows[0]["Reg_Confirmation_Footer"].ToString();

            // Emailer Section
            dataParams.emailSubject = dataParams.formTitle;//+ " " + dataParams.formDescription;
            dataParams.emailerName = dataParams.formTitle;
            dataParams.emailId_To = dataParams.visitor_emailID;

            string emailSentStatusMessage = "";

            if (dataParams.AlreadyRegistered == false && dataParams.FromEmail == false)
            {
                if (string.IsNullOrWhiteSpace(dataParams.emailId_To) == false)
                {
                   dataParams.emailSentStatus = SendHTMLEmail(ref emailSentStatusMessage, dataParams.emailId_To, dataParams.emailSubject, "~/Areas/Facility/Views/Chart/FormResponseEmailer.cshtml", dataParams, false, dataParams.emailerName, dataParams.emailId_CC, dataParams.emailId_BCC, dataParams.emailId_ReplyTo);
                }
            }
            dataParams.emailSentStatusMessage = emailSentStatusMessage;

            if (dataParams.AlreadyRegistered == false && dataParams.visitor_mobileNo.Length > 0 && dataParams.FromEmail == false)
            {
                if (chartResponseStatus.ToUpper() == "APPROVE" && DT.Rows[0]["Approval_Whatsapp"].ToString().Length > 0)
                {
                  BASE._Notifications_DBOps.InsertWhatsappQueue(dataParams.visitor_mobileNo, DT.Rows[0]["Approval_Whatsapp"].ToString(), DT.Rows[0]["Batch_Name_Whatsapp_Approval"].ToString(), ChartResponseId);
                }
                if (chartResponseStatus.ToUpper() == "REJECT" && DT.Rows[0]["Rejection_Whatsapp"].ToString().Length > 0)
                {
                   BASE._Notifications_DBOps.InsertWhatsappQueue(dataParams.visitor_mobileNo, DT.Rows[0]["Rejection_Whatsapp"].ToString(), DT.Rows[0]["Batch_Name_Whatsapp_Reject"].ToString(), ChartResponseId);
                }
            }

            // SMS Section
            string SMSSentStatusMessage = "";
            if (dataParams.AlreadyRegistered == false && dataParams.FromEmail == false)
            {
                if (string.IsNullOrWhiteSpace(dataParams.visitor_mobileNo) == false)
                {
                    string REMINDER_TYPE = "";

                    if (chartResponseStatus == "APPROVE")
                    {
                        if (string.IsNullOrWhiteSpace(dataParams.registration_no))
                        {
                            REMINDER_TYPE = "ChartApprove";
                        }
                        else
                        {
                            REMINDER_TYPE = "ChartApprove_Registration";
                        }
                    }
                    else if (chartResponseStatus == "REJECT")
                    {
                        REMINDER_TYPE = "ChartReject";
                    }

                    BASE._Notifications_DBOps.SendSMS(ChartInstanceId, REMINDER_TYPE, ref SMSSentStatusMessage, ChartResponseId, "", dataParams.visitor_mobileNo);
                }
            }

            if (dataParams.AlreadyRegistered == false && dataParams.FromEmail == true) // View will be returned only if its redirected from email.
            {
                return View(dataParams);
            }
            else //On Approval Or Rejection nothing will be returned and only Email & SMS will be sent to the applicant.
            {
                return new EmptyResult();
            }


        }
        public ActionResult ChangeResponseArrivalStatus(string chartResponseID, string _Action)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            string[] Array_chartRespIds = chartResponseID.Split('|');

            try
            {
                for (var i = 0; i < Array_chartRespIds.Length; i++)
                {
                    BASE._Form_dbops.updateChartResponseArrivalStatus(Array_chartRespIds[i], _Action);
                }


                jsonParam.message = _Action.ToTitleCase() + "ed As Arrived Successfully";
                jsonParam.title = "Success!!";
                jsonParam.result = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Frm_ChartResponse_Export2GoogleSheet_Email(int Form_Instance_Id, string option)
        {
            ViewBag.Form_Instance_Id = Form_Instance_Id;
            ViewBag.option = option;
            return View();
        }
        [HttpGet]
        public ActionResult Frm_ChartResponse_Export2GoogleSheet_RemoveSheet(int Form_Instance_Id, string optionChoosed)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (string.IsNullOrWhiteSpace(optionChoosed) == false && optionChoosed.Equals("Remove"))
            {
                BASE._Form_dbops.ExportData2GoogleSheet_RemoveSheetId(Form_Instance_Id);
                jsonParam.message = "Successfully removed Google sheet. <br> <b>Note:</b> Updated data will not be exported in Google sheet further but existing data will remain available in existing Google sheet.";
                jsonParam.title = "Success!!";
                jsonParam.result = true;
            }
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Frm_ChartResponse_Export2GoogleSheet_Email_Post(int Form_Instance_Id, string optionChoosed, string email = null, string SessionGUID = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string url = null;
                string emails = null;
                bool overwrite = false;                
                if (string.IsNullOrWhiteSpace(optionChoosed) == false && optionChoosed.Equals("overwrite"))
                {
                    overwrite = true;
                }
                string result = BASE._Form_dbops.ExportData2GoogleSheet(Form_Instance_Id, email, overwrite);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    if (result.Contains("|"))
                    {
                        string[] tokens = result.Split('|');
                        emails = tokens[0];
                        url = tokens[tokens.Length - 1];
                    }                    
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        if (url.Contains("https://docs.google.com/spreadsheets/d/"))
                        {
                            jsonParam.message = "Google Sheet has been shared with email: <b>" + emails + "</b><br> Click OK button to open exported Google Sheet in new Tab|" + url;
                        }
                        else if(url.Contains("Unable to parse range: Sheet1"))
                        {
                            throw new Exception("Invalid Sheet name. Sheet name must not be changed. It should remain 'Sheet1'.");
                        }
                        else
                        {
                            throw new Exception(url);
                        }
                    }
                    else if(result.Equals("No data to Export"))
                    {
                        jsonParam.message = "There are no reponses to export.";
                    }
                }
                else
                {
                    jsonParam.message = "There are no reponses to export.";
                }
                jsonParam.title = "Success!!";
                jsonParam.result = true;
            }
            catch (Exception e)
            {                
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
            }

            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Frm_ChartResponse_ListPreview2GoogleSheet_Email(string email=null)
        {
            ViewBag.email = email;
            return View();
        }
        [HttpGet]
        public ActionResult Frm_ChartResponse_ListPreview2GoogleSheet_Email_Export(string email, string SessionGUID = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string url = null;
                string emails = null;
                string result = null;
                if(dt_chartInfoGrid != null && dt_chartInfoGrid.Rows.Count > 0)
                {
                    result = BASE._Form_dbops.ListPreviewData2GoogleSheet(dt_chartInfoGrid, email);
                }
                else
                {
                    jsonParam.message = "There is no data to export";
                    jsonParam.title = "Success!!";
                    jsonParam.result = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                string[] tokens = result.Split('|');
                emails = tokens[0];
                url = tokens[tokens.Length - 1];
                if (string.IsNullOrWhiteSpace(url))
                {
                    jsonParam.message = "There is no data to export";
                }
                else
                {
                    jsonParam.message = "Google Sheet has been shared with email: <b>" + emails + "</b><br> Click OK button to open exported Google Sheet in new Tab|"+url;
                }
                jsonParam.title = "Success!!";
                jsonParam.result = true;
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
            }

            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Test()
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            String urls = BASE._Form_dbops.OverwriteFormResponsesOfGoogleSheet();
            string [] ids = urls.Split('|');
            jsonParam.message = "";
            for (int i = 0; i < ids.Length; i++)
            {
                jsonParam.message += "<a target='_blank' href='" + ids[i]+"'>Click to visit Google Sheet"+i+1+" </a><br>";
            }
            jsonParam.title = "Success!!";
            jsonParam.result = true;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet) ;
        }
            
        //public ActionResult Frm_ChartResponse_Accomodation(string _ChartResponseID, Int64 _ChartInstanceID, string gridToRefresh = "ChartResponsesInfoGrid")
        //{
        //    ResponseMiscDetails model = new ResponseMiscDetails();
        //    if (string.IsNullOrWhiteSpace(_ChartResponseID) == false)//Does not check in case of first time login attempt 
        //    {
        //        DataTable _table = BASE._Form_dbops.GetResponseMiscDetails(_ChartResponseID);
        //        if (_table.Rows.Count > 0)
        //        {
        //            if (string.IsNullOrWhiteSpace(_table.Rows[0]["CRMD_ACCOMODATION_MISC_REC_ID"].ToString()) == false)
        //            {
        //                model.AccommodationMiscID = _table.Rows[0]["CRMD_ACCOMODATION_MISC_REC_ID"].ToString();
        //            }
        //            if ( _table.Rows[0]["CRMD_BED_COUNT"] != null)
        //            {
        //                model.AccommodationMiscID = _table.Rows[0]["CRMD_ACCOMODATION_MISC_REC_ID"].ToString();
        //                model.BedCount = (Int32)_table.Rows[0]["CRMD_BED_COUNT"];
        //                model.Remarks = _table.Rows[0]["CRMD_REMARKS"].ToString();
        //                if (_table.Rows[0]["CRMD_FROMDATE"] != System.DBNull.Value)
        //                {
        //                    model.fromdate = (DateTime?)_table.Rows[0]["CRMD_FROMDATE"];
        //                    model.fromtime = (DateTime?)_table.Rows[0]["CRMD_FROMDATE"];
        //                }
        //                if (_table.Rows[0]["CRMD_TODATE"] != System.DBNull.Value)
        //                {
        //                    model.todate = (DateTime?)_table.Rows[0]["CRMD_TODATE"];
        //                    model.totime = (DateTime?)_table.Rows[0]["CRMD_TODATE"];
        //                }
        //            }

        //            else { model.BedCount = 1; }
        //        }
        //        else 
        //        { 
        //            model.BedCount = 1;
        //            DataTable d1 = BASE._Form_dbops.Get_From_ToDateOfEventsByServiceReportId(_ChartInstanceID);
        //            if (d1 != null && d1.Rows.Count > 0)
        //            {
        //                model.fromdate = Convert.ToDateTime(d1.Rows[0]["SR_PROG_FR_DATE"].ToString());
        //                model.todate = Convert.ToDateTime(d1.Rows[0]["SR_PROG_TO_DATE"].ToString());
        //                model.fromtime = Convert.ToDateTime(model.fromtime).Add(new TimeSpan(0, 0, 1));
        //                model.totime = Convert.ToDateTime(model.totime).Add(new TimeSpan(23, 59, 59));
        //            }
        //        }
        //    }
        //    ViewBag.ChartResponseID = _ChartResponseID;
        //    ViewBag.ChartInstanceID = _ChartInstanceID;
        //    ViewBag.gridToRefresh = gridToRefresh;
        //    return View(model);
        //}


        [HttpPost]
        public ActionResult UpdateChartResponseAccomodation(string ChartResponseID, string Accomodation, int BedCount, string remarksText, string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, string gridToRefresh = "ChartResponsesInfoGrid")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);

            DataTable d1 = BASE._Form_dbops.GetAccomodationList(BASE._open_Cen_ID, startdate, enddate, ChartResponseID, Accomodation).Tables[0];
            Int32 allowed = 0;
            Int32 allotted = 0;
            if (d1 != null && d1.Rows.Count > 0)
            {
                allowed = Convert.ToInt32(d1.Rows[0]["Allowed"].ToString());
                allotted = Convert.ToInt32(d1.Rows[0]["Allotted"].ToString());
                if (allotted + BedCount > allowed)
                {
                    jsonParam.message = "Only " + (allowed - allotted) + " beds are remain to allocate";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    jsonParam.focusid = "BedCount";
                    jsonParam.flag = "refeshDropdown";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
            }
            if (enddate != null && startdate != null)
            {
                if (enddate < startdate)
                {
                    if (Convert.ToDateTime(enddate).Date < Convert.ToDateTime(startdate).Date)
                    {
                        jsonParam.message = "End Date Should be greater than or equal to Start Date";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "EndDate_chartUserMapping";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "End time Should be greater than Start time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    jsonParam.focusid = "EndTime_chartUserMapping";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            try
            {
                BASE._Form_dbops.updateChartResponseAccomodation(ChartResponseID, Accomodation, BedCount, remarksText, startdate, enddate);

                jsonParam.message = "Accomodation Has Been Updated Successfully";
                jsonParam.title = "Success!!";
                jsonParam.result = true;
                jsonParam.flag = gridToRefresh;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LookUp_Get_AccomodationList(DataSourceLoadOptions loadOptions, string fromdate = null, string end_date = null, string fromtime = null, string end_time = null)
        {
            DateTime? startdate = null;
            DateTime? enddate = null;
            if (fromdate != null && fromdate != "")
            {
                if (fromtime != null && fromtime != "")
                {
                    startdate = (Convert.ToDateTime(fromdate).Date).AddTicks(Convert.ToDateTime(fromtime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(fromdate);
                }
            }
            if (end_date != null && end_date != "")
            {
                if (end_time != null && end_time != "")
                {
                    enddate = (Convert.ToDateTime(end_date).Date).AddTicks(Convert.ToDateTime(end_time).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(end_date);
                }
            }

            //var roomid = "";
            DataSet ds = BASE._Form_dbops.GetAccomodationList(BASE._open_Cen_ID, startdate, enddate);
            DataTable d1 = ds.Tables[0];
            DataTable d2 = ds.Tables[1];
            if (d2 != null)
            {
                var data = DatatableToModel.DataTableTo_AccommodationDetailsList(d2);
                GetAccommodationDetailedList = data;
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_AccommodationPlaces(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_AccomodationDetailsList(DataSourceLoadOptions loadOptions, string fromdate = null, string end_date = null,
            string fromtime = null, string end_time = null, string roomid = null)
        {
            DateTime? startdate = null;
            DateTime? enddate = null;
            if (fromdate != null && fromdate != "")
            {
                if (fromtime != null && fromtime != "")
                {
                    startdate = (Convert.ToDateTime(fromdate).Date).AddTicks(Convert.ToDateTime(fromtime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(fromdate);
                }
            }
            if (end_date != null && end_date != "")
            {
                if (end_time != null && end_time != "")
                {
                    enddate = (Convert.ToDateTime(end_date).Date).AddTicks(Convert.ToDateTime(end_time).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(end_date);
                }
            }

            var filteredlist = GetAccommodationDetailedList.Where(e => e.ROOMID == roomid);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(filteredlist, loadOptions)), "application/json");
        }


        public ActionResult LookUp_ServicePlaceNamesList(DataSourceLoadOptions loadOptions)
        {
            DataTable ServicePlacesList = BASE._Form_dbops.GetServicePlacesByCenid(BASE._open_Cen_ID) as DataTable;
            var Purposedata = DatatableToModel.DataTableTo_ServicePlaces(ServicePlacesList);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Purposedata, loadOptions)), "application/json");
        }
        #endregion
        #region TravelAndGuest Summary
        public ActionResult Frm_TravelSummary_Info(string summary_type, string chartInstanceID = "", string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, string eventid = null)
        {

            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
            eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
            if (eventid == "null") { eventid = null; }

            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
            }
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            dt_chartResponsesummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(chartInstanceID), summary_type, startdate, enddate, null, eventid).Tables[0];
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            return View(dt_chartResponsesummaryGrid);
        }

        public ActionResult Frm_TravelSummary_Info_Grid(string command, string summary_type, int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
            string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",
            string RowKeyToFocus = "", string fromdate = null, string todate = null, string EventID = null)
        {
            DateTime? startdate = Convert.ToDateTime(null);
            DateTime? enddate = Convert.ToDateTime(null);
            if (fromdate != null && fromdate != "")
            {
                startdate = Convert.ToDateTime(fromdate);
            }
            if (todate != null && todate != "")
            {
                enddate = Convert.ToDateTime(todate);
            }
            if (command == "REFRESH" || dt_chartResponsesummaryGrid == null)
            {
                dt_chartResponsesummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID).Tables[0];
            }

            if (EventID == "" || EventID == "null") { EventID = null; }
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.summary_type = summary_type;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;

            return View(dt_chartResponsesummaryGrid);
        }

        public ActionResult Frm_Export_Options_Summary()
        {
            return PartialView();
        }
        public ActionResult SendRegSMS(string ChartResponseId="", string MobNo = "", string SMSType = "")
        {
            string SMSSentStatusMessage = "";
            BASE._Notifications_DBOps.SendSMS(null, SMSType, ref SMSSentStatusMessage, ChartResponseId, "", MobNo);
            Return_Json_Param jsonParam = new Return_Json_Param();
            jsonParam.message = SMSSentStatusMessage;
            jsonParam.title = "SMS Send Status!!";
            jsonParam.result = true;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        #endregion     
     
        public ActionResult LookUp_Get_EventsList(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.Get_ServiceReports_And_Events();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Service_Report_Event(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_EventsList_ThreeMonths(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.Get_ServiceReports_And_Events_ThreeMonths();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Service_Report_Event(d1), loadOptions)), "application/json");
        }
        public ActionResult Frm_FormVisibility_Info(string FormID)
        {
            ChartVisibilityInfo model = new ChartVisibilityInfo();
            model.ChartID = FormID;
            return View(model);
        }
        public List<ChartVisibilityGridData> GetChartVisibilityDetails(string FormID)
        {
            DataTable d1 = BASE._Form_dbops.GetChartVisibilityDetails(FormID);
            if (d1 != null && d1.Rows.Count > 0)
            {
                ChartVisiblityDetailList = DatatableToModel.DataTableTo_ChartVisiblityDetailList(d1);
            }
            else
            {
                ChartVisiblityDetailList = new List<ChartVisibilityGridData>();
            }
            return ChartVisiblityDetailList;
        }
        public ActionResult LookUp_Get_ChartVisibilityList(DataSourceLoadOptions loadOptions, string FormID)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetChartVisibilityDetails(FormID), loadOptions)), "application/json");
        }
        public ActionResult Frm_FormVisibility_Window(string Tag, string RecID, string ChartID)
        {
            ChartVisibilityWindow model = new ChartVisibilityWindow();
            model.Tag = Tag;
            if (string.IsNullOrWhiteSpace(RecID) == false)
            {
                model.RecID = Convert.ToInt32(RecID);
            }
            model.ChartID = ChartID;
            model.Ins_All_ChartVisibility = true;
            model.Cen_All_ChartVisibility = true;
            model.AccType_All_ChartVisibility = true;
            if (string.IsNullOrWhiteSpace(RecID) == false)
            {
                ChartVisibilityGridData row = ChartVisiblityDetailList.Find(x => x.Rec_ID == Convert.ToInt32(RecID));
                if (row != null)
                {
                    model.Cen_ID_ChartVisibility = row.Cen_ID;
                    model.FromYear_ChartVisibility = row.FromYear;
                    model.ToYear_ChartVisibility = row.ToYear;
                    model.Ins_ID_ChartVisibility = row.Ins_ID;
                    model.AccType_ID_ChartVisibility = row.AccType_ID;
                    model.UserType_ChartVisibility = row.UserType;
                    model.Menu_ChartVisibility = row.VisibleInMenu;
                }
                model.Ins_All_ChartVisibility = false;
                model.Cen_All_ChartVisibility = false;
                model.AccType_All_ChartVisibility = false;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveChartVisibilityDetails(ChartVisibilityWindow model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag == "_New" || model.Tag == "_Edit")
                {
                    if (model.FromYear_ChartVisibility != null && model.ToYear_ChartVisibility != null)
                    {
                        if (model.ToYear_ChartVisibility < model.FromYear_ChartVisibility)
                        {
                            jsonParam.message = "To Year Must Be Greater Than Equal To From Year";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.focusid = "ToYear_ChartVisibility";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.Tag == "_New")
                {
                    if (model.Ins_All_ChartVisibility == false)
                    {
                        if (string.IsNullOrWhiteSpace(model.Ins_ID_ChartVisibility) == true)
                        {
                            jsonParam.message = "Institute Is Required";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.focusid = "Ins_ID_ChartVisibility";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Cen_All_ChartVisibility == false)
                    {
                        if (model.Cen_ID_ChartVisibility == null || model.Cen_ID_ChartVisibility == 0)
                        {
                            jsonParam.message = "Centre Is Required";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.focusid = "Cen_ID_ChartVisibility";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.AccType_All_ChartVisibility == false)
                    {
                        if (string.IsNullOrWhiteSpace(model.AccType_ID_ChartVisibility))
                        {
                            jsonParam.message = "Account Type Is Required";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.focusid = "AccType_ID_ChartVisibility";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    ChartVisibilityGridData row = ChartVisiblityDetailList.Find(x => string.IsNullOrWhiteSpace(x.UserType) == false);
                    if (row == null)
                    {
                        if (string.IsNullOrWhiteSpace(model.UserType_ChartVisibility) == true)
                        {
                            jsonParam.message = "User Type Is Required";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.focusid = "UserType_ChartVisibility";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    row = ChartVisiblityDetailList.Find(x => string.IsNullOrWhiteSpace(x.VisibleInMenu) == false);
                    if (row == null)
                    {
                        if (string.IsNullOrWhiteSpace(model.Menu_ChartVisibility) == true)
                        {
                            jsonParam.message = "Menu Is Required";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.focusid = "Menu_ChartVisibility";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    Param_Insert_Chart_Visibility_Details Inparam = new Param_Insert_Chart_Visibility_Details();
                    Inparam.Acc_Type = model.AccType_ID_ChartVisibility;
                    Inparam.Cen_ID = model.Cen_ID_ChartVisibility;
                    Inparam.Chart_ID = model.ChartID;
                    Inparam.FY_From = model.FromYear_ChartVisibility;
                    Inparam.FY_To = model.ToYear_ChartVisibility;
                    Inparam.Instt_ID = model.Ins_ID_ChartVisibility;
                    Inparam.Menu = model.Menu_ChartVisibility;
                    Inparam.User_Type = model.UserType_ChartVisibility;
                    Inparam.Ins_All = model.Ins_All_ChartVisibility;
                    Inparam.Cen_All = model.Cen_All_ChartVisibility;
                    Inparam.AccType_All = model.AccType_All_ChartVisibility;
                    if (BASE._Form_dbops.InsertChartVisibilityDetails(Inparam))
                    {
                        jsonParam.message = "Chart Visibility Detail Added Successfully";
                        jsonParam.title = "Success!!";
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Information!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (model.Tag == "_Edit")
                {
                    Param_Update_Chart_Visibility_Details UpParam = new Param_Update_Chart_Visibility_Details();
                    UpParam.Acc_Type = model.AccType_ID_ChartVisibility;
                    UpParam.Cen_ID = model.Cen_ID_ChartVisibility;
                    UpParam.Chart_ID = model.ChartID;
                    UpParam.FY_From = model.FromYear_ChartVisibility;
                    UpParam.FY_To = model.ToYear_ChartVisibility;
                    UpParam.Instt_ID = model.Ins_ID_ChartVisibility;
                    UpParam.Menu = model.Menu_ChartVisibility;
                    UpParam.User_Type = model.UserType_ChartVisibility;
                    UpParam.Rec_ID = (int)model.RecID;
                    if (BASE._Form_dbops.EditChartVisibilityDetails(UpParam))
                    {
                        jsonParam.message = "Chart Visibility Detail Updated Successfully";
                        jsonParam.title = "Success!!";
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Information!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (model.Tag == "_Delete")
                {
                    if (BASE._Form_dbops.DeleteChartVisibilityDetails((int)model.RecID))
                    {
                        jsonParam.message = "Chart Visibility Detail Deleted Successfully";
                        jsonParam.title = "Success!!";
                        jsonParam.result = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Information!!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Information!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LookUp_Get_AccType(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.GetAccType();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Name_ID(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Institutes(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.GetInstitutes();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_Name_ID(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_FinancialYear(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = BASE._Form_dbops.GetFinancialYears();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_FinancialYearList(d1), loadOptions)), "application/json");
        }
        public ActionResult LookUp_Get_Centres(DataSourceLoadOptions loadOptions, string InsID)
        {
            DataTable d1 = BASE._Form_dbops.GetCentres(InsID);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_CentresList(d1), loadOptions)), "application/json");
        }
        public ActionResult Frm_UsersMappingtoChart_Info(Int32? Chart_ID)
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Chartid = Chart_ID;
            ChartUserMappingInfoGridData(Chart_ID);
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View(dt_ChartUserMappingInfoGrid);
        }
        public ActionResult Frm_UsersMappingtoChart_Info_Grid(string command, Int32? Chart_ID, string Project_ID = null, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Default",
            string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            if (command == "REFRESH" || dt_ChartUserMappingInfoGrid == null)
            {
                ChartUserMappingInfoGridData(Chart_ID);
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

            return View(dt_ChartUserMappingInfoGrid);
        }
        public void ChartUserMappingInfoGridData(Int32? Chart_ID)
        {
            dt_ChartUserMappingInfoGrid = BASE._Form_dbops.get_usersForMappingtoChart(Chart_ID);
        }

        #region User Chart Mappings
        public ActionResult Frm_MappedUserstoChart_Info(Int32 Chart_ID, string Chart_Name = "", string Screen = null)
        {
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Chartid = Chart_ID;
            ViewBag.Chart_Name = Chart_Name;
            ViewBag.Screen = Screen;
            ChartMappedUsersInfoGridData(Chart_ID);

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View(dt_ChartMappedUsersInfoGrid);
        }
        public ActionResult Frm_MappedUserstoChart_Info_Grid(string command, Int32? Chart_ID, int ShowHorizontalBar = 1, string Layout = null, bool VouchingMode = false, string ViewMode = "Default",
            string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            if (command == "REFRESH" || dt_ChartMappedUsersInfoGrid == null)
            {
                ChartMappedUsersInfoGridData(Chart_ID);
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

            return View(dt_ChartMappedUsersInfoGrid);
        }
        public void ChartMappedUsersInfoGridData(Int32? Chart_ID)
        {
            dt_ChartMappedUsersInfoGrid = BASE._Form_dbops.GetMappedUsersofChart(Chart_ID);
        }

        public ActionResult Save_MapUsersToChart(string selectedUserIDs, string selectedUserABIDs, Int32 Chart_ID, string fromdate = null, string todate = null, string fromtime = null, string totime = null) //string RegChartID
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string[] Array_UserIds = selectedUserIDs.Split('|');
                string[] Array_UserAbIds = selectedUserABIDs.Split('|');

                //Below commented code needs to be discussed with Saurabh Bhaiji.
                //DataTable d1 = BASE._Form_dbops.GetMappedUsersWithChartResponsesForChart(Chart_ID);
                //if (d1 != null && d1.Rows.Count > 0)
                //{
                //    for (int i = 0; i < d1.Rows.Count; i++)
                //    {
                //        if (Array_UserIds.Contains(d1.Rows[i]["CR_SERV_USER_ID"].ToString()) == false) //need to check with abid with and parameter
                //        {
                //            jsonParam.message = "User With ID " + d1.Rows[i]["CR_SERV_USER_ID"].ToString() + " Cannot Be UnMapped.<br> User Has Responses Against Form Linked With The Project";
                //            jsonParam.title = "Incomplete Information...";
                //            jsonParam.result = false;
                //            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                //}
                DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
                DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
                DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
                DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
                if (starttime != null && startdate == null)
                {
                    jsonParam.message = "Start Date Must Be Mentioned Before Setting Start Time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    jsonParam.focusid = "StartDate_chartUserMapping";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (endtime != null && enddate == null)
                {
                    jsonParam.message = "End Date Must Be Mentioned Before Setting End Time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    jsonParam.focusid = "EndDate_chartUserMapping";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }

                if (startdate != null)
                {
                    if (starttime != null)
                    {
                        startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                    }
                    else
                    {
                        startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                    }
                }
                if (enddate != null)
                {
                    if (endtime != null)
                    {
                        enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                    }
                    else
                    {
                        enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                    }
                }
                if (enddate != null && startdate != null)
                {
                    if (enddate < startdate)
                    {
                        if (Convert.ToDateTime(enddate).Date < Convert.ToDateTime(startdate).Date)
                        {
                            jsonParam.message = "End Date Should be greater than or equal to Start Date";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.focusid = "EndDate_chartUserMapping";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.message = "End time Should be greater than Start time";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "EndTime_chartUserMapping";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Array_UserIds != null && Array_UserIds.Length > 0)
                {
                    for (int i = 0; i < Array_UserIds.Length; i++)
                    {
                        BASE._Form_dbops.Insert_usersMappingToChart(Convert.ToInt32(Array_UserIds[i]), Array_UserAbIds[i], Chart_ID, startdate, enddate);
                    }
                }
                jsonParam.message = Messages.SaveSuccess;
                jsonParam.title = "Success..";
                jsonParam.result = true;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult deleteUsersFromChartByRecid(string selectedRecIDs,string Chart_ID = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            string[] Array_RecIds = selectedRecIDs.Split('|');
            try
            {
                DataTable d1 = BASE._Form_dbops.Get_ChartMappedUsersRec_IDWithChartResponsesForChart(Chart_ID);
                if (d1 != null && d1.Rows.Count > 0)
                {
                    for (int i = 0; i < d1.Rows.Count; i++)
                    {
                        if (Array_RecIds.Contains(d1.Rows[i]["REC_ID"].ToString()) == true)
                        {
                            jsonParam.message = "User With ID " + d1.Rows[i]["SUCM_SERVICE_USER_ID"].ToString() + " Cannot Be UnMapped.<br> User Has Responses Against The Chart";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (Array_RecIds != null && Array_RecIds.Length > 0)
                {
                    for (int i = 0; i < Array_RecIds.Length; i++)
                    {
                        BASE._Form_dbops.Delete_usersMappedToChartByRecid(Convert.ToInt32(Array_RecIds[i].ToString()));
                    }
                }
                jsonParam.message = "Users UnMapped Successfully";
                jsonParam.title = "Success..";
                jsonParam.result = true;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult editUsersoftheChartByRecid(string selectedRecIDs, string fromdate = null, string todate = null, string fromtime = null, string totime = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string[] Array_RecIds = selectedRecIDs.Split('|');

                DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
                DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
                DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
                DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);


                if (startdate != null)
                {
                    if (starttime != null)
                    {
                        startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                    }
                    else
                    {
                        startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                    }
                }
                if (enddate != null)
                {
                    if (endtime != null)
                    {
                        enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                    }
                    else
                    {
                        enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                    }
                }
                if (enddate != null && startdate != null)
                {
                    if (enddate < startdate)
                    {
                        jsonParam.message = "End time Should be greater than Start time";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Array_RecIds != null && Array_RecIds.Length > 0)
                {
                    for (int i = 0; i < Array_RecIds.Length; i++)
                    {
                        BASE._Form_dbops.UpdateMappedUserOfChart(Convert.ToInt32(Array_RecIds[i]), startdate, enddate);
                    }
                }
                jsonParam.message = Messages.UpdateSuccess;
                jsonParam.title = "Success..";
                jsonParam.result = true;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_FromToDateSelection()
        {
            return View();
        }
        #endregion

        public ActionResult Frm_BulkAllotment_Info(Int64 instanceid, Int32 TotalRegCount = 0)
        {
            ViewBag.instanceid = instanceid;
            ViewBag.TotalRegCount = TotalRegCount;
            DataTable dt = BASE._Form_dbops.GetEventDatesOfInstance(instanceid);
            if (dt.Rows.Count == 0)
            {
                ViewBag.fromdate = DateTime.Now.Date.Add(new TimeSpan(0, 0, 0));
                ViewBag.todate = DateTime.Now.AddDays(1).Date.Add(new TimeSpan(23, 59, 59));
            }
            else
            {
                ViewBag.fromdate = dt.Rows[0]["SR_PROG_FR_DATE"];
                ViewBag.todate = dt.Rows[0]["SR_PROG_TO_DATE"];
            }

            return View();
        }
        public ActionResult LookUp_Get_BulkAllotmentsList(DataSourceLoadOptions loadOptions, Int64 instanceid)
        {
            DataTable dt = BASE._Form_dbops.get_BulkAllotments_List_of_Instance(instanceid);
            return Content(JsonConvert.SerializeObject(dt), "application/json");
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_LiveFromsList(dt), loadOptions)), "application/json");
        }

        [HttpPost]
        public ActionResult GetAvailableRoomCountOnChangeofOptions(Int64 instanceid, string BuildingID = null, string AC_NONAC = null, string Category = null, Int32 RoomCapacity = 0, string fromdate = null,
            string todate = null, string fromtime = null, string totime = null, Int32 BedCount = 0, string ActionMethod = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);

            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
            }
            if (enddate != null && startdate != null)
            {
                if (enddate < startdate)
                {
                    if (Convert.ToDateTime(enddate).Date < Convert.ToDateTime(startdate).Date)
                    {
                        jsonParam.message = "End Date Should be greater than Start Date";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        jsonParam.focusid = "EndDate_chartUserMapping";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "End time Should be greater than Start time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    jsonParam.focusid = "EndTime_chartUserMapping";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (Category == "") { Category = null; }
            if (AC_NONAC == "") { AC_NONAC = null; }
            if (RoomCapacity == 0) { RoomCapacity = Convert.ToInt32(null); }
            if (BedCount == 0) { BedCount = Convert.ToInt32(null); }
            int AvailableRoomCount = 0;
            string msg = ""; string title = "";
            try
            {
                DataTable dt = BASE._Form_dbops.getAvailableRoomsCount_Bulk(instanceid, BuildingID, RoomCapacity, AC_NONAC, Category, startdate, enddate);
                if (dt.Rows.Count > 0)
                {
                    AvailableRoomCount = Convert.ToInt32(dt.Rows[0][0]);
                    msg = "Bulk Allottment Has Been Allotted Successfully";
                    title = "Success!!";
                }
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }


            jsonParam.message = msg;
            jsonParam.title = title;
            jsonParam.flag = Convert.ToString(AvailableRoomCount);
            jsonParam.result = true;
            //jsonParam.flag = gridToRefresh;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveBulkAllottment(Int64 instanceid, string BuildingID = null, string AC_NONAC = null, string Category = null, Int32 RoomCapacity = 0, string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, Int32 BedCount = 0, string ActionMethod = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            fromdate = fromdate.Replace(" GMT+0530 (India Standard Time)", "");
            todate = todate.Replace(" GMT+0530 (India Standard Time)", "");
            fromtime = fromtime.Replace(" GMT+0530 (India Standard Time)", "");
            totime = totime.Replace(" GMT+0530 (India Standard Time)", "");

            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);

            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
            }
            if (enddate != null && startdate != null)
            {
                if (enddate < startdate)
                {
                    if (Convert.ToDateTime(enddate).Date < Convert.ToDateTime(startdate).Date)
                    {
                        jsonParam.message = "To Date Should be greater than From Date";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        //jsonParam.focusid = "EndDate_chartUserMapping";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "To time Should be greater than From time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    //jsonParam.focusid = "EndTime_chartUserMapping";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            String msg = ""; string title = "";
            if (AC_NONAC == "") { AC_NONAC = null; }
            if(Category == "") { Category = null; }
            if (ActionMethod.ToUpper() == "_ADD")
            {
                try
                {
                    Boolean s = BASE._Form_dbops.InsertBulkAllotmentToInstance(instanceid, BuildingID, RoomCapacity, AC_NONAC, Category, BedCount, startdate, enddate);
                    if (s) {
                        msg = "Bulk Allottment Has Been Allotted Successfully";
                        title = "Success!!";
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

            jsonParam.message = msg;
            jsonParam.title = title;
            jsonParam.result = true;
            //jsonParam.flag = gridToRefresh;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UpdateBulkAllottment(Int64 instanceid, Int64 recid_blk, string BuildingID = null, string AC_NONAC = null, string Category = null, string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, Int32 RoomCapacity= 0, Int32 BedCount = 0, string ActionMethod = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            //fromdate = fromdate.Replace(" GMT+0530 (India Standard Time)", "");
            //todate = todate.Replace(" GMT+0530 (India Standard Time)", "");
            //fromtime = fromtime.Replace(" GMT+0530 (India Standard Time)", "");
            //totime = totime.Replace(" GMT+0530 (India Standard Time)", "");

            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);

            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
            }
            if (enddate != null && startdate != null)
            {
                if (enddate < startdate)
                {
                    if (Convert.ToDateTime(enddate).Date < Convert.ToDateTime(startdate).Date)
                    {
                        jsonParam.message = "To Date Should be greater than From Date";
                        jsonParam.title = "Information";
                        jsonParam.result = false;
                        //jsonParam.focusid = "EndDate_chartUserMapping";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "To time Should be greater than From time";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    //jsonParam.focusid = "EndTime_chartUserMapping";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            if (AC_NONAC == "") { AC_NONAC = null; }
            if (Category == "") { Category = null; }

            String msg = ""; string title = "";
            if(ActionMethod.ToUpper() == "_EDIT")
            {
                try
                {
                    Boolean s = BASE._Form_dbops.Update_BulkAllotment(instanceid, BuildingID, RoomCapacity, AC_NONAC, Category, BedCount, recid_blk, startdate, enddate);
                    if (!s) {
                        msg = "Bulk Allottment Has Been Updated Successfully";
                        title = "Success!!";
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

            jsonParam.message = msg;
            jsonParam.title = title;
            jsonParam.result = true;
            //jsonParam.flag = gridToRefresh;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult deleteBulkAllottement(Int64 recid)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            String msg = ""; string title = "";
            try
            {
                Boolean s = BASE._Form_dbops.Delete_BulkAllottmentByRecID(recid);
                if (s)
                {
                    msg = "Selected Bulk Allottment Has Been Deleted Successfully";
                    title = "Success!!";
                }
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            jsonParam.message = msg;
            jsonParam.title = title;
            jsonParam.result = true;
            //jsonParam.flag = gridToRefresh;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Add_BulkAllotment_window(Int64 instanceid, Int32 TotalRegistrations, Int32 AlreadyAllottedTotal, string fromdate = null, string todate = null, string fromtime = null,
            string totime = null, string ActionMethod="_Add", string BuildingID = null, string AC_NONAC = null, string Category = null, Int32 RoomCapacity = 0, Int32 BedCount = 0, Int64 recid_blk = 1)
        {
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);

            if (startdate != null)
            {
                if (starttime != null)
                {
                    startdate = Convert.ToDateTime(startdate).AddTicks(Convert.ToDateTime(starttime).TimeOfDay.Ticks);
                }
                else
                {
                    startdate = Convert.ToDateTime(startdate).Add(new TimeSpan(0, 0, 0));
                }
            }
            if (enddate != null)
            {
                if (endtime != null)
                {
                    enddate = Convert.ToDateTime(enddate).AddTicks(Convert.ToDateTime(endtime).TimeOfDay.Ticks);
                }
                else
                {
                    enddate = Convert.ToDateTime(enddate).Add(new TimeSpan(23, 59, 59));
                }
            }


            BulkAllottment model = new BulkAllottment();
            model.instanceid = instanceid;
            model.totalregistrations = TotalRegistrations;
            model.actionmethod = ActionMethod;
            model.alreadyallottedCount = AlreadyAllottedTotal;
            model.recid_blk = recid_blk;
            model.buildingid = BuildingID;
            model.acNonac = AC_NONAC;
            model.category = Category;
            model.roomCapacity = RoomCapacity;
            model.BedCount = BedCount;
            model.FromDate_Blk = startdate;
            model.ToDate_Blk = enddate;
            model.FromTime_Blk = startdate;
            model.ToTime_Blk = enddate;
            ViewBag.CurrentRecordOldAllottment = BedCount;
            return View(model);
        }
        public ActionResult Frm_ChartResponse_GroupDetails(string ChartResponseID)
        {
            ViewBag.ChartResponseID = ChartResponseID;
            return View();
        }
        public ActionResult LookUp_Get_ResponseGroupDetails(DataSourceLoadOptions loadOptions, string ChartResponseID)
        {
            return Content(JsonConvert.SerializeObject(BASE._Form_dbops.get_chartResponses_GroupDetails(ChartResponseID)), "application/json");
        }
        public void SessionClearChartInfo()
        {
            ClearBaseSession("_ChartInfo");
        }
        public void SessionClearChartAccommodation()
        {
            ClearBaseSession("_ChartAccommodation");
        }
        public JsonResult CheckResponseExistsForChartInstance(Int32 ChartInstanceID)
        {
            DataTable _data = BASE._Chart_DBOps.GetResponseForChartInstance(ChartInstanceID, BASE._open_Cen_ID);

            ConnectOneMVC.Models.Return_Json_Param jsonParam = new ConnectOneMVC.Models.Return_Json_Param();
            jsonParam.message = "";
            jsonParam.result = false;
            int cen_id = BASE._open_Cen_ID;
            string user_id = BASE._open_User_ID;
            string user_type = BASE._open_User_Type.ToUpper();
            string ResponseID = "";
            if (_data.Rows.Count > 0) { ResponseID = _data.Rows[0][0].ToString(); jsonParam.message = "Response Exists"; jsonParam.result = true; }
            return Json(new
            {
                jsonParam,
                cen_id,
                user_id,
                user_type,
                ResponseID
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frm_Recommendation_Info(Int32? InstanceID, string ResponseIDs = null, string RegistrationsCountString = null, string ChartID = null)
        {
            ViewBag.instanceid = InstanceID;
            var totalRegCount = 0;
            string[] a = RegistrationsCountString.Split(',');
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "" || a[i] == null) { a[i] = "1"; }
                totalRegCount = totalRegCount + Convert.ToInt32(a[i]);
            }
            ViewBag.TotalRegCount = totalRegCount;
            ViewBag.responseids = ResponseIDs;
            ViewBag.chartid_recommendation = ChartID;
            return View();
        }
        public ActionResult LookUp_Get_BulkAllotmentUniqueListForRecommendation(DataSourceLoadOptions loadOptions, Int64 instanceid, string responseids = null)
        {
            DataTable dt = BASE._Form_dbops.get_BulkAllotmentListForRecommendationOfInstance(instanceid, responseids);
            return Content(JsonConvert.SerializeObject(dt), "application/json");
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DatatableToModel.DataTableTo_LiveFromsList(dt), loadOptions)), "application/json");
        }

        [HttpPost]
        public ActionResult SaveRecommendation_Bulk(string responseids, string BuildingID = null, string AC_NONAC = null, string Category = null, Int32 RoomCapacity = 0,
            Int32 BedCount = 0, string Remarks = null, Int32 chartid = 0, Int32 instanceid = 0, string RoomNumber = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            string[] responseids_Array = responseids.Split(',');
            String msg = ""; string title = "";
            string responseid = "";
            if (AC_NONAC == "") { AC_NONAC = null; }
            if (Category == "") { Category = null; }
            for (int i = 0; i < responseids_Array.Length; i++)
            {
                responseid = responseids_Array[i];
                try
                {
                    Boolean s = BASE._Form_dbops.Insert_RecommendationToResponce_Bulk(responseid, BuildingID, RoomCapacity, AC_NONAC, Category, 1, Remarks, chartid, instanceid, RoomNumber);
                    if (s)
                    {
                        msg = "Recommendation Saved Successfully";
                        title = "Success!!";
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

            jsonParam.message = msg;
            jsonParam.title = title;
            jsonParam.result = true;
            //jsonParam.flag = gridToRefresh;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LookUp_Get_BuildingsList(DataSourceLoadOptions loadOptions, Int64 instanceid)
        {
            DataTable d1 = BASE._Form_dbops.GetBulkAllottedBuildingsListByInstanceID(instanceid);
            return Content(JsonConvert.SerializeObject(d1), "application/json");
        }
        public ActionResult LookUp_GetRoomsList(DataSourceLoadOptions loadOptions, bool DDRefresh = false, string buildingid = null)
        {
            if (buildingid == "") { buildingid = null; }
            if (RoomNumbersdt == null || DDRefresh == true)
            {
                DataTable RoomNumbersdt = BASE._Form_dbops.GetRoomNumbersByBuildingID(buildingid);
                if (RoomNumbersdt == null)
                {
                    return Json(new { result = false, message = Messages.SomeError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(RoomNumbersdt), "application/json");
                }
            }
            return Content(JsonConvert.SerializeObject(RoomNumbersdt), "application/json");
        }
        public ActionResult CreateLocalAddress(string AB_ID)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (string.IsNullOrWhiteSpace(AB_ID))
                {
                    jsonParam.message = "The details of the User are not available for this response !";
                    jsonParam.title = "Alert!";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                int insertionResult = BASE._Address_DBOps.Insert_LocalAddress(AB_ID);

                if (insertionResult == 1)
                {
                    jsonParam.message = "The user of the selected response is successfully created for this centre.";
                    jsonParam.title = "Success!";
                    jsonParam.result = true;
                }
                if (insertionResult == 0)
                {                    
                    jsonParam.message = "The user of the selected response already belongs to this centre.";
                    jsonParam.title = "Alert!";
                    jsonParam.result = false;
                }

                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                jsonParam.message = e.Message;
                jsonParam.title = "Error!";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Passes_Info(string Reg)
        {
            DataTable dt = BASE._Form_dbops.get_UserName_Category_From_RegNo(Reg);
            if (dt.Rows.Count > 0)
            {
                ViewBag.Name = dt.Rows[0]["Name"].ToString();
                ViewBag.RegNo = dt.Rows[0]["REG_NO"].ToString();
                ViewBag.Category = dt.Rows[0]["Category"].ToString();
            }
            return View();
        }

        public void Chart_user_rights()
        {
            //ViewData["Facility_ChartInfo_FirstOrAllInstancesRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "FirstOrAllInstances");
            ViewData["Facility_ChartInfo_BulkAllotmentRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Add");
            ViewData["Facility_ChartInfo_SummariesRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "View");
            ViewData["Facility_ChartInfo_UserMappingsRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Add");
            ViewData["Facility_ChartInfo_AddRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Add");
            ViewData["Facility_ChartInfo_CopyRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Add");
            ViewData["Facility_ChartInfo_ShiftRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Add");
            ViewData["Facility_ChartInfo_CustomNotificationRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Add");//Change it when rights provided
            ViewData["Facility_ChartInfo_OnlyAllowedToSpecificUsersRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "View");
            ViewData["Facility_ChartInfo_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Update");
            ViewData["Facility_ChartInfo_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Delete");
            ViewData["Facility_ChartInfo_VisibilityRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Update");
            ViewData["Facility_ChartInfo_ResponsesRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "List");
            //ViewData["Facility_ChartInfo_CompactDefaultRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "CompactDefault");            
            ViewData["Facility_ChartInfo_ExportRight"] = CheckRights(BASE, ClientScreen.Facility_ChartInfo, "Export");     
        }
        public void Chart_Responses_Info_user_rights()
        {
            ViewData["Facility_ChartResponsesInfo_CreateLocalAddressRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Add");
            ViewData["Facility_ChartResponsesInfo_NotifyRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Approve");
            ViewData["Facility_ChartResponsesInfo_GroupDetailRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "View");
            ViewData["Facility_ChartResponsesInfo_RecommendationRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Add");
            ViewData["Facility_ChartResponsesInfo_MarkCategoryRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Update");
            ViewData["Facility_ChartResponsesInfo_AddRemarkRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Update");
            ViewData["Facility_ChartResponsesInfo_ApproveRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Approve");
            ViewData["Facility_ChartResponsesInfo_DraftRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Update");
            ViewData["Facility_ChartResponsesInfo_RejectRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Approve");
            ViewData["Facility_ChartResponsesInfo_CancelRegistrationRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Approve");
            ViewData["Facility_ChartResponsesInfo_ArrivedRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Update");
            ViewData["Facility_ChartResponsesInfo_NotArrivedRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Update");
            ViewData["Facility_ChartResponsesInfo_AccomodationRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Approve");
            ViewData["Facility_ChartResponsesInfo_AddRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Add");
            ViewData["Facility_ChartResponsesInfo_UpdateRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Update");
            ViewData["Facility_ChartResponsesInfo_DeleteRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Delete");
            ViewData["Facility_ChartResponsesInfo_ReConfirmRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "View");
            ViewData["Facility_ChartResponsesInfo_PreviewRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "View");
            ViewData["Facility_ChartResponsesInfo_ExportRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "Export");
            ViewData["Facility_ChartResponsesInfo_ShareRight"] = CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "List");
        }

        #region Devextreme

        public ActionResult Frm_Chart_Info_dx(string serviceProject_ID = null, string screen = "")
        {
            if (!(CheckRights(BASE, ClientScreen.Facility_ChartInfo, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Facility_ServiceReport').hide();</script>");
            }
            Chart_user_rights();
            Project_ID = serviceProject_ID;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.Screen = screen;
            ViewBag.CenId = BASE._open_Cen_ID;
            ViewBag.serviceProject_ID = serviceProject_ID;
            ViewBag.InstanceMode = true;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            //if (string.IsNullOrWhiteSpace(screen) && screen.Contains("Sparc_"))
            //{
            //    chartInfoGridData(Project_ID, false);
            //}
            //else
            //{
            //    chartInfoGridData(Project_ID);
            //}
            return View();
        }
        [HttpGet]
        public ActionResult ChartInfoGrid_dx_load(bool AllInstances = true, string FormType="Open")
        {
            ViewBag.InstanceMode = AllInstances;
             dt_chartInfoGrid = BASE._Form_dbops.get_chartInfo(Project_ID, AllInstances, FormType);
            return Content(JsonConvert.SerializeObject(dt_chartInfoGrid), "application/json");
        }

        [HttpGet]
        public ActionResult ChartInfoGridDetailGrid_Load(int chartInstanceID = 0, string summary_type = "CENTRE SUMMARY", DateTime? startdate = null, DateTime? enddate = null, string buildingId = null, string eventid = null)
        {
            dt_chartResponsesummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(chartInstanceID), summary_type, startdate, enddate, buildingId, eventid).Tables[0];
            return Content(JsonConvert.SerializeObject(dt_chartResponsesummaryGrid), "application/json");
        }


        public ActionResult Frm_Chart_Responses_Info_dx(string chartInstanceID = "", bool isApprovalRequired = false, string ServiceReportID = null, string EventID = null, string formid = null,
            string regCenId = null, string regCentre = null, string registrationsTotalCount = null, string approvedResponseCount = null,
            string rejectionResponseCount = null, bool showApproval = true, string QuestionFilter = "", string ChartPurpose = "")
        {
            if (!(CheckRights(BASE, ClientScreen.Facility_ChartResponsesInfo, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Facility_ServiceReport').hide();</script>");
            }
            Chart_Responses_Info_user_rights();
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.isApprovalRequired = isApprovalRequired;
            ViewBag.show_Approval = showApproval;
            ViewBag.ServiceReportID = ServiceReportID;
            ViewBag.ChartID_Responses = formid;
            ViewBag.EventID = EventID;
            ViewBag.RegCenId = regCenId;
            ViewBag.RegCentre = string.IsNullOrWhiteSpace(regCentre) ? null : "[" + regCentre + "]";
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.RegistrationsTotalCount = registrationsTotalCount;
            ViewBag.ApprovedResponseCount = approvedResponseCount;
            ViewBag.RejectionResponseCount = rejectionResponseCount;
            ViewBag.Question_Filter = QuestionFilter;

            ViewBag.Purpose = ChartPurpose;
            ViewBag.ServicePath = System.Configuration.ConfigurationManager.AppSettings["Servicespath"];
            chartResponsesInfoGridData(chartInstanceID, string.IsNullOrWhiteSpace(regCenId) ? null : regCenId, false, QuestionFilter);
           
            if (dt_chartResponsesInfoGrid != null && dt_chartResponsesInfoGrid.Rows.Count > 0)
            {
                ViewBag.isApprovalRequired = Convert.ToBoolean(dt_chartResponsesInfoGrid.Rows[0]["ApprovalReq"]);
                ViewBag.ServiceReportID = dt_chartResponsesInfoGrid.Rows[0]["ServiceReportID"].ToString();
                ViewBag.DefaultStatus = dt_chartResponsesInfoGrid.AsEnumerable().Count(row => row.Field<string>("Status") == "Default");
                ViewBag.EventID = dt_chartResponsesInfoGrid.Rows[0]["EventID"].ToString();
                ViewBag.Purpose = dt_chartResponsesInfoGrid.Rows[0]["CHART_PURPOSE"].ToString();
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult Frm_Chart_Responses_Info_GridData_dx(string chartInstanceID = "", string regCenId = null, string QuestionFilter = "",bool FirstCall=false)
        {
            if (FirstCall == false)
            {
                chartResponsesInfoGridData(chartInstanceID, string.IsNullOrWhiteSpace(regCenId) ? null : regCenId, false, QuestionFilter);
            }
            return Content(JsonConvert.SerializeObject(dt_chartResponsesInfoGrid), "application/json");
        }

        public ActionResult ResponsesInfoGrid_dx_BatchUpdate(string editData = null)//By BK SOUMY
        {
            /*Service Forms DxdataGrid Conversion to Editable DxDataGrid*/
           
            /*Data from grid is coming accurately but updating to the server 
            * (business logic part) yet to be implemented */

            Return_Json_Param jsonParam = new Return_Json_Param();
            jsonParam.message = "Old and New data from editable Dxdatagrid are coming properly.";

            jsonParam.title = "Success!!";
            jsonParam.result = true;
            jsonParam.message = editData;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public ActionResult Frm_Chart_Responses_Info_SaveEdited_GridData_dx(string values, string key)
        {
            var jsonObject = JObject.Parse(values);
            Return_Json_Param jsonParam = new Return_Json_Param();
            Address_Book_ViewModel model = new Address_Book_ViewModel();
            var formData = ConvertJsonToFormDataCollection(values);
            model.Rec_ID = formData.Get("AB_ID") ?? null;
            DataTable d1 = BASE._Address_DBOps.GetRecord(model.Rec_ID);
            if (!Convert.IsDBNull(d1.Rows[0]["C_CEN_CATEGORY"]))
            {
                if (d1.Rows[0]["C_CEN_CATEGORY"].ToString().Length > 0)
                {                   
                    int cencategory = Convert.ToInt32(d1.Rows[0]["C_CEN_CATEGORY"]);
                    model.Cmb_Cen_Cagetory_AB = cencategory == 1 ? 1 : 0;                 
                }
            }
            if (!Convert.IsDBNull(d1.Rows[0]["C_CLASS_CEN_ID"]))
            {
                if (d1.Rows[0]["C_CLASS_CEN_ID"].ToString().Length > 0)
                {
                    model.GLookUp_Cen_List_AB = Convert.ToString(d1.Rows[0]["C_CLASS_CEN_ID"]);
                }
            }
            if (!Convert.IsDBNull(d1.Rows[0]["C_ORG_REC_ID"]))
            {
                if (d1.Rows[0]["C_ORG_REC_ID"].ToString().Length > 0)
                {
                    model.Org_RecID = Convert.ToString(d1.Rows[0]["C_ORG_REC_ID"]);
                }
            }
            if (!Convert.IsDBNull(d1.Rows[0]["C_BK_TITLE"]))
            {
                if (d1.Rows[0]["C_BK_TITLE"].ToString().Length > 0)
                {
                    model.Com_BK_Title_AB = Convert.ToString(d1.Rows[0]["C_BK_TITLE"]);
                }
            }
            if (!Convert.IsDBNull(d1.Rows[0]["C_COD_YEAR_ID"]))
            {
                if (d1.Rows[0]["C_COD_YEAR_ID"].ToString().Length > 0)
                {
                    model.YearID = Convert.ToInt32(Convert.ToString(d1.Rows[0]["C_COD_YEAR_ID"]));
                }
            }
            if (!Convert.IsDBNull(d1.Rows[0]["C_STATUS"]))
            {
                if (d1.Rows[0]["C_STATUS"].ToString().Length > 0)
                {
                    model.Rad_Status_AB = d1.Rows[0]["C_STATUS"].ToString();
                }
            }
            string dateString = null;
            string isoFormattedADOBString = null;
            dateString = formData.Get("DATE_OF_COURSE") ?? null;//Invalid date formate aa rha hai idhar pr isko check krna hai
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime dob;
                if (DateTime.TryParseExact(dateString, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                {
                    // Format the date string back to ISO 8601 format
                    model.DE_DOB_A_AB = dob;
                    isoFormattedADOBString = dob.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Address")))
            {
                model.Txt_R_Add1_AB = formData.Get("Address");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("BK_TITLE")))
            {
                model.Com_BK_Title_AB = formData.Get("BK_TITLE");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Name")))
            {
                model.Txt_Name_AB = formData.Get("Name"); 
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Gender")))
            {
                model.Rad_Gender_AB = formData.Get("Gender");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Education")))
            {
                model.Txt_Education_AB = formData.Get("Education");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Occupation")))
            {
                model.Look_OccupationList_AB = formData.Get("Occupation");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("City")))
            {
                model.GLookUp_RCityList_AB = formData.Get("City");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("State")))
            {
                model.GLookUp_RStateList_AB = formData.Get("State");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Country")))
            {
                model.GLookUp_RCountryList_AB = formData.Get("Country");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("PIN")))
            {
                model.Txt_R_Pincode_AB = formData.Get("PIN");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Mob1")))
            {
                model.Txt_Mob_1_AB = formData.Get("Mob1");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Mob2")))
            {
                model.Txt_Mob_2_AB = formData.Get("Mob2");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Email1")))
            {
                model.Txt_Email1_AB = formData.Get("Email1");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Email2")))
            {
                model.Txt_Email2_AB = formData.Get("Email2");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Course")))
            {
                model.Rad_Status_AB = formData.Get("Course");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Centre")))
            {
                model.GLookUp_Cen_List_AB = formData.Get("Centre");
            }
            if (string.IsNullOrWhiteSpace(formData.Get("CenCategory")) == false)
            {
                if (formData.Get("CenCategory").Equals("False"))
                {
                    model.Cmb_Cen_Cagetory_AB = 0;
                }
                else if (formData.Get("CenCategory").Equals("True"))
                {
                    model.Cmb_Cen_Cagetory_AB = 1;
                }
            }
            DataTable dAdd = BASE._Form_dbops.Get_AddressBook_AdditionalInfo(model.Rec_ID);
            string mobile3 = null; string mobile4 = null; string mobile5 = null; string email3 = null; string email4 = null; string email5 = null; string profilePicPath = null;
            if (!Convert.IsDBNull(dAdd.Rows[0]["C_MOB_NO_3"]))
            {
                if (dAdd.Rows[0]["C_MOB_NO_3"].ToString().Length > 0)
                {
                    mobile3 = Convert.ToString(dAdd.Rows[0]["C_MOB_NO_3"]);
                }
            }
            if (!Convert.IsDBNull(dAdd.Rows[0]["C_MOB_NO_4"]))
            {
                if (dAdd.Rows[0]["C_MOB_NO_4"].ToString().Length > 0)
                {
                    mobile4 = Convert.ToString(dAdd.Rows[0]["C_MOB_NO_4"]);
                }
            }
            if (!Convert.IsDBNull(dAdd.Rows[0]["C_MOB_NO_5"]))
            {
                if (dAdd.Rows[0]["C_MOB_NO_5"].ToString().Length > 0)
                {
                    mobile5 = Convert.ToString(dAdd.Rows[0]["C_MOB_NO_5"]);
                }
            }
            if (!Convert.IsDBNull(dAdd.Rows[0]["C_EMAIL_ID_3"]))
            {
                if (dAdd.Rows[0]["C_EMAIL_ID_3"].ToString().Length > 0)
                {
                    email3 = Convert.ToString(dAdd.Rows[0]["C_EMAIL_ID_3"]);
                }
            }
            if (!Convert.IsDBNull(dAdd.Rows[0]["C_EMAIL_ID_4"]))
            {
                if (dAdd.Rows[0]["C_EMAIL_ID_4"].ToString().Length > 0)
                {
                    email4 = Convert.ToString(dAdd.Rows[0]["C_EMAIL_ID_4"]);
                }
            }
            if (!Convert.IsDBNull(dAdd.Rows[0]["C_EMAIL_ID_5"]))
            {
                if (dAdd.Rows[0]["C_EMAIL_ID_5"].ToString().Length > 0)
                {
                    email5 = Convert.ToString(dAdd.Rows[0]["C_EMAIL_ID_5"]);
                }
            }
            if (!Convert.IsDBNull(dAdd.Rows[0]["C_PROFILE_PIC_FILENAME"]))
            {
                if (dAdd.Rows[0]["C_PROFILE_PIC_FILENAME"].ToString().Length > 0)
                {
                    profilePicPath = Convert.ToString(dAdd.Rows[0]["C_PROFILE_PIC_FILENAME"]);
                }
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Mob3")))
            {
                mobile3 = formData.Get("Mob3");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Mob4")))
            {
                mobile4 = formData.Get("Mob4");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Mob5")))
            {
                mobile5 = formData.Get("Mob5");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Email3")))
            {
                email3 = formData.Get("Email3");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Email4")))
            {
                email4 = formData.Get("Email4");
            }
            if (!string.IsNullOrWhiteSpace(formData.Get("Email5")))
            {
                email5 = formData.Get("Email5");
            }
            int age = 0;
            if (!string.IsNullOrWhiteSpace(formData.Get("Age")))
            {
                age = Convert.ToInt16(formData.Get("Age"));
            }
            //if (!string.IsNullOrWhiteSpace(formData.Get("ProfilePicPath")))//Data in address_book_additional_info table in DB is differen with repect to whatever data is coming from UI(UI data contains path)
            //{
            //    profilePicPath = formData.Get("ProfilePicPath");
            //}
            if (!string.IsNullOrWhiteSpace(formData.Get("Specialities")))
            {
                model.Specialities = new List<Specialitites>();
                string[] listSpecialilty = formData.Get("Specialities").Split(',');
                foreach (string item in listSpecialilty)
                {
                    model.Specialities.Add(new Specialitites() { Specialities_Misc_ID = item, Selected=true });
                }
            }
            foreach (var x in jsonObject)
            {
                string name = x.Key;
                string value = x.Value.ToString();
                switch (x.Key)
                {                                                                               
                    case "ID_NAME":
                        if (value.Trim().Equals("Voter ID No."))
                        {
                            model.Txt_VoterID_AB = formData.Get("ID_NO");
                        }
                        else if (value.Trim().Equals("Passport No."))
                        {
                            model.Txt_Passport_No_AB = formData.Get("ID_NO") ;
                        }
                        else if (value.Trim().Equals("PAN No."))
                        {
                            model.Txt_PAN_No_AB = formData.Get("ID_NO") ;
                        }
                        else if (value.Trim().Equals("Driving License No."))
                        {
                            model.Txt_DLNo_AB = formData.Get("ID_NO") ;
                        }
                        else if (value.Trim().Equals("Aadhar No."))
                        {
                            model.Txt_UID_AB = formData.Get("ID_NO") ;
                        }
                        break;                                            
                }
            }
            
            Param_Txn_Update_Addresses EditParam = new Param_Txn_Update_Addresses();
            Parameter_Update_Addresses UpParam = new Parameter_Update_Addresses();
            Common_Lib.RealTimeService.Param_Insert_additional_info_address additional_Info_Address = new Common_Lib.RealTimeService.Param_Insert_additional_info_address();            
            string isoFormattedLDOBString = null;
            if (age > 0)
            {
                UpParam.Remarks1 = "Age As On " + new DateTime(DateTime.Now.Year, 01, 01).ToString("dd/MM/yyyy") + " : " + age;
                DateTime dob = new DateTime(DateTime.Now.Year, 01, 01).AddYears(-age);
                UpParam.LokikDob = dob.ToString(BASE._Server_Date_Format_Long);//already in ISO format so no need to isoFormattedLDOBString.ToString(BASE._Server_Date_Format_Long);
            }
            dateString = formData.Get("DOB") ?? null;
            if (!string.IsNullOrEmpty(formData.Get("DOB")))
            {
                DateTime dob;
                if (DateTime.TryParseExact(dateString, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                {
                    // Format the date string back to ISO 8601 format
                    model.DE_DOB_L_AB = dob;
                    isoFormattedLDOBString = dob.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);//already in ISO format so no need to isoFormattedLDOBString.ToString(BASE._Server_Date_Format_Long);
                    UpParam.LokikDob = isoFormattedLDOBString;
                }
            }
            
            UpParam.Rec_ID = model.Rec_ID;
            UpParam.BKTitle = model.Com_BK_Title_AB ?? null;
            UpParam.Name = model.Txt_Name_AB ?? null;
            UpParam.Gender = model.Rad_Gender_AB ?? null;
            UpParam.OccupationID = model.Look_OccupationList_AB ?? null;
            UpParam.Education = model.Txt_Education_AB ?? null;
            UpParam.CenCategory = Convert.ToInt32(model.Cmb_Cen_Cagetory_AB);
            UpParam.PANNo = model.Txt_PAN_No_AB ?? null;
            UpParam.UID = model.Txt_UID_AB ?? null;
            UpParam.VoterID = model.Txt_VoterID_AB ?? null;
            UpParam.DLNo = model.Txt_DLNo_AB ?? null;
            UpParam.PassportNo = model.Txt_Passport_No_AB ?? null;
            UpParam.Res_Add1 = model.Txt_R_Add1_AB ?? null;
            UpParam.Res_cityID = model.GLookUp_RCityList_AB??null;
            UpParam.Res_StateID = model.GLookUp_RStateList_AB ?? null;
            UpParam.Res_CountryID = model.GLookUp_RCountryList_AB ?? null;
            UpParam.Res_PinCode = model.Txt_R_Pincode_AB ?? null;
            UpParam.Mob1 = model.Txt_Mob_1_AB ?? null;
            UpParam.Mob2 = model.Txt_Mob_2_AB ?? null;
            UpParam.Email1 = model.Txt_Email1_AB ?? null;
            UpParam.Email2 = model.Txt_Email2_AB ?? null;
            UpParam.Status = model.Rad_Status_AB ?? null;
            UpParam.AlokikDOB = isoFormattedADOBString ?? null;//Convert.ToDateTime(model.DE_DOB_A_AB).ToString(BASE._Server_Date_Format_Long);
            UpParam.ClassCID = model.GLookUp_Cen_List_AB ?? null;
            UpParam.YearID = model.YearID;

            additional_Info_Address.Mobile3 = mobile3;
            additional_Info_Address.Mobile4 = mobile4;
            additional_Info_Address.Mobile5 = mobile5;
            additional_Info_Address.Email3 = email3;
            additional_Info_Address.Email4 = email4;
            additional_Info_Address.Email5 = email5;
            additional_Info_Address.File_Name = profilePicPath;
            additional_Info_Address.AB_Rec_ID = model.Rec_ID;
            additional_Info_Address.Rec_ID = System.Guid.NewGuid().ToString();
            EditParam.RecID_DeleteAdditionalAddress = model.Rec_ID;

            string SelectedItems ="";int I = 0;

            if (model.Specialities != null)
            {
                for (; I < model.Specialities.Count(); I++)
                {
                    if (model.Specialities[I].Selected == true)
                    {
                        SelectedItems += model.Specialities[I].Specialities_Misc_ID + " | ";
                    }
                }
            }
            if (SelectedItems.Trim().Length > 0)
            {
                SelectedItems = SelectedItems.Trim().EndsWith("|") ? SelectedItems.Trim().Substring(0, SelectedItems.Trim().Length - 1) : SelectedItems.Trim();
            }
            string Chk_SpecialList_Tag = SelectedItems;
            if (model.Specialities != null)
            {
                UpParam.Specialities = Chk_SpecialList_Tag;
                EditParam.RecID_DeleteSpeciality = model.Rec_ID;
            }
            List<Parameter_InsertSpecialities_Addresses> UpSpecInfo = new List<Parameter_InsertSpecialities_Addresses>();
            if (model.Specialities != null && model.Specialities.Count > 0)
            {
                foreach (var currSelection in model.Specialities.Where(x => x.Selected == true))
                {
                    Parameter_InsertSpecialities_Addresses UpSpec = new Parameter_InsertSpecialities_Addresses();
                    UpSpec.AB_Rec_ID = model.Rec_ID;
                    UpSpec.Specialities_Misc_ID = currSelection.Specialities_Misc_ID;
                    UpSpec.Status_Action = ((int)Common.Record_Status._Completed).ToString();
                    UpSpec.Rec_ID = System.Guid.NewGuid().ToString();
                    UpSpecInfo.Add(UpSpec);
                }
            }
            EditParam.InsertSpecialities = UpSpecInfo.ToArray();
            EditParam.param_UpdateAddresses = UpParam;
            EditParam.param_AdditionalAddress = additional_Info_Address;
            if (BASE._Address_DBOps.UpdateAddresses_Txn(EditParam))
            {
                jsonParam.message = Messages.UpdateSuccess;
                jsonParam.title = "Form Response";
                jsonParam.result = true;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam,
                    xid = model.Rec_ID
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public FormDataCollection ConvertJsonToFormDataCollection(string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);
            if (jsonObject["Specialities"] != null)
            {
                jsonObject["Specialities"] = string.Join(",", jsonObject["Specialities"].ToObject<List<string>>());
            }
            var formData = new FormDataCollection(jsonObject.ToObject<Dictionary<string, string>>());
            return formData;
        }
        public User_Profile_Chart_Responses_ViewModel ConvertFormDataToModel(FormDataCollection formData)
        {
            User_Profile_Chart_Responses_ViewModel model = new User_Profile_Chart_Responses_ViewModel();
            var binder = new DefaultModelBinder();
            var bindingContext = new ModelBindingContext
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(User_Profile_Chart_Responses_ViewModel)),
                ValueProvider = new NameValueCollectionValueProvider(formData.ReadAsNameValueCollection(), null)
            };

            binder.BindModel(new ControllerContext(), bindingContext);
            return model;
        }

        #region Load State For Form Responses Edit DxDatagrid
        public ActionResult StateList_CR(string CountryAccessibleDescription = "IN")
        {
            if (string.IsNullOrWhiteSpace(CountryAccessibleDescription))
            {
                return Content(JsonConvert.SerializeObject(BASE._Address_DBOps.GetAllState()), "application/json");
            }
            else
            {
                var d1 = BASE._Address_DBOps.GetStates(CountryAccessibleDescription, "R_ST_NAME", "R_ST_CODE", "R_ST_REC_ID");
                d1 = d1.OrderBy(x => x.R_ST_NAME).ToList();
                return Content(JsonConvert.SerializeObject(d1), "application/json");
            }
        }
        public ActionResult GetCitiesWithSTandCO_Code(string StateAccessibleDescription, string CountryAccessibleDescription = "IN")
        {
            List<DbOperations.Return_CityList_With_StateandCountryCode> d1 = BASE._Address_DBOps.GetCitiesWithSTandCO_Code(CountryAccessibleDescription, StateAccessibleDescription, "CITY_NAME", "R_CI_REC_ID");
            d1 = d1.OrderBy(x => x.COUNTRY_NAME).ToList();
            return Content(JsonConvert.SerializeObject(d1), "application/json");
        }
        public ActionResult UserProfile_OccupationList_FrmResponse()
        {
            DataTable dt = BASE._Form_dbops.get_ChartResponseInGridProfileEdit_Occupation();
            DataView DV1 = new DataView(dt);
            DV1.RowFilter = " [MASTERID]='OCCUPATION' OR [MASTERID]='BLANK' ";
            DV1.Sort = "Name";
            return Content(JsonConvert.SerializeObject(DatatableToModel.DataTabletoTitle_INFO(DV1.ToTable())), "application/json");
        }
        public ActionResult UserProfile_SpecialityList_FrmResponse()
        {
            DataTable dt = BASE._Form_dbops.get_ChartResponseInGridProfileEdit_SpecialityOrHobby();
            DataView DV1 = new DataView(dt);
            DV1.RowFilter = " [MASTERID]='SPECIALTIES' OR [MASTERID]='BLANK' ";
            DV1.Sort = "Name";
            return Content(JsonConvert.SerializeObject(DatatableToModel.DataTabletoTitle_INFO(DV1.ToTable())), "application/json");
        }
        public ActionResult UserProfile_QualificationList_FrmResponse()
        {
            DataTable dt = BASE._Form_dbops.get_ChartResponseInGridProfileEdit_Qualification();
            DataView DV1 = new DataView(dt);
            DV1.RowFilter = " [MASTERID]='QUALIFICATIONS' OR [MASTERID]='BLANK' ";
            DV1.Sort = "Name";
            return Content(JsonConvert.SerializeObject(DatatableToModel.DataTabletoTitle_INFO(DV1.ToTable())), "application/json");
        }
        public ActionResult UserProfile_CountryList_FrmResponse()
        {
            DataTable d1 = BASE._Address_DBOps.GetCountries("R_CO_NAME", "R_CO_CODE", "R_CO_REC_ID");
            DataView dview = new DataView(d1);
            dview.Sort = "R_CO_NAME";                        
            return Content(JsonConvert.SerializeObject(DatatableToModel.DataTabletoCountry_INFO(dview.ToTable())), "application/json");
        }
        public ActionResult UserProfile_CentreList_FrmResponse(int? CenterCategory)
        {
            DataTable D1;
            if (CenterCategory == 0)
            {
                D1 = BASE._Address_DBOps.GetCenterList();
            }
            else
            {
                D1 = BASE._Address_DBOps.GetOverseasCenterList();
            }
            DataView dview = new DataView(D1);
            dview.Sort = "CEN_NAME";
            return Content(JsonConvert.SerializeObject(dview.ToTable()), "application/json");
        }
        #endregion
        public ActionResult Frm_Export_Options_Responses_dx()
        {
            return PartialView();
        }

        public ActionResult Frm_Export_Options_dx()
        {
            return PartialView();
        }

        #endregion
    }
}