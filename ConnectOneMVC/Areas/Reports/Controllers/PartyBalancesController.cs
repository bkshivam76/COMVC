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
    public class PartyBalancesController : BaseController
    {
        // GET: Reports/PartyBalances
        #region Global Variables        
        public List<DbOperations.Report_Ledgers.PartyBalancesReport> PartyBalancesReportExportData
        {
            get
            {
                return (List<DbOperations.Report_Ledgers.PartyBalancesReport>)GetBaseSession("PartyBalancesReportExportData_PartyBal");
            }
            set
            {
                SetBaseSession("PartyBalancesReportExportData_PartyBal", value);
            }
        }
        
        #endregion
        public ActionResult Frm_Party_Listing()
        {
            if (!CheckRights(BASE, ClientScreen.Report_PartyListing, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_PartyListing').hide();</script>");
            }
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_PartyListing).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            var param = new Common_Lib.RealTimeService.Param_GetPartyListing();
            param.FromDate = BASE._open_Year_Sdt;
            param.ToDate = BASE._open_Year_Edt;
            var _data = BASE._Reports_Ledgers_DBOps.GetPartyList(param);
            PartyBalancesReportExportData = _data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["PartyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_PartyListing,"Export");
            ViewData["PartyBalances_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            if ((_data.Count == 0))
            {
                return View();
            }
            return View(_data);
        }

        public PartialViewResult Frm_Party_Listing_Grid(string command = null, int ShowHorizontalBar = 0,bool VouchingMode=false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["PartyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_PartyListing, "Export");
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (PartyBalancesReportExportData == null || command == "REFRESH")
            {
                var param = new Common_Lib.RealTimeService.Param_GetPartyListing();
                param.FromDate = BASE._open_Year_Sdt;
                param.ToDate = BASE._open_Year_Edt;
                var data = BASE._Reports_Ledgers_DBOps.GetPartyList(param);
                PartyBalancesReportExportData = data;
            }
            var _data = PartyBalancesReportExportData as List<Common_Lib.DbOperations.Report_Ledgers.PartyBalancesReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }
        
        #region export       
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_PartyListing, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('PartyBalancesReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_PartyBal");
        }
    }
}