using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data;
//using DevExpress.Web.Demos.Mvc;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.Web;
using DevExpress.Utils;
using ConnectOneMVC.Controllers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraReports.UI;
using Common_Lib;
using DevExpress.XtraPrintingLinks;

namespace ConnectOneMVC.Helper
{
    //public enum GridViewExportFormat { None, Pdf, Xls, Xlsx, Rtf, Csv }
    public enum GridViewExportFormat { None, Pdf, Xls, Xlsx, Rtf, Csv }
    public delegate ActionResult GridViewExportMethod(GridViewSettings settings, object dataObject);
    public class GridViewExportHelper
    {
        public static List<string> GetExportFormats()
        {
            return new List<string>() { "pdf", "xls", "xlsx", "rtf", "mht", "png", "jpeg", "bmp", "tiff", "gif" };
        }


        static string ExcelDataAwareGridViewSettingsKey = "51172A18-2073-426B-A5CB-136347B3A79F";
        static string FormatConditionsExportGridViewSettingsKey = "14634B6F-E1DC-484A-9728-F9608615B628";

        static Dictionary<GridViewExportFormat, GridViewExportMethod> exportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> ExportFormatsInfo
        {
            get
            {
                if (exportFormatsInfo == null)
                    exportFormatsInfo = CreateExportFormatsInfo();
                return exportFormatsInfo;
            }
        }

        static IDictionary Context { get { return System.Web.HttpContext.Current.Items; } }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Pdf, GridViewExtension.ExportToPdf },
                {
                    GridViewExportFormat.Xls,
                    (settings, data) => GridViewExtension.ExportToXls(settings, data, new XlsExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })

                },
                {
                    GridViewExportFormat.Xlsx,
                    (settings, data) => GridViewExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                },
                { GridViewExportFormat.Rtf, GridViewExtension.ExportToRtf },
                {
                    GridViewExportFormat.Csv,
                    (settings, data) => GridViewExtension.ExportToCsv(settings, data, new CsvExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG })
                }
            };
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> dataAwareExportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> DataAwareExportFormatsInfo
        {
            get
            {
                if (dataAwareExportFormatsInfo == null)
                    dataAwareExportFormatsInfo = CreateDataAwareExportFormatsInfo();
                return dataAwareExportFormatsInfo;
            }
        }
        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateDataAwareExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Xls, GridViewExtension.ExportToXls },
                { GridViewExportFormat.Xlsx, GridViewExtension.ExportToXlsx },
                { GridViewExportFormat.Csv, GridViewExtension.ExportToCsv }
            };
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> formatConditionsExportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> FormatConditionsExportFormatsInfo
        {
            get
            {
                if (formatConditionsExportFormatsInfo == null)
                    formatConditionsExportFormatsInfo = CreateFormatConditionsExportFormatsInfo();
                return formatConditionsExportFormatsInfo;
            }
        }
        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateFormatConditionsExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Pdf, GridViewExtension.ExportToPdf},
                { GridViewExportFormat.Xls, (settings, data) => GridViewExtension.ExportToXls(settings, data) },
                { GridViewExportFormat.Xlsx,
                    (settings, data) => GridViewExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx {ExportType = DevExpress.Export.ExportType.WYSIWYG})
                },
                { GridViewExportFormat.Rtf, GridViewExtension.ExportToRtf }
            };
        }

        static Dictionary<GridViewExportFormat, GridViewExportMethod> advancedBandsExportFormatsInfo;
        public static Dictionary<GridViewExportFormat, GridViewExportMethod> AdvancedBandsExportFormatsInfo
        {
            get
            {
                if (advancedBandsExportFormatsInfo == null)
                    advancedBandsExportFormatsInfo = CreateAdvancedBandsExportFormatsInfo();
                return advancedBandsExportFormatsInfo;
            }
        }
        static Dictionary<GridViewExportFormat, GridViewExportMethod> CreateAdvancedBandsExportFormatsInfo()
        {
            return new Dictionary<GridViewExportFormat, GridViewExportMethod> {
                { GridViewExportFormat.Pdf, GridViewExtension.ExportToPdf },
                { GridViewExportFormat.Xls, (settings, data) => GridViewExtension.ExportToXls(settings, data, new XlsExportOptionsEx {ExportType = DevExpress.Export.ExportType.WYSIWYG}) },
                { GridViewExportFormat.Xlsx, (settings, data) => GridViewExtension.ExportToXlsx(settings, data, new XlsxExportOptionsEx {ExportType = DevExpress.Export.ExportType.WYSIWYG}) },
                { GridViewExportFormat.Rtf, GridViewExtension.ExportToRtf }
            };
        }

        public static string GetExportButtonTitle(GridViewExportFormat format)
        {
            if (format == GridViewExportFormat.None)
                return string.Empty;
            return string.Format("Export to {0}", format.ToString().ToUpper());
        }

        public static GridViewSettings CreateGeneralDetailGridSettings(int uniqueKey)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "detailGrid" + uniqueKey;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "ProductID";
            settings.Columns.Add("ProductID");
            settings.Columns.Add("ProductName");
            settings.Columns.Add("UnitPrice");
            settings.Columns.Add("QuantityPerUnit");

            settings.SettingsDetail.MasterGridName = "masterGrid";

            return settings;
        }
        public static GridViewSettings FormatConditionsExportGridViewSettings
        {
            get
            {
                var settings = Context[FormatConditionsExportGridViewSettingsKey] as GridViewSettings;
                if (settings == null)
                    Context[FormatConditionsExportGridViewSettingsKey] = settings = CreateFormatConditionsExportGridViewSettings();
                return settings;
            }
        }
        static GridViewSettings CreateFormatConditionsExportGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "grid";
            settings.CallbackRouteValues = new { Controller = "Exporting", Action = "ExportWithFormatConditionsPartial" };
            settings.KeyFieldName = "OrderID;ProductID";
            settings.EnableRowsCache = false;
            settings.Width = Unit.Percentage(100);
            settings.Columns.Add(column =>
            {
                column.FieldName = "CustomerName";
                column.Width = 260;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Freight";
                column.SortOrder = ColumnSortOrder.Descending;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "UnitPrice";
                column.ColumnType = MVCxGridViewColumnType.TextBox;
                column.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "Discount";
                column.ColumnType = MVCxGridViewColumnType.SpinEdit;
                column.PropertiesEdit.DisplayFormatString = "p0";
            });
            settings.Columns.Add("Quantity");
            settings.Columns.Add(column =>
            {
                column.FieldName = "Total";
                column.UnboundType = UnboundColumnType.Decimal;
                column.UnboundExpression = "UnitPrice * Quantity * (1 - Discount)";
                column.PropertiesEdit.DisplayFormatString = "c";
            });
            settings.FormatConditions.AddTopBottom("UnitPrice", GridTopBottomRule.AboveAverage, GridConditionHighlightFormat.ItalicText);
            settings.FormatConditions.AddTopBottom("UnitPrice", GridTopBottomRule.AboveAverage, GridConditionHighlightFormat.RedText);
            settings.FormatConditions.AddHighlight("Discount", "[Discount] > 0", GridConditionHighlightFormat.GreenFillWithDarkGreenText);
            settings.FormatConditions.AddTopBottom(formatCondition =>
            {
                formatCondition.FieldName = "Discount";
                formatCondition.Rule = GridTopBottomRule.TopItems;
                formatCondition.Threshold = 15;
                formatCondition.Format = GridConditionHighlightFormat.BoldText;
            });
            settings.FormatConditions.AddColorScale("Quantity", GridConditionColorScaleFormat.GreenWhite);
            settings.FormatConditions.AddIconSet("Quantity", GridConditionIconSetFormat.Ratings4);
            settings.FormatConditions.AddTopBottom(formatCondition =>
            {
                formatCondition.FieldName = "Total";
                formatCondition.Rule = GridTopBottomRule.TopPercent;
                formatCondition.Threshold = 20;
                formatCondition.Format = GridConditionHighlightFormat.Custom;
                formatCondition.CellStyle.Font.Bold = true;
                formatCondition.CellStyle.ForeColor = Color.FromArgb(0x9c, 0, 0x6);
            });
            return settings;
        }


        public static GridViewSettings AdvancedBandsExportGridViewSettings
        {
            get
            {
                var settings = Context[FormatConditionsExportGridViewSettingsKey] as GridViewSettings;
                if (settings == null)
                    Context[FormatConditionsExportGridViewSettingsKey] = settings = CreateFormatAdvancedBandGridViewSettings();
                return settings;
            }
        }
        static GridViewSettings CreateFormatAdvancedBandGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "grid";
            settings.CallbackRouteValues = new { Controller = "Exporting", Action = "ExportWithDataCellBandsPartial" };
            settings.EnableRowsCache = false;
            settings.Width = Unit.Percentage(100);
            settings.SettingsPager.PageSize = 3;
            settings.Styles.Header.HorizontalAlign = HorizontalAlign.Center;

            settings.Columns.Add(column =>
            {
                column.ColumnType = MVCxGridViewColumnType.SpinEdit;
                column.FieldName = "Price";
                column.PropertiesEdit.DisplayFormatString = "c0";
            });
            var addressColumn = new GridViewDataColumn("Address");
            settings.Columns.Add(addressColumn);
            addressColumn.CellStyle.CssClass = "address-cell";
            var featuresColumn = new GridViewDataColumn("Features");
            addressColumn.Columns.Add(featuresColumn);
            featuresColumn.Columns.AddRange(
                new GridViewDataColumn("Beds") { ExportWidth = 80 },
                new GridViewDataColumn("Baths") { ExportWidth = 80 },
                new GridViewDataColumn("HouseSize") { ExportWidth = 80 });
            var yearBuiltColumn = new GridViewDataColumn("YearBuilt") { ExportWidth = 80 };
            yearBuiltColumn.CellStyle.HorizontalAlign = HorizontalAlign.Right;
            featuresColumn.Columns.Add(yearBuiltColumn);
            settings.Columns.Add(column =>
            {
                column.ColumnType = MVCxGridViewColumnType.Image;
                var imageProperties = (ImageEditProperties)column.PropertiesEdit;
                column.FieldName = "PhotoUrl";
                column.Caption = "Photo";
                column.CellStyle.CssClass = "photo-cell";
                imageProperties.ImageWidth = 200;
            });

            settings.SettingsExport.RenderBrick += (s, e) =>
            {
                if (e.RowType != GridViewRowType.Data)
                    return;
                var dataColumn = (GridViewDataColumn)e.Column;
                if (dataColumn.FieldName == "PhotoUrl")
                {
                    var path = HostingEnvironment.MapPath(e.Value.ToString());
                    if (File.Exists(path))
                        e.ImageValue = File.ReadAllBytes(path);
                }
            };

            return settings;
        }

        #region Bank
        static GridViewSettings exportGridViewSettings;
        public static GridViewSettings ExportGridViewSettings
        {
            get
            {
                exportGridViewSettings = CreateExportGridViewSettings();
                return exportGridViewSettings;
            }
        }
        static GridViewSettings CreateExportGridViewSettings()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "gvExport";
            settings.CallbackRouteValues = new { Controller = "Bank", Action = "ExportPartial" };
            settings.Width = Unit.Percentage(100);
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Name").Caption = "Name";
            settings.Columns.Add("Branch").Caption = "Branch";
            settings.Columns.Add("BA_ACCOUNT_NO").Caption = "Account No.";
            settings.Columns.Add("BA_CUST_NO").Caption = "Customer No.";
            settings.Columns.Add("OP_AMOUNT").Caption = "Opening balance";
            settings.Columns.Add("BA_OTHER_DETAIL").Caption = "Other Details";
            settings.Columns.Add("BB_IFSC_CODE").Caption = "IFSC Code";
            settings.Columns.Add("BB_MICR_CODE").Caption = "MICR Code";
            settings.Columns.Add("BI_BANK_PAN_NO").Caption = "PAN. No.";
            settings.Columns.Add("BA_TAN_NO").Caption = "TAN. No.";
            settings.Columns.Add("BA_TEL_NOS").Caption = "Tel. No.";
            settings.Columns.Add("BA_EMAIL_ID").Caption = "Email Id";
            settings.Columns.Add("BA_OPEN_DATE").Caption = "A/c. Open date";
            settings.Columns.Add("BA_CLOSE_DATE").Caption = "A/c. Closed date";
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "BA_ACCOUNT_TYPE",
                Caption = "Acount Type",
                GroupIndex = 0
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_Status",
                Caption = "Status",
                GroupIndex = 1
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "BA_ACCOUNT_NEW",
                Caption = "Status",
                GroupIndex = 2

            });
            settings.Settings.ShowGroupPanel = true;
            return settings;
        }
        #endregion

        #region GoldSilver Report     
        static GridViewSettings exportGoldSilverGrid;
        public static GridViewSettings ExportGoldSilverGrid
        {
            get
            {
                exportGoldSilverGrid = CreateExportGoldSilverGrid();
                return exportGoldSilverGrid;
            }
        }
        static GridViewSettings CreateExportGoldSilverGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "GoldSilverListGrid";
            settings.CallbackRouteValues = new { Controller = "GoldSilver", Action = "Frm_GoldSilver_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("ITEM_NAME").Caption = "ITEM NAME";
            settings.Columns.Add("MISC_NAME").Caption = "Description";
            settings.Columns.Add("GS_AMT").Caption = "Value";
            settings.Columns.Add("Curr_Value").Caption = "Curr Value";
            settings.Columns.Add("GS_ITEM_WEIGHT").Caption = "Weight";
            settings.Columns.Add("Curr_Weight").Caption = "Curr weight";
            settings.Columns.Add("Rate_per_Gram").Caption = "Rate per Gram";
            settings.Columns.Add("AL_LOC_NAME").Caption = "Location";
            settings.Columns.Add("GS_OTHER_DETAIL").Caption = "Other Detail";
            settings.Columns.Add("Sale_Status").Caption = "Sale Status";

            return settings;
        }
        #endregion

        #region Vehicle Export report 

        static GridViewSettings exportVehicleGrid;
        public static GridViewSettings ExportVehicleGrid
        {
            get
            {
                exportVehicleGrid = CreateExportVehicleGrid();
                return exportVehicleGrid;
            }
        }
        static GridViewSettings CreateExportVehicleGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "VehcileListGrid";
            settings.CallbackRouteValues = new { Controller = "Vehicle", Action = "Frm_Telephone_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("ITEM_NAME").Caption = "ITEM NAME";
            settings.Columns.Add("MAKE").Caption = "MAKE";
            settings.Columns.Add("Model").Caption = "Model";
            settings.Columns.Add("VI_REG_NO").Caption = "VI_REG_NO";
            settings.Columns.Add("Date_of_First_Registration").Caption = "Date of First Registration";
            settings.Columns.Add("Opening_Value").Caption = "Opening Value";
            settings.Columns.Add("Curr_Value").Caption = "VI_REG_NO";
            settings.Columns.Add("Ownership").Caption = "Ownership";
            settings.Columns.Add("Opening_Value").Caption = "Opening Value";
            settings.Columns.Add("Curr_Value").Caption = "Curr Value";
            settings.Columns.Add("VI_DOC_RC_BOOK").Caption = "VI_DOC_RC_BOOK";
            settings.Columns.Add("Affidavit").Caption = "Affidavit";
            settings.Columns.Add("Will").Caption = "Will";
            settings.Columns.Add("Transfer_Lettter").Caption = "Transfer_Lettter";
            settings.Columns.Add("Free_Use_Letter").Caption = "Free Use Letter";
            settings.Columns.Add("Other_Documents").Caption = "Other Documents";
            settings.Columns.Add("Insurance_Company").Caption = "Insurance Company";
            settings.Columns.Add("VI_INS_POLICY_NO").Caption = "Policy No";
            settings.Columns.Add("Expiry_Date").Caption = "Expiry Date";
            settings.Columns.Add("VI_OTHER_DETAIL").Caption = "Other_Detail";
            settings.Columns.Add("AL_LOC_Name").Caption = "Location Name";
            settings.Columns.Add("Sale_Status").Caption = "Sale Status";

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionDate",
                Caption = "Action Date",
                Visible = false,
                PropertiesEdit = { DisplayFormatString = "dd MMMM yyyy  HH:mm" }
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionBy",
                Caption = "Action By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionStatus",
                Caption = "Action Status",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddBy",
                Caption = "Added By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddDate",
                Caption = "Added Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditBy",
                Caption = "Edited By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditDate",
                Caption = "Edited Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }
        #endregion

        #region FD Export Report

        static GridViewSettings exportFDGrid;
        public static GridViewSettings ExportFDGrid
        {
            get
            {
                exportFDGrid = CreateExportFDGrid();
                return exportFDGrid;
            }
        }
        static GridViewSettings CreateExportFDGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "FDListGrid";
            settings.CallbackRouteValues = new { Controller = "FD", Action = "Frm_FD_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            //settings.Columns.Add("BI_BANK_NAME").Caption = "Bank Name";
            //settings.Columns.Add("BB_BRANCH_NAME").Caption = "Branch Name";
            //settings.Columns.Add("FD_NO").Caption = "FD NO";
            settings.Columns.Add("FD_DATE").Caption = "FD Date";
            settings.Columns.Add("FD_AS_DATE").Caption = "FD AS DATE";
            settings.Columns.Add("FD_AMOUNT").Caption = "FD Amount";
            settings.Columns.Add("FD_INT_RATE").Caption = "Interest";
            settings.Columns.Add("FD_MAT_DATE").Caption = "Maturity Date";
            //settings.Columns.Add("FD_MAT_AMT ").Caption ="Maturity Amount";
            settings.Columns.Add("FD_INT_PAY_COND").Caption = "Interest Condition";
            settings.Columns.Add("BA_CUST_NO").Caption = "Customer No";
            settings.Columns.Add("BB_BRANCH_NAME").Caption = "Branch";
            settings.Columns.Add("FD_Status").Caption = "FD Status";
            //settings.Columns.Add("CLOSE_DATE ").Caption = "CLOSE DATE";
            settings.Columns.Add("FD_Less_Maturity").Caption = "Maturity - FD Amt";
            settings.Columns.Add("Interest_Recd").Caption = "Gross Interest";
            settings.Columns.Add("TDS_Paid").Caption = "TDS Deducted";
            settings.Columns.Add("Nett_Interest").Caption = "Nett Interest";
            settings.Columns.Add("Other_Detail").Caption = "Other Details";
            settings.Columns.Add("Remarks").Caption = "Remarks";
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionDate",
                Caption = "Action Date",
                Visible = false,
                PropertiesEdit = { DisplayFormatString = "dd MMMM yyyy  HH:mm" }
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionBy",
                Caption = "Action By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionStatus",
                Caption = "Action Status",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddBy",
                Caption = "Added By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddDate",
                Caption = "Added Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditBy",
                Caption = "Edited By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditDate",
                Caption = "Edited Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }


        #endregion

        #region telephone
        static GridViewSettings exportTelephoneGrid;
        public static GridViewSettings ExportTelephoneGrid
        {
            get
            {
                exportTelephoneGrid = CreateExportTelephoneGrid();
                return exportTelephoneGrid;
            }
        }
        static GridViewSettings CreateExportTelephoneGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "TelephoneListGrid";
            settings.CallbackRouteValues = new { Controller = "Telephone", Action = "Frm_Telephone_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("tP_NOField").Caption = "Telephone No.";
            settings.Columns.Add("telMiscIdField").Caption = "Telecom Company";
            settings.Columns.Add("categoryField").Caption = "Category";
            settings.Columns.Add("typeField").Caption = "Plan Type";
            settings.Columns.Add("other_DetField").Caption = "Other Details";
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionDate",
                Caption = "Action Date",
                Visible = false,
                PropertiesEdit = { DisplayFormatString = "dd MMMM yyyy  HH:mm" }
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionBy",
                Caption = "Action By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionStatus",
                Caption = "Action Status",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddBy",
                Caption = "Added By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddDate",
                Caption = "Added Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditBy",
                Caption = "Edited By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditDate",
                Caption = "Edited Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }


        #endregion

        #region Advances
        static GridViewSettings exportAdvancesGrid;
        public static GridViewSettings ExportAdvancesGrid
        {
            get
            {
                exportTelephoneGrid = CreateExportAdvancesGrid();
                return exportTelephoneGrid;
            }
        }
        static GridViewSettings CreateExportAdvancesGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "AdvancesListGrid";
            settings.CallbackRouteValues = new { Controller = "Advances", Action = "Frm_Advances_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("ITEM_NAME").Caption = "Item Name";
            settings.Columns.Add("PARTY_NAME").Caption = "Party Name";
            settings.Columns.Add(column =>
            {
                column.FieldName = "AI_ADV_DATE";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
                column.Caption = "Given Date";
            });
            settings.Columns.Add("Advance");
            settings.Columns.Add("Addition");
            settings.Columns.Add("Adjusted");
            settings.Columns.Add("Refund");
            settings.Columns.Add("OutStanding");
            settings.FormatConditions.AddHighlight("OutStanding", "[OutStanding] > '0.00'", GridConditionHighlightFormat.LightRedFill);
            settings.Columns.Add("Reason");
            settings.Columns.Add("AI_OTHER_DETAIL").Caption = "Other Detail";

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "YearID",
                Caption = "YearID",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddBy",
                Caption = "Added By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddDate",
                Caption = "Added Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditBy",
                Caption = "Edited By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditDate",
                Caption = "Edited Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionBy",
                Caption = "Action By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ActionDate",
                Caption = "Action Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "TR_ID",
                Caption = "Tr.ID",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "RemarkCount",
                Caption = "RemarkCount",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "RemarkStatus",
                Caption = "RemarkStatus",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Remarks",
                Caption = "Remarks",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "OpenActions",
                Caption = "OpenActions",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "CrossedTimeLimit",
                Caption = "CrossedTimeLimit",
                Visible = false
            });
            return settings;
        }


        #endregion

        #region AddressBook
        static GridViewSettings exportAddressbookGrid;
        public static GridViewSettings ExportAddressbookGrid
        {
            get
            {
                exportAddressbookGrid = CreateExportAddressbookGrid();
                return exportAddressbookGrid;
            }
        }
        static GridViewSettings CreateExportAddressbookGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "AddressListGrid";
            settings.CallbackRouteValues = new
            {
                Controller = "AddressBook",
                Action = "Frm_Address_Info_Grid",
            };
            settings.Settings.HorizontalScrollBarMode = ScrollBarMode.Visible;
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;
            //settings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
            //settings.Settings.VerticalScrollableHeight = 500;

            settings.KeyboardSupport = true;
            settings.AccessKey = "G";

            //settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            //settings.Styles.EditFormColumnCaption.Font.Size = 40;
            //settings.Styles.Table.Font.Size = 24;
            //settings.Styles.Row.Font.Size = 24;

            //settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;

            settings.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleAlways;
            settings.Settings.ShowGroupPanel = true;

            settings.SettingsBehavior.AutoExpandAllGroups = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            //settings.Settings.UseFixedTableLayout = true;
            settings.SettingsBehavior.ColumnMoveMode = GridColumnMoveMode.AmongSiblings;
            settings.SettingsBehavior.AllowSort = true;
            settings.SettingsBehavior.EnableCustomizationWindow = true;

            // Filter and serach 
            settings.Settings.ShowHeaderFilterButton = true;
            settings.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            settings.Settings.ShowFilterRow = true;

            settings.SettingsSearchPanel.Visible = true;
            settings.SettingsSearchPanel.ShowApplyButton = true;
            settings.SettingsSearchPanel.ShowClearButton = true;

            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.ClientSideEvents.FocusedRowChanged = "OnGridFocusedRowChanged";
            settings.ClientSideEvents.EndCallback = "OnGridAddressListGrid_EndCallback";

            settings.Settings.ShowHeaderFilterButton = true;
            settings.Settings.ShowHeaderFilterBlankItems = false;
            settings.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

            settings.SettingsContextMenu.Enabled = true;
            settings.SettingsBehavior.EnableCustomizationWindow = true;
            settings.SettingsContextMenu.RowMenuItemVisibility.DeleteRow = false;
            settings.SettingsContextMenu.RowMenuItemVisibility.EditRow = false;
            settings.SettingsContextMenu.RowMenuItemVisibility.NewRow = false;
            // show filter control
            settings.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            settings.SettingsFilterControl.ViewMode = true ? FilterControlViewMode.VisualAndText : FilterControlViewMode.Visual;
            settings.SettingsFilterControl.AllowHierarchicalColumns = true;// AllowHierarchicalColumns;
            settings.SettingsFilterControl.ShowAllDataSourceColumns = true;//FilterBuilderDemoHelper.Options.ShowAllDataSourceColumns;
            settings.SettingsFilterControl.MaxHierarchyDepth = 1;

            settings.Settings.ShowGroupButtons = true;
            settings.FillContextMenuItems = (sender, e) =>
            {
                if (e.MenuType == GridViewContextMenuType.Rows)
                {
                    var Additem = e.CreateItem("New", "New");
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.Refresh), Additem);
                    var Edititem = e.CreateItem("Edit", "Edit");
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.Refresh), Edititem);
                    var Deleteitem = e.CreateItem("Delete", "Delete");
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.Refresh), Deleteitem);
                    var Viewitem = e.CreateItem("View", "View");
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.Refresh), Viewitem);
                    var PrintViewitem = e.CreateItem("List Preview", "ListPreview");
                    e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.Refresh), PrintViewitem);
                }
            };
            settings.ClientSideEvents.ContextMenuItemClick = "ContextMenuFunction";
            // DXCOMMENT: Specify the grid's key field name and define grid columns in accordance with data model fields
            settings.KeyFieldName = "ID";

            settings.Columns.Add("Title").Caption = "Title";
            settings.Columns.Add("Name").Caption = "Name";
            settings.Columns.Add("Organization").Caption = "Organization";
            settings.Columns.Add("PinCode").Caption = "Pin Code";
            settings.Columns.Add("City").Caption = "City";
            settings.Columns.Add("State").Caption = "State";
            settings.Columns.Add("Country").Caption = "Country";
            settings.Columns.Add("Passport_No").Caption = "Passport No.";
            settings.Columns.Add("PAN_No").Caption = "PAN No.";
            settings.Columns.Add("VAT_TIN_No").Caption = "VAT TIN No.";
            settings.Columns.Add("CST_TIN_No").Caption = "CST TIN No.";
            settings.Columns.Add("TAN_No").Caption = "TAN No.";
            settings.Columns.Add("UID_No").Caption = "UID No.";
            settings.Columns.Add("Service_Tax_Reg_No").Caption = "Service Tax Reg. No.";
            settings.Columns.Add("Address_Line1").Caption = "Address Line.1";
            settings.Columns.Add("Address_Line2").Caption = "Address Line.2";
            settings.Columns.Add("Address_Line3").Caption = "Address Line.3";
            settings.Columns.Add("Address_Line4").Caption = "Address Line.4";
            settings.Columns.Add("Resi_Tel_No").Caption = "Resi.Tel.No(s)";
            settings.Columns.Add("Office_Tel_No").Caption = "Office Tel.No(s)";
            settings.Columns.Add("Office_Fax_No").Caption = "Office Fax No(s)";
            settings.Columns.Add("Resi_Fax_No").Caption = "Resi.Fax No(s)";
            settings.Columns.Add("Mobile_No").Caption = "Mobile No(s)";
            settings.Columns.Add("Email").Caption = "Email";
            settings.Columns.Add("Website").Caption = "Website";
            settings.Columns.Add("Date_of_Birth").Caption = "Date of Birth (Lokik)";
            settings.Columns.Add("Blood_Group").Caption = "Blood Group";
            settings.Columns.Add("Status").Caption = "Status";
            settings.Columns.Add("BK_Title").Caption = "BK Title";
            settings.Columns.Add("BK_PAD_No").Caption = "BK PAD No.";
            settings.Columns.Add("Class_At").Caption = "Class At";
            settings.Columns.Add("Centre_Category").Caption = "Centre Category";
            settings.Columns.Add("Centre_Name").Caption = "Centre Name";
            settings.Columns.Add("Category").Caption = "Category";
            settings.Columns.Add("Referene").Caption = "Referene";
            settings.Columns.Add("Remarks").Caption = "Remarks";
            settings.Columns.Add("Events").Caption = "Events";


            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_Date",
                Caption = "Action Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_Status",
                Caption = "Action Status",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_By",
                Caption = "Added By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_Date",
                Caption = "Added Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_By",
                Caption = "Edited By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_Date",
                Caption = "Edited Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }
        #endregion AddressBook

        #region CashBook
        static GridViewSettings exportCBGrid;
        public static GridViewSettings ExportCBGrid
        {
            get
            {
                exportCBGrid = CreateExportCBGrid();
                return exportCBGrid;
            }
        }
        static GridViewSettings CreateExportCBGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "CashBookListGrid";
            settings.CallbackRouteValues = new { Controller = "Voucher", Action = "Frm_Voucher_Info_CB_Grid" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "TEMP_ID";

            settings.Columns.Add("iREC_ID").Visible = false;
            settings.Columns.Add("iREC_EDIT_ON").Visible = false;
            settings.Columns.Add("iTR_ITEM_ID").Visible = false;
            settings.Columns.Add("iTR_AB_ID_1").Visible = false;
            settings.Columns.Add("iTR_M_ID").Visible = false;

            settings.Columns.AddBand(voucherBand =>
            {
                voucherBand.ShowInCustomizationForm = true;
                voucherBand.AllowDragDrop = DefaultBoolean.True;
                voucherBand.Caption = "Voucher";
                voucherBand.HeaderStyle.Font.Bold = true;
                voucherBand.Columns.Add(column =>
                {
                    column.FieldName = "V_Date";
                    column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
                    column.Caption = "Date";
                });
                voucherBand.Visible = false;
            });

            settings.Columns.AddBand(productBand =>
            {
                productBand.ShowInCustomizationForm = true;
                productBand.Caption = "Receipts";
                productBand.HeaderStyle.Font.Bold = true;
                productBand.Columns.Add("R_Cash").Caption = "Cash ";

                //if (ShowBank)
                //{
                //    productBand.Columns.Add(c =>
                //    {
                //        c.FieldName = "R_Bank";
                //        c.Caption = "Bank";
                //        c.CellStyle.CssClass = "hide-bank";
                //        c.HeaderStyle.CssClass = "hide-bank";
                //        c.Visible = false;
                //    });
                //}
                //else
                //{
                //    productBand.Columns.Add(c =>
                //    {
                //        c.FieldName = "R_Bank";
                //        c.Caption = "Bank";
                //        c.CellStyle.CssClass = "hide-bank";
                //        c.HeaderStyle.CssClass = "hide-bank";
                //        c.Visible = true;
                //    });
                //}

                //productBand.Columns.Add(c =>
                //{
                //    c.FieldName = "R_Allban_4544";
                //    c.Caption = "Allban 4544";
                //    c.CellStyle.CssClass = "hide-other-bank";
                //    c.HeaderStyle.CssClass = "hide-other-bank";
                //    //c.Visible = ShowBank;
                //});

                //productBand.Columns.Add(c =>
                //{
                //    c.FieldName = "R_Allban_1212";
                //    c.Caption = "Allban 1212";
                //    c.CellStyle.CssClass = "hide-other-bank";
                //    c.HeaderStyle.CssClass = "hide-other-bank";
                //   // c.Visible = ShowBank;
                //});

                //productBand.Columns.Add(c =>
                //{
                //    c.FieldName = "R_Dena_2223";
                //    c.Caption = "Dena 2223";
                //    c.CellStyle.CssClass = "hide-other-bank";
                //    c.HeaderStyle.CssClass = "hide-other-bank";
                //    //c.Visible = ShowBank;
                //});

                productBand.Columns.Add("R_Journal").Caption = "Journal";
                productBand.Columns.Add("R_Total").Caption = "Total";
            });
            settings.Columns.AddBand(productBand =>
            {
                productBand.ShowInCustomizationForm = true;
                productBand.Caption = "Particulars";
                productBand.HeaderStyle.Font.Bold = true;
                productBand.Columns.Add("P_Item").Caption = "Item ";
                productBand.Columns.Add("P_Head").Caption = "Head";
                productBand.Columns.Add("P_Party_Bank").Caption = "Party/Bank";
                productBand.Columns.Add("P_Chk_Dd_Ref").Caption = "Chq/DD/Ref No";
            });
            settings.Columns.AddBand(productBand =>
            {
                productBand.Caption = "Payments";
                productBand.HeaderStyle.Font.Bold = true;
                productBand.Columns.Add("Pay_Cash").Caption = "Cash ";
                productBand.Columns.Add(c =>
                {
                    c.FieldName = "Pay_Bank";
                    c.Caption = "Bank";
                    c.CellStyle.CssClass = "hide-bank";
                    c.HeaderStyle.CssClass = "hide-bank";
                    //c.Visible = ShowBank;
                });

                //productBand.Columns.Add(c =>
                //{
                //    c.FieldName = "Pay_Allban_4544";
                //    c.Caption = "Allban 4544";
                //    c.CellStyle.CssClass = "hide-other-bank";
                //    c.HeaderStyle.CssClass = "hide-other-bank";
                //    //c.Visible = ShowBank;
                //});

                //productBand.Columns.Add(c =>
                //{
                //    c.FieldName = "Pay_Dena_2223";
                //    c.Caption = "Dena 2223";
                //    c.CellStyle.CssClass = "hide-other-bank";
                //    c.HeaderStyle.CssClass = "hide-other-bank";
                //    //c.Visible = ShowBank;
                //});

                //productBand.Columns.Add(c =>
                //{
                //    c.FieldName = "Pay_Allban_1212";
                //    c.Caption = "Allban 1212";
                //    c.CellStyle.CssClass = "hide-other-bank";
                //    c.HeaderStyle.CssClass = "hide-other-bank";
                //    ///c.Visible = ShowBank;
                //});
                productBand.Columns.Add("Pay_Journal").Caption = "Journal";
                productBand.Columns.Add("Pay_Total").Caption = "Total";
            });

            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Cash").DisplayFormat = "Total: {0:#,0.00}";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Bank").DisplayFormat = "{0}";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Allban_4544").DisplayFormat = "{0}";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Dena_2223").DisplayFormat = "{0}";
            //settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Sbi_452").DisplayFormat = "{0}";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Allban_1212").DisplayFormat = "{0}";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Journal").DisplayFormat = "{0}";
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "R_Total").DisplayFormat = "{0}";
            //if (Model.ShowTotal)
            {
                settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Custom, "P_Party_Bank").DisplayFormat = "Total Payments: {0}";
                settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Custom, "P_Party_Bank").DisplayFormat = "Closing Balance{0}: ";
                settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Custom, "P_Party_Bank").DisplayFormat = "Total Receipts{0}: ";

            }
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Pay_Cash").DisplayFormat = "{0:#,0.00}";
            //if (Model.ShowTotal)
            {
                settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "Pay_Cash").DisplayFormat = "{0:#,0.00}";
                settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Pay_Cash").DisplayFormat = "{0:#,0.00}";
            }
            //new add
            settings.SettingsSearchPanel.ShowApplyButton = true;
            settings.SettingsSearchPanel.ShowClearButton = true;

            settings.Settings.ShowPreview = true;
            settings.PreviewFieldName = "R_NARRATION";

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_ADD_BY",
                Caption = "Entry Add By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_ADD_ON",
                Caption = "Entry Add On",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_EDIT_ON",
                Caption = "Entry Edit On",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "R_SR_NO",
                Caption = "Entry Sr.No.",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ACTION_STATUS",
                Caption = "Entry Status",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "R_CODE",
                Caption = "Entry Tr.Code",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "R_TYPE",
                Caption = "Entry Tr.Type",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Advance_Filter",
                Caption = "iAdvance_Filter",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "R_NARRATION",
                Caption = "Narration",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Pay_Bank",
                Caption = "Payment Bank",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "R_Bank",
                Caption = "Receipt Bank",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ACTION_STATUS",
                Caption = "Remark Status",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "R_VNO",
                Caption = "Voucher No.",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "R_REF_NO",
                Caption = "Voucher Ref.NO.",
                Visible = false
            });
            return settings;
        }


        #endregion

        #region Action Items
        static GridViewSettings exportActionItemsGrid;
        public static GridViewSettings ExportActionItemsGrid
        {
            get
            {
                exportActionItemsGrid = CreateExportActionItemsGrid();
                return exportActionItemsGrid;
            }
        }

        static GridViewSettings CreateExportActionItemsGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "ActionItemsListGrid";
            settings.CallbackRouteValues = new { Controller = "ActionItems", Action = "Frm_Action_Items_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Type").Caption = "Type";
            settings.Columns.Add("Date").Caption = "Date";
            settings.Columns.Add("Auditor").Caption = "Auditor";
            settings.Columns.Add("Title").Caption = "Title";
            settings.Columns.Add("Description").Caption = "Description";
            settings.Columns.Add("Due_On").Caption = "Due On";
            settings.Columns.Add("Centre_Remarks").Caption = "Centre Remarks";
            settings.Columns.Add("Close_Remarks").Caption = "Close Remarks";
            settings.Columns.Add("Closed_On").Caption = "Closed On";
            settings.Columns.Add("Closed_By").Caption = "Closed By";
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Status",
                Caption = "Status",
                GroupIndex = 0
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_Date",
                Caption = "Action Date",
                Visible = false,
                PropertiesEdit = { DisplayFormatString = "dd MMMM yyyy  HH:mm" }
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_By",
                Caption = "Action By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_Status",
                Caption = "Action Status",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_By",
                Caption = "Add By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_Date",
                Caption = "Add Date",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "CrossedTimeLimit",
                Caption = "Crossed Time Limit",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_By",
                Caption = "Edited By",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_Date",
                Caption = "Edited Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }

        #endregion

        #region InternalTransfer
        static GridViewSettings exportpInternalTransferGrid;
        public static GridViewSettings ExportpInternalTransferGrid
        {
            get
            {
                exportpInternalTransferGrid = CreateExportpInternalTransferGrid();
                return exportpInternalTransferGrid;
            }
        }
        static GridViewSettings CreateExportpInternalTransferGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "PendingInternalTransferListGrid";
            settings.CallbackRouteValues = new { Controller = "InternalTransfer", Action = "Frm_I_Transfer_Pending" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Centre_Name").Caption = "Centre Name";
            settings.Columns.Add("Centre_UID").Caption = "Centre ID";
            settings.Columns.Add("No").Caption = "No.";
            settings.Columns.Add("Zone").Caption = "Zone";
            settings.Columns.Add("Sub_Zone").Caption = "Sub Zone";
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Date",
                Caption = "Date",
                PropertiesEdit = { DisplayFormatString = "dd MMMM yyyy" }
            });
            //settings.Columns.Add("Date").Caption = "Date";
            settings.Columns.Add("Mode").Caption = "Mode";
            settings.Columns.Add("Amount").Caption = "Amount";
            settings.Columns.Add("Bank_Name").Caption = "Bank_Name";
            settings.Columns.Add("Ref_Branch").Caption = "Branch Name";
            settings.Columns.Add("Bank_Ac_No").Caption = "Bank A/c. No.";
            settings.Columns.Add("Incharge").Caption = "Incharge";
            settings.Columns.Add("Contact_No").Caption = "Contact No.";
            settings.Columns.Add("Purpose").Caption = "Purpose";
            settings.Columns.Add("Ref_No").Caption = "Ref No.";
            settings.Columns.Add("Ref_Date").Caption = "Ref.Date";
            settings.Columns.Add("Narration").Caption = "Narration";
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Description",
                Caption = "Description",
                GroupIndex = 0
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ITEM_ID",
                Caption = "ITEM_ID",
                Visible = false
            });


            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "BI_ID",
                Caption = "BI_ID",
                Visible = false

            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "PUR_ID",
                Caption = "PUR_ID",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "M_ID",
                Caption = "M_ID",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Status",
                Caption = "Status",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REF_BI_ID",
                Caption = "REF_BI_ID",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Ref_No",
                Caption = "Ref_No",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Ref_Date",
                Caption = "Ref_Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Ref_Others",
                Caption = "Ref_Others",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Ref_Bank_AccNo",
                Caption = "Ref_Bank_AccNo",
                Visible = false
            });


            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_By",
                Caption = "Action By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_Date",
                Caption = "Action Date",
                Visible = false,
                PropertiesEdit = { DisplayFormatString = "dd MMMM yyyy  HH:mm" }
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Action_Status",
                Caption = "Action Status",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_By",
                Caption = "Add By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_Date",
                Caption = "Add Date",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_By",
                Caption = "Edit By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_Date",
                Caption = "Edit Date",
                Visible = false
            });
            return settings;
        }
        #endregion

        #region Store_Dept_Master
        static GridViewSettings exportStore_Dept_MasterGrid;
        public static GridViewSettings ExportStore_Dept_MasterGrid
        {
            get
            {
                exportStore_Dept_MasterGrid = CreateExportStore_Dept_MasterGrid();
                return exportStore_Dept_MasterGrid;
            }
        }
        static GridViewSettings CreateExportStore_Dept_MasterGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "Store_Dept_MasterListGrid";
            settings.CallbackRouteValues = new { Controller = "Store_Dept_Master", Action = "Frm_Store_Dept_Master_Info_Grid" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("Store_Dept_Name").Caption = "Store/Main Dept./Sub Dept. Name";
            settings.Columns.Add("Category").Caption = "Category";
            settings.Columns.Add("Connecting_Main_Dept").Caption = "Connecting Main Dept.";
            settings.Columns.Add("Connecting_Sub_Dept").Caption = "Connecting Sub Dept.";
            settings.Columns.Add("Registration_No").Caption = "Registration/Store Number";
            settings.Columns.Add("Dept_Incharge").Caption = "In-Charge Name";
            settings.Columns.Add("Contact_No").Caption = "Contact No.";
            settings.Columns.Add("Contact_Person").Caption = "Contact Person";
            settings.Columns.Add("Premesis_Type").Caption = "Premesis Type";
            settings.Columns.Add("Premesis_Name").Caption = "Premesis Name";
            settings.Columns.Add("Is_Central_Store").Caption = "Is Central Store?";
            settings.Columns.Add("Remarks").Caption = "Remarks";
            settings.Columns.Add("Mapped_Locations").Caption = "Mapped Locations";
            settings.Columns.Add("Store_Dept_Address").Caption = "Address";
            settings.Columns.Add("Store_Dept_State").Caption = "State";
            settings.Columns.Add("Store_Dept_City").Caption = "City";




            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_By",
                Caption = "Add By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Add_Date",
                Caption = "Add Date",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_By",
                Caption = "Edit By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_Date",
                Caption = "Edit Date",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }


        #endregion

        #region personnel master

        static GridViewSettings exportPersonnel_MasterGrid;
        public static GridViewSettings ExportPersonnel_MasterGrid
        {
            get
            {
                exportPersonnel_MasterGrid = CreateExportPersonnel_MasterGrid();
                return exportPersonnel_MasterGrid;
            }
        }
        static GridViewSettings CreateExportPersonnel_MasterGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "Personnel_MasterListGrid";
            settings.CallbackRouteValues = new { Controller = "PersonnelMaster", Action = "Frm_PersonnelMaster_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "REC_ID";
            settings.Columns.Add("Name").Caption = "Name";
            settings.Columns.Add("Gender").Caption = "Gender";
            settings.Columns.Add("Personnel_Type").Caption = "Personnel Type";
            settings.Columns.Add("Skill_Type").Caption = "Skill Type";
            settings.Columns.Add("Aadhaar_No").Caption = "Aadhaar No";
            settings.Columns.Add("PAN_No").Caption = "PAN no";
            settings.Columns.Add("DOB").Caption = "Date of Birth";
            settings.Columns.Add("PF_No").Caption = "PF No";
            settings.Columns.Add("Dept_Name").Caption = "Department Name";
            settings.Columns.Add("Contractor_Name").Caption = "Contractor Name";
            settings.Columns.Add("Payment_Mode").Caption = "Payment Mode";
            settings.Columns.Add("Joining_Date").Caption = "Joining Date";
            settings.Columns.Add("Leaving_Date").Caption = "Leaving Date";
            settings.Columns.Add("Other_Details").Caption = "Other Details";

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_ADD_ON",
                Caption = "Rec. Add On",
                Visible = false,
                PropertiesEdit = { DisplayFormatString = "dd MMMM yyyy  HH:mm" }
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_ADD_BY",
                Caption = "Rec. Add On",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_EDIT_ON",
                Caption = "Rec.Edit On",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_EDIT_BY",
                Caption = "Rec.Edit By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "REC_ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }


        #endregion
        #region UserRegister
        static GridViewSettings exportUserRegisterGrid;
        public static GridViewSettings ExportUserRegisterGrid
        {
            get
            {
                exportUserRegisterGrid = CreateExportUserRegisterGrid();
                return exportUserRegisterGrid;
            }
        }
        static GridViewSettings CreateExportUserRegisterGrid()
        {
            GridViewSettings settings = new GridViewSettings();

            settings.Name = "UserRegisterListGrid";
            settings.CallbackRouteValues = new { Controller = "UserRegister", Action = "Frm_UserRegister_Info" };
            settings.Width = Unit.Percentage(100);
            settings.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
            settings.Settings.UseFixedTableLayout = true;
            settings.KeyFieldName = "ID";
            settings.Columns.Add("UserName").Caption = "UserName";
            settings.Columns.Add("PersonnelName").Caption = "PersonnelName";
            settings.Columns.Add("SewaDept").Caption = "Sewa Department";
            settings.Columns.Add("EmailID").Caption = "EmailID";
            settings.Columns.Add("ContactNo").Caption = "Mobile No";
            settings.Columns.Add("Groups").Caption = "Groups";
            settings.Columns.Add("Is_Admin").Caption = "Is_Admin";
            settings.Columns.Add("AddedOn").Caption = "AddedOn";




            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "AddedOn",
                Caption = "Add Date",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditedBy",
                Caption = "Edit By",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "Edit_By",
                Caption = "Edit By",
                Visible = false
            });
            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "EditedOn",
                Caption = "Edit Date",
                Visible = false
            });

            settings.Columns.Add(new MVCxGridViewColumn()
            {
                FieldName = "ID",
                Caption = "ID",
                Visible = false
            });
            return settings;
        }
        #endregion
        public static XtraReport Show_ListPreview(GridViewSettings settings, object Data, string xTitle, bool xLandScape, System.Drawing.Printing.PaperKind xPaperKind, bool xZoomToPageWidth,int xSetTopMargin = 50, int xSetBottomMargin = 40)
        {         
            var ps = new PrintingSystemBase();
            var xPCL = new PrintableComponentLinkBase(ps);
            xPCL.Component = GridViewExtension.CreatePrintableObject(settings, Data);
            xPCL.Landscape = xLandScape;
            xPCL.PaperKind = xPaperKind;
            xPCL.Margins = new System.Drawing.Printing.Margins(40, 40, xSetTopMargin, xSetBottomMargin);
            xPCL.PrintingSystemBase.Document.Name = xTitle;                
            xPCL.CreateDocument();
            if (xZoomToPageWidth)
            {
                xPCL.PrintingSystemBase.ExecCommand(PrintingSystemCommand.ZoomToPageWidth);
            }
            xPCL.PrintingSystemBase.ExecCommand(PrintingSystemCommand.HandTool, new object[] { true });
            xPCL.PrintingSystemBase.Document.AutoFitToPagesWidth = 1;       

            MemoryStream stream = new MemoryStream();         
            xPCL.PrintingSystemBase.SaveDocument(stream);             
            XtraReport report = new XtraReport();
            report.DisplayName = xTitle;             
            report.PrintingSystem.LoadDocument(stream);            
            ps.Dispose();
            xPCL.Dispose();
            return report;
        }
    }
}