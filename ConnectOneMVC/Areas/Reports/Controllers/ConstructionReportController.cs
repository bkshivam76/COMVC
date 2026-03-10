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
using DevExpress.Utils;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    [CheckLogin]
    public class ConstructionReportController : BaseController
    {
        // GET: Reports/ConstructionReport
        #region Global Variables
        public List<DbOperations.Reports_All.ConstructionReport> ConstructionReportExportData
        {
            get
            {
                return (List<DbOperations.Reports_All.ConstructionReport>)GetBaseSession("ConstructionReportExportData_ConstruReport");
            }
            set
            {
                SetBaseSession("ConstructionReportExportData_ConstruReport", value);
            }
        }        
        #endregion
        public ActionResult Frm_Construction_Report()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Construction_Statement,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_Construction').hide();</script>");
            }
            ViewData["ConstructionReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Construction_Statement, "Export");
            var _data = BASE._Reports_Common_DBOps.GetConstructionReport(BASE._open_Year_Sdt, BASE._open_Year_Edt, Common_Lib.RealTimeService.ClientScreen.Report_Construction_List);
            ConstructionReportExportData = _data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if ((_data.Count == 0))
            {
                return View();
            }
            return View(_data);
        }

        public PartialViewResult Frm_Construction_Report_Grid(string command = null)
        {
            ViewData["ConstructionReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Construction_Statement, "Export");            
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (ConstructionReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetConstructionReport(BASE._open_Year_Sdt, BASE._open_Year_Edt, Common_Lib.RealTimeService.ClientScreen.Report_Construction_List);
                ConstructionReportExportData = data;
            }
            var _data = ConstructionReportExportData as List<Common_Lib.DbOperations.Reports_All.ConstructionReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }        

        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Construction_Statement, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('ConstructionReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        public ActionResult ExportToXLSX_DataAware()
        {
            return PivotGridExtension.ExportToXlsx(PivotGridHelper.Settings, ConstructionReportExportData,true);
        }
        public ActionResult ExportToXLS_DataAware()
        {
            return PivotGridExtension.ExportToXls(PivotGridHelper.Settings, ConstructionReportExportData, true);
        }
        public ActionResult ExportToPDF_DataAware()
        {
            return PivotGridExtension.ExportToPdf(PivotGridHelper.Settings, ConstructionReportExportData,true);
        }
        public ActionResult ExportToCSV_DataAware()
        {
            return PivotGridExtension.ExportToCsv(PivotGridHelper.Settings, ConstructionReportExportData, true);
        }
        public ActionResult ExportToDOCX_DataAware()
        {
            return PivotGridExtension.ExportToDocx(PivotGridHelper.Settings, ConstructionReportExportData, true);
        }
        public ActionResult ExportToRTF_DataAware()
        {
            return PivotGridExtension.ExportToRtf(PivotGridHelper.Settings, ConstructionReportExportData, true);
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_ConstruReport");
        }
    }
    public class PivotGridHelper
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
            settings.Name = "ConstructionReportListGrid";                      
            
            settings.SettingsExport.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PrintRowAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PageSettings.Landscape = true;

            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                field.AreaIndex = 0;
                field.FieldName = "Month";
                field.Caption = "Month";
                field.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
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
                field.CellFormat.FormatType = FormatType.Numeric;
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