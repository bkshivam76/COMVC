using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Help.Models;
using ConnectOneMVC.Helper;
using System.Data;
using System.Web.Configuration;

namespace ConnectOneMVC.Areas.Help.Controllers
{
    public class SchedulerController : BaseController
    {
        // GET: Help/Scheduler
        public DataTable Scheduler_InfoData
        {
            get { return (DataTable)GetBaseSession("Scheduler_InfoData_Scheduler"); }
            set { SetBaseSession("Scheduler_InfoData_Scheduler", value); }
        }
        public DataTable Scheduler_Instance_ExportData
        {
            get { return (DataTable)GetBaseSession("Scheduler_Instance_ExportData_Scheduler"); }
            set { SetBaseSession("Scheduler_Instance_ExportData_Scheduler", value); }
        }
        public DataTable Scheduler_QueueData
        {
            get { return (DataTable)GetBaseSession("Scheduler_QueueData_SchedulerQueue"); }
            set { SetBaseSession("Scheduler_QueueData_SchedulerQueue", value); }
        }
        public DataTable Scheduler_LogData
        {
            get { return (DataTable)GetBaseSession("Scheduler_LogData_SchedulerLog"); }
            set { SetBaseSession("Scheduler_LogData_SchedulerLog", value); }
        }
        [CheckLogin]
        public ActionResult Frm_Scheduler_Info(string screen = null)
        {
            //Scheduler_UserRights();
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            ViewBag.CurrentCenID = BASE._open_Cen_ID.ToString();
            ViewBag.Screen = screen;
            //if (CheckRights(BASE, ClientScreen.Help_Scheduler, "List"))
            //{
        
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Scheduler).ToString()) ? 1 : 0;
            Scheduler_InfoData = GridDisplay_Schedule();

            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();

            return View(Scheduler_InfoData);
            //}
            //else
            //{
            //    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");
            //s}
        }

        [HttpPost]
        public PartialViewResult Frm_Scheduler_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null,
             string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "", string screen = null)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            //Scheduler_UserRights();
            if (command == "REFRESH" || Scheduler_InfoData == null)
            {
                Scheduler_InfoData=GridDisplay_Schedule();
            }
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            //ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.Screen = screen;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;      
            return PartialView(Scheduler_InfoData);
        }
        private DataTable GridDisplay_Schedule()
        {
            return BASE._Schedule_DBOps.Get_ScheduleListing();
        }
        public ActionResult Frm_Scheduler_Info_DetailGrid(string REC_ID, string command, int ShowHorizontalBar = 0)
        {
            ViewBag.SchedulerInfo_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.SchedulerInfo_REC_ID = REC_ID;
            if (command == "REFRESH")
            {
                Scheduler_Instance_ExportData = BASE._Schedule_DBOps.Get_ScheduleInstanceListing(REC_ID);
                Session["SchedulerInfo_detailGrid_Data"] = Scheduler_Instance_ExportData;
            }
            return View(Scheduler_Instance_ExportData);
        }
        public ActionResult Frm_Export_Options()
        {
            //if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Scheduler, "Export")))
            //{
            //return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove    
            //}
            return PartialView();
        }
        public ActionResult Frm_QueueExport_Options()
        {
            //if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Scheduler, "Export")))
            //{
            //return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove    
            //}
            return PartialView();
        }
        public ActionResult Frm_LogExport_Options()
        {
            //if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Scheduler, "Export")))
            //{
            //return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove    
            //}
            return PartialView();
        }
        private DataTable GridDisplay_Schedule_Queuing()
        {
            return BASE._Schedule_DBOps.Get_ScheduleQueueListing();
        }
        [HttpGet]
        public ActionResult Frm_Scheduler_Queue_Info(string ActionMethod = "_Queue", string PopupName = "SchedulerQueue_modal",
            string GridToRefresh = "SchedulerQueueListGrid")
        {
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Scheduler).ToString()) ? 1 : 0;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Scheduler_QueueData = GridDisplay_Schedule_Queuing();
            return View(Scheduler_QueueData);
        }
        public PartialViewResult Frm_Scheduler_Queue_Grid(string command, int ShowHorizontalBar = 0, string Layout = null,
             string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            //Scheduler_UserRights();
            if (command == "REFRESH" || Scheduler_QueueData == null)
            {
                Scheduler_QueueData = GridDisplay_Schedule_Queuing();
            }
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            return PartialView(Scheduler_QueueData);
        }
        [HttpGet]
        public ActionResult Frm_Scheduler_Log_Info(string nestedGridFocusedRowID = "", string PopupName = "SchedulerLog_modal",
            string GridToRefresh = "SchedulerLogListGrid")
        {
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Help_Scheduler).ToString()) ? 1 : 0;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Scheduler_ViewModel model = new Scheduler_ViewModel();
            model.Scheduler_Instance_ID = nestedGridFocusedRowID?? "";
            this.Scheduler_LogData = GridDisplay_Schedule_Log(model.Scheduler_Instance_ID);
            return View(this.Scheduler_LogData);
        }
        public PartialViewResult Frm_Scheduler_Log_Grid(string Scheduler_Instance_ID, string command, int ShowHorizontalBar = 0, string Layout = null,
             string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            //Scheduler_UserRights();
            if (command == "REFRESH" || Scheduler_LogData == null)
            {
                Scheduler_LogData = GridDisplay_Schedule_Log(Scheduler_Instance_ID);
            }
            ViewBag.ViewMode = ViewMode;
            ViewData["Layout"] = Layout;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            return PartialView(Scheduler_LogData);
        }
        private DataTable GridDisplay_Schedule_Log(string instanceID)
        {
            return BASE._Schedule_DBOps.Get_ScheduleLogListing(instanceID);
        }
        public void Scheduler_UserRights()
        {
            ViewData["Scheduler_ExportRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Scheduler, "Export");
            ViewData["Scheduler_ListRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Help_Scheduler, "List");
            ViewData["Scheduler_AddRight"] = CheckRights(BASE, ClientScreen.Help_Scheduler, "Add");
            ViewData["Scheduler_UpdateRight"] = CheckRights(BASE, ClientScreen.Help_Scheduler, "Update");
            ViewData["Scheduler_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Scheduler, "Delete");
            ViewData["Scheduler_ReportRight"] = CheckRights(BASE, ClientScreen.Help_Scheduler, "Report");

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["Help_Attachments_ListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");//attachment
        }

        [HttpGet]
        public ActionResult Frm_Scheduler_Window(string ActionMethod = "_New", string xID = "", string cenId = "", string Info_LastEditedOn = null, 
            string PopupName = "Dynamic_Content_popup", string GridToRefresh = "SchedulerListGrid",string Screen="")
        {
            ViewBag.Screen = Screen;
            Scheduler_ViewModel model = new Scheduler_ViewModel();
            string Universal_Scheduler_CenID = WebConfigurationManager.AppSettings["Universal_Scheduler_CenID"].ToString();
            model.Universal_Scheduler_CenID = Universal_Scheduler_CenID;
            if (model.Universal_Scheduler_CenID.Contains(BASE._open_Cen_ID.ToString()))
            {
                model.Is_Universal_SchedulerWindow_Editable = true;
            }
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), ActionMethod);
            model.ActionMethod = model.Tag.ToString();
            model.TitleX_Scheduler = "Scheduler";
            model.Scheduler_ID = xID ?? "";
            model.Cen_ID = cenId ?? "";        
            if (model.Tag == Common.Navigation_Mode._New)
            {
                /*GUID : a number so large that it is mathematically guaranteed to be unique not only in a single system like a database, 
                 * but across multiple systems or distributed applications*/
                model.Scheduler_ID = Guid.NewGuid().ToString();
            }
            else if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete||model.Tag == Common.Navigation_Mode._View)
            {
                DataTable d1 = BASE._Schedule_DBOps.GetRecord_Schedule(model.Scheduler_ID);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (d1.Rows.Count == 0)
                {
                    string message = Messages.RecordChanged("Current Schedule");
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Changed / Removed in Background!!','" + GridToRefresh + "');</script>");
                }
                model.Universal_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_IS_UNIVERSAL"]);
                if (model.Tag == Common.Navigation_Mode._View)
                {
                    //Skip rest all checks
                }
                else if(model.Universal_SchedulerWindow && (!model.Cen_ID.Equals(BASE._open_Cen_ID.ToString())) )
                {
                    string message = "You are not AUTHORIZED to perform Edit/Delete on this Universal Schedule!!!";
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','NOT AUTHORIZED!!!','" + GridToRefresh + "');</script>");
                }
                else if ((model.Universal_SchedulerWindow) && (model.Cen_ID.Equals(BASE._open_Cen_ID.ToString())) && ((!model.Universal_Scheduler_CenID.Contains(BASE._open_Cen_ID.ToString()))))
                {
                    string message = "Your rights have been revoked to perform operations with Universal Schedule!!!";
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Rights have been revoked!!!','" + GridToRefresh + "');</script>");
                }
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                DataTable d2 = BASE._Schedule_DBOps.GetRecord_ScheduleTimeBand(model.Scheduler_ID);
                
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                        {
                            string message = Messages.RecordChanged("Current Schedule");
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + GridToRefresh + "');</script>");
                        }
                    }
                }
                #region Edit model Binding
                model.MappedChartCount_SchedulerWindow = BASE._Schedule_DBOps.GetChartMappedToSchedule(model.Scheduler_ID);
                model.Name_SchedulerWindow = d1.Rows[0]["SI_NAME"].ToString();
                model.Prev_Name_SchedulerWindow = model.Name_SchedulerWindow;
                model.ScheduleType_SchedulerWindow = d1.Rows[0]["SI_TYPE"].ToString();
                if (model.ScheduleType_SchedulerWindow == "ONCE")
                {
                    if (Convert.IsDBNull(d1.Rows[0]["SI_FR_TIME"]) == false)
                    {
                        model.Time_SchedulerWindow = new DateTime(((TimeSpan)d1.Rows[0]["SI_FR_TIME"]).Ticks);
                    }
                }
                else if (model.ScheduleType_SchedulerWindow == "RECURRING")
                {
                    if (Convert.IsDBNull(d1.Rows[0]["SI_FREQ_TYPE"]) == false)
                    {
                        model.FrequencyType_SchedulerWindow = d1.Rows[0]["SI_FREQ_TYPE"].ToString();
                    }
                    if (model.FrequencyType_SchedulerWindow == "DAILY")
                    {
                        if (Convert.IsDBNull(d1.Rows[0]["SI_DAILY_RECURRENCE"]) == false)
                        {
                            model.DailyRecurrenceEvery_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_DAILY_RECURRENCE"]);
                        }
                    }
                    else if (model.FrequencyType_SchedulerWindow == "WEEKLY")
                    {
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_RECURRENCE"]) == false)
                        {
                            model.WeeklyRecurrenceEvery_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_WEEKLY_RECURRENCE"]);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_MON"]) == false)
                        {
                            model.Monday_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_WEEKLY_MON"]);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_TUE"]) == false)
                        {
                            model.Tuesday_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_WEEKLY_TUE"]);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_WED"]) == false)
                        {
                            model.Wednesday_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_WEEKLY_WED"]);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_THURS"]) == false)
                        {
                            model.Thursday_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_WEEKLY_THURS"]);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_FRI"]) == false)
                        {
                            model.Friday_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_WEEKLY_FRI"]);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_SAT"]) == false)
                        {
                            model.Saturday_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_WEEKLY_SAT"]);
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_WEEKLY_SUN"]) == false)
                        {
                            model.Sunday_SchedulerWindow = Convert.ToBoolean(d1.Rows[0]["SI_WEEKLY_SUN"]);
                        }
                    }
                    else if (model.FrequencyType_SchedulerWindow == "MONTHLY")
                    {
                        if (Convert.IsDBNull(d1.Rows[0]["SI_MONTHLY_TYPE"]) == false)
                        {
                            model.MonthlyFrequencyType_SchedulerWindow = d1.Rows[0]["SI_MONTHLY_TYPE"].ToString();
                        }
                        if (model.MonthlyFrequencyType_SchedulerWindow == "SPECIFIC DATE")
                        {
                            if (Convert.IsDBNull(d1.Rows[0]["SI_MONTHLY_DATE"]) == false)
                            {
                                model.MonthlyFrequencyDateSpecific_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_MONTHLY_DATE"]);
                            }
                            if (Convert.IsDBNull(d1.Rows[0]["SI_MONTHLY_RECURRENCE"]) == false)
                            {
                                model.MonthlyFrequencyEveryMonthInterval_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_MONTHLY_RECURRENCE"]);
                            }
                        }
                        else if (model.MonthlyFrequencyType_SchedulerWindow == "SPECIFIC DAY")
                        {
                            if (Convert.IsDBNull(d1.Rows[0]["SI_MONTHLY_RECURRENCE"]) == false)
                            {
                                model.MonthlyFrequencyEveryMonthInterval_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_MONTHLY_RECURRENCE"]);
                            }
                            if (Convert.IsDBNull(d1.Rows[0]["SI_MONTHLY_DAY_NO"]) == false)
                            {
                                model.MonthlyFrequencyMultipleDay_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_MONTHLY_DAY_NO"]);
                            }
                            if (Convert.IsDBNull(d1.Rows[0]["SI_MONTHLY_DAY"]) == false)
                            {
                                model.MonthlyFrequencyEveryWeekdaySpecific_SchedulerWindow = d1.Rows[0]["SI_MONTHLY_DAY"].ToString();
                            }
                        }
                    }
                    model.DayFrequencyType_SchedulerWindow = d1.Rows[0]["SI_DAY_FREQ"].ToString();
                    if (model.DayFrequencyType_SchedulerWindow == "ONCE")
                    {
                        if (Convert.IsDBNull(d1.Rows[0]["SI_FR_TIME"]) == false)
                        {
                            model.DayFrequencyOccuranceAt_SchedulerWindow = new DateTime(((TimeSpan)d1.Rows[0]["SI_FR_TIME"]).Ticks);//d1
                        }
                    }
                    else if (model.DayFrequencyType_SchedulerWindow == "RECURRING")
                    {
                        if (Convert.IsDBNull(d1.Rows[0]["SI_TOTAL_TIMEBANDS"]) == false)
                        {
                            model.DayFrequencyNoOfTimeBands_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_TOTAL_TIMEBANDS"]);//d1
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_DAY_RECURRENCE"]) == false)
                        {
                            model.DayFrequencyOccuranceEveryTB1_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_DAY_RECURRENCE"]);//d1
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_FR_TIME"]) == false)
                        {
                            model.DayFrequencyStartTimeTB1_SchedulerWindow = new DateTime(((TimeSpan)d1.Rows[0]["SI_FR_TIME"]).Ticks);//d1
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_TO_TIME"]) == false)
                        {
                            model.DayFrequencyEndTimeTB1_SchedulerWindow = new DateTime(((TimeSpan)d1.Rows[0]["SI_TO_TIME"]).Ticks);//d1
                        }
                        if (model.DayFrequencyNoOfTimeBands_SchedulerWindow > 1 && d2.Rows.Count > 0)
                        {
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_2"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB2_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_2"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_2"]) == false)
                            {
                                model.DayFrequencyStartTimeTB2_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_2"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_2"]) == false)
                            {
                                model.DayFrequencyEndTimeTB2_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_2"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_3"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB3_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_3"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_3"]) == false)
                            {
                                model.DayFrequencyStartTimeTB3_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_3"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_3"]) == false)
                            {
                                model.DayFrequencyEndTimeTB3_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_3"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_4"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB4_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_4"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_4"]) == false)
                            {
                                model.DayFrequencyStartTimeTB4_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_4"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_4"]) == false)
                            {
                                model.DayFrequencyEndTimeTB4_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_4"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_5"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB5_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_5"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_5"]) == false)
                            {
                                model.DayFrequencyStartTimeTB5_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_5"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_5"]) == false)
                            {
                                model.DayFrequencyEndTimeTB5_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_5"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_6"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB6_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_6"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_6"]) == false)
                            {
                                model.DayFrequencyStartTimeTB6_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_6"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_6"]) == false)
                            {
                                model.DayFrequencyEndTimeTB6_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_6"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_7"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB7_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_7"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_7"]) == false)
                            {
                                model.DayFrequencyStartTimeTB7_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_7"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_7"]) == false)
                            {
                                model.DayFrequencyEndTimeTB7_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_7"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_8"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB8_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_8"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_8"]) == false)
                            {
                                model.DayFrequencyStartTimeTB8_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_8"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_8"]) == false)
                            {
                                model.DayFrequencyEndTimeTB8_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_8"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_9"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB9_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_9"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_9"]) == false)
                            {
                                model.DayFrequencyStartTimeTB9_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_9"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_9"]) == false)
                            {
                                model.DayFrequencyEndTimeTB9_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_9"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_DAY_RECURRENCE_10"]) == false)
                            {
                                model.DayFrequencyOccuranceEveryTB10_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_DAY_RECURRENCE_10"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_10"]) == false)
                            {
                                model.DayFrequencyStartTimeTB10_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_10"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_10"]) == false)
                            {
                                model.DayFrequencyEndTimeTB10_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_10"]).Ticks);//d2
                            }
                        }
                    }
                    else if (model.DayFrequencyType_SchedulerWindow == "RANDOM")
                    {
                        if (Convert.IsDBNull(d1.Rows[0]["SI_TOTAL_TIMEBANDS"]) == false)
                        {
                            model.DayFrequencyNoOfTimeBands_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_TOTAL_TIMEBANDS"]);//d1
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_RANDOM_RECURRENCE"]) == false)
                        {
                            model.DayFrequencyRandomOccurenceNoTB1_SchedulerWindow = Convert.ToInt32(d1.Rows[0]["SI_RANDOM_RECURRENCE"]);//d1
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_FR_TIME"]) == false)
                        {
                            model.DayFrequencyStartTimeTB1_SchedulerWindow = new DateTime(((TimeSpan)d1.Rows[0]["SI_FR_TIME"]).Ticks);//d1
                        }
                        if (Convert.IsDBNull(d1.Rows[0]["SI_TO_TIME"]) == false)
                        {
                            model.DayFrequencyEndTimeTB1_SchedulerWindow = new DateTime(((TimeSpan)d1.Rows[0]["SI_TO_TIME"]).Ticks);//d1
                        }
                        if (model.DayFrequencyNoOfTimeBands_SchedulerWindow > 1 && d2.Rows.Count>0)
                        {
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_2"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB2_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_2"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_2"]) == false)
                            {
                                model.DayFrequencyStartTimeTB2_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_2"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_2"]) == false)
                            {
                                model.DayFrequencyEndTimeTB2_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_2"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_3"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB3_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_3"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_3"]) == false)
                            {
                                model.DayFrequencyStartTimeTB3_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_3"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_3"]) == false)
                            {
                                model.DayFrequencyEndTimeTB3_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_3"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_4"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB4_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_4"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_4"]) == false)
                            {
                                model.DayFrequencyStartTimeTB4_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_4"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_4"]) == false)
                            {
                                model.DayFrequencyEndTimeTB4_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_4"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_5"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB5_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_5"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_5"]) == false)
                            {
                                model.DayFrequencyStartTimeTB5_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_5"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_5"]) == false)
                            {
                                model.DayFrequencyEndTimeTB5_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_5"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_6"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB6_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_6"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_6"]) == false)
                            {
                                model.DayFrequencyStartTimeTB6_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_6"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_6"]) == false)
                            {
                                model.DayFrequencyEndTimeTB6_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_6"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_7"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB7_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_7"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_7"]) == false)
                            {
                                model.DayFrequencyStartTimeTB7_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_7"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_7"]) == false)
                            {
                                model.DayFrequencyEndTimeTB7_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_7"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_8"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB8_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_8"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_8"]) == false)
                            {
                                model.DayFrequencyStartTimeTB8_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_8"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_8"]) == false)
                            {
                                model.DayFrequencyEndTimeTB8_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_8"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_9"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB9_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_9"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_9"]) == false)
                            {
                                model.DayFrequencyStartTimeTB9_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_9"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_9"]) == false)
                            {
                                model.DayFrequencyEndTimeTB9_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_9"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_RANDOM_RECURRENCE_10"]) == false)
                            {
                                model.DayFrequencyRandomOccurenceNoTB10_SchedulerWindow = Convert.ToInt32(d2.Rows[0]["TB_RANDOM_RECURRENCE_10"]);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_FR_TIME_10"]) == false)
                            {
                                model.DayFrequencyStartTimeTB10_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_FR_TIME_10"]).Ticks);//d2
                            }
                            if (Convert.IsDBNull(d2.Rows[0]["TB_TO_TIME_10"]) == false)
                            {
                                model.DayFrequencyEndTimeTB10_SchedulerWindow = new DateTime(((TimeSpan)d2.Rows[0]["TB_TO_TIME_10"]).Ticks);//d2
                            }
                        }                     
                    }
                }
                if (Convert.IsDBNull(d1.Rows[0]["SI_SUMMARY"]) == false)
                {
                    model.Summary_SchedulerWindow = d1.Rows[0]["SI_SUMMARY"].ToString();//d1
                }
                #endregion
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_Scheduler_Window(Scheduler_ViewModel model)
        {
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New)
                {
                    if (model.Name_SchedulerWindow == null)
                    {
                        jsonParam.message = "Empty Scheduler Name. . . !";
                        jsonParam.title = "Invalid Name . . .";
                        jsonParam.focusid = "Name_SchedulerWindow";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //name of the schedule is mandatory and should be unique in the given UID and not matching with any universal schedules
                    if (BASE._Schedule_DBOps.checkScheduleNameUniqueness(model.Name_SchedulerWindow.Trim()) >= 1)
                    {
                        jsonParam.message = "Schedule Name already Exists. . . !";
                        jsonParam.title = "Duplicate Schedule . . .";
                        jsonParam.focusid = "Name_SchedulerWindow";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DbOperations.Schedule.Param_Insert_Schedule InParam = new DbOperations.Schedule.Param_Insert_Schedule();
                    InParam.Sch_Name = model.Name_SchedulerWindow ?? "";
                    InParam.Sch_Type = model.ScheduleType_SchedulerWindow;
                    InParam.Is_Universal = model.Universal_SchedulerWindow;
                    if (model.ScheduleType_SchedulerWindow == "ONCE")
                    {
                        InParam.From_Time = Convert.ToDateTime(model.Time_SchedulerWindow).ToLongTimeString();//Change it to Time corresponding field
                    }
                    else if (model.ScheduleType_SchedulerWindow == "RECURRING")
                    {
                        if (model.FrequencyType_SchedulerWindow == "DAILY")
                        {
                            InParam.Freq_Type = model.FrequencyType_SchedulerWindow;
                            InParam.Daily_Recurrence = Convert.ToInt32(model.DailyRecurrenceEvery_SchedulerWindow);
                        }
                        else if (model.FrequencyType_SchedulerWindow == "WEEKLY")
                        {
                            InParam.Freq_Type = model.FrequencyType_SchedulerWindow;
                            InParam.Weekly_Recurrence = Convert.ToInt32(model.WeeklyRecurrenceEvery_SchedulerWindow);
                            InParam.Weekly_Monday = model.Monday_SchedulerWindow;
                            InParam.Weekly_Tuesday = model.Tuesday_SchedulerWindow;
                            InParam.Weekly_Wednesday = model.Wednesday_SchedulerWindow;
                            InParam.Weekly_Thursday = model.Thursday_SchedulerWindow;
                            InParam.Weekly_Friday = model.Friday_SchedulerWindow;
                            InParam.Weekly_Saturday = model.Saturday_SchedulerWindow;
                            InParam.Weekly_Sunday = model.Sunday_SchedulerWindow;
                        }
                        else if (model.FrequencyType_SchedulerWindow == "MONTHLY")
                        {
                            InParam.Freq_Type = model.FrequencyType_SchedulerWindow;
                            InParam.Monthly_Type = model.MonthlyFrequencyType_SchedulerWindow;
                            if (model.MonthlyFrequencyType_SchedulerWindow == "SPECIFIC DATE")
                            {
                                InParam.Monthly_Date = Convert.ToInt32(model.MonthlyFrequencyDateSpecific_SchedulerWindow);
                                InParam.Monthly_Recurrence = Convert.ToInt32(model.MonthlyFrequencyEveryMonthInterval_SchedulerWindow);
                            }
                            else if (model.MonthlyFrequencyType_SchedulerWindow == "SPECIFIC DAY")
                            {
                                InParam.Monthly_Day_No = Convert.ToInt32(model.MonthlyFrequencyMultipleDay_SchedulerWindow);
                                InParam.Monthly_Day = model.MonthlyFrequencyEveryWeekdaySpecific_SchedulerWindow;
                                InParam.Monthly_Recurrence = Convert.ToInt32(model.MonthlyFrequencyEveryMonthInterval_SchedulerWindow);
                            }
                        }

                        if (model.DayFrequencyType_SchedulerWindow == "ONCE")
                        {
                            InParam.Day_Freq_Type = model.DayFrequencyType_SchedulerWindow;
                            InParam.From_Time = Convert.ToDateTime(model.DayFrequencyOccuranceAt_SchedulerWindow).ToLongTimeString();
                        }
                        else if (model.DayFrequencyType_SchedulerWindow == "RECURRING")
                        {
                            InParam.Day_Freq_Type = model.DayFrequencyType_SchedulerWindow;
                            Int32 timeBand = Convert.ToInt32(model.DayFrequencyNoOfTimeBands_SchedulerWindow);
                            InParam.Total_Timebands = timeBand;
                            InParam.Day_Recurrence = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB1_SchedulerWindow);
                            InParam.From_Time = Convert.ToDateTime(model.DayFrequencyStartTimeTB1_SchedulerWindow).ToLongTimeString();
                            InParam.To_Time = Convert.ToDateTime(model.DayFrequencyEndTimeTB1_SchedulerWindow).ToLongTimeString();
                            if (model.DayFrequencyOccuranceEveryTB2_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_2 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB2_SchedulerWindow);
                                InParam.From_Time_2 = Convert.ToDateTime(model.DayFrequencyStartTimeTB2_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_2 = Convert.ToDateTime(model.DayFrequencyEndTimeTB2_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB3_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_3 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB3_SchedulerWindow);
                                InParam.From_Time_3 = Convert.ToDateTime(model.DayFrequencyStartTimeTB3_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_3 = Convert.ToDateTime(model.DayFrequencyEndTimeTB3_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB4_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_4 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB4_SchedulerWindow);
                                InParam.From_Time_4 = Convert.ToDateTime(model.DayFrequencyStartTimeTB4_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_4 = Convert.ToDateTime(model.DayFrequencyEndTimeTB4_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB5_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_5 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB5_SchedulerWindow);
                                InParam.From_Time_5 = Convert.ToDateTime(model.DayFrequencyStartTimeTB5_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_5 = Convert.ToDateTime(model.DayFrequencyEndTimeTB5_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB6_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_6 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB6_SchedulerWindow);
                                InParam.From_Time_6 = Convert.ToDateTime(model.DayFrequencyStartTimeTB6_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_6 = Convert.ToDateTime(model.DayFrequencyEndTimeTB6_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB7_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_7 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB7_SchedulerWindow);
                                InParam.From_Time_7 = Convert.ToDateTime(model.DayFrequencyStartTimeTB7_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_7 = Convert.ToDateTime(model.DayFrequencyEndTimeTB7_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB8_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_8 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB8_SchedulerWindow);
                                InParam.From_Time_8 = Convert.ToDateTime(model.DayFrequencyStartTimeTB8_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_8 = Convert.ToDateTime(model.DayFrequencyEndTimeTB8_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB9_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_9 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB9_SchedulerWindow);
                                InParam.From_Time_9 = Convert.ToDateTime(model.DayFrequencyStartTimeTB9_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_9 = Convert.ToDateTime(model.DayFrequencyEndTimeTB9_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB10_SchedulerWindow > 0)
                            {
                                InParam.Day_Recurrence_10 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB10_SchedulerWindow);
                                InParam.From_Time_10 = Convert.ToDateTime(model.DayFrequencyStartTimeTB10_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_10 = Convert.ToDateTime(model.DayFrequencyEndTimeTB10_SchedulerWindow).ToLongTimeString();
                            }
                        }
                        else if (model.DayFrequencyType_SchedulerWindow == "RANDOM")
                        {
                            InParam.Day_Freq_Type = model.DayFrequencyType_SchedulerWindow;
                            Int32 timeBand = Convert.ToInt32(model.DayFrequencyNoOfTimeBands_SchedulerWindow);
                            InParam.Total_Timebands = timeBand;
                            InParam.Random_Recurrence = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB1_SchedulerWindow);
                            InParam.From_Time = Convert.ToDateTime(model.DayFrequencyStartTimeTB1_SchedulerWindow).ToLongTimeString();
                            InParam.To_Time = Convert.ToDateTime(model.DayFrequencyEndTimeTB1_SchedulerWindow).ToLongTimeString();
                            if (model.DayFrequencyStartTimeTB2_SchedulerWindow!=null|| model.DayFrequencyEndTimeTB2_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB2_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_2 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB2_SchedulerWindow);
                                InParam.From_Time_2 = Convert.ToDateTime(model.DayFrequencyStartTimeTB2_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_2 = Convert.ToDateTime(model.DayFrequencyEndTimeTB2_SchedulerWindow).ToLongTimeString(); 
                            }
                            if (model.DayFrequencyStartTimeTB3_SchedulerWindow != null || model.DayFrequencyEndTimeTB3_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB3_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_3 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB3_SchedulerWindow);
                                InParam.From_Time_3 = Convert.ToDateTime(model.DayFrequencyStartTimeTB3_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_3 = Convert.ToDateTime(model.DayFrequencyEndTimeTB3_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB4_SchedulerWindow != null || model.DayFrequencyEndTimeTB4_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB4_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_4 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB4_SchedulerWindow);
                                InParam.From_Time_4 = Convert.ToDateTime(model.DayFrequencyStartTimeTB4_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_4 = Convert.ToDateTime(model.DayFrequencyEndTimeTB4_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB5_SchedulerWindow != null || model.DayFrequencyEndTimeTB5_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB5_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_5 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB5_SchedulerWindow);
                                InParam.From_Time_5 = Convert.ToDateTime(model.DayFrequencyStartTimeTB5_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_5 = Convert.ToDateTime(model.DayFrequencyEndTimeTB5_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB6_SchedulerWindow != null || model.DayFrequencyEndTimeTB6_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB6_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_6 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB6_SchedulerWindow);
                                InParam.From_Time_6 = Convert.ToDateTime(model.DayFrequencyStartTimeTB6_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_6 = Convert.ToDateTime(model.DayFrequencyEndTimeTB6_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB7_SchedulerWindow != null || model.DayFrequencyEndTimeTB7_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB7_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_7 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB7_SchedulerWindow);
                                InParam.From_Time_7 = Convert.ToDateTime(model.DayFrequencyStartTimeTB7_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_7 = Convert.ToDateTime(model.DayFrequencyEndTimeTB7_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB8_SchedulerWindow != null || model.DayFrequencyEndTimeTB8_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB8_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_8 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB8_SchedulerWindow);
                                InParam.From_Time_8 = Convert.ToDateTime(model.DayFrequencyStartTimeTB8_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_8 = Convert.ToDateTime(model.DayFrequencyEndTimeTB8_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB9_SchedulerWindow != null || model.DayFrequencyEndTimeTB9_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB9_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_9 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB9_SchedulerWindow);
                                InParam.From_Time_9 = Convert.ToDateTime(model.DayFrequencyStartTimeTB9_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_9 = Convert.ToDateTime(model.DayFrequencyEndTimeTB9_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB10_SchedulerWindow != null || model.DayFrequencyEndTimeTB10_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB10_SchedulerWindow > 0)
                            {
                                InParam.Random_Recurrence_10 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB10_SchedulerWindow);
                                InParam.From_Time_10 = Convert.ToDateTime(model.DayFrequencyStartTimeTB10_SchedulerWindow).ToLongTimeString();
                                InParam.To_Time_10 = Convert.ToDateTime(model.DayFrequencyEndTimeTB10_SchedulerWindow).ToLongTimeString();
                            }
                        }
                    }
                    int id = BASE._Schedule_DBOps.InsertSchedule(InParam);
                    model.Scheduler_ID = id.ToString();
                    if (id > 0)
                    /*{
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                    }
                    else*/
                    {

                        jsonParam.message = Common_Lib.Messages.SaveSuccess;
                        jsonParam.title = model.TitleX_Scheduler;
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                    }
                }

                else if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    DataTable d1 = BASE._Schedule_DBOps.GetRecord_Schedule(model.Scheduler_ID);
                    if (d1 == null)
                    {
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                    }

                    if (d1.Rows.Count == 0)
                    {
                        jsonParam.message = "No Record Found. Please reload and try again. . !";
                        jsonParam.title = "No record found. . .";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((!model.Prev_Name_SchedulerWindow.Equals(model.Name_SchedulerWindow))&& BASE._Schedule_DBOps.checkScheduleNameUniqueness(model.Name_SchedulerWindow.Trim()) >= 1)
                    {
                        jsonParam.message = "Schedule Name already Exists. . . !";
                        jsonParam.title = "Duplicate Schedule . . .";
                        jsonParam.focusid = "Name_SchedulerWindow";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE.AllowMultiuser())
                    {
                        if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                        {
                            if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                            {
                                jsonParam.message =Messages.RecordChanged("Current Schedule");
                                jsonParam.title = "Record Changed. . .";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    DbOperations.Schedule.Param_Update_Schedule UpParam = new DbOperations.Schedule.Param_Update_Schedule();
                    UpParam.Sch_Name = model.Name_SchedulerWindow ?? "";
                    UpParam.Sch_Type = model.ScheduleType_SchedulerWindow;
                    UpParam.Is_Universal = model.Universal_SchedulerWindow;
                    UpParam.Schedule_ID = Convert.ToInt32(model.Scheduler_ID);
                    if (model.ScheduleType_SchedulerWindow == "ONCE")
                    {
                        UpParam.From_Time = Convert.ToDateTime(model.Time_SchedulerWindow).ToLongTimeString();//Change it to Time corresponding field
                    }
                    else if (model.ScheduleType_SchedulerWindow == "RECURRING")
                    {
                        UpParam.Freq_Type = model.FrequencyType_SchedulerWindow;
                        if (model.FrequencyType_SchedulerWindow == "DAILY")
                        {
                            UpParam.Daily_Recurrence = Convert.ToInt32(model.DailyRecurrenceEvery_SchedulerWindow);
                        }
                        else if (model.FrequencyType_SchedulerWindow == "WEEKLY")
                        {
                            UpParam.Weekly_Recurrence = Convert.ToInt32(model.WeeklyRecurrenceEvery_SchedulerWindow);
                            UpParam.Weekly_Monday = model.Monday_SchedulerWindow;
                            UpParam.Weekly_Tuesday = model.Tuesday_SchedulerWindow;
                            UpParam.Weekly_Wednesday = model.Wednesday_SchedulerWindow;
                            UpParam.Weekly_Thursday = model.Thursday_SchedulerWindow;
                            UpParam.Weekly_Friday = model.Friday_SchedulerWindow;
                            UpParam.Weekly_Saturday = model.Saturday_SchedulerWindow;
                            UpParam.Weekly_Sunday = model.Sunday_SchedulerWindow;
                        }
                        else if (model.FrequencyType_SchedulerWindow == "MONTHLY")
                        {
                            UpParam.Monthly_Type = model.MonthlyFrequencyType_SchedulerWindow;
                            if (model.MonthlyFrequencyType_SchedulerWindow == "SPECIFIC DATE")
                            {
                                UpParam.Monthly_Date = Convert.ToInt32(model.MonthlyFrequencyDateSpecific_SchedulerWindow);
                                UpParam.Monthly_Recurrence = Convert.ToInt32(model.MonthlyFrequencyEveryMonthInterval_SchedulerWindow);
                            }
                            else if (model.MonthlyFrequencyType_SchedulerWindow == "SPECIFIC DAY")
                            {
                                UpParam.Monthly_Recurrence = Convert.ToInt32(model.MonthlyFrequencyEveryMonthInterval_SchedulerWindow);
                                UpParam.Monthly_Day_No = Convert.ToInt32(model.MonthlyFrequencyMultipleDay_SchedulerWindow);
                                UpParam.Monthly_Day = model.MonthlyFrequencyEveryWeekdaySpecific_SchedulerWindow;
                            }
                        }

                        if (model.DayFrequencyType_SchedulerWindow == "ONCE")
                        {
                            UpParam.Day_Freq_Type = model.DayFrequencyType_SchedulerWindow;
                            UpParam.From_Time = Convert.ToDateTime(model.DayFrequencyOccuranceAt_SchedulerWindow).ToLongTimeString();
                        }
                        else if (model.DayFrequencyType_SchedulerWindow == "RECURRING")
                        {
                            UpParam.Day_Freq_Type = model.DayFrequencyType_SchedulerWindow;
                            Int32 timeBand = Convert.ToInt32(model.DayFrequencyNoOfTimeBands_SchedulerWindow);
                            UpParam.Total_Timebands = timeBand;
                            UpParam.Day_Recurrence = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB1_SchedulerWindow); Convert.ToDateTime(model.DayFrequencyStartTimeTB4_SchedulerWindow).ToLongTimeString();
                            UpParam.From_Time = Convert.ToDateTime(model.DayFrequencyStartTimeTB1_SchedulerWindow).ToLongTimeString();
                            UpParam.To_Time = Convert.ToDateTime(model.DayFrequencyEndTimeTB1_SchedulerWindow).ToLongTimeString();
                            if (model.DayFrequencyOccuranceEveryTB2_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_2 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB2_SchedulerWindow);
                                UpParam.From_Time_2 = Convert.ToDateTime(model.DayFrequencyStartTimeTB2_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_2 = Convert.ToDateTime(model.DayFrequencyEndTimeTB2_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB3_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_3 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB3_SchedulerWindow);
                                UpParam.From_Time_3 =Convert.ToDateTime(model.DayFrequencyStartTimeTB3_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_3 =Convert.ToDateTime(model.DayFrequencyEndTimeTB3_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB4_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_4 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB4_SchedulerWindow);
                                UpParam.From_Time_4 =Convert.ToDateTime(model.DayFrequencyStartTimeTB4_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_4 =Convert.ToDateTime(model.DayFrequencyEndTimeTB4_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB5_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_5 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB5_SchedulerWindow);
                                UpParam.From_Time_5 =Convert.ToDateTime(model.DayFrequencyStartTimeTB5_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_5 =Convert.ToDateTime(model.DayFrequencyEndTimeTB5_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB6_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_6 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB6_SchedulerWindow);
                                UpParam.From_Time_6 =Convert.ToDateTime(model.DayFrequencyStartTimeTB6_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_6 =Convert.ToDateTime(model.DayFrequencyEndTimeTB6_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB7_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_7 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB7_SchedulerWindow);
                                UpParam.From_Time_7 =Convert.ToDateTime(model.DayFrequencyStartTimeTB7_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_7 =Convert.ToDateTime(model.DayFrequencyEndTimeTB7_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB8_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_8 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB8_SchedulerWindow);
                                UpParam.From_Time_8 =Convert.ToDateTime(model.DayFrequencyStartTimeTB8_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_8 =Convert.ToDateTime(model.DayFrequencyEndTimeTB8_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB9_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_9 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB9_SchedulerWindow);
                                UpParam.From_Time_9 =Convert.ToDateTime(model.DayFrequencyStartTimeTB9_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_9 =Convert.ToDateTime(model.DayFrequencyEndTimeTB9_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyOccuranceEveryTB10_SchedulerWindow > 0)
                            {
                                UpParam.Day_Recurrence_10 = Convert.ToInt32(model.DayFrequencyOccuranceEveryTB10_SchedulerWindow);
                                UpParam.From_Time_10 =Convert.ToDateTime(model.DayFrequencyStartTimeTB10_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_10 =Convert.ToDateTime(model.DayFrequencyEndTimeTB10_SchedulerWindow).ToLongTimeString();
                            }
                        }
                        else if (model.DayFrequencyType_SchedulerWindow == "RANDOM")
                        {
                            UpParam.Day_Freq_Type = model.DayFrequencyType_SchedulerWindow;
                            Int32 timeBand = Convert.ToInt32(model.DayFrequencyNoOfTimeBands_SchedulerWindow);
                            UpParam.Total_Timebands = timeBand;
                            UpParam.Random_Recurrence = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB1_SchedulerWindow);
                            UpParam.From_Time =Convert.ToDateTime(model.DayFrequencyStartTimeTB1_SchedulerWindow).ToLongTimeString();
                            UpParam.To_Time =Convert.ToDateTime(model.DayFrequencyEndTimeTB1_SchedulerWindow).ToLongTimeString();
                            if (model.DayFrequencyStartTimeTB2_SchedulerWindow != null || model.DayFrequencyEndTimeTB2_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB2_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_2 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB2_SchedulerWindow);
                                UpParam.From_Time_2 =Convert.ToDateTime(model.DayFrequencyStartTimeTB2_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_2 =Convert.ToDateTime(model.DayFrequencyEndTimeTB2_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB3_SchedulerWindow != null || model.DayFrequencyEndTimeTB3_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB3_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_3 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB3_SchedulerWindow);
                                UpParam.From_Time_3 =Convert.ToDateTime(model.DayFrequencyStartTimeTB3_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_3 =Convert.ToDateTime(model.DayFrequencyEndTimeTB3_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB4_SchedulerWindow != null || model.DayFrequencyEndTimeTB4_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB4_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_4 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB4_SchedulerWindow);
                                UpParam.From_Time_4 =Convert.ToDateTime(model.DayFrequencyStartTimeTB4_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_4 =Convert.ToDateTime(model.DayFrequencyEndTimeTB4_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB5_SchedulerWindow != null || model.DayFrequencyEndTimeTB5_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB5_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_5 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB5_SchedulerWindow);
                                UpParam.From_Time_5 =Convert.ToDateTime(model.DayFrequencyStartTimeTB5_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_5 =Convert.ToDateTime(model.DayFrequencyEndTimeTB5_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB6_SchedulerWindow != null || model.DayFrequencyEndTimeTB6_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB6_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_6 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB6_SchedulerWindow);
                                UpParam.From_Time_6 =Convert.ToDateTime(model.DayFrequencyStartTimeTB6_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_6 =Convert.ToDateTime(model.DayFrequencyEndTimeTB6_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB7_SchedulerWindow != null || model.DayFrequencyEndTimeTB7_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB7_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_7 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB7_SchedulerWindow);
                                UpParam.From_Time_7 =Convert.ToDateTime(model.DayFrequencyStartTimeTB7_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_7 =Convert.ToDateTime(model.DayFrequencyEndTimeTB7_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB8_SchedulerWindow != null || model.DayFrequencyEndTimeTB8_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB8_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_8 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB8_SchedulerWindow);
                                UpParam.From_Time_8 =Convert.ToDateTime(model.DayFrequencyStartTimeTB8_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_8 =Convert.ToDateTime(model.DayFrequencyEndTimeTB8_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB9_SchedulerWindow != null || model.DayFrequencyEndTimeTB9_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB9_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_9 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB9_SchedulerWindow);
                                UpParam.From_Time_9 =Convert.ToDateTime(model.DayFrequencyStartTimeTB9_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_9 =Convert.ToDateTime(model.DayFrequencyEndTimeTB9_SchedulerWindow).ToLongTimeString();
                            }
                            if (model.DayFrequencyStartTimeTB10_SchedulerWindow != null || model.DayFrequencyEndTimeTB10_SchedulerWindow != null || model.DayFrequencyRandomOccurenceNoTB10_SchedulerWindow > 0)
                            {
                                UpParam.Random_Recurrence_10 = Convert.ToInt32(model.DayFrequencyRandomOccurenceNoTB3_SchedulerWindow);
                                UpParam.From_Time_10 =Convert.ToDateTime(model.DayFrequencyStartTimeTB10_SchedulerWindow).ToLongTimeString();
                                UpParam.To_Time_10 =Convert.ToDateTime(model.DayFrequencyEndTimeTB10_SchedulerWindow).ToLongTimeString();
                            }
                        }
                    }
                    if (!BASE._Schedule_DBOps.UpdateSchedule(UpParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.UpdateSuccess;
                        jsonParam.title = model.TitleX_Scheduler;
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                    }
                }
                
                else if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    DataTable d1 = BASE._Schedule_DBOps.GetRecord_Schedule(model.Scheduler_ID);
                    if (d1 == null)
                    {
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                    }
                    if (d1.Rows.Count == 0)
                    {
                        jsonParam.message = "No Record Found. Please reload and retry...!";
                        jsonParam.title = "No Record Found. . .";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE.AllowMultiuser())
                    {
                        if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                        {
                            if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Info_LastEditedOn), Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"])) == false)
                            {
                                jsonParam.message = Messages.RecordChanged("Current Schedule");
                                jsonParam.title = "Record Changed. . .";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    if (BASE._Schedule_DBOps.checkJobsMappedToSchedule(model.Scheduler_ID)!=0)
                    {
                        jsonParam.message = "Scheduler cannot be deleted as it is alredy mapped with a Job.";
                        jsonParam.title = "Invalid Operation!";
                        jsonParam.result = false;
                    }
                    else if (!BASE._Schedule_DBOps.DeleteSchedule(model.Scheduler_ID))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                        jsonParam.title = model.TitleX_Scheduler;
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                    }
                }
                    
                    return Json(new { jsonParam, xID = model.Scheduler_ID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public void SessionClear()
        {
            ClearBaseSession("_Scheduler");
            Session.Remove("SchedulerInfo_DetailGrid_Data");
        }
        public void QueueSessionClear()
        {
            ClearBaseSession("_SchedulerQueue");            
        }
        public void LogSessionClear()
        {
            ClearBaseSession("_SchedulerLog");           
        }
        public bool CheckRights(Common _base, ClientScreen screenName, string RightsReqd)
        {
            //Superuser/Admin/Auditor get all rights on screens in accounts Modules(including masters)
            if (((_base._open_User_Type.ToUpper() == Common.ClientUserType.SuperUser.ToUpper())
                          || (_base._open_User_Type.ToUpper() == Common.ClientUserType.Auditor.ToUpper()) || (_base._open_User_User_Is_Admin))
                          && (GetScreenModuleName(screenName).ToLower() == "accounts" || GetScreenModuleName(screenName).ToLower() == "magazine" || GetScreenModuleName(screenName).ToLower() == "membership"))
            {
                return true;
            }
            //All Other users & Screens Admins checked for Rights 
            else
            {
                return CheckRightsByAllocation(_base, screenName, RightsReqd);
            }
        }
    }
}