using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib;
using Common_Lib.RealTimeService;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Controllers;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using ConnectOneMVC.Models;
using System.Windows.Forms;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class JournalVoucherController : BaseController
    {
        // GET: Account/JournalVoucher
        public byte[] JV_Asset_Image
        {
            get { return (byte[])GetBaseSession("Payment_Asset_Image_Payment"); }
            set { SetBaseSession("Payment_Asset_Image_Payment", value); }
        }
        public DataTable DT_JV
        {
            get { return (DataTable)GetBaseSession("DT_JV"); }
            set { SetBaseSession("DT_JV", value); }
        }
        public DataTable LB_DOCS_ARRAY
        {
            get { return (DataTable)GetBaseSession("LB_DOCS_ARRAY_JV"); }
            set { SetBaseSession("LB_DOCS_ARRAY_JV", value); }
        }
        public DataTable LB_EXTENDED_PROPERTY_TABLE
        {
            get { return (DataTable)GetBaseSession("LB_EXTENDED_PROPERTY_TABLE_JV"); }
            set { SetBaseSession("LB_EXTENDED_PROPERTY_TABLE_JV", value); }
        }
        public double? Txt_CrTotal
        {
            get { return (double?)GetBaseSession("Txt_CrTotal_JV"); }
            set { SetBaseSession("Txt_CrTotal_JV", value); }
        }
        public double? Txt_DrTotal
        {
            get { return (double?)GetBaseSession("Txt_DrTotal_JV"); }
            set { SetBaseSession("Txt_DrTotal_JV", value); }
        }
        public double? Txt_DiffAmt
        {
            get { return (double?)GetBaseSession("Txt_DiffAmt_JV"); }
            set { SetBaseSession("Txt_DiffAmt_JV", value); }
        }
        public List<ItemList_JV_Itm> GetItemList_Itm
        {
            get { return (List<ItemList_JV_Itm>)GetBaseSession("GetItemList_Itm_JV"); }
            set { SetBaseSession("GetItemList_Itm_JV", value); }
        }
        public List<PurList_JV_Itm> GetPurList_Itm
        {
            get { return (List<PurList_JV_Itm>)GetBaseSession("GetPurList_Itm_JV"); }
            set { SetBaseSession("GetPurList_Itm_JV", value); }
        }
        public List<RefList_JV_Ref> GetRefList_JV_Ref
        {
            get { return (List<RefList_JV_Ref>)GetBaseSession("GetRef_List_JV_Ref"); }
            set { SetBaseSession("GetRef_List_JV_Ref", value); }
        }
        public DataTable RefData_Jv_Ref
        {
            get { return (DataTable)GetBaseSession("RefData_JV"); }
            set { SetBaseSession("RefData_JV", value); }
        }
        public List<JournalPartyList> Jv_Party_DD_Data
        {
            get { return (List<JournalPartyList>)GetBaseSession("Jv_Party_DD_Data_JV"); }
            set { SetBaseSession("Jv_Party_DD_Data_JV", value); }
        }
        public IDictionary<string, DateTime> Info_MaxEdit
        {
            get { return (IDictionary<string, DateTime>)GetBaseSession("Jv_Info_MaxEdit_JV"); }
            set { SetBaseSession("Jv_Info_MaxEdit_JV", value); }
        }
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Journal(string Tag = "", string xID = "", string xMID = "", string Info_LastEditedOn = "",
                                                    string iSpecific_ItemID = "", string GridToRefresh = "CashBookListGrid")
        {
            JournalVoucher model = new JournalVoucher();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);

            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            Common.Navigation_Mode[] AM = {Common.Navigation_Mode._New, Common.Navigation_Mode._New_From_Selection,
                                           Common.Navigation_Mode._Edit, Common.Navigation_Mode._View,
                                           Common.Navigation_Mode._Delete};

            //This loop is for user athentication check. Please do not delete.
            for (int i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal, Rights[i]) && model.Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");
                }
            }
            if (Info_MaxEdit == null) 
            {
                Info_MaxEdit = new Dictionary<string, DateTime>();
            }
            ViewBag.GridToRefresh = GridToRefresh;
            model.ActionMethod = model.Tag.ToString();
            model.xID = xID;
            model.xMID = xMID;
            model.TitleX = "Journal Entry";

            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_Jv = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_Jv = model.SpecialReferenceList_Data_Jv.Count();

            SetGridData_Jv();
            model.itemGrid_RowCount_Jv = DT_JV.Rows.Count;
            model.Txt_CrTotal_Jv = 0;
            model.Txt_DrTotal_Jv = 0;
            model.Txt_DiffAmt_Jv = 0;
            if (model.Tag == Common.Navigation_Mode._New_From_Selection || model.Tag == Common.Navigation_Mode._New)
            {
                model.Me_Text = "New ~" + model.TitleX;
                model.Txt_V_NO_Jv = "";
            }
            else if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._View || model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Me_Text = "Edit ~" + model.TitleX;
                if (string.IsNullOrWhiteSpace(Info_LastEditedOn) == false)
                {
                    model.Info_LastEditedOn_Jv = Convert.ToDateTime(Info_LastEditedOn);
                }

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_Jv = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                ContentResult return_DataBinding_JV = Data_Binding_Jv(ref model, GridToRefresh);

                if (return_DataBinding_JV != null)
                {
                    return return_DataBinding_JV;
                }

            }
            if (model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Me_Text = "Delete ~" + model.TitleX;
            }
            if (model.Tag == Common.Navigation_Mode._View)
            {
                model.Me_Text = "View ~" + model.TitleX;
            }

            return View(model);
        }

        #region Post action of main view
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Journal(JournalVoucher model)
        {
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            string Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
            if (model.Tag == Common.Navigation_Mode._Delete)
            {
                Status_Action = ((int)Common_Lib.Common.Record_Status._Deleted).ToString();
            }

            try
            {
                bool isProfileItem = false;
                bool isInternalAccountTransferLedgerItem = false;
                for (int i = 1; i <= DT_JV.Rows.Count; i++)
                {
                    var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + i);
                    if (GridRow_ToEdit[0]["Item_Profile"].ToString() != "NOT APPLICABLE" && GridRow_ToEdit[0]["Item_Profile"].ToString() != "ADVANCES" && GridRow_ToEdit[0]["Item_Profile"].ToString() != "OTHER LIABILITIES" && GridRow_ToEdit[0]["Item_Profile"].ToString() != "OTHER DEPOSITS" && GridRow_ToEdit[0]["Item_Profile"].ToString() != "OPENING" && GridRow_ToEdit[0]["Item_Profile"].ToString() != "FIXED DEPOSITS" && GridRow_ToEdit[0]["Item_Profile"].ToString() != "WIP" && GridRow_ToEdit[0]["Item_Profile"].ToString() != "TELEPHONE BILL" && isProfileItem == false)
                    {
                        isProfileItem = true;
                        
                    }
                    if (GridRow_ToEdit[0]["Item_Led_ID"].ToString() == "00110") // "Internal Account Transfer" Ledger ID.
                    {
                        isInternalAccountTransferLedgerItem = true;

                    }
                    if(isProfileItem == true && isInternalAccountTransferLedgerItem == true)
                    {
                        break;
                    }
                }
                if (isProfileItem == true && isInternalAccountTransferLedgerItem == true)
                {
                    jsonParam.message = "Sorry ! Asset transfer is not allowed via Journal Voucher ..<br> Please use Asset Transfer Voucher for same.";
                    jsonParam.result = false;
                    jsonParam.title = "Information ...";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }

                for (int i = 1; i <= DT_JV.Rows.Count; i++)
                {
                    var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + i);
                    if (GridRow_ToEdit[0]["Item_Profile"].ToString() == "LAND & BUILDING" ||
                        GridRow_ToEdit[0]["Item_Profile"].ToString() == "OTHER ASSETS" ||
                        (GridRow_ToEdit[0]["Item_Voucher_Type"].ToString() == "LAND & BUILDING" &&
                        GridRow_ToEdit[0]["Item_Profile"].ToString() != "LAND & BUILDING"))
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Insurance Related Assests Value Cannot be Added/Editeed After The Completion of Insurance Audit";
                            jsonParam.result = false;
                            jsonParam.title = "Information...";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (GridRow_ToEdit[0]["Item_Voucher_Type"].ToString().Trim().ToUpper() == "LAND & BUILDING" &&
                        GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() != "LAND & BUILDING")
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Property Related Expenses Cannot be Added/Edited After The Completion of Insurance Audit";
                            jsonParam.result = false;
                            jsonParam.title = "Information...";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (GridRow_ToEdit[0]["WIP_REF_TYPE"] != null && Convert.IsDBNull(GridRow_ToEdit[0]["WIP_REF_TYPE"]))
                    {
                        if (GridRow_ToEdit[0]["WIP_REF_TYPE"].ToString() == "EXISTING")
                        {
                            var WIP_ID = Convert.IsDBNull(GridRow_ToEdit[0]["REF_REC_ID"]) ? null : GridRow_ToEdit[0]["REF_REC_ID"].ToString();
                            DataTable creationStats = BASE._WIPCretionVouchers.GetRefCreationDateByWIPID(WIP_ID);
                            if (creationStats != null)
                            {
                                if (creationStats.Rows.Count > 0)
                                {
                                    if (Convert.ToDateTime(creationStats.Rows[0]["TR_DATE"]) > model.Txt_V_Date_Jv)
                                    {
                                        jsonParam.message = "Referencing Voucher Date must be reater than creation Date(" +
                                            Convert.ToDateTime(creationStats.Rows[0]["TR_DATE"]).ToString(BASE._Date_Format_DD_MMM_YYYY) +
                                            ") of WIP namely " + creationStats.Rows[0]["WIP_REF"].ToString() + ". . . !";
                                        jsonParam.result = false;
                                        jsonParam.title = "Incomplete Information. . .";
                                        jsonParam.focusid = "Win_Journal_Item_Grid";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                }

                if (model.Tag != Common.Navigation_Mode._Delete)
                {
                    foreach (DataRow _Row in DT_JV.Rows)
                    {
                        if (Convert.IsDBNull(_Row["PartyID"]))
                        {
                            _Row["PartyID"] = "";
                        }
                        _Row["PartyID"].ToString().Trim();
                    }
                    Boolean TDS_Present = false;
                    int PartyCount = DT_JV.Rows.Count ;

                    for (int i = 1; i <= DT_JV.Rows.Count; i++)
                    {
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + i);
                        if (GridRow_ToEdit[0]["Party"].ToString() == "")
                        {
                            PartyCount = PartyCount - 1;
                        }
                        if (GridRow_ToEdit[0]["Party"].ToString().ToUpper().Contains("TDS"))
                        {
                            TDS_Present = true;
                        }
                    }
                    if(model.isconfirmPayCntTDS_Jv == false) { 
                        if (PartyCount == 0 && TDS_Present == true)
                        {
                            jsonParam.message = "<size=13><b>This Voucher contains TDS item(s) but no Party has been selected. </b></size>" + "<br>" + "<br>" + "Do you still want to Save Voucher...?";
                            jsonParam.result = false;
                            jsonParam.title = "Message...";
                            jsonParam.isconfirm = true;
                            jsonParam.focusid = "partycount";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                //-----------------------------+
                //Start : Check if entry already changed 
                //-----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable Journal_DbOps = BASE._Journal_voucher_DBOps.GetRecords(model.xMID);
                        if (Journal_DbOps == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Journal_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Journal Voucher");
                            jsonParam.result = false;
                            jsonParam.title = "Record Already Changed!";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn_Jv != Convert.ToDateTime(Journal_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Journal Voucher");
                            jsonParam.result = false;
                            jsonParam.title = "Record Already Changed!";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        object MaxValue = 0;
                        MaxValue = BASE._Payment_DBOps.GetStatusByMID(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found. . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Information...";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted. . . !" + "<br>" + "<br>" + "Note:" + "<br>" + "-------" + "<br>" + "Drop your Request to Madhuban for Unlock this Entry," + "<br>" + "If you really want to do some action...!";
                            jsonParam.result = false;
                            jsonParam.title = "Information...";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        DataTable AssetItems = BASE._Voucher_DBOps.GetAssetItemID(model.xMID);
                        if (AssetItems == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        foreach (DataRow cRow in AssetItems.Rows) //Get Actual Item IDs from Selected Transaction
                        {
                            string xTemp_ItemID = cRow[0].ToString();
                            DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); // Gets Asset Profile
                            string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                            if (xTemp_AssetProfile.ToUpper() != "NOT APPLICABLE") //Leaving Construciton Items
                            {
                                string xTemp_AssetID = "";
                                switch (xTemp_AssetProfile) //Get Asset RecID from Particular Table 
                                {
                                    case "GOLD":
                                    case "SILVER":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, model.xMID);
                                        break;
                                    case "OTHER ASSETS":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, model.xMID);
                                        break;
                                    case "LIVESTOCK":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, model.xMID);
                                        break;
                                    case "VEHICLES":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, model.xMID);
                                        break;
                                    case "LAND & BUILDING":
                                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, model.xMID);
                                        break;
                                }
                                if (xTemp_AssetID.Length > 0)
                                {
                                    DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                    if (SaleRecord != null)
                                    {
                                        if (SaleRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was sold on " +
                                                Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() +
                                                " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" +
                                                " Please delete the record for " + (model.Tag == Common.Navigation_Mode._Edit ? "editing" : "deleting") +
                                                " this Entry.";
                                            jsonParam.result = false;
                                            jsonParam.title = "Error!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    //Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, model.xMID);
                                    if (ReferenceRecord != null)
                                    {
                                        if (ReferenceRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " +
                                                Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." +
                                                ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" +
                                                " Please delete the record for " + (model.Tag == Common.Navigation_Mode._Edit ? "editing" : "deleting") +
                                                " this Entry.";
                                            jsonParam.result = false;
                                            jsonParam.title = "Error!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Convert.ToInt32(null), xTemp_AssetID);
                                    if (AssetTrfRecord.Rows.Count > 0)
                                    {
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was Transfered on " +
                                                Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() +
                                                " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" +
                                                " Please delete the record for " + (model.Tag == Common.Navigation_Mode._Edit ? "editing" : "deleting") +
                                                "this Entry.";
                                            jsonParam.result = false;
                                            jsonParam.title = "Error!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }

                        //Special Checks
                        object UseCount = 0;
                        string RefId = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(model.xMID);
                        if (RefId == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._AdvanceDBOps.GetAdvancePaymentCount(RefId);
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! Advance Created by Current journal entry is used in some other entry. . . !" + "<br>" +
                                    "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = model.xMID;
                            param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = (BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)).Rows.Count;
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! Advance Created by Current journal entry is used in some other entry. . . !" + "<br>" +
                                    "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }

                            //DataTable AdvAssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, RefId);
                            //if (AdvAssetTrfRecord != null)
                            //{
                            //    if (AdvAssetTrfRecord.Rows.Count > 0)
                            //    {
                            //        jsonParam.message = "Sorry! Advance Created by Current journal entry has been transferred . . . !" + "<br>" +
                            //        "<br>" + "Please delete that transfer to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                            //        "this entry.";
                            //        jsonParam.title = "Error!!";
                            //        jsonParam.result = false;
                            //        return Json(new
                            //        {
                            //            jsonParam
                            //        }, JsonRequestBehavior.AllowGet);
                            //    }
                            //}
                        }
                        RefId = BASE._Voucher_DBOps.GetRaisedDepositRecID(model.xMID);
                        if (RefId == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._DepositsDBOps.GetTransactionCount(RefId);
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! deposit Created by Current journal entry is used in some other entry. . . !" + "<br>" +
                                    "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = model.xMID;
                            param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count;
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! deposit Created by Current journal entry is used in some other entry. . . !" + "<br>" +
                                    "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        RefId = BASE._Voucher_DBOps.GetRaisedLiabilityRecID(model.xMID);
                        if (RefId == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (RefId.Length > 0)
                        {
                            UseCount = BASE._LiabilityDBOps.GetTransactionCount(RefId);
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! Liability created by Current Journal Entry is used in some other entry. . . !" +
                                    "<br>" + "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments param = new Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments();
                            param.CrossRefId = RefId;
                            param.Excluded_Rec_M_ID = model.xMID;
                            param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both;
                            param.NextUnauditedYearID = BASE._next_Unaudited_YearID;
                            UseCount = BASE._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count;
                            if (Convert.ToInt32(UseCount) > 0)
                            {
                                jsonParam.message = "Sorry! Liability created by current journal entry is used in some other entry. . . !" +
                                    "<br>" + "<br>" + "Please delete that dependency to" + (model.Tag == Common.Navigation_Mode._Edit ? "edit" : "delete") +
                                    "this entry.";
                                jsonParam.result = false;
                                jsonParam.title = "Error!!";
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }//Allow Multiuser

                //End : Check if entry already changed 
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New ||
                model.Tag == Common_Lib.Common.Navigation_Mode._Edit ||
                model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                {
                    if (DT_JV.Rows.Count == 0)
                    {
                        jsonParam.message = "Please insert items . . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Error";
                        jsonParam.focusid = "Win_Journal_Item_Grid";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_Jv.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank . . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . . !";
                        jsonParam.focusid = "Txt_V_Date_Jv";
                        jsonParam.refreshgrid = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_Jv.ToString()) == true)
                    {
                        if (model.Txt_V_Date_Jv < BASE._open_Year_Sdt || model.Txt_V_Date_Jv > BASE._open_Year_Edt)
                        {
                            jsonParam.message = "Date not as per Financial Year. . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_V_Date_Jv";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    //Check TDS postings 
                    Boolean TDS_Led_Credits = false;
                    Boolean TDS_Debits = false;
                    Boolean Credits = false;
                    Boolean debits = false;
                    for (int i = 1; i <= DT_JV.Rows.Count; i++)
                    {
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + i);
                        if (GridRow_ToEdit[0]["Item_Led_ID"].ToString() == "00075" &&
                            GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "CREDIT")
                        {
                            TDS_Led_Credits = true;
                        }
                        if (BASE._Journal_voucher_DBOps.IsTDSApplicable(GridRow_ToEdit[0]["Item_ID"].ToString()) == true &&
                            (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "DEBIT"))
                        {
                            TDS_Debits = true;
                        }
                        if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "CREDIT")
                        {
                            Credits = true;
                        }
                        if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "DEBIT")
                        {
                            debits = true;
                        }
                    }
                    if(model.isconfirmTDS_Jv == false) 
                    { 
                        if (TDS_Debits == true && TDS_Led_Credits == false)
                        {
                            jsonParam.message = "<u><b style='color:maroon; font-size:24px'>Items involving TDS are debited. But No TDS <br class='hidden-xs'> Entry is found.!</color></b></u><size=10>" +
                                "<br><br><b style='color:red'>Do you still want to save... ?</color></b></size>";
                            jsonParam.result = false;
                            jsonParam.title = model.Me_Text;
                            jsonParam.isconfirm = true;
                            jsonParam.focusid = "tds";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            //DialogResult dialogResult = MessageBox.Show("<u><b><color=maroon>Items involving TDS are debited. But No TDS Entry is found.!</color></b></u><size=10>" +
                            //    "<br>" + "<br>" + "<b><color=red>Do you still want to save... ?</color></b></size>", model.Me_Text, MessageBoxButtons.YesNo);
                        
                            //if (dialogResult == DialogResult.Yes)
                            //{
                            
                            //}
                            //else if (dialogResult == DialogResult.No)
                            //{
                            //    jsonParam.result = false;
                            //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            //}
                        }
                    }
                    if (debits == false || Credits == false)
                    {
                        jsonParam.message = "J V    m u s t   h a v e   b o t h   C r e d i t   a n d   D e b i t   P o s t i n g s . . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information. . .";
                        jsonParam.focusid = "Txt_DiffAmt";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                { // Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit ' Removed this check on updation as we dont recreate location on updation of property creation voucher
                    DataTable d1 = BASE._L_B_DBOps.GetIDsBytxnID(model.xMID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal);
                    foreach (DataRow cRow in d1.Rows)
                    {
                        string Msg = FindLocationUsage(model.Tag, cRow[0].ToString(), false); //sold/tf assets not excluded
                        if (Msg.Length > 0)
                        {
                            jsonParam.message = Msg;
                            jsonParam.result = false;
                            jsonParam.title = model.Me_Text;
                            jsonParam.focusid = "Win_Journal_Item_Grid";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection ||
                        model.Tag == Common.Navigation_Mode._Edit)
                    {
                        //Checks for deletion of asset location in background in all assets
                        foreach (DataRow XRow in DT_JV.Rows)
                        {
                            Object cnt;
                            var Loc_ID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            if (Loc_ID != null)
                            {
                                if (Loc_ID.ToString().Length > 0)
                                {
                                    Object PropertyId = BASE._AssetLocDBOps.GetPropertyID(Loc_ID);
                                    if (PropertyId != null && !(Convert.IsDBNull(PropertyId)))
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(PropertyId.ToString()); //checks if the referred property for constt items has been sold 
                                        if (SaleRecord != null)
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a property which was sold on " +
                                                    Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() +
                                                    " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." +
                                                    "<br>" + "<br>" + " Please delete the record for completing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.refreshgrid = true;
                                                jsonParam.closeform = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Convert.ToInt32(null), PropertyId.ToString()); //checks if the referred property for constt items has been transferred 
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a property which was Transfered on " +
                                                    Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() +
                                                    " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." +
                                                    "<br>" + "<br>" + " Please delete the record for editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.closeform = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                    if (XRow["Item_Profile"].ToString() == "GOLD" || XRow["Item_Profile"].ToString() == "SILVER")
                                    {
                                        cnt = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, Loc_ID, null).Rows.Count;
                                        if (Convert.ToInt32(cnt) <= 0)
                                        {
                                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Asset Location");
                                            jsonParam.result = false;
                                            jsonParam.title = "Referred Record Already Changed!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    if (XRow["Item_Profile"].ToString() == "OTHER ASSETS")
                                    {
                                        cnt = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_Assets, Loc_ID, null).Rows.Count;
                                        if (Convert.ToInt32(cnt) <= 0)
                                        {
                                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Asset Location");
                                            jsonParam.result = false;
                                            jsonParam.title = "Referred Record Already Changed!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    if (XRow["Item_Profile"].ToString() == "LIVESTOCK")
                                    {
                                        cnt = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_LiveStock, Loc_ID, null).Rows.Count;
                                        if (Convert.ToInt32(cnt) <= 0)
                                        {
                                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Asset Location");
                                            jsonParam.result = false;
                                            jsonParam.title = "Referred Record Already Changed!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    if (XRow["Item_Profile"].ToString() == "VEHICLES")
                                    {
                                        cnt = BASE._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Loc_ID, null).Rows.Count;
                                        if (Convert.ToInt32(cnt) <= 0)
                                        {
                                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Asset Location");
                                            jsonParam.result = false;
                                            jsonParam.title = "Referred Record Already Changed!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                } //if(Loc_ID.ToString().Length > 0)
                            }// if (loc != null) end point
                            if (XRow["Item_Profile"].ToString() == "VEHICLES")
                            {
                                if (!(XRow["VI_OWNERSHIP_AB_ID"] == null) && (!Convert.IsDBNull(XRow["VI_OWNERSHIP_AB_ID"])))
                                {
                                    var Ownership_ID = XRow["VI_OWNERSHIP_AB_ID"].ToString().Length == 0 ? null : XRow["VI_OWNERSHIP_AB_ID"].ToString();
                                    var cnt1 = BASE._VehicleDBOps.GetOwners_List(Ownership_ID).Rows.Count;
                                    if (cnt1 <= 0)
                                    {
                                        jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                                        jsonParam.result = false;
                                        jsonParam.title = "Referred Record Already Changed!!";
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                            if (XRow["Item_Profile"].ToString() == "LAND & BUILDING")
                            {
                                var PartyID = XRow["LB_OWNERSHIP_PARTY_ID"].ToString().Length == 0 ? null : XRow["LB_OWNERSHIP_PARTY_ID"].ToString();
                                if (PartyID != null)
                                {
                                    cnt = BASE._L_B_DBOps.GetOwners(PartyID).Count;
                                    if (Convert.ToInt32(cnt) <= 0 && PartyID != "NULL")
                                    {
                                        jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                                        jsonParam.result = false;
                                        jsonParam.title = "Referred Record Already Changed!!";
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            if (XRow["LB_REC_ID"].ToString().Length > 0 && !(XRow["Item_Profile"].ToString().ToUpper() == "LAND & BUILDING"))
                            { //Select Property screen
                                string Cross_Ref_ID = "";
                                Cross_Ref_ID = XRow["LB_REC_ID"].ToString();
                                cnt = BASE._L_B_DBOps.GetListForExpenses(model.xMID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment, Cross_Ref_ID).Count;
                                if (Convert.ToInt32(cnt) <= 0)
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Property");
                                    jsonParam.result = false;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }

                            //Location Specific Checks
                            if (XRow["Item_Profile"].ToString() == "LAND & BUILDING")
                            {
                                foreach (DataRow cRow in DT_JV.Rows)
                                {
                                    if ((Convert.IsDBNull(XRow["LB_PRO_NAME"]) ? "" : XRow["LB_PRO_NAME"])
                                        .Equals((Convert.IsDBNull(cRow["LB_PRO_NAME"]) ? "" : cRow["LB_PRO_NAME"])) &&
                                        !((Convert.IsDBNull(XRow["LB_REC_ID"]) ? "" : XRow["LB_REC_ID"]))
                                        .Equals((Convert.IsDBNull(cRow["LB_REC_ID"]) ? "" : cRow["LB_REC_ID"])))
                                    {
                                        jsonParam.message = "Property/Location With Same Name Already Available in same voucher. . . !";
                                        jsonParam.result = false;
                                        jsonParam.title = "Property Name Duplicate";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }

                                if (model.Tag == Common_Lib.Common.Navigation_Mode._New ||
                                    model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                                {
                                    object MaxValue_Loc = 0;
                                    MaxValue_Loc = BASE._AssetLocDBOps.GetRecordCountByName(XRow["LB_PRO_NAME"].ToString(), Common_Lib.RealTimeService.ClientScreen.Profile_LandAndBuilding, BASE._open_PAD_No_Main, model.xMID);
                                    if (MaxValue_Loc == null)
                                    {
                                        jsonParam.message = Common_Lib.Messages.SomeError;
                                        jsonParam.result = false;
                                        jsonParam.title = "Error!!";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                    if (Convert.ToInt32(MaxValue_Loc) != 0)
                                    {
                                        jsonParam.message = "Location With Same Name Already Available. . . !";
                                        jsonParam.result = false;
                                        jsonParam.title = "Property Name Duplicate";
                                        jsonParam.closeform = true;
                                        jsonParam.refreshgrid = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }

                                DataTable LocNames = BASE._L_B_DBOps.GetPendingTfs_LocNames(BASE._open_Cen_Rec_ID);
                                if (LocNames != null)
                                {
                                    if (LocNames.Rows.Count > 0)
                                    {
                                        if (XRow["LB_PRO_NAME"].ToString().Length > 0)
                                        {
                                            for (int I = 0; I < LocNames.Rows.Count; I++)
                                            {
                                                if (XRow["LB_PRO_NAME"].ToString().ToUpper() == LocNames.Rows[I][0].ToString().ToUpper())
                                                {
                                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Location name");
                                                    jsonParam.result = false;
                                                    jsonParam.title = "Duplication in Referred Record, Location with same name already exists in pending Transfers!!";
                                                    jsonParam.closeform = true;
                                                    jsonParam.refreshgrid = true;
                                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        } //for loop end point
                    }// if new or new_from_selection or edit end point
                } //Allow multiuser end point

                IDictionary<int, DataTable> ProfilesData = new Dictionary<int, DataTable>();

                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection ||
                    model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    for (int I = 1; I <= DT_JV.Rows.Count; I++)
                    { // Bugs #4902, #4903, #4904 fixed
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + I);
                        if ((GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() != "NOT APPLICABLE" ||
                            GridRow_ToEdit[0]["Item_Voucher_Type"].ToString().ToUpper() == "LAND & BUILDING") &&
                            GridRow_ToEdit[0]["Addition"].ToString().ToUpper() == "FALSE")
                        { //Allows Profile || Construction Adjustment Entries only && leaves out Profile Creation / Expense / Income Entries
                            if (GridRow_ToEdit[0]["CrossRefID"].ToString().Length > 0)
                            { // Entries With Profile Reference 
                                if (BASE.AllowMultiuser())
                                {
                                    //Bug #5697
                                    switch (GridRow_ToEdit[0]["Item_Profile"].ToString())
                                    {
                                        case "GOLD":
                                        case "SILVER":
                                        case "OTHER ASSETS":
                                        case "VEHICLES":
                                        case "LIVESTOCK":
                                        case "LAND & BUILDING":
                                            
                                            if (Info_MaxEdit.ContainsKey(GridRow_ToEdit[0]["CrossRefID"].ToString()))
                                            {
                                                if (Info_MaxEdit[GridRow_ToEdit[0]["CrossRefID"].ToString()] != DateTime.MinValue)
                                                { //Record has been opened on basis of this being a last edited record for(referred asset
                                                    if (Convert.ToDateTime(BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(GridRow_ToEdit[0]["CrossRefID"].ToString())) >
                                                        Info_MaxEdit[GridRow_ToEdit[0]["CrossRefID"].ToString()])
                                                    {
                                                        jsonParam.message = Common_Lib.Messages.CustomChanges("Sorry ! Current Voucher is no Longer Latest Edited Voucher referring the Current Asset. Another Record has been Added/Edited in background which refers the same Asset.", "Last Edited Reference Entry");
                                                        jsonParam.result = false;
                                                        jsonParam.title = "Referred Record Already Changed!!";
                                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                                    }
                                                }
                                            }
                                            break;
                                        case "OTHER DEPOSITS":
                                            DateTime FinalPayDate = Convert.ToDateTime(BASE._DepositsDBOps.GetFinalPaymentDate(GridRow_ToEdit[0]["CrossRefID"].ToString())); //Bug #5683
                                            if (IsDate(FinalPayDate.ToString()) && FinalPayDate != DateTime.MinValue)
                                            {
                                                jsonParam.message = "Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " + FinalPayDate.ToLongDateString();
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                            }
                                            break;
                                    }
                                }
                                //Frm_Voucher_Win_Journal_Item jrnl_Item = new Frm_Voucher_Win_Journal_Item();
                                // Dim PROFILE_TABLE As DataTable = ProfilesData(I)  //jrnl_Item.GetReferenceData(GridRow_ToEdit[0]["Item_Profile"), GridRow_ToEdit[0]["Item_ID"), GridRow_ToEdit[0]["PartyID"].ToString(), Me.xMID.Text, model.Tag, GridRow_ToEdit[0]["CrossRefID"))
                                DataTable PROFILE_TABLE = CommonFunctions.GetReferenceData(BASE, GridRow_ToEdit[0]["Item_Profile"].ToString(),
                                    GridRow_ToEdit[0]["Item_ID"].ToString(), model.xMID, GridRow_ToEdit[0]["PartyID"].ToString(), model.Tag,
                                    GridRow_ToEdit[0]["CrossRefID"].ToString());
                                // BASE._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam) //Fetch asset data as per selection
                                ProfilesData.Add(I, PROFILE_TABLE); // adding in dictionary control to be used in date checks too
                                if (PROFILE_TABLE == null)
                                {
                                    jsonParam.message = Common_Lib.Messages.SomeError;
                                    jsonParam.result = false;
                                    jsonParam.title = "Error!!";
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                                if (BASE.AllowMultiuser())
                                {
                                    if (PROFILE_TABLE.Rows.Count <= 0)
                                    { // Profile existence check ...(Removed Due to Sale/Tf/Deletion/Discard)
                                        jsonParam.message = Common_Lib.Messages.DependencyChanged("Profile" + GridRow_ToEdit[0]["Item_Profile"]);
                                        jsonParam.result = false;
                                        jsonParam.title = "Referred Record Already Deleted/Transferred/Sold!!";
                                        jsonParam.refreshgrid = true;
                                        jsonParam.closeform = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        DateTime oldEditOn = Convert.ToDateTime(GridRow_ToEdit[0]["RefItem_RecEditOn"]);
                                        DateTime newEditOn = Convert.ToDateTime(PROFILE_TABLE.Rows[0]["REC_EDIT_ON"]);
                                        if (CommonFunctions.AreDatesEqual(oldEditOn, newEditOn) == false)
                                        { //assets changed in profile
                                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Profile" + " " + GridRow_ToEdit[0]["Item_Profile"]);
                                            jsonParam.result = false;
                                            jsonParam.title = "Referred Record Already Changed!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    string MessageOfComparision = "";
                                    switch (GridRow_ToEdit[0]["Item_Profile"].ToString())
                                    {
                                        case "GOLD":
                                        case "SILVER":

                                            decimal qty_goldsilver = string.IsNullOrWhiteSpace(GridRow_ToEdit[0]["Qty."].ToString()) ? 0 : Convert.ToDecimal(GridRow_ToEdit[0]["Qty."]);

                                            if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "CREDIT")
                                            { //DEDUCTION OF QTY
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Weight"]) < qty_goldsilver)
                                                { // Weight/qty remaining is less) { the weight/qty demanded for deduction 
                                                    MessageOfComparision = "Qty/Weight remaining  for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " is less than the qty/weight entered for deduction !!";
                                                }
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) < Convert.ToDecimal(GridRow_ToEdit[0]["Credit"]))
                                                { // Value remaining is less) { the amount demanded for deduction 
                                                    MessageOfComparision = "Current Value for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " is less than the Value credited !!";
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Weight"]) + qty_goldsilver < 0)
                                                { // Weight/qty remaining is less) { the weight/qty demanded for deduction 
                                                    MessageOfComparision = "Qty/Weight remaining  for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " becomes less than the 0!!";
                                                }
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) + Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]) < 0)
                                                { // Value remaining is less) { the amount demanded for deduction 
                                                    MessageOfComparision = "Adjusted Value for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " becomes less than 0 !!";
                                                }
                                            }
                                            break;
                                        case "OTHER ASSETS":

                                            decimal qty_otherAssets = string.IsNullOrWhiteSpace(GridRow_ToEdit[0]["Qty."].ToString()) ? 0 : Convert.ToDecimal(GridRow_ToEdit[0]["Qty."]);

                                            if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "CREDIT")
                                            { //DEDUCTION OF QTY
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Qty"]) < qty_otherAssets)
                                                { // Weight/qty remaining is less) { the weight/qty demanded for deduction 
                                                    MessageOfComparision = "Qty/Weight remaining  for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " is less) { the qty/weight entered for deduction !!";
                                                }
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) < Convert.ToDecimal(GridRow_ToEdit[0]["Credit"]))
                                                { // Value remaining is less) { the amount demanded for deduction 
                                                    MessageOfComparision = "Current Value for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " is less than the Value credited !!";
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Qty"]) + qty_otherAssets < 0)
                                                { // Weight/qty remaining is less) { 0
                                                    MessageOfComparision = "Qty/Weight remaining  for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " becomes less than 0 !!";
                                                }
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) + Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]) < 0)
                                                { // Value remaining is less) { 0
                                                    MessageOfComparision = "Adjusted Value for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " becomes less than the Value credited !!";
                                                }
                                            }
                                            break;
                                        case "VEHICLES":
                                        case "LIVESTOCK":
                                        case "LAND & BUILDING":
                                        case "ADVANCES":
                                        case "OTHER DEPOSITS":
                                        case "WIP":
                                            if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "CREDIT")
                                            { //DEDUCTION OF VALUE
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) < Convert.ToDecimal(GridRow_ToEdit[0]["Credit"]))
                                                { // Value remaining is less) { the amount demanded for deduction 
                                                    MessageOfComparision = "Current Value for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " is less than the Value credited !!";
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) + Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]) < 0)
                                                { // Value remaining is less) { 0
                                                    MessageOfComparision = "Adjusted Value for selected " + GridRow_ToEdit[0]["Item_Profile"].ToString().ToLower() +
                                                        " becomes less than 0 !!";
                                                }
                                            }
                                            break;
                                        case "OTHER LIABILITIES":
                                            if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "DEBIT")
                                            { //DEDUCTION OF VALUE
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) < Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]))
                                                { // Value remaining is less) { the amount demanded for deduction 
                                                    MessageOfComparision = "Current Value for selected liability is less than the Value debited !!";
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) + Convert.ToDecimal(GridRow_ToEdit[0]["Credit"]) < 0)
                                                { // Value remaining is less) { 0
                                                    MessageOfComparision = "Adjusted Value for selected liability becomes less than 0 !!";
                                                }
                                            }
                                            break;
                                        case "OPENING":
                                            break;
                                        default: //Construction Items

                                            if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "CREDIT")
                                            { //DEDUCTION OF VALUE
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) < Convert.ToDecimal(GridRow_ToEdit[0]["Credit"]))
                                                { // Value remaining is less) { the amount demanded for deduction 
                                                    MessageOfComparision = "Current Value for referred property is less than the Value credited !!";
                                                }
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) + Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]) < 0)
                                                { // Value remaining is less) { 0
                                                    MessageOfComparision = "Adjusted Value for referred property becomes less than 0 !!";
                                                }
                                            }
                                            break;
                                    }

                                    if (MessageOfComparision.Length > 0)
                                    {
                                        jsonParam.message = Common_Lib.Messages.CustomChanges(MessageOfComparision, GridRow_ToEdit[0]["Item_Profile"].ToString());
                                        jsonParam.result = false;
                                        jsonParam.title = "Action not allowed!!";
                                        jsonParam.refreshgrid = true;
                                        jsonParam.closeform = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        if (BASE.AllowMultiuser())
                        {
                            if (model.Tag == Common_Lib.Common.Navigation_Mode._New ||
                                model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection ||
                                model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                            {
                                if (GridRow_ToEdit[0]["PartyID"].ToString().Length > 0)
                                {
                                    DataTable PartyDetail = BASE._Address_DBOps.GetRecord(GridRow_ToEdit[0]["PartyID"].ToString());
                                    DateTime oldEditOn = Convert.ToDateTime(GridRow_ToEdit[0]["Party_RecEditOn"]);
                                    if (PartyDetail.Rows.Count <= 0)
                                    { //Party already changed/deleted
                                        jsonParam.message = Common_Lib.Messages.DependencyChanged("Address book");
                                        jsonParam.result = false;
                                        jsonParam.title = "Record Already Deleted !!";
                                        jsonParam.refreshgrid = true;
                                        jsonParam.closeform = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        DateTime newEditOn = Convert.ToDateTime(PartyDetail.Rows[0]["REC_EDIT_ON"]);
                                        if (newEditOn != oldEditOn)
                                        {
                                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Address book");
                                            jsonParam.result = false;
                                            jsonParam.title = "Record Already Changed !!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    //Check for Full Party Address , if (a Gift Item has been referred 
                                    if (GridRow_ToEdit[0]["Item_Led_ID"].ToString() == "00182")
                                    { // Contributions in Kind Ledger //Bug #4731
                                        if ((PartyDetail.Rows[0]["C_R_ADD1"].ToString().Trim().Length <= 0) ||
                                                (PartyDetail.Rows[0]["C_R_CITY_ID"].ToString().Trim().Length <= 0) ||
                                                (PartyDetail.Rows[0]["C_R_DISTRICT_ID"].ToString().Trim().Length <= 0) ||
                                                (PartyDetail.Rows[0]["C_R_STATE_ID"].ToString().Trim().Length <= 0) ||
                                                (PartyDetail.Rows[0]["C_R_COUNTRY_ID"].ToString().Trim().Length <= 0))
                                        {
                                            jsonParam.message = "Donor(" + PartyDetail.Rows[0]["C_NAME"] + ") Address Incomplete . . . !" + "<br>" + "Mandatory: Address Line.1, City, District, State & Country...";
                                            jsonParam.result = false;
                                            jsonParam.title = "Incomplete Information . . .";
                                            jsonParam.focusid = "Win_Journal_Item_Grid";
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }
                    }// Next
                }
                //Location check 
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection ||
                    model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                {
                    for (int I = 1; I <= DT_JV.Rows.Count; I++)
                    {
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + I);
                        if ((GridRow_ToEdit[0]["Item_Voucher_Type"].ToString().ToUpper() == "LAND & BUILDING" ||
                            GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "LAND & BUILDING") &&
                            GridRow_ToEdit[0]["Addition"].ToString().ToUpper() == "FALSE")
                        {
                            if (GridRow_ToEdit[0]["CrossRefID"].ToString().Length > 0)
                            { // Entries With Profile Reference 
                                DataTable PROFILE_TABLE = ProfilesData[I];
                                if (PROFILE_TABLE == null)
                                {
                                    jsonParam.message = Common_Lib.Messages.SomeError;
                                    jsonParam.result = false;
                                    jsonParam.title = "Error!!";
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                                // if(Base.AllowMultiuser()) {
                                string MessageOfComparision = "";
                                if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "CREDIT")
                                {
                                    if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) <= Convert.ToDecimal(GridRow_ToEdit[0]["Credit"]))
                                    {
                                        // Value remaining is less) { the amount demanded for(deduction 
                                        MessageOfComparision = "Value for Selected Property ( " + PROFILE_TABLE.Rows[0]["Item"] +
                                            " ) becomes Less than or equals to 0 !!";
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) + Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]) <= 0)
                                    {
                                        // Value remaining is less) { the amount demanded for(deduction 
                                        MessageOfComparision = "Value for Selected Property ( " + PROFILE_TABLE.Rows[0]["Item"] +
                                            " )  b e c o m e s   l e s s   t h a n   o r  e q u a l  t o  0 !!";
                                    }
                                }
                                if (MessageOfComparision.Length > 0)
                                {
                                    string Msg = FindLocationUsage(model.Tag, GridRow_ToEdit[0]["CrossRefID"].ToString(), true, true);
                                    if (Msg.Length > 0)
                                    {
                                        jsonParam.message = MessageOfComparision + "<br>" + "<br>" + Msg;
                                        jsonParam.result = false;
                                        jsonParam.title = "Information";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                }
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    for (int I = 1; I <= DT_JV.Rows.Count; I++)
                    { // Bugs #4902, #4903, #4904 fixed
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + I);
                        if ((GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() != "NOT APPLICABLE" ||
                            GridRow_ToEdit[0]["Item_Voucher_Type"].ToString().ToUpper() == "LAND & BUILDING") &&
                            GridRow_ToEdit[0]["Addition"].ToString().ToUpper() == "FALSE")
                        { //Allows Profile || Construction Adjustment Entries only and leaves out Profile Creation / Expense / Income Entries
                            if (GridRow_ToEdit[0]["CrossRefID"].ToString().Length > 0)
                            { // Entries With Profile Reference 
                                //Frm_Voucher_Win_Journal_Item jrnl_Item = new Frm_Voucher_Win_Journal_Item;
                                DataTable PROFILE_TABLE = ProfilesData[I];
                                if (PROFILE_TABLE == null)
                                {
                                    jsonParam.message = Common_Lib.Messages.SomeError;
                                    jsonParam.result = false;
                                    jsonParam.title = "Error!!";
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }

                                //Property Value Reduced to 0 on deletion           
                                string MsgLoc = "";

                                if ((GridRow_ToEdit[0]["Item_Voucher_Type"].ToString().ToUpper() == "LAND & BUILDING" ||
                                    GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "LAND & BUILDING") &&
                                    GridRow_ToEdit[0]["Addition"].ToString().ToUpper() == "FALSE")
                                {
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString().ToUpper() == "DEBIT")
                                    { //DEDUCTION OF VALUE

                                        if (Convert.ToDecimal(PROFILE_TABLE.Rows[0]["Curr Value"]) == Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]))
                                        { // 
                                            MsgLoc = FindLocationUsage(model.Tag, GridRow_ToEdit[0]["CrossRefID"].ToString(), false, true);
                                        }
                                    }
                                }
                                if (MsgLoc.Length > 0)
                                {
                                    jsonParam.message = MsgLoc;
                                    jsonParam.result = false;
                                    jsonParam.title = model.Me_Text;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }

                ArrayList ReferenceRepetitionCheck = new ArrayList();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection ||
                    model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                {
                    for (int I = 1; I <= DT_JV.Rows.Count; I++)
                    {
                        // Bugs #4902, #4903, #4904 fixed
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + I);
                        if ((GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() != "NOT APPLICABLE" ||
                            GridRow_ToEdit[0]["Item_Voucher_Type"].ToString().ToUpper() == "LAND & BUILDING") &&
                            GridRow_ToEdit[0]["Addition"].ToString().ToUpper() == "FALSE")
                        {
                            //Allows Profile || Construction Adjustment Entries only && leaves out Profile Creation / Expense / Income Entries
                            if (GridRow_ToEdit[0]["CrossRefID"].ToString().Length > 0)
                            { // Entries With Profile Reference                                
                                DataTable PROF_TABLE = ProfilesData[I];
                                if (model.Tag != Common_Lib.Common.Navigation_Mode._Delete)
                                {
                                    switch (GridRow_ToEdit[0]["Item_Profile"])
                                    {
                                        case "GOLD":
                                        case "SILVER":
                                        case "OTHER ASSETS":
                                        case "VEHICLES":
                                        case "LIVESTOCK":
                                        case "LAND & BUILDING": //http://pm.bkinfo.in/issues/5124#note-7 pt.1
                                            Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate inparam = new Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate();
                                            inparam.Creation_Date = Convert.IsDBNull(PROF_TABLE.Rows[0]["REF_CREATION_DATE"]) ? BASE._open_Year_Sdt : Convert.ToDateTime(PROF_TABLE.Rows[0]["REF_CREATION_DATE"].ToString());
                                            inparam.Asset_RecID = PROF_TABLE.Rows[0]["REC_ID"].ToString();
                                            inparam.YearID = BASE._open_Year_ID;
                                            inparam.Tr_M_ID = model.xMID;
                                            DateTime MxDate = Convert.ToDateTime(BASE._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam));
                                            if (MxDate == null)
                                            {
                                                jsonParam.message = Common_Lib.Messages.SomeError;
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                            if (model.Txt_V_Date_Jv < MxDate)
                                            {
                                                jsonParam.message = "Voucher Date Cannot be less than previous transaction on same asset dated " +
                                                    MxDate.ToLongDateString() + ". . . !";
                                                jsonParam.result = false;
                                                jsonParam.title = "Incomplete Information . . .";
                                                jsonParam.focusid = "Txt_V_Date_Jv";
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                            break;
                                        case "ADVANCES":
                                        case "OTHER DEPOSITS":
                                        case "OTHER LIABILITIES": ////  //http://pm.bkinfo.in/issues/5124#note-7 pt.6, , "WIP"
                                            DateTime CreationDate = Convert.IsDBNull(PROF_TABLE.Rows[0]["REF_CREATION_DATE"]) ?
                                                BASE._open_Year_Sdt : Convert.ToDateTime(PROF_TABLE.Rows[0]["REF_CREATION_DATE"].ToString());
                                            if (model.Txt_V_Date_Jv < CreationDate)
                                            {
                                                jsonParam.message = "Current Reference Voucher Date Cannot be less than Creation Voucher dated" +
                                                    CreationDate.ToLongDateString() + " for " + GridRow_ToEdit[0]["Item_Profile"] + ". . . !";
                                                jsonParam.result = false;
                                                jsonParam.title = "Incomplete Information . . .";
                                                jsonParam.focusid = "Txt_V_Date_Jv";
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                            break;
                                    }
                                    //Bug #5684
                                    if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() != "NOT APPLICABLE")
                                    { //Repetition check skipped for Constt Entries 
                                        if (ReferenceRepetitionCheck.Contains(GridRow_ToEdit[0]["CrossRefID"].ToString()))
                                        {
                                            string Reference = "";
                                            switch (GridRow_ToEdit[0]["Item_Profile"])
                                            {
                                                case "GOLD":
                                                case "SILVER":
                                                case "OTHER ASSETS":
                                                case "LIVESTOCK":
                                                case "LAND & BUILDING":
                                                case "ADVANCES":
                                                case "OTHER DEPOSITS":
                                                    Reference = PROF_TABLE.Rows[0]["Item"].ToString();
                                                    break;
                                                case "VEHICLES":
                                                    Reference = PROF_TABLE.Rows[0]["Vehicle"].ToString();
                                                    break;
                                                case "WIP":
                                                    Reference = PROF_TABLE.Rows[0]["Reference"].ToString();
                                                    break;
                                                case "OTHER LIABILITIES":
                                                    Reference = "Org. Value : " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                    break;
                                            }
                                            jsonParam.message = "Sorry! Each Profile Record Can be referred only once in a Journal Voucher." +
                                                "<br>" + "<br>" + "Please remove duplicate references to same" + GridRow_ToEdit[0]["Item_Profile"] +
                                                " Record (" + Reference + ")";
                                            jsonParam.result = false;
                                            jsonParam.title = "Error!!";
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            ReferenceRepetitionCheck.Add(GridRow_ToEdit[0]["CrossRefID"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
              
                Common_Lib.RealTimeService.Param_Txn_Insert_VoucherJournal InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_VoucherJournal();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                { //new      
                    string XID = "";
                    model.xMID = Guid.NewGuid().ToString();//Master Record 
                    Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucheJournal InMInfo = new Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucheJournal();
                    InMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Payment;
                    InMInfo.VNo = model.Txt_V_NO_Jv;
                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                    {
                        InMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMInfo.TDate = model.Txt_V_Date_Jv.ToString();
                    }
                    InMInfo.SubTotal = Convert.ToDouble(model.Txt_DrTotal_Jv);
                    InMInfo.Status_Action = Status_Action;
                    InMInfo.RecID = model.xMID;


                    InNewParam.param_InsertMaster = InMInfo;
                    string Ref_Rec_ID = "";
                    int cnt = 0;

                    Parameter_InsertTRID_Advances[] InAdv = new Common_Lib.RealTimeService.Parameter_InsertTRID_Advances[DT_JV.Rows.Count];
                    Parameter_InsertTRID_Liabilities[] InLiab = new Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities[DT_JV.Rows.Count];
                    Parameter_InsertTRID_Deposits[] InDep = new Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits[DT_JV.Rows.Count];
                    Parameter_Insert_VoucherJournal[] Insert = new Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal[DT_JV.Rows.Count];
                    Parameter_InsertPurpose_VoucherJournal[] InsertPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSRNo_GoldSilver[] InsertGS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSrNo_Assets[] InsertAssets = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSrNo_LiveStock[] InsertLivestock = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSrNo_Vehicles[] InsertVehicles = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles[DT_JV.Rows.Count];
                    Parameter_InsertMasterIDAndSrNo_LandAndBuilding[] InsertProperty = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding[DT_JV.Rows.Count];
                    Param_InsertTRIDAndTRSrNo_WIP_Profile[] InsertReferencesWIP = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile[DT_JV.Rows.Count];


                    for (int I = 1; I <= DT_JV.Rows.Count; I++) //Add Txn
                    {
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + I);
                        string Cross_Ref_ID = "";
                        if (GridRow_ToEdit[0]["LB_REC_ID"].ToString().Length > 0 && GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() != "LAND + BUILDING")
                        { //Bug #5637
                            Cross_Ref_ID = GridRow_ToEdit[0]["LB_REC_ID"].ToString();
                            if (BASE.AllowMultiuser())
                            { // Ref A/AE in AO33
                                if (Cross_Ref_ID != null)
                                {
                                    if (Cross_Ref_ID.Length > 0)
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(Cross_Ref_ID); //checks if(the referred property for(constt items has been sold 
                                        if (SaleRecord != null)
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " +
                                                Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() +
                                                " for(initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." +
                                                "<br>" + "<br>" + " Please delete the record for(editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.refreshgrid = true;
                                                jsonParam.closeform = true;
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers,
                                            Convert.ToInt32(null), Cross_Ref_ID); //checks if(the referred property for(constt items has been transferred 
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " +
                                                Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for(initial payment of Rs." +
                                                AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" +
                                                " Please delete the record for(editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.refreshgrid = true;
                                                jsonParam.closeform = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //check for(new additions
                        if (Convert.ToBoolean(GridRow_ToEdit[0]["Addition"]))
                        {
                            Ref_Rec_ID = Guid.NewGuid().ToString();
                            switch (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper())
                            {
                                case "ADVANCES":
                                    Common_Lib.RealTimeService.Parameter_InsertTRID_Advances _Adv = new Common_Lib.RealTimeService.Parameter_InsertTRID_Advances();
                                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                                    {
                                        _Adv.AdvanceDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else { _Adv.AdvanceDate = model.Txt_V_Date_Jv.ToString(); }
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { _Adv.Amount = 0; }
                                        else { _Adv.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { _Adv.Amount = 0; }
                                        else { _Adv.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]); }
                                    }
                                    _Adv.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    _Adv.PartyID = GridRow_ToEdit[0]["PartyID"].ToString();
                                    _Adv.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    _Adv.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    _Adv.Status_Action = Status_Action;
                                    _Adv.TxnID = model.xMID;
                                    _Adv.RecID = Ref_Rec_ID;

                                    InAdv[cnt] = _Adv;
                                    break;
                                case "OTHER LIABILITIES":
                                    Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities _Liab = new Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities();
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { _Liab.Amount = 0; }
                                        else { _Liab.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString()); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { _Liab.Amount = 0; }
                                        else { _Liab.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    _Liab.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                                    { _Liab.LiabilityDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short); }
                                    else
                                    { _Liab.LiabilityDate = model.Txt_V_Date_Jv.ToString(); }
                                    _Liab.PartyID = GridRow_ToEdit[0]["PartyID"].ToString();
                                    _Liab.RecID = Ref_Rec_ID;
                                    _Liab.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    _Liab.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    _Liab.Status_Action = Status_Action;
                                    _Liab.TxnID = model.xMID;
                                    InLiab[cnt] = _Liab;
                                    break;

                                case "OTHER DEPOSITS":
                                    Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits InDept = new Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits();
                                    InDept.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InDept.AgainstInsurance = "NO";
                                    InDept.PartyID = GridRow_ToEdit[0]["PartyID"].ToString();
                                    InDept.InsCompanyMiscID = "";
                                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                                    { InDept.DepositDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short); }
                                    else { InDept.DepositDate = model.Txt_V_Date_Jv.ToString(); }
                                    InDept.DepositPeriod = 0;
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null)
                                        { InDept.Amount = 0; }
                                        else
                                        { InDept.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString()); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null)
                                        { InDept.Amount = 0; }
                                        else
                                        { InDept.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    InDept.InterestRate = 0;
                                    InDept.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InDept.TxnID = model.xMID;
                                    InDept.Status_Action = Status_Action;
                                    InDept.RecID = Ref_Rec_ID;
                                    InDep[cnt] = InDept;
                                    break;

                                case "GOLD":
                                case "SILVER":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver InGS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver();
                                    InGS.Type = Convert.IsDBNull(GridRow_ToEdit[0]["Item_Profile"]) ? null : GridRow_ToEdit[0]["Item_Profile"].ToString();
                                    InGS.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InGS.DescMiscID = Convert.IsDBNull(GridRow_ToEdit[0]["GS_DESC_MISC_ID"]) ? null : GridRow_ToEdit[0]["GS_DESC_MISC_ID"].ToString();
                                    InGS.Weight = Convert.IsDBNull(GridRow_ToEdit[0]["GS_ITEM_WEIGHT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["GS_ITEM_WEIGHT"]);
                                    InGS.LocationID = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InGS.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InGS.TxnID = model.xMID;
                                    InGS.TxnSrno = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);

                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null)
                                        { InGS.Amount = 0; }
                                        else
                                        { InGS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null)
                                        { InGS.Amount = 0; }
                                        else
                                        { InGS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]); }
                                    }
                                    InGS.Status_Action = Status_Action;
                                    InGS.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertGS[cnt] = InGS;
                                    break;
                                case "OTHER ASSETS":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets InAsset = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets();
                                    InAsset.AssetType = Convert.IsDBNull(GridRow_ToEdit[0]["AI_TYPE"]) ? null : GridRow_ToEdit[0]["AI_TYPE"].ToString();
                                    InAsset.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InAsset.Make = Convert.IsDBNull(GridRow_ToEdit[0]["AI_MAKE"]) ? null : GridRow_ToEdit[0]["AI_MAKE"].ToString();
                                    InAsset.Model = Convert.IsDBNull(GridRow_ToEdit[0]["AI_MODEL"]) ? null : GridRow_ToEdit[0]["AI_MODEL"].ToString();
                                    InAsset.SrNo = Convert.IsDBNull(GridRow_ToEdit[0]["AI_SERIAL_NO"]) ? null : GridRow_ToEdit[0]["AI_SERIAL_NO"].ToString();
                                    InAsset.Rate = 0;
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["AI_PUR_DATE"]) ? null : GridRow_ToEdit[0]["AI_PUR_DATE"].ToString()))
                                    {
                                        InAsset.PurchaseDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["AI_PUR_DATE"]) ? null : GridRow_ToEdit[0]["AI_PUR_DATE"]).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InAsset.PurchaseDate = Convert.IsDBNull(GridRow_ToEdit[0]["AI_PUR_DATE"]) ? null : GridRow_ToEdit[0]["AI_PUR_DATE"].ToString();
                                    }

                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null)
                                        { InAsset.InsAmount = 0; }
                                        else
                                        { InAsset.InsAmount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null)
                                        { InAsset.InsAmount = 0; }
                                        else
                                        { InAsset.InsAmount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]); }
                                    }
                                    InAsset.PurchaseAmount = InAsset.AssetType.ToUpper() == "ASSET" ? InAsset.InsAmount : 0;
                                    InAsset.Warranty = Convert.IsDBNull(GridRow_ToEdit[0]["AI_WARRANTY"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["AI_WARRANTY"]);
                                    InAsset.Quantity = Convert.IsDBNull(GridRow_ToEdit[0]["Qty."]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["Qty."]);
                                    InAsset.LocationId = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InAsset.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InAsset.TxnID = model.xMID;
                                    InAsset.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    InAsset.Status_Action = Status_Action;
                                    InAsset.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertAssets[cnt] = InAsset;
                                    break;
                                case "LIVESTOCK":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock InLS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock();
                                    InLS.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InLS.Name = Convert.IsDBNull(GridRow_ToEdit[0]["LS_NAME"]) ? null : GridRow_ToEdit[0]["LS_NAME"].ToString();
                                    InLS.Year = Convert.IsDBNull(GridRow_ToEdit[0]["LS_BIRTH_YEAR"]) ? null : GridRow_ToEdit[0]["LS_BIRTH_YEAR"].ToString();

                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null)
                                        { InLS.Amount = 0; }
                                        else
                                        { InLS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null)
                                        { InLS.Amount = 0; }
                                        else
                                        { InLS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]); }
                                    }
                                    InLS.Insurance = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INSURANCE"]) ? null : GridRow_ToEdit[0]["LS_INSURANCE"].ToString();
                                    InLS.InsuranceID = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INSURANCE_ID"]) ? null : GridRow_ToEdit[0]["LS_INSURANCE_ID"].ToString();
                                    InLS.PolicyNo = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_POLICY_NO"]) ? null : GridRow_ToEdit[0]["LS_INS_POLICY_NO"].ToString();
                                    InLS.InsAmount = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LS_INS_AMT"]);
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_DATE"]) ? null : GridRow_ToEdit[0]["LS_INS_DATE"].ToString()))
                                    {
                                        InLS.InsuranceDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_DATE"]) ? null : GridRow_ToEdit[0]["LS_INS_DATE"]).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLS.InsuranceDate = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_DATE"]) ? null : GridRow_ToEdit[0]["LS_INS_DATE"].ToString();
                                    }

                                    InLS.LocationID = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InLS.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InLS.TxnID = model.xMID;
                                    InLS.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    InLS.Status_Action = Status_Action;
                                    InLS.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertLivestock[cnt] = InLS;
                                    break;

                                case "VEHICLES":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles InVeh = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles();
                                    InVeh.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InVeh.Make = Convert.IsDBNull(GridRow_ToEdit[0]["VI_MAKE"]) ? null : GridRow_ToEdit[0]["VI_MAKE"].ToString();
                                    InVeh.Model = Convert.IsDBNull(GridRow_ToEdit[0]["VI_MODEL"]) ? null : GridRow_ToEdit[0]["VI_MODEL"].ToString();
                                    InVeh.Reg_No_Pattern = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_NO_PATTERN"]) ? null : GridRow_ToEdit[0]["VI_REG_NO_PATTERN"].ToString();
                                    InVeh.Reg_No = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_NO"]) ? null : GridRow_ToEdit[0]["VI_REG_NO"].ToString();
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_DATE"]) ? null : GridRow_ToEdit[0]["VI_REG_DATE"].ToString()))
                                    {
                                        InVeh.RegDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_DATE"]) ? null : GridRow_ToEdit[0]["VI_REG_DATE"]).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InVeh.RegDate = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_DATE"]) ? null : GridRow_ToEdit[0]["VI_REG_DATE"].ToString();
                                    }
                                    InVeh.Ownership = Convert.IsDBNull(GridRow_ToEdit[0]["VI_OWNERSHIP"]) ? null : GridRow_ToEdit[0]["VI_OWNERSHIP"].ToString();
                                    if (GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"] != null && Convert.IsDBNull(GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"]) == false)
                                    {
                                        InVeh.Ownership_AB_ID = GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"].ToString().Length == 0 ? null : GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"].ToString();
                                    }
                                    InVeh.Doc_RC_Book = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_RC_BOOK"]) ? null : GridRow_ToEdit[0]["VI_DOC_RC_BOOK"].ToString();
                                    InVeh.Doc_Affidavit = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_AFFIDAVIT"]) ? null : GridRow_ToEdit[0]["VI_DOC_AFFIDAVIT"].ToString();
                                    InVeh.Doc_Will = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_WILL"]) ? null : GridRow_ToEdit[0]["VI_DOC_WILL"].ToString();
                                    InVeh.Doc_TRF_Letter = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_TRF_LETTER"]) ? null : GridRow_ToEdit[0]["VI_DOC_TRF_LETTER"].ToString();
                                    InVeh.DOC_FU_Letter = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_FU_LETTER"]) ? null : GridRow_ToEdit[0]["VI_DOC_FU_LETTER"].ToString();
                                    InVeh.Doc_Is_Others = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_OTHERS"]) ? null : GridRow_ToEdit[0]["VI_DOC_OTHERS"].ToString();
                                    InVeh.Doc_Others_Name = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_NAME"]) ? null : GridRow_ToEdit[0]["VI_DOC_NAME"].ToString();
                                    if (Convert.IsDBNull(GridRow_ToEdit[0]["VI_INSURANCE_ID"]))
                                    {
                                        InVeh.Insurance_ID = null;
                                    }
                                    else if (GridRow_ToEdit[0]["VI_INSURANCE_ID"] == null)
                                    {
                                        InVeh.Insurance_ID = null;
                                    }
                                    else if (GridRow_ToEdit[0]["VI_INSURANCE_ID"].ToString().Length == 0)
                                    {
                                        InVeh.Insurance_ID = null;
                                    }
                                    else
                                    {
                                        InVeh.Insurance_ID = GridRow_ToEdit[0]["VI_INSURANCE_ID"].ToString();
                                    }
                                    InVeh.Ins_Policy_No = Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_POLICY_NO"]) ? null : GridRow_ToEdit[0]["VI_INS_POLICY_NO"].ToString();
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]) ? null : GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"].ToString()))
                                    {
                                        InVeh.Ins_Expiry_Date = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]) ? null : GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InVeh.Ins_Expiry_Date = Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]) ? null : GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"].ToString();
                                    }
                                    InVeh.Location_ID = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InVeh.Other_Details = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InVeh.TxnID = model.xMID;
                                    InVeh.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null)
                                        { InVeh.Amount = 0; }
                                        else
                                        { InVeh.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null)
                                        { InVeh.Amount = 0; }
                                        else
                                        { InVeh.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]); }
                                    }
                                    InVeh.Status_Action = Status_Action;
                                    InVeh.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertVehicles[cnt] = InVeh;
                                    break;

                                case "LAND & BUILDING":
                                    string PartyID = "NULL";
                                    if (GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"].ToString().ToUpper() != "NULL")
                                    { PartyID = "'" + GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"] + "'"; }
                                    Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding InLB = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding();
                                    InLB.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InLB.PropertyType = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_TYPE"]) ? null : GridRow_ToEdit[0]["LB_PRO_TYPE"].ToString();
                                    InLB.Category = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_CATEGORY"]) ? null : GridRow_ToEdit[0]["LB_PRO_CATEGORY"].ToString();
                                    InLB.Use = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_USE"]) ? null : GridRow_ToEdit[0]["LB_PRO_USE"].ToString();
                                    InLB.Name = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_NAME"]) ? null : GridRow_ToEdit[0]["LB_PRO_NAME"].ToString().Trim();
                                    InLB.Address = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_ADDRESS"]) ? null : GridRow_ToEdit[0]["LB_PRO_ADDRESS"].ToString();
                                    InLB.LB_Add1 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS1"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS1"].ToString();
                                    InLB.LB_Add2 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS2"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS2"].ToString();
                                    InLB.LB_Add3 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS3"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS3"].ToString();
                                    InLB.LB_Add4 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS4"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS4"].ToString();
                                    InLB.LB_CityID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CITY_ID"]) ? null : GridRow_ToEdit[0]["LB_CITY_ID"].ToString();
                                    InLB.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e";
                                    InLB.LB_DisttID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DISTRICT_ID"]) ? null : GridRow_ToEdit[0]["LB_DISTRICT_ID"].ToString();
                                    InLB.LB_PinCode = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PINCODE"]) ? null : GridRow_ToEdit[0]["LB_PINCODE"].ToString();
                                    InLB.LB_StateID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_STATE_ID"]) ? null : GridRow_ToEdit[0]["LB_STATE_ID"].ToString();
                                    InLB.Ownership = Convert.IsDBNull(GridRow_ToEdit[0]["LB_OWNERSHIP"]) ? null : GridRow_ToEdit[0]["LB_OWNERSHIP"].ToString();
                                    InLB.Owner_Party_ID = PartyID;
                                    InLB.SurveyNo = Convert.IsDBNull(GridRow_ToEdit[0]["LB_SURVEY_NO"]) ? null : GridRow_ToEdit[0]["LB_SURVEY_NO"].ToString();
                                    InLB.TotalArea = Convert.IsDBNull(GridRow_ToEdit[0]["LB_TOT_P_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_TOT_P_AREA"]);
                                    InLB.ConstructedArea = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CON_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_CON_AREA"]);
                                    InLB.ConstructionYear = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CON_YEAR"]) ? null : GridRow_ToEdit[0]["LB_CON_YEAR"].ToString();
                                    InLB.RCCRoof = Convert.IsDBNull(GridRow_ToEdit[0]["LB_RCC_ROOF"]) ? null : GridRow_ToEdit[0]["LB_RCC_ROOF"].ToString();
                                    InLB.DepositAmount = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DEPOSIT_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_DEPOSIT_AMT"]);
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PAID_DATE"]) ? null : GridRow_ToEdit[0]["LB_PAID_DATE"].ToString()))
                                    {
                                        InLB.PaymentDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PAID_DATE"]) ? null : GridRow_ToEdit[0]["LB_PAID_DATE"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLB.PaymentDate = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PAID_DATE"]) ? null : GridRow_ToEdit[0]["LB_PAID_DATE"].ToString();
                                    }

                                    InLB.MonthlyRent = Convert.IsDBNull(GridRow_ToEdit[0]["LB_MONTH_RENT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_RENT"]);
                                    InLB.MonthlyOtherExpenses = Convert.IsDBNull(GridRow_ToEdit[0]["LB_MONTH_O_PAYMENTS"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_O_PAYMENTS"]);
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_FROM"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString()))
                                    {
                                        InLB.PeriodFrom = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_FROM"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLB.PeriodFrom = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_FROM"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString();
                                    }
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_TO"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString()))
                                    {
                                        InLB.PeriodTo = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_TO"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLB.PeriodTo = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_TO"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString();
                                    }
                                    InLB.OtherDocs = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DOC_OTHERS"]) ? null : GridRow_ToEdit[0]["LB_DOC_OTHERS"].ToString();
                                    InLB.DocNames = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DOC_NAME"]) ? null : GridRow_ToEdit[0]["LB_DOC_NAME"].ToString();

                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null)
                                        { InLB.Value = 0; }
                                        else { InLB.Value = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { InLB.Value = 0; } else { InLB.Value = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]); }
                                    }
                                    InLB.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["LB_OTHER_DETAIL"]) ? null : GridRow_ToEdit[0]["LB_OTHER_DETAIL"].ToString();
                                    InLB.MasterID = model.xMID;
                                    InLB.SrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    InLB.Status_Action = Status_Action;
                                    InLB.RecID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_REC_ID"]) ? null : GridRow_ToEdit[0]["LB_REC_ID"].ToString();

                                    //EXTENSIONS 
                                    Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[] ExtInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[LB_EXTENDED_PROPERTY_TABLE.Rows.Count];
                                    int cnt1 = 0;
                                    if (LB_EXTENDED_PROPERTY_TABLE != null)
                                    {
                                        foreach (DataRow _Ext_Row in LB_EXTENDED_PROPERTY_TABLE.Rows)
                                        {
                                            if (_Ext_Row["LB_REC_ID"].ToString() == GridRow_ToEdit[0]["LB_REC_ID"].ToString())
                                            {
                                                Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding InEInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding();
                                                InEInfo.LB_Rec_ID = Convert.IsDBNull(_Ext_Row["LB_REC_ID"]) ? null : _Ext_Row["LB_REC_ID"].ToString();
                                                InEInfo.SrNo = Convert.IsDBNull(_Ext_Row["LB_SR_NO"]) ? null : _Ext_Row["LB_SR_NO"].ToString();
                                                InEInfo.Inst_ID = Convert.IsDBNull(_Ext_Row["LB_INS_ID"]) ? null : _Ext_Row["LB_INS_ID"].ToString();
                                                InEInfo.TotalArea = Convert.ToDouble(_Ext_Row["LB_TOT_P_AREA"]);
                                                InEInfo.ConstructedArea = Convert.ToDouble(_Ext_Row["LB_CON_AREA"]);
                                                InEInfo.ConYear = Convert.IsDBNull(_Ext_Row["LB_CON_YEAR"]) ? null : _Ext_Row["LB_CON_YEAR"].ToString();
                                                if (IsDate(Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"].ToString()))
                                                {
                                                    InEInfo.MOU_Date = Convert.ToDateTime(Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                                }
                                                else
                                                { InEInfo.MOU_Date = Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"].ToString(); }

                                                InEInfo.Value = Convert.ToDouble(_Ext_Row["LB_VALUE"]);
                                                InEInfo.OtherDetails = Convert.IsDBNull(_Ext_Row["LB_OTHER_DETAIL"]) ? null : _Ext_Row["LB_OTHER_DETAIL"].ToString();
                                                InEInfo.Status_Action = Status_Action;
                                                InEInfo.RecID = System.Guid.NewGuid().ToString();

                                                ExtInfo[cnt1] = InEInfo;
                                                cnt1 += 1;
                                            }
                                        }
                                    }
                                    InLB.InsertExtInfo = ExtInfo;
                                    //DOCS
                                    Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[] DocInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[LB_DOCS_ARRAY.Rows.Count];
                                    cnt1 = 0;
                                    if (LB_DOCS_ARRAY != null)
                                    {
                                        foreach (DataRow _Ext_Row in LB_DOCS_ARRAY.Rows)
                                        {
                                            if (_Ext_Row["LB_REC_ID"].ToString() == GridRow_ToEdit[0]["LB_REC_ID"].ToString())
                                            {
                                                Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding InDocInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding();
                                                InDocInfo.LB_Rec_ID = Convert.IsDBNull(_Ext_Row["LB_REC_ID"]) ? null : _Ext_Row["LB_REC_ID"].ToString();
                                                InDocInfo.Doc_Misc_ID = Convert.IsDBNull(_Ext_Row["LB_MISC_ID"]) ? null : _Ext_Row["LB_MISC_ID"].ToString();
                                                InDocInfo.Status_Action = Status_Action;
                                                InDocInfo.RecID = System.Guid.NewGuid().ToString();

                                                DocInfo[cnt1] = InDocInfo;
                                                cnt1 += 1;
                                            }
                                        }
                                    }
                                    InLB.InsertDocInfo = DocInfo;

                                    //Add Location
                                    Common_Lib.RealTimeService.Param_AssetLoc_Insert InAssetLoc = new Common_Lib.RealTimeService.Param_AssetLoc_Insert();
                                    InAssetLoc.name = InLB.Name.Trim();
                                    InAssetLoc.OtherDetails = "Use Type: " + InLB.PropertyType;
                                    InAssetLoc.Status_Action = Status_Action;
                                    InAssetLoc.Match_LB_ID = InLB.RecID;
                                    InAssetLoc.Match_SP_ID = "";
                                    InLB.param_InsertAssetLoc = InAssetLoc;
                                    InsertProperty[cnt] = InLB;
                                    break;
                                //----------WIP References------------

                                case "WIP":
                                    if (GridRow_ToEdit[0]["WIP_REF_TYPE"] != null)
                                    {
                                        if (GridRow_ToEdit[0]["WIP_REF_TYPE"].ToString() == "NEW")
                                        {
                                            Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile InReference = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile();
                                            InReference.LedID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_Led_ID"]) ? null : GridRow_ToEdit[0]["Item_Led_ID"].ToString();
                                            InReference.Reference = Convert.IsDBNull(GridRow_ToEdit[0]["REFERENCE"]) ? null : GridRow_ToEdit[0]["REFERENCE"].ToString();
                                            // InReference.Amount = Val(GridRow_ToEdit[0]["AMOUNT"))
                                            if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                            {
                                                if (GridRow_ToEdit[0]["Debit"] == null)
                                                { InReference.Amount = 0; }
                                                else
                                                { InReference.Amount = Convert.ToDecimal(GridRow_ToEdit[0]["Debit"]); }
                                            }
                                            else
                                            {
                                                if (GridRow_ToEdit[0]["Credit"] == null)
                                                { InReference.Amount = 0; }
                                                else { InReference.Amount = Convert.ToDecimal(GridRow_ToEdit[0]["Credit"]); }
                                            }
                                            InReference.Status_Action = Status_Action;
                                            InReference.TxnID = model.xMID;
                                            InReference.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                            InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment;
                                            InsertReferencesWIP[cnt] = InReference;
                                        }
                                    }
                                    break;
                            }
                        }

                        Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal();                        
                        InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Journal;
                        InParam.VNo = model.Txt_V_NO_Jv;
                        if (IsDate(model.Txt_V_Date_Jv.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = model.Txt_V_Date_Jv.ToString();
                        }
                        InParam.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                        InParam.Type = GridRow_ToEdit[0]["Trans_Type"].ToString();
                        if (InParam.Type.ToUpper() == "DEBIT")
                        {
                            InParam.Dr_Led_ID = GridRow_ToEdit[0]["Item_Led_ID"].ToString();
                        }
                        else
                        {
                            InParam.Dr_Led_ID = "";
                        }
                        if (InParam.Type.ToUpper() == "CREDIT")
                        {
                            InParam.Cr_Led_ID = GridRow_ToEdit[0]["Item_Led_ID"].ToString();
                        }
                        else
                        {
                            InParam.Cr_Led_ID = "";
                        }
                        if (InParam.Type.ToUpper() == "DEBIT")
                        {
                            if (GridRow_ToEdit[0]["Debit"] == null)
                            { InParam.Amount = 0; }
                            else { InParam.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                        }
                        else
                        {
                            if (GridRow_ToEdit[0]["Credit"] == null)
                            { InParam.Amount = 0; }
                            else
                            { InParam.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]); }
                        }
                        InParam.Mode = "JOURNAL";
                        if(GridRow_ToEdit[0]["Qty."].ToString() != "")
                        {
                            if (Convert.ToDouble(GridRow_ToEdit[0]["Qty."].ToString()) > 0)
                            {
                                InParam.Qty = Convert.ToDecimal(GridRow_ToEdit[0]["Qty."]);
                            }
                            else
                            {
                                InParam.Qty = 0;
                            }
                        }
                        else
                        {
                            InParam.Qty = 0;
                        }

                        InParam.Party1 = GridRow_ToEdit[0]["PartyID"].ToString();
                        InParam.Narration = model.Txt_Narration_Jv;
                        InParam.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                        InParam.Reference = model.Txt_Reference_Jv;
                        if (!Convert.ToBoolean(GridRow_ToEdit[0]["Addition"]))
                        { InParam.CrossRefID = GridRow_ToEdit[0]["CrossRefID"].ToString(); }
                        else { InParam.CrossRefID = ""; }
                        InParam.MasterTxnID = model.xMID;
                        InParam.SrNo = GridRow_ToEdit[0]["Sr."].ToString();
                        InParam.Status_Action = Status_Action;
                        InParam.RecID = Guid.NewGuid().ToString();
                        XID = InParam.RecID;

                        Insert[cnt] = InParam;

                        //Add purpose
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal InPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal();
                        InPurpose.Amount = InParam.Amount;
                        InPurpose.PurposeID = GridRow_ToEdit[0]["Pur_ID"].ToString();
                        InPurpose.RecID = Guid.NewGuid().ToString();
                        InPurpose.SrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                        InPurpose.Status_Action = Status_Action;
                        InPurpose.TxnID = model.xMID;

                        InsertPurpose[cnt] = InPurpose;

                        cnt += 1;

                    } // End Add Txn

                    InNewParam.Insert = Insert;
                    InNewParam.InsertAdvances = InAdv;
                    InNewParam.InsertDeposits = InDep;
                    InNewParam.InsertLiabilities = InLiab;
                    InNewParam.InsertPurpose = InsertPurpose;
                    InNewParam.InsertAssets = InsertAssets;
                    InNewParam.InsertGS = InsertGS;
                    InNewParam.InsertLivestock = InsertLivestock;
                    InNewParam.InsertProperty = InsertProperty;
                    InNewParam.InsertVehicles = InsertVehicles;
                    InNewParam.InsertReferencesWIP = InsertReferencesWIP;

                    //FCRA Insert Process
                    if (model.Jv_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.Jv_SplVchrReferenceSelected.Split(',');
                        var splitLength = SplVchrRefsSplit.Length;
                        if (splitLength > 0)
                        {
                            Parameter_InsertSplVchrRef_Vouchers[] InsertSplVchrRefs = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers[splitLength];
                            for (int j = 0; j < splitLength; j++)
                            {
                                Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers _SplVchr = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers();
                                _SplVchr.Task_Name = SplVchrRefsSplit[j];
                                InsertSplVchrRefs[j] = _SplVchr;
                            }
                            InNewParam.InsertSplVchrRefs = InsertSplVchrRefs;
                        }
                    }

                    if (!BASE._Journal_voucher_DBOps.InsertJournal_Txn(InNewParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Common_Lib.Messages.SaveSuccess;
                    jsonParam.result = true;
                    jsonParam.title = model.TitleX;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, CashbookGridPK = model.xMID + XID }, JsonRequestBehavior.AllowGet);
                    
                }
                string Message = "";
                Boolean IsLBIncluded = false;
                Common_Lib.RealTimeService.Param_Txn_Update_VoucherJournal EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_VoucherJournal();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit) //edit
                {
                    string XID = "";
                    Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherJournal UpMInfo = new Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherJournal();
                    UpMInfo.VNo = model.Txt_V_NO_Jv ?? "";
                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                    {
                        UpMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    { UpMInfo.TDate = model.Txt_V_Date_Jv.ToString(); }
                    UpMInfo.SubTotal = Convert.ToDouble(model.Txt_DrTotal_Jv);
                    UpMInfo.RecID = model.xMID;
                    EditParam.param_UpdateMaster = UpMInfo;
                    EditParam.MID_Delete = model.xMID;
                    EditParam.MID_DeletePurpose = model.xMID;
                    EditParam.MID_DeleteAdvances = model.xMID;
                    EditParam.MID_DeleteLiabilities = model.xMID;
                    EditParam.MID_DeleteDeposits = model.xMID;
                    EditParam.MID_ReferenceDelete = model.xMID;
                    EditParam.MID_DeleteGS = model.xMID;
                    EditParam.MID_DeleteAssets = model.xMID;
                    EditParam.MID_DeleteLS = model.xMID;
                    EditParam.MID_DeleteVehicle = model.xMID;

                    DataTable d1 = BASE._L_B_DBOps.GetIDsBytxnID(model.xMID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal);
                    string[] DelExtInfo = new string[d1.Rows.Count];
                    string[] DelDocInfo = new string[d1.Rows.Count];
                    string[] DelByLB = new string[d1.Rows.Count];
                    string[] DelComplexBuildings = new string[d1.Rows.Count];
                    int ctr = 0;

                    foreach (DataRow cRow in d1.Rows)
                    {
                        DelComplexBuildings[ctr] = cRow[0].ToString();
                        DelExtInfo[ctr] = cRow[0].ToString();
                        DelDocInfo[ctr] = cRow[0].ToString();
                        DelByLB[ctr] = cRow[0].ToString();
                        ctr += 1;
                    }
                    EditParam.DeleteComplexBuilding = DelComplexBuildings;
                    EditParam.DeleteDocumentInfo = DelDocInfo;
                    EditParam.DeleteExtendedInfo = DelExtInfo;
                    EditParam.DeleteByLB = DelByLB;
                    EditParam.MID_DeleteLandB = model.xMID;



                    int cnt = 0;
                    Parameter_InsertTRID_Advances[] InAdvances = new Common_Lib.RealTimeService.Parameter_InsertTRID_Advances[DT_JV.Rows.Count];
                    Parameter_InsertTRID_Liabilities[] InLiabilities = new Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities[DT_JV.Rows.Count];
                    Parameter_InsertTRID_Deposits[] InDeposits = new Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits[DT_JV.Rows.Count];
                    Parameter_Insert_VoucherJournal[] Insert = new Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal[DT_JV.Rows.Count];
                    Parameter_InsertPurpose_VoucherJournal[] InsertPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSRNo_GoldSilver[] InsertGS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSrNo_Assets[] InsertAssets = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSrNo_LiveStock[] InsertLivestock = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock[DT_JV.Rows.Count];
                    Parameter_InsertTRIDAndTRSrNo_Vehicles[] InsertVehicles = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles[DT_JV.Rows.Count];
                    Parameter_InsertMasterIDAndSrNo_LandAndBuilding[] InsertProperty = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding[DT_JV.Rows.Count];
                    Param_InsertTRIDAndTRSrNo_WIP_Profile[] InsertReferencesWIP = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile[DT_JV.Rows.Count];
                    string Ref_Rec_ID = "";

                    for (int I = 1; I <= DT_JV.Rows.Count; I++)//Edit Txn
                    {
                        var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + I);
                        string Cross_Ref_ID = "";
                        if (GridRow_ToEdit[0]["LB_REC_ID"].ToString().Length > 0 && GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() != "LAND & BUILDING")
                        {
                            Cross_Ref_ID = GridRow_ToEdit[0]["LB_REC_ID"].ToString();
                            if (BASE.AllowMultiuser())
                            {
                                if (Cross_Ref_ID != null)
                                {
                                    if (Cross_Ref_ID.Length > 0)
                                    {
                                        //checks if(the referred property for constt items has been sold
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(Cross_Ref_ID);
                                        if (SaleRecord != null)
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " +
                                                    Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() +
                                                    " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." +
                                                    "<br>" + "<br>" + " Please delete the record for editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.refreshgrid = true;
                                                jsonParam.closeform = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        //checks if(the referred property for constt items has been sold
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Convert.ToInt32(null), Cross_Ref_ID);
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " +
                                                    Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() +
                                                    " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" +
                                                    " Please delete the record for editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.refreshgrid = true;
                                                jsonParam.closeform = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                            } //Base.allowMultiuser() end
                        }
                        if (GridRow_ToEdit[0]["REF_REC_ID"].ToString().Length > 0)
                        {
                            Cross_Ref_ID = "'" + GridRow_ToEdit[0]["REF_REC_ID"] + "'";
                        }
                        //Add                         
                        if (Convert.ToBoolean(GridRow_ToEdit[0]["Addition"]))
                        {
                            Ref_Rec_ID = Guid.NewGuid().ToString();
                            switch (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper())
                            {
                                case "ADVANCES":
                                    Common_Lib.RealTimeService.Parameter_InsertTRID_Advances _Adv = new Common_Lib.RealTimeService.Parameter_InsertTRID_Advances();
                                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                                    {
                                        _Adv.AdvanceDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        _Adv.AdvanceDate = model.Txt_V_Date_Jv.ToString();
                                    }

                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { _Adv.Amount = 0; }
                                        else
                                        { _Adv.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { _Adv.Amount = 0; }
                                        else
                                        {
                                            _Adv.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]);
                                        }
                                    }
                                    _Adv.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    _Adv.PartyID = GridRow_ToEdit[0]["PartyID"].ToString();
                                    _Adv.Purpose = GridRow_ToEdit[0]["Pur_ID"].ToString();
                                    _Adv.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    _Adv.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    _Adv.Status_Action = Status_Action;
                                    _Adv.TxnID = model.xMID;
                                    _Adv.RecID = Ref_Rec_ID;
                                    InAdvances[cnt] = _Adv;
                                    break;

                                case "OTHER LIABILITIES":
                                    Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities _Liab = new Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities();
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { _Liab.Amount = 0; }
                                        else
                                        { _Liab.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString()); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { _Liab.Amount = 0; }
                                        else { _Liab.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    _Liab.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                                    {
                                        _Liab.LiabilityDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        _Liab.LiabilityDate = model.Txt_V_Date_Jv.ToString();
                                    }
                                    _Liab.PartyID = GridRow_ToEdit[0]["PartyID"].ToString();
                                    _Liab.Purpose = GridRow_ToEdit[0]["Pur_ID"].ToString();
                                    _Liab.RecID = Ref_Rec_ID;
                                    _Liab.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    _Liab.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    _Liab.Status_Action = Status_Action;
                                    _Liab.TxnID = model.xMID;
                                    InLiabilities[cnt] = _Liab;
                                    break;
                                case "OTHER DEPOSITS":
                                    Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits InDept = new Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits();
                                    InDept.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InDept.AgainstInsurance = "NO";
                                    InDept.PartyID = GridRow_ToEdit[0]["PartyID"].ToString();
                                    InDept.InsCompanyMiscID = "";
                                    if (IsDate(model.Txt_V_Date_Jv.ToString()))
                                    {
                                        InDept.DepositDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InDept.DepositDate = model.Txt_V_Date_Jv.ToString();
                                    }
                                    InDept.DepositPeriod = 0;
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { InDept.Amount = 0; }
                                        else
                                        {
                                            InDept.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { InDept.Amount = 0; }
                                        else
                                        { InDept.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    InDept.InterestRate = 0;
                                    InDept.Purpose = GridRow_ToEdit[0]["Pur_ID"].ToString();
                                    InDept.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InDept.TxnID = model.xMID;
                                    InDept.Status_Action = Status_Action;
                                    InDept.RecID = Ref_Rec_ID;
                                    InDeposits[cnt] = InDept;
                                    break;
                                case "GOLD":
                                case "SILVER":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver InGS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver();
                                    InGS.Type = Convert.IsDBNull(GridRow_ToEdit[0]["Item_Profile"]) ? null : GridRow_ToEdit[0]["Item_Profile"].ToString();
                                    InGS.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InGS.DescMiscID = Convert.IsDBNull(GridRow_ToEdit[0]["GS_DESC_MISC_ID"]) ? null : GridRow_ToEdit[0]["GS_DESC_MISC_ID"].ToString();
                                    InGS.Weight = Convert.IsDBNull(GridRow_ToEdit[0]["GS_ITEM_WEIGHT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["GS_ITEM_WEIGHT"]);
                                    InGS.LocationID = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InGS.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InGS.TxnID = model.xMID;
                                    InGS.TxnSrno = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { InGS.Amount = 0; }
                                        else { InGS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString()); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { InGS.Amount = 0; }
                                        else { InGS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    InGS.Status_Action = Status_Action;
                                    InGS.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertGS[cnt] = InGS;
                                    break;
                                case "OTHER ASSETS":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets InAsset = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets();
                                    InAsset.AssetType = Convert.IsDBNull(GridRow_ToEdit[0]["AI_TYPE"]) ? null : GridRow_ToEdit[0]["AI_TYPE"].ToString();
                                    InAsset.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InAsset.Make = Convert.IsDBNull(GridRow_ToEdit[0]["AI_MAKE"]) ? null : GridRow_ToEdit[0]["AI_MAKE"].ToString();
                                    InAsset.Model = Convert.IsDBNull(GridRow_ToEdit[0]["AI_MODEL"]) ? null : GridRow_ToEdit[0]["AI_MODEL"].ToString();
                                    InAsset.SrNo = Convert.IsDBNull(GridRow_ToEdit[0]["AI_SERIAL_NO"]) ? null : GridRow_ToEdit[0]["AI_SERIAL_NO"].ToString();
                                    if (Convert.IsDBNull(GridRow_ToEdit[0]["Rate"]))
                                    {
                                        InAsset.Rate = Convert.ToDouble(null);
                                    }
                                    else
                                    {
                                        InAsset.Rate = Convert.ToDouble(GridRow_ToEdit[0]["Rate"]);
                                    }
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["AI_PUR_DATE"]) ? null : GridRow_ToEdit[0]["AI_PUR_DATE"].ToString()))
                                    {
                                        InAsset.PurchaseDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["AI_PUR_DATE"]) ? null : GridRow_ToEdit[0]["AI_PUR_DATE"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InAsset.PurchaseDate = Convert.IsDBNull(GridRow_ToEdit[0]["AI_PUR_DATE"]) ? null : GridRow_ToEdit[0]["AI_PUR_DATE"].ToString();
                                    }
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { InAsset.InsAmount = 0; }
                                        else { InAsset.InsAmount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString()); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { InAsset.InsAmount = 0; }
                                        else
                                        { InAsset.InsAmount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    InAsset.PurchaseAmount = InAsset.AssetType.ToUpper() == "ASSET" ? InAsset.InsAmount : 0;
                                    InAsset.Warranty = Convert.IsDBNull(GridRow_ToEdit[0]["AI_WARRANTY"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["AI_WARRANTY"]);
                                    InAsset.Quantity = Convert.IsDBNull(GridRow_ToEdit[0]["Qty."]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["Qty."]);
                                    InAsset.LocationId = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InAsset.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InAsset.TxnID = model.xMID;
                                    InAsset.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    InAsset.Status_Action = Status_Action;
                                    InAsset.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertAssets[cnt] = InAsset;
                                    break;
                                case "LIVESTOCK":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock InLS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock();
                                    InLS.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InLS.Name = Convert.IsDBNull(GridRow_ToEdit[0]["LS_NAME"]) ? null : GridRow_ToEdit[0]["LS_NAME"].ToString();
                                    InLS.Year = Convert.IsDBNull(GridRow_ToEdit[0]["LS_BIRTH_YEAR"]) ? null : GridRow_ToEdit[0]["LS_BIRTH_YEAR"].ToString();
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { InLS.Amount = 0; }
                                        else { InLS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { InLS.Amount = 0; }
                                        else { InLS.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    InLS.Insurance = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INSURANCE"]) ? null : GridRow_ToEdit[0]["LS_INSURANCE"].ToString();
                                    InLS.InsuranceID = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INSURANCE_ID"]) ? null : GridRow_ToEdit[0]["LS_INSURANCE_ID"].ToString();
                                    InLS.PolicyNo = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_POLICY_NO"]) ? null : GridRow_ToEdit[0]["LS_INS_POLICY_NO"].ToString();
                                    InLS.InsAmount = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LS_INS_AMT"]);
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_DATE"]) ? null : GridRow_ToEdit[0]["LS_INS_DATE"].ToString()))
                                    {
                                        InLS.InsuranceDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_DATE"]) ? null : GridRow_ToEdit[0]["LS_INS_DATE"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLS.InsuranceDate = Convert.IsDBNull(GridRow_ToEdit[0]["LS_INS_DATE"]) ? null : GridRow_ToEdit[0]["LS_INS_DATE"].ToString();
                                    }
                                    InLS.LocationID = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InLS.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InLS.TxnID = model.xMID;
                                    InLS.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    InLS.Status_Action = Status_Action;
                                    InLS.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertLivestock[cnt] = InLS;
                                    break;
                                case "VEHICLES":
                                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles InVeh = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles();
                                    InVeh.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InVeh.Make = Convert.IsDBNull(GridRow_ToEdit[0]["VI_MAKE"]) ? null : GridRow_ToEdit[0]["VI_MAKE"].ToString();
                                    InVeh.Model = Convert.IsDBNull(GridRow_ToEdit[0]["VI_MODEL"]) ? null : GridRow_ToEdit[0]["VI_MODEL"].ToString();
                                    InVeh.Reg_No_Pattern = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_NO_PATTERN"]) ? null : GridRow_ToEdit[0]["VI_REG_NO_PATTERN"].ToString();
                                    InVeh.Reg_No = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_NO"]) ? null : GridRow_ToEdit[0]["VI_REG_NO"].ToString();
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_DATE"]) ? null : GridRow_ToEdit[0]["VI_REG_DATE"].ToString()))
                                    {
                                        InVeh.RegDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_DATE"]) ? null : GridRow_ToEdit[0]["VI_REG_DATE"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InVeh.RegDate = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_DATE"]) ? null : GridRow_ToEdit[0]["VI_REG_DATE"].ToString();
                                    }
                                    InVeh.Ownership = Convert.IsDBNull(GridRow_ToEdit[0]["VI_OWNERSHIP"]) ? null : GridRow_ToEdit[0]["VI_OWNERSHIP"].ToString();
                                    if (GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"] != null && !Convert.IsDBNull(GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"]))
                                    {
                                        InVeh.Ownership_AB_ID = GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"].ToString().Length == 0 ? null : GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"].ToString();
                                    }

                                    InVeh.Doc_RC_Book = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_RC_BOOK"]) ? null : GridRow_ToEdit[0]["VI_DOC_RC_BOOK"].ToString();
                                    InVeh.Doc_Affidavit = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_AFFIDAVIT"]) ? null : GridRow_ToEdit[0]["VI_DOC_AFFIDAVIT"].ToString();
                                    InVeh.Doc_Will = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_WILL"]) ? null : GridRow_ToEdit[0]["VI_DOC_WILL"].ToString();
                                    InVeh.Doc_TRF_Letter = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_TRF_LETTER"]) ? null : GridRow_ToEdit[0]["VI_DOC_TRF_LETTER"].ToString();
                                    InVeh.DOC_FU_Letter = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_FU_LETTER"]) ? null : GridRow_ToEdit[0]["VI_DOC_FU_LETTER"].ToString();
                                    InVeh.Doc_Is_Others = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_OTHERS"]) ? null : GridRow_ToEdit[0]["VI_DOC_OTHERS"].ToString();
                                    InVeh.Doc_Others_Name = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_NAME"]) ? null : GridRow_ToEdit[0]["VI_DOC_NAME"].ToString();
                                    if (Convert.IsDBNull(GridRow_ToEdit[0]["VI_INSURANCE_ID"]))
                                    {
                                        InVeh.Insurance_ID = null;
                                    }
                                    else if (GridRow_ToEdit[0]["VI_INSURANCE_ID"] == null)
                                    {
                                        InVeh.Insurance_ID = null;
                                    }

                                    else if (GridRow_ToEdit[0]["VI_INSURANCE_ID"].ToString().Length == 0)
                                    {
                                        InVeh.Insurance_ID = null;
                                    }
                                    else
                                    {
                                        InVeh.Insurance_ID = GridRow_ToEdit[0]["VI_INSURANCE_ID"].ToString();
                                    }
                                    InVeh.Ins_Policy_No = Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_POLICY_NO"]) ? null : GridRow_ToEdit[0]["VI_INS_POLICY_NO"].ToString();
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]) ? null : GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"].ToString()))
                                    {
                                        InVeh.Ins_Expiry_Date = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]) ? null : GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InVeh.Ins_Expiry_Date = Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]) ? null : GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"].ToString();
                                    }
                                    InVeh.Location_ID = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                                    InVeh.Other_Details = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                                    InVeh.TxnID = model.xMID;
                                    InVeh.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);

                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { InVeh.Amount = 0; }
                                        else
                                        {
                                            InVeh.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { InVeh.Amount = 0; }
                                        else
                                        {
                                            InVeh.Amount = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString());
                                        }
                                    }
                                    InVeh.Status_Action = Status_Action;
                                    InVeh.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal;
                                    InsertVehicles[cnt] = InVeh;
                                    break;

                                case "LAND & BUILDING":

                                    string PartyID = "NULL";
                                    if (GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"].ToString() != "NULL")
                                    {
                                        PartyID = "'" + GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"] + "'";
                                    }
                                    if (GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"].ToString().Length == 0)
                                    {
                                        PartyID = "NULL";
                                    }
                                    Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding InLB = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding();
                                    InLB.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                                    InLB.PropertyType = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_TYPE"]) ? null : GridRow_ToEdit[0]["LB_PRO_TYPE"].ToString();
                                    InLB.Category = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_CATEGORY"]) ? null : GridRow_ToEdit[0]["LB_PRO_CATEGORY"].ToString();
                                    InLB.Use = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_USE"]) ? null : GridRow_ToEdit[0]["LB_PRO_USE"].ToString();
                                    InLB.Name = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_NAME"]) ? null : GridRow_ToEdit[0]["LB_PRO_NAME"].ToString().Trim();
                                    InLB.Address = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_ADDRESS"]) ? null : GridRow_ToEdit[0]["LB_PRO_ADDRESS"].ToString();
                                    InLB.LB_Add1 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS1"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS1"].ToString();
                                    InLB.LB_Add2 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS2"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS2"].ToString();
                                    InLB.LB_Add3 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS3"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS3"].ToString();
                                    InLB.LB_Add4 = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS4"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS4"].ToString();
                                    InLB.LB_CityID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CITY_ID"]) ? null : GridRow_ToEdit[0]["LB_CITY_ID"].ToString();
                                    InLB.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e";
                                    InLB.LB_DisttID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DISTRICT_ID"]) ? null : GridRow_ToEdit[0]["LB_DISTRICT_ID"].ToString();
                                    InLB.LB_PinCode = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PINCODE"]) ? null : GridRow_ToEdit[0]["LB_PINCODE"].ToString();
                                    InLB.LB_StateID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_STATE_ID"]) ? null : GridRow_ToEdit[0]["LB_STATE_ID"].ToString();
                                    InLB.Ownership = Convert.IsDBNull(GridRow_ToEdit[0]["LB_OWNERSHIP"]) ? null : GridRow_ToEdit[0]["LB_OWNERSHIP"].ToString();
                                    InLB.Owner_Party_ID = PartyID;
                                    InLB.SurveyNo = Convert.IsDBNull(GridRow_ToEdit[0]["LB_SURVEY_NO"]) ? null : GridRow_ToEdit[0]["LB_SURVEY_NO"].ToString();
                                    InLB.TotalArea = Convert.IsDBNull(GridRow_ToEdit[0]["LB_TOT_P_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_TOT_P_AREA"]);
                                    InLB.ConstructedArea = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CON_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_CON_AREA"]);
                                    InLB.ConstructionYear = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CON_YEAR"]) ? null : GridRow_ToEdit[0]["LB_CON_YEAR"].ToString();
                                    InLB.RCCRoof = Convert.IsDBNull(GridRow_ToEdit[0]["LB_RCC_ROOF"]) ? null : GridRow_ToEdit[0]["LB_RCC_ROOF"].ToString();
                                    InLB.DepositAmount = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DEPOSIT_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_DEPOSIT_AMT"]);
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PAID_DATE"]) ? null : GridRow_ToEdit[0]["LB_PAID_DATE"].ToString()))
                                    {
                                        InLB.PaymentDate = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PAID_DATE"]) ? null : GridRow_ToEdit[0]["LB_PAID_DATE"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLB.PaymentDate = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PAID_DATE"]) ? null : GridRow_ToEdit[0]["LB_PAID_DATE"].ToString();
                                    }
                                    InLB.MonthlyRent = Convert.IsDBNull(GridRow_ToEdit[0]["LB_MONTH_RENT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_RENT"]);
                                    InLB.MonthlyOtherExpenses = Convert.IsDBNull(GridRow_ToEdit[0]["LB_MONTH_O_PAYMENTS"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_O_PAYMENTS"]);
                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_FROM"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString()))
                                    {
                                        InLB.PeriodFrom = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_FROM"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLB.PeriodFrom = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_FROM"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString();
                                    }

                                    if (IsDate(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_TO"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString()))
                                    {
                                        InLB.PeriodTo = Convert.ToDateTime(Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_TO"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString()).ToString(BASE._Server_Date_Format_Short);
                                    }
                                    else
                                    {
                                        InLB.PeriodTo = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_TO"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString();
                                    }
                                    InLB.OtherDocs = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DOC_OTHERS"]) ? null : GridRow_ToEdit[0]["LB_DOC_OTHERS"].ToString();
                                    InLB.DocNames = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DOC_NAME"]) ? null : GridRow_ToEdit[0]["LB_DOC_NAME"].ToString();
                                    if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                    {
                                        if (GridRow_ToEdit[0]["Debit"] == null) { InLB.Value = 0; }
                                        else { InLB.Value = Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString()); }
                                    }
                                    else
                                    {
                                        if (GridRow_ToEdit[0]["Credit"] == null) { InLB.Value = 0; }
                                        else { InLB.Value = Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString()); }
                                    }
                                    InLB.OtherDetails = Convert.IsDBNull(GridRow_ToEdit[0]["LB_OTHER_DETAIL"]) ? null : GridRow_ToEdit[0]["LB_OTHER_DETAIL"].ToString();
                                    InLB.MasterID = model.xMID;
                                    InLB.SrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                    InLB.Status_Action = Status_Action;
                                    InLB.RecID = Convert.IsDBNull(GridRow_ToEdit[0]["LB_REC_ID"]) ? null : GridRow_ToEdit[0]["LB_REC_ID"].ToString();
                                    //Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[] ExtInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[LB_EXTENDED_PROPERTY_TABLE.Rows.Count];

                                    Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[] ExtInfo;
                                    //EXTENSIONS 
                                    int ctr1 = 0;
                                    if (LB_EXTENDED_PROPERTY_TABLE != null)
                                    {
                                        ExtInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[LB_EXTENDED_PROPERTY_TABLE.Rows.Count];
                                        foreach (DataRow _Ext_Row in LB_EXTENDED_PROPERTY_TABLE.Rows)
                                        {
                                            if (_Ext_Row["LB_REC_ID"].ToString() == GridRow_ToEdit[0]["LB_REC_ID"].ToString())
                                            {
                                                Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding InEInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding();
                                                InEInfo.LB_Rec_ID = Convert.IsDBNull(_Ext_Row["LB_REC_ID"]) ? null : _Ext_Row["LB_REC_ID"].ToString();
                                                InEInfo.SrNo = Convert.IsDBNull(_Ext_Row["LB_SR_NO"]) ? null : _Ext_Row["LB_SR_NO"].ToString();
                                                InEInfo.Inst_ID = Convert.IsDBNull(_Ext_Row["LB_INS_ID"]) ? null : _Ext_Row["LB_INS_ID"].ToString();
                                                InEInfo.TotalArea = Convert.ToDouble(_Ext_Row["LB_TOT_P_AREA"].ToString());
                                                InEInfo.ConstructedArea = Convert.ToDouble(_Ext_Row["LB_CON_AREA"].ToString());
                                                InEInfo.ConYear = Convert.IsDBNull(_Ext_Row["LB_CON_YEAR"]) ? null : _Ext_Row["LB_CON_YEAR"].ToString();
                                                if (IsDate(Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"].ToString()))
                                                {
                                                    InEInfo.MOU_Date = Convert.ToDateTime(Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"]).ToString(BASE._Server_Date_Format_Short);
                                                }
                                                else
                                                {
                                                    InEInfo.MOU_Date = Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"].ToString();
                                                }
                                                InEInfo.Value = Convert.ToDouble(_Ext_Row["LB_VALUE"]);
                                                InEInfo.OtherDetails = Convert.IsDBNull(_Ext_Row["LB_OTHER_DETAIL"]) ? null : _Ext_Row["LB_OTHER_DETAIL"].ToString();
                                                InEInfo.Status_Action = Status_Action;
                                                InEInfo.RecID = System.Guid.NewGuid().ToString();
                                                ExtInfo[ctr1] = InEInfo;
                                                ctr1 += 1;
                                            }
                                        }
                                        InLB.InsertExtInfo = ExtInfo;
                                    }

                                    //DOCS
                                    Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[] DocInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[0];
                                    ctr1 = 0;
                                    if (LB_DOCS_ARRAY != null)
                                    {
                                        DocInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[LB_DOCS_ARRAY.Rows.Count];
                                        foreach (DataRow _Ext_Row in LB_DOCS_ARRAY.Rows)
                                        {
                                            if (_Ext_Row["LB_REC_ID"].ToString() == GridRow_ToEdit[0]["LB_REC_ID"].ToString())
                                            {
                                                Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding InDInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding();
                                                InDInfo.LB_Rec_ID = Convert.IsDBNull(_Ext_Row["LB_REC_ID"]) ? null : _Ext_Row["LB_REC_ID"].ToString();
                                                InDInfo.Doc_Misc_ID = Convert.IsDBNull(_Ext_Row["LB_MISC_ID"]) ? null : _Ext_Row["LB_MISC_ID"].ToString();
                                                InDInfo.Status_Action = Status_Action;
                                                InDInfo.RecID = System.Guid.NewGuid().ToString();
                                                DocInfo[ctr1] = InDInfo;
                                                ctr1 += 1;
                                            }
                                        }
                                        InLB.InsertDocInfo = DocInfo;
                                    }
                                    DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal, Convert.IsDBNull(GridRow_ToEdit[0]["LB_REC_ID"]) ? null : GridRow_ToEdit[0]["LB_REC_ID"].ToString());
                                    if (Locations == null)
                                    {
                                        jsonParam.message = Common_Lib.Messages.SomeError;
                                        jsonParam.result = false;
                                        jsonParam.title = "Error!!";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

                                    }
                                    if (Locations.Rows.Count > 0)
                                    {
                                        IsLBIncluded = true;
                                    }
                                    InsertProperty[cnt] = InLB;
                                    break;
                                //----------WIP References------------
                                case "WIP":


                                    if (GridRow_ToEdit[0]["WIP_REF_TYPE"].ToString() == "NEW")
                                    {
                                        Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile InReference = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile();
                                        InReference.LedID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_Led_ID"]) ? null : GridRow_ToEdit[0]["Item_Led_ID"].ToString();
                                        InReference.Reference = Convert.IsDBNull(GridRow_ToEdit[0]["REFERENCE"]) ? null : GridRow_ToEdit[0]["REFERENCE"].ToString();
                                        


                                        if (GridRow_ToEdit[0]["Trans_Type"].ToString() == "DEBIT")
                                        {
                                            if (GridRow_ToEdit[0]["Debit"] == null) { InReference.Amount = 0; }
                                            else
                                            { InReference.Amount = Convert.ToDecimal(GridRow_ToEdit[0]["Debit"].ToString()); }
                                        }
                                        else
                                        {
                                            if (GridRow_ToEdit[0]["Credit"] == null) { InReference.Amount = 0; }
                                            else
                                            { InReference.Amount = Convert.ToDecimal(GridRow_ToEdit[0]["Credit"].ToString()); }

                                        }
                                        InReference.Status_Action = Status_Action;
                                        InReference.TxnID = model.xMID;
                                        InReference.TxnSrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                                        InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment;
                                        InsertReferencesWIP[cnt] = InReference;
                                    }
                                    break;
                            }
                        }                                                
                        //End Addition

                        Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal();                     
                        InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Journal;
                        InParam.VNo = model.Txt_V_NO_Jv;
                        if (IsDate(model.Txt_V_Date_Jv.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_Jv).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = model.Txt_V_Date_Jv.ToString();
                        }
                        InParam.ItemID = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                        InParam.Type = GridRow_ToEdit[0]["Trans_Type"].ToString();
                        if (InParam.Type.ToUpper() == "DEBIT")
                        {
                            InParam.Dr_Led_ID = GridRow_ToEdit[0]["Item_Led_ID"].ToString();
                        }
                        else
                        {
                            InParam.Dr_Led_ID = "";
                        }
                        if (InParam.Type.ToUpper() == "CREDIT")
                        {
                            InParam.Cr_Led_ID = GridRow_ToEdit[0]["Item_Led_ID"].ToString();
                        }
                        else
                        {
                            InParam.Cr_Led_ID = "";
                        }
                        if (InParam.Type.ToUpper() == "DEBIT")
                        {
                            InParam.Amount = GridRow_ToEdit[0]["Debit"] == null ? 0 : Convert.ToDouble(GridRow_ToEdit[0]["Debit"].ToString());
                        }
                        else
                        {
                            InParam.Amount = GridRow_ToEdit[0]["Credit"] == null ? 0 : Convert.ToDouble(GridRow_ToEdit[0]["Credit"].ToString());
                        }
                        InParam.Mode = "JOURNAL";
                        if (GridRow_ToEdit[0]["Qty."].ToString() == "")
                        {
                            InParam.Qty = 0;
                        }
                        else
                        {
                            if (Convert.ToDouble(GridRow_ToEdit[0]["Qty."].ToString()) > 0)
                            {
                                InParam.Qty = Convert.ToDecimal(GridRow_ToEdit[0]["Qty."].ToString());
                            }
                            else
                            {
                                InParam.Qty = 0;
                            }
                        }
                        
                        InParam.Party1 = GridRow_ToEdit[0]["PartyID"].ToString();
                        InParam.Narration = model.Txt_Narration_Jv;
                        InParam.Remarks = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                        InParam.Reference = model.Txt_Reference_Jv;
                        if (!Convert.ToBoolean(GridRow_ToEdit[0]["Addition"]))
                        {
                            InParam.CrossRefID = GridRow_ToEdit[0]["CrossRefID"].ToString();
                        }
                        else
                        {
                            InParam.CrossRefID = "";
                        }
                        InParam.MasterTxnID = model.xMID;
                        InParam.SrNo = GridRow_ToEdit[0]["Sr."].ToString();
                        InParam.Status_Action = Status_Action;
                        InParam.RecID = Guid.NewGuid().ToString();
                        XID = InParam.RecID;
                        Insert[cnt] = InParam;

                        //Add purpose
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal InPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal();
                        InPurpose.Amount = InParam.Amount;
                        InPurpose.PurposeID = GridRow_ToEdit[0]["Pur_ID"].ToString();
                        InPurpose.RecID = Guid.NewGuid().ToString();
                        InPurpose.SrNo = Convert.IsDBNull(GridRow_ToEdit[0]["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(GridRow_ToEdit[0]["Sr."]);
                        InPurpose.Status_Action = Status_Action;
                        InPurpose.TxnID = model.xMID;
                        InsertPurpose[cnt] = InPurpose;
                        cnt += 1;
                    } //for loop end 
                    EditParam.InsertAdvances = InAdvances;
                    EditParam.InsertDeposits = InDeposits;
                    EditParam.InsertLiabilities = InLiabilities;
                    EditParam.Insert = Insert;
                    EditParam.InsertPurpose = InsertPurpose;
                    EditParam.InsertAssets = InsertAssets;
                    EditParam.InsertGS = InsertGS;
                    EditParam.InsertLivestock = InsertLivestock;
                    EditParam.InsertProperty = InsertProperty;
                    EditParam.InsertVehicles = InsertVehicles;
                    EditParam.InsertReferencesWIP = InsertReferencesWIP;

                    //FCRA Update Process               
                    if (model.Jv_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.Jv_SplVchrReferenceSelected.Split(',');
                        var splitLength = SplVchrRefsSplit.Length;
                        if (splitLength > 0)
                        {
                            Parameter_InsertSplVchrRef_Vouchers[] UpdateSplVchrRefs = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers[splitLength];
                            for (int j = 0; j < splitLength; j++)
                            {
                                Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers _SplVchr = new Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers();
                                _SplVchr.Task_Name = SplVchrRefsSplit[j];
                                UpdateSplVchrRefs[j] = _SplVchr;
                            }
                            EditParam.UpdateSplVchrRefs = UpdateSplVchrRefs;
                        }
                    }

                    if (!BASE._Journal_voucher_DBOps.UpdateJournal_Txn(EditParam)) {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if (IsLBIncluded)
                    {
                        Message = "<br>" + "<br>" + "No Subsequent Changes have been made in Location(s) mapped to the property/properties mentioned in the current voucher." + "<br>" + "User may make the required changes manually from Profile - > Core - > Locations.";
                    }

                    jsonParam.message = Common_Lib.Messages.UpdateSuccess + Message;
                    jsonParam.result = true;
                    jsonParam.title = model.TitleX;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, CashbookGridPK = model.xMID + XID }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Param_Txn_Delete_VoucherJournal DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_VoucherJournal();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete) { //DELETE
                    foreach (DataRow XRow in DT_JV.Rows)
                    {
                        if (XRow["Item_Profile"].ToString() == "LAND & BUILDING" || XRow["Item_Profile"].ToString() == "OTHER ASSETS" ||
                            (XRow["ITEM_VOUCHER_TYPE"].ToString().Trim().ToUpper() == "LAND & BUILDING" &&
                            XRow["Item_Profile"].ToString().ToUpper() != "LAND & BUILDING"))
                        {
                            if (BASE.IsInsuranceAudited()) {
                                jsonParam.message = "Insurance Related Assets Cannot be Deleted After The Completion of Insurance Audit";
                                jsonParam.result = false;
                                jsonParam.title = "Information...";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    DataTable LB_REC_ID = BASE._L_B_DBOps.GetIDsBytxnID(model.xMID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment);
                    DataTable TblRecID = LB_REC_ID.Copy();
                    //check any L&B Expenses done on basis of requested deletion entries 
                    foreach (DataRow _RECROW in TblRecID.Rows)
                    {
                        DataTable Dependent_payments = BASE._Payment_DBOps.GetTxnDetailsByRefID(_RECROW[0].ToString());
                        if (Dependent_payments.Rows.Count > 0)
                        {
                            DateTime TrDate = Convert.ToDateTime(Dependent_payments.Rows[0]["TR_Date"]);
                            jsonParam.message = "A Construction Expense Entry of Rs." + Dependent_payments.Rows[0]["TR_AMOUNT"] + " with date " +
                                TrDate.ToString("dd-MMM-yyy") + " is dependednt on this voucher. Please delete that entry first!!";
                            jsonParam.result = false;
                            jsonParam.title = model.TitleX;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteGS = model.xMID;
                    DelParam.MID_DeleteAssets = model.xMID;
                    DelParam.MID_DeleteLS = model.xMID;
                    DelParam.MID_DeleteVehicle = model.xMID;
                    DelParam.MID_ReferenceDelete = model.xMID;

                    //Get Rec ID for Curr TxnMaster ID
                    string[] DelComplexBuildings = new string[TblRecID.Rows.Count + 1];
                    string[] DelExtInfo = new string[TblRecID.Rows.Count + 1];
                    string[] DelDocInfo = new string[TblRecID.Rows.Count + 1];
                    string[] DelByLB = new string[TblRecID.Rows.Count + 1];
                    int ctr1 = 1;
                    foreach (DataRow _RECROW in TblRecID.Rows)
                    {
                        DelComplexBuildings[ctr1] = _RECROW["REC_ID"].ToString();
                        DelExtInfo[ctr1] = _RECROW["REC_ID"].ToString();
                        DelDocInfo[ctr1] = _RECROW["REC_ID"].ToString();
                        DelByLB[ctr1] = _RECROW["REC_ID"].ToString();
                        ctr1 += 1;
                    }
                    DelParam.DeleteComplexBuilding = DelComplexBuildings;
                    DelParam.DeleteDocumentInfo = DelDocInfo;
                    DelParam.DeleteExtendedInfo = DelExtInfo;
                    DelParam.DeleteByLB = DelByLB;
                    DelParam.MID_DeleteLandB = model.xMID;
                    DelParam.MID_DeleteAdvances = model.xMID;
                    DelParam.MID_DeleteLiabilities = model.xMID;
                    DelParam.MID_DeleteDeposits = model.xMID;
                    DelParam.MID_DeletePurpose = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;

                    if (!BASE._Journal_voucher_DBOps.DeleteJournal_Txn(DelParam)) {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.result = false;
                        jsonParam.title = "Error!!";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                    jsonParam.result = true;
                    jsonParam.title = model.TitleX;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
            }//try end point
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
        public string FindLocationUsage(Common.Navigation_Mode TAG, string PropertyID = "", bool Exclude_Sold_TF = true, Boolean Adjusted = false)
        {
            string Message = "";
            DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal, PropertyID);
            foreach (DataRow cRow in Locations.Rows)
            {
                string LocationID = cRow[0].ToString();
                string UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF);
                Boolean DeleteAllow = true;
                if (UsedPage.Length > 0) { DeleteAllow = false; }
                if (!DeleteAllow)
                {
                    if (Adjusted)
                    {
                        Message = "Property Adjusted in this Voucher is being used in Another Page as  Location. . . !" +
                            "<br>" + "<br>" + "Name : " + UsedPage;
                    }
                    else
                    {
                        if (TAG == Common_Lib.Common.Navigation_Mode._Delete)
                        {
                            Message = "Can'tDelete . . . !" + "<br>" + "<br>" + "Property Created in this Voucher is being used in Another Page as Location. . . !" +
                                "<br>" + "<br>" + "Name : " + UsedPage;
                        }
                        else
                        {
                            Message = "Can'tEdit . . . !" + "<br>" + "<br>" + "Property Created in this Voucher is being used in Another Page as Location. . . !" +
                                "<br>" + "<br>" + "Name : " + UsedPage;
                        }
                    }
                    break;
                }
            }
            return Message;
        }
        #endregion
        public ActionResult Frm_Win_Journal_Item_Listing_Grid()
        {
            ViewData["Txt_DiffAmt"] = Txt_DiffAmt ?? 0.00;
            ViewData["Txt_DrTotal"] = Txt_DrTotal ?? 0.00;
            ViewData["Txt_CrTotal"] = Txt_CrTotal ?? 0.00;
            ViewData["itemGrid_RowCount_Jv"] = DT_JV.Rows.Count;
            return PartialView(DT_JV);
        }

        #region Fill Items in Grid

        [HttpPost]
        public void Fill_Item_Grid(JournalVoucher_Item xfrm)
        {
            bool Delete_Action = false;
            xfrm.Tag_JV_Itm = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), xfrm.ActionMethod_JV_Itm);
            if (xfrm.Tag_JV_Itm == Common.Navigation_Mode._New)
            {
                DataView dv = DT_JV.DefaultView;
                dv.Sort = "Sr.";
                DT_JV = dv.ToTable();
                DataRow ROW = DT_JV.NewRow();
                if (DT_JV.Rows.Count == 0)
                {
                    ROW["Sr."] = 1;
                }
                else
                {
                    ROW["Sr."] = Convert.ToInt32(DT_JV.Rows[DT_JV.Rows.Count - 1]["Sr."]) + 1;
                }
                ROW["Item Name"] = xfrm.GLookUp_ItemName_JV_Itm;
                ROW["Item_ID"] = xfrm.GLookUp_ItemList_JV_Itm;
                if (xfrm.iCond_Ledger_ID_JV_Itm != "00000" && xfrm.Cmb_RefType_JV_Itm == "New")
                {
                    if (xfrm.Txt_Amt_JV_Itm > xfrm.iMinValue_JV_Itm && xfrm.Txt_Amt_JV_Itm <= xfrm.iMaxValue_JV_Itm)
                    {
                        ROW["Item_Led_ID"] = xfrm.iCond_Ledger_ID_JV_Itm;
                    }
                    else
                    {
                        ROW["Item_Led_ID"] = xfrm.iLed_ID_JV_Itm;
                    }
                }
                else
                {
                    ROW["Item_Led_ID"] = xfrm.iLed_ID_JV_Itm;
                }
                if (xfrm.RdAction_JV_Itm == 0)
                {
                    ROW["Trans_Type"] = "DEBIT";
                }
                else
                {
                    ROW["Trans_Type"] = "CREDIT";
                }
                ROW["Item_Profile"] = xfrm.iProfile_JV_Itm;
                ROW["Item_Party_Req"] = xfrm.iParty_Req_JV_Itm;
                ROW["Head"] = xfrm.BE_Item_Head_JV_Itm;
                if (string.IsNullOrWhiteSpace(xfrm.GLookUp_PartyList_JV_Itm) == false) {
                    ROW["Party"] = xfrm.GLookUp_PartyName_JV_Itm;
                }
                if (xfrm.Txt_Qty_JV_Itm > 0) {
                    ROW["Qty."] = Convert.IsDBNull(xfrm.Txt_Qty_JV_Itm) ? Convert.ToDecimal(null): Convert.ToDecimal(xfrm.Txt_Qty_JV_Itm);
                }
                if (xfrm.RdAction_JV_Itm == 0) {
                    ROW["Debit"] = xfrm.Txt_Amt_JV_Itm;
                    ROW["Credit"] = "";
                }
                if (xfrm.RdAction_JV_Itm == 1) {
                    ROW["Credit"] = xfrm.Txt_Amt_JV_Itm;
                    ROW["Debit"] = "";
                }
                ROW["Remarks"] = xfrm.Txt_Remarks_JV_Itm;
                ROW["PUR_ID"] = xfrm.GLookUp_PurList_JV_Itm;
                ROW["Purpose"] = xfrm.GLookUp_PurName_JV_Itm;
                ROW["PartyID"] = xfrm.GLookUp_PartyList_JV_Itm;
                ROW["Item_Voucher_Type"] = xfrm.Txt_ItemNature_JV_Itm;
                ROW["CrossReference"] = xfrm.TXT_Reference_JV_Itm;

                ROW["RefItem_RecEditOn"] = xfrm.RefItem_RecEditOn_JV_Itm;
                if(xfrm.Party_RecEditOn_JV_Itm == null)
                {
                    ROW["Party_RecEditOn"] = System.DBNull.Value;
                }
                else
                {
                    ROW["Party_RecEditOn"] = xfrm.Party_RecEditOn_JV_Itm;
                }
                
                if (xfrm.Cmb_RefType_JV_Itm == "New") {
                    ROW["Addition"] = true;
                }
                else {
                    ROW["Addition"] = false;
                }

                if (xfrm.Cmb_RefType_JV_Itm == "Existing") {
                    if (xfrm.Cross_RefID_JV_Itm != null) {
                        ROW["CrossRefID"] = xfrm.Cross_RefID_JV_Itm;
                    }
                }
                else
                {
                    ROW["CrossRefID"] = "";
                }
                ROW["LB_REC_ID"] = xfrm.LB_REC_ID_JV_Itm;

                if (xfrm.iProfile_JV_Itm == "GOLD" || xfrm.iProfile_JV_Itm == "SILVER")
                {
                    ROW["GS_DESC_MISC_ID"] = xfrm.GS_DESC_MISC_ID_JV_Itm;
                    ROW["GS_ITEM_WEIGHT"] = Convert.IsDBNull(xfrm.GS_ITEM_WEIGHT_JV_Itm) ? Convert.ToDouble(null) : Convert.ToDouble(xfrm.GS_ITEM_WEIGHT_JV_Itm);
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;
                }

                if (xfrm.iProfile_JV_Itm == "OTHER ASSETS") {
                    ROW["AI_TYPE"] = xfrm.AI_TYPE_JV_Itm;
                    ROW["AI_MAKE"] = xfrm.AI_MAKE_JV_Itm;
                    ROW["AI_MODEL"] = xfrm.AI_MODEL_JV_Itm;
                    ROW["AI_SERIAL_NO"] = xfrm.AI_SERIAL_NO_JV_Itm;
                    if (IsDate(xfrm.AI_PUR_DATE_JV_Itm)) {
                        ROW["AI_PUR_DATE"] = xfrm.AI_PUR_DATE_JV_Itm;
                    }
                    ROW["AI_WARRANTY"] = xfrm.AI_WARRANTY_JV_Itm ?? 0;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;
                }
                if (xfrm.iProfile_JV_Itm == "LIVESTOCK") {
                    ROW["LS_NAME"] = xfrm.LS_NAME_JV_Itm;
                    ROW["LS_BIRTH_YEAR"] = xfrm.LS_BIRTH_YEAR_JV_Itm;
                    ROW["LS_INSURANCE"] = xfrm.LS_INSURANCE_JV_Itm;
                    ROW["LS_INSURANCE_ID"] = xfrm.LS_INSURANCE_ID_JV_Itm;
                    ROW["LS_INS_POLICY_NO"] = xfrm.LS_INS_POLICY_NO_JV_Itm;
                    ROW["LS_INS_AMT"] = Convert.IsDBNull(xfrm.LS_INS_AMT_JV_Itm) ? Convert.ToDouble(null) : Convert.ToDouble(xfrm.LS_INS_AMT_JV_Itm);
                    if (IsDate(xfrm.LS_INS_DATE_JV_Itm)) {
                        ROW["LS_INS_DATE"] = xfrm.LS_INS_DATE_JV_Itm;
                    }
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;
                }
                if (xfrm.iProfile_JV_Itm == "VEHICLES") {
                    ROW["VI_MAKE"] = xfrm.VI_MAKE_JV_Itm;
                    ROW["VI_MODEL"] = xfrm.VI_MODEL_JV_Itm;
                    ROW["VI_REG_NO_PATTERN"] = xfrm.VI_REG_NO_PATTERN_JV_Itm;
                    ROW["VI_REG_NO"] = xfrm.VI_REG_NO_JV_Itm;
                    ROW["VI_REG_DATE"] = xfrm.VI_REG_DATE_JV_Itm;
                    ROW["VI_OWNERSHIP"] = xfrm.VI_OWNERSHIP_JV_Itm;
                    ROW["VI_OWNERSHIP_AB_ID"] = xfrm.VI_OWNERSHIP_AB_ID_JV_Itm;
                    ROW["VI_DOC_RC_BOOK"] = xfrm.VI_DOC_RC_BOOK_JV_Itm;
                    ROW["VI_DOC_AFFIDAVIT"] = xfrm.VI_DOC_AFFIDAVIT_JV_Itm;
                    ROW["VI_DOC_WILL"] = xfrm.VI_DOC_WILL_JV_Itm;
                    ROW["VI_DOC_TRF_LETTER"] = xfrm.VI_DOC_TRF_LETTER_JV_Itm;
                    ROW["VI_DOC_FU_LETTER"] = xfrm.VI_DOC_FU_LETTER_JV_Itm;
                    ROW["VI_DOC_OTHERS"] = xfrm.VI_DOC_OTHERS_JV_Itm;
                    ROW["VI_DOC_NAME"] = xfrm.VI_DOC_NAME_JV_Itm;
                    ROW["VI_INSURANCE_ID"] = xfrm.VI_INSURANCE_ID_JV_Itm;
                    ROW["VI_INS_POLICY_NO"] = xfrm.VI_INS_POLICY_NO_JV_Itm;
                    ROW["VI_INS_EXPIRY_DATE"] = xfrm.VI_INS_EXPIRY_DATE_JV_Itm;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;
                }
                if (xfrm.iProfile_JV_Itm == "LAND & BUILDING") {
                    ROW["LB_PRO_TYPE"] = xfrm.LB_PRO_TYPE_JV_Itm;
                    ROW["LB_PRO_CATEGORY"] = xfrm.LB_PRO_CATEGORY_JV_Itm;
                    ROW["LB_PRO_USE"] = xfrm.LB_PRO_USE_JV_Itm;
                    ROW["LB_PRO_NAME"] = xfrm.LB_PRO_NAME_JV_Itm;
                    ROW["LB_PRO_ADDRESS"] = xfrm.LB_PRO_ADDRESS_JV_Itm;

                    ROW["LB_ADDRESS1"] = xfrm.LB_ADDRESS1_JV_Itm;
                    ROW["LB_ADDRESS2"] = xfrm.LB_ADDRESS2_JV_Itm;
                    ROW["LB_ADDRESS3"] = xfrm.LB_ADDRESS3_JV_Itm;
                    ROW["LB_ADDRESS4"] = xfrm.LB_ADDRESS4_JV_Itm;
                    ROW["LB_CITY_ID"] = xfrm.LB_CITY_ID_JV_Itm;
                    ROW["LB_DISTRICT_ID"] = xfrm.LB_DISTRICT_ID_JV_Itm;
                    ROW["LB_STATE_ID"] = xfrm.LB_STATE_ID_JV_Itm;
                    ROW["LB_PINCODE"] = xfrm.LB_PINCODE_JV_Itm;
                    ROW["LB_OWNERSHIP"] = xfrm.LB_OWNERSHIP_JV_Itm;
                    ROW["LB_OWNERSHIP_PARTY_ID"] = xfrm.LB_OWNERSHIP_PARTY_ID_JV_Itm;
                    ROW["LB_SURVEY_NO"] = xfrm.LB_SURVEY_NO_JV_Itm;
                    ROW["LB_CON_YEAR"] = xfrm.LB_CON_YEAR_JV_Itm;
                    ROW["LB_RCC_ROOF"] = xfrm.LB_RCC_ROOF_JV_Itm;
                    ROW["LB_PAID_DATE"] = xfrm.LB_PAID_DATE_JV_Itm;
                    ROW["LB_PERIOD_FROM"] = xfrm.LB_PERIOD_FROM_JV_Itm;
                    ROW["LB_PERIOD_TO"] = xfrm.LB_PERIOD_TO_JV_Itm;
                    ROW["LB_DOC_OTHERS"] = xfrm.LB_DOC_OTHERS_JV_Itm;
                    ROW["LB_DOC_NAME"] = xfrm.LB_DOC_NAME_JV_Itm;
                    ROW["LB_OTHER_DETAIL"] = xfrm.LB_OTHER_DETAIL_JV_Itm;
                    ROW["LB_TOT_P_AREA"] = Convert.IsDBNull(xfrm.LB_TOT_P_AREA_JV_Itm) ? Convert.ToDouble(null) : Convert.ToDouble(xfrm.LB_TOT_P_AREA_JV_Itm);
                    ROW["LB_CON_AREA"] = xfrm.LB_CON_AREA_JV_Itm == null ? 0 : xfrm.LB_CON_AREA_JV_Itm;
                    ROW["LB_DEPOSIT_AMT"] = xfrm.LB_DEPOSIT_AMT_JV_Itm == null ? 0 : xfrm.LB_DEPOSIT_AMT_JV_Itm;
                    ROW["LB_MONTH_RENT"] = xfrm.LB_MONTH_RENT_JV_Itm == null ? 0 : xfrm.LB_MONTH_RENT_JV_Itm;
                    ROW["LB_MONTH_O_PAYMENTS"] = xfrm.LB_MONTH_O_PAYMENTS_JV_Itm == null ? 0 : xfrm.LB_MONTH_O_PAYMENTS_JV_Itm;

                    if (xfrm.List_LB_DOCS_ARRAY_JV_Itm != null)
                    {
                        var Raw_LB_DOCS_ARRAY = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>>(xfrm.List_LB_DOCS_ARRAY_JV_Itm);
                        xfrm.LB_DOCS_ARRAY_JV_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>(Raw_LB_DOCS_ARRAY);
                    }
                    if (xfrm.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm != null)
                    {
                        var Raw_LB_EXTENDED_PROPERTY_TABLE = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>>(xfrm.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm);
                        xfrm.LB_EXTENDED_PROPERTY_TABLE_JV_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>(Raw_LB_EXTENDED_PROPERTY_TABLE);
                    }
                    if (LB_DOCS_ARRAY == null) {
                        LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY_JV_Itm;
                    }
                    else
                    {
                        if (LB_DOCS_ARRAY.Rows.Count <= 0) {
                            LB_DOCS_ARRAY = new DataTable();
                            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        }
                        if (xfrm.LB_DOCS_ARRAY_JV_Itm != null) {
                            foreach (DataRow XROW in xfrm.LB_DOCS_ARRAY_JV_Itm.Rows)
                            {
                                DataRow Row = LB_DOCS_ARRAY.NewRow();
                                Row["LB_MISC_ID"] = XROW["LB_MISC_ID"].ToString();
                                Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                                LB_DOCS_ARRAY.Rows.Add(Row);
                            }
                        }
                    }
                    if (LB_EXTENDED_PROPERTY_TABLE == null) {
                        LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE_JV_Itm;
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
                            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_REC_ID", Type.GetType("System.String")); ;
                        }
                        if (xfrm.LB_EXTENDED_PROPERTY_TABLE_JV_Itm == null) {
                            foreach (DataRow XROW in xfrm.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Rows)
                            {
                                DataRow Row = LB_EXTENDED_PROPERTY_TABLE.NewRow();
                                Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"].ToString();
                                Row["LB_SR_NO"] = XROW["LB_SR_NO"].ToString();
                                Row["LB_INS_ID"] = XROW["LB_INS_ID"].ToString();
                                Row["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"].ToString();
                                Row["LB_CON_AREA"] = XROW["LB_CON_AREA"].ToString();
                                Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"].ToString();
                                Row["LB_VALUE"] = XROW["LB_VALUE"];
                                Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"].ToString();
                                Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                                LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                            }
                        }
                    }
                }

                if (xfrm.iProfile_JV_Itm.Equals("WIP")) {
                    if (xfrm.iRefType_JV_Itm == "NEW") {
                        ROW["REFERENCE"] = xfrm.iReference_JV_Itm;
                        ROW["WIP_REF_TYPE"] = xfrm.iRefType_JV_Itm;
                    }
                    else
                    {
                        ROW["REF_REC_ID"] = xfrm.Ref_RecID_JV_Itm;
                        ROW["WIP_REF_TYPE"] = xfrm.iRefType_JV_Itm;
                    }
                }
                DT_JV.Rows.Add(ROW);
            }
            else if (xfrm.Tag_JV_Itm == Common.Navigation_Mode._Edit)
            {
                DataRow ROW = DT_JV.Select("[Sr.] =" + xfrm.Sr)[0];
                ROW["Item Name"] = xfrm.GLookUp_ItemName_JV_Itm;
                ROW["Item_ID"] = xfrm.GLookUp_ItemList_JV_Itm;
                if (xfrm.iCond_Ledger_ID_JV_Itm != "00000" && xfrm.Cmb_RefType_JV_Itm == "New")
                {
                    if (xfrm.Txt_Amt_JV_Itm > xfrm.iMinValue_JV_Itm && xfrm.Txt_Amt_JV_Itm <= xfrm.iMaxValue_JV_Itm)
                    {
                        ROW["Item_Led_ID"] = xfrm.iCond_Ledger_ID_JV_Itm;
                    }
                    else
                    {
                        ROW["Item_Led_ID"] = xfrm.iLed_ID_JV_Itm;
                    }
                }
                else
                {
                    ROW["Item_Led_ID"] = xfrm.iLed_ID_JV_Itm;
                }
                string txnType = "CREDIT";
                if (xfrm.RdAction_JV_Itm == 0)
                {
                    txnType = "DEBIT";
                }
                ROW["Trans_Type"] = txnType;
                ROW["Item_Profile"] = xfrm.iProfile_JV_Itm;
                ROW["Item_Party_Req"] = xfrm.iParty_Req_JV_Itm;
                ROW["Head"] = xfrm.BE_Item_Head_JV_Itm;
                ROW["Party"] = xfrm.GLookUp_PartyName_JV_Itm;
                if (xfrm.Txt_Qty_JV_Itm > 0)
                {
                    ROW["Qty."] = Convert.IsDBNull(xfrm.Txt_Qty_JV_Itm) ?Convert.ToDecimal(null): Convert.ToDecimal(xfrm.Txt_Qty_JV_Itm);
                }
                else if (xfrm.Txt_Qty_JV_Itm == 0)
                {
                    ROW["Qty."] = xfrm.Txt_Qty_JV_Itm;
                }
                if (xfrm.RdAction_JV_Itm == 0)
                {
                    ROW["DEBIT"] = xfrm.Txt_Amt_JV_Itm;
                    ROW["CREDIT"] = "";
                }
                if (xfrm.RdAction_JV_Itm == 1)
                {
                    ROW["CREDIT"] = xfrm.Txt_Amt_JV_Itm;
                    ROW["DEBIT"] = "";
                }
                ROW["Remarks"] = xfrm.Txt_Remarks_JV_Itm;
                ROW["Pur_ID"] = xfrm.GLookUp_PurList_JV_Itm;
                ROW["Purpose"] = xfrm.GLookUp_PurName_JV_Itm;
                ROW["PartyID"] = xfrm.GLookUp_PartyList_JV_Itm;
                ROW["Item_Voucher_Type"] = xfrm.Txt_ItemNature_JV_Itm;

                if (xfrm.Cmb_RefType_JV_Itm == "New")
                {
                    ROW["Addition"] = true;
                }
                else
                {
                    ROW["Addition"] = false;
                }

                if (xfrm.Cmb_RefType_JV_Itm == "New")
                {
                    ROW["CrossRefID"] = "";
                }
                else
                {
                    ROW["CrossRefID"] = xfrm.Cross_RefID_JV_Itm;
                }
                ROW["CrossReference"] = xfrm.TXT_Reference_JV_Itm;
                ROW["RefItem_RecEditOn"] = xfrm.RefItem_RecEditOn_JV_Itm;
                ROW["Party_RecEditOn"] = xfrm.Party_RecEditOn_JV_Itm;
                ROW["LB_REC_ID"] = xfrm.LB_REC_ID_JV_Itm;

                if (xfrm.iProfile_JV_Itm == "GOLD" || xfrm.iProfile_JV_Itm == "SILVER")
                {
                    ROW["GS_DESC_MISC_ID"] = xfrm.GS_DESC_MISC_ID_JV_Itm;
                    ROW["GS_ITEM_WEIGHT"] = Convert.IsDBNull(xfrm.GS_ITEM_WEIGHT_JV_Itm) ? Convert.ToDouble(null) : Convert.ToDouble(xfrm.GS_ITEM_WEIGHT_JV_Itm);
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;
                }
                if (xfrm.iProfile_JV_Itm == "OTHER ASSETS")
                {
                    ROW["AI_TYPE"] = xfrm.AI_TYPE_JV_Itm;
                    ROW["AI_MAKE"] = xfrm.AI_MAKE_JV_Itm;
                    ROW["AI_MODEL"] = xfrm.AI_MODEL_JV_Itm;
                    ROW["AI_SERIAL_NO"] = xfrm.AI_SERIAL_NO_JV_Itm;
                    if (IsDate(xfrm.AI_PUR_DATE_JV_Itm))
                    {
                        ROW["AI_PUR_DATE"] = xfrm.AI_PUR_DATE_JV_Itm;
                    }
                    else
                    {
                        ROW["AI_PUR_DATE"] = "";
                    }
                    ROW["AI_WARRANTY"] = xfrm.AI_WARRANTY_JV_Itm ?? 0;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;

                }
                if (xfrm.iProfile_JV_Itm == "LIVESTOCK")
                {
                    ROW["LS_NAME"] = xfrm.LS_NAME_JV_Itm;
                    ROW["LS_BIRTH_YEAR"] = xfrm.LS_BIRTH_YEAR_JV_Itm;
                    ROW["LS_INSURANCE"] = xfrm.LS_INSURANCE_JV_Itm;
                    ROW["LS_INSURANCE_ID"] = xfrm.LS_INSURANCE_ID_JV_Itm;
                    ROW["LS_INS_POLICY_NO"] = xfrm.LS_INS_POLICY_NO_JV_Itm;
                    ROW["LS_INS_AMT"] = Convert.IsDBNull(xfrm.LS_INS_AMT_JV_Itm) ? Convert.ToDouble(null) : Convert.ToDouble(xfrm.LS_INS_AMT_JV_Itm);
                    if (IsDate(xfrm.LS_INS_DATE_JV_Itm))
                    {
                        ROW["LS_INS_DATE"] = xfrm.LS_INS_DATE_JV_Itm;
                    }
                    else
                    {
                        ROW["LS_INS_DATE"] = "";
                    }
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;
                }
                if (xfrm.iProfile_JV_Itm == "VEHICLES")
                {
                    ROW["VI_MAKE"] = xfrm.VI_MAKE_JV_Itm;
                    ROW["VI_MODEL"] = xfrm.VI_MODEL_JV_Itm;
                    ROW["VI_REG_NO_PATTERN"] = xfrm.VI_REG_NO_PATTERN_JV_Itm;
                    ROW["VI_REG_NO"] = xfrm.VI_REG_NO_JV_Itm;
                    ROW["VI_REG_DATE"] = xfrm.VI_REG_DATE_JV_Itm;
                    ROW["VI_OWNERSHIP"] = xfrm.VI_OWNERSHIP_JV_Itm;
                    ROW["VI_OWNERSHIP_AB_ID"] = xfrm.VI_OWNERSHIP_AB_ID_JV_Itm;
                    ROW["VI_DOC_RC_BOOK"] = xfrm.VI_DOC_RC_BOOK_JV_Itm;
                    ROW["VI_DOC_AFFIDAVIT"] = xfrm.VI_DOC_AFFIDAVIT_JV_Itm;
                    ROW["VI_DOC_WILL"] = xfrm.VI_DOC_WILL_JV_Itm;
                    ROW["VI_DOC_TRF_LETTER"] = xfrm.VI_DOC_TRF_LETTER_JV_Itm;
                    ROW["VI_DOC_FU_LETTER"] = xfrm.VI_DOC_FU_LETTER_JV_Itm;
                    ROW["VI_DOC_OTHERS"] = xfrm.VI_DOC_OTHERS_JV_Itm;
                    ROW["VI_DOC_NAME"] = xfrm.VI_DOC_NAME_JV_Itm;
                    ROW["VI_INSURANCE_ID"] = xfrm.VI_INSURANCE_ID_JV_Itm;
                    ROW["VI_INS_POLICY_NO"] = xfrm.VI_INS_POLICY_NO_JV_Itm;
                    ROW["VI_INS_EXPIRY_DATE"] = xfrm.VI_INS_EXPIRY_DATE_JV_Itm;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_JV_Itm;
                }
                if (xfrm.iProfile_JV_Itm == "LAND & BUILDING")
                {
                    ROW["LB_PRO_TYPE"] = xfrm.LB_PRO_TYPE_JV_Itm;
                    ROW["LB_PRO_CATEGORY"] = xfrm.LB_PRO_CATEGORY_JV_Itm;
                    ROW["LB_PRO_USE"] = xfrm.LB_PRO_USE_JV_Itm;
                    ROW["LB_PRO_NAME"] = xfrm.LB_PRO_NAME_JV_Itm;
                    ROW["LB_PRO_ADDRESS"] = xfrm.LB_PRO_ADDRESS_JV_Itm;
                    ROW["LB_ADDRESS1"] = xfrm.LB_ADDRESS1_JV_Itm;
                    ROW["LB_ADDRESS2"] = xfrm.LB_ADDRESS2_JV_Itm;
                    ROW["LB_ADDRESS3"] = xfrm.LB_ADDRESS3_JV_Itm;
                    ROW["LB_ADDRESS4"] = xfrm.LB_ADDRESS4_JV_Itm;
                    ROW["LB_CITY_ID"] = xfrm.LB_CITY_ID_JV_Itm;
                    ROW["LB_DISTRICT_ID"] = xfrm.LB_DISTRICT_ID_JV_Itm;
                    ROW["LB_STATE_ID"] = xfrm.LB_STATE_ID_JV_Itm;
                    ROW["LB_PINCODE"] = xfrm.LB_PINCODE_JV_Itm;
                    ROW["LB_OWNERSHIP"] = xfrm.LB_OWNERSHIP_JV_Itm;
                    ROW["LB_OWNERSHIP_PARTY_ID"] = xfrm.LB_OWNERSHIP_PARTY_ID_JV_Itm;
                    ROW["LB_SURVEY_NO"] = xfrm.LB_SURVEY_NO_JV_Itm;
                    ROW["LB_CON_YEAR"] = xfrm.LB_CON_YEAR_JV_Itm;
                    ROW["LB_RCC_ROOF"] = xfrm.LB_RCC_ROOF_JV_Itm;
                    ROW["LB_PAID_DATE"] = xfrm.LB_PAID_DATE_JV_Itm;
                    ROW["LB_PERIOD_FROM"] = xfrm.LB_PERIOD_FROM_JV_Itm;
                    ROW["LB_PERIOD_TO"] = xfrm.LB_PERIOD_TO_JV_Itm;
                    ROW["LB_DOC_OTHERS"] = xfrm.LB_DOC_OTHERS_JV_Itm;
                    ROW["LB_DOC_NAME"] = xfrm.LB_DOC_NAME_JV_Itm;
                    ROW["LB_OTHER_DETAIL"] = xfrm.LB_OTHER_DETAIL_JV_Itm;
                    ROW["LB_TOT_P_AREA"] = Convert.IsDBNull(xfrm.LB_TOT_P_AREA_JV_Itm) ? Convert.ToDouble(null) : Convert.ToDouble(xfrm.LB_TOT_P_AREA_JV_Itm);
                    ROW["LB_CON_AREA"] = xfrm.LB_CON_AREA_JV_Itm == null ? 0 : xfrm.LB_CON_AREA_JV_Itm;
                    ROW["LB_DEPOSIT_AMT"] = xfrm.LB_DEPOSIT_AMT_JV_Itm == null ? 0 : xfrm.LB_DEPOSIT_AMT_JV_Itm;
                    ROW["LB_MONTH_RENT"] = xfrm.LB_MONTH_RENT_JV_Itm == null ? 0 : xfrm.LB_MONTH_RENT_JV_Itm;
                    ROW["LB_MONTH_O_PAYMENTS"] = xfrm.LB_MONTH_O_PAYMENTS_JV_Itm == null ? 0 : xfrm.LB_MONTH_O_PAYMENTS_JV_Itm;
                    if (xfrm.List_LB_DOCS_ARRAY_JV_Itm != null)
                    {
                        var Raw_LB_DOCS_ARRAY = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>>(xfrm.List_LB_DOCS_ARRAY_JV_Itm);
                        xfrm.LB_DOCS_ARRAY_JV_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>(Raw_LB_DOCS_ARRAY);
                    }
                    if (xfrm.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm != null)
                    {
                        var Raw_LB_EXTENDED_PROPERTY_TABLE = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>>(xfrm.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm);
                        xfrm.LB_EXTENDED_PROPERTY_TABLE_JV_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>(Raw_LB_EXTENDED_PROPERTY_TABLE);
                    }
                    if (LB_DOCS_ARRAY == null)
                    {
                        LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY_JV_Itm;
                    }
                    else
                    {
                        if (LB_DOCS_ARRAY.Rows.Count <= 0)
                        {
                            LB_DOCS_ARRAY = new DataTable();
                            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        }
                        //delete any previously added docs for same l&b
                        DataTable New_LB_DOCS_ARRAY = LB_DOCS_ARRAY.Clone();
                        foreach (DataRow XROW in LB_DOCS_ARRAY.Rows)
                        {
                            if (!(XROW["LB_REC_ID"].ToString() == xfrm.LB_REC_ID_JV_Itm))
                            {
                                New_LB_DOCS_ARRAY.ImportRow(XROW);
                            }
                        }
                        foreach (DataRow XROW in xfrm.LB_DOCS_ARRAY_JV_Itm.Rows)
                        {
                            DataRow Row = LB_DOCS_ARRAY.NewRow();
                            Row["LB_MISC_ID"] = XROW["LB_MISC_ID"].ToString();
                            Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_DOCS_ARRAY.Rows.Add(Row);
                        }
                    }
                    if (LB_EXTENDED_PROPERTY_TABLE == null)
                    {
                        LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE_JV_Itm;
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
                        //delete any previously added extensions for same l&b
                        DataTable New_LB_EXTENDED_PROPERTY_TABLE = LB_EXTENDED_PROPERTY_TABLE.Clone();
                        foreach (DataRow XROW in LB_EXTENDED_PROPERTY_TABLE.Rows)
                        {
                            if (!(XROW["LB_REC_ID"].ToString() == xfrm.LB_REC_ID_JV_Itm))
                            {
                                New_LB_EXTENDED_PROPERTY_TABLE.ImportRow(XROW);
                            }
                        }
                        LB_EXTENDED_PROPERTY_TABLE = New_LB_EXTENDED_PROPERTY_TABLE;
                        New_LB_EXTENDED_PROPERTY_TABLE.Dispose();
                        foreach (DataRow XROW in xfrm.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Rows)
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
                if (xfrm.iProfile_JV_Itm.Equals("WIP"))
                {
                    ROW["REF_REC_ID"] = xfrm.Ref_RecID_JV_Itm;
                    ROW["REFERENCE"] = xfrm.iReference_JV_Itm;
                    ROW["WIP_REF_TYPE"] = xfrm.iRefType_JV_Itm;
                }
            }
            else if (xfrm.Tag_JV_Itm == Common.Navigation_Mode._Delete)
            {
                Delete_Action = true;
            }
            Sub_Amt_Calculation(Delete_Action);
            
        }

        #endregion

        [HttpGet]
        public ActionResult Frm_Voucher_Win_Journal_New_Item_Detail(string Tag, string iTxnM_ID, string Vdt = "", int Grid_RowNo = 0, string MainTag = "", int rdaction = 0)
        {
            JournalVoucher_Item model = new JournalVoucher_Item();
            model.ActionMethod_JV_Itm = Tag;
            model.Tag_JV_Itm = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod_JV_Itm);
            model.Sr = Grid_RowNo;
            model.iTxnM_ID_JV_Itm = iTxnM_ID;

            Return_Json_Param jsonParam = new Return_Json_Param();
            if (model.Tag_JV_Itm != Common.Navigation_Mode._Delete)
            {
                model.TitleX_JV_Itm = "Item Detail";

                RefreshItemList_Itm();
                RefreshPurList_Itm_Dtel();
                RefreshPartyList();
            }
            if (model.Tag_JV_Itm == Common.Navigation_Mode._New)
            {
                model.Me_Text_JV_Itm = "New ~ " + model.TitleX_JV_Itm;
                //model.iSpecific_Allow_JV_Itm = iSpecific_Allow;
                //model.iSpecific_ItemID_JV_Itm = iSpecific_ItemID;
                model.Vdt_JV_Itm = string.IsNullOrWhiteSpace(Vdt) ? null : Convert.ToDateTime(Vdt).ToString(BASE._Date_Format_Current);
                model.RdAction_JV_Itm = rdaction;
                //model.Txt_Amt_JV_Itm = 1;
                return View(model);
            }
            else if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit || model.Tag_JV_Itm == Common.Navigation_Mode._View)
            {
                if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit)
                {
                    model.Me_Text_JV_Itm = "Edit ~ " + model.TitleX_JV_Itm;
                }
                else
                {
                    model.Me_Text_JV_Itm = "View ~ " + model.TitleX_JV_Itm;
                }
                var GridRow_ToEdit = DT_JV.Select("[Sr.] =" + Grid_RowNo);
                model.iProfile_OLD_JV_Itm = GridRow_ToEdit[0]["Item_Profile"].ToString();
                model.iSpecific_ItemID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["Item_ID"]) ? null : GridRow_ToEdit[0]["Item_ID"].ToString();
                model.iPartyID_JV_Itm = GridRow_ToEdit[0]["PartyID"].ToString();
                model.iPur_ID_JV_Itm = GridRow_ToEdit[0]["Pur_ID"].ToString();
                model.Txt_Qty_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["Qty."]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["Qty."]);
                
                if ((GridRow_ToEdit[0]["Debit"].ToString() == ""? 0 : Convert.ToDouble(GridRow_ToEdit[0]["Debit"])) > 0)
                {
                    model.Txt_Amt_JV_Itm = Convert.ToDouble(GridRow_ToEdit[0]["Debit"]);
                    model.RdAction_JV_Itm = 0;
                }
                if ((GridRow_ToEdit[0]["Credit"].ToString() == "" ? 0 : Convert.ToDouble(GridRow_ToEdit[0]["Credit"])) > 0)
                {
                    model.Txt_Amt_JV_Itm = Convert.ToDouble(GridRow_ToEdit[0]["Credit"]);
                    model.RdAction_JV_Itm = 1;
                }
                model.Txt_Remarks_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["Remarks"]) ? null : GridRow_ToEdit[0]["Remarks"].ToString();
                model.Cross_RefID_JV_Itm = GridRow_ToEdit[0]["CrossRefID"].ToString();
                model.TXT_Reference_JV_Itm = GridRow_ToEdit[0]["CrossReference"].ToString();
                if (Convert.ToBoolean(GridRow_ToEdit[0]["Addition"]))
                {
                    model.Cmb_RefType_JV_Itm = "New";
                    //model.Cmb_RefType_JV_Itm = 1;
                }
                
                if (GridRow_ToEdit[0]["RefItem_RecEditOn"].ToString().Length > 0)
                {
                    model.RefItem_RecEditOn_JV_Itm = Convert.ToDateTime(GridRow_ToEdit[0]["RefItem_RecEditOn"]);
                }
                if (GridRow_ToEdit[0]["Party_RecEditOn"].ToString().Length > 0)
                {
                    model.Party_RecEditOn_JV_Itm = Convert.ToDateTime(GridRow_ToEdit[0]["Party_RecEditOn"]);
                }
                if (GridRow_ToEdit[0]["LB_REC_ID"].ToString() != null)
                {
                    model.LB_REC_ID_JV_Itm = GridRow_ToEdit[0]["LB_REC_ID"].ToString();

                }


                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "GOLD" || GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "SILVER")
                {
                    model.GS_DESC_MISC_ID_JV_Itm = GridRow_ToEdit[0]["GS_DESC_MISC_ID"].ToString();
                    model.GS_ITEM_WEIGHT_JV_Itm = Convert.ToDouble(GridRow_ToEdit[0]["GS_ITEM_WEIGHT"]);
                    model.X_LOC_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "OTHER ASSETS")
                {
                    model.AI_TYPE_JV_Itm = GridRow_ToEdit[0]["AI_TYPE"].ToString();
                    model.AI_MAKE_JV_Itm = GridRow_ToEdit[0]["AI_MAKE"].ToString();
                    model.AI_MODEL_JV_Itm = GridRow_ToEdit[0]["AI_MODEL"].ToString();
                    model.AI_SERIAL_NO_JV_Itm = GridRow_ToEdit[0]["AI_SERIAL_NO"].ToString();
                    model.AI_PUR_DATE_JV_Itm = GridRow_ToEdit[0]["AI_PUR_DATE"].ToString();
                    model.AI_WARRANTY_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["AI_WARRANTY"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["AI_WARRANTY"]);
                    model.X_LOC_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "LIVESTOCK")
                {
                    model.LS_NAME_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LS_NAME"]) ? null : GridRow_ToEdit[0]["LS_NAME"].ToString();
                    model.LS_BIRTH_YEAR_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LS_BIRTH_YEAR"]) ? null : GridRow_ToEdit[0]["LS_BIRTH_YEAR"].ToString();
                    model.LS_INSURANCE_JV_Itm = GridRow_ToEdit[0]["LS_INSURANCE"].ToString();
                    model.LS_INSURANCE_ID_JV_Itm = GridRow_ToEdit[0]["LS_INSURANCE_ID"].ToString();
                    model.LS_INS_POLICY_NO_JV_Itm = GridRow_ToEdit[0]["LS_INS_POLICY_NO"].ToString();
                    model.LS_INS_AMT_JV_Itm = Convert.ToDouble(GridRow_ToEdit[0]["LS_INS_AMT"]);
                    model.LS_INS_DATE_JV_Itm = GridRow_ToEdit[0]["LS_INS_DATE"].ToString();
                    model.X_LOC_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "VEHICLES")
                {
                    model.VI_MAKE_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_MAKE"]) ? null : GridRow_ToEdit[0]["VI_MAKE"].ToString();
                    model.VI_MODEL_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_MODEL"]) ? null : GridRow_ToEdit[0]["VI_MODEL"].ToString();
                    model.VI_REG_NO_PATTERN_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_NO_PATTERN"]) ? null : GridRow_ToEdit[0]["VI_REG_NO_PATTERN"].ToString();
                    model.VI_REG_NO_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_NO"]) ? null : GridRow_ToEdit[0]["VI_REG_NO"].ToString();
                    model.VI_REG_DATE_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_REG_DATE"]) ? null : GridRow_ToEdit[0]["VI_REG_DATE"].ToString();
                    model.VI_OWNERSHIP_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_OWNERSHIP"]) ? null : GridRow_ToEdit[0]["VI_OWNERSHIP"].ToString();
                    model.VI_OWNERSHIP_AB_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"]) ? null : GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"].ToString();
                    model.VI_DOC_RC_BOOK_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_RC_BOOK"]) ? null : GridRow_ToEdit[0]["VI_DOC_RC_BOOK"].ToString();
                    model.VI_DOC_AFFIDAVIT_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_AFFIDAVIT"]) ? null : GridRow_ToEdit[0]["VI_DOC_AFFIDAVIT"].ToString();
                    model.VI_DOC_WILL_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_WILL"]) ? null : GridRow_ToEdit[0]["VI_DOC_WILL"].ToString();
                    model.VI_DOC_TRF_LETTER_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_TRF_LETTER"]) ? null : GridRow_ToEdit[0]["VI_DOC_TRF_LETTER"].ToString();
                    model.VI_DOC_FU_LETTER_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_FU_LETTER"]) ? null : GridRow_ToEdit[0]["VI_DOC_FU_LETTER"].ToString();
                    model.VI_DOC_OTHERS_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_OTHERS"]) ? null : GridRow_ToEdit[0]["VI_DOC_OTHERS"].ToString();
                    model.VI_DOC_NAME_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_DOC_NAME"]) ? null : GridRow_ToEdit[0]["VI_DOC_NAME"].ToString();
                    model.VI_INSURANCE_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_INSURANCE_ID"]) ? null : GridRow_ToEdit[0]["VI_INSURANCE_ID"].ToString();
                    model.VI_INS_POLICY_NO_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_POLICY_NO"]) ? null : GridRow_ToEdit[0]["VI_INS_POLICY_NO"].ToString();
                    model.VI_INS_EXPIRY_DATE_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"]) ? null : GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"].ToString();
                    model.X_LOC_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LOC_ID"]) ? null : GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "LAND & BUILDING")
                {
                    model.LB_PRO_TYPE_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_TYPE"]) ? null : GridRow_ToEdit[0]["LB_PRO_TYPE"].ToString();
                    model.LB_PRO_CATEGORY_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_CATEGORY"]) ? null : GridRow_ToEdit[0]["LB_PRO_CATEGORY"].ToString();
                    model.LB_PRO_USE_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_USE"]) ? null : GridRow_ToEdit[0]["LB_PRO_USE"].ToString();
                    model.LB_PRO_NAME_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_NAME"]) ? null : GridRow_ToEdit[0]["LB_PRO_NAME"].ToString();
                    model.LB_PRO_ADDRESS_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PRO_ADDRESS"]) ? null : GridRow_ToEdit[0]["LB_PRO_ADDRESS"].ToString();
                    model.LB_ADDRESS1_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS1"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS1"].ToString();
                    model.LB_ADDRESS2_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS2"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS2"].ToString();
                    model.LB_ADDRESS3_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS3"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS3"].ToString();
                    model.LB_ADDRESS4_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_ADDRESS4"]) ? null : GridRow_ToEdit[0]["LB_ADDRESS4"].ToString();
                    model.LB_CITY_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CITY_ID"]) ? null : GridRow_ToEdit[0]["LB_CITY_ID"].ToString();
                    model.LB_DISTRICT_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DISTRICT_ID"]) ? null : GridRow_ToEdit[0]["LB_DISTRICT_ID"].ToString();
                    model.LB_STATE_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_STATE_ID"]) ? null : GridRow_ToEdit[0]["LB_STATE_ID"].ToString();
                    model.LB_PINCODE_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PINCODE"]) ? null : GridRow_ToEdit[0]["LB_PINCODE"].ToString();
                    model.LB_OWNERSHIP_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_OWNERSHIP"]) ? null : GridRow_ToEdit[0]["LB_OWNERSHIP"].ToString();
                    model.LB_OWNERSHIP_PARTY_ID_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"]) ? null : GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"].ToString();
                    model.LB_SURVEY_NO_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_SURVEY_NO"]) ? null : GridRow_ToEdit[0]["LB_SURVEY_NO"].ToString();
                    model.LB_CON_YEAR_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CON_YEAR"]) ? null : GridRow_ToEdit[0]["LB_CON_YEAR"].ToString();
                    model.LB_RCC_ROOF_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_RCC_ROOF"]) ? null : GridRow_ToEdit[0]["LB_RCC_ROOF"].ToString();
                    model.LB_PAID_DATE_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PAID_DATE"]) ? null : GridRow_ToEdit[0]["LB_PAID_DATE"].ToString();
                    model.LB_PERIOD_FROM_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_FROM"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString();
                    model.LB_PERIOD_TO_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_PERIOD_TO"]) ? null : GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString();
                    model.LB_DOC_OTHERS_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DOC_OTHERS"]) ? null : GridRow_ToEdit[0]["LB_DOC_OTHERS"].ToString();
                    model.LB_DOC_NAME_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DOC_NAME"]) ? null : GridRow_ToEdit[0]["LB_DOC_NAME"].ToString();
                    model.LB_OTHER_DETAIL_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_OTHER_DETAIL"]) ? null : GridRow_ToEdit[0]["LB_OTHER_DETAIL"].ToString();
                    model.LB_TOT_P_AREA_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_TOT_P_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_TOT_P_AREA"].ToString());
                    model.LB_CON_AREA_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_CON_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_CON_AREA"].ToString());
                    model.LB_DEPOSIT_AMT_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_DEPOSIT_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_DEPOSIT_AMT"].ToString());
                    model.LB_MONTH_RENT_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_MONTH_RENT"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_RENT"].ToString());
                    model.LB_MONTH_O_PAYMENTS_JV_Itm = Convert.IsDBNull(GridRow_ToEdit[0]["LB_MONTH_O_PAYMENTS"]) ? Convert.ToDouble(null) : Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_O_PAYMENTS"].ToString());


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

                    if (LB_EXTENDED_PROPERTY_TABLE != null) //'LB Item screen already opened in same instance 
                    {
                        foreach (DataRow XROW in LB_EXTENDED_PROPERTY_TABLE.Rows)
                        {
                            if (XROW["LB_REC_ID"].ToString() == model.LB_REC_ID_JV_Itm)
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
                                Row["LB_REC_ID"] = model.LB_REC_ID_JV_Itm;
                                EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                            }
                        }
                    }
                    else
                    {
                        DataTable LB_Ext = BASE._L_B_DBOps.GetExtensionDetails(model.LB_REC_ID_JV_Itm, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal);
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
                            Row["LB_REC_ID"] = model.LB_REC_ID_JV_Itm;
                            EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                        }
                    }

                    model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm = EDIT_LB_EXTENDED_PROPERTY_TABLE;

                    List<Models.LB_EXTENDED_PROPERTY_TABLE_List> Return_LB_EXTENDED_PROPERTY_TABLE_List = new List<Models.LB_EXTENDED_PROPERTY_TABLE_List>();
                    foreach (DataRow row in model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Rows)
                    {
                        Models.LB_EXTENDED_PROPERTY_TABLE_List nrow = new Models.LB_EXTENDED_PROPERTY_TABLE_List();
                        nrow.LB_MOU_DATE = row["LB_MOU_DATE"].ToString();
                        nrow.LB_SR_NO = row["LB_SR_NO"].ToString();
                        nrow.LB_INS_ID = row["LB_INS_ID"].ToString();
                        nrow.LB_TOT_P_AREA = row["LB_TOT_P_AREA"].ToString();
                        nrow.LB_CON_AREA = row["LB_CON_AREA"].ToString();
                        nrow.LB_CON_YEAR = row["LB_CON_YEAR"].ToString();
                        nrow.LB_VALUE = row["LB_VALUE"].ToString();
                        nrow.LB_OTHER_DETAIL = row["LB_OTHER_DETAIL"].ToString();
                        nrow.LB_REC_ID = row["LB_REC_ID"].ToString();
                        Return_LB_EXTENDED_PROPERTY_TABLE_List.Add(nrow);
                    }
                    model.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm = new JavaScriptSerializer().Serialize(Return_LB_EXTENDED_PROPERTY_TABLE_List);

                    DataTable EDIT_LB_DOCS_ARRAY = new DataTable();

                    EDIT_LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                    EDIT_LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

                    if (LB_DOCS_ARRAY != null)
                    {
                        foreach (DataRow XROW in LB_DOCS_ARRAY.Rows)
                        {
                            if (XROW["LB_REC_ID"].ToString() == model.LB_REC_ID_JV_Itm)
                            {
                                DataRow Row = EDIT_LB_DOCS_ARRAY.NewRow();
                                Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                                Row["LB_REC_ID"] = model.LB_REC_ID_JV_Itm;
                                EDIT_LB_DOCS_ARRAY.Rows.Add(Row);
                            }
                        }
                    }
                    else
                    {
                        DataTable LB_DOC = BASE._L_B_DBOps.GetDocsDetails(model.LB_REC_ID_JV_Itm, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal);
                        foreach (DataRow XROW in LB_DOC.Rows)
                        {
                            DataRow Row = EDIT_LB_DOCS_ARRAY.NewRow();
                            Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                            Row["LB_REC_ID"] = model.LB_REC_ID_JV_Itm;
                            EDIT_LB_DOCS_ARRAY.Rows.Add(Row);
                        }
                    }
                    model.LB_DOCS_ARRAY_JV_Itm = EDIT_LB_DOCS_ARRAY;


                    List<Models.LB_DOCS_ARRAY_List> return_LB_DOCS_ARRAY = new List<Models.LB_DOCS_ARRAY_List>();
                    foreach (DataRow row in model.LB_DOCS_ARRAY_JV_Itm.Rows)
                    {
                        Models.LB_DOCS_ARRAY_List nrow = new Models.LB_DOCS_ARRAY_List();
                        nrow.LB_MISC_ID = row["LB_MISC_ID"].ToString();
                        nrow.LB_REC_ID = row["LB_REC_ID"].ToString();
                        return_LB_DOCS_ARRAY.Add(nrow);
                    }
                    model.List_LB_DOCS_ARRAY_JV_Itm = new JavaScriptSerializer().Serialize(return_LB_DOCS_ARRAY);

                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "WIP")
                {
                    model.iReference_JV_Itm = GridRow_ToEdit[0]["REFERENCE"].ToString();
                    model.Ref_RecID_JV_Itm = GridRow_ToEdit[0]["REF_REC_ID"].ToString();
                    model.iRefType_JV_Itm = GridRow_ToEdit[0]["WIP_REF_TYPE"].ToString();
                }

                return View(model);
            }
            else if(model.Tag_JV_Itm == Common.Navigation_Mode._Delete)
            {
                var GridRow_ToDelete = DT_JV.Select("[Sr.] =" + Grid_RowNo);
                if (MainTag == "_Edit")
                {
                    if(GridRow_ToDelete[0]["Item_Profile"].ToString() == "LAND & BUILDING" || 
                        GridRow_ToDelete[0]["Item_Profile"].ToString() == "OTHER ASSETS" || 
                        (GridRow_ToDelete[0]["Item_Voucher_Type"].ToString().ToUpper() == "LAND & BUILDING" &&
                        GridRow_ToDelete[0]["Item_Profile"].ToString() != "LAND & BUILDING"))
                    {
                        if(BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Insurance Related Assets Cannot be Added/Edited After The Completion of Insurance Audit";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if(GridRow_ToDelete[0]["Item_Profile"].ToString() == "WIP") 
                    {
                        if(GridRow_ToDelete[0]["WIP_REF_TYPE"].ToString() == "EXISTING") 
                        {
                            object RefId = GridRow_ToDelete[0]["REF_REC_ID"];
                            if (RefId != null)
                            {
                                if (RefId.ToString().Length > 0)
                                {
                                    DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, "WIP", GridRow_ToDelete[0]["Item_ID"].ToString(), iTxnM_ID, null, Common.Navigation_Mode._Delete, RefId.ToString());
                                    if (PROF_TABLE.Rows.Count > 0)
                                    {
                                        if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                                        {
                                            if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                            {                                                 
                                                jsonParam.message = "Sorry ! Deletion of Selected Payment Entry creates a Negative Closing Balance in Next Year for " +
                                                    GridRow_ToDelete[0]["Item_Profile"].ToString().ToLower() + " with Original Value " +
                                                    PROF_TABLE.Rows[0]["Org Value"].ToString();
                                                jsonParam.title = "Error!!";
                                                jsonParam.result = false;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }

                                        if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                        {
                                            jsonParam.message = "Sorry ! Deletion of Selected Payment Entry creates a Negative Closing Balance in Current Year for " +
                                                GridRow_ToDelete[0]["Item_Profile"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }                                        
                                }
                            }
                        }
                    }
                }
                GridRow_ToDelete[0].Delete();
                Fill_Item_Grid(model);
            }
            jsonParam.result = true;
            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
        }
        public void SetGridData_Jv()
        {
            DT_JV = new DataTable();
            DT_JV.TableName = "Item_Detail";
            DT_JV.Columns.Add("Sr.", Type.GetType("System.Int32"));
            DT_JV.Columns.Add("Item_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("Item_Led_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("Trans_Type", Type.GetType("System.String"));
            DT_JV.Columns.Add("Item_Voucher_Type", Type.GetType("System.String"));
            DT_JV.Columns.Add("Item_Party_Req", Type.GetType("System.String"));
            DT_JV.Columns.Add("Item_Profile", Type.GetType("System.String"));
            DT_JV.Columns.Add("Item Name", Type.GetType("System.String"));
            DT_JV.Columns.Add("Head", Type.GetType("System.String"));
            DT_JV.Columns.Add("Party", Type.GetType("System.String"));
            DT_JV.Columns.Add("Purpose", Type.GetType("System.String"));
            DT_JV.Columns.Add("Debit", Type.GetType("System.String"));
            DT_JV.Columns.Add("Credit", Type.GetType("System.String"));
            DT_JV.Columns.Add("Remarks", Type.GetType("System.String"));
            DT_JV.Columns.Add("Pur_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("PartyID", Type.GetType("System.String"));
            DT_JV.Columns.Add("Addition", Type.GetType("System.Boolean"));
            DT_JV.Columns.Add("CrossRefID", Type.GetType("System.String"));
            DT_JV.Columns.Add("CrossReference", Type.GetType("System.String"));
            DT_JV.Columns.Add("Qty.", Type.GetType("System.Decimal"));
            DT_JV.Columns.Add("RefItem_RecEditOn", Type.GetType("System.DateTime"));
            DT_JV.Columns.Add("Party_RecEditOn", Type.GetType("System.DateTime"));
            DT_JV.Columns.Add("Unit", Type.GetType("System.String"));
            DT_JV.Columns.Add("Rate", Type.GetType("System.Double"));            
            DT_JV.Columns.Add("LOC_ID", Type.GetType("System.String"));

            //---Gold / Silver---- -
            DT_JV.Columns.Add("GS_DESC_MISC_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("GS_ITEM_WEIGHT", Type.GetType("System.Decimal"));
            //---Other Asset---- -
            DT_JV.Columns.Add("AI_TYPE", Type.GetType("System.String"));
            DT_JV.Columns.Add("AI_MAKE", Type.GetType("System.String"));
            DT_JV.Columns.Add("AI_MODEL", Type.GetType("System.String"));
            DT_JV.Columns.Add("AI_SERIAL_NO", Type.GetType("System.String"));
            DT_JV.Columns.Add("AI_PUR_DATE", Type.GetType("System.String"));
            DT_JV.Columns.Add("AI_WARRANTY", Type.GetType("System.Double"));            
            //---LIVE STOCK----
            DT_JV.Columns.Add("LS_NAME", Type.GetType("System.String"));
            DT_JV.Columns.Add("LS_BIRTH_YEAR", Type.GetType("System.String"));
            DT_JV.Columns.Add("LS_INSURANCE", Type.GetType("System.String"));
            DT_JV.Columns.Add("LS_INSURANCE_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("LS_INS_POLICY_NO", Type.GetType("System.String"));
            DT_JV.Columns.Add("LS_INS_AMT", Type.GetType("System.Double"));
            DT_JV.Columns.Add("LS_INS_DATE", Type.GetType("System.String"));

            //---Vehicles------
            DT_JV.Columns.Add("VI_MAKE", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_MODEL", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_REG_NO_PATTERN", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_REG_NO", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_REG_DATE", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_OWNERSHIP", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_OWNERSHIP_AB_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_DOC_RC_BOOK", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_DOC_AFFIDAVIT", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_DOC_WILL", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_DOC_TRF_LETTER", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_DOC_FU_LETTER", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_DOC_OTHERS", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_DOC_NAME", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_INSURANCE_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_INS_POLICY_NO", Type.GetType("System.String"));
            DT_JV.Columns.Add("VI_INS_EXPIRY_DATE", Type.GetType("System.String"));

            //-----Land & Building---- -
            DT_JV.Columns.Add("LB_PRO_TYPE", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_PRO_CATEGORY", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_PRO_USE", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_PRO_NAME", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_PRO_ADDRESS", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_OWNERSHIP", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_OWNERSHIP_PARTY_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_SURVEY_NO", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            DT_JV.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            DT_JV.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_RCC_ROOF", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_DEPOSIT_AMT", Type.GetType("System.Double"));
            DT_JV.Columns.Add("LB_PAID_DATE", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_MONTH_RENT", Type.GetType("System.Double"));
            DT_JV.Columns.Add("LB_MONTH_O_PAYMENTS", Type.GetType("System.Double"));
            DT_JV.Columns.Add("LB_PERIOD_FROM", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_PERIOD_TO", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_DOC_OTHERS", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_DOC_NAME", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_ADDRESS1", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_ADDRESS2", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_ADDRESS3", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_ADDRESS4", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_STATE_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_DISTRICT_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_CITY_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("LB_PINCODE", Type.GetType("System.String"));

            //--WIP--
            DT_JV.Columns.Add("REF_REC_ID", Type.GetType("System.String"));
            DT_JV.Columns.Add("REFERENCE", Type.GetType("System.String"));
            DT_JV.Columns.Add("WIP_REF_TYPE", Type.GetType("System.String"));
            //}
        }

        #region Databinding
        public ContentResult Data_Binding_Jv(ref JournalVoucher model, string GridToRefresh = "CashBookListGrid")
        {
            DataTable d1 = BASE._Journal_voucher_DBOps.GetRecords(model.xMID);
            if (d1 == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','"
                    + Messages.SomeError + "','Error!!','');</script>");
            }
            DateTime xDate;
            xDate = Convert.ToDateTime(d1.Rows[0]["TR_DATE"]);
            model.Txt_V_Date_Jv = xDate;

            //Start: if Entry already changed
            if (BASE.AllowMultiuser())
            {
                if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
                {
                    string viewstr = "";
                    if (model.Tag == Common.Navigation_Mode._View)
                    {
                        viewstr = "view";
                    }
                    if (model.Info_LastEditedOn_Jv != Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]))
                    {
                        string message = Messages.RecordChanged("Current Journal Voucher", viewstr);
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','"
                            + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");

                    }
                }
            }
            //End : Check if entry already changed

            model.LastEditedOn_Jv = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
            DataTable GS = BASE._Gift_DBOps.GetGoldSilverList(model.xMID);
            DataTable AI = BASE._Gift_DBOps.GetAssetList(model.xMID);
            DataTable VI = BASE._Gift_DBOps.GetVehiclesList(model.xMID);
            DataTable LS = BASE._Gift_DBOps.GetLiveStockList(model.xMID);
            DataTable LB = BASE._Payment_DBOps.GetLandBuilingList(model.xMID);
            DataTable WIP = BASE._Payment_DBOps.Get_WIP_List(model.xMID);
            if (GS == null || AI == null || VI == null || LS == null || LB == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
            }
            DataSet JointData = new DataSet();
            JointData.Tables.Add(d1.Copy());

            //Gold/Silver for Item_Detail
            JointData.Tables.Add(GS.Copy());
            DataRelation GS_Relation = JointData.Relations.Add("GS", JointData.Tables["TRANSACTION_INFO"]
                .Columns["TR_M_ID"], JointData.Tables["Gold_Silver_Info"].Columns["GS_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(GS_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_Row["GS_TR_ITEM_SRNO"]))
                    {
                        XROW["GS_DESC_MISC_ID"] = _Row["GS_DESC_MISC_ID"];
                        XROW["GS_ITEM_WEIGHT"] = _Row["GS_ITEM_WEIGHT"];
                        XROW["LOC_ID"] = _Row["GS_LOC_AL_ID"];
                    }
                }
            }

            //Other Assets
            JointData.Tables.Add(AI.Copy());
            DataRelation AI_Relation = JointData.Relations.Add("AI", JointData.Tables["TRANSACTION_INFO"]
                .Columns["TR_M_ID"], JointData.Tables["Asset_Info"].Columns["AI_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(AI_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_Row["AI_TR_ITEM_SRNO"]))
                    {
                        XROW["AI_TYPE"] = _Row["AI_TYPE"];
                        XROW["AI_MAKE"] = _Row["AI_MAKE"];
                        XROW["AI_MODEL"] = _Row["AI_MODEL"];
                        XROW["AI_SERIAL_NO"] = _Row["AI_SERIAL_NO"];
                        XROW["AI_WARRANTY"] = _Row["AI_WARRANTY"];
                        XROW["AI_PUR_DATE"] = _Row["AI_PUR_DATE"];
                        XROW["Rate"] = _Row["AI_RATE"];
                        XROW["LOC_ID"] = _Row["AI_LOC_AL_ID"];
                    }
                }
            }

            //For Vehicles
            JointData.Tables.Add(VI.Copy());
            DataRelation VI_Relation = JointData.Relations.Add("VI", JointData.Tables["TRANSACTION_INFO"]
                .Columns["TR_M_ID"], JointData.Tables["Vehicles_Info"].Columns["VI_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(VI_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_Row["VI_TR_ITEM_SRNO"]))
                    {
                        XROW["VI_MAKE"] = _Row["VI_MAKE"];
                        XROW["VI_MODEL"] = _Row["VI_MODEL"];
                        XROW["VI_REG_NO_PATTERN"] = _Row["VI_REG_NO_PATTERN"];
                        XROW["VI_REG_NO"] = _Row["VI_REG_NO"];
                        XROW["VI_REG_DATE"] = _Row["VI_REG_DATE"];
                        XROW["VI_OWNERSHIP"] = _Row["VI_OWNERSHIP"];
                        XROW["VI_OWNERSHIP_AB_ID"] = _Row["VI_OWNERSHIP_AB_ID"];
                        XROW["VI_DOC_RC_BOOK"] = _Row["VI_DOC_RC_BOOK"];
                        XROW["VI_DOC_AFFIDAVIT"] = _Row["VI_DOC_AFFIDAVIT"];
                        XROW["VI_DOC_WILL"] = _Row["VI_DOC_WILL"];
                        XROW["VI_DOC_TRF_LETTER"] = _Row["VI_DOC_TRF_LETTER"];
                        XROW["VI_DOC_FU_LETTER"] = _Row["VI_DOC_FU_LETTER"];
                        XROW["VI_DOC_OTHERS"] = _Row["VI_DOC_OTHERS"];
                        XROW["VI_DOC_NAME"] = _Row["VI_DOC_NAME"];
                        XROW["VI_INSURANCE_ID"] = _Row["VI_INSURANCE_ID"];
                        XROW["VI_INS_POLICY_NO"] = _Row["VI_INS_POLICY_NO"];
                        XROW["VI_INS_EXPIRY_DATE"] = _Row["VI_INS_EXPIRY_DATE"];
                        XROW["LOC_ID"] = _Row["VI_LOC_AL_ID"];
                    }
                }
            }

            //FOR LiveStock
            JointData.Tables.Add(LS.Copy());
            DataRelation LS_Relation = JointData.Relations.Add("LS", JointData.Tables["TRANSACTION_INFO"]
                .Columns["TR_M_ID"], JointData.Tables["Live_Stock_Info"].Columns["LS_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(LS_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_Row["LS_TR_ITEM_SRNO"]))
                    {
                        XROW["LS_NAME"] = _Row["LS_NAME"];
                        XROW["LS_BIRTH_YEAR"] = _Row["LS_BIRTH_YEAR"];
                        XROW["LS_INSURANCE"] = _Row["LS_INSURANCE"];
                        XROW["LS_INSURANCE_ID"] = _Row["LS_INSURANCE_ID"];
                        XROW["LS_INS_POLICY_NO"] = _Row["LS_INS_POLICY_NO"];
                        XROW["LS_INS_AMT"] = _Row["LS_INS_AMT"];
                        XROW["LS_INS_DATE"] = _Row["LS_INS_DATE"];
                        XROW["LOC_ID"] = _Row["LS_LOC_AL_ID"];
                    }
                }
            }

            //FOR WIP
            JointData.Tables.Add(WIP.Copy());
            DataRelation WIP_Relation = JointData.Relations.Add("WIP", JointData.Tables["TRANSACTION_INFO"]
                .Columns["TR_M_ID"], JointData.Tables["WIP_INFO"].Columns["WIP_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _ROW in XROW.GetChildRows(WIP_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_ROW["WIP_TR_ITEM_SRNO"]))
                    {
                        XROW["WIP_REF"] = _ROW["WIP_REF"];
                        XROW["WIP_REC_ID"] = _ROW["REC_ID"];
                        XROW["WIP_REF_TYPE"] = "NEW";
                    }
                }
            }

            //FOR Land&Building
            JointData.Tables.Add(LB.Copy());
            DataRelation LB_Relation = JointData.Relations.Add("LB", JointData.Tables["TRANSACTION_INFO"]
                .Columns["TR_M_ID"], JointData.Tables["Land_Building_info"].Columns["LB_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _ROW in XROW.GetChildRows(LB_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_ROW["LB_TR_ITEM_SRNO"]))
                    {
                        XROW["LB_PRO_TYPE"] = _ROW["LB_PRO_TYPE"];
                        XROW["LB_PRO_CATEGORY"] = _ROW["LB_PRO_CATEGORY"];
                        XROW["LB_PRO_USE"] = _ROW["LB_PRO_USE"];
                        XROW["LB_PRO_NAME"] = _ROW["LB_PRO_NAME"];
                        XROW["LB_PRO_ADDRESS"] = _ROW["LB_PRO_ADDRESS"];
                        XROW["LB_OWNERSHIP"] = _ROW["LB_OWNERSHIP"];
                        XROW["LB_OWNERSHIP_PARTY_ID"] = _ROW["LB_OWNERSHIP_PARTY_ID"];
                        XROW["LB_SURVEY_NO"] = _ROW["LB_SURVEY_NO"];
                        XROW["LB_CON_YEAR"] = _ROW["LB_CON_YEAR"];
                        XROW["LB_RCC_ROOF"] = _ROW["LB_RCC_ROOF"];
                        XROW["LB_PAID_DATE"] = _ROW["LB_PAID_DATE"];
                        XROW["LB_PERIOD_FROM"] = _ROW["LB_PERIOD_FROM"];
                        XROW["LB_PERIOD_TO"] = _ROW["LB_PERIOD_TO"];
                        XROW["LB_DOC_OTHERS"] = _ROW["LB_DOC_OTHERS"];
                        XROW["LB_DOC_NAME"] = _ROW["LB_DOC_NAME"];
                        XROW["LB_OTHER_DETAIL"] = _ROW["LB_OTHER_DETAIL"];
                        XROW["LB_TOT_P_AREA"] = _ROW["LB_TOT_P_AREA"];
                        XROW["LB_CON_AREA"] = _ROW["LB_CON_AREA"];
                        XROW["LB_DEPOSIT_AMT"] = _ROW["LB_DEPOSIT_AMT"];
                        XROW["LB_MONTH_RENT"] = _ROW["LB_MONTH_RENT"];
                        XROW["LB_MONTH_O_PAYMENTS"] = _ROW["LB_MONTH_O_PAYMENTS"];
                        XROW["LB_REC_ID"] = _ROW["LB_REC_ID"];
                        XROW["LB_ADDRESS1"] = _ROW["LB_ADDRESS1"];
                        XROW["LB_ADDRESS2"] = _ROW["LB_ADDRESS2"];
                        XROW["LB_ADDRESS3"] = _ROW["LB_ADDRESS3"];
                        XROW["LB_ADDRESS4"] = _ROW["LB_ADDRESS4"];
                        XROW["LB_COUNTRY_ID"] = _ROW["LB_COUNTRY_ID"];
                        XROW["LB_STATE_ID"] = _ROW["LB_STATE_ID"];
                        XROW["LB_DISTRICT_ID"] = _ROW["LB_DISTRICT_ID"];
                        XROW["LB_CITY_ID"] = _ROW["LB_CITY_ID"];
                        XROW["LB_PINCODE"] = _ROW["LB_PINCODE"];
                    }
                }
            }

            //For Docs 
            DataTable LB_DOCS_ARRAY = new DataTable();
            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));


            foreach (DataRow LBRow in LB.Rows)
            {
                DataTable docs = BASE._L_B_DBOps.GetDocumentRecord(LBRow["LB_REC_ID"].ToString(),
                    Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal);
                foreach (DataRow docRow in docs.Rows)
                {
                    DataRow Row = LB_DOCS_ARRAY.NewRow();
                    Row["LB_MISC_ID"] = docRow["LB_MISC_ID"].ToString();
                    Row["LB_REC_ID"] = docRow["LB_REC_ID"].ToString();
                    LB_DOCS_ARRAY.Rows.Add(Row);
                }
            }

            //For Extended Property 
            DataTable LB_EXTENDED_PROPERTY_TABLE = new DataTable();
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            LB_EXTENDED_PROPERTY_TABLE.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

            foreach (DataRow LBRow in LB.Rows)
            {
                DataTable extensions = BASE._L_B_DBOps.GetExtendedRecord(LBRow["LB_REC_ID"].ToString(),
                    Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal);
                foreach (DataRow extensionsROW in extensions.Rows)
                {
                    DataRow ROW = LB_EXTENDED_PROPERTY_TABLE.NewRow();
                    ROW["LB_MOU_DATE"] = extensionsROW["LB_MOU_DATE"].ToString();
                    ROW["LB_SR_NO"] = extensionsROW["LB_SR_NO"].ToString();
                    ROW["LB_INS_ID"] = extensionsROW["LB_INS_ID"].ToString();
                    ROW["LB_TOT_P_AREA"] = Convert.ToDouble(extensionsROW["LB_TOT_P_AREA"].ToString());
                    ROW["LB_CON_AREA"] = Convert.ToDouble(extensionsROW["LB_CON_AREA"].ToString());
                    ROW["LB_CON_YEAR"] = extensionsROW["LB_CON_YEAR"].ToString();
                    ROW["LB_VALUE"] = Convert.ToDouble(extensionsROW["LB_VALUE"]);
                    ROW["LB_OTHER_DETAIL"] = extensionsROW["LB_OTHER_DETAIL"].ToString();
                    ROW["LB_REC_ID"] = extensionsROW["LB_REC_ID"].ToString();
                    LB_EXTENDED_PROPERTY_TABLE.Rows.Add(ROW);
                }
            }

            model.Txt_V_NO_Jv = d1.Rows[0]["TR_VNO"].ToString();

            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                DataRow ROW = DT_JV.NewRow();
                ROW["Sr."] = XROW["TR_SR_NO"];
                ROW["Item_ID"] = XROW["TR_ITEM_ID"];
                ROW["Trans_Type"] = XROW["TR_TYPE"];
                if (ROW["Trans_Type"].ToString().ToUpper() == "DEBIT")
                {
                    ROW["Item_Led_ID"] = XROW["TR_DR_LED_ID"];
                }
                else
                {
                    ROW["Item_Led_ID"] = XROW["TR_CR_LED_ID"];
                }
                ROW["Item_Voucher_Type"] = XROW["Item_Voucher_Type"];
                ROW["Item_Party_Req"] = XROW["Item_Party_Req"];
                ROW["Item_Profile"] = XROW["Item_Profile"];
                ROW["Item Name"] = XROW["Item_Name"];
                ROW["Head"] = XROW["LED_NAME"];
                ROW["Party"] = XROW["PARTY"];
                ROW["Qty."] = XROW["Qty"];
                ROW["Rate"] = XROW["Rate"];
                ROW["Debit"] = XROW["Debit"];
                ROW["Credit"] = XROW["Credit"];
                ROW["Remarks"] = XROW["TR_REMARKS"];
                ROW["Pur_ID"] = XROW["Pur_ID"]; //Purpose ID
                ROW["Purpose"] = XROW["Purpose"]; //Purpose ID
                ROW["PartyID"] = XROW["PartyID"];
                ROW["Party_RecEditOn"] = XROW["Party_RecEditOn"];
                ROW["LOC_ID"] = XROW["LOC_ID"];
                //Gold/Silver
                ROW["GS_DESC_MISC_ID"] = XROW["GS_DESC_MISC_ID"];
                ROW["GS_ITEM_WEIGHT"] = XROW["GS_ITEM_WEIGHT"];
                //OTHER ASSET
                ROW["AI_TYPE"] = XROW["AI_TYPE"]; //Bug #5125 FIX
                ROW["AI_MAKE"] = XROW["AI_MAKE"];
                ROW["AI_MODEL"] = XROW["AI_MODEL"];
                ROW["AI_SERIAL_NO"] = XROW["AI_SERIAL_NO"];
                ROW["AI_WARRANTY"] = XROW["AI_WARRANTY"];
                ROW["AI_PUR_DATE"] = XROW["AI_PUR_DATE"];
                //LIVE STOCK
                ROW["LS_NAME"] = XROW["LS_NAME"];
                ROW["LS_BIRTH_YEAR"] = XROW["LS_BIRTH_YEAR"];
                ROW["LS_INSURANCE"] = XROW["LS_INSURANCE"];
                ROW["LS_INSURANCE_ID"] = XROW["LS_INSURANCE_ID"];
                ROW["LS_INS_POLICY_NO"] = XROW["LS_INS_POLICY_NO"];
                ROW["LS_INS_AMT"] = XROW["LS_INS_AMT"];
                ROW["LS_INS_DATE"] = XROW["LS_INS_DATE"];
                //VEHICLES
                ROW["VI_MAKE"] = XROW["VI_MAKE"];
                ROW["VI_MODEL"] = XROW["VI_MODEL"];
                ROW["VI_REG_NO_PATTERN"] = XROW["VI_REG_NO_PATTERN"];
                ROW["VI_REG_NO"] = XROW["VI_REG_NO"];
                ROW["VI_REG_DATE"] = XROW["VI_REG_DATE"];
                ROW["VI_OWNERSHIP"] = XROW["VI_OWNERSHIP"];
                ROW["VI_OWNERSHIP_AB_ID"] = XROW["VI_OWNERSHIP_AB_ID"];
                ROW["VI_DOC_RC_BOOK"] = XROW["VI_DOC_RC_BOOK"];
                ROW["VI_DOC_AFFIDAVIT"] = XROW["VI_DOC_AFFIDAVIT"];
                ROW["VI_DOC_WILL"] = XROW["VI_DOC_WILL"];
                ROW["VI_DOC_TRF_LETTER"] = XROW["VI_DOC_TRF_LETTER"];
                ROW["VI_DOC_FU_LETTER"] = XROW["VI_DOC_FU_LETTER"];
                ROW["VI_DOC_OTHERS"] = XROW["VI_DOC_OTHERS"];
                ROW["VI_DOC_NAME"] = XROW["VI_DOC_NAME"];
                ROW["VI_INSURANCE_ID"] = XROW["VI_INSURANCE_ID"];
                ROW["VI_INS_POLICY_NO"] = XROW["VI_INS_POLICY_NO"];
                ROW["VI_INS_EXPIRY_DATE"] = XROW["VI_INS_EXPIRY_DATE"];
                //Land & Building
                ROW["LB_PRO_TYPE"] = XROW["LB_PRO_TYPE"];
                ROW["LB_PRO_CATEGORY"] = XROW["LB_PRO_CATEGORY"];
                ROW["LB_PRO_USE"] = XROW["LB_PRO_USE"];
                ROW["LB_PRO_NAME"] = XROW["LB_PRO_NAME"];
                ROW["LB_PRO_ADDRESS"] = XROW["LB_PRO_ADDRESS"];
                ROW["LB_OWNERSHIP"] = XROW["LB_OWNERSHIP"];
                ROW["LB_OWNERSHIP_PARTY_ID"] = XROW["LB_OWNERSHIP_PARTY_ID"];
                ROW["LB_SURVEY_NO"] = XROW["LB_SURVEY_NO"];
                ROW["LB_CON_YEAR"] = XROW["LB_CON_YEAR"];
                ROW["LB_RCC_ROOF"] = XROW["LB_RCC_ROOF"];
                ROW["LB_PAID_DATE"] = XROW["LB_PAID_DATE"];
                ROW["LB_PERIOD_FROM"] = XROW["LB_PERIOD_FROM"];
                ROW["LB_PERIOD_TO"] = XROW["LB_PERIOD_TO"];
                ROW["LB_DOC_OTHERS"] = XROW["LB_DOC_OTHERS"];
                ROW["LB_DOC_NAME"] = XROW["LB_DOC_NAME"];
                ROW["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"];
                ROW["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"];
                ROW["LB_CON_AREA"] = XROW["LB_CON_AREA"];
                ROW["LB_DEPOSIT_AMT"] = XROW["LB_DEPOSIT_AMT"];
                ROW["LB_MONTH_RENT"] = XROW["LB_MONTH_RENT"];
                ROW["LB_MONTH_O_PAYMENTS"] = XROW["LB_MONTH_O_PAYMENTS"];
                ROW["LB_REC_ID"] = XROW["LB_REC_ID"];
                ROW["LB_ADDRESS1"] = XROW["LB_ADDRESS1"];
                ROW["LB_ADDRESS2"] = XROW["LB_ADDRESS2"];
                ROW["LB_ADDRESS3"] = XROW["LB_ADDRESS3"];
                ROW["LB_ADDRESS4"] = XROW["LB_ADDRESS4"];
                ROW["LB_STATE_ID"] = XROW["LB_STATE_ID"];
                ROW["LB_DISTRICT_ID"] = XROW["LB_DISTRICT_ID"];
                ROW["LB_CITY_ID"] = XROW["LB_CITY_ID"];
                ROW["LB_PINCODE"] = XROW["LB_PINCODE"];
                //ROW["LB_ADDRESS"] = XRow("LB_ADDRESS"];
                //WIP
                ROW["REF_REC_ID"] = XROW["WIP_REF_TYPE"].ToString() == "NEW" ? "" : XROW["WIP_REC_ID"];
                ROW["REFERENCE"] = XROW["WIP_REF"];
                ROW["WIP_REF_TYPE"] = XROW["WIP_REF_TYPE"];

                Boolean ReferencePresent = true;
                if (XROW["TR_TRF_CROSS_REF_ID"] == null || Convert.IsDBNull(XROW["TR_TRF_CROSS_REF_ID"]))
                {
                    ReferencePresent = false;
                }
                else
                {
                    if (XROW["TR_TRF_CROSS_REF_ID"].ToString().Length == 0)
                    {
                        ReferencePresent = false;
                    }
                }


                string xTemp_AssetID = "";
                string RefId = "";
                string Response;
                switch (ROW["Item_Profile"].ToString().ToUpper())
                {
                    case "ADVANCES":
                        Response = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(model.xMID);
                        if (Response == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (Response.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "OTHER DEPOSITS":
                        Response = BASE._Voucher_DBOps.GetRaisedDepositRecID(model.xMID);
                        if (Response == null) {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (Response.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "OTHER LIABILITIES":
                        Response = BASE._Voucher_DBOps.GetRaisedLiabilityRecID(model.xMID);
                        if (Response == null) {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (Response.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "GOLD":
                    case "SILVER":
                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, model.xMID);
                        if (xTemp_AssetID == null) {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (xTemp_AssetID.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "OTHER ASSETS":
                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, model.xMID);
                        if (xTemp_AssetID == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (xTemp_AssetID.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "LIVESTOCK":
                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, model.xMID);
                        if (xTemp_AssetID == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (xTemp_AssetID.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "VEHICLES":
                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, model.xMID);
                        if (xTemp_AssetID == null)
                        {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (xTemp_AssetID.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "LAND & BUILDING":
                        xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, model.xMID);
                        if (xTemp_AssetID == null) {
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                        }
                        if (xTemp_AssetID.Length > 0 && !ReferencePresent)
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    case "WIP":
                        if (ROW["WIP_REF_TYPE"].ToString() == "NEW")
                        {
                            ROW["Addition"] = true;
                        }
                        else
                        {
                            ROW["Addition"] = false;
                        }
                        break;

                    default:
                        ROW["Addition"] = false;
                        break;
                }
                ROW["CrossRefID"] = XROW["TR_TRF_CROSS_REF_ID"];
                ROW["CrossReference"] = XROW["CROSS_REFERENCE"];

                if (BASE.AllowMultiuser())
                {
                    if (XROW["Item_Profile"].ToString().ToUpper() != "NOT APPLICABLE" || XROW["Item_Voucher_Type"].ToString().ToUpper() == "LAND & BUILDING" && ROW["Addition"].ToString().ToUpper() == "FALSE")
                    { //Allows Profile or Construction Adjustment Entries only and leaves out Profile Creation / Expense / Income Entries
                        if (XROW["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                        {
                            //Frm_Voucher_Win_Journal_Item jrnl_Item = new Frm_Voucher_Win_Journal_Item();
                            DataTable PROFILE_TABLE = CommonFunctions.GetReferenceData(BASE,XROW["Item_Profile"].ToString(), XROW["TR_ITEM_ID"].ToString(), model.xMID, XROW["PartyID"].ToString(),  model.Tag, XROW["TR_TRF_CROSS_REF_ID"].ToString());
                            if (PROFILE_TABLE == null)
                            {
                                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + Messages.SomeError + "','Error!!','');</script>");
                            }
                            if (PROFILE_TABLE.Rows.Count > 0)
                            {
                                ROW["RefItem_RecEditOn"] = PROFILE_TABLE.Rows[0]["REC_EDIT_ON"];
                            }
                        }
                    }
                    //Else   Bug #5931 fix
                    //    ROW["RefItem_RecEditOn"] = ""
                }
                DT_JV.Rows.Add(ROW);
            }

            model.Txt_Narration_Jv = d1.Rows[0]["TR_NARRATION"].ToString();
            model.Txt_Reference_Jv = d1.Rows[0]["TR_REFERENCE"].ToString();
            Sub_Amt_Calculation(false);
            model.Txt_CrTotal_Jv = (decimal)Txt_CrTotal;
            model.Txt_DrTotal_Jv = (decimal)Txt_DrTotal;
            model.Txt_DiffAmt_Jv = (decimal)(Txt_DrTotal - Txt_CrTotal);
            model.itemGrid_RowCount_Jv = DT_JV.Rows.Count;
            //Calculation_Check();         

            return null;
        }


        public void Sub_Amt_Calculation(Boolean Delete_Action)
        {
            DataView dv = DT_JV.DefaultView;
            dv.Sort = "Sr.";
            Double xDebit = 0;
            Double xCredit = 0;
            for (int i = 0; i < DT_JV.Rows.Count; i++)
            {
                if (Delete_Action)
                {
                    DT_JV.Rows[i]["Sr."] = i + 1;
                }
                if (DT_JV.Rows[i]["Debit"].ToString() != "")
                {
                    xDebit = xDebit + Convert.ToDouble(DT_JV.Rows[i]["Debit"].ToString());
                }
                if (DT_JV.Rows[i]["Credit"].ToString() != "")
                {
                    xCredit = xCredit + Convert.ToDouble(DT_JV.Rows[i]["Credit"].ToString());
                }
            }
            Txt_CrTotal = xCredit;
            Txt_DrTotal = xDebit;
            Txt_DiffAmt = Txt_DrTotal - Txt_CrTotal;
            //Calculation_Check
        }

        //public void Calculation_Check()
        //{

        //}
        #endregion //end of databidings and related functions

        #region New Item Window Post method
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Journal_New_Item_Detail(JournalVoucher_Item model)
        {
            model.Tag_JV_Itm = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod_JV_Itm);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag_JV_Itm == Common_Lib.Common.Navigation_Mode._New ||
                    model.Tag_JV_Itm == Common_Lib.Common.Navigation_Mode._Edit ||
                    model.Tag_JV_Itm == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_ItemList_JV_Itm))
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Item Name Not Selected . . .!";
                        jsonParam.focusid = "GLookUp_ItemList_JV_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.iProfile_JV_Itm.ToUpper() != "NOT APPLICABLE")
                    {
                        if (model.RdAction_JV_Itm == 0 && model.Cmb_RefType_JV_Itm.ToUpper() == "NEW" &&
                                model.iProfile_JV_Itm.ToUpper() == "OTHER LIABILITIES")
                        {
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.message = "'New' Ref Type not allowed in case of Debit of liabilities . . . !";
                            jsonParam.focusid = "Cmb_RefType_JV_Itm";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if (model.RdAction_JV_Itm == 1 && model.Cmb_RefType_JV_Itm.ToUpper() == "NEW"
                             && model.iProfile_JV_Itm.ToUpper() != "OTHER LIABILITIES")
                        {
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.message = "'New' Ref Type not allowed in case of Credit . . . !";
                            jsonParam.focusid = "Cmb_RefType_JV_Itm";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (string.IsNullOrEmpty(model.Txt_Amt_JV_Itm.ToString()) || model.Txt_Amt_JV_Itm <= 0)
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Amount Cannot be Zero/Negative . . . !";
                        jsonParam.focusid = "Txt_Amt_JV_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_PartyList_JV_Itm) && model.iParty_Req_JV_Itm.ToUpper().Trim() == "YES")
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Party Required . . . !";
                        jsonParam.focusid = "GLookUp_PartyList_JV_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    model.BE_Party_Category_JV_Itm = string.IsNullOrWhiteSpace(model.BE_Party_Category_JV_Itm) ? "" : model.BE_Party_Category_JV_Itm;
                    if (model.BE_Party_Category_JV_Itm.ToUpper() != "GOVT ENTITY")
                    {
                        if (model.GLookUp_ItemList_JV_Itm == "d0a33061-d679-4f21-ac12-a29541de8fcb") //donation in kind
                        {
                            if (BASE._open_Year_ID >= 2122 && BASE._open_Ins_ID != "00001" && BASE._open_Ins_ID != "00005")
                            {
                                if (string.IsNullOrWhiteSpace(model.BE_PAN_No_JV_Itm) && string.IsNullOrWhiteSpace(model.BE_UID_No_JV_Itm) && string.IsNullOrWhiteSpace(model.BE_ID_No_JV_Itm))
                                {
                                    jsonParam.message = "Atleast one of the PAN/Aadhar No./Passport No./ Voter ID/Ration Card No./Driving License is Compulsory for donation entry. . . !";
                                    jsonParam.result = false;
                                    jsonParam.title = "Incomplete Information...";
                                    jsonParam.focusid = "BE_PAN_No_JV_Itm";
                                    return Json(new
                                    {
                                        jsonParam
                                    }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }                       

                    if (string.IsNullOrWhiteSpace(model.GLookUp_PurList_JV_Itm))
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Purpose Not Selected . . . !";
                        jsonParam.focusid = "GLookUp_PurList_JV_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((model.iProfile_JV_Itm.ToUpper() == "ADVANCES" || model.iProfile_JV_Itm.ToUpper() == "OTHER DEPOSITS")
                            && model.RdAction_JV_Itm == 1 && model.Cmb_RefType_JV_Itm == "New")
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Please select Debit to raise a new Advance/Deposit . . . !";
                        jsonParam.focusid = "RdAction_JV_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((model.iProfile_JV_Itm.ToUpper() == "OTHER LIABILITIES")
                            && model.RdAction_JV_Itm == 0 && model.Cmb_RefType_JV_Itm == "New")
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Please select Credit to raise a new New Liability . . . !";
                        jsonParam.focusid = "RdAction_JV_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                Boolean NewProfile = false;
                if (model.iProfile_OLD_JV_Itm != null && model.iProfile_OLD_JV_Itm.Length > 0)
                {
                    if (model.iProfile_JV_Itm != model.iProfile_OLD_JV_Itm)
                    {
                        NewProfile = true;
                    }
                }

                if (model.Cmb_RefType_JV_Itm == "New")
                {
                    switch (model.iProfile_JV_Itm.ToUpper())
                    {
                        case "GOLD":
                        case "SILVER":
                            jsonParam.result = true;
                            jsonParam.popup_title = model.Me_Text_JV_Itm + " (Gold / Silver Detail)...";
                            jsonParam.popup_form_name = "Frm_GoldSilver_Window";
                            jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_GoldSilver_Window/";
                            jsonParam.popup_querystring = "Cmd_Type=" + Url.Encode(model.iProfile_JV_Itm) +
                                "&BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_JV_Itm) + "&FromJV=" + true;
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._New)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Url.Encode(model.Tag_JV_Itm.ToString());
                            }
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit || model.Tag_JV_Itm == Common.Navigation_Mode._View)
                            {
                                if (NewProfile)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag="
                                        + Url.Encode(Common.Navigation_Mode._New.ToString());
                                }
                                else
                                {
                                    if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit)
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag="
                                            + Url.Encode(Common.Navigation_Mode._Edit.ToString());
                                    }
                                    else
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag="
                                            + Url.Encode(Common.Navigation_Mode._View.ToString());
                                    }
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&GS_DESC_MISC_ID="
                                        + Url.Encode(model.GS_DESC_MISC_ID_JV_Itm) +
                                        "&GS_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_JV_Itm) +
                                        "&Txt_Weight=" + model.GS_ITEM_WEIGHT_JV_Itm +
                                        "&Txt_Others=" + Url.Encode(model.Txt_Remarks_JV_Itm);
                                }
                            }
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tr_M_ID=" + Url.Encode(model.iTxnM_ID_JV_Itm);

                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

                        case "OTHER ASSETS":
                            jsonParam.result = true;
                            jsonParam.popup_title = model.Me_Text_JV_Itm + " (Movable Asset Detail)...";
                            jsonParam.popup_form_name = "Frm_Asset_Window";
                            jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Asset_Window/";
                            jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_JV_Itm) +
                                "&FromJV=" + true + "&IsGift=" + true;
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._New)
                            {
                                JV_Asset_Image = null;
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New +
                                    "&Txt_Amt=" + model.Txt_Amt_JV_Itm +
                                    "&Txt_Rate=" + model.Txt_Amt_JV_Itm +
                                    "&Txt_Qty=" + model.Txt_Qty_JV_Itm;
                                if (IsDate(model.Vdt_JV_Itm))
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Date=" + Url.Encode(model.Vdt_JV_Itm.ToString());
                                }
                            }
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit || model.Tag_JV_Itm == Common.Navigation_Mode._View)
                            {
                                if (NewProfile)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                                }
                                else
                                {
                                    if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit)
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                    }
                                    else
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                    }

                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Make=" + Url.Encode(model.AI_MAKE_JV_Itm) +
                                        "&Txt_Model=" + Url.Encode(model.AI_MODEL_JV_Itm) +
                                        "&Txt_Serial=" + Url.Encode(model.AI_SERIAL_NO_JV_Itm) +
                                        "&Txt_Warranty=" + model.AI_WARRANTY_JV_Itm;

                                    if (IsDate(model.AI_PUR_DATE_JV_Itm))
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&AI_PUR_DATE="
                                            + Url.Encode(model.AI_PUR_DATE_JV_Itm);
                                    }
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Amt=" + model.Txt_Amt_JV_Itm +
                                        "&Txt_Rate=" + model.Txt_Amt_JV_Itm +
                                        "&Txt_Qty=" + model.Txt_Qty_JV_Itm +
                                        "&Txt_Others=" + Url.Encode(model.Txt_Remarks_JV_Itm) +
                                        "&AI_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_JV_Itm);
                                }
                            }
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        case "LIVESTOCK":
                            jsonParam.result = true;
                            jsonParam.popup_title = model.Me_Text_JV_Itm + " (Livestock Detail)...";
                            jsonParam.popup_form_name = "Frm_Live_Stock_Window";
                            jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Live_Stock_Window/";
                            jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_JV_Itm) + "&FromJV=" + true;
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._New)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                            }
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit || model.Tag_JV_Itm == Common.Navigation_Mode._View)
                            {
                                if (NewProfile)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                                }
                                else
                                {
                                    if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit)
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                    }
                                    else
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                    }

                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Name=" + Url.Encode(model.LS_NAME_JV_Itm) +
                                        "&LS_BIRTH_YEAR=" + Url.Encode(model.LS_BIRTH_YEAR_JV_Itm) +
                                        "&LS_INSURANCE=" + Url.Encode(model.LS_INSURANCE_JV_Itm) +
                                        "&LS_INS_ID=" + Url.Encode(model.LS_INSURANCE_ID_JV_Itm) +
                                        "&LS_INS_POLICY_NO=" + Url.Encode(model.LS_INS_POLICY_NO_JV_Itm) +
                                        "&LS_INS_AMT=" + model.LS_INS_AMT_JV_Itm;
                                    if (IsDate(model.LS_INS_DATE_JV_Itm))
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&LS_INS_DATE="
                                            + Url.Encode(model.LS_INS_DATE_JV_Itm);
                                    }
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Others="
                                        + Url.Encode(model.Txt_Remarks_JV_Itm) + "&LS_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_JV_Itm);
                                }
                            }
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        case "VEHICLES":
                            jsonParam.result = true;
                            jsonParam.popup_title = model.Me_Text_JV_Itm + " (Vehicle Detail)...";
                            jsonParam.popup_form_name = "Frm_Vehicles_Window";
                            jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Vehicles_Window/";
                            jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_JV_Itm) + "&FromJV=" + true;

                            if (model.Tag_JV_Itm == Common.Navigation_Mode._New)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                            }
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit || model.Tag_JV_Itm == Common.Navigation_Mode._View)
                            {
                                if (NewProfile)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                                }
                                else
                                {
                                    if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit)
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                    }
                                    else
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                    }

                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Cmd_Make=" + Url.Encode(model.VI_MAKE_JV_Itm) +
                                        "&VI_MODEL=" + Url.Encode(model.VI_MODEL_JV_Itm) +
                                        "&VI_REG_NO_PATTERN=" + Url.Encode(model.VI_REG_NO_PATTERN_JV_Itm) +
                                        "&VI_REG_NO=" + Url.Encode(model.VI_REG_NO_JV_Itm);

                                    if (IsDate(model.VI_REG_DATE_JV_Itm))
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&VI_REG_DATE="
                                            + Url.Encode(model.VI_REG_DATE_JV_Itm);
                                    }

                                    jsonParam.popup_querystring = jsonParam.popup_querystring +
                                        "&VI_OWNERSHIP=" + Url.Encode(model.VI_OWNERSHIP_JV_Itm) +
                                        "&VI_OWNERSHIP_AB_ID=" + Url.Encode(model.VI_OWNERSHIP_AB_ID_JV_Itm) +
                                        "&VI_DOC_RC_BOOK=" + Url.Encode(model.VI_DOC_RC_BOOK_JV_Itm) +
                                        "&VI_DOC_AFFIDAVIT=" + Url.Encode(model.VI_DOC_AFFIDAVIT_JV_Itm) +
                                        "&VI_DOC_WILL=" + Url.Encode(model.VI_DOC_WILL_JV_Itm) +
                                        "&VI_DOC_TRF_LETTER=" + Url.Encode(model.VI_DOC_TRF_LETTER_JV_Itm) +
                                        "&VI_DOC_FU_LETTER=" + Url.Encode(model.VI_DOC_FU_LETTER_JV_Itm) +
                                        "&VI_DOC_OTHERS=" + Url.Encode(model.VI_DOC_OTHERS_JV_Itm) +
                                        "&VI_DOC_NAME=" + Url.Encode(model.VI_DOC_NAME_JV_Itm) +
                                        "&VI_INSURANCE_ID=" + Url.Encode(model.VI_INSURANCE_ID_JV_Itm) +
                                        "&Txt_PolicyNo=" + Url.Encode(model.VI_INS_POLICY_NO_JV_Itm);

                                    if (IsDate(model.VI_INS_EXPIRY_DATE_JV_Itm))
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring +
                                            "&VI_E_DATE=" + Url.Encode(model.VI_INS_EXPIRY_DATE_JV_Itm);
                                    }

                                    jsonParam.popup_querystring = jsonParam.popup_querystring +
                                        "&Txt_Others=" + Url.Encode(model.Txt_Remarks_JV_Itm) +
                                        "&VI_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_JV_Itm);
                                }
                            }
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        case "LAND & BUILDING":
                            jsonParam.result = true;
                            jsonParam.popup_title = model.Me_Text_JV_Itm + " (Land & Building Detail)...";
                            jsonParam.popup_form_name = "Frm_Property_Window";
                            jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Property_Window/";
                            jsonParam.popup_querystring = "IsJV=" + true + "&ITEM_ID=" + Url.Encode(model.GLookUp_ItemList_JV_Itm)
                                + "&FromJV=" + true;

                            if (model.Tag_JV_Itm == Common.Navigation_Mode._New)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                            }
                            if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit || model.Tag_JV_Itm == Common.Navigation_Mode._View)
                            {
                                if (NewProfile)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                                }
                                else
                                {
                                    if (model.Tag_JV_Itm == Common.Navigation_Mode._Edit)
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                    }
                                    else
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                    }

                                    jsonParam.popup_querystring = jsonParam.popup_querystring +
                                        "&LB_PRO_TYPE=" + Url.Encode(model.LB_PRO_TYPE_JV_Itm) +
                                        "&LB_PRO_CATEGORY=" + Url.Encode(model.LB_PRO_CATEGORY_JV_Itm) +
                                        "&LB_PRO_USE=" + Url.Encode(model.LB_PRO_USE_JV_Itm) +
                                        "&LB_PRO_NAME=" + Url.Encode(model.LB_PRO_NAME_JV_Itm) +
                                        "&LB_PRO_ADDRESS=" + Url.Encode(model.LB_PRO_ADDRESS_JV_Itm) +
                                        "&LB_ADDRESS1=" + Url.Encode(model.LB_ADDRESS1_JV_Itm) +
                                        "&LB_ADDRESS2=" + Url.Encode(model.LB_ADDRESS2_JV_Itm) +
                                        "&LB_ADDRESS3=" + Url.Encode(model.LB_ADDRESS3_JV_Itm) +
                                        "&LB_ADDRESS4=" + Url.Encode(model.LB_ADDRESS4_JV_Itm) +
                                        "&LB_CITY_ID=" + Url.Encode(model.LB_CITY_ID_JV_Itm) +
                                        "&LB_DISTRICT_ID=" + Url.Encode(model.LB_DISTRICT_ID_JV_Itm) +
                                        "&LB_STATE_ID=" + Url.Encode(model.LB_STATE_ID_JV_Itm) +
                                        "&LB_PINCODE=" + Url.Encode(model.LB_PINCODE_JV_Itm) +
                                        "&LB_OWNERSHIP=" + Url.Encode(model.LB_OWNERSHIP_JV_Itm) +
                                        "&LB_OWNERSHIP_PARTY_ID=" + Url.Encode(model.LB_OWNERSHIP_PARTY_ID_JV_Itm) +
                                        "&LB_SURVEY_NO=" + Url.Encode(model.LB_SURVEY_NO_JV_Itm) +
                                        "&LB_CON_YEAR=" + Url.Encode(model.LB_CON_YEAR_JV_Itm) +
                                        "&LB_RCC_ROOF=" + Url.Encode(model.LB_RCC_ROOF_JV_Itm) +
                                        "&LB_PAID_DATE=" + Url.Encode(model.LB_PAID_DATE_JV_Itm) +
                                        "&LB_PERIOD_FROM=" + Url.Encode(model.LB_PERIOD_FROM_JV_Itm) +
                                        "&LB_PERIOD_TO=" + Url.Encode(model.LB_PERIOD_TO_JV_Itm) +
                                        "&LB_DOC_OTHERS=" + Url.Encode(model.LB_DOC_OTHERS_JV_Itm) +
                                        "&LB_DOC_NAME=" + Url.Encode(model.LB_DOC_NAME_JV_Itm) +
                                        "&LB_OTHER_DETAIL=" + Url.Encode(model.LB_OTHER_DETAIL_JV_Itm) +
                                        "&LB_TOT_P_AREA=" + model.LB_TOT_P_AREA_JV_Itm +
                                        "&LB_CON_AREA=" + model.LB_CON_AREA_JV_Itm +
                                        "&LB_DEPOSIT_AMT=" + model.LB_DEPOSIT_AMT_JV_Itm +
                                        "&LB_MONTH_RENT=" + model.LB_MONTH_RENT_JV_Itm +
                                        "&LB_MONTH_O_PAYMENTS=" + model.LB_MONTH_O_PAYMENTS_JV_Itm +
                                        "&LB_REC_ID=" + Url.Encode(model.LB_REC_ID_JV_Itm) +
                                        "&xID=" + Url.Encode(model.LB_REC_ID_JV_Itm);
                                    if (model.List_LB_DOCS_ARRAY_JV_Itm == null)
                                    {
                                        FetchLBDocuments(ref model);
                                        model.List_LB_DOCS_ARRAY_JV_Itm = new JavaScriptSerializer().Serialize(model.LB_DOCS_ARRAY_JV_Itm);
                                    }
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&List_LB_DOCS_ARRAY=" + Url.Encode(model.List_LB_DOCS_ARRAY_JV_Itm);
                                    if (model.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm == null)
                                    {
                                        FetchExtensionData(ref model);
                                        model.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm = new JavaScriptSerializer().Serialize(model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm);
                                    }
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&List_LB_EXTENDED_PROPERTY_TABLE=" + Url.Encode(model.List_LB_EXTENDED_PROPERTY_TABLE_JV_Itm);
                                }
                            }
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

                        case "WIP":
                            jsonParam.result = true;
                            jsonParam.popup_title = model.Me_Text_JV_Itm + " (WIP Detail)...";
                            jsonParam.popup_form_name = "Frm_WIP_Window";
                            jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_WIP_Window/";
                            jsonParam.popup_querystring = "LEDID=" + Url.Encode(model.iLed_ID_JV_Itm) +
                                "&Amount=" + model.Txt_Amt_JV_Itm + "&Tag=" + model.Tag_JV_Itm +
                                "&Reference=" + Url.Encode(model.iReference_JV_Itm) + "&xID=" + Url.Encode(model.iTxnM_ID_JV_Itm) +
                                "&iTxnM_ID=" + Url.Encode(model.iTxnM_ID_JV_Itm) +
                                "&FromJV=" + true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
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
                if ((model.iProfile_JV_Itm.ToUpper() != "NOT APPLICABLE" || model.Txt_ItemNature_JV_Itm.ToUpper() == "LAND & BUILDING") &&
                        (model.Cmb_RefType_JV_Itm == "Existing"))
                {
                    DataTable RefData = CommonFunctions.GetReferenceData(BASE, model.iProfile_JV_Itm, model.GLookUp_ItemList_JV_Itm,
                                        model.iTxnM_ID_JV_Itm, model.GLookUp_PartyList_JV_Itm, model.Tag_JV_Itm);
                    RefData_Jv_Ref = RefData;
                    jsonParam.result = true;
                    jsonParam.popup_title =  "Referenced Asset/Liability";
                    jsonParam.popup_form_name = "Frm_Voucher_Win_Journal_Reference";
                    jsonParam.popup_form_path = "/Account/JournalVoucher/Frm_Voucher_Win_Journal_Reference/";
                    if (RefData == null)
                    {
                        jsonParam.message = "There is no selectable reference for selected Item/Party...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_ItemList_JV_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (RefData.Rows.Count == 0)
                        {
                            jsonParam.message = "There is no selectable reference for selected Item/Party...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "GLookUp_ItemList_JV_Itm";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    jsonParam.popup_querystring = "Text=" + Url.Encode("Referenced Asset/Liability");
                    if (model.RdAction_JV_Itm == 0)
                    {
                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Action=" + Url.Encode("Debit");
                    }
                    else
                    {
                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Action=" + Url.Encode("Credit");
                    }
                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Amt=" + model.Txt_Amt_JV_Itm;
                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Item=" + Url.Encode(model.GLookUp_ItemName_JV_Itm);
                    if (model.Cross_RefID_JV_Itm != null && model.Cross_RefID_JV_Itm.Length > 0)
                    {
                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&SelectedRefID=" + Url.Encode(model.Cross_RefID_JV_Itm);
                    }
                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&iItemProfile=" + Url.Encode(model.iProfile_JV_Itm) +
                        "&SelectedItemID=" + Url.Encode(model.GLookUp_ItemList_JV_Itm) + "&Txt_Party=" + Url.Encode(model.GLookUp_PartyName_JV_Itm) +
                        "&iTxnM_ID=" + Url.Encode(model.iTxnM_ID_JV_Itm);

                    if (model.Txt_Qty_JV_Itm > 0)
                    {
                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Qty=" + model.Txt_Qty_JV_Itm;
                            
                    }
                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + model.Tag_JV_Itm;
                    
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                model.Cross_RefID_JV_Itm = null;
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

        public void FetchLBDocuments(ref JournalVoucher_Item model)
        {
            model.LB_DOCS_ARRAY_JV_Itm = new DataTable();
            model.LB_DOCS_ARRAY_JV_Itm.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
            model.LB_DOCS_ARRAY_JV_Itm.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

            var LB_DOC = new Common_Lib.Get_Data(BASE, "SYS", "LAND_BUILDING_EXTENDED_INFO",
                "SELECT LB_MISC_ID FROM LAND_BUILDING_DOCUMENTS_INFO where LB_REC_ID ='" + model.LB_REC_ID_JV_Itm + "'");
            foreach (DataRow XROW in LB_DOC._dc_DataTable.Rows)
            {
                DataRow Row = model.LB_DOCS_ARRAY_JV_Itm.NewRow();
                Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                Row["LB_REC_ID"] = model.LB_REC_ID_JV_Itm;
                model.LB_DOCS_ARRAY_JV_Itm.Rows.Add(Row);
            }
        }
        public void FetchExtensionData(ref JournalVoucher_Item model)
        {
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm = new DataTable();

            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

            var LB_Ext = new Common_Lib.Get_Data(BASE, "SYS", "LAND_BUILDING_EXTENDED_INFO", "SELECT LB_SR_NO, " +
                "LB_INS_ID, LB_TOT_P_AREA, LB_CON_AREA,LB_CON_YEAR, LB_MOU_DATE, LB_VALUE, " +
                "LB_OTHER_DETAIL FROM LAND_BUILDING_EXTENDED_INFO where LB_REC_ID ='" + model.LB_REC_ID_JV_Itm + "'");

            foreach (DataRow XROW in LB_Ext._dc_DataTable.Rows)
            {
                DataRow Row = model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.NewRow();
                Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"];
                Row["LB_SR_NO"] = XROW["LB_SR_NO"];
                Row["LB_INS_ID"] = XROW["LB_INS_ID"];
                Row["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"];
                Row["LB_CON_AREA"] = XROW["LB_CON_AREA"];
                Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"];
                Row["LB_VALUE"] = XROW["LB_VALUE"];
                Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"];
                Row["LB_REC_ID"] = model.LB_REC_ID_JV_Itm;
                model.LB_EXTENDED_PROPERTY_TABLE_JV_Itm.Rows.Add(Row);
            }
        }


        #endregion

        #region
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Journal_Reference(string Text, string Txt_Action, string Tag, string Txt_Amt, string Txt_Item, 
            string SelectedRefID, string iItemProfile, string SelectedItemID, string iTxnM_ID, string Txt_Qty, string Txt_Party = "")
        {
            //ProfilePayment_User_Rights();
            JournalVoucher_Reference model = new JournalVoucher_Reference();
            //RefreshMiscList();//Redmine Bug #134888 fix 
            //RefreshLocationList();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethodRefWindow = model.Tag.ToString();
            model.Txt_Action_JV_Ref = Txt_Action;
            model.Txt_Amt_JV_Ref = Convert.ToDouble(Txt_Amt);
            model.Txt_Item_JV_Ref = Txt_Item;
            model.SelectedRefID_JV_Ref = SelectedRefID;
            model.iItemProfile_JV_Ref = iItemProfile;
            model.SelectedItemID_JV_Ref = SelectedItemID;
            model.Txt_Party_JV_Ref = Txt_Party;
            //model.ReferenceData_JV_Ref = ReferenceData;
            model.Txt_Qty_JV_Ref = Convert.ToDouble(Txt_Qty);
            //ViewBag.FromDNK = FromDNK;
            //ViewBag.FromJV = FromJV;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Journal_Reference_Window(JournalVoucher_Reference model)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethodRefWindow.ToString());
                if (string.IsNullOrWhiteSpace(model.GLookUp_ReferenceList_JV_Ref))
                {
                    jsonParam.message = "No Reference Selected...!";
                    jsonParam.result = false;
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.focusid = "GLookUp_ReferenceList_JV_Ref";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }


                if ((model.iItemProfile_JV_Ref.ToUpper() != "OTHER LIABILITIES") && (model.iItemProfile_JV_Ref.ToUpper() != "OPENING"))
                {
                    if (model.SelectedRefID_JV_Ref != null)
                    {
                        if (model.SelectedRefID_JV_Ref != model.GLookUp_ReferenceList_JV_Ref)
                        {                                                    
                            if(Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.SelectedRefID_JV_Ref).Curr_Value) < 0) 
                            {                        
                                jsonParam.message = "Value of previously referenced asset becomes negative in Current Year...!";
                                jsonParam.result = false;
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.focusid = "Txt_Amt_JV_Ref";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }

                            if (model.iItemProfile_JV_Ref.ToUpper().Equals("WIP") || model.iItemProfile_JV_Ref.ToUpper().Equals("ADVANCES") ||
                                model.iItemProfile_JV_Ref.ToUpper().Equals("OTHER DEPOSITS") ||
                                model.iItemProfile_JV_Ref.ToUpper().Equals("OTHER LIABILITIES"))
                            {
                                if (BASE._next_Unaudited_YearID != Convert.ToInt32(null))
                                {
                                    if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.SelectedRefID_JV_Ref).Next_Year_Closing_Value) < 0)
                                    {
                                        jsonParam.message = "Value of previously referenced asset becomes negative in Next Year...!";
                                        jsonParam.result = false;
                                        jsonParam.title = "Incomplete Information..";
                                        jsonParam.focusid = "Txt_Amt_JV_Ref";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }


                    if (model.Txt_Action_JV_Ref.ToUpper() == "CREDIT")
                    {
                        if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value) < model.Txt_Amt_JV_Ref)
                        { // "- AdjDebit + AdjCredit" removed #Task 3864 dated 09/09/12
                            jsonParam.message = "Value reduced is greater than existing value...!";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information..";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if (model.iItemProfile_JV_Ref.ToUpper().Equals("WIP") || model.iItemProfile_JV_Ref.ToUpper().Equals("ADVANCES") ||
                            model.iItemProfile_JV_Ref.ToUpper().Equals("OTHER DEPOSITS") ||
                            model.iItemProfile_JV_Ref.ToUpper().Equals("OTHER LIABILITIES"))
                        {
                            if (BASE._next_Unaudited_YearID != Convert.ToInt32(null))
                            {
                                if (GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value != Convert.ToDecimal(null) &&
                                    (Convert.IsDBNull(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value)))
                                {
                                    if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value) < (model.Txt_Amt_JV_Ref))
                                    {
                                        jsonParam.message = "Value reduced is greater than existing value in next year...!";
                                        jsonParam.title = "Incomplete Information..";
                                        jsonParam.focusid = "Txt_Amt_JV_Ref";
                                        jsonParam.result = false;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value) + 
                            model.Txt_Amt_JV_Ref < 0)
                        { // "- AdjDebit + AdjCredit" removed #Task 3864 dated 09/09/12
                            jsonParam.message = "Value reduced is greater than existing value...!";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.focusid = "Txt_Amt_JV_Ref";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if (BASE._next_Unaudited_YearID != Convert.ToInt32(null))
                        {
                            if (GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value != Convert.ToDecimal(null) &&
                                !(Convert.IsDBNull(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value)))
                            {
                                if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value) +
                                    model.Txt_Amt_JV_Ref < 0)
                                {
                                    jsonParam.message = "Value reduced is greater than existing value in next year...!";
                                    jsonParam.result = false;
                                    jsonParam.title = "Incomplete Information..";
                                    jsonParam.focusid = "Txt_Amt_JV_Ref";
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }

                if (model.iItemProfile_JV_Ref.ToUpper() == "OTHER LIABILITIES" && model.iItemProfile_JV_Ref.ToUpper() != "OPENING")
                {
                    if (model.Txt_Action_JV_Ref.ToUpper() == "DEBIT")
                    {
                        if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value) < model.Txt_Amt_JV_Ref)
                        {
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.message = "Value reduced is greater than existing value. . . !";
                            jsonParam.focusid = "Txt_Amt_JV_Ref";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (BASE._next_Unaudited_YearID != Convert.ToInt32(null))
                        {
                            if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value) < model.Txt_Amt_JV_Ref)
                            {
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.message = "Value reduced is greater than existing value in next year...!";
                                jsonParam.focusid = "Txt_Amt_JV_Ref";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value) + model.Txt_Amt_JV_Ref < 0)
                        {
                            jsonParam.title = "Incomplete Information..";
                            jsonParam.message = "Value reduced is greater than existing value. . . !";
                            jsonParam.focusid = "Txt_Amt_JV_Ref";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);


                        }
                        if (BASE._next_Unaudited_YearID != Convert.ToInt32(null))
                        {
                            if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value) + model.Txt_Amt_JV_Ref < 0)
                            {
                                jsonParam.title = "Incomplete Information..";
                                jsonParam.message = "Value reduced is greater than existing value in next year. . . !";
                                jsonParam.focusid = "Txt_Amt_JV_Ref";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }


                if (model.Txt_Action_JV_Ref.ToUpper() == "CREDIT" && model.iItemProfile_JV_Ref == "OTHER ASSETS")
                {
                    if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Qty) < model.Txt_Qty_JV_Ref)
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Quantity reduced is greater than existing quantity. . . !";
                        jsonParam.focusid = "Txt_Qty_JV_Ref";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);


                    }

                    if ((Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Qty) == model.Txt_Qty_JV_Ref ||
                        Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value) == model.Txt_Amt_JV_Ref) &&
                        (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Qty) != model.Txt_Qty_JV_Ref ||
                        Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value)!= model.Txt_Amt_JV_Ref))
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Quantity & Amount Both need to become zero Simultaneously . . . !";
                        jsonParam.focusid = "Txt_Qty_JV_Ref";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (model.Txt_Action_JV_Ref.ToUpper() == "CREDIT" && (model.iItemProfile_JV_Ref == "GOLD" || model.iItemProfile_JV_Ref == "SILVER"))
                {
                    if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Weight) < model.Txt_Qty_JV_Ref)
                    { //- AdjDebitQty + AdjCreditQty removed #Task 3864 dated 09/09/12
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Weight reduced is greater than existing quantity. . . !";
                        jsonParam.focusid = "Txt_Qty_JV_Ref";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if ((Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Weight) == model.Txt_Qty_JV_Ref ||
                        Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value) == model.Txt_Amt_JV_Ref) &&
                       (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Weight) != model.Txt_Qty_JV_Ref ||
                      Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value) != model.Txt_Amt_JV_Ref))
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Weight & Amount Both Need to become Zero Simultaneously . . . !";
                        jsonParam.focusid = "Txt_Qty_JV_Ref";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (model.Txt_Action_JV_Ref.ToUpper() == "CREDIT" && (model.iItemProfile_JV_Ref == "WIP"))
                {
                    if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Curr_Value) + model.Txt_Amt_JV_Ref < 0)
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Sorry !  Updating of Reference Amount creates a Negative Closing Balance in Current Year for WIP( " +
                            Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Reference) +
                            " ) with Original Value " +
                            Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Org_Value);
                        jsonParam.focusid = "GLookUp_ReferenceList";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if (BASE._next_Unaudited_YearID != Convert.ToInt32(null))
                    {
                        if (Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Next_Year_Closing_Value) + 
                            model.Txt_Amt_JV_Ref < 0)
                        {
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.message = "Sorry !  Updating of Reference Amount creates a Negative Closing Balance in Next Year for WIP( " +
                                Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Reference) +
                                " ) with Original Value " +
                                Convert.ToDouble(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).Org_Value);
                            jsonParam.focusid = "GLookUp_ReferenceList";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }                
                jsonParam.result = true;
                jsonParam.closeform = true;
                return Json(new { jsonParam,
                    Cross_RefID = model.GLookUp_ReferenceList_JV_Ref,
                    TXT_Reference = model.GLookUp_ReferenceName_JV_Ref,
                    RefItem_RecEditOn = IsDate((GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).REC_EDIT_ON).ToString()) ? Convert.ToDateTime(GetRefList_JV_Ref.Find(x => x.REC_ID == model.GLookUp_ReferenceList_JV_Ref).REC_EDIT_ON).ToString(BASE._Server_Date_Format_Long) : string.Empty,
                    Txt_Qty = model.Txt_Qty_JV_Ref
                }, JsonRequestBehavior.AllowGet);
            }// try end
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br><br>", ex.Message);
                jsonParam.message = msg;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region All Dropdown load actions

        public ActionResult LookUp_GetItemList_Itm(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetItemList_Itm == null || DDRefresh == true)
            {
                RefreshItemList_Itm();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetItemList_Itm, loadOptions)), "application/json");
        }
        public void RefreshItemList_Itm()
        {
            var d1 = BASE._Journal_voucher_DBOps.GetLedgerItems(BASE.Is_HQ_Centre);
            DataView dview = new DataView((DataTable)d1);
            dview.Sort = "ITEM_NAME";
            GetItemList_Itm = DatatableToModel.DataTableto_ItemList_Itm_JV(dview.ToTable());
        }

        public ActionResult LookUp_GetPartyList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (Jv_Party_DD_Data == null || DDRefresh == true)
            {
                RefreshPartyList();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(Jv_Party_DD_Data, loadOptions)), "application/json");
        }
        public void RefreshPartyList()
        {
            DataTable d1 = BASE._Journal_voucher_DBOps.GetParties("Name", "ID");
            DataView dview = new DataView(d1);
            dview.Sort = "Name";
            Jv_Party_DD_Data = DatatableToModel.DataTabletoJournalVoucherLookUp_GetPartyList(dview.ToTable());
        }


        public ActionResult LookUp_GetPurList_Itm(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetPurList_Itm == null || DDRefresh == true)
            {
                RefreshPurList_Itm_Dtel();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetPurList_Itm, loadOptions)), "application/json");
        }
        public void RefreshPurList_Itm_Dtel()
        {
            DataTable d1 = BASE._Journal_voucher_DBOps.GetProjects("PUR_NAME", "PUR_ID");
            DataView dview = new DataView(d1);
            GetPurList_Itm = DatatableToModel.DataTableto_PurList_Itm_Jv(d1);
        }
        #endregion

        #region Journal Reference Dropdownbox lookup function
        public ActionResult LookUp_GetReferenceList_JV_Ref(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if(GetRefList_JV_Ref == null || DDRefresh == true)
            {
                JournalVoucher_Reference model = new JournalVoucher_Reference();
                DataTable d3 = RefData_Jv_Ref;
                //int ctr = 0;
                if (d3 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                
                GetRefList_JV_Ref = DatatableToModel.DataTableto_RefList_Jv_Ref(d3);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetRefList_JV_Ref, loadOptions)), "application/json");

            //foreach (DataColumn col in d3.Columns) 
            //{
            //    DevExpress.XtraGrid.Columns.GridColumn  cCol = new DevExpress.XtraGrid.Columns.GridColumn
            //    cCol.FieldName = col.ColumnName;
            //    cCol.Name = col.ColumnName;
            //    cCol.Visible = True;
            //    cCol.Caption = col.ColumnName;
            //    cCol.VisibleIndex = ctr;
            //    GLookUp_ReferenceListView.Columns.Add(cCol);
            //    ctr += 1;
            //}



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
        public void SessionClear()
        {
            ClearBaseSession("_JV");
            ClearBaseSession("_JV_Ref");
            BASE._SessionDictionary.Remove("Payment_Asset_Image_Payment");
        }
        #endregion

    }
}