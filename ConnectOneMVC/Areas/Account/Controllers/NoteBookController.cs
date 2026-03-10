using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class NoteBookController : BaseController
    {
        public DateTime? xFr_Date
        {
            get { return (DateTime?)GetBaseSession("xFr_Date_Notebook"); }
            set { SetBaseSession("xFr_Date_Notebook", value); }
        }
        public DateTime? xTo_Date
        {
            get { return (DateTime?)GetBaseSession("xTo_Date_Notebook"); }
            set { SetBaseSession("xTo_Date_Notebook", value); }
        }
        public DataTable TR_Table
        {
            get { return (DataTable)GetBaseSession("TR_Table_Notebook"); }
            set { SetBaseSession("TR_Table_Notebook", value); }
        }
        public DataTable SaveData
        {
            get { return (DataTable)GetBaseSession("SaveData_Notebook"); }
            set { SetBaseSession("SaveData_Notebook", value); }
        }
        public string BE_Cash_Bank_Text
        {
            get { return (string)GetBaseSession("BE_Cash_Bank_Text_Notebook"); }
            set { SetBaseSession("BE_Cash_Bank_Text_Notebook", value); }
        }
        public List<NoteBook_Grid> NoteBook_Data
        {
            get { return (List<NoteBook_Grid>)GetBaseSession("NoteBook_Data_Notebook"); }
            set { SetBaseSession("NoteBook_Data_Notebook", value); }
        }
        public List<Summary> NB_SummaryGridData
        {
            get { return (List<Summary>)GetBaseSession("NB_SummaryGridData_Notebook"); }
            set { SetBaseSession("NB_SummaryGridData_Notebook", value); }
        }
        public List<NoteBook_Period> NB_PeriodSelectionData
        {
            get { return (List<NoteBook_Period>)GetBaseSession("NB_PeriodSelectionData_Notebook"); }
            set { SetBaseSession("NB_PeriodSelectionData_Notebook", value); }
        }
        public ActionResult Frm_NoteBook_Info(string Tag = null, string xEntryDate = null, string GridToRefresh = "CashBookListGrid")
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_Notebook, "Add")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            ViewBag.GridToRefresh = GridToRefresh;
            ViewData["Accnt_Notebook_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_Notebook, "Export");
            ViewBag.Open_From_CB = xEntryDate == null ? false : true;

            NoteBook_Info model = new NoteBook_Info();

            model.PeriodData = Fill_Change_Period_Items();
            NB_PeriodSelectionData = model.PeriodData;

            model.ActionMethod = Tag;
            ViewBag.Set_Disable = false;
            if (model.ActionMethod == "_View")
            {
                ViewBag.Set_Disable = true;
            }

            model.xEntryDate = xEntryDate == null ? (DateTime?)null : Convert.ToDateTime(xEntryDate);
            model.Lock_unlockRight = BASE.CheckActionRights(ClientScreen.Accounts_Notebook, Common.ClientAction.Lock_Unlock);

            DateTime xLastDate = DateTime.Now;
            if (Tag == "_Edit" || Tag == "_View")
            {
                xLastDate = Convert.ToDateTime(xEntryDate);
            }
            else
            {
                object MaxValue = 0;
                MaxValue = BASE._NoteBook_DBOps.GetMaxTransactionDate();
                if (MaxValue == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                if (Convert.IsDBNull(MaxValue))
                {
                    xLastDate = BASE._open_Year_Sdt;
                }
                else
                {
                    xLastDate = Convert.ToDateTime(MaxValue);
                }
            }
            int xMM = xLastDate.Month;
            int index = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            model.Cmb_View_SelectedIndex = index;
            dynamic data = Cmb_View_SelectedIndexChanged(index);
            model.TotalDays = data.Data.TotalDays;
            model.Periodtext = data.Data.Periodtext;
            model.GridBand2_Caption = data.Data.GridBand2_Caption;
            model.fr_date = data.Data.fr_date;
            ViewData["BE_Cash_Bank_Text"] = Get_Cash_Bank_Balance();
            TR_Table = BASE._NoteBook_DBOps.GetList(Convert.ToDateTime(xFr_Date).Month);
            SaveData = TR_Table.Copy();
            model.NoteBookData = ConvertGridToList(TR_Table);
            NoteBook_Data = model.NoteBookData;
            ViewBag.TotalDays = model.TotalDays;
            ViewBag.Band2_Caption = model.GridBand2_Caption;
            return View(model);
        }
        public ActionResult Frm_NoteBook_Info_Grid(string command, int? TotalDays = null, string Band_Caption = null, bool setDisable = false, string ChangedValue = null)
        {
            ViewBag.Set_Disable = setDisable;
            ViewBag.TotalDays = TotalDays;
            ViewBag.Band2_Caption = Band_Caption;
            ViewData["Accnt_Notebook_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_Notebook, "Export");
            ViewData["BE_Cash_Bank_Text"] = BE_Cash_Bank_Text;
            if (string.IsNullOrWhiteSpace(ChangedValue) == false && ChangedValue != "[]")
            {
                List<NoteBook_ChangedValue> updateValues = JsonConvert.DeserializeObject<List<NoteBook_ChangedValue>>(ChangedValue);
                foreach (var item in updateValues)
                {
                    UpdateCellData(item.Key, item.Field, (decimal?)item.Value);
                }
            }
            if (NoteBook_Data == null || command == "REFRESH")
            {
                ViewData["BE_Cash_Bank_Text"] = Get_Cash_Bank_Balance();
                TR_Table = BASE._NoteBook_DBOps.GetList(Convert.ToDateTime(xFr_Date).Month);
                if (TR_Table == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                SaveData = TR_Table.Copy();
                List<NoteBook_Grid> Data = ConvertGridToList(TR_Table);
                NoteBook_Data = Data;
            }
            return View("Frm_NoteBook_Info_Grid", NoteBook_Data);
        }
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<NoteBook_Grid, int> updateValues, string command, int? TotalDays = null, string Band_Caption = null, bool setDisable = false)
        {
            foreach (NoteBook_Grid updateRow in updateValues.Update)
            {
                int index = NoteBook_Data.FindIndex(x => x.REC_ID == updateRow.REC_ID);
                var oldData = NoteBook_Data[index];
                NoteBook_Data[index] = updateRow;
                NoteBook_Data[index].LED_NAME = oldData.LED_NAME;
                NoteBook_Data[index].ITEM_TRANS_TYPE = oldData.ITEM_TRANS_TYPE;
                NoteBook_Data[index].ITEM_LED_ID = oldData.ITEM_LED_ID;

            }
            return Frm_NoteBook_Info_Grid(command, TotalDays, Band_Caption, setDisable);
        }
        [HttpPost]
        public ActionResult ReportGenerator(int? TotalDays = null, string Band_Caption = null, bool Set_Disable = false, string PeriodText = "", string ChangedValue = null)
        {
            if (string.IsNullOrWhiteSpace(ChangedValue) == false && ChangedValue != "[]")
            {
                List<NoteBook_ChangedValue> updateValues = JsonConvert.DeserializeObject<List<NoteBook_ChangedValue>>(ChangedValue);
                foreach (var item in updateValues)
                {
                    UpdateCellData(item.Key, item.Field, (decimal?)item.Value);
                }
            }
            string Incharge = "";
            DataTable Centre_Inc = BASE._Report_DBOps.GetCenterDetails();
            if (Convert.IsDBNull(Centre_Inc.Rows[0]["CEN_INCHARGE"]) == false)
            {
                Incharge = Centre_Inc.Rows[0]["CEN_INCHARGE"].ToString();
            }
            var Reoprt = GridViewExportHelper.Show_ListPreview(GridExportSettings(TotalDays, Band_Caption, PeriodText, Incharge), NoteBook_Data, "Note Book  (UID: " + BASE._open_UID_No + ")", true, System.Drawing.Printing.PaperKind.A4, true, 50, 75);
            return View("Frm_Export_Options", Reoprt);
        }
        public GridViewSettings GridExportSettings(int? TotalDays = null, string Band_Caption = null, string PeriodText = "", string Incharge = "")
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "NoteBookListGrid";
            settings.Styles.Cell.ForeColor = System.Drawing.Color.Black;
            settings.Styles.Cell.Font.Name = "Tahoma";
            settings.Styles.Cell.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8.25");
            settings.KeyFieldName = "REC_ID";
            settings.Columns.AddBand(ParticularBand =>
            {
                ParticularBand.ShowInCustomizationForm = true;
                ParticularBand.Caption = "Particulars";
                ParticularBand.HeaderStyle.Font.Bold = true;
                ParticularBand.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                ParticularBand.Columns.Add(column =>
                {
                    column.FieldName = "ITEM_NAME";
                    column.Caption = "Item Name";
                    column.Width = 200;
                    column.FixedStyle = GridViewColumnFixedStyle.Left;
                    column.Settings.ShowEditorInBatchEditMode = false;
                    ASPxSummaryItem summaryItem = new ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Count);                   
                    summaryItem.DisplayFormat = "Total:";
                    settings.TotalSummary.Add(summaryItem);       
                });
            });
            settings.Columns.AddBand(GrandBand =>
            {
                GrandBand.ShowInCustomizationForm = true;
                GrandBand.Caption = "Grand";
                GrandBand.HeaderStyle.Font.Bold = true;
                GrandBand.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                GrandBand.Columns.Add(column =>
                {
                    column.FieldName = "AMT_TOTAL";
                    column.PropertiesEdit.DisplayFormatString = "#0";
                    column.Caption = "Total";
                    column.Name = "Total";
                    column.Width = 80;
                    column.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
                    column.FixedStyle = GridViewColumnFixedStyle.Left;
                    column.Settings.ShowEditorInBatchEditMode = false;
                    ASPxSummaryItem summaryItem = new ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Sum);           
                    summaryItem.DisplayFormat = "0";
                    settings.TotalSummary.Add(summaryItem);
                });
            });
            settings.Columns.AddBand(DayBand =>
            {
                DayBand.ShowInCustomizationForm = true;
                DayBand.Caption = Band_Caption;
                DayBand.HeaderStyle.Font.Bold = true;
                DayBand.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                for (int i = 1; i <= TotalDays; i++) 
                {
                    DayBand.Columns.Add(column =>
                    {
                        column.FieldName = "AMT_DT_"+i.ToString("D2");
                        column.Caption = i.ToString();
                        column.Name = i.ToString();
                        column.PropertiesEdit.DisplayFormatString = "#";
                        column.Width = 60;
                        column.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;                    
                        column.ExportWidth = 30;              

                        ASPxSummaryItem summaryItem = new ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Sum);                     
                        summaryItem.DisplayFormat = "0";
                        settings.TotalSummary.Add(summaryItem);
                    });
                }       
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "REC_ID";
                column.Caption = "REC_ID";
                column.Visible = false;
                column.Settings.ShowEditorInBatchEditMode = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "ITEM_LED_ID";
                column.Caption = "ITEM_LED_ID";
                column.Visible = false;
                column.Settings.ShowEditorInBatchEditMode = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "LED_NAME";
                column.Caption = "LED_NAME";
                column.Visible = false;
                column.Settings.ShowEditorInBatchEditMode = false;
            });
            settings.Columns.Add(column =>
            {
                column.FieldName = "ITEM_TRANS_TYPE";
                column.Caption = "ITEM_TRANS_TYPE";
                column.Visible = false;
                column.Settings.ShowEditorInBatchEditMode = false;
            });            
            settings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            settings.Settings.ShowFooter = true;

            MyExtensions.GridViewHelper.GridViewExportSettings(settings, "Note Book  (UID: " + BASE._open_UID_No + ")", true, System.Drawing.Printing.PaperKind.A4, "Note-Book Entry", "UID: " + BASE._open_UID_No, PeriodText, 50, 75, 14, "Date:[Date Printed] \r\n" + "Place:" + BASE._open_Cen_Name, "ver " + BASE._Current_Version, "_________________________ \r\n" + "Signature of Centre In - charge \r\n" + Incharge);       
            return settings;
        }
        public string Get_Cash_Bank_Balance()
        {
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            var Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return null;
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

            double Open_Bank_Bal = 0;
            double Close_Bank_Bal = 0;
            var Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                return null;
            }
            int _bankCnt = 1;
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
                    _bankCnt += 1;
                }
            }
            else
            {
                Open_Bank_Bal = 0;
                Close_Bank_Bal = 0;
            }
            BE_Cash_Bank_Text = "Cash: " + Close_Cash_Bal.ToString("#,0.00") + "  Bank: " + Close_Bank_Bal.ToString("#,0.00");
            return BE_Cash_Bank_Text;
        }
        public bool Data_Change()
        {
            bool Flag_Data_Change = false;
            var TR_Table = NoteBook_Data;
            for (int i = 0; i < TR_Table.Count; i++)
            {
                int TR_Amt = 0;
                int Save_Amt = 0;
                if (TR_Table[i].AMT_DT_01 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_01);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_01"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_01"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_02 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_02);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_02"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_02"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_03 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_03);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_03"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_03"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_04 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_04);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_04"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_04"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_05 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_05);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_05"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_05"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_06 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_06);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_06"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_06"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_07 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_07);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_07"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_07"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_08 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_08);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_08"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_08"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_09 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_09);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_09"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_09"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_10 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_10);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_10"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_10"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_11 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_11);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_11"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_11"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_12 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_12);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_12"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_12"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_13 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_13);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_13"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_13"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_14 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_14);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_14"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_14"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_15 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_15);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_15"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_15"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_16 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_16);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_16"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_16"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_17 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_17);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_17"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_17"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_18 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_18);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_18"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_18"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_19 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_19);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_19"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_19"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_20 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_20);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_20"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_20"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_21 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_21);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_21"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_21"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_22 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_22);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_22"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_22"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_23 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_23);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_23"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_23"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_24 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_24);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_24"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_24"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_25 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_25);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_25"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_25"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_26 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_26);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_26"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_26"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_27 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_27);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_27"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_27"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_28 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_28);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_28"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_28"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_29 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_29);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_29"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_29"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_30 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_30);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_30"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_30"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
                if (TR_Table[i].AMT_DT_31 != null)
                {
                    TR_Amt = Convert.ToInt32(TR_Table[i].AMT_DT_31);
                }
                else
                {
                    TR_Amt = 0;
                }
                if (!Convert.IsDBNull(SaveData.Rows[i]["AMT_DT_31"]))
                {
                    Save_Amt = Convert.ToInt32(SaveData.Rows[i]["AMT_DT_31"]);
                }
                else
                {
                    Save_Amt = 0;
                }
                if (TR_Amt != Save_Amt)
                {
                    Flag_Data_Change = true;
                    break;
                }
                else
                {
                    Flag_Data_Change = false;
                }
            }
            return Flag_Data_Change;
            //return Json(new
            //{
            //    true
            //}, JsonRequestBehavior.AllowGet);           

        }
        public ActionResult Cmb_View_SelectedIndexChanged(int? Index = null)
        {
            var Perioddata = NB_PeriodSelectionData;
            string Text = Perioddata.Where(x => x.Index == Index).First().Period;
            if (Index >= 0 && Index <= 11)
            {
                string Sel_Mon = Text.Substring(0, 3).ToUpper();
                int SEL_MM = Sel_Mon == "JAN" ? 1 : Sel_Mon == "FEB" ? 2 : Sel_Mon == "MAR" ? 3 : Sel_Mon == "APR" ? 4 : Sel_Mon == "MAY" ? 5 : Sel_Mon == "JUN" ? 6 : Sel_Mon == "JUL" ? 7 : Sel_Mon == "AUG" ? 8 : Sel_Mon == "SEP" ? 9 : Sel_Mon == "OCT" ? 10 : Sel_Mon == "NOV" ? 11 : Sel_Mon == "DEC" ? 12 : 4;
                xFr_Date = new DateTime(Convert.ToInt32(Text.Substring(4, 4)), SEL_MM, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(1).AddDays(-1);
            }
            else if (Index == 12)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (Index == 13)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (Index == 14)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (Index == 15)
            {
                xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (Index == 16)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (Index == 17)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (Index == 18)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(9).AddDays(-1);
            }
            else if (Index == 19)
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
            }
            int TotalDays = DateTime.DaysInMonth(Convert.ToDateTime(xFr_Date).Year, Convert.ToDateTime(xFr_Date).Month);
            string Periodtext = "Fr.: " + Convert.ToDateTime(xFr_Date).ToString("dd-MMM, yyyy") + "  to  " + Convert.ToDateTime(xTo_Date).ToString("dd-MMM, yyyy");
            string Month = Convert.ToDateTime(xFr_Date).ToString("MMMM");
            string Year = Convert.ToDateTime(xFr_Date).Year.ToString();
            string GridBand2_Caption = Month + ',' + Year;
            var fr_date = Convert.ToDateTime(xFr_Date).ToString("dd/MM/yyyy");
            //if (model != null) 
            //{
            //    model.TotalDays = TotalDays;
            //    model.Periodtext = Periodtext;
            //    model.GridBand2_Caption = GridBand2_Caption;
            //    model.fr_date = fr_date;
            //}
            return Json(new
            {
                TotalDays,
                Periodtext,
                GridBand2_Caption,
                fr_date
            }, JsonRequestBehavior.AllowGet);
        }
        public List<NoteBook_Period> Fill_Change_Period_Items()
        {
            List<NoteBook_Period> data = new List<NoteBook_Period>();
            int index = 0;
            for (int i = BASE._open_Year_Sdt.Month; i <= 12; i++)
            {
                NoteBook_Period row = new NoteBook_Period();
                string xMonth = "";
                if (i == 1)
                {
                    xMonth = "JAN";
                }
                else if (i == 2)
                {
                    xMonth = "FEB";
                }
                else if (i == 3)
                {
                    xMonth = "MAR";
                }
                else if (i == 4)
                {
                    xMonth = "APR";
                }
                else if (i == 5)
                {
                    xMonth = "MAY";
                }
                else if (i == 6)
                {
                    xMonth = "JUN";
                }
                else if (i == 7)
                {
                    xMonth = "JUL";
                }
                else if (i == 8)
                {
                    xMonth = "AUG";
                }
                else if (i == 9)
                {
                    xMonth = "SEP";
                }
                else if (i == 10)
                {
                    xMonth = "OCT";
                }
                else if (i == 11)
                {
                    xMonth = "NOV";
                }
                else
                {
                    xMonth = "DEC";
                }
                row.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row.Index = index;
                data.Add(row);
                index++;
            }
            for (int i = 1; i <= BASE._open_Year_Edt.Month; i++)
            {
                NoteBook_Period row = new NoteBook_Period();
                string xMonth = "";
                if (i == 1)
                {
                    xMonth = "JAN";
                }
                else if (i == 2)
                {
                    xMonth = "FEB";
                }
                else if (i == 3)
                {
                    xMonth = "MAR";
                }
                else if (i == 4)
                {
                    xMonth = "APR";
                }
                else if (i == 5)
                {
                    xMonth = "MAY";
                }
                else if (i == 6)
                {
                    xMonth = "JUN";
                }
                else if (i == 7)
                {
                    xMonth = "JUL";
                }
                else if (i == 8)
                {
                    xMonth = "AUG";
                }
                else if (i == 9)
                {
                    xMonth = "SEP";
                }
                else if (i == 10)
                {
                    xMonth = "OCT";
                }
                else if (i == 11)
                {
                    xMonth = "NOV";
                }
                else
                {
                    xMonth = "DEC";
                }
                row.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                row.Index = index;
                data.Add(row);
                index++;
            }
            return data;
        }
        public List<NoteBook_Grid> ConvertGridToList(DataTable TR_Table)
        {
            List<NoteBook_Grid> Grid_data = new List<NoteBook_Grid>();
            List<DataRow> data = TR_Table.AsEnumerable().ToList();
            for (int i = 0; i < data.Count; i++)
            {
                NoteBook_Grid row = new NoteBook_Grid();
                row.REC_ID = data[i]["REC_ID"].ToString();
                row.ITEM_NAME = data[i]["ITEM_NAME"].ToString();
                row.ITEM_LED_ID = data[i]["ITEM_LED_ID"].ToString();
                row.LED_NAME = data[i]["LED_NAME"].ToString();
                row.ITEM_TRANS_TYPE = data[i]["ITEM_TRANS_TYPE"].ToString();
                row.AMT_DT_01 = Convert.IsDBNull(data[i]["AMT_DT_01"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_01"]);
                row.AMT_DT_02 = Convert.IsDBNull(data[i]["AMT_DT_02"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_02"]);
                row.AMT_DT_03 = Convert.IsDBNull(data[i]["AMT_DT_03"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_03"]);
                row.AMT_DT_04 = Convert.IsDBNull(data[i]["AMT_DT_04"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_04"]);
                row.AMT_DT_05 = Convert.IsDBNull(data[i]["AMT_DT_05"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_05"]);
                row.AMT_DT_06 = Convert.IsDBNull(data[i]["AMT_DT_06"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_06"]);
                row.AMT_DT_07 = Convert.IsDBNull(data[i]["AMT_DT_07"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_07"]);
                row.AMT_DT_08 = Convert.IsDBNull(data[i]["AMT_DT_08"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_08"]);
                row.AMT_DT_09 = Convert.IsDBNull(data[i]["AMT_DT_09"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_09"]);
                row.AMT_DT_10 = Convert.IsDBNull(data[i]["AMT_DT_10"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_10"]);
                row.AMT_DT_11 = Convert.IsDBNull(data[i]["AMT_DT_11"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_11"]);
                row.AMT_DT_12 = Convert.IsDBNull(data[i]["AMT_DT_12"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_12"]);
                row.AMT_DT_13 = Convert.IsDBNull(data[i]["AMT_DT_13"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_13"]);
                row.AMT_DT_14 = Convert.IsDBNull(data[i]["AMT_DT_14"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_14"]);
                row.AMT_DT_15 = Convert.IsDBNull(data[i]["AMT_DT_15"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_15"]);
                row.AMT_DT_16 = Convert.IsDBNull(data[i]["AMT_DT_16"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_16"]);
                row.AMT_DT_17 = Convert.IsDBNull(data[i]["AMT_DT_17"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_17"]);
                row.AMT_DT_18 = Convert.IsDBNull(data[i]["AMT_DT_18"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_18"]);
                row.AMT_DT_19 = Convert.IsDBNull(data[i]["AMT_DT_19"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_19"]);
                row.AMT_DT_20 = Convert.IsDBNull(data[i]["AMT_DT_20"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_20"]);
                row.AMT_DT_21 = Convert.IsDBNull(data[i]["AMT_DT_21"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_21"]);
                row.AMT_DT_22 = Convert.IsDBNull(data[i]["AMT_DT_22"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_22"]);
                row.AMT_DT_23 = Convert.IsDBNull(data[i]["AMT_DT_23"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_23"]);
                row.AMT_DT_24 = Convert.IsDBNull(data[i]["AMT_DT_24"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_24"]);
                row.AMT_DT_25 = Convert.IsDBNull(data[i]["AMT_DT_25"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_25"]);
                row.AMT_DT_26 = Convert.IsDBNull(data[i]["AMT_DT_26"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_26"]);
                row.AMT_DT_27 = Convert.IsDBNull(data[i]["AMT_DT_27"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_27"]);
                row.AMT_DT_28 = Convert.IsDBNull(data[i]["AMT_DT_28"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_28"]);
                row.AMT_DT_29 = Convert.IsDBNull(data[i]["AMT_DT_29"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_29"]);
                row.AMT_DT_30 = Convert.IsDBNull(data[i]["AMT_DT_30"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_30"]);
                row.AMT_DT_31 = Convert.IsDBNull(data[i]["AMT_DT_31"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_DT_31"]);
                row.AMT_TOTAL = Convert.IsDBNull(data[i]["AMT_TOTAL"]) ? (decimal?)null : Convert.ToDecimal(data[i]["AMT_TOTAL"]);
                Grid_data.Add(row);
            }
            return Grid_data;
        }
        public ActionResult UpdateCellData(string key, string field, decimal? value)
        {
            var data = NoteBook_Data;
            var DataToEdit = data.FirstOrDefault(x => x.REC_ID == key);
            if (field == "AMT_DT_01")
            {
                DataToEdit.AMT_DT_01 = value;
            }
            else if (field == "AMT_DT_02")
            {
                DataToEdit.AMT_DT_02 = value;
            }
            else if (field == "AMT_DT_03")
            {
                DataToEdit.AMT_DT_03 = value;
            }
            else if (field == "AMT_DT_04")
            {
                DataToEdit.AMT_DT_04 = value;
            }
            else if (field == "AMT_DT_05")
            {
                DataToEdit.AMT_DT_05 = value;
            }
            else if (field == "AMT_DT_06")
            {
                DataToEdit.AMT_DT_06 = value;
            }
            else if (field == "AMT_DT_07")
            {
                DataToEdit.AMT_DT_07 = value;
            }
            else if (field == "AMT_DT_08")
            {
                DataToEdit.AMT_DT_08 = value;
            }
            else if (field == "AMT_DT_09")
            {
                DataToEdit.AMT_DT_09 = value;
            }
            else if (field == "AMT_DT_10")
            {
                DataToEdit.AMT_DT_10 = value;
            }
            else if (field == "AMT_DT_11")
            {
                DataToEdit.AMT_DT_11 = value;
            }
            else if (field == "AMT_DT_12")
            {
                DataToEdit.AMT_DT_12 = value;
            }
            else if (field == "AMT_DT_13")
            {
                DataToEdit.AMT_DT_13 = value;
            }
            else if (field == "AMT_DT_14")
            {
                DataToEdit.AMT_DT_14 = value;
            }
            else if (field == "AMT_DT_15")
            {
                DataToEdit.AMT_DT_15 = value;
            }
            else if (field == "AMT_DT_16")
            {
                DataToEdit.AMT_DT_16 = value;
            }
            else if (field == "AMT_DT_17")
            {
                DataToEdit.AMT_DT_17 = value;
            }
            else if (field == "AMT_DT_18")
            {
                DataToEdit.AMT_DT_18 = value;
            }
            else if (field == "AMT_DT_19")
            {
                DataToEdit.AMT_DT_19 = value;
            }
            else if (field == "AMT_DT_20")
            {
                DataToEdit.AMT_DT_20 = value;
            }
            else if (field == "AMT_DT_21")
            {
                DataToEdit.AMT_DT_21 = value;
            }
            else if (field == "AMT_DT_22")
            {
                DataToEdit.AMT_DT_22 = value;
            }
            else if (field == "AMT_DT_23")
            {
                DataToEdit.AMT_DT_23 = value;
            }
            else if (field == "AMT_DT_24")
            {
                DataToEdit.AMT_DT_24 = value;
            }
            else if (field == "AMT_DT_25")
            {
                DataToEdit.AMT_DT_25 = value;
            }
            else if (field == "AMT_DT_26")
            {
                DataToEdit.AMT_DT_26 = value;
            }
            else if (field == "AMT_DT_27")
            {
                DataToEdit.AMT_DT_27 = value;
            }
            else if (field == "AMT_DT_28")
            {
                DataToEdit.AMT_DT_28 = value;
            }
            else if (field == "AMT_DT_29")
            {
                DataToEdit.AMT_DT_29 = value;
            }
            else if (field == "AMT_DT_30")
            {
                DataToEdit.AMT_DT_30 = value;
            }
            else if (field == "AMT_DT_31")
            {
                DataToEdit.AMT_DT_31 = value;
            }

            NoteBook_Data = data;
            var Grand_Total = UpdateTotal(key);
            var Summary = UpdateSummary(field);
            return Json(new
            {
                Summary,
                Grand_Total
            }, JsonRequestBehavior.AllowGet);
        }
        public decimal UpdateTotal(string REC_ID)
        {
            var data = NoteBook_Data;
            NoteBook_Grid row = data.FirstOrDefault(x => x.REC_ID == REC_ID);
            var Total_Value = CalculateTotalValue(row);
            row.AMT_TOTAL = Total_Value;
            NoteBook_Data = data;
            return Total_Value;
        }
        public decimal CalculateTotalValue(NoteBook_Grid row)
        {
            decimal total_value = 0;
            total_value = (row.AMT_DT_01 ?? 0) + (row.AMT_DT_02 ?? 0) + (row.AMT_DT_03 ?? 0) + (row.AMT_DT_04 ?? 0) + (row.AMT_DT_05 ?? 0) + (row.AMT_DT_06 ?? 0) +
                        (row.AMT_DT_07 ?? 0) + (row.AMT_DT_08 ?? 0) + (row.AMT_DT_09 ?? 0) + (row.AMT_DT_10 ?? 0) + (row.AMT_DT_11 ?? 0) + (row.AMT_DT_12 ?? 0) +
                         (row.AMT_DT_13 ?? 0) + (row.AMT_DT_14 ?? 0) + (row.AMT_DT_15 ?? 0) + (row.AMT_DT_16 ?? 0) + (row.AMT_DT_17 ?? 0) + (row.AMT_DT_18 ?? 0) +
                         (row.AMT_DT_19 ?? 0) + (row.AMT_DT_20 ?? 0) + (row.AMT_DT_21 ?? 0) + (row.AMT_DT_22 ?? 0) + (row.AMT_DT_23 ?? 0) + (row.AMT_DT_24 ?? 0) +
                         (row.AMT_DT_25 ?? 0) + (row.AMT_DT_26 ?? 0) + (row.AMT_DT_27 ?? 0) + (row.AMT_DT_28 ?? 0) + (row.AMT_DT_29 ?? 0) + (row.AMT_DT_30 ?? 0) + (row.AMT_DT_31 ?? 0);
            return total_value;
        }
        public string UpdateSummary(string field)
        {
            string Summary = "";
            decimal Total_Summary = 0;
            decimal Day_Summary = 0;
            for (int i = 0; i < NoteBook_Data.Count; i++)
            {
                Total_Summary = Total_Summary + (NoteBook_Data[i].AMT_TOTAL ?? 0);
                switch (field)
                {
                    case "AMT_DT_01":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_01 ?? 0);
                        break;
                    case "AMT_DT_02":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_02 ?? 0);
                        break;
                    case "AMT_DT_03":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_03 ?? 0);
                        break;
                    case "AMT_DT_04":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_04 ?? 0);
                        break;
                    case "AMT_DT_05":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_05 ?? 0);
                        break;
                    case "AMT_DT_06":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_06 ?? 0);
                        break;
                    case "AMT_DT_07":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_07 ?? 0);
                        break;
                    case "AMT_DT_08":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_08 ?? 0);
                        break;
                    case "AMT_DT_09":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_09 ?? 0);
                        break;
                    case "AMT_DT_10":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_10 ?? 0);
                        break;
                    case "AMT_DT_11":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_11 ?? 0);
                        break;
                    case "AMT_DT_12":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_12 ?? 0);
                        break;
                    case "AMT_DT_13":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_13 ?? 0);
                        break;
                    case "AMT_DT_14":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_14 ?? 0);
                        break;
                    case "AMT_DT_15":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_15 ?? 0);
                        break;
                    case "AMT_DT_16":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_16 ?? 0);
                        break;
                    case "AMT_DT_17":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_17 ?? 0);
                        break;
                    case "AMT_DT_18":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_18 ?? 0);
                        break;
                    case "AMT_DT_19":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_19 ?? 0);
                        break;
                    case "AMT_DT_20":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_20 ?? 0);
                        break;
                    case "AMT_DT_21":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_21 ?? 0);
                        break;
                    case "AMT_DT_22":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_22 ?? 0);
                        break;
                    case "AMT_DT_23":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_23 ?? 0);
                        break;
                    case "AMT_DT_24":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_24 ?? 0);
                        break;
                    case "AMT_DT_25":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_25 ?? 0);
                        break;
                    case "AMT_DT_26":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_26 ?? 0);
                        break;
                    case "AMT_DT_27":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_27 ?? 0);
                        break;
                    case "AMT_DT_28":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_28 ?? 0);
                        break;
                    case "AMT_DT_29":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_29 ?? 0);
                        break;
                    case "AMT_DT_30":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_30 ?? 0);
                        break;
                    case "AMT_DT_31":
                        Day_Summary = Day_Summary + (NoteBook_Data[i].AMT_DT_31 ?? 0);
                        break;
                }
            }
            Summary = Total_Summary + "|" + Day_Summary;
            return Summary;
        }
        public ActionResult Saveclick()
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {               
                var _Entries = new ArrayList();
                var data = NoteBook_Data;
                if (data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].AMT_DT_01 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 1, Convert.ToDouble(data[i].AMT_DT_01), ref _Entries);
                        }
                        if (data[i].AMT_DT_02 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 2, Convert.ToDouble(data[i].AMT_DT_02), ref _Entries);
                        }
                        if (data[i].AMT_DT_03 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 3, Convert.ToDouble(data[i].AMT_DT_03), ref _Entries);
                        }
                        if (data[i].AMT_DT_04 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 4, Convert.ToDouble(data[i].AMT_DT_04), ref _Entries);
                        }
                        if (data[i].AMT_DT_05 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 5, Convert.ToDouble(data[i].AMT_DT_05), ref _Entries);
                        }
                        if (data[i].AMT_DT_06 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 6, Convert.ToDouble(data[i].AMT_DT_06), ref _Entries);
                        }
                        if (data[i].AMT_DT_07 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 7, Convert.ToDouble(data[i].AMT_DT_07), ref _Entries);
                        }
                        if (data[i].AMT_DT_08 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 8, Convert.ToDouble(data[i].AMT_DT_08), ref _Entries);
                        }
                        if (data[i].AMT_DT_09 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 9, Convert.ToDouble(data[i].AMT_DT_09), ref _Entries);
                        }
                        if (data[i].AMT_DT_10 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 10, Convert.ToDouble(data[i].AMT_DT_10), ref _Entries);
                        }
                        if (data[i].AMT_DT_11 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 11, Convert.ToDouble(data[i].AMT_DT_11), ref _Entries);
                        }
                        if (data[i].AMT_DT_12 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 12, Convert.ToDouble(data[i].AMT_DT_12), ref _Entries);
                        }
                        if (data[i].AMT_DT_13 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 13, Convert.ToDouble(data[i].AMT_DT_13), ref _Entries);
                        }
                        if (data[i].AMT_DT_14 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 14, Convert.ToDouble(data[i].AMT_DT_14), ref _Entries);
                        }
                        if (data[i].AMT_DT_15 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 15, Convert.ToDouble(data[i].AMT_DT_15), ref _Entries);
                        }
                        if (data[i].AMT_DT_16 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 16, Convert.ToDouble(data[i].AMT_DT_16), ref _Entries);
                        }
                        if (data[i].AMT_DT_17 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 17, Convert.ToDouble(data[i].AMT_DT_17), ref _Entries);
                        }
                        if (data[i].AMT_DT_18 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 18, Convert.ToDouble(data[i].AMT_DT_18), ref _Entries);
                        }
                        if (data[i].AMT_DT_19 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 19, Convert.ToDouble(data[i].AMT_DT_19), ref _Entries);
                        }
                        if (data[i].AMT_DT_20 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 20, Convert.ToDouble(data[i].AMT_DT_20), ref _Entries);
                        }
                        if (data[i].AMT_DT_21 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 21, Convert.ToDouble(data[i].AMT_DT_21), ref _Entries);
                        }
                        if (data[i].AMT_DT_22 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 22, Convert.ToDouble(data[i].AMT_DT_22), ref _Entries);
                        }
                        if (data[i].AMT_DT_23 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 23, Convert.ToDouble(data[i].AMT_DT_23), ref _Entries);
                        }
                        if (data[i].AMT_DT_24 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 24, Convert.ToDouble(data[i].AMT_DT_24), ref _Entries);
                        }
                        if (data[i].AMT_DT_25 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 25, Convert.ToDouble(data[i].AMT_DT_25), ref _Entries);
                        }
                        if (data[i].AMT_DT_26 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 26, Convert.ToDouble(data[i].AMT_DT_26), ref _Entries);
                        }
                        if (data[i].AMT_DT_27 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 27, Convert.ToDouble(data[i].AMT_DT_27), ref _Entries);
                        }
                        if (data[i].AMT_DT_28 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 28, Convert.ToDouble(data[i].AMT_DT_28), ref _Entries);
                        }
                        if (data[i].AMT_DT_29 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 29, Convert.ToDouble(data[i].AMT_DT_29), ref _Entries);
                        }
                        if (data[i].AMT_DT_30 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 30, Convert.ToDouble(data[i].AMT_DT_30), ref _Entries);
                        }
                        if (data[i].AMT_DT_31 > 0)
                        {
                            PrepareNoteBookEntry(data[i].REC_ID, 31, Convert.ToDouble(data[i].AMT_DT_31), ref _Entries);
                        }
                    }
                    bool xFlag = true;
                    if (BASE._NoteBook_DBOps.Delete(Convert.ToDateTime(xFr_Date).Month, Convert.ToDateTime(xFr_Date).Year))
                    {
                        xFlag = true;
                    }
                    else
                    {
                        xFlag = false;
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (_Entries.Count > 0)
                    {
                        if (BASE._NoteBook_DBOps.Insert(_Entries) && xFlag == true)
                        {

                        }
                        else
                        {
                            xFlag = false;
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (xFlag == true)
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Success... !!";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return null;
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
        }
        public void PrepareNoteBookEntry(string key, int _Day, double _Amount, ref ArrayList _Entries)
        {               
            var data = NoteBook_Data;           
            var row = data.FirstOrDefault(x => x.REC_ID == key);
            DateTime VDate = new DateTime(Convert.ToDateTime(xFr_Date).Year, Convert.ToDateTime(xFr_Date).Month, _Day);
            string Dr_Led_id = "";
            string Cr_Led_id = "";
            if (row.ITEM_TRANS_TYPE != null && row.ITEM_TRANS_TYPE.ToUpper() == "DEBIT")
            {
                Dr_Led_id = row.ITEM_LED_ID;
                Cr_Led_id = "00080";
            }
            else
            {
                Cr_Led_id = row.ITEM_LED_ID;
                Dr_Led_id = "00080";
            }
            Parameter_Insert_NoteBook InParam = new Parameter_Insert_NoteBook();
            InParam.TransCode = (int)Common.Voucher_Screen_Code.Payment;
            InParam.VNo = "";
            InParam.TDate = VDate.ToString(BASE._Server_Date_Format_Short);
            InParam.ItemID = row.REC_ID;
            InParam.Type = row.ITEM_TRANS_TYPE;
            InParam.Cr_Led_ID = Cr_Led_id;
            InParam.Dr_Led_ID = Dr_Led_id;
            InParam.Amount = _Amount;
            InParam.Narration = "";
            InParam.Status_Action = ((int)Common.Record_Status._Completed).ToString();
            InParam.RecID = Guid.NewGuid().ToString();
            _Entries.Add(InParam);
        }
        public ActionResult Frm_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Accounts_Notebook, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_modal_NB','Not Allowed','No Rights');</script>");
            }
            string Incharge = "";
            DataTable Centre_Inc = BASE._Report_DBOps.GetCenterDetails();
            if (Centre_Inc == null)
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (!Convert.IsDBNull(Centre_Inc.Rows[0]["CEN_INCHARGE"]))
            {
                Incharge = Centre_Inc.Rows[0]["CEN_INCHARGE"].ToString();
            }
            // ExportGridFooterRight = "______________________________" + "\r\nSignature Of Centre Incharge" + "\r\n" + Incharge;
            return PartialView();
        }
        public ActionResult Frm_View_Summary()
        {
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            double Open_Bank_Bal = 0;
            double Close_Bank_Bal = 0;
            double R_CASH = 0;
            double R_BANK = 0;
            double P_CASH = 0;
            double P_BANK = 0;

            ViewBag.SummaryPeriod = "Period Fr.: " + Convert.ToDateTime(xFr_Date).ToString("dd-MMM, yyyy") + "  to  " + Convert.ToDateTime(xTo_Date).ToString("dd-MMM, yyyy");
            //'PREPARE TABLE

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

            //'CASH................................
            Open_Cash_Bal = 0;
            Close_Cash_Bal = 0;
            var Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["OPENING"].ToString()))
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                else
                    Open_Cash_Bal = 0;
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["RECEIPT"].ToString()))
                    R_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["RECEIPT"]);
                else
                    R_CASH = 0;
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["PAYMENT"].ToString()))
                    P_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["PAYMENT"]);
                else
                    P_CASH = 0;
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["CLOSING"].ToString()))
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                else
                    Close_Cash_Bal = 0;
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
            var Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            int XCNT = 2;
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
                {
                    if (!string.IsNullOrEmpty(XROW["OPENING"].ToString()))
                        Open_Bank_Bal = Convert.ToDouble(XROW["OPENING"]);
                    else
                        Open_Bank_Bal += 0;
                    if (!string.IsNullOrEmpty(XROW["RECEIPT"].ToString()))
                        R_BANK = Convert.ToDouble(XROW["RECEIPT"]);
                    else
                        R_BANK += 0;
                    if (!string.IsNullOrEmpty(XROW["PAYMENT"].ToString()))
                        P_BANK = Convert.ToDouble(XROW["PAYMENT"]);
                    else
                        P_BANK += 0;
                    if (!string.IsNullOrEmpty(XROW["CLOSING"].ToString()))
                        Close_Bank_Bal = Convert.ToDouble(XROW["CLOSING"]);
                    else
                        Close_Bank_Bal += 0;

                    ROW = CashBank_Table.NewRow();
                    ROW["Title"] = "BANK";
                    ROW["Sr"] = XCNT;
                    ROW["Description"] = XROW["BI_SHORT_NAME"] + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"];
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
            NB_SummaryGridData = SummaryGridData;
            return View(SummaryGridData);
        }
        public ActionResult Frm_View_SummaryGrid()
        {
            return View(NB_SummaryGridData);
        }
        public void Frm_View_SummaryGrid_Close()
        {
            BASE._SessionDictionary.Remove("NB_SummaryGridData_Notebook");
        }
        public void SessionClear()
        {
            ClearBaseSession("_Notebook");
        }
        public void ResetStaticVariable()
        {
            //xEntryDate_NB = null;
            xFr_Date = null;
            xTo_Date = null;
            //Open_Cash_Bal = 0;
            //Open_Bank_Bal = 0;
            //Close_Cash_Bal = 0;
            //Close_Bank_Bal = 0;
            TR_Table = null;
            SaveData = null;
            //Check_UnSave_Data = false;
            //Flag_Data_Change = false;
            BE_Cash_Bank_Text = "";
            //_Entries = new ArrayList();
            NoteBook_Data = null;
            NB_SummaryGridData = null;
            NB_PeriodSelectionData = null;
            //ExportGridHeaderRight = "";
            //ExportGridFooterRight = "";
        }
        #region Devextreme
        public ActionResult Frm_NoteBook_Info_dx(string Tag = null, string xEntryDate = null, string GridToRefresh = "CashBookListGrid")
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_Notebook, "Add")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            ViewBag.Open_From_CB = xEntryDate == null ? false : true;
            ViewBag.GridToRefresh = GridToRefresh;
            ViewBag.self_posted = BASE._open_User_Self_Posted;
            ViewBag.OpenYearSdt = BASE._open_Year_Sdt;
            ViewBag.OpenYearEdt = BASE._open_Year_Edt;
            ViewBag.Tag = Tag;
            ViewBag.Set_Disable = false;
            if (Tag == "_View")
            {
                ViewBag.Set_Disable = true;
            }
            ViewBag.xEntryDate = xEntryDate == null ? (DateTime?)null : Convert.ToDateTime(xEntryDate);
            ViewData["Accnt_Notebook_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_Notebook, "Export");       
            ViewData["Lock_unlockRight"] = BASE.CheckActionRights(ClientScreen.Accounts_Notebook, Common.ClientAction.Lock_Unlock);    
            DateTime xLastDate = DateTime.Now;
            if (Tag == "_Edit" || Tag == "_View")
            {
                xLastDate = Convert.ToDateTime(xEntryDate);
            }
            else
            {
                object MaxValue = 0;
                MaxValue = BASE._NoteBook_DBOps.GetMaxTransactionDate();
                if (MaxValue == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                }
                if (Convert.IsDBNull(MaxValue))
                {
                    xLastDate = BASE._open_Year_Sdt;
                }
                else
                {
                    xLastDate = Convert.ToDateTime(MaxValue);
                }
            }
            int xMM = xLastDate.Month;
            ViewBag.PeriodSelectedIndex = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            //model.Cmb_View_SelectedIndex = index;
            //dynamic data = Cmb_View_SelectedIndexChanged(index);
            //model.TotalDays = data.Data.TotalDays;
            //model.Periodtext = data.Data.Periodtext;
            //model.GridBand2_Caption = data.Data.GridBand2_Caption;
            //model.fr_date = data.Data.fr_date;
            //ViewData["BE_Cash_Bank_Text"] = Get_Cash_Bank_Balance();
            //TR_Table = BASE._NoteBook_DBOps.GetList(Convert.ToDateTime(xFr_Date).Month);
            //SaveData = TR_Table.Copy();
            //model.NoteBookData = ConvertGridToList(TR_Table);
            //NoteBook_Data = model.NoteBookData;
            //ViewBag.TotalDays = model.TotalDays;
            //ViewBag.Band2_Caption = model.GridBand2_Caption;
            NoteBook_Info model = new NoteBook_Info();
            return View(model);
        }
        public ActionResult Period_data_dx(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Fill_Change_Period_Items(), loadOptions)), "application/json");
        }
        public ActionResult GetNotebookData_dx(string xFr_Date = "")
        {
            if (string.IsNullOrWhiteSpace(xFr_Date) == false)
            {
                return Content(JsonConvert.SerializeObject(BASE._NoteBook_DBOps.GetList(Convert.ToDateTime(xFr_Date).Month)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(""), "application/json");
        }
        public ActionResult Frm_View_Summary_dx(string xFr_Date = "", string xTo_Date = "")
        {
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            double Open_Bank_Bal = 0;
            double Close_Bank_Bal = 0;
            double R_CASH = 0;
            double R_BANK = 0;
            double P_CASH = 0;
            double P_BANK = 0;

            ViewBag.SummaryPeriod = "Period Fr.: " + Convert.ToDateTime(xFr_Date).ToString("dd-MMM, yyyy") + "  to  " + Convert.ToDateTime(xTo_Date).ToString("dd-MMM, yyyy");
            //'PREPARE TABLE

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

            //'CASH................................
            Open_Cash_Bal = 0;
            Close_Cash_Bal = 0;
            var Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["OPENING"].ToString()))
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                else
                    Open_Cash_Bal = 0;
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["RECEIPT"].ToString()))
                    R_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["RECEIPT"]);
                else
                    R_CASH = 0;
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["PAYMENT"].ToString()))
                    P_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["PAYMENT"]);
                else
                    P_CASH = 0;
                if (!string.IsNullOrEmpty(Cash_Bal.Rows[0]["CLOSING"].ToString()))
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                else
                    Close_Cash_Bal = 0;
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
            var Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            int XCNT = 2;
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
                {
                    if (!string.IsNullOrEmpty(XROW["OPENING"].ToString()))
                        Open_Bank_Bal = Convert.ToDouble(XROW["OPENING"]);
                    else
                        Open_Bank_Bal += 0;
                    if (!string.IsNullOrEmpty(XROW["RECEIPT"].ToString()))
                        R_BANK = Convert.ToDouble(XROW["RECEIPT"]);
                    else
                        R_BANK += 0;
                    if (!string.IsNullOrEmpty(XROW["PAYMENT"].ToString()))
                        P_BANK = Convert.ToDouble(XROW["PAYMENT"]);
                    else
                        P_BANK += 0;
                    if (!string.IsNullOrEmpty(XROW["CLOSING"].ToString()))
                        Close_Bank_Bal = Convert.ToDouble(XROW["CLOSING"]);
                    else
                        Close_Bank_Bal += 0;

                    ROW = CashBank_Table.NewRow();
                    ROW["Title"] = "BANK";
                    ROW["Sr"] = XCNT;
                    ROW["Description"] = XROW["BI_SHORT_NAME"] + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"];
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
            return View("Frm_View_Summary",SummaryGridData);
        }
        public ActionResult Get_Cash_Bank_Balance_dx(string xFr_Date = "", string xTo_Date = "")
        {
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            var Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return null;
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

            double Open_Bank_Bal = 0;
            double Close_Bank_Bal = 0;
            var Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(Convert.ToDateTime(xFr_Date), Convert.ToDateTime(xTo_Date), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                return null;
            }
            int _bankCnt = 1;
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
                    _bankCnt += 1;
                }
            }
            else
            {
                Open_Bank_Bal = 0;
                Close_Bank_Bal = 0;
            }       
            return Json(new
            {
                Close_Cash_Bal,
                Close_Bank_Bal
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Saveclick_dx(string CurrentData, string xFr_Date = "")
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                List<NoteBook_Grid> data = JsonConvert.DeserializeObject<List<NoteBook_Grid>>(CurrentData);
                var _Entries = new ArrayList(); 
                if (data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].AMT_DT_01 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 1, Convert.ToDouble(data[i].AMT_DT_01), ref _Entries,data, xFr_Date);
                        }
                        if (data[i].AMT_DT_02 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 2, Convert.ToDouble(data[i].AMT_DT_02), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_03 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 3, Convert.ToDouble(data[i].AMT_DT_03), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_04 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 4, Convert.ToDouble(data[i].AMT_DT_04), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_05 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 5, Convert.ToDouble(data[i].AMT_DT_05), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_06 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 6, Convert.ToDouble(data[i].AMT_DT_06), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_07 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 7, Convert.ToDouble(data[i].AMT_DT_07), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_08 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 8, Convert.ToDouble(data[i].AMT_DT_08), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_09 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 9, Convert.ToDouble(data[i].AMT_DT_09), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_10 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 10, Convert.ToDouble(data[i].AMT_DT_10), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_11 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 11, Convert.ToDouble(data[i].AMT_DT_11), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_12 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 12, Convert.ToDouble(data[i].AMT_DT_12), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_13 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 13, Convert.ToDouble(data[i].AMT_DT_13), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_14 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 14, Convert.ToDouble(data[i].AMT_DT_14), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_15 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 15, Convert.ToDouble(data[i].AMT_DT_15), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_16 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 16, Convert.ToDouble(data[i].AMT_DT_16), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_17 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 17, Convert.ToDouble(data[i].AMT_DT_17), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_18 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 18, Convert.ToDouble(data[i].AMT_DT_18), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_19 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 19, Convert.ToDouble(data[i].AMT_DT_19), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_20 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 20, Convert.ToDouble(data[i].AMT_DT_20), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_21 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 21, Convert.ToDouble(data[i].AMT_DT_21), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_22 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 22, Convert.ToDouble(data[i].AMT_DT_22), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_23 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 23, Convert.ToDouble(data[i].AMT_DT_23), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_24 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 24, Convert.ToDouble(data[i].AMT_DT_24), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_25 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 25, Convert.ToDouble(data[i].AMT_DT_25), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_26 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 26, Convert.ToDouble(data[i].AMT_DT_26), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_27 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 27, Convert.ToDouble(data[i].AMT_DT_27), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_28 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 28, Convert.ToDouble(data[i].AMT_DT_28), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_29 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 29, Convert.ToDouble(data[i].AMT_DT_29), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_30 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 30, Convert.ToDouble(data[i].AMT_DT_30), ref _Entries, data, xFr_Date);
                        }
                        if (data[i].AMT_DT_31 > 0)
                        {
                            PrepareNoteBookEntry_dx(data[i].REC_ID, 31, Convert.ToDouble(data[i].AMT_DT_31), ref _Entries, data, xFr_Date);
                        }
                    }
                    bool xFlag = true;
                    if (BASE._NoteBook_DBOps.Delete(Convert.ToDateTime(xFr_Date).Month, Convert.ToDateTime(xFr_Date).Year))
                    {
                        xFlag = true;
                    }
                    else
                    {
                        xFlag = false;
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (_Entries.Count > 0)
                    {
                        if (BASE._NoteBook_DBOps.Insert(_Entries) && xFlag == true)
                        {

                        }
                        else
                        {
                            xFlag = false;
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (xFlag == true)
                    {
                        jsonParam.message = Messages.SaveSuccess;
                        jsonParam.title = "Success... !!";
                        jsonParam.result = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return null;
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
        }
        public void PrepareNoteBookEntry_dx(string key, int _Day, double _Amount, ref ArrayList _Entries,List<NoteBook_Grid> data,string xFr_Date)
        {
            var row = data.FirstOrDefault(x => x.REC_ID == key);
            DateTime VDate = new DateTime(Convert.ToDateTime(xFr_Date).Year, Convert.ToDateTime(xFr_Date).Month, _Day);
            string Dr_Led_id = "";
            string Cr_Led_id = "";
            if (row.ITEM_TRANS_TYPE != null && row.ITEM_TRANS_TYPE.ToUpper() == "DEBIT")
            {
                Dr_Led_id = row.ITEM_LED_ID;
                Cr_Led_id = "00080";
            }
            else
            {
                Cr_Led_id = row.ITEM_LED_ID;
                Dr_Led_id = "00080";
            }
            Parameter_Insert_NoteBook InParam = new Parameter_Insert_NoteBook();
            InParam.TransCode = (int)Common.Voucher_Screen_Code.Payment;
            InParam.VNo = "";
            InParam.TDate = VDate.ToString(BASE._Server_Date_Format_Short);
            InParam.ItemID = row.REC_ID;
            InParam.Type = row.ITEM_TRANS_TYPE;
            InParam.Cr_Led_ID = Cr_Led_id;
            InParam.Dr_Led_ID = Dr_Led_id;
            InParam.Amount = _Amount;
            InParam.Narration = "";
            InParam.Status_Action = ((int)Common.Record_Status._Completed).ToString();
            InParam.RecID = Guid.NewGuid().ToString();
            _Entries.Add(InParam);
        }
        [HttpPost]
        public ActionResult Export_dx(string CurrentData, int TotalDay,string Band_Caption,string PeriodText)
        {
            List<NoteBook_Grid> data = JsonConvert.DeserializeObject<List<NoteBook_Grid>>(CurrentData);
            string Incharge = "";
            DataTable Centre_Inc = BASE._Report_DBOps.GetCenterDetails();
            if (Convert.IsDBNull(Centre_Inc.Rows[0]["CEN_INCHARGE"]) == false)
            {
                Incharge = Centre_Inc.Rows[0]["CEN_INCHARGE"].ToString();
            }
            var Reoprt = GridViewExportHelper.Show_ListPreview(GridExportSettings(TotalDay, Band_Caption, PeriodText, Incharge), data, "Note Book  (UID: " + BASE._open_UID_No + ")", true, System.Drawing.Printing.PaperKind.A4, true, 50, 75);
            return View("Frm_Export_Options", Reoprt);
        }
        #endregion
    }
}