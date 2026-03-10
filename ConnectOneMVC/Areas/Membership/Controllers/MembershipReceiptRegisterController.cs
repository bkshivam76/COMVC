using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Membership.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Common_Lib.Common;
using static Common_Lib.DbOperations;
using static Common_Lib.DbOperations.Voucher_Donation;

namespace ConnectOneMVC.Areas.Membership.Controllers
{
    public class MembershipReceiptRegisterController : BaseController
    {
        // GET: Membership/MembershipReceiptRegister
        #region Global Variables
        public List<WingList> WingList_Data_MRR
        {
            get
            {
                return (List<WingList>)GetBaseSession("MRR_WingList_Data_MRR");
            }
            set
            {
                SetBaseSession("MRR_WingList_Data_MRR", value);
            }
        }
        public List<VoucherTypeItems> MRR_Item_Data
        {
            get
            {
                return (List<VoucherTypeItems>)GetBaseSession("MRR_Item_Data_MRR");
            }
            set
            {
                SetBaseSession("MRR_Item_Data_MRR", value);
            }
        }
        public List<Period_Till_MRR> Period_Data_MRR
        {
            get
            {
                return (List<Period_Till_MRR>)GetBaseSession("MRR_Period_Data_MRR");
            }
            set
            {
                SetBaseSession("MRR_Period_Data_MRR", value);
            }
        }
        public List<SubscriptionList> MRR_SubList_Data
        {
            get
            {
                return (List<SubscriptionList>)GetBaseSession("MRR_SubList_Data_MRR");
            }
            set
            {
                SetBaseSession("MRR_SubList_Data_MRR", value);
            }
        }
        public List<ReceiptPurposeList> PurposeList_Data
        {
            get
            {
                return (List<ReceiptPurposeList>)GetBaseSession("PurposeList_Data_MRR");
            }
            set
            {
                SetBaseSession("PurposeList_Data_MRR", value);
            }
        }
        public List<BankList_MRR> BankList_MRR
        {
            get
            {
                return (List<BankList_MRR>)GetBaseSession("BankList_MRR");
            }
            set
            {
                SetBaseSession("BankList_MRR", value);
            }
        }
        public List<Return_RefBank> RefBankList_MRR
        {
            get
            {
                return (List<Return_RefBank>)GetBaseSession("RefBank_MRR");
            }
            set
            {
                SetBaseSession("RefBank_MRR", value);
            }
        }
        public decimal Default_Ent_Fee
        {
            get
            {
                return (decimal)GetBaseSession("Default_Ent_Fee_MRR");
            }
            set
            {
                SetBaseSession("Default_Ent_Fee_MRR", value);
            }
        }
        public decimal Default_Subs_Fee
        {
            get
            {
                return (decimal)GetBaseSession("Default_Subs_Fee_MRR");
            }
            set
            {
                SetBaseSession("Default_Subs_Fee_MRR", value);
            }
        }
        public decimal Default_Renew_Fee
        {
            get
            {
                return (decimal)GetBaseSession("Default_Renew_Fee_MRR");
            }
            set
            {
                SetBaseSession("Default_Renew_Fee_MRR", value);
            }
        }
        public decimal Txt_CreditAmt_MRR
        {
            get
            {
                return (decimal)GetBaseSession("Txt_CreditAmt_MRR");
            }
            set
            {
                SetBaseSession("Txt_CreditAmt_MRR", value);
            }
        }
        public decimal Txt_DiffAmt_MRR
        {
            get
            {
                return (decimal)GetBaseSession("Txt_DiffAmt_MRR");
            }
            set
            {
                SetBaseSession("Txt_DiffAmt_MRR", value);
            }
        }
        public decimal Txt_SubTotal_MRR
        {
            get
            {
                return (decimal)GetBaseSession("Txt_SubTotal_MRR");
            }
            set
            {
                SetBaseSession("Txt_SubTotal_MRR", value);
            }
        }
        public decimal Txt_BankAmt_MRR
        {
            get
            {
                return (decimal)GetBaseSession("Txt_BankAmt_MRR");
            }
            set
            {
                SetBaseSession("Txt_BankAmt_MRR", value);
            }
        }
        public decimal Txt_CashAmt_MRR
        {
            get
            {
                return (decimal)GetBaseSession("Txt_CashAmt_MRR");
            }
            set
            {
                SetBaseSession("Txt_CashAmt_MRR", value);
            }
        }
        public decimal Ent_Fee_MRR
        {
            get
            {
                return (decimal)GetBaseSession("MRR_Txt_Ent_Fee_MRR");
            }
            set
            {
                SetBaseSession("MRR_Txt_Ent_Fee_MRR", value);
            }
        }
        public decimal Subs_Fee_MRR
        {
            get
            {
                return (decimal)GetBaseSession("Txt_Subs_Fee_MRR");
            }
            set
            {
                SetBaseSession("Txt_Subs_Fee_MRR", value);
            }
        }
        public decimal Txt_Adv_Fee_MRR
        {
            get
            {
                return (decimal)GetBaseSession("Txt_Adv_Fee_MRR");
            }
            set
            {
                SetBaseSession("Txt_Adv_Fee_MRR", value);
            }
        }
        public DataTable xWing_Short_Text
        {
            get
            {
                return (DataTable)GetBaseSession("xWing_Short_Text_MRR");
            }
            set
            {
                SetBaseSession("xWing_Short_Text_MRR", value);
            }
        }
        public string lbl_FeeEff_Text
        {
            get
            {
                return (string)GetBaseSession("lbl_FeeEff_Text_MRR");
            }
            set
            {
                SetBaseSession("lbl_FeeEff_Text_MRR", value);
            }
        }
        public string lbl_Expire_Text
        {
            get
            {
                return (string)GetBaseSession("lbl_Expire_Text_MRR");
            }
            set
            {
                SetBaseSession("lbl_Expire_Text_MRR", value);
            }
        }
        public List<MembershipNamesList> MembershipNames_DD
        {
            get
            {
                return (List<MembershipNamesList>)GetBaseSession("MembershipNames_DD_MRR");
            }
            set
            {
                SetBaseSession("MembershipNames_DD_MRR", value);
            }
        }
        public DataTable MRR_GridData
        {
            get
            {
                return (DataTable)GetBaseSession("MRR_GridData_MRRInfo");
            }
            set
            {
                SetBaseSession("MRR_GridData_MRRInfo", value);
            }
        }
        public DataTable BankGridData_MRR
        {
            get
            {
                return (DataTable)GetBaseSession("BankGridData_MRR");
            }
            set
            {
                SetBaseSession("BankGridData_MRR", value);
            }
        }
        public DataTable ItemGridData
        {
            get
            {
                return (DataTable)GetBaseSession("ItemGridData_MRR");
            }
            set
            {
                SetBaseSession("ItemGridData_MRR", value);
            }
        }
        public bool IsMobile
        {
            get
            {
                return (bool)GetBaseSession("IsMobile_MRRInfo");
            }
            set
            {
                SetBaseSession("IsMobile_MRRInfo", value);
            }
        }
        public string BE_Cash_Bank_Text
        {
            get
            {
                return (string)GetBaseSession("BE_Cash_Bank_Text_MRRInfo");
            }
            set
            {
                SetBaseSession("BE_Cash_Bank_Text_MRRInfo", value);
            }
        }
        public string Edit_MEM_NO
        {
            get
            {
                return (string)GetBaseSession("Edit_MEM_NO_MRR");
            }
            set
            {
                SetBaseSession("Edit_MEM_NO_MRR", value);
            }
        }
        public string Edit_AB_ID
        {
            get
            {
                return (string)GetBaseSession("Edit_AB_ID_MRR");
            }
            set
            {
                SetBaseSession("Edit_AB_ID_MRR", value);
            }
        }
        public string Edit_WING_ID
        {
            get
            {
                return (string)GetBaseSession("Edit_WING_ID_MRR");
            }
            set
            {
                SetBaseSession("Edit_WING_ID_MRR", value);
            }
        }
        public string MRR_Edit_MEM_OLD_NO_MRR
        {
            get
            {
                return (string)GetBaseSession("Edit_MEM_OLD_NO_MRR");
            }
            set
            {
                SetBaseSession("Edit_MEM_OLD_NO_MRR", value);
            }
        }
        public DateTime? Add_Time
        {
            get
            {
                return (DateTime?)GetBaseSession("Add_Time_MRR");
            }
            set
            {
                SetBaseSession("Add_Time_MRR", value);
            }
        }
        public DateTime? xFr_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xFr_Date_MRRInfo");
            }
            set
            {
                SetBaseSession("xFr_Date_MRRInfo", value);
            }
        }
        public DateTime? xTo_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xTo_Date_MRRInfo");
            }
            set
            {
                SetBaseSession("xTo_Date_MRRInfo", value);
            }
        }
        public List<MRR_Period> MRR_PeriodSelectionData
        {
            get
            {
                return (List<MRR_Period>)GetBaseSession("MRR_PeriodSelectionData_MRRInfo");
            }
            set
            {
                SetBaseSession("MRR_PeriodSelectionData_MRRInfo", value);
            }
        }
        public List<Summary> MRR_SummaryGridData
        {
            get
            {
                return (List<Summary>)GetBaseSession("MRR_SummaryGridData_MRRInfo");
            }
            set
            {
                SetBaseSession("MRR_SummaryGridData_MRRInfo", value);
            }
        }
        public DateTime? dtnull = null;
        #endregion
        #region Grid  
        public void FillChangePeriod()
        {
            var period = new List<MRR_Period>();
            int index = 0;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                MRR_Period row1 = new MRR_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row1.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row1.SelectedIndex = index;
                index++;
                period.Add(row1);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                MRR_Period row2 = new MRR_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row2.SelectedIndex = index;
                row2.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                period.Add(row2);
                index++;
            }
            MRR_Period row = new MRR_Period
            {
                Period = "1st Quarter",
                SelectedIndex = index
            };
            period.Add(row);
            MRR_Period row3 = new MRR_Period
            {
                Period = "2nd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row3);
            MRR_Period row4 = new MRR_Period
            {
                Period = "3rd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row4);
            MRR_Period row5 = new MRR_Period
            {
                Period = "4th Quarter",
                SelectedIndex = ++index
            };
            period.Add(row5);
            MRR_Period row6 = new MRR_Period
            {
                Period = "1st Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row6);
            MRR_Period row7 = new MRR_Period
            {
                Period = "2nd Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row7);
            MRR_Period row8 = new MRR_Period
            {
                Period = "Nine Months",
                SelectedIndex = ++index
            };
            period.Add(row8);
            MRR_Period row9 = new MRR_Period
            {
                Period = "Financial Year",
                SelectedIndex = ++index
            };
            period.Add(row9);
            MRR_Period row10 = new MRR_Period
            {
                Period = "Specific Period",
                SelectedIndex = ++index
            };
            period.Add(row10);
            MRR_PeriodSelectionData = period;
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            if (MRR_PeriodSelectionData == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<MRR_Period>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load((List<MRR_Period>)MRR_PeriodSelectionData, loadOptions)), "application/json");
        }
        public ActionResult Cmb_View_SelectedIndexChanged(int? SelectedIndex = null)
        {
            var Perioddata = (List<MRR_Period>)MRR_PeriodSelectionData;
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
        public ActionResult frm_MembershipRR_Specific_Period(bool PeriodSelectionFirstTime = false)
        {
            ViewBag.PeriodSelectionFirstTime = PeriodSelectionFirstTime;
            MRR_SpeceficPeriod model = new MRR_SpeceficPeriod();
            model.MRR_Fromdate = _xFr_Date();
            model.MRR_Todate = _xTo_Date();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult GetSpecificPeriod(MRR_SpeceficPeriod model)
        {
            if (model.MRR_Fromdate < BASE._open_Year_Sdt || model.MRR_Fromdate > BASE._open_Year_Edt) 
            {
                return Json(new
                {
                    result = false,
                    message = "From Date Not As Per Financial Year!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.MRR_Todate < BASE._open_Year_Sdt || model.MRR_Todate > BASE._open_Year_Edt)
            {
                return Json(new
                {
                    result = false,
                    message = "To Date Not As Per Financial Year!!"
                }, JsonRequestBehavior.AllowGet);
            }
            if (model.MRR_Todate >= model.MRR_Fromdate)
            {
                xFr_Date = model.MRR_Fromdate;
                xTo_Date = model.MRR_Todate;
                string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");
                return Json(new
                {
                    result = true,
                    BE_View_Period,
                    BE_Cash_Bank_Text,
                    FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                    ToDate = _xTo_Date().ToString("MM/dd/yyyy")
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = false,
                    message = "\'To Date\' Cannot Be Less From \'From Date..\'!!"
                }, JsonRequestBehavior.AllowGet);//Redmine Bug #135467 fixed
            }
        }
        public ActionResult Get_Cash_Bank_Balance()
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            double Open_Cash = 0;
            double Open_Bank = 0;
            double R_CASH = 0;
            double R_BANK = 0;
            double P_CASH = 0;
            double P_BANK = 0;
            //Cash.bal
            DataTable d1 = BASE._NoteBook_DBOps.GetCashBalance();
            if (d1 == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                //jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (d1.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(d1.Rows[0]["OP_AMOUNT"]))
                    Open_Cash = Convert.ToDouble(d1.Rows[0]["OP_AMOUNT"]);
                else
                    Open_Cash = 0;
            }
            else
            {
                Open_Cash = 0;
            }
            //Bank.bal
            DataTable d2 = BASE._NoteBook_DBOps.GetSavingAccounts();
            if (d2 == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                //jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            string Bank_IDs = "";
            foreach (DataRow xRow in d2.Rows)
            {
                Bank_IDs += "'" + xRow["ID"].ToString() + "',";
            }
            if (Bank_IDs.Trim().Length > 0)
            {
                Bank_IDs = (Bank_IDs.Trim().EndsWith(",") ? Bank_IDs.Trim().ToString().Substring(0, (Bank_IDs.Trim().Length - 1)) : Bank_IDs.Trim().ToString());
                //Bank_IDs = IIf(Bank_IDs.Trim().EndsWith(","), Mid(Bank_IDs.Trim().ToString(), 1,(Bank_IDs.Trim().Length - 1)), Bank_IDs.Trim().ToString());
            }
            if (Bank_IDs.Trim().Length == 0)
            {
                Bank_IDs = "''";
            }

            DataTable d3 = BASE._NoteBook_DBOps.GetBankBalance(Bank_IDs);
            if (d3 == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                //jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (d3.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(d3.Rows[0]["BALANCE"]))
                    Open_Bank = Convert.ToDouble(d3.Rows[0]["BALANCE"]);
                else
                    Open_Bank = 0;
            }
            else
            {
                Open_Bank = 0;
            }
            DataTable d4 = BASE._NoteBook_DBOps.GetCashBankTransSum(Convert.ToDateTime(xFr_Date));
            if (d4 == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                //jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (d4.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(d4.Rows[0]["R_CASH"]))
                {
                    R_CASH = Convert.ToDouble(d4.Rows[0]["R_CASH"]);
                }
                else
                {
                    R_CASH = 0;
                }
                if (!Convert.IsDBNull(d4.Rows[0]["P_CASH"]))
                {
                    P_CASH = Convert.ToDouble(d4.Rows[0]["P_CASH"]);
                }
                else
                {
                    P_CASH = 0;
                }
                if (!Convert.IsDBNull(d4.Rows[0]["R_BANK"]))
                {
                    R_BANK = Convert.ToDouble(d4.Rows[0]["R_BANK"]);
                }
                else
                {
                    R_BANK = 0;
                }
                if (!Convert.IsDBNull(d4.Rows[0]["P_BANK"]))
                {
                    P_BANK = Convert.ToDouble(d4.Rows[0]["P_BANK"]);
                }
                else
                {
                    P_BANK = 0;
                }
            }
            Open_Cash = (Open_Cash + R_CASH) - P_CASH;
            Open_Bank = (Open_Bank + R_BANK) - P_BANK;

            DataTable d5 = BASE._NoteBook_DBOps.GetCashBankTransSum(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date));
            if (d5 == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                //jsonParam.closeform = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            if (d5.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(d5.Rows[0]["R_CASH"]))
                {
                    R_CASH = Convert.ToDouble(d5.Rows[0]["R_CASH"]);
                }
                else
                {
                    R_CASH = 0;
                }
                if (!Convert.IsDBNull(d5.Rows[0]["P_CASH"]))
                {
                    P_CASH = Convert.ToDouble(d5.Rows[0]["P_CASH"]);
                }
                else
                {
                    P_CASH = 0;
                }
                if (!Convert.IsDBNull(d5.Rows[0]["R_BANK"]))
                {
                    R_BANK = Convert.ToDouble(d5.Rows[0]["R_BANK"]);
                }
                else
                {
                    R_BANK = 0;
                }
                if (!Convert.IsDBNull(d5.Rows[0]["P_BANK"]))
                {
                    P_BANK = Convert.ToDouble(d5.Rows[0]["P_BANK"]);
                }
                else
                {
                    P_BANK = 0;
                }
            }
            double Close_Cash = (Open_Cash + R_CASH) - P_CASH;
            double Close_Bank = (Open_Bank + R_BANK) - P_BANK;

            BE_Cash_Bank_Text = " Cash: " + Close_Cash.ToString("#,0.00") + "    Bank: " + Close_Bank.ToString("#,0.00")+" ";
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
        public ActionResult MembershipReceiptRegister_Print(string ID)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            IsMobile = isMobileBrowser();
            ViewBag.IsMobile = IsMobile;
            string xTemp_ID = ID;
            //string xRecNo = Receipt_No;
            object rCount = BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xTemp_ID);
            if (rCount == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (Convert.ToInt32(rCount) <= 0)
            {
                jsonParam.message = "Membership Receipt Not Generated. . . !";
                jsonParam.title = "Information..!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            ConnectOne.D0010._001.Rec_Membership_Print xRep = new ConnectOne.D0010._001.Rec_Membership_Print(BASE, xTemp_ID);
            return View(xRep);
        }
        public ActionResult Frm_Membership_Receipt_Register(string PopupID = "")
        {
            ViewBag.PopupID = PopupID;
            ViewBag.UserType = BASE._open_User_Type;
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            if (!(CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            Membership_user_rights();
            FillChangePeriod();
            if (DateTime.Now.Date <= BASE._open_Year_Edt)
            {
                xFr_Date = DateTime.Now.Date;
                xTo_Date = DateTime.Now.Date;
                ViewBag.AllowDialogBox = false;
                ViewBag.PeriodSelectedIndex = 20;
            }
            else
            {
                xFr_Date = BASE._open_Year_Edt;
                xTo_Date = BASE._open_Year_Edt;
                ViewBag.AllowDialogBox = false;
                ViewBag.PeriodSelectedIndex = 20;
            }
            ViewBag.AllowDialogBox = true;
            return View();
        }
        public ActionResult Frm_Membership_Receipt_Register_Grid(string command, int ShowHorizontalBar = 0, string Layout = null, bool VouchingMode = false, string ViewMode = "Default", string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "", string RowKeyToFocus = "")
        {
           
            if (MRR_GridData == null || MRR_GridData.Rows.Count == 0 || command == "REFRESH")
            {
                Get_Cash_Bank_Balance();//Redmine Bug #135960 fixed
                MRR_GridData = BASE._Membership_Receipt_Register_DBOps.GetList(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date));
            }
            Membership_user_rights();
            ViewData["ExportGridHeaderLeft"] = "UID : " + BASE._open_UID_No;
            ViewData["ExportGridHeaderRight"] = "Year : " + BASE._open_Year_Name + "";
            ViewData["ExportGridFooter"] = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewData["Layout"] = Layout == "" ? null : Layout;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewBag.ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.ViewMode = ViewMode;
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewBag.BE_Cash_Bank_Text = BE_Cash_Bank_Text;


            return PartialView(MRR_GridData);
        }
        #endregion
        public ActionResult Frm_Membership_Receipt_Cancel(string TitleX = "", string xID = "", string xName = "", string xMemNo = "", string xCenName = "", string xCenUID = "", string xWing = "", string xReceiptNo = "", string xReceiptDt = "")
        {
            try
            {
                Membership_Receipt_Cancel model = new Membership_Receipt_Cancel();
                model.TitleX = "Membership Receipt Cancellation";
                model.Tag = Common_Lib.Common.Navigation_Mode._Edit;
                model.xID = xID;
                model.xName = xName;
                model.xMemNo = xMemNo;
                model.xCenName = xCenName;
                model.xCenUID = xCenUID;
                model.xWing = xWing;
                model.xReceiptNo = xReceiptNo;
                model.xReceiptDt = xReceiptDt;
                return View(model);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);

                return Json(new
                {
                    message = msg,
                    title = "Error..",
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Frm_Membership_Receipt_Cancel(Membership_Receipt_Cancel model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
            {
                if (string.IsNullOrWhiteSpace(model.MRR_Txt_Remarks_MRR))
                {
                    jsonParam.message = "Reason Not Specified...!";
                    jsonParam.title = "Incomplete Information . . .";
                    jsonParam.result = false;
                    jsonParam.focusid = "MRR_Txt_Remarks_MRR";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (!BASE._Membership_Receipt_Register_DBOps.DeleteReceipt(model.MRR_Txt_Remarks_MRR, model.xID))
                {
                    jsonParam.message = Common_Lib.Messages.SomeError;
                    jsonParam.title = "Error";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }
            jsonParam.message = "Membership Receipt Cancelled Successully";
            jsonParam.title = "Membership Receipt Cancellation";
            jsonParam.result = true;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DataNavigation(string ActionMethod = "", string ID = "", string Edit_Date = "", string Selected_Item_ID = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                DataTable allgriddata = MRR_GridData;
                DataRow membershipRowData = MRR_GridData.AsEnumerable().SingleOrDefault(r => r.Field<string>("ID") == ID);
                VoucherMembershipInfo model = new VoucherMembershipInfo();
                if (ActionMethod == "EDIT")
                {
                    string xTemp_ID = ID;
                    model.xID_MRR = xTemp_ID;

                    //'1 Check for Discontinued
                    object GetValue = "";
                    GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xTemp_ID);
                    if (GetValue == null)
                    {
                        jsonParam.message = "Entry Not Found...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                    {
                        jsonParam.message = "Discontiuned Member Entry cannot be Edited / Deleted...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //'2 Check for Another Transacation
                    DateTime xDate = Convert.ToDateTime(membershipRowData["REC_ADD_ON"]);
                    bool TrFound = false;
                    string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                    DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xTemp_ID);
                    if (T1 == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (T1.Rows.Count > 0)
                    {
                        foreach (DataRow XRow in T1.Rows)
                        {
                            if (XRow["REC_ID"].ToString() == xTemp_ID)
                            {
                                continue;
                            }
                            else
                            {
                                xDate = Convert.ToDateTime(XRow["REC_ADD_ON"]);
                                if (Convert.ToDateTime(xDate.ToString("yyyy-MM-dd HH:mm:ss")) > Convert.ToDateTime(CurRecordAddOn))
                                {
                                    TrFound = true;
                                    break;
                                }
                            }
                        }
                        if (TrFound)
                        {
                            jsonParam.message = "Some another Entry available after this Entry of same Member....! Cannot Edit/Delete...";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //'3. Check Receipt Generated.
                    if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xTemp_ID) > 0)
                    {
                        jsonParam.message = "Membership Receipt generated voucher cannot be Edited/Deleted...!";
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    //'4 Bank Closure Check
                    object AccNo = BASE._BankAccountDBOps.GetClosedBank_ByMasterID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership, xTemp_ID);
                    if (AccNo == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (AccNo.ToString().Length > 0)
                    {
                        jsonParam.message = "Entry cannot be Edited/Deleted...! <br><br>In this entry, Used Bank A/c No.: " + AccNo + " was closed...!!!";
                        jsonParam.title = "Information..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership)
                    {
                        model.xMID_MRR = xTemp_ID;
                        Add_Time = Convert.ToDateTime(membershipRowData["REC_ADD_ON"]);
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Membership";
                        jsonParam.popup_form_name = "Frm_Voucher_Membership";
                        jsonParam.popup_form_path = "/Membership/MembershipReceiptRegister/Frm_Voucher_Membership/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + model.xMID_MRR + "&Info_LastEditedOn=" + (DateTime)membershipRowData["REC_EDIT_ON"]+"&Add_Time=" + Add_Time;

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership_Renewal)
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Edit of Membership Renewal is not allowed";
                        jsonParam.title = "Error!!";

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership_Conversion)
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Edit of Membership Conversion is not allowed";
                        jsonParam.title = "Error!!";

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "VIEW")
                {
                    string xTemp_ID = ID;
                    model.xID_MRR = xTemp_ID;
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership)
                    {
                        model.xMID_MRR = xTemp_ID;
                        Add_Time = Convert.ToDateTime(membershipRowData["REC_ADD_ON"]);
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Membership";
                        jsonParam.popup_form_name = "Frm_Voucher_Membership";
                        jsonParam.popup_form_path = "/Membership/MembershipReceiptRegister/Frm_Voucher_Membership/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + model.xMID_MRR + "&Info_LastEditedOn=" + (DateTime)membershipRowData["REC_EDIT_ON"];

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership_Renewal)
                    {
                        jsonParam.result = false;
                        jsonParam.message = "View of Membership Renewal is not allowed";
                        jsonParam.title = "Error!!";

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership_Conversion)
                    {
                        jsonParam.result = false;
                        jsonParam.message = "View of Membership Conversion is not allowed";
                        jsonParam.title = "Error!!";

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "DELETE")
                {
                    bool Flag = false;
                    string xTemp_ID = ID;
                    model.xID_MRR = xTemp_ID;

                    //'1 Check for Discontinued
                    object GetValue = "";
                    GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xTemp_ID);
                    if (GetValue == null)
                    {
                        jsonParam.message = "Entry Not Found...!";
                        jsonParam.title = "Information...";
                        jsonParam.closeform = true;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                    {
                        jsonParam.message = "Discontiuned Member Entry cannot be Edited / Deleted...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //'2 Check for Another Transacation
                    DateTime xDate = Convert.ToDateTime(membershipRowData["REC_ADD_ON"]);
                    bool TrFound = false;
                    string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                    DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xTemp_ID);
                    if (T1 == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (T1.Rows.Count > 0)
                    {
                        foreach (DataRow XRow in T1.Rows)
                        {
                            if (XRow["REC_ID"].ToString() == xTemp_ID)
                            {
                                continue;
                            }
                            else
                            {
                                xDate = Convert.ToDateTime(XRow["REC_ADD_ON"]);
                                if (Convert.ToDateTime(xDate.ToString("yyyy-MM-dd HH:mm:ss")) > Convert.ToDateTime(CurRecordAddOn))
                                {
                                    TrFound = true;
                                    break;
                                }
                            }
                        }
                        if (TrFound)
                        {
                            jsonParam.message = "Some another Entry available after this Entry of same Member....! Cannot Edit/Delete...";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //'3. Check Receipt Generated.
                    if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xTemp_ID) > 0)
                    {
                        jsonParam.message = "Membership Receipt generated voucher cannot be Edited/Deleted...!";
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    //'4 Bank Closure Check
                    object AccNo = BASE._BankAccountDBOps.GetClosedBank_ByMasterID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership, xTemp_ID);
                    if (AccNo == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.IsDBNull(AccNo))
                    {
                        AccNo = "";
                    }
                    if (AccNo.ToString().Length > 0)
                    {
                        jsonParam.message = "Entry cannot be Edited/Deleted...! <br><br>In this entry, Used Bank A/c No.: " + AccNo + " was closed...!!!";
                        jsonParam.title = "Information..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership)
                    {
                        model.xMID_MRR = xTemp_ID;
                        Add_Time = Convert.ToDateTime(membershipRowData["REC_ADD_ON"]);
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Membership";
                        jsonParam.popup_form_name = "Frm_Voucher_Membership";
                        jsonParam.popup_form_path = "/Membership/MembershipReceiptRegister/Frm_Voucher_Membership/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + model.xMID_MRR + "&Info_LastEditedOn=" + (DateTime)membershipRowData["REC_EDIT_ON"]+ "&Add_Time="+ Add_Time;

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership_Renewal)
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Delete of Membership Renewal is not allowed";
                        jsonParam.title = "Error!!";

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)membershipRowData["Entry Tr. Code"] == (int)Common_Lib.Common.Voucher_Screen_Code.Membership_Conversion)
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Delete of Membership Conversion is not allowed";
                        jsonParam.title = "Error!!";

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (ActionMethod == "RECEIPT - GENERATE")
                {
                    string xTemp_ID = ID;
                    model.xID_MRR = xTemp_ID;
                    string xTemp_0 = membershipRowData["Member Name"].ToString();
                    string xTemp_1 = membershipRowData["Membership No."].ToString();
                    string xTemp_2 = membershipRowData["Membership"].ToString();
                    string xTemp_3 = membershipRowData["Centre Name"].ToString();
                    string xTemp_4 = membershipRowData["Centre UID"].ToString();
                    string xTemp_5 = membershipRowData["Wing"].ToString();
                    string xTemp_6 = membershipRowData["Total Amount"].ToString();

                    string VDate = Convert.ToDateTime(membershipRowData["Voucher Date"].ToString()).ToString(BASE._Server_Date_Format_Short);

                    string xEntry = "Member Name" + new string(' ', 6) + ":  <color=Maroon><b>" + xTemp_0 + "</b></color><br><br/>Membership No." + new string(' ', 3) + ":  " + xTemp_1 + "<br><br/>Membership" + new string(' ', 10) + ":  " + xTemp_2 + "<br><br/>Centre Name" + new string(' ', 9) + ":  " + xTemp_3 + ", UID: " + xTemp_4 + "<br><br/>Wing" + new string(' ', 23) + ":  " + xTemp_5 + "<br><br/>Amount" + new string(' ', 19) + ":  " + xTemp_6;
                    // 'Check for Discontinued
                    object GetValue = "";
                    GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xTemp_ID);
                    if (GetValue == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                    {
                        jsonParam.message = "Discontiuned Member Receipt cannot be Generated...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //'1 Chech Already Generated Receipt
                    object rCount = BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xTemp_ID);
                    if (rCount == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)rCount > 0)
                    {
                        jsonParam.message = "Membership Receipt already generated...!<br></br>Note: Refresh Current List, if Receipt not shown.";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //'2 Check for Another Transacation without Receipt Generated
                    DateTime xDate = Convert.ToDateTime(membershipRowData["REC_ADD_ON"].ToString());
                    bool TrFound = false;
                    string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                    DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xTemp_ID);
                    if (T1 == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (T1.Rows.Count > 0)
                    {
                        foreach (DataRow XRow in T1.Rows)
                        {
                            if (XRow["REC_ID"].ToString() == xTemp_ID)
                            {
                                continue;
                            }
                            else
                            {
                                xDate = Convert.ToDateTime(XRow["REC_ADD_ON"]);
                                if (Convert.ToDateTime(xDate.ToString("yyyy-MM-dd HH:mm:ss")) < Convert.ToDateTime(CurRecordAddOn))
                                {
                                    if (Convert.IsDBNull(XRow["MR_NO"]))
                                    {
                                        TrFound = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (TrFound)
                        {
                            jsonParam.message = "Please Generate receipts for older entries of the same Member before generating this receipt...!";
                            jsonParam.title = "Cannot Generate...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (!BASE._Membership_Receipt_Register_DBOps.InsertReceipt(xTemp_ID, VDate))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "Membership Receipt Generated Successfully";
                    jsonParam.title = "Information...";
                    jsonParam.refreshgrid = true;
                    jsonParam.result = true;
                    return Json(new { jsonParam, xTemp_ID }, JsonRequestBehavior.AllowGet);  //Redmine Bug #135459 fixed
                }
                if (ActionMethod == "RECEIPT - CANCELLED")
                {
                    string xTemp_ID = ID;
                    model.xID_MRR = xTemp_ID;
                    string xTemp_0 = membershipRowData["Member Name"].ToString();
                    string xTemp_1 = membershipRowData["Membership"].ToString().ToTitleCase() + " Membership No.:" + membershipRowData["Membership No."].ToString();//Redmine Bug #135464 fixed
                    string xTemp_2 = "Centre Name: " + membershipRowData["Centre Name"].ToString();
                    string xTemp_3 = "Centre UID: " + membershipRowData["Centre UID"].ToString();
                    string xTemp_4 = "Wing: " + membershipRowData["Wing"].ToString().ToTitleCase();
                    string xTemp_5 = "Receipt No.: " + membershipRowData["Receipt No."].ToString();
                    string xTemp_6 = "";
                    DateTime? rptDate = membershipRowData.IsNull("Receipt Date") ? (DateTime?)null : (DateTime?)membershipRowData["Receipt Date"];
                    if (IsDate(rptDate))
                    {
                        xTemp_6 = "Receipt Date: " + Convert.ToDateTime(membershipRowData["Receipt Date"]).ToString(BASE._Date_Format_Current);
                    }
                    string xTemp_7 = membershipRowData["Receipt ID"].ToString();
                    //1. Check Receipt Generated
                    object rCount = BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xTemp_ID);
                    if (rCount == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((int)rCount <= 0)
                    {
                        jsonParam.message = "Membership Receipt cannot be Cancelled....<br></br>Reason:    Membership Receipt not generated...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //'2 Check for Another Transacation
                    DateTime xDate = Convert.ToDateTime(membershipRowData["REC_ADD_ON"].ToString());
                    bool TrFound = false;
                    string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                    DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xTemp_ID);
                    if (T1 == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (T1.Rows.Count > 0)
                    {
                        foreach (DataRow XRow in T1.Rows)
                        {
                            if (XRow["REC_ID"].ToString() == xTemp_ID)
                            {
                                continue;
                            }
                            else
                            {
                                xDate = Convert.ToDateTime(XRow["REC_ADD_ON"]);
                                if (Convert.ToDateTime(xDate.ToString("yyyy-MM-dd HH:mm:ss")) > Convert.ToDateTime(CurRecordAddOn))
                                {
                                    if (Convert.IsDBNull(XRow["MR_NO"])==false)
                                    {
                                        TrFound = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (TrFound)
                        {
                            jsonParam.message = "Please cancel the receipt of other latest entries before cancelling the receipt of this entry...!";
                            jsonParam.title = "Cannot Cancelled...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //'3 Check for Discontinued
                    object GetValue = "";
                    GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xTemp_ID);
                    if (GetValue == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                    {
                        jsonParam.message = "Discontiuned Member Receipt Entry cannot be Cancelled...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    Membership_Receipt_Cancel xfrm = new Membership_Receipt_Cancel();
                    xfrm.TitleX = "Membership Receipt Cancellation";
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit;

                    jsonParam.result = true;
                    jsonParam.popup_title = "Membership Receipt Register";
                    jsonParam.popup_form_name = "Frm_Membership_Receipt_Cancel";
                    jsonParam.popup_form_path = "/Membership/MembershipReceiptRegister/Frm_Membership_Receipt_Cancel/";
                    jsonParam.popup_querystring = "xTitle=" + xfrm.TitleX + "&xID=" + xTemp_7 + "&xName=" + xTemp_0 + "&xMemNo=" + xTemp_1 + "&xCenName=" + xTemp_2 + "&xCenUID=" + xTemp_3 + "&xWing=" + xTemp_4 + "&xReceiptNo=" + xTemp_5 + "&xReceiptDt=" + xTemp_6;

                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                jsonParam,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Voucher_Membership(string Tag = "", string xID = "", string xMID = "", DateTime? Info_LastEditedOn = null, string PopupName = "Dynamic_Content_popup", string Add_Time = null, string GridToRefresh = "MembershipReceiptRegisterGrid", string iSpecific_ItemID = "")
        {
            ViewData["MembershipRR_ListAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewData["MembershipRR_AddAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
            ViewData["MembershipRR_UpdateAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Update");
            ResetStaticVariable();
            var j = 0;
            string[] Rights = { "Add", "Update", "View", "Delete" };
            Common.Navigation_Mode[] AM = { Common.Navigation_Mode._New, Navigation_Mode._Edit, Navigation_Mode._View, Navigation_Mode._Delete };
            for (j = 0; j < Rights.Length; j++)
            {
                if (!CheckRights(BASE, ClientScreen.Membership_Receipt_Register, Rights[j]) && Tag == AM[j].ToString())
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
            }
            VoucherMembershipInfo model = new VoucherMembershipInfo();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = model.Tag.ToString();
            model.TempActionMethod = Tag;
            model.DocumentGettingReady = true;
            model.xID_MRR = xID;
            model.xMID_MRR = xMID;
            model.TitleX_MRR = "Membership";
            model.Info_LastEditedOn_MRR = Info_LastEditedOn;
            model.Txt_V_Date_MRR = DateTime.Now;
            model.Txt_S_Date_MRR = model.Txt_V_Date_MRR;
            model.GridToRefresh = GridToRefresh;
            model.Edit_WING_ID = "";
            if (string.IsNullOrWhiteSpace(Add_Time) == false)
            {
                model.Add_Time = Convert.ToDateTime(Add_Time);
            }         
            model.ItemDD_Data = LookUp_GetItemList();
            if (model.ItemDD_Data.Count == 1)
            {
                model.GLookUp_ItemList_MRR = model.ItemDD_Data[0].ITEMID;
            }
            model.PurposeDD_Data = LookUp_GetPurposeList();
            if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                model.GLookUp_PurList_MRR = "8f6b3279-166a-4cd9-8497-ca9fc6283b25";
            }
            model.WingDD_Data = LookUp_GetWingList();
            if (model.WingDD_Data.Count <= 0)
            {
                model.xWing_Short_MRR = (string)BASE._Membership_DBOps.GetInsttShortcodeForMembership();
            }
            model.SubscriptionListDD_Data = LookUp_GetSubscriptionList();
            model.MemberDD_Data = new List<MembershipNamesList>();//Get_MemberList(false, "ami");
            model.Cmb_Period_Data = new List<Period_Till_MRR>();
            model.Cmb_Period_MRR = "";
            SetGridData();
            if (Tag == "_New" || Tag == "_New_From_Selection")
            {
                model.Txt_V_No_MRR = "";
            }
            else
            {
                DataTable d1 = BASE._Membership_Voucher_DBOps.GetMasterRecord(xMID);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                //.......Start: Check if entry already changed.....//                
                if (BASE.AllowMultiuser())
                {
                    if (model.TempActionMethod == "_Edit" || model.TempActionMethod == "_Delete" || model.TempActionMethod == "_View")
                    {
                        var viewstr = "";
                        if (model.TempActionMethod == "_View")
                        {
                            viewstr = "view";
                        }
                        if (d1.Rows.Count <= 0)
                        {
                            string message = Messages.RecordChanged("Current Membership", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Deleted!!','" + GridToRefresh + "');</script>");
                        }
                        if (model.Info_LastEditedOn_MRR != (DateTime)d1.Rows[0]["REC_EDIT_ON"])
                        {
                            string message = Messages.RecordChanged("Current Membership", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                //......'End : Check if entry already changed.......//

                DataTable d2 = BASE._Membership_Voucher_DBOps.GetPurposeRecord(xMID);
                DataTable d3 = BASE._Membership_Voucher_DBOps.GetRecord(xMID);
                DataTable d4 = BASE._Membership_Voucher_DBOps.GetMembershipRecord(xMID);
                DataTable d5 = BASE._Membership_Voucher_DBOps.GetTxnBankPaymentDetail(xMID);
                DateTime LastPeriod = (DateTime)BASE._Membership_Voucher_DBOps.GetLastPeriod(xMID);//Redmine Bug #135971 fixed
                if (d1 == null | d2 == null | d3 == null | d4 == null | d5 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (d1.Rows.Count <= 0 | d2.Rows.Count <= 0 | d3.Rows.Count <= 0 | d4.Rows.Count <= 0)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.No_InvalidData + "','Error!!');</script>");
                }
                Data_Binding(ref model, d1, d2, d3, d4, d5, LastPeriod);//Redmine Bug #135971 fixed
            }
            if (model.Tag == Navigation_Mode._New_From_Selection)
            {
                model.GLookUp_ItemList_MRR = iSpecific_ItemID;
            }   
            ViewData["Open_Ins_ID"] = BASE._open_Ins_ID;
            return PartialView(model);
        }
        public void Data_Binding(ref VoucherMembershipInfo model, DataTable d1, DataTable d2, DataTable d3, DataTable d4, DataTable d5, DateTime Lastperiod)
        {
            model.xID_MRR = d3.Rows[0]["REC_ID"].ToString();
            model.Txt_V_Date_MRR = Convert.ToDateTime(d3.Rows[0]["TR_DATE"]);
            model.Txt_V_No_MRR = d3.Rows[0]["TR_VNO"].ToString();
            if (d3.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
            {
                model.GLookUp_ItemList_MRR = "45afe059-14c8-11e1-9111-00ffddbf0f50";
            }
            model.PC_MemberName_MRR = d4.Rows[0]["MS_AB_ID"].ToString();
            model.MemberDD_Data = Get_MemberList(true, model.PC_MemberName_MRR);
            model.Edit_REC_ID_MRR = d4.Rows[0]["REC_ID"].ToString();
            model.Txt_S_Date_MRR = Convert.ToDateTime(d4.Rows[0]["MS_START_DATE"]);
            if (d4.Rows[0]["MS_SI_ID"].ToString().Length > 0)
            {
                model.GLookUp_SubList_MRR = d4.Rows[0]["MS_SI_ID"].ToString();
                GetDataFromSubsDropDown(ref model);
                model.Txt_Subs_Fee_MRR = Convert.ToDouble(Default_Subs_Fee);
                model.Txt_Ent_Fee_MRR = Convert.ToDouble(Default_Ent_Fee);
                model.Cmb_Period_Data = Period_Data_MRR;
                model.lbl_FeeEff_Text = lbl_FeeEff_Text;
            }
            if (model.Subs_Category_MRR.ToUpper() != "LIFETIME")
            {
                for (int I = 0; I < Period_Data_MRR.Count; I++)
                {
                    if (Lastperiod.ToString("dd MMM, yyyy").ToUpper() == Period_Data_MRR[I].Period_MRR.ToString().Substring(0, 12).ToUpper().Trim())
                    {
                        model.Cmb_Period_SelectedIndexChanged = I;
                    }
                }
            }
            else 
            {
                model.Cmb_Period_SelectedIndexChanged = 0;
            }
            if (model.Cmb_Period_SelectedIndexChanged >= 0) //cmb period selected
            {
                Sub_Amt_Calculation(model.Subs_Category_MRR, model.Subs_Start_Month_MRR.ToString(), model.Txt_S_Date_MRR.ToString(), model.GLookUp_SubList_MRR, (int)model.Cmb_Period_SelectedIndexChanged);
                model.Txt_Ent_Fee_MRR = Convert.ToDouble(Ent_Fee_MRR);
                model.Txt_Subs_Fee_MRR= Convert.ToDouble(Subs_Fee_MRR);
                model.Txt_Adv_Fee_MRR = Convert.ToDouble(Txt_Adv_Fee_MRR);
                model.Txt_SubTotal_MRR = Convert.ToDouble(Txt_SubTotal_MRR);
                model.Txt_CashAmt_MRR= Convert.ToDouble(Txt_CashAmt_MRR);
                model.lbl_Expire_Text = lbl_Expire_Text;               
            }         

            model.Txt_Mem_Old_No_MRR = d4.Rows[0]["MS_OLD_NO"].ToString();
            MRR_Edit_MEM_OLD_NO_MRR = model.Txt_Mem_Old_No_MRR;
            if (Convert.IsDBNull(d4.Rows[0]["MS_WING_ID"]) == false)
            {
                if (d4.Rows[0]["MS_WING_ID"].ToString().Length > 0)
                {
                    model.Edit_WING_ID = d4.Rows[0]["MS_WING_ID"].ToString();
                    model.GLookUp_WingList_MRR = model.Edit_WING_ID;
                }
            }//Redmine Bug #135971 fixed
            model.Txt_Mem_No_MRR = d4.Rows[0]["MS_NO"].ToString();
            Edit_MEM_NO = model.Txt_Mem_No_MRR;

            if (Convert.IsDBNull(d2.Rows[0]["TR_PURPOSE_MISC_ID"]) == false)
            {
                if (d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().Length > 0)
                {
                    model.GLookUp_PurList_MRR = d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString();
                }
            }
            //bank details          
            foreach (DataRow XRow in d5.Rows)
            {
                DataRow ROW = BankGridData_MRR.NewRow();
                ROW["Sr."] = XRow["TR_SR_NO"];
                ROW["Mode"] = XRow["TR_MODE"];
                ROW["Amount"] = XRow["TR_REF_AMT"];
                //Reference Bank
                ROW["Bank Name"] = XRow["REF_BANK_NAME"];
                ROW["Branch"] = XRow["TR_REF_BRANCH"];
                ROW["No."] = XRow["TR_REF_NO"];
                if (Convert.IsDBNull(XRow["TR_REF_DATE"]) == false)
                {
                    ROW["Date"] = Convert.ToDateTime(XRow["TR_REF_DATE"]).ToString(BASE._Date_Format_Current);
                }
                if (Convert.IsDBNull(XRow["TR_REF_CDATE"]) == false)
                {
                    ROW["Clearing Date"] = Convert.ToDateTime(XRow["TR_REF_CDATE"]).ToString(BASE._Date_Format_Current);
                }
                ROW["Ref_Bank_ID"] = XRow["TR_REF_ID"];
                //DEPOSITED BANK
                ROW["Deposited Bank"] = XRow["DEP_BANK_NAME"];
                ROW["Deposited Branch"] = XRow["DEP_BRANCH_NAME"];
                ROW["Deposited A/c. No."] = XRow["DEP_BANK_ACC_NO"];
                ROW["Dep_Bank_ID"] = XRow["TR_DEP_BA_REC_ID"];
                ROW["BA_EDIT_ON"] = XRow["BA_EDIT_ON"];
                BankGridData_MRR.Rows.Add(ROW);
            }
            Bank_Amt_Calculation(false);
            model.Txt_Narration_MRR = d3.Rows[0]["TR_NARRATION"].ToString();
            model.Txt_Remarks_MRR = d3.Rows[0]["TR_REMARKS"].ToString();
            model.Txt_Reference_MRR = d3.Rows[0]["TR_REFERENCE"].ToString();

            Txt_CashAmt_MRR = Convert.ToDecimal(d1.Rows[0]["TR_CASH_AMT"]);
            Txt_BankAmt_MRR = Convert.ToDecimal(d1.Rows[0]["TR_BANK_AMT"]);
            model.Txt_CashAmt_MRR = Convert.ToDouble(Txt_CashAmt_MRR);
            model.Txt_BankAmt_MRR = Convert.ToDouble(Txt_BankAmt_MRR);
            model.Txt_DiffAmt_MRR = Convert.ToDecimal(Txt_DiffAmt_MRR);          
            model.Last_Edit_Time_MRR = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
        }
        public void GetDataFromSubsDropDown(ref VoucherMembershipInfo model)
        {
            if (model.SubscriptionListDD_Data != null && model.SubscriptionListDD_Data.Count > 0)
            {
                var Subsid = model.GLookUp_SubList_MRR;
                var SubsData = model.SubscriptionListDD_Data.Find(x => x.SI_REC_ID == Subsid);
                if (SubsData == null)
                {
                    model.Subs_Category_MRR = "";
                    model.Subs_Start_Month_MRR = 0;
                    model.Subs_Total_Month_MRR = 0;
                }
                else
                {
                    model.Subs_Category_MRR = SubsData.SI_CATEGORY ?? "";
                    model.Subs_Start_Month_MRR = SubsData.SI_START_MONTH ?? 0;
                    model.Subs_Total_Month_MRR = SubsData.SI_TOTAL_MONTH ?? 0;
                }
                GetSubscriptionFee(model.Txt_S_Date_MRR.ToString(), Subsid, model.Subs_Category_MRR, model.Subs_Start_Month_MRR.ToString(), model.Subs_Total_Month_MRR.ToString());
            }
        }
        

        [HttpPost]
        public ActionResult Frm_Voucher_Membership_Window_BUT_SAVE_Click(VoucherMembershipInfo model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            try
            {
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_ItemList_MRR) || model.GLookUp_ItemList_MRR.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "Item Name Not Selected . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_ItemList_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_MRR) == false)
                    {
                        jsonParam.message = "Voucher Date Incorrect/Blank . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_V_Date_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(Convert.ToDateTime(model.Txt_V_Date_MRR)))
                    {
                        if (Convert.ToDateTime(model.Txt_V_Date_MRR) < Convert.ToDateTime(BASE._open_Year_Sdt) || Convert.ToDateTime(model.Txt_V_Date_MRR) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_V_Date_MRR";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.PC_MemberName_MRR) || model.PC_MemberName_MRR.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "Member Not Selected . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "PC_MemberName_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.BE_Cen_Name_MRR) && BASE._open_Ins_ID != "00008")
                    {
                        jsonParam.message = "Centre Name Not Defined . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "BE_Cen_Name_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_SubList_MRR) || model.GLookUp_SubList_MRR.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "Membership Type Not Selected . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_SubList_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_S_Date_MRR) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_S_Date_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(Convert.ToDateTime(model.Txt_S_Date_MRR)))
                    {
                        if (Convert.ToDateTime(model.Txt_S_Date_MRR) < Convert.ToDateTime(model.Txt_V_Date_MRR))
                        {
                            jsonParam.message = "Date shall be Voucher date / 1st April of next Financial year...!";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_S_Date_MRR";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDateTime(model.Txt_S_Date_MRR) > Convert.ToDateTime(model.Txt_V_Date_MRR) && Convert.ToDateTime(model.Txt_S_Date_MRR) != BASE._open_Year_Edt.AddDays(1))
                        {
                            jsonParam.message = "Date shall be Voucher date / 1st April of next Financial year...!";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_S_Date_MRR";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_WingList_MRR) && model.WingListEnabled==true)
                    {
                        jsonParam.message = "Wing Name Not Selected . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_WingList_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDouble(model.Txt_CashAmt_MRR) < 0)
                    {
                        jsonParam.message = "Amount cannot be Negative. . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_CashAmt_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_PurList_MRR) || model.GLookUp_PurList_MRR.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "Purpose Not Selected . . . !";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PurList_MRR";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                //.....Start : Check if entry already changed .....//

                //........Add checks for user count checking to reduce overhead........//

                if (BASE.AllowMultiuser())
                {
                    foreach (DataRow cRow in BankGridData_MRR.Rows)
                    {
                        if (Convert.IsDBNull(cRow["Ref_Bank_ID"]) == false && cRow.IsNull("Ref_Bank_ID") == false)
                        {
                            object AccNo = BASE._Voucher_DBOps.GetBankAccount(cRow["Ref_Bank_ID"].ToString(), "");
                            if (AccNo == null)
                            {
                                jsonParam.message = Common_Lib.Messages.SomeError;
                                jsonParam.title = "Error";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            if (Convert.IsDBNull(AccNo))
                            {
                                AccNo = "";
                            }
                            if (AccNo.ToString().Length > 0)
                            {
                                jsonParam.message = "Entry cannot be Added/Edited/Deleted. . . !" + "<br>" + "<br>" + "In this entry, Used Bank A/c No.: " + AccNo.ToString() + " was closed...!!!";
                                jsonParam.title = "Information...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    foreach (DataRow dRow in BankGridData_MRR.Rows)
                    {
                        object EditTime = BASE._BankAccountDBOps.GetLastEditTime(dRow["Dep_Bank_ID"].ToString(), Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership);
                        if (Convert.IsDBNull(EditTime))
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Bank Account Referred in payment");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(dRow["BA_EDIT_ON"]),Convert.ToDateTime(EditTime))==false)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Bank Account Referred in payment");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    object member_Last_Edit = BASE._Membership_DBOps.GetAddressEditTime(model.PC_MemberName_MRR);
                    if (Convert.IsDBNull(member_Last_Edit))
                    {
                        jsonParam.message = Common_Lib.Messages.RecordChanged("Referred Member Address Book Record");
                        jsonParam.title = "Record Already Changed!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    TimeSpan ts = Convert.ToDateTime(member_Last_Edit) - Convert.ToDateTime(model.Membership_Last_Edit_Time);
                    int X = Convert.ToInt32(ts.TotalMinutes);
                    if (X != 0)
                    {
                        jsonParam.message = Common_Lib.Messages.RecordChanged("Referred Member Address Book Record");
                        jsonParam.title = "Record Already Changed!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable master_DbOps = BASE._Membership_Voucher_DBOps.GetMasterRecord(model.xMID_MRR);
                        if (master_DbOps == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (master_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Membership receipt");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (CommonFunctions.AreDatesEqual(Convert.ToDateTime(model.Last_Edit_Time_MRR),Convert.ToDateTime(master_DbOps.Rows[0]["REC_EDIT_ON"]))==false)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Membership receipt");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._Membership_DBOps.GetDiscontinued(true, model.xMID_MRR).ToString().ToUpper() == "DISCONTINUED")
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Referred Member");
                            jsonParam.title = "Membership discontinued!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToInt32(BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(model.xMID_MRR)) > 0)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Membership receipt", "Generated", "Membership Renewal Receipt");
                            jsonParam.title = "Receipt Already Generated!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        //Special Checks

                        //2 Check for Another Transacation
                        bool TrFound = false;
                        string CurRecordAddOn = model.Add_Time.ToString("yyyy-MM-dd HH:mm:ss");

                        //if (Add_Time != null)
                        //{
                        //    CurRecordAddOn = Convert.ToDateTime(Add_Time).ToString("yyyy-MM-dd HH:mm:ss");
                        //}
                        //else if (MRR_GridData != null && MRR_GridData.Rows.Count > 0)
                        //{
                        //    foreach (DataRow DR in MRR_GridData.Rows)
                        //    {
                        //        if (DR["ID"].ToString() == model.xMID_MRR)
                        //        {
                        //            CurRecordAddOn = Convert.ToDateTime(DR["REC_ADD_ON"]).ToString("yyyy-MM-dd HH:mm:ss");
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    CurRecordAddOn = Convert.ToDateTime(model.Add_Time).ToString("yyyy-MM-dd HH:mm:ss");
                        //}
                        DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, model.xMID_MRR);
                        if (T1 == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime xDate;
                        if (T1.Rows.Count > 0)
                        {
                            foreach (DataRow XRow in T1.Rows)
                            {
                                if (XRow["REC_ID"].ToString() == model.xID_MRR)
                                {
                                    continue;
                                }
                                else
                                {
                                    xDate = Convert.ToDateTime(XRow["REC_ADD_ON"]);
                                    if (Convert.ToDateTime(xDate.ToString("yyyy-MM-dd HH:mm:ss")) > Convert.ToDateTime(CurRecordAddOn))
                                    {
                                        TrFound = true;
                                        break;
                                    }
                                }
                            }
                            if (TrFound)
                            {
                                jsonParam.message = "Some another Entry available after this Entry of same Member . . . . !";
                                jsonParam.title = "Cannot be Edited / Deleted ...";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                //.....End : Check if entry already changed .......//

                //CHECKING LOCK STATUS
                string Old_Status_ID = "";
                if (model.Tag == Common.Navigation_Mode._Edit)
                {
                    object MaxValue = 0;
                    MaxValue = BASE._Membership_Voucher_DBOps.GetStatus(model.xID_MRR);
                    if (MaxValue == null)
                    {
                        jsonParam.message = "Entry Not Found...";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                    {
                        jsonParam.message = "Locked Entry cannot be Edited/Deleted . . . !" + "<br>" + "<br>" + "Note:" + "<br>" + "-------" + "<br>" + "Drop your Request to Madhuban for Unlock this Entry," + "<br>" + "If you really want to do some action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                //CHECKING duplicate
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    object MaxValue = 0;
                    //1 MEMBER
                    string Wing = null;
                    if (string.IsNullOrWhiteSpace(model.GLookUp_WingList_MRR)==false)
                    {
                        Wing = model.GLookUp_WingList_MRR;
                    }
                    MaxValue = BASE._Membership_DBOps.GetDuplicateCount(model.Edit_REC_ID_MRR, model.PC_MemberName_MRR, Wing);
                    if (MaxValue == null)
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt32(MaxValue) > 0)
                    {
                        model.Edit_WING_ID = model.Edit_WING_ID ?? "";
                        model.Edit_AB_ID = model.Edit_AB_ID ?? "";
                        if (!(model.Edit_AB_ID.Trim().Equals(model.PC_MemberName_MRR.ToString().Trim())) || !(model.Edit_WING_ID.Trim().Equals(model.GLookUp_WingList_MRR.ToString().Trim())))
                        {
                            jsonParam.message = "Same Member Already Exists...!";
                            jsonParam.title = "Duplicate Information . . .";
                            jsonParam.result = false;
                            jsonParam.focusid = "PC_MemberName_MRR";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //2 OLD NO
                    if (!string.IsNullOrWhiteSpace(model.Txt_Mem_Old_No_MRR) && model.Txt_Mem_Old_No_MRR.Length > 0)
                    {
                        MaxValue = BASE._Membership_DBOps.GetDuplicateOldNoCount(model.xID_MRR, model.Txt_Mem_Old_No_MRR);
                        if (MaxValue == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if (Convert.ToInt32(MaxValue) > 0)
                        {
                            if (!MRR_Edit_MEM_OLD_NO_MRR.Trim().Equals(model.Txt_Mem_Old_No_MRR.Trim()))
                            {
                                jsonParam.message = "Same Old No. Already Exists. . . !";
                                jsonParam.title = "Duplicate Information . . .";
                                jsonParam.result = false;
                                jsonParam.focusid = "Txt_Mem_Old_No_MRR";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                string Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                if (model.Tag == Navigation_Mode._Delete)
                {
                    Status_Action = ((int)Common_Lib.Common.Record_Status._Deleted).ToString();
                }
                //.......Split Entry Data Table....//
                //....Start....//
                int xCnt = 0;
                //DataTable gridData = new DataTable();
                if (model.Txt_Ent_Fee_MRR > 0)  //entrance fee
                {
                    DataTable d1 = BASE._Membership_Voucher_DBOps.GetItemsByID("6b4e2492-14c7-11e1-9111-00ffddbf0f50");
                    if (d1 == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    xCnt += 1;
                    DataRow ROW = ItemGridData.NewRow();
                    //Membership_Grid_Datatable ROW = new Membership_Grid_Datatable();
                    ROW["Sr."] = Convert.ToInt32(xCnt);
                    ROW["Item Name"] = d1.Rows[0]["ITEM_NAME"].ToString();
                    ROW["Item_ID"] = d1.Rows[0]["ITEM_ID"].ToString();
                    ROW["Item_Led_ID"] = d1.Rows[0]["ITEM_LED_ID"].ToString();
                    ROW["Item_Trans_Type"] = d1.Rows[0]["ITEM_TRANS_TYPE"].ToString();
                    ROW["Item_Profile"] = d1.Rows[0]["ITEM_PROFILE"].ToString();
                    ROW["Item_Party_Req"] = d1.Rows[0]["ITEM_PARTY_REQ"].ToString();
                    ROW["Head"] = d1.Rows[0]["LED_NAME"].ToString();
                    ROW["Qty."] = 0; ROW["Unit"] = ""; ROW["Rate"] = 0;
                    ROW["Amount"] = Convert.ToDecimal(model.Txt_Ent_Fee_MRR);
                    ROW["Remarks"] = model.Txt_Remarks_MRR ?? "";
                    ROW["Pur_ID"] = model.GLookUp_PurList_MRR;
                    ItemGridData.Rows.Add(ROW);
                }
                if (model.Txt_Subs_Fee_MRR > 0)        //Subscription Fee
                {
                    string _id = "";
                    if (model.Subs_Category_MRR.ToUpper() == "LIFETIME")
                    {
                        _id = "68d917b2-14c8-11e1-9111-00ffddbf0f50";
                    }
                    else
                    {
                        _id = "45afe059-14c8-11e1-9111-00ffddbf0f50";
                    }
                    DataTable d1 = BASE._Membership_Voucher_DBOps.GetItemsByID(_id);
                    if (d1 == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    xCnt += 1;
                    //Membership_Grid_Datatable ROW = new Membership_Grid_Datatable();
                    DataRow ROW = ItemGridData.NewRow();
                    ROW["Sr."] = Convert.ToInt32(xCnt);
                    ROW["Item Name"] = d1.Rows[0]["ITEM_NAME"].ToString();
                    ROW["Item_ID"] = d1.Rows[0]["ITEM_ID"].ToString();
                    ROW["Item_Led_ID"] = d1.Rows[0]["ITEM_LED_ID"].ToString();
                    ROW["Item_Trans_Type"] = d1.Rows[0]["ITEM_TRANS_TYPE"].ToString();
                    ROW["Item_Profile"] = d1.Rows[0]["ITEM_PROFILE"].ToString();
                    ROW["Item_Party_Req"] = d1.Rows[0]["ITEM_PARTY_REQ"].ToString();
                    ROW["Head"] = d1.Rows[0]["LED_NAME"].ToString();
                    ROW["Qty."] = 0; ROW["Unit"] = ""; ROW["Rate"] = 0;
                    ROW["Amount"] = Convert.ToDecimal(model.Txt_Subs_Fee_MRR);
                    ROW["Remarks"] = model.Txt_Remarks_MRR;
                    ROW["Pur_ID"] = model.GLookUp_PurList_MRR;
                    ItemGridData.Rows.Add(ROW);
                }
                if (model.Txt_Adv_Fee_MRR > 0)   // Adv Fee
                {
                    DataTable d1 = BASE._Membership_Voucher_DBOps.GetItemsByID("5870e47d-14c8-11e1-9111-00ffddbf0f50");
                    if (d1 == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    xCnt += 1;
                    //Membership_Grid_Datatable ROW = new Membership_Grid_Datatable();
                    DataRow ROW = ItemGridData.NewRow();
                    ROW["Sr."] = Convert.ToInt32(xCnt);
                    ROW["Item Name"] = d1.Rows[0]["ITEM_NAME"].ToString();
                    ROW["Item_ID"] = d1.Rows[0]["ITEM_ID"].ToString();
                    ROW["Item_Led_ID"] = d1.Rows[0]["ITEM_LED_ID"].ToString();
                    ROW["Item_Trans_Type"] = d1.Rows[0]["ITEM_TRANS_TYPE"].ToString();
                    ROW["Item_Profile"] = d1.Rows[0]["ITEM_PROFILE"].ToString();
                    ROW["Item_Party_Req"] = d1.Rows[0]["ITEM_PARTY_REQ"].ToString();
                    ROW["Head"] = d1.Rows[0]["LED_NAME"].ToString();
                    ROW["Qty."] = 0; ROW["Unit"] = ""; ROW["Rate"] = 0;
                    ROW["Amount"] = Convert.ToDecimal(model.Txt_Adv_Fee_MRR);
                    ROW["Remarks"] = model.Txt_Remarks_MRR;
                    ROW["Pur_ID"] = model.GLookUp_PurList_MRR;
                    ItemGridData.Rows.Add(ROW);
                }
                DataSet Link_DS = new DataSet();
                DataTable Link_DT = Link_DS.Tables.Add("Link_Item");
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    DataSet Payment_DS = new DataSet();
                    DataTable Payment_DT = Payment_DS.Tables.Add("Data");
                    Payment_DT.Columns.Add("Sr.", Type.GetType("System.Int32"));
                    Payment_DT.Columns.Add("Amount", Type.GetType("System.Double"));
                    Payment_DT.Columns.Add("Mode", Type.GetType("System.String"));
                    Payment_DT.Columns.Add("No.", Type.GetType("System.String"));
                    //Payment_DT.Columns.Add("Date", Type.GetType("System.String"));
                    Payment_DT.Columns.Add("Date", typeof(DateTime));
                    //Payment_DT.Columns.Add("CDate", Type.GetType("System.String"));
                    Payment_DT.Columns.Add("CDate", typeof(DateTime));
                    Payment_DT.Columns.Add("Ref_ID", Type.GetType("System.String")); //bank id
                    Payment_DT.Columns.Add("Branch", Type.GetType("System.String"));
                    Payment_DT.Columns.Add("Dep_ID", Type.GetType("System.String")); //Deposited Bank a/c. id
                    xCnt = 1;

                    foreach (DataRow XRow in BankGridData_MRR.Rows)
                    {
                        DataRow ROW = Payment_DT.NewRow();
                        ROW["Sr."] = xCnt;//Redmine Bug #135440 fixed
                        ROW["Amount"] = XRow["Amount"];
                        ROW["Mode"] = XRow["Mode"];
                        ROW["No."] = XRow["No."];
                        ROW["Date"] = XRow["Date"];
                        ROW["CDate"] = XRow["Clearing Date"];
                        ROW["Ref_ID"] = XRow["Ref_Bank_ID"];
                        ROW["Branch"] = XRow["Branch"];
                        ROW["Dep_ID"] = XRow["Dep_Bank_ID"];
                        Payment_DT.Rows.Add(ROW);
                        xCnt += 1;
                    }
                    if (model.Txt_CashAmt_MRR > 0)
                    {
                        DataRow ROW = Payment_DT.NewRow();
                        ROW["Sr."] = xCnt;
                        ROW["Amount"] = model.Txt_CashAmt_MRR;
                        ROW["Mode"] = "CASH";
                        ROW["No."] = "";
                        ROW["Ref_ID"] = "";
                        Payment_DT.Rows.Add(ROW);
                    }
                    Link_DT.Columns.Add("SrNo", Type.GetType("System.Int32"));
                    Link_DT.Columns.Add("Item_ID", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Item_Trans_Type", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Item_Profile", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Dr_Led_id", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Sub_Dr_Led_id", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Cr_Led_id", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Sub_Cr_Led_id", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Mode", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Ref_ID", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Ref_Branch", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Ref_No", Type.GetType("System.String"));
                    //Link_DT.Columns.Add("Ref_Date", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Ref_Date", typeof(DateTime));
                    //Link_DT.Columns.Add("Ref_CDate", Type.GetType("System.String"));
                    Link_DT.Columns.Add("Ref_CDate", typeof(DateTime));
                    Link_DT.Columns.Add("Amount", Type.GetType("System.Double"));
                    Link_DT.Columns.Add("TDS", Type.GetType("System.Double"));
                    Link_DT.Columns.Add("Remarks", Type.GetType("System.String"));

                    xCnt = 0; int zID = 1; double AdjustAmt = 0; double PayAmt = 0;
                    PayAmt = Convert.ToDouble(Payment_DT.Rows[xCnt]["Amount"]);
                    foreach (DataRow XRow in ItemGridData.Rows)
                    {
                        double xAmt = Convert.ToDouble(XRow["Amount"]);
                        while (xAmt != 0)
                        {
                            if (xAmt >= PayAmt)
                            {
                                AdjustAmt = PayAmt; xAmt = xAmt - PayAmt; PayAmt = 0;
                            }
                            else
                            {
                                AdjustAmt = xAmt; PayAmt = PayAmt - xAmt; xAmt = 0;
                            }
                            //...........
                            DataRow ROW = Link_DT.NewRow();
                            ROW["SrNo"] = zID;
                            ROW["Amount"] = AdjustAmt;
                            ROW["Item_ID"] = XRow["Item_ID"];
                            ROW["Item_Trans_Type"] = XRow["Item_Trans_Type"];
                            ROW["Item_Profile"] = XRow["Item_Profile"];
                            if (XRow["Item_Trans_Type"].ToString() == "DEBIT")
                            {
                                ROW["Dr_Led_id"] = XRow["Item_Led_ID"];

                                if (Payment_DT.Rows[xCnt]["Mode"].ToString() == "CASH")
                                {
                                    ROW["Cr_Led_id"] = "00080";
                                }
                                else
                                {
                                    ROW["Cr_Led_id"] = "00079";
                                    ROW["Sub_Cr_Led_id"] = Payment_DT.Rows[xCnt]["Dep_ID"];
                                }
                            }
                            else
                            {
                                ROW["Cr_Led_id"] = XRow["Item_Led_ID"];
                                if (Payment_DT.Rows[xCnt]["Mode"].ToString() == "CASH")
                                {
                                    ROW["Dr_Led_id"] = "00080";
                                }
                                else
                                {
                                    ROW["Dr_Led_id"] = "00079";
                                    ROW["Sub_Dr_Led_id"] = Payment_DT.Rows[xCnt]["Dep_ID"];
                                }
                            }
                            ROW["Mode"] = Payment_DT.Rows[xCnt]["Mode"];
                            ROW["Ref_ID"] = Payment_DT.Rows[xCnt]["Ref_ID"];
                            ROW["Ref_Branch"] = Payment_DT.Rows[xCnt]["Branch"];
                            ROW["Ref_No"] = Payment_DT.Rows[xCnt]["No."];
                            ROW["Ref_Date"] = Payment_DT.Rows[xCnt]["Date"];
                            ROW["Ref_CDate"] = Payment_DT.Rows[xCnt]["CDate"];
                            ROW["Remarks"] = XRow["Remarks"];
                            Link_DT.Rows.Add(ROW);
                            //...............+
                            if (PayAmt == 0)
                            {
                                xCnt += 1;
                            }
                            if (xCnt <= (Payment_DT.Rows.Count - 1))
                            {
                                PayAmt = Convert.ToDouble(Payment_DT.Rows[xCnt]["Amount"]);
                            }
                        }
                        if (PayAmt == 0)
                        {
                            xCnt += 1;
                        }
                        if (xCnt <= (Payment_DT.Rows.Count - 1))
                        {
                            PayAmt = Convert.ToDouble(Payment_DT.Rows[xCnt]["Amount"]);
                        }
                        zID += 1;
                    }
                }
                //........End.........
                DateTime? Fdate = null; DateTime? Tdate = null;
                int sMonth = Convert.ToDateTime(model.Txt_S_Date_MRR).Month;
                int sYear = Convert.ToDateTime(model.Txt_S_Date_MRR).Year;
                Common_Lib.RealTimeService.Param_Txn_Insert_VoucherMembership InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_VoucherMembership();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)      //new
                {
                    //New
                    model.xMID_MRR = Guid.NewGuid().ToString();
                    string xMem_Rec_ID = Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherMembership InMInfo = new Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherMembership();
                    InMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Membership;
                    InMInfo.VNo = model.Txt_V_No_MRR ?? "";
                    if (IsDate(model.Txt_V_Date_MRR))
                    {
                        InMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMInfo.TDate = model.Txt_V_Date_MRR.ToString();
                    }
                    InMInfo.PartyID = BASE._Address_DBOps.GetAddressRecID_ForOrgID(model.PC_MemberName_MRR);
                    InMInfo.Ref_ID = xMem_Rec_ID;
                    InMInfo.SubTotal = Convert.ToDouble(model.Txt_SubTotal_MRR);
                    InMInfo.Cash = Convert.ToDouble(model.Txt_CashAmt_MRR);
                    InMInfo.Bank = Convert.ToDouble(model.Txt_BankAmt_MRR);
                    InMInfo.Status_Action = Status_Action;
                    InMInfo.RecID = model.xMID_MRR ?? "";

                    InNewParam.param_InsertMaster = InMInfo;
                    //.............'Prepare Membership no.
                    string New_Mem_No = "";
                    if (model.iProfile_MRR == "MEMBERSHIP")
                    {
                        if (Convert.ToInt32(model.Txt_Mem_Old_No_MRR) > 0)
                        {
                            New_Mem_No = model.Txt_Mem_Old_No_MRR.PadLeft(6, '0') + "/" + Convert.ToDateTime(model.Txt_S_Date_MRR).ToString("MMyy") + "/" + model.GLookUp_SubList_Text_MRR.Substring(0, 1) + "/" + model.xWing_Short_MRR;//Redmine Bug #135455 fixed
                        }
                        else
                        {
                            object Max_Mem_No = 0;
                            Max_Mem_No = BASE._Membership_Voucher_DBOps.GetNewMembershipNo();
                            if (Max_Mem_No == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            New_Mem_No = Max_Mem_No.ToString().PadLeft(6, '0') + "/" + Convert.ToDateTime(model.Txt_S_Date_MRR).ToString("MMyy") + "/" + model.GLookUp_SubList_Text_MRR.Substring(0, 1) + "/" + model.xWing_Short_MRR;//Redmine Bug #135455 fixed
                        }
                        model.Txt_Narration_MRR = "Membership No.: " + New_Mem_No;
                    }
                    //.............................
                    List<Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership> Insert = new List<Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership>();
                    int ctr = 0;
                    foreach (DataRow XRow in Link_DT.Rows)
                    {
                        string ID = System.Guid.NewGuid().ToString();
                        Common_Lib.Common.Voucher_Screen_Code ScreenCode = Common_Lib.Common.Voucher_Screen_Code.Membership;
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership();
                        InParam.TransCode = Convert.ToInt32(ScreenCode);
                        InParam.VNo = model.Txt_V_No_MRR ?? "";
                        if (IsDate(model.Txt_V_Date_MRR))
                        {
                            InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = model.Txt_V_Date_MRR.ToString();
                        }
                        InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? "" : XRow["Item_ID"].ToString();
                        InParam.Type = Convert.IsDBNull(XRow["Item_Trans_Type"]) ? "" : XRow["Item_Trans_Type"].ToString();
                        InParam.Cr_Led_ID = Convert.IsDBNull(XRow["Cr_Led_id"]) ? "" : XRow["Cr_Led_id"].ToString();
                        InParam.Dr_Led_ID = Convert.IsDBNull(XRow["Dr_Led_id"]) ? "" : XRow["Dr_Led_id"].ToString();
                        InParam.SUB_Cr_Led_ID = Convert.IsDBNull(XRow["Sub_Cr_Led_id"]) ? "" : XRow["Sub_Cr_Led_id"].ToString();
                        InParam.SUB_Dr_Led_ID = Convert.IsDBNull(XRow["Sub_Dr_Led_id"]) ? "" : XRow["Sub_Dr_Led_id"].ToString();
                        InParam.Amount = Convert.IsDBNull((double)XRow["Amount"]) ? Convert.ToDouble(null) : (double)XRow["Amount"];
                        InParam.Mode = Convert.IsDBNull(XRow["Mode"]) ? "" : XRow["Mode"].ToString();
                        InParam.Ref_BANK_ID = Convert.IsDBNull(XRow["Ref_ID"]) ? "" : XRow["Ref_ID"].ToString();
                        InParam.Ref_Branch = Convert.IsDBNull(XRow["Ref_Branch"]) ? "" : XRow["Ref_Branch"].ToString();
                        InParam.Ref_No = Convert.IsDBNull(XRow["Ref_No"]) ? "" : XRow["Ref_No"].ToString();
                        if (!Convert.IsDBNull(XRow["Ref_Date"]) && IsDate(Convert.ToDateTime(XRow["Ref_Date"])))
                        {
                            InParam.Ref_Date = Convert.IsDBNull(XRow["Ref_Date"]) ? "" : Convert.ToDateTime(XRow["Ref_Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.Ref_Date = Convert.IsDBNull(XRow["Ref_Date"]) ? "" : XRow["Ref_Date"].ToString();
                        }

                        if (!Convert.IsDBNull(XRow["Ref_CDate"]) && IsDate(Convert.ToDateTime(XRow["Ref_CDate"])))
                        {
                            InParam.Ref_CDate = Convert.IsDBNull(XRow["Ref_CDate"]) ? "" : Convert.ToDateTime(XRow["Ref_CDate"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.Ref_CDate = Convert.IsDBNull(XRow["Ref_CDate"]) ? "" : XRow["Ref_CDate"].ToString();
                        }
                        InParam.Party1 = BASE._Address_DBOps.GetAddressRecID_ForOrgID(model.PC_MemberName_MRR);
                        InParam.Narration = model.Txt_Narration_MRR ?? "";
                        InParam.Remarks = Convert.IsDBNull(XRow["Remarks"]) ? "" : XRow["Remarks"].ToString();
                        InParam.Reference = model.Txt_Reference_MRR ?? "";
                        InParam.MasterTxnID = model.xMID_MRR ?? "";
                        InParam.SrNo = Convert.IsDBNull(XRow["SrNo"]) ? "" : XRow["SrNo"].ToString();
                        InParam.Status_Action = Status_Action;
                        InParam.RecID = Guid.NewGuid().ToString();

                        Insert.Add(InParam);
                        ctr += 1;
                    }
                    InNewParam.Insert = Insert.ToArray();
                    if (model.iProfile_MRR == "MEMBERSHIP")
                    {
                        Common_Lib.RealTimeService.Parameter_Insert_Membership InMem = new Common_Lib.RealTimeService.Parameter_Insert_Membership();
                        InMem.AB_ID = model.PC_MemberName_MRR;
                        InMem.SUBS_ID = model.GLookUp_SubList_MRR;
                        object WingID = null;
                        if (string.IsNullOrWhiteSpace(model.GLookUp_WingList_MRR) == false)
                        {
                            WingID = model.GLookUp_WingList_MRR;
                            InMem.Wing_Id = WingID.ToString();
                        }
                        else 
                        {
                            InMem.Wing_Id = "";
                        }
                        if (IsDate(model.Txt_S_Date_MRR))
                        {
                            InMem.StartDate = Convert.ToDateTime(model.Txt_S_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InMem.StartDate = model.Txt_S_Date_MRR.ToString();
                        }
                        InMem.Mem_Old_No = model.Txt_Mem_Old_No_MRR;
                        InMem.Mem_No = New_Mem_No;
                        InMem.OtherDetails = model.Txt_Remarks_MRR;
                        InMem.Status_Action = Status_Action;
                        InMem.Rec_ID = xMem_Rec_ID;
                        InMem.Txn_ID = model.xMID_MRR ?? "";
                        InMem.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership;

                        InNewParam.param_InsertMembership = InMem;
                    }
                    List<Parameter_InsertItem_VoucherMembership> InsertItem = new List<Common_Lib.RealTimeService.Parameter_InsertItem_VoucherMembership>();
                    List<Parameter_InsertPurpose_VoucherMembership> InsertPurpose = new List<Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMembership>();
                    List<Parameter_InsertBalances_Membership> InsertBalNotAdvFee = new List<Common_Lib.RealTimeService.Parameter_InsertBalances_Membership>();
                    Parameter_InsertBalances_Membership[][] InsertBalFeeInAdvance = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership[ItemGridData.Rows.Count + 1][];

                    //Main Items
                    int cntMI = 0;
                    xCnt = 0;

                    foreach (DataRow XRow in ItemGridData.Rows)
                    {
                        xCnt += 1;
                        Common_Lib.RealTimeService.Parameter_InsertItem_VoucherMembership InItem = new Common_Lib.RealTimeService.Parameter_InsertItem_VoucherMembership();
                        InItem.Txn_M_ID = model.xMID_MRR ?? "";
                        InItem.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : (int)XRow["Sr."];
                        InItem.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? "" : XRow["Item_ID"].ToString();
                        InItem.LedID = Convert.IsDBNull(XRow["Item_Led_ID"]) ? "" : XRow["Item_Led_ID"].ToString();
                        InItem.Type = Convert.IsDBNull(XRow["Item_Trans_Type"]) ? "" : XRow["Item_Trans_Type"].ToString();
                        InItem.PartyReq = Convert.IsDBNull(XRow["Item_Party_Req"]) ? "" : XRow["Item_Party_Req"].ToString();
                        InItem.Profile = Convert.IsDBNull(XRow["Item_Profile"]) ? "" : XRow["Item_Profile"].ToString();
                        InItem.ItemName = Convert.IsDBNull(XRow["Item Name"]) ? "" : XRow["Item Name"].ToString();
                        InItem.Head = Convert.IsDBNull(XRow["Head"]) ? "" : XRow["Head"].ToString();
                        InItem.Qty = (double)XRow["Qty."];
                        InItem.Unit = Convert.IsDBNull(XRow["Unit"]) ? "" : XRow["Unit"].ToString();
                        InItem.Rate = (double)XRow["Rate"];
                        InItem.Amount = (double)XRow["Amount"];
                        InItem.Remarks = Convert.IsDBNull(XRow["Remarks"]) ? "" : XRow["Remarks"].ToString();
                        InItem.Status_Action = Status_Action;
                        InItem.RecID = System.Guid.NewGuid().ToString();

                        InsertItem.Add(InItem);
                        //Purpose........
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMembership InPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMembership();
                        InPurpose.TxnID = model.xMID_MRR ?? "";
                        InPurpose.PurposeID = Convert.IsDBNull(XRow["PUR_ID"]) ? "" : XRow["PUR_ID"].ToString();
                        InPurpose.Amount = (double)XRow["Amount"];
                        InPurpose.SrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : (int)XRow["Sr."];
                        InPurpose.Status_Action = Status_Action;
                        InPurpose.RecID = System.Guid.NewGuid().ToString();

                        InsertPurpose.Add(InPurpose);

                        if (XRow["Item_ID"].ToString() != "5870e47d-14c8-11e1-9111-00ffddbf0f50")   //advance fee
                        {
                            if (model.Subs_Category_MRR.ToUpper() == "LIFETIME")
                            {
                                Fdate = new DateTime((sMonth < Convert.ToInt32(model.Subs_Start_Month_MRR) ? sYear - 1 : sYear), Convert.ToInt32(model.Subs_Start_Month_MRR), 1);
                            }
                            else
                            {
                                var fdd = Period_Data_MRR[0].Period_MRR.ToString().Substring(0, 2);
                                var fmm = Period_Data_MRR[0].Period_MRR.ToString().Substring(3, 3).ToUpper();
                                var fyy = Period_Data_MRR[0].Period_MRR.ToString().Substring(8, 4);
                                int MonthStringToNumbe_fmm = DateTime.ParseExact(fmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                Fdate = new DateTime(Convert.ToInt32(fyy), MonthStringToNumbe_fmm, Convert.ToInt32(fdd));
                                var tdd = Period_Data_MRR[0].Period_MRR.ToString().Substring(15, 2);
                                var tmm = Period_Data_MRR[0].Period_MRR.ToString().Substring(18, 3).ToUpper();
                                var tyy = Period_Data_MRR[0].Period_MRR.ToString().Substring(23, 4);
                                int MonthStringToNumber_tmm = DateTime.ParseExact(tmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                Tdate = new DateTime(Convert.ToInt32(tyy), MonthStringToNumber_tmm, Convert.ToInt32(tdd));
                            }
                            Common_Lib.RealTimeService.Parameter_InsertBalances_Membership InBal = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership();
                            InBal.REC_ID = xMem_Rec_ID;
                            InBal.Sr_No = xCnt;
                            InBal.SUBS_ID = model.GLookUp_SubList_MRR;
                            InBal.Item_ID = XRow["Item_ID"].ToString();
                            if (IsDate(model.Txt_V_Date_MRR))
                            {
                                InBal.Entry_Date = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InBal.Entry_Date = model.Txt_V_Date_MRR.ToString();
                            }
                            InBal.Period_From = Convert.ToDateTime(Fdate).ToString(BASE._Server_Date_Format_Short);
                            InBal.Period_To = model.Subs_Category_MRR.ToUpper() == "LIFETIME" ? "" : Convert.ToDateTime(Tdate).ToString(BASE._Server_Date_Format_Short);
                            InBal.Amount = (double)XRow["Amount"];
                            InBal.Status_Action = Status_Action;
                            InBal.Txn_ID = model.xMID_MRR ?? "";
                            InBal.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership;

                            InsertBalNotAdvFee.Add(InBal);
                        }
                        else      //FEE IN ADVANCE
                        {
                            int _Index = 0;

                            if (model.Subs_Category_MRR.ToUpper() == "LIFETIME")
                            {
                                DateTime FirstDate = new DateTime((sMonth < model.Subs_Start_Month_MRR ? sYear - 1 : sYear), Convert.ToInt32(model.Subs_Start_Month_MRR), 1);
                                TimeSpan ts = FirstDate - BASE._open_Year_Edt;
                                int X = Convert.ToInt32(ts.TotalDays);
                                if (X > 0)
                                {
                                    _Index = 0;
                                }
                                else
                                {
                                    _Index = 1;
                                }
                            }
                            else
                            {
                                var fdd = Period_Data_MRR[0].Period_MRR.ToString().Substring(0, 2);
                                var fmm = Period_Data_MRR[0].Period_MRR.ToString().Substring(3, 3).ToUpper();
                                var fyy = Period_Data_MRR[0].Period_MRR.ToString().Substring(8, 4);
                                int MonthStringToNumbe_fmm = DateTime.ParseExact(fmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                Fdate = new DateTime(Convert.ToInt32(fyy), MonthStringToNumbe_fmm, Convert.ToInt32(fdd));
                                TimeSpan ts = Convert.ToDateTime(Fdate) - BASE._open_Year_Edt;
                                int X = Convert.ToInt32(ts.TotalDays);
                                if (X > 0)
                                {
                                    _Index = 0;
                                }
                                else
                                {
                                    _Index = 1;
                                }
                            }
                            Parameter_InsertBalances_Membership[] FeeInAdv = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership[(int)model.Cmb_Period_SelectedIndexChanged];
                            ctr = 0;
                            for (int i = _Index; i <= model.Cmb_Period_SelectedIndexChanged; i++)
                            {
                                if (model.Subs_Category_MRR.ToUpper() == "LIFETIME")
                                {
                                    Fdate = new DateTime((sMonth < Convert.ToInt32(model.Subs_Start_Month_MRR) ? sYear - 1 : sYear), Convert.ToInt32(model.Subs_Start_Month_MRR), 1);
                                }
                                else
                                {
                                    var fdd = Period_Data_MRR[i].Period_MRR.ToString().Substring(0, 2);
                                    var fmm = Period_Data_MRR[i].Period_MRR.ToString().Substring(3, 3).ToUpper();
                                    var fyy = Period_Data_MRR[i].Period_MRR.ToString().Substring(8, 4);
                                    int MonthStringToNumber_fmm = DateTime.ParseExact(fmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                    Fdate = new DateTime(Convert.ToInt32(fyy), MonthStringToNumber_fmm, Convert.ToInt32(fdd));
                                    var tdd = Period_Data_MRR[i].Period_MRR.ToString().Substring(15, 2);
                                    var tmm = Period_Data_MRR[i].Period_MRR.ToString().Substring(18, 3).ToUpper();
                                    var tyy = Period_Data_MRR[i].Period_MRR.ToString().Substring(23, 4);
                                    int MonthStringToNumber_tmm = DateTime.ParseExact(tmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                    Tdate = new DateTime(Convert.ToInt32(tyy), MonthStringToNumber_tmm, Convert.ToInt32(tdd));
                                }
                                Common_Lib.RealTimeService.Parameter_InsertBalances_Membership InBalances = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership();
                                InBalances.REC_ID = xMem_Rec_ID;
                                InBalances.Sr_No = xCnt;
                                InBalances.SUBS_ID = model.GLookUp_SubList_MRR;
                                InBalances.Item_ID = XRow["Item_ID"].ToString();
                                if (IsDate(model.Txt_V_Date_MRR))
                                {
                                    InBalances.Entry_Date = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InBalances.Entry_Date = model.Txt_V_Date_MRR.ToString();
                                }
                                InBalances.Period_From = Convert.ToDateTime(Fdate).ToString(BASE._Server_Date_Format_Short);
                                InBalances.Period_To = model.Subs_Category_MRR.ToUpper() == "LIFETIME" ? "" : Convert.ToDateTime(Tdate).ToString(BASE._Server_Date_Format_Short);
                                InBalances.Amount = (double)Default_Subs_Fee;
                                InBalances.Status_Action = Status_Action;
                                InBalances.Txn_ID = model.xMID_MRR ?? "";
                                InBalances.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership;
                                FeeInAdv[ctr] = InBalances;
                                ctr += 1;
                            }
                            InsertBalFeeInAdvance[cntMI] = FeeInAdv;
                        }
                        cntMI += 1;
                    }
                    InNewParam.InsertItem = InsertItem.ToArray();
                    InNewParam.InsertPurpose = InsertPurpose.ToArray();
                    InNewParam.InsertBalNotAdvFee = InsertBalNotAdvFee.ToArray();
                    InNewParam.InsertBalFeeInAdvance = InsertBalFeeInAdvance;

                    List<Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership> BankPayment = new List<Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership>();
                    int cntBP = 0;
                    //Bank Payment
                    foreach (DataRow XRow in BankGridData_MRR.Rows)
                    {
                        Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership InPmt = new Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership();
                        InPmt.TxnMID = model.xMID_MRR ?? "";
                        InPmt.Type = "BANK";
                        InPmt.SrNo = Convert.IsDBNull(XRow["Sr."]) ? "" : XRow["Sr."].ToString();
                        InPmt.Mode = Convert.IsDBNull(XRow["Mode"]) ? "" : XRow["Mode"].ToString();
                        InPmt.RefID = Convert.IsDBNull(XRow["Ref_Bank_ID"]) ? "" : XRow["Ref_Bank_ID"].ToString();
                        InPmt.RefBranch = Convert.IsDBNull(XRow["Branch"]) ? "" : XRow["Branch"].ToString();
                        InPmt.RefNo = Convert.IsDBNull(XRow["No."]) ? "" : XRow["No."].ToString();
                        if (!Convert.IsDBNull(XRow["Date"]) && IsDate(Convert.ToDateTime(XRow["Date"])))
                        {
                            InPmt.RefDate = Convert.IsDBNull(XRow["Date"]) ? "" : Convert.ToDateTime(XRow["Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPmt.RefDate = Convert.IsDBNull(XRow["Date"]) ? "" : XRow["Date"].ToString();
                        }
                        if (!Convert.IsDBNull(XRow["Clearing Date"]) && IsDate(Convert.ToDateTime(XRow["Clearing Date"])))
                        {
                            InPmt.ClearingDate = Convert.IsDBNull(XRow["Clearing Date"]) ? "" : Convert.ToDateTime(XRow["Clearing Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPmt.ClearingDate = Convert.IsDBNull(XRow["Clearing Date"]) ? "" : XRow["Clearing Date"].ToString();
                        }
                        //InPmt.RefDate = Convert.IsDBNull(XRow["Date"]) ? "" : XRow["Date"].ToString();
                        //InPmt.ClearingDate = Convert.IsDBNull(XRow["Clearing Date"]) ? "" : XRow["Clearing Date"].ToString();
                        InPmt.RefAmount = (double)XRow["Amount"];
                        InPmt.Dep_BA_ID = Convert.IsDBNull(XRow["Dep_Bank_ID"]) ? "" : XRow["Dep_Bank_ID"].ToString();
                        InPmt.Status_Action = Status_Action;
                        InPmt.RecID = System.Guid.NewGuid().ToString();

                        BankPayment.Add(InPmt);
                        cntBP += 1;
                    }
                    InNewParam.InsertPayment = BankPayment.ToArray();
                    if (!BASE._Membership_Voucher_DBOps.InsertMembershipVoucher_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "Membership No.:" + New_Mem_No + "<br />" + "<br />" + Messages.SaveSuccess;
                    jsonParam.title = model.TitleX_MRR;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new
                    {
                        jsonParam,
                        xid = model.xMID_MRR,
                        CashbookGridPK = model.xMID_MRR + model.xID_MRR
                    }, JsonRequestBehavior.AllowGet); ;
                }
                //Start.........Edit........//
                Common_Lib.RealTimeService.Param_Txn_Update_VoucherMembership EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_VoucherMembership();
                if (model.Tag == Common.Navigation_Mode._Edit)
                {
                    string xMem_Rec_ID = System.Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherMembership UpMaster = new Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherMembership();
                    UpMaster.VNo = model.Txt_V_No_MRR ?? "";
                    if (IsDate(model.Txt_V_Date_MRR))
                    {
                        UpMaster.TDate = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMaster.TDate = model.Txt_V_Date_MRR.ToString();
                    }
                    UpMaster.PartyID = BASE._Address_DBOps.GetAddressRecID_ForOrgID(model.PC_MemberName_MRR);
                    UpMaster.Ref_ID = xMem_Rec_ID;
                    UpMaster.SubTotal = model.Txt_SubTotal_MRR;
                    UpMaster.Cash = model.Txt_CashAmt_MRR;
                    UpMaster.Bank = model.Txt_BankAmt_MRR;
                    UpMaster.RecID = model.xMID_MRR ?? "";

                    EditParam.param_UpdateMaster = UpMaster;
                    EditParam.MID_Delete = model.xMID_MRR;
                    EditParam.MID_DeleteItems = model.xMID_MRR;
                    EditParam.MID_DeletePurpose = model.xMID_MRR;
                    EditParam.MID_DeletePayment = model.xMID_MRR;
                    EditParam.MID_DeleteBalances = model.xMID_MRR;
                    EditParam.MID_DeleteMembership = model.xMID_MRR;

                    //.......'Prepare Membership no.
                    string New_Mem_No = "";
                    if (model.iProfile_MRR == "MEMBERSHIP")
                    {
                        if (Convert.ToInt32(model.Txt_Mem_Old_No_MRR) > 0)
                        {
                            New_Mem_No = model.Txt_Mem_Old_No_MRR.PadLeft(6, '0') + "/" + Convert.ToDateTime(model.Txt_S_Date_MRR).ToString("MMyy") + "/" + model.GLookUp_SubList_Text_MRR.Substring(0, 1) + "/" + model.xWing_Short_MRR;//Redmine Bug #135455 fixed
                        }
                        else
                        {
                            object Max_Mem_No = 0;
                            if (Edit_MEM_NO.Length > 0)
                            {
                                //'Use pre generated no.
                                Max_Mem_No = Edit_MEM_NO.Substring(0, 6);
                            }
                            else
                            {
                                //'Get fresh new no.
                                Max_Mem_No = BASE._Membership_Voucher_DBOps.GetNewMembershipNo();
                                if (Max_Mem_No == null)
                                {
                                    jsonParam.message = Messages.SomeError;
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            //'NEW NO + START DATE (MONTH&YEAR) + TYPE + WING
                            New_Mem_No = Max_Mem_No.ToString().PadLeft(6, '0') + "/" + Convert.ToDateTime(model.Txt_S_Date_MRR).ToString("MMyy") + "/" + model.GLookUp_SubList_Text_MRR.Substring(0, 1) + "/" + model.xWing_Short_MRR;//Redmine Bug #135455 fixed
                        }
                    }
                    model.Txt_Narration_MRR = "Membership No.: " + New_Mem_No;
                    //'--------------------------------
                    List<Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership> Insert = new List<Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership>();
                    int ctr = 0;
                    foreach (DataRow XRow in Link_DT.Rows)
                    {
                        string ID = System.Guid.NewGuid().ToString();
                        Common_Lib.Common.Voucher_Screen_Code ScreenCode = Common_Lib.Common.Voucher_Screen_Code.Membership;
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership InParams = new Common_Lib.RealTimeService.Parameter_Insert_VoucherMembership();
                        InParams.TransCode = Convert.ToInt32(ScreenCode);
                        InParams.VNo = model.Txt_V_No_MRR ?? "";
                        if (IsDate(model.Txt_V_Date_MRR))
                        {
                            InParams.TDate = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.TDate = model.Txt_V_Date_MRR.ToString();
                        }
                        InParams.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? "" : XRow["Item_ID"].ToString();
                        InParams.Type = Convert.IsDBNull(XRow["Item_Trans_Type"]) ? "" : XRow["Item_Trans_Type"].ToString();
                        InParams.Cr_Led_ID = Convert.IsDBNull(XRow["Cr_Led_id"]) ? "" : XRow["Cr_Led_id"].ToString();
                        InParams.Dr_Led_ID = Convert.IsDBNull(XRow["Dr_Led_id"]) ? "" : XRow["Dr_Led_id"].ToString();
                        InParams.SUB_Cr_Led_ID = Convert.IsDBNull(XRow["Sub_Cr_Led_id"]) ? "" : XRow["Sub_Cr_Led_id"].ToString();
                        InParams.SUB_Dr_Led_ID = Convert.IsDBNull(XRow["Sub_Dr_Led_id"]) ? "" : XRow["Sub_Dr_Led_id"].ToString();
                        InParams.Amount = Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : (double)XRow["Amount"];
                        InParams.Mode = Convert.IsDBNull(XRow["Mode"]) ? "" : XRow["Mode"].ToString();
                        InParams.Ref_BANK_ID = Convert.IsDBNull(XRow["Ref_ID"]) ? "" : XRow["Ref_ID"].ToString();
                        InParams.Ref_Branch = Convert.IsDBNull(XRow["Ref_Branch"]) ? "" : XRow["Ref_Branch"].ToString();
                        InParams.Ref_No = Convert.IsDBNull(XRow["Ref_No"]) ? "" : XRow["Ref_No"].ToString();
                        if (!Convert.IsDBNull(XRow["Ref_Date"]) && IsDate(Convert.ToDateTime(XRow["Ref_Date"])))
                        {
                            InParams.Ref_Date = Convert.IsDBNull(XRow["Ref_Date"]) ? "" : Convert.ToDateTime(XRow["Ref_Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.Ref_Date = Convert.IsDBNull(XRow["Ref_Date"]) ? "" : XRow["Ref_Date"].ToString();
                        }
                        if (!Convert.IsDBNull(XRow["Ref_CDate"]) && IsDate(Convert.ToDateTime(XRow["Ref_CDate"])))
                        {
                            InParams.Ref_CDate = Convert.IsDBNull(XRow["Ref_CDate"]) ? "" : Convert.ToDateTime(XRow["Ref_CDate"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParams.Ref_CDate = Convert.IsDBNull(XRow["Ref_CDate"]) ? "" : XRow["Ref_CDate"].ToString();
                        }
                        InParams.Party1 = BASE._Address_DBOps.GetAddressRecID_ForOrgID(model.PC_MemberName_MRR);
                        InParams.Narration = model.Txt_Narration_MRR ?? "";
                        InParams.Remarks = Convert.IsDBNull(XRow["Remarks"]) ? "" : XRow["Remarks"].ToString();
                        InParams.Reference = model.Txt_Reference_MRR ?? "";
                        InParams.MasterTxnID = model.xMID_MRR;
                        InParams.SrNo = Convert.IsDBNull(XRow["SrNo"]) ? "" : XRow["SrNo"].ToString();
                        InParams.Status_Action = Status_Action;
                        InParams.RecID = Guid.NewGuid().ToString();

                        Insert.Add(InParams);
                        ctr += 1;
                    }
                    EditParam.Insert = Insert.ToArray();

                    if (model.iProfile_MRR == "MEMBERSHIP")
                    {
                        Common_Lib.RealTimeService.Parameter_Insert_Membership InMship = new Common_Lib.RealTimeService.Parameter_Insert_Membership();
                        InMship.AB_ID = model.PC_MemberName_MRR;
                        InMship.SUBS_ID = model.GLookUp_SubList_MRR;
                        object WingID = null;
                        if (string.IsNullOrWhiteSpace(model.GLookUp_WingList_MRR) == false)
                        {
                            WingID = model.GLookUp_WingList_MRR;
                            InMship.Wing_Id = WingID.ToString();
                        }
                        else 
                        {
                            InMship.Wing_Id = "";
                        }
                        
                        if (IsDate(model.Txt_S_Date_MRR))
                        {
                            InMship.StartDate = Convert.ToDateTime(model.Txt_S_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InMship.StartDate = model.Txt_S_Date_MRR.ToString();
                        }
                        InMship.Mem_Old_No = model.Txt_Mem_Old_No_MRR;
                        InMship.Mem_No = New_Mem_No;
                        InMship.OtherDetails = model.Txt_Remarks_MRR;
                        InMship.Status_Action = Status_Action;
                        InMship.Rec_ID = xMem_Rec_ID;
                        InMship.Txn_ID = model.xMID_MRR;
                        InMship.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership;

                        EditParam.param_InsertMembership = InMship;
                    }
                    List<Parameter_InsertItem_VoucherMembership> InsertItem = new List<Common_Lib.RealTimeService.Parameter_InsertItem_VoucherMembership>();
                    List<Parameter_InsertPurpose_VoucherMembership> InsertPurpose = new List<Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMembership>();
                    List<Parameter_InsertBalances_Membership> InsertBalNotAdvFee = new List<Common_Lib.RealTimeService.Parameter_InsertBalances_Membership>();
                    Parameter_InsertBalances_Membership[][] InsertBalFeeInAdvance = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership[ItemGridData.Rows.Count + 1][];
                    //Main Items....
                    int cntMI = 0;
                    xCnt = 0;
                    foreach (DataRow XRow in ItemGridData.Rows)
                    {
                        xCnt += 1;
                        Common_Lib.RealTimeService.Parameter_InsertItem_VoucherMembership InsItem = new Common_Lib.RealTimeService.Parameter_InsertItem_VoucherMembership();
                        InsItem.Txn_M_ID = model.xMID_MRR;
                        InsItem.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : (int)XRow["Sr."];
                        InsItem.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? "" : XRow["Item_ID"].ToString();
                        InsItem.LedID = Convert.IsDBNull(XRow["Item_Led_ID"]) ? "" : XRow["Item_Led_ID"].ToString();
                        InsItem.Type = Convert.IsDBNull(XRow["Item_Trans_Type"]) ? "" : XRow["Item_Trans_Type"].ToString();
                        InsItem.PartyReq = Convert.IsDBNull(XRow["Item_Party_Req"]) ? "" : XRow["Item_Party_Req"].ToString();
                        InsItem.Profile = Convert.IsDBNull(XRow["Item_Profile"]) ? "" : XRow["Item_Profile"].ToString();
                        InsItem.ItemName = Convert.IsDBNull(XRow["Item Name"]) ? "" : XRow["Item Name"].ToString();
                        InsItem.Head = Convert.IsDBNull(XRow["Head"]) ? "" : XRow["Head"].ToString();
                        InsItem.Qty = (double)XRow["Qty."];
                        InsItem.Unit = Convert.IsDBNull(XRow["Unit"]) ? "" : XRow["Unit"].ToString();
                        InsItem.Rate = (double)XRow["Rate"];
                        InsItem.Amount = (double)XRow["Amount"];
                        InsItem.Remarks = Convert.IsDBNull(XRow["Remarks"]) ? "" : XRow["Remarks"].ToString();
                        InsItem.Status_Action = Status_Action;
                        InsItem.RecID = System.Guid.NewGuid().ToString();

                        InsertItem.Add(InsItem);
                        //Purpose........
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMembership InPurp = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherMembership();
                        InPurp.TxnID = model.xMID_MRR;
                        InPurp.PurposeID = Convert.IsDBNull(XRow["PUR_ID"]) ? "" : XRow["PUR_ID"].ToString();
                        InPurp.Amount = (double)XRow["Amount"];
                        InPurp.SrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : (int)XRow["Sr."];
                        InPurp.Status_Action = Status_Action;
                        InPurp.RecID = System.Guid.NewGuid().ToString();

                        InsertPurpose.Add(InPurp);
                        if (XRow["Item_ID"].ToString() != "5870e47d-14c8-11e1-9111-00ffddbf0f50")   //advance fee
                        {
                            if (model.Subs_Category_MRR.ToUpper() == "LIFETIME")
                            {
                                Fdate = new DateTime((sMonth < Convert.ToInt32(model.Subs_Start_Month_MRR) ? sYear - 1 : sYear), Convert.ToInt32(model.Subs_Start_Month_MRR), 1);
                            }
                            else
                            {
                                var fdd = Period_Data_MRR[0].Period_MRR.ToString().Substring(0, 2);
                                var fmm = Period_Data_MRR[0].Period_MRR.ToString().Substring(3, 3).ToUpper();
                                var fyy = Period_Data_MRR[0].Period_MRR.ToString().Substring(8, 4);
                                int MonthStringToNumbe_fmm = DateTime.ParseExact(fmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                Fdate = new DateTime(Convert.ToInt32(fyy), MonthStringToNumbe_fmm, Convert.ToInt32(fdd));
                                var tdd = Period_Data_MRR[0].Period_MRR.ToString().Substring(15, 2);
                                var tmm = Period_Data_MRR[0].Period_MRR.ToString().Substring(18, 3).ToUpper();
                                var tyy = Period_Data_MRR[0].Period_MRR.ToString().Substring(23, 4);
                                int MonthStringToNumber_tmm = DateTime.ParseExact(tmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                Tdate = new DateTime(Convert.ToInt32(tyy), MonthStringToNumber_tmm, Convert.ToInt32(tdd));
                            }
                            Common_Lib.RealTimeService.Parameter_InsertBalances_Membership InBal = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership();
                            InBal.REC_ID = xMem_Rec_ID;
                            InBal.Sr_No = xCnt;
                            InBal.SUBS_ID = model.GLookUp_SubList_MRR;
                            InBal.Item_ID = XRow["Item_ID"].ToString();
                            if (IsDate(model.Txt_V_Date_MRR))
                            {
                                InBal.Entry_Date = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InBal.Entry_Date = model.Txt_V_Date_MRR.ToString();
                            }

                            InBal.Period_From = Convert.ToDateTime(Fdate).ToString(BASE._Server_Date_Format_Short);
                            InBal.Period_To = model.Subs_Category_MRR.ToUpper() == "LIFETIME" ? "" : Convert.ToDateTime(Tdate).ToString(BASE._Server_Date_Format_Short);
                            InBal.Amount = (double)XRow["Amount"];
                            InBal.Status_Action = Status_Action;
                            InBal.Txn_ID = model.xMID_MRR ?? "";
                            InBal.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership;

                            InsertBalNotAdvFee.Add(InBal);
                        }
                        else      //FEE IN ADVANCE
                        {
                            int _Index = 0;
                            if (model.Subs_Category_MRR.ToUpper() == "LIFETIME")
                            {
                                DateTime FirstDate = new DateTime((sMonth < model.Subs_Start_Month_MRR ? sYear - 1 : sYear), Convert.ToInt32(model.Subs_Start_Month_MRR), 1);
                                TimeSpan ts = FirstDate - BASE._open_Year_Edt;
                                int X = Convert.ToInt32(ts.TotalDays);
                                if (X > 0)
                                {
                                    _Index = 0;
                                }
                                else
                                {
                                    _Index = 1;
                                }
                            }
                            else
                            {
                                var fdd = Period_Data_MRR[0].Period_MRR.ToString().Substring(0, 2);
                                var fmm = Period_Data_MRR[0].Period_MRR.ToString().Substring(3, 3).ToUpper();
                                var fyy = Period_Data_MRR[0].Period_MRR.ToString().Substring(8, 4);
                                int MonthStringToNumbe_fmm = DateTime.ParseExact(fmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                Fdate = new DateTime(Convert.ToInt32(fyy), MonthStringToNumbe_fmm, Convert.ToInt32(fdd));
                                TimeSpan ts = Convert.ToDateTime(Fdate) - BASE._open_Year_Edt;
                                int X = Convert.ToInt32(ts.TotalDays);
                                if (X > 0)
                                {
                                    _Index = 0;
                                }
                                else
                                {
                                    _Index = 1;
                                }
                            }
                            var FeeInAdv = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership[(int)model.Cmb_Period_SelectedIndexChanged];
                            ctr = 0;
                            for (int i = _Index; i <= model.Cmb_Period_SelectedIndexChanged; i++)
                            {
                                if (model.Subs_Category_MRR.ToUpper() == "LIFETIME")
                                {
                                    Fdate = new DateTime((sMonth < Convert.ToInt32(model.Subs_Start_Month_MRR) ? sYear - 1 : sYear), Convert.ToInt32(model.Subs_Start_Month_MRR), 1);
                                }
                                else
                                {
                                    var fdd = Period_Data_MRR[i].Period_MRR.ToString().Substring(0, 2);
                                    var fmm = Period_Data_MRR[i].Period_MRR.ToString().Substring(3, 3).ToUpper();
                                    var fyy = Period_Data_MRR[i].Period_MRR.ToString().Substring(8, 4);
                                    int MonthStringToNumber_fmm = DateTime.ParseExact(fmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                    Fdate = new DateTime(Convert.ToInt32(fyy), MonthStringToNumber_fmm, Convert.ToInt32(fdd));
                                    var tdd = Period_Data_MRR[i].Period_MRR.ToString().Substring(15, 2);
                                    var tmm = Period_Data_MRR[i].Period_MRR.ToString().Substring(18, 3).ToUpper();
                                    var tyy = Period_Data_MRR[i].Period_MRR.ToString().Substring(23, 4);
                                    int MonthStringToNumber_tmm = DateTime.ParseExact(tmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                                    Tdate = new DateTime(Convert.ToInt32(tyy), MonthStringToNumber_tmm, Convert.ToInt32(tdd));
                                }
                                Common_Lib.RealTimeService.Parameter_InsertBalances_Membership InBals = new Common_Lib.RealTimeService.Parameter_InsertBalances_Membership();
                                InBals.REC_ID = xMem_Rec_ID;
                                InBals.Sr_No = xCnt;
                                InBals.SUBS_ID = model.GLookUp_SubList_MRR;
                                InBals.Item_ID = XRow["Item_ID"].ToString();
                                if (IsDate(model.Txt_V_Date_MRR))
                                {
                                    InBals.Entry_Date = Convert.ToDateTime(model.Txt_V_Date_MRR).ToString(BASE._Server_Date_Format_Short);
                                }
                                else
                                {
                                    InBals.Entry_Date = model.Txt_V_Date_MRR.ToString();
                                }
                                InBals.Period_From = Convert.ToDateTime(Fdate).ToString(BASE._Server_Date_Format_Short);
                                InBals.Period_To = model.Subs_Category_MRR.ToUpper() == "LIFETIME" ? "" : Convert.ToDateTime(Tdate).ToString(BASE._Server_Date_Format_Short);
                                InBals.Amount = (double)Default_Subs_Fee;
                                InBals.Status_Action = Status_Action;
                                InBals.Txn_ID = model.xMID_MRR;
                                InBals.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Membership;
                                FeeInAdv[ctr] = InBals;
                                ctr += 1;
                            }
                            InsertBalFeeInAdvance[cntMI] = FeeInAdv;
                        }
                        cntMI += 1;
                    }
                    EditParam.InsertItem = InsertItem.ToArray();
                    EditParam.InsertPurpose = InsertPurpose.ToArray();
                    EditParam.InsertBalFeeInAdvance = InsertBalFeeInAdvance;
                    EditParam.InsertBalNotAdvFee = InsertBalNotAdvFee.ToArray();

                    List<Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership> BankPayment = new List<Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership>();
                    int cntBP = 0;
                    //Bank Payment
                    foreach (DataRow XRow in BankGridData_MRR.Rows)
                    {
                        Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership InPmt = new Common_Lib.RealTimeService.Parameter_InsertPayment_VoucherMembership();
                        InPmt.TxnMID = model.xMID_MRR;
                        InPmt.Type = "BANK";
                        InPmt.SrNo = Convert.IsDBNull(XRow["Sr."]) ? "" : XRow["Sr."].ToString();
                        InPmt.Mode = Convert.IsDBNull(XRow["Mode"]) ? "" : XRow["Mode"].ToString();
                        InPmt.RefID = Convert.IsDBNull(XRow["Ref_Bank_ID"]) ? "" : XRow["Ref_Bank_ID"].ToString();
                        InPmt.RefBranch = Convert.IsDBNull(XRow["Branch"]) ? "" : XRow["Branch"].ToString();
                        InPmt.RefNo = Convert.IsDBNull(XRow["No."]) ? "" : XRow["No."].ToString();
                        if (!Convert.IsDBNull(XRow["Date"]) && IsDate(Convert.ToDateTime(XRow["Date"])))
                        {
                            InPmt.RefDate = Convert.IsDBNull(XRow["Date"]) ? "" : Convert.ToDateTime(XRow["Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPmt.RefDate = Convert.IsDBNull(XRow["Date"]) ? "" : XRow["Date"].ToString();
                        }
                        if (!Convert.IsDBNull(XRow["Clearing Date"]) && IsDate(Convert.ToDateTime(XRow["Clearing Date"])))
                        {
                            InPmt.ClearingDate = Convert.IsDBNull(XRow["Clearing Date"]) ? "" : Convert.ToDateTime(XRow["Clearing Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPmt.ClearingDate = Convert.IsDBNull(XRow["Clearing Date"]) ? "" : XRow["Clearing Date"].ToString();
                        }
                        InPmt.RefAmount = (double)XRow["Amount"];
                        InPmt.Dep_BA_ID = Convert.IsDBNull(XRow["Dep_Bank_ID"]) ? "" : XRow["Dep_Bank_ID"].ToString();
                        InPmt.Status_Action = Status_Action;
                        InPmt.RecID = System.Guid.NewGuid().ToString();

                        BankPayment.Add(InPmt);
                        cntBP += 1;
                    }
                    EditParam.InsertPayment = BankPayment.ToArray();

                    if (!BASE._Membership_Voucher_DBOps.UpdateMembershipVoucher_Txn(EditParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "Membership No.:" + New_Mem_No + "<br>" + "<br>" + Messages.UpdateSuccess;
                    jsonParam.title = model.TitleX_MRR;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam,
                        xid = model.xMID_MRR,
                        CashbookGridPK= model.xMID_MRR+model.xID_MRR
                    }, JsonRequestBehavior.AllowGet);
                }
                //End....Edit....//
                //Start.......Delete
                Param_Txn_Delete_VoucherMembership DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_VoucherMembership();
                if (model.Tag == Common.Navigation_Mode._Delete)
                {
                    DelParam.MID_DeleteItems = model.xMID_MRR;
                    DelParam.MID_DeletePurpose = model.xMID_MRR;
                    DelParam.MID_DeletePayment = model.xMID_MRR;
                    DelParam.MID_DeleteBalances = model.xMID_MRR;
                    DelParam.MID_DeleteMembership = model.xMID_MRR;
                    DelParam.MID_DeleteReceiptRef = model.xMID_MRR;
                    DelParam.MID_Delete = model.xMID_MRR;
                    DelParam.MID_DeleteMaster = model.xMID_MRR;
                    if (!BASE._Membership_Voucher_DBOps.DeleteMembershipVoucher_Txn(DelParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (!string.IsNullOrWhiteSpace(Edit_MEM_NO) && Edit_MEM_NO.Length > 0)
                    {
                        jsonParam.message = "Membership No.: " + Edit_MEM_NO + "<br>" + "<br>" + Common_Lib.Messages.DeleteSuccess;
                    }
                    else
                    {
                        jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                    }
                    var gridStatus = "";
                    if (!string.IsNullOrWhiteSpace(model.GridToRefresh) && model.GridToRefresh.Length > 0)
                    {
                        gridStatus = "yes";
                    }
                    jsonParam.result = true;
                    jsonParam.title = model.TitleX_MRR;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, GridToRefreshStatus = gridStatus }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        #region LookUp
        public List<VoucherTypeItems> LookUp_GetItemList()
        {
            DataTable d1 = BASE._Membership_Voucher_DBOps.GetItemList();
            DataView dview = new DataView(d1);
            dview.Sort = "ITEM_NAME";
            return DatatableToModel.DataTabletoVoucherMembershipLookUp_GetItemList(dview.ToTable());
        }
        public List<Return_DonationVocuherPurpose> LookUp_GetPurposeList()
        {
            return BASE._Donation_DBOps.GetPurposes();
        }
        public List<WingList> LookUp_GetWingList()
        {
            DataTable d1 = BASE._Membership_DBOps.GetWings();
            DataTable d2 = BASE._ClientUserDBOps.GetWingsCenterTasks();
            var BuildData = (from Wing in d1.AsEnumerable()
                             join Task in d2.AsEnumerable()
                             on Wing["WING_REC_ID"] equals Task["TASK_REF_ID"]
                             select new WingList
                             {
                                 WING_NAME = Wing["WING_NAME"].ToString(),
                                 WING_REC_ID = Wing["WING_REC_ID"].ToString(),
                                 WING_SHORT_MS = Wing["WING_SHORT_MS"].ToString()
                             });
            return BuildData.DistinctBy(x => x.WING_REC_ID).ToList();
        }
        public List<SubscriptionList> LookUp_GetSubscriptionList()
        {
            DataTable d1 = BASE._Membership_DBOps.GetSubscriptionList(BASE._open_Ins_ID).Select("SI_NAME = 'ANNUAL' OR SI_NAME = 'LIFE'").CopyToDataTable();
            DataView dview = new DataView(d1);
            dview.Sort = "SI_NAME";
            return DatatableToModel.DataTabletoLookUp_GetSubsList(dview.ToTable());
        }
        public List<MembershipNamesList> Get_MemberList(bool Use_Rec_ID = false, string SearchStr = "")
        {
            var data = BASE._Membership_DBOps.GetPartyDetails(SearchStr, Use_Rec_ID);
            return DatatableToModel.DataTabletoGetMembershipNamesList(data);
        }
        public ActionResult Search_Memberlist(bool Use_Rec_ID = false, string SearchStr = "")
        {
            return Content(JsonConvert.SerializeObject(Get_MemberList(Use_Rec_ID, SearchStr)), "application/json");
        }
        public ActionResult RefreshMemberList(string SearchStr)
        {
            return Content(JsonConvert.SerializeObject(Get_MemberList(true, SearchStr)), "application/json");
        }
        public ActionResult GetSubscriptionFee(string S_date_MRR = null, string GLookUp_SubList_Tag = "", string Subs_Cat = "", string Subs_Start_M = "", string Subs_Total_M = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (IsDate(Convert.ToDateTime(S_date_MRR)))
            {
                DateTime _SDate = Convert.ToDateTime(S_date_MRR);
                DataTable d1 = BASE._Membership_DBOps.GetSubscriptionFee(GLookUp_SubList_Tag, _SDate);
                if (d1 == null)
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (d1.Rows.Count > 0)
                {
                    Default_Ent_Fee = Convert.ToDecimal(d1.Rows[0]["SF_ENT_FEE"]);
                    Default_Subs_Fee = Convert.ToDecimal(d1.Rows[0]["SF_SUBS_FEE"]);
                    Default_Renew_Fee = Convert.ToDecimal(d1.Rows[0]["SF_RENEW_FEE"]);
                    if (!Convert.IsDBNull(d1.Rows[0]["SF_EFF_DATE"]))
                    {
                        lbl_FeeEff_Text = "Subscription Fee Effective from : " + Convert.ToDateTime(d1.Rows[0]["SF_EFF_DATE"]).ToString("dd MMM,yyyy");
                    }
                    else
                    {
                        lbl_FeeEff_Text = "";
                    }
                }
                else
                {
                    jsonParam.message = "Subscription Fee not Defined. . . ! ! !<br><br/>Contact: System Administrator, Madhuban";
                    jsonParam.title = "Information";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                jsonParam.message = "Start Date Incorrect / Blank . . .";
                jsonParam.title = "Information";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            RefreshPeriodItems(Subs_Cat, Subs_Start_M, S_date_MRR, Subs_Total_M);

            jsonParam.message = "";
            jsonParam.title = "";
            jsonParam.result = true;
            return Json(new
            {
                jsonParam,
                lbl_FeeEff_Text,
                Period_Data_MRR
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_PeriodItems(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (Period_Data_MRR == null || DDRefresh == true)
            {
                Period_Data_MRR = new List<Period_Till_MRR>();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Period_Data_MRR, loadOptions)), "application/json");
        }
        public void RefreshPeriodItems(string Subs_Cat = "", string Subs_Start_Month = "", string Txt_S_Date_Text = "", string Subs_Total_Month = "")
        {
            Period_Data_MRR = new List<Period_Till_MRR>();
            if (Subs_Cat.ToUpper() == "LIFETIME")
            {
                Period_Data_MRR.Add(new Period_Till_MRR() { SelectedIndex_MRR = 0, Period_MRR = Subs_Cat });
            }
            else
            {
                int sMonth = Convert.ToDateTime(Txt_S_Date_Text).Month;
                int sYear = Convert.ToDateTime(Txt_S_Date_Text).Year;
                DateTime FirstDate = new DateTime((Convert.ToDateTime(Txt_S_Date_Text).Month < Convert.ToInt32(Subs_Start_Month) ? (Convert.ToDateTime(Txt_S_Date_Text).Year) - 1 : (Convert.ToDateTime(Txt_S_Date_Text).Year)), Convert.ToInt32(Subs_Start_Month), 1);
                for (int x = 0; x <= 10; x++)
                {
                    DateTime FDate = new DateTime(FirstDate.Year + x, Convert.ToInt32(Subs_Start_Month), 1);
                    DateTime TDate = FDate.AddMonths(Convert.ToInt32(Subs_Total_Month)).AddDays(-1);
                    Period_Data_MRR.Add(new Period_Till_MRR() { SelectedIndex_MRR = x, Period_MRR = FDate.ToString("dd MMM, yyyy") + " - " + TDate.ToString("dd MMM, yyyy") });
                }
            }
        }
        #endregion
        private bool IsDate(DateTime? date)
        {
            string text;
            if (date == null)
            {
                return false;
            }
            else
            {
                text = date.ToString();
            }
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        #region BankPayment
        [HttpGet]
        public ActionResult Frm_Voucher_Membership_Bank(string ActionMethod = null, int SrID = 0, double Amount = 0)
        {
            BankPaymentDetail_MRR model = new BankPaymentDetail_MRR();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Tag;
            model.TempActionMethod = Tag.ToString();
            model.DepBankDD_Data = LookUp_GetBankList();
            model.RefBankDD_Data = LookUp_GetRefBankList();
            if (model.DepBankDD_Data.Count == 1)
            {
                model.GLookUp_BankList_MRR = model.DepBankDD_Data[0].BA_ID;
            }
            switch (ActionMethod)
            {
                case "New":
                    model.Cmd_Mode_MRR = "CHEQUE";    
                    break;

                case "Edit":
                case "View":
                    var Sr = Convert.ToInt16(SrID);
                    DataTable all_data = BankGridData_MRR;
                    DataRow dataToEdit = all_data.AsEnumerable().SingleOrDefault(r => r.Field<int>("Sr.") == Sr);
                    model.Sr = Sr;
                    model.iBank_ID = dataToEdit["Dep_Bank_ID"].ToString();
                    model.iRef_BANK_ID = dataToEdit["Ref_Bank_ID"].ToString();
                    model.Cmd_Mode_MRR = dataToEdit["Mode"].ToString();
                    model.Txt_Ref_No_MRR = dataToEdit["No."].ToString();
                    model.Txt_Ref_Branch_MRR = dataToEdit["Branch"].ToString();
                    if (dataToEdit.IsNull("Date")==false) 
                    {
                        model.Txt_Ref_Date_MRR = Convert.ToDateTime(dataToEdit["Date"]);
                    }                  
                    if (dataToEdit.IsNull("Clearing Date")==false)
                    {                   
                        model.Txt_Ref_CDate_MRR = Convert.ToDateTime(dataToEdit["Clearing Date"]);
                    }//Redmine Bug #135456 fixed

                    model.Txt_Amount_MRR = (double)dataToEdit["Amount"];
                    model.BE_Bank_Branch_MRR = dataToEdit["Deposited Branch"].ToString();
                    model.BE_Bank_Acc_No_MRR = dataToEdit["Deposited A/c. No."].ToString();
                    model.GLookUp_BankList_MRR = model.iBank_ID;
                    model.GLookUp_RefBankList_MRR = model.iRef_BANK_ID;
                    break;

                case "Delete":
                    Return_Json_Param jsonParam = new Return_Json_Param();
                    DataTable gridRows = new DataTable();
                    if (BankGridData_MRR != null)
                    {
                        gridRows = BankGridData_MRR;
                    }
                    var grid = gridRows.AsEnumerable().SingleOrDefault(r => r.Field<int>("Sr.") == Convert.ToInt16(SrID));                  
                    gridRows.Rows.Remove(grid);
                    for (int i = 0; i < gridRows.Rows.Count; i++)
                    {
                        gridRows.Rows[i]["Sr."] = i + 1;
                    }
                    BankGridData_MRR = gridRows;
                    Bank_Amt_Calculation(true);
                    jsonParam.result = true;
                    return Json(new { jsonParam, Txt_DiffAmt = Txt_DiffAmt_MRR, Txt_BankAmt = Txt_BankAmt_MRR, Txt_CashAmt = Txt_CashAmt_MRR }, JsonRequestBehavior.AllowGet);
            }      
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Membership_Bank_Window(BankPaymentDetail_MRR model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (model.Txt_Amount_MRR <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero / Negative ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount_MRR";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_RefBankList_MRR) && (model.Cmd_Mode_MRR.ToString().Trim().ToUpper() != "BANK ACCOUNT"))
                    {
                        jsonParam.message = "Bank Not Selected ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_RefBankList_MRR";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((string.IsNullOrEmpty(model.Txt_Ref_Branch_MRR) && (model.Cmd_Mode_MRR.ToString().Trim().ToUpper() != "BANK ACCOUNT")))
                    {
                        jsonParam.message = "Bank Branch Not Specified ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_Branch_MRR";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((string.IsNullOrEmpty(model.Txt_Ref_No_MRR) && (model.Cmd_Mode_MRR.ToString().Trim().ToUpper() != "BANK ACCOUNT")))
                    {
                        jsonParam.message = "No. Not Specified ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_No_MRR";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if ((model.Txt_Ref_Date_MRR == null || IsDate(model.Txt_Ref_Date_MRR) == false) && (model.Cmd_Mode_MRR.ToString().Trim().ToUpper() != "BANK ACCOUNT"))
                    {
                        jsonParam.message = "Date Incorrect / Blank ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_Date_MRR";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_BankList_MRR) || model.GLookUp_BankList_MRR.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "Bank Not Selected ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList_MRR";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_Ref_CDate_MRR != null && (IsDate(model.Txt_Ref_CDate_MRR) == true))
                    {
                        TimeSpan ts = (Convert.ToDateTime(model.Txt_Ref_CDate_MRR)) - (Convert.ToDateTime(model.Txt_Ref_Date_MRR));
                        double diff = ts.TotalDays;
                        if (diff < 0)
                        {
                            jsonParam.message = "Clearing Date Cannot be less than Reference Date...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Ref_CDate_MRR";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return FillGrid(model, jsonParam);
                //jsonParam.result = true;
                //return Json(new
                //{
                //    jsonParam
                //}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillGrid(BankPaymentDetail_MRR model, Return_Json_Param jsonParam)
        {            
            DataTable gridRows = new DataTable();
            if (BankGridData_MRR != null)
            {
                gridRows = BankGridData_MRR;
            }
            if (model.TempActionMethod == "_New")
            {
                DataRow gridrow = gridRows.NewRow();             
                if ((gridRows.Rows.Count <= 0))
                {
                    gridrow["Sr."] = 1;
                }
                else
                {
                    gridrow["Sr."] = gridRows.Rows.Count + 1;
                }
                gridrow["Mode"] = model.Cmd_Mode_MRR;
                gridrow["Amount"] = model.Txt_Amount_MRR;
                gridrow["Bank Name"] = model.REF_BANK_NAME;
                gridrow["Branch"] = model.Txt_Ref_Branch_MRR;
                gridrow["No."] = model.Txt_Ref_No_MRR;
                if (model.Txt_Ref_Date_MRR != null) 
                {
                    gridrow["Date"] = model.Txt_Ref_Date_MRR;
                }
                if (model.Txt_Ref_CDate_MRR != null)
                {
                    gridrow["Clearing Date"] = model.Txt_Ref_CDate_MRR;
                }                
                gridrow["Ref_Bank_ID"] = model.GLookUp_RefBankList_MRR;
                gridrow["Deposited Bank"] = model.DEP_BANK_NAME;
                gridrow["Deposited Branch"] = model.BE_Bank_Branch_MRR;
                gridrow["Deposited A/c. No."] = model.BE_Bank_Acc_No_MRR;
                gridrow["Dep_Bank_ID"] = model.GLookUp_BankList_MRR;
                gridrow["BA_EDIT_ON"] = Convert.ToDateTime(model.Bank_Last_Edit_On);              
                gridRows.Rows.Add(gridrow);
            }
            else if (model.TempActionMethod == "_Edit")
            {               
                var grid = gridRows.AsEnumerable().SingleOrDefault(r => r.Field<int>("Sr.") == model.Sr);
                grid["Amount"] = model.Txt_Amount_MRR;
                grid["Mode"] = model.Cmd_Mode_MRR;
                grid["No."] = model.Txt_Ref_No_MRR;
                if (model.Txt_Ref_Date_MRR != null)
                {
                    grid["Date"] = model.Txt_Ref_Date_MRR;
                }
                if (model.Txt_Ref_CDate_MRR != null)
                {
                    grid["Clearing Date"] = model.Txt_Ref_CDate_MRR;
                }
                grid["Ref_Bank_ID"] = model.GLookUp_RefBankList_MRR;
                grid["Branch"] = model.Txt_Ref_Branch_MRR;
                grid["Bank Name"] = model.REF_BANK_NAME;

                grid["Dep_Bank_ID"] = model.GLookUp_BankList_MRR;
                grid["Deposited Bank"] = model.DEP_BANK_NAME;
                grid["Deposited A/c. No."] = model.BE_Bank_Acc_No_MRR;
                grid["Deposited Branch"] = model.BE_Bank_Branch_MRR;
                grid["BA_EDIT_ON"] = Convert.ToDateTime(model.Bank_Last_Edit_On);
            }
            BankGridData_MRR = gridRows;
            if (model.TempActionMethod == "_New" || model.TempActionMethod == "_Edit")
            {
                Bank_Amt_Calculation(false);
            }
            else
            {
                Bank_Amt_Calculation(true);
            }
            jsonParam.result = true;
            return Json(new { jsonParam, Txt_DiffAmt = Txt_DiffAmt_MRR, Txt_BankAmt = Txt_BankAmt_MRR, Txt_CashAmt = Txt_CashAmt_MRR }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Voucher_Membership_Bank_Grid()
        {
            var data = (BankGridData_MRR == null) ? new DataTable() : BankGridData_MRR;
            int count = 1;
            foreach (DataRow item in data.Rows)
            {
                item["Sr."] = count;
                count++;
            }
            return PartialView(data);
        }
        public List<BankList_MRR> LookUp_GetBankList()
        {
            DataTable BA_Table = BASE._Payment_DBOps.GetBankAccounts();          
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs += "'" + xRow["BA_BRANCH_ID"].ToString() + "',";
            }
            if (Branch_IDs.Trim().Length > 0)
            {
                Branch_IDs = Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim();
            }
            if (Branch_IDs.Trim().Length == 0)
            {
                Branch_IDs = "''";
            }
            DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);         
            var BuildData = from B in BB_Table.AsEnumerable()
                            join A in BA_Table.AsEnumerable()
                                   on B["BB_BRANCH_ID"] equals A["BA_BRANCH_ID"]
                            select new BankList_MRR
                            {
                                BANK_NAME = B.Field<string>("Name"),
                                BI_SHORT_NAME = B.Field<string>("BI_SHORT_NAME"),
                                BANK_BRANCH = B.Field<string>("Branch"),
                                BANK_ACC_NO = A.Field<string>("BA_ACCOUNT_NO"),
                                BA_ID = A.Field<string>("ID"),
                                BANK_ID = B.Field<string>("BANK_ID"),
                                REC_EDIT_ON = A.Field<DateTime>("REC_EDIT_ON")
                            };
            var finalData = BuildData.ToList();
           return  finalData.OrderBy(x => x.BANK_NAME).ToList();
         
        }     
        public List<Return_RefBank> LookUp_GetRefBankList()
        {
            var B2 = BASE._Payment_DBOps.GetBanks();          
            return B2.OrderBy(x => x.BI_BANK_NAME).ToList();           
        } 
        public ActionResult Sub_Amt_Calculation(string Subs_Cat = "", string Subs_Start_Month = "", string Txt_S_Date_Text = "", string GLookUp_SubList_Text = "", int Cmb_Period_SelectedIndex = 0)
        {
            string Cmb_Period_Text = Period_Data_MRR.Where(x => x.SelectedIndex_MRR == Cmb_Period_SelectedIndex).FirstOrDefault().Period_MRR;
            if (Subs_Cat.ToUpper() == "LIFETIME")
            {
                int sMonth = Convert.ToDateTime(Txt_S_Date_Text).Month;
                int sYear = Convert.ToDateTime(Txt_S_Date_Text).Year;
                DateTime FirstDate = new DateTime((sMonth < Convert.ToInt32(Subs_Start_Month) ? sYear - 1 : sYear), Convert.ToInt32(Subs_Start_Month), 1);
                TimeSpan ts = FirstDate - BASE._open_Year_Edt;
                int X = Convert.ToInt32(ts.TotalDays);
                if (X > 0)  //--> FEE IN ADVANCE
                {
                    Ent_Fee_MRR = Convert.ToDecimal(Default_Ent_Fee.ToString("#0.00"));
                    Subs_Fee_MRR = 0.00M;
                    Txt_Adv_Fee_MRR = Convert.ToDecimal((Default_Subs_Fee * (Cmb_Period_SelectedIndex + 1)).ToString("#0.00"));
                }
                else
                {
                    Ent_Fee_MRR = Convert.ToDecimal(Default_Ent_Fee.ToString("#0.00"));
                    Subs_Fee_MRR = Convert.ToDecimal(Default_Subs_Fee.ToString("#0.00"));
                    Txt_Adv_Fee_MRR = 0.00M;
                }
            }
            else
            {
                if (Period_Data_MRR.Count > 0)
                {
                    var fdd = Period_Data_MRR[0].Period_MRR.Substring(0, 2);
                    var fmm = Period_Data_MRR[0].Period_MRR.ToString().Substring(3, 3).ToUpper();
                    var fyy = Period_Data_MRR[0].Period_MRR.ToString().Substring(8, 4).ToUpper();
                    int MonthStringToNumber = DateTime.ParseExact(fmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                    DateTime Fdate = new DateTime(Convert.ToInt32(fyy), MonthStringToNumber, Convert.ToInt32(fdd));
                    TimeSpan ts = Fdate - BASE._open_Year_Edt;
                    int X = Convert.ToInt32(ts.TotalDays);
                    if (X > 0)   //--> FEE IN ADVANCE
                    {
                        Ent_Fee_MRR = Convert.ToDecimal(Default_Ent_Fee.ToString("#0.00"));
                        Subs_Fee_MRR = 0.00M;
                        Txt_Adv_Fee_MRR = Convert.ToDecimal((Default_Subs_Fee * (Cmb_Period_SelectedIndex + 1)).ToString("#0.00"));
                    }
                    else
                    {
                        Ent_Fee_MRR = Convert.ToDecimal(Default_Ent_Fee.ToString("#0.00"));
                        Subs_Fee_MRR = Convert.ToDecimal(Default_Subs_Fee.ToString("#0.00"));
                        Txt_Adv_Fee_MRR = Convert.ToDecimal((Default_Subs_Fee * (Cmb_Period_SelectedIndex)).ToString("#0.00"));
                    }
                }
            }
            Txt_SubTotal_MRR = Convert.ToDecimal((Ent_Fee_MRR + Subs_Fee_MRR + Txt_Adv_Fee_MRR).ToString("#0.00"));
            if (Txt_BankAmt_MRR > 0)
            {
                Txt_DiffAmt_MRR = Convert.ToDecimal((Txt_SubTotal_MRR - (Txt_CashAmt_MRR + Txt_BankAmt_MRR)).ToString("#0.00"));
            }
            else
            {
                Txt_DiffAmt_MRR = 0.00M;
                Txt_CashAmt_MRR = Convert.ToDecimal(Txt_SubTotal_MRR.ToString("#0.00"));
            }
            Difference_Calculation();
            if (IsDate(Convert.ToDateTime(Txt_S_Date_Text)) && GLookUp_SubList_Text.Length > 0 && Cmb_Period_Text.Length > 0 && Subs_Cat.ToUpper() != "LIFETIME")
            {
                var tdd = Cmb_Period_Text.Substring(15, 2);
                var tmm = Cmb_Period_Text.Substring(18, 3).ToUpper();
                var tyy = Cmb_Period_Text.Substring(23, 4);
                int MonthStringToNumbertmm = DateTime.ParseExact(tmm, "MMM", CultureInfo.CreateSpecificCulture("en-GB")).Month;
                DateTime Tdate = new DateTime(Convert.ToInt32(tyy), MonthStringToNumbertmm, Convert.ToInt32(tdd));
                TimeSpan ts = Tdate - DateTime.Now.Date;
                double exp = ts.TotalDays;
                lbl_Expire_Text = "Subscription Expires in " + (exp + 1) + " day(s) from today.";
            }
            else
            {
                lbl_Expire_Text = "Subscription Never Expire.";
            }
            return Json(new
            {
                result = true,
                Ent_Fee_MRR,
                Subs_Fee_MRR,
                Txt_Adv_Fee_MRR,
                Txt_SubTotal_MRR,
                Txt_DiffAmt_MRR,
                Txt_CashAmt_MRR,
                lbl_Expire_Text
            }, JsonRequestBehavior.AllowGet);
        }
        public void Bank_Amt_Calculation(bool Delete_Action)
        {
            DataView dv = BankGridData_MRR.DefaultView;
            dv.Sort = "Sr.";
            BankGridData_MRR = dv.ToTable();
            double xAmt = 0;
            if (BankGridData_MRR != null)
            {
                if (BankGridData_MRR.Rows.Count > 0)
                {
                    for (int i = 0; i < BankGridData_MRR.Rows.Count; i++)
                    {
                        if (Delete_Action)
                        {
                            BankGridData_MRR.Rows[i]["Sr."] = i + 1;
                        }
                        xAmt += Convert.ToDouble(BankGridData_MRR.Rows[i]["Amount"]);
                    }
                }
            }
            Txt_BankAmt_MRR = Convert.ToDecimal(xAmt.ToString("#0.00"));
            Difference_Calculation();
        }
        public void Difference_Calculation()
        {
            if (Txt_BankAmt_MRR > 0)
            {
                Txt_CashAmt_MRR = 0.00M;
                Txt_DiffAmt_MRR = Convert.ToDecimal((Txt_SubTotal_MRR - (Txt_CashAmt_MRR + Txt_BankAmt_MRR)).ToString("#0.00"));
            }
            else
            {
                Txt_DiffAmt_MRR = 0.00M;
                Txt_CashAmt_MRR = Convert.ToDecimal(Txt_SubTotal_MRR.ToString("#0.00"));
            }
        }
        #endregion
        public void ResetStaticVariable()
        {
            Txt_DiffAmt_MRR = 0.00M;
            Txt_SubTotal_MRR = 0.00M;
            Txt_CashAmt_MRR = 0.00M;
            Txt_CreditAmt_MRR = 0.00M;
            Txt_BankAmt_MRR = 0.00M;
            MRR_Edit_MEM_OLD_NO_MRR = "";
            Edit_MEM_NO = "";
            Edit_AB_ID = "";
            Edit_WING_ID = "";
        }
        public void SetGridData()
        {
            ItemGridData = new DataTable();
            ItemGridData.TableName = "Item_Detail";
            ItemGridData.Columns.Add("Sr.", Type.GetType("System.Int32"));
            ItemGridData.Columns.Add("Item_ID", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Item_Led_ID", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Item_Trans_Type", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Item_Party_Req", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Item_Profile", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Item Name", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Head", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Qty.", Type.GetType("System.Double"));
            ItemGridData.Columns.Add("Unit", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Rate", Type.GetType("System.Double"));
            ItemGridData.Columns.Add("Amount", Type.GetType("System.Double"));
            ItemGridData.Columns.Add("Remarks", Type.GetType("System.String"));
            ItemGridData.Columns.Add("Pur_ID", Type.GetType("System.String"));

            BankGridData_MRR = new DataTable();
            BankGridData_MRR.TableName = "Bank_Details";
            BankGridData_MRR.Columns.Add("Sr.", Type.GetType("System.Int32"));
            BankGridData_MRR.Columns.Add("Mode", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Amount", Type.GetType("System.Double"));
            BankGridData_MRR.Columns.Add("Bank Name", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Branch", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("No.", Type.GetType("System.String"));
           // BankGridData_MRR.Columns.Add("Date", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Date", typeof(DateTime));
            //BankGridData_MRR.Columns.Add("Clearing Date", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Clearing Date", typeof(DateTime));
            BankGridData_MRR.Columns.Add("Ref_Bank_ID", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Deposited Bank", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Deposited Branch", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Deposited A/c. No.", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("Dep_Bank_ID", Type.GetType("System.String"));
            BankGridData_MRR.Columns.Add("BA_EDIT_ON", Type.GetType("System.DateTime"));
        }

        #region Frm_View_Summary

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
                return Content("<script language='javascript' type='text/javascript'> MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
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
            MRR_SummaryGridData = SummaryGridData;
            return PartialView(SummaryGridData);
        }
        public ActionResult Frm_View_SummaryGrid()
        {
            return PartialView(MRR_SummaryGridData);
        }
        public void Frm_View_SummaryGrid_Close()
        {
            BASE._SessionDictionary.Remove("MRR_SummaryGridData_MRR");
        }
        #region Export
        public ActionResult Frm_Membership_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');$('#Print').hide();</script>");
            }
            return View();
        }
        #endregion

        #endregion
        public void SessionClear()
        {
            ClearBaseSession("_MRRInfo");
        }
        public void SessionClear_MRRWindow()
        {
            ClearBaseSession("_MRR");
        }
        public void Membership_user_rights()
        {
            ViewData["MembershipRR_AddRight"] = CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "Add");
            ViewData["MembershipRR_RenewalRight"] = CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "Renewal");
            ViewData["MembershipRR_ConversionRight"] = CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "Conversion");
            ViewData["MembershipRR_UpdateRight"] = CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "Update");
            ViewData["MembershipRR_ViewRight"] = CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "View");
            ViewData["MembershipRR_DeleteRight"] = CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "Delete");
            ViewData["MembershipRR_ExportRight"] = CheckRights(BASE, ClientScreen.Membership_Receipt_Register, "Export");
            ViewData["MembershipRR_ListAdresBookRight"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            ViewData["MembershipRR_LockUnlockRight"] = BASE.CheckActionRights(ClientScreen.Membership_Receipt_Register, Common.ClientAction.Lock_Unlock);

            ViewData["Help_Attachment_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");  //attachment
            ViewData["Help_Attachment_EditRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");//attachment
            ViewData["Help_Attachment_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");//attachment
            ViewData["Help_Attachment_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");//attachment
            ViewData["MembershipRR_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                       || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
        }
        public DateTime _xFr_Date()
        {
            return Convert.ToDateTime(xFr_Date);
        }
        public DateTime _xTo_Date()
        {
            return Convert.ToDateTime(xTo_Date);
        }
        private bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            System.Web.HttpContext context = System.Web.HttpContext.Current;

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
    }
}