using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOne.D0010._001;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Reports.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    public class DailyBalancesController : BaseController
    {
        // GET: Reports/DailyBalances
        #region Global Variables
        public string Balances
        {
            get
            {
                return (string)GetBaseSession("Balances_DailyBal");
            }
            set
            {
                SetBaseSession("Balances_DailyBal", value);
            }
        }
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> DailyBalance_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("DailyBalance_AdditionalInfoGrid_DailyReport");
            }
            set
            {
                SetBaseSession("DailyBalance_AdditionalInfoGrid_DailyReport", value);
            }
        }
        public List<DailyBalancesModel> DailyBalances_ExportData
        {
            get
            {
                return (List<DailyBalancesModel>)GetBaseSession("DailyBalances_ExportData_DailyReport");
            }
            set
            {
                SetBaseSession("DailyBalances_ExportData_DailyReport", value);
            }
        }
        public List<BankReconcileModel> DailyBalances_ExportDataReconcile
        {
            get
            {
                return (List<BankReconcileModel>)GetBaseSession("DailyBalances_ExportDataReconcile_DailyReport");
            }
            set
            {
                SetBaseSession("DailyBalances_ExportDataReconcile_DailyReport", value);
            }
        }
        public string _BankAccno
        {
            get
            {
                return (string)GetBaseSession("BankAccno_DailyBal");
            }
            set
            {
                SetBaseSession("BankAccno_DailyBal", value);
            }
        }
        public string _xView_Sel_Id
        {
            get
            {
                return (string)GetBaseSession("xView_Sel_Id_DailyBal");
            }
            set
            {
                SetBaseSession("xView_Sel_Id_DailyBal", value);
            }
        }
        public string _xStatus_Choice
        {
            get
            {
                return (string)GetBaseSession("xStatus_Choice_DailyBal");
            }
            set
            {
                SetBaseSession("xStatus_Choice_DailyBal", value);
            }
        }
        public string Bank_ID
        {
            get
            {
                return (string)GetBaseSession("Bank_ID_DailyBal");
            }
            set
            {
                SetBaseSession("Bank_ID_DailyBal", value);
            }
        }
        public string _BankName
        {
            get
            {
                return (string)GetBaseSession("BankName_DailyBal");
            }
            set
            {
                SetBaseSession("BankName_DailyBal", value);
            }
        }
        public string _DisplayType
        {
            get
            {
                return (string)GetBaseSession("DisplayType_DailyBal");
            }
            set
            {
                SetBaseSession("DisplayType_DailyBal", value);
            }
        }
        public string Txt_TitleX
        {
            get
            {
                return (string)GetBaseSession("Txt_TitleX_DailyBal");
            }
            set
            {
                SetBaseSession("Txt_TitleX_DailyBal", value);
            }
        }
        public string Led_ID
        {
            get
            {
                return (string)GetBaseSession("Led_ID_DailyBal");
            }
            set
            {
                SetBaseSession("Led_ID_DailyBal", value);
            }
        }
        public string OtherCondition
        {
            get
            {
                return (string)GetBaseSession("OtherCondition_DailyBal");
            }
            set
            {
                SetBaseSession("OtherCondition_DailyBal", value);
            }
        }
        public string _Bank_Acc_ID
        {
            get
            {
                return (string)GetBaseSession("Bank_Acc_ID_DailyBal");
            }
            set
            {
                SetBaseSession("Bank_Acc_ID_DailyBal", value);
            }
        }
        public DateTime? xFr_Date
        {
            get { return (DateTime?)GetBaseSession("xFr_Date_DailyReport"); }
            set { SetBaseSession("xFr_Date_DailyReport", value); }
        }
        public DateTime? xTo_Date
        {
            get { return (DateTime?)GetBaseSession("xTo_Date_DailyReport"); }
            set { SetBaseSession("xTo_Date_DailyReport", value); }
        }
        public int xSelViewIndex
        {
            get { return (int)GetBaseSession("xSelViewIndex_DailyBal"); }
            set { SetBaseSession("xSelViewIndex_DailyBal", value); }
        }
        public int xSelModeIndex
        {
            get { return (int)GetBaseSession("xSelModeIndex_DailyBal"); }
            set { SetBaseSession("xSelModeIndex_DailyBal", value); }
        }
        public List<CB_Period> DB_PeriodSelectionData
        {
            get { return (List<CB_Period>)GetBaseSession("DB_PeriodSelectionData_DailyBal"); }
            set { SetBaseSession("DB_PeriodSelectionData_DailyBal", value); }
        }
        public List<DailyBalance> DB_Bank_Data
        {
            get { return (List<DailyBalance>)GetBaseSession("DB_Bank_Data_DailyBal"); }
            set { SetBaseSession("DB_Bank_Data_DailyBal", value); }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> Daily_Balances_DetailGrid_Data
        {
            get { return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("Daily_Balances_DetailGrid_Data_DailyBal"); }
            set { SetBaseSession("Daily_Balances_DetailGrid_Data_DailyBal", value); }
        }

        public DateTime _xFr_Date()
        {
            return (DateTime)xFr_Date;
        }
        public DateTime _xTo_Date()
        {
            return (DateTime)xTo_Date;
        }
        public void SetDefaultValues()
        {
            Bank_ID = "";
            _BankName = "";
            _DisplayType = "";
            Txt_TitleX = "";
            Led_ID = "";
            OtherCondition = "";
            _Bank_Acc_ID = "";
        }

        #endregion

        public ActionResult Dialog_DailyBalances(int xSelViewIndex = -1, int xSelModeIndex = -1, string Bank_Acc_ID = "", string xStatus_Choice = "", bool ViaOptionClick = false)
        {
            SetDefaultValues();
            DailyBalancesModel model = new DailyBalancesModel();
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.ViaOptionClick = ViaOptionClick;
            this.xSelViewIndex = xSelViewIndex;
            this.xSelModeIndex = xSelModeIndex;
            if (!CheckRights(BASE, ClientScreen.Report_Daily_Balances, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
            }
            ViewBag.PeriodSelectionData = Fill_Change_Period_Items();
            int xMM = DateTime.Now.Month;
            if (xSelViewIndex > -1)
            {
                model.Cmb_View_DB = xSelViewIndex;
            }
            else
            {
                model.Cmb_View_DB = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            }
            model.rdo_Balances_Mode_DB = "CASH";
            model.RadioGroup2_DB = "On Screen";
            model.RadioGroup3_DB = "All";
            if (xSelModeIndex == -1)
            {
                model.rdo_Balances_Mode_DB = "BANK";
            }
            else
            {
                model.rdo_Balances_Mode_DB = "CASH";
            }
            if (ViaOptionClick == true)
            {
                if (xSelModeIndex == 1)
                {
                    model.GLookUp_BankList_DB = Bank_Acc_ID;
                    model.rdo_Balances_Mode_DB = "BANK";
                }
            }
            model.RadioGroup3_DB = xStatus_Choice.ToUpper() == "UNRECONCILED" ? "Unreconciled" : "All";

            ViewData["DailyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export");
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment

            return View(model);
        }
        public List<CB_Period> Fill_Change_Period_Items()
        {
            var period = new List<CB_Period>();
            int index = 0;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                CB_Period row1 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row1.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row1.SelectedIndex = index;
                index++;
                period.Add(row1);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                CB_Period row2 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row2.SelectedIndex = index;
                row2.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                period.Add(row2);
                index++;
            }
            CB_Period row = new CB_Period
            {
                Period = "1st Quarter",
                SelectedIndex = index
            };
            period.Add(row);
            CB_Period row3 = new CB_Period
            {
                Period = "2nd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row3);
            CB_Period row4 = new CB_Period
            {
                Period = "3rd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row4);
            CB_Period row5 = new CB_Period
            {
                Period = "4th Quarter",
                SelectedIndex = ++index
            };
            period.Add(row5);
            CB_Period row6 = new CB_Period
            {
                Period = "1st Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row6);
            CB_Period row7 = new CB_Period
            {
                Period = "2nd Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row7);
            CB_Period row8 = new CB_Period
            {
                Period = "Nine Months",
                SelectedIndex = ++index
            };
            period.Add(row8);
            CB_Period row9 = new CB_Period
            {
                Period = "Financial Year",
                SelectedIndex = ++index
            };
            period.Add(row9);
            CB_Period row10 = new CB_Period
            {
                Period = "Specific Period",
                SelectedIndex = ++index
            };
            period.Add(row10);
            DB_PeriodSelectionData = period;
            return period;
        }
        public ActionResult Cmb_View_SelectedIndexChanged(int? SelectedIndex = null)
        {
            var Perioddata = (List<CB_Period>)DB_PeriodSelectionData;
            string Text = Perioddata.Where(x => x.SelectedIndex == SelectedIndex).First().Period;
            if (SelectedIndex >= 0 && SelectedIndex <= 11)
            {
                string Sel_Mon = Text.Substring(0, 3).ToUpper();
                int SEL_MM = Sel_Mon == "JAN" ? 1 : Sel_Mon == "FEB" ? 2 : Sel_Mon == "MAR" ? 3 : Sel_Mon == "APR" ? 4 : Sel_Mon == "MAY" ? 5 : Sel_Mon == "JUN" ? 6 : Sel_Mon == "JUL" ? 7 : Sel_Mon == "AUG" ? 8 : Sel_Mon == "SEP" ? 9 : Sel_Mon == "OCT" ? 10 : Sel_Mon == "NOV" ? 11 : Sel_Mon == "DEC" ? 12 : 4;
                xFr_Date = new DateTime(Convert.ToInt32(Text.Substring(4, 4)), SEL_MM, 1);
                xTo_Date = _xFr_Date().AddMonths(1).AddDays(-1);
            }
            else if (SelectedIndex == 12)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 13)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 14)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 15)
            {
                xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 16)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (SelectedIndex == 17)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (SelectedIndex == 18)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(9).AddDays(-1);
            }
            else if (SelectedIndex == 19)
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
            }
            string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");
            return Json(new
            {
                BE_View_Period,
                FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                ToDate = _xTo_Date().ToString("MM/dd/yyyy")
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult frm_Voucher_Specific_Period()
        {
            DB_SpeceficPeriod model = new DB_SpeceficPeriod();
            model.DB_Fromdate = _xFr_Date();
            model.DB_Todate = _xTo_Date();
            return View(model);
        }
        [HttpPost]
        public ActionResult GetSpecificPeriod(DB_SpeceficPeriod model)
        {
            if (model.DB_Todate >= model.DB_Fromdate)
            {
                xFr_Date = model.DB_Fromdate;
                xTo_Date = model.DB_Todate;
                string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd - MMM, yyyy");
                return Json(new
                {
                    result = true,
                    BE_View_Period,
                    FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                    ToDate = _xTo_Date().ToString("MM/dd/yyyy")
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = false,
                    message = "To Date Cannot Be Less From From Date..!!"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RefreshBankList()
        {
            DataTable BA_Table = BASE._Voucher_DBOps.GetBankAccountsList();
            if (BA_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = (Branch_IDs + ("\'" + (xRow["BA_BRANCH_ID"].ToString() + "\',")));
            }
            if ((Branch_IDs.Trim().Length > 0))
            {
                Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            }
            if ((Branch_IDs.Trim().Length == 0))
            {
                Branch_IDs = "\'\'";
            }
            DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);
            if (BB_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            // BUILD DATA

            var BuildData = (from BB in BB_Table.AsEnumerable()
                             join BA in BA_Table.AsEnumerable()
                             on BB["BB_BRANCH_ID"] equals BA["BA_BRANCH_ID"]
                             select new DailyBalance
                             {
                                 BANK_NAME = BB.Field<string>("Name"),
                                 BI_SHORT_NAME = BB.Field<string>("BI_SHORT_NAME"),
                                 BANK_BRANCH = BB.Field<string>("Branch"),
                                 BANK_ACC_NO = BA.Field<string>("BA_ACCOUNT_NO"),
                                 BA_ID = BA.Field<string>("ID"),
                                 BANK_ID = BB.Field<string>("BANK_ID"),
                             }).ToList();

            var Final_Data = BuildData.ToList();
            Final_Data.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            DB_Bank_Data = Final_Data;
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LookUp_Get_Bank_List(bool? IsVisible, DataSourceLoadOptions loadOptions)
        {
            if (DB_Bank_Data == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<DailyBalance>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(DB_Bank_Data, loadOptions)), "application/json");
        }

        public ActionResult Frm_Daily_Balances_info_Grid(string command, string DisplayType = "", string BankAccno="",string xView_Sel_Id="",string xStatus_Choice="",string bankName="",string Bank_Acc_ID="", int ShowHorizontalBar = 0, bool VouchingMode = false, string Layout = null,string ViewMode="Default",string ColumnToBeHidddenIndex="",string ColumnToBeShownIndex="")
        {           
            ViewData["DailyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export");
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.DisplayType = DisplayType;
            ViewData["Layout"] = Layout;
            var Balancestext = Balances;
            ViewBag.Balances = Balancestext;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            if (command == "REFRESH")
            {
                Frm_Daily_Balances_Report(DisplayType,Bank_Acc_ID,bankName, xStatus_Choice,xView_Sel_Id, BankAccno);
            }          
            return PartialView(DailyBalances_ExportData);
        }
        public ActionResult Frm_Daily_Balances_info_DetailGrid(string RecID, string command, int ShowHorizontalBar = 0, string MID = "", bool VouchingMode = false)
        {
            ViewBag.Daily_Balances_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.Daily_Balances_RecID = RecID;
            ViewBag.DailyBalanceMID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Accounts_CashBook);
                    Daily_Balances_DetailGrid_Data = _docList;
                    Session["Daily_Balances_detailGrid_Data"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(RecID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook);
                    Daily_Balances_DetailGrid_Data = data.DocumentMapping;
                    Session["Daily_Balances_detailGrid_Data"] = data.DocumentMapping;
                    DailyBalance_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }  
            return PartialView(Daily_Balances_DetailGrid_Data);
        }
        public ActionResult AdditionalInfo_Grid()
        {
            return View(DailyBalance_AdditionalInfoGrid);
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
            Frm_Daily_Balances_Report(_DisplayType, _Bank_Acc_ID, _BankName, _xStatus_Choice, _xView_Sel_Id, _BankAccno);
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(TempID, TempID, ClientScreen.Accounts_CashBook);
            Daily_Balances_DetailGrid_Data = _docList;
            var AttachmentRow = Daily_Balances_DetailGrid_Data.Where(x => x.UniqueID == NestedRowKeyValue).First();
            var Attachment_VOUCHING_STATUS = AttachmentRow.Vouching_Status;
            var Attachment_VOUCHING_REMARKS = AttachmentRow.Vouching_Remarks;
            var Attachment_Vouching_During_Audit = AttachmentRow.Vouching_During_Audit;
            var Vouching_History = AttachmentRow.Vouching_History;
            string Main_iIcon = DailyBalances_ExportData.Where(x => x.iTR_TEMP_ID == TempID).First().iIcon;
            return Json(new
            {
                Main_iIcon,
                Attachment_VOUCHING_STATUS,
                Attachment_VOUCHING_REMARKS,
                Attachment_Vouching_During_Audit,
                Vouching_History
            }, JsonRequestBehavior.AllowGet);
        }
        public static GridViewSettings NestedGridExportSettings(string RecID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "DailyBalancesListGrid" + RecID;
            settings.Width = Unit.Percentage(100);

            settings.KeyFieldName = "UniqueID";
            settings.Columns.Add("Item_Name");
            settings.Columns.Add("Document_Name");
            settings.Columns.Add("Reason");
            settings.Columns.Add("FromDate");
            settings.Columns.Add("ToDate");
            settings.Columns.Add("Description");
            settings.Columns.Add("ATTACH_FILE_NAME");
            settings.SettingsDetail.MasterGridName = "DailyBalancesListGrid";
            return settings;
        }
        public static IEnumerable NestedGridExportData(string RecID)
        {
            return (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["Daily_Balances_DetailGrid_Data"];
        }
        public ActionResult Frm_Bank_Reconcile(string xBankAccID, string xDate, string lblTxnBalance)
        {
            ViewData["DailyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export");
            DateTime X_Date = Convert.ToDateTime(xDate);
            DataTable _Party_Table = BASE._Voucher_DBOps.GetBank_Reconciliation(X_Date, xBankAccID) as DataTable;
            List<BankReconcileModel> BuildData = new List<BankReconcileModel>();
            decimal ClearingBalance = Convert.ToDecimal(lblTxnBalance);
            if (_Party_Table.Rows.Count > 0)
            {
                foreach (DataRow cRow in _Party_Table.Rows)
                {
                    if (Convert.ToString(cRow["Status"]).ToLower().Contains("add:"))
                        ClearingBalance += Convert.ToDecimal(cRow["Amount"]);
                    else
                        ClearingBalance -= Convert.ToDecimal(cRow["Amount"]);
                }
            }
            string Reconciletext = "Reconciliation as on " + X_Date.ToString("dd-MMM, yyyy");
            ViewBag.Reconciletext = Reconciletext;
            decimal lblBalance = Math.Round(System.Convert.ToDecimal(lblTxnBalance.ToString()), 2);
            decimal lblClearingBalance = Math.Round(System.Convert.ToDecimal(ClearingBalance.ToString()), 2);
            decimal lblNetBalance = Math.Round(System.Convert.ToDecimal(lblTxnBalance) - System.Convert.ToDecimal(lblClearingBalance), 2);
            ViewBag.ClearingBalance = lblClearingBalance;
            ViewBag.lblNetBalance = lblNetBalance;
            ViewBag.Balance = lblBalance;
            BuildData = (from DataRow T in _Party_Table.AsEnumerable()
                         select new BankReconcileModel
                         {
                             VoucherDate = string.IsNullOrEmpty(T["Voucher Date"].ToString()) ? "" : Convert.ToDateTime(T["Voucher Date"]).ToString(),
                             ClearingDate = string.IsNullOrEmpty(T["Clearing Date"].ToString()) ? "" : Convert.ToDateTime(T["Clearing Date"]).ToString(),
                             Mode = T["Mode"].ToString(),
                             RefNo = T["Ref No."].ToString(),
                             Amount = T["Amount"].ToString(),
                             status = T["Status"].ToString(),

                         }).ToList();
            var Final_Data = BuildData.ToList();
            DailyBalances_ExportDataReconcile = Final_Data;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return View(Final_Data);
        }
        public ActionResult Frm_Bank_Reconcile_Grid(string command, int ShowHorizontalBar = 0,string lblTxnBalance="",string xDate="",string xBankAccID="")
        {
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (command == "REFRESH")
            {
                Frm_Bank_Reconcile(xBankAccID, xDate, lblTxnBalance);
            }
            var Final_Data = DailyBalances_ExportDataReconcile as List<BankReconcileModel>;
            return PartialView(DailyBalances_ExportDataReconcile);
        }
        public ActionResult DBCustomDataAction(string key)
        {
            var FinalData = DailyBalances_ExportData as List<DailyBalancesModel>;
            var FDData = (DailyBalancesModel)FinalData.Where(f => f.iREC_ID == key).FirstOrDefault();
            string itstr = "";
            if (FDData != null)
            {
                itstr = FDData.ITR_Date + "![" + FDData.Item + "![" + FDData.Mode + "![" + FDData.RefNo + "![" + FDData.Party + "![" +
                            FDData.Instrumentdate + "![" + FDData.Clearingdate + "![" + FDData.Debit + "![" + FDData.Credit + "![" + FDData.Balance + "![" + FDData.iREC_ID + "![" + FDData.iTR_M_ID+"!["+FDData.iTR_TEMP_ID;
                // FDData.Action_Status + "![" + FDData.Action_By + "![" + FDData.Action_Date + "![" + FDData.TR_ID + "![" + FDData.ID + "![" + FDData.Remarks + "![" + FDData.RemarkStatus + "![" +
                //FDData.OpenActions + "![" + FDData.CrossedTimeLimit + "![" + FDData.YearID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult Daily_BalanceGridCustomDataAction(string key)
        {
            var Final_Data = Daily_Balances_DetailGrid_Data;
            string itstr = "";
            if (Final_Data != null)
            {
                var it = Final_Data.Where(f => f.UniqueID == key).FirstOrDefault();

                if (it != null)
                {
                    itstr = it.Doc_Status + "![" + it.Params_Mandatory + "![" + it.LABEL_FROM_DATE + "![" + it.LABEL_TO_DATE + "![" + it.LABEL_DESCRIPTION + "![" + it.Document_Category + "![" + it.Document_ID + "![" + it.ATTACH_ID + "![" + it.TxnID + "![" + it.TxnMID + "![" + it.MAP_ID + "![" + it.Reason + "![" + it.ATTACH_FILE_NAME + "![" + it.Attachment_Action_Status + "![" + it.UniqueID + "![" + it.ReasonID;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        #region export
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('DailyBalances_report_modal','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        public ActionResult Frm_Export_Options_Reconcile()
        {
            if (!CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_DailyBal");
            Session.Remove("Daily_Balances_DetailGrid_Data");
        }
        [HttpPost]
        public ActionResult Frm_Daily_Balances_Report(string DisplayType, string Bank_Acc_ID, string bankName, string xStatus_Choice, string xView_Sel_Id, string BankAccno)
        {
            _DisplayType = DisplayType;
            _Bank_Acc_ID = Bank_Acc_ID;
            _BankName = bankName;
            _BankAccno = BankAccno;
            _xView_Sel_Id = xView_Sel_Id;
            _xStatus_Choice = xStatus_Choice;
            string OtherCondition = "";
            string Txt_TitleX = "";
            string Led_ID = "";
            ViewData["DailyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export");
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Daily_Balances).ToString()) ? 1 : 0;

            ViewBag.UserType = BASE._open_User_Type;
            if (DisplayType.ToUpper().Trim() == "BANK")
            {
                Txt_TitleX = "Daily Bank Balances";
                Led_ID = "00079";
                //OtherCondition = " AND ( TR_SUB_CR_LED_ID ='" + Bank_Acc_ID + "' OR TR_SUB_DR_LED_ID ='" + Bank_Acc_ID + "' )";
                OtherCondition = "BANK";
            }
            if (DisplayType.ToUpper().Trim() == "CASH")
            {
                Txt_TitleX = "Daily Cash Balances";
                Led_ID = "00080";
                OtherCondition = "";
            }
            Txt_TitleX += "(Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy") + ")";
            if (!string.IsNullOrWhiteSpace(bankName))
            {
                Txt_TitleX += " for " + bankName;
            }
            if (xStatus_Choice.ToLower() == "unreconciled")//redmine bug #133301 fixed
            {
               // OtherCondition += " AND TI.TR_REF_CDATE IS NULL ";
                OtherCondition += " unreconciled";
                Txt_TitleX += " (Un-Reconciled) ";
            }
            DataTable XTABLE = CreateData(Bank_Acc_ID, OtherCondition, Led_ID, DisplayType);
            ViewBag.Txt_TitleX = Txt_TitleX;
            ViewBag.DisplayType = DisplayType;
            ViewBag.OtherCondition = OtherCondition;
            ViewBag.Bank_Acc_ID = Bank_Acc_ID;
            ViewBag.xView_Sel_Id = xView_Sel_Id;
            ViewBag.xStatus_Choice = xStatus_Choice;
            ViewBag.BankAccNo = BankAccno;
            ViewBag.BankName = bankName;
            SetBalances(XTABLE);
            List<DailyBalancesModel> BuildData = new List<DailyBalancesModel>();
            foreach (DataRow T in XTABLE.Rows)
            {
                DailyBalancesModel newrow = new DailyBalancesModel();
                newrow.ITR_Date = Convert.IsDBNull(T["ITR_DATE"]) ? (DateTime?)null : Convert.ToDateTime(T["ITR_DATE"]);
                newrow.Item = T["ITR_ITEM"].ToString();
                newrow.iTR_ITEM_ID= T["iTR_ITEM_ID"].ToString();
                newrow.Mode = T["ITR_MODE"].ToString();
                newrow.RefNo = T["Ref"].ToString();
                newrow.Party = T["ITR_PARTY_1"].ToString();
                newrow.Instrumentdate = Convert.IsDBNull(T["ITR_REF_DATE"]) ? (DateTime?)null : Convert.ToDateTime(T["ITR_REF_DATE"]);
                newrow.Clearingdate = Convert.IsDBNull(T["ITR_REF_CDATE"]) ? (DateTime?)null : Convert.ToDateTime(T["ITR_REF_CDATE"]);
                if (!Convert.IsDBNull(T["ITR_RECEIPT"]))
                {
                    newrow.Debit = Convert.ToDouble(T["ITR_RECEIPT"]);
                }
                else { newrow.Debit = 0.00; }
                if (!Convert.IsDBNull(T["ITR_PAYMENT"]))
                {
                    newrow.Credit = Convert.ToDouble(T["ITR_PAYMENT"]);
                }
                else { newrow.Credit = 0.00; }
                if (!Convert.IsDBNull(T["ITR_BALANCE"]))
                {
                    newrow.Balance = Convert.ToDouble(T["ITR_BALANCE"]);
                }
                else { newrow.Balance = 0.00; }
                newrow.SpecialVoucherReference = T["SPECIAL_VOUCHER_REFERENCE"].ToString();
                newrow.iREC_ID = T["iREC_ID"].ToString();
                newrow.iTR_M_ID = T["iTR_M_ID"].ToString();
                newrow.iTR_TEMP_ID = T["iTR_TEMP_ID"].ToString();
                newrow.Grid_PK = (string.IsNullOrEmpty(T.Field<string>("iTR_M_ID")) ? "Null" : T.Field<string>("iTR_M_ID")) + (string.IsNullOrEmpty(T.Field<string>("iREC_ID")) ? "Null" : T.Field<string>("iREC_ID"));
                newrow.iREQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]);
                newrow.iCOMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]);
                newrow.iRESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]);
                newrow.iREJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]);
                newrow.iOTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]);
                newrow.iALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]);

                newrow.VOUCHING_PENDING_COUNT = Convert.IsDBNull(T["VOUCHING_PENDING_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_PENDING_COUNT"]);
                newrow.VOUCHING_ACCEPTED_COUNT = Convert.IsDBNull(T["VOUCHING_ACCEPTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_ACCEPTED_COUNT"]);
                newrow.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = Convert.IsDBNull(T["VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"]);
                newrow.VOUCHING_REJECTED_COUNT = Convert.IsDBNull(T["VOUCHING_REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_REJECTED_COUNT"]);
                newrow.VOUCHING_TOTAL_COUNT = Convert.IsDBNull(T["VOUCHING_TOTAL_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_TOTAL_COUNT"]);
                newrow.AUDIT_PENDING_COUNT = Convert.IsDBNull(T["AUDIT_PENDING_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_PENDING_COUNT"]);
                newrow.AUDIT_ACCEPTED_COUNT = Convert.IsDBNull(T["AUDIT_ACCEPTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_ACCEPTED_COUNT"]);
                newrow.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = Convert.IsDBNull(T["AUDIT_ACCEPTED_WITH_REMARKS_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_ACCEPTED_WITH_REMARKS_COUNT"]);
                newrow.AUDIT_REJECTED_COUNT = Convert.IsDBNull(T["AUDIT_REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_REJECTED_COUNT"]);
                newrow.AUDIT_TOTAL_COUNT = Convert.IsDBNull(T["AUDIT_TOTAL_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_TOTAL_COUNT"]);
                newrow.iIcon = "";

                if ((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                {
                    newrow.iIcon += "RedShield|";
                }
                else if (((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((T.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                {
                    newrow.iIcon += "GreenShield|";
                }
                else if ((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                {
                    newrow.iIcon += "YellowShield|";
                }
                else if (((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((T.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                {
                    newrow.iIcon += "BlueShield|";
                }
                if (((T.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                {
                    newrow.iIcon += "RedFlag|";
                }
                if ((((T.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (T.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                {
                    newrow.iIcon += "RequiredAttachment|";
                }
                else if ((((T.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (T.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                {
                    newrow.iIcon += "AdditionalAttachment|";
                }
                if ((T.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newrow.iIcon += "VouchingAccepted|"; }
                if ((T.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newrow.iIcon += "VouchingReject|"; }
                if ((T.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newrow.iIcon += "VouchingAcceptWithRemarks|"; }
                if ((T.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (T.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newrow.iIcon += "VouchingPartial|"; }
                if ((T.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newrow.iIcon += "AuditAccepted|"; }
                if ((T.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newrow.iIcon += "AuditReject|"; }
                if ((T.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newrow.iIcon += "AuditAcceptWithRemarks|"; }
                if ((T.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (T.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newrow.iIcon += "AuditPartial|"; }

                BuildData.Add(newrow);
            }        
            DailyBalances_ExportData = BuildData;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["DailyBalReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(BuildData);
        }
        public DataTable CreateData(string _Bank_Acc_ID = "", string _OtherCondition = "", string _Led_ID = "", string DisplayType = "")
        {
            double Open_Cash_Bal = 0; double Close_Cash_Bal = 0;
            double Open_Bank_Bal = 0; double Close_Bank_Bal = 0;
            Get_Cash_Bank_Balance(_Bank_Acc_ID, ref Open_Cash_Bal, ref Close_Cash_Bal, ref Open_Bank_Bal, ref Close_Bank_Bal);
            DataTable ITEM_Table = BASE._Voucher_DBOps.GetItemList("ID", "ITEM_NAME");
            DataTable Unique_AB_Table = BASE._Voucher_DBOps.GetPastParties(_xFr_Date(), _xTo_Date(), false);
            string AB_IDs = "";
            foreach (DataRow xRow in Unique_AB_Table.Rows)
            {
                AB_IDs += "'" + xRow["TR_AB_ID_1"].ToString() + "',";
            }
            if (AB_IDs.Trim().Length > 0)
            {
                AB_IDs = AB_IDs.Trim().EndsWith(",") ? AB_IDs.Substring(0, AB_IDs.Trim().Length - 1) : AB_IDs.Trim();
            }
            if (AB_IDs.Trim().Length == 0)
            {
                AB_IDs = "''";
            }
            //Get Party Name
            DataTable AB_TABLE = BASE._Voucher_DBOps.GetPastPartyDetails(AB_IDs);
            //Get Internal Transfer Centre List
            DataTable Trf_CEN_Table = BASE._Voucher_DBOps.GetPastParties(_xFr_Date(), _xTo_Date(), true);
            string CEN_REC_IDs = "";
            foreach (DataRow xRow in Trf_CEN_Table.Rows)
            {
                CEN_REC_IDs += "'" + xRow["TR_AB_ID_1"].ToString() + "',";
            }
            if (CEN_REC_IDs.Trim().Length > 0)
            {
                CEN_REC_IDs = CEN_REC_IDs.Trim().EndsWith(",") ? CEN_REC_IDs.Substring(0, CEN_REC_IDs.Trim().Length - 1) : CEN_REC_IDs.Trim();
            }
            if (CEN_REC_IDs.Trim().Length == 0)
            {
                CEN_REC_IDs = "''";
            }
            //Get Centre Name
            DataTable CEN_Name_Table = BASE._Voucher_DBOps.GetCenterList(CEN_REC_IDs);
            //Bank List
            //(A) Bank A/c.
            DataTable BA_Table = BASE._Voucher_DBOps.GetSavingAccountList();
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs += "'" + xRow["BA_BRANCH_ID"].ToString() + "',";
            }
            if (Branch_IDs.Trim().Length > 0)
            {
                Branch_IDs = Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Substring(0, Branch_IDs.Trim().Length - 1) : Branch_IDs.Trim();
            }
            if (Branch_IDs.Trim().Length == 0)
            {
                Branch_IDs = "''";
            }
            //(B) Bank Branch
            DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);
            DataSet Bank_DS = new DataSet();
            Bank_DS.Tables.Add(BA_Table);
            Bank_DS.Tables.Add(BB_Table);
            DataRelation BA_Relation = Bank_DS.Relations.Add("BANK", Bank_DS.Tables["BANK_ACCOUNT_INFO"].Columns["BA_BRANCH_ID"], Bank_DS.Tables["BANK_BRANCH_INFO"].Columns["BB_BRANCH_ID"], false);
            foreach (DataRow XROW in Bank_DS.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(BA_Relation))
                {
                    XROW["BI_SHORT_NAME"] = _Row["BI_SHORT_NAME"];
                }
            }
            Bank_DS.Dispose();
            //Transaction
            DataTable TR_Table = BASE._Voucher_DBOps.GetList(_xFr_Date(), _xTo_Date(), _Led_ID, _Bank_Acc_ID, _OtherCondition) as DataTable;
            DataSet Voucher_DS = new DataSet();
            Voucher_DS.Tables.Add(TR_Table.Copy());
            //Item
            Voucher_DS.Tables.Add(ITEM_Table.Copy());
            DataRelation Item_Relation = Voucher_DS.Relations.Add("Item", Voucher_DS.Tables[0].Columns["iTR_ITEM_ID"], Voucher_DS.Tables[1].Columns["ID"], false);
            //Party
            Voucher_DS.Tables.Add(AB_TABLE.Copy());
            DataRelation AB_Relation = Voucher_DS.Relations.Add("AB", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_AB_ID_1"], Voucher_DS.Tables["ADDRESS_BOOK"].Columns["C_ID"], false);
            // 'Centre
            Voucher_DS.Tables.Add(CEN_Name_Table.Copy());
            DataRelation Centre_Relation = Voucher_DS.Relations.Add("CEN_NAME", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_AB_ID_1"], Voucher_DS.Tables["CENTRE_INFO"].Columns["REC_ID"], false);
            //Bank
            Voucher_DS.Tables.Add(Bank_DS.Tables[0].Copy());
            DataRelation BANK_Relation = Voucher_DS.Relations.Add("BANK_ACC", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_SUB_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            DataRelation BANK_Relation2 = Voucher_DS.Relations.Add("BANK_ACC2", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_CR_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            // '-------------------(3) Update Relational Data--------------------------------------------------------------------------------------------
            foreach (DataRow XROW in Voucher_DS.Tables[0].Rows)
            {
                // Item
                foreach (DataRow Item_Row in XROW.GetChildRows(Item_Relation))
                {
                    if (XROW["iREC_ID"].Equals("NOTE-BOOK"))
                    {
                        XROW["iTR_ITEM"] = "Monthly " + Item_Row["ITEM_NAME"];
                    }
                    else
                    {
                        XROW["iTR_ITEM"] = Item_Row["ITEM_NAME"];
                    }
                    if (XROW["Ref"].ToString().Length > 0)
                    {
                        XROW["iTR_ITEM"] = XROW["iTR_ITEM"];
                    }
                }
                // Party
                foreach (var _Row in XROW.GetChildRows(AB_Relation))
                {
                    XROW["iTR_PARTY_1"] = _Row["C_NAME"];
                }
                // Centre
                foreach (var _Row in XROW.GetChildRows(Centre_Relation))
                {
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 8 && XROW["iTR_PARTY_1"].ToString().Length <= 0)
                    {
                        XROW["iTR_PARTY_1"] = Convert.ToString(_Row["CEN_NAME"]) + " (" + _Row["CEN_BK_PAD_NO"] + ")";
                    }
                }
                // Bank
                foreach (var _Row in XROW.GetChildRows(BANK_Relation))
                {
                    if (XROW["iTR_PARTY_1"].ToString().Length <= 0)
                    {
                        XROW["iTR_PARTY_1"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
                foreach (var _Row in XROW.GetChildRows(BANK_Relation2))
                {
                    if (XROW["iTR_CR_NAME"].ToString().Length <= 0)
                    {
                        XROW["iTR_CR_NAME"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
            }
            // '-------------------(4) Clear Relation-----------------------------------------------------------------------------------------------------------
            Voucher_DS.Relations.Clear();

            //  '-------------------(5) Insert Opening Balance---------------------------------------------------------------------------------------------------

            int _Date_Serial = 0;
            string _Date_Show = "";
            if (Convert.ToInt32(_xFr_Date().ToString("MM")) > 3)
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date().AddMonths(-3).ToString("MM"));
                _Date_Show = BASE._open_Year_Sdt.ToString("yyyy") + "-" + string.Format(_xFr_Date().ToString("MM"), "00") + "-01";
            }
            else
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date().AddMonths(+9).ToString("MM"));
                _Date_Show = BASE._open_Year_Edt.ToString("yyyy") + "-" + string.Format(_xFr_Date().ToString("MM"), "00") + "-01";
            }
            DataRow ROW;
            ROW = Voucher_DS.Tables[0].NewRow();
            ROW["iTR_DATE_SERIAL"] = _Date_Serial;
            ROW["iTR_DATE_SHOW"] = _Date_Show;
            ROW["iTR_TEMP_ID"] = "OPENING BALANCE";
            ROW["iREC_ID"] = "OPENING BALANCE";
            ROW["iTR_ROW_POS"] = "A";
            ROW["iTR_VNO"] = "";
            ROW["iTR_DATE"] = string.Format(_xFr_Date().ToString(), BASE._Date_Format_Current);
            ROW["ITR_REF_CDATE"] = BASE._open_Year_Sdt;
            ROW["iTR_REC_CASH"] = Open_Cash_Bal;
            ROW["iTR_REC_BANK"] = Open_Bank_Bal;
            ROW["iTR_ITEM"] = "OPENING BALANCE";
            Voucher_DS.Tables[0].Rows.Add(ROW);
            DataView DV1 = new DataView(Voucher_DS.Tables[0]);
            DV1.Sort = "iTR_DATE,iTR_ROW_POS,iTR_ENTRY,iTR_M_ID,iTR_SORT,iTR_SR_NO,iREC_ADD_ON";
            DataTable XTABLE = DV1.ToTable();
            string _TEMP = "";
            if (XTABLE.Rows.Count > 0)
            {
                _TEMP = DV1.Table.Rows[0]["iTR_TEMP_ID"].ToString();
            }
            double _SR = 1;
            double _Temp_Balance_C, _Temp_Receipt_C, _Temp_Payment_C;
            _Temp_Balance_C = 0; _Temp_Receipt_C = 0; _Temp_Payment_C = 0;
            double _Temp_Balance_B, _Temp_Receipt_B, _Temp_Payment_B;
            _Temp_Balance_B = 0; _Temp_Receipt_B = 0; _Temp_Payment_B = 0;

            XTABLE.Columns.Add("iTR_RECEIPT", Type.GetType("System.Double"));
            XTABLE.Columns.Add("iTR_PAYMENT", Type.GetType("System.Double"));
            XTABLE.Columns.Add("iTR_BALANCE", Type.GetType("System.Double"));
            XTABLE.Columns.Add("iTR_VOUCHER", Type.GetType("System.String"));

            foreach (DataRow XROW in XTABLE.Rows)
            {
                if (XROW["iTR_TEMP_ID"].ToString() == _TEMP)
                {
                    XROW["iTR_REF_NO"] = _SR;
                }
                else
                {
                    _TEMP = XROW["iTR_TEMP_ID"].ToString();
                    _SR = _SR + 1;
                    XROW["iTR_REF_NO"] = _SR;
                }

                if (DisplayType.ToUpper().Trim() == "CASH")
                {
                    if (!Convert.IsDBNull(XROW["iTR_REC_CASH"]))
                    {
                        _Temp_Receipt_C = Convert.ToDouble(XROW["iTR_REC_CASH"]);
                    }
                    else
                    {
                        _Temp_Receipt_C = 0;
                    }
                    if (!Convert.IsDBNull(XROW["iTR_PAY_CASH"]))
                    {
                        _Temp_Payment_C = Convert.ToDouble(XROW["iTR_PAY_CASH"]);
                    }
                    else
                    {
                        _Temp_Payment_C = 0;
                    }
                    if (_Temp_Receipt_C <= 0 & _Temp_Payment_C <= 0)
                    {
                    }
                    else
                    {
                        _Temp_Balance_C = (_Temp_Balance_C + _Temp_Receipt_C) - _Temp_Payment_C;
                        if (_Temp_Receipt_C > 0)
                        {
                            XROW["iTR_RECEIPT"] = _Temp_Receipt_C;
                        }
                        if (_Temp_Payment_C > 0)
                        {
                            XROW["iTR_PAYMENT"] = _Temp_Payment_C;
                        }
                        XROW["iTR_BALANCE"] = _Temp_Balance_C;
                    }
                }
                if (DisplayType.ToUpper().Trim() == "BANK")
                {
                    if (!Convert.IsDBNull(XROW["iTR_REC_BANK"]))
                    {
                        _Temp_Receipt_B = Convert.ToDouble(XROW["iTR_REC_BANK"]);
                    }
                    else
                    {
                        _Temp_Receipt_B = 0;
                    }
                    if (!Convert.IsDBNull(XROW["iTR_PAY_BANK"]))
                    {
                        _Temp_Payment_B = Convert.ToDouble(XROW["iTR_PAY_BANK"]);
                    }
                    else
                    {
                        _Temp_Payment_B = 0;
                    }
                    if (_Temp_Receipt_B <= 0 & _Temp_Payment_B <= 0)
                    {
                    }
                    else
                    {
                        _Temp_Balance_B = (_Temp_Balance_B + _Temp_Receipt_B) - _Temp_Payment_B;
                        if (_Temp_Receipt_B > 0)
                        {
                            XROW["iTR_RECEIPT"] = _Temp_Receipt_B;
                        }
                        if (_Temp_Payment_B > 0)
                        {
                            XROW["iTR_PAYMENT"] = _Temp_Payment_B;
                        }
                        XROW["iTR_BALANCE"] = _Temp_Balance_B;
                    }
                    if (!Convert.IsDBNull(XROW["iTR_SUB_ID"]))
                    {
                        if (XROW["iTR_SUB_ID"].ToString() == _Bank_Acc_ID)
                        {
                            XROW["iTR_PARTY_1"] = XROW["iTR_CR_NAME"];
                        }
                    }
                }
                if (!Convert.IsDBNull(XROW["iTR_CODE"]))
                {
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 1)
                        XROW["iTR_VOUCHER"] = "Cash Deposit / Withdrawn";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 2)
                        XROW["iTR_VOUCHER"] = "Bank to Bank Transfer";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 3)
                        XROW["iTR_VOUCHER"] = "Payment";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 4)
                        XROW["iTR_VOUCHER"] = "Receipt";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 5)
                        XROW["iTR_VOUCHER"] = "Donation - Regular";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 6)
                        XROW["iTR_VOUCHER"] = "Donation - Foreign";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 7)
                        XROW["iTR_VOUCHER"] = "Donation - Gift";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 8)
                        XROW["iTR_VOUCHER"] = "Internal Transfer";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 9)
                        XROW["iTR_VOUCHER"] = "Collection Box";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 10)
                        XROW["iTR_VOUCHER"] = "Fixed Deposits (F.D.)";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 11)
                        XROW["iTR_VOUCHER"] = "Sale Asset";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 12)
                        XROW["iTR_VOUCHER"] = "Membership - New";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 13)
                        XROW["iTR_VOUCHER"] = "Membership - Renewal";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 14)
                        XROW["iTR_VOUCHER"] = "Journal";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 15)
                        XROW["iTR_VOUCHER"] = "Asset Transfer";
                }
            }
            return XTABLE;
        }
        public void Get_Cash_Bank_Balance(string _ID, ref double Open_Cash_Bal, ref double Close_Cash_Bal, ref double Open_Bank_Bal, ref double Close_Bank_Bal)
        {
            Open_Cash_Bal = 0; Close_Cash_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Open_Cash_Bal = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["CLOSING"]))
                {
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                }
                else
                {
                    Close_Cash_Bal = 0;
                }
            }
            else
            {
                Open_Cash_Bal = 0; Close_Cash_Bal = 0;
            }
            Open_Bank_Bal = 0; Close_Bank_Bal = 0;
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
                {
                    if (XROW["ID"].ToString() == _ID)
                    {
                        if (!Convert.IsDBNull(XROW["OPENING"]))
                        {
                            Open_Bank_Bal += Convert.ToDouble(XROW["OPENING"]);
                        }
                        else
                        {
                            Open_Bank_Bal += 0;
                        }
                        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        {
                            Close_Bank_Bal += Convert.ToDouble(XROW["CLOSING"]);
                        }
                        else
                        {
                            Close_Bank_Bal += 0;
                        }
                    }
                }
            }
            else
            {
                Open_Bank_Bal = 0; Close_Bank_Bal = 0;
            }
        }
        public void SetBalances(DataTable xTABLE)
        {
            decimal SetTxnBalance = 0.00M; decimal SetNetBalance = 0.00M; decimal SetClearingBalance = 0.00M;
            foreach (DataRow cRow in xTABLE.Rows)
            {
                if (!Convert.IsDBNull(cRow["iTR_RECEIPT"]))
                {
                    if ((double)cRow["iTR_RECEIPT"] > 0)
                    {
                        SetTxnBalance = SetTxnBalance + Convert.ToDecimal(cRow["iTR_RECEIPT"]);
                        if (!Convert.IsDBNull(cRow["iTR_REF_CDATE"]))
                        {
                            SetClearingBalance = SetClearingBalance + Convert.ToDecimal(cRow["iTR_RECEIPT"]);
                        }
                    }
                }
                if (!Convert.IsDBNull(cRow["iTR_PAYMENT"]))
                {
                    if ((double)cRow["iTR_PAYMENT"] > 0)
                    {
                        SetTxnBalance = SetTxnBalance - Convert.ToDecimal(cRow["iTR_PAYMENT"]);
                        if (!Convert.IsDBNull(cRow["iTR_REF_CDATE"]))
                        {
                            SetClearingBalance = SetClearingBalance - Convert.ToDecimal(cRow["iTR_PAYMENT"]);
                        }
                    }
                }
            }
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            cultureInfo.NumberFormat.CurrencySymbol = "₹";
            SetNetBalance = SetTxnBalance - SetClearingBalance;
            ViewBag.SetTxnBalance = String.Format(CultureInfo.CurrentCulture, "{0:C}", SetTxnBalance).Replace("(", "-").Replace(")", "");
            ViewBag.SetNetBalance = String.Format(CultureInfo.CurrentCulture, "{0:C}", SetNetBalance).Replace("(", "-").Replace(")", "");
            ViewBag.SetClearingBalance = String.Format(CultureInfo.CurrentCulture, "{0:C}", SetClearingBalance).Replace("(", "-").Replace(")", "");
        }
        [HttpPost]
        public void UpdateCDate(string iRec_ID, string Cdate)
        {
            var data = DailyBalances_ExportData;
            var Rowdata = data.Where(x => x.iREC_ID == iRec_ID).FirstOrDefault();
            var instrumentdate = Rowdata.Instrumentdate;
            var Clearingdate = Rowdata.Clearingdate;
            if (Convert.ToDateTime(Cdate) != Convert.ToDateTime(Clearingdate))
            {
                if (instrumentdate != null)
                {
                    if (Convert.ToDateTime(Cdate) > Convert.ToDateTime(instrumentdate))
                    {
                        Rowdata.Clearingdate = Convert.ToDateTime(Cdate);
                    }
                }
            }
            DailyBalances_ExportData = data;
        }
        [HttpPost]
        public ActionResult SaveClick(string[] updateList)
        {
            if (updateList != null)
            {
                if (updateList.Count() > 0)
                {
                    for (int i = 0; i < updateList.Count(); i++)
                    {
                        var iREC_ID = updateList[i].Split('^')[0];
                        var Cdate = updateList[i].Split('^')[1];
                        var Rowdata = DailyBalances_ExportData.Where(x => x.iREC_ID == iREC_ID).FirstOrDefault();
                        var Mode = Rowdata.Mode;
                        var TxnDate = Rowdata.ITR_Date;
                        var Ref = Rowdata.RefNo;
                        var iTR_M_ID = Rowdata.iTR_M_ID;
                        var instrumentdate = Rowdata.Instrumentdate;
                        var Clearingdate = Rowdata.Clearingdate;
                        if (instrumentdate != null)
                        {
                            if (Convert.ToDateTime(Cdate) < Convert.ToDateTime(instrumentdate))
                            {
                                continue;
                            }
                        }
                        Param_UpdateClearingDate upParam = new Param_UpdateClearingDate();
                        upParam.iRecID = iREC_ID;
                        upParam.ClearingDate = Convert.ToDateTime(Cdate);
                        upParam.TxnDate = Convert.ToDateTime(TxnDate);
                        upParam.iRefNo = Ref;
                        upParam.Mode = Mode;
                        upParam.iTrMID = iTR_M_ID;
                        BASE._Reports_Common_DBOps.UpdateClearingDate(upParam);
                    }
                }
            }
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }
        public void SessionClear_Report()
        {
            ClearBaseSession("_DailyReport");
        }
        #region Dev Extreme 
        public ActionResult Dialog_DailyBalances_dx(int xSelViewIndex = -1, int xSelModeIndex = -1, string Bank_Acc_ID = "", string xStatus_Choice = "", bool ViaOptionClick = false)
        {
            if (!CheckRights(BASE, ClientScreen.Report_Daily_Balances, "List"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
            }
            DailyBalancesModel model = new DailyBalancesModel();
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.ViaOptionClick = ViaOptionClick;   
            int xMM = DateTime.Now.Month;
            if (xSelViewIndex > -1)
            {
                model.Cmb_View_DB = xSelViewIndex;
            }
            else
            {
                model.Cmb_View_DB = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            }
            model.rdo_Balances_Mode_DB = "CASH";
            model.RadioGroup2_DB = "On Screen";
            model.RadioGroup3_DB = "All";
            if (xSelModeIndex == -1)
            {
                model.rdo_Balances_Mode_DB = "BANK";
            }
            else
            {
                model.rdo_Balances_Mode_DB = "CASH";
            }
            if (ViaOptionClick == true)
            {
                if (xSelModeIndex == 1)
                {
                    model.GLookUp_BankList_DB = Bank_Acc_ID;
                    model.rdo_Balances_Mode_DB = "BANK";
                }
            }
            model.RadioGroup3_DB = xStatus_Choice.ToUpper() == "UNRECONCILED" ? "Unreconciled" : "All";
            ViewBag.OpenYearSdt = BASE._open_Year_Sdt;
            ViewBag.OpenYearEdt = BASE._open_Year_Edt;
            return View(model);
        }
        public ActionResult Frm_Daily_Balances_Report_dx(string DisplayType, string Bank_Acc_ID, string bankName, string xStatus_Choice, string xView_Sel_Id, string BankAccno, string FromDate = "", string ToDate = "")
        {           
            string OtherCondition = "";
            string Txt_TitleX = "";
            string Led_ID = "";      

            if (DisplayType.ToUpper().Trim() == "BANK")
            {
                Txt_TitleX = "Daily Bank Balances";
                Led_ID = "00079";           
                OtherCondition = "BANK";
            }
            if (DisplayType.ToUpper().Trim() == "CASH")
            {
                Txt_TitleX = "Daily Cash Balances";
                Led_ID = "00080";
                OtherCondition = "";
            }
            Txt_TitleX += "(Fr.: " + Convert.ToDateTime(FromDate).ToString("dd-MMM, yyyy") + "  to  " + Convert.ToDateTime(ToDate).ToString("dd-MMM, yyyy") + ")";
            if (!string.IsNullOrWhiteSpace(bankName))
            {
                Txt_TitleX += " for " + bankName;
            }
            if (xStatus_Choice.ToLower() == "unreconciled")//redmine bug #133301 fixed
            {
                // OtherCondition += " AND TI.TR_REF_CDATE IS NULL ";
                OtherCondition += " unreconciled";
                Txt_TitleX += " (Un-Reconciled) ";
            }
            ViewBag.Txt_TitleX = Txt_TitleX;
            ViewBag.DisplayType = DisplayType;
            ViewBag.OtherCondition = OtherCondition;
            ViewBag.Bank_Acc_ID = Bank_Acc_ID;
            ViewBag.xView_Sel_Id = xView_Sel_Id;
            ViewBag.xStatus_Choice = xStatus_Choice;
            ViewBag.Led_ID = Led_ID;
            ViewBag.BankAccNo = BankAccno;
            ViewBag.BankName = bankName;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Daily_Balances).ToString()) ? 1 : 0;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.FromDate = FromDate;
            ViewBag.ToDate = ToDate;
            ViewData["DailyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export");        
            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["DailyBalReport_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                      || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View();
        }
        [HttpGet]
        public ActionResult Frm_Daily_Balances_Grid_Data(string OtherCondition = "", string Led_ID = "", string DisplayType = "", string Bank_Acc_ID = "", string FromDate = "", string ToDate = "")
        {
            DataTable XTABLE = CreateData_dx(Bank_Acc_ID, OtherCondition, Led_ID, DisplayType,FromDate,ToDate);           
            List<DailyBalancesModel> GridData = new List<DailyBalancesModel>();
            foreach (DataRow T in XTABLE.Rows)
            {           
                DailyBalancesModel newrow = new DailyBalancesModel();
                newrow.ITR_Date = Convert.IsDBNull(T["ITR_DATE"]) ? (DateTime?)null : Convert.ToDateTime(T["ITR_DATE"]);
                newrow.Item = T["ITR_ITEM"].ToString();
                newrow.iTR_ITEM_ID = T["iTR_ITEM_ID"].ToString();
                newrow.Mode = T["ITR_MODE"].ToString();
                newrow.RefNo = T["Ref"].ToString();
                newrow.Party = T["ITR_PARTY_1"].ToString();
                newrow.Instrumentdate = Convert.IsDBNull(T["ITR_REF_DATE"]) ? (DateTime?)null : Convert.ToDateTime(T["ITR_REF_DATE"]);
                newrow.Clearingdate = Convert.IsDBNull(T["ITR_REF_CDATE"]) ? (DateTime?)null : Convert.ToDateTime(T["ITR_REF_CDATE"]);
                if (!Convert.IsDBNull(T["ITR_RECEIPT"]))
                {
                    newrow.Debit = Convert.ToDouble(T["ITR_RECEIPT"]);
                }
                else { newrow.Debit = 0.00; }
                if (!Convert.IsDBNull(T["ITR_PAYMENT"]))
                {
                    newrow.Credit = Convert.ToDouble(T["ITR_PAYMENT"]);
                }
                else { newrow.Credit = 0.00; }
                if (!Convert.IsDBNull(T["ITR_BALANCE"]))
                {
                    newrow.Balance = Convert.ToDouble(T["ITR_BALANCE"]);
                }
                else { newrow.Balance = 0.00; }
                newrow.SpecialVoucherReference = T["SPECIAL_VOUCHER_REFERENCE"].ToString();
                newrow.iREC_ID = T["iREC_ID"].ToString();
                newrow.iTR_M_ID = T["iTR_M_ID"].ToString();
                newrow.iTR_TEMP_ID = T["iTR_TEMP_ID"].ToString();
                newrow.Grid_PK = (string.IsNullOrEmpty(T.Field<string>("iTR_M_ID")) ? "Null" : T.Field<string>("iTR_M_ID")) + (string.IsNullOrEmpty(T.Field<string>("iREC_ID")) ? "Null" : T.Field<string>("iREC_ID"));
                newrow.iREQ_ATTACH_COUNT = Convert.IsDBNull(T["REQ_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["REQ_ATTACH_COUNT"]);
                newrow.iCOMPLETE_ATTACH_COUNT = Convert.IsDBNull(T["COMPLETE_ATTACH_COUNT"]) ? (int?)null : Convert.ToInt32(T["COMPLETE_ATTACH_COUNT"]);
                newrow.iRESPONDED_COUNT = Convert.IsDBNull(T["RESPONDED_COUNT"]) ? (int?)null : Convert.ToInt32(T["RESPONDED_COUNT"]);
                newrow.iREJECTED_COUNT = Convert.IsDBNull(T["REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["REJECTED_COUNT"]);
                newrow.iOTHER_ATTACH_CNT = Convert.IsDBNull(T["OTHER_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["OTHER_ATTACH_CNT"]);
                newrow.iALL_ATTACH_CNT = Convert.IsDBNull(T["ALL_ATTACH_CNT"]) ? (int?)null : Convert.ToInt32(T["ALL_ATTACH_CNT"]);

                newrow.VOUCHING_PENDING_COUNT = Convert.IsDBNull(T["VOUCHING_PENDING_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_PENDING_COUNT"]);
                newrow.VOUCHING_ACCEPTED_COUNT = Convert.IsDBNull(T["VOUCHING_ACCEPTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_ACCEPTED_COUNT"]);
                newrow.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = Convert.IsDBNull(T["VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"]);
                newrow.VOUCHING_REJECTED_COUNT = Convert.IsDBNull(T["VOUCHING_REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_REJECTED_COUNT"]);
                newrow.VOUCHING_TOTAL_COUNT = Convert.IsDBNull(T["VOUCHING_TOTAL_COUNT"]) ? (int?)null : Convert.ToInt32(T["VOUCHING_TOTAL_COUNT"]);
                newrow.AUDIT_PENDING_COUNT = Convert.IsDBNull(T["AUDIT_PENDING_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_PENDING_COUNT"]);
                newrow.AUDIT_ACCEPTED_COUNT = Convert.IsDBNull(T["AUDIT_ACCEPTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_ACCEPTED_COUNT"]);
                newrow.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = Convert.IsDBNull(T["AUDIT_ACCEPTED_WITH_REMARKS_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_ACCEPTED_WITH_REMARKS_COUNT"]);
                newrow.AUDIT_REJECTED_COUNT = Convert.IsDBNull(T["AUDIT_REJECTED_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_REJECTED_COUNT"]);
                newrow.AUDIT_TOTAL_COUNT = Convert.IsDBNull(T["AUDIT_TOTAL_COUNT"]) ? (int?)null : Convert.ToInt32(T["AUDIT_TOTAL_COUNT"]);
                newrow.iIcon = "";

                if ((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) == 0 && (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0))
                {
                    newrow.iIcon += "RedShield|";
                }
                else if (((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((T.Field<Int32?>("RESPONDED_COUNT") ?? 0) == 0)))
                {
                    newrow.iIcon += "GreenShield|";
                }
                else if ((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) > 0 && (((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) < (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0))))
                {
                    newrow.iIcon += "YellowShield|";
                }
                else if (((((T.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0) + (T.Field<Int32?>("RESPONDED_COUNT") ?? 0)) >= (T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0)) && ((T.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0) > 0) && ((T.Field<Int32?>("RESPONDED_COUNT") ?? 0) > 0)))
                {
                    newrow.iIcon += "BlueShield|";
                }
                if (((T.Field<Int32?>("REJECTED_COUNT") ?? 0) > 0))
                {
                    newrow.iIcon += "RedFlag|";
                }
                if ((((T.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (T.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) == 0))
                {
                    newrow.iIcon += "RequiredAttachment|";
                }
                else if ((((T.Field<Int32?>("ALL_ATTACH_CNT") ?? 0) > 0) && (T.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0) != 0))
                {
                    newrow.iIcon += "AdditionalAttachment|";
                }
                if ((T.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0) { newrow.iIcon += "VouchingAccepted|"; }
                if ((T.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0) { newrow.iIcon += "VouchingReject|"; }
                if ((T.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newrow.iIcon += "VouchingAcceptWithRemarks|"; }
                if ((T.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0) > 0 && ((T.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0) > 0 || (T.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0) > 0)) { newrow.iIcon += "VouchingPartial|"; }
                if ((T.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) == 0 && (T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0) { newrow.iIcon += "AuditAccepted|"; }
                if ((T.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0) { newrow.iIcon += "AuditReject|"; }
                if ((T.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0) == (T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) && (T.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0) > 0) { newrow.iIcon += "AuditAcceptWithRemarks|"; }
                if ((T.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0) > 0 && ((T.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0) > 0 || (T.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0) > 0)) { newrow.iIcon += "AuditPartial|"; }

                GridData.Add(newrow);
            }    
            var result = new
            {
                GridData           
            };
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }
        public DataTable CreateData_dx(string _Bank_Acc_ID = "", string _OtherCondition = "", string _Led_ID = "", string DisplayType = "",string FromDate="",string ToDate="")
        {
            double Open_Cash_Bal = 0; double Close_Cash_Bal = 0;
            double Open_Bank_Bal = 0; double Close_Bank_Bal = 0;
            Get_Cash_Bank_Balance_dx(_Bank_Acc_ID, ref Open_Cash_Bal, ref Close_Cash_Bal, ref Open_Bank_Bal, ref Close_Bank_Bal,FromDate,ToDate);
            DataTable ITEM_Table = BASE._Voucher_DBOps.GetItemList("ID", "ITEM_NAME");
            DataTable Unique_AB_Table = BASE._Voucher_DBOps.GetPastParties(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), false);
            string AB_IDs = "";
            foreach (DataRow xRow in Unique_AB_Table.Rows)
            {
                AB_IDs += "'" + xRow["TR_AB_ID_1"].ToString() + "',";
            }
            if (AB_IDs.Trim().Length > 0)
            {
                AB_IDs = AB_IDs.Trim().EndsWith(",") ? AB_IDs.Substring(0, AB_IDs.Trim().Length - 1) : AB_IDs.Trim();
            }
            if (AB_IDs.Trim().Length == 0)
            {
                AB_IDs = "''";
            }
            //Get Party Name
            DataTable AB_TABLE = BASE._Voucher_DBOps.GetPastPartyDetails(AB_IDs);
            //Get Internal Transfer Centre List
            DataTable Trf_CEN_Table = BASE._Voucher_DBOps.GetPastParties(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), true);
            string CEN_REC_IDs = "";
            foreach (DataRow xRow in Trf_CEN_Table.Rows)
            {
                CEN_REC_IDs += "'" + xRow["TR_AB_ID_1"].ToString() + "',";
            }
            if (CEN_REC_IDs.Trim().Length > 0)
            {
                CEN_REC_IDs = CEN_REC_IDs.Trim().EndsWith(",") ? CEN_REC_IDs.Substring(0, CEN_REC_IDs.Trim().Length - 1) : CEN_REC_IDs.Trim();
            }
            if (CEN_REC_IDs.Trim().Length == 0)
            {
                CEN_REC_IDs = "''";
            }
            //Get Centre Name
            DataTable CEN_Name_Table = BASE._Voucher_DBOps.GetCenterList(CEN_REC_IDs);
            //Bank List
            //(A) Bank A/c.
            DataTable BA_Table = BASE._Voucher_DBOps.GetSavingAccountList();
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs += "'" + xRow["BA_BRANCH_ID"].ToString() + "',";
            }
            if (Branch_IDs.Trim().Length > 0)
            {
                Branch_IDs = Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Substring(0, Branch_IDs.Trim().Length - 1) : Branch_IDs.Trim();
            }
            if (Branch_IDs.Trim().Length == 0)
            {
                Branch_IDs = "''";
            }
            //(B) Bank Branch
            DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);
            DataSet Bank_DS = new DataSet();
            Bank_DS.Tables.Add(BA_Table);
            Bank_DS.Tables.Add(BB_Table);
            DataRelation BA_Relation = Bank_DS.Relations.Add("BANK", Bank_DS.Tables["BANK_ACCOUNT_INFO"].Columns["BA_BRANCH_ID"], Bank_DS.Tables["BANK_BRANCH_INFO"].Columns["BB_BRANCH_ID"], false);
            foreach (DataRow XROW in Bank_DS.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(BA_Relation))
                {
                    XROW["BI_SHORT_NAME"] = _Row["BI_SHORT_NAME"];
                }
            }
            Bank_DS.Dispose();
            //Transaction
            DataTable TR_Table = BASE._Voucher_DBOps.GetList(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), _Led_ID, _Bank_Acc_ID, _OtherCondition) as DataTable;
            DataSet Voucher_DS = new DataSet();
            Voucher_DS.Tables.Add(TR_Table.Copy());
            //Item
            Voucher_DS.Tables.Add(ITEM_Table.Copy());
            DataRelation Item_Relation = Voucher_DS.Relations.Add("Item", Voucher_DS.Tables[0].Columns["iTR_ITEM_ID"], Voucher_DS.Tables[1].Columns["ID"], false);
            //Party
            Voucher_DS.Tables.Add(AB_TABLE.Copy());
            DataRelation AB_Relation = Voucher_DS.Relations.Add("AB", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_AB_ID_1"], Voucher_DS.Tables["ADDRESS_BOOK"].Columns["C_ID"], false);
            // 'Centre
            Voucher_DS.Tables.Add(CEN_Name_Table.Copy());
            DataRelation Centre_Relation = Voucher_DS.Relations.Add("CEN_NAME", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_AB_ID_1"], Voucher_DS.Tables["CENTRE_INFO"].Columns["REC_ID"], false);
            //Bank
            Voucher_DS.Tables.Add(Bank_DS.Tables[0].Copy());
            DataRelation BANK_Relation = Voucher_DS.Relations.Add("BANK_ACC", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_SUB_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            DataRelation BANK_Relation2 = Voucher_DS.Relations.Add("BANK_ACC2", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_CR_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            // '-------------------(3) Update Relational Data--------------------------------------------------------------------------------------------
            foreach (DataRow XROW in Voucher_DS.Tables[0].Rows)
            {
                // Item
                foreach (DataRow Item_Row in XROW.GetChildRows(Item_Relation))
                {
                    if (XROW["iREC_ID"].Equals("NOTE-BOOK"))
                    {
                        XROW["iTR_ITEM"] = "Monthly " + Item_Row["ITEM_NAME"];
                    }
                    else
                    {
                        XROW["iTR_ITEM"] = Item_Row["ITEM_NAME"];
                    }
                    if (XROW["Ref"].ToString().Length > 0)
                    {
                        XROW["iTR_ITEM"] = XROW["iTR_ITEM"];
                    }
                }
                // Party
                foreach (var _Row in XROW.GetChildRows(AB_Relation))
                {
                    XROW["iTR_PARTY_1"] = _Row["C_NAME"];
                }
                // Centre
                foreach (var _Row in XROW.GetChildRows(Centre_Relation))
                {
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 8 && XROW["iTR_PARTY_1"].ToString().Length <= 0)
                    {
                        XROW["iTR_PARTY_1"] = Convert.ToString(_Row["CEN_NAME"]) + " (" + _Row["CEN_BK_PAD_NO"] + ")";
                    }
                }
                // Bank
                foreach (var _Row in XROW.GetChildRows(BANK_Relation))
                {
                    if (XROW["iTR_PARTY_1"].ToString().Length <= 0)
                    {
                        XROW["iTR_PARTY_1"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
                foreach (var _Row in XROW.GetChildRows(BANK_Relation2))
                {
                    if (XROW["iTR_CR_NAME"].ToString().Length <= 0)
                    {
                        XROW["iTR_CR_NAME"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
            }
            // '-------------------(4) Clear Relation-----------------------------------------------------------------------------------------------------------
            Voucher_DS.Relations.Clear();

            //  '-------------------(5) Insert Opening Balance---------------------------------------------------------------------------------------------------

            int _Date_Serial = 0;
            string _Date_Show = "";
            if (Convert.ToInt32(Convert.ToDateTime(FromDate).ToString("MM")) > 3)
            {
                _Date_Serial = Convert.ToInt32(Convert.ToDateTime(FromDate).AddMonths(-3).ToString("MM"));
                _Date_Show = BASE._open_Year_Sdt.ToString("yyyy") + "-" + string.Format(Convert.ToDateTime(FromDate).ToString("MM"), "00") + "-01";
            }
            else
            {
                _Date_Serial = Convert.ToInt32(Convert.ToDateTime(FromDate).AddMonths(+9).ToString("MM"));
                _Date_Show = BASE._open_Year_Edt.ToString("yyyy") + "-" + string.Format(Convert.ToDateTime(FromDate).ToString("MM"), "00") + "-01";
            }
            DataRow ROW;
            ROW = Voucher_DS.Tables[0].NewRow();
            ROW["iTR_DATE_SERIAL"] = _Date_Serial;
            ROW["iTR_DATE_SHOW"] = _Date_Show;
            ROW["iTR_TEMP_ID"] = "OPENING BALANCE";
            ROW["iREC_ID"] = "OPENING BALANCE";
            ROW["iTR_ROW_POS"] = "A";
            ROW["iTR_VNO"] = "";
            ROW["iTR_DATE"] = string.Format(Convert.ToDateTime(FromDate).ToString(), BASE._Date_Format_Current);
            ROW["ITR_REF_CDATE"] = BASE._open_Year_Sdt;
            ROW["iTR_REC_CASH"] = Open_Cash_Bal;
            ROW["iTR_REC_BANK"] = Open_Bank_Bal;
            ROW["iTR_ITEM"] = "OPENING BALANCE";
            Voucher_DS.Tables[0].Rows.Add(ROW);
            DataView DV1 = new DataView(Voucher_DS.Tables[0]);
            DV1.Sort = "iTR_DATE,iTR_ROW_POS,iTR_ENTRY,iTR_M_ID,iTR_SORT,iTR_SR_NO,iREC_ADD_ON";
            DataTable XTABLE = DV1.ToTable();
            string _TEMP = "";
            if (XTABLE.Rows.Count > 0)
            {
                _TEMP = DV1.Table.Rows[0]["iTR_TEMP_ID"].ToString();
            }
            double _SR = 1;
            double _Temp_Balance_C, _Temp_Receipt_C, _Temp_Payment_C;
            _Temp_Balance_C = 0; _Temp_Receipt_C = 0; _Temp_Payment_C = 0;
            double _Temp_Balance_B, _Temp_Receipt_B, _Temp_Payment_B;
            _Temp_Balance_B = 0; _Temp_Receipt_B = 0; _Temp_Payment_B = 0;

            XTABLE.Columns.Add("iTR_RECEIPT", Type.GetType("System.Double"));
            XTABLE.Columns.Add("iTR_PAYMENT", Type.GetType("System.Double"));
            XTABLE.Columns.Add("iTR_BALANCE", Type.GetType("System.Double"));
            XTABLE.Columns.Add("iTR_VOUCHER", Type.GetType("System.String"));

            foreach (DataRow XROW in XTABLE.Rows)
            {
                if (XROW["iTR_TEMP_ID"].ToString() == _TEMP)
                {
                    XROW["iTR_REF_NO"] = _SR;
                }
                else
                {
                    _TEMP = XROW["iTR_TEMP_ID"].ToString();
                    _SR = _SR + 1;
                    XROW["iTR_REF_NO"] = _SR;
                }

                if (DisplayType.ToUpper().Trim() == "CASH")
                {
                    if (!Convert.IsDBNull(XROW["iTR_REC_CASH"]))
                    {
                        _Temp_Receipt_C = Convert.ToDouble(XROW["iTR_REC_CASH"]);
                    }
                    else
                    {
                        _Temp_Receipt_C = 0;
                    }
                    if (!Convert.IsDBNull(XROW["iTR_PAY_CASH"]))
                    {
                        _Temp_Payment_C = Convert.ToDouble(XROW["iTR_PAY_CASH"]);
                    }
                    else
                    {
                        _Temp_Payment_C = 0;
                    }
                    if (_Temp_Receipt_C <= 0 & _Temp_Payment_C <= 0)
                    {
                    }
                    else
                    {
                        _Temp_Balance_C = (_Temp_Balance_C + _Temp_Receipt_C) - _Temp_Payment_C;
                        if (_Temp_Receipt_C > 0)
                        {
                            XROW["iTR_RECEIPT"] = _Temp_Receipt_C;
                        }
                        if (_Temp_Payment_C > 0)
                        {
                            XROW["iTR_PAYMENT"] = _Temp_Payment_C;
                        }
                        XROW["iTR_BALANCE"] = _Temp_Balance_C;
                    }
                }
                if (DisplayType.ToUpper().Trim() == "BANK")
                {
                    if (!Convert.IsDBNull(XROW["iTR_REC_BANK"]))
                    {
                        _Temp_Receipt_B = Convert.ToDouble(XROW["iTR_REC_BANK"]);
                    }
                    else
                    {
                        _Temp_Receipt_B = 0;
                    }
                    if (!Convert.IsDBNull(XROW["iTR_PAY_BANK"]))
                    {
                        _Temp_Payment_B = Convert.ToDouble(XROW["iTR_PAY_BANK"]);
                    }
                    else
                    {
                        _Temp_Payment_B = 0;
                    }
                    if (_Temp_Receipt_B <= 0 & _Temp_Payment_B <= 0)
                    {
                    }
                    else
                    {
                        _Temp_Balance_B = (_Temp_Balance_B + _Temp_Receipt_B) - _Temp_Payment_B;
                        if (_Temp_Receipt_B > 0)
                        {
                            XROW["iTR_RECEIPT"] = _Temp_Receipt_B;
                        }
                        if (_Temp_Payment_B > 0)
                        {
                            XROW["iTR_PAYMENT"] = _Temp_Payment_B;
                        }
                        XROW["iTR_BALANCE"] = _Temp_Balance_B;
                    }
                    if (!Convert.IsDBNull(XROW["iTR_SUB_ID"]))
                    {
                        if (XROW["iTR_SUB_ID"].ToString() == _Bank_Acc_ID)
                        {
                            XROW["iTR_PARTY_1"] = XROW["iTR_CR_NAME"];
                        }
                    }
                }
                if (!Convert.IsDBNull(XROW["iTR_CODE"]))
                {
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 1)
                        XROW["iTR_VOUCHER"] = "Cash Deposit / Withdrawn";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 2)
                        XROW["iTR_VOUCHER"] = "Bank to Bank Transfer";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 3)
                        XROW["iTR_VOUCHER"] = "Payment";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 4)
                        XROW["iTR_VOUCHER"] = "Receipt";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 5)
                        XROW["iTR_VOUCHER"] = "Donation - Regular";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 6)
                        XROW["iTR_VOUCHER"] = "Donation - Foreign";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 7)
                        XROW["iTR_VOUCHER"] = "Donation - Gift";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 8)
                        XROW["iTR_VOUCHER"] = "Internal Transfer";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 9)
                        XROW["iTR_VOUCHER"] = "Collection Box";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 10)
                        XROW["iTR_VOUCHER"] = "Fixed Deposits (F.D.)";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 11)
                        XROW["iTR_VOUCHER"] = "Sale Asset";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 12)
                        XROW["iTR_VOUCHER"] = "Membership - New";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 13)
                        XROW["iTR_VOUCHER"] = "Membership - Renewal";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 14)
                        XROW["iTR_VOUCHER"] = "Journal";
                    if (Convert.ToInt32(XROW["iTR_CODE"]) == 15)
                        XROW["iTR_VOUCHER"] = "Asset Transfer";
                }
            }
            return XTABLE;
        }
        public void Get_Cash_Bank_Balance_dx(string _ID, ref double Open_Cash_Bal, ref double Close_Cash_Bal, ref double Open_Bank_Bal, ref double Close_Bank_Bal, string FromDate = "", string ToDate = "")
        {
            Open_Cash_Bal = 0; Close_Cash_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Open_Cash_Bal = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["CLOSING"]))
                {
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                }
                else
                {
                    Close_Cash_Bal = 0;
                }
            }
            else
            {
                Open_Cash_Bal = 0; Close_Cash_Bal = 0;
            }
            Open_Bank_Bal = 0; Close_Bank_Bal = 0;
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
                {
                    if (XROW["ID"].ToString() == _ID)
                    {
                        if (!Convert.IsDBNull(XROW["OPENING"]))
                        {
                            Open_Bank_Bal += Convert.ToDouble(XROW["OPENING"]);
                        }
                        else
                        {
                            Open_Bank_Bal += 0;
                        }
                        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        {
                            Close_Bank_Bal += Convert.ToDouble(XROW["CLOSING"]);
                        }
                        else
                        {
                            Close_Bank_Bal += 0;
                        }
                    }
                }
            }
            else
            {
                Open_Bank_Bal = 0; Close_Bank_Bal = 0;
            }
        }
            
        public ActionResult Frm_Daily_Balances_info_DetailGrid_dx(string MID, bool VouchingMode = false,string RecID="")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(RecID, MID, ClientScreen.Accounts_CashBook, !VouchingMode)), "application/json");
        }
        public ActionResult Frm_Daily_Balances_Info_AdditionalGridData_dx(bool VouchingMode = false, string RecID = "", string MID = null)
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(RecID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook)), "application/json");
        }
        public ActionResult Frm_Export_Options_dx(string GridName= "DailyBalancesListGrid", string title="")
        {
            if (!CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_export_modal','Not Allowed','No Rights');</script>");
            }
            ViewBag.GridName = GridName;
            ViewBag.Filename = title;
            return View("Common_Export");          
        }
        public ActionResult Frm_Bank_Reconcile_dx(string xBankAccID, string xDate, string lblTxnBalance)
        {
            string Reconciletext = "Reconciliation as on " + Convert.ToDateTime(xDate).ToString("dd-MM-yyyy");            
            ViewBag.xDate = xDate;         
            ViewBag.xBankAccID = xBankAccID;         
            ViewBag.lblTxnBalance = lblTxnBalance;
            ViewBag.Reconciletext = Reconciletext;
            ViewData["DailyBalances_ExportRight"] = CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export");
            return View();
        }
        public ActionResult Frm_Bank_Reconcile_Grid_data(string xDate = "", string xBankAccID = "")
        {
            DataTable _Party_Table = BASE._Voucher_DBOps.GetBank_Reconciliation(Convert.ToDateTime(xDate), xBankAccID) as DataTable;
            _Party_Table.CleanColumnName();        
            return Content(JsonConvert.SerializeObject(_Party_Table), "application/json");
        }
        public ActionResult Frm_Export_Options_Reconcile_dx(string GridName = "", string title = "")
        {
            if (!CheckRights(BASE, ClientScreen.Report_Daily_Balances, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_export_modal','Not Allowed','No Rights');</script>");
            }
            ViewBag.GridName = GridName;
            ViewBag.Filename = title;
            return View("Common_Export");
        }              
        public ActionResult Fill_Change_Period_Items_dx(DataSourceLoadOptions loadOptions)
        {
            var period = new List<CB_Period>();
            int index = 0;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                CB_Period row1 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row1.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row1.SelectedIndex = index;
                index++;
                period.Add(row1);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                CB_Period row2 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row2.SelectedIndex = index;
                row2.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                period.Add(row2);
                index++;
            }
            CB_Period row = new CB_Period
            {
                Period = "1st Quarter",
                SelectedIndex = index
            };
            period.Add(row);
            CB_Period row3 = new CB_Period
            {
                Period = "2nd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row3);
            CB_Period row4 = new CB_Period
            {
                Period = "3rd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row4);
            CB_Period row5 = new CB_Period
            {
                Period = "4th Quarter",
                SelectedIndex = ++index
            };
            period.Add(row5);
            CB_Period row6 = new CB_Period
            {
                Period = "1st Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row6);
            CB_Period row7 = new CB_Period
            {
                Period = "2nd Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row7);
            CB_Period row8 = new CB_Period
            {
                Period = "Nine Months",
                SelectedIndex = ++index
            };
            period.Add(row8);
            CB_Period row9 = new CB_Period
            {
                Period = "Financial Year",
                SelectedIndex = ++index
            };
            period.Add(row9);
            CB_Period row10 = new CB_Period
            {
                Period = "Specific Period",
                SelectedIndex = ++index
            };
            period.Add(row10);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(period, loadOptions)), "application/json");
        }
        public ActionResult frm_Voucher_Specific_Period_dx(string xFrDate = "", string xToDate = "")
        {
            DB_SpeceficPeriod model = new DB_SpeceficPeriod();
            model.DB_Fromdate = Convert.ToDateTime(xFrDate);
            model.DB_Todate = Convert.ToDateTime(xToDate);
            return View(model);
        }
        public ActionResult LookUp_Get_Bank_List_dx(DataSourceLoadOptions loadOptions)
        {
            DataTable BA_Table = BASE._Voucher_DBOps.GetBankAccountsList();
            if (BA_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = (Branch_IDs + ("\'" + (xRow["BA_BRANCH_ID"].ToString() + "\',")));
            }
            if ((Branch_IDs.Trim().Length > 0))
            {
                Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            }
            if ((Branch_IDs.Trim().Length == 0))
            {
                Branch_IDs = "\'\'";
            }
            DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);
            if (BB_Table == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            // BUILD DATA

            var BuildData = (from BB in BB_Table.AsEnumerable()
                             join BA in BA_Table.AsEnumerable()
                             on BB["BB_BRANCH_ID"] equals BA["BA_BRANCH_ID"]
                             select new DailyBalance
                             {
                                 BANK_NAME = BB.Field<string>("Name"),
                                 BI_SHORT_NAME = BB.Field<string>("BI_SHORT_NAME"),
                                 BANK_BRANCH = BB.Field<string>("Branch"),
                                 BANK_ACC_NO = BA.Field<string>("BA_ACCOUNT_NO"),
                                 BA_ID = BA.Field<string>("ID"),
                                 BANK_ID = BB.Field<string>("BANK_ID"),
                             }).ToList();

            var Final_Data = BuildData.ToList();
            Final_Data.Sort((x, y) => x.BANK_NAME.CompareTo(y.BANK_NAME));
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Final_Data, loadOptions)), "application/json");
        }
        [HttpPost]
        public ActionResult SaveClick_dx(string updateList)
        {            
            if (string.IsNullOrWhiteSpace(updateList) == false)
            {               
                JArray jArray = JArray.Parse(updateList);
                foreach(JObject item in jArray)                    
                {
                    if ((bool)item["valid"] == true) 
                    {
                        Param_UpdateClearingDate upParam = new Param_UpdateClearingDate();
                        upParam.iRecID = (string)item["iREC_ID"];
                        if (string.IsNullOrWhiteSpace((string)item["Cdate"])==false)
                        {
                            upParam.ClearingDate =Convert.ToDateTime(item["Cdate"]);
                        }
                        upParam.TxnDate = Convert.ToDateTime(item["TxnDate"]);
                        upParam.iRefNo = (string)item["Ref"];
                        upParam.Mode = (string)item["Mode"];
                        upParam.iTrMID = (string)item["iTR_M_ID"];
                        BASE._Reports_Common_DBOps.UpdateClearingDate(upParam);
                    }
                }      
            }
            return Json(new
            {
                result = true,
                message = "Saved Successfully"
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ChequePrinting(string Party,decimal Amount,string VchDate) 
        {
            return View(new Cheque_Printing(Party, Amount, Convert.ToDateTime(VchDate)));
        }
        #endregion
    }
}