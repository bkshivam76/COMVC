using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
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

namespace ConnectOneMVC.Areas.Reports.Controllers
{
    [CheckLogin]
    public class MultiBankCashBookController : BaseController
    {
        // GET: Reports/MultiBankCashBook
        public ActionResult Frm_MultiBank_Cashbook()
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_CashBook, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            ViewData["CashBook_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_CashBook, "Export");
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.self_posted = BASE._open_User_Self_Posted;
            ViewBag.CB_ShowHorizontalBar = BASE._List_Of_FullData_Screen.Any(s => s.Equals((ClientScreen.Accounts_CashBook).ToString(), StringComparison.OrdinalIgnoreCase)) ? 1 : 0;
            ViewBag.OpenYearSdt = BASE._open_Year_Sdt;
            ViewBag.OpenYearEdt = BASE._open_Year_Edt;
            ViewBag._IsVolumeCenter = BASE._IsVolumeCenter;
            object MaxValue = 0;
            DateTime xLastDate = DateTime.Now;
            MaxValue = BASE._Voucher_DBOps.GetMaxTransactionDate();
            if (MaxValue == null)
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!'); btnCloseActiveTab();</script>");
            }
            if (Convert.IsDBNull(MaxValue))
            {
                xLastDate = BASE._open_Year_Sdt;
            }
            else
            {
                xLastDate = Convert.ToDateTime(MaxValue);
            }
            int xMM = xLastDate.Month;
            ViewBag.PeriodSelectedIndex = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;            
            return View();
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(FillChangePeriod(), loadOptions)), "application/json");
        }
        public List<CB_Period> FillChangePeriod()
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
            return period;
        }
        public ActionResult Frm_MultiBank_Cashbook_Specific_Period(bool PeriodSelectionFirstTime = false, string xFrDate = "", string xToDate = "")
        {
            ViewBag.PeriodSelectionFirstTime = PeriodSelectionFirstTime;
            CB_SpeceficPeriod model = new CB_SpeceficPeriod();
            model.CB_Fromdate = Convert.ToDateTime(xFrDate);
            model.CB_Todate = Convert.ToDateTime(xToDate);
            return View(model);
        }
        public ActionResult GetMultiBankCashBookData(string xFrDate = "", string xToDate = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();       
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            DateTime _xFr_Date = Convert.ToDateTime(xFrDate);
            DateTime _xTo_Date = Convert.ToDateTime(xToDate);
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date, _xTo_Date, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
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
            if (Cash_Bal.Rows.Count > 0 && BASE._open_User_Self_Posted == false)
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
            double Open_Bank_Bal = 0; 
            double Close_Bank_Bal = 0;
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date, _xTo_Date, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            int _bankCnt = 1;
            string _online_BANK_COL_TR_REC = "";
            string _online_BANK_COL_NB_REC = "";
            string _online_BANK_COL_TR_PAY = "";
            string _online_BANK_COL_NB_PAY = "";
            string _local__BANK_COL_TR_REC = "";
            string _local__BANK_COL_NB_REC = "";
            string _local__BANK_COL_TR_PAY = "";
            string _local__BANK_COL_NB_PAY = "";          
            int count = Bank_Bal.Rows.Count;
            if (count > 0 && BASE._open_User_Self_Posted == false)
            {
                for (int i = 0; i < count; i++)
                {
                    DataRow XROW = Bank_Bal.Rows[i];
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
                    _bankCnt += 1;
                }
            }
            else
            {
                Open_Bank_Bal = 0;
                Close_Bank_Bal = 0; 
            }
            string Advanced_Filter_Category = "";
            string Advanced_Filter_RefID = "";
            Param_Vouchers_GetListWithMultipleParams Param = new Param_Vouchers_GetListWithMultipleParams();
            Param.FromDate = _xFr_Date;
            Param.Local_Bank_Col_NB_Pay = _local__BANK_COL_NB_PAY;
            Param.Local_Bank_Col_NB_Rec = _local__BANK_COL_NB_REC;
            Param.Local_Bank_Col_TR_Pay = _local__BANK_COL_TR_PAY;
            Param.Local_Bank_Col_TR_Rec = _local__BANK_COL_TR_REC;
            Param.Online_Bank_Col_NB_Pay = _online_BANK_COL_NB_PAY;
            Param.Online_Bank_Col_NB_Rec = _online_BANK_COL_NB_REC;
            Param.Online_Bank_Col_TR_Pay = _online_BANK_COL_TR_PAY;
            Param.Online_Bank_Col_TR_Rec = _online_BANK_COL_TR_REC;
            Param.openYearEdt = BASE._open_Year_Edt;
            Param.openYearSdt = BASE._open_Year_Sdt;
            Param.ToDate = _xTo_Date;
            Param.Advanced_Filter_Category = Advanced_Filter_Category;
            Param.Advanced_Filter_Ref_ID = Advanced_Filter_RefID;
            Param.showDynamicBankColumns = false;           
            DataTable TR_Table = BASE._Reports_Common_DBOps.MultiBankCashBookReport(Param);
            if (TR_Table == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            DataSet Voucher_DS = new DataSet();
            Voucher_DS.Tables.Add(TR_Table.Copy());
            Voucher_DS.Tables.Add(Bank_Bal.Copy());
            DataRelation BANK_Relation = Voucher_DS.Relations.Add("BANK_ACC", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_SUB_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            DataRelation BANK_Relation2 = Voucher_DS.Relations.Add("BANK_ACC2", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_CR_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            count = Voucher_DS.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow XROW = Voucher_DS.Tables[0].Rows[i];
                DataRow[] bankrelation_childrows = XROW.GetChildRows(BANK_Relation);
                int bank_relationcount = bankrelation_childrows.Count();
                for (int j = 0; j < bank_relationcount; j++)
                {
                    DataRow _Row = bankrelation_childrows[j];
                    if (XROW["iTR_PARTY_1"].ToString().Length <= 0)
                    {
                        XROW["iTR_PARTY_1"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
                DataRow[] bankrelation2_childrows = XROW.GetChildRows(BANK_Relation2);
                int bank_relation2count = bankrelation2_childrows.Count();
                for (int k = 0; k < bank_relation2count; k++)
                {
                    DataRow _Row = bankrelation2_childrows[k];
                    if (XROW["iTR_CR_NAME"].ToString().Length <= 0)
                    {
                        XROW["iTR_CR_NAME"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
            }
            Voucher_DS.Relations.Clear();
            int _Date_Serial = 0;
            string _Date_Show = "";
            if (Convert.ToInt32(_xFr_Date.ToString("MM")) > 3)
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date.AddMonths(-3).ToString("MM"));
                _Date_Show = BASE._open_Year_Sdt.ToString("yyyy") + "-" + string.Format(_xFr_Date.ToString("MM"), "00") + "-01";
            }
            else
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date.AddMonths(+9).ToString("MM"));
                _Date_Show = BASE._open_Year_Edt.ToString("yyyy") + "-" + string.Format(_xFr_Date.ToString("MM"), "00") + "-01";
            }
            DataRow ROW = default(DataRow);
            ROW = Voucher_DS.Tables[0].NewRow();
            ROW["iTR_DATE_SERIAL"] = _Date_Serial;
            ROW["iTR_DATE_SHOW"] = _Date_Show;
            ROW["iTR_TEMP_ID"] = "OPENING BALANCE";
            ROW["iREC_ID"] = "OPENING BALANCE";
            ROW["iTR_ROW_POS"] = "A";
            ROW["iTR_VNO"] = "";
            ROW["iTR_DATE"] = string.Format(_xFr_Date.ToString(), BASE._Date_Format_Current);
            ROW["iTR_REC_CASH"] = Open_Cash_Bal;
            ROW["iTR_REC_BANK"] = Open_Bank_Bal;
            ROW["iTR_ITEM"] = "OPENING BALANCE";
            for (int i = 0; i < Bank_Bal.Rows.Count; i++)
            {
                string accountNo = Bank_Bal.Rows[i]["BA_ACCOUNT_NO"].ToString();
                string columnName = "Dyn_Rec_" + Bank_Bal.Rows[i]["BI_SHORT_NAME"].ToString() + accountNo.Substring(accountNo.Length - 4);
                int openingValue = Convert.ToInt32(Bank_Bal.Rows[i]["OPENING"]);
                if (TR_Table.Columns.Contains(columnName))
                {
                    ROW[columnName] = openingValue;
                }
            }
            //if ((string)REC_BANK01_Tag == "YES")
            //{
            //    ROW["REC_BANK01"] = Open_Bank_Bal01;
            //}
            //if ((string)REC_BANK02_Tag == "YES")
            //{
            //    ROW["REC_BANK02"] = Open_Bank_Bal02;
            //}
            //if ((string)REC_BANK03_Tag == "YES")
            //{
            //    ROW["REC_BANK03"] = Open_Bank_Bal03;
            //}
            //if ((string)REC_BANK04_Tag == "YES")
            //{
            //    ROW["REC_BANK04"] = Open_Bank_Bal04;
            //}
            //if ((string)REC_BANK05_Tag == "YES")
            //{
            //    ROW["REC_BANK05"] = Open_Bank_Bal05;
            //}
            //if ((string)REC_BANK06_Tag == "YES")
            //{
            //    ROW["REC_BANK06"] = Open_Bank_Bal06;
            //}
            //if ((string)REC_BANK07_Tag == "YES")
            //{
            //    ROW["REC_BANK07"] = Open_Bank_Bal07;
            //}
            //if ((string)REC_BANK08_Tag == "YES")
            //{
            //    ROW["REC_BANK08"] = Open_Bank_Bal08;
            //}
            //if ((string)REC_BANK09_Tag == "YES")
            //{
            //    ROW["REC_BANK09"] = Open_Bank_Bal09;
            //}
            //if ((string)REC_BANK10_Tag == "YES")
            //{
            //    ROW["REC_BANK10"] = Open_Bank_Bal10;
            //}
            Voucher_DS.Tables[0].Rows.Add(ROW);
            DataView DV1 = new DataView(Voucher_DS.Tables[0]);
            DV1.Sort = "iTR_DATE,iTR_ROW_POS,iTR_ENTRY,iREC_ADD_ON,iTR_M_ID,iTR_SORT,iTR_SR_NO";
            DataTable XTABLE = DV1.ToTable();
            string _TEMP = "";
            if (XTABLE.Rows.Count > 0)
            {
                _TEMP = DV1.ToTable().Rows[0]["iTR_TEMP_ID"].ToString();
            }       
            if (XTABLE.Columns.Contains("Grid_PK") == false)
            {
                XTABLE.Columns.Add("Grid_PK", typeof(System.String));
            }
            double _SR = 1;     
            var TotalRowCount = XTABLE.Rows.Count;
            for (int i = 0; i < TotalRowCount; i++)
            {
                DataRow Row = XTABLE.Rows[i];
                if ((string)Row["iTR_TEMP_ID"] == _TEMP)
                {
                    Row["iTR_REF_NO"] = _SR;
                }
                else
                {
                    _TEMP = Row["iTR_TEMP_ID"].ToString();
                    _SR = _SR + 1;
                    Row["iTR_REF_NO"] = _SR;
                }            
                string Grid_PK = "";
                string iREC_ID = Row.Field<string>("iREC_ID");
                string iTR_M_ID = Row.Field<string>("iTR_M_ID");
                string iTR_ITEM_ID = Row.Field<string>("iTR_ITEM_ID");
                if (iREC_ID == "NOTE-BOOK")
                {
                    Grid_PK = (string.IsNullOrEmpty(iTR_M_ID) ? "Null" : iTR_M_ID) + (string.IsNullOrEmpty(iREC_ID) ? "Null" : iTR_ITEM_ID);
                }
                else
                {
                    Grid_PK = (string.IsNullOrEmpty(iTR_M_ID) ? "Null" : iTR_M_ID) + (string.IsNullOrEmpty(iREC_ID) ? "Null" : iREC_ID);
                }
                Row["Grid_PK"] = Grid_PK;    
            }

            jsonParam.message = "";
            jsonParam.title = "";
            jsonParam.result = true;
            jsonParam.closeform = false;
            jsonParam.Next_Unattended_Attachment_Index = -1;
            var result = new
            {
                jsonParam,
                Close_Bank_Bal,
                Close_Cash_Bal,
                TotalRowCount,
                ShowRECBank = true,
                ShowPAYBank = true,              
                GridData = XTABLE
            };
            return Content(JsonConvert.SerializeObject(result), "application/json");
            //return Json(new
            //{
            //    jsonParam,
            //    Close_Bank_Bal,
            //    Close_Cash_Bal,
            //    TotalRowCount,         
            //    ShowRECBank = true,
            //    ShowPAYBank = true,
            //    GridData = JsonConvert.SerializeObject(XTABLE)
            //}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Export_Options(string GridName="")
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_CashBook, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_modal','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove                
            }
            ViewBag.Filename = "MultiBankCashBook_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            ViewBag.GridName = GridName;
            return PartialView();
        }
    }
}