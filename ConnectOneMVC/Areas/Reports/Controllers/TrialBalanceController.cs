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
using ConnectOneMVC.Areas.Reports.Models;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    [CheckLogin]
    public class TrialBalanceController : BaseController
    {
        // GET: Reports/TrialBalance
        #region Global Variables
        public List<Common_Lib.DbOperations.Reports_All.TrialBalanceReport> TrialBalanceReportExportData
        {
            get
            {
                return (List<Common_Lib.DbOperations.Reports_All.TrialBalanceReport>)GetBaseSession("TrialBalanceReportExportData_TB");
            }
            set
            {
                SetBaseSession("TrialBalanceReportExportData_TB", value);
            }
        }        
        #endregion
        public ActionResult Frm_Trial_Balance()
        {
            if (!CheckRights(BASE,ClientScreen.Report_TB,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_TB').hide();</script>");
            }
            ViewData["TrialBalance_ExportRight"] = CheckRights(BASE, ClientScreen.Report_TB, "Export");
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_TB).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            TrialBalanceModel model = new TrialBalanceModel();
            model.SVR_DD_TB_Report = "ALL";
            model.SVR_List = LookUp_GetSVRList();             
            model.Grid_Data = BASE._Reports_Common_DBOps.GetReportTrialBalance(Common_Lib.RealTimeService.ClientScreen.Report_Potamail, BASE._open_Year_Sdt, BASE._open_Year_Edt).OrderBy(o => o.NATURE).ToList();
            TrialBalanceReportExportData = model.Grid_Data;
            ViewBag.Filename = "Trial_Balance_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["TrialBalance_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if ((model.Grid_Data.Count == 0))
            {
                return View();
            }            
            return View(model);
        }

        public PartialViewResult Frm_Trial_Balance_Grid(string command = null, int ShowHorizontalBar = 0,bool VouchingMode=false, string specialVoucherReference = "ALL")
        {
            ViewData["TrialBalance_ExportRight"] = CheckRights(BASE, ClientScreen.Report_TB, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.Filename = "Trial_Balance_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            if (TrialBalanceReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetReportTrialBalance(Common_Lib.RealTimeService.ClientScreen.Report_Potamail, BASE._open_Year_Sdt, BASE._open_Year_Edt, specialVoucherReference).OrderBy(o => o.NATURE).ToList();
                TrialBalanceReportExportData = data;
            }
            var _data = TrialBalanceReportExportData as List<Common_Lib.DbOperations.Reports_All.TrialBalanceReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }
        public List<SVR_List> LookUp_GetSVRList()
        {
            DataTable d1 = BASE._Reports_Common_DBOps.GetSVRList(ClientScreen.Report_TB);
            DataView dview = new DataView(d1);
            dview.Sort = "TASK_NAME";
            return DatatableToModel.DataTabletoTB_Report_GetSVRList(dview.ToTable());
        }
        #region export  
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_TB, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>$('#TrialBalanceReport_report_modal').dxPopup('hide');DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_TB");
        }
    }
}