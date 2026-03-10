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
    public class AssetInsValReportController : BaseController
    {
        #region Global Variables
        public List<DbOperations.Reports_All.AssetInsuranceBreakUpReport> AssetInsBreakupReportExportData
        {
            get
            {
                return (List<DbOperations.Reports_All.AssetInsuranceBreakUpReport>)GetBaseSession("AssetInsBreakupReportExportData_AssetInsValReport");
            }
            set
            {
                SetBaseSession("AssetInsBreakupReportExportData_AssetInsValReport", value);
            }
        }        
        #endregion
        public ActionResult Frm_Asset_Breakup_Info()
        {            
            if (!CheckRights(BASE,ClientScreen.Report_AssetInsurance_Breakup,"List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_AssetInsurance_Breakup').hide();</script>");
            }
            ViewData["AssetInsValReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_AssetInsurance_Breakup, "Export");
            var InsValue = BASE._Reports_Common_DBOps.GetAssetInsuranceBreakUpReport();
            AssetInsBreakupReportExportData = InsValue;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if ((InsValue.Count == 0))
            {
                return View();
            }
            return View(InsValue);
        }
        public PartialViewResult Frm_Asset_Breakup_Info_Grid(string command = null)
        {
            ViewData["AssetInsValReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_AssetInsurance_Breakup, "Export");
            if (AssetInsBreakupReportExportData == null || command == "REFRESH")
            {
                var data = BASE._Reports_Common_DBOps.GetAssetInsuranceBreakUpReport();
                AssetInsBreakupReportExportData = data;
            }
            var _data = AssetInsBreakupReportExportData as List<Common_Lib.DbOperations.Reports_All.AssetInsuranceBreakUpReport>;
            if ((_data.Count == 0))
            {
                return PartialView();
            }
            return PartialView(_data);
        }
        public void SessionClear()
        {
            ClearBaseSession("_AssetInsValReport");
        }
        #region Export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_AssetInsurance_Breakup, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('AssetInsValReport_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        public ActionResult AssetInsExportToXLSX_DataAware()
        {
            return PivotGridExtension.ExportToXlsx(AssetInsPivotGridHelper.Settings, AssetInsBreakupReportExportData, true);
        }
        public ActionResult AssetInsExportToXLS_DataAware()
        {
            return PivotGridExtension.ExportToXls(AssetInsPivotGridHelper.Settings, AssetInsBreakupReportExportData, true);
        }
        public ActionResult AssetInsExportToPDF_DataAware()
        {
            return PivotGridExtension.ExportToPdf(AssetInsPivotGridHelper.Settings, AssetInsBreakupReportExportData, true);
        }
        public ActionResult AssetInsExportToCSV_DataAware()
        {
            return PivotGridExtension.ExportToCsv(AssetInsPivotGridHelper.Settings, AssetInsBreakupReportExportData, true);
        }
        public ActionResult AssetInsExportToDOCX_DataAware()
        {
            return PivotGridExtension.ExportToDocx(AssetInsPivotGridHelper.Settings, AssetInsBreakupReportExportData, true);
        }
        public ActionResult AssetInsExportToRTF_DataAware()
        {
            return PivotGridExtension.ExportToRtf(AssetInsPivotGridHelper.Settings, AssetInsBreakupReportExportData, true);
        }

        #endregion
    }

    public class AssetInsPivotGridHelper
    {
        static PivotGridSettings _settings;
        public static PivotGridSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = AssetInsGetSettings();
                }
                return _settings;
            }
        }
        static PivotGridSettings AssetInsGetSettings()
        {
            PivotGridSettings settings = new PivotGridSettings();
            settings.Name = "AssetInsValReportListGrid";

            settings.SettingsExport.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PrintRowAreaOnEveryPage = true;
            settings.SettingsExport.OptionsPrint.PageSettings.Landscape = true;

            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 0;
                field.FieldName = "Complex";
                field.Caption = "Complex";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 1;
                field.FieldName = "TYPE";
                field.Caption = "Type";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 2;
                field.FieldName = "Name";
                field.Caption = "Name";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                field.AreaIndex = 3;
                field.FieldName = "Location_Name";
                field.Caption = "Location_Name";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea;
                field.AreaIndex = 0;
                field.FieldName = "Item";
                field.Caption = "Item";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
                field.AreaIndex = 0;
                field.FieldName = "Asset_Insurance_Value";
                field.Caption = "Asset_Insurance_Value";
                field.CellFormat.FormatType = FormatType.Custom;
                field.CellFormat.FormatString = "c";
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea;
                field.AreaIndex = 0;
                field.FieldName = "Asset_Insurance_Value";
                field.Caption = "Asset_Insurance_Value";
                field.CellFormat.FormatType = FormatType.Numeric;
            });
            settings.Fields.Add(field =>
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                field.AreaIndex = 1;
                field.FieldName = "UID";
                field.Caption = "UID";
            });
            return settings;
        }
    }
}