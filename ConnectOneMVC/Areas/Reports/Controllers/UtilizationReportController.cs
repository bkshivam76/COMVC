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
    public class UtilizationReportController : BaseController
    {
        // GET: Reports/UtilizationReport
        #region Global Variables
        public string Utilization_Percent
        {
            get
            {
                return (string)GetBaseSession("Utilization_Percent_UtRepo");
            }
            set
            {
                SetBaseSession("Utilization_Percent_UtRepo", value);
            }
        }
        public DataSet UtilizationReportExportData//List<DbOperations.Reports_All.UtilizationReport> 
        {
            get
            {
                return (DataSet)GetBaseSession("UtilizationReportExportData_UtRepo");
            }
            set
            {
                SetBaseSession("UtilizationReportExportData_UtRepo", value);
            }
        }        
        #endregion
        public ActionResult Frm_Utilization_Report()
        {
            if (!CheckRights(BASE,ClientScreen.Report_Utilization,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Utilization').hide();</script>");
            }

            ViewData["UtilizationReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Utilization, "Export");
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Utilization).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            DataSet _data = BASE._Reports_Common_DBOps.GetUtilizationReport(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, BASE._open_Year_Sdt, BASE._open_Year_Edt);
            //UtilizationReportExportData = _data;
            Utilization_Percent = Convert.ToDecimal(Convert.ToString(_data.Tables[2].Rows[0]["Utilization Percentage"].ToString())) < 0 ? "You have Completed Your Utilization of Income! Om Shanti" : (Convert.ToString(_data.Tables[2].Rows[0]["Utilization Percentage"].ToString()) );// UtilizationPercentage();
            ViewBag.Utilization_Percent = Utilization_Percent;
            ViewBag.Pending_Utilization = Convert.ToDecimal(Convert.ToString(_data.Tables[2].Rows[0]["Pending Utilization"].ToString())) < 0 ? "0" : _data.Tables[2].Rows[0]["Pending Utilization"].ToString();
            ViewBag.Pending_Corpus = _data.Tables[2].Rows[0]["Pending Corpus"].ToString();
            ViewBag.Change_FD = _data.Tables[2].Rows[0]["Increase/ (decrease) in Investments in Fixed Deposits - Section 11(5)"].ToString();
            ViewBag.Change_Oth = _data.Tables[2].Rows[0]["Increase/ (decrease) in Investments other than Fixed Deposits - Section 11(5)"].ToString();
            ViewBag.Change_Cash = _data.Tables[2].Rows[0]["Increase/ (decrease) in Cash In Hand"].ToString();
            ViewBag.Sale_Receivable = _data.Tables[2].Rows[0]["Sales Consideration Receivable"].ToString();
            ViewBag.Total_Change_Funds = (Convert.ToDecimal(_data.Tables[2].Rows[0]["Increase/ (decrease) in Investments in Fixed Deposits - Section 11(5)"]) + Convert.ToDecimal(_data.Tables[2].Rows[0]["Increase/ (decrease) in Investments other than Fixed Deposits - Section 11(5)"]) + Convert.ToDecimal(_data.Tables[2].Rows[0]["Increase/ (decrease) in Cash In Hand"]) + Convert.ToDecimal(_data.Tables[2].Rows[0]["Sales Consideration Receivable"])).ToString();
            ViewBag.Excess_Shortfall = Convert.ToDecimal(_data.Tables[2].Rows[0]["Excess of Application Over Income"]) > 0 ? (_data.Tables[2].Rows[0]["Excess of Application Over Income"]).ToString() : (_data.Tables[2].Rows[0]["Shortfall of Application"]).ToString();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["UtilizationReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            //if ((_data.Count == 0))
            //{
            //    return View();
            //}
            return View(_data);
        }

        public PartialViewResult Frm_Utilization_ReceiptReport_Grid(string command = null, int ShowHorizontalBar = 0,bool VouchingMode=false,string Layout=null)
        {
            ViewData["UtilizationReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Utilization, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewData["Layout"] = Layout;
            if (UtilizationReportExportData == null || command == "REFRESH")
            {
                UtilizationReportExportData = BASE._Reports_Common_DBOps.GetUtilizationReport(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, BASE._open_Year_Sdt, BASE._open_Year_Edt);
            }
            //Utilization_Percent = "Total Utilization : " + (Convert.ToString(UtilizationReportExportData.Tables[2].Rows[0]["Utilization Percentage"].ToString()) + "% approx.");
            //if ((_data.Count == 0))
            //{
            //    return PartialView();
            //}
            return PartialView(UtilizationReportExportData.Tables[0]);
        }
        public PartialViewResult Frm_Utilization_PaymentReport_Grid(string command = null, int ShowHorizontalBar = 0, bool VouchingMode = false, string Layout = null)
        {
            ViewData["UtilizationReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Utilization, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewData["Layout"] = Layout;
            if (UtilizationReportExportData == null || command == "REFRESH")
            {
                UtilizationReportExportData = BASE._Reports_Common_DBOps.GetUtilizationReport(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, BASE._open_Year_Sdt, BASE._open_Year_Edt);
                //UtilizationReportExportData = data;
            }
            //Utilization_Percent = "Total Utilization : " + (Convert.ToString(UtilizationReportExportData.Tables[2].Rows[0]["Utilization Percentage"].ToString()) + "% approx.");
            //if ((_data.Count == 0))
            //{
            //    return PartialView();
            //}
            return PartialView(UtilizationReportExportData.Tables[1]);
        }

        //private String UtilizationPercentage()
        //{
        //    Decimal TotReceipts = 0;
        //    Decimal TotPayments = 0;
        //    foreach (DbOperations.Reports_All.UtilizationReport cRow in UtilizationReportExportData as List<DbOperations.Reports_All.UtilizationReport>)
        //    {
        //        decimal _out= 0;
        //        //TotReceipts = (TotReceipts + (decimal.TryParse(cRow.IAmount, out _out) ? Convert.ToDecimal(cRow.IAmount) : 0));
        //        //TotPayments = (TotPayments + (decimal.TryParse(cRow.IPAmount, out _out) ? Convert.ToDecimal(cRow.IPAmount) : 0));
        //        TotReceipts = (TotReceipts + Convert.ToDecimal(cRow.IAmount)) ;
        //        TotPayments = (TotPayments + Convert.ToDecimal(cRow.IPAmount));
        //    }
        //   decimal UtilizePercent = Decimal.Round(Convert.ToDecimal((100 * (TotPayments / TotReceipts))), 2);
        //    return ("Total Utilization : " + (Convert.ToString(UtilizePercent) + "% approx."));
        //}
        public JsonResult GetCurrentYearUtilizationPercentage()
        {
            Decimal TotReceipts = 0;
            Decimal TotPayments = 0;
            UtilizationReportExportData = BASE._Reports_Common_DBOps.GetUtilizationReport(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, BASE._open_Year_Sdt, BASE._open_Year_Edt);
            //foreach (DbOperations.Reports_All.UtilizationReport cRow in _data as List<DbOperations.Reports_All.UtilizationReport>)
            //{
            //    decimal _out = 0;
            //    //TotReceipts = (TotReceipts + (decimal.TryParse(cRow.IAmount, out _out) ? Convert.ToDecimal(cRow.IAmount) : 0));
            //    //TotPayments = (TotPayments + (decimal.TryParse(cRow.IPAmount, out _out) ? Convert.ToDecimal(cRow.IPAmount) : 0));
            //    TotReceipts = (TotReceipts + Convert.ToDecimal(cRow.IAmount));
            //    TotPayments = (TotPayments + Convert.ToDecimal(cRow.IPAmount));
            //}
            decimal UtilizePercent = Decimal.Round(Convert.ToDecimal(UtilizationReportExportData.Tables[2].Rows[0]["Utilization Percentage"]));
            ConnectOneMVC.Models.Return_Json_Param jsonParam = new ConnectOneMVC.Models.Return_Json_Param();
            jsonParam.message = UtilizePercent.ToString();
            jsonParam.result = true;
            int cen_id = BASE._open_Cen_ID;
            string user_id = BASE._open_User_ID;
            decimal PendingUtilization = Decimal.Round(Convert.ToDecimal(UtilizationReportExportData.Tables[2].Rows[0]["Pending Utilization"]));
            return Json(new
            {
                jsonParam,
                cen_id,
                user_id,
                UtilizePercent, 
                PendingUtilization
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckBigExpenseDeclarationExists()
        {
            DataTable _data = BASE._Chart_DBOps.GetResponseForChartInstance(3008,BASE._open_Cen_ID);//Remove this hardcoded value and make it dynamic
           
            ConnectOneMVC.Models.Return_Json_Param jsonParam = new ConnectOneMVC.Models.Return_Json_Param();
            jsonParam.message = "";
            jsonParam.result = false;
            int cen_id = BASE._open_Cen_ID;
            string user_id = BASE._open_User_ID;
            string user_type = BASE._open_User_Type.ToUpper();
            string ResponseID = "";
            if (_data.Rows.Count > 0) { ResponseID = _data.Rows[0][0].ToString(); jsonParam.message = "Response Exists"; jsonParam.result = true; }
            return Json(new
            {
                jsonParam,
                cen_id,
                user_id,
                user_type,
                ResponseID
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options_Utilization()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Utilization, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('UtilizationReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        public void SessionClear()
        {
            ClearBaseSession("_UtRepo");
        }
    }
}