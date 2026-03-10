using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    [CheckLogin]
    public class AssetMovementLogsController : BaseController
    {
        // GET: Reports/AssetMovementLogs
        #region Global Variables
        public List<DbOperations.Reports_All.AssetLocationLogs> AssetMovementLogsExportData
        {
            get
            {
                return (List<DbOperations.Reports_All.AssetLocationLogs>)GetBaseSession("AssetMovementLogsExportData_AsetMoveLog");
            }
            set
            {
                SetBaseSession("AssetMovementLogsExportData_AsetMoveLog", value);
            }
        }        
        #endregion
        public ActionResult Frm_Asset_Location_Logs()
        {
            if (!CheckRights(BASE,ClientScreen.Report_Asset_Movement_Logs,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Asset_Movement_Logs').hide();</script>");
            }
            
            ViewData["AsetMovLog_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Asset_Movement_Logs, "Export");
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Asset_Movement_Logs).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            var _Assetdata = BASE._Reports_Common_DBOps.GetAssetMovementLogs(Common_Lib.RealTimeService.ClientScreen.Report_Asset_Movement_Logs);
            AssetMovementLogsExportData = _Assetdata;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if ((_Assetdata.Count == 0))
            {
                return View(_Assetdata);
            }            
            return View(_Assetdata);
        }

        public PartialViewResult Frm_Asset_Location_Logs_Grid(string command = null, int ShowHorizontalBar = 0,bool VouchingMode=false)
        {
            ViewData["AsetMovLog_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Asset_Movement_Logs, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            if (AssetMovementLogsExportData == null || command == "REFRESH")
            {
                var Assetdata = BASE._Reports_Common_DBOps.GetAssetMovementLogs(Common_Lib.RealTimeService.ClientScreen.Report_Asset_Movement_Logs);
                AssetMovementLogsExportData = Assetdata;
            }
            var _Assetdata = AssetMovementLogsExportData as List<Common_Lib.DbOperations.Reports_All.AssetLocationLogs>;
            if ((_Assetdata.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_Assetdata);
        }
        public void SessionClear()
        {
            ClearBaseSession("_AsetMoveLog");
        }

        #region export       
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Report_Asset_Movement_Logs, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Asset_Location_Logs_report_modal','Not Allowed','No Rights');$('#Asset_Location_LogsModelListPreview').hide();</script>");
            }
            return PartialView();
        }
        #endregion
    }
}