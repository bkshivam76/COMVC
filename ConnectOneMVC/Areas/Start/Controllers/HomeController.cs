using Common_Lib;
using ConnectOneMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib.RealTimeService;
using Newtonsoft.Json;
using System.Configuration;

namespace ConnectOneMVC.Areas.Start.Controllers
{
    
    public class HomeController : BaseController
    {
        public string[] AutoOpenScreenIDs
        {
            get { return (string[])GetBaseSession("AutoOpenScreenIDs_AutoOpenScreen"); }
            set { SetBaseSession("AutoOpenScreenIDs_AutoOpenScreen", value); }
        }
        // GET: Start/Home
        [CheckLogin]
        public JsonResult UserAuthorization()   //checks User Authorization for user and sets the session rights variable.
        {   
            foreach (DataRow cRow in BASE._Auth_Rights_Listing.Rows)             
            {                   
                SetRightSession(cRow["Task"].ToString() + "_" + cRow["Permission"].ToString(), true);   
            }               
            if (BASE._open_User_User_Is_Admin == true)                
            {                   
                SetRightSession("isAdmin", true);       
            }//0000260 bug fixed                
          
            if ((BASE._open_User_Type == Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) ||
                (BASE._open_User_Type == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))
            {
                SetRightSession("SuperUser_Auditor", true);               
            }//Mantis bug 0000380 fixed
            var counter=0;
            counter = Session.Count;
            var user = BASE._open_User_ID;
            List<string> arr = new List<string>();
            foreach (object obj in Session)
            {
                arr.Add(obj.ToString());                
            }
            return Json(new { result = arr, message = "Session Count= " + Session.Count + "   User= "+ user }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public ActionResult Index()
        {
            UserAuthorization();
            // ...................................
            BASE._open_ClientUser = "NO";
            BASE.Refresh_Notes_List = true;        
            try
            {
                DateTime ServerDate = Convert.ToDateTime(BASE._Action_Items_DBOps.GetServerDateTime());
            }
            catch (Exception ex)
            {
            }

            // 'CHECKING CENTRE TASK INFO.......................
            BASE.Allow_Foreign_Donation = false;
            BASE.Allow_Bank_In_C_Box = false;
            BASE.Is_HQ_Centre = false;
            BASE.Allow_Membership = false;
            DataTable PER_DB = BASE._ClientUserDBOps.GetCenterTasks();
            if ((PER_DB == null))
            {
            }

            DataView DV1 = new DataView(PER_DB);
            DV1.Sort = "TASK_NAME";
            if ((DV1.Count > 0))
            {
                int index = DV1.Find("FOREIGN DONATION");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Allow_Foreign_Donation = true;
                    }
                    else
                    {
                        BASE.Allow_Foreign_Donation = false;
                    }

                }

                index = DV1.Find("COLLECTION BOX - BANK");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Allow_Bank_In_C_Box = true;
                    }
                    else
                    {
                        BASE.Allow_Bank_In_C_Box = false;
                    }

                }

                index = DV1.Find("H.Q. CENTRE");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Is_HQ_Centre = true;
                    }
                    else
                    {
                        BASE.Is_HQ_Centre = false;
                    }

                }

                index = DV1.Find("PRINT STATEMENTS");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Allow_Statements_Without_Restrictions = true;
                    }
                    else
                    {
                        BASE.Allow_Statements_Without_Restrictions = false;
                    }

                }
            }
            else
            {
                BASE.Allow_Foreign_Donation = false;
                BASE.Allow_Bank_In_C_Box = false;
                BASE.Is_HQ_Centre = false;
                BASE.Allow_Membership = false;
                BASE.Allow_Statements_Without_Restrictions = false;
            }

            if ((BASE._open_Year_Acc_Type == "MEMBERSHIP"))
            {
                BASE.Allow_Membership = true;
            }
            else
            {
                BASE.Allow_Membership = false;
            }

            if ((BASE._open_Year_Acc_Type == "MAGAZINE"))
            {
                BASE.Allow_Magazine = true;
            }
            else
            {
                BASE.Allow_Magazine = false;
            }

            object ReportsToBePrinted = "";
            ReportsToBePrinted = BASE._CenterDBOps.GetReportsToBePrintedInfo(BASE._open_Year_ID);
            BASE._ReportsToBePrinted = ReportsToBePrinted.ToString();
            // BASE._ReportsToBePrinted = IIf((Not ReportsToBePrinted Is Nothing) AndAlso ReportsToBePrinted.Length > 0, ReportsToBePrinted, "")
            // .................................................
            GetAttachmentRights();
            ViewBag.ServerDateFormat = BASE._Server_Date_Format_Short;
            ViewBag.DateFormatCurrent = BASE._Date_Format_Current;
            ViewBag.LoginUserType = BASE._open_User_Type.ToUpper();
            ViewBag.InsName = BASE._open_Ins_Name;
            ViewBag.CenName = BASE._open_Cen_Name;
            ViewBag.CenUID = BASE._open_UID_No;
            ViewBag.AllowForeignDonation = BASE.Allow_Foreign_Donation;
            ViewBag.Open_User_ID = BASE._open_User_ID;
            ViewBag._open_Ins_ID = BASE._open_Ins_ID;
            ViewBag._open_FY = BASE._open_Year_ID;
            ViewBag.WebConfigSettings = ConfigurationManager.AppSettings.AllKeys.ToDictionary(key => key, key => ConfigurationManager.AppSettings[key]);

            return View("Index");
        }
        [CheckLogin]
        public ActionResult Page() 
        {
            ViewBag.IsGenericLogin = true;
            return Index();
        }
        [CheckLogin]
        public ActionResult IndexDashboard()
        {

            // ...................................
            BASE._open_ClientUser = "NO";
            BASE.Refresh_Notes_List = true;
            try
            {
                DateTime ServerDate = Convert.ToDateTime(BASE._Action_Items_DBOps.GetServerDateTime());
            }
            catch (Exception ex)
            {
            }

            // 'CHECKING CENTRE TASK INFO.......................
            BASE.Allow_Foreign_Donation = false;
            BASE.Allow_Bank_In_C_Box = false;
            BASE.Is_HQ_Centre = false;
            BASE.Allow_Membership = false;
            DataTable PER_DB = BASE._ClientUserDBOps.GetCenterTasks();
            if ((PER_DB == null))
            {
            }

            DataView DV1 = new DataView(PER_DB);
            DV1.Sort = "TASK_NAME";
            if ((DV1.Count > 0))
            {
                int index = DV1.Find("FOREIGN DONATION");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Allow_Foreign_Donation = true;
                    }
                    else
                    {
                        BASE.Allow_Foreign_Donation = false;
                    }

                }

                index = DV1.Find("COLLECTION BOX - BANK");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Allow_Bank_In_C_Box = true;
                    }
                    else
                    {
                        BASE.Allow_Bank_In_C_Box = false;
                    }

                }

                index = DV1.Find("H.Q. CENTRE");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Is_HQ_Centre = true;
                    }
                    else
                    {
                        BASE.Is_HQ_Centre = false;
                    }

                }

                index = DV1.Find("PRINT STATEMENTS");
                if ((index >= 0))
                {
                    if (((DV1[index]["PERMISSION"].ToString().IndexOf("F") + 1)
                                != 0))
                    {
                        BASE.Allow_Statements_Without_Restrictions = true;
                    }
                    else
                    {
                        BASE.Allow_Statements_Without_Restrictions = false;
                    }

                }
            }
            else
            {
                BASE.Allow_Foreign_Donation = false;
                BASE.Allow_Bank_In_C_Box = false;
                BASE.Is_HQ_Centre = false;
                BASE.Allow_Membership = false;
                BASE.Allow_Statements_Without_Restrictions = false;
            }

            if ((BASE._open_Year_Acc_Type == "MEMBERSHIP"))
            {
                BASE.Allow_Membership = true;
            }
            else
            {
                BASE.Allow_Membership = false;
            }

            if ((BASE._open_Year_Acc_Type == "MAGAZINE"))
            {
                BASE.Allow_Magazine = true;
            }
            else
            {
                BASE.Allow_Magazine = false;
            }

            object ReportsToBePrinted = "";
            ReportsToBePrinted = BASE._CenterDBOps.GetReportsToBePrintedInfo(BASE._open_Year_ID);
            BASE._ReportsToBePrinted = ReportsToBePrinted.ToString();
            // BASE._ReportsToBePrinted = IIf((Not ReportsToBePrinted Is Nothing) AndAlso ReportsToBePrinted.Length > 0, ReportsToBePrinted, "")
            // .................................................

            return View();
        }
        [CheckLogin]
        public ActionResult Home(bool IsGenericLogin=false)
        {
            DataTable GetAutoOpenScreen = BASE._UserPreferences_DBOps.AutoOpenScreen_GetList();
            if (GetAutoOpenScreen != null && GetAutoOpenScreen.Rows.Count > 0)
            {
                AutoOpenScreenIDs = GetAutoOpenScreen.Rows.OfType<DataRow>().Select(k => k["SCREENID"].ToString()).ToArray();
            }
            ViewData["AutoOpenScreenIDs"] = AutoOpenScreenIDs;
            DataTable _Table = BASE._News_DBOps.GetCenterNews();
            if (_Table.Rows.Count > 0)
            {
                ViewBag.CenterNews = _Table.Rows[0]["NEWS_HTML"].ToString();
            }
            else { ViewBag.CenterNews = ""; }
            ViewBag.CenID = BASE._open_Cen_ID.ToString();
            ViewBag.IsGenericLogin = IsGenericLogin;


            DataTable dt = BASE._HelpVideos_dbops.get_HelpVideos();
            ViewBag.helpVideos = dt;
            List<string> categories = dt.AsEnumerable()
                                    .Select(row => row["Category"].ToString())
                                    .Distinct()
                                    .ToList();
            ViewBag.categories = categories;

            return View();
        }
        public ActionResult Circulars()
        {
            return View();
        }
        public ActionResult Videos()
        {
            return View();
        }
        public ActionResult HelpVideos()
        {
            DataTable dt = BASE._HelpVideos_dbops.get_HelpVideos();
            ViewBag.helpVideos = dt;
            List<string >categories = dt.AsEnumerable()
                                    .Select(row => row["Category"].ToString())
                                    .Distinct()
                                    .ToList();
            ViewBag.categories = categories;            
            return View();
        }
        public ActionResult GSRVideos()
        {
            return View();
        }
        public ActionResult Circulars_Page()
        {
            return View();
        }
        [CheckLogin]
        public JsonResult Allow_Superuser_Auditor_Operations()
        {
            if (BASE._open_User_Type == "AUDITOR" || BASE._open_User_Type == "SUPERUSER")
            {
                return Json(new
                {
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = false
            }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public JsonResult Allow_HQ_Operations()
        {
            if (BASE.Is_HQ_Centre)
            {
                return Json(new
                {
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = false
            }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public JsonResult get_Footer_Data(bool IsGenericLogin = false)
        {
            String Footer_String = "";

            if (BASE.CheckPrevYearID(BASE._prev_Unaudited_YearID))
            {
                Footer_String += "Previous Yr Status:";
                Footer_String += "NOT SPLIT";
                Footer_String += "     |    ";
            }
            Footer_String += "CenID:" + BASE._open_Cen_ID + "     |    ";
            Footer_String += "User Type:" + BASE._open_User_Type + "     |    ";
            if (BASE._Completed_Year_Count > 0)
            {
                Footer_String += "Year No.:";
                Footer_String += (BASE._Completed_Year_Count + 1).ToString();
                Footer_String += "     |    ";
            }
            Footer_String += "Version :" + typeof(ConnectOneMVC.MvcApplication).Assembly.GetName().Version.ToString() + "     |    ";
            Footer_String += "User :" + BASE._open_User_ID;

            return Json(new
            {
                cenID= BASE._open_Cen_ID,
                UserType= BASE._open_User_Type,
                CompletedYear= BASE._Completed_Year_Count > 0?(BASE._Completed_Year_Count + 1):0,
                Version= typeof(ConnectOneMVC.MvcApplication).Assembly.GetName().Version.ToString(),
                User= BASE._open_User_ID,
                Footer = Footer_String,              
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        [CheckLogin]
        public JsonResult get_Alerts_Data()
        {
            Int32 Action_Item_Count = (Int32)BASE._Action_Items_DBOps.GetOverDueCount(Common_Lib.RealTimeService.ClientScreen.Home_StartUp);
            Int32 Center_Remark_Count = (Int32)BASE._Action_Items_DBOps.GetPendingCentreRemarkCount(Common_Lib.RealTimeService.ClientScreen.Home_StartUp);
            Int32 Asset_Transfer_Count = BASE._AssetTransfer_DBOps.GetUnmatchedCount_AssetTransfer();
            Int32 Internal_Transfer_Count = BASE._Internal_Tf_Voucher_DBOps.GetCenterToCentreUnmatchedCount();
            Int32 Unread_Request_Count = (Int32)BASE._Req_DBOps.GetUnreadCount(Common_Lib.RealTimeService.ClientScreen.Home_StartUp);
            Int32 MonthlyServiceReport = (Int32)BASE._SR_DBOps.GetMonthlyServiceReport();
           // Common_Lib.DbOperations.Attachments.Return_GetAttachmentCount AttachmentCount = BASE._Attachments_DBOps.GetAttachmentCount();

            return Json(new
            {
                Action_Item_Count = Action_Item_Count,
                Center_Remark_Count = Center_Remark_Count,
                Asset_Transfer_Count = Asset_Transfer_Count,
                Internal_Transfer_Count = Internal_Transfer_Count,
                Unread_Request_Count = Unread_Request_Count,
                MonthlyServiceReport,
                //Attachment_Pending = AttachmentCount.PENDING,
                //Attachment_Rejected = AttachmentCount.REJECTED,
                //Attachment_Accepted = AttachmentCount.ACCEPTED,
                //Attachment_Total = AttachmentCount.TOTAL_ATTACHED,
                //Attachment_Total_Reqd = AttachmentCount.TOTAL_REQD,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserAuthMenu() //checks User Authorization for Screens and then hide menus which does not have rights
        {
            List<string> menuList = new List<string>();
            string DynamicMenu= string.Empty;
            if (BASE._Dynamic_Menu_Listing != null && BASE._Dynamic_Menu_Listing.Rows.Count > 0) 
            {
                DynamicMenu=JsonConvert.SerializeObject(BASE._Dynamic_Menu_Listing);
            }
            foreach (DataRow row in BASE._Menu_vibilities_Listing.Rows)
            {
                menuList.Add(row[0].ToString());
            }
            if (BASE._open_User_Type == "CLIENT ROLE")
            {
                return Json(new { result = menuList, message = "CLIENT ROLE",_dynamicMenu= DynamicMenu }, JsonRequestBehavior.AllowGet);
            }
            else if (BASE._open_User_Type == "SUPERUSER")
            {
                return Json(new { result = menuList, message = "SUPERUSER", _dynamicMenu = DynamicMenu }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = menuList, message = "Not_ClientRole", _dynamicMenu = DynamicMenu }, JsonRequestBehavior.AllowGet);
            }
        }
        public void GetAttachmentRights()
        {
            ViewData["Help_Attachments_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["Help_Attachments_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");
            ViewData["Help_Attachments_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");
            ViewData["Help_Attachments_UpdateRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachments_ListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");
        }
    }

}