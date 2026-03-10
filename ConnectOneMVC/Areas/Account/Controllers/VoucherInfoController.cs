using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class VoucherInfoController : BaseController
    {
        public DateTime? xFr_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xFr_Date_VoucherInfo");
            }
            set
            {
                SetBaseSession("xFr_Date_VoucherInfo", value);
            }
        }
        public DateTime? xTo_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xTo_Date_VoucherInfo");
            }
            set
            {
                SetBaseSession("xTo_Date_VoucherInfo", value);
            }
        }
        public double Open_Bank_Bal
        {
            get
            {
                return (double)GetBaseSession("Open_Bank_Bal_VoucherInfo");
            }
            set
            {
                SetBaseSession("Open_Bank_Bal_VoucherInfo", value);
            }
        }
        public double Close_Bank_Bal
        {
            get
            {
                return (double)GetBaseSession("Close_Bank_Bal_VoucherInfo");
            }
            set
            {
                SetBaseSession("Close_Bank_Bal_VoucherInfo", value);
            }
        }
        public List<CB_Period> PeriodSelectionData
        {
            get { return (List<CB_Period>)GetBaseSession("PeriodSelectionData_VoucherInfo"); }
            set { SetBaseSession("PeriodSelectionData_VoucherInfo", value); }
        }
        public List<Summary> Voucher_SummaryGridData
        {
            get { return (List<Summary>)GetBaseSession("Voucher_SummaryGridData_VoucherInfo"); }
            set { SetBaseSession("Voucher_SummaryGridData_VoucherInfo", value); }
        }
        public ActionResult Frm_Voucher_Info()
        {
         
            Add_User_Rights();
            object MaxValue = 0;
            DateTime xLastDate = DateTime.Now;
            MaxValue = BASE._Voucher_DBOps.GetMaxTransactionDate();
            if (Convert.IsDBNull(MaxValue))
            {
                xLastDate = BASE._open_Year_Sdt;
            }
            else
            {
                xLastDate = Convert.ToDateTime(MaxValue);
            }
            FillChangePeriod();
            int xMM = xLastDate.Month;
            ViewBag.IsVolumeCenter = BASE._IsVolumeCenter;
            ViewBag.AllowForeignDonation = BASE.Allow_Foreign_Donation;
            ViewBag.PeriodSelectedIndex = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            ViewBag.self_posted = BASE._open_User_Self_Posted;
            return View();
        
        }
        public ActionResult Grid_Display_VoucherInfo()
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
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
                Open_Cash_Bal = 0;
                Close_Cash_Bal = 0;
            }
            Open_Bank_Bal = 0;
            Close_Bank_Bal = 0;
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
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
            else
            {
                Open_Bank_Bal = 0;
                Close_Bank_Bal = 0;
            }
            string BE_Cash_Bank_Text = "Cash: " + Close_Cash_Bal.ToString("#,0.00") + "  Bank: " + Close_Bank_Bal.ToString("#,0.00");
            jsonParam.message = "";
            jsonParam.title = "";
            jsonParam.result = true;
            jsonParam.closeform = false;
            return Json(new
            {
                jsonParam,
                BE_Cash_Bank_Text
            }, JsonRequestBehavior.AllowGet);
        }
        public void FillChangePeriod()
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
            PeriodSelectionData = period;
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            if (PeriodSelectionData == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<CB_Period>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PeriodSelectionData, loadOptions)), "application/json");
        }
        public ActionResult Cmb_View_SelectedIndexChanged(int? SelectedIndex = null)
        {
            var Perioddata = PeriodSelectionData;
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
        public ActionResult frm_Specific_Period_voucherInfo()
        {
            Voucher_SpeceficPeriod model = new Voucher_SpeceficPeriod();
            model.Voucher_Fromdate = _xFr_Date();
            model.Voucher_Todate = _xTo_Date();
            return View(model);
        }
        [HttpPost]
        public ActionResult GetSpecificPeriod_VoucherInfo(Voucher_SpeceficPeriod model)
        {
            if (model.Voucher_Todate >= model.Voucher_Fromdate)
            {
                xFr_Date = model.Voucher_Fromdate;
                xTo_Date = model.Voucher_Todate;
                string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd - MMM, yyyy");
                return Json(new
                {
                    result = true,
                    BE_View_Period,
                    FromDate = _xFr_Date().ToString(),
                    ToDate = _xTo_Date().ToString()
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
        public ActionResult Frm_View_Summary(string PopupID = "")
        {
            ViewBag.PopupID = PopupID.Length > 0 ? PopupID : "popup_frm_Frm_View_Summary";
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            double Open_Bank_Bal = 0;
            double Close_Bank_Bal = 0;
            double R_CASH = 0;
            double R_BANK = 0;
            double P_CASH = 0;
            double P_BANK = 0;

            ViewBag.SummaryPeriod = "Period Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");

            DataSet CashBank_DS = new DataSet();
            DataTable CashBank_Table = CashBank_DS.Tables.Add("Table");
            DataRow ROW = default(DataRow);
            var _with1 = CashBank_Table;
            _with1.Columns.Add("Title", Type.GetType("System.String"));
            _with1.Columns.Add("Sr", Type.GetType("System.Double"));
            _with1.Columns.Add("Description", Type.GetType("System.String"));
            _with1.Columns.Add("O_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["O_BALANCE"].Caption = "Opening Balance";
            _with1.Columns.Add("R_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["R_BALANCE"].Caption = "Total Receipt";
            _with1.Columns.Add("P_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["P_BALANCE"].Caption = "Total Payment";
            _with1.Columns.Add("C_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["C_BALANCE"].Caption = "Closing Balance";

            Open_Cash_Bal = 0;
            Close_Cash_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'> MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
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
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["RECEIPT"]))
                {
                    R_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["RECEIPT"]);
                }
                else
                {
                    R_CASH = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["PAYMENT"]))
                {
                    P_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["PAYMENT"]);
                }
                else
                {
                    P_CASH = 0;
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
                Open_Cash_Bal = 0;
                R_CASH = 0;
                P_CASH = 0;
                Close_Cash_Bal = 0;
            }
            ROW = CashBank_Table.NewRow();
            ROW["Title"] = "CASH";
            ROW["Sr"] = 1;
            ROW["Description"] = "CASH Summary";
            ROW["O_BALANCE"] = Open_Cash_Bal;
            ROW["R_BALANCE"] = R_CASH;
            ROW["P_BALANCE"] = P_CASH;
            ROW["C_BALANCE"] = Close_Cash_Bal;
            CashBank_Table.Rows.Add(ROW);

            //'BANK................................
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'> MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!')</script>");
            }
            int XCNT = 2;
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
                {
                    if (!Convert.IsDBNull(XROW["OPENING"]))
                    {
                        Open_Bank_Bal = Convert.ToDouble(XROW["OPENING"]);
                    }
                    else
                    {
                        Open_Bank_Bal += 0;
                    }
                    if (!Convert.IsDBNull(XROW["RECEIPT"]))
                    {
                        R_BANK = Convert.ToDouble(XROW["RECEIPT"]);
                    }
                    else
                    {
                        R_BANK += 0;
                    }
                    if (!Convert.IsDBNull(XROW["PAYMENT"]))
                    {
                        P_BANK = Convert.ToDouble(XROW["PAYMENT"]);
                    }
                    else
                    {
                        P_BANK += 0;
                    }
                    if (!Convert.IsDBNull(XROW["CLOSING"]))
                    {
                        Close_Bank_Bal = Convert.ToDouble(XROW["CLOSING"]);
                    }
                    else
                    {
                        Close_Bank_Bal += 0;
                    }

                    ROW = CashBank_Table.NewRow();
                    ROW["Title"] = "BANK";
                    ROW["Sr"] = XCNT;
                    ROW["Description"] = XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    ROW["O_BALANCE"] = Open_Bank_Bal;
                    ROW["R_BALANCE"] = R_BANK;
                    ROW["P_BALANCE"] = P_BANK;
                    ROW["C_BALANCE"] = Close_Bank_Bal;
                    CashBank_Table.Rows.Add(ROW);
                    XCNT += 1;
                }
            }
            var SummaryGridData = new List<Summary>();
            foreach (DataRow XROW in CashBank_Table.Rows)
            {
                var newrow = new Summary();
                newrow.Title = XROW["Title"].ToString();
                newrow.Sr = Convert.ToInt32(XROW["Sr"]);
                newrow.Description = XROW["Description"].ToString();
                newrow.O_BALANCE = Convert.ToDouble(XROW["O_BALANCE"]);
                newrow.R_BALANCE = Convert.ToDouble(XROW["R_BALANCE"]);
                newrow.P_BALANCE = Convert.ToDouble(XROW["P_BALANCE"]);
                newrow.C_BALANCE = Convert.ToDouble(XROW["C_BALANCE"]);
                SummaryGridData.Add(newrow);
            }
            Voucher_SummaryGridData = SummaryGridData;
            return View(SummaryGridData);
        }
        public ActionResult Frm_View_SummaryGrid()
        {
            return View(Voucher_SummaryGridData);
        }
        public void Frm_View_SummaryGrid_Close()
        {
            BASE._SessionDictionary.Remove("Voucher_SummaryGridData_VoucherInfo");        
        }
        public ActionResult LookUp_GetItemList(DataSourceLoadOptions loadOptions)
        {
            string ITEM_APPLICABLE = "";
            if (BASE.Is_HQ_Centre)
            {
                ITEM_APPLICABLE = "'GENERAL','H.Q.'";
            }
            else
            {
                ITEM_APPLICABLE = "'GENERAL','CENTRE'";
            }
            DataTable d1 = BASE._Voucher_DBOps.GetItem_LedgerListMain(BASE.Allow_Foreign_Donation, BASE.Allow_Membership, ITEM_APPLICABLE);

            DataView dview = new DataView(d1);
            dview.Sort = "ITEM_NAME";
            var data = DatatableToModel.DataTabletoVoucherTypeLookUp_GetItemList(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        public ActionResult Frm_Voucher_Win_D_Type(bool CallFromVoucherInfo = false, int CallFromNavigation = 0)
        {
            ViewBag.CallFromVoucherInfo = CallFromVoucherInfo;
            ViewBag.CallFromNavigation = CallFromNavigation;
            return View();
        }
        public DateTime _xFr_Date()
        {
            return Convert.ToDateTime(xFr_Date);
        }
        public DateTime _xTo_Date()
        {
            return Convert.ToDateTime(xTo_Date);
        }
        public void SessionClear()
        {
            ClearBaseSession("_VoucherInfo"); 
        }
        public void Add_User_Rights()
        {
            ViewData["CashBook_ListRight"] = CheckRights(BASE, ClientScreen.Accounts_CashBook, "List");
            ViewData["NoteBook_ListRight"] = CheckRights(BASE, ClientScreen.Accounts_Notebook, "List");
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Acc_Vou_B2B_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "Add");
            ViewData["Acc_Vou_Payment_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
            ViewData["Acc_Vou_Receipt_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Receipt, "Add");
            ViewData["Acc_Vou_Donation_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Donation, "Add");
            ViewData["Acc_Vou_Gift_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Gift, "Add");
            ViewData["Acc_Vou_Int_Transfer_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Internal_Transfer, "Add");
            ViewData["Acc_Vou_ColBox_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CollectionBox, "Add");
            ViewData["Acc_Vou_FD_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_FD, "Add");
            ViewData["Acc_Vou_SaleAsset_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_SaleOfAsset, "Add");
            ViewData["Acc_Vou_Journal_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Journal, "Add");
            ViewData["Acc_Vou_AsetTransfer_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_AssetTransfer, "Add");
            ViewData["Acc_Vou_WIP_Finalization_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_WIP_Finalization, "Add");

            //for Membership, Membership_Renewal, Membership_Conversion----Screen Name will be added when they are created
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Help_Attachments_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
        }
    }
}