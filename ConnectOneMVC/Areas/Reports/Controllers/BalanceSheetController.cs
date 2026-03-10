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
    public class BalanceSheetController : BaseController
    {
        // GET: Reports/BalanceSheet
        #region Global Variables
        public List<DbOperations.Reports_All.BalanceSheetReport> BalanceSheetReportExportData
        {
            get
            {
                return (List<DbOperations.Reports_All.BalanceSheetReport>)GetBaseSession("BalanceSheetReportExportData_BS");
            }
            set
            {
                SetBaseSession("BalanceSheetReportExportData_BS", value);
            }
        }
        
        #endregion
        public ActionResult Frm_Balance_Sheet_Report()
        {
            if (!CheckRights(BASE,ClientScreen.Report_BS,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_BS').hide();</script>");
            }

            ViewData["BalanceSheet_ExportRight"] = CheckRights(BASE, ClientScreen.Report_BS, "Export");
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_BS).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            var _data = BASE._Reports_Common_DBOps.GetReportBalanceSheet(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, BASE._open_Year_Sdt, BASE._open_Year_Edt);
            BalanceSheetReportExportData = _data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["BalanceSheet_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if ((_data.Count == 0))
            {
                return View();
            }
            return View(_data);
        }
        public PartialViewResult Frm_Balance_Sheet_Liability_Report_Grid(string command = null, int ShowHorizontalBar = 0,bool VouchingMode=false)
        {
            ViewData["BalanceSheet_ExportRight"] = CheckRights(BASE, ClientScreen.Report_BS, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            if (BalanceSheetReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetReportBalanceSheet(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, BASE._open_Year_Sdt, BASE._open_Year_Edt);
                BalanceSheetReportExportData = data;
            }
            var _data = BalanceSheetReportExportData as List<Common_Lib.DbOperations.Reports_All.BalanceSheetReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }
        public PartialViewResult Frm_Balance_Sheet_Asset_Report_Grid(string command = null, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewData["BalanceSheet_ExportRight"] = CheckRights(BASE, ClientScreen.Report_BS, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            if (BalanceSheetReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetReportBalanceSheet(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, BASE._open_Year_Sdt, BASE._open_Year_Edt);
                BalanceSheetReportExportData = data;
            }
            var _data = BalanceSheetReportExportData as List<Common_Lib.DbOperations.Reports_All.BalanceSheetReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }

        #region export     
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_BS, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('BalanceSheetReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_BS");
        }
    }
}