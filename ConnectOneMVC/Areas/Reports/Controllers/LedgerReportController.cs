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
    public class LedgerReportController : BaseController
    {
        // GET: Reports/PartyLedger
        #region Start--> Default Variables

        public string SelectedPeriodValue
        {
            get
            {
                return (string)GetBaseSession("SelectedPeriodValue_LedgerReport");
            }
            set
            {
                SetBaseSession("SelectedPeriodValue_LedgerReport", value);
            }
        }
        public DateTime xFr_Date
        {
            get
            {
                return (DateTime)GetBaseSession("xFr_Date_LedgerReport");
            }
            set
            {
                SetBaseSession("xFr_Date_LedgerReport", value);
            }
        }
        public DateTime xTo_Date
        {
            get
            {
                return (DateTime)GetBaseSession("xTo_Date_LedgerReport");
            }
            set
            {
                SetBaseSession("xTo_Date_LedgerReport", value);
            }
        }
        public decimal? OpeningValue
        {
            get
            {
                return (decimal?)GetBaseSession("OpeningValue_LedgerReport");
            }
            set
            {
                SetBaseSession("OpeningValue_LedgerReport", value);
            }
        }
        public List<DbOperations.Report_Ledgers.LedgerDetailReport> LedgerReportExportData
        {
            get
            {
                return (List<DbOperations.Report_Ledgers.LedgerDetailReport>)GetBaseSession("LedgerReportExportData_LedgerReport");
            }
            set
            {
                SetBaseSession("LedgerReportExportData_LedgerReport", value);
            }
        }

        public Common_Lib.RealTimeService.Param_GelLedgerReport LedgerReport_param
        {
            get
            {
                return (Common_Lib.RealTimeService.Param_GelLedgerReport)GetBaseSession("param_LedgerReport");
            }
            set
            {
                SetBaseSession("param_LedgerReport", value);
            }
        }

        public decimal LedgerReport_LedgerOpening
        {
            get
            {
                return (decimal)GetBaseSession("LedgerOpening_LedgerReport");
            }
            set
            {
                SetBaseSession("LedgerOpening_LedgerReport", value);
            }
        }

        public List<DbOperations.Audit.Return_GetDocumentMapping> LedgerReport_DetailGrid_Data
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("LedgerReport_DetailGrid_Data_LedgerReport");
            }
            set
            {
                SetBaseSession("LedgerReport_DetailGrid_Data_LedgerReport", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> LedgerReport_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("LedgerReport_AdditionalInfoGrid_LedgerReport");
            }
            set
            {
                SetBaseSession("LedgerReport_AdditionalInfoGrid_LedgerReport", value);
            }
        }
        public void SetDefaultValues()
        {
            xFr_Date = DateTime.Now.AddYears(-5);
            xTo_Date = DateTime.Now;
            OpeningValue = null;
        }
        #endregion
        
        public ActionResult Frm_Ledger_Report(decimal? _Opening = null,string ledgerName = "", string ledgerID = "")
        {
            SetDefaultValues();
            if (!CheckRights(BASE, ClientScreen.Report_LedgerReport, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();$('#Report_LedgerReport').hide();</script>");
            }
            ViewData["LedgerReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_LedgerReport, "Export");
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_LedgerReport).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            // LedgerReportExportData = null; //Clear Previous data 
            ViewData["SelectedLedger"] = ledgerName;
            ViewData["SelectedLedgerID"] = ledgerID;
            if (_Opening != null) OpeningValue = _Opening;
            DateTime xLastDate =  DateTime.Now > BASE._open_Year_Sdt && DateTime.Now <= BASE._open_Year_Edt ? DateTime.Now : BASE._open_Year_Sdt;
            int xMM = xLastDate.Month;
            //SEL_MM = ((xMM == 4) ? 0 : ((xMM == 5) ? 1 : ((xMM == 6) ? 2 : ((xMM == 7) ? 3 : ((xMM == 8) ? 4 : ((xMM == 9) ? 5 : ((xMM == 10) ? 6 : ((xMM == 11) ? 7 : ((xMM == 12) ? 8 : ((xMM == 1) ? 9 : ((xMM == 2) ? 10 : ((xMM == 3) ? 11 : 0))))))))))));
            int SEL_MM = xMM;

            string xMonth = (SEL_MM == 1 ? "JAN" : (SEL_MM == 2 ? "FEB" : (SEL_MM == 3 ? "MAR" : (SEL_MM == 4 ? "APR" : (SEL_MM == 5 ? "MAY" : (SEL_MM == 6 ? "JUN" : (SEL_MM == 7 ? "JUL" : (SEL_MM == 8 ? "AUG" : (SEL_MM == 9 ? "SEP" : (SEL_MM == 10 ? "OCT" : (SEL_MM == 11 ? "NOV" : (SEL_MM == 12 ? "DEC" : ""))))))))))));            
            SelectedPeriodValue = xMonth + "-" + xLastDate.Year;
            ViewBag.SelectedPeriodValue = SelectedPeriodValue;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            xFr_Date = new System.DateTime(xLastDate.Year, SEL_MM, 1);
            xTo_Date = new System.DateTime(xLastDate.Year, SEL_MM, DateTime.DaysInMonth(xLastDate.Year, SEL_MM));
            //xTo_Date = xFr_Date.AddMonths(+1);
            ViewData["returnPeriod"] = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd - MMM, yyyy");
            ViewData["xFr_Date"] = xFr_Date.ToString("MM/dd/yyyy");
            ViewData["xTo_Date"] = xTo_Date.ToString("MM/dd/yyyy");
            ViewData["LedgerRep_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");

            return View();
        }
        public PartialViewResult Frm_Ledger_Report_Grid(string Fr_Date, string To_Date, string ledgerid, string command = null, string Ledger_Name =null, string sub_led_id = null, int ShowHorizontalBar = 0,bool VouchingMode=false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "")
        {
            ViewData["LedgerReport_ExportRight"] = CheckRights(BASE, ClientScreen.Report_LedgerReport, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;

            if (LedgerReportExportData == null || command == "REFRESH")
            {
                Common_Lib.RealTimeService.Param_GelLedgerReport param = new Common_Lib.RealTimeService.Param_GelLedgerReport();
                param.FromDate = new DateTime(Convert.ToInt32(Fr_Date.Split('-')[2]), Convert.ToInt32(Fr_Date.Split('-')[0]), Convert.ToInt32(Fr_Date.Split('-')[1]));
                param.ToDate = new DateTime(Convert.ToInt32(To_Date.Split('-')[2]), Convert.ToInt32(To_Date.Split('-')[0]), Convert.ToInt32(To_Date.Split('-')[1]));
                param.Led_ID = ledgerid;
                param.YearID = BASE._open_Year_ID;
                param.InsttId = BASE._open_Ins_ID;
                param.SubLedID = sub_led_id;
                decimal LedgerOpening = 0;
                if (OpeningValue == null)
                {
                    var OpeningParam = new Common_Lib.RealTimeService.Param_GetLedgerOpeningBalance();
                    OpeningParam.InsttId = BASE._open_Ins_ID;
                    OpeningParam.OnDate = BASE._open_Year_Sdt;
                    OpeningParam.Led_ID= ledgerid;
                    OpeningParam.SubLedID = sub_led_id;
                    OpeningParam.YearID = BASE._open_Year_ID;
                    LedgerOpening = (decimal)BASE._Reports_Ledgers_DBOps.GetLedgerOpening(OpeningParam);
                }

                LedgerReport_param = param;
                LedgerReport_LedgerOpening = LedgerOpening;

                var data = BASE._Reports_Ledgers_DBOps.GetLedgerReport(param, LedgerOpening, param.FromDate);
                LedgerReportExportData = data;
            }
            var _data = LedgerReportExportData as List<Common_Lib.DbOperations.Report_Ledgers.LedgerDetailReport>;
       
            return PartialView(_data);
        }
        
        #region <--NestedGrid-->
        public ActionResult Frm_Ledger_Report_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, string MID = "", bool VouchingMode = false)
        {
            ViewBag.LedgerReport_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.LedgerReport_RecID = RecID;
            ViewBag.LedgerReport_MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if(command == "REFRESH")
            {
                if(VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Accounts_CashBook);
                    LedgerReport_DetailGrid_Data = _docList;
                    Session["LedgerReport_DetailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook);
                    LedgerReport_DetailGrid_Data = data.DocumentMapping;
                    Session["LedgerReport_DetailGrid_Data"] = data.DocumentMapping;
                    LedgerReport_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(LedgerReport_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(LedgerReport_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }

        public ActionResult Refresh_GridIcon_PreviewRow(string TempID, string NestedRowKeyValue)
        {
            LedgerReportExportData = BASE._Reports_Ledgers_DBOps.GetLedgerReport(LedgerReport_param, LedgerReport_LedgerOpening, LedgerReport_param.FromDate);
            LedgerReport_DetailGrid_Data = BASE._Audit_DBOps.GetDocumentMapping(TempID, TempID, ClientScreen.Accounts_CashBook);           
            var AttachmentRow = LedgerReport_DetailGrid_Data.Where(x => x.UniqueID == NestedRowKeyValue).First();
            var Attachment_VOUCHING_STATUS = AttachmentRow.Vouching_Status;
            var Attachment_VOUCHING_REMARKS = AttachmentRow.Vouching_Remarks;
            var Attachment_Vouching_During_Audit = AttachmentRow.Vouching_During_Audit;
            var Vouching_History = AttachmentRow.Vouching_History;
            string Main_iIcon = LedgerReportExportData.Where(x => x.iTR_TEMP_ID == TempID).First().iIcon;
            return Json(new
            {
                Main_iIcon,
                Attachment_VOUCHING_STATUS,
                Attachment_VOUCHING_REMARKS,
                Attachment_Vouching_During_Audit,
                Vouching_History
            }, JsonRequestBehavior.AllowGet);
        }

        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["LedgerReport_DetailGrid_Data"];
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "LedgerReportListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "LedgerReportListGrid";
            return settings;
        }
        //public ActionResult LedgerRepoNestedCustomDataAction(string key)
        //{
        //    var Final_Data = LedgerReport_DetailGrid_Data;
        //    string itstr = "";
        //    if (Final_Data != null)
        //    {
        //        var it = Final_Data.Where(f => f.UniqueID == key).FirstOrDefault();

        //        if (it != null)
        //        {
        //            itstr = it.Doc_Status + "," + it.Params_Mandatory + "," + it.LABEL_FROM_DATE + "," + it.LABEL_TO_DATE + "," + it.LABEL_DESCRIPTION + "," + it.Document_Category + "," + it.Document_ID + "," + it.ATTACH_ID + "," + it.TxnID + "," + it.TxnMID + "," + it.MAP_ID + "," + it.Reason + "," + it.ATTACH_FILE_NAME + "," + it.Attachment_Action_Status + "," + it.UniqueID;
        //        }
        //    }
        //    return GridViewExtension.GetCustomDataCallbackResult(itstr);
        //}

        #endregion // <--NestedGrid-->
        private string GetLedgerBalanceText(List<Common_Lib.DbOperations.Report_Ledgers.LedgerDetailReport> ledgerData)
        {

            if ((ledgerData.Count > 0))
            {
                if (ledgerData[ledgerData.Count - 1].Balance != null)
                { return "Rs. " + ledgerData[ledgerData.Count - 1].Balance.ToString(); }
                else
                { return "Rs. 0"; }
            }
            else
            {
                return "Rs. 0";
            }
        }
        public ActionResult Fill_Ledgers(DataSourceLoadOptions loadOptions)
        {
            //ViewData["SelectedPartyValue"] = d1.Rows[0][0].ToString();
            var titledata = BASE._Reports_Ledgers_DBOps.GetLedgerList().OrderBy(o=>o.Name).ToList();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(titledata, loadOptions)), "application/json");
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = new DataTable();
            d1.Columns.Add("ID");
            d1.Columns.Add("Name");

            //this.Cmb_View.Properties.Items.Clear();
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                d1.Rows.Add(xMonth + "-" + BASE._open_Year_Sdt.Year, xMonth + "-" + BASE._open_Year_Sdt.Year);
                //this.Cmb_View.Properties.Items.Add(xMonth + "-" + BASE._open_Year_Sdt.Year);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                d1.Rows.Add(xMonth + "-" + BASE._open_Year_Edt.Year, xMonth + "-" + BASE._open_Year_Edt.Year);

                //this.Cmb_View.Properties.Items.Add(xMonth + "-" + BASE._open_Year_Edt.Year);
            }
            //this.Cmb_View.Properties.Items.Add("1st Quarter");
            d1.Rows.Add("1st Quarter", "1st Quarter");

            // : APR to JUN
            //this.Cmb_View.Properties.Items.Add("2nd Quarter");
            d1.Rows.Add("2nd Quarter", "2nd Quarter");

            // : JUL to SEP
            //this.Cmb_View.Properties.Items.Add("3rd Quarter");
            d1.Rows.Add("3rd Quarter", "3rd Quarter");

            // : OCT to DEC
            //this.Cmb_View.Properties.Items.Add("4th Quarter");
            d1.Rows.Add("4th Quarter", "4th Quarter");

            // : JAN to MAR
            //this.Cmb_View.Properties.Items.Add("1st Half Yearly");
            d1.Rows.Add("1st Half Yearly", "1st Half Yearly");

            // : APR to SEP
            //this.Cmb_View.Properties.Items.Add("2nd Half Yearly");
            d1.Rows.Add("2nd Half Yearly", "2nd Half Yearly");

            // : OCT to MAR
            //this.Cmb_View.Properties.Items.Add("Nine Months");
            d1.Rows.Add("Nine Months", "Nine Months");

            // : APR to DEC
            //this.Cmb_View.Properties.Items.Add("Financial Year");
            //this.Cmb_View.Properties.Items.Add("Specific Period");
            d1.Rows.Add("Financial Year", "Financial Year");
            d1.Rows.Add("Specific Period", "Specific Period");
            SelectedPeriodValue = d1.Rows[0][0].ToString();
            DataView DV1 = new DataView(d1);
            var titledata = DatatableToModel.DataTabletoTitle_INFO(DV1.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(titledata, loadOptions)), "application/json");

        }
        public ActionResult GetSelectPeriod(string xFr_DateSelected, string xTo_DateSelected)
        {
            bool isSectionPeriod = false;
            if (xFr_DateSelected == "1st Quarter")
            {
                xFr_Date = new System.DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(+3).AddDays(-1);
                isSectionPeriod = true;
                //Q2
            }
            else if (xFr_DateSelected == "2nd Quarter")
            {
                xFr_Date = new System.DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = xFr_Date.AddMonths(+3).AddDays(-1);
                isSectionPeriod = true;
                //Q3
            }
            else if (xFr_DateSelected == "3rd Quarter")
            {
                xFr_Date = new System.DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = xFr_Date.AddMonths(+3).AddDays(-1);
                isSectionPeriod = true;
                //Q4
            }
            else if (xFr_DateSelected == "4th Quarter")
            {
                xFr_Date = new System.DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = xFr_Date.AddMonths(+3).AddDays(-1);
                isSectionPeriod = true;
                //H1
            }
            else if (xFr_DateSelected == "1st Half Yearly")
            {
                xFr_Date = new System.DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(+6).AddDays(-1);
                isSectionPeriod = true;
                //H2
            }
            else if (xFr_DateSelected == "2nd Half Yearly")
            {
                xFr_Date = new System.DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = xFr_Date.AddMonths(+6).AddDays(-1);
                isSectionPeriod = true;
                //NINE MONTHS
            }
            else if (xFr_DateSelected == "Nine Months")
            {
                xFr_Date = new System.DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = xFr_Date.AddMonths(+9).AddDays(-1);
                isSectionPeriod = true;
                //FINANCIAL YEAR
            }
            else if (xFr_DateSelected == "Financial Year")
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
                isSectionPeriod = true;
                //SPECIFIC PERIOD
            }
            if (isSectionPeriod == false)
            {
                xFr_Date = Convert.ToDateTime(xFr_DateSelected);
                xTo_Date = new System.DateTime(xFr_Date.Year, xFr_Date.Month, DateTime.DaysInMonth(xFr_Date.Year, xFr_Date.Month));
            }
            if (!string.IsNullOrEmpty(xTo_DateSelected))
            {
                xTo_Date = Convert.ToDateTime(xTo_DateSelected);
            }

            string returnPeriod = "Fr.: " + xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + xTo_Date.ToString("dd - MMM, yyyy");
            return Json(new
            {
                returnPeriod = returnPeriod,
                xFr_Date = xFr_Date.ToString("MM/dd/yyyy"),
                xTo_Date = xTo_Date.ToString("MM/dd/yyyy")
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLedgerBalance()
        {
            var _data = LedgerReportExportData as List<Common_Lib.DbOperations.Report_Ledgers.LedgerDetailReport>;
            string Balance = GetLedgerBalanceText(_data);
            return Json(new
            {
                LedgerBalance = Balance
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options()
        {
            if ((!CheckRights(BASE, ClientScreen.Report_LedgerReport, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('LedgerReport_report_modal','Not Allowed','No Rights');$('#btnLedgerPrintExport').hide();</script>");
            }
            return PartialView();
        }
        public JsonResult Check_Period_Selection(string _FrDate, string _ToDate)
        {
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate;
            DateTime toDate;
            String Msg = "";

            if ((DateTime.TryParse(_FrDate, out frDate) == false))
            {
                Msg = "Date Incorrect / Blank...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

            if ((DateTime.TryParse(_ToDate, out toDate) == false))
            {
                Msg = "Date Incorrect / Blank...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

            double diff = toDate.Subtract(frDate).TotalDays;
            if ((diff < 0))
            {
                Msg = "From Date cannot be Higher Than To Date...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

            diff = frDate.Subtract(BASE._open_Year_Sdt).TotalDays;
            if ((diff < 0))
            {
                Msg = "From Date not as per Financial Year...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }

            diff = toDate.Subtract(BASE._open_Year_Edt).TotalDays;
            if ((diff > 0))
            {
                Msg = "To Date not as per Financial Year...!";
                return Json(new
                {
                    message = Msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                message = Msg,
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public void SessionClear()
        {
            ClearBaseSession("_LedgerReport");
            Session.Remove("LedgerReport_DetailGrid_Data");
        }
    }
}