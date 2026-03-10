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
    public class WIPReportController : BaseController
    {
        // GET: Reports/WIPReport
        #region Global Variables
        public List<Common_Lib.DbOperations.Reports_All.WIPReport> WIPReportExportData
        {
            get
            {
                return (List<Common_Lib.DbOperations.Reports_All.WIPReport>)GetBaseSession("WIPReportExportData_WIPReport");
            }
            set
            {
                SetBaseSession("WIPReportExportData_WIPReport", value);
            }
        }        
        #endregion
        public ActionResult Frm_WIP_Report()
        {
            if (!CheckRights(BASE,ClientScreen.Report_WIPInfo,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_WIPInfo').hide();</script>");
            }            
            ViewData["WIPReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_WIPInfo, "Export");

            var _data = BASE._Reports_Common_DBOps.GetWIPReport();
            WIPReportExportData = _data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if ((_data.Count == 0))
            {
                return View();
            }
            return View(_data);
        }

        public PartialViewResult Frm_WIP_Report_Grid(string command = null)
        {
            ViewData["WIPReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_WIPInfo, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (WIPReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetWIPReport();
                WIPReportExportData = data;
            }
            var _data = WIPReportExportData as List<Common_Lib.DbOperations.Reports_All.WIPReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }

        #region export       
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_WIPInfo, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('WIPReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        public ActionResult WIPExportToXLSX_DataAware()
        {
            return PivotGridExtension.ExportToXlsx(WIPPivotGridHelper.Settings, WIPReportExportData, true);
        }
        public ActionResult WIPExportToXLS_DataAware()
        {
            return PivotGridExtension.ExportToXls(WIPPivotGridHelper.Settings, WIPReportExportData, true);
        }
        public ActionResult WIPExportToPDF_DataAware()
        {
            return PivotGridExtension.ExportToPdf(WIPPivotGridHelper.Settings, WIPReportExportData, true);
        }
        public ActionResult WIPExportToCSV_DataAware()
        {
            return PivotGridExtension.ExportToCsv(WIPPivotGridHelper.Settings, WIPReportExportData, true);
        }
        public ActionResult WIPExportToDOCX_DataAware()
        {
            return PivotGridExtension.ExportToDocx(WIPPivotGridHelper.Settings, WIPReportExportData, true);
        }
        public ActionResult WIPExportToRTF_DataAware()
        {
            return PivotGridExtension.ExportToRtf(WIPPivotGridHelper.Settings, WIPReportExportData, true);
        }

        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_WIPReport");
        }
    }
    public class WIPPivotGridHelper
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
            settings.Name = "WIPReportListGrid";

            settings.SettingsExport.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PrintRowAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PageSettings.Landscape = true;

            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                field.AreaIndex = 0;
                field.FieldName = "Month";
                field.Caption = "Month";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                field.AreaIndex = 0;
                field.FieldName = "MonthNo";
                field.Caption = "MonthNo";
                field.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Ascending;
                field.Visible = true;
                field.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value;
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
                field.AreaIndex = 0;
                field.FieldName = "Amt";
                field.Caption = "Amt";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea;
                field.AreaIndex = 0;
                field.FieldName = "Amt";
                field.Caption = "Amt";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 0;
                field.FieldName = "_Property";
                field.Caption = "Property";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 1;
                field.FieldName = "Items";
                field.Caption = "Items";
            });
            return settings;
        }
    }
}