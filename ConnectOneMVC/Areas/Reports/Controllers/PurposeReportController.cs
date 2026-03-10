using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Utils;
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
    public class PurposeReportController : BaseController
    {
        #region Global Variables
        public List<DbOperations.Reports_All.PurposeReport> PurposeReportExportData
        {
            get
            {
                return (List<DbOperations.Reports_All.PurposeReport>)GetBaseSession("PurposeReportExportData_PurposeReport");
            }
            set
            {
                SetBaseSession("PurposeReportExportData_PurposeReport", value);
            }
        }
        
        #endregion
        public ActionResult Frm_Purpose_Report()
        {
            if(!CheckRights(BASE,ClientScreen.Report_Purpose,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Purpose').hide();</script>");
            }
            ViewData["Purpose_Report_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Purpose, "Export");
            var _data = BASE._Reports_Common_DBOps.GetPurposeReport(Common_Lib.RealTimeService.ClientScreen.Report_Purpose);
            PurposeReportExportData = _data;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if ((_data.Count == 0))
            {
                return View();
            }
            return View(_data);
        }
        public PartialViewResult Frm_Purpose_Report_Grid(string command = null)
        {
            ViewData["Purpose_Report_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Purpose, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (PurposeReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetPurposeReport(Common_Lib.RealTimeService.ClientScreen.Report_Purpose);
                PurposeReportExportData = data;
            }
            var _data = PurposeReportExportData as List<Common_Lib.DbOperations.Reports_All.PurposeReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }

        #region export       
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Purpose, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('PurposeReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        public ActionResult PurposeExportToXLSX_DataAware()
        {
            return PivotGridExtension.ExportToXlsx(PurposePivotGridHelper.Settings, PurposeReportExportData, true);
        }
        public ActionResult PurposeExportToXLS_DataAware()
        {
            return PivotGridExtension.ExportToXls(PurposePivotGridHelper.Settings, PurposeReportExportData, true);
        }
        public ActionResult PurposeExportToPDF_DataAware()
        {
            return PivotGridExtension.ExportToPdf(PurposePivotGridHelper.Settings, PurposeReportExportData, true);
        }
        public ActionResult PurposeExportToCSV_DataAware()
        {
            return PivotGridExtension.ExportToCsv(PurposePivotGridHelper.Settings, PurposeReportExportData, true);
        }
        public ActionResult PurposeExportToDOCX_DataAware()
        {
            return PivotGridExtension.ExportToDocx(PurposePivotGridHelper.Settings, PurposeReportExportData, true);
        }
        public ActionResult PurposeExportToRTF_DataAware()
        {
            return PivotGridExtension.ExportToRtf(PurposePivotGridHelper.Settings, PurposeReportExportData, true);
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_PurposeReport");
        }
    }
    public class PurposePivotGridHelper
    {
        static PivotGridSettings _settings;
        public static PivotGridSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = GetSettings();
                }
                return _settings;
            }
        }        
        static PivotGridSettings GetSettings()
        {            
            PivotGridSettings settings = new PivotGridSettings();
            settings.Name = "PurposeReportListGrid";                      
            
            settings.SettingsExport.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PrintRowAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PageSettings.Landscape = true;

            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 2;
                field.FieldName = "TxnDate";
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
                field.FieldName = "Purpose";
                field.Caption = "Purpose";
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
    }
}