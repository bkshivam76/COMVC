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
    public class IncomeExpenditureController : BaseController
    {
        // GET: Reports/IncomeExpenditure
        #region  Global Variables
        public List<DbOperations.Reports_All.IncomeExpenditureReport> IncomeExpReportExportData
        {
            get
            {
                return (List<DbOperations.Reports_All.IncomeExpenditureReport>)GetBaseSession("IncomeExpReportExportData_IE");
            }
            set
            {
                SetBaseSession("IncomeExpReportExportData_IE", value);
            }
        }
        
        #endregion

        public ActionResult Frm_Income_Expenditure_Report()
        {
            if (!CheckRights(BASE,ClientScreen.Report_Income_Expenditure, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Income_Expenditure').hide();</script>");
            }
            ViewData["IncomeExpenditure_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Income_Expenditure, "Export");
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Income_Expenditure).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            var _data = BASE._Reports_Common_DBOps.GetReportIncomeExpenditure(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, BASE._open_Year_Sdt, BASE._open_Year_Edt);
            IncomeExpReportExportData = _data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["IncomeExpenditure_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if ((_data.Count == 0))
            {
                return View();
            }
            return View(_data);
        }

        public PartialViewResult Frm_Expenditure_Report_Grid(string command = null, int ShowHorizontalBar = 0,bool VouchingMode=false)
        {
            ViewData["IncomeExpenditure_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Income_Expenditure, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            if (IncomeExpReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetReportIncomeExpenditure(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, BASE._open_Year_Sdt, BASE._open_Year_Edt);
                IncomeExpReportExportData = data;
            }
            var _data = IncomeExpReportExportData as List<Common_Lib.DbOperations.Reports_All.IncomeExpenditureReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }
        public PartialViewResult Frm_Income_Report_Grid(string command = null, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewData["IncomeExpenditure_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Income_Expenditure, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            if (IncomeExpReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetReportIncomeExpenditure(Common_Lib.RealTimeService.ClientScreen.Report_Income_Expenditure, BASE._open_Year_Sdt, BASE._open_Year_Edt);
                IncomeExpReportExportData = data;
            }
            var _data = IncomeExpReportExportData as List<Common_Lib.DbOperations.Reports_All.IncomeExpenditureReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }
        #region export    
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Income_Expenditure, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('IncomeExpReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_IE");
        }
    }
}