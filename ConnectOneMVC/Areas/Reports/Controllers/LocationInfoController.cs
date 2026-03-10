using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
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
    public class LocationInfoController : BaseController
    {
        // GET: Reports/LocationInfo
        #region Global Variables
        public List<DbOperations.AssetLocations.LocationInfoReport> LocationInfoReportExportData
        {
            get
            {
                return (List<DbOperations.AssetLocations.LocationInfoReport>)GetBaseSession("LocationInfoReportExportData_LocInfo");
            }
            set
            {
                SetBaseSession("LocationInfoReportExportData_LocInfo", value);
            }
        }
        
        #endregion
        public ActionResult Frm_Location_Map_Info( string assetid = null)
        {
            if (!CheckRights(BASE, ClientScreen.Report_Location,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Location').hide();</script>");
            }

            ViewData["LocationInfo_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Location, "Export");
            var _data = BASE._AssetLocDBOps.GetLocationMapping(Common_Lib.RealTimeService.ClientScreen.Profile_Core, BASE._open_PAD_No_Main);
            LocationInfoReportExportData = _data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["LocationReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Location).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            if ((_data.Count == 0))
            {   
                return View();
            }
            return View(_data);
        }
        public PartialViewResult Frm_Location_Map_Info_Grid(int ShowHorizontalBar = 0, string command = null,bool VouchingMode=false)
        {
            ViewData["LocationInfo_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Location, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            if (LocationInfoReportExportData == null || command == "REFRESH")
            {
                var data = BASE._AssetLocDBOps.GetLocationMapping(Common_Lib.RealTimeService.ClientScreen.Profile_Core, BASE._open_PAD_No_Main);
                LocationInfoReportExportData = data;
            }
            var _data = LocationInfoReportExportData as List<Common_Lib.DbOperations.AssetLocations.LocationInfoReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }
        #region export       
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Location, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('LocationReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_LocInfo");
        }
    }
}