using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using System;
using System.Web.Mvc;
using System.Data;
using DevExpress.Web.Mvc;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    public class VoucherReferenceReportController : BaseController
    {
        // GET: Reports/VoucherReferenceReport
        public DataTable VoucherReference_Data 
        {
            get { return (DataTable)GetBaseSession("VoucherReference_Data_VoucherReferenceReport"); }
            set { SetBaseSession("VoucherReference_Data_VoucherReferenceReport", value); }
        }
        public ActionResult Frm_VoucherReference_Report()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Purpose, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Purpose').hide();</script>");
            }
            ViewData["VoucherReference_Report_ExportRight"] = CheckRights(BASE, ClientScreen.Report_VoucherReference, "Export");
            VoucherReference_Data= BASE._Reports_Common_DBOps.GetVoucherReferenceReport(Common_Lib.RealTimeService.ClientScreen.Report_VoucherReference);
           
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
      
            return View(VoucherReference_Data);
        }
        public ActionResult Frm_VoucherReference_Report_Grid(string command) 
        {
            ViewData["VoucherReference_Report_ExportRight"] = CheckRights(BASE, ClientScreen.Report_VoucherReference, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (VoucherReference_Data == null || command == "REFRESH")
            {
                VoucherReference_Data = BASE._Reports_Common_DBOps.GetVoucherReferenceReport(Common_Lib.RealTimeService.ClientScreen.Report_VoucherReference);             
            }
            return View(VoucherReference_Data);
        }
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_VoucherReference, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('VoucherReferenceReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return View();
        }
        public ActionResult VoucherReferenceExportToXLSX_DataAware()
        {
            return PivotGridExtension.ExportToXlsx(GetSettings(), VoucherReference_Data, true);
        }
        public ActionResult VoucherReferenceExportToXLS_DataAware()
        {
            return PivotGridExtension.ExportToXls(GetSettings(), VoucherReference_Data, true);
        }
        public ActionResult VoucherReferenceExportToPDF_DataAware()
        {
            return PivotGridExtension.ExportToPdf(GetSettings(), VoucherReference_Data, true);
        }
        public ActionResult VoucherReferenceExportToCSV_DataAware()
        {
            return PivotGridExtension.ExportToCsv(GetSettings(), VoucherReference_Data, true);
        }
        public ActionResult VoucherReferenceExportToDOCX_DataAware()
        {
            return PivotGridExtension.ExportToDocx(GetSettings(), VoucherReference_Data, true);
        }
        public ActionResult VoucherReferenceExportToRTF_DataAware()
        {
            return PivotGridExtension.ExportToRtf(GetSettings(), VoucherReference_Data, true);
        }
        public static PivotGridSettings GetSettings()
        {
            PivotGridSettings settings = new PivotGridSettings();
            settings.Name = "VoucherReferenceReportListGrid";

            settings.SettingsExport.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PrintRowAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PageSettings.Landscape = true;
            settings.SettingsExport.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.OptionsPrint.PageSettings.Margins.Bottom = 10;         
            settings.SettingsExport.OptionsPrint.PageSettings.Margins.Top = 10; 
            settings.SettingsExport.OptionsPrint.PageSettings.Margins.Left = 10; 
            settings.SettingsExport.OptionsPrint.PageSettings.Margins.Right = 10;    
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 2;
                field.FieldName = "Date";
                field.Caption = "Date";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 1;
                field.FieldName = "Item";
                field.Caption = "Item";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 0;
                field.FieldName = "Ledger";
                field.Caption = "Ledger";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                field.AreaIndex = 0;
                field.FieldName = "Voucher_Ref";
                field.Caption = "Voucher Ref";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
                field.AreaIndex = 1;
                field.FieldName = "Amount";
                field.Caption = "Amount";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea;
                field.AreaIndex = 1;
                field.FieldName = "Nature";
                field.Caption = "Nature";
            });

            return settings;
        }
    
    public void SessionClear()
        {
            ClearBaseSession("_VoucherReferenceReport");
        }
    }
}