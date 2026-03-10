using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Facility.Controllers
{
    public class ChartReportsController : BaseController
    {
        public DataTable dt_FoodsummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_FoodsummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_FoodsummaryGrid_ChartReports", value); }
        }
        public DataSet dt_FooddetailGrid
        {
            get { return (DataSet)GetBaseSession("dt_FooddetailGrid_ChartReports"); }
            set { SetBaseSession("dt_FooddetailGrid_ChartReports", value); }
        }
        public DataTable dt_GuestsummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_GuestsummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_GuestsummaryGrid_ChartReports", value); }
        }
        public DataTable dt_AccommSummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_AccommSummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_AccommSummaryGrid_ChartReports", value); }
        }
        public DataTable dt_AccommDetailGrid
        {
            get { return (DataTable)GetBaseSession("dt_AccommDetailGrid_ChartReports"); }
            set { SetBaseSession("dt_AccommDetailGrid_ChartReports", value); }
        }
        public DataTable dt_AccommChartGrid
        {
            get { return (DataTable)GetBaseSession("dt_AccommChartGrid_ChartReports"); }
            set { SetBaseSession("dt_AccommChartGrid_ChartReports", value); }
        }
        public DataTable dt_PointsSummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_PointsSummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_PointsSummaryGrid_ChartReports", value); }
        }
        public DataTable dt_RecommendationSummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_RecommendationSummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_RecommendationSummaryGrid_ChartReports", value); }
        }
        public DataTable dt_CenterwiseGuestSummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_CenterwiseGuestSummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_CenterwiseGuestSummaryGrid_ChartReports", value); }
        }
        public DataTable dt_SevadhariSummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_SevadhariSummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_SevadhariSummaryGrid_ChartReports", value); }
        }
        public DataTable dt_QuizResultSummaryGrid
        {
            get { return (DataTable)GetBaseSession("dt_QuizResultSummaryGrid_ChartReports"); }
            set { SetBaseSession("dt_QuizResultSummaryGrid_ChartReports", value); }
        }
        public ActionResult Frm_FoodSummary_Info(string cenId, string chartInstanceID = null, string fromdate = null, string todate = null, string fromtime = null, string totime = null, string eventid = null, string summary_type="FOOD SUMMARY", string SessionGUID = null,string Theme="")
        {              
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri+ "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.CentreId = cenId;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();   
            ViewBag.Theme = Theme;
            dt_FoodsummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID)?(int?)null:Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, null, eventid).Tables[0];
     
            return View(dt_FoodsummaryGrid);
        }
        public ActionResult Frm_FoodSummary_Info_Grid(string command, int ShowHorizontalBar = 1, string Chart_Instance_ID = null, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string fromdate = null, string todate = null, string EventID = null,  string summary_type = "FOOD SUMMARY", string SessionGUID = null)
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_FoodsummaryGrid == null)
            {
                dt_FoodsummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID).Tables[0];
            }
                       
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

            return View(dt_FoodsummaryGrid);
        }
        public ActionResult Frm_FoodDetail_Info(string cenId, string chartInstanceID = null, string fromdate = null, string todate = null, string fromtime = null, string totime = null, 
            string eventid = null, string summary_type = "FOOD DETAIL", string SessionGUID = null, string Theme = "")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else 
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.CentreId = cenId;
            ViewBag.summary_type = summary_type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            dt_FooddetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID) ? (int?)null : Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, null, eventid);   
            return View(dt_FooddetailGrid);
        }
        public ActionResult Frm_FoodDetail_Info_Grid(string command, int ShowHorizontalBar = 1, string Chart_Instance_ID = null, string Layout = null, bool VouchingMode = false, 
            string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string fromdate = null, string todate = null,
            string EventID = null, string summary_type = "FOOD DETAIL", string SessionGUID = null)
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_FooddetailGrid == null)
            {
                dt_FooddetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID);
            }
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

            return View(dt_FooddetailGrid.Tables[0]);
        }
        public ActionResult Frm_FoodDetail_Info_Arrival_Grid(string command, int ShowHorizontalBar = 1, string Chart_Instance_ID = null, string Layout = null, bool VouchingMode = false,
            string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string fromdate = null, string todate = null,
            string EventID = null, string summary_type = "FOOD DETAIL", string SessionGUID = null)
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_FooddetailGrid == null)
            {
                dt_FooddetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID);
            }
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

            return View(dt_FooddetailGrid.Tables[1]);
        }
        public ActionResult Frm_FoodDetail_NextTenDays_Info(string cenId, string chartInstanceID = null, string fromdate = null, string todate = null, string fromtime = null, 
            string totime = null, string eventid = null, string summary_type = "FOOD DETAILS NEXT TEN DAYS", string SessionGUID = null, string Theme = "")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.CentreId = cenId;
            ViewBag.summary_type = summary_type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            dt_FooddetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID) ? (int?)null : Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, null, eventid);
            return View(dt_FooddetailGrid);
        }
        public ActionResult Frm_FoodDetail_NextTenDays_Info_Grid(string command, int ShowHorizontalBar = 1, string Chart_Instance_ID = null, string Layout = null, 
            bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", 
            string fromdate = null, string todate = null, string EventID = null, string summary_type = "FOOD DETAILS NEXT TEN DAYS", string SessionGUID = null)
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_FooddetailGrid == null)
            {
                dt_FooddetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID);
            }
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

            return View(dt_FooddetailGrid.Tables[0]);
        }
        public ActionResult Frm_FoodDetail_NextTenDays_Info_Arrival_Grid(string command, int ShowHorizontalBar = 1, string Chart_Instance_ID = null, string Layout = null, 
            bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "",
            string fromdate = null, string todate = null, string EventID = null, string summary_type = "FOOD DETAILS NEXT TEN DAYS", string SessionGUID = null)
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_FooddetailGrid == null)
            {
                dt_FooddetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID);
            }
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

            return View(dt_FooddetailGrid.Tables[1]);
        }

        public ActionResult Frm_GuestSummary_Info(string cenId, string summary_type= "GUEST SUMMARY", string chartInstanceID = null, string fromdate = null, string todate = null,
     string fromtime = null, string totime = null, string eventid = null, string SessionGUID = null, string Theme = "")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewBag.CentreId = cenId;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            dt_GuestsummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID)?(int?)null:Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, null, eventid).Tables[0];
         
            return View(dt_GuestsummaryGrid);
        }
        public ActionResult Frm_GuestSummary_Info_Grid(string command, string summary_type= "GUEST SUMMARY", int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_GuestsummaryGrid == null)
            {
                dt_GuestsummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, null).Tables[0];
            }
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

            return View(dt_GuestsummaryGrid);
        }
        public ActionResult Frm_SevadhariSummary_Info(string cenId, string summary_type = "SEVADHARI SUMMARY", string chartInstanceID = null, string fromdate = null, string todate = null,
     string fromtime = null, string totime = null, string eventid = null, string SessionGUID = null, string Theme = "")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewBag.CentreId = cenId;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            dt_SevadhariSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID) ? (int?)null : Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, null, eventid).Tables[0];

            return View(dt_SevadhariSummaryGrid);
        }
        public ActionResult Frm_SevadhariSummary_Info_Grid(string command, string summary_type = "SEVADHARI SUMMARY", int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_SevadhariSummaryGrid == null)
            {
                dt_SevadhariSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, null).Tables[0];
            }
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

            return View(dt_SevadhariSummaryGrid);
        }
        public ActionResult Frm_AccommodationSummary_Info(string cenId, string summary_type= "ACCOM SUMMARY", string chartInstanceID = null, string fromdate = null, string todate = null,
       string fromtime = null, string totime = null, string eventid = null, string SessionGUID = null, string Theme = "")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewBag.CentreId = cenId;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            dt_AccommSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID) ? (int?)null : Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, null, eventid).Tables[0];
        
            return View(dt_AccommSummaryGrid);
        }

        public ActionResult Frm_AccommodationSummary_Info_Grid(string command, string summary_type = "ACCOM SUMMARY", int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
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
            if (EventID == "" || EventID == "null") { EventID = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_AccommSummaryGrid == null)
            {
                dt_AccommSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID).Tables[0];
            }
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

            return View(dt_AccommSummaryGrid);
        }
        public ActionResult Frm_AccommodationDetails_Info(string cenId, string summary_type= "ACCOM DETAIL", string chartInstanceID = null, string fromdate = null, string todate = null,
     string fromtime = null, string totime = null, string eventid = null, string buildingid = null, string SessionGUID = null, string Theme = "")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);                
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
            eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
            buildingid = string.IsNullOrWhiteSpace(buildingid) ? null : buildingid;
            if (buildingid == "" || buildingid == "null") { buildingid = null; }
            if (eventid == "" || eventid == "null") { eventid = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.buildingid = buildingid;
            ViewBag.eventid = eventid;
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewBag.CentreId = cenId;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            dt_AccommDetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID) ? (int?)null : Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, buildingid, eventid).Tables[0];
            return View(dt_AccommDetailGrid);
        }

        public ActionResult Frm_AccommodationDetails_Info_Grid(string command, string summary_type, int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
            string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",
            string RowKeyToFocus = "", string fromdate = null, string todate = null, string buildingid = null, string EventId = null)
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
            if (buildingid == "" || buildingid == "null") { buildingid = null; }
            if (EventId == "" || EventId == "null") { EventId = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_AccommDetailGrid == null)
            {
                dt_AccommDetailGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, buildingid, EventId).Tables[0];
            }
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

            return View(dt_AccommDetailGrid);
        }
        public ActionResult Frm_AccommodationChart_Info(string cenId, string summary_type = "ACCOM CHART", string chartInstanceID = null, string fromdate = null, string todate = null,
string fromtime = null, string totime = null, string eventid = null, string buildingid = null, string SessionGUID = null, string Theme = "")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
            eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
            buildingid = string.IsNullOrWhiteSpace(buildingid) ? null : buildingid;
            if (buildingid == "" || buildingid == "null") { buildingid = null; }
            if (eventid == "" || eventid == "null") { eventid = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.buildingid = buildingid;
            ViewBag.eventid = eventid;
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewBag.CentreId = cenId;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            dt_AccommChartGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID) ? (int?)null : Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, buildingid, eventid).Tables[0];
            return View(dt_AccommChartGrid);
        }

        public ActionResult Frm_AccommodationChart_Info_Grid(string command, string summary_type, int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
            string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",
            string RowKeyToFocus = "", string fromdate = null, string todate = null, string buildingid = null, string EventId = null)
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
            if (buildingid == "" || buildingid == "null") { buildingid = null; }
            if (EventId == "" || EventId == "null") { EventId = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_AccommChartGrid == null)
            {
                dt_AccommChartGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, buildingid, EventId).Tables[0];
            }
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

            return View(dt_AccommChartGrid);
        }
        public ActionResult Frm_PointsSummary_Info(string cenId, string summary_type = "POINTS SUMMARY", string chartInstanceID = null, string fromdate = null, string todate = null,
string fromtime = null, string totime = null, string eventid = null, string buildingid = null, string SessionGUID = null, string Theme = "",string FormName="")
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                BASE._open_Cen_ID = Convert.ToInt32(cenId);
                BASE._open_User_ID = cenId;
                BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                    BASE._open_Cen_ID = Convert.ToInt32(cenId);
                    BASE._open_User_ID = cenId;
                    BASE._open_Year_ID = DateTime.Now.GetFinancialYear();
                    BASE._open_Year_Name = DateTime.Now.GetFinancialYearName();
                }
            }
            //Return_Json_Param jsonParam = new Return_Json_Param();
            if (fromdate == "null") { fromdate = null; }
            if (todate == "null") { todate = null; }
            if (fromtime == "null") { fromtime = null; }
            if (totime == "null") { totime = null; }
            if (chartInstanceID == "null") { chartInstanceID = null; }
            DateTime? startdate = string.IsNullOrWhiteSpace(fromdate) ? (DateTime?)null : Convert.ToDateTime(fromdate).Date;
            DateTime? enddate = string.IsNullOrWhiteSpace(todate) ? (DateTime?)null : Convert.ToDateTime(todate).Date;
            DateTime? starttime = string.IsNullOrWhiteSpace(fromtime) ? (DateTime?)null : Convert.ToDateTime(fromtime);
            DateTime? endtime = string.IsNullOrWhiteSpace(totime) ? (DateTime?)null : Convert.ToDateTime(totime);
            eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
            buildingid = string.IsNullOrWhiteSpace(buildingid) ? null : buildingid;
            if (buildingid == "" || buildingid == "null") { buildingid = null; }
            if (eventid == "" || eventid == "null") { eventid = null; }
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
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.buildingid = buildingid;
            ViewBag.eventid = eventid;
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Facility_ChartResponsesInfo).ToString()) ? 1 : 0;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartInstanceID = chartInstanceID;
            ViewBag.summary_type = summary_type;
            ViewBag.CentreId = cenId;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            ViewBag.FormName = FormName;
            dt_PointsSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(chartInstanceID) ? (int?)null : Convert.ToInt32(chartInstanceID), summary_type, from_date, to_date, buildingid, eventid).Tables[0];
            return View(dt_PointsSummaryGrid);
        }

        public ActionResult Frm_PointsSummary_Info_Grid(string command, string summary_type, int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
            string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",
            string RowKeyToFocus = "", string fromdate = null, string todate = null, string buildingid = null, string EventId = null)
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
            if (buildingid == "" || buildingid == "null") { buildingid = null; }
            if (EventId == "" || EventId == "null") { EventId = null; }
            if (Chart_Instance_ID == "null") { Chart_Instance_ID = null; }
            if (command == "REFRESH" || dt_PointsSummaryGrid == null)
            {
                dt_PointsSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, string.IsNullOrWhiteSpace(Chart_Instance_ID) ? (int?)null : Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, buildingid, EventId).Tables[0];
            }
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

            return View(dt_PointsSummaryGrid);
        }

        public ActionResult Frm_RecommendationSummary_Info(string summary_type, string chartInstanceID = "", string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, string eventid = null)
        {

            //Return_Json_Param jsonParam = new Return_Json_Param();
            if(chartInstanceID == "null") { chartInstanceID = null; }
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
            string buildingid = null;
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
            dt_RecommendationSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(chartInstanceID), summary_type, startdate, enddate, buildingid, eventid).Tables[0];
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            return View(dt_RecommendationSummaryGrid);
        }

        public ActionResult Frm_RecommendationSummary_Info_Grid(string command, string summary_type, int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
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
            if (command == "REFRESH" || dt_RecommendationSummaryGrid == null)
            {
                dt_RecommendationSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID).Tables[0];
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

            return View(dt_RecommendationSummaryGrid);
        }
        
        public ActionResult Frm_CenterwiseGuestSummary_Info(string summary_type, string chartInstanceID = "", string fromdate = null, string todate = null,
            string fromtime = null, string totime = null, string eventid = null)
        {
            if (chartInstanceID == "null") { chartInstanceID = null; }
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
            string buildingid = null;
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
            dt_CenterwiseGuestSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(chartInstanceID), summary_type, startdate, enddate, buildingid, eventid).Tables[0];
            DateTime? from_date = startdate == null ? DateTime.Today : startdate;
            DateTime? to_date = enddate == null ? DateTime.Today.AddDays(1) : enddate;
            ViewBag.fromdate = Convert.ToDateTime(from_date);
            ViewBag.todate = Convert.ToDateTime(to_date);
            ViewBag.eventid = eventid;
            return View(dt_CenterwiseGuestSummaryGrid);
        }

        public ActionResult Frm_CenterwiseGuestSummary_Info_Grid(string command, string summary_type, int ShowHorizontalBar = 1, string Chart_Instance_ID = "",
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
            if (command == "REFRESH" || dt_CenterwiseGuestSummaryGrid == null)
            {
                dt_CenterwiseGuestSummaryGrid = BASE._Form_dbops.get_chartResponsesummary(BASE._open_Cen_ID, Convert.ToInt32(Chart_Instance_ID), summary_type, startdate, enddate, null, EventID).Tables[0];
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

            return View(dt_CenterwiseGuestSummaryGrid);
        }
        public ActionResult Frm_Quiz_Result_Info(string summary_type = "Quiz Result", string chartID = null, string eventid = null, string SessionGUID = null, string Theme = "", string FormName="", string EventName="", string EventFromDate = null, string EventToDate = null)
        {
            if (string.IsNullOrWhiteSpace(SessionGUID))
            {
                var AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var MyGuid = string.IsNullOrEmpty(SessionGUID) ? Guid.NewGuid() : Guid.Parse(SessionGUID);
                AllBASEs.Add(new BaseModel
                {
                    CenterGuid = MyGuid,
                    BASE = BASE
                });
                SessionGUID = MyGuid.ToString();
                Session["BASEClass"] = AllBASEs;
                Response.Redirect(Request.Url.AbsoluteUri + "&SessionGUID=" + SessionGUID);
            }
            else
            {
                List<BaseModel> AllBASEs = Session["BASEClass"] == null ? new List<BaseModel>() : Session["BASEClass"] as List<BaseModel>;
                var basedata = AllBASEs.FirstOrDefault(x => x.CenterGuid == Guid.Parse(SessionGUID.ToString()));
                if (basedata == null)
                {
                    AllBASEs.Add(new BaseModel
                    {
                        CenterGuid = Guid.Parse(SessionGUID),
                        BASE = BASE
                    });
                    Session["BASEClass"] = AllBASEs;
                }
            }
            if (chartID == "null") { chartID = null; }
            if (string.IsNullOrWhiteSpace(FormName)||FormName.Equals("null")) { FormName = null; }
            if (string.IsNullOrWhiteSpace(EventName) || EventName.Equals("null")) { EventName = null; }
            if (string.IsNullOrWhiteSpace(EventFromDate) || EventFromDate.Equals("null")) { EventFromDate = null; }
            if (string.IsNullOrWhiteSpace(EventToDate) || EventToDate.Equals("null")) { EventToDate = null; }
            if (string.IsNullOrWhiteSpace(summary_type)||summary_type.Equals("null")) { summary_type = "Quiz Result"; }
            eventid = string.IsNullOrWhiteSpace(eventid) ? null : eventid;
            if (eventid == "" || eventid == "null") { eventid = null; }
            ViewBag.eventid = eventid;
            ViewBag.ShowHorizontalBar = 1;
            ViewBag.chartID = chartID;
            ViewBag.summary_type = summary_type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Theme = Theme;
            ViewBag.FormName = FormName;
            ViewBag.EventName = EventName;
            ViewBag.EventFromDate = EventFromDate;
            ViewBag.EventToDate = EventToDate;
            dt_QuizResultSummaryGrid = BASE._Form_dbops.get_QuizResult((string.IsNullOrWhiteSpace(chartID) ? (int?)null : Convert.ToInt32(chartID)), eventid);
            return View(dt_QuizResultSummaryGrid);
        }
        public ActionResult Frm_Quiz_Result_Info_Grid(string command, string summary_type, int ShowHorizontalBar = 1,
            string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",
            string RowKeyToFocus = "", string EventID = null, string Chart_ID = "")
        {
            
            if (command == "REFRESH" || dt_QuizResultSummaryGrid == null)
            {
                dt_QuizResultSummaryGrid = BASE._Form_dbops.get_QuizResult((string.IsNullOrWhiteSpace(Chart_ID) ? (int?)null : Convert.ToInt32(Chart_ID)), EventID);
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

            return View(dt_QuizResultSummaryGrid);
        }
        public ActionResult Add_Notification_Quiz_Result(string IDs, string EventName = "", string EventFromDate = null, string EventToDate = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            string message = null;
            string[] arr_id = IDs.Split(',');
            for (int i = 0; i < arr_id.Length; i++)
            {
                DataRow[] filteredRows = dt_QuizResultSummaryGrid.Select("ID = '" + arr_id[i] + "'");
                foreach (DataRow row in filteredRows)
                {
                    if (Double.Parse(row["Marks"].ToString()) >= 75.00)
                    {
                        if (EventFromDate.Length > 0)
                        {
                            message = "ॐ शांति " + row["NAME"].ToString() + "<br>मधुबन एकाउंट्स डिपार्टमेंट द्वारा " + EventFromDate + " से " + EventToDate + " को आयोजित "+EventName+" में भाग लेने के लिए आपका धन्यवाद।<br>आपने उपरोक्त सहयोगमूर्त(ARP Level) ट्रेनिंग को 100 में से " + row["Marks"].ToString() + " अंक लेकर उत्तीर्ण कर लिया है।  आपको हार्दिक बधाइयां। उम्मीद है यह ट्रेनिंग आपको भविष्य में यज्ञ का कारोबार सुचारू रूप से चलने में मददगार होगी।<br><br> भविष्य में आयोजन की जाने वाली 'सफलतामूर्त (Advance ARP Level)' ट्रेनिंग में आप भाग ले सकते हैं। आप स्व-इच्छा से 'सहयोगमूर्त (ARP Level)' ट्रेनिंग में दुबारा भाग लेना चाहें तो भी आपका स्वागत है।<br><br> ईश्वरीय सेवाधारी,<br> बीके CA ललित<br>सबका भला हो सब सुख पाएं";
                        }
                        else
                        {
                            if (EventName.Length > 0)
                            {
                                message = "ॐ शांति " + row["NAME"].ToString() + "<br>मधुबन एकाउंट्स डिपार्टमेंट द्वारा आयोजित " + EventName + " में भाग लेने के लिए आपका धन्यवाद।<br>आपने उपरोक्त सहयोगमूर्त(ARP Level) ट्रेनिंग को 100 में से " + row["Marks"].ToString() + " अंक लेकर उत्तीर्ण कर लिया है।  आपको हार्दिक बधाइयां। उम्मीद है यह ट्रेनिंग आपको भविष्य में यज्ञ का कारोबार सुचारू रूप से चलने में मददगार होगी।<br><br> भविष्य में आयोजन की जाने वाली 'सफलतामूर्त (Advance ARP Level)' ट्रेनिंग में आप भाग ले सकते हैं। आप स्व-इच्छा से 'सहयोगमूर्त (ARP Level)' ट्रेनिंग में दुबारा भाग लेना चाहें तो भी आपका स्वागत है।<br><br> ईश्वरीय सेवाधारी,<br> बीके CA ललित<br>सबका भला हो सब सुख पाएं";
                            }
                            else
                            {
                                message = "ॐ शांति " + row["NAME"].ToString() + "<br>मधुबन एकाउंट्स डिपार्टमेंट द्वारा आयोजित 'सहयोगमूर्त (ARP Level)' ट्रेनिंग में भाग लेने के लिए आपका धन्यवाद।<br>आपने ज्ञानमूर्त(Beginner Level) ट्रेनिंग को 100 में से "+ row["Marks"].ToString() + " अंक लेकर उत्तीर्ण कर लिया है।  आपको हार्दिक बधाइयां। उम्मीद है यह ट्रेनिंग आपको भविष्य में यज्ञ का कारोबार सुचारू रूप से चलने में मददगार होगी।<br><br> भविष्य में आयोजन की जाने वाली 'आधार मूर्त (Basic Level)' ट्रेनिंग में आप भाग ले सकते हैं। <br><br> ईश्वरीय सेवाधारी,<br> बीके CA ललित<br>सबका भला हो सब सुख पाएं";
                            }
                        }
                        message = message.ConvertHtmlToWhatsappText();
                    }
                    else if (Double.Parse(row["Marks"].ToString()) < 75.00)
                    {
                        if (EventFromDate.Length > 0)
                        {
                            message = "ॐ शांति " + row["NAME"].ToString() + "<br>मधुबन एकाउंट्स डिपार्टमेंट द्वारा " + EventFromDate + " से " + EventToDate + " को आयोजित "+ EventName+" में भाग लेने के लिए आपका धन्यवाद।  <br> आपने उपरोक्त सहयोगमूर्त (ARP Level) ट्रेनिंग में 100 में से " + row["Marks"].ToString() + " अंक ही प्राप्त किए हैं। अगले लेवल की ट्रेनिंग करने के लिए कम से कम 75 अंकों की जरूरत पड़ती है।  <br> अत: <b>आप भविष्य में सहयोगमूर्त (ARP Level) ट्रेनिंग पुनः अटेंड करें और उसमें 75 अंकों से ज्यादा अंक प्राप्त कर उत्तीर्ण करें</b> ताकि आप भविष्य में 'सफलता मूर्त (Advanced ARP Level)' ट्रेनिंग में भाग ले सकें। यज्ञ के इस कारोबार को सुचारू रूप में चलाने के लिए आपका आभार।<br><br> ईश्वरीय सेवाधारी,<br> बीके CA ललित<br>सबका भला हो सब सुख पाएं";
                        }
                        else
                        {
                            if (EventName.Length > 0)
                            {
                                message = "ॐ शांति " + row["NAME"].ToString() + "<br>मधुबन एकाउंट्स डिपार्टमेंट द्वारा आयोजित " + EventName + " में भाग लेने के लिए आपका धन्यवाद।  <br> आपने उपरोक्त ज्ञानमूर्त(Beginner Level) ट्रेनिंग में 100 में से " + row["Marks"].ToString() + " अंक ही प्राप्त किए हैं। अगले लेवल की ट्रेनिंग करने के लिए कम से कम 75 अंकों की जरूरत पड़ती है।  <br> अत: <b>आप भविष्य में ज्ञानमूर्त(Beginner Level) ट्रेनिंग पुनः अटेंड करें और उसमें 75 अंकों से ज्यादा अंक प्राप्त कर उत्तीर्ण करें</b> ताकि आप भविष्य में 'सहयोगमूर्त (ARP Level)' ट्रेनिंग में भाग ले सकें। यज्ञ के इस कारोबार को सुचारू रूप में चलाने के लिए आपका आभार।<br><br> ईश्वरीय सेवाधारी,<br> बीके CA ललित<br>सबका भला हो सब सुख पाएं";
                            }
                            else
                            {
                                message = "ॐ शांति " + row["NAME"].ToString() + "<br>मधुबन एकाउंट्स डिपार्टमेंट द्वारा आयोजित 'सहयोगमूर्त (ARP Level)' ट्रेनिंग में भाग लेने के लिए आपका धन्यवाद।  <br> आपने ज्ञानमूर्त(Beginner Level) ट्रेनिंग में 100 में से " + row["Marks"].ToString() + " अंक ही प्राप्त किए हैं। अगले लेवल की ट्रेनिंग करने के लिए कम से कम 75 अंकों की जरूरत पड़ती है।  <br> अत: <b>आप भविष्य में ज्ञानमूर्त(Beginner Level) ट्रेनिंग पुनः अटेंड करें और उसमें 75 अंकों से अंक ज्यादा प्राप्त कर उत्तीर्ण करें</b> ताकि आप भविष्य में 'सहयोगमूर्त (ARP Level)' ट्रेनिंग में भाग ले सकें। यज्ञ के इस कारोबार को सुचारू रूप में चलाने के लिए आपका आभार।<br><br> ईश्वरीय सेवाधारी,<br> बीके CA ललित<br>सबका भला हो सब सुख पाएं";
                            }
                        }
                        message = message.ConvertHtmlToWhatsappText();
                    }
                    BASE._Notifications_DBOps.InsertWhatsappQueue(row["MOB"].ToString(), message, EventName + "_QUIZRESULT", null, null, ConfigurationManager.AppSettings["DefaultWhatsAppSender"]);
                }
            }
            jsonParam.message = arr_id.Length+" Whatsapp Messages has been Queued Successfully!!";
            jsonParam.title = "Success";
            jsonParam.result = true;
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options(string GridName) 
        {
            ViewBag.GridName = GridName;
            return View();
        }
        public void SessionClearChartReports(string SummaryType)
        {
            if (SummaryType == "GUEST SUMMARY")
            {
                BASE._SessionDictionary.Remove("dt_GuestsummaryGrid_ChartReports");
            }
            else if (SummaryType == "SEVADHARI SUMMARY")
            {
                BASE._SessionDictionary.Remove("dt_SevadhariSummaryGrid_ChartReports");
            }
            else if(SummaryType == "ACCOM SUMMARY")
            {
                BASE._SessionDictionary.Remove("dt_AccommSummaryGrid_ChartReports");
            }
            else if (SummaryType == "ACCOM DETAIL")
            {
                BASE._SessionDictionary.Remove("dt_AccommDetailGrid_ChartReports");
            }
            else if (SummaryType == "FOOD SUMMARY")
            {
                BASE._SessionDictionary.Remove("dt_FoodsummaryGrid_ChartReports");
            }
            else if (SummaryType == "FOOD DETAIL")
            {
                BASE._SessionDictionary.Remove("dt_FooddetailGrid_ChartReports");
            }
            else if (SummaryType == "ACCOM CHART")
            {
                BASE._SessionDictionary.Remove("dt_AccommChartGrid_ChartReports");
            }
            else if (SummaryType == "POINTS SUMMARY")
            {
                BASE._SessionDictionary.Remove("dt_PointsSummaryGrid_ChartReports");
            }
            else if (SummaryType == "Quiz Result")
            {
                BASE._SessionDictionary.Remove("dt_QuizResultSummaryGrid_ChartReports");
            }
            else
            {
                ClearBaseSession("_ChartReports");
            }
        }
    }
}