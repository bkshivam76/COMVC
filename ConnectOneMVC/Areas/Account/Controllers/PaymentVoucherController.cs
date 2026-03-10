using Common_Lib;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Common_Lib.DbOperations;
using Common_Lib.RealTimeService;
using System.Drawing;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using ConnectOneMVC.Models;
using System.Text;
using System.Web.Routing;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class PaymentVoucherController : BaseController
    {
        #region Global Variable  
        #region Grid
        public List<Payment_Grid_Datatable> ItemGridData
        {
            get
            {
                return (List<Payment_Grid_Datatable>)GetBaseSession("ItemGridData_Payment");
            }
            set
            {
                SetBaseSession("ItemGridData_Payment", value);
            }
        }
        public List<PaymentBankDetail_Grid_Datatable> BankGridData
        {
            get
            {
                return (List<PaymentBankDetail_Grid_Datatable>)GetBaseSession("BankGridData_Payment");
            }
            set
            {
                SetBaseSession("BankGridData_Payment", value);
            }
        }
        public List<Return_AdvanceAdjustment_Grid_Datatable> AdvanceGridData
        {
            get
            {
                return (List<Return_AdvanceAdjustment_Grid_Datatable>)GetBaseSession("AdvanceGridData_Payment");
            }
            set
            {
                SetBaseSession("AdvanceGridData_Payment", value);
            }
        }
        public List<Return_PendingLiabilities_Grid> LiabilityGridData
        {
            get
            {
                return (List<Return_PendingLiabilities_Grid>)GetBaseSession("LiabilityGridData_Payment");
            }
            set
            {
                SetBaseSession("LiabilityGridData_Payment", value);
            }
        }
        #endregion
        public decimal Txt_DiffAmt
        {
            get
            {
                return (decimal)GetBaseSession("Txt_DiffAmt_Payment");
            }
            set
            {
                SetBaseSession("Txt_DiffAmt_Payment", value);
            }
        }
        public decimal Txt_SubTotal
        {
            get
            {
                return (decimal)GetBaseSession("Txt_SubTotal_Payment");
            }
            set
            {
                SetBaseSession("Txt_SubTotal_Payment", value);
            }
        }
        public decimal Txt_CashAmt
        {
            get
            {
                return (decimal)GetBaseSession("Txt_CashAmt_Payment");
            }
            set
            {
                SetBaseSession("Txt_CashAmt_Payment", value);
            }
        }
        public decimal Txt_CreditAmt
        {
            get
            {
                return (decimal)GetBaseSession("Txt_CreditAmt_Payment");
            }
            set
            {
                SetBaseSession("Txt_CreditAmt_Payment", value);
            }
        }
        public decimal Txt_BankAmt
        {
            get
            {
                return (decimal)GetBaseSession("Txt_BankAmt_Payment");
            }
            set
            {
                SetBaseSession("Txt_BankAmt_Payment", value);
            }
        }
        public decimal Txt_AdvAmt
        {
            get
            {
                return (decimal)GetBaseSession("Txt_AdvAmt_Payment");
            }
            set
            {
                SetBaseSession("Txt_AdvAmt_Payment", value);
            }
        }
        public decimal Txt_LB_Amt
        {
            get
            {
                return (decimal)GetBaseSession("Txt_LB_Amt_Payment");
            }
            set
            {
                SetBaseSession("Txt_LB_Amt_Payment", value);
            }
        }
        public decimal Txt_TDS_Amt
        {
            get
            {
                return (decimal)GetBaseSession("Txt_TDS_Amt_Payment");// redmine Bug #133109 fixed
            }
            set
            {
                SetBaseSession("Txt_TDS_Amt_Payment", value);
            }
        }
        public DataTable LB_EXTENDED_PROPERTY_TABLE
        {
            get
            {
                return (DataTable)GetBaseSession("LB_EXTENDED_PROPERTY_TABLE_Payment");
            }
            set
            {
                SetBaseSession("LB_EXTENDED_PROPERTY_TABLE_Payment", value);
            }
        }
        public DataTable LB_DOCS_ARRAY
        {
            get
            {
                return (DataTable)GetBaseSession("LB_DOCS_ARRAY_Payment");
            }
            set
            {
                SetBaseSession("LB_DOCS_ARRAY_Payment", value);
            }
        }
        public string Payment_xMID
        {
            get
            {
                return (string)GetBaseSession("xMID_Payment");
            }
            set
            {
                SetBaseSession("xMID_Payment", value);
            }
        }
        public bool Payment_iParty_Req
        {
            get
            {
                return (bool)GetBaseSession("iParty_Req_Payment");
            }
            set
            {
                SetBaseSession("iParty_Req_Payment", value);
            }
        }
        public string Payment_Sel_Bank_ID
        {
            get
            {
                return (string)GetBaseSession("Sel_Bank_ID_Payment");
            }
            set
            {
                SetBaseSession("Sel_Bank_ID_Payment", value);
            }
        }
        public PaymentType Payment_SelectedPaymentType
        {
            get
            {
                return (PaymentType)GetBaseSession("SelectedPaymentType_Payment");
            }
            set
            {
                SetBaseSession("SelectedPaymentType_Payment", value);
            }
        }
        public string Payment_Tag
        {
            get
            {
                return (string)GetBaseSession("Tag_Payment");
            }
            set
            {
                SetBaseSession("Tag_Payment", value);
            }
        }
        public byte[] Payment_Asset_Image
        {
            get
            {
                return (byte[])GetBaseSession("Payment_Asset_Image_Payment");
            }
            set
            {
                SetBaseSession("Payment_Asset_Image_Payment", value);
            }
        }
        #endregion
        public ActionResult Frm_Voucher_Win_Gen_Pay(string Tag, string iSpecific_ItemID = "", string xID = null, string xMID = null, string Info_LastEditedOn = "", string SelectedPaymentType = "", string PostSucessFunction = null, string PopupName = "Dynamic_Content_popup", string TitleX = "", string Sel_Bank_ID = "", string GridToRefresh = "CashBookListGrid")
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add")) && (Tag == "_New" || Tag == "_New_From_Selection"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','Not Allowed','No Rights');</script>");
            }
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Update")) && Tag == "_Edit")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','Not Allowed','No Rights');</script>");
            }
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Delete")) && Tag == "_Delete")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','Not Allowed','No Rights');</script>");
            }
            if (!(CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "View")) && Tag == "_View")
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','Not Allowed','No Rights');</script>");
            }
            ViewBag.GridToRefresh = GridToRefresh;
            var model = new Param_paymentVoucherDetailsViewModel();
            ResetStaticVariable();
            model.xID = xID;
            model.xMID = xMID;
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.TempActionMethod = Tag;
            model.iSpecific_ItemID = iSpecific_ItemID;
            model.iParty_Req = false;
            model.IsProfileCreationVoucher = false;
            Payment_xMID = xMID;
            Payment_iParty_Req = false;
            Payment_Sel_Bank_ID = Sel_Bank_ID;
            Payment_Tag = Tag.ToString();
            if (TitleX == "Land and Building Payment")
            {
                model.TitleX = "Land and Building Payment";
            }
            else
            {
                model.TitleX = "Payment";
            }
            model.Text = (model.TempActionMethod.StartsWith("_") ? model.TempActionMethod.Substring(1): model.TempActionMethod) + " ~ " + model.TitleX;
            if (string.IsNullOrEmpty(SelectedPaymentType))
            {
                model.SelectedPaymentType = PaymentType.Cash;
                model.PaymentType = "Cash";

            }
            else
            {
                model.SelectedPaymentType = (PaymentType)Enum.Parse(typeof(PaymentType), SelectedPaymentType);
                model.PaymentType = SelectedPaymentType;
            }
            Payment_SelectedPaymentType = model.SelectedPaymentType;
            model.iSpecific_ItemID = iSpecific_ItemID;
            model.PostSucessFunction = PostSucessFunction == null ? "Frm_Voucher_Win_Gen_Pay_BUT_SAVE_Click_OnSuccess" : PostSucessFunction;
            model.PopupName = PopupName == null ? "Dynamic_Content_popup" : PopupName;

            model.PartyListData = LookUp_GetPartyList();     
            SetGridData();

            //Special Voucher References (FCRA Related) Code Get items from Center Task info 
            model.SpecialReferenceList_Data_Pmt = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_Pmt = model.SpecialReferenceList_Data_Pmt.Count();

            if (Tag == "_New" || Tag == "_New_From_Selection")
            {
                model.Txt_V_NO = "";
            }
            else
            {

                //FCRA Related or Special Voucher References Related onEditGet dbfunction calling from Special Voucher transactions data.
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_Pmt = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                Param_PaymentData dsPayment;
                dsPayment = BASE._Voucher_DBOps.GetPaymentDetails(xMID);
                DataTable d1;
                DataTable d3;
                DataTable d4;
                DataTable d5;
                DataTable d6;
                DataTable d7;
                d1 = dsPayment.param_MasterInfo;
                d3 = dsPayment.param_TransactionInfo;
                d4 = dsPayment.param_TransactionItem;
                d5 = dsPayment.param_BankPayment;
                d6 = dsPayment.param_TxnAdvancePayment;
                d7 = dsPayment.param_TxnLiabPayment;
                model.LB_DOCS_ARRAY = dsPayment.param_LB_DOCS_ARRAY;
                model.LB_EXTENDED_PROPERTY_TABLE = dsPayment.param_LB_EXTENDED_PROPERTY;            
                model.Txt_V_Date = Convert.ToDateTime(d1.Rows[0]["TR_DATE"]);
                //Start : Check if entry already changed 
                if (BASE.AllowMultiuser())
                {
                    if (model.TempActionMethod == "_Edit" || model.TempActionMethod == "_Delete" || model.TempActionMethod == "_View")
                    {
                        var viewstr = "";
                        if (model.TempActionMethod == "_View")
                        {
                            viewstr = "view";
                        }
                        if (CommonFunctions.AreDatesEqual(model.Info_LastEditedOn,Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]))==false)
                        {
                            string message = Messages.RecordChanged("Current Payment", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('"+ PopupName + "','"+ message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                    if (model.TempActionMethod == "_Edit" || model.TempActionMethod == "_Delete")
                    {
                        string Adv_Dep_ID = "";
                        if (d6 != null) {
                            if (d6.Rows.Count > 0) {
                                Adv_Dep_ID = d6.Rows[0]["TR_REF_ID"].ToString();
                            }
                        }
                        if (Adv_Dep_ID.Length > 0)
                        {
                            DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, Adv_Dep_ID);
                            if (AssetTrfRecord != null)
                            {
                                if (AssetTrfRecord.Rows.Count > 0)
                                {
                                    string message="";
                                    if (model.TempActionMethod == "_Edit")
                                    {
                                        message = "Sorry ! Selected Entry Refers a Advance/Deposit which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                    }
                                    if (model.TempActionMethod == "_Delete")
                                    {
                                        message = "Sorry ! Selected Entry Refers a Advance/Deposit which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                    }
                                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                                }
                            }
                        }

                        string Liab_ID = "";
                        if (d7 != null)
                        {
                            if (d7.Rows.Count > 0)
                            {
                                Liab_ID = d7.Rows[0]["TR_REF_ID"].ToString();
                            }
                        }
                        if (Liab_ID.Length > 0)
                        {
                            DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, Liab_ID);
                            if (AssetTrfRecord != null)
                            {
                                if (AssetTrfRecord.Rows.Count > 0)
                                {
                                    string message = "";
                                    if (model.TempActionMethod == "_Edit")
                                    {
                                        message = "Sorry ! Selected Entry Refers a Liability which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                    }
                                    if (model.TempActionMethod == "_Delete")
                                    {
                                        message = "Sorry ! Selected Entry Refers a Liability which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                    }
                                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('" + PopupName + "','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                                }
                            }
                        }
                    }
                }
                //End : Check if entry already changed             

                Data_Binding(ref model, d1, d3, d4, d5, d6, d7);
            }
            return View(model);
        }
        public void Data_Binding(ref Param_paymentVoucherDetailsViewModel model, DataTable d1, DataTable d3, DataTable d4, DataTable d5, DataTable d6, DataTable d7)
        {
            model.LastEditedOn = Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]);
            model.Txt_V_NO = d1.Rows[0]["TR_VNO"].ToString();
            model.Txt_Inv_No = d1.Rows[0]["TR_INV_NO"].ToString();
            if (!Convert.IsDBNull(d1.Rows[0]["TR_INV_DATE"]))
            {
                model.Txt_Inv_Date = (DateTime)d1.Rows[0]["TR_INV_DATE"];
            }
            if (!Convert.IsDBNull(d1.Rows[0]["TR_AB_ID_1"]))
            {
                if (d1.Rows[0]["TR_AB_ID_1"].ToString().Length > 0)
                {
                    model.GLookUp_PartyList1_Tag = d1.Rows[0]["TR_AB_ID_1"].ToString();
                    Party_Outstanding_Advances(model.GLookUp_PartyList1_Tag); // reloading data of Advances Adj grid, as per party selected              
                    Party_Outstanding_Liabilities(model.GLookUp_PartyList1_Tag); // reloading data of Liab Adj grid, as per party selected                
                }
            }
            model.IsPartyReadOnly = false;
            Sub_Amt_Calculation(false);
            List<Payment_Grid_Datatable> GridData = new List<Payment_Grid_Datatable>();
            foreach (DataRow XRow in d4.Rows)
            {
                Payment_Grid_Datatable ROW = new Payment_Grid_Datatable();
                ROW.Sr = (int)XRow["TR_SR_NO"];
                ROW.Item_ID = Convert.ToString(XRow["TR_ITEM_ID"]);
                ROW.Item_Led_ID = Convert.ToString(XRow["TR_LED_ID"]);
                ROW.Item_Trans_Type = Convert.ToString(XRow["TR_TRANS_TYPE"]);
                ROW.Item_Party_Req = Convert.ToString(XRow["TR_PARTY_REQ"]);
                ROW.Item_Profile = Convert.ToString(XRow["TR_PROFILE"]);
                ROW.ITEM_VOUCHER_TYPE = Convert.ToString(XRow["ITEM_VOUCHER_TYPE"]);
                ROW.ItemName = Convert.ToString(XRow["TR_ITEM_NAME"]);
                ROW.Head = Convert.ToString(XRow["TR_ITEM_HEAD"]);
                ROW.Qty = Convert.ToDecimal(XRow["TR_QTY"]);
                ROW.Unit = Convert.ToString(XRow["TR_UNIT"]);
                ROW.Rate = Convert.ToDecimal(XRow["TR_RATE"]);
                ROW.Amount = Convert.ToDecimal(XRow["TR_AMOUNT"]);
                ROW.Remarks = Convert.ToString(XRow["TR_REMARKS"]);
                ROW.TDS = Convert.ToDecimal(XRow["TDS"]);
                //Purpose
                ROW.Pur_ID = Convert.ToString(XRow["Pur_ID"]);
                ROW.LOC_ID = Convert.ToString(XRow["LOC_ID"]);
                ROW.CREATION_PROF_REC_ID = Convert.IsDBNull(XRow["CREATION_PROF_REC_ID"]) ? "" : (XRow["CREATION_PROF_REC_ID"]).ToString();
                if (!Convert.IsDBNull(XRow["CREATION_PROF_REC_ID"]))
                {
                    if (ROW.CREATION_PROF_REC_ID.Length > 0)
                    {
                        model.IsProfileCreationVoucher = true;
                    }
                }
                //Gold/Silver
                ROW.GS_DESC_MISC_ID = Convert.ToString(XRow["GS_DESC_MISC_ID"]);
                ROW.GS_ITEM_WEIGHT = (XRow["GS_ITEM_WEIGHT"] == System.DBNull.Value) ? null : (decimal?)XRow["GS_ITEM_WEIGHT"];
                //OTHER ASSET
                ROW.AI_TYPE = Convert.ToString(XRow["AI_TYPE"]);
                ROW.AI_MAKE = Convert.ToString(XRow["AI_MAKE"]);
                ROW.AI_MODEL = Convert.ToString(XRow["AI_MODEL"]);
                ROW.AI_SERIAL_NO = Convert.ToString(XRow["AI_SERIAL_NO"]);
                ROW.AI_WARRANTY = (XRow["AI_WARRANTY"] == System.DBNull.Value) ? (decimal?)null : Convert.ToDecimal(XRow["AI_WARRANTY"]);
                if (Convert.IsDBNull(XRow["AI_IMAGE"]))
                {
                    ROW.AI_IMAGE = null;
                }
                else
                {
                    ROW.AI_IMAGE = (byte[])XRow["AI_IMAGE"];
                }
                if (XRow["AI_PUR_DATE"] != System.DBNull.Value && XRow["AI_PUR_DATE"].ToString().Length > 0)
                {
                    ROW.AI_PUR_DATE = Convert.ToDateTime(XRow["AI_PUR_DATE"]).ToString(BASE._Server_Date_Format_Short);
                }
                //LIVE STOCK
                ROW.LS_NAME = Convert.ToString(XRow["LS_NAME"]);
                ROW.LS_BIRTH_YEAR = Convert.ToString(XRow["LS_BIRTH_YEAR"]);
                ROW.LS_INSURANCE = Convert.ToString(XRow["LS_INSURANCE"]);
                ROW.LS_INSURANCE_ID = Convert.ToString(XRow["LS_INSURANCE_ID"]);
                ROW.LS_INS_POLICY_NO = Convert.ToString(XRow["LS_INS_POLICY_NO"]);
                ROW.LS_INS_AMT = (XRow["LS_INS_AMT"] == System.DBNull.Value) ? (decimal?)null : Convert.ToDecimal(XRow["LS_INS_AMT"]);
                if (XRow["LS_INS_DATE"] != System.DBNull.Value && XRow["LS_INS_DATE"].ToString().Length > 0)
                {
                    ROW.LS_INS_DATE = Convert.ToDateTime(XRow["LS_INS_DATE"]).ToString(BASE._Server_Date_Format_Short);
                }
                //VEHICLES
                ROW.VI_MAKE = Convert.ToString(XRow["VI_MAKE"]);
                ROW.VI_MODEL = Convert.ToString(XRow["VI_MODEL"]);
                ROW.VI_REG_NO_PATTERN = Convert.ToString(XRow["VI_REG_NO_PATTERN"]);
                ROW.VI_REG_NO = Convert.ToString(XRow["VI_REG_NO"]);
                if (XRow["VI_REG_DATE"] != System.DBNull.Value && XRow["VI_REG_DATE"].ToString().Length > 0)
                {
                    ROW.VI_REG_DATE = Convert.ToDateTime(XRow["VI_REG_DATE"]).ToString(BASE._Server_Date_Format_Short);
                }
                ROW.VI_OWNERSHIP = Convert.ToString(XRow["VI_OWNERSHIP"]);
                ROW.VI_OWNERSHIP_AB_ID = Convert.ToString(XRow["VI_OWNERSHIP_AB_ID"]);
                ROW.VI_DOC_RC_BOOK = Convert.ToString(XRow["VI_DOC_RC_BOOK"]);
                ROW.VI_DOC_AFFIDAVIT = Convert.ToString(XRow["VI_DOC_AFFIDAVIT"]);
                ROW.VI_DOC_WILL = Convert.ToString(XRow["VI_DOC_WILL"]);
                ROW.VI_DOC_TRF_LETTER = Convert.ToString(XRow["VI_DOC_TRF_LETTER"]);
                ROW.VI_DOC_FU_LETTER = Convert.ToString(XRow["VI_DOC_FU_LETTER"]);
                ROW.VI_DOC_OTHERS = Convert.ToString(XRow["VI_DOC_OTHERS"]);
                ROW.VI_DOC_NAME = Convert.ToString(XRow["VI_DOC_NAME"]);
                ROW.VI_INSURANCE_ID = Convert.ToString(XRow["VI_INSURANCE_ID"]);
                ROW.VI_INS_POLICY_NO = Convert.ToString(XRow["VI_INS_POLICY_NO"]);
                if (XRow["VI_INS_EXPIRY_DATE"] != System.DBNull.Value && XRow["VI_INS_EXPIRY_DATE"].ToString().Length > 0)
                {
                    ROW.VI_INS_EXPIRY_DATE = Convert.ToDateTime(XRow["VI_INS_EXPIRY_DATE"]).ToString(BASE._Server_Date_Format_Short);
                }
                //Land & Building
                ROW.LB_PRO_TYPE = Convert.ToString(XRow["LB_PRO_TYPE"]);
                ROW.LB_PRO_CATEGORY = Convert.ToString(XRow["LB_PRO_CATEGORY"]);
                ROW.LB_PRO_USE = Convert.ToString(XRow["LB_PRO_USE"]);
                ROW.LB_PRO_NAME = Convert.ToString(XRow["LB_PRO_NAME"]);
                ROW.LB_PRO_ADDRESS = Convert.ToString(XRow["LB_PRO_ADDRESS"]);
                ROW.LB_OWNERSHIP = Convert.ToString(XRow["LB_OWNERSHIP"]);
                ROW.LB_OWNERSHIP_PARTY_ID = Convert.ToString(XRow["LB_OWNERSHIP_PARTY_ID"]);
                ROW.LB_SURVEY_NO = Convert.ToString(XRow["LB_SURVEY_NO"]);
                ROW.LB_CON_YEAR = Convert.ToString(XRow["LB_CON_YEAR"]);
                ROW.LB_RCC_ROOF = Convert.ToString(XRow["LB_RCC_ROOF"]);
                if (XRow["LB_PAID_DATE"] != System.DBNull.Value && XRow["LB_PAID_DATE"].ToString().Length > 0)
                {
                    ROW.LB_PAID_DATE = Convert.ToDateTime(XRow["LB_PAID_DATE"]).ToString(BASE._Server_Date_Format_Short);
                }
                if (XRow["LB_PERIOD_FROM"] != System.DBNull.Value && XRow["LB_PERIOD_FROM"].ToString().Length > 0)
                {
                    ROW.LB_PERIOD_FROM = Convert.ToDateTime(XRow["LB_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Short);
                }
                if (XRow["LB_PERIOD_TO"] != System.DBNull.Value && XRow["LB_PERIOD_TO"].ToString().Length > 0)
                {
                    ROW.LB_PERIOD_TO = Convert.ToDateTime(XRow["LB_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Short);
                }
                ROW.LB_DOC_OTHERS= Convert.ToString(XRow["LB_DOC_OTHERS"]);
                ROW.LB_DOC_NAME = Convert.ToString(XRow["LB_DOC_NAME"]);
                ROW.LB_OTHER_DETAIL = Convert.ToString(XRow["LB_OTHER_DETAIL"]);
                ROW.LB_TOT_P_AREA = (XRow["LB_TOT_P_AREA"] == System.DBNull.Value) ? null : (decimal?)(XRow["LB_TOT_P_AREA"]);
                ROW.LB_CON_AREA = (XRow["LB_CON_AREA"] == System.DBNull.Value) ? null : (decimal?)(XRow["LB_CON_AREA"]);
                ROW.LB_DEPOSIT_AMT = (XRow["LB_DEPOSIT_AMT"] == System.DBNull.Value) ? null : (decimal?)(XRow["LB_DEPOSIT_AMT"]);
                ROW.LB_MONTH_RENT = (XRow["LB_MONTH_RENT"] == System.DBNull.Value) ? null : (decimal?)(XRow["LB_MONTH_RENT"]);
                ROW.LB_MONTH_O_PAYMENTS = (XRow["LB_MONTH_O_PAYMENTS"] == System.DBNull.Value) ? null : (decimal?)(XRow["LB_MONTH_O_PAYMENTS"]);
                ROW.LB_REC_ID = Convert.IsDBNull(XRow["LB_REC_ID"]) == true ? null : (string)XRow["LB_REC_ID"];
                ROW.LB_REC_EDIT_ON = (XRow["LB_REC_EDIT_ON"] == System.DBNull.Value) ? null : (DateTime?)XRow["LB_REC_EDIT_ON"];
                ROW.LB_ADDRESS1 = Convert.ToString(XRow["LB_ADDRESS1"]);
                ROW.LB_ADDRESS2 = Convert.ToString(XRow["LB_ADDRESS2"]);
                ROW.LB_ADDRESS3 = Convert.ToString(XRow["LB_ADDRESS3"]);
                ROW.LB_ADDRESS4 = Convert.ToString(XRow["LB_ADDRESS4"]);
                ROW.LB_STATE_ID = Convert.ToString(XRow["LB_STATE_ID"]);
                ROW.LB_DISTRICT_ID = Convert.ToString(XRow["LB_DISTRICT_ID"]);
                ROW.LB_CITY_ID = Convert.ToString(XRow["LB_CITY_ID"]);
                ROW.LB_PINCODE = Convert.ToString(XRow["LB_PINCODE"]);
                //TELEPHONE BILL
                ROW.TP_ID = (string)XRow["TP_ID"];
                ROW.TP_BILL_NO = (string)XRow["TP_BILL_NO"];
                if (XRow["TP_BILL_DATE"] != System.DBNull.Value && XRow["TP_BILL_DATE"].ToString().Length > 0)
                {
                    ROW.TP_BILL_DATE = Convert.ToDateTime(XRow["TP_BILL_DATE"]).ToString(BASE._Server_Date_Format_Short); //Redmine Bug #134895 fix
                }
                if (XRow["TP_PERIOD_FROM"] != System.DBNull.Value && XRow["TP_PERIOD_FROM"].ToString().Length > 0)
                {
                    ROW.TP_PERIOD_FROM = Convert.ToDateTime(XRow["TP_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Short); //Redmine Bug #134895 fix
                }
                if (XRow["TP_PERIOD_TO"] != System.DBNull.Value && XRow["TP_PERIOD_TO"].ToString().Length > 0)
                {
                    ROW.TP_PERIOD_TO = Convert.ToDateTime(XRow["TP_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Short); //Redmine Bug #134895 fix
                }
                //WIP
                ROW.REF_REC_ID = Convert.ToString(XRow["WIP_REF_TYPE"]) == "NEW" ? "" : Convert.ToString(XRow["WIP_REC_ID"]);
                ROW.REFERENCE = Convert.ToString(XRow["WIP_REF"]);
                ROW.WIP_REF_TYPE = Convert.ToString(XRow["WIP_REF_TYPE"]);
                GridData.Add(ROW);
            }
            ItemGridData = GridData;
            Sub_Amt_Calculation(false);
            //BANK DETAIL.......................... 
            DataTable BA_Table = BASE._Payment_DBOps.GetBankAccounts();
            BA_Table.Columns.Add("Name");
            BA_Table.Columns.Add("Branch");
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs += "'" + (string)xRow["BA_BRANCH_ID"] + "',";
            }
            if (Branch_IDs.Trim().Length > 0)
            {
                Branch_IDs = Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().Substring(0, Branch_IDs.Trim().Length - 1) : Branch_IDs.Trim();
            }
            if (Branch_IDs.Trim().Length == 0)
            {
                Branch_IDs = "''";
            }
            DataTable BB_Table = BASE._Payment_DBOps.GetBranches(Branch_IDs);
            DataSet BankJointdata = new DataSet();
            BankJointdata.Tables.Add(BA_Table);
            BankJointdata.Tables.Add(BB_Table.Copy());
            DataRelation BA_Relation = BankJointdata.Relations.Add("BANK", BankJointdata.Tables["BANK_ACCOUNT_INFO"].Columns["BA_BRANCH_ID"], BankJointdata.Tables["BANK_BRANCH_INFO"].Columns["BB_BRANCH_ID"], false);
            foreach (DataRow XROW in BankJointdata.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(BA_Relation))
                {
                    XROW["Name"] = _Row["NAME"];
                    XROW["Branch"] = _Row["Branch"];
                }
            }
            BankJointdata.Relations.Clear();
            BA_Table = BankJointdata.Tables[0];
            BankJointdata.Tables.Clear();
            BankJointdata.Tables.Add(d5.Copy());
            BankJointdata.Tables.Add(BA_Table.Copy());

            DataRelation BANK_Relation = BankJointdata.Relations.Add("BANK_ACC", BankJointdata.Tables["TRANSACTION_D_PAYMENT_INFO"].Columns["TR_REF_ID"], BankJointdata.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            string MT_Bank_IDs = "";
            foreach (DataRow XROW in BankJointdata.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(BANK_Relation))
                {
                    XROW["BANK_NAME"] = _Row["Name"];
                    XROW["BRANCH_NAME"] = _Row["Branch"];
                    XROW["ACC_NO"] = _Row["BA_ACCOUNT_NO"];
                    XROW["REC_EDIT_ON"] = _Row["REC_EDIT_ON"];
                }
                if (XROW["TR_MT_BANK_ID"].ToString().Length > 0)
                {
                    MT_Bank_IDs += "'" + XROW["TR_MT_BANK_ID"].ToString() + "',";
                }
            }
            if (MT_Bank_IDs.Trim().Length > 0)
            {
                MT_Bank_IDs = MT_Bank_IDs.Trim().EndsWith(",") ? MT_Bank_IDs.Trim().Substring(0, MT_Bank_IDs.Trim().Length - 1) : MT_Bank_IDs.Trim();
            }
            if (MT_Bank_IDs.Trim().Length == 0)
            {
                MT_Bank_IDs = "''";
            }
            DataTable MT_BANK_Table = BASE._Payment_DBOps.GetBanks(MT_Bank_IDs);
            BankJointdata.Tables.Add(MT_BANK_Table.Copy());
            DataRelation MT_BANK_Relation = BankJointdata.Relations.Add("MT_BANK_ACC", BankJointdata.Tables["TRANSACTION_D_PAYMENT_INFO"].Columns["TR_MT_BANK_ID"], BankJointdata.Tables["BANK_INFO"].Columns["REC_ID"], false);
            foreach (DataRow XROW in BankJointdata.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(MT_BANK_Relation))
                {
                    XROW["MT_BANK_NAME"] = _Row["BI_BANK_NAME"];
                }
            }
            d5 = BankJointdata.Tables[0];
            List<PaymentBankDetail_Grid_Datatable> BankData = new List<PaymentBankDetail_Grid_Datatable>();
            foreach (DataRow XRow in d5.Rows)
            {
                PaymentBankDetail_Grid_Datatable ROW = new PaymentBankDetail_Grid_Datatable();
                ROW.Sr = (int)XRow["TR_SR_NO"];
                ROW.Amount = Convert.ToDouble(XRow["TR_REF_AMT"]);
                ROW.Mode = (string)XRow["TR_MODE"];
                ROW.Ref_No = (string)XRow["TR_REF_NO"];
                if (!Convert.IsDBNull(XRow["TR_REF_DATE"]))
                {
                    ROW.Ref_Date = Convert.ToDateTime(Convert.ToDateTime(XRow["TR_REF_DATE"]).ToString(BASE._Date_Format_Current));
                }
                if (!Convert.IsDBNull(XRow["TR_REF_CDATE"]))
                {
                    ROW.Ref_CDate = Convert.ToDateTime(Convert.ToDateTime(XRow["TR_REF_CDATE"]).ToString(BASE._Date_Format_Current));
                }
                ROW.BANK_NAME = (string)XRow["BANK_NAME"];
                ROW.Branch = (string)XRow["BRANCH_NAME"];
                ROW.Acc_No = (string)XRow["ACC_NO"];
                ROW.ID = (string)XRow["TR_REF_ID"];
                ROW.MT_BANK_Name = (string)XRow["MT_BANK_NAME"];
                ROW.Ref_Acc_No = (string)XRow["TR_MT_ACC_NO"];
                ROW.MT_BANK_ID = Convert.IsDBNull(XRow["TR_MT_BANK_ID"]) ? null : (string)XRow["TR_MT_BANK_ID"];
                ROW.Edit_Time = (DateTime)XRow["REC_EDIT_ON"];
                BankData.Add(ROW);
            }
            BankGridData = BankData;
            Bank_Amt_Calculation(false);
            //ADVANCE DETAIL....................................................This PArt of Code has been shifted to DB call itself
            //var GridView3 = (List<Return_AdvanceAdjustment_Grid_Datatable>)Session["Frm_Voucher_Win_Gen_Pay_Grid_3_Data"];
            //if (GridView3 != null)
            //{
            //    if (GridView3.Count > 0)
            //    {
            //        for (int I = 0; I < GridView3.Count; I++)
            //        {
            //            foreach (DataRow XRow in d6.Rows)
            //            {
            //                if (GridView3[I].AI_ID == (string)XRow["TR_REF_ID"])
            //                {

            //                    GridView3[I].Payment = (decimal)XRow["TR_REF_AMT"];
            //                }
            //            }
            //        }
            //    }
            //}
            //Session["Frm_Voucher_Win_Gen_Pay_Grid_3_Data"] = GridView3;


            //LIABILITIES DETAIL....................................................This PArt of Code has been shifted to DB call itself
            //var GridView4 = (List<Return_PendingLiabilities_Grid>)Session["Frm_Voucher_Win_Gen_Pay_Grid_4_Data"];
            //if (GridView4 != null)
            //{
            //    if (GridView4.Count > 0)
            //    {
            //        for (int I = 0; I < GridView4.Count; I++)
            //        {
            //            foreach (DataRow XRow in d7.Rows)
            //            {
            //                if (GridView4[I].LI_ID == (string)XRow["TR_REF_ID"])
            //                {
            //                    GridView4[I].Payment = (decimal)XRow["TR_REF_AMT"];
            //                }
            //            }
            //        }
            //    }
            //}
            //Session["Frm_Voucher_Win_Gen_Pay_Grid_4_Data"] = GridView4;

            Advance_Amt_Calculation();
            LB_Amt_Calculation();
            Difference_Calculation();
            Txt_CreditAmt = Convert.ToDecimal(d1.Rows[0]["TR_CREDIT_AMT"]);
            if (Txt_CreditAmt > 0)
            {
                model.IsProfileCreationVoucher = true;
            }
            Txt_CashAmt = Convert.ToDecimal(d1.Rows[0]["TR_CASH_AMT"]);
            Txt_TDS_Amt = Convert.ToDecimal(d1.Rows[0]["TR_TDS_AMT"]);
            if (Txt_TDS_Amt > 0)
            {
                model.IsPartyReadOnly = true;
            }
            model.Txt_Narration = (string)d3.Rows[0]["TR_NARRATION"];
            model.Txt_Reference = (string)d3.Rows[0]["TR_REFERENCE"];      
            if (!Convert.IsDBNull(d1.Rows[0]["TR_CR_DUE_DATE"]))
            {
                model.Txt_DueDate =Convert.ToDateTime(d1.Rows[0]["TR_CR_DUE_DATE"]);
            }
            if (Txt_AdvAmt > 0 || Txt_CreditAmt > 0 || Txt_LB_Amt > 0)
            {
                model.IsPartyReadOnly = true;
            }
            if (Txt_CreditAmt > 0)
            {
                string xTemp_LiabID = BASE._Voucher_DBOps.GetRaisedLiabilityRecID(model.xMID);
                if (xTemp_LiabID != null)
                {
                    if (xTemp_LiabID.Length > 0)
                    {
                        DataTable txnReferLiab = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_LiabID);
                        if (txnReferLiab != null)
                        {
                            if (txnReferLiab.Rows.Count > 0)
                            {
                                model.IsCreditReadOnly = true;
                                model.CreditTooltip = "Liability is readonly, as it has been adjusted in other entries";
                                model.IsPartyReadOnly = true;
                                model.PartyTooltip = "Party is readonly, as liability created in this voucher has been adjusted in other entries";
                            }
                        }
                        Parameter_GetJornalEntryAdjustments paramJornalEntry = new Parameter_GetJornalEntryAdjustments();
                        paramJornalEntry.CrossRefId = xTemp_LiabID;
                        paramJornalEntry.Excluded_Rec_M_ID = model.xMID;
                        paramJornalEntry.SpecifiedEntryType = EntryType.Both;
                        paramJornalEntry.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                        txnReferLiab = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, ClientScreen.Accounts_Vouchers);
                        if (txnReferLiab != null)
                        {
                            if (txnReferLiab.Rows.Count > 0)
                            {
                                model.IsCreditReadOnly = true;
                                model.CreditTooltip = "Liability is readonly, as it has been adjusted in other entries";
                                model.IsPartyReadOnly = true;
                                model.PartyTooltip = "Party is readonly, as liability created in this voucher has been adjusted in other entries";
                            }
                        }
                    }
                }
            }
            model.Txt_DiffAmt = Txt_DiffAmt;
            model.Txt_SubTotal = Txt_SubTotal;
            model.Txt_TDS_Amt = Txt_TDS_Amt;
            model.Txt_BankAmt = Txt_BankAmt;
            model.Txt_CreditAmt = Txt_CreditAmt;
            model.Txt_CashAmt = Txt_CashAmt;
            model.Txt_AdvAmt = Txt_AdvAmt;
            model.Txt_LB_Amt = Txt_LB_Amt;
        }
        public void SetGridData()
        {
            ItemGridData = null;
            BankGridData = null;
            AdvanceGridData = null;
            LiabilityGridData = null;
        }
        public void ResetStaticVariable()
        {
            Txt_DiffAmt = 0.00M;
            Txt_SubTotal = 0.00M;
            Txt_CashAmt = 0.00M;
            Txt_CreditAmt = 0.00M;
            Txt_BankAmt = 0.00M;
            Txt_AdvAmt = 0.00M;
            Txt_LB_Amt = 0.00M;
            Txt_TDS_Amt = 0.00M;
        }
        public ActionResult Frm_Voucher_Win_Gen_Pay_BUT_SAVE_Click(Param_paymentVoucherDetailsViewModel model)
        {
            try
            {            
                Voucher_Payment.Param_paymentVoucherDetails paymentVoucherDetails = new Voucher_Payment.Param_paymentVoucherDetails();
                // Voucher_Payment.Param_BasicData paymentVoucherDetailBasicData = new Voucher_Payment.Param_BasicData();
                Voucher_Payment.Param_paymentVoucherFormData paymentVoucherDetailFormData = new Voucher_Payment.Param_paymentVoucherFormData();
                Voucher_Payment.Param_paymentVoucherGlobalData paymentVoucherDetailGlobalData = new Voucher_Payment.Param_paymentVoucherGlobalData();
                if (model.TempActionMethod == "_New")
                {
                    model.Tag = Common.Navigation_Mode._New;
                }
                // Set basic values
                //paymentVoucherDetailBasicData.open_Ins_ID = BASE._open_Ins_ID;
                //paymentVoucherDetailBasicData.open_UID_No = BASE._open_UID_No;
                //paymentVoucherDetailBasicData.open_PAD_No_Main = BASE._open_PAD_No_Main;
                //paymentVoucherDetailBasicData.open_Cen_Rec_ID = BASE._open_Cen_Rec_ID;
                //paymentVoucherDetailBasicData.open_Year_Sdt = BASE._open_Year_Sdt;
                //paymentVoucherDetailBasicData.open_Year_Edt = BASE._open_Year_Edt;
                //paymentVoucherDetailBasicData.prev_Unaudited_YearID = BASE._prev_Unaudited_YearID;
                //paymentVoucherDetailBasicData.next_Unaudited_YearID = BASE._next_Unaudited_YearID;
                //paymentVoucherDetailBasicData.IsMultiUserAllowed = BASE.AllowMultiuser();
                //paymentVoucherDetailBasicData.IsInsuranceAudited = BASE.IsInsuranceAudited();

                // Filled Form Values
                paymentVoucherDetailFormData.NavMode = (int)model.Tag;
                paymentVoucherDetailFormData.Rec_ID = model.xMID == null ? "xMID" : model.xMID;
                paymentVoucherDetailFormData.Txt_AdvAmt = model.Txt_AdvAmt.ToString("#0.00");
                paymentVoucherDetailFormData.Txt_CreditAmt = model.Txt_CreditAmt.ToString("#0.00");
                paymentVoucherDetailFormData.Txt_LB_Amt = model.Txt_LB_Amt.ToString("#0.00");
                paymentVoucherDetailFormData.Txt_CashAmt = model.Txt_CashAmt.ToString("#0.00");
                if (IsDate(model.Txt_V_Date.ToString()))
                {
                    paymentVoucherDetailFormData.Txt_V_Date = Convert.ToDateTime(model.Txt_V_Date).ToString(BASE._Server_Date_Format_Short);
                }
                else
                {
                    paymentVoucherDetailFormData.Txt_V_Date = model.Txt_V_Date.ToString();
                }
                paymentVoucherDetailFormData.Txt_V_NO = string.IsNullOrEmpty(model.Txt_V_NO) ? "" : model.Txt_V_NO;
                paymentVoucherDetailFormData.Txt_Inv_No = string.IsNullOrEmpty(model.Txt_Inv_No) ? "" : model.Txt_Inv_No.ToUpper();
                if (model.Txt_Inv_Date != null)
                {
                    paymentVoucherDetailFormData.Txt_Inv_Date = Convert.ToDateTime(model.Txt_Inv_Date).ToString(BASE._Server_Date_Format_Short);
                }
                else
                {
                    paymentVoucherDetailFormData.Txt_Inv_Date = model.Txt_Inv_Date.ToString();
                }
                paymentVoucherDetailFormData.Txt_SubTotal = model.Txt_SubTotal.ToString("#0.00");
                paymentVoucherDetailFormData.Txt_BankAmt = model.Txt_BankAmt.ToString("#0.00");
                paymentVoucherDetailFormData.Txt_TDS_Amt = model.Txt_TDS_Amt.ToString("#0.00");
                if (IsDate(model.Txt_DueDate.ToString()))
                {
                    paymentVoucherDetailFormData.Txt_DueDate = Convert.ToDateTime(model.Txt_DueDate).ToString(BASE._Server_Date_Format_Short);
                }
                else
                {
                    paymentVoucherDetailFormData.Txt_DueDate = model.Txt_DueDate.ToString();
                }
                paymentVoucherDetailFormData.Txt_Narration = string.IsNullOrEmpty(model.Txt_Narration) ? "" : model.Txt_Narration;
                paymentVoucherDetailFormData.Txt_Reference = string.IsNullOrEmpty(model.Txt_Reference) ? "" : model.Txt_Reference;
                paymentVoucherDetailFormData.GLookUp_PartyList1_Txt = string.IsNullOrEmpty(model.GLookUp_PartyList1_Txt) ? "" : model.GLookUp_PartyList1_Txt;
                paymentVoucherDetailFormData.GLookUp_PartyList1_Tag = string.IsNullOrEmpty(model.GLookUp_PartyList1_Tag) ? "" : model.GLookUp_PartyList1_Tag;
                if (model.GLookUp_PartyList1_REC_EDIT_ON.HasValue)
                {
                    paymentVoucherDetailFormData.oldREC_EDIT_ON = model.GLookUp_PartyList1_REC_EDIT_ON.Value;
                }
                if (ItemGridData != null)
                {
                    DataTable Grid1 = CommonFunctions.ConvertToDataTable(ItemGridData);
                    DataView dv = new DataView(Grid1);
                    paymentVoucherDetailFormData.GridView1 = dv.ToTable();
                    paymentVoucherDetailFormData.GridView1.TableName = "Item_Detail";
                    paymentVoucherDetailFormData.GridView1.Columns["Sr"].ColumnName = "Sr.";
                    paymentVoucherDetailFormData.GridView1.Columns["ItemName"].ColumnName = "Item Name";
                    paymentVoucherDetailFormData.GridView1.Columns["Qty"].ColumnName = "Qty.";
                    foreach (DataRow dRow in paymentVoucherDetailFormData.GridView1.Rows)
                    {
                        if (IsDate(dRow["AI_PUR_DATE"].ToString())) dRow["AI_PUR_DATE"] = Convert.ToDateTime(dRow["AI_PUR_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LS_INS_DATE"].ToString())) dRow["LS_INS_DATE"] = Convert.ToDateTime(dRow["LS_INS_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["VI_REG_DATE"].ToString())) dRow["VI_REG_DATE"] = Convert.ToDateTime(dRow["VI_REG_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["VI_INS_EXPIRY_DATE"].ToString())) dRow["VI_INS_EXPIRY_DATE"] = Convert.ToDateTime(dRow["VI_INS_EXPIRY_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LB_PAID_DATE"].ToString())) dRow["LB_PAID_DATE"] = Convert.ToDateTime(dRow["LB_PAID_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LB_PERIOD_FROM"].ToString())) dRow["LB_PERIOD_FROM"] = Convert.ToDateTime(dRow["LB_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LB_PERIOD_TO"].ToString())) dRow["LB_PERIOD_TO"] = Convert.ToDateTime(dRow["LB_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["TP_BILL_DATE"].ToString())) dRow["TP_BILL_DATE"] = Convert.ToDateTime(dRow["TP_BILL_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["TP_PERIOD_FROM"].ToString())) dRow["TP_PERIOD_FROM"] = Convert.ToDateTime(dRow["TP_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["TP_PERIOD_TO"].ToString())) dRow["TP_PERIOD_TO"] = Convert.ToDateTime(dRow["TP_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Long);
                    }
                    if (!(paymentVoucherDetailFormData.GridView1 == null))
                    {
                        paymentVoucherDetailFormData.GridView1.TableName = "VoucherDetailFormDataGrid1";
                    }
                }
                if (BankGridData != null)
                {
                    DataTable Grid2 = new DataTable();
                    Grid2.Columns.Add("Sr.", Type.GetType("System.Int32"));
                    Grid2.Columns.Add("Amount", Type.GetType("System.Decimal"));
                    Grid2.Columns.Add("Mode", Type.GetType("System.String"));
                    Grid2.Columns.Add("No.", Type.GetType("System.String"));
                    Grid2.Columns.Add("Date", Type.GetType("System.String"));
                    Grid2.Columns.Add("Clearing Date", Type.GetType("System.String"));
                    Grid2.Columns.Add("Bank Name", Type.GetType("System.String"));
                    Grid2.Columns.Add("Branch", Type.GetType("System.String"));
                    Grid2.Columns.Add("A/c. No.", Type.GetType("System.String"));
                    Grid2.Columns.Add("ID", Type.GetType("System.String"));
                    Grid2.Columns.Add("MT_BANK_ID", Type.GetType("System.String"));
                    Grid2.Columns.Add("Money Transfer Bank", Type.GetType("System.String"));
                    Grid2.Columns.Add("Ref. A/c. No.", Type.GetType("System.String"));
                    Grid2.Columns.Add("Edit Time", Type.GetType("System.DateTime"));
                    var Grid2Data = BankGridData;
                    for (int i = 0; i < Grid2Data.Count; i++)
                    {
                        DataRow Row = Grid2.NewRow();
                        Row["Sr."] = Grid2Data[i].Sr;
                        Row["Amount"] = Grid2Data[i].Amount;
                        Row["Mode"] = Grid2Data[i].Mode;
                        Row["No."] = Grid2Data[i].Ref_No;
                        Row["Date"] = Grid2Data[i].Ref_Date;
                        Row["Clearing Date"] = Grid2Data[i].Ref_CDate;
                        Row["Bank Name"] = Grid2Data[i].BANK_NAME;
                        Row["Branch"] = Grid2Data[i].Branch;
                        Row["A/c. No."] = Grid2Data[i].Acc_No;
                        Row["ID"] = Grid2Data[i].ID;
                        Row["MT_BANK_ID"] = Grid2Data[i].MT_BANK_ID;
                        Row["Money Transfer Bank"] = Grid2Data[i].MT_BANK_Name;
                        Row["Ref. A/c. No."] = Grid2Data[i].Ref_Acc_No;
                        Row["Edit Time"] = Grid2Data[i].Edit_Time;
                        Grid2.Rows.Add(Row);
                    }
                    DataView dv = new DataView(Grid2);
                    paymentVoucherDetailFormData.DTGridView2 = dv.ToTable();
                    paymentVoucherDetailFormData.DTGridView2.TableName = "Bank_Detail";
                    foreach (DataRow dRow in paymentVoucherDetailFormData.DTGridView2.Rows)
                    {
                        if (IsDate(Convert.ToString(dRow["Date"])))
                        {
                            dRow["Date"] = Convert.ToDateTime(dRow["Date"]).ToString(BASE._Server_Date_Format_Short);
                        }

                        if (IsDate(Convert.ToString(dRow["Clearing Date"])))
                        {
                            dRow["Clearing Date"] = Convert.ToDateTime(dRow["Clearing Date"]).ToString(BASE._Server_Date_Format_Short);
                        }

                    }
                    if (!(paymentVoucherDetailFormData.DTGridView2 == null))
                    {
                        paymentVoucherDetailFormData.DTGridView2.TableName = "VoucherDetailFormDataGrid2";
                    }
                }
                if (AdvanceGridData != null)
                {
                    DataTable Grid3 = CommonFunctions.ConvertToDataTable(AdvanceGridData);
                    DataView dv = new DataView(Grid3);
                    paymentVoucherDetailFormData.GridView3 = dv.ToTable();
                    paymentVoucherDetailFormData.GridView3.TableName = "ADVANCES_INFO";
                    paymentVoucherDetailFormData.GridView3.Columns["GivenDate"].ColumnName = "Given Date";
                    paymentVoucherDetailFormData.GridView3.Columns["Out_Standing"].ColumnName = "Out-Standing";
                    paymentVoucherDetailFormData.GridView3.Columns["Next_Year_Out_Standing"].ColumnName = "Next Year Out-Standing";
                    foreach (DataRow dRow in paymentVoucherDetailFormData.GridView3.Rows)
                    {
                        if (IsDate(Convert.ToString(dRow["Given Date"])))
                        {
                            dRow["Given Date"] = Convert.ToDateTime(dRow["Given Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                    }
                    if (!(paymentVoucherDetailFormData.GridView3 == null))
                    {
                        paymentVoucherDetailFormData.GridView3.TableName = "VoucherDetailFormDataGrid3";
                    }
                }
                if (LiabilityGridData != null)
                {
                    DataTable Grid4 = CommonFunctions.ConvertToDataTable(LiabilityGridData);
                    DataView dv = new DataView(Grid4);
                    paymentVoucherDetailFormData.GridView4 = dv.ToTable();
                    paymentVoucherDetailFormData.GridView4.TableName = "LIABILITIES_INFO";
                    paymentVoucherDetailFormData.GridView4.Columns["xDate"].ColumnName = "Date";
                    paymentVoucherDetailFormData.GridView4.Columns.Remove("Given_Date");
                    paymentVoucherDetailFormData.GridView4.Columns["OutStanding"].ColumnName = "Out-Standing";
                    paymentVoucherDetailFormData.GridView4.Columns["Next_Year_OutStanding"].ColumnName = "Next Year Out-Standing";
                    if (!(paymentVoucherDetailFormData.GridView4 == null))
                    {
                        paymentVoucherDetailFormData.GridView4.TableName = "voucherdetailformdatagrid4";
                    }
                }

                paymentVoucherDetailFormData.IsChk_Incompleted = false;
                paymentVoucherDetailFormData.xID = model.xID == null ? "xID" : model.xID;
                paymentVoucherDetailFormData.TitleX = model.TitleX;
                paymentVoucherDetailFormData.WindowText = model.Text;

                // Form's global variables
                if (BankGridData != null)
                {
                    DataTable Grid2 = new DataTable();
                    Grid2.Columns.Add("Sr.", Type.GetType("System.Int32"));
                    Grid2.Columns.Add("Amount", Type.GetType("System.Decimal"));
                    Grid2.Columns.Add("Mode", Type.GetType("System.String"));
                    Grid2.Columns.Add("No.", Type.GetType("System.String"));
                    Grid2.Columns.Add("Date", Type.GetType("System.String"));
                    Grid2.Columns.Add("Clearing Date", Type.GetType("System.String"));
                    Grid2.Columns.Add("Bank Name", Type.GetType("System.String"));
                    Grid2.Columns.Add("Branch", Type.GetType("System.String"));
                    Grid2.Columns.Add("A/c. No.", Type.GetType("System.String"));
                    Grid2.Columns.Add("ID", Type.GetType("System.String"));
                    Grid2.Columns.Add("MT_BANK_ID", Type.GetType("System.String"));
                    Grid2.Columns.Add("Money Transfer Bank", Type.GetType("System.String"));
                    Grid2.Columns.Add("Ref. A/c. No.", Type.GetType("System.String"));
                    Grid2.Columns.Add("Edit Time", Type.GetType("System.DateTime"));
                    var Grid2Data = (BankGridData);
                    for (int i = 0; i < Grid2Data.Count; i++)
                    {
                        DataRow Row = Grid2.NewRow();
                        Row["Sr."] = Grid2Data[i].Sr;
                        Row["Amount"] = Grid2Data[i].Amount;
                        Row["Mode"] = Grid2Data[i].Mode;
                        Row["No."] = Grid2Data[i].Ref_No;
                        Row["Date"] = Grid2Data[i].Ref_Date;
                        Row["Clearing Date"] = Grid2Data[i].Ref_CDate;
                        Row["Bank Name"] = Grid2Data[i].BANK_NAME;
                        Row["Branch"] = Grid2Data[i].Branch;
                        Row["A/c. No."] = Grid2Data[i].Acc_No;
                        Row["ID"] = Grid2Data[i].ID;
                        Row["MT_BANK_ID"] = Grid2Data[i].MT_BANK_ID;
                        Row["Money Transfer Bank"] = Grid2Data[i].MT_BANK_Name;
                        Row["Ref. A/c. No."] = Grid2Data[i].Ref_Acc_No;
                        Row["Edit Time"] = Grid2Data[i].Edit_Time;
                        Grid2.Rows.Add(Row);
                    }
                    paymentVoucherDetailGlobalData.Bank_Detail = Grid2;
                    paymentVoucherDetailGlobalData.Bank_Detail.TableName = "Bank_Detail";
                }
                if (!(paymentVoucherDetailGlobalData.Bank_Detail == null))
                {
                    foreach (DataRow dRow in paymentVoucherDetailGlobalData.Bank_Detail.Rows)
                    {
                        if (IsDate(Convert.ToString(dRow["Date"])))
                        {
                            dRow["Date"] = Convert.ToDateTime(dRow["Date"]).ToString(BASE._Server_Date_Format_Short);
                        }

                        if (IsDate(Convert.ToString(dRow["Clearing Date"])))
                        {
                            dRow["Clearing Date"] = Convert.ToDateTime(dRow["Clearing Date"]).ToString(BASE._Server_Date_Format_Short);
                        }
                    }
                }
                paymentVoucherDetailGlobalData.LastEditedOn = model.LastEditedOn;
                if (ItemGridData != null)
                {
                    paymentVoucherDetailGlobalData.DT = CommonFunctions.ConvertToDataTable(ItemGridData);
                    paymentVoucherDetailGlobalData.DT.TableName = "Item_Detail";
                    paymentVoucherDetailGlobalData.DT.Columns["Sr"].ColumnName = "Sr.";
                    paymentVoucherDetailGlobalData.DT.Columns["ItemName"].ColumnName = "Item Name";
                    paymentVoucherDetailGlobalData.DT.Columns["Qty"].ColumnName = "Qty.";
                }
                if (!(paymentVoucherDetailGlobalData.DT == null))
                {
                    foreach (DataRow dRow in paymentVoucherDetailGlobalData.DT.Rows)
                    {
                        if (IsDate(dRow["AI_PUR_DATE"].ToString())) dRow["AI_PUR_DATE"] = Convert.ToDateTime(dRow["AI_PUR_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LS_INS_DATE"].ToString())) dRow["LS_INS_DATE"] = Convert.ToDateTime(dRow["LS_INS_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["VI_REG_DATE"].ToString())) dRow["VI_REG_DATE"] = Convert.ToDateTime(dRow["VI_REG_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["VI_INS_EXPIRY_DATE"].ToString())) dRow["VI_INS_EXPIRY_DATE"] = Convert.ToDateTime(dRow["VI_INS_EXPIRY_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LB_PAID_DATE"].ToString())) dRow["LB_PAID_DATE"] = Convert.ToDateTime(dRow["LB_PAID_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LB_PERIOD_FROM"].ToString())) dRow["LB_PERIOD_FROM"] = Convert.ToDateTime(dRow["LB_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["LB_PERIOD_TO"].ToString())) dRow["LB_PERIOD_TO"] = Convert.ToDateTime(dRow["LB_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["TP_BILL_DATE"].ToString())) dRow["TP_BILL_DATE"] = Convert.ToDateTime(dRow["TP_BILL_DATE"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["TP_PERIOD_FROM"].ToString())) dRow["TP_PERIOD_FROM"] = Convert.ToDateTime(dRow["TP_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Long);
                        if (IsDate(dRow["TP_PERIOD_TO"].ToString())) dRow["TP_PERIOD_TO"] = Convert.ToDateTime(dRow["TP_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Long);
                    }
                }
                if (!(paymentVoucherDetailGlobalData.DT == null))
                {
                    paymentVoucherDetailGlobalData.DT.TableName = "DT";
                }
                paymentVoucherDetailGlobalData.LB_EXTENDED_PROPERTY_TABLE = LB_EXTENDED_PROPERTY_TABLE;
                if (!(paymentVoucherDetailGlobalData.LB_EXTENDED_PROPERTY_TABLE == null))
                {
                    paymentVoucherDetailGlobalData.LB_EXTENDED_PROPERTY_TABLE.TableName = "LB_EXTENDED_PROPERTY_TABLE";
                }
                paymentVoucherDetailGlobalData.LB_DOCS_ARRAY = LB_DOCS_ARRAY;
                if (!(paymentVoucherDetailGlobalData.LB_DOCS_ARRAY == null))
                {
                    paymentVoucherDetailGlobalData.LB_DOCS_ARRAY.TableName = "LB_DOCS_ARRAY";
                }
                paymentVoucherDetailGlobalData.iParty_Req = model.iParty_Req;
                //  paymentVoucherDetails.BasicData = paymentVoucherDetailBasicData;
                paymentVoucherDetails.FormData = paymentVoucherDetailFormData;
                paymentVoucherDetails.GlobalData = paymentVoucherDetailGlobalData;

                //FCRA Insert Process
                paymentVoucherDetails.Pmt_SplVchrReferenceSelected = model.Pmt_SplVchrReferenceSelected;
                
                // Service Call
                Voucher_Payment.Param_SaveButtonChecks retPaymentSaveChecks = BASE._Voucher_DBOps.GetPaymentSaveChecks(paymentVoucherDetails);
                if (retPaymentSaveChecks == null)
                {
                    return Json(new
                    {
                        message = "",
                        result = "close_form"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string iconclass = null;
                    string DialogResult = null;
                    string focusid = null;
                    string messageBoxText = null;
                    string messagewindowTitle = null;
                    string TooltipText = null;
                    string TooltipWindow = null;
                    if (!string.IsNullOrEmpty(retPaymentSaveChecks.ToolTipWindow))
                    {
                        TooltipWindow = retPaymentSaveChecks.ToolTipWindow;
                    }
                    if (!string.IsNullOrEmpty(retPaymentSaveChecks.ToolTipText))
                    {
                        TooltipText = retPaymentSaveChecks.ToolTipText;
                    }
                    if (!string.IsNullOrEmpty(retPaymentSaveChecks.messageCaption))
                    {
                        messagewindowTitle = retPaymentSaveChecks.messageCaption;
                    }
                    if (!string.IsNullOrEmpty(retPaymentSaveChecks.focusControlId))
                    {
                        focusid = retPaymentSaveChecks.focusControlId;
                        focusid = focusid == "GridView1" ? "Frm_Voucher_Win_Gen_Pay_Grid_1" : focusid == "GLookUp_PartyList1" ? "GLookUp_PartyList1_Tag" : focusid;
                    }
                    if (!string.IsNullOrEmpty(retPaymentSaveChecks.messageBoxText))
                    {
                        messageBoxText = retPaymentSaveChecks.messageBoxText;
                    }
                    if (!string.IsNullOrEmpty(retPaymentSaveChecks.messageIcon))
                    {
                        switch (retPaymentSaveChecks.messageIcon.ToLower())
                        {
                            case "asterisk":
                            case "information":
                                iconclass = "paymentErrorIconInformation";
                                break;
                            case "error":
                            case "stop":
                            case "hand":
                                iconclass = "paymentErrorIconHand";
                                break;
                            case "exclamation":
                            case "warning":
                                iconclass = "paymentErrorIconExclamation";
                                break;
                            case "question":
                                iconclass = "paymentErrorIconQuestion";
                                break;
                        }
                    }
                    if (!string.IsNullOrEmpty(retPaymentSaveChecks.dialogResult))
                    {
                        DialogResult = retPaymentSaveChecks.dialogResult.ToLower();
                    }
                    if (DialogResult == "ok")
                    {
                        return Json(new
                        {
                            message = messageBoxText,
                            messagecaption = messagewindowTitle,
                            CashbookGridPK = retPaymentSaveChecks.CashbookGridPK,
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (TooltipWindow != null)
                    {
                        focusid = focusid == "GLookUp_PartyList1" ? "GLookUp_PartyList1_Tag" : focusid;
                        messageBoxText = TooltipText;
                        messagewindowTitle = model.Text;
                        //ModelState.AddModelError(focusid, TooltipText);
                        //if (!ModelState.IsValid)
                        //{
                        //  return View("Frm_Voucher_Win_Gen_Pay", model);
                        //}
                    }
                    return Json(new
                    {
                        message = messageBoxText,
                        messagecaption = messagewindowTitle,
                        result = false,
                        iconclass = iconclass,
                        dialogresult = DialogResult,
                        focusid = focusid
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Txt_V_Date_EditValueChanging(string xMID, string NewValue)
        {
            DataTable TxnDates = BASE._Payment_DBOps.Get_CreatedAssets_MinTxnDate(xMID,Convert.ToDateTime(NewValue));
            if (TxnDates.Rows.Count > 0)
            {
                var Message = "Specified Voucher Date not allowed!!<br><br>A Transaction has been posted against " + TxnDates.Rows[0]["ProfName"] + " created in current voucher , for Rs." + TxnDates.Rows[0]["Amount"].ToString() + " on date(" + TxnDates.Rows[0]["MinTxnDate"].ToString() + ") which is less than voucher date specified by you.<br>Please specify a lower Voucher Date!!";
                return Json(new
                {
                    message = Message,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Difference Calculation
        public void LB_Amt_Calculation()
        {
            decimal xAmt = 0.00M;
            if (LiabilityGridData != null)
            {
                var GridView4 = LiabilityGridData;
                if (GridView4.Count > 0)
                {
                    GridView4 = GridView4.OrderBy(x => x.Sr).ToList();
                }
                for (int i = 0; i < GridView4.Count; i++)
                {
                    xAmt += Convert.ToDecimal(GridView4[i].Payment);
                }
                LiabilityGridData = GridView4;
            }
            Txt_LB_Amt = xAmt;
        }
        public void Advance_Amt_Calculation()
        {
            decimal xAmt = 0;
            if (AdvanceGridData != null)
            {
                var GridView3 = AdvanceGridData;
                if (GridView3.Count > 0)
                {
                    GridView3 = GridView3.OrderBy(x => x.Sr).ToList();
                }
                for (int i = 0; i < GridView3.Count; i++)
                {
                    xAmt += Convert.ToDecimal(GridView3[i].Payment);
                }
                AdvanceGridData = GridView3;
            }
            Txt_AdvAmt = xAmt;
            Difference_Calculation();
        }
        public void Bank_Amt_Calculation(bool Delete_Action)
        {
            decimal xAmt = 0;
            if (BankGridData != null)
            {
                var grid = BankGridData;

                if (grid.Count > 0)
                {
                    grid = grid.OrderBy(x => x.Sr).ToList();
                }
                for (int i = 0; i < grid.Count; i++)
                {
                    xAmt += Convert.ToDecimal(grid[i].Amount);
                }
                BankGridData = grid;
            }
            Txt_BankAmt = xAmt;
            Difference_Calculation();
        }
        public void Sub_Amt_Calculation(bool Delete_Action)
        {
            decimal xAmt = 0;
            decimal xTDS = 0;
            if (ItemGridData != null)
            {
                var grid = ItemGridData;
                if (grid.Count > 0)
                {
                    grid = grid.OrderBy(x => x.Sr).ToList();
                }
                for (int i = 0; i < grid.Count; i++)
                {
                    if (Delete_Action)
                    {
                        grid[i].Sr = i + 1;
                    }
                    xAmt = xAmt + grid[i].Amount;
                    xTDS = xTDS + grid[i].TDS;
                    if (grid[i].Item_Party_Req.ToUpper().Trim() == "YES")
                    {
                        Payment_iParty_Req = true;
                    }
                }
                ItemGridData = grid;
            }
            Txt_SubTotal = xAmt;
            Txt_TDS_Amt = xTDS;
            if (Txt_BankAmt + Txt_AdvAmt + Txt_CreditAmt > 0)
            {
                Txt_DiffAmt = Txt_SubTotal - (Txt_CashAmt + Txt_BankAmt + Txt_AdvAmt + Txt_CreditAmt);
            }
            else
            {
                Txt_DiffAmt = 0;
                if (BASE._IsVolumeCenter && (Payment_SelectedPaymentType == PaymentType.Credit))
                {
                    Txt_CreditAmt = Txt_SubTotal;
                }
                else
                {
                    Txt_CashAmt = Txt_SubTotal;
                }
            }
            Difference_Calculation();
        }
        public void Difference_Calculation()
        {
            if (Txt_BankAmt + Txt_AdvAmt + Txt_CreditAmt > 0)
            {
                Txt_CashAmt = 0.00M;
                Txt_DiffAmt = Txt_SubTotal - (Txt_CashAmt + Txt_BankAmt + Txt_AdvAmt + Txt_CreditAmt);
            }
            else
            {
                Txt_DiffAmt = 0;
                if (BASE._IsVolumeCenter && Payment_SelectedPaymentType == PaymentType.Credit)
                {
                    Txt_CreditAmt = Txt_SubTotal;
                }
                else
                {
                    Txt_CashAmt = Txt_SubTotal;
                }
            }
        }
        #endregion
        #region item reference check  
        public string CreatedItemReferenceChecks_AllGridRow(bool checkAdvDepOnly = false) 
        {
            var ReturnMessage = "";
            var count = ItemGridData.Count;           
            for (var i=0; i < count; i++) 
            {
                ReturnMessage = CreatedItemReferenceChecks(ItemGridData[i].Sr, checkAdvDepOnly);
                if (string.IsNullOrEmpty(ReturnMessage) == false) 
                {
                    break;
                }
            }
            return ReturnMessage;
        }
        public string CreatedItemReferenceChecks(int? GridRowNo = null, bool checkAdvDepOnly = false)
        {
            var ReturnMessage = "";
            var itemdata = ItemGridData;
            if (itemdata.Count > 0)
            {
                var itemID = itemdata.Where(x => x.Sr == GridRowNo).FirstOrDefault().Item_ID;
                var SelectedRow = itemdata.Where(x => x.Sr == GridRowNo).FirstOrDefault();
                DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(itemID);   //Gets Asset Profile
                string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                string xTemp_AssetID = "";
                bool isProperty = false;
                switch (xTemp_AssetProfile)
                {
                    case "GOLD":
                    case "SILVER":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        break;
                    case "OTHER ASSETS":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        break;
                    case "LIVESTOCK":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        break;
                    case "VEHICLES":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        break;
                    case "WIP":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        break;
                    case "OTHER DEPOSITS":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        break;
                    case "ADVANCES":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        break;
                    case "LAND & BUILDING":
                        xTemp_AssetID = SelectedRow.CREATION_PROF_REC_ID;
                        isProperty = true;
                        break;
                    case "NOT APPLICABLE":
                        var value = BASE._Voucher_DBOps.GetReferenceRecordID(Payment_xMID);
                        if (value != null)
                        {
                            xTemp_AssetID = value;
                        }
                        break;
                }
                if (!checkAdvDepOnly)
                {
                    ReturnMessage = ItemReferenceCheck(xTemp_AssetProfile, xTemp_AssetID);
                }
                else if (xTemp_AssetProfile == "OTHER DEPOSITS" || xTemp_AssetProfile == "ADVANCES")
                {
                    ReturnMessage = ItemReferenceCheck(xTemp_AssetProfile, xTemp_AssetID);
                }
            }
            return ReturnMessage;
        }
        public string ItemReferenceCheck(string xTemp_AssetProfile, string xTemp_AssetID)
        {
            string ReturnMessage = "";
            xTemp_AssetID = xTemp_AssetID ?? "";
            if ((xTemp_AssetID.Length > 0)
                && ((xTemp_AssetProfile == "GOLD")
                || (xTemp_AssetProfile == "SILVER")
                || (xTemp_AssetProfile == "OTHER ASSETS")
                || (xTemp_AssetProfile == "LIVESTOCK")
                || (xTemp_AssetProfile == "VEHICLES")
                || (xTemp_AssetProfile == "WIP")
                || (xTemp_AssetProfile == "LAND & BUILDING")
                || (xTemp_AssetProfile == "NOT APPLICABLE")))
            {
                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID, false);
                if (!(SaleRecord == null))
                {
                    if ((SaleRecord.Rows.Count > 0))
                    {
                        ReturnMessage = "Sorry ! Selected Entry creates/refers a asset which was sold on "
                                    + (Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + (" for initial payment of Rs."
                                    + (SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".")));
                        return ReturnMessage;
                    }
                }

                DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_Payment, 0, xTemp_AssetID, false);
                // Bug #5339 fix
                if (!(AssetTrfRecord == null))
                {
                    if (AssetTrfRecord.Rows.Count > 0)
                    {
                        ReturnMessage = ("Sorry ! Selected Entry creates/refers asset which was Transfered on "
                                    + (Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + (" for initial payment of Rs."
                                    + (AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "."))));
                        return ReturnMessage;
                    }
                }
                if (xTemp_AssetProfile != "NOT APPLICABLE")
                {
                    // Reference need not be checked in case of Construction entries
                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID);
                    if (!(ReferenceRecord == null))
                    {
                        if ((ReferenceRecord.Rows.Count > 0))
                        {
                            ReturnMessage = ("Sorry ! Selected Entry creates/refers asset which was referred in a Dependent Entry Dated "
                                        + (Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + (" of Rs."
                                        + (ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + "."))));
                            return ReturnMessage;
                        }
                    }
                }
            }

            if ((xTemp_AssetProfile == "OTHER DEPOSITS"))
            {
                // 'Check If created deposit is used anywhere 
                if (!(xTemp_AssetID == null))
                {
                    string xTemp_DepID = xTemp_AssetID;
                    if (xTemp_DepID.Length > 0)
                    {
                        // Adjustments/ Refund has been made againt the deposit raised
                        DataTable txnReferDeposits = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_DepID);
                        if (!(txnReferDeposits == null))
                        {
                            if ((txnReferDeposits.Rows.Count > 0))
                            {
                                ReturnMessage = ("Sorry ! Some adjustment / refund has been made against the deposit raised in current transaction on "
                                            + (Convert.ToDateTime(txnReferDeposits.Rows[0]["TR_DATE"]).ToLongDateString() + (" for Rs."
                                            + (txnReferDeposits.Rows[0]["TR_AMOUNT"].ToString() + "."))));
                            }
                        }

                        Parameter_GetJornalEntryAdjustments paramJornalEntry = new Parameter_GetJornalEntryAdjustments();
                        paramJornalEntry.CrossRefId = xTemp_DepID;
                        paramJornalEntry.SpecifiedEntryType = EntryType.Both;
                        paramJornalEntry.Excluded_Rec_M_ID = Payment_xMID;
                        paramJornalEntry.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                        txnReferDeposits = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers);
                        if (!(txnReferDeposits == null))
                        {
                            if ((txnReferDeposits.Rows.Count > 0))
                            {

                                ReturnMessage = "Sorry! Deposit created by current entry is used in some other journal  entry...!";
                            }
                        }
                    }
                }
            }
            if (xTemp_AssetProfile == "ADVANCES")
            {
                // 'Check If created deposit is used anywhere 
                if (!(xTemp_AssetID == null))
                {
                    string xTemp_AdvID = xTemp_AssetID;
                    if (xTemp_AdvID.Length > 0)
                    {
                        // Adjustments/ Refund has been made againt the deposit raised
                        DataTable txnReferAdvances = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID);
                        if (!(txnReferAdvances == null))
                        {
                            if ((txnReferAdvances.Rows.Count > 0))
                            {
                                ReturnMessage = ("Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on "
                                            + (Convert.ToDateTime(txnReferAdvances.Rows[0]["TR_DATE"]).ToLongDateString() + (" for Rs."
                                            + (txnReferAdvances.Rows[0]["TR_AMOUNT"].ToString() + "."))));
                            }
                        }

                        Parameter_GetJornalEntryAdjustments paramJornalEntry = new Parameter_GetJornalEntryAdjustments();
                        paramJornalEntry.CrossRefId = xTemp_AdvID;
                        paramJornalEntry.SpecifiedEntryType = EntryType.Both;
                        paramJornalEntry.Excluded_Rec_M_ID = Payment_xMID;
                        paramJornalEntry.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                        txnReferAdvances = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers);
                        if (!(txnReferAdvances == null))
                        {
                            if ((txnReferAdvances.Rows.Count > 0))
                            {
                                ReturnMessage = "Sorry! Advance created by current entry is used in some other journal entry ...!";
                            }
                        }
                    }
                }
            }
            return ReturnMessage;
        }

        #endregion
        #region Frm_Voucher_Win_Gen_Pay_Item
        public ActionResult Frm_Voucher_Win_Gen_Pay_Grid_1()
        {
            List<Payment_Grid_Datatable> data;
            if (ItemGridData == null)
            {
                data = new List<Payment_Grid_Datatable>();
            }
            else
            {
                data = ItemGridData;
            }
            //int count = 1;
            //foreach (var item in data)
            //{
            //    item.Sr = count;
            //    count++;
            //}
            return PartialView(data);
        } //item grid call back fn
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Gen_Pay_Item(string ActionMethod = null, bool iSpecific_Allow = false, string iSpecific_ItemID = "", string Vdt = null, string iPartyID = "", int GridRowNo = 0)
        {            
            PaymentItem model = new PaymentItem();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.TempActionMethod = "_" + ActionMethod;
            model.ActionMethod = Tag;
            model.Cmb_Unit = "NO";
            model.BE_TDS_Section = "NA";
            model.BE_TDS_Rate = "NA";
            model.AllowAmountEdit = true;
            model.Calc_Allow = true;
            model.iSpecific_Allow = false;
            model.iMinValue = 0;
            model.iMaxValue = 0;
            model.ItemDataList = LookUp_GetItemList();
            model.PartyDataList = LookUp_GetPartyList_Item();
            model.PurposeDataList = LookUp_GetPurposeList();
            if (model.ItemDataList.Count == 1) 
            {
                model.GLookUp_ItemList = model.ItemDataList[0].ITEMID;
            }
            switch (ActionMethod)
            {
                case "New":
                    model.iSpecific_Allow = iSpecific_Allow;
                    model.iSpecific_ItemID = iSpecific_ItemID;
                    if (model.iSpecific_Allow == true)
                    {
                        model.GLookUp_ItemList = iSpecific_ItemID;
                    }
                    model.Vdt = IsDate(Vdt) ? Convert.ToDateTime(Convert.ToDateTime(Vdt).ToString(BASE._Date_Format_Current)) : (DateTime?)null;
                    model.iPartyID = iPartyID;
                    model.GLookUp_PartyList = iPartyID;
                    if (Payment_Tag == "_Edit")
                    {
                        model.iTxnM_ID = Payment_xMID;
                    }
                    if (BASE._IsVolumeCenter)
                    {
                        model.GLookUp_PurList = "8f6b3279-166a-4cd9-8497-ca9fc6283b25";
                    }
                    break;
                case "View":
                case "Edit":
                    model.AllowAmountEdit = true;
                    var GridView1FocusedRowHandle = ItemGridData.FirstOrDefault(x => x.Sr == GridRowNo);
                    model.Sr = GridRowNo;
                    if (Tag == Common.Navigation_Mode._Edit)
                    {
                        model.AllowAmountEdit = (CreatedItemReferenceChecks(GridRowNo).Length > 0) ? false : true;
                    }
                    model.GLookUp_ItemList= GridView1FocusedRowHandle.Item_ID;
                    model.iSpecific_ItemID = GridView1FocusedRowHandle.Item_ID;
                    model.iProfile_OLD = GridView1FocusedRowHandle.Item_Profile;
                    model.iPur_ID = GridView1FocusedRowHandle.Pur_ID;
                    model.GLookUp_PurList = model.iPur_ID;
                    model.Txt_Qty = GridView1FocusedRowHandle.Qty;
                    model.Txt_Rate = GridView1FocusedRowHandle.Rate;
                    model.Txt_Amt = GridView1FocusedRowHandle.Amount + GridView1FocusedRowHandle.TDS;
                    model.TXT_TDS = GridView1FocusedRowHandle.TDS;
                    model.Txt_Remarks = GridView1FocusedRowHandle.Remarks;
                    model.LB_REC_ID = GridView1FocusedRowHandle.LB_REC_ID;
                    if (GridView1FocusedRowHandle.LB_REC_EDIT_ON.ToString().Length > 0)
                    {
                        model.LB_REC_EDIT_ON = GridView1FocusedRowHandle.LB_REC_EDIT_ON;
                    }
                    model.iTxnM_ID = Payment_xMID;
                    model.iPartyID = iPartyID;
                    model.GLookUp_PartyList = iPartyID;
                    if (GridView1FocusedRowHandle.Item_Profile.ToUpper() == "GOLD" || GridView1FocusedRowHandle.Item_Profile.ToUpper() == "SILVER")
                    {
                        model.GS_DESC_MISC_ID = GridView1FocusedRowHandle.GS_DESC_MISC_ID;
                        model.GS_ITEM_WEIGHT = Convert.ToDecimal(GridView1FocusedRowHandle.GS_ITEM_WEIGHT);
                        model.X_LOC_ID = GridView1FocusedRowHandle.LOC_ID;
                    }
                    if (GridView1FocusedRowHandle.Item_Profile.ToUpper() == "OTHER ASSETS")
                    {
                        model.AI_TYPE = GridView1FocusedRowHandle.AI_TYPE;
                        model.AI_MAKE = GridView1FocusedRowHandle.AI_MAKE;
                        model.AI_MODEL = GridView1FocusedRowHandle.AI_MODEL;
                        model.AI_SERIAL_NO = GridView1FocusedRowHandle.AI_SERIAL_NO;
                        model.AI_PUR_DATE = GridView1FocusedRowHandle.AI_PUR_DATE;
                        model.AI_WARRANTY = GridView1FocusedRowHandle.AI_WARRANTY;
                        if (!Convert.IsDBNull(GridView1FocusedRowHandle.AI_IMAGE))
                        {
                            Payment_Asset_Image = GridView1FocusedRowHandle.AI_IMAGE;
                        }
                        model.X_LOC_ID = GridView1FocusedRowHandle.LOC_ID;
                    }
                    if (GridView1FocusedRowHandle.Item_Profile.ToUpper() == "LIVESTOCK")
                    {
                        model.LS_NAME = GridView1FocusedRowHandle.LS_NAME;
                        model.LS_BIRTH_YEAR = GridView1FocusedRowHandle.LS_BIRTH_YEAR;
                        model.LS_INSURANCE = GridView1FocusedRowHandle.LS_INSURANCE;
                        model.LS_INSURANCE_ID = GridView1FocusedRowHandle.LS_INSURANCE_ID;
                        model.LS_INS_POLICY_NO = GridView1FocusedRowHandle.LS_INS_POLICY_NO;
                        model.LS_INS_AMT = GridView1FocusedRowHandle.LS_INS_AMT;
                        model.LS_INS_DATE = GridView1FocusedRowHandle.LS_INS_DATE;
                        model.X_LOC_ID = GridView1FocusedRowHandle.LOC_ID;
                    }
                    if (GridView1FocusedRowHandle.Item_Profile.ToUpper() == "VEHICLES")
                    {
                        model.VI_MAKE = GridView1FocusedRowHandle.VI_MAKE;
                        model.VI_MODEL = GridView1FocusedRowHandle.VI_MODEL;
                        model.VI_REG_NO_PATTERN = GridView1FocusedRowHandle.VI_REG_NO_PATTERN;
                        model.VI_REG_NO = GridView1FocusedRowHandle.VI_REG_NO;
                        model.VI_REG_DATE = GridView1FocusedRowHandle.VI_REG_DATE;
                        model.VI_OWNERSHIP = GridView1FocusedRowHandle.VI_OWNERSHIP;
                        model.VI_OWNERSHIP_AB_ID = GridView1FocusedRowHandle.VI_OWNERSHIP_AB_ID;
                        model.VI_DOC_RC_BOOK = GridView1FocusedRowHandle.VI_DOC_RC_BOOK.ToString();
                        model.VI_DOC_AFFIDAVIT = GridView1FocusedRowHandle.VI_DOC_AFFIDAVIT.ToString();
                        model.VI_DOC_WILL = GridView1FocusedRowHandle.VI_DOC_WILL.ToString();
                        model.VI_DOC_TRF_LETTER = GridView1FocusedRowHandle.VI_DOC_TRF_LETTER.ToString();
                        model.VI_DOC_FU_LETTER = GridView1FocusedRowHandle.VI_DOC_FU_LETTER.ToString();
                        model.VI_DOC_OTHERS = GridView1FocusedRowHandle.VI_DOC_OTHERS.ToString();
                        model.VI_DOC_NAME = GridView1FocusedRowHandle.VI_DOC_NAME;
                        model.VI_INSURANCE_ID = GridView1FocusedRowHandle.VI_INSURANCE_ID;
                        model.VI_INS_POLICY_NO = GridView1FocusedRowHandle.VI_INS_POLICY_NO;
                        model.VI_INS_EXPIRY_DATE = GridView1FocusedRowHandle.VI_INS_EXPIRY_DATE;
                        model.X_LOC_ID = GridView1FocusedRowHandle.LOC_ID;
                    }
                    if (GridView1FocusedRowHandle.Item_Profile.ToUpper() == "LAND & BUILDING")
                    {
                        model.LB_PRO_TYPE = GridView1FocusedRowHandle.LB_PRO_TYPE;
                        model.LB_PRO_CATEGORY = GridView1FocusedRowHandle.LB_PRO_CATEGORY;
                        model.LB_PRO_USE = GridView1FocusedRowHandle.LB_PRO_USE;
                        model.LB_PRO_NAME = GridView1FocusedRowHandle.LB_PRO_NAME;
                        model.LB_PRO_ADDRESS = GridView1FocusedRowHandle.LB_PRO_ADDRESS;
                        model.LB_ADDRESS1 = GridView1FocusedRowHandle.LB_ADDRESS1;
                        model.LB_ADDRESS2 = GridView1FocusedRowHandle.LB_ADDRESS2;
                        model.LB_ADDRESS3 = GridView1FocusedRowHandle.LB_ADDRESS3;
                        model.LB_ADDRESS4 = GridView1FocusedRowHandle.LB_ADDRESS4;
                        model.LB_CITY_ID = GridView1FocusedRowHandle.LB_CITY_ID;
                        model.LB_DISTRICT_ID = GridView1FocusedRowHandle.LB_DISTRICT_ID;
                        model.LB_STATE_ID = GridView1FocusedRowHandle.LB_STATE_ID;
                        model.LB_PINCODE = GridView1FocusedRowHandle.LB_PINCODE;
                        model.LB_OWNERSHIP = GridView1FocusedRowHandle.LB_OWNERSHIP;
                        model.LB_OWNERSHIP_PARTY_ID = GridView1FocusedRowHandle.LB_OWNERSHIP_PARTY_ID;
                        model.LB_SURVEY_NO = GridView1FocusedRowHandle.LB_SURVEY_NO;
                        model.LB_CON_YEAR = GridView1FocusedRowHandle.LB_CON_YEAR;
                        model.LB_RCC_ROOF = GridView1FocusedRowHandle.LB_RCC_ROOF;
                        model.LB_PAID_DATE = GridView1FocusedRowHandle.LB_PAID_DATE;
                        model.LB_PERIOD_FROM = GridView1FocusedRowHandle.LB_PERIOD_FROM;
                        model.LB_PERIOD_TO = GridView1FocusedRowHandle.LB_PERIOD_TO;
                        model.LB_DOC_OTHERS = GridView1FocusedRowHandle.LB_DOC_OTHERS;
                        model.LB_DOC_NAME = GridView1FocusedRowHandle.LB_DOC_NAME;
                        model.LB_OTHER_DETAIL = GridView1FocusedRowHandle.LB_OTHER_DETAIL;
                        model.LB_TOT_P_AREA = GridView1FocusedRowHandle.LB_TOT_P_AREA;
                        model.LB_CON_AREA = GridView1FocusedRowHandle.LB_CON_AREA;
                        model.LB_DEPOSIT_AMT = GridView1FocusedRowHandle.LB_DEPOSIT_AMT;
                        model.LB_MONTH_RENT = GridView1FocusedRowHandle.LB_MONTH_RENT;
                        model.LB_MONTH_O_PAYMENTS = GridView1FocusedRowHandle.LB_MONTH_O_PAYMENTS;

                        DataTable EDIT_LB_EXTENDED_PROPERTY_TABLE = new DataTable();
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

                        if (!(LB_EXTENDED_PROPERTY_TABLE == null))
                        {
                            foreach (DataRow XROW in LB_EXTENDED_PROPERTY_TABLE.Rows)
                            {
                                if (XROW["LB_REC_ID"].ToString() == model.LB_REC_ID)
                                {
                                    DataRow Row = EDIT_LB_EXTENDED_PROPERTY_TABLE.NewRow();
                                    Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"];
                                    Row["LB_SR_NO"] = XROW["LB_SR_NO"];
                                    Row["LB_INS_ID"] = XROW["LB_INS_ID"];
                                    Row["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"]=="" ? 0 : XROW["LB_TOT_P_AREA"];
                                    Row["LB_CON_AREA"] = XROW["LB_CON_AREA"]=="" ? 0 : XROW["LB_CON_AREA"];
                                    Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"];
                                    Row["LB_VALUE"] = XROW["LB_VALUE"]==""?0: XROW["LB_VALUE"];
                                    Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"];
                                    Row["LB_REC_ID"] = model.LB_REC_ID;
                                    EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                                }
                            }
                        }
                        else
                        {
                            DataTable LB_Ext = BASE._L_B_DBOps.GetExtensionDetails(model.LB_REC_ID, ClientScreen.Accounts_Voucher_Payment);
                            foreach (DataRow XROW in LB_Ext.Rows)
                            {
                                DataRow Row = EDIT_LB_EXTENDED_PROPERTY_TABLE.NewRow();
                                Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"];
                                Row["LB_SR_NO"] = XROW["LB_SR_NO"];
                                Row["LB_INS_ID"] = XROW["LB_INS_ID"];
                                Row["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"];
                                Row["LB_CON_AREA"] = XROW["LB_CON_AREA"];
                                Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"];
                                Row["LB_VALUE"] = XROW["LB_VALUE"];
                                Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"];
                                Row["LB_REC_ID"] = model.LB_REC_ID;
                                EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                            }
                        }
                        model.LB_EXTENDED_PROPERTY_TABLE = EDIT_LB_EXTENDED_PROPERTY_TABLE;
                        List<Models.LB_EXTENDED_PROPERTY_TABLE_List> return_LB_EXTENDED_PROPERTY_TABLE_List = new List<Models.LB_EXTENDED_PROPERTY_TABLE_List>();
                        foreach (DataRow Row in model.LB_EXTENDED_PROPERTY_TABLE.Rows)
                        {
                            Models.LB_EXTENDED_PROPERTY_TABLE_List newrow = new Models.LB_EXTENDED_PROPERTY_TABLE_List();
                            newrow.LB_MOU_DATE = Row["LB_MOU_DATE"].ToString();
                            newrow.LB_SR_NO = Row["LB_SR_NO"].ToString();
                            newrow.LB_INS_ID = Row["LB_INS_ID"].ToString();
                            newrow.LB_TOT_P_AREA = Row["LB_TOT_P_AREA"].ToString();
                            newrow.LB_CON_AREA = Row["LB_CON_AREA"].ToString();
                            newrow.LB_CON_YEAR = Row["LB_CON_YEAR"].ToString();
                            newrow.LB_VALUE = Row["LB_VALUE"].ToString();
                            newrow.LB_OTHER_DETAIL = Row["LB_OTHER_DETAIL"].ToString();
                            newrow.LB_REC_ID = Row["LB_REC_ID"].ToString();
                            return_LB_EXTENDED_PROPERTY_TABLE_List.Add(newrow);
                        }
                        model.List_LB_EXTENDED_PROPERTY_TABLE = new JavaScriptSerializer().Serialize(return_LB_EXTENDED_PROPERTY_TABLE_List);

                        DataTable EDIT_LB_DOCS_ARRAY = new DataTable();
                        EDIT_LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                        EDIT_LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        if (!(LB_DOCS_ARRAY == null))
                        {
                            foreach (DataRow XROW in LB_DOCS_ARRAY.Rows)
                            {
                                if (XROW["LB_REC_ID"].ToString() == model.LB_REC_ID)
                                {
                                    DataRow Row = EDIT_LB_DOCS_ARRAY.NewRow();
                                    Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                                    Row["LB_REC_ID"] = model.LB_REC_ID;
                                    EDIT_LB_DOCS_ARRAY.Rows.Add(Row);
                                }
                            }
                        }
                        else
                        {
                            DataTable LB_DOC = BASE._L_B_DBOps.GetDocsDetails(model.LB_REC_ID, ClientScreen.Accounts_Voucher_Payment);
                            foreach (DataRow XROW in LB_DOC.Rows)
                            {
                                DataRow Row = EDIT_LB_DOCS_ARRAY.NewRow();
                                Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                                Row["LB_REC_ID"] = model.LB_REC_ID;
                                EDIT_LB_DOCS_ARRAY.Rows.Add(Row);
                            }
                        }
                        model.LB_DOCS_ARRAY = EDIT_LB_DOCS_ARRAY;
                        List<Models.LB_DOCS_ARRAY_List> return_LB_DOCS_ARRAY = new List<Models.LB_DOCS_ARRAY_List>();
                        foreach (DataRow XROW in model.LB_DOCS_ARRAY.Rows)
                        {
                            Models.LB_DOCS_ARRAY_List newrow = new Models.LB_DOCS_ARRAY_List();
                            newrow.LB_MISC_ID = XROW["LB_MISC_ID"].ToString();
                            newrow.LB_REC_ID = XROW["LB_REC_ID"].ToString();
                            return_LB_DOCS_ARRAY.Add(newrow);
                        }
                        model.List_LB_DOCS_ARRAY = new JavaScriptSerializer().Serialize(return_LB_DOCS_ARRAY);
                    }
                    if (GridView1FocusedRowHandle.Item_Profile.ToUpper() == "TELEPHONE BILL")
                    {
                        model.X_TP_ID = GridView1FocusedRowHandle.TP_ID;
                        model.TP_BILL_NO = GridView1FocusedRowHandle.TP_BILL_NO;
                        model.TP_BILL_DATE = GridView1FocusedRowHandle.TP_BILL_DATE;
                        model.TP_PERIOD_FROM = GridView1FocusedRowHandle.TP_PERIOD_FROM;
                        model.TP_PERIOD_TO = GridView1FocusedRowHandle.TP_PERIOD_TO;
                    }
                    if (GridView1FocusedRowHandle.Item_Profile.ToUpper() == "WIP")
                    {
                        model.iReference = GridView1FocusedRowHandle.REFERENCE;
                        model.Ref_RecID = GridView1FocusedRowHandle.REF_REC_ID;
                        model.iRefType = GridView1FocusedRowHandle.WIP_REF_TYPE;
                    }
                    break;
                case "Delete":
                    var GridView1FocusedRow = ItemGridData.FirstOrDefault(x => x.Sr == GridRowNo);
                    if (Payment_Tag == "_Edit")
                    {
                        if (GridView1FocusedRow.Item_Profile == "LAND & BUILDING" || GridView1FocusedRow.Item_Profile == "OTHER ASSETS")
                        {
                            if (BASE.IsInsuranceAudited())
                            {
                                return Json(new { result = false, message = "Insurance Related Assets Cannot Be Deleted After The Completeion Of Insurance Audit" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (GridView1FocusedRow.ITEM_VOUCHER_TYPE.ToUpper() == "LAND & BUILDING" && !(GridView1FocusedRow.Item_Profile == "LAND & BUILDING"))
                        {
                            if (BASE.IsInsuranceAudited())
                            {
                                return Json(new { result = false, message = "Property Related Expenses Cannot Be Deleted After The Completeion Of Insurance Audit" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        string RefMessage = CreatedItemReferenceChecks(GridRowNo);
                        if (RefMessage.Length > 0)
                        {
                            return Json(new { result = false, message = RefMessage }, JsonRequestBehavior.AllowGet);
                        }
                        if (GridView1FocusedRow.Item_Profile == "WIP" && GridView1FocusedRow.WIP_REF_TYPE == "EXISTING")
                        {
                            object RefId = GridView1FocusedRow.REF_REC_ID;
                            if (!(RefId == null))
                            {
                                if (RefId.ToString().Length > 0)
                                {
                                    DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, "WIP", GridView1FocusedRow.Item_ID, Payment_xMID, null, Common.Navigation_Mode._Delete, RefId.ToString());
                                    if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                                    {
                                        if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                        {
                                            string Message = "Sorry !Deletion of Selected Payment Entry creates a Negative Closing Balance in Next Year for " + GridView1FocusedRow.Item_Profile.ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                            return Json(new { result = false, message = Message }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                    {
                                        string Message = "Sorry !Deletion of Selected Payment Entry creates a Negative Closing Balance in Current Year for " + GridView1FocusedRow.Item_Profile.ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                        return Json(new { result = false, message = Message }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                    var allData = ItemGridData;
                    allData.Remove(GridView1FocusedRow);
                    for (int i = 0; i < allData.Count; i++)
                    {
                        allData[i].Sr = i + 1;
                    }
                    ItemGridData = allData;
                    return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);
            }

            return View("Frm_Voucher_Win_Gen_Pay_Item", model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Gen_Pay_Item(PaymentItem model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var Tag = model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);

                if ((Tag == Common.Navigation_Mode._New) || (Tag == Common.Navigation_Mode._Edit) || (Tag == Common.Navigation_Mode._New_From_Selection))
                {
                    if (string.IsNullOrEmpty(model.GLookUp_ItemList))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_ItemList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }        
                    if (string.IsNullOrWhiteSpace(model.Cmb_Unit))
                    {
                        jsonParam.message = "Unit Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Cmb_Unit";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Amt <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amt";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Qty.ToString().Trim().Contains(".") && model.iProfile.ToUpper() == "OTHER ASSETS")
                    {
                        jsonParam.message = "Quantity cannot be partial in case of Movable Asset...";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Qty";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_PartyList) && (model.iParty_Req.ToUpper().Trim() == "YES"))
                    {
                        jsonParam.message = "Party is Required...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PartyList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (string.IsNullOrEmpty(model.GLookUp_PurList))
                    {
                        jsonParam.message = "Purpose Not Selected...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_PurList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                bool NewProfile = false;
                if (!(string.IsNullOrEmpty(model.iProfile_OLD)))
                {
                    if (model.iProfile != model.iProfile_OLD)
                    {
                        NewProfile = true;
                    }
                }
                //-------------------------Check this code for opening new popups--------------------------

                if (model.iVoucher_Type.Trim().ToUpper() == "LAND & BUILDING" && !(model.iProfile.ToUpper() == "LAND & BUILDING"))
                {
                    var db_Table = BASE._Payment_DBOps.GetLandBuildingNames(string.IsNullOrEmpty(model.iTxnM_ID) ? "" : model.iTxnM_ID);
                    if (db_Table == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error..";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }
                    if (((DataTable)db_Table).Rows.Count <= 0)
                    {
                        jsonParam.message = "No Selectable Property Exists! <br><br>Please create a Land and Building entry before adding expenses for construction <br>or add a different voucher for expenses if you are using same voucher for property purchase and expenses!";
                        jsonParam.title = "Add Property First...";
                        jsonParam.result = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.result = true;
                    jsonParam.popup_title = "Select Property";
                    jsonParam.popup_form_name = "Frm_Property_Select";
                    jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Property_Select/";
                    jsonParam.popup_querystring = "LB_REC_ID=" + Url.Encode(model.LB_REC_ID) + "&Txn_M_ID=" + Url.Encode(model.iTxnM_ID) + "&SessionGUID=" + Url.Encode(SessionGUID);
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (model.iProfile == "WIP")
                {
                    jsonParam.result = true;
                    jsonParam.popup_title = "Reference type";
                    jsonParam.popup_form_name = "Frm_Reference_Type";
                    jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Reference_Type/";
                    jsonParam.popup_querystring = "Tag=" + Url.Encode(model.ActionMethod.ToString()) + "&iRefType=" + Url.Encode(model.iRefType) + "&iLed_ID=" + Url.Encode(model.iLed_ID) + "&SessionGUID=" + Url.Encode(SessionGUID);
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }

                switch (model.iProfile.ToUpper())
                {
                    case "TELEPHONE BILL":

                        Common.Navigation_Mode xfrmTag = 0;
                        if (model.ActionMethod == Common.Navigation_Mode._New)
                        {
                            xfrmTag = Common.Navigation_Mode._New;
                        }
                        if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                xfrmTag = Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                                {
                                    xfrmTag = Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    xfrmTag = Common.Navigation_Mode._View;
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = xfrmTag.ToString().Substring(1) + " ~ Item Detail (Telephone Detail)...";
                        jsonParam.popup_form_name = "Frm_Telephone_Select";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Telephone_Select/";
                        jsonParam.popup_querystring = "Tag=" + Url.Encode(xfrmTag.ToString()) +
                            "&xID=" + Url.Encode(model.X_TP_ID) +
                            "&TP_BILL_NO=" + Url.Encode(model.TP_BILL_NO) +
                            "&TP_BILL_DATE=" + Url.Encode(model.TP_BILL_DATE) +
                            "&TP_PERIOD_FROM=" + Url.Encode(model.TP_PERIOD_FROM) +
                            "&TP_PERIOD_TO=" + Url.Encode(model.TP_PERIOD_TO) + "&SessionGUID=" + Url.Encode(SessionGUID);
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    case "GOLD":
                    case "SILVER":
                        model.Calc_Allow = false;
                        jsonParam.popup_querystring = "Cmd_Type=" + Url.Encode(model.iProfile) +
                           "&BE_ItemName=" + Url.Encode(model.Item_Name) + "&SessionGUID=" + Url.Encode(SessionGUID);
                        xfrmTag = 0;
                        if (model.ActionMethod == Common.Navigation_Mode._New)
                        {
                            xfrmTag = Common.Navigation_Mode._New;
                        }
                        if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                xfrmTag = Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                                {
                                    xfrmTag = Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    xfrmTag = Common.Navigation_Mode._View;
                                }
                                jsonParam.popup_querystring = jsonParam.popup_querystring +                           
                           "&GS_DESC_MISC_ID=" + Url.Encode(model.GS_DESC_MISC_ID) +
                           "&GS_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID) +
                           "&Txt_Weight=" + model.GS_ITEM_WEIGHT +
                           "&Txt_Others=" + Url.Encode(model.Txt_Remarks);
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = xfrmTag.ToString().Substring(1) + " ~ Item Detail (Gold / Silver Detail)...";
                        jsonParam.popup_form_name = "Frm_GoldSilver_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_GoldSilver_Window/";
                        jsonParam.popup_querystring = jsonParam.popup_querystring+
                            "&Tag=" + Url.Encode(xfrmTag.ToString());
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    case "OTHER ASSETS":
                        model.Calc_Allow = false;
                        xfrmTag = 0;                
                        jsonParam.popup_form_name = "Frm_Asset_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Asset_Window/";
                        jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.Item_Name)+ "&SessionGUID=" + Url.Encode(SessionGUID);           
                        if (model.ActionMethod == Common.Navigation_Mode._New)
                        {
                            Payment_Asset_Image = null;
                            xfrmTag = Common.Navigation_Mode._New;
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Amt=" + model.Txt_Amt + "&Txt_Rate=" + model.Txt_Rate + "&Txt_Qty=" + model.Txt_Qty + "&Txt_Date=" + Url.Encode(model.Vdt.ToString());                        
                        }
                        if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._View)
                        {
                           if (NewProfile)
                            {
                                xfrmTag = Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                                {
                                    xfrmTag = Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    xfrmTag = Common.Navigation_Mode._View;
                                }                  
                                jsonParam.popup_querystring = jsonParam.popup_querystring +
                                                              "&Txt_Make=" + Url.Encode(model.AI_MAKE) +
                                                              "&Txt_Model=" + Url.Encode(model.AI_MODEL) +
                                                              "&Txt_Serial=" + Url.Encode(model.AI_SERIAL_NO) +
                                                              "&Txt_Warranty=" + model.AI_WARRANTY +
                                                              "&AI_PUR_DATE=" + model.AI_PUR_DATE +
                                                              "&Txt_Amt=" + model.Txt_Amt +
                                                              "&Txt_Rate=" + model.Txt_Rate +
                                                              "&Txt_Qty=" + model.Txt_Qty +
                                                              "&Txt_Others=" + Url.Encode(model.Txt_Remarks) +
                                                              "&AI_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID);
                                if (!(model.AI_IMAGE == null))
                                {
                                    Payment_Asset_Image = model.AI_IMAGE;

                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Url.Encode(xfrmTag.ToString());                       
                        jsonParam.popup_title = xfrmTag.ToString().Substring(1) + " ~ Item Detail (Movable Asset Detail)...";                   
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    case "LIVESTOCK":
                        model.Calc_Allow = false;
                        jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.Item_Name) + "&SessionGUID=" + Url.Encode(SessionGUID);
                        xfrmTag = 0;
                        if (model.ActionMethod == Common.Navigation_Mode._New)
                        {
                            xfrmTag = Common.Navigation_Mode._New;
                        }
                        if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                xfrmTag = Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                                {
                                    xfrmTag = Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    xfrmTag = Common.Navigation_Mode._View;
                                }
                                jsonParam.popup_querystring = jsonParam.popup_querystring+                           
                           "&Txt_Name=" + Url.Encode(model.LS_NAME) +
                           "&LS_BIRTH_YEAR=" + Url.Encode(model.LS_BIRTH_YEAR) +
                           "&LS_INSURANCE=" + Url.Encode(model.LS_INSURANCE) +
                           "&LS_INS_ID=" + Url.Encode(model.LS_INSURANCE_ID) +
                           "&LS_INS_POLICY_NO=" + Url.Encode(model.LS_INS_POLICY_NO) +
                           "&LS_INS_AMT=" + model.LS_INS_AMT +
                           "&LS_INS_DATE=" + Url.Encode(model.LS_INS_DATE) +
                           "&Txt_Others=" + Url.Encode(model.Txt_Remarks) +
                           "&LS_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID);
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = xfrmTag.ToString().Substring(1) + " ~ Item Detail (Livestock Detail)...";
                        jsonParam.popup_form_name = "Frm_Live_Stock_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Live_Stock_Window/";
                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + xfrmTag;                
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    case "VEHICLES":
                        model.Calc_Allow = false;
                        xfrmTag = 0;
                        if (model.ActionMethod == Common.Navigation_Mode._New)
                        {
                            xfrmTag = Common.Navigation_Mode._New;
                        }
                        if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                xfrmTag = Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                                {
                                    xfrmTag = Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    xfrmTag = Common.Navigation_Mode._View;
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = xfrmTag.ToString().Substring(1) + " ~ Item Detail (Vehicle Detail)...";
                        jsonParam.popup_form_name = "Frm_Vehicles_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Vehicles_Window/";
                        jsonParam.popup_querystring = "Tag=" + xfrmTag +
                            "&BE_ItemName=" + Url.Encode(model.Item_Name) +
                            "&Cmd_Make=" + Url.Encode(model.VI_MAKE) +
                            "&VI_MODEL=" + Url.Encode(model.VI_MODEL) +
                            "&VI_REG_NO_PATTERN=" + Url.Encode(model.VI_REG_NO_PATTERN) +
                            "&VI_REG_NO=" + Url.Encode(model.VI_REG_NO) +
                            "&VI_REG_DATE=" + Url.Encode(model.VI_REG_DATE) +
                            "&VI_OWNERSHIP=" + Url.Encode(model.VI_OWNERSHIP) +
                            "&VI_OWNERSHIP_AB_ID=" + Url.Encode(model.VI_OWNERSHIP_AB_ID) +
                            "&VI_DOC_RC_BOOK=" + Url.Encode(model.VI_DOC_RC_BOOK) +
                            "&VI_DOC_AFFIDAVIT=" + Url.Encode(model.VI_DOC_AFFIDAVIT) +
                            "&VI_DOC_WILL=" + Url.Encode(model.VI_DOC_WILL) +
                            "&VI_DOC_TRF_LETTER=" + Url.Encode(model.VI_DOC_TRF_LETTER) +
                            "&VI_DOC_FU_LETTER=" + Url.Encode(model.VI_DOC_FU_LETTER) +
                            "&VI_DOC_OTHERS=" + Url.Encode(model.VI_DOC_OTHERS) +
                            "&VI_DOC_NAME=" + Url.Encode(model.VI_DOC_NAME) +
                            "&VI_INSURANCE_ID=" + Url.Encode(model.VI_INSURANCE_ID) +
                            "&Txt_PolicyNo=" + Url.Encode(model.VI_INS_POLICY_NO) +
                            "&VI_E_DATE=" + Url.Encode(model.VI_INS_EXPIRY_DATE) +
                            "&Txt_Others=" + Url.Encode(model.Txt_Remarks) +
                            "&VI_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID) + "&SessionGUID=" + Url.Encode(SessionGUID);
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    case "LAND & BUILDING":
                        xfrmTag = 0;
                        model.Calc_Allow = false;
                        if (model.ActionMethod == Common.Navigation_Mode._New)
                        {
                            xfrmTag = Common.Navigation_Mode._New;
                        }
                        if (model.ActionMethod == Common.Navigation_Mode._Edit || model.ActionMethod == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                xfrmTag = Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.ActionMethod == Common.Navigation_Mode._Edit)
                                {
                                    xfrmTag = Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    xfrmTag = Common.Navigation_Mode._View;
                                }
                                if (model.List_LB_DOCS_ARRAY == null)
                                {
                                    model.List_LB_DOCS_ARRAY = new JavaScriptSerializer().Serialize(FetchLBDocuments(model.LB_REC_ID, ref model));
                                }
                                if (model.List_LB_EXTENDED_PROPERTY_TABLE == null)
                                {
                                    model.List_LB_EXTENDED_PROPERTY_TABLE = new JavaScriptSerializer().Serialize(FetchExtensionData(model.LB_REC_ID, ref model));
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = xfrmTag.ToString().Substring(1) + "~ Item Detail (Land & Building Detail)...";
                        jsonParam.popup_form_name = "Frm_Property_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Property_Window/";
                        jsonParam.popup_querystring = "Tag=" + xfrmTag +
                            "&ITEM_ID=" + Url.Encode(model.GLookUp_ItemList) +
                            "&LB_PRO_TYPE=" + Url.Encode(model.LB_PRO_TYPE) +
                            "&LB_PRO_CATEGORY=" + Url.Encode(model.LB_PRO_CATEGORY) +
                            "&LB_PRO_USE=" + Url.Encode(model.LB_PRO_USE) +
                            "&LB_PRO_NAME=" + Url.Encode(model.LB_PRO_NAME) +
                            "&LB_PRO_ADDRESS=" + Url.Encode(model.LB_PRO_ADDRESS) +
                            "&LB_ADDRESS1=" + Url.Encode(model.LB_ADDRESS1) +
                            "&LB_ADDRESS2=" + Url.Encode(model.LB_ADDRESS2) +
                            "&LB_ADDRESS3=" + Url.Encode(model.LB_ADDRESS3) +
                            "&LB_ADDRESS4=" + Url.Encode(model.LB_ADDRESS4) +
                            "&LB_CITY_ID=" + Url.Encode(model.LB_CITY_ID) +
                            "&LB_DISTRICT_ID=" + Url.Encode(model.LB_DISTRICT_ID) +
                            "&LB_STATE_ID=" + Url.Encode(model.LB_STATE_ID) +
                            "&LB_PINCODE=" + Url.Encode(model.LB_PINCODE) +
                            "&LB_OWNERSHIP=" + Url.Encode(model.LB_OWNERSHIP) +
                            "&LB_OWNERSHIP_PARTY_ID=" + Url.Encode(model.LB_OWNERSHIP_PARTY_ID) +
                            "&LB_SURVEY_NO=" + Url.Encode(model.LB_SURVEY_NO) +
                            "&LB_CON_YEAR=" + Url.Encode(model.LB_CON_YEAR) +
                            "&LB_RCC_ROOF=" + Url.Encode(model.LB_RCC_ROOF) +
                            "&LB_PAID_DATE=" + Url.Encode(model.LB_PAID_DATE) +
                            "&LB_PERIOD_FROM=" + Url.Encode(model.LB_PERIOD_FROM)+
                            "&LB_PERIOD_TO=" + Url.Encode(model.LB_PERIOD_TO) +
                            "&LB_DOC_OTHERS=" + Url.Encode(model.LB_DOC_OTHERS) +
                            "&LB_DOC_NAME=" + Url.Encode(model.LB_DOC_NAME) +
                            "&LB_OTHER_DETAIL=" + Url.Encode(model.LB_OTHER_DETAIL) +
                            "&LB_TOT_P_AREA=" + model.LB_TOT_P_AREA +
                            "&LB_CON_AREA=" + model.LB_CON_AREA +
                            "&LB_DEPOSIT_AMT=" +model.LB_DEPOSIT_AMT +
                            "&LB_MONTH_RENT=" + model.LB_MONTH_RENT +
                            "&LB_MONTH_O_PAYMENTS=" + model.LB_MONTH_O_PAYMENTS +
                            "&LB_REC_ID=" + Url.Encode(model.LB_REC_ID) +
                            "&xID=" + Url.Encode(model.LB_REC_ID)+
                            "&List_LB_DOCS_ARRAY=" + Url.Encode(model.List_LB_DOCS_ARRAY) +
                            "&List_LB_EXTENDED_PROPERTY_TABLE=" + Url.Encode(model.List_LB_EXTENDED_PROPERTY_TABLE) + "&SessionGUID=" + Url.Encode(SessionGUID);
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    default:
                        jsonParam.result = true;
                        jsonParam.popup_title = "";
                        jsonParam.popup_form_name = "";
                        jsonParam.popup_form_path = "";
                        return Json(new
                        {
                            jsonParam
                        });
                }
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
        [HttpPost]
        public ActionResult FillGrid1(PaymentItem model)
        {            
            List<Payment_Grid_Datatable> gridRows = new List<Payment_Grid_Datatable>();
            Payment_Grid_Datatable grid = new Payment_Grid_Datatable();
            model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
            Txt_CreditAmt = Convert.ToDecimal(model.Payment_Credit_Amt);
            #region FillGrid1 NEW
            if (model.ActionMethod == Common.Navigation_Mode._New)
            {
                if (ItemGridData != null)
                {
                    gridRows = ItemGridData;
                }
                if (gridRows.Count == 0)
                {
                    grid.Sr = 1;
                }
                else 
                {
                    grid.Sr = gridRows.Count + 1;
                }
                grid.ItemName = model.Item_Name;
                grid.Item_ID = model.GLookUp_ItemList;
                if (model.iCond_Ledger_ID != "00000")
                {
                    if (model.Txt_Amt > model.iMinValue && model.Txt_Amt <= model.iMaxValue)
                    {
                        grid.Item_Led_ID = model.iCond_Ledger_ID;
                    }
                    else
                    {
                        grid.Item_Led_ID = model.iLed_ID;
                    }
                }
                else
                {
                    grid.Item_Led_ID = model.iLed_ID;
                }
                grid.ITEM_VOUCHER_TYPE = model.iVoucher_Type;
                grid.Item_Trans_Type = model.iTrans_Type;
                grid.Item_Profile = model.iProfile;
                grid.Item_Party_Req = model.iParty_Req;
                grid.Head = model.BE_Item_Head;
                grid.Qty = model.Txt_Qty == null ? 0 : model.Txt_Qty;
                grid.Unit = model.Cmb_Unit;
                grid.Rate = model.Txt_Rate == null ? 0 : model.Txt_Rate;
                model.TXT_TDS = model.TXT_TDS ?? 0;
                grid.Amount = (decimal)(model.Txt_Amt) - (decimal)model.TXT_TDS;
                grid.TDS = (decimal)model.TXT_TDS;
                grid.Remarks = model.Txt_Remarks;
                grid.Pur_ID = model.GLookUp_PurList;
                grid.LB_REC_ID = model.LB_REC_ID;
                grid.LB_REC_EDIT_ON = Convert.ToDateTime(model.LB_REC_EDIT_ON);

                if (model.iProfile == "GOLD" || model.iProfile == "SILVER")
                {
                    grid.GS_DESC_MISC_ID = model.GS_DESC_MISC_ID;
                    grid.GS_ITEM_WEIGHT = (model.GS_ITEM_WEIGHT);
                    grid.LOC_ID = model.X_LOC_ID;
                }
                if (model.iProfile == "OTHER ASSETS")
                {
                    grid.AI_TYPE = model.AI_TYPE;
                    grid.AI_MAKE = model.AI_MAKE;
                    grid.AI_MODEL = model.AI_MODEL;
                    grid.AI_SERIAL_NO = model.AI_SERIAL_NO;
                    grid.AI_PUR_DATE = model.AI_PUR_DATE;
                    grid.AI_WARRANTY = model.AI_WARRANTY == null ? 0 : model.AI_WARRANTY;
                    if (Payment_Asset_Image != null)
                    {
                        grid.AI_IMAGE = Payment_Asset_Image;
                    }
                    grid.LOC_ID = model.X_LOC_ID;
                }
                if (model.iProfile == "LIVESTOCK")
                {
                    grid.LS_NAME = model.LS_NAME;
                    grid.LS_BIRTH_YEAR = model.LS_BIRTH_YEAR;
                    grid.LS_INSURANCE = model.LS_INSURANCE;
                    grid.LS_INSURANCE_ID = model.LS_INSURANCE_ID;
                    grid.LS_INS_POLICY_NO = model.LS_INS_POLICY_NO;
                    grid.LS_INS_AMT = model.LS_INS_AMT??0;
                    grid.LS_INS_DATE = model.LS_INS_DATE;
                    grid.LOC_ID = model.X_LOC_ID;
                }
                if (model.iProfile == "VEHICLES")
                {
                    grid.VI_MAKE = model.VI_MAKE;
                    grid.VI_MODEL = model.VI_MODEL;
                    grid.VI_REG_NO_PATTERN = model.VI_REG_NO_PATTERN;
                    grid.VI_REG_NO = model.VI_REG_NO==null?null:model.VI_REG_NO.ToUpper();
                    grid.VI_REG_DATE = model.VI_REG_DATE;
                    grid.VI_OWNERSHIP = model.VI_OWNERSHIP;
                    grid.VI_OWNERSHIP_AB_ID = model.VI_OWNERSHIP_AB_ID;
                    grid.VI_DOC_RC_BOOK = model.VI_DOC_RC_BOOK;
                    grid.VI_DOC_AFFIDAVIT = model.VI_DOC_AFFIDAVIT;
                    grid.VI_DOC_WILL = model.VI_DOC_WILL;
                    grid.VI_DOC_TRF_LETTER = model.VI_DOC_TRF_LETTER;
                    grid.VI_DOC_FU_LETTER = model.VI_DOC_FU_LETTER;
                    grid.VI_DOC_OTHERS = model.VI_DOC_OTHERS;
                    grid.VI_DOC_NAME = model.VI_DOC_NAME;
                    grid.VI_INSURANCE_ID = model.VI_INSURANCE_ID;
                    grid.VI_INS_POLICY_NO = model.VI_INS_POLICY_NO==null?null: model.VI_INS_POLICY_NO.ToUpper();
                    grid.VI_INS_EXPIRY_DATE = model.VI_INS_EXPIRY_DATE;
                    grid.LOC_ID = model.X_LOC_ID;
                }
                if (model.iProfile == "LAND & BUILDING")
                {
                    grid.LB_PRO_TYPE = model.LB_PRO_TYPE;
                    grid.LB_PRO_CATEGORY = model.LB_PRO_CATEGORY;
                    grid.LB_PRO_USE = model.LB_PRO_USE;
                    grid.LB_PRO_NAME = model.LB_PRO_NAME;
                    grid.LB_PRO_ADDRESS = model.LB_PRO_ADDRESS;
                    grid.LB_ADDRESS1 = model.LB_ADDRESS1;
                    grid.LB_ADDRESS2 = model.LB_ADDRESS2;
                    grid.LB_ADDRESS3 = model.LB_ADDRESS3;
                    grid.LB_ADDRESS4 = model.LB_ADDRESS4;
                    grid.LB_CITY_ID = model.LB_CITY_ID;
                    grid.LB_DISTRICT_ID = model.LB_DISTRICT_ID;
                    grid.LB_STATE_ID = model.LB_STATE_ID;
                    grid.LB_PINCODE = model.LB_PINCODE;
                    grid.LB_OWNERSHIP = model.LB_OWNERSHIP;
                    grid.LB_OWNERSHIP_PARTY_ID = model.LB_OWNERSHIP_PARTY_ID;
                    grid.LB_SURVEY_NO = model.LB_SURVEY_NO;
                    grid.LB_CON_YEAR = model.LB_CON_YEAR;
                    grid.LB_RCC_ROOF = model.LB_RCC_ROOF;
                    grid.LB_PAID_DATE = model.LB_PAID_DATE;
                    grid.LB_PERIOD_FROM = model.LB_PERIOD_FROM;
                    grid.LB_PERIOD_TO = model.LB_PERIOD_TO;
                    grid.LB_DOC_OTHERS = model.LB_DOC_OTHERS;
                    grid.LB_DOC_NAME = model.LB_DOC_NAME;
                    grid.LB_OTHER_DETAIL = model.LB_OTHER_DETAIL;
                    grid.LB_TOT_P_AREA = (model.LB_TOT_P_AREA);
                    grid.LB_CON_AREA = (model.LB_CON_AREA);
                    grid.LB_DEPOSIT_AMT = (model.LB_DEPOSIT_AMT);
                    grid.LB_MONTH_RENT = (model.LB_MONTH_RENT);
                    grid.LB_MONTH_O_PAYMENTS = model.LB_MONTH_O_PAYMENTS;

                    if (model.List_LB_DOCS_ARRAY != null)
                    {
                        var Obj_LB_DOCS_ARRAY = JsonConvert.DeserializeObject<List<Profile.Models.LB_DOCS_ARRAY_List>>(model.List_LB_DOCS_ARRAY);
                        model.LB_DOCS_ARRAY = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>(Obj_LB_DOCS_ARRAY);
                    }
                    if (model.List_LB_EXTENDED_PROPERTY_TABLE != null)
                    {
                        var Obj_LB_EXTENDED_PROPERTY_TABLE = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>>(model.List_LB_EXTENDED_PROPERTY_TABLE);
                        model.LB_EXTENDED_PROPERTY_TABLE = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>(Obj_LB_EXTENDED_PROPERTY_TABLE);
                    }

                    if (LB_DOCS_ARRAY == null)
                    {
                        LB_DOCS_ARRAY = model.LB_DOCS_ARRAY;
                    }
                    else
                    {
                        if (LB_DOCS_ARRAY.Rows.Count <= 0)
                        {
                            LB_DOCS_ARRAY = new DataTable();
                            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        }
                        foreach (DataRow XROW in model.LB_DOCS_ARRAY.Rows)
                        {
                            DataRow Row = LB_DOCS_ARRAY.NewRow();
                            Row["LB_MISC_ID"] = XROW["LB_MISC_ID"].ToString();
                            Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_DOCS_ARRAY.Rows.Add(Row);
                        }
                    }
                    if (LB_EXTENDED_PROPERTY_TABLE == null)
                    {
                        LB_EXTENDED_PROPERTY_TABLE = model.LB_EXTENDED_PROPERTY_TABLE;
                    }
                    else
                    {
                        if (LB_EXTENDED_PROPERTY_TABLE.Rows.Count <= 0)
                        {
                            LB_EXTENDED_PROPERTY_TABLE = new DataTable();
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        }
                        foreach (DataRow XROW in model.LB_EXTENDED_PROPERTY_TABLE.Rows)
                        {
                            DataRow Row = LB_EXTENDED_PROPERTY_TABLE.NewRow();
                            Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"].ToString();
                            Row["LB_SR_NO"] = XROW["LB_SR_NO"].ToString();
                            Row["LB_INS_ID"] = XROW["LB_INS_ID"].ToString();
                            Row["LB_TOT_P_AREA"] = !string.IsNullOrEmpty(XROW["LB_TOT_P_AREA"].ToString()) ? double.Parse(XROW["LB_TOT_P_AREA"].ToString()) : 0;
                            Row["LB_CON_AREA"] = !string.IsNullOrEmpty(XROW["LB_CON_AREA"].ToString()) ? double.Parse(XROW["LB_CON_AREA"].ToString()) : 0;
                            Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"].ToString();
                            Row["LB_VALUE"] = !string.IsNullOrEmpty(XROW["LB_VALUE"].ToString()) ? double.Parse(XROW["LB_VALUE"].ToString()) : 0;
                            Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"].ToString();
                            Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                        }
                    }
                }
                if (model.iProfile == "TELEPHONE BILL")
                {
                    grid.TP_ID = model.X_TP_ID;
                    grid.TP_BILL_NO = model.TP_BILL_NO;
                    grid.TP_BILL_DATE = model.TP_BILL_DATE;
                    grid.TP_PERIOD_FROM = model.TP_PERIOD_FROM;
                    grid.TP_PERIOD_TO = model.TP_PERIOD_TO;
                }
                if (model.iProfile.Equals("WIP"))
                {
                    if (model.iRefType == "NEW")
                    {
                        grid.REFERENCE = model.iReference;
                        grid.WIP_REF_TYPE = model.iRefType;
                    }
                    else
                    {
                        grid.REF_REC_ID = model.Ref_RecID;
                        grid.WIP_REF_TYPE = model.iRefType;
                    }
                }
                if (!(model.TDS_Deduction_List == null))
                {
                    if (model.TDS_Deduction_List.Count > 0)
                    {
                        foreach (Frm_Voucher_Win_Gen_Pay_Tds_Ded_Out_TDS cParam in model.TDS_Deduction_List)
                        {
                            if (grid.REF_TDS_DED.ToString().Length > 0)
                            {
                                grid.REF_TDS_DED = grid.REF_TDS_DED + ";" + cParam.RefMID + ":" + cParam.TDS_Ded.ToString();
                            }
                            else
                            {
                                grid.REF_TDS_DED = cParam.RefMID + ":" + cParam.TDS_Ded.ToString();
                            }
                        }
                    }
                }
                gridRows.Add(grid);
                ItemGridData = gridRows;
            }
            #endregion
            #region FIllGrid1 Edit
            else if ((model.ActionMethod == Common.Navigation_Mode._Edit))
            {

                gridRows = ItemGridData;
                var gridRow = gridRows.FirstOrDefault(x => x.Sr == model.Sr);

                gridRow.ItemName = model.Item_Name;
                gridRow.Item_ID = model.GLookUp_ItemList;

                if ((model.iCond_Ledger_ID != "00000"))
                {
                    if (model.Txt_Amt > model.iMinValue && model.Txt_Amt <= model.iMaxValue)
                    {
                        gridRow.Item_Led_ID = model.iCond_Ledger_ID;
                    }
                    else
                    {
                        gridRow.Item_Led_ID = model.iLed_ID;
                    }
                }
                else
                {
                    gridRow.Item_Led_ID = model.iLed_ID;
                }

                gridRow.Item_Trans_Type = model.iTrans_Type;
                gridRow.Item_Profile = model.iProfile;
                gridRow.ITEM_VOUCHER_TYPE = model.iVoucher_Type;
                gridRow.Item_Party_Req = model.iParty_Req;
                gridRow.Head = model.BE_Item_Head;
                gridRow.Qty = model.Txt_Qty == null ? 0 : model.Txt_Qty;
                gridRow.Unit = model.Cmb_Unit;
                gridRow.Rate = model.Txt_Rate == null ? 0 : model.Txt_Rate;
                gridRow.Amount = Convert.ToDecimal(model.Txt_Amt - model.TXT_TDS);
                gridRow.TDS = (decimal)(model.TXT_TDS);
                gridRow.Remarks = model.Txt_Remarks;
                gridRow.Pur_ID = model.GLookUp_PurList;
                gridRow.LB_REC_ID = model.LB_REC_ID;
                gridRow.LB_REC_EDIT_ON = (DateTime?)(model.LB_REC_EDIT_ON);


                if (model.iProfile == "GOLD" || model.iProfile == "SILVER")
                {
                    gridRow.GS_DESC_MISC_ID = model.GS_DESC_MISC_ID;
                    gridRow.GS_ITEM_WEIGHT = model.GS_ITEM_WEIGHT;
                    gridRow.LOC_ID = model.X_LOC_ID;
                }
                if (model.iProfile == "OTHER ASSETS")
                {
                    gridRow.AI_TYPE = model.AI_TYPE;
                    gridRow.AI_MAKE = model.AI_MAKE;
                    gridRow.AI_MODEL = model.AI_MODEL;
                    gridRow.AI_SERIAL_NO = model.AI_SERIAL_NO;
                    gridRow.AI_PUR_DATE = model.AI_PUR_DATE;
                    gridRow.AI_WARRANTY = model.AI_WARRANTY;
                    gridRow.AI_IMAGE = Payment_Asset_Image;
                    gridRow.LOC_ID = model.X_LOC_ID;
                }
                if (model.iProfile == "LIVESTOCK")
                {
                    gridRow.LS_NAME = model.LS_NAME;
                    gridRow.LS_BIRTH_YEAR = model.LS_BIRTH_YEAR;
                    gridRow.LS_INSURANCE = model.LS_INSURANCE;
                    gridRow.LS_INSURANCE_ID = model.LS_INSURANCE_ID;
                    gridRow.LS_INS_POLICY_NO = model.LS_INS_POLICY_NO;
                    gridRow.LS_INS_AMT = model.LS_INS_AMT??0;
                    gridRow.LS_INS_DATE = model.LS_INS_DATE;
                    gridRow.LOC_ID = model.X_LOC_ID;
                    // #4864 fix
                }
                if (model.iProfile == "VEHICLES")
                {
                    gridRow.VI_MAKE = model.VI_MAKE;
                    gridRow.VI_MODEL = model.VI_MODEL;
                    gridRow.VI_REG_NO_PATTERN = model.VI_REG_NO_PATTERN;
                    gridRow.VI_REG_NO = model.VI_REG_NO;
                    gridRow.VI_REG_DATE = model.VI_REG_DATE;
                    gridRow.VI_OWNERSHIP = model.VI_OWNERSHIP;
                    gridRow.VI_OWNERSHIP_AB_ID = model.VI_OWNERSHIP_AB_ID;
                    gridRow.VI_DOC_RC_BOOK = model.VI_DOC_RC_BOOK;
                    gridRow.VI_DOC_AFFIDAVIT = model.VI_DOC_AFFIDAVIT;
                    gridRow.VI_DOC_WILL = model.VI_DOC_WILL;
                    gridRow.VI_DOC_TRF_LETTER = model.VI_DOC_TRF_LETTER;
                    gridRow.VI_DOC_FU_LETTER = model.VI_DOC_FU_LETTER;
                    gridRow.VI_DOC_OTHERS = model.VI_DOC_OTHERS;
                    gridRow.VI_DOC_NAME = model.VI_DOC_NAME;
                    gridRow.VI_INSURANCE_ID = model.VI_INSURANCE_ID;
                    gridRow.VI_INS_POLICY_NO = model.VI_INS_POLICY_NO;
                    gridRow.VI_INS_EXPIRY_DATE = model.VI_INS_EXPIRY_DATE;
                    gridRow.LOC_ID = model.X_LOC_ID;
                }
                if (model.iProfile == "LAND & BUILDING")
                {
                    gridRow.LB_PRO_TYPE = model.LB_PRO_TYPE;
                    gridRow.LB_PRO_CATEGORY = model.LB_PRO_CATEGORY;
                    gridRow.LB_PRO_USE = model.LB_PRO_USE;
                    gridRow.LB_PRO_NAME = model.LB_PRO_NAME;
                    gridRow.LB_PRO_ADDRESS = model.LB_PRO_ADDRESS;
                    gridRow.LB_ADDRESS1 = model.LB_ADDRESS1;
                    gridRow.LB_ADDRESS2 = model.LB_ADDRESS2;
                    gridRow.LB_ADDRESS3 = model.LB_ADDRESS3;
                    gridRow.LB_ADDRESS4 = model.LB_ADDRESS4;
                    gridRow.LB_CITY_ID = model.LB_CITY_ID;
                    gridRow.LB_DISTRICT_ID = model.LB_DISTRICT_ID;
                    gridRow.LB_STATE_ID = model.LB_STATE_ID;
                    gridRow.LB_PINCODE = model.LB_PINCODE;
                    gridRow.LB_OWNERSHIP = model.LB_OWNERSHIP;
                    gridRow.LB_OWNERSHIP_PARTY_ID = model.LB_OWNERSHIP_PARTY_ID;
                    gridRow.LB_SURVEY_NO = model.LB_SURVEY_NO;
                    gridRow.LB_CON_YEAR = model.LB_CON_YEAR;
                    gridRow.LB_RCC_ROOF = model.LB_RCC_ROOF;
                    gridRow.LB_PAID_DATE = model.LB_PAID_DATE;
                    gridRow.LB_PERIOD_FROM = model.LB_PERIOD_FROM;
                    gridRow.LB_PERIOD_TO = model.LB_PERIOD_TO;
                    gridRow.LB_DOC_OTHERS = model.LB_DOC_OTHERS;
                    gridRow.LB_DOC_NAME = model.LB_DOC_NAME;
                    gridRow.LB_OTHER_DETAIL = model.LB_OTHER_DETAIL;
                    gridRow.LB_TOT_P_AREA = (model.LB_TOT_P_AREA);
                    gridRow.LB_CON_AREA = (model.LB_CON_AREA);
                    gridRow.LB_DEPOSIT_AMT = (model.LB_DEPOSIT_AMT);
                    gridRow.LB_MONTH_RENT = (model.LB_MONTH_RENT);
                    gridRow.LB_MONTH_O_PAYMENTS = model.LB_MONTH_O_PAYMENTS;
                    if (model.List_LB_DOCS_ARRAY != null)
                    {
                        var Obj_LB_DOCS_ARRAY = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>>(model.List_LB_DOCS_ARRAY);
                        model.LB_DOCS_ARRAY = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>(Obj_LB_DOCS_ARRAY);
                    }
                    if (model.List_LB_EXTENDED_PROPERTY_TABLE != null)
                    {
                        var Obj_LB_EXTENDED_PROPERTY_TABLE = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>>(model.List_LB_EXTENDED_PROPERTY_TABLE);
                        model.LB_EXTENDED_PROPERTY_TABLE = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>(Obj_LB_EXTENDED_PROPERTY_TABLE);
                    }

                    if (LB_DOCS_ARRAY == null)
                    {
                        LB_DOCS_ARRAY = model.LB_DOCS_ARRAY;
                    }
                    else
                    {
                        if (LB_DOCS_ARRAY.Rows.Count <= 0)
                        {
                            LB_DOCS_ARRAY = new DataTable();
                            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        }
                        DataTable New_LB_DOCS_ARRAY = LB_DOCS_ARRAY.Clone();
                        foreach (DataRow XROW in LB_DOCS_ARRAY.Rows)
                        {
                            if (!(XROW["LB_REC_ID"].ToString() == model.LB_REC_ID))
                            {
                                New_LB_DOCS_ARRAY.ImportRow(XROW);
                            }
                        }
                        LB_DOCS_ARRAY = New_LB_DOCS_ARRAY;
                        foreach (DataRow XROW in model.LB_DOCS_ARRAY.Rows)
                        {
                            DataRow Row = LB_DOCS_ARRAY.NewRow();
                            Row["LB_MISC_ID"] = XROW["LB_MISC_ID"].ToString();
                            Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_DOCS_ARRAY.Rows.Add(Row);
                        }
                    }
                    if (LB_EXTENDED_PROPERTY_TABLE == null)
                    {
                        LB_EXTENDED_PROPERTY_TABLE = model.LB_EXTENDED_PROPERTY_TABLE;
                    }
                    else
                    {
                        if (LB_EXTENDED_PROPERTY_TABLE.Rows.Count <= 0)
                        {
                            LB_EXTENDED_PROPERTY_TABLE = new DataTable();
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        }
                        DataTable New_LB_EXTENDED_PROPERTY_TABLE = LB_EXTENDED_PROPERTY_TABLE.Clone();
                        foreach (DataRow XROW in LB_EXTENDED_PROPERTY_TABLE.Rows)
                        {
                            if (!(XROW["LB_REC_ID"].ToString() == model.LB_REC_ID))
                            {
                                New_LB_EXTENDED_PROPERTY_TABLE.ImportRow(XROW);
                            }
                        }
                        LB_EXTENDED_PROPERTY_TABLE = New_LB_EXTENDED_PROPERTY_TABLE;
                        New_LB_EXTENDED_PROPERTY_TABLE.Dispose();
                        foreach (DataRow XROW in model.LB_EXTENDED_PROPERTY_TABLE.Rows)
                        {
                            DataRow Row = LB_EXTENDED_PROPERTY_TABLE.NewRow();
                            Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"].ToString();
                            Row["LB_SR_NO"] = XROW["LB_SR_NO"].ToString();
                            Row["LB_INS_ID"] = XROW["LB_INS_ID"].ToString();
                            Row["LB_TOT_P_AREA"] = !string.IsNullOrEmpty(XROW["LB_TOT_P_AREA"].ToString()) ? double.Parse(XROW["LB_TOT_P_AREA"].ToString()) : 0;
                            Row["LB_CON_AREA"] = !string.IsNullOrEmpty(XROW["LB_CON_AREA"].ToString()) ? double.Parse(XROW["LB_CON_AREA"].ToString()) : 0;
                            Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"].ToString();
                            Row["LB_VALUE"] = !string.IsNullOrEmpty(XROW["LB_VALUE"].ToString()) ? double.Parse(XROW["LB_VALUE"].ToString()) : 0;
                            Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"].ToString();
                            Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                        }
                    }
                }

                if ((model.iProfile == "TELEPHONE BILL"))
                {
                    gridRow.TP_ID = model.X_TP_ID;
                    gridRow.TP_BILL_NO = model.TP_BILL_NO;
                    gridRow.TP_BILL_DATE = model.TP_BILL_DATE;
                    gridRow.TP_PERIOD_FROM = model.TP_PERIOD_FROM;
                    gridRow.TP_PERIOD_TO = model.TP_PERIOD_TO;
                }

                if (model.iProfile.Equals("WIP"))
                {
                    gridRow.REF_REC_ID = model.Ref_RecID;
                    gridRow.REFERENCE = model.iReference;
                    gridRow.WIP_REF_TYPE = model.iRefType;
                }
                ItemGridData = gridRows;

            }
            #endregion
            if (!string.IsNullOrWhiteSpace(model.GLookUp_PartyList))
            {
                Txt_AdvAmt = 0.0M;
                Txt_LB_Amt = 0.0M;
                Party_Outstanding_Advances(model.GLookUp_PartyList);
                Party_Outstanding_Liabilities(model.GLookUp_PartyList);
            }
            if ((model.ActionMethod == Common.Navigation_Mode._New) || (model.ActionMethod == Common.Navigation_Mode._Edit))
            {
                Sub_Amt_Calculation(false);
            }
            else if (model.ActionMethod == Common.Navigation_Mode._Delete)
            {
                Sub_Amt_Calculation(true);
            }
            decimal AdjustableAmount = Txt_BankAmt - Txt_LB_Amt;
            var GridLastrow = gridRows.OrderByDescending(x => x.Sr).FirstOrDefault();
            var volume_result="";
            var volume_amount=0.00M;
            var volume_sr=0;
            if (BASE._IsVolumeCenter && (model.ActionMethod == Common.Navigation_Mode._New))
            {
                if (gridRows.Count > 0)
                {
                    if (Payment_SelectedPaymentType == PaymentType.Bank)
                    {
                        volume_result = "BankGrid";
                        volume_amount = GridLastrow.Amount;                      
                    }
                    if (GridLastrow.Item_Led_ID.Equals("00083") && LiabilityGridData != null)
                    {
                        if (LiabilityGridData.Count == 1)
                        {
                            volume_result = "LBGrid";
                            volume_amount = AdjustableAmount;
                            volume_sr = (int)LiabilityGridData[0].Sr;              
                        }
                        else if (LiabilityGridData.Count > 1)
                        {
                            for (int i = 0; i < LiabilityGridData.Count; i++)
                            {
                                if (LiabilityGridData[i].OutStanding > 0 && AdjustableAmount > 0)
                                {
                                    volume_result = "LBGrid";
                                    volume_amount = AdjustableAmount;
                                    volume_sr = (int)LiabilityGridData[i].Sr;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
           
            return Json(new { result = true, AdjustableAmount = AdjustableAmount, Txt_DiffAmt = Txt_DiffAmt, iParty_Req = Payment_iParty_Req, Txt_SubTotal = Txt_SubTotal, Txt_TDS_Amt = Txt_TDS_Amt, Txt_BankAmt = Txt_BankAmt, Txt_CreditAmt = Txt_CreditAmt, Txt_CashAmt = Txt_CashAmt, Txt_AdvAmt = Txt_AdvAmt, Txt_LB_Amt = Txt_LB_Amt, volume_result, volume_amount, volume_sr }, JsonRequestBehavior.AllowGet);
        }
        #region Lookup Edit Events
        public List<Return_Items> LookUp_GetItemList() 
        {
            var d1 = BASE._Payment_DBOps.GetLedgerItems(BASE.Is_HQ_Centre);
            d1.Sort((x, y) => x.ITEM_NAME.CompareTo(y.ITEM_NAME));
            return d1;
        }
        public List<Return_Party> LookUp_GetPartyList_Item() 
        {
            var d1 = BASE._Payment_DBOps.GetParties("Name", "ID");
            d1.Sort((x, y) => x.Name.CompareTo(y.Name));
            return d1;
        }
        public ActionResult RefreshPartyList_Item() 
        {
            return Content(JsonConvert.SerializeObject(LookUp_GetPartyList_Item()), "application/json");
        }
        public List<Return_Purpose> LookUp_GetPurposeList()
        {
            return BASE._Payment_DBOps.GetProjects("PUR_NAME", "PUR_ID");
        }   
        #endregion
        #region Procedures
        public DataTable FetchExtensionData(string LB_REC_ID, ref PaymentItem model)
        {

            model.LB_EXTENDED_PROPERTY_TABLE = new DataTable();
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
            var LB_Ext = BASE._Payment_DBOps.GetLandBuildingExtensions(LB_REC_ID);
            if (LB_Ext != null)
            {
                foreach (DataRow XROW in ((DataTable)LB_Ext).Rows)
                {
                    DataRow Row = model.LB_EXTENDED_PROPERTY_TABLE.NewRow();
                    Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"];
                    Row["LB_SR_NO"] = XROW["LB_SR_NO"];
                    Row["LB_INS_ID"] = XROW["LB_INS_ID"];
                    Row["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"];
                    Row["LB_CON_AREA"] = XROW["LB_CON_AREA"];
                    Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"];
                    Row["LB_VALUE"] = XROW["LB_VALUE"];
                    Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"];
                    Row["LB_REC_ID"] = LB_REC_ID;
                    model.LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                }
            }
            return model.LB_EXTENDED_PROPERTY_TABLE;
        }
        public DataTable FetchLBDocuments(string LB_REC_ID, ref PaymentItem model)
        {
            model.LB_DOCS_ARRAY = new DataTable();
            model.LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", typeof(string));
            model.LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", typeof(string));
            var LB_DOC = BASE._Payment_DBOps.GetLandBuildingDocs(LB_REC_ID);
            if (LB_DOC != null)
            {
                foreach (DataRow XROW in ((DataTable)LB_DOC).Rows)
                {
                    DataRow Row = model.LB_DOCS_ARRAY.NewRow();
                    Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                    Row["LB_REC_ID"] = LB_REC_ID;
                    model.LB_DOCS_ARRAY.Rows.Add(Row);
                }
            }
            return model.LB_DOCS_ARRAY;
        }
        public ActionResult GetTDSRate(string PAN, string TDS_Code)
        {
            string pan = PAN == null ? "" : PAN;
            DataTable d1 = BASE._Payment_DBOps.GetTDSRate(pan, TDS_Code);
            if (d1 == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            if (d1.Rows.Count > 0)
            {
                string TDSRate = d1.Rows[0][0].ToString() + "%";
                return Json(new
                {
                    result = true,
                    taxRate = TDSRate
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                result = true
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #endregion
        #region Frm_Voucher_Win_Gen_Pay_Bank
        public ActionResult Frm_Voucher_Win_Gen_Pay_Grid_2()// bank grid call back fn
        {
            var data = (BankGridData == null) ? new List<PaymentBankDetail_Grid_Datatable>() : BankGridData;
            int count = 1;
            foreach (var item in data)
            {
                item.Sr = count;
                count++;
            }
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Gen_Pay_Bank(string ActionMethod = null, int SrID = 0, double Amount = 0)
        {
            BankPaymentDetail model = new BankPaymentDetail();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Tag;
            model.TempActionMethod = Tag.ToString();
            model.BankListData = LookUp_GetBankList();
            model.RefBankListData = LookUp_GetRefBankList();
            if (model.BankListData.Count == 1) 
            {
                model.GLookUp_BankList = model.BankListData[0].BA_ID;
            }
            switch (ActionMethod)
            {
                case "New":
                    model.Cmd_Mode = "CHEQUE";
                    if (BASE._IsVolumeCenter)
                    {
                        model.Txt_Amount = Convert.ToDouble(Txt_CashAmt);
                        model.Cmd_Mode = "CHEQUE";
                        model.iBank_ID = Payment_Sel_Bank_ID;
                    }
                    if (Amount > 0)
                    {
                        model.Txt_Amount = Amount;
                    }
                    break;
                case "Edit":
                case "View":
                    var Sr = Convert.ToInt16(SrID);
                    var all_data = BankGridData;
                    var dataToEdit = all_data.FirstOrDefault(x => x.Sr == Sr);
                    model.Sr = Sr;
                    model.iBank_ID = dataToEdit.ID;
                    model.GLookUp_BankList = model.iBank_ID;
                    model.iMT_BANK_ID = dataToEdit.MT_BANK_ID;
                    model.GLookUp_RefBankList = model.iMT_BANK_ID;
                    model.Cmd_Mode = dataToEdit.Mode;
                    model.Txt_Ref_No = dataToEdit.Ref_No;
                    model.Txt_Trf_ANo = dataToEdit.Ref_Acc_No;
                    model.Txt_Ref_Date = dataToEdit.Ref_Date;
                    model.Txt_Ref_CDate = dataToEdit.Ref_CDate;
                    model.Txt_Amount = dataToEdit.Amount;

                    if (BASE._IsVolumeCenter)
                    {
                        model.GLookUp_BankList = Payment_Sel_Bank_ID;
                    }
                    break;
                case "Delete":
                    List<PaymentBankDetail_Grid_Datatable> gridRows = new List<PaymentBankDetail_Grid_Datatable>();
                    if (BankGridData != null)
                    {
                        gridRows = BankGridData;
                    }
                    var grid = gridRows.FirstOrDefault(x => x.Sr == Convert.ToInt16(SrID));
                    gridRows.Remove(grid);
                    for (int i = 0; i < gridRows.Count; i++)
                    {
                        gridRows[i].Sr = i + 1;
                    }
                    BankGridData = gridRows;
                    return Json(new { result = true, message = "" }, JsonRequestBehavior.AllowGet);

            }
            model.Txt_Ref_No=model.Txt_Ref_No.HandleEscapeCharacters();
            model.Txt_Trf_ANo = model.Txt_Trf_ANo.HandleEscapeCharacters();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Gen_Pay_Bank_Window(BankPaymentDetail model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (Tag == Common.Navigation_Mode._New || Tag == Common.Navigation_Mode._Edit || Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrEmpty(model.GLookUp_BankList) || model.GLookUp_BankList.ToString().Trim().Length == 0)
                    {
                        jsonParam.message = "Bank Not Selected ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_BankList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((string.IsNullOrEmpty(model.Txt_Ref_No) && (model.Cmd_Mode.ToString().ToUpper().Trim() != "OTHERS")))
                    {
                        jsonParam.message = "No. Not Specified ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_No";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_Ref_Date == null || IsDate(model.Txt_Ref_Date.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Ref_Date";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);

                    }

                    if (((string.IsNullOrEmpty(model.GLookUp_RefBankList)
                                || (model.GLookUp_RefBankList.ToString().Trim().Length == 0))
                                && ((model.Cmd_Mode.ToString().ToUpper() == "CBS")
                                || ((model.Cmd_Mode.ToString().ToUpper() == "RTGS")
                                || (model.Cmd_Mode.ToString().ToUpper() == "NEFT")))))
                    {
                        jsonParam.message = "Bank Not Selected ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "GLookUp_RefBankList";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((string.IsNullOrEmpty(model.Txt_Trf_ANo)
                                && ((model.Cmd_Mode.ToString().ToUpper() == "CBS")
                                || ((model.Cmd_Mode.ToString().ToUpper() == "RTGS")
                                || (model.Cmd_Mode.ToString().ToUpper() == "NEFT")))))
                    {
                        jsonParam.message = "Ref. A/c. No.Specified ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Trf_ANo";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_Amount <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero / Negative ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_Ref_CDate != null && (IsDate(model.Txt_Ref_CDate.ToString()) == true))
                    {
                        TimeSpan ts = (Convert.ToDateTime(model.Txt_Ref_CDate)) - (Convert.ToDateTime(model.Txt_Ref_Date));
                        double diff = ts.TotalDays;
                        if (diff < 0)
                        {
                            jsonParam.message = "Clearing Date Cannot be less than Reference Date...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Ref_CDate";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);

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
        public ActionResult FillGrid2(BankPaymentDetail model)
        {
            Txt_CreditAmt = Convert.ToDecimal(model.Payment_Credit_Amt);
            List<PaymentBankDetail_Grid_Datatable> gridRows = new List<PaymentBankDetail_Grid_Datatable>();
            if (BankGridData != null)
            {
                gridRows = BankGridData;
            }

            if (model.TempActionMethod == "_New")
            {
                PaymentBankDetail_Grid_Datatable grid = new PaymentBankDetail_Grid_Datatable();
                if ((gridRows.Count <= 0))
                {
                    grid.Sr = 1;
                }
                else
                {
                    grid.Sr = gridRows.Count + 1;
                }
                grid.Amount = model.Txt_Amount;
                grid.Mode = model.Cmd_Mode;
                grid.Ref_No = model.Txt_Ref_No;
                grid.Ref_Date = model.Txt_Ref_Date;
                grid.Ref_CDate = model.Txt_Ref_CDate;
                grid.BANK_NAME = model.BANK_NAME;
                grid.Branch = model.BE_Bank_Branch;
                grid.Acc_No = model.BE_Bank_Acc_No;
                grid.ID = model.GLookUp_BankList;
                grid.MT_BANK_ID = model.GLookUp_RefBankList;
                grid.MT_BANK_Name = model.MT_BANK_NAME;
                grid.Ref_Acc_No = model.Txt_Trf_ANo;
                grid.Edit_Time = Convert.ToDateTime(model.Edit_Time);
                Payment_Sel_Bank_ID = model.GLookUp_BankList;
                gridRows.Add(grid);
            }
            else if (model.TempActionMethod == "_Edit")
            {
                var grid = gridRows.FirstOrDefault(x => x.Sr == model.Sr);

                grid.Amount = model.Txt_Amount;
                grid.Mode = model.Cmd_Mode;
                grid.Ref_No = model.Txt_Ref_No;
                grid.Ref_Date = model.Txt_Ref_Date;
                grid.Ref_CDate = model.Txt_Ref_CDate;
                grid.BANK_NAME = model.BANK_NAME;
                grid.Branch = model.BE_Bank_Branch;
                grid.Acc_No = model.BE_Bank_Acc_No;
                grid.ID = model.GLookUp_BankList;
                grid.MT_BANK_ID = model.GLookUp_RefBankList;
                grid.MT_BANK_Name = model.MT_BANK_NAME;
                grid.Ref_Acc_No = model.Txt_Trf_ANo;
                grid.Edit_Time = Convert.ToDateTime(model.Edit_Time);
            }
            BankGridData = gridRows;
            if (model.TempActionMethod == "_New" || model.TempActionMethod == "_Edit")
            {
                Bank_Amt_Calculation(false);

            }
            else
            {
                Bank_Amt_Calculation(true);
            }



            return Json(new { result = true, Txt_DiffAmt = Txt_DiffAmt, Txt_BankAmt = Txt_BankAmt, Txt_CashAmt = Txt_CashAmt }, JsonRequestBehavior.AllowGet);

        }

        #endregion Frm_Voucher_Win_Gen_Pay_Bank
        #region Frm_Voucher_Win_Gen_Pay_Adv
        public ActionResult Party_Outstanding_Advances(string xParty_ID)
        {
            Param_GetPaymentAdvances PmtAdvances = new Param_GetPaymentAdvances();
            PmtAdvances.Adv_Party_ID = xParty_ID??"";
            PmtAdvances.Next_YearID = BASE._next_Unaudited_YearID;
            PmtAdvances.Prev_YearId = BASE._prev_Unaudited_YearID;
            PmtAdvances.Tr_M_Id = Payment_xMID;
            var ADV_TABLE = BASE._Payment_DBOps.GetPendingAdvances(PmtAdvances);
            if (ADV_TABLE == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            AdvanceGridData = ADV_TABLE;
            //Advance_Amt_Calculation();
            return Json(new
            {
                result = true,
                message = "",
                Txt_AdvAmt = Txt_AdvAmt
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Voucher_Win_Gen_Pay_Grid_3(string command, string xParty_ID)
        {
            if (command == "REFRESH") 
            {
                Party_Outstanding_Advances(xParty_ID);
            }
            return PartialView(AdvanceGridData);
        }
        public ActionResult Frm_Voucher_Win_Gen_Pay_Adv(string ActionMethod = null, string iPartyID = null, int SrID = 0)
        {
            AdvanceAdjustment model = new AdvanceAdjustment();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            model.ActionMethod = Tag;
            model.TempActionMethod = Tag.ToString();
            List<Return_AdvanceAdjustment_Grid_Datatable> GridView3FocusedRowHandle = new List<Return_AdvanceAdjustment_Grid_Datatable>();
            if (AdvanceGridData != null)
            {
                GridView3FocusedRowHandle = AdvanceGridData;
            }
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var Sr = Convert.ToInt16(SrID);
                var dataToEdit = GridView3FocusedRowHandle.FirstOrDefault(x => x.Sr == Sr);
                model.Sr = Sr;
                model.BE_ItemName = dataToEdit.Item;
                model.BE_Purpose = dataToEdit.Purpose;
                model.BE_Other_Detail = dataToEdit.Other_Details;
                model.BE_Adv_Amt = Convert.ToDouble(dataToEdit.Advance);
                model.BE_Adjust_Amt = Convert.ToDouble(dataToEdit.Adjusted);
                model.BE_Refund_Amt = Convert.ToDouble(dataToEdit.Refund);
                model.BE_OS_Amt = Convert.ToDouble(dataToEdit.Out_Standing);
                model.Next_Year_Balance = Convert.ToDouble(dataToEdit.Next_Year_Out_Standing);
                if (dataToEdit.GivenDate != null && IsDate(dataToEdit.GivenDate.ToString()))
                {
                    model.BE_Given_Date = Convert.ToDateTime(Convert.ToDateTime(dataToEdit.GivenDate).ToString(BASE._Date_Format_Current));
                }
                if (dataToEdit.Payment <= 0)
                {
                    if (ActionMethod == "Edit" || ActionMethod == "New")
                    {
                        model.Txt_Amount = Convert.ToDouble(dataToEdit.Out_Standing);
                    }
                    else
                    {
                        model.Txt_Amount = null;
                    }
                }
                else
                {
                    model.Txt_Amount = Convert.ToDouble(dataToEdit.Payment);
                }

            }
            return View(model);
        }
        public ActionResult Frm_Voucher_Win_Gen_Pay_Adv_Window(AdvanceAdjustment model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var Tag = model.ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.TempActionMethod);
                if (((Tag == Common.Navigation_Mode._New)
                            || ((Tag == Common.Navigation_Mode._Edit)
                            || (Tag == Common.Navigation_Mode._New_From_Selection))))
                {
                    if (model.Txt_Amount < 0)
                    {
                        jsonParam.message = "Amount cannot be Negative ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_Amount > model.BE_OS_Amt)
                    {
                        jsonParam.message = "Amount cannot be more than Out-Standing Amount ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                    {
                        if (model.Txt_Amount > model.Next_Year_Balance)
                        {
                            jsonParam.message = "Amount cannot be more than Next Year Out - Standing  Amount(" + model.Next_Year_Balance + ")...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Amount";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                jsonParam.result = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
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
        public ActionResult FillGrid3(AdvanceAdjustment model)
        {
            Txt_CreditAmt = Convert.ToDecimal(model.Payment_Credit_Amt);
            List<Return_AdvanceAdjustment_Grid_Datatable> gridRows = new List<Return_AdvanceAdjustment_Grid_Datatable>();
            if (AdvanceGridData != null)
            {
                gridRows = AdvanceGridData;
            }
            if (model.TempActionMethod == "_Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                dataToEdit.Payment = Convert.ToDecimal(model.Txt_Amount);
            }
            else if (model.TempActionMethod == "_Delete")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                dataToEdit.Payment = 0;
            }
            AdvanceGridData = gridRows;
            if (model.TempActionMethod == "_Edit" || model.TempActionMethod == "_New" || model.TempActionMethod == "_Delete")
            {
                Advance_Amt_Calculation();
            }
            return Json(new { result = true, Txt_DiffAmt = Txt_DiffAmt, Txt_CashAmt = Txt_CashAmt, Txt_AdvAmt = Txt_AdvAmt }, JsonRequestBehavior.AllowGet);
        }

        #endregion Frm_Voucher_Win_Gen_Pay_Adv
        #region Frm_Voucher_Win_Gen_Pay_LB
        public ActionResult Party_Outstanding_Liabilities(string xParty_ID)
        {
            Param_GetPaymentLiabilities PmtLiabilities = new Param_GetPaymentLiabilities();
            PmtLiabilities.LI_Party_ID = xParty_ID??"";
            PmtLiabilities.Next_YearID = BASE._next_Unaudited_YearID;
            PmtLiabilities.Prev_YearId = BASE._prev_Unaudited_YearID;
            PmtLiabilities.Tr_M_Id = Payment_xMID;
            var LI_TABLE = BASE._Payment_DBOps.GetPendingLiabilities(PmtLiabilities);
            if (LI_TABLE == null)
            {
                return Json(new
                {
                    result = false,
                    message = Messages.SomeError
                }, JsonRequestBehavior.AllowGet);
            }
            LiabilityGridData = LI_TABLE;
            //LB_Amt_Calculation();
            return Json(new
            {
                result = true,
                message = "",
                Txt_LB_Amt = Txt_LB_Amt
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Voucher_Win_Gen_Pay_Grid_4(string command, string xParty_ID)
        {
            if (command == "REFRESH") 
            {
                Party_Outstanding_Liabilities(xParty_ID);
            }
            return PartialView(LiabilityGridData);
        }
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Gen_Pay_LB(string ActionMethod = null, string iPartyID = null, int SrID = 0, double AdjustedAmount = 0)
        {
            Frm_Voucher_Win_Gen_Pay_LB_Model xfrm = new Frm_Voucher_Win_Gen_Pay_LB_Model();
            Common.Navigation_Mode Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod);
            xfrm.ActionMethod = Tag;
            xfrm.TempActionMethod = Tag.ToString();
            xfrm.i_PartyID = iPartyID;
            if (ActionMethod == "Edit" || ActionMethod == "View")
            {
                var GridView4FocusedRowHandle = LiabilityGridData;
                var Sr = Convert.ToInt16(SrID);
                var model = GridView4FocusedRowHandle.FirstOrDefault(x => x.Sr == Sr);
                xfrm.Sr = Sr;
                xfrm.BE_ItemName = model.Item;
                xfrm.BE_Purpose = model.Purpose;
                xfrm.BE_Other_Detail = model.Other_Details;
                xfrm.BE_Adv_Amt = Convert.ToDouble(model.Amount);
                xfrm.BE_Paid_Amt = Convert.ToDouble(model.Paid);
                xfrm.BE_OS_Amt = Convert.ToDouble(model.OutStanding);
                xfrm.Next_Year_Balance = Convert.ToDouble(model.Next_Year_OutStanding);
                if (model.Given_Date != null && IsDate(model.Given_Date.ToString()))
                    xfrm.BE_Given_Date = Convert.ToDateTime(Convert.ToDateTime(model.Given_Date).ToString(BASE._Date_Format_Current));
                if (model.Payment <= 0)
                {
                    if (ActionMethod == "Edit" || ActionMethod == "New")
                    {
                        xfrm.Txt_Amount = Convert.ToDouble(model.OutStanding);
                    }
                    else
                    {
                        xfrm.Txt_Amount = null;
                    }
                }
                else
                {
                    xfrm.Txt_Amount = Convert.ToDouble(model.Payment);
                }
                if (AdjustedAmount > 0)
                {
                    xfrm.Txt_Amount = AdjustedAmount <= xfrm.BE_OS_Amt ? AdjustedAmount : xfrm.BE_OS_Amt;
                }
            }
            return PartialView("Frm_Voucher_Win_Gen_Pay_LB", xfrm);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Gen_Pay_LB(Frm_Voucher_Win_Gen_Pay_LB_Model model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.TempActionMethod == "_New" || model.TempActionMethod == "_Edit" || model.TempActionMethod == "_New_From_Selection")
                {
                    if (model.Txt_Amount < 0)
                    {
                        jsonParam.message = "Amount cannot be Negative ...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_Amount > model.BE_OS_Amt)
                    {
                        jsonParam.message = "Payment cannot be more than  Liabilities Amount...!";
                        jsonParam.title = "Incomplete Information..";
                        jsonParam.result = false;
                        jsonParam.focusid = "Txt_Amount";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                    {
                        if (model.Txt_Amount > model.Next_Year_Balance)
                        {
                            jsonParam.message = "Payment cannot be more than Next Year Liabilities" + " Balance( " + model.Next_Year_Balance.ToString() + ")...!";
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.result = false;
                            jsonParam.focusid = "Txt_Amount";
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                jsonParam.result = true;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
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
        public ActionResult FillGrid4(Frm_Voucher_Win_Gen_Pay_LB_Model model)
        {
            Txt_CreditAmt = Convert.ToDecimal(model.Payment_Credit_Amt);
            List<Return_PendingLiabilities_Grid> gridRows = new List<Return_PendingLiabilities_Grid>();
            if (LiabilityGridData != null)
            {
                gridRows = LiabilityGridData;
            }
            if (model.TempActionMethod == "_Edit")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                dataToEdit.Payment = Convert.ToDecimal(model.Txt_Amount);
            }
            else if (model.TempActionMethod == "_Delete")
            {
                var dataToEdit = gridRows.FirstOrDefault(x => x.Sr == model.Sr);
                dataToEdit.Payment = 0;
            }
            LiabilityGridData = gridRows;
            if (model.TempActionMethod == "_Edit" || model.TempActionMethod == "_New" || model.TempActionMethod == "_Delete")
            {
                LB_Amt_Calculation();
            }

            return Json(new { result = true, Txt_DiffAmt = Txt_DiffAmt, Txt_CashAmt = Txt_CashAmt, Txt_LB_Amt = Txt_LB_Amt }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Start--> LookupEdit Events
        public List<Return_PaymentVoucherPartyList> LookUp_GetPartyList() 
        {
            var d1 = BASE._Payment_DBOps.GetParties();
            d1.Sort((x, y) => x.C_NAME.CompareTo(y.C_NAME));
            return d1;
        }
        public ActionResult RefreshPartyList() 
        {
            return Content(JsonConvert.SerializeObject(LookUp_GetPartyList()), "application/json");
        } 
        public List<BankList> LookUp_GetBankList()
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
                            select new BankList
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
            return finalData.OrderBy(x => x.BANK_NAME).ToList();           
        } 
        public List<Return_RefBank> LookUp_GetRefBankList()
        {
            var B2 = BASE._Payment_DBOps.GetBanks();   
           return B2.OrderBy(x => x.BI_BANK_NAME).ToList();        
        }
        #endregion

        private bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public void SessionClear()
        {
            ClearBaseSession("_Payment");
        }        
    }
}