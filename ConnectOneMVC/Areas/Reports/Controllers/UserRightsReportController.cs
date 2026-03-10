using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using DevExpress.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    public class UserRightsReportController : BaseController
    {
        public DataTable UserRights_ExportData
        {
            get
            {
                return (DataTable)GetBaseSession("UserRights_ExportData_UserRightsReport");
            }
            set
            {
                SetBaseSession("UserRights_ExportData_UserRightsReport", value);
            }

        }


        // GET: Reports/UserRightsReport
        public ActionResult Frm_User_Rights_Report_Info()
        {
            UserRights();
            if ((bool)ViewData["Report_UserRights_ListRight"]==false)
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(true);</script>");//Code written for User Authorization do not remove                
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_User_Rights).ToString()) ? 1 : 0;

            Grid_Display();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(UserRights_ExportData);
        }

        public PartialViewResult Frm_User_Rights_Report_Info_Grid(string command, int ShowHorizontalBar = 0, string Layout = null)
        {
            UserRights();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            if (command == "REFRESH")
            {
                Grid_Display();
            }

            return PartialView(UserRights_ExportData);
        }



        public void Grid_Display()
        {


            UserRights_ExportData = BASE._Reports_Common_DBOps.UserRightsReportList();


        }

        public void SessionClear()
        {
            ClearBaseSession("_UserRightsReport");

        }

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Report_User_Rights, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>NoRightsPrevention(false,'Dynamic_Content_popup')</script>");//Code written for User Authorization do not remove                
            }
            return PartialView();
        }
        public void UserRights() 
        {
            ViewData["Report_UserRights_ExportRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Report_User_Rights, "Export");
            ViewData["Report_UserRights_ListRight"] = CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Report_User_Rights, "List");
        }
        #endregion
    }
}