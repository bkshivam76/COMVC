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
    public class BankAccountReportController : BaseController
    {
        // GET: Reports/BankAccountReport
        #region Global Variables
        public List<DbOperations.Report_Ledgers.BankAccountReport> BankReportExportData
        {
            get
            {
                return (List<DbOperations.Report_Ledgers.BankAccountReport>)GetBaseSession("BankReportExportData_BankACReport");
            }
            set
            {
                SetBaseSession("BankReportExportData_BankACReport", value);
            }
        }
        
        #endregion
        public ActionResult Frm_Bank_Accounts_Report()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Bank_Account_List, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Bank_Account_List').hide();</script>");
            }
            ViewData["BankAccountReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Bank_Account_List, "Export");
            ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Bank_Account_List).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            var _BankData = BASE._Reports_Ledgers_DBOps.GetBankAccountsList();
            BankReportExportData = _BankData;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if ((_BankData.Count == 0))
            {
                return View();
            }
            return View(_BankData);
        }
        public PartialViewResult Frm_Bank_Accounts_Report_Grid(string command = null, int ShowHorizontalBar = 0)
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["BankAccountReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Bank_Account_List, "Export");
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (BankReportExportData == null || command == "REFRESH")
            {
                var BankData = BASE._Reports_Ledgers_DBOps.GetBankAccountsList();
                BankReportExportData = BankData;
            }
            var _BankData = BankReportExportData as List<Common_Lib.DbOperations.Report_Ledgers.BankAccountReport>;
            if ((_BankData.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_BankData);
        }

        #region export       
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Bank_Account_List, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Bank_Report_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_BankACReport");
        }
    }
}