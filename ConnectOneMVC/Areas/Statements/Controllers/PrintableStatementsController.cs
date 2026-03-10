using Common_Lib;
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ConnectOne.D0010._001;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Statements.Models;

namespace ConnectOneMVC.Areas.Statements.Controllers
{
    public class PrintableStatementsController : BaseController
    {
        #region Start--> Default Variables
        public bool IsMobile
        {
            get
            {
                return (bool)GetBaseSession("IsMobile_PrintStat");
            }
            set
            {
                SetBaseSession("IsMobile_PrintStat", value);
            }
        }        
        public DateTime? xFr_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xFr_Date_PrintStat");
            }
            set
            {
                SetBaseSession("xFr_Date_PrintStat", value);
            }
        }
        public DateTime? xTo_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xTo_Date_PrintStat");
            }
            set
            {
                SetBaseSession("xTo_Date_PrintStat", value);
            }
        }
        public bool SingleDateSelection
        {
            get { return (bool)GetBaseSession("SingleDateSelection_PrintStat"); }
            set { SetBaseSession("SingleDateSelection_PrintStat", value); }
        }
        public string SelectedPeriodValue
        {
            get
            {
                return (string)GetBaseSession("SelectedPeriodValue_PrintStat");
            }
            set
            {
                SetBaseSession("SelectedPeriodValue_PrintStat", value);
            }
        }
        public string returnPeriod
        {
            get
            {
                return (string)GetBaseSession("returnPeriod_PrintStat");
            }
            set
            {
                SetBaseSession("returnPeriod_PrintStat", value);
            }
        }
        public string Statement_Glob
        {
            get
            {
                return (string)GetBaseSession("Statement_Glob_PrintStat");
            }
            set
            {
                SetBaseSession("Statement_Glob_PrintStat", value);
            }
        }
        public string StatementPath
        {
            get
            {
                return (string)GetBaseSession("StatementPath_PrintStat");
            }
            set
            {
                SetBaseSession("StatementPath_PrintStat", value);
            }
        }
        public string PopupTitle
        {
            get
            {
                return (string)GetBaseSession("PopupTitle_PrintStat");
            }
            set
            {
                SetBaseSession("PopupTitle_PrintStat", value);
            }
        }
        public Common Temp_BASE
        {
            get
            {
                return (Common)GetBaseSession("Temp_BASE_PrintStat");
            }
            set
            {
                SetBaseSession("Temp_BASE_PrintStat", value);
            }
        }
        public DateTime _FrDate
        {
            get
            {
                return (DateTime)GetBaseSession("_FrDate_PrintStat");
            }
            set
            {
                SetBaseSession("_FrDate_PrintStat", value);
            }
        }
        public DateTime _ToDate
        {
            get
            {
                return (DateTime)GetBaseSession("_ToDate_PrintStat");
            }
            set
            {
                SetBaseSession("_ToDate_PrintStat", value);
            }
        }
        public string xFr_Date_Formatting
        {
            get
            {
                return (string)GetBaseSession("xFr_Date_Formatting_PrintStat");
            }
            set
            {
                SetBaseSession("xFr_Date_Formatting_PrintStat", value);
            }
        }
        public string xTo_Date_Formatting
        {
            get
            {
                return (string)GetBaseSession("xTo_Date_Formatting_PrintStat");
            }
            set
            {
                SetBaseSession("xTo_Date_Formatting_PrintStat", value);
            }
        }
        public void SetDefaultValues()
        {
            xFr_Date = null;
            xTo_Date = null;
            SingleDateSelection = false;
        }
        public DateTime _xFr_Date()
        {
            return Convert.ToDateTime(xFr_Date);
        }
        public DateTime _xTo_Date()
        {
            return Convert.ToDateTime(xTo_Date);
        }
        #endregion
        public ActionResult Frm_ReportInput(string ReportType)
        {
            SetDefaultValues();
            if (CheckRights(BASE, ClientScreen.Statement_FD, "List"))//Code written for User Authorization do not remove
            { 
                string Txt_ReportTitle = "";
                string PopupTitle = "";
                string StatementPath = "";
                switch (ReportType)
                {
                    case "Cash_Book":
                        Txt_ReportTitle = "Cash Book";
                        StatementPath = "CashBook";
                        PopupTitle = "Cash Book Statement";
                        break;
                    case "Potamel":
                        Txt_ReportTitle = "Potamel";
                        StatementPath = "Potamel";
                        PopupTitle = "Potamel Statement";
                        break;
                    case "CollectionBox":
                        Txt_ReportTitle = "Collection Box";
                        StatementPath = "CollectionBox";
                        PopupTitle = "Collection Box Statement";
                        break;
                    case "Donation":
                        Txt_ReportTitle = "Donation";
                        StatementPath = "Donation";
                        PopupTitle = "Donation Statement";
                        break;
                    case "FD":
                        Txt_ReportTitle = "FD";
                        StatementPath = "FD";
                        PopupTitle = "FD Statement";
                        break;
                    case "MovableAsset":
                        Txt_ReportTitle = "Movable Asset";
                        StatementPath = "MovableAsset";
                        PopupTitle = "Movable Asset Statement";
                        SingleDateSelection = true;
                        break;
                    case "Property":
                        Txt_ReportTitle = "Property";
                        StatementPath = "Property";
                        PopupTitle = "Property Statement";
                        SingleDateSelection = true;
                        break;
                    case "Vehicle":
                        Txt_ReportTitle = "Vehicle";
                        StatementPath = "Vehicle";
                        PopupTitle = "Vehicle Statement";
                        SingleDateSelection = true;
                        break;
                    case "GoldSilver":
                        Txt_ReportTitle = "Gold Silver";
                        StatementPath = "GoldSilver";
                        PopupTitle = "Gold Silver Statement";
                        SingleDateSelection = true;
                        break;
                    case "PotamelReport":
                        Txt_ReportTitle = "Potamel Report";
                        StatementPath = "PotamelReport";
                        PopupTitle = "Potamel Report";                   
                        break;
                }
                ViewBag.Txt_ReportTitle = Txt_ReportTitle;
                ViewBag.StatementPath = StatementPath;
                ViewBag.PopupTitle = PopupTitle;       
                ViewBag.SingleDateSelection = SingleDateSelection;           
                IsMobile = isMobileBrowser();
                ViewBag.IsMobile = IsMobile;
                return View();
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('StatementPeriodPopup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
        }
        public ActionResult frm_Specific_Period_Static(string Statement)
        {
            IsMobile = isMobileBrowser();
            xFr_Date = BASE._open_Year_Sdt;
            xTo_Date = BASE._open_Year_Edt;
            SelectedPeriodValue = "Financial Year";//xMonth + " - " + xLastDate.Year;
            returnPeriod = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd - MMM, yyyy");
            xFr_Date_Formatting = _xFr_Date().ToString("MM/dd/yyyy");
            ViewBag.xFr_Date_Formatting = xFr_Date_Formatting;
            xTo_Date_Formatting = _xTo_Date().ToString("MM/dd/yyyy");
            ViewBag.xTo_Date_Formatting = xTo_Date_Formatting;
            switch (Statement)
            {
                case "MovableAsset":
                    Statement_Glob = "Movable Asset";
                    StatementPath = "MovableAsset";
                    break;
                case "Property":
                    Statement_Glob = "Property";
                    StatementPath = "Property";
                    break;
                case "Vehicle":
                    Statement_Glob = "Vehicle";
                    StatementPath = "Vehicle";
                    break;
                case "GoldSilver":
                    Statement_Glob = "Gold Silver";
                    StatementPath = "GoldSilver";
                    break;
            }
            ViewBag.Statement_Glob = Statement_Glob;
            ViewBag.StatementPath = StatementPath;
            Temp_BASE = BASE;
            _FrDate = BASE._open_Year_Sdt;
            _ToDate = BASE._open_Year_Edt;
            return View();
        }

        public ActionResult Fill_Ledgers(DataSourceLoadOptions loadOptions)
        {
            //TempData["SelectedPartyValue"] = d1.Rows[0][0].ToString();
            var titledata = BASE._Reports_Ledgers_DBOps.GetLedgerList();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(titledata, loadOptions)), "application/json");
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            DataTable d1 = new DataTable();
            d1.Columns.Add("ID");
            d1.Columns.Add("Name");        
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                d1.Rows.Add(xMonth + "-" + BASE._open_Year_Sdt.Year, xMonth + "-" + BASE._open_Year_Sdt.Year);             
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                d1.Rows.Add(xMonth + "-" + BASE._open_Year_Edt.Year, xMonth + "-" + BASE._open_Year_Edt.Year);
            }     
            d1.Rows.Add("1st Quarter", "1st Quarter");
            d1.Rows.Add("2nd Quarter", "2nd Quarter");
            d1.Rows.Add("3rd Quarter", "3rd Quarter");
            d1.Rows.Add("4th Quarter", "4th Quarter");
            d1.Rows.Add("1st Half Yearly", "1st Half Yearly");
            d1.Rows.Add("2nd Half Yearly", "2nd Half Yearly");
            d1.Rows.Add("Nine Months", "Nine Months");
            d1.Rows.Add("Financial Year", "Financial Year");
            d1.Rows.Add("Specific Period", "Specific Period");    
            DataView DV1 = new DataView(d1);
            var titledata = DatatableToModel.DataTabletoTitle_INFO(DV1.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(titledata, loadOptions)), "application/json");
        }
        public ActionResult GetSelectPeriod(string DateSelected)
        {
            if (DateSelected == "1st Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
            }
            else if (DateSelected == "2nd Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = _xFr_Date().AddMonths(+3).AddDays(-1);
            }
            else if (DateSelected == "3rd Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = _xFr_Date().AddMonths(+3).AddDays(-1);
            }
            else if (DateSelected == "4th Quarter")
            {
                xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = _xFr_Date().AddMonths(+3).AddDays(-1);
            }
            else if (DateSelected == "1st Half Yearly")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = _xFr_Date().AddMonths(+6).AddDays(-1);
            }
            else if (DateSelected == "2nd Half Yearly")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = _xFr_Date().AddMonths(+6).AddDays(-1);
            }
            else if (DateSelected == "Nine Months")
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = _xFr_Date().AddMonths(+9).AddDays(-1);
            }
            else if (DateSelected == "Financial Year")
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
            }
            else
            {
                string Sel_Mon = DateSelected.Substring(0, 3).ToUpper();
                int SEL_MM = Sel_Mon == "JAN" ? 1 : Sel_Mon == "FEB" ? 2 : Sel_Mon == "MAR" ? 3 : Sel_Mon == "APR" ? 4 : Sel_Mon == "MAY" ? 5 : Sel_Mon == "JUN" ? 6 : Sel_Mon == "JUL" ? 7 : Sel_Mon == "AUG" ? 8 : Sel_Mon == "SEP" ? 9 : Sel_Mon == "OCT" ? 10 : Sel_Mon == "NOV" ? 11 : Sel_Mon == "DEC" ? 12 : 4;
                xFr_Date = new DateTime(Convert.ToInt32(DateSelected.Substring(4, 4)), SEL_MM, 1);
                xTo_Date = _xFr_Date().AddMonths(1).AddDays(-1);
            }
            string returnPeriod = "Period : Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");
            return Json(new
            {
                returnPeriod,
                FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                ToDate = _xTo_Date().ToString("MM/dd/yyyy")
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult frm_Voucher_Specific_Period()
        {
            Smt_Period model = new Smt_Period();
            model.Smt_Fromdate = xFr_Date==null?(DateTime?)null: _xFr_Date();
            model.Smt_Todate = xTo_Date==null?(DateTime?)null:_xTo_Date();
            ViewBag.SingleDateSelection = SingleDateSelection;
            if (SingleDateSelection)
            {
                model.Smt_Fromdate = BASE._open_Year_Sdt;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult GetSpecificPeriod(Smt_Period model)
        {
            if (IsDate(model.Smt_Fromdate.ToString()) == false)
            {
                return Json(new
                {
                    result = false,
                    message = "From Date Incorrect/Blank..!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (IsDate(model.Smt_Todate.ToString()) == false)
            {
                return Json(new
                {
                    result = false,
                    message = "To Date Incorrect/Blank..!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Smt_Fromdate < BASE._open_Year_Sdt || model.Smt_Fromdate > BASE._open_Year_Edt)
            {
                return Json(new
                {
                    result = false,
                    message = "From Date Not As Per Financial Year..!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Smt_Todate < BASE._open_Year_Sdt || model.Smt_Todate > BASE._open_Year_Edt)
            {
                return Json(new
                {
                    result = false,
                    message = "To Date Not As Per Financial Year..!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.Smt_Todate >= model.Smt_Fromdate)
            {
                xFr_Date = model.Smt_Fromdate;
                xTo_Date = model.Smt_Todate;
                string returnPeriod = "Period : Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");
                return Json(new
                {
                    result = true,
                    returnPeriod,
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
        public ActionResult CashBook(string _FrDate, string _ToDate)
        {
            ViewBag.IsMobile = IsMobile;
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            return View(new Report_CashBook(BASE, frDate, toDate));
        }
        public ActionResult CollectionBox(string _FrDate, string _ToDate, string SelectedViewType)
        {
            ViewBag.IsMobile = IsMobile;
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);

            GeneralReport_Portrait report = new GeneralReport_Portrait(BASE);
            //xPleaseWait.Show("G e n e r a t i n g   C o l l e c t i o n   B o x   R e p o r t");
            report.ReportType = "Collection Box Statement";
            DataTable dt = new DataTable();

            DataTable TxnList = BASE._Reports_Common_DBOps.GetCollectionBoxTransactionList(frDate, toDate, Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box);
            TxnList.Columns["Amt"].ColumnName = "Amount";
            TxnList.Columns["tr_date"].ColumnName = "Date";
            TxnList.Columns.Remove("TR_AB_ID_1");
            TxnList.Columns.Remove("TR_AB_ID_2");
            if ((TxnList.Rows.Count > 0))
            {
                dt = TxnList;
                // add total row
                DataRow dr = dt.NewRow();
                dr["Second Surrendered Person Name"] = "Total";
                dr["Amount"] = dt.Compute("Sum(Amount)", "");
                dt.Rows.Add(dr);
            }
            else
            {
                return Json(new
                {
                    result=false,
                    message= "No Data available for the chosen period",
                    title= "Generation message..."
                }, JsonRequestBehavior.AllowGet);//redmine Bug #132725 fixed
            }

            // dt = ReplaceTableHeaders(dt);
            //if ((dt.Rows.Count > 0))
            //{
            //    ds.Tables.Add(dt);
            //    report.DataSource = ds;
            //}
            //else
            //{ ***Handle this
            //    DevExpress.XtraEditors.XtraMessageBox.Show("No Data available for the choosen period", "Generation message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    xPleaseWait.Hide();
            //    return;
            //}
            DataSet ds = new DataSet("table");
            ds.Tables.Add(dt);
            report.DataSource = ds;
            report.DisplayName = report.ReportType;
            report.DesignerOptions.ShowDesignerHints = true;
            report.InitBands();
            report.PerpareHeaderAndFooter(SelectedViewType, frDate, toDate);
            // Build report 
            report.InitCollectionBox(ds);
            return View(report);
        }
        public ActionResult Donation(string _FrDate, string _ToDate)
        {
            ViewBag.IsMobile = IsMobile;
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            return View(new Donation_Gifts_Report(frDate, toDate, BASE));
        }
        public ActionResult Potamel(string _FrDate, string _ToDate, string PeriodSelection)
        {
            ViewBag.IsMobile = IsMobile;
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);

            Report_Potamel report = new Report_Potamel(BASE);
            report.ReportType = "Transaction Summary (Potamel)";
            DataSet ds = getTransactionsSummaryData(frDate, toDate);
            //if(ds == null)
            //    {
            //    BASE.HandleDBError_OnNothingReturned();
            //        //xPleaseWait.Hide()
            //        //Exit Sub
            //    }

            if (ds.Tables[0].Rows.Count > 0)
            {
                report.DataSource = ds;
            }
            else
            {
                return Json(new
                {
                    result = false,
                    message = "No Data available for the chosen period",
                    title = "Generation message..."
                }, JsonRequestBehavior.AllowGet);
               
            }
            report.DisplayName = report.ReportType;
            report.DesignerOptions.ShowDesignerHints = true;
            report.InitBands();
            report.PerpareHeaderAndFooter(PeriodSelection, frDate, toDate); ;
            //'Build report 
            report.InitTransactionsSummary(ds);
            //Dim printTool As New ReportPrintTool(report)
            //printTool.Report.CreateDocument(False)
            //'printTool.Report.UpdatePageSettings(
            //AddHandler printTool.PreviewForm.Load, AddressOf PreviewForm_Load
            //printTool.ShowPreviewDialog()

            return View(report);
        }
        public ActionResult Property(string _FrDate, string _ToDate)
        {
            ViewBag.IsMobile = IsMobile;
            //_FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            //DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            LB_Report lb = new LB_Report();
            lb.MainBase = BASE;
            lb.OnDate = toDate;
            return View(lb);
        }
        public ActionResult MovableAsset(string _FrDate, string _ToDate)
        {
            ViewBag.IsMobile = IsMobile;
            //_FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            //DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            Asset_Report _asset = new Asset_Report();
            _asset.MainBase = BASE;
            _asset.OnDate = toDate;
            return View(_asset);
        }
        public ActionResult Vehicle(string _FrDate, string _ToDate)
        {
            ViewBag.IsMobile = IsMobile;
            //_FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            //DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            Vehicle_Report _vehicle = new Vehicle_Report();
            _vehicle.MainBase = BASE;
            _vehicle.OnDate = toDate;
          
            return View(_vehicle);
        }
        public ActionResult GoldSilver(string _FrDate, string _ToDate)
        {
            ViewBag.IsMobile = IsMobile;
            //_FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            //DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            GS_Report _gs = new GS_Report();
            _gs.MainBase = BASE;
            _gs.OnDate = toDate;
            return View(_gs);
        }
        public ActionResult FD(string _FrDate, string _ToDate)
        {
            ViewBag.IsMobile = IsMobile;
            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);
            FD_Report _fd = new FD_Report();
            _fd.MainBase = BASE;
            _fd.Start_Date = frDate;
            _fd.End_Date = toDate;
            return View(_fd);
        }
        public JsonResult Check_Restrictions(string Msg)
        {
            var User_Type = BASE._open_User_Type.ToUpper();
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (BASE._IsVolumeCenter)
            {
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            if (BASE.Allow_Statements_Without_Restrictions)
            {
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            double Opening_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(BASE._open_Year_Sdt, BASE._open_Year_Edt, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.result = false;               
                jsonParam.title = "Error!!";
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Opening_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Opening_Bal = 0;
                }
            }
            else
            {
                Opening_Bal = 0;
            }

            DataTable TR_Table = (DataTable)BASE._Voucher_DBOps.GetNegativeBalance("00080", "", "");
            if (TR_Table == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.result = false;             
                jsonParam.title = "Error!!";
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
            {
                DataTable dTABLE = BASE._Voucher_DBOps.GetBankEntriesCountInNextEvent();
                DateTime NextYearStartDate = BASE._open_Year_Edt.AddDays(1);
                if (dTABLE.Rows.Count > 0)
                {
                    jsonParam.message = "There is no entry in saving bank A/c after " + NextYearStartDate.ToString("dd MMM yyyy") +". So please deposit cash in bank or withdraw from bank,update your bank Passbook and enter in CONNECTONE after " + NextYearStartDate.ToString("dd MMM yyyy") + " in the financial year " + BASE._next_Unaudited_YearID + "!!<br>==========================================<brAcc. No.       : " + dTABLE.Rows[0]["BA_ACCOUNT_NO"].ToString() +"<br>Bank             : " + dTABLE.Rows[0]["BI_SHORT_NAME"].ToString()+"<br>==========================================<br>Please post atleast one entry in this bank in year " + BASE._next_Unaudited_YearID + " to print statement.";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                        User_Type
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            DataTable Bank_Entry = (DataTable)BASE._Voucher_DBOps.GetNegativeBalance("00080", "", "");

            // (1.3) Cash Opening Balance Insert..........
            DataRow ROW;
            ROW = TR_Table.NewRow();
            ROW["iTR_SORT_REC"] = "A";
            ROW["iTR_DATE"] = BASE._open_Year_Sdt.ToString(BASE._Server_Date_Format_Short);
            ROW["iTR_REC_CASH"] = Opening_Bal;
            ROW["iTR_PAY_CASH"] = 0;
            TR_Table.Rows.Add(ROW);

            // (1.4) Cash Data Sorting
            DataView DV1 = new DataView(TR_Table);
            DV1.Sort = "iTR_DATE,iTR_SORT_REC";
            DataTable XTABLE = DV1.ToTable();
            double _Temp_Balance = 0;
            double _Temp_Receipt = 0;
            double _Temp_Payment = 0;

            // (1.5) Check Negative Cash
            foreach (DataRow XROW in XTABLE.Rows)
            {
                if (!Convert.IsDBNull(XROW["iTR_REC_CASH"]))
                {
                    _Temp_Receipt = Convert.ToDouble(XROW["iTR_REC_CASH"]);
                }
                else
                {
                    _Temp_Receipt = 0;
                }

                if (!Convert.IsDBNull(XROW["iTR_PAY_CASH"]))
                {
                    _Temp_Payment = Convert.ToDouble(XROW["iTR_PAY_CASH"]);
                }
                else
                {
                    _Temp_Payment = 0;
                }

                if (_Temp_Receipt <= 0 && _Temp_Payment <= 0)
                {
                }
                else
                {
                    _Temp_Balance = Math.Round((_Temp_Balance + _Temp_Receipt) - _Temp_Payment, 2);
                }
                if (_Temp_Balance < 0)
                {
                    jsonParam.message = "Print out cannot be taken as Cash Balance is NEGATIVE.<br>==========================================<br>Date      : " + Convert.ToDateTime(XROW["iTR_DATE"]).ToString("dd-MMM, yyyy")+"<br>Amount : "+ _Temp_Balance.ToString("#,0.##")+ "<br>==========================================<br>For more details: Check Daily Balances Report";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam,
                        User_Type
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            // Centre Remarks Count 
            if ((int)BASE._Action_Items_DBOps.GetPendingCentreRemarkCount(ClientScreen.Home_StartUp) > 0)
            {
                jsonParam.message = "Some of the Actions posted by Auditors in Madhuban require your attention!!<br><br>Please click Help -> Audit Actions.<br>Select an Audit Action and Click on Centre Remarks.<br>Enter your remarks and Save, Repeat this for all audit actions which are not closed,Then prints can be taken..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }

            // Asset Tf
            if (BASE._AssetTransfer_DBOps.GetUnmatchedCount_AssetTransfer() > 0)
            {
                jsonParam.message = "Some of the Asset Transfer Entries with other centres/HQ are not matched yet !!<br>Match all the pending asset transfer entries. Then prints can be taken.";
                jsonParam.result = false;         
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            // Internal Tf
            if (BASE._Internal_Tf_Voucher_DBOps.GetCenterToCentreUnmatchedCount() > 0)
            {
                jsonParam.message = "Some of the Transfer Entries with other centres are not matched yet !!<br>Please refer Accounts-> Internal Transfer Register<br>Prints can be taken if 1) All Transfers with centers 2) TDS Transfers with H.Q. 3)Non-Cash Transfers with H.Q. are matched.";
                jsonParam.result = false;
                if (BASE._open_Ins_ID != "00001")
                {
                    jsonParam.message = "Some of the Transfer Entries with other centres are not matched yet !!<br>Please refer Accounts->Internal Transfer Register for unmatched Entries.";
                }
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }
            string DynamicRestriction = "";
            DbOperations.SharedVariables Dbops = new DbOperations.SharedVariables(BASE);
            DynamicRestriction = Dbops.GetDynaicClientRestriction();
            if (DynamicRestriction.Length > 0)
            {
                jsonParam.message = DynamicRestriction;
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam,
                    User_Type
                }, JsonRequestBehavior.AllowGet);
            }

            jsonParam.result = true;
            return Json(new
            {
                jsonParam,
                User_Type
            }, JsonRequestBehavior.AllowGet);
        }
        public bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }

        #region "Private Functions"
        private DataSet getTransactionsSummaryData(DateTime Fr_Date, DateTime To_Date)
        {
            DataSet returnDataset = new DataSet();
            DataTable receiptsDt = new DataTable();
            DataTable paymentsDt = new DataTable();
            receiptsDt = getTransactionTable(true, Fr_Date, To_Date);
            //  Gets datatable for receipts
            paymentsDt = getTransactionTable(false, Fr_Date, To_Date);
            //  Gets datatable for Payments
            DataTable TransactionSummary_Table = new DataTable();
            TransactionSummary_Table.Columns.Add("ITEM", Type.GetType("System.String"));
            TransactionSummary_Table.Columns.Add("IAmount", Type.GetType("System.String"));
            TransactionSummary_Table.Columns.Add("IGroupSum", Type.GetType("System.String"));
            TransactionSummary_Table.Columns.Add("PITEM", Type.GetType("System.String"));
            TransactionSummary_Table.Columns.Add("IPAmount", Type.GetType("System.String"));
            TransactionSummary_Table.Columns.Add("IPGroupSum", Type.GetType("System.String"));

            DataRow ROW;
            // With...
            int totalRows = ((receiptsDt.Rows.Count > paymentsDt.Rows.Count) ? receiptsDt.Rows.Count : paymentsDt.Rows.Count);
            double testDouble = 0;
            for (int i = 0; (i <= (totalRows - 2)); i++)
            {
                ROW = TransactionSummary_Table.NewRow();
                if (((receiptsDt.Rows.Count - 1) > i))
                {
                    ROW["ITEM"] = receiptsDt.Rows[i]["ITEM"].ToString();
                    ROW["IAmount"] = (double.TryParse(receiptsDt.Rows[i]["IAmount"].ToString(), out testDouble) ? receiptsDt.Rows[i]["IAmount"] : "");
                    ROW["IGroupSum"] = (double.TryParse(receiptsDt.Rows[i]["IGroupSum"].ToString(), out testDouble) ? receiptsDt.Rows[i]["IGroupSum"] : "");
                }
                else
                {
                    ROW["ITEM"] = "";
                    ROW["IAmount"] = "";
                    ROW["IGroupSum"] = "";
                }

                if (((paymentsDt.Rows.Count - 1)
                            > i))
                {
                    ROW["PITEM"] = paymentsDt.Rows[i]["ITEM"];
                    ROW["IPAmount"] = (double.TryParse(paymentsDt.Rows[i]["IAmount"].ToString(), out testDouble) ? paymentsDt.Rows[i]["IAmount"] : "");
                    ROW["IPGroupSum"] = (double.TryParse(paymentsDt.Rows[i]["IGroupSum"].ToString(), out testDouble) ? paymentsDt.Rows[i]["IGroupSum"] : "");
                }
                else
                {
                    ROW["PITEM"] = "";
                    ROW["IPAmount"] = "";
                    ROW["IPGroupSum"] = "";
                }

                if (!Convert.IsDBNull(ROW["ITEM"]) || !Convert.IsDBNull(ROW["PITEM"]))
                {
                    TransactionSummary_Table.Rows.Add(ROW);
                }

            }

            // insert a dummy row to distinguish total from others
            ROW = TransactionSummary_Table.NewRow();
            TransactionSummary_Table.Rows.Add(ROW);
            //  add last row of each table
            ROW = TransactionSummary_Table.NewRow();
            ROW["ITEM"] = receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["ITEM"].ToString();
            ROW["IAmount"] = receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["IAmount"].ToString();
            ROW["IGroupSum"] = receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["IGroupSum"].ToString();
            ROW["PITEM"] = paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["ITEM"].ToString();
            ROW["IPAmount"] = paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["IAmount"].ToString();
            ROW["IPGroupSum"] = paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["IGroupSum"].ToString();
            TransactionSummary_Table.Rows.Add(ROW);
            // Replace Headers
            TransactionSummary_Table.Columns["IAmount"].ColumnName = "AMOUNT";
            TransactionSummary_Table.Columns["IGroupSum"].ColumnName = "TOTAL";
            TransactionSummary_Table.Columns["IPAmount"].ColumnName = " AMOUNT";
            TransactionSummary_Table.Columns["IPGroupSum"].ColumnName = " TOTAL";
            TransactionSummary_Table.Columns["ITEM"].ColumnName = "RECEIPTS";
            TransactionSummary_Table.Columns["PITEM"].ColumnName = "PAYMENTS";
            returnDataset.Tables.Add(TransactionSummary_Table);
            return returnDataset;
        }

        private DataTable getTransactionTable(bool IsReceipt, DateTime FrDate, DateTime ToDate)
        {
            DataTable dt = BASE._Reports_Common_DBOps.GetTSummaryList(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, IsReceipt, FrDate, ToDate);

            decimal CashBankTotalAmt = 0;
            if ((dt.Rows.Count > 0))
            {
                CashBankTotalAmt = Convert.ToDecimal(dt.Rows[0]["IGroupSum"].ToString());
                // remove 1st row from the table-- this represents the total sum. This will be added as last row
                dt.Rows.RemoveAt(0);
                // make GroupSum display adjustment. 
                // Table obtained from the database will have GroupSum displayed corresponding to its groupname
                // Hence a small adjustment is made to show GroupSum at the end of subgroups
                string totalAmount = "";
                string tempAmount = "";
                for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["IGroupSum"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(totalAmount))
                        {
                            tempAmount = totalAmount;
                        }

                        totalAmount = dt.Rows[i]["IGroupSum"].ToString();
                        dt.Rows[i]["IGroupSum"] = "";
                    }

                    if ((string.IsNullOrEmpty(dt.Rows[i]["IAmount"].ToString())
                                && (i > 0)))
                    {
                        dt.Rows[(i - 1)]["IGroupSum"] = tempAmount;
                    }

                }

                dt.Rows[(dt.Rows.Count - 1)]["IGroupSum"] = totalAmount;
                dt.AcceptChanges();
                DataTable nTable = dt.Clone();
                foreach (DataRow dRow in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dRow["ITEM"].ToString()))
                    {
                        DataRow newRow = nTable.NewRow();
                        newRow.ItemArray = dRow.ItemArray;
                        nTable.Rows.Add(newRow);
                    }

                }

                dt = nTable;
            }

            // add Opening and closing balances for cash and bank
            // Opening Balances in Receipts table
            if (IsReceipt)
            {
                // insert a dummy row after opening balances
                DataRow dtReceiptsRow = dt.NewRow();
                dt.Rows.InsertAt(dtReceiptsRow, 0);
                DataRow Bankobj = dt.NewRow();
                Bankobj["Item"] = "Opening Bank Balance";
                Bankobj["IType"] = "RECEIPTS";
                Bankobj["IAmount"] = "";
                Bankobj["IGroupSum"] = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).OpeningBalance).ToString("#0.00");
                CashBankTotalAmt = (CashBankTotalAmt + GetBankDetails(FrDate, ToDate).OpeningBalance);
                dt.Rows.InsertAt(Bankobj, 0);
                DataRow CashRow = dt.NewRow();
                CashRow["ITEM"] = "Opening Cash Balance";
                CashRow["IType"] = "RECEIPTS";
                CashRow["IAmount"] = "";
                CashRow["IGroupSum"] = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).OpeningBalance).ToString("#0.00");
                CashBankTotalAmt = (CashBankTotalAmt + GetCashDetails(FrDate, ToDate).OpeningBalance);
                dt.Rows.InsertAt(CashRow, 0);
            }
            else
            {
                // insert a dummy row @ first
                DataRow dtPaymentRow = dt.NewRow();
                dt.Rows.InsertAt(dtPaymentRow, dt.Rows.Count);
                DataRow Cashobj = dt.NewRow();
                Cashobj["Item"] = "Closing Cash Balance";
                Cashobj["IType"] = "PAYMENTS";
                Cashobj["IAmount"] = "";
                Cashobj["IGroupSum"] = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).ClosingBalance).ToString("#0.00");
                CashBankTotalAmt = (CashBankTotalAmt + GetCashDetails(FrDate, ToDate).ClosingBalance);
                dt.Rows.InsertAt(Cashobj, dt.Rows.Count);
                DataRow Bankobj = dt.NewRow();
                Bankobj["ITEM"] = "Closing Bank Balance";
                Bankobj["IType"] = "PAYMENTS";
                Bankobj["IAmount"] = "";
                Bankobj["IGroupSum"] = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).ClosingBalance).ToString("#0.00");
                CashBankTotalAmt = (CashBankTotalAmt + GetBankDetails(FrDate, ToDate).ClosingBalance);
                dt.Rows.InsertAt(Bankobj, dt.Rows.Count);
            }

            DataRow dr1 = dt.NewRow();
            dr1["IGroupSum"] = Convert.ToDecimal(CashBankTotalAmt).ToString("#0.00");
            dt.Rows.Add(dr1);
            // add row that corresponds to total 
            dt.AcceptChanges();
            return dt;
        }

        private ReportDataObjects.BalanceDetails GetCashDetails(DateTime FromDate, DateTime ToDate)
        {
            Decimal Open_Cash = 0;
            Decimal R_CASH = 0;
            Decimal P_CASH = 0;
            Decimal Close_Cash = 0;
            DataTable dt = BASE._Voucher_DBOps.GetCashBalanceSummary(FromDate, ToDate, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            // Dim dt As DataTable = Base._Reports_Common_DBOps.GetCashOpeningBalance(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)

            if ((dt.Rows.Count > 0))
            {
                if (!Convert.IsDBNull(dt.Rows[0]["OPENING"]))
                {
                    Open_Cash = (Decimal)dt.Rows[0]["OPENING"];
                }
                else
                {
                    Open_Cash = 0;
                }

                if (!Convert.IsDBNull(dt.Rows[0]["CLOSING"]))
                {
                    Close_Cash = (Decimal)dt.Rows[0]["CLOSING"];
                }
                else
                {
                    Close_Cash = 0;
                }

                if (!Convert.IsDBNull(dt.Rows[0]["RECEIPT"]))
                {
                    R_CASH = (Decimal)dt.Rows[0]["RECEIPT"];
                }
                else
                {
                    R_CASH = 0;
                }

                if (!Convert.IsDBNull(dt.Rows[0]["PAYMENT"]))
                {
                    P_CASH = (Decimal)dt.Rows[0]["PAYMENT"];
                }
                else
                {
                    P_CASH = 0;
                }

            }
            else
            {
                Open_Cash = 0;
            }

            ReportDataObjects.BalanceDetails cashDetails = new ReportDataObjects.BalanceDetails();
            cashDetails.OpeningBalance = Open_Cash;
            cashDetails.Receipt = R_CASH;
            cashDetails.Payment = P_CASH;
            cashDetails.ClosingBalance = Close_Cash;
            return cashDetails;
        }

        private ReportDataObjects.BalanceDetails GetBankDetails(DateTime FromDate, DateTime ToDate)
        {
            //DataTable BA_Table = BASE._Reports_Common_DBOps.GetSavingBankAccounts(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary);

            //string Branch_IDs = "";
            //foreach (DataRow xRow in BA_Table.Rows)
            //{
            //    Branch_IDs = (Branch_IDs + ("\'" + (xRow["BA_BRANCH_ID"].ToString() + "\',")));
            //}

            //if ((Branch_IDs.Trim().Length > 0))
            //{
            //    Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            //}

            //if ((Branch_IDs.Trim().Length == 0))
            //{
            //    Branch_IDs = "\'\'";
            //}

            //DataTable BB_Table = BASE._Reports_Common_DBOps.GetBranches(Branch_IDs, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary);

            DataTable OP_Table = BASE._Voucher_DBOps.GetBankBalanceSummary(FromDate, ToDate, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            // Dim OP_Table As DataTable = Base._Reports_Common_DBOps.GetOpBalance(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
            //if ((OP_Table == null))
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return null;
            //}

            // BUILD DATA'
            //        var BuildData = From B In BB_Table, A In BA_Table, O In OP_Table _
            //                    Where(B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID")) _
            //                    And(A.Field(Of String)("ID") = O.Field(Of String)("ID")) _
            //                   Select New With {
            //            _
            //          .NAME = B.Field(Of String)("BI_SHORT_NAME"), _
            //          .BA_ACCOUNT_NO = A.Field(Of String)("BA_ACCOUNT_NO"), _
            //          .OP_AMOUNT = O.Field(Of Decimal)("OPENING"), _
            //          .CLOSE_AMOUNT = O.Field(Of Decimal)("CLOSING"), _
            //          .REC_AMOUNT = O.Field(Of Decimal)("RECEIPT"), _
            //          .PAY_AMOUNT = O.Field(Of Decimal)("PAYMENT"), _
            //          .ID = A.Field(Of String)("ID")
            //                                    } 

            //object Final_Data = BuildData.ToList();
            int XCNT = 2;
            Decimal XOpen_Bank = 0;
            Decimal XR_BANK = 0;
            Decimal XP_BANK = 0;
            Decimal XClose_Bank = 0;


            foreach (DataRow dRow in OP_Table.Rows)
            {
                XOpen_Bank = (XOpen_Bank + Convert.ToDecimal(dRow["OPENING"]));
                XClose_Bank = (XClose_Bank + Convert.ToDecimal(dRow["CLOSING"]));
                XR_BANK = (XR_BANK + Convert.ToDecimal(dRow["RECEIPT"]));
                XP_BANK = (XP_BANK + Convert.ToDecimal(dRow["PAYMENT"]));
            }


            ReportDataObjects.BalanceDetails BankDetails = new ReportDataObjects.BalanceDetails();
            BankDetails.OpeningBalance = XOpen_Bank;
            BankDetails.Receipt = XR_BANK;
            BankDetails.Payment = XP_BANK;
            BankDetails.ClosingBalance = XClose_Bank;
            return BankDetails;
        }

        private bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = System.Web.HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "android","midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "NEC", "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }



        #endregion
    }
}