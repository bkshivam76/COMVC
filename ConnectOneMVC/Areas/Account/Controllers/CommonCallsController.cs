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
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class CommonCallsController : BaseController
    {
        public DataTable VoucherUpdateData
        {
            get
            {
                return (DataTable)GetBaseSession("VoucherUpdateData_CommonCallWindow");
            }
            set
            {
                SetBaseSession("VoucherUpdateData_CommonCallWindow", value);
            }
        }
        public List<Return_Purpose> PurPoseList
        {
            get { return (List<Return_Purpose>)GetBaseSession("PurList_CommonCallWindow"); }
            set { SetBaseSession("PurList_CommonCallWindow", value); }
        }
        #region Edit
        /// <summary>
        /// Voucher Edit call when only Txn Entry Record ID is available on Screen
        /// </summary>
        /// <param name="Txn_Rec_ID"></param>
        /// <param name="Me_Text"></param>
        /// <param name="GridToRefresh"></param>
        /// <param name="Tr_Date"></param>  
        /// <param name="MultiUserConfirmationTaken"></param>
        [ActionName("Vouchers_Edit1")]
        public ActionResult Vouchers_Edit(string Txn_Rec_ID, string Me_Text, string GridToRefresh,string Tr_Date = null, bool MultiUserConfirmationTaken = false)
        {
            if (Txn_Rec_ID != "NOTE-BOOK")
            {
                DataTable TxnTable = BASE._Voucher_DBOps.GetEntryDetails(Txn_Rec_ID);
                return Vouchers_Edit(Txn_Rec_ID, TxnTable.Rows[0]["TR_M_ID"].ToString(), TxnTable.Rows[0]["REC_EDIT_ON"].ToString(),
              TxnTable.Rows[0]["REC_ADD_ON"].ToString(), Me_Text, TxnTable.Rows[0]["TR_DATE"].ToString(),
                                   TxnTable.Rows[0]["ACTION STATUS"].ToString(), TxnTable.Rows[0]["TR_CODE"].ToString(),
                                   TxnTable.Rows[0]["TR_ITEM_ID"].ToString(),
                                   TxnTable.Rows[0]["TR_SR_NO"].ToString(),
                                   TxnTable.Rows[0]["TR_TYPE"].ToString(), GridToRefresh,null, MultiUserConfirmationTaken);
            }
            else
            {
                object RefVar = new object();
                if (string.IsNullOrWhiteSpace(Tr_Date))
                {
                    Tr_Date = DateTime.MinValue.ToString();
                }
                return Vouchers_Edit(Txn_Rec_ID, RefVar.ToString(), RefVar.ToString(), RefVar.ToString(), Me_Text, Tr_Date, RefVar.ToString(), RefVar.ToString(), RefVar.ToString(), RefVar.ToString(), RefVar.ToString(),GridToRefresh,null,MultiUserConfirmationTaken);
            }
        }
        /// <summary>
        ///  Voucher Edit call when only Txn Entry Record Master ID and created Profile REC_ID are available on Screen
        /// </summary>
        /// <param name="Txn_Rec_M_ID"></param>
        /// <param name="Profile_Rec_ID"></param>
        /// <param name="ProfileUsed"></param>
        /// <param name="Me_Text"></param>
        /// <param name="GridToRefresh"></param>
        /// <param name="MultiUserConfirmationTaken"></param>      
        [ActionName("Vouchers_Edit2")]
        public ActionResult Vouchers_Edit(string Txn_Rec_M_ID, string Profile_Rec_ID, string ProfileUsed,string Me_Text, string GridToRefresh, bool MultiUserConfirmationTaken = false)
        {
            Common_Lib.RealTimeService.AssetProfiles profiles = (Common_Lib.RealTimeService.AssetProfiles)Enum.Parse(typeof(Common_Lib.RealTimeService.AssetProfiles), ProfileUsed);
            DataTable TxnTable = BASE._Voucher_DBOps.GetProfileEntryDetails(Profile_Rec_ID, Txn_Rec_M_ID, profiles);
            return Vouchers_Edit(TxnTable.Rows[0]["TXN_REC_ID"].ToString(), Txn_Rec_M_ID, TxnTable.Rows[0]["REC_EDIT_ON"].ToString(),
               TxnTable.Rows[0]["REC_ADD_ON"].ToString(), Me_Text, TxnTable.Rows[0]["TR_DATE"].ToString(),
                                    TxnTable.Rows[0]["ACTION STATUS"].ToString(), TxnTable.Rows[0]["TR_CODE"].ToString(),
                                    TxnTable.Rows[0]["TR_ITEM_ID"].ToString(),
                                    TxnTable.Rows[0]["TR_SR_NO"].ToString(),
                                    TxnTable.Rows[0]["TR_TYPE"].ToString(),GridToRefresh,null,MultiUserConfirmationTaken);
        }
        /// <summary>
        /// Voucher Edit call when all Txn Entry Record Data is available on Screen
        /// </summary>
        /// <param name="xTemp_ID"></param>
        /// <param name="xTemp_MID"></param>
        /// <param name="iREC_EDIT_ON"></param>
        /// <param name="iREC_ADD_ON"></param>
        /// <param name="Me_Text"></param>
        /// <param name="iTR_DATE"></param>
        /// <param name="iACTION_STATUS"></param>
        /// <param name="iTR_CODE"></param>
        /// <param name="iTR_ITEM_ID"></param>
        /// <param name="iTR_SR_NO"></param>
        /// <param name="iTR_TYPE"></param>
        /// <param name="GridToRefresh"></param>
        /// <param name="RowKey"></param>
        /// <param name="MultiUserConfirmationTaken"></param>       
        [ActionName("Vouchers_Edit3")]
        public ActionResult Vouchers_Edit(string xTemp_ID, string xTemp_MID, string iREC_EDIT_ON, string iREC_ADD_ON, string Me_Text, string iTR_DATE, string iACTION_STATUS, string iTR_CODE, string iTR_ITEM_ID, string iTR_SR_NO, string iTR_TYPE, string GridToRefresh, string RowKey, bool MultiUserConfirmationTaken = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string Closed_Bank_Account_No = "";
                string xMaster_ID = "";
                if (!string.IsNullOrWhiteSpace(RowKey))
                {
                    if (GridToRefresh == "CashBookListGrid")
                    {
                        //var CashbookData = (List<CB_Grid_Model>)GetBaseSession("MainGrid_Data_CB");
                        //if (CashbookData != null && CashbookData.Count > 0)
                        //{
                        //    CB_Grid_Model CashBookRowData = CashbookData.Where(x => x.Grid_PK == RowKey).FirstOrDefault();
                        //    xTemp_ID = CashBookRowData.iREC_ID;
                        //    xTemp_MID = CashBookRowData.iTR_M_ID;
                        //    iREC_EDIT_ON = CashBookRowData.iREC_EDIT_ON.ToString();
                        //    iREC_ADD_ON = CashBookRowData.iREC_ADD_ON.ToString();                           
                        //    iTR_DATE = CashBookRowData.iTR_DATE.ToString();
                        //    iACTION_STATUS = CashBookRowData.iACTION_STATUS;
                        //    iTR_CODE = CashBookRowData.iTR_CODE != null ? CashBookRowData.iTR_CODE.ToString() : "";
                        //    iTR_ITEM_ID = CashBookRowData.iTR_ITEM_ID;
                        //    iTR_SR_NO = CashBookRowData.iTR_SR_NO != null ? CashBookRowData.iTR_SR_NO.ToString() : "";
                        //    iTR_TYPE = CashBookRowData.iTR_TYPE;
                        //}
                        //else 
                        //{
                        iREC_EDIT_ON = string.IsNullOrWhiteSpace(iREC_EDIT_ON) ? iREC_EDIT_ON : Convert.ToDateTime(iREC_EDIT_ON).ToString();
                        iREC_ADD_ON = string.IsNullOrWhiteSpace(iREC_ADD_ON) ? iREC_ADD_ON : Convert.ToDateTime(iREC_ADD_ON).ToString();
                        iTR_DATE = string.IsNullOrWhiteSpace(iTR_DATE) ? iTR_DATE : Convert.ToDateTime(iTR_DATE).ToString();
                        iTR_CODE = iTR_CODE ?? "";
                        iTR_SR_NO = iTR_SR_NO ?? "";
                        //}
                        Me_Text = "Cash Book";
                    }
                    else if (GridToRefresh == "ITR_PostedGrid")
                    {
                        var GridData = (List<Return_InternalTransferRegister_Posted>)GetBaseSession("ITR_GridView1_ITReg");
                        if (GridData != null && GridData.Count > 0)
                        {
                            var RowData = GridData.Where(x => x.ID == RowKey).FirstOrDefault();
                            xTemp_ID = RowData.ID;
                            xTemp_MID = RowData.MID;
                            iREC_EDIT_ON = RowData.EditDate.ToString();
                            iREC_ADD_ON = RowData.AddDate.ToString();
                            Me_Text = "Internal Transfer Register";
                            iTR_DATE = RowData.xDate.ToString();
                            iACTION_STATUS = RowData.ActionStatus;
                            iTR_CODE = "8";
                            iTR_ITEM_ID = RowData.TR_ITEM_ID;
                            iTR_SR_NO = RowData.TR_SR_NO != null ? RowData.TR_SR_NO.ToString() : "";
                            iTR_TYPE = RowData.TR_TYPE;
                        }
                    }               
                }
                if (!string.IsNullOrWhiteSpace(xTemp_MID))
                {
                    xMaster_ID = xTemp_MID;
                }
                else
                {
                    xMaster_ID = xTemp_ID;
                }
                bool isRecChanged = false;
                bool Flag = false;
                DateTime? xEntryDate = null;
                if (xTemp_ID == "OPENING BALANCE")
                {
                    jsonParam.result = false;
                    jsonParam.message = "Opening Balance Cannot Edit/Delete from Voucher Entry...!<br><br>Note: Use Profile - Cash A/c. Information for CASH<br>or Bank A/c. Information for BANK";
                    jsonParam.title = "Information...";
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam

                    }, JsonRequestBehavior.AllowGet);
                }
                if (xTemp_ID == "NOTE-BOOK")
                {
                    jsonParam.result = true;
                    jsonParam.popup_title = "Note-Book Entry (" + Me_Text + ")";
                    jsonParam.popup_form_name = "Frm_NoteBook_Info";
                    jsonParam.popup_form_path = "/Account/NoteBook/Frm_NoteBook_Info/";
                    jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xEntryDate=" + iTR_DATE;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!string.IsNullOrWhiteSpace(xTemp_ID))
                {
                    bool Entry_Found = false;
                    string xRec_Status = "";
                    string xTR_CODE = "";
                    string xTemp_D_Status = "";
                    string xCross_Ref_Id = "";
                    string multiUserMsg = "";
                    bool AllowUser = false;
                    DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
                    if (Status == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Status.Rows.Count > 0)
                    {
                        Entry_Found = true;
                        xRec_Status = Status.Rows[0]["REC_STATUS"].ToString();
                        xTR_CODE = Status.Rows[0]["TR_CODE"].ToString();
                        for (int i = 0; i < Status.Rows.Count; i++)
                        {
                            if (!Convert.IsDBNull(Status.Rows[i]["TR_TRF_CROSS_REF_ID"]))
                            {
                                xCross_Ref_Id = (string)Status.Rows[i]["TR_TRF_CROSS_REF_ID"];
                            }
                            if (xCross_Ref_Id.Length > 0)
                            {
                                break;
                            }
                        }
                        string xStatus = iACTION_STATUS;
                        var value = ((int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus));
                        if (value != Convert.ToInt32(xRec_Status))
                        {
                            if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Locked)
                            {
                                multiUserMsg = "The Record Has Been Locked In The Background By Another User.";
                            }
                            else if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Completed)
                            {
                                multiUserMsg = "The Record Has Been Unlocked In The Background By Another User.";
                                AllowUser = true;
                            }
                            else
                            {
                                multiUserMsg = "The Record Has Been Changed In The Background By Another User.";
                                AllowUser = true;
                            }
                            if (AllowUser && MultiUserConfirmationTaken == false)
                            {
                                jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (Entry_Found == false)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Locked)
                    {
                        multiUserMsg = multiUserMsg.Length > 0 ? "<br><br>" + multiUserMsg : multiUserMsg;
                        jsonParam.message = "Locked Entry Cannot Be Edited/Deleted...!" + multiUserMsg + "<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Get_Closed_Bank_Status(xMaster_ID, ref Closed_Bank_Account_No))
                    {
                        jsonParam.message = "Entry Cannot Be Edited/Deleted...!<br><br>In This Entry, Used Bank A/C No.: " + Closed_Bank_Account_No + " Was Closed...!!!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt32(xTR_CODE) == 5 || Convert.ToInt32(xTR_CODE) == 6)
                    {
                        DataTable Status2 = BASE._Voucher_DBOps.GetDonationStatus(xTemp_ID);
                        if (Status2 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Status2.Rows.Count > 0)
                        {
                            xTemp_D_Status = (string)Status2.Rows[0]["DS_STATUS"];
                        }
                    }

                    int xVCode = Convert.ToInt32(iTR_CODE);
                    string xItemID = iTR_ITEM_ID;
                    if (xVCode == (int)Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Cash Deposit / Withdrawn";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Cash";
                        jsonParam.popup_form_path = "/Account/CashDepositAndWithdrawnVoucher/Frm_Voucher_Win_Cash/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Bank_To_Bank_Transfer)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Bank to Bank Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_B2B";
                        jsonParam.popup_form_path = "/Account/BankToBankTransferVoucher/Frm_Voucher_Win_B2B/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Regular)
                    {
                        if ((!string.IsNullOrEmpty(xTemp_D_Status)) && (xTemp_D_Status != "42189485-9b6b-430a-8112-0e8882596f3c") && (xTemp_D_Status != "3a99fadc-b336-480d-8116-fbd144bd7671") && (xTemp_D_Status != "6a7c38ba-5779-4e21-acc7-c1829b7ec933"))
                        {
                            jsonParam.message = "Donation Entry cannot be Edited/Deleted...!<br> Note: Donation Status Changed, Check Donation Register..!!!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_ID, ClientScreen.Accounts_Voucher_Donation))
                        {
                            jsonParam.message = "Sorry! Deposit slip has already been printed for current transaction!!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Donation";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Donation_R";
                        jsonParam.popup_form_path = "/Account/DonationVoucher/Frm_Voucher_Win_Donation_R/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Foreign)
                    {
                        if ((!string.IsNullOrEmpty(xTemp_D_Status)) && (xTemp_D_Status != "42189485-9b6b-430a-8112-0e8882596f3c") && (xTemp_D_Status != "3a99fadc-b336-480d-8116-fbd144bd7671") && (xTemp_D_Status != "6a7c38ba-5779-4e21-acc7-c1829b7ec933"))
                        {
                            jsonParam.message = "Donation Entry cannot be Edited/Deleted...!<br> Note: Donation Status Changed, Check Donation Register..!!!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Foreign Donation";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Donation_F";
                        jsonParam.popup_form_path = "/Account/DonationVoucher/Frm_Voucher_Win_Donation_F/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Collection_Box)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Collection Box";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_C_Box";
                        jsonParam.popup_form_path = "/Account/CollectionBoxVoucher/Frm_Voucher_Win_C_Box/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Payment)
                    {
                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xCross_Ref_Id);
                        if (AssetTrfRecord != null)
                        {
                            if (AssetTrfRecord.Rows.Count > 0)
                            {
                                jsonParam.message = "Sorry ! Selected Entry Refers a Advance/Deposit which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        string msg_CheckPaymentEditDetails = BASE._Voucher_DBOps.CheckPaymentEditDetails(xTemp_MID);
                        if (msg_CheckPaymentEditDetails.Length == 0)
                        {
                            jsonParam.result = true;
                            jsonParam.popup_title = "Edit ~ Payment";
                            jsonParam.popup_form_name = "Frm_Voucher_Win_Gen_Pay";
                            jsonParam.popup_form_path = "/Account/PaymentVoucher/Frm_Voucher_Win_Gen_Pay/";
                            jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            jsonParam.message = msg_CheckPaymentEditDetails;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Receipt)
                    {
                        object FinalPayDate = BASE._DepositsDBOps.GetFinalPaymentDate(xCross_Ref_Id, xTemp_MID);
                        if (IsDate(FinalPayDate.ToString()))
                        {
                            if (Convert.ToDateTime(FinalPayDate) != DateTime.MinValue)
                            {
                                jsonParam.message = "Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " + Convert.ToDateTime(FinalPayDate).ToLongDateString();
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xCross_Ref_Id);
                        if (AssetTrfRecord != null)
                        {
                            if (AssetTrfRecord.Rows.Count > 0)
                            {
                                jsonParam.message = "Sorry ! Selected Entry Refers a Advance/Deposit which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (BASE._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_MID, ClientScreen.Accounts_Voucher_Receipt))
                        {
                            jsonParam.message = "Sorry! Deposit slip has already been printed for current transaction!!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Receipt";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gen_Rec";
                        jsonParam.popup_form_path = "/Account/ReceiptVoucher/Frm_Voucher_Win_Gen_Rec/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Gift)
                    {
                        DataTable xTemp_WIP_Dependencies = BASE._WIPCretionVouchers.GetWIP_Dependencies(xTemp_MID); //Get Dependent Entries on WIP creted by current Txn
                        if (xTemp_WIP_Dependencies.Rows.Count > 0)
                        {
                            jsonParam.message = "Sorry ! Some Adjustment has been made against the WIP(" + xTemp_WIP_Dependencies.Rows[0]["WIP_REF"].ToString() + ") raised in current transaction on " + Convert.ToDateTime(xTemp_WIP_Dependencies.Rows[0]["Date"]).ToString("dd-MM-yyyy") + " for Rs." + xTemp_WIP_Dependencies.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for Editing this Entry.";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable GetAssetItemID = BASE._Voucher_DBOps.GetAssetItemID(xTemp_MID); // Get Actual Item IDs from Selected Transaction
                        int count = GetAssetItemID.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow cRow = GetAssetItemID.Rows[i];
                            string xTemp_ItemID = cRow[0].ToString();
                            DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); //Gets Asset Profile
                            string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                            if (xTemp_AssetProfile.ToUpper() != "NOT APPLICABLE") //Leaving Constuction Items
                            {
                                string xTemp_AssetID = "";
                                switch (xTemp_AssetProfile)
                                {
                                    case "GOLD":
                                    case "SILVER":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, xTemp_MID);
                                        break;
                                    case "OTHER ASSETS":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                                        break;
                                    case "LIVESTOCK":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, xTemp_MID);
                                        break;
                                    case "VEHICLES":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, xTemp_MID);
                                        break;
                                    case "LAND & BUILDING":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, xTemp_MID);
                                        break;
                                }
                                if (xTemp_AssetID.Length > 0)
                                {
                                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                    if (SaleRecord != null)
                                    {
                                        if (SaleRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID); //Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                    if (ReferenceRecord != null)
                                    {
                                        if (ReferenceRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                                    if (AssetTrfRecord != null)
                                    {
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                            else //Non Profile Entries 
                            {
                                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(BASE._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)); //checks if the referred property for constt items has been sold 
                                if (SaleRecord != null)
                                {
                                    if (SaleRecord.Rows.Count > 0)
                                    {
                                        jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, BASE._Voucher_DBOps.GetReferenceRecordID(xTemp_MID));
                                if (AssetTrfRecord != null)
                                {
                                    if (AssetTrfRecord.Rows.Count > 0)
                                    {
                                        jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Donation in Kind";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gift";
                        jsonParam.popup_form_path = "/Account/DonationInKindVoucher/Frm_Voucher_Win_Gift/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Internal_Transfer)
                    {
                        DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1);
                        if (d1 != null)
                        {
                            if (d1.Rows.Count > 0)
                            {
                                if (Convert.ToDateTime(iREC_EDIT_ON) != (DateTime?)d1.Rows[0]["REC_EDIT_ON"])
                                {
                                    isRecChanged = true;
                                }
                                if (!Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                                {
                                    if (d1.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                                    {
                                        jsonParam.message = "Matched Internal Transfer Cannot be Edited...!<br><br>Record has already been matched in the background";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = isRecChanged;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    if (isRecChanged)
                                    {
                                        jsonParam.message = "Record has already been unmatched in the background";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = true;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        if (BASE._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_MID, ClientScreen.Accounts_Voucher_Internal_Transfer))
                        {
                            jsonParam.message = "Sorry! Deposit slip has already been printed for current transaction!!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Internal Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_I_Transfer";
                        jsonParam.popup_form_path = "/Account/InternalTransfer/Frm_Voucher_Win_I_Transfer/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID_1=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Fixed_Deposits)
                    {
                        object IsClosed = BASE._FD_Voucher_DBOps.GetFDCloseDate(xTemp_MID);
                        object Has_Int_TDS = BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, xCross_Ref_Id, 1);
                        object IsRenew = BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 2); //CHECKS IF CURRENT TRANSACTIONS IS A RENEWAL ONE, IF YES THEN GET THE ID OF NEWLY CREATED FD
                        object New_FD = null;
                        if ((int)IsRenew > 0)
                        {
                            New_FD = BASE._FD_Voucher_DBOps.GetNewFDIdFromClosed(xCross_Ref_Id);
                            if (!Convert.IsDBNull(New_FD))
                            {
                                Has_Int_TDS = BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, New_FD.ToString(), 1);
                            }
                        }
                        Common.FDAction? iAction = null;
                        string TitleX = "New";//Redmine Bug #132913 fixed
                        string iSpecific_ItemID = string.Empty;
                        string CreatedFDID = string.Empty;
                        switch (xItemID)
                        {
                            case "f6e4da62-821f-4961-9f93-f5177fca2a77":       //FD New
                                if (IsDate(IsClosed.ToString()))
                                {
                                    jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else if ((int)Has_Int_TDS > 0)
                                {
                                    jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    iAction = Common.FDAction.New_FD;
                                    TitleX = "FD Creation";
                                    CreatedFDID = xCross_Ref_Id;
                                }
                                break;
                            case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b":     //FD Renewed
                                if (IsDate(IsClosed.ToString()))
                                {
                                    jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else if ((int)Has_Int_TDS > 0)
                                {
                                    jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    iAction = Common.FDAction.Renew_FD;
                                    TitleX = "FD Renewal";
                                    CreatedFDID = xCross_Ref_Id;
                                }
                                break;
                            case "1ed5cbe4-c8aa-4583-af44-eba3db08e117":    //FD Close - Interest, 65730a27-e365-4195-853e-2f59225fe8f4
                            case "65730a27-e365-4195-853e-2f59225fe8f4":
                                if ((int)IsRenew > 0)
                                {
                                    if (IsDate(IsClosed.ToString()))
                                    {
                                        jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else if ((int)Has_Int_TDS > 0)
                                    {
                                        jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        iAction = Common.FDAction.Renew_FD;
                                        TitleX = "FD Renewal";
                                        CreatedFDID = New_FD.ToString();
                                    }
                                }
                                else
                                {
                                    iAction = Common.FDAction.Close_FD;
                                    TitleX = "FD Closure";
                                }
                                break;
                            default:
                                if ((int)IsRenew > 0)
                                {
                                    if (IsDate(IsClosed.ToString()))
                                    {
                                        jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else if ((int)Has_Int_TDS > 0)
                                    {
                                        jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        iAction = Common.FDAction.Renew_FD;
                                        TitleX = "FD Renewal";
                                        CreatedFDID = New_FD.ToString();
                                    }
                                }
                                else
                                {
                                    // fdclose principle
                                    if ((int)BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 3) > 0)
                                    {
                                        iAction = Common.FDAction.Close_FD;
                                        TitleX = "FD Closure";
                                    }
                                    else
                                    {
                                        iSpecific_ItemID = xItemID;
                                    }
                                }
                                break;
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ " + TitleX;
                        jsonParam.popup_form_name = "Frm_Voucher_Win_FD";
                        jsonParam.popup_form_path = "/Account/FDVoucher/Frm_Voucher_Win_FD/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&iSpecific_ItemID=" + iSpecific_ItemID + "&iAction=" + iAction + "&TitleX=" + TitleX + "&xMID=" + xTemp_MID + "&CreatedFDID=" + CreatedFDID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Sale_Asset)
                    {
                        string xTemp_AdvID = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID); //Get advance cretaed by current Txn
                        if (xTemp_AdvID.Length > 0)
                        {
                            DataTable txnReferAdvance = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID); //Adjustments/ Refund has been made againt the Advance raised
                            if (txnReferAdvance != null)
                            {
                                if (txnReferAdvance.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " + Convert.ToDateTime(txnReferAdvance.Rows[0]["TR_DATE"]).ToLongDateString() + " for Rs." + txnReferAdvance.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = xTemp_AdvID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            txnReferAdvance = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers);
                            if (txnReferAdvance != null)
                            {
                                if (txnReferAdvance.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry! Advance created by current entry is used in some other entry...!<br><br>Please delete that dependency to edit this entry.";
                                    jsonParam.title = Me_Text;
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        DateTime MaxEditOn = Convert.ToDateTime(BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(xCross_Ref_Id));
                        DateTime? Info_MaxEditedOn = null;
                        if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                        {
                            Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                            Param.CrossRefId = xCross_Ref_Id;
                            Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            Param.SpecifiedEntryType = EntryType.Both;
                            int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count;
                            if (Adj_NextYear > 0)
                            {
                                jsonParam.message = "Sorry! Some adjustments have already been made on this asset<br><br>Please delete those entries for editing this Entry.";
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            Info_MaxEditedOn = MaxEditOn;
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Sale Of Asset";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Sale_Asset";
                        jsonParam.popup_form_path = "/Account/SaleAsset/Frm_Voucher_Win_Sale_Asset/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn=" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership)
                    {
                        object GetValue = "";
                        GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xMaster_ID);
                        if (GetValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                        {
                            jsonParam.message = "Discontiuned Member Entry cannot be Edited/Deleted...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime xDate = Convert.ToDateTime(iREC_ADD_ON);
                        bool TrFound = false;
                        string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                        DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xMaster_ID);
                        if (T1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        int count = T1.Rows.Count;
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                DataRow XRow = T1.Rows[i];
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
                                jsonParam.message = "Some another Entry available after this Entry of same Member....!";
                                jsonParam.title = "Cannot Edit/Delete...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0)
                        {
                            jsonParam.message = "Membership Receipt generated Voucher cannot be Edited/Deleted...!";//Redmine Bug #135913 fixed
                            jsonParam.title = Me_Text;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Membership";
                        jsonParam.popup_form_name = "Frm_Voucher_Membership";
                        jsonParam.popup_form_path = "/Membership/MembershipReceiptRegister/Frm_Voucher_Membership/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Add_Time=" + CurRecordAddOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership_Renewal)
                    {
                        object GetValue = "";
                        GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xMaster_ID);
                        if (GetValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                        {
                            jsonParam.message = "Discontiuned Member Entry cannot be Edited/Deleted...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime xDate = Convert.ToDateTime(iREC_ADD_ON);
                        bool TrFound = false;
                        string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                        DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xMaster_ID);
                        if (T1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        int count = T1.Rows.Count;
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                DataRow XRow = T1.Rows[i];
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
                                jsonParam.message = "Some another Entry available after this Entry of same Member....!";
                                jsonParam.title = "Cannot Edit/Delete...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0)
                        {
                            jsonParam.message = "Membership Receipt generated Voucher cannot be Edited/Deleted...!";//Redmine Bug #135913 fixed
                            jsonParam.title = Me_Text;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        //jsonParam.result = true;
                        //jsonParam.popup_title = "Edit ~ Membership Renewal";
                        //jsonParam.popup_form_name = "Frm_Voucher_Membership_Renewal";
                        //jsonParam.popup_form_path = "";
                        //jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Add_Time=" + CurRecordAddOn + "&GridToRefresh=" + GridToRefresh;
                        jsonParam.result = false;
                        jsonParam.message = "Edit of Membership Renewal is not allowed";
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership_Conversion)
                    {
                        object GetValue = "";
                        GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xMaster_ID);
                        if (GetValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                        {
                            jsonParam.message = "Discontiuned Member Entry cannot be Edited/Deleted...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime xDate = Convert.ToDateTime(iREC_ADD_ON);
                        bool TrFound = false;
                        string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                        DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xMaster_ID);
                        if (T1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        int count = T1.Rows.Count;
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                DataRow XRow = T1.Rows[i];
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
                                jsonParam.message = "Some another Entry available after this Entry of same Member....!";
                                jsonParam.title = "Cannot Edit/Delete...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0)
                        {
                            jsonParam.message = "Membership Receipt generated voucher cannot be Edited/Deleted...!";
                            jsonParam.title = Me_Text;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        //jsonParam.result = true;
                        //jsonParam.popup_title = "Edit ~ Membership Conversion";
                        //jsonParam.popup_form_name = "Frm_Voucher_Membership_Conversion";
                        //jsonParam.popup_form_path = "";
                        //jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Add_Time=" + CurRecordAddOn + "&GridToRefresh=" + GridToRefresh;
                        jsonParam.result = false;
                        jsonParam.message = "Edit of Membership Conversion is not allowed";
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Journal)
                    {
                        DataTable xTemp_WIP_Dependencies = BASE._WIPCretionVouchers.GetWIP_Dependencies(xTemp_MID); //Get Dependent Entries on WIP creted by current Txn
                        if (xTemp_WIP_Dependencies.Rows.Count > 0)
                        {
                            jsonParam.message = "Sorry ! Some Adjustment has been made against the WIP(" + xTemp_WIP_Dependencies.Rows[0]["WIP_REF"].ToString() + ") raised in current transaction on " + Convert.ToDateTime(xTemp_WIP_Dependencies.Rows[0]["Date"]).ToString("dd-MM-yyyy") + " for Rs." + xTemp_WIP_Dependencies.Rows[0]["AMOUNT"].ToString() + "." + "<br><br>Please delete the record for Editing this Entry.";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }


                        DataTable GetAssetItemID = BASE._Voucher_DBOps.GetAssetItemID(xTemp_MID); //Get Actual Asset IDs from Selected Transaction
                        int count = GetAssetItemID.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow cRow = GetAssetItemID.Rows[i];
                            string xTemp_ItemID = cRow[0].ToString();
                            DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); //Gets Asset Profile
                            string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                            if (xTemp_AssetProfile.ToUpper() != "NOT APPLICABLE") //Leaving Constuction Items
                            {
                                string xTemp_AssetID = "";
                                switch (xTemp_AssetProfile)
                                {
                                    case "GOLD":
                                    case "SILVER":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, xTemp_MID);
                                        break;
                                    case "OTHER ASSETS":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                                        break;
                                    case "LIVESTOCK":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, xTemp_MID);
                                        break;
                                    case "VEHICLES":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, xTemp_MID);
                                        break;
                                    case "LAND & BUILDING":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, xTemp_MID);
                                        break;
                                    case "WIP":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.WIP_INFO, xTemp_MID);
                                        break;
                                }
                                if (xTemp_AssetID.Length > 0)
                                {
                                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                    if (SaleRecord != null)
                                    {
                                        if (SaleRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Editing this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                                    if (AssetTrfRecord != null)
                                    {
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for Editing this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID);
                                    if (ReferenceRecord != null)
                                    {
                                        if (ReferenceRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Editing this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }
                        object UseCount = 0;
                        string RefId = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID);
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._AdvanceDBOps.GetAdvancePaymentCount(RefId);
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! Advance created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Edit this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers).Rows.Count;
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! Advance created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Edit this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        RefId = BASE._Voucher_DBOps.GetRaisedDepositRecID(xTemp_MID);
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._DepositsDBOps.GetTransactionCount(RefId);
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! deposit created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Edit this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers).Rows.Count;
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! deposit created by current  journal entry is used in some other entry...<br><br>Please delete that dependency to Edit this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        RefId = BASE._Voucher_DBOps.GetRaisedLiabilityRecID(xTemp_MID);
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._LiabilityDBOps.GetTransactionCount(RefId);
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! liability created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Edit this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers).Rows.Count;
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! liability created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Edit this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        Dictionary<string, DateTime> Info_MaxEditedOn = new Dictionary<string, DateTime>();
                        DataTable GetRefRecordIDS = BASE._Voucher_DBOps.GetRefRecordIDS(xTemp_MID);
                        count = GetRefRecordIDS.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow Row = GetRefRecordIDS.Rows[i];
                            if (Row["ITEM_PROFILE"].ToString() == "GOLD" || Row["ITEM_PROFILE"].ToString() == "SILVER" || Row["ITEM_PROFILE"].ToString() == "OTHER ASSETS" || Row["ITEM_PROFILE"].ToString() == "LIVESTOCK" || Row["ITEM_PROFILE"].ToString() == "LAND & BUILDING" || Row["ITEM_PROFILE"].ToString() == "VEHICLES" || Row["ITEM_PROFILE"].ToString() == "ADVANCES" || Row["ITEM_PROFILE"].ToString() == "OTHER DEPOSITS" || Row["ITEM_PROFILE"].ToString() == "OTHER LIABILITIES" || Row["ITEM_VOUCHER_TYPE"].ToString().ToUpper() == "LAND & BUILDING")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    if (Row["TR_TRF_CROSS_REF_ID"] != null)
                                    {
                                        RefId = Row["TR_TRF_CROSS_REF_ID"].ToString();
                                        if (RefId.Length > 0)
                                        {
                                            DateTime MaxEditOn = Convert.ToDateTime(BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(RefId));
                                            if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                                            {
                                                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(RefId, true);
                                                if (SaleRecord != null)
                                                {
                                                    if (SaleRecord.Rows.Count > 0)
                                                    {
                                                        jsonParam.message = "Sorry ! Selected Entry refers a "+ Row["ITEM_PROFILE"].ToString() + " which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Editing this Entry.";
                                                        jsonParam.title = "Error!!";
                                                        jsonParam.result = false;
                                                        return Json(new
                                                        {
                                                            jsonParam
                                                        }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, BASE._open_Year_ID, RefId, true);
                                                if (AssetTrfRecord != null)
                                                {
                                                    if (AssetTrfRecord.Rows.Count > 0)
                                                    {
                                                        jsonParam.message = "Sorry ! Selected Entry refers a " + Row["ITEM_PROFILE"].ToString() + " which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for Editing this Entry.";
                                                        jsonParam.title = "Error!!";
                                                        jsonParam.result = false;
                                                        return Json(new
                                                        {
                                                            jsonParam
                                                        }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                                                Param.CrossRefId = RefId;
                                                Param.YearID = BASE._open_Year_ID;
                                                Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                                                Param.Excluded_Rec_M_ID = xMaster_ID;
                                                Param.SpecifiedEntryType = EntryType.Both;
                                                int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_Journal).Rows.Count;
                                                if (Adj_NextYear > 0)
                                                {
                                                    jsonParam.message = "Sorry! Selected Entry contains an " + Row["ITEM_PROFILE"].ToString() + " against which adjustments have been made in current / next year<br><br> Please delete those entries for Editing this Entry.";
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                if (!Info_MaxEditedOn.ContainsKey(RefId))
                                                {
                                                    Info_MaxEditedOn.Add(RefId, MaxEditOn);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //else 
                            if (Row["ITEM_PROFILE"].ToString() == "OTHER DEPOSITS")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    RefId = Row["TR_TRF_CROSS_REF_ID"].ToString();
                                    if (RefId != null)
                                    {
                                        if (RefId.Length > 0)
                                        {
                                            object FinalPayDate = BASE._DepositsDBOps.GetFinalPaymentDate(RefId);
                                            if (IsDate(FinalPayDate.ToString()) & Convert.ToDateTime(FinalPayDate) != DateTime.MinValue)
                                            {
                                                jsonParam.message = "Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " + Convert.ToDateTime(FinalPayDate).ToLongDateString();
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        SetBaseSession("Jv_Info_MaxEdit_JV", Info_MaxEditedOn);
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Journal Entry";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Journal";
                        jsonParam.popup_form_path = "/Account/JournalVoucher/Frm_Voucher_Win_Journal/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn=" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Asset_Transfer)
                    {
                        DataTable Rec = BASE._AssetTransfer_DBOps.GetRecord(xTemp_MID, 1);
                        if (Rec != null)
                        {
                            if (Rec.Rows.Count > 0)
                            {
                                if (Convert.ToDateTime(iREC_EDIT_ON) != (DateTime)Rec.Rows[0]["REC_EDIT_ON"])
                                {
                                    isRecChanged = true;
                                }
                                if (!Convert.IsDBNull(Rec.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                                {
                                    if (Rec.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                                    {
                                        jsonParam.message = "Matched Asset Transfer Cannot Be Edited...!<br><br>Record Has Already Been Matched In The Background";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = isRecChanged;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    if (isRecChanged)
                                    {
                                        jsonParam.message = "Record Has Already Been Unmatched In The Background";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = true;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        int _RowHandle = 0;
                        if (Convert.ToInt32(iTR_SR_NO) == 2)
                        {
                            _RowHandle = 1;
                        }
                        DataTable d2= BASE._AssetTransfer_DBOps.GetRecord(xTemp_MID, 1);
                        var iTR_TYPE_AssetTransfer_item = "";
                        if (d2.Rows.Count > 0) 
                        {
                            iTR_TYPE_AssetTransfer_item = d2.Rows[0]["TR_TYPE"].ToString(); //credit for from entry and debit for to entry
                        }
                        if (iTR_TYPE_AssetTransfer_item == "CREDIT")
                        {
                            jsonParam.message = "Asset Transfer From Entry Cannot Be Edited...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime? Info_MaxEditedOn = null;
                        if (iTR_TYPE_AssetTransfer_item == "DEBIT")
                        {
                            DataTable d1 = BASE._AssetTransfer_DBOps.GetRecord(xTemp_MID, 2);
                            DateTime MaxEditOn = (DateTime)BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn((string)d1.Rows[0]["TR_REF_OTHERS"]);
                            if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                            {
                                Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                                Param.CrossRefId = (string)d1.Rows[0]["TR_REF_OTHERS"];
                                Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                                Param.SpecifiedEntryType = EntryType.Both;
                                int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count;
                                if (Adj_NextYear > 0)
                                {
                                    jsonParam.message = "Sorry! Some adjustments have already been made on this asset<br><br> Please delete those entries for Editing this Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                Info_MaxEditedOn = MaxEditOn;
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ Asset Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Asset_Transfer";
                        jsonParam.popup_form_path = "/Account/AssetTransferVoucher/Frm_Voucher_Win_Asset_Transfer/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID1=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.WIP_Finalization)
                    {
                        string xTemp_AssetID = "";
                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                        if (xTemp_AssetID.Length > 0)
                        {
                            DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                            if (SaleRecord != null)
                            {
                                if (SaleRecord.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Sold On " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Editing This Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                            if (AssetTrfRecord != null)
                            {
                                if (AssetTrfRecord.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Transfered On " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Editing This Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID); //Gets any Txn where Current Asset is referenced
                            if (ReferenceRecord != null)
                            {
                                if (ReferenceRecord.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Referred In A Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " Of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Editing This Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        DateTime? Info_MaxEditedOn = null;
                        DataTable GetRefRecordIDS = BASE._Voucher_DBOps.GetRefRecordIDS(xTemp_MID);
                        int count = GetRefRecordIDS.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow Row = GetRefRecordIDS.Rows[i];
                            if ((string)Row["ITEM_PROFILE"] == "OTHER ASSETS")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    DateTime MaxEditOn = (DateTime)BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(xCross_Ref_Id);
                                    if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xCross_Ref_Id);
                                        if (SaleRecord != null)
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Sold On " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Editing This Entry.";
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xCross_Ref_Id);
                                        if (AssetTrfRecord != null)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Transfered On " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Editing This Entry.";
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                                        Param.CrossRefId = xCross_Ref_Id;
                                        Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                                        Param.SpecifiedEntryType = EntryType.Both;
                                        int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count;
                                        if (Adj_NextYear > 0)
                                        {
                                            jsonParam.message = "Sorry! Some Adjustments Have Already Been Made On This Asset<br><br> Please Delete Those Entries For Editing This Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        Info_MaxEditedOn = MaxEditOn;
                                    }
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Edit ~ WIP Finalization";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_WIP_Finalization";
                        jsonParam.popup_form_path = "/Account/WIPFinalization/Frm_Voucher_Win_WIP_Finalization/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn=" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        #region Delete
        /// <summary>
        /// Voucher delete call when only Txn Entry Record ID is available on Screen
        /// </summary>
        /// <param name="Txn_Rec_ID"></param>      
        /// <param name="Me_Text"></param>
        /// <param name="GridToRefresh"></param>
        /// <param name="Tr_Date"></param>
        /// <param name="MultiUserConfirmationTaken"></param>
        /// <param name="AssetTransferDeleteConfirmationTaken"></param>
        [ActionName("Vouchers_Delete1")]
        public ActionResult Vouchers_Delete(string Txn_Rec_ID, string Me_Text, string GridToRefresh, string Tr_Date = null, bool MultiUserConfirmationTaken = false, bool AssetTransferDeleteConfirmationTaken = false)
        {
            if (Txn_Rec_ID != "NOTE-BOOK")
            {
                DataTable TxnTable = BASE._Voucher_DBOps.GetEntryDetails(Txn_Rec_ID);
                return Vouchers_Delete(Txn_Rec_ID, TxnTable.Rows[0]["TR_M_ID"].ToString(), TxnTable.Rows[0]["REC_EDIT_ON"].ToString(),
                      TxnTable.Rows[0]["REC_ADD_ON"].ToString(), Me_Text, TxnTable.Rows[0]["TR_DATE"].ToString(),
                                           TxnTable.Rows[0]["ACTION STATUS"].ToString(), TxnTable.Rows[0]["TR_CODE"].ToString(),
                                           TxnTable.Rows[0]["TR_ITEM_ID"].ToString(),
                                           TxnTable.Rows[0]["TR_SR_NO"].ToString(),
                                           TxnTable.Rows[0]["TR_TYPE"].ToString(), GridToRefresh, null, MultiUserConfirmationTaken, AssetTransferDeleteConfirmationTaken);
            }
            else
            {
                object RefVar = new object();
                if (string.IsNullOrWhiteSpace(Tr_Date))
                {
                    Tr_Date = DateTime.MinValue.ToString();
                }
                return Vouchers_Delete(Txn_Rec_ID, RefVar.ToString(), RefVar.ToString(), RefVar.ToString(), Me_Text, Tr_Date, RefVar.ToString(), RefVar.ToString(), RefVar.ToString(), RefVar.ToString(), RefVar.ToString(), GridToRefresh, null, MultiUserConfirmationTaken, AssetTransferDeleteConfirmationTaken);
            }
        }
        /// <summary>
        /// Voucher Delete call when only Txn Entry Record Master ID and created Profile REC_ID are available on Screen
        /// </summary>
        /// <param name="Txn_Rec_M_ID"></param>
        /// <param name="Profile_Rec_ID"></param>
        /// <param name="ProfileUsed"></param>       
        /// <param name="Me_Text"></param>
        /// <param name="GridToRefresh"></param>
        /// <param name="MultiUserConfirmationTaken"></param>
        /// <param name="AssetTransferDeleteConfirmationTaken"></param>

        [ActionName("Vouchers_Delete2")]
        public ActionResult Vouchers_Delete(string Txn_Rec_M_ID, string Profile_Rec_ID, string ProfileUsed, string Me_Text, string GridToRefresh, bool MultiUserConfirmationTaken = false, bool AssetTransferDeleteConfirmationTaken = false)
        {
            Common_Lib.RealTimeService.AssetProfiles profiles = (Common_Lib.RealTimeService.AssetProfiles)Enum.Parse(typeof(Common_Lib.RealTimeService.AssetProfiles), ProfileUsed);
            DataTable TxnTable = BASE._Voucher_DBOps.GetProfileEntryDetails(Profile_Rec_ID, Txn_Rec_M_ID, profiles);
            return Vouchers_Delete(TxnTable.Rows[0]["TXN_REC_ID"].ToString(), Txn_Rec_M_ID, TxnTable.Rows[0]["REC_EDIT_ON"].ToString(),
                   TxnTable.Rows[0]["REC_ADD_ON"].ToString(), Me_Text, TxnTable.Rows[0]["TR_DATE"].ToString(),
                                        TxnTable.Rows[0]["ACTION STATUS"].ToString(), TxnTable.Rows[0]["TR_CODE"].ToString(),
                                        TxnTable.Rows[0]["TR_ITEM_ID"].ToString(),
                                        TxnTable.Rows[0]["TR_SR_NO"].ToString(),
                                        TxnTable.Rows[0]["TR_TYPE"].ToString(), GridToRefresh, null, MultiUserConfirmationTaken, AssetTransferDeleteConfirmationTaken);
        }
        /// <summary>
        /// Voucher delete call when all Txn Entry Record Data is available on Screen
        /// </summary>
        /// <param name="xTemp_ID"></param>
        /// <param name="xTemp_MID"></param>
        /// <param name="iREC_EDIT_ON"></param>
        /// <param name="iREC_ADD_ON"></param>
        /// <param name="Me_Text"></param>
        /// <param name="iTR_DATE"></param>
        /// <param name="iACTION_STATUS"></param>
        /// <param name="iTR_CODE"></param>
        /// <param name="iTR_ITEM_ID"></param>
        /// <param name="iTR_SR_NO"></param>
        /// <param name="iTR_TYPE"></param>
        /// <param name="GridToRefresh"></param>
        /// <param name="MultiUserConfirmationTaken"></param>
        /// <param name="AssetTransferDeleteConfirmationTaken"></param>

        [ActionName("Vouchers_Delete3")]
        public ActionResult Vouchers_Delete(string xTemp_ID, string xTemp_MID, string iREC_EDIT_ON, string iREC_ADD_ON, string Me_Text, string iTR_DATE, string iACTION_STATUS, string iTR_CODE, string iTR_ITEM_ID, string iTR_SR_NO, string iTR_TYPE, string GridToRefresh, string RowKey, bool MultiUserConfirmationTaken = false, bool AssetTransferDeleteConfirmationTaken = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                string Closed_Bank_Account_No = "";
                string xMaster_ID = "";
                if (!string.IsNullOrWhiteSpace(RowKey))
                {
                    if (GridToRefresh == "CashBookListGrid")
                    {
                        //var CashbookData = (List<CB_Grid_Model>)GetBaseSession("MainGrid_Data_CB");
                        //if (CashbookData != null && CashbookData.Count > 0)
                        //{
                        //    CB_Grid_Model CashBookRowData = CashbookData.Where(x => x.Grid_PK == RowKey).FirstOrDefault();
                        //    xTemp_ID = CashBookRowData.iREC_ID;
                        //    xTemp_MID = CashBookRowData.iTR_M_ID;
                        //    iREC_EDIT_ON = CashBookRowData.iREC_EDIT_ON.ToString();
                        //    iREC_ADD_ON = CashBookRowData.iREC_ADD_ON.ToString();                        
                        //    iTR_DATE = CashBookRowData.iTR_DATE.ToString();
                        //    iACTION_STATUS = CashBookRowData.iACTION_STATUS;
                        //    iTR_CODE = CashBookRowData.iTR_CODE != null ? CashBookRowData.iTR_CODE.ToString() : "";
                        //    iTR_ITEM_ID = CashBookRowData.iTR_ITEM_ID;
                        //    iTR_SR_NO = iTR_SR_NO = CashBookRowData.iTR_SR_NO != null ? CashBookRowData.iTR_SR_NO.ToString() : "";
                        //    iTR_TYPE = CashBookRowData.iTR_TYPE;
                        //}
                        //else 
                        //{
                        iREC_EDIT_ON = string.IsNullOrWhiteSpace(iREC_EDIT_ON) ? iREC_EDIT_ON : Convert.ToDateTime(iREC_EDIT_ON).ToString();
                        iREC_ADD_ON = string.IsNullOrWhiteSpace(iREC_ADD_ON) ? iREC_ADD_ON : Convert.ToDateTime(iREC_ADD_ON).ToString();
                        iTR_DATE = string.IsNullOrWhiteSpace(iTR_DATE) ? iTR_DATE : Convert.ToDateTime(iTR_DATE).ToString();
                        iTR_CODE = iTR_CODE ?? "";
                        iTR_SR_NO = iTR_SR_NO ?? "";
                        //}
                        Me_Text = "Cash Book";
                    }
                    else if (GridToRefresh == "ITR_PostedGrid")
                    {
                        var GridData = (List<Return_InternalTransferRegister_Posted>)GetBaseSession("ITR_GridView1_ITReg");
                        if (GridData != null && GridData.Count > 0)
                        {
                            var RowData = GridData.Where(x => x.ID == RowKey).FirstOrDefault();
                            xTemp_ID = RowData.ID;
                            xTemp_MID = RowData.MID;
                            iREC_EDIT_ON = RowData.EditDate.ToString();
                            iREC_ADD_ON = RowData.AddDate.ToString();
                            Me_Text = "Internal Transfer Register";
                            iTR_DATE = RowData.xDate.ToString();
                            iACTION_STATUS = RowData.ActionStatus;
                            iTR_CODE = "8";
                            iTR_ITEM_ID = RowData.TR_ITEM_ID;
                            iTR_SR_NO = RowData.TR_SR_NO != null ? RowData.TR_SR_NO.ToString() : "";
                            iTR_TYPE = RowData.TR_TYPE;
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(xTemp_MID))
                {
                    xMaster_ID = xTemp_MID;
                }
                else
                {
                    xMaster_ID = xTemp_ID;
                }
                bool isRecChanged = false;
                if (xTemp_ID == "OPENING BALANCE")
                {
                    jsonParam.result = false;
                    jsonParam.message = "Opening Balance Cannot Edit/Delete from Voucher Entry...!<br><br>Note: Use Profile - Cash A/c. Information for CASH<br>or Bank A/c. Information for BANK";
                    jsonParam.title = "Information...";
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam

                    }, JsonRequestBehavior.AllowGet);
                }
                if (xTemp_ID == "NOTE-BOOK")
                {
                    jsonParam.result = true;
                    jsonParam.popup_title = "Note-Book Entry (" + Me_Text + ")";
                    jsonParam.popup_form_name = "Frm_NoteBook_Info";
                    jsonParam.popup_form_path = "/Account/NoteBook/Frm_NoteBook_Info/";
                    jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Edit + "&xEntryDate=" + iTR_DATE;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!string.IsNullOrWhiteSpace(xTemp_ID))
                {
                    bool Entry_Found = false;
                    string xRec_Status = "";
                    string xTR_CODE = "";
                    string xTemp_D_Status = "";
                    string xCross_Ref_Id = "";
                    string multiUserMsg = "";
                    bool AllowUser = false;
                    DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
                    if (Status == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Status.Rows.Count > 0)
                    {
                        Entry_Found = true;
                        xRec_Status = Status.Rows[0]["REC_STATUS"].ToString();
                        xTR_CODE = Status.Rows[0]["TR_CODE"].ToString();
                        for (int i = 0; i < Status.Rows.Count; i++)
                        {
                            if (!Convert.IsDBNull(Status.Rows[i]["TR_TRF_CROSS_REF_ID"]))
                            {
                                xCross_Ref_Id = (string)Status.Rows[i]["TR_TRF_CROSS_REF_ID"];
                            }
                            if (xCross_Ref_Id.Length > 0)
                            {
                                break;
                            }
                        }
                        string xStatus = iACTION_STATUS;
                        var value = ((int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus));
                        if (value != Convert.ToInt32(xRec_Status))
                        {
                            if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Locked)
                            {
                                multiUserMsg = "The Record Has Been Locked In The Background By Another User.";
                            }
                            else if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Completed)
                            {
                                multiUserMsg = "The Record Has Been Unlocked In The Background By Another User.";
                                AllowUser = true;
                            }
                            else
                            {
                                multiUserMsg = "The Record Has Been Changed In The Background By Another User.";
                                AllowUser = true;
                            }
                            if (AllowUser && MultiUserConfirmationTaken == false)
                            {
                                jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    if (Entry_Found == false)
                    {
                        jsonParam.message = "Entry Not Found/Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Locked)
                    {
                        multiUserMsg = multiUserMsg.Length > 0 ? "<br><br>" + multiUserMsg : multiUserMsg;
                        jsonParam.message = "Locked Entry Cannot Be Edited/Deleted...!" + multiUserMsg + "<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (Get_Closed_Bank_Status(xMaster_ID, ref Closed_Bank_Account_No))
                    {
                        jsonParam.message = "Entry Cannot Be Edited/Deleted...!<br><br>In This Entry, Used Bank A/C No.: " + Closed_Bank_Account_No + " Was Closed...!!!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToInt32(xTR_CODE) == 5 || Convert.ToInt32(xTR_CODE) == 6)
                    {
                        DataTable Status2 = BASE._Voucher_DBOps.GetDonationStatus(xTemp_ID);
                        if (Status2 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Status2.Rows.Count > 0)
                        {
                            xTemp_D_Status = (string)Status2.Rows[0]["DS_STATUS"];
                        }
                    }

                    int xVCode = Convert.ToInt32(iTR_CODE);
                    string xItemID = iTR_ITEM_ID;
                    if (xVCode == (int)Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Cash Deposit / Withdrawn";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Cash";
                        jsonParam.popup_form_path = "/Account/CashDepositAndWithdrawnVoucher/Frm_Voucher_Win_Cash/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Bank_To_Bank_Transfer)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Bank to Bank Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_B2B";
                        jsonParam.popup_form_path = "/Account/BankToBankTransferVoucher/Frm_Voucher_Win_B2B/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Regular)
                    {
                        if ((!string.IsNullOrEmpty(xTemp_D_Status)) && (xTemp_D_Status != "42189485-9b6b-430a-8112-0e8882596f3c") && (xTemp_D_Status != "3a99fadc-b336-480d-8116-fbd144bd7671") && (xTemp_D_Status != "6a7c38ba-5779-4e21-acc7-c1829b7ec933"))
                        {
                            jsonParam.message = "Donation Entry cannot be Edited/Deleted...!<br> Note: Donation Status Changed, Check Donation Register..!!!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_ID, ClientScreen.Accounts_Voucher_Donation))
                        {
                            jsonParam.message = "Sorry! Deposit slip has already been printed for current transaction!!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Donation";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Donation_R";
                        jsonParam.popup_form_path = "/Account/DonationVoucher/Frm_Voucher_Win_Donation_R/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Foreign)
                    {
                        if ((!string.IsNullOrEmpty(xTemp_D_Status)) && (xTemp_D_Status != "42189485-9b6b-430a-8112-0e8882596f3c") && (xTemp_D_Status != "3a99fadc-b336-480d-8116-fbd144bd7671") && (xTemp_D_Status != "6a7c38ba-5779-4e21-acc7-c1829b7ec933"))
                        {
                            jsonParam.message = "Donation Entry cannot be Edited/Deleted...!<br> Note: Donation Status Changed, Check Donation Register..!!!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Foreign Donation";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Donation_F";
                        jsonParam.popup_form_path = "/Account/DonationVoucher/Frm_Voucher_Win_Donation_F/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Collection_Box)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Collection Box";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_C_Box";
                        jsonParam.popup_form_path = "/Account/CollectionBoxVoucher/Frm_Voucher_Win_C_Box/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Payment)
                    {
                        DataTable xTemp_WIP_Dependencies = BASE._WIPCretionVouchers.GetWIP_Dependencies(xTemp_MID); //Get Dependent Entries on WIP creted by current Txn
                        if (xTemp_WIP_Dependencies.Rows.Count > 0)
                        {
                            jsonParam.message = "Sorry ! Some Adjustment has been made against the WIP(" + xTemp_WIP_Dependencies.Rows[0]["WIP_REF"].ToString() + ") raised in current transaction on " + Convert.ToDateTime(xTemp_WIP_Dependencies.Rows[0]["Date"]).ToString("dd-MM-yyyy") + " for Rs." + xTemp_WIP_Dependencies.Rows[0]["AMOUNT"].ToString() + "." + "<br><br>Please delete the record for Deleting this Entry.";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xCross_Ref_Id);
                        if (AssetTrfRecord != null)
                        {
                            if (AssetTrfRecord.Rows.Count > 0)
                            {
                                jsonParam.message = "Sorry ! Selected Entry refers a Advance/Deposit which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        string xTemp_LiabID = BASE._Voucher_DBOps.GetRaisedLiabilityRecID(xTemp_MID); //Get Liab created by current Txn
                        if (xTemp_LiabID.Length > 0)
                        {
                            DataTable txnReferLiab = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_LiabID);//Payment has been made againt the liability raised
                            if (txnReferLiab != null)
                            {
                                if (txnReferLiab.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! Some payment has been made against the liability raised in current transaction on " + Convert.ToDateTime(txnReferLiab.Rows[0]["TR_DATE"]).ToLongDateString() + " for Rs." + txnReferLiab.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br> Please delete the record for deleting this Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = xTemp_LiabID;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            txnReferLiab = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers);
                            if (txnReferLiab != null)
                            {
                                if (txnReferLiab.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry! Liability created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to delete this entry.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        string xTemp_DepID = BASE._Voucher_DBOps.GetRaisedDepositRecID(xTemp_MID); //Get Liab creted by current Txn
                        if (xTemp_DepID.Length > 0)
                        {
                            DataTable txnReferDeposits = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_DepID); //Adjustments/Refund has been made againt the deposit raised
                            if (txnReferDeposits != null)
                            {
                                if (txnReferDeposits.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! Some adjustment / refund has been made against the deposit raised in current transaction on " + Convert.ToDateTime(txnReferDeposits.Rows[0]["TR_DATE"]).ToLongDateString() + " for Rs." + txnReferDeposits.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = xTemp_DepID;
                            param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            txnReferDeposits = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers);
                            if (txnReferDeposits != null)
                            {
                                if (txnReferDeposits.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry! Deposit created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to delete this entry.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        string xTemp_AdvID = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID); //Get Advances creted by current Txn
                        if (xTemp_AdvID.Length > 0)
                        {
                            DataTable txnReferAdvance = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID); //Adjustments/Refund has been made againt the Advance raised
                            if (txnReferAdvance != null)
                            {
                                if (txnReferAdvance.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " + Convert.ToDateTime(txnReferAdvance.Rows[0]["TR_DATE"]).ToLongDateString() + " for Rs." + txnReferAdvance.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = xTemp_AdvID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            txnReferAdvance = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers);
                            if (txnReferAdvance != null)
                            {
                                if (txnReferAdvance.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry! Advance created by current entry is used in some other entry...!<br><br>Please delete that dependency to delete this entry.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        bool IsInsuaranceApplicable = false;
                        DataTable GetAssetItemID = BASE._Voucher_DBOps.GetAssetItemID(xTemp_MID); // Get Actual Item IDs from Selected Transaction
                        int count = GetAssetItemID.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow cRow = GetAssetItemID.Rows[i];
                            string xTemp_ItemID = cRow[0].ToString();
                            DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); //Gets Asset Profile
                            string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                            if (xTemp_AssetProfile.ToUpper() != "NOT APPLICABLE") //Leaving Constuction Items
                            {
                                string xTemp_AssetID = "";
                                switch (xTemp_AssetProfile)  //Get Asset RecID from Particular Table 
                                {
                                    case "GOLD":
                                    case "SILVER":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, xTemp_MID);
                                        break;
                                    case "OTHER ASSETS":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                                        IsInsuaranceApplicable = true;
                                        break;
                                    case "LIVESTOCK":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, xTemp_MID);
                                        break;
                                    case "VEHICLES":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, xTemp_MID);
                                        break;
                                    case "LAND & BUILDING":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, xTemp_MID);
                                        IsInsuaranceApplicable = true;
                                        break;
                                }
                                if (xTemp_AssetID.Length > 0)
                                {
                                    if (IsInsuaranceApplicable)
                                    {
                                        if (BASE.IsInsuranceAudited())
                                        {
                                            jsonParam.message = "Sorry! Selected Entry contains insurance related assets which cannot be deleted after the completion of Insurance audit";
                                            jsonParam.title = "Information...";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                    if (SaleRecord != null)
                                    {
                                        if (SaleRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                                    if (AssetTrfRecord != null)
                                    {
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID); //Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                    if (ReferenceRecord != null)
                                    {
                                        if (ReferenceRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                            else //Non Profile Entries 
                            {
                                string PropertyID = BASE._Voucher_DBOps.GetReferenceRecordID(xTemp_MID);
                                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(PropertyID); //checks if the referred property for constt items has been sold 
                                if (SaleRecord != null)
                                {
                                    if (SaleRecord.Rows.Count > 0)
                                    {
                                        jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, PropertyID);
                                if (AssetTrfRecord != null)
                                {
                                    if (AssetTrfRecord.Rows.Count > 0)
                                    {
                                        jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for editing this Entry.";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        DataTable GetRefRecordIDS = BASE._Voucher_DBOps.GetRefRecordIDS(xTemp_MID);
                        count = GetRefRecordIDS.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow Row = GetRefRecordIDS.Rows[i];
                            if (Row["ITEM_PROFILE"].ToString() == "NOT APPLICABLE")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"])) //Construction Entry
                                {
                                    object RefId = Row["TR_TRF_CROSS_REF_ID"];
                                    if (RefId != null)
                                    {
                                        if (RefId.ToString().Length > 0)
                                        {
                                            DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, Row["ITEM_PROFILE"].ToString(), Row["TR_ITEM_ID"].ToString(), xTemp_MID, Row["TR_AB_ID_1"].ToString(), Common.Navigation_Mode._Delete, RefId.ToString());
                                            if (PROF_TABLE != null)
                                            {
                                                if (PROF_TABLE.Rows.Count == 0)
                                                {
                                                    jsonParam.message = "Sorry !  Some transactions posted against selected property dont allow this operation.";
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                                if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                                {
                                                    jsonParam.message = "Sorry !  Deletion of Selected Payment Entry creates a Negative Closing Balance in Current Year for property with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (Row["ITEM_PROFILE"].ToString() == "WIP")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    object RefId = Row["TR_TRF_CROSS_REF_ID"];
                                    if (RefId != null)
                                    {
                                        if (RefId.ToString().Length > 0)
                                        {
                                            DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, Row["ITEM_PROFILE"].ToString(), Row["TR_ITEM_ID"].ToString(), xTemp_MID, Row["TR_AB_ID_1"].ToString(), Common.Navigation_Mode._Delete, RefId.ToString());
                                            if (PROF_TABLE != null)
                                            {
                                                if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                                                {
                                                    if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                                    {
                                                        jsonParam.message = "Sorry !  Deletion of Selected Payment Entry creates a Negative Closing Balance in Next Year for " + Row["ITEM_PROFILE"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                        jsonParam.title = "Error!!";
                                                        jsonParam.result = false;
                                                        return Json(new
                                                        {
                                                            jsonParam
                                                        }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                                {
                                                    jsonParam.message = "Sorry !  Deletion of Selected Payment Entry creates a Negative Closing Balance in Current Year for" + Row["ITEM_PROFILE"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Payment";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gen_Pay";
                        jsonParam.popup_form_path = "/Account/PaymentVoucher/Frm_Voucher_Win_Gen_Pay/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Receipt)
                    {
                        object FinalPayDate = BASE._DepositsDBOps.GetFinalPaymentDate(xCross_Ref_Id, xTemp_MID);
                        if (IsDate(FinalPayDate.ToString()))
                        {
                            if (Convert.ToDateTime(FinalPayDate) != DateTime.MinValue)
                            {
                                jsonParam.message = "Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " + Convert.ToDateTime(FinalPayDate).ToLongDateString();
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (BASE._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_MID, ClientScreen.Accounts_Voucher_Receipt))
                        {
                            jsonParam.message = "Sorry! Deposit slip has already been printed for current transaction!!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xCross_Ref_Id);
                        if (AssetTrfRecord != null)
                        {
                            if (AssetTrfRecord.Rows.Count > 0)
                            {
                                jsonParam.message = "Sorry ! Selected Entry refers a Advance/Deposit which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Receipt";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gen_Rec";
                        jsonParam.popup_form_path = "/Account/ReceiptVoucher/Frm_Voucher_Win_Gen_Rec/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Gift)
                    {
                        DataTable xTemp_WIP_Dependencies = BASE._WIPCretionVouchers.GetWIP_Dependencies(xTemp_MID); //Get Dependent Entries on WIP creted by current Txn
                        if (xTemp_WIP_Dependencies.Rows.Count > 0)
                        {
                            jsonParam.message = "Sorry ! Some Adjustment has been made against the WIP(" + xTemp_WIP_Dependencies.Rows[0]["WIP_REF"].ToString() + ") raised in current transaction on " + Convert.ToDateTime(xTemp_WIP_Dependencies.Rows[0]["Date"]).ToString("dd-MM-yyyy") + " for Rs." + xTemp_WIP_Dependencies.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        bool IsInsuranceApplicable = false;
                        DataTable GetAssetItemID = BASE._Voucher_DBOps.GetAssetItemID(xTemp_MID); // Get Actual Item IDs from Selected Transaction
                        int count = GetAssetItemID.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow cRow = GetAssetItemID.Rows[i];
                            string xTemp_ItemID = cRow[0].ToString();
                            DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); //Gets Asset Profile
                            string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                            if (xTemp_AssetProfile.ToUpper() != "NOT APPLICABLE") //Leaving Constuction Items
                            {
                                string xTemp_AssetID = "";
                                switch (xTemp_AssetProfile)
                                {
                                    case "GOLD":
                                    case "SILVER":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, xTemp_MID);
                                        break;
                                    case "OTHER ASSETS":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                                        IsInsuranceApplicable = true;
                                        break;
                                    case "LIVESTOCK":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, xTemp_MID);
                                        break;
                                    case "VEHICLES":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, xTemp_MID);
                                        break;
                                    case "LAND & BUILDING":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, xTemp_MID);
                                        IsInsuranceApplicable = true;
                                        break;
                                }
                                if (xTemp_AssetID.Length > 0)
                                {
                                    if (IsInsuranceApplicable)
                                    {
                                        if (BASE.IsInsuranceAudited())
                                        {
                                            jsonParam.message = "Sorry! Selected Entry contains insurance related assets which cannot be deleted after the completion of Insurance audit";
                                            jsonParam.title = "Information...";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                    if (SaleRecord != null)
                                    {
                                        if (SaleRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID); //Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                    if (ReferenceRecord != null)
                                    {
                                        if (ReferenceRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                                    if (AssetTrfRecord != null)
                                    {
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                            else //Non Profile Entries 
                            {
                                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(BASE._Voucher_DBOps.GetReferenceRecordID(xTemp_MID)); //checks if the referred property for constt items has been sold 
                                if (SaleRecord != null)
                                {
                                    if (SaleRecord.Rows.Count > 0)
                                    {
                                        jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, BASE._Voucher_DBOps.GetReferenceRecordID(xTemp_MID));
                                if (AssetTrfRecord != null)
                                {
                                    if (AssetTrfRecord.Rows.Count > 0)
                                    {
                                        jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        DataTable GetRefRecordIDS = BASE._Voucher_DBOps.GetRefRecordIDS(xTemp_MID);
                        count = GetRefRecordIDS.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow Row = GetRefRecordIDS.Rows[i];
                            if ((string)Row["ITEM_PROFILE"] == "WIP")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    string RefId = Row["TR_TRF_CROSS_REF_ID"].ToString();
                                    if (RefId != null)
                                    {
                                        if (RefId.Length > 0)
                                        {
                                            DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, Row["ITEM_PROFILE"].ToString(), Row["TR_ITEM_ID"].ToString(), xTemp_MID, Row["TR_AB_ID_1"].ToString(), Common.Navigation_Mode._Delete,RefId);
                                            if (PROF_TABLE != null)
                                            {
                                                if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                                                {
                                                    if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                                    {
                                                        jsonParam.message = "Sorry! Deletion of Selected Donation Entry creates a Negative Closing Balance in Next Year for " + Row["ITEM_PROFILE"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                        jsonParam.title = "Error!!";
                                                        jsonParam.result = false;
                                                        return Json(new
                                                        {
                                                            jsonParam
                                                        }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                                {
                                                    jsonParam.message = "Sorry! Deletion of Selected Donation Entry creates a Negative Closing Balance in Current Year for " + Row["ITEM_PROFILE"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Donation in Kind";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gift";
                        jsonParam.popup_form_path = "/Account/DonationInKindVoucher/Frm_Voucher_Win_Gift/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Internal_Transfer)
                    {
                        DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1);
                        if (d1 != null)
                        {
                            if (d1.Rows.Count > 0)
                            {
                                if (Convert.ToDateTime(iREC_EDIT_ON) != (DateTime?)d1.Rows[0]["REC_EDIT_ON"])
                                {
                                    isRecChanged = true;
                                }
                                if (!Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                                {
                                    if (d1.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                                    {
                                        jsonParam.message = "Matched Internal Transfer Cannot be Deleted...!<br><br>Record has already been matched in the background";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = isRecChanged;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    if (isRecChanged)
                                    {
                                        jsonParam.message = "Record has already been unmatched in the background";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = true;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        if (BASE._DepositSlipsDBOps.GetSlipPrintStatus(xTemp_MID, ClientScreen.Accounts_Voucher_Internal_Transfer))
                        {
                            jsonParam.message = "Sorry! Deposit slip has already been printed for current transaction!!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Internal Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_I_Transfer";
                        jsonParam.popup_form_path = "/Account/InternalTransfer/Frm_Voucher_Win_I_Transfer/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID_1=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Fixed_Deposits)
                    {
                        object IsClosed = BASE._FD_Voucher_DBOps.GetFDCloseDate(xTemp_MID);
                        object Has_Int_TDS = BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, xCross_Ref_Id, 1);
                        object IsRenew = BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 2); //CHECKS IF CURRENT TRANSACTIONS IS A RENEWAL ONE, IF YES THEN GET THE ID OF NEWLY CREATED FD
                        object New_FD = null;
                        if ((int)IsRenew > 0)
                        {
                            New_FD = BASE._FD_Voucher_DBOps.GetNewFDIdFromClosed(xCross_Ref_Id);
                            if (New_FD == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                Has_Int_TDS = BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, New_FD.ToString(), 1);
                            }
                        }
                        Common.FDAction? iAction = null;
                        string TitleX = "New";//Redmine Bug #132913 fixed
                        string iSpecific_ItemID = string.Empty;
                        string CreatedFDID = string.Empty;
                        switch (xItemID)
                        {
                            case "f6e4da62-821f-4961-9f93-f5177fca2a77":       //FD New
                                if (IsDate(IsClosed.ToString()))
                                {
                                    jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else if ((int)Has_Int_TDS > 0)
                                {
                                    jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    iAction = Common.FDAction.New_FD;
                                    TitleX = "FD Creation";
                                    CreatedFDID = xCross_Ref_Id;
                                }
                                break;
                            case "4eb60d78-ce90-4a9f-891b-7a82d79dc84b":     //FD Renewed
                                if (IsDate(IsClosed.ToString()))
                                {
                                    jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else if ((int)Has_Int_TDS > 0)
                                {
                                    jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    iAction = Common.FDAction.Renew_FD;
                                    TitleX = "FD Renewal";
                                    CreatedFDID = xCross_Ref_Id;
                                }
                                break;
                            case "1ed5cbe4-c8aa-4583-af44-eba3db08e117":    //FD Close - Interest, 65730a27-e365-4195-853e-2f59225fe8f4
                            case "65730a27-e365-4195-853e-2f59225fe8f4":
                                if ((int)IsRenew > 0)
                                {
                                    if (IsDate(IsClosed.ToString()))
                                    {
                                        jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else if ((int)Has_Int_TDS > 0)
                                    {
                                        jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        iAction = Common.FDAction.Renew_FD;
                                        TitleX = "FD Renewal";
                                        CreatedFDID = New_FD.ToString();
                                    }
                                }
                                else
                                {
                                    iAction = Common.FDAction.Close_FD;
                                    TitleX = "FD Closure";
                                }
                                break;
                            default:
                                if ((int)IsRenew > 0)
                                {
                                    if (IsDate(IsClosed.ToString()))
                                    {
                                        jsonParam.message = "FD Already Closed/Renewed/Transferred...!";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else if ((int)Has_Int_TDS > 0)
                                    {
                                        jsonParam.message = "Interest/TDS already entered against FD!<br><br>Please remove such transactions to edit/delete this FD.";
                                        jsonParam.title = "Information...";
                                        jsonParam.result = false;
                                        return Json(new
                                        {
                                            jsonParam
                                        }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        iAction = Common.FDAction.Renew_FD;
                                        TitleX = "FD Renewal";
                                        CreatedFDID = New_FD.ToString();
                                    }
                                }
                                else
                                {
                                    // fdclose principle
                                    if ((int)BASE._FD_Voucher_DBOps.GetCount(xTemp_MID, "", 3) > 0)
                                    {
                                        iAction = Common.FDAction.Close_FD;
                                        TitleX = "FD Closure";
                                    }
                                    else
                                    {
                                        iSpecific_ItemID = xItemID;
                                    }
                                }
                                break;
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ " + TitleX;
                        jsonParam.popup_form_name = "Frm_Voucher_Win_FD";
                        jsonParam.popup_form_path = "/Account/FDVoucher/Frm_Voucher_Win_FD/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&iSpecific_ItemID=" + iSpecific_ItemID + "&iAction=" + iAction + "&TitleX=" + TitleX + "&xMID=" + xTemp_MID + "&CreatedFDID=" + CreatedFDID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Sale_Asset)
                    {
                        string xTemp_AdvID = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID); //Get advance cretaed by current Txn
                        if (xTemp_AdvID.Length > 0)
                        {
                            DataTable txnReferAdvance = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID); //Adjustments/ Refund has been made againt the Advance raised
                            if (txnReferAdvance != null)
                            {
                                if (txnReferAdvance.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " + Convert.ToDateTime(txnReferAdvance.Rows[0]["TR_DATE"]).ToLongDateString() + " for Rs." + txnReferAdvance.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for deleting this Entry.";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = xTemp_AdvID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            txnReferAdvance = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers);
                            if (txnReferAdvance != null)
                            {
                                if (txnReferAdvance.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry! Advance created by current entry is used in some other entry...!<br><br>Please delete that dependency to delete this entry.";
                                    jsonParam.title = Me_Text;
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        DateTime MaxEditOn = Convert.ToDateTime(BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(xCross_Ref_Id));
                        DateTime? Info_MaxEditedOn = null;
                        if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                        {
                            Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                            Param.CrossRefId = xCross_Ref_Id;
                            Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            Param.SpecifiedEntryType = EntryType.Both;
                            int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count;
                            if (Adj_NextYear > 0)
                            {
                                jsonParam.message = "Sorry! Some adjustments have already been made on this asset<br><br>Please delete those entries for deleting this Entry.";
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            Info_MaxEditedOn = MaxEditOn;
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Sale Of Asset";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Sale_Asset";
                        jsonParam.popup_form_path = "/Account/SaleAsset/Frm_Voucher_Win_Sale_Asset/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn=" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership)
                    {
                        object GetValue = "";
                        GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xMaster_ID);
                        if (GetValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                        {
                            jsonParam.message = "Discontiuned Member Entry cannot be Edited/Deleted...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime xDate = Convert.ToDateTime(iREC_ADD_ON);
                        bool TrFound = false;
                        string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                        DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xMaster_ID);
                        if (T1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        int count = T1.Rows.Count;
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                DataRow XRow = T1.Rows[i];
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
                                jsonParam.message = "Some another Entry available after this Entry of same Member....!";
                                jsonParam.title = "Cannot Edit/Delete...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0)
                        {
                            jsonParam.message = "Membership Receipt generated Voucher cannot be Edited/Deleted...!";//Redmine Bug #135913 fixed
                            jsonParam.title = Me_Text;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Membership";
                        jsonParam.popup_form_name = "Frm_Voucher_Membership";
                        jsonParam.popup_form_path = "/Membership/MembershipReceiptRegister/Frm_Voucher_Membership/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Add_Time=" + CurRecordAddOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership_Renewal)
                    {
                        object GetValue = "";
                        GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xMaster_ID);
                        if (GetValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                        {
                            jsonParam.message = "Discontiuned Member Entry cannot be Edited/Deleted...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime xDate = Convert.ToDateTime(iREC_ADD_ON);
                        bool TrFound = false;
                        string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                        DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xMaster_ID);
                        if (T1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        int count = T1.Rows.Count;
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                DataRow XRow = T1.Rows[i];
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
                                jsonParam.message = "Some another Entry available after this Entry of same Member....!";
                                jsonParam.title = "Cannot Edit/Delete...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0)
                        {
                            jsonParam.message = "Membership Receipt generated Voucher cannot be Edited/Deleted...!";//Redmine Bug #135913 fixed
                            jsonParam.title = Me_Text;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        //jsonParam.result = true;
                        //jsonParam.popup_title = "Delete ~ Membership Renewal";
                        //jsonParam.popup_form_name = "Frm_Voucher_Membership_Renewal";
                        //jsonParam.popup_form_path = "";
                        //jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Add_Time=" + CurRecordAddOn + "&GridToRefresh=" + GridToRefresh;
                        jsonParam.result = false;
                        jsonParam.message = "Delete of Membership Renewal is not allowed";
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership_Conversion)
                    {
                        object GetValue = "";
                        GetValue = BASE._Membership_DBOps.GetDiscontinued(true, xMaster_ID);
                        if (GetValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (GetValue.ToString().ToUpper() == "DISCONTINUED")
                        {
                            jsonParam.message = "Discontiuned Member Entry cannot be Edited/Deleted...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        DateTime xDate = Convert.ToDateTime(iREC_ADD_ON);
                        bool TrFound = false;
                        string CurRecordAddOn = xDate.ToString("yyyy-MM-dd HH:mm:ss");
                        DataTable T1 = BASE._Membership_DBOps.GetMasterTransactionList(true, xMaster_ID);
                        if (T1 == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        int count = T1.Rows.Count;
                        if (count > 0)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                DataRow XRow = T1.Rows[i];
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
                                jsonParam.message = "Some another Entry available after this Entry of same Member....!";
                                jsonParam.title = "Cannot Edit/Delete...";
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if ((int)BASE._Membership_Receipt_Register_DBOps.GetReceiptCount(xMaster_ID) > 0)
                        {
                            jsonParam.message = "Membership Receipt generated voucher cannot be Edited/Deleted...!";
                            jsonParam.title = Me_Text;
                            jsonParam.result = false;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        //jsonParam.result = true;
                        //jsonParam.popup_title = "Delete ~ Membership Conversion";
                        //jsonParam.popup_form_name = "Frm_Voucher_Membership_Conversion";
                        //jsonParam.popup_form_path = "";
                        //jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Add_Time=" + CurRecordAddOn + "&GridToRefresh=" + GridToRefresh;
                        jsonParam.result = false;
                        jsonParam.message = "Delete of Membership Conversion is not allowed";
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Journal)
                    {
                        object UseCount = 0;
                        DataTable GetAssetItemID = BASE._Voucher_DBOps.GetAssetItemID(xTemp_MID); //Get Actual Asset IDs from Selected Transaction
                        int count = GetAssetItemID.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow cRow = GetAssetItemID.Rows[i];
                            string xTemp_ItemID = cRow[0].ToString();
                            DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); //Gets Asset Profile
                            string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                            if (xTemp_AssetProfile.ToUpper() != "NOT APPLICABLE") //Leaving Constuction Items
                            {
                                string xTemp_AssetID = "";
                                switch (xTemp_AssetProfile)
                                {
                                    case "GOLD":
                                    case "SILVER":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, xTemp_MID);
                                        break;
                                    case "OTHER ASSETS":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                                        break;
                                    case "LIVESTOCK":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, xTemp_MID);
                                        break;
                                    case "VEHICLES":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, xTemp_MID);
                                        break;
                                    case "LAND & BUILDING":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, xTemp_MID);
                                        break;
                                    case "WIP":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.WIP_INFO, xTemp_MID);
                                        break;
                                }
                                if (xTemp_AssetID.Length > 0)
                                {
                                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                    if (SaleRecord != null)
                                    {
                                        if (SaleRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a " + xTemp_AssetProfile + " which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                                    if (AssetTrfRecord != null)
                                    {
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a " + xTemp_AssetProfile + " which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID);
                                    if (ReferenceRecord != null)
                                    {
                                        if (ReferenceRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a " + xTemp_AssetProfile + " which was referred in a Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }
                        string RefId = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(xTemp_MID);
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._AdvanceDBOps.GetAdvancePaymentCount(RefId);
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! Advance created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Delete this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers).Rows.Count;
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! Advance created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Delete this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        RefId = BASE._Voucher_DBOps.GetRaisedDepositRecID(xTemp_MID);
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._DepositsDBOps.GetTransactionCount(RefId);
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! deposit created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Delete this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers).Rows.Count;
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! deposit created by current  journal entry is used in some other entry...<br><br>Please delete that dependency to Delete this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        RefId = BASE._Voucher_DBOps.GetRaisedLiabilityRecID(xTemp_MID);
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._LiabilityDBOps.GetTransactionCount(RefId);
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! liability created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Delete this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                            Parameter_GetJornalEntryAdjustments param = new Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = xTemp_MID;
                            param.SpecifiedEntryType = EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, ClientScreen.Accounts_Vouchers).Rows.Count;
                            if ((int)UseCount > 0)
                            {
                                jsonParam.message = "Sorry! liability created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to Delete this entry.";
                                jsonParam.title = Me_Text;
                                jsonParam.result = false;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        Dictionary<string, DateTime> Info_MaxEditedOn = new Dictionary<string, DateTime>();
                        DataTable GetRefRecordIDS = BASE._Voucher_DBOps.GetRefRecordIDS(xTemp_MID);
                        count = GetRefRecordIDS.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow Row = GetRefRecordIDS.Rows[i];
                            if (Row["ITEM_PROFILE"].ToString() == "OTHER ASSETS" || Row["ITEM_PROFILE"].ToString() == "LAND & BUILDING")
                            {
                                if (BASE.IsInsuranceAudited())
                                {
                                    jsonParam.message = "Sorry! Selected Entry contains insurance related assets which cannot be deleted after the completion of Insurance audit";
                                    jsonParam.title = "Information...";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if (Row["ITEM_PROFILE"].ToString() == "GOLD" || Row["ITEM_PROFILE"].ToString() == "SILVER" || Row["ITEM_PROFILE"].ToString() == "OTHER ASSETS" || Row["ITEM_PROFILE"].ToString() == "LIVESTOCK" || Row["ITEM_PROFILE"].ToString() == "LAND & BUILDING" || Row["ITEM_PROFILE"].ToString() == "VEHICLES" || Row["ITEM_PROFILE"].ToString() == "ADVANCES" || Row["ITEM_PROFILE"].ToString() == "OTHER DEPOSITS" || Row["ITEM_PROFILE"].ToString() == "OTHER LIABILITIES" || Row["ITEM_VOUCHER_TYPE"].ToString().ToUpper() == "LAND & BUILDING")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    if (Row["TR_TRF_CROSS_REF_ID"] != null)
                                    {
                                        RefId = Row["TR_TRF_CROSS_REF_ID"].ToString();
                                        if (RefId.Length > 0)
                                        {
                                            DateTime MaxEditOn = Convert.ToDateTime(BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(RefId));
                                            if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                                            {
                                                DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(RefId, true);
                                                if (SaleRecord != null)
                                                {
                                                    if (SaleRecord.Rows.Count > 0)
                                                    {
                                                        jsonParam.message = "Sorry ! Selected Entry refers a "+ Row["ITEM_PROFILE"].ToString() + " which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                                        jsonParam.title = "Error!!";
                                                        jsonParam.result = false;
                                                        return Json(new
                                                        {
                                                            jsonParam
                                                        }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, BASE._open_Year_ID, RefId, true);
                                                if (AssetTrfRecord != null)
                                                {
                                                    if (AssetTrfRecord.Rows.Count > 0)
                                                    {
                                                        jsonParam.message = "Sorry ! Selected Entry refers a " + Row["ITEM_PROFILE"].ToString() + " which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                                        jsonParam.title = "Error!!";
                                                        jsonParam.result = false;
                                                        return Json(new
                                                        {
                                                            jsonParam
                                                        }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                                                Param.CrossRefId = RefId;
                                                Param.YearID = BASE._open_Year_ID;
                                                Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                                                Param.Excluded_Rec_M_ID = xMaster_ID;
                                                Param.SpecifiedEntryType = EntryType.Both;
                                                int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_Journal).Rows.Count;
                                                if (Adj_NextYear > 0)
                                                {
                                                    jsonParam.message = "Sorry! Selected Entry contains an " + Row["ITEM_PROFILE"].ToString() + " against which adjustments have been made in current / next year<br><br> Please delete those entries for Deleting this Entry.";
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                if (!Info_MaxEditedOn.ContainsKey(RefId))
                                                {
                                                    Info_MaxEditedOn.Add(RefId, MaxEditOn);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //else 
                            if (Row["ITEM_PROFILE"].ToString() == "ADVANCES" || Row["ITEM_PROFILE"].ToString() == "OTHER LIABILITIES" || Row["ITEM_PROFILE"].ToString() == "WIP" || Row["ITEM_PROFILE"].ToString() == "OTHER DEPOSITS")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    RefId = Row["TR_TRF_CROSS_REF_ID"].ToString();
                                    if (RefId != null)
                                    {
                                        if (RefId.Length > 0)
                                        {
                                            if (Row["ITEM_PROFILE"].ToString() == "OTHER DEPOSITS")
                                            {
                                                object FinalPayDate = BASE._DepositsDBOps.GetFinalPaymentDate(RefId);
                                                if (IsDate(FinalPayDate.ToString()) & Convert.ToDateTime(FinalPayDate) != DateTime.MinValue)
                                                {
                                                    jsonParam.message = "Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " + Convert.ToDateTime(FinalPayDate).ToLongDateString();
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, Row["ITEM_PROFILE"].ToString(), Row["TR_ITEM_ID"].ToString(), xTemp_MID, Row["TR_AB_ID_1"].ToString(), Common.Navigation_Mode._Delete, RefId.ToString());
                                            if (PROF_TABLE != null)
                                            {
                                                if ((BASE.CheckNextYearID(BASE._next_Unaudited_YearID)))
                                                {
                                                    if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                                    {
                                                        jsonParam.message = "Sorry !  Deletion of Selected Journal Entry creates a Negative Closing Balance in Next Year for " + Row["ITEM_PROFILE"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                        jsonParam.title = "Error!!";
                                                        jsonParam.result = false;
                                                        return Json(new
                                                        {
                                                            jsonParam
                                                        }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                                if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                                {
                                                    jsonParam.message = "Sorry !  Deletion of Selected Journal Entry creates a Negative Closing Balance in Current Year for " + Row["ITEM_PROFILE"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                    jsonParam.title = "Error!!";
                                                    jsonParam.result = false;
                                                    return Json(new
                                                    {
                                                        jsonParam
                                                    }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        SetBaseSession("Jv_Info_MaxEdit_JV", Info_MaxEditedOn);
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Journal Entry";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Journal";
                        jsonParam.popup_form_path = "/Account/JournalVoucher/Frm_Voucher_Win_Journal/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn=" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Asset_Transfer)
                    {
                        int _RowHandle = 0;
                        if (Convert.ToInt32(iTR_SR_NO) == 2)
                        {
                            _RowHandle = 1;
                        }
                        DataTable d2 = BASE._AssetTransfer_DBOps.GetRecord(xTemp_MID, 1);
                        var iTR_TYPE_AssetTransfer_item = "";
                        if (d2.Rows.Count > 0)
                        {
                            iTR_TYPE_AssetTransfer_item = d2.Rows[0]["TR_TYPE"].ToString(); //credit for from entry and debit for to entry
                        }
                        if (iTR_TYPE_AssetTransfer_item == "CREDIT" && !string.IsNullOrEmpty(xCross_Ref_Id))
                        {
                            if (xCross_Ref_Id.Length > 0)
                            {
                                if (AssetTransferDeleteConfirmationTaken == false)
                                {
                                    jsonParam.message = "<b>Sure you want to <font color='red'><u>Delete</u></font> Matched Asset Transfer...?</b>";
                                    jsonParam.isconfirm = true;
                                    jsonParam.result = false;
                                    jsonParam.title = Me_Text;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                                DataTable GetAssetItemID = BASE._Voucher_DBOps.GetAssetItemID(xTemp_MID); //Get Actual Item IDs from Selected Transaction
                                int count = GetAssetItemID.Rows.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    DataRow cRow = GetAssetItemID.Rows[i];
                                    string xTemp_ItemID = cRow[0].ToString();
                                    DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); //Gets Asset Profile
                                    string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                                    string xTemp_AssetID = "";
                                    switch (xTemp_AssetProfile)
                                    {
                                        case "GOLD":
                                        case "SILVER":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, xTemp_MID);
                                            break;
                                        case "OTHER ASSETS":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                                            break;
                                        case "LIVESTOCK":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, xTemp_MID);
                                            break;
                                        case "VEHICLES":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, xTemp_MID);
                                            break;
                                        case "LAND & BUILDING":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, xTemp_MID);
                                            break;
                                        case "ADVANCES":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ADVANCES_INFO, xTemp_MID);
                                            break;
                                        case "OTHER DEPOSITS":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.DEPOSITS_INFO, xTemp_MID);
                                            break;
                                        case "OTHER LIABILITIES":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIABILITIES_INFO, xTemp_MID);
                                            break;
                                        case "WIP":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.WIP_INFO, xTemp_MID);
                                            break;
                                        case "OPENING":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.OTHER_PROFILE_INFO, xTemp_MID);
                                            break;
                                        case "FIXED DEPOSITS":
                                            xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.FD_INFO, xTemp_MID);
                                            break;
                                    }
                                    if (xTemp_AssetID.Length > 0)
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                        if (SaleRecord != null)
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry contains a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                                        if (AssetTrfRecord != null)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry contains a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID); //Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                        if (ReferenceRecord != null)
                                        {
                                            if (ReferenceRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please delete the record for Deleting this Entry.";
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            DataTable Rec = BASE._AssetTransfer_DBOps.GetRecord(xTemp_MID, 1);
                            if (Rec != null)
                            {
                                if (Rec.Rows.Count > 0)
                                {
                                    if (Convert.ToDateTime(iREC_EDIT_ON) != (DateTime)Rec.Rows[0]["REC_EDIT_ON"])
                                    {
                                        isRecChanged = true;
                                    }
                                    if (!Convert.IsDBNull(Rec.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                                    {
                                        if (Rec.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                                        {
                                            jsonParam.message = "Matched Asset Transfer Cannot Be Deleted...!<br><br>Record Has Already Been Matched In The Background";
                                            jsonParam.title = "Information...";
                                            jsonParam.result = false;
                                            jsonParam.refreshgrid = isRecChanged;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        if (isRecChanged)
                                        {
                                            jsonParam.message = "Record Has Already Been Unmatched In The Background";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            jsonParam.refreshgrid = true;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }

                        DateTime? Info_MaxEditedOn = null;
                        if (iTR_TYPE_AssetTransfer_item == "DEBIT")
                        {
                            DataTable d1 = BASE._AssetTransfer_DBOps.GetRecord(xTemp_MID, 2);
                            DateTime MaxEditOn = (DateTime)BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn((string)d1.Rows[0]["TR_REF_OTHERS"]);
                            if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                            {
                                Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                                Param.CrossRefId = (string)d1.Rows[0]["TR_REF_OTHERS"];
                                Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                                Param.SpecifiedEntryType = EntryType.Both;
                                int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count;
                                if (Adj_NextYear > 0)
                                {
                                    jsonParam.message = "Sorry! Some adjustments have already been made on this asset<br><br> Please delete those entries for Deleting this Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                Info_MaxEditedOn = MaxEditOn;
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ Asset Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Asset_Transfer";
                        jsonParam.popup_form_path = "/Account/AssetTransferVoucher/Frm_Voucher_Win_Asset_Transfer/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID1=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.WIP_Finalization)
                    {
                        string xTemp_AssetID = "";
                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, xTemp_MID);
                        if (xTemp_AssetID.Length > 0)
                        {
                            DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                            if (SaleRecord != null)
                            {
                                if (SaleRecord.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Sold On " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Deleting This Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);
                            if (AssetTrfRecord != null)
                            {
                                if (AssetTrfRecord.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Transfered On " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Deleting This Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, xTemp_MID); //Gets any Txn where Current Asset is referenced
                            if (ReferenceRecord != null)
                            {
                                if (ReferenceRecord.Rows.Count > 0)
                                {
                                    jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Referred In A Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " Of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Deleting This Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        DateTime? Info_MaxEditedOn = null;
                        DataTable GetRefRecordIDS = BASE._Voucher_DBOps.GetRefRecordIDS(xTemp_MID);
                        int count = GetRefRecordIDS.Rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            DataRow Row = GetRefRecordIDS.Rows[i];
                            if ((string)Row["ITEM_PROFILE"] == "OTHER ASSETS")
                            {
                                if (!Convert.IsDBNull(Row["TR_TRF_CROSS_REF_ID"]))
                                {
                                    DateTime MaxEditOn = (DateTime)BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(xCross_Ref_Id);
                                    if (MaxEditOn > Convert.ToDateTime(iREC_EDIT_ON))
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xCross_Ref_Id);
                                        if (SaleRecord != null)
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Sold On " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Deleting This Entry.";
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xCross_Ref_Id);
                                        if (AssetTrfRecord != null)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! The Asset Created By Current Voucher Was Transfered On " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " For Initial Payment Of Rs. " + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + ".<br><br>Please Delete The Record For Deleting This Entry.";
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new
                                                {
                                                    jsonParam
                                                }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        Parameter_GetJornalEntryAdjustments Param = new Parameter_GetJornalEntryAdjustments();
                                        Param.CrossRefId = xCross_Ref_Id;
                                        Param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                                        Param.SpecifiedEntryType = EntryType.Both;
                                        int Adj_NextYear = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(Param, ClientScreen.Accounts_Voucher_SaleOfAsset).Rows.Count;
                                        if (Adj_NextYear > 0)
                                        {
                                            jsonParam.message = "Sorry! Some Adjustments Have Already Been Made On This Asset<br><br> Please Delete Those Entries For Deleting This Entry.";
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new
                                            {
                                                jsonParam
                                            }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        Info_MaxEditedOn = MaxEditOn;
                                    }
                                }
                            }
                        }
                        jsonParam.result = true;
                        jsonParam.popup_title = "Delete ~ WIP Finalization";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_WIP_Finalization";
                        jsonParam.popup_form_path = "/Account/WIPFinalization/Frm_Voucher_Win_WIP_Finalization/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._Delete + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&Info_MaxEditedOn=" + Info_MaxEditedOn + "&GridToRefresh=" + GridToRefresh;

                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region View
        /// <summary>
        /// Voucher View call when only Txn Entry Record ID is available on Screen
        /// </summary>
        /// <param name="Txn_Rec_ID"></param>  
        /// <param name="Me_Text"></param>
        /// <param name="Tr_Date"></param>
        /// <param name="GridToRefresh"></param>         
        [ActionName("Vouchers_View1")]
        public ActionResult Vouchers_View(string Txn_Rec_ID, string Me_Text, string Tr_Date = null, string GridToRefresh = "")
        {
            if (Txn_Rec_ID != "NOTE-BOOK")
            {
                DataTable TxnTable = BASE._Voucher_DBOps.GetEntryDetails(Txn_Rec_ID);
                return Vouchers_View(Txn_Rec_ID, TxnTable.Rows[0]["TR_M_ID"].ToString(), TxnTable.Rows[0]["REC_EDIT_ON"].ToString(), Me_Text, TxnTable.Rows[0]["TR_DATE"].ToString(), TxnTable.Rows[0]["TR_CODE"].ToString(), GridToRefresh, null);
            }
            else
            {
                object RefVar = new object();
                if (string.IsNullOrWhiteSpace(Tr_Date))
                {
                    Tr_Date = DateTime.MinValue.ToString();
                }
                return Vouchers_View(Txn_Rec_ID, RefVar.ToString(), RefVar.ToString(), Me_Text, Tr_Date, RefVar.ToString(), GridToRefresh, null);
            }
        }
        /// <summary>
        /// Voucher View call when only Txn Entry Record Master ID and created Profile REC_ID are available on Screen
        /// </summary>
        /// <param name="Txn_Rec_M_ID"></param>
        /// <param name="Profile_Rec_ID"></param>
        /// <param name="ProfileUsed"></param>   
        /// <param name="Me_Text"></param>
        /// <param name="GridToRefresh"></param>  
        [ActionName("Vouchers_View2")]
        public ActionResult Vouchers_View(string Txn_Rec_M_ID, string Profile_Rec_ID, string ProfileUsed, string Me_Text, string GridToRefresh)
        {
            Common_Lib.RealTimeService.AssetProfiles profiles = (Common_Lib.RealTimeService.AssetProfiles)Enum.Parse(typeof(Common_Lib.RealTimeService.AssetProfiles), ProfileUsed);
            DataTable TxnTable = BASE._Voucher_DBOps.GetProfileEntryDetails(Profile_Rec_ID, Txn_Rec_M_ID, profiles);
            return Vouchers_View(TxnTable.Rows[0]["TXN_REC_ID"].ToString(), Txn_Rec_M_ID, TxnTable.Rows[0]["REC_EDIT_ON"].ToString(),
                  Me_Text, TxnTable.Rows[0]["TR_DATE"].ToString(), TxnTable.Rows[0]["TR_CODE"].ToString(), GridToRefresh, null);
        }
        /// <summary>
        /// Voucher View call when all Txn Entry Record Data is available on Screen
        /// </summary>
        /// <param name="xTemp_ID"></param>
        /// <param name="xTemp_MID"></param>
        /// <param name="iREC_EDIT_ON"></param>
        /// <param name="Me_Text"></param>
        /// <param name="iTR_DATE"></param>
        /// <param name="iTR_CODE"></param>
        /// <param name="GridToRefresh"></param>   
        [ActionName("Vouchers_View3")]
        public ActionResult Vouchers_View(string xTemp_ID, string xTemp_MID, string iREC_EDIT_ON, string Me_Text, string iTR_DATE, string iTR_CODE, string GridToRefresh, string RowKey)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (!string.IsNullOrWhiteSpace(RowKey))
                {
                    if (GridToRefresh == "CashBookListGrid")
                    {
                        //var CashbookData = (List<CB_Grid_Model>)GetBaseSession("MainGrid_Data_CB");
                        //if (CashbookData != null && CashbookData.Count > 0)
                        //{
                        //    var CashBookRowData = CashbookData.Where(x => x.Grid_PK == RowKey).FirstOrDefault();
                        //    xTemp_ID = CashBookRowData.iREC_ID;
                        //    xTemp_MID = CashBookRowData.iTR_M_ID;
                        //    iREC_EDIT_ON = CashBookRowData.iREC_EDIT_ON.ToString();
                        //    Me_Text = "Cash Book";
                        //    iTR_DATE = CashBookRowData.iTR_DATE.ToString();
                        //    iTR_CODE = CashBookRowData.iTR_CODE.ToString();
                        //}
                        //else 
                        //{
                            Me_Text = "Cash Book";
                            iREC_EDIT_ON = string.IsNullOrWhiteSpace(iREC_EDIT_ON) ? iREC_EDIT_ON : Convert.ToDateTime(iREC_EDIT_ON).ToString();
                            iTR_DATE = string.IsNullOrWhiteSpace(iTR_DATE) ? iTR_DATE : Convert.ToDateTime(iTR_DATE).ToString();
                       //}
                    }
                    else if (GridToRefresh == "ITR_PostedGrid")
                    {
                        var GridData = (List<Return_InternalTransferRegister_Posted>)GetBaseSession("ITR_GridView1_ITReg");
                        if (GridData != null && GridData.Count > 0)
                        {
                            var RowData = GridData.Where(x => x.ID == RowKey).FirstOrDefault();
                            xTemp_ID = RowData.ID;
                            xTemp_MID = RowData.MID;
                            iREC_EDIT_ON = RowData.EditDate.ToString();
                            Me_Text = "Internal Transfer Register";
                            iTR_DATE = RowData.xDate.ToString();
                            iTR_CODE = "8";
                        }
                    }
                }
                string xMaster_ID = "";
                if (!string.IsNullOrWhiteSpace(xTemp_MID))
                {
                    xMaster_ID = xTemp_MID;
                }
                else
                {
                    xMaster_ID = xTemp_ID;
                }
                if (xTemp_ID == "OPENING BALANCE")
                {
                    jsonParam.result = false;
                    jsonParam.message = "Opening Balance Cannot View from Voucher Entry...!<br><br>Note: Use Profile - Cash A/c. Information for CASH<br>or Bank A/c. Information for BANK";
                    jsonParam.title = "Information...";
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (xTemp_ID == "NOTE-BOOK")
                {
                    jsonParam.result = true;
                    jsonParam.popup_title = "Note-Book Entry (" + Me_Text + ")";
                    jsonParam.popup_form_name = "Frm_NoteBook_Info";
                    jsonParam.popup_form_path = "/Account/NoteBook/Frm_NoteBook_Info/";
                    jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xEntryDate=" + iTR_DATE;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!string.IsNullOrWhiteSpace(xTemp_ID))
                {
                    bool Entry_Found = false;
                    DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
                    if (Status == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Status.Rows.Count > 0)
                    {
                        Entry_Found = true;
                    }
                    if (Entry_Found == false)
                    {
                        jsonParam.message = "Entry Not Found Or Changed In Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    int xVCode = Convert.ToInt32(iTR_CODE);
                    if (xVCode == (int)Common.Voucher_Screen_Code.Cash_Deposit_Withdrawn)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Cash Deposit / Withdrawn";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Cash";
                        jsonParam.popup_form_path = "/Account/CashDepositAndWithdrawnVoucher/Frm_Voucher_Win_Cash/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Bank_To_Bank_Transfer)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Bank to Bank Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_B2B";
                        jsonParam.popup_form_path = "/Account/BankToBankTransferVoucher/Frm_Voucher_Win_B2B/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Regular)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Donation";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Donation_R";
                        jsonParam.popup_form_path = "/Account/DonationVoucher/Frm_Voucher_Win_Donation_R/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Foreign)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Foreign Donation";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Donation_F";
                        jsonParam.popup_form_path = "/Account/DonationVoucher/Frm_Voucher_Win_Donation_F/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Collection_Box)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Collection Box";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_C_Box";
                        jsonParam.popup_form_path = "/Account/CollectionBoxVoucher/Frm_Voucher_Win_C_Box/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Payment)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Payment";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gen_Pay";
                        jsonParam.popup_form_path = "/Account/PaymentVoucher/Frm_Voucher_Win_Gen_Pay/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Receipt)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Receipt";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gen_Rec";
                        jsonParam.popup_form_path = "/Account/ReceiptVoucher/Frm_Voucher_Win_Gen_Rec/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Donation_Gift)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Donation in Kind";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Gift";
                        jsonParam.popup_form_path = "/Account/DonationInKindVoucher/Frm_Voucher_Win_Gift/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Internal_Transfer)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Internal Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_I_Transfer";
                        jsonParam.popup_form_path = "/Account/InternalTransfer/Frm_Voucher_Win_I_Transfer/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID_1=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Fixed_Deposits)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ FD related Income / Expenses";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_FD";
                        jsonParam.popup_form_path = "/Account/FDVoucher/Frm_Voucher_Win_FD/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Sale_Asset)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Sale Of Asset";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Sale_Asset";
                        jsonParam.popup_form_path = "/Account/SaleAsset/Frm_Voucher_Win_Sale_Asset/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Membership";
                        jsonParam.popup_form_name = "Frm_Voucher_Membership";
                        jsonParam.popup_form_path = "/Membership/MembershipReceiptRegister/Frm_Voucher_Membership/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership_Renewal)
                    {
                        //jsonParam.result = true;
                        //jsonParam.popup_title = "View ~ Membership Renewal";
                        //jsonParam.popup_form_name = "Frm_Voucher_Membership_Renewal";
                        //jsonParam.popup_form_path = "";
                        //jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        jsonParam.result = false;
                        jsonParam.message = "View of Membership Renewal is not allowed";
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Membership_Conversion)
                    {
                        //jsonParam.result = true;
                        //jsonParam.popup_title = "View ~ Membership Conversion";
                        //jsonParam.popup_form_name = "Frm_Voucher_Membership_Conversion";
                        //jsonParam.popup_form_path = "";
                        //jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        jsonParam.result = false;
                        jsonParam.message = "View of Membership Conversion is not allowed";
                        jsonParam.title = "Error!!";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Journal)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Journal Entry";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Journal";
                        jsonParam.popup_form_path = "/Account/JournalVoucher/Frm_Voucher_Win_Journal/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.Asset_Transfer)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ Asset Transfer";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_Asset_Transfer";
                        jsonParam.popup_form_path = "/Account/AssetTransferVoucher/Frm_Voucher_Win_Asset_Transfer/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID1=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (xVCode == (int)Common.Voucher_Screen_Code.WIP_Finalization)
                    {
                        jsonParam.result = true;
                        jsonParam.popup_title = "View ~ WIP Finalization";
                        jsonParam.popup_form_name = "Frm_Voucher_Win_WIP_Finalization";
                        jsonParam.popup_form_path = "/Account/WIPFinalization/Frm_Voucher_Win_WIP_Finalization/";
                        jsonParam.popup_querystring = "Tag=" + Common.Navigation_Mode._View + "&xID=" + xTemp_ID + "&xMID=" + xMaster_ID + "&Info_LastEditedOn=" + iREC_EDIT_ON + "&GridToRefresh=" + GridToRefresh;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Edit-Common
        [HttpGet]
        public ActionResult Frm_Voucher_Updates(string xID, string xMID, string GridToRefresh,string TRCode=null)
        {
            Voucher_Updates model = new Voucher_Updates();
            xMID = xMID ?? "";
            model.xID = xID;
            model.xMID = xMID;
            model.GridToRefresh = GridToRefresh;
            Refresh_GetPurList();
            model.SpecialVoucherReferenceList_VoucherUpdate = BASE._Voucher_DBOps.GetSplVoucherRefsList(CommonFunctions.GetClientScreenFromTRCode(TRCode), Common.Navigation_Mode._Edit);
            model.SpecialVoucherReferenceList_Count = model.SpecialVoucherReferenceList_VoucherUpdate.Count;
            var voucherRef = "";
            if (xMID.Length > 10)
            {
                VoucherUpdateData = BASE._Voucher_DBOps.GetVoucherItemDetails(xMID);
            }
            else
            {
                VoucherUpdateData = BASE._Voucher_DBOps.GetVoucherItemDetails(xID);
            }
            if (VoucherUpdateData.Rows.Count > 0)
            {
                model.Txt_Narration_VoucherUpdate = VoucherUpdateData.Rows[0]["TR_NARRATION"].ToString();
                model.Txt_Reference_VoucherUpdate = VoucherUpdateData.Rows[0]["TR_REFERENCE"].ToString();
                voucherRef = VoucherUpdateData.Rows[0]["Voucher Ref"].ToString();
            }
            if (string.IsNullOrWhiteSpace(voucherRef) == false)
            {
                model.ChosenSpecialVoucherReference_VoucherUpdate = voucherRef;
            }
            else 
            {
                model.ChosenSpecialVoucherReference_VoucherUpdate = "";
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Updates(Voucher_Updates model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                var Inparm = new Param_UpdateCommonDetails_Txn();
                Inparm.ID = model.xID;
                Inparm.TR_M_ID = model.xMID;
                Inparm.Narration = model.Txt_Narration_VoucherUpdate ?? "";
                Inparm.Reference = model.Txt_Reference_VoucherUpdate ?? "";
                Inparm.Purpose_ID = model.GLookUp_Purpose_VoucherUpdate ?? "";
                //FCRA Insert Process
                if (model.ChosenSpecialVoucherReference_VoucherUpdate != null)
                {
                    var SplVchrRefsSplit = model.ChosenSpecialVoucherReference_VoucherUpdate.Split('|');
                    var splitLength = SplVchrRefsSplit.Length;
                    if (splitLength > 0)
                    {
                        Parameter_InsertSplVchrRef_Vouchers[] InsertSplVchrRefs = new Parameter_InsertSplVchrRef_Vouchers[splitLength];
                        for (int j = 0; j < splitLength; j++)
                        {
                            Parameter_InsertSplVchrRef_Vouchers _SplVchr = new Parameter_InsertSplVchrRef_Vouchers();
                            _SplVchr.Task_Name = SplVchrRefsSplit[j];
                            InsertSplVchrRefs[j] = _SplVchr;
                        }
                        Inparm.InsertSplVchrRefs = InsertSplVchrRefs;
                    }
                }
                if (BASE._Voucher_DBOps.UpdateCommonDetails(Inparm))
                {
                    jsonParam.message = Messages.UpdateSuccess;
                    jsonParam.title = "Update Common Voucher Details";
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Update Common Voucher Details";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", e.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error..";
                jsonParam.result = false;
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_Voucher_Updates_Grid()
        {
            return View(VoucherUpdateData);
        }
        public void Refresh_GetPurList()
        {
            PurPoseList = BASE._Payment_DBOps.GetProjects("PUR_NAME", "PUR_ID");
        }
        public ActionResult LookUp_GetPurList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (PurPoseList == null || DDRefresh == true)
            {
                Refresh_GetPurList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PurPoseList, loadOptions)), "application/json");
        }
        public void sessionClear_voucherUpdate()
        {
            ClearBaseSession("_CommonCallWindow");
        }
        #endregion
        #region Misc 
        public bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public bool Get_Closed_Bank_Status(string xRecID, ref string _Closed_Bank_Account_No)
        {
            bool Flag = false;
            string CR_LED_ID = "";
            string DR_LED_ID = "";
            string xTR_MODE = "";
            int xTR_CODE = 0;

            DataTable d4 = BASE._Voucher_DBOps.GetTransactionDetail(xRecID);
            if (d4.Rows.Count > 0)
            {
                for (int i = 0; i < d4.Rows.Count; i++)
                {
                    DataRow xRow = d4.Rows[i];
                    if (!Convert.IsDBNull(xRow["TR_SUB_CR_LED_ID"]))
                    {
                        CR_LED_ID = xRow["TR_SUB_CR_LED_ID"].ToString();
                    }
                    else
                    {
                        CR_LED_ID = "";
                    }
                    if (!Convert.IsDBNull(xRow["TR_SUB_DR_LED_ID"]))
                    {
                        DR_LED_ID = xRow["TR_SUB_DR_LED_ID"].ToString();
                    }
                    else
                    {
                        DR_LED_ID = "";
                    }
                    if (!Convert.IsDBNull(xRow["TR_CODE"]))
                    {
                        xTR_CODE = Convert.ToInt32(xRow["TR_CODE"]);
                    }
                    else
                    {
                        xTR_CODE = 0;
                    }
                    if (!Convert.IsDBNull(xRow["TR_MODE"]))
                    {
                        xTR_MODE = xRow["TR_MODE"].ToString();
                    }
                    else
                    {
                        xTR_MODE = "";
                    }
                    if (xTR_CODE == 6 || xTR_CODE == 1 || xTR_MODE.ToUpper() != "CASH")
                    {
                        object MaxValue = null;
                        MaxValue = BASE._Voucher_DBOps.GetBankAccount(CR_LED_ID, DR_LED_ID).ToString();
                        if (Convert.IsDBNull(MaxValue) || string.IsNullOrEmpty((string)MaxValue))
                        {
                            Flag = false;
                            _Closed_Bank_Account_No = "";
                        }
                        else
                        {
                            Flag = true;
                            _Closed_Bank_Account_No = (string)MaxValue;
                            break;
                        }
                    }
                }
            }
            return Flag;
        }
        #endregion
    }
}