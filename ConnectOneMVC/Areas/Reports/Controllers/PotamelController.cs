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
using Common_Lib.RealTimeService;
using ConnectOneMVC.Models;
using ConnectOneMVC.Areas.Reports.Models;
using ConnectOne.D0010._001;

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    public class PotamelController : BaseController
    {
        // GET: Reports/Potamel
        #region Start--> Default Variables
        public bool IsMobile
        {
            get
            {
                return (bool)GetBaseSession("IsMobile_PotaRep");
            }
            set
            {
                SetBaseSession("IsMobile_PotaRep", value);
            }
        }
      
     
        public string _FrDate
        {
            get
            {
                return (string)GetBaseSession("_FrDate_PotaRep");
            }
            set
            {
                SetBaseSession("_FrDate_PotaRep", value);
            }
        }
        public string _ToDate
        {
            get
            {
                return (string)GetBaseSession("_ToDate_PotaRep");
            }
            set
            {
                SetBaseSession("_ToDate_PotaRep", value);
            }
        }

        public DataTable Potamel_ExportData
        {
            get
            {
                return (DataTable)GetBaseSession("Potamel_ExportData_PotaRep");
            }
            set
            {
                SetBaseSession("Potamel_ExportData_PotaRep", value);
            }
        }
        #endregion
       
        public ActionResult Frm_Potamel_Info(string _FrDate, string _ToDate, string PeriodSelection)
        {
            
            //ViewBag.ShowHorizontalBar = 0;
            ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.Contains((ClientScreen.Report_Potamail).ToString()) ? 1 : 0;

            _FrDate = _FrDate.Replace("'", "");
            _ToDate = _ToDate.Replace("'", "");
            DateTime frDate = Convert.ToDateTime(_FrDate.Split('-')[2] + "/" + _FrDate.Split('-')[0] + "/" + _FrDate.Split('-')[1]);
            DateTime toDate = Convert.ToDateTime(_ToDate.Split('-')[2] + "/" + _ToDate.Split('-')[0] + "/" + _ToDate.Split('-')[1]);

            DataSet ds = getTransactionsSummaryData(frDate, toDate);
            DataTable dt_Potamel = ds.Tables[0];

            var Final_Data = new List<Potamel>();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                return Json(new
                {
                    result = false,
                    message = "No Data available for the chosen period",
                    title = "Generation message..."
                }, JsonRequestBehavior.AllowGet);
            }
              
            //    var BuildData = from T in dt_Potamel.AsEnumerable()
            //                    select new Potamel
            //                    {
            //                        Receipts = T["RECEIPTS"].ToString(),
            //                        Amount_Receipts = (Decimal)T["AMOUNT"],
            //                        Total_Receipts = (Decimal)T["TOTAL"],
            //                        Payments = T["PAYMENTS"].ToString(),
            //                        Amount_Payments = (Decimal)T["PAMOUNT"],
            //                        Total_Payments = (Decimal)T["PTOTAL"],
                               
            //};
            
            //Final_Data = BuildData.ToList();
            Potamel_ExportData = dt_Potamel;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["xFr_Date"] = frDate.ToString("MM/dd/yyyy");
            ViewData["xTo_Date"] = toDate.ToString("MM/dd/yyyy");
            ViewData["Potamel_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;

            return View(dt_Potamel);
         
        }

        public ActionResult Frm_Potamel_Receipt_Info_Grid(string Fr_Date, string To_Date, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.VouchingMode = VouchingMode;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (Potamel_ExportData == null || command == "REFRESH")
            {
                DateTime FromDate = new DateTime(Convert.ToInt32(Fr_Date.Split('-')[2]), Convert.ToInt32(Fr_Date.Split('-')[0]), Convert.ToInt32(Fr_Date.Split('-')[1]));
                DateTime ToDate = new DateTime(Convert.ToInt32(To_Date.Split('-')[2]), Convert.ToInt32(To_Date.Split('-')[0]), Convert.ToInt32(To_Date.Split('-')[1]));

                DataSet ds = getTransactionsSummaryData(FromDate, ToDate);
                Potamel_ExportData = ds.Tables[0];
            }

            return PartialView(Potamel_ExportData);
        }

        public ActionResult Frm_Potamel_Payment_Info_Grid(string Fr_Date, string To_Date, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.VouchingMode = VouchingMode;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            if (Potamel_ExportData == null || command == "REFRESH")
            {
                DateTime FromDate = new DateTime(Convert.ToInt32(Fr_Date.Split('-')[2]), Convert.ToInt32(Fr_Date.Split('-')[0]), Convert.ToInt32(Fr_Date.Split('-')[1]));
                DateTime ToDate = new DateTime(Convert.ToInt32(To_Date.Split('-')[2]), Convert.ToInt32(To_Date.Split('-')[0]), Convert.ToInt32(To_Date.Split('-')[1]));

                DataSet ds = getTransactionsSummaryData(FromDate, ToDate);
                Potamel_ExportData = ds.Tables[0];
            }

            return PartialView(Potamel_ExportData);
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
            DataColumn IAmount = new DataColumn("IAmount", Type.GetType("System.Decimal"));
            IAmount.AllowDBNull = true;
            TransactionSummary_Table.Columns.Add(IAmount);
            DataColumn IGroupSum = new DataColumn("IGroupSum", Type.GetType("System.Decimal"));
            IGroupSum.AllowDBNull = true;
            TransactionSummary_Table.Columns.Add(IGroupSum);
            TransactionSummary_Table.Columns.Add("PITEM", Type.GetType("System.String"));
            DataColumn IPAmount = new DataColumn("IPAmount", Type.GetType("System.Decimal"));
            IPAmount.AllowDBNull = true;
            TransactionSummary_Table.Columns.Add(IPAmount);
            DataColumn IPGroupSum = new DataColumn("IPGroupSum", Type.GetType("System.Decimal"));
            IPGroupSum.AllowDBNull = true;
            TransactionSummary_Table.Columns.Add(IPGroupSum);

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
                    ROW["IAmount"] = (double.TryParse(receiptsDt.Rows[i]["IAmount"].ToString(), out testDouble) ? receiptsDt.Rows[i]["IAmount"] :System.DBNull.Value);
                    ROW["IGroupSum"] = (double.TryParse(receiptsDt.Rows[i]["IGroupSum"].ToString(), out testDouble) ? receiptsDt.Rows[i]["IGroupSum"] : System.DBNull.Value);
                }
                else
                {
                    ROW["ITEM"] = "";
                   // ROW["IAmount"] = "";
                   // ROW["IGroupSum"] = "";
                }

                if (((paymentsDt.Rows.Count - 1)
                            > i))
                {
                    ROW["PITEM"] = paymentsDt.Rows[i]["ITEM"].ToString();
                    ROW["IPAmount"] = (double.TryParse(paymentsDt.Rows[i]["IAmount"].ToString(), out testDouble) ? paymentsDt.Rows[i]["IAmount"] : System.DBNull.Value);
                    ROW["IPGroupSum"] = (double.TryParse(paymentsDt.Rows[i]["IGroupSum"].ToString(), out testDouble) ? paymentsDt.Rows[i]["IGroupSum"] : System.DBNull.Value);
                }
                else
                {
                    ROW["PITEM"] = "";
                   // ROW["IPAmount"] = "";
                   // ROW["IPGroupSum"] = "";
                }

                if (!Convert.IsDBNull(ROW["ITEM"]) || !Convert.IsDBNull(ROW["PITEM"]))
                {
                    TransactionSummary_Table.Rows.Add(ROW);
                }

            }

            //// insert a dummy row to distinguish total from others
            //ROW = TransactionSummary_Table.NewRow();
            //TransactionSummary_Table.Rows.Add(ROW);
            ////  add last row of each table
            //ROW = TransactionSummary_Table.NewRow();
            //ROW["ITEM"] = receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["ITEM"].ToString();
            //ROW["IAmount"] = (double.TryParse(receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["IAmount"].ToString(), out testDouble) ? receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["IAmount"] : System.DBNull.Value); 
            //ROW["IGroupSum"] = (double.TryParse(receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["IGroupSum"].ToString(), out testDouble) ? receiptsDt.Rows[(receiptsDt.Rows.Count - 1)]["IGroupSum"] : System.DBNull.Value);
            //ROW["PITEM"] = paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["ITEM"].ToString();
            //ROW["IPAmount"] = (double.TryParse(paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["IAmount"].ToString(), out testDouble) ? paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["IAmount"] : System.DBNull.Value); 
            //ROW["IPGroupSum"] = (double.TryParse(paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["IGroupSum"].ToString(), out testDouble) ? paymentsDt.Rows[(paymentsDt.Rows.Count - 1)]["IGroupSum"] : System.DBNull.Value); 
            //TransactionSummary_Table.Rows.Add(ROW);
            // Replace Headers
            TransactionSummary_Table.Columns["IAmount"].ColumnName = "AMOUNT";
            TransactionSummary_Table.Columns["IGroupSum"].ColumnName = "TOTAL";
            TransactionSummary_Table.Columns["IPAmount"].ColumnName = "PAMOUNT";
            TransactionSummary_Table.Columns["IPGroupSum"].ColumnName = "PTOTAL";
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
                decimal? totalAmount = null;
                decimal? tempAmount= null;
                for (int i = 0; (i <= (dt.Rows.Count - 1)); i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["IGroupSum"].ToString()))
                    {
                        if (totalAmount != null)
                        {
                            tempAmount = totalAmount;
                        }

                       if(dt.Rows[i]["IGroupSum"].ToString().Length>0) totalAmount = Convert.ToDecimal(dt.Rows[i]["IGroupSum"]);
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
                //Bankobj["IAmount"] = "";
                Bankobj["IGroupSum"] = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).OpeningBalance);//.ToString("#0.00")
                CashBankTotalAmt = (CashBankTotalAmt + GetBankDetails(FrDate, ToDate).OpeningBalance);
                dt.Rows.InsertAt(Bankobj, 0);
                DataRow CashRow = dt.NewRow();
                CashRow["ITEM"] = "Opening Cash Balance";
                CashRow["IType"] = "RECEIPTS";
            //CashRow["IAmount"] = "";
                CashRow["IGroupSum"] = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).OpeningBalance);
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
                //Cashobj["IAmount"] = "";
                Cashobj["IGroupSum"] = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).ClosingBalance);
                CashBankTotalAmt = (CashBankTotalAmt + GetCashDetails(FrDate, ToDate).ClosingBalance);
                dt.Rows.InsertAt(Cashobj, dt.Rows.Count);
                DataRow Bankobj = dt.NewRow();
                Bankobj["ITEM"] = "Closing Bank Balance";
                Bankobj["IType"] = "PAYMENTS";
               // Bankobj["IAmount"] = "";
                Bankobj["IGroupSum"] = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).ClosingBalance);
                CashBankTotalAmt = (CashBankTotalAmt + GetBankDetails(FrDate, ToDate).ClosingBalance);
                dt.Rows.InsertAt(Bankobj, dt.Rows.Count);
            }

            DataRow dr1 = dt.NewRow();
            dr1["IGroupSum"] = Convert.ToDecimal(CashBankTotalAmt);
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

        #region export
        public ActionResult Frm_Export_Options()
        {
           
            return PartialView();
        }
        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_PotaRep");
        }
    }
}