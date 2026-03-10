using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Controllers;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using System.Data;
using ConnectOneMVC.Helper;
using Common_Lib;
using System.Collections;
using ConnectOneMVC.Models;
using System.Web.Script.Serialization;
using DevExpress.Web.Mvc;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class DonationInKindVoucherController : BaseController
    {
        // GET: Account/DonationInKindVoucher
        #region Global Variables
        public byte[] DNK_Asset_Image
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
        public List<PartyList1_DNK> GetPartyList_DNK
        {
            get { return (List<PartyList1_DNK>)GetBaseSession("GetPartyList_DNK"); }
            set { SetBaseSession("GetPartyList_DNK", value); }
        }
        public List<ItemList_Itm_Dtel> GetItemList_Itm_Dtel
        {
            get { return (List<ItemList_Itm_Dtel>)GetBaseSession("GetItemList_Itm_Dtel_DNK"); }
            set { SetBaseSession("GetItemList_Itm_Dtel_DNK", value); }
        }
        public List<PurList_Itm_Dtel> GetPurList_Itm_Dtel
        {
            get { return (List<PurList_Itm_Dtel>)GetBaseSession("GetPurList_Itm_Dtel_DNK"); }
            set { SetBaseSession("GetPurList_Itm_Dtel_DNK", value); }
        }
        public DataTable DT_DNK
        {
            get { return (DataTable)GetBaseSession("DT_DNK"); }
            set { SetBaseSession("DT_DNK", value); }
        }
        public DataTable LB_DOCS_ARRAY
        {
            get { return (DataTable)GetBaseSession("LB_DOCS_ARRAY_DNK"); }
            set { SetBaseSession("LB_DOCS_ARRAY_DNK", value); }
        }
        public DataTable LB_EXTENDED_PROPERTY_TABLE
        {
            get { return (DataTable)GetBaseSession("LB_EXTENDED_PROPERTY_TABLE_DNK"); }
            set { SetBaseSession("LB_EXTENDED_PROPERTY_TABLE_DNK", value); }
        }
        public bool iParty_Req
        {
            get { return (bool)GetBaseSession("iParty_Req_DNK"); }
            set { SetBaseSession("iParty_Req_DNK", value); }
        }
        public double? Txt_SubTotal
        {
            get { return (double?)GetBaseSession("Txt_SubTotal_DNK"); }
            set { SetBaseSession("Txt_SubTotal_DNK", value); }
        }

        #endregion
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Gift(string Tag = "", string xID = "", string xMID = "", string Info_LastEditedOn = "", string iSpecific_ItemID = "", string GridToRefresh = "CashBookListGrid")
        {
            DonationInKind model = new DonationInKind();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            Common.Navigation_Mode[] AM = { Common.Navigation_Mode._New, Common.Navigation_Mode._New_From_Selection, Common.Navigation_Mode._Edit, Common.Navigation_Mode._View, Common.Navigation_Mode._Delete };
            for (int i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, ClientScreen.Accounts_Voucher_Gift, Rights[i]) && model.Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights')</script>");//Code written for User Authorization do not remove
                }
            }
            ViewBag.GridToRefresh = GridToRefresh;
            iParty_Req = false;
            ViewData["Donation_DNK_AddFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
            ViewData["Donation_DNK_ListFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");
            model.ActionMethod = model.Tag.ToString();
            model.xID = xID;
            model.xMID = xMID;
            model.TitleX = "Donation in Kind";
            RefreshPartyList_DNK();
            SetGridData_DNK();

            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_DNK = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_DNK = model.SpecialReferenceList_Data_DNK.Count();

            if (model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                model.iSpecific_ItemID = iSpecific_ItemID;
                model.Me_Text = "New ~ " + model.TitleX;
            }
            else if (model.Tag == Common.Navigation_Mode._New)
            {
                model.Me_Text = "New ~ " + model.TitleX;
            }
            else if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._View || model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Me_Text = "Edit ~ " + model.TitleX;

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_DNK = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                if (!string.IsNullOrWhiteSpace(Info_LastEditedOn))
                {
                    model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
                }
                DataTable d1 = BASE._Gift_DBOps.GetMasterRecord(model.xMID);
                DataTable d3 = BASE._Gift_DBOps.GetRecord(model.xMID);
                DataTable d4 = BASE._Gift_DBOps.GetTxnItems(model.xMID);
                if (d1 == null || d3 == null || d4 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                model.Txt_V_Date_DNK = Convert.ToDateTime(d1.Rows[0]["TR_DATE"]);
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
                    {
                        string viewstr = "";
                        if (model.Tag == Common.Navigation_Mode._View)
                        {
                            viewstr = "view";
                        }
                        if (model.Info_LastEditedOn != Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]))
                        {
                            string message = Messages.RecordChanged("Current Gift", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }
                Data_Binding_DNK(d1, d3, d4, ref model);
            }
            if (model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Me_Text = "Delete ~ " + model.TitleX;
            }
            if (model.Tag == Common.Navigation_Mode._View)
            {
                model.Me_Text = "View ~ " + model.TitleX;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Gift_Window(DonationInKind model)
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
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                {
                    foreach (DataRow XRow in DT_DNK.Rows)
                    {
                        if (XRow["Item_Profile"].ToString() == "LAND & BUILDING" || XRow["Item_Profile"].ToString() == "OTHER ASSETS")
                        {
                            if (BASE.IsInsuranceAudited())
                            {
                                jsonParam.message = "Insurance Related Assets Cannot be Added/Edited After The Completion of Insurance Audit";
                                jsonParam.result = false;
                                jsonParam.title = "Information...";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (XRow["ITEM_VOUCHER_TYPE"].ToString().Trim().ToUpper() == "LAND & BUILDING" && XRow["Item_Profile"].ToString().ToUpper() != "LAND & BUILDING")
                        {        //' L&B Expense Item
                            if (BASE.IsInsuranceAudited())
                            {
                                jsonParam.message = "Property Related Expenses Cannot be Added/Edited After The Completion of Insurane Audit";
                                jsonParam.result = false;
                                jsonParam.title = "Information...";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                //'-----------------------------+
                //'Start : Check if entry already changed 
                //'-----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit || model.Tag == Common_Lib.Common.Navigation_Mode._Delete)
                    {
                        DataTable gift_DbOps = BASE._Gift_DBOps.GetRecord(model.xMID);
                        if (gift_DbOps == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (gift_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Gift");
                            jsonParam.result = false;
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(gift_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Gift");
                            jsonParam.result = false;
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        Object MaxValue = 0;
                        MaxValue = BASE._Gift_DBOps.GetStatus(model.xMID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found . . . !";
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
                        foreach (DataRow cRow in AssetItems.Rows) //' Get Actual Item IDs from Selected Transaction
                        {
                            string xTemp_ItemID = cRow[0].ToString();
                            DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID); //'Gets Asset Profile
                            string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                            if (xTemp_AssetProfile.ToUpper() != "NOT APPLICABLE")   //' Leaving Constuction Items
                            {
                                string xTemp_AssetID = "";
                                switch (xTemp_AssetProfile) //' Get Asset RecID from Particular Table 
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
                                    if (!(SaleRecord == null))
                                    {
                                        if (SaleRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for " + (model.Tag == Common_Lib.Common.Navigation_Mode._Edit ? "editing" : "deleting") + " this Entry.";
                                            jsonParam.result = false;
                                            jsonParam.title = "Error!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    //'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                    DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, model.xMID);
                                    if (!(ReferenceRecord == null))
                                    {
                                        if (ReferenceRecord.Rows.Count > 0)
                                        {
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " + Convert.ToDateTime(ReferenceRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " of Rs." + ReferenceRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for " + (model.Tag == Common_Lib.Common.Navigation_Mode._Edit ? "editing" : "deleting") + " this Entry.";
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
                                            jsonParam.message = "Sorry ! Selected Entry contains a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for " + (model.Tag == Common_Lib.Common.Navigation_Mode._Edit ? "editing" : "deleting") + " this Entry.";
                                            jsonParam.result = false;
                                            jsonParam.title = "Error!!";
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //'Non Profile Entries 
                                string RefID = BASE._Voucher_DBOps.GetReferenceRecordID(model.xMID);
                                if (!(RefID == null))
                                {
                                    if (RefID.Length > 0)
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(RefID); //'checks if the referred property for constt items has been sold 
                                        if (!(SaleRecord == null))
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for " + (model.Tag == Common_Lib.Common.Navigation_Mode._Edit ? "editing" : "deleting") + " this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Convert.ToInt32(null), RefID); //'checks if the referred property for constt items has been transfered 
                                        if (AssetTrfRecord.Rows.Count > 0)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for " + (model.Tag == Common_Lib.Common.Navigation_Mode._Edit ? "editing" : "deleting") + " this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //    '-----------------------------+
                //'End : Check if entry already changed 
                //'-----------------------------+

                if (model.Tag == Common_Lib.Common.Navigation_Mode._New ||
                model.Tag == Common_Lib.Common.Navigation_Mode._Edit ||
                model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_PartyList1_DNK))
                    {
                        jsonParam.message = "Donor Not Selected. . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_PartyList1_DNK";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.BE_COUNTRY_DNK))
                    {
                        jsonParam.message = "Donor Address Incomplete. . . !" + "<br>" + "Mandatory: Address Line.1 & Country...";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_PartyList1_DNK";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (model.BE_COUNTRY_DNK.ToUpper() == "INDIA")
                        {
                            if ((string.IsNullOrWhiteSpace(model.BE_ADD1_DNK)) || (string.IsNullOrWhiteSpace(model.BE_City_DNK)) || (string.IsNullOrWhiteSpace(model.BE_DISTRICT_DNK)) || (string.IsNullOrWhiteSpace(model.BE_STATE_DNK)) || (string.IsNullOrWhiteSpace(model.BE_COUNTRY_DNK)))
                            {
                                jsonParam.message = "Donor Address Incomplete. . . !" + "<br>" + "Mandatory: Address Line.1, City, District, State & Country...";
                                jsonParam.result = false;
                                jsonParam.title = "Incomplete Information . . .";
                                jsonParam.focusid = "GLookUp_PartyList1_DNK";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if ((string.IsNullOrWhiteSpace(model.BE_ADD1_DNK)) || (string.IsNullOrWhiteSpace(model.BE_COUNTRY_DNK)))
                            {
                                jsonParam.message = "Donor Address Incomplete. . . !" + "<br>" + "Mandatory: Address Line.1 & Country...";
                                jsonParam.result = false;
                                jsonParam.title = "Incomplete Information . . .";
                                jsonParam.focusid = "GLookUp_PartyList1_DNK";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    model.BE_Party_Category_DNK = string.IsNullOrWhiteSpace(model.BE_Party_Category_DNK) ? "" : model.BE_Party_Category_DNK;
                    if (model.BE_Party_Category_DNK.ToUpper() != "GOVT ENTITY")
                    {
                        if (BASE._open_Year_ID >= 2122 && BASE._open_Ins_ID != "00001" && BASE._open_Ins_ID != "00005")
                        {
                            if (string.IsNullOrWhiteSpace(model.BE_PAN_No_DNK) && string.IsNullOrWhiteSpace(model.BE_UID_No_DNK) && string.IsNullOrWhiteSpace(model.BE_ID_No_DNK))
                            {
                                jsonParam.message = "Atleast one of the PAN/Aadhar No./Passport No./ Voter ID/Ration Card No./Driving License is Compulsory for Regular donation...!";
                                jsonParam.result = false;
                                jsonParam.closeform = false;
                                jsonParam.title = "Incomplete Information...";
                                jsonParam.focusid = "BE_PAN_No_DNK";
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    if (IsDate(model.Txt_V_Date_DNK.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank. . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_V_Date_DNK";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_DNK.ToString()) == true)
                    {
                        //'1    //'2                    
                        if (model.Txt_V_Date_DNK < BASE._open_Year_Sdt || model.Txt_V_Date_DNK > BASE._open_Year_Edt)
                        {
                            jsonParam.message = "Date not as per Financial Year. . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_V_Date_DNK";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (DT_DNK.Rows.Count <= 0)
                    {
                        jsonParam.message = "Item Detail Not Specified. . . !";
                        jsonParam.result = false;
                        jsonParam.title = "Error . . .";
                        jsonParam.focusid = "Win_Gift_Item_Grid";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)  //' Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit) //' Removed this check on updation as we dont recreate location on updation of property creation voucher
                {//'Properties Created in Current Voucher
                    DataTable d1 = BASE._L_B_DBOps.GetIDsBytxnID(model.xMID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift);
                    foreach (DataRow cRow in d1.Rows)
                    {
                        string Msg = FindLocationUsage(model.Tag, cRow[0].ToString(), false); //'sold/tf assets not excluded
                        if (Msg.Length > 0)
                        {
                            jsonParam.message = Msg;
                            jsonParam.result = false;
                            jsonParam.title = model.Me_Text;
                            jsonParam.focusid = "Win_Gift_Item_Grid";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }


                //'---------------------------// Start Dependencies //------------------------------------------

                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection || model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                    {
                        DataTable d1 = BASE._Gift_DBOps.GetParties(model.GLookUp_PartyList1_DNK); //'party not deleted yet
                        if (d1 == null)
                        {
                            jsonParam.message = Common_Lib.Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error!!";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (d1.Rows.Count <= 0)
                        {
                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                            jsonParam.result = false;
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (d1.Rows[0]["CO_NAME"].ToString().Length <= 0)
                        {
                            jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                            jsonParam.result = false;
                            jsonParam.title = "Referred Record Already Changed!!";
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (d1.Rows[0]["CO_NAME"].ToString() == "INDIA")
                            {
                                if (d1.Rows[0]["C_R_ADD1"].ToString().Length <= 0 || d1.Rows[0]["CI_NAME"].ToString().Length <= 0 || d1.Rows[0]["ST_NAME"].ToString().Length <= 0 || d1.Rows[0]["DI_NAME"].ToString().Length <= 0 || d1.Rows[0]["CO_NAME"].ToString().Length <= 0)
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                                    jsonParam.result = false;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (d1.Rows[0]["C_R_ADD1"].ToString().Length <= 0)
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book");
                                    jsonParam.result = false;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        var Party_Category = Convert.IsDBNull(d1.Rows[0]["C_CATEGORY"]) ? "" : d1.Rows[0]["C_CATEGORY"].ToString();
                        if (Party_Category != "GOVT ENTITY")
                        {
                            if (BASE._open_Year_ID >= 2122 && BASE._open_Ins_ID != "00001" && BASE._open_Ins_ID != "00005")
                            {
                                if (d1.Rows[0]["C_PAN_NO"].ToString().Length <= 0 && d1.Rows[0]["C_UID_NO"].ToString().Length <= 0 && d1.Rows[0]["C_OTHER_ID"].ToString().Length <= 0)
                                {
                                    jsonParam.message = Common_Lib.Messages.DependencyChanged("Address Book"); ;
                                    jsonParam.result = false;
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        //Checks for deletion of asset location in background in all assets
                        foreach (DataRow XRow in DT_DNK.Rows)
                        {
                            Object cnt;
                            var Loc_ID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            if (!(Loc_ID == null))
                            {
                                if (Loc_ID.ToString().Length > 0)
                                {
                                    Object PropertyId = BASE._AssetLocDBOps.GetPropertyID(Loc_ID);
                                    if (!(PropertyId == null) && !Convert.IsDBNull(PropertyId))
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(PropertyId.ToString()); //'checks if the referred property for constt items has been sold 
                                        if (!(SaleRecord == null))
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a property which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for completing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.refreshgrid = true;
                                                jsonParam.closeform = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Convert.ToInt32(null), PropertyId.ToString()); //'checks if the referred property for constt items has been transferred 
                                        if (AssetTrfRecord != null)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a property which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for editing this Entry.";
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
                                            jsonParam.closeform = true;
                                            jsonParam.refreshgrid = true;
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
                                            jsonParam.closeform = true;
                                            jsonParam.refreshgrid = true;
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
                                            jsonParam.closeform = true;
                                            jsonParam.refreshgrid = true;
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
                                            jsonParam.closeform = true;
                                            jsonParam.refreshgrid = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }

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
                                if (!(PartyID == null))
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
                            if (XRow["LB_REC_ID"].ToString().Length > 0 && (!(XRow["Item_Profile"].ToString() == "LAND & BUILDING"))) //'Select Property screen
                            {
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

                            //'Location Specific Checks
                            if (XRow["Item_Profile"].ToString() == "LAND & BUILDING")
                            {
                                foreach (DataRow cRow in DT_DNK.Rows)
                                {
                                    if ((Convert.IsDBNull(XRow["LB_PRO_NAME"]) ? "" : XRow["LB_PRO_NAME"]).Equals((Convert.IsDBNull(cRow["LB_PRO_NAME"]) ? "" : cRow["LB_PRO_NAME"])) && !((Convert.IsDBNull(XRow["LB_REC_ID"]) ? "" : XRow["LB_REC_ID"]).Equals((Convert.IsDBNull(cRow["LB_REC_ID"]) ? "" : cRow["LB_REC_ID"]))))
                                    {
                                        jsonParam.message = "Property/Location With Same Name Already Available in same voucher. . . !";
                                        jsonParam.result = false;
                                        jsonParam.title = "Property Name Duplicate";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                                {
                                    Object MaxValue_Loc = 0;
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
                                if (!(LocNames == null))
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
                        }
                    }
                }

                //'------------------------------// End Dependencies //--------------------------------

                //'+----JV LEDGER DETAIL----+
                string JV_Item_ID = "d0a33061-d679-4f21-ac12-a29541de8fcb";
                string JV_Cr_Led_id = "";
                //'Dim GIFT_SQL_1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID  FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='" & JV_Item_ID & "' AND UCASE(ITEM_TRANS_TYPE)='CREDIT' "
                DataTable GIFT_DT = BASE._Gift_DBOps.GetItemsList(JV_Item_ID);
                if (GIFT_DT == null)
                {
                    jsonParam.message = Common_Lib.Messages.SomeError;
                    jsonParam.result = false;
                    jsonParam.title = "Error!!";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (GIFT_DT.Rows.Count > 0)
                {
                    JV_Cr_Led_id = GIFT_DT.Rows[0]["ITEM_LED_ID"].ToString();
                }
                else
                {
                    jsonParam.message = "Donation - Gift Item Not Found..!";
                    jsonParam.result = false;
                    jsonParam.title = "Donation By Gift...";
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                //'+----------END-----------+

                Common_Lib.RealTimeService.Param_Txn_Insert_VoucherGift InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_VoucherGift();

                if (model.Tag == Common_Lib.Common.Navigation_Mode._New || model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)  //'new
                {
                    //'Dim xDate As DateTime = null : xDate = New Date(Me.Txt_V_Date.DateTime.Year, Me.Txt_V_Date.DateTime.Month, Me.Txt_V_Date.DateTime.Day)
                    model.xMID = System.Guid.NewGuid().ToString();
                    string STR1 = "";
                    int xCnt = 1;
                    Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherGift InMInfo = new Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherGift();
                    InMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Donation_Gift;
                    InMInfo.VNo = model.Txt_V_NO_DNK ?? "";
                    if (IsDate(model.Txt_V_Date_DNK.ToString()))
                    {
                        InMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date_DNK).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMInfo.TDate = model.Txt_V_Date_DNK.ToString();
                    }
                    //'InMInfo.TDate = Me.Txt_V_Date.Text
                    InMInfo.PartyID = model.GLookUp_PartyList1_DNK ?? "";
                    InMInfo.SubTotal = Convert.ToDouble(model.Txt_SubTotal_DNK);
                    InMInfo.Cash = 0;
                    InMInfo.Bank = 0;
                    InMInfo.Advance = 0;
                    InMInfo.Liability = 0;
                    InMInfo.Credit = 0;
                    InMInfo.TDS = 0;
                    InMInfo.Status_Action = Status_Action;
                    InMInfo.RecID = model.xMID;

                    InNewParam.param_InsertMaster = InMInfo;

                    Parameter_Insert_VoucherGift[] Insert = new Common_Lib.RealTimeService.Parameter_Insert_VoucherGift[DT_DNK.Rows.Count + 1];
                    Parameter_InsertItem_VoucherGift[] InsertItem = new Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift[DT_DNK.Rows.Count + 1];
                    Parameter_InsertPurpose_VoucherGift[] InsertPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift[DT_DNK.Rows.Count + 1];
                    Parameter_InsertTRIDAndTRSRNo_GoldSilver[] InsertGS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver[DT_DNK.Rows.Count + 1];
                    Parameter_InsertTRIDAndTRSrNo_Assets[] InsertAssets = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets[DT_DNK.Rows.Count + 1];
                    Parameter_InsertTRIDAndTRSrNo_LiveStock[] InsertLivestock = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock[DT_DNK.Rows.Count + 1];
                    Parameter_InsertTRIDAndTRSrNo_Vehicles[] InsertVehicles = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles[DT_DNK.Rows.Count + 1];
                    Parameter_InsertMasterIDAndSrNo_LandAndBuilding[] InsertProperty = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding[DT_DNK.Rows.Count + 1];
                    Param_InsertTRIDAndTRSrNo_WIP_Profile[] InsertReferencesWIP = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile[DT_DNK.Rows.Count + 1];

                    foreach (DataRow XRow in DT_DNK.Rows)
                    {
                        string Cross_Ref_ID = "";
                        Common_Lib.Common.Voucher_Screen_Code ScreenCode = Common_Lib.Common.Voucher_Screen_Code.Payment;

                        if (XRow["LB_REC_ID"].ToString().Length > 0 && (!(XRow["Item_Profile"].ToString() == "LAND & BUILDING")))  //'Bug #5637
                        {
                            Cross_Ref_ID = XRow["LB_REC_ID"].ToString();
                            if (BASE.AllowMultiuser())  //' Ref A/AE in AO33
                            {
                                if (!(Cross_Ref_ID == null))
                                {
                                    if (Cross_Ref_ID.Length > 0)
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(Cross_Ref_ID); //'checks if the referred property for constt items has been sold 
                                        if (!(SaleRecord == null))
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.closeform = true;
                                                jsonParam.refreshgrid = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Convert.ToInt32(null), Cross_Ref_ID); //'checks if the referred property for constt items has been transferred 
                                        if (AssetTrfRecord != null)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.closeform = true;
                                                jsonParam.refreshgrid = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (XRow["REF_REC_ID"].ToString().Length > 0)
                        {
                            Cross_Ref_ID = "'" + XRow["REF_REC_ID"] + "'";
                        }

                        model.xID = System.Guid.NewGuid().ToString();
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherGift InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherGift();
                        InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Donation_Gift;
                        InParam.VNo = model.Txt_V_NO_DNK ?? "";
                        if (IsDate(model.Txt_V_Date_DNK.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_DNK).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = model.Txt_V_Date_DNK.ToString();
                        }
                        //'InParam.TDate = Me.Txt_V_Date.Text
                        InParam.ItemID = XRow["Item_ID"].ToString();
                        InParam.Type = "DEBIT";
                        InParam.Cr_Led_ID = "";
                        InParam.Dr_Led_ID = XRow["Item_Led_ID"].ToString();
                        InParam.Sub_Cr_Led_ID = "";
                        InParam.Mode = "GIFT";
                        InParam.Ref_No = "";
                        InParam.Amount = Convert.ToDouble(XRow["Amount"]);
                        InParam.PartyID = model.GLookUp_PartyList1_DNK;
                        InParam.Narration = model.Txt_Narration_DNK ?? "";
                        InParam.Remarks = XRow["Remarks"].ToString() ?? "";
                        InParam.Reference = model.Txt_Reference_DNK ?? "";
                        InParam.Tr_M_ID = model.xMID;
                        InParam.TxnSrNo = Convert.ToInt32(XRow["Sr."]);
                        InParam.Status_Action = Status_Action;
                        InParam.RecID = model.xID;
                        InParam.Cross_Ref_Id = Cross_Ref_ID;

                        Insert[xCnt] = InParam;
                        xCnt += 1;
                    }
                    InNewParam.Insert = Insert;

                    //'JV Entry
                    model.xID = System.Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_Insert_VoucherGift InParam1 = new Common_Lib.RealTimeService.Parameter_Insert_VoucherGift();
                    InParam1.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Donation_Gift;
                    InParam1.VNo = model.Txt_V_NO_DNK ?? "";
                    if (IsDate(model.Txt_V_Date_DNK.ToString()))
                    {
                        InParam1.TDate = Convert.ToDateTime(model.Txt_V_Date_DNK).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam1.TDate = model.Txt_V_Date_DNK.ToString();
                    }
                    //'InParam1.TDate = Me.Txt_V_Date.Text
                    InParam1.ItemID = JV_Item_ID;
                    InParam1.Type = "CREDIT";
                    InParam1.Cr_Led_ID = JV_Cr_Led_id;
                    InParam1.Dr_Led_ID = "";
                    InParam1.Sub_Cr_Led_ID = "";
                    InParam1.Mode = "GIFT";
                    InParam1.Ref_No = "";
                    InParam1.Amount = Convert.ToDouble(model.Txt_SubTotal_DNK);
                    InParam1.PartyID = model.GLookUp_PartyList1_DNK;
                    InParam1.Narration = model.Txt_Narration_DNK ?? "";
                    InParam1.Remarks = "";
                    InParam1.Reference = model.Txt_Reference_DNK ?? "";
                    InParam1.Tr_M_ID = model.xMID;
                    InParam1.TxnSrNo = xCnt;
                    InParam1.Status_Action = Status_Action;
                    InParam1.RecID = model.xID;
                    InParam1.Cross_Ref_Id = "";

                    InNewParam.param_InsertJV = InParam1;
                    STR1 = "";


                    //'Main Items
                    int cnt = 0;

                    foreach (DataRow XRow in DT_DNK.Rows)
                    {
                        Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift InItem = new Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift();
                        InItem.Txn_M_ID = model.xMID;
                        InItem.TxnSrNo = Convert.ToInt32(XRow["Sr."]);
                        InItem.ItemID = XRow["Item_ID"].ToString();
                        InItem.LedID = XRow["Item_Led_ID"].ToString();
                        InItem.Type = XRow["Item_Trans_Type"].ToString();
                        InItem.PartyReq = XRow["Item_Party_Req"].ToString();
                        InItem.Profile = XRow["Item_Profile"].ToString();
                        InItem.ItemName = XRow["Item Name"].ToString();
                        InItem.Head = XRow["Head"].ToString();
                        InItem.Qty = Convert.ToDouble(XRow["Qty."]);
                        InItem.Unit = XRow["Unit"].ToString();
                        InItem.Rate = Convert.ToDouble(XRow["Rate"]);
                        InItem.Amount = Convert.ToDouble(XRow["Amount"]);
                        InItem.Remarks = XRow["Remarks"].ToString() ?? "";
                        InItem.Status_Action = Status_Action;
                        InItem.RecID = System.Guid.NewGuid().ToString();

                        InsertItem[cnt] = InItem;

                        //'Purpose.........

                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift InPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift();
                        InPurpose.TxnID = model.xMID;
                        InPurpose.PurposeID = XRow["Pur_ID"].ToString();
                        InPurpose.Amount = Convert.ToDouble(XRow["Amount"]);
                        InPurpose.ItemSrNo = Convert.ToInt32(XRow["Sr."]);
                        InPurpose.Status_Action = Status_Action;
                        InPurpose.RecID = System.Guid.NewGuid().ToString();

                        InsertPurpose[cnt] = InPurpose;

                        if (XRow["Item_Profile"].ToString() == "GOLD" || XRow["Item_Profile"].ToString() == "SILVER")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver InParam = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver();
                            InParam.Type = Convert.IsDBNull(XRow["Item_Profile"]) ? null : XRow["Item_Profile"].ToString();
                            InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InParam.DescMiscID = Convert.IsDBNull(XRow["GS_DESC_MISC_ID"]) ? null : XRow["GS_DESC_MISC_ID"].ToString();
                            InParam.Weight = Convert.IsDBNull(XRow["GS_ITEM_WEIGHT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["GS_ITEM_WEIGHT"]);
                            InParam.LocationID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InParam.OtherDetails = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InParam.TxnID = model.xMID;
                            InParam.TxnSrno = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InParam.Amount = Convert.ToDouble(XRow["Amount"]);
                            InParam.Status_Action = Status_Action;
                            InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;

                            InsertGS[cnt] = InParam;
                        }
                        if (XRow["Item_Profile"].ToString() == "OTHER ASSETS")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets InParam = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets();

                            InParam.AssetType = Convert.IsDBNull(XRow["AI_TYPE"]) ? null : XRow["AI_TYPE"].ToString();
                            InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InParam.Make = Convert.IsDBNull(XRow["AI_MAKE"]) ? null : XRow["AI_MAKE"].ToString();
                            InParam.Model = Convert.IsDBNull(XRow["AI_MODEL"]) ? null : XRow["AI_MODEL"].ToString();
                            InParam.SrNo = Convert.IsDBNull(XRow["AI_SERIAL_NO"]) ? "" : XRow["AI_SERIAL_NO"].ToString();
                            InParam.Rate = Convert.IsDBNull(XRow["Rate"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Rate"]);
                            InParam.InsAmount = Convert.IsDBNull(XRow["AI_INS_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["AI_INS_AMT"]);
                            if (IsDate(Convert.IsDBNull(XRow["AI_PUR_DATE"]) ? null : XRow["AI_PUR_DATE"].ToString()))
                            {
                                InParam.PurchaseDate = Convert.ToDateTime(Convert.IsDBNull(XRow["AI_PUR_DATE"]) ? null : XRow["AI_PUR_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PurchaseDate = Convert.IsDBNull(XRow["AI_PUR_DATE"]) ? null : XRow["AI_PUR_DATE"].ToString();
                            }
                            InParam.PurchaseAmount = InParam.AssetType.ToUpper() == "ASSET" ? Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]) : 0; //'/*http://pm.bkinfo.in/issues/5345#note-12*/                        
                            InParam.Warranty = Convert.IsDBNull(XRow["AI_WARRANTY"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["AI_WARRANTY"]);
                            InParam.Image = Convert.IsDBNull(XRow["AI_IMAGE"]) ? null : (byte[])XRow["AI_IMAGE"];
                            InParam.Quantity = Convert.IsDBNull(XRow["Qty."]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Qty."]);
                            InParam.LocationId = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InParam.OtherDetails = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InParam.TxnID = model.xMID;
                            InParam.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InParam.Status_Action = Status_Action;
                            InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;

                            InsertAssets[cnt] = InParam;
                        }
                        if (XRow["Item_Profile"].ToString() == "LIVESTOCK")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock InParam = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock();
                            InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InParam.Name = Convert.IsDBNull(XRow["LS_NAME"]) ? null : XRow["LS_NAME"].ToString();
                            InParam.Year = Convert.IsDBNull(XRow["LS_BIRTH_YEAR"]) ? null : XRow["LS_BIRTH_YEAR"].ToString();
                            InParam.Amount = Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]);
                            InParam.Insurance = Convert.IsDBNull(XRow["LS_INSURANCE"]) ? null : XRow["LS_INSURANCE"].ToString();
                            InParam.InsuranceID = Convert.IsDBNull(XRow["LS_INSURANCE_ID"]) ? null : XRow["LS_INSURANCE_ID"].ToString();
                            InParam.PolicyNo = Convert.IsDBNull(XRow["LS_INS_POLICY_NO"]) ? null : XRow["LS_INS_POLICY_NO"].ToString();
                            InParam.InsAmount = Convert.IsDBNull(XRow["LS_INS_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LS_INS_AMT"]);
                            if (IsDate(Convert.IsDBNull(XRow["LS_INS_DATE"]) ? null : XRow["LS_INS_DATE"].ToString()))
                            {
                                InParam.InsuranceDate = Convert.ToDateTime(Convert.IsDBNull(XRow["LS_INS_DATE"]) ? null : XRow["LS_INS_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.InsuranceDate = Convert.IsDBNull(XRow["LS_INS_DATE"]) ? null : XRow["LS_INS_DATE"].ToString();
                            }
                            InParam.LocationID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InParam.OtherDetails = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InParam.TxnID = model.xMID;
                            InParam.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InParam.Status_Action = Status_Action;
                            InParam.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;
                            InsertLivestock[cnt] = InParam;
                        }
                        if (XRow["Item_Profile"].ToString() == "VEHICLES")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles InPms = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles();
                            InPms.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InPms.Make = Convert.IsDBNull(XRow["VI_MAKE"]) ? null : XRow["VI_MAKE"].ToString();
                            InPms.Model = Convert.IsDBNull(XRow["VI_MODEL"]) ? null : XRow["VI_MODEL"].ToString();
                            InPms.Reg_No_Pattern = Convert.IsDBNull(XRow["VI_REG_NO_PATTERN"]) ? null : XRow["VI_REG_NO_PATTERN"].ToString();
                            InPms.Reg_No = Convert.IsDBNull(XRow["VI_REG_NO"]) ? null : XRow["VI_REG_NO"].ToString();
                            if (IsDate(Convert.IsDBNull(XRow["VI_REG_DATE"]) ? null : XRow["VI_REG_DATE"].ToString()))
                            {
                                InPms.RegDate = Convert.ToDateTime(Convert.IsDBNull(XRow["VI_REG_DATE"]) ? null : XRow["VI_REG_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPms.RegDate = Convert.IsDBNull(XRow["VI_REG_DATE"]) ? null : XRow["VI_REG_DATE"].ToString();
                            }
                            InPms.Ownership = Convert.IsDBNull(XRow["VI_OWNERSHIP"]) ? null : XRow["VI_OWNERSHIP"].ToString();
                            if (XRow["VI_OWNERSHIP_AB_ID"] != null && Convert.IsDBNull(XRow["VI_OWNERSHIP_AB_ID"]) == false)
                            {
                                InPms.Ownership_AB_ID = XRow["VI_OWNERSHIP_AB_ID"].ToString().Length == 0 ? null : XRow["VI_OWNERSHIP_AB_ID"].ToString();
                            }
                            InPms.Doc_RC_Book = Convert.IsDBNull(XRow["VI_DOC_RC_BOOK"]) ? null : XRow["VI_DOC_RC_BOOK"].ToString();
                            InPms.Doc_Affidavit = Convert.IsDBNull(XRow["VI_DOC_AFFIDAVIT"]) ? null : XRow["VI_DOC_AFFIDAVIT"].ToString();
                            InPms.Doc_Will = Convert.IsDBNull(XRow["VI_DOC_WILL"]) ? null : XRow["VI_DOC_WILL"].ToString();
                            InPms.Doc_TRF_Letter = Convert.IsDBNull(XRow["VI_DOC_TRF_LETTER"]) ? null : XRow["VI_DOC_TRF_LETTER"].ToString();
                            InPms.DOC_FU_Letter = Convert.IsDBNull(XRow["VI_DOC_FU_LETTER"]) ? null : XRow["VI_DOC_FU_LETTER"].ToString();
                            InPms.Doc_Is_Others = Convert.IsDBNull(XRow["VI_DOC_OTHERS"]) ? null : XRow["VI_DOC_OTHERS"].ToString();
                            InPms.Doc_Others_Name = Convert.IsDBNull(XRow["VI_DOC_NAME"]) ? null : XRow["VI_DOC_NAME"].ToString();
                            if (Convert.IsDBNull(XRow["VI_INSURANCE_ID"]))
                            {
                                InPms.Insurance_ID = null;
                            }
                            else if (XRow["VI_INSURANCE_ID"] == null)
                            {
                                InPms.Insurance_ID = null;
                            }
                            else if (XRow["VI_INSURANCE_ID"].ToString().Length == 0)
                            {
                                InPms.Insurance_ID = null;
                            }
                            else
                            {
                                InPms.Insurance_ID = XRow["VI_INSURANCE_ID"].ToString();
                            }
                            InPms.Ins_Policy_No = Convert.IsDBNull(XRow["VI_INS_POLICY_NO"]) ? null : XRow["VI_INS_POLICY_NO"].ToString();

                            if (IsDate(Convert.IsDBNull(XRow["VI_INS_EXPIRY_DATE"]) ? null : XRow["VI_INS_EXPIRY_DATE"].ToString()))
                            {
                                InPms.Ins_Expiry_Date = Convert.ToDateTime(Convert.IsDBNull(XRow["VI_INS_EXPIRY_DATE"]) ? null : XRow["VI_INS_EXPIRY_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPms.Ins_Expiry_Date = Convert.IsDBNull(XRow["VI_INS_EXPIRY_DATE"]) ? null : XRow["VI_INS_EXPIRY_DATE"].ToString();
                            }
                            //'InPms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), null, XRow("VI_INS_EXPIRY_DATE"))

                            InPms.Location_ID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InPms.Other_Details = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InPms.TxnID = model.xMID;
                            InPms.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InPms.Amount = Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]);
                            InPms.Status_Action = Status_Action;
                            InPms.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;

                            InsertVehicles[cnt] = InPms;
                        }
                        if (XRow["Item_Profile"].ToString() == "LAND & BUILDING")
                        {
                            string PartyID = "NULL";
                            if (XRow["LB_OWNERSHIP_PARTY_ID"].ToString() != "NULL")
                            {
                                PartyID = "'" + XRow["LB_OWNERSHIP_PARTY_ID"] + "'";
                            }
                            Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding InParam = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding();
                            InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InParam.PropertyType = Convert.IsDBNull(XRow["LB_PRO_TYPE"]) ? null : XRow["LB_PRO_TYPE"].ToString();
                            InParam.Category = Convert.IsDBNull(XRow["LB_PRO_CATEGORY"]) ? null : XRow["LB_PRO_CATEGORY"].ToString();
                            InParam.Use = Convert.IsDBNull(XRow["LB_PRO_USE"]) ? null : XRow["LB_PRO_USE"].ToString();
                            InParam.Name = Convert.IsDBNull(XRow["LB_PRO_NAME"]) ? null : XRow["LB_PRO_NAME"].ToString();
                            InParam.Address = Convert.IsDBNull(XRow["LB_PRO_ADDRESS"]) ? null : XRow["LB_PRO_ADDRESS"].ToString();
                            InParam.LB_Add1 = Convert.IsDBNull(XRow["LB_ADDRESS1"]) ? null : XRow["LB_ADDRESS1"].ToString();
                            InParam.LB_Add2 = Convert.IsDBNull(XRow["LB_ADDRESS2"]) ? null : XRow["LB_ADDRESS2"].ToString();
                            InParam.LB_Add3 = Convert.IsDBNull(XRow["LB_ADDRESS3"]) ? null : XRow["LB_ADDRESS3"].ToString();
                            InParam.LB_Add4 = Convert.IsDBNull(XRow["LB_ADDRESS4"]) ? null : XRow["LB_ADDRESS4"].ToString();
                            InParam.LB_CityID = Convert.IsDBNull(XRow["LB_CITY_ID"]) ? null : XRow["LB_CITY_ID"].ToString();
                            InParam.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e";
                            InParam.LB_DisttID = Convert.IsDBNull(XRow["LB_DISTRICT_ID"]) ? null : XRow["LB_DISTRICT_ID"].ToString();
                            InParam.LB_PinCode = Convert.IsDBNull(XRow["LB_PINCODE"]) ? null : XRow["LB_PINCODE"].ToString();
                            InParam.LB_StateID = Convert.IsDBNull(XRow["LB_STATE_ID"]) ? null : XRow["LB_STATE_ID"].ToString();
                            InParam.Ownership = Convert.IsDBNull(XRow["LB_OWNERSHIP"]) ? null : XRow["LB_OWNERSHIP"].ToString();
                            InParam.Owner_Party_ID = PartyID;
                            InParam.SurveyNo = Convert.IsDBNull(XRow["LB_SURVEY_NO"]) ? null : XRow["LB_SURVEY_NO"].ToString();
                            InParam.TotalArea = Convert.IsDBNull(XRow["LB_TOT_P_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_TOT_P_AREA"]);
                            InParam.ConstructedArea = Convert.IsDBNull(XRow["LB_CON_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_CON_AREA"]);
                            InParam.ConstructionYear = Convert.IsDBNull(XRow["LB_CON_YEAR"]) ? null : XRow["LB_CON_YEAR"].ToString();
                            InParam.RCCRoof = Convert.IsDBNull(XRow["LB_RCC_ROOF"]) ? null : XRow["LB_RCC_ROOF"].ToString();
                            InParam.DepositAmount = Convert.IsDBNull(XRow["LB_DEPOSIT_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_DEPOSIT_AMT"]);
                            if (IsDate(Convert.IsDBNull(XRow["LB_PAID_DATE"]) ? null : XRow["LB_PAID_DATE"].ToString()))
                            {
                                InParam.PaymentDate = Convert.ToDateTime(Convert.IsDBNull(XRow["LB_PAID_DATE"]) ? null : XRow["LB_PAID_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PaymentDate = Convert.IsDBNull(XRow["LB_PAID_DATE"]) ? null : XRow["LB_PAID_DATE"].ToString();
                            }
                            //'InParam.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), null, XRow("LB_PAID_DATE"))

                            InParam.MonthlyRent = Convert.IsDBNull(XRow["LB_MONTH_RENT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_MONTH_RENT"]);

                            InParam.MonthlyOtherExpenses = Convert.IsDBNull(XRow["LB_MONTH_O_PAYMENTS"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_MONTH_O_PAYMENTS"]);
                            if (IsDate(Convert.IsDBNull(XRow["LB_PERIOD_FROM"]) ? null : XRow["LB_PERIOD_FROM"].ToString()))
                            {
                                InParam.PeriodFrom = Convert.ToDateTime(Convert.IsDBNull(XRow["LB_PERIOD_FROM"]) ? null : XRow["LB_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PeriodFrom = Convert.IsDBNull(XRow["LB_PERIOD_FROM"]) ? null : XRow["LB_PERIOD_FROM"].ToString();
                            }
                            //'InParam.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), null, XRow("LB_PERIOD_FROM"))
                            if (IsDate(Convert.IsDBNull(XRow["LB_PERIOD_TO"]) ? null : XRow["LB_PERIOD_TO"].ToString()))
                            {
                                InParam.PeriodTo = Convert.ToDateTime(Convert.IsDBNull(XRow["LB_PERIOD_TO"]) ? null : XRow["LB_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PeriodTo = Convert.IsDBNull(XRow["LB_PERIOD_TO"]) ? null : XRow["LB_PERIOD_TO"].ToString();
                            }
                            //'InParam.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), null, XRow("LB_PERIOD_TO"))                        
                            InParam.OtherDocs = Convert.IsDBNull(XRow["LB_DOC_OTHERS"]) ? null : XRow["LB_DOC_OTHERS"].ToString();
                            InParam.DocNames = Convert.IsDBNull(XRow["LB_DOC_NAME"]) ? null : XRow["LB_DOC_NAME"].ToString();
                            InParam.Value = Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]);
                            InParam.OtherDetails = Convert.IsDBNull(XRow["LB_OTHER_DETAIL"]) ? null : XRow["LB_OTHER_DETAIL"].ToString();
                            InParam.MasterID = model.xMID;
                            InParam.SrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InParam.Status_Action = Status_Action;
                            InParam.RecID = Convert.IsDBNull(XRow["LB_REC_ID"]) ? null : XRow["LB_REC_ID"].ToString();

                            //'EXTENSIONS 
                            Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[] ExtInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[LB_EXTENDED_PROPERTY_TABLE.Rows.Count];
                            int cnt1 = 0;
                            if (!(LB_EXTENDED_PROPERTY_TABLE == null))
                            {
                                foreach (DataRow _Ext_Row in LB_EXTENDED_PROPERTY_TABLE.Rows)
                                {
                                    if (_Ext_Row["LB_REC_ID"].ToString() == XRow["LB_REC_ID"].ToString())
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
                                            InEInfo.MOU_Date = Convert.ToDateTime(Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"]).ToString(BASE._Server_Date_Format_Short);
                                        }
                                        else
                                        {
                                            InEInfo.MOU_Date = Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"].ToString();
                                        }
                                        //'InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), null, _Ext_Row("LB_MOU_DATE"))
                                        InEInfo.Value = Convert.ToDouble(_Ext_Row["LB_VALUE"]);
                                        InEInfo.OtherDetails = Convert.IsDBNull(_Ext_Row["LB_OTHER_DETAIL"]) ? null : _Ext_Row["LB_OTHER_DETAIL"].ToString();
                                        InEInfo.Status_Action = Status_Action;
                                        InEInfo.RecID = System.Guid.NewGuid().ToString();

                                        ExtInfo[cnt1] = InEInfo;
                                        cnt1 += 1;
                                    }
                                }
                            }
                            InParam.InsertExtInfo = ExtInfo;
                            //'DOCS
                            Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[] DocInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[LB_DOCS_ARRAY.Rows.Count];
                            cnt1 = 0;
                            if (!(LB_DOCS_ARRAY == null))
                            {
                                foreach (DataRow _Ext_Row in LB_DOCS_ARRAY.Rows)
                                {
                                    if (_Ext_Row["LB_REC_ID"].ToString() == XRow["LB_REC_ID"].ToString())
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
                            InParam.InsertDocInfo = DocInfo;

                            //'Add Location
                            Common_Lib.RealTimeService.Param_AssetLoc_Insert InAssetLoc = new Common_Lib.RealTimeService.Param_AssetLoc_Insert();
                            InAssetLoc.name = InParam.Name.Trim();
                            InAssetLoc.OtherDetails = "Use Type: " + InParam.PropertyType;
                            InAssetLoc.Status_Action = Status_Action;
                            InAssetLoc.Match_LB_ID = InParam.RecID;
                            InAssetLoc.Match_SP_ID = "";

                            InParam.param_InsertAssetLoc = InAssetLoc;
                            InsertProperty[cnt] = InParam;
                        }

                        //'----------WIP References------------

                        if (XRow["Item_Profile"].ToString().Equals("WIP"))
                        {
                            if (!(XRow["WIP_REF_TYPE"] == null))
                            {
                                if (XRow["WIP_REF_TYPE"].ToString() == "NEW")
                                {
                                    Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile InReference = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile();
                                    InReference.LedID = Convert.IsDBNull(XRow["Item_Led_ID"]) ? null : XRow["Item_Led_ID"].ToString();
                                    InReference.Reference = Convert.IsDBNull(XRow["REFERENCE"]) ? null : XRow["REFERENCE"].ToString();
                                    InReference.Amount = Convert.ToDecimal(XRow["Amount"]);
                                    InReference.Status_Action = Status_Action;
                                    InReference.TxnID = model.xMID;
                                    InReference.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                                    InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment;
                                    InsertReferencesWIP[cnt] = InReference;
                                }
                            }
                        }

                        cnt += 1;
                    }
                    InNewParam.InsertAssets = InsertAssets;
                    InNewParam.InsertGS = InsertGS;
                    InNewParam.InsertItem = InsertItem;
                    InNewParam.InsertLivestock = InsertLivestock;
                    InNewParam.InsertProperty = InsertProperty;
                    InNewParam.InsertVehicles = InsertVehicles;
                    InNewParam.InsertPurpose = InsertPurpose;
                    InNewParam.InsertReferencesWIP = InsertReferencesWIP;

                    //FCRA Insert Process
                    if (model.DNK_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.DNK_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._Gift_DBOps.InsertGift_Txn(InNewParam))
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
                    return Json(new { jsonParam, CashbookGridPK = model.xMID + model.xID }, JsonRequestBehavior.AllowGet);
                }

                string Message = "";
                bool IsLBIncluded = false;
                Common_Lib.RealTimeService.Param_Txn_Update_VoucherGift EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_VoucherGift();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit) //'edit
                {
                    //'Dim xDate As DateTime = null : xDate = New Date(Me.Txt_V_Date.DateTime.Year, Me.Txt_V_Date.DateTime.Month, Me.Txt_V_Date.DateTime.Day)
                    int xCnt = 1;
                    Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherGift UpMaster = new Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherGift();
                    UpMaster.VNo = model.Txt_V_NO_DNK ?? "";
                    if (IsDate(model.Txt_V_Date_DNK.ToString()))
                    {
                        UpMaster.TDate = Convert.ToDateTime(model.Txt_V_Date_DNK).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMaster.TDate = model.Txt_V_Date_DNK.ToString();
                    }
                    //'UpMaster.TDate = Me.Txt_V_Date.Text
                    UpMaster.PartyID = model.GLookUp_PartyList1_DNK;
                    UpMaster.SubTotal = Convert.ToDouble(model.Txt_SubTotal_DNK);
                    UpMaster.Cash = 0;
                    UpMaster.Bank = 0;
                    UpMaster.Advance = 0;
                    UpMaster.Liability = 0;
                    UpMaster.Credit = 0;
                    UpMaster.TDS = 0;
                    //'UpMaster.Status_Action = Status_Action
                    UpMaster.RecID = model.xMID;

                    EditParam.param_UpdateMaster = UpMaster;

                    EditParam.MID_Delete = model.xMID;

                    EditParam.MID_ReferenceDelete = model.xMID;

                    //'-------------

                    Common_Lib.RealTimeService.Parameter_Insert_VoucherGift[] Insert = new Common_Lib.RealTimeService.Parameter_Insert_VoucherGift[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift[] InsertItem = new Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift[] InsertPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver[] InsertGS = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets[] InsertAssets = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock[] InsertLivestock = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles[] InsertVehicles = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding[] InsertProperty = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding[DT_DNK.Rows.Count + 1];
                    Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile[] InsertReferencesWIP = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile[DT_DNK.Rows.Count + 1];

                    foreach (DataRow XRow in DT_DNK.Rows)
                    {
                        string Cross_Ref_ID = "";
                        if (XRow["LB_REC_ID"].ToString().Length > 0 && (!(XRow["Item_Profile"].ToString() == "LAND & BUILDING")))  //'Bug #5637
                        {
                            Cross_Ref_ID = XRow["LB_REC_ID"].ToString();
                            if (BASE.AllowMultiuser())
                            {
                                if (!(Cross_Ref_ID == null))
                                {
                                    if (Cross_Ref_ID.Length > 0)
                                    {
                                        DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(Cross_Ref_ID); //'checks if the referred property for constt items has been sold 
                                        if (!(SaleRecord == null))
                                        {
                                            if (SaleRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was sold on " + Convert.ToDateTime(SaleRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + SaleRecord.Rows[0]["TR_AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.closeform = true;
                                                jsonParam.refreshgrid = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Convert.ToInt32(null), Cross_Ref_ID); //'checks if the referred property for constt items has been sold 
                                        if (AssetTrfRecord != null)
                                        {
                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                jsonParam.message = "Sorry ! Selected Entry refers a asset which was Transfered on " + Convert.ToDateTime(AssetTrfRecord.Rows[0]["TR_DATE"]).ToLongDateString() + " for initial payment of Rs." + AssetTrfRecord.Rows[0]["AMOUNT"].ToString() + "." + "<br>" + "<br>" + " Please delete the record for editing this Entry.";
                                                jsonParam.result = false;
                                                jsonParam.title = "Error!!";
                                                jsonParam.closeform = true;
                                                jsonParam.refreshgrid = true;
                                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (XRow["REF_REC_ID"].ToString().Length > 0)
                        {
                            Cross_Ref_ID = "'" + XRow["REF_REC_ID"] + "'";
                        }
                        model.xID = System.Guid.NewGuid().ToString();
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherGift InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherGift();
                        InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Donation_Gift;
                        InParam.VNo = model.Txt_V_NO_DNK ?? "";
                        if (IsDate(model.Txt_V_Date_DNK.ToString()))
                        {
                            InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_DNK).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.TDate = model.Txt_V_Date_DNK.ToString();
                        }
                        //'InParam.TDate = Me.Txt_V_Date.Text
                        InParam.ItemID = XRow["Item_ID"].ToString();
                        InParam.Type = "DEBIT";
                        InParam.Cr_Led_ID = "";
                        InParam.Dr_Led_ID = XRow["Item_Led_ID"].ToString();
                        InParam.Sub_Cr_Led_ID = "";
                        InParam.Mode = "GIFT";
                        InParam.Ref_No = "";
                        InParam.Amount = Convert.ToDouble(XRow["Amount"]);
                        InParam.PartyID = model.GLookUp_PartyList1_DNK;
                        InParam.Narration = model.Txt_Narration_DNK ?? "";
                        InParam.Remarks = XRow["Remarks"].ToString() ?? "";
                        InParam.Reference = model.Txt_Reference_DNK ?? "";
                        InParam.Tr_M_ID = model.xMID;
                        InParam.TxnSrNo = Convert.ToInt32(XRow["Sr."]);
                        InParam.Status_Action = Status_Action;
                        InParam.RecID = model.xID;
                        InParam.Cross_Ref_Id = Cross_Ref_ID;

                        Insert[xCnt] = InParam;
                        xCnt += 1;
                    }
                    EditParam.Insert = Insert;



                    //'JV Entry
                    model.xID = System.Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Parameter_Insert_VoucherGift InParams = new Common_Lib.RealTimeService.Parameter_Insert_VoucherGift();
                    InParams.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Donation_Gift;
                    InParams.VNo = model.Txt_V_NO_DNK ?? "";
                    if (IsDate(model.Txt_V_Date_DNK.ToString()))
                    {
                        InParams.TDate = Convert.ToDateTime(model.Txt_V_Date_DNK).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParams.TDate = model.Txt_V_Date_DNK.ToString();
                    }
                    //'InParams.TDate = Me.Txt_V_Date.Text
                    InParams.ItemID = JV_Item_ID;
                    InParams.Type = "CREDIT";
                    InParams.Cr_Led_ID = JV_Cr_Led_id;
                    InParams.Dr_Led_ID = "";
                    InParams.Sub_Cr_Led_ID = "";
                    InParams.Mode = "GIFT";
                    InParams.Ref_No = "";
                    InParams.Amount = Convert.ToDouble(model.Txt_SubTotal_DNK);
                    InParams.PartyID = model.GLookUp_PartyList1_DNK;
                    InParams.Narration = model.Txt_Narration_DNK ?? "";
                    InParams.Remarks = "";
                    InParams.Reference = model.Txt_Reference_DNK ?? "";
                    InParams.Tr_M_ID = model.xMID;
                    InParams.TxnSrNo = xCnt;
                    InParams.Status_Action = Status_Action;
                    InParams.RecID = model.xID;
                    InParams.Cross_Ref_Id = "";

                    EditParam.InsertJV = InParams;

                    EditParam.MID_DeleteItems = model.xMID;

                    EditParam.MID_DeletePurpose = model.xMID;
                    //'PROFILE ENTRIES DELETE
                    string SQL_DEL = "";

                    EditParam.MID_DeleteGS = model.xMID;

                    EditParam.MID_DeleteAssets = model.xMID;

                    EditParam.MID_DeleteLS = model.xMID;

                    EditParam.MID_DeleteVehicle = model.xMID;

                    DataTable d1 = BASE._L_B_DBOps.GetIDsBytxnID(model.xMID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift);

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

                    //'Main Items
                    int cnt = 0;
                    foreach (DataRow XRow in DT_DNK.Rows)
                    {
                        Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift InItem = new Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift();
                        InItem.Txn_M_ID = model.xMID;
                        InItem.TxnSrNo = Convert.ToInt32(XRow["Sr."]);
                        InItem.ItemID = XRow["Item_ID"].ToString();
                        InItem.LedID = XRow["Item_Led_ID"].ToString();
                        InItem.Type = XRow["Item_Trans_Type"].ToString();
                        InItem.PartyReq = XRow["Item_Party_Req"].ToString();
                        InItem.Profile = XRow["Item_Profile"].ToString();
                        InItem.ItemName = XRow["Item Name"].ToString();
                        InItem.Head = XRow["Head"].ToString();
                        InItem.Qty = Convert.ToDouble(XRow["Qty."]);
                        InItem.Unit = XRow["Unit"].ToString();
                        InItem.Rate = Convert.ToDouble(XRow["Rate"]);
                        InItem.Amount = Convert.ToDouble(XRow["Amount"]);
                        InItem.Remarks = XRow["Remarks"].ToString() ?? "";
                        InItem.Status_Action = Status_Action;
                        InItem.RecID = System.Guid.NewGuid().ToString();

                        InsertItem[cnt] = InItem;

                        //'Purpose.........
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift InPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift();
                        InPurpose.TxnID = model.xMID;
                        InPurpose.PurposeID = XRow["Pur_ID"].ToString();
                        InPurpose.Amount = Convert.ToDouble(XRow["Amount"]);
                        InPurpose.ItemSrNo = Convert.ToInt32(XRow["Sr."]);
                        InPurpose.Status_Action = Status_Action;
                        InPurpose.RecID = System.Guid.NewGuid().ToString();

                        InsertPurpose[cnt] = InPurpose;

                        if (XRow["Item_Profile"].ToString() == "GOLD" || XRow["Item_Profile"].ToString() == "SILVER")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver InParam = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver();
                            InParam.Type = Convert.IsDBNull(XRow["Item_Profile"]) ? null : XRow["Item_Profile"].ToString();
                            InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InParam.DescMiscID = Convert.IsDBNull(XRow["GS_DESC_MISC_ID"]) ? null : XRow["GS_DESC_MISC_ID"].ToString();
                            InParam.Weight = Convert.IsDBNull(XRow["GS_ITEM_WEIGHT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["GS_ITEM_WEIGHT"]);
                            InParam.LocationID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InParam.OtherDetails = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InParam.TxnID = model.xMID;
                            InParam.TxnSrno = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InParam.Amount = Convert.ToDouble(XRow["Amount"]);
                            InParam.Status_Action = Status_Action;
                            InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;

                            InsertGS[cnt] = InParam;
                        }
                        if (XRow["Item_Profile"].ToString() == "OTHER ASSETS")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets InParam = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets();

                            InParam.AssetType = Convert.IsDBNull(XRow["AI_TYPE"]) ? null : XRow["AI_TYPE"].ToString();
                            InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InParam.Make = Convert.IsDBNull(XRow["AI_MAKE"]) ? null : XRow["AI_MAKE"].ToString();
                            InParam.Model = Convert.IsDBNull(XRow["AI_MODEL"]) ? null : XRow["AI_MODEL"].ToString();
                            InParam.SrNo = Convert.IsDBNull(XRow["AI_SERIAL_NO"]) ? "" : XRow["AI_SERIAL_NO"].ToString();
                            InParam.Rate = Convert.IsDBNull(XRow["Rate"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Rate"]);
                            InParam.InsAmount = Convert.IsDBNull(XRow["AI_INS_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["AI_INS_AMT"]);
                            if (IsDate(Convert.IsDBNull(XRow["AI_PUR_DATE"]) ? null : XRow["AI_PUR_DATE"].ToString()))
                            {
                                InParam.PurchaseDate = Convert.ToDateTime(Convert.IsDBNull(XRow["AI_PUR_DATE"]) ? null : XRow["AI_PUR_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PurchaseDate = Convert.IsDBNull(XRow["AI_PUR_DATE"]) ? null : XRow["AI_PUR_DATE"].ToString();
                            }
                            InParam.PurchaseAmount = InParam.AssetType.ToUpper() == "ASSET" ? Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]) : 0; //'/*http://pm.bkinfo.in/issues/5345#note-12*/                        
                            InParam.Warranty = Convert.IsDBNull(XRow["AI_WARRANTY"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["AI_WARRANTY"]);
                            InParam.Image = Convert.IsDBNull(XRow["AI_IMAGE"]) ? null : (byte[])XRow["AI_IMAGE"];
                            InParam.Quantity = Convert.IsDBNull(XRow["Qty."]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Qty."]);
                            InParam.LocationId = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InParam.OtherDetails = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InParam.TxnID = model.xMID;
                            InParam.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InParam.Status_Action = Status_Action;
                            InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;

                            InsertAssets[cnt] = InParam;
                        }
                        if (XRow["Item_Profile"].ToString() == "LIVESTOCK")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock InPms = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock();
                            InPms.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InPms.Name = Convert.IsDBNull(XRow["LS_NAME"]) ? null : XRow["LS_NAME"].ToString();
                            InPms.Year = Convert.IsDBNull(XRow["LS_BIRTH_YEAR"]) ? null : XRow["LS_BIRTH_YEAR"].ToString();
                            InPms.Amount = Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]);
                            InPms.Insurance = Convert.IsDBNull(XRow["LS_INSURANCE"]) ? null : XRow["LS_INSURANCE"].ToString();
                            InPms.InsuranceID = Convert.IsDBNull(XRow["LS_INSURANCE_ID"]) ? null : XRow["LS_INSURANCE_ID"].ToString();
                            InPms.PolicyNo = Convert.IsDBNull(XRow["LS_INS_POLICY_NO"]) ? null : XRow["LS_INS_POLICY_NO"].ToString();
                            InPms.InsAmount = Convert.IsDBNull(XRow["LS_INS_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LS_INS_AMT"]);
                            if (IsDate(Convert.IsDBNull(XRow["LS_INS_DATE"]) ? null : XRow["LS_INS_DATE"].ToString()))
                            {
                                InPms.InsuranceDate = Convert.ToDateTime(Convert.IsDBNull(XRow["LS_INS_DATE"]) ? null : XRow["LS_INS_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPms.InsuranceDate = Convert.IsDBNull(XRow["LS_INS_DATE"]) ? null : XRow["LS_INS_DATE"].ToString();
                            }
                            InPms.LocationID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InPms.OtherDetails = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InPms.TxnID = model.xMID;
                            InPms.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InPms.Status_Action = Status_Action;
                            InPms.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;
                            InsertLivestock[cnt] = InPms;
                        }
                        if (XRow["Item_Profile"].ToString() == "VEHICLES")
                        {
                            Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles InPrms = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles();
                            InPrms.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InPrms.Make = Convert.IsDBNull(XRow["VI_MAKE"]) ? null : XRow["VI_MAKE"].ToString();
                            InPrms.Model = Convert.IsDBNull(XRow["VI_MODEL"]) ? null : XRow["VI_MODEL"].ToString();
                            InPrms.Reg_No_Pattern = Convert.IsDBNull(XRow["VI_REG_NO_PATTERN"]) ? null : XRow["VI_REG_NO_PATTERN"].ToString();
                            InPrms.Reg_No = Convert.IsDBNull(XRow["VI_REG_NO"]) ? null : XRow["VI_REG_NO"].ToString();
                            if (IsDate(Convert.IsDBNull(XRow["VI_REG_DATE"]) ? null : XRow["VI_REG_DATE"].ToString()))
                            {
                                InPrms.RegDate = Convert.ToDateTime(Convert.IsDBNull(XRow["VI_REG_DATE"]) ? null : XRow["VI_REG_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPrms.RegDate = Convert.IsDBNull(XRow["VI_REG_DATE"]) ? null : XRow["VI_REG_DATE"].ToString();
                            }
                            InPrms.Ownership = Convert.IsDBNull(XRow["VI_OWNERSHIP"]) ? null : XRow["VI_OWNERSHIP"].ToString();
                            if (XRow["VI_OWNERSHIP_AB_ID"] != null && Convert.IsDBNull(XRow["VI_OWNERSHIP_AB_ID"]) == false)
                            {
                                InPrms.Ownership_AB_ID = XRow["VI_OWNERSHIP_AB_ID"].ToString().Length == 0 ? null : XRow["VI_OWNERSHIP_AB_ID"].ToString();
                            }
                            InPrms.Doc_RC_Book = Convert.IsDBNull(XRow["VI_DOC_RC_BOOK"]) ? null : XRow["VI_DOC_RC_BOOK"].ToString();
                            InPrms.Doc_Affidavit = Convert.IsDBNull(XRow["VI_DOC_AFFIDAVIT"]) ? null : XRow["VI_DOC_AFFIDAVIT"].ToString();
                            InPrms.Doc_Will = Convert.IsDBNull(XRow["VI_DOC_WILL"]) ? null : XRow["VI_DOC_WILL"].ToString();
                            InPrms.Doc_TRF_Letter = Convert.IsDBNull(XRow["VI_DOC_TRF_LETTER"]) ? null : XRow["VI_DOC_TRF_LETTER"].ToString();
                            InPrms.DOC_FU_Letter = Convert.IsDBNull(XRow["VI_DOC_FU_LETTER"]) ? null : XRow["VI_DOC_FU_LETTER"].ToString();
                            InPrms.Doc_Is_Others = Convert.IsDBNull(XRow["VI_DOC_OTHERS"]) ? null : XRow["VI_DOC_OTHERS"].ToString();
                            InPrms.Doc_Others_Name = Convert.IsDBNull(XRow["VI_DOC_NAME"]) ? null : XRow["VI_DOC_NAME"].ToString();
                            if (Convert.IsDBNull(XRow["VI_INSURANCE_ID"]))
                            {
                                InPrms.Insurance_ID = null;
                            }
                            else if (XRow["VI_INSURANCE_ID"] == null)
                            {
                                InPrms.Insurance_ID = null;
                            }
                            else if (XRow["VI_INSURANCE_ID"].ToString().Length == 0)
                            {
                                InPrms.Insurance_ID = null;
                            }
                            else
                            {
                                InPrms.Insurance_ID = XRow["VI_INSURANCE_ID"].ToString();
                            }
                            InPrms.Ins_Policy_No = Convert.IsDBNull(XRow["VI_INS_POLICY_NO"]) ? null : XRow["VI_INS_POLICY_NO"].ToString();

                            if (IsDate(Convert.IsDBNull(XRow["VI_INS_EXPIRY_DATE"]) ? null : XRow["VI_INS_EXPIRY_DATE"].ToString()))
                            {
                                InPrms.Ins_Expiry_Date = Convert.ToDateTime(Convert.IsDBNull(XRow["VI_INS_EXPIRY_DATE"]) ? null : XRow["VI_INS_EXPIRY_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InPrms.Ins_Expiry_Date = Convert.IsDBNull(XRow["VI_INS_EXPIRY_DATE"]) ? null : XRow["VI_INS_EXPIRY_DATE"].ToString();
                            }
                            //'InPms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), null, XRow("VI_INS_EXPIRY_DATE"))

                            InPrms.Location_ID = Convert.IsDBNull(XRow["LOC_ID"]) ? null : XRow["LOC_ID"].ToString();
                            InPrms.Other_Details = Convert.IsDBNull(XRow["Remarks"]) ? null : XRow["Remarks"].ToString();
                            InPrms.TxnID = model.xMID;
                            InPrms.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InPrms.Amount = Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]);
                            InPrms.Status_Action = Status_Action;
                            InPrms.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift;

                            InsertVehicles[cnt] = InPrms;
                        }
                        if (XRow["Item_Profile"].ToString() == "LAND & BUILDING")
                        {
                            string PartyID = "NULL";
                            if (XRow["LB_OWNERSHIP_PARTY_ID"].ToString() != "NULL")
                            {
                                PartyID = "'" + XRow["LB_OWNERSHIP_PARTY_ID"] + "'";
                            }
                            if (XRow["LB_OWNERSHIP_PARTY_ID"].ToString().Length == 0)
                            {
                                PartyID = "NULL";
                            }
                            Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding InParam = new Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding();
                            InParam.ItemID = Convert.IsDBNull(XRow["Item_ID"]) ? null : XRow["Item_ID"].ToString();
                            InParam.PropertyType = Convert.IsDBNull(XRow["LB_PRO_TYPE"]) ? null : XRow["LB_PRO_TYPE"].ToString();
                            InParam.Category = Convert.IsDBNull(XRow["LB_PRO_CATEGORY"]) ? null : XRow["LB_PRO_CATEGORY"].ToString();
                            InParam.Use = Convert.IsDBNull(XRow["LB_PRO_USE"]) ? null : XRow["LB_PRO_USE"].ToString();
                            InParam.Name = Convert.IsDBNull(XRow["LB_PRO_NAME"]) ? null : XRow["LB_PRO_NAME"].ToString();
                            InParam.Address = Convert.IsDBNull(XRow["LB_PRO_ADDRESS"]) ? null : XRow["LB_PRO_ADDRESS"].ToString();
                            InParam.LB_Add1 = Convert.IsDBNull(XRow["LB_ADDRESS1"]) ? null : XRow["LB_ADDRESS1"].ToString();
                            InParam.LB_Add2 = Convert.IsDBNull(XRow["LB_ADDRESS2"]) ? null : XRow["LB_ADDRESS2"].ToString();
                            InParam.LB_Add3 = Convert.IsDBNull(XRow["LB_ADDRESS3"]) ? null : XRow["LB_ADDRESS3"].ToString();
                            InParam.LB_Add4 = Convert.IsDBNull(XRow["LB_ADDRESS4"]) ? null : XRow["LB_ADDRESS4"].ToString();
                            InParam.LB_CityID = Convert.IsDBNull(XRow["LB_CITY_ID"]) ? null : XRow["LB_CITY_ID"].ToString();
                            InParam.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e";
                            InParam.LB_DisttID = Convert.IsDBNull(XRow["LB_DISTRICT_ID"]) ? null : XRow["LB_DISTRICT_ID"].ToString();
                            InParam.LB_PinCode = Convert.IsDBNull(XRow["LB_PINCODE"]) ? null : XRow["LB_PINCODE"].ToString();
                            InParam.LB_StateID = Convert.IsDBNull(XRow["LB_STATE_ID"]) ? null : XRow["LB_STATE_ID"].ToString();
                            InParam.Ownership = Convert.IsDBNull(XRow["LB_OWNERSHIP"]) ? null : XRow["LB_OWNERSHIP"].ToString();
                            InParam.Owner_Party_ID = PartyID;
                            InParam.SurveyNo = Convert.IsDBNull(XRow["LB_SURVEY_NO"]) ? null : XRow["LB_SURVEY_NO"].ToString();
                            InParam.TotalArea = Convert.IsDBNull(XRow["LB_TOT_P_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_TOT_P_AREA"]);
                            InParam.ConstructedArea = Convert.IsDBNull(XRow["LB_CON_AREA"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_CON_AREA"]);
                            InParam.ConstructionYear = Convert.IsDBNull(XRow["LB_CON_YEAR"]) ? null : XRow["LB_CON_YEAR"].ToString();
                            InParam.RCCRoof = Convert.IsDBNull(XRow["LB_RCC_ROOF"]) ? null : XRow["LB_RCC_ROOF"].ToString();
                            InParam.DepositAmount = Convert.IsDBNull(XRow["LB_DEPOSIT_AMT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_DEPOSIT_AMT"]);
                            if (IsDate(Convert.IsDBNull(XRow["LB_PAID_DATE"]) ? null : XRow["LB_PAID_DATE"].ToString()))
                            {
                                InParam.PaymentDate = Convert.ToDateTime(Convert.IsDBNull(XRow["LB_PAID_DATE"]) ? null : XRow["LB_PAID_DATE"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PaymentDate = Convert.IsDBNull(XRow["LB_PAID_DATE"]) ? null : XRow["LB_PAID_DATE"].ToString();
                            }
                            //'InParam.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), null, XRow("LB_PAID_DATE"))

                            InParam.MonthlyRent = Convert.IsDBNull(XRow["LB_MONTH_RENT"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_MONTH_RENT"]);

                            InParam.MonthlyOtherExpenses = Convert.IsDBNull(XRow["LB_MONTH_O_PAYMENTS"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["LB_MONTH_O_PAYMENTS"]);
                            if (IsDate(Convert.IsDBNull(XRow["LB_PERIOD_FROM"]) ? null : XRow["LB_PERIOD_FROM"].ToString()))
                            {
                                InParam.PeriodFrom = Convert.ToDateTime(Convert.IsDBNull(XRow["LB_PERIOD_FROM"]) ? null : XRow["LB_PERIOD_FROM"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PeriodFrom = Convert.IsDBNull(XRow["LB_PERIOD_FROM"]) ? null : XRow["LB_PERIOD_FROM"].ToString();
                            }
                            //'InParam.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), null, XRow("LB_PERIOD_FROM"))
                            if (IsDate(Convert.IsDBNull(XRow["LB_PERIOD_TO"]) ? null : XRow["LB_PERIOD_TO"].ToString()))
                            {
                                InParam.PeriodTo = Convert.ToDateTime(Convert.IsDBNull(XRow["LB_PERIOD_TO"]) ? null : XRow["LB_PERIOD_TO"]).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParam.PeriodTo = Convert.IsDBNull(XRow["LB_PERIOD_TO"]) ? null : XRow["LB_PERIOD_TO"].ToString();
                            }
                            //'InParam.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), null, XRow("LB_PERIOD_TO"))                        
                            InParam.OtherDocs = Convert.IsDBNull(XRow["LB_DOC_OTHERS"]) ? null : XRow["LB_DOC_OTHERS"].ToString();
                            InParam.DocNames = Convert.IsDBNull(XRow["LB_DOC_NAME"]) ? null : XRow["LB_DOC_NAME"].ToString();
                            InParam.Value = Convert.IsDBNull(XRow["Amount"]) ? Convert.ToDouble(null) : Convert.ToDouble(XRow["Amount"]);
                            InParam.OtherDetails = Convert.IsDBNull(XRow["LB_OTHER_DETAIL"]) ? null : XRow["LB_OTHER_DETAIL"].ToString();
                            InParam.MasterID = model.xMID;
                            InParam.SrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                            InParam.Status_Action = Status_Action;
                            InParam.RecID = Convert.IsDBNull(XRow["LB_REC_ID"]) ? null : XRow["LB_REC_ID"].ToString();

                            //'EXTENSIONS 
                            Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[] ExtInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[0];
                            int cnt1 = 0;
                            if (!(LB_EXTENDED_PROPERTY_TABLE == null))
                            {
                                ExtInfo = new Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding[LB_EXTENDED_PROPERTY_TABLE.Rows.Count + 1];
                                foreach (DataRow _Ext_Row in LB_EXTENDED_PROPERTY_TABLE.Rows)
                                {
                                    if (_Ext_Row["LB_REC_ID"].ToString() == XRow["LB_REC_ID"].ToString())
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
                                            InEInfo.MOU_Date = Convert.ToDateTime(Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"]).ToString(BASE._Server_Date_Format_Short);
                                        }
                                        else
                                        {
                                            InEInfo.MOU_Date = Convert.IsDBNull(_Ext_Row["LB_MOU_DATE"]) ? null : _Ext_Row["LB_MOU_DATE"].ToString();
                                        }
                                        //'InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), null, _Ext_Row("LB_MOU_DATE"))
                                        InEInfo.Value = Convert.ToDouble(_Ext_Row["LB_VALUE"]);
                                        InEInfo.OtherDetails = Convert.IsDBNull(_Ext_Row["LB_OTHER_DETAIL"]) ? null : _Ext_Row["LB_OTHER_DETAIL"].ToString();
                                        InEInfo.Status_Action = Status_Action;
                                        InEInfo.RecID = System.Guid.NewGuid().ToString();

                                        ExtInfo[cnt1] = InEInfo;
                                        cnt1 += 1;
                                    }
                                }
                                InParam.InsertExtInfo = ExtInfo;
                            }
                            Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[] DocInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[0];
                            cnt1 = 0;
                            if (!(LB_DOCS_ARRAY == null))
                            {
                                DocInfo = new Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding[LB_DOCS_ARRAY.Rows.Count + 1];
                                foreach (DataRow _Ext_Row in LB_DOCS_ARRAY.Rows)
                                {
                                    if (_Ext_Row["LB_REC_ID"].ToString() == XRow["LB_REC_ID"].ToString())
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
                                InParam.InsertDocInfo = DocInfo;
                            }

                            DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, Convert.IsDBNull(XRow["LB_REC_ID"]) ? null : XRow["LB_REC_ID"].ToString());
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

                            InsertProperty[cnt] = InParam;
                        }

                        //'----------WIP References------------
                        if (XRow["Item_Profile"].ToString().Equals("WIP"))
                        {
                            if (!(XRow["WIP_REF_TYPE"] == null))
                            {
                                if (XRow["WIP_REF_TYPE"].ToString() == "NEW")
                                {
                                    Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile InReference = new Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile();
                                    InReference.LedID = Convert.IsDBNull(XRow["Item_Led_ID"]) ? null : XRow["Item_Led_ID"].ToString();
                                    InReference.Reference = Convert.IsDBNull(XRow["REFERENCE"]) ? null : XRow["REFERENCE"].ToString();
                                    InReference.Amount = Convert.ToDecimal(XRow["Amount"]);
                                    InReference.Status_Action = Status_Action;
                                    InReference.TxnID = model.xMID;
                                    InReference.TxnSrNo = Convert.IsDBNull(XRow["Sr."]) ? Convert.ToInt32(null) : Convert.ToInt32(XRow["Sr."]);
                                    InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment;
                                    InsertReferencesWIP[cnt] = InReference;
                                }
                            }
                        }
                        cnt += 1;
                    }
                    EditParam.InsertAssets = InsertAssets;
                    EditParam.InsertGS = InsertGS;
                    EditParam.InsertItem = InsertItem;
                    EditParam.InsertLivestock = InsertLivestock;
                    EditParam.InsertProperty = InsertProperty;
                    EditParam.InsertPurpose = InsertPurpose;
                    EditParam.InsertVehicles = InsertVehicles;
                    EditParam.InsertReferencesWIP = InsertReferencesWIP;

                    //FCRA Update Process               
                    if (model.DNK_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.DNK_SplVchrReferenceSelected.Split(',');
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

                    if (!(BASE._Gift_DBOps.UpdateGift_Txn(EditParam)))
                    {
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
                    return Json(new { jsonParam, CashbookGridPK = model.xMID + model.xID }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Param_Txn_Delete_VoucherGift DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_VoucherGift();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)     //'DELETE
                {
                    foreach (DataRow XRow in DT_DNK.Rows)
                    {
                        if (XRow["Item_Profile"].ToString() == "LAND & BUILDING" || XRow["Item_Profile"].ToString() == "OTHER ASSETS" || (XRow["ITEM_VOUCHER_TYPE"].ToString().Trim().ToUpper() == "LAND & BUILDING" && XRow["Item_Profile"].ToString().ToUpper() != "LAND & BUILDING"))
                        {
                            if (BASE.IsInsuranceAudited())
                            {
                                jsonParam.message = "Insurance Related Assets Cannot be Deleted After The Completion of Insurance Audit";
                                jsonParam.result = false;
                                jsonParam.title = "Information...";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    DataTable LB_REC_ID = BASE._L_B_DBOps.GetIDsBytxnID(model.xMID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment);
                    DataTable TblRecID = LB_REC_ID.Copy();
                    //'check any L&B Expenses done on basis of requested deletion entries                        
                    foreach (DataRow _RECROW in TblRecID.Rows)
                    {
                        DataTable Dependent_payments = BASE._Payment_DBOps.GetTxnDetailsByRefID(_RECROW[0].ToString());
                        if (Dependent_payments.Rows.Count > 0)
                        {
                            DateTime TrDate = Convert.ToDateTime(Dependent_payments.Rows[0]["TR_Date"]);
                            jsonParam.message = "A Construction Expense Entry of Rs." + Dependent_payments.Rows[0]["TR_AMOUNT"] + " with date " + TrDate.ToString("dd-MMM-yyy") + " is dependednt on this voucher. Please delete that entry first!!";
                            jsonParam.result = false;
                            jsonParam.title = model.TitleX;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    //'PROFILE ENTRIES DELETE
                    DelParam.MID_DeleteGS = model.xMID;
                    DelParam.MID_DeleteAssets = model.xMID;
                    DelParam.MID_DeleteLS = model.xMID;
                    DelParam.MID_DeleteVehicle = model.xMID;
                    DelParam.MID_ReferenceDelete = model.xMID;

                    //'Get Rec ID for Curr TxnMaster ID
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
                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteItems = model.xMID;
                    DelParam.MID_DeletePurpose = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;

                    if (!BASE._Gift_DBOps.DeleteGift_Txn(DelParam))
                    {
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
        private ContentResult Data_Binding_DNK(DataTable d1, DataTable d3, DataTable d4, ref DonationInKind model)
        {
            model.LastEditedOn = Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]);
            DataTable GS = BASE._Gift_DBOps.GetGoldSilverList(model.xMID);
            DataTable AI = BASE._Gift_DBOps.GetAssetList(model.xMID);
            DataTable VI = BASE._Gift_DBOps.GetVehiclesList(model.xMID);
            DataTable LS = BASE._Gift_DBOps.GetLiveStockList(model.xMID);
            DataTable LB = BASE._Payment_DBOps.GetLandBuilingList(model.xMID);
            DataTable WIP = BASE._Payment_DBOps.Get_WIP_List(model.xMID);

            if (GS == null || AI == null || VI == null || LS == null || LB == null)
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
            }
            model.Txt_V_NO_DNK = d1.Rows[0]["TR_VNO"].ToString();
            if (!Convert.IsDBNull(d1.Rows[0]["TR_AB_ID_1"]))
            {
                if (d1.Rows[0]["TR_AB_ID_1"].ToString().Length > 0)
                {
                    model.GLookUp_PartyList1_DNK = d1.Rows[0]["TR_AB_ID_1"].ToString();
                }
            }
            model.Txt_Narration_DNK = d3.Rows[0]["TR_NARRATION"].ToString();
            model.Txt_Reference_DNK = d3.Rows[0]["TR_REFERENCE"].ToString();

            DataSet JointData = new DataSet();
            JointData.Tables.Add(d4.Copy());
            JointData.Tables.Add(GS.Copy());
            DataRelation GS_Relation = JointData.Relations.Add("GS", JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_M_ID"], JointData.Tables["Gold_Silver_Info"].Columns["GS_TR_ID"], false);
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
            //OTHER ASSETS
            JointData.Tables.Add(AI.Copy());
            DataRelation AI_Relation = JointData.Relations.Add("AI", JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_M_ID"], JointData.Tables["Asset_Info"].Columns["AI_TR_ID"], false);
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
                        XROW["LOC_ID"] = _Row["AI_LOC_AL_ID"];
                        if (_Row["AI_IMAGE"] != null)
                        {
                            XROW["AI_IMAGE"] = _Row["AI_IMAGE"];
                        }
                        XROW["AI_INS_AMT"] = _Row["AI_AMT_FOR_INS"];
                    }
                }
            }

            //For Vehicles
            JointData.Tables.Add(VI.Copy());
            DataRelation VI_Relation = JointData.Relations.Add("VI", JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_M_ID"], JointData.Tables["Vehicles_Info"].Columns["VI_TR_ID"], false);
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
            DataRelation LS_Relation = JointData.Relations.Add("LS", JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_M_ID"], JointData.Tables["Live_Stock_Info"].Columns["LS_TR_ID"], false);
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
            DataRelation WIP_Relation = JointData.Relations.Add("WIP", JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_M_ID"], JointData.Tables["WIP_INFO"].Columns["WIP_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(WIP_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_Row["WIP_TR_ITEM_SRNO"]))
                    {
                        XROW["WIP_REF"] = _Row["WIP_REF"];
                        XROW["WIP_REC_ID"] = _Row["REC_ID"];
                        XROW["WIP_REF_TYPE"] = "NEW";
                    }
                }
            }

            //FOR Land&Building
            JointData.Tables.Add(LB.Copy());
            DataRelation LB_Relation = JointData.Relations.Add("LB", JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_M_ID"], JointData.Tables["Land_Building_info"].Columns["LB_TR_ID"], false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(LB_Relation))
                {
                    if (XROW["TR_SR_NO"].Equals(_Row["LB_TR_ITEM_SRNO"]))
                    {
                        XROW["LB_PRO_TYPE"] = _Row["LB_PRO_TYPE"];
                        XROW["LB_PRO_CATEGORY"] = _Row["LB_PRO_CATEGORY"];
                        XROW["LB_PRO_USE"] = _Row["LB_PRO_USE"];
                        XROW["LB_PRO_NAME"] = _Row["LB_PRO_NAME"];
                        XROW["LB_PRO_ADDRESS"] = _Row["LB_PRO_ADDRESS"];
                        XROW["LB_OWNERSHIP"] = _Row["LB_OWNERSHIP"];
                        XROW["LB_OWNERSHIP_PARTY_ID"] = _Row["LB_OWNERSHIP_PARTY_ID"];
                        XROW["LB_SURVEY_NO"] = _Row["LB_SURVEY_NO"];
                        XROW["LB_CON_YEAR"] = _Row["LB_CON_YEAR"];
                        XROW["LB_RCC_ROOF"] = _Row["LB_RCC_ROOF"];
                        XROW["LB_PAID_DATE"] = _Row["LB_PAID_DATE"];
                        XROW["LB_PERIOD_FROM"] = _Row["LB_PERIOD_FROM"];
                        XROW["LB_PERIOD_TO"] = _Row["LB_PERIOD_TO"];
                        XROW["LB_DOC_OTHERS"] = _Row["LB_DOC_OTHERS"];
                        XROW["LB_DOC_NAME"] = _Row["LB_DOC_NAME"];
                        XROW["LB_OTHER_DETAIL"] = _Row["LB_OTHER_DETAIL"];
                        XROW["LB_TOT_P_AREA"] = _Row["LB_TOT_P_AREA"];
                        XROW["LB_CON_AREA"] = _Row["LB_CON_AREA"];
                        XROW["LB_DEPOSIT_AMT"] = _Row["LB_DEPOSIT_AMT"];
                        XROW["LB_MONTH_RENT"] = _Row["LB_MONTH_RENT"];
                        XROW["LB_MONTH_O_PAYMENTS"] = _Row["LB_MONTH_O_PAYMENTS"];
                        XROW["LB_REC_ID"] = _Row["LB_REC_ID"];
                        XROW["LB_ADDRESS1"] = _Row["LB_ADDRESS1"];
                        XROW["LB_ADDRESS2"] = _Row["LB_ADDRESS2"];
                        XROW["LB_ADDRESS3"] = _Row["LB_ADDRESS3"];
                        XROW["LB_ADDRESS4"] = _Row["LB_ADDRESS4"];
                        XROW["LB_COUNTRY_ID"] = _Row["LB_COUNTRY_ID"];
                        XROW["LB_STATE_ID"] = _Row["LB_STATE_ID"];
                        XROW["LB_DISTRICT_ID"] = _Row["LB_DISTRICT_ID"];
                        XROW["LB_CITY_ID"] = _Row["LB_CITY_ID"];
                        XROW["LB_PINCODE"] = _Row["LB_PINCODE"];
                    }
                }
            }
            //Docs
            LB_DOCS_ARRAY = new DataTable();

            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

            foreach (DataRow LBRow in LB.Rows)
            {
                DataTable docs = BASE._L_B_DBOps.GetDocumentRecord(LBRow["LB_REC_ID"].ToString(), Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift);
                foreach (DataRow docRow in docs.Rows)
                {
                    DataRow Row = LB_DOCS_ARRAY.NewRow();
                    Row["LB_MISC_ID"] = docRow["LB_MISC_ID"].ToString();
                    Row["LB_REC_ID"] = docRow["LB_REC_ID"].ToString();
                    LB_DOCS_ARRAY.Rows.Add(Row);
                }
            }

            //Extended Property
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


            foreach (DataRow LBRow in LB.Rows)
            {
                DataTable extensions = BASE._L_B_DBOps.GetExtendedRecord(LBRow["LB_REC_ID"].ToString(), Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift);
                foreach (DataRow extensionsRow in extensions.Rows)
                {
                    DataRow Row = LB_EXTENDED_PROPERTY_TABLE.NewRow();
                    Row["LB_MOU_DATE"] = extensionsRow["LB_MOU_DATE"].ToString();
                    Row["LB_SR_NO"] = extensionsRow["LB_SR_NO"].ToString();
                    Row["LB_INS_ID"] = extensionsRow["LB_INS_ID"].ToString();
                    Row["LB_TOT_P_AREA"] = Convert.ToDouble(extensionsRow["LB_TOT_P_AREA"]);
                    Row["LB_CON_AREA"] = Convert.ToDouble(extensionsRow["LB_CON_AREA"]);
                    Row["LB_CON_YEAR"] = extensionsRow["LB_CON_YEAR"].ToString();
                    Row["LB_VALUE"] = Convert.ToDouble(extensionsRow["LB_VALUE"]);
                    Row["LB_OTHER_DETAIL"] = extensionsRow["LB_OTHER_DETAIL"].ToString();
                    Row["LB_REC_ID"] = extensionsRow["LB_REC_ID"].ToString();
                    LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                }
            }

            //FOR Land&Building Expenses
            JointData.Tables.Add(d3.Copy());
            DataColumn[] parentColumns = { JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_M_ID"], JointData.Tables["TRANSACTION_D_ITEM_INFO"].Columns["TR_SR_NO"] };


            DataColumn[] childColumns = { JointData.Tables["TRANSACTION_INFO"].Columns["TR_M_ID"], JointData.Tables["TRANSACTION_INFO"].Columns["TR_SR_NO"] };


            DataRelation LB_Exp_Relation = JointData.Relations.Add("LB_Exp", parentColumns, childColumns, false);
            foreach (DataRow XROW in JointData.Tables[0].Rows)
            {
                foreach (DataRow _Row in XROW.GetChildRows(LB_Exp_Relation))
                {
                    if (XROW["ITEM_PROFILE"].ToString() == "WIP")
                    {
                        if (XROW["WIP_REC_ID"].ToString().Length == 0)
                        {
                            XROW["WIP_REC_ID"] = _Row["TR_TRF_CROSS_REF_ID"];
                            XROW["WIP_REF_TYPE"] = "EXISTING";
                        }
                    }
                    else
                    {
                        if (XROW["LB_REC_ID"].ToString().Length == 0)
                        {
                            XROW["LB_REC_ID"] = _Row["TR_TRF_CROSS_REF_ID"]; //'Reference to Land and Building entry for which the expense has been incurred 
                        }
                    }
                }
            }

            //ITEM DETAIL
            foreach (DataRow XRow in JointData.Tables[0].Rows)
            {
                DataRow ROW_DNK = DT_DNK.NewRow();
                ROW_DNK["Sr."] = XRow["TR_SR_NO"];
                ROW_DNK["Item_ID"] = XRow["TR_ITEM_ID"];
                ROW_DNK["Item_Led_ID"] = XRow["TR_LED_ID"];
                ROW_DNK["Item_Trans_Type"] = XRow["TR_TRANS_TYPE"];
                ROW_DNK["Item_Party_Req"] = XRow["TR_PARTY_REQ"];
                ROW_DNK["Item_Profile"] = XRow["TR_PROFILE"];
                ROW_DNK["ITEM_VOUCHER_TYPE"] = XRow["ITEM_VOUCHER_TYPE"];
                ROW_DNK["Item Name"] = XRow["TR_ITEM_NAME"];
                ROW_DNK["Head"] = XRow["TR_ITEM_HEAD"];
                ROW_DNK["Qty."] = XRow["TR_QTY"];
                ROW_DNK["Unit"] = XRow["TR_UNIT"];
                ROW_DNK["Rate"] = XRow["TR_RATE"];
                ROW_DNK["Amount"] = XRow["TR_AMOUNT"];
                ROW_DNK["Remarks"] = XRow["TR_REMARKS"];

                //Purpose
                ROW_DNK["Pur_ID"] = XRow["Pur_ID"];
                ROW_DNK["LOC_ID"] = XRow["LOC_ID"];

                //Gold/Silver
                ROW_DNK["GS_DESC_MISC_ID"] = XRow["GS_DESC_MISC_ID"];
                ROW_DNK["GS_ITEM_WEIGHT"] = XRow["GS_ITEM_WEIGHT"];

                //OTHER ASSET
                ROW_DNK["AI_TYPE"] = XRow["AI_TYPE"]; //'Bug #5125 FIX
                ROW_DNK["AI_MAKE"] = XRow["AI_MAKE"];
                ROW_DNK["AI_MODEL"] = XRow["AI_MODEL"];
                ROW_DNK["AI_SERIAL_NO"] = XRow["AI_SERIAL_NO"];
                ROW_DNK["AI_WARRANTY"] = XRow["AI_WARRANTY"];
                if (XRow["AI_IMAGE"] != null)
                {
                    ROW_DNK["AI_IMAGE"] = XRow["AI_IMAGE"];
                }
                ROW_DNK["AI_PUR_DATE"] = XRow["AI_PUR_DATE"];
                ROW_DNK["AI_INS_AMT"] = XRow["AI_INS_AMT"];
                //LIVE STOCK
                ROW_DNK["LS_NAME"] = XRow["LS_NAME"];
                ROW_DNK["LS_BIRTH_YEAR"] = XRow["LS_BIRTH_YEAR"];
                ROW_DNK["LS_INSURANCE"] = XRow["LS_INSURANCE"];
                ROW_DNK["LS_INSURANCE_ID"] = XRow["LS_INSURANCE_ID"];
                ROW_DNK["LS_INS_POLICY_NO"] = XRow["LS_INS_POLICY_NO"];
                ROW_DNK["LS_INS_AMT"] = XRow["LS_INS_AMT"];
                ROW_DNK["LS_INS_DATE"] = XRow["LS_INS_DATE"];

                //VEHICLES
                ROW_DNK["VI_MAKE"] = XRow["VI_MAKE"];
                ROW_DNK["VI_MODEL"] = XRow["VI_MODEL"];
                ROW_DNK["VI_REG_NO_PATTERN"] = XRow["VI_REG_NO_PATTERN"];
                ROW_DNK["VI_REG_NO"] = XRow["VI_REG_NO"];
                ROW_DNK["VI_REG_DATE"] = XRow["VI_REG_DATE"];
                ROW_DNK["VI_OWNERSHIP"] = XRow["VI_OWNERSHIP"];
                ROW_DNK["VI_OWNERSHIP_AB_ID"] = XRow["VI_OWNERSHIP_AB_ID"];
                ROW_DNK["VI_DOC_RC_BOOK"] = XRow["VI_DOC_RC_BOOK"];
                ROW_DNK["VI_DOC_AFFIDAVIT"] = XRow["VI_DOC_AFFIDAVIT"];
                ROW_DNK["VI_DOC_WILL"] = XRow["VI_DOC_WILL"];
                ROW_DNK["VI_DOC_TRF_LETTER"] = XRow["VI_DOC_TRF_LETTER"];
                ROW_DNK["VI_DOC_FU_LETTER"] = XRow["VI_DOC_FU_LETTER"];
                ROW_DNK["VI_DOC_OTHERS"] = XRow["VI_DOC_OTHERS"];
                ROW_DNK["VI_DOC_NAME"] = XRow["VI_DOC_NAME"];
                ROW_DNK["VI_INSURANCE_ID"] = XRow["VI_INSURANCE_ID"];
                ROW_DNK["VI_INS_POLICY_NO"] = XRow["VI_INS_POLICY_NO"];
                ROW_DNK["VI_INS_EXPIRY_DATE"] = XRow["VI_INS_EXPIRY_DATE"];

                //Land & Building
                ROW_DNK["LB_PRO_TYPE"] = XRow["LB_PRO_TYPE"];
                ROW_DNK["LB_PRO_CATEGORY"] = XRow["LB_PRO_CATEGORY"];
                ROW_DNK["LB_PRO_USE"] = XRow["LB_PRO_USE"];
                ROW_DNK["LB_PRO_NAME"] = XRow["LB_PRO_NAME"];
                ROW_DNK["LB_PRO_ADDRESS"] = XRow["LB_PRO_ADDRESS"];
                ROW_DNK["LB_OWNERSHIP"] = XRow["LB_OWNERSHIP"];
                ROW_DNK["LB_OWNERSHIP_PARTY_ID"] = XRow["LB_OWNERSHIP_PARTY_ID"];
                ROW_DNK["LB_SURVEY_NO"] = XRow["LB_SURVEY_NO"];
                ROW_DNK["LB_CON_YEAR"] = XRow["LB_CON_YEAR"];
                ROW_DNK["LB_RCC_ROOF"] = XRow["LB_RCC_ROOF"];
                ROW_DNK["LB_PAID_DATE"] = XRow["LB_PAID_DATE"];
                ROW_DNK["LB_PERIOD_FROM"] = XRow["LB_PERIOD_FROM"];
                ROW_DNK["LB_PERIOD_TO"] = XRow["LB_PERIOD_TO"];
                ROW_DNK["LB_DOC_OTHERS"] = XRow["LB_DOC_OTHERS"];
                ROW_DNK["LB_DOC_NAME"] = XRow["LB_DOC_NAME"];
                ROW_DNK["LB_OTHER_DETAIL"] = XRow["LB_OTHER_DETAIL"];
                ROW_DNK["LB_TOT_P_AREA"] = XRow["LB_TOT_P_AREA"];
                ROW_DNK["LB_CON_AREA"] = XRow["LB_CON_AREA"];
                ROW_DNK["LB_DEPOSIT_AMT"] = XRow["LB_DEPOSIT_AMT"];
                ROW_DNK["LB_MONTH_RENT"] = XRow["LB_MONTH_RENT"];
                ROW_DNK["LB_MONTH_O_PAYMENTS"] = XRow["LB_MONTH_O_PAYMENTS"];
                ROW_DNK["LB_REC_ID"] = XRow["LB_REC_ID"];
                ROW_DNK["LB_ADDRESS1"] = XRow["LB_ADDRESS1"];
                ROW_DNK["LB_ADDRESS2"] = XRow["LB_ADDRESS2"];
                ROW_DNK["LB_ADDRESS3"] = XRow["LB_ADDRESS3"];
                ROW_DNK["LB_ADDRESS4"] = XRow["LB_ADDRESS4"];
                ROW_DNK["LB_STATE_ID"] = XRow["LB_STATE_ID"];
                ROW_DNK["LB_DISTRICT_ID"] = XRow["LB_DISTRICT_ID"];
                ROW_DNK["LB_CITY_ID"] = XRow["LB_CITY_ID"];
                ROW_DNK["LB_PINCODE"] = XRow["LB_PINCODE"];
                //ROW_DNK["LB_ADDRESS"] = XRow["LB_ADDRESS"]

                //WIP
                ROW_DNK["REF_REC_ID"] = XRow["WIP_REF_TYPE"].ToString() == "NEW" ? "" : XRow["WIP_REC_ID"];
                ROW_DNK["REFERENCE"] = XRow["WIP_REF"];
                ROW_DNK["WIP_REF_TYPE"] = XRow["WIP_REF_TYPE"];

                DT_DNK.Rows.Add(ROW_DNK);
            }
            Sub_Amt_Calculation(false);
            model.iParty_Req = iParty_Req;
            model.Txt_SubTotal_DNK = Txt_SubTotal ?? 0.00;
            return null;
        }
        public void Sub_Amt_Calculation(bool Delete_Action)
        {
            DataView dv = DT_DNK.DefaultView;
            dv.Sort = "Sr.";
            DT_DNK = dv.ToTable();
            double xAmt = 0;
            int count = DT_DNK.Rows.Count;
            for (int I = 0; I < count; I++)
            {
                if (Delete_Action)
                {
                    DT_DNK.Rows[I]["Sr."] = I + 1;
                }
                xAmt += Convert.ToDouble(DT_DNK.Rows[I]["Amount"]);
                if (DT_DNK.Rows[I]["Item_Party_Req"].ToString().ToUpper().Trim() == "YES")
                {
                    iParty_Req = true;
                }
            }
            Txt_SubTotal = xAmt;
        }
        public string FindLocationUsage(Common.Navigation_Mode TAG, string PropertyID = "", bool Exclude_Sold_TF = true)
        {
            string Message = "";
            DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_Gift, PropertyID);
            foreach (DataRow cRow in Locations.Rows)
            {
                string LocationID = cRow[0].ToString();
                string UsedPage = BASE._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF);
                bool DeleteAllow = true;
                if (UsedPage.Length > 0)
                {
                    DeleteAllow = false;
                }
                if (!DeleteAllow)
                {
                    if (TAG == Common.Navigation_Mode._Delete)
                    {
                        Message = "Can't Delete...!" + "<br>" + "<br>" + "Property Created in this Voucher is being used in Another Page as Location. . . !" + "<br>" + "<br>" + "Name : " + UsedPage;
                    }
                    else
                    {
                        Message = "Can't Edit...!" + "<br>" + "<br>" + "Property Created in this Voucher is being used in Another Page as Location. . . !" + "<br>" + "<br>" + "Name : " + UsedPage;
                    }
                    break;
                }
            }
            return Message;
        }
        public void SetGridData_DNK()
        {
            DT_DNK = new DataTable();
            DT_DNK.TableName = "Item_Detail";
            DT_DNK.Columns.Add("Sr.", Type.GetType("System.Int32"));
            DT_DNK.Columns.Add("Item_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Item_Led_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Item_Trans_Type", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Item_Party_Req", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Item_Profile", Type.GetType("System.String"));
            DT_DNK.Columns.Add("ITEM_VOUCHER_TYPE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Item Name", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Head", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Qty.", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("Unit", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Rate", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("Amount", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("Remarks", Type.GetType("System.String"));
            DT_DNK.Columns.Add("Pur_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LOC_ID", Type.GetType("System.String"));
            //---Gold / Silver---- -
            DT_DNK.Columns.Add("GS_DESC_MISC_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("GS_ITEM_WEIGHT", Type.GetType("System.Decimal"));
            //---Other Asset---- -
            DT_DNK.Columns.Add("AI_TYPE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("AI_MAKE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("AI_MODEL", Type.GetType("System.String"));
            DT_DNK.Columns.Add("AI_SERIAL_NO", Type.GetType("System.String"));
            DT_DNK.Columns.Add("AI_PUR_DATE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("AI_WARRANTY", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("AI_IMAGE", Type.GetType("System.Byte[]"));
            DT_DNK.Columns.Add("AI_INS_AMT", Type.GetType("System.Double"));
            //---LIVE STOCK----
            DT_DNK.Columns.Add("LS_NAME", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LS_BIRTH_YEAR", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LS_INSURANCE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LS_INSURANCE_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LS_INS_POLICY_NO", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LS_INS_AMT", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("LS_INS_DATE", Type.GetType("System.String"));

            //---Vehicles------
            DT_DNK.Columns.Add("VI_MAKE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_MODEL", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_REG_NO_PATTERN", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_REG_NO", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_REG_DATE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_OWNERSHIP", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_OWNERSHIP_AB_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_DOC_RC_BOOK", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_DOC_AFFIDAVIT", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_DOC_WILL", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_DOC_TRF_LETTER", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_DOC_FU_LETTER", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_DOC_OTHERS", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_DOC_NAME", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_INSURANCE_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_INS_POLICY_NO", Type.GetType("System.String"));
            DT_DNK.Columns.Add("VI_INS_EXPIRY_DATE", Type.GetType("System.String"));

            //-----Land & Building---- -
            DT_DNK.Columns.Add("LB_PRO_TYPE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_PRO_CATEGORY", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_PRO_USE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_PRO_NAME", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_PRO_ADDRESS", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_OWNERSHIP", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_OWNERSHIP_PARTY_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_SURVEY_NO", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_RCC_ROOF", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_DEPOSIT_AMT", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("LB_PAID_DATE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_MONTH_RENT", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("LB_MONTH_O_PAYMENTS", Type.GetType("System.Double"));
            DT_DNK.Columns.Add("LB_PERIOD_FROM", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_PERIOD_TO", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_DOC_OTHERS", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_DOC_NAME", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_ADDRESS1", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_ADDRESS2", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_ADDRESS3", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_ADDRESS4", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_STATE_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_DISTRICT_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_CITY_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("LB_PINCODE", Type.GetType("System.String"));

            //--WIP--
            DT_DNK.Columns.Add("REF_REC_ID", Type.GetType("System.String"));
            DT_DNK.Columns.Add("REFERENCE", Type.GetType("System.String"));
            DT_DNK.Columns.Add("WIP_REF_TYPE", Type.GetType("System.String"));
            //}
        }
        public ActionResult Frm_Win_Gift_Item_Listing_Grid()
        {
            ViewData["Txt_SubTotal"] = Txt_SubTotal ?? 0.00;
            return PartialView(DT_DNK);
        }
        #region Item
        [HttpPost]
        public void Fill_Item_Grid(DonationInKind_Item_Detail xfrm)
        {
            bool Delete_Action = false;
            xfrm.Tag_DNK_Itm = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), xfrm.ActionMethod_DNK_Itm);
            if (xfrm.Tag_DNK_Itm == Common.Navigation_Mode._New)
            {
                DataView dv = DT_DNK.DefaultView;
                dv.Sort = "Sr.";
                DT_DNK = dv.ToTable();
                DataRow ROW = DT_DNK.NewRow();
                if (DT_DNK.Rows.Count == 0)
                {
                    ROW["Sr."] = 1;
                }
                else
                {
                    ROW["Sr."] = Convert.ToInt32(DT_DNK.Rows[DT_DNK.Rows.Count - 1]["Sr."]) + 1;
                }
                ROW["Item Name"] = xfrm.GLookUp_ItemName_DNK_Itm;
                ROW["Item_ID"] = xfrm.GLookUp_ItemList_DNK_Itm;
                if (xfrm.iCond_Ledger_ID_DNK_Itm != "00000")
                {
                    if (xfrm.Txt_Amount_DNK_Itm > xfrm.iMinValue_DNK_Itm && xfrm.Txt_Amount_DNK_Itm <= xfrm.iMaxValue_DNK_Itm)
                    {
                        ROW["Item_Led_ID"] = xfrm.iCond_Ledger_ID_DNK_Itm;
                    }
                    else
                    {
                        ROW["Item_Led_ID"] = xfrm.iLed_ID_DNK_Itm;
                    }
                }
                else
                {
                    ROW["Item_Led_ID"] = xfrm.iLed_ID_DNK_Itm;
                }
                ROW["Item_Trans_Type"] = xfrm.iTrans_Type_DNK_Itm;
                ROW["Item_Profile"] = xfrm.iProfile_DNK_Itm;
                ROW["ITEM_VOUCHER_TYPE"] = xfrm.iVoucher_Type_DNK_Itm;
                ROW["Item_Party_Req"] = xfrm.iParty_Req_DNK_Itm;
                ROW["Head"] = xfrm.BE_Item_Head_DNK_Itm;
                ROW["Qty."] = xfrm.Txt_Qty_DNK_Itm ?? 0.00;
                ROW["Unit"] = xfrm.Cmd_UOM_DNK_Itm;
                ROW["Rate"] = xfrm.Txt_Rate_DNK_Itm ?? 0.00;
                ROW["Amount"] = xfrm.Txt_Amount_DNK_Itm;
                ROW["Remarks"] = xfrm.Txt_Remarks_DNK_Itm;
                ROW["Pur_ID"] = xfrm.GLookUp_PurList_DNK_Itm;
                ROW["LB_REC_ID"] = xfrm.LB_REC_ID_DNK_Itm;
                if (xfrm.iProfile_DNK_Itm == "GOLD" || xfrm.iProfile_DNK_Itm == "SILVER")
                {
                    ROW["GS_DESC_MISC_ID"] = xfrm.GS_DESC_MISC_ID_DNK_Itm;
                    ROW["GS_ITEM_WEIGHT"] = xfrm.GS_ITEM_WEIGHT_DNK_Itm;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "OTHER ASSETS")
                {
                    ROW["AI_TYPE"] = xfrm.AI_TYPE_DNK_Itm;
                    ROW["AI_MAKE"] = xfrm.AI_MAKE_DNK_Itm;
                    ROW["AI_MODEL"] = xfrm.AI_MODEL_DNK_Itm;
                    ROW["AI_SERIAL_NO"] = xfrm.AI_SERIAL_NO_DNK_Itm;
                    if (IsDate(xfrm.AI_PUR_DATE_DNK_Itm))
                    {
                        ROW["AI_PUR_DATE"] = xfrm.AI_PUR_DATE_DNK_Itm;
                    }
                    ROW["AI_WARRANTY"] = xfrm.AI_WARRANTY_DNK_Itm;
                    //if (DNK_Asset_Image != null)
                    //{
                    //    ROW["AI_IMAGE"] = DNK_Asset_Image;
                    //}
                    ROW["AI_IMAGE"] = DNK_Asset_Image;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                    ROW["AI_INS_AMT"] = xfrm.AI_INS_AMT_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "LIVESTOCK")
                {
                    ROW["LS_NAME"] = xfrm.LS_NAME_DNK_Itm;
                    ROW["LS_BIRTH_YEAR"] = xfrm.LS_BIRTH_YEAR_DNK_Itm;
                    ROW["LS_INSURANCE"] = xfrm.LS_INSURANCE_DNK_Itm;
                    ROW["LS_INSURANCE_ID"] = xfrm.LS_INSURANCE_ID_DNK_Itm;
                    ROW["LS_INS_POLICY_NO"] = xfrm.LS_INS_POLICY_NO_DNK_Itm;
                    ROW["LS_INS_AMT"] = xfrm.LS_INS_AMT_DNK_Itm;
                    if (IsDate(xfrm.LS_INS_DATE_DNK_Itm))
                    {
                        ROW["LS_INS_DATE"] = xfrm.LS_INS_DATE_DNK_Itm;
                    }
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "VEHICLES")
                {
                    ROW["VI_MAKE"] = xfrm.VI_MAKE_DNK_Itm;
                    ROW["VI_MODEL"] = xfrm.VI_MODEL_DNK_Itm;
                    ROW["VI_REG_NO_PATTERN"] = xfrm.VI_REG_NO_PATTERN_DNK_Itm;
                    ROW["VI_REG_NO"] = xfrm.VI_REG_NO_DNK_Itm;
                    ROW["VI_REG_DATE"] = xfrm.VI_REG_DATE_DNK_Itm;
                    ROW["VI_OWNERSHIP"] = xfrm.VI_OWNERSHIP_DNK_Itm;
                    ROW["VI_OWNERSHIP_AB_ID"] = xfrm.VI_OWNERSHIP_AB_ID_DNK_Itm;
                    ROW["VI_DOC_RC_BOOK"] = xfrm.VI_DOC_RC_BOOK_DNK_Itm;
                    ROW["VI_DOC_AFFIDAVIT"] = xfrm.VI_DOC_AFFIDAVIT_DNK_Itm;
                    ROW["VI_DOC_WILL"] = xfrm.VI_DOC_WILL_DNK_Itm;
                    ROW["VI_DOC_TRF_LETTER"] = xfrm.VI_DOC_TRF_LETTER_DNK_Itm;
                    ROW["VI_DOC_FU_LETTER"] = xfrm.VI_DOC_FU_LETTER_DNK_Itm;
                    ROW["VI_DOC_OTHERS"] = xfrm.VI_DOC_OTHERS_DNK_Itm;
                    ROW["VI_DOC_NAME"] = xfrm.VI_DOC_NAME_DNK_Itm;
                    ROW["VI_INSURANCE_ID"] = xfrm.VI_INSURANCE_ID_DNK_Itm;
                    ROW["VI_INS_POLICY_NO"] = xfrm.VI_INS_POLICY_NO_DNK_Itm;
                    ROW["VI_INS_EXPIRY_DATE"] = xfrm.VI_INS_EXPIRY_DATE_DNK_Itm;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "LAND & BUILDING")
                {
                    ROW["LB_PRO_TYPE"] = xfrm.LB_PRO_TYPE_DNK_Itm;
                    ROW["LB_PRO_CATEGORY"] = xfrm.LB_PRO_CATEGORY_DNK_Itm;
                    ROW["LB_PRO_USE"] = xfrm.LB_PRO_USE_DNK_Itm;
                    ROW["LB_PRO_NAME"] = xfrm.LB_PRO_NAME_DNK_Itm;
                    ROW["LB_PRO_ADDRESS"] = xfrm.LB_PRO_ADDRESS_DNK_Itm;
                    //ROW["LB_ADDRESS"] = xfrm.LB_ADDRESS;
                    ROW["LB_ADDRESS1"] = xfrm.LB_ADDRESS1_DNK_Itm;
                    ROW["LB_ADDRESS2"] = xfrm.LB_ADDRESS2_DNK_Itm;
                    ROW["LB_ADDRESS3"] = xfrm.LB_ADDRESS3_DNK_Itm;
                    ROW["LB_ADDRESS4"] = xfrm.LB_ADDRESS4_DNK_Itm;
                    ROW["LB_CITY_ID"] = xfrm.LB_CITY_ID_DNK_Itm;
                    ROW["LB_DISTRICT_ID"] = xfrm.LB_DISTRICT_ID_DNK_Itm;
                    ROW["LB_STATE_ID"] = xfrm.LB_STATE_ID_DNK_Itm;
                    ROW["LB_PINCODE"] = xfrm.LB_PINCODE_DNK_Itm;
                    ROW["LB_OWNERSHIP"] = xfrm.LB_OWNERSHIP_DNK_Itm;
                    ROW["LB_OWNERSHIP_PARTY_ID"] = xfrm.LB_OWNERSHIP_PARTY_ID_DNK_Itm;
                    ROW["LB_SURVEY_NO"] = xfrm.LB_SURVEY_NO_DNK_Itm;
                    ROW["LB_CON_YEAR"] = xfrm.LB_CON_YEAR_DNK_Itm;
                    ROW["LB_RCC_ROOF"] = xfrm.LB_RCC_ROOF_DNK_Itm;
                    ROW["LB_PAID_DATE"] = xfrm.LB_PAID_DATE_DNK_Itm;
                    ROW["LB_PERIOD_FROM"] = xfrm.LB_PERIOD_FROM_DNK_Itm;
                    ROW["LB_PERIOD_TO"] = xfrm.LB_PERIOD_TO_DNK_Itm;
                    ROW["LB_DOC_OTHERS"] = xfrm.LB_DOC_OTHERS_DNK_Itm;
                    ROW["LB_DOC_NAME"] = xfrm.LB_DOC_NAME_DNK_Itm;
                    ROW["LB_OTHER_DETAIL"] = xfrm.LB_OTHER_DETAIL_DNK_Itm;
                    ROW["LB_TOT_P_AREA"] = xfrm.LB_TOT_P_AREA_DNK_Itm;
                    ROW["LB_CON_AREA"] = xfrm.LB_CON_AREA_DNK_Itm;
                    ROW["LB_DEPOSIT_AMT"] = xfrm.LB_DEPOSIT_AMT_DNK_Itm;
                    ROW["LB_MONTH_RENT"] = xfrm.LB_MONTH_RENT_DNK_Itm;
                    ROW["LB_MONTH_O_PAYMENTS"] = xfrm.LB_MONTH_O_PAYMENTS_DNK_Itm;
                    if (xfrm.List_LB_DOCS_ARRAY_DNK_Itm != null)
                    {
                        var Raw_LB_DOCS_ARRAY = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>>(xfrm.List_LB_DOCS_ARRAY_DNK_Itm);
                        xfrm.LB_DOCS_ARRAY_DNK_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>(Raw_LB_DOCS_ARRAY);
                    }
                    if (xfrm.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm != null)
                    {
                        var Raw_LB_EXTENDED_PROPERTY_TABLE = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>>(xfrm.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm);
                        xfrm.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>(Raw_LB_EXTENDED_PROPERTY_TABLE);
                    }
                    if (LB_DOCS_ARRAY == null)
                    {
                        LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY_DNK_Itm;
                    }
                    else
                    {
                        if (LB_DOCS_ARRAY.Rows.Count <= 0)
                        {
                            LB_DOCS_ARRAY = new DataTable();
                            LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                            LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));
                        }
                        foreach (DataRow XROW in xfrm.LB_DOCS_ARRAY_DNK_Itm.Rows)
                        {
                            DataRow row = LB_DOCS_ARRAY.NewRow();
                            row["LB_MISC_ID"] = XROW["LB_MISC_ID"].ToString();
                            row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_DOCS_ARRAY.Rows.Add(row);
                        }
                    }
                    if (LB_EXTENDED_PROPERTY_TABLE == null)
                    {
                        LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm;
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
                        foreach (DataRow XROW in xfrm.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Rows)
                        {
                            DataRow row = LB_EXTENDED_PROPERTY_TABLE.NewRow();
                            row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"].ToString();
                            row["LB_SR_NO"] = XROW["LB_SR_NO"].ToString();
                            row["LB_INS_ID"] = XROW["LB_INS_ID"].ToString();
                            row["LB_TOT_P_AREA"] = !string.IsNullOrEmpty(XROW["LB_TOT_P_AREA"].ToString()) ? double.Parse(XROW["LB_TOT_P_AREA"].ToString()) : 0;
                            row["LB_CON_AREA"] = !string.IsNullOrEmpty(XROW["LB_CON_AREA"].ToString()) ? double.Parse(XROW["LB_CON_AREA"].ToString()) : 0;
                            row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"].ToString();
                            row["LB_VALUE"] = !string.IsNullOrEmpty(XROW["LB_VALUE"].ToString()) ? double.Parse(XROW["LB_VALUE"].ToString()) : 0;
                            row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"].ToString();
                            row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_EXTENDED_PROPERTY_TABLE.Rows.Add(row);
                        }
                    }
                }
                if (xfrm.iProfile_DNK_Itm.Equals("WIP"))
                {
                    if (xfrm.iRefType_DNK_Itm == "NEW")
                    {
                        ROW["REFERENCE"] = xfrm.iReference_DNK_Itm;
                        ROW["WIP_REF_TYPE"] = xfrm.iRefType_DNK_Itm;
                    }
                    else
                    {
                        ROW["REF_REC_ID"] = xfrm.Ref_RecID_DNK_Itm;
                        ROW["WIP_REF_TYPE"] = xfrm.iRefType_DNK_Itm;
                    }
                }
                DT_DNK.Rows.Add(ROW);
            }
            else if (xfrm.Tag_DNK_Itm == Common.Navigation_Mode._Edit)
            {
                DataRow ROW = DT_DNK.Select("[Sr.] =" + xfrm.Sr)[0];
                ROW["Item Name"] = xfrm.GLookUp_ItemName_DNK_Itm;
                ROW["Item_ID"] = xfrm.GLookUp_ItemList_DNK_Itm;
                if (xfrm.iCond_Ledger_ID_DNK_Itm != "00000")
                {
                    if (xfrm.Txt_Amount_DNK_Itm > xfrm.iMinValue_DNK_Itm && xfrm.Txt_Amount_DNK_Itm <= xfrm.iMaxValue_DNK_Itm)
                    {
                        ROW["Item_Led_ID"] = xfrm.iCond_Ledger_ID_DNK_Itm;
                    }
                    else
                    {
                        ROW["Item_Led_ID"] = xfrm.iLed_ID_DNK_Itm;
                    }
                }
                else
                {
                    ROW["Item_Led_ID"] = xfrm.iLed_ID_DNK_Itm;
                }
                ROW["Item_Trans_Type"] = xfrm.iTrans_Type_DNK_Itm;
                ROW["ITEM_VOUCHER_TYPE"] = xfrm.iVoucher_Type_DNK_Itm;
                ROW["Item_Profile"] = xfrm.iProfile_DNK_Itm;
                ROW["Item_Party_Req"] = xfrm.iParty_Req_DNK_Itm;
                ROW["Head"] = xfrm.BE_Item_Head_DNK_Itm;
                ROW["Qty."] = xfrm.Txt_Qty_DNK_Itm ?? 0.00;
                ROW["Unit"] = xfrm.Cmd_UOM_DNK_Itm;
                ROW["Rate"] = xfrm.Txt_Rate_DNK_Itm ?? 0.00;
                ROW["Amount"] = xfrm.Txt_Amount_DNK_Itm;
                ROW["Remarks"] = xfrm.Txt_Remarks_DNK_Itm;
                ROW["Pur_ID"] = xfrm.GLookUp_PurList_DNK_Itm;
                ROW["LB_REC_ID"] = xfrm.LB_REC_ID_DNK_Itm;
                if (xfrm.iProfile_DNK_Itm == "GOLD" || xfrm.iProfile_DNK_Itm == "SILVER")
                {
                    ROW["GS_DESC_MISC_ID"] = xfrm.GS_DESC_MISC_ID_DNK_Itm;
                    ROW["GS_ITEM_WEIGHT"] = xfrm.GS_ITEM_WEIGHT_DNK_Itm;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "OTHER ASSETS")
                {
                    ROW["AI_TYPE"] = xfrm.AI_TYPE_DNK_Itm;
                    ROW["AI_MAKE"] = xfrm.AI_MAKE_DNK_Itm;
                    ROW["AI_MODEL"] = xfrm.AI_MODEL_DNK_Itm;
                    ROW["AI_SERIAL_NO"] = xfrm.AI_SERIAL_NO_DNK_Itm;
                    if (IsDate(xfrm.AI_PUR_DATE_DNK_Itm))
                    {
                        ROW["AI_PUR_DATE"] = xfrm.AI_PUR_DATE_DNK_Itm;
                    }
                    ROW["AI_WARRANTY"] = xfrm.AI_WARRANTY_DNK_Itm;
                    ROW["AI_IMAGE"] = DNK_Asset_Image;
                    //if (DNK_Asset_Image != null)
                    //{
                    //    ROW["AI_IMAGE"] = DNK_Asset_Image;
                    //}
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                    ROW["AI_INS_AMT"] = xfrm.AI_INS_AMT_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "LIVESTOCK")
                {
                    ROW["LS_NAME"] = xfrm.LS_NAME_DNK_Itm;
                    ROW["LS_BIRTH_YEAR"] = xfrm.LS_BIRTH_YEAR_DNK_Itm;
                    ROW["LS_INSURANCE"] = xfrm.LS_INSURANCE_DNK_Itm;
                    ROW["LS_INSURANCE_ID"] = xfrm.LS_INSURANCE_ID_DNK_Itm;
                    ROW["LS_INS_POLICY_NO"] = xfrm.LS_INS_POLICY_NO_DNK_Itm;
                    ROW["LS_INS_AMT"] = xfrm.LS_INS_AMT_DNK_Itm;
                    if (IsDate(xfrm.LS_INS_DATE_DNK_Itm))
                    {
                        ROW["LS_INS_DATE"] = xfrm.LS_INS_DATE_DNK_Itm;
                    }
                    else
                    {
                        ROW["LS_INS_DATE"] = "";
                    }
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "VEHICLES")
                {
                    ROW["VI_MAKE"] = xfrm.VI_MAKE_DNK_Itm;
                    ROW["VI_MODEL"] = xfrm.VI_MODEL_DNK_Itm;
                    ROW["VI_REG_NO_PATTERN"] = xfrm.VI_REG_NO_PATTERN_DNK_Itm;
                    ROW["VI_REG_NO"] = xfrm.VI_REG_NO_DNK_Itm;
                    ROW["VI_REG_DATE"] = xfrm.VI_REG_DATE_DNK_Itm;
                    ROW["VI_OWNERSHIP"] = xfrm.VI_OWNERSHIP_DNK_Itm;
                    ROW["VI_OWNERSHIP_AB_ID"] = xfrm.VI_OWNERSHIP_AB_ID_DNK_Itm;
                    ROW["VI_DOC_RC_BOOK"] = xfrm.VI_DOC_RC_BOOK_DNK_Itm;
                    ROW["VI_DOC_AFFIDAVIT"] = xfrm.VI_DOC_AFFIDAVIT_DNK_Itm;
                    ROW["VI_DOC_WILL"] = xfrm.VI_DOC_WILL_DNK_Itm;
                    ROW["VI_DOC_TRF_LETTER"] = xfrm.VI_DOC_TRF_LETTER_DNK_Itm;
                    ROW["VI_DOC_FU_LETTER"] = xfrm.VI_DOC_FU_LETTER_DNK_Itm;
                    ROW["VI_DOC_OTHERS"] = xfrm.VI_DOC_OTHERS_DNK_Itm;
                    ROW["VI_DOC_NAME"] = xfrm.VI_DOC_NAME_DNK_Itm;
                    ROW["VI_INSURANCE_ID"] = xfrm.VI_INSURANCE_ID_DNK_Itm;
                    ROW["VI_INS_POLICY_NO"] = xfrm.VI_INS_POLICY_NO_DNK_Itm;
                    ROW["VI_INS_EXPIRY_DATE"] = xfrm.VI_INS_EXPIRY_DATE_DNK_Itm;
                    ROW["LOC_ID"] = xfrm.X_LOC_ID_DNK_Itm;
                }
                if (xfrm.iProfile_DNK_Itm == "LAND & BUILDING")
                {
                    ROW["LB_PRO_TYPE"] = xfrm.LB_PRO_TYPE_DNK_Itm;
                    ROW["LB_PRO_CATEGORY"] = xfrm.LB_PRO_CATEGORY_DNK_Itm;
                    ROW["LB_PRO_USE"] = xfrm.LB_PRO_USE_DNK_Itm;
                    ROW["LB_PRO_NAME"] = xfrm.LB_PRO_NAME_DNK_Itm;
                    ROW["LB_PRO_ADDRESS"] = xfrm.LB_PRO_ADDRESS_DNK_Itm;
                    //ROW["LB_ADDRESS"] = xfrm.LB_ADDRESS;
                    ROW["LB_ADDRESS1"] = xfrm.LB_ADDRESS1_DNK_Itm;
                    ROW["LB_ADDRESS2"] = xfrm.LB_ADDRESS2_DNK_Itm;
                    ROW["LB_ADDRESS3"] = xfrm.LB_ADDRESS3_DNK_Itm;
                    ROW["LB_ADDRESS4"] = xfrm.LB_ADDRESS4_DNK_Itm;
                    ROW["LB_CITY_ID"] = xfrm.LB_CITY_ID_DNK_Itm;
                    ROW["LB_DISTRICT_ID"] = xfrm.LB_DISTRICT_ID_DNK_Itm;
                    ROW["LB_STATE_ID"] = xfrm.LB_STATE_ID_DNK_Itm;
                    ROW["LB_PINCODE"] = xfrm.LB_PINCODE_DNK_Itm;
                    ROW["LB_OWNERSHIP"] = xfrm.LB_OWNERSHIP_DNK_Itm;
                    ROW["LB_OWNERSHIP_PARTY_ID"] = xfrm.LB_OWNERSHIP_PARTY_ID_DNK_Itm;
                    ROW["LB_SURVEY_NO"] = xfrm.LB_SURVEY_NO_DNK_Itm;
                    ROW["LB_CON_YEAR"] = xfrm.LB_CON_YEAR_DNK_Itm;
                    ROW["LB_RCC_ROOF"] = xfrm.LB_RCC_ROOF_DNK_Itm;
                    ROW["LB_PAID_DATE"] = xfrm.LB_PAID_DATE_DNK_Itm;
                    ROW["LB_PERIOD_FROM"] = xfrm.LB_PERIOD_FROM_DNK_Itm;
                    ROW["LB_PERIOD_TO"] = xfrm.LB_PERIOD_TO_DNK_Itm;
                    ROW["LB_DOC_OTHERS"] = xfrm.LB_DOC_OTHERS_DNK_Itm;
                    ROW["LB_DOC_NAME"] = xfrm.LB_DOC_NAME_DNK_Itm;
                    ROW["LB_OTHER_DETAIL"] = xfrm.LB_OTHER_DETAIL_DNK_Itm;
                    ROW["LB_TOT_P_AREA"] = xfrm.LB_TOT_P_AREA_DNK_Itm;
                    ROW["LB_CON_AREA"] = xfrm.LB_CON_AREA_DNK_Itm;
                    ROW["LB_DEPOSIT_AMT"] = xfrm.LB_DEPOSIT_AMT_DNK_Itm;
                    ROW["LB_MONTH_RENT"] = xfrm.LB_MONTH_RENT_DNK_Itm;
                    ROW["LB_MONTH_O_PAYMENTS"] = xfrm.LB_MONTH_O_PAYMENTS_DNK_Itm;
                    if (xfrm.List_LB_DOCS_ARRAY_DNK_Itm != null)
                    {
                        var Raw_LB_DOCS_ARRAY = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>>(xfrm.List_LB_DOCS_ARRAY_DNK_Itm);
                        xfrm.LB_DOCS_ARRAY_DNK_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Profile.Models.LB_DOCS_ARRAY_List>(Raw_LB_DOCS_ARRAY);
                    }
                    if (xfrm.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm != null)
                    {
                        var Raw_LB_EXTENDED_PROPERTY_TABLE = JsonConvert.DeserializeObject<List<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>>(xfrm.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm);
                        xfrm.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm = CommonFunctions.ConvertToDataTable<ConnectOneMVC.Areas.Account.Models.LB_EXTENDED_PROPERTY_TABLE_List>(Raw_LB_EXTENDED_PROPERTY_TABLE);
                    }
                    if (LB_DOCS_ARRAY == null)
                    {
                        LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY_DNK_Itm;
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
                            if (!(XROW["LB_REC_ID"].ToString() == xfrm.LB_REC_ID_DNK_Itm))
                            {
                                New_LB_DOCS_ARRAY.ImportRow(XROW);
                            }
                        }
                        LB_DOCS_ARRAY = New_LB_DOCS_ARRAY;
                        foreach (DataRow XROW in xfrm.LB_DOCS_ARRAY_DNK_Itm.Rows)
                        {
                            DataRow Row = LB_DOCS_ARRAY.NewRow();
                            Row["LB_MISC_ID"] = XROW["LB_MISC_ID"].ToString();
                            Row["LB_REC_ID"] = XROW["LB_REC_ID"].ToString();
                            LB_DOCS_ARRAY.Rows.Add(Row);
                        }
                    }
                    if (LB_EXTENDED_PROPERTY_TABLE == null)
                    {
                        LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm;
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
                            if (!(XROW["LB_REC_ID"].ToString() == xfrm.LB_REC_ID_DNK_Itm))
                            {
                                New_LB_EXTENDED_PROPERTY_TABLE.ImportRow(XROW);
                            }
                        }
                        LB_EXTENDED_PROPERTY_TABLE = New_LB_EXTENDED_PROPERTY_TABLE;
                        New_LB_EXTENDED_PROPERTY_TABLE.Dispose();
                        foreach (DataRow XROW in xfrm.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Rows)
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
                if (xfrm.iProfile_DNK_Itm.Equals("WIP"))
                {
                    ROW["REF_REC_ID"] = xfrm.Ref_RecID_DNK_Itm;
                    ROW["REFERENCE"] = xfrm.iReference_DNK_Itm;
                    ROW["WIP_REF_TYPE"] = xfrm.iRefType_DNK_Itm;
                }
            }
            else if (xfrm.Tag_DNK_Itm == Common.Navigation_Mode._Delete)
            {
                Delete_Action = true;
            }
            Sub_Amt_Calculation(Delete_Action);
        }
        public ActionResult Frm_Voucher_Win_Gift_Item(string Tag, string iTxnM_ID, bool iSpecific_Allow = false, string iSpecific_ItemID = null, string Vdt = "", int Grid_RowNo = 0, string MainTag = "")
        {
            DonationInKind_Item_Detail model = new DonationInKind_Item_Detail();
            model.ActionMethod_DNK_Itm = Tag;
            model.Tag_DNK_Itm = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod_DNK_Itm);
            model.Sr = Grid_RowNo;
            ViewBag.YearID = BASE._open_Year_ID;
            Return_Json_Param jsonParam = new Return_Json_Param();
            if (model.Tag_DNK_Itm != Common.Navigation_Mode._Delete)
            {
                model.TitleX_DNK_Itm = "Item Detail";
                model.Cmd_UOM_DNK_Itm = "NO";
                RefreshItemList_Itm_Dtel();
                RefreshPurList_Itm_Dtel();
            }
            if (model.Tag_DNK_Itm == Common.Navigation_Mode._New)
            {
                model.Me_Text_DNK_Itm = "New ~ " + model.TitleX_DNK_Itm;
                model.iTxnM_ID_DNK_Itm = iTxnM_ID;
                model.iSpecific_Allow_DNK_Itm = iSpecific_Allow;
                model.iSpecific_ItemID_DNK_Itm = iSpecific_ItemID;
                model.Vdt_DNK_Itm = string.IsNullOrWhiteSpace(Vdt) ? null : Convert.ToDateTime(Vdt).ToString(BASE._Date_Format_Current);
                if (BASE._open_Year_ID <= 2223) model.Txt_Amount_DNK_Itm = 1;
                return View(model);
            }
            else if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit || model.Tag_DNK_Itm == Common.Navigation_Mode._View)
            {
                if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit)
                {
                    model.Me_Text_DNK_Itm = "Edit ~ " + model.TitleX_DNK_Itm;
                }
                else
                {
                    model.Me_Text_DNK_Itm = "View ~ " + model.TitleX_DNK_Itm;
                }
                var GridRow_ToEdit = DT_DNK.Select("[Sr.] =" + Grid_RowNo);
                model.iTxnM_ID_DNK_Itm = iTxnM_ID;
                model.iSpecific_ItemID_DNK_Itm = GridRow_ToEdit[0]["Item_ID"].ToString();
                model.iProfile_OLD_DNK_Itm = GridRow_ToEdit[0]["Item_Profile"].ToString();
                model.iPur_ID_DNK_Itm = GridRow_ToEdit[0]["Pur_ID"].ToString();
                model.Txt_Qty_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["Qty."]);
                model.Txt_Rate_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["Rate"]);
                model.Txt_Amount_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["Amount"]);
                model.Txt_Remarks_DNK_Itm = GridRow_ToEdit[0]["Remarks"].ToString();
                model.LB_REC_ID_DNK_Itm = GridRow_ToEdit[0]["LB_REC_ID"].ToString();

                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "GOLD" || GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "SILVER")
                {
                    model.GS_DESC_MISC_ID_DNK_Itm = GridRow_ToEdit[0]["GS_DESC_MISC_ID"].ToString();
                    model.GS_ITEM_WEIGHT_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["GS_ITEM_WEIGHT"]);
                    model.X_LOC_ID_DNK_Itm = GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "OTHER ASSETS")
                {
                    model.AI_TYPE_DNK_Itm = GridRow_ToEdit[0]["AI_TYPE"].ToString();
                    model.AI_MAKE_DNK_Itm = GridRow_ToEdit[0]["AI_MAKE"].ToString();
                    model.AI_MODEL_DNK_Itm = GridRow_ToEdit[0]["AI_MODEL"].ToString();
                    model.AI_SERIAL_NO_DNK_Itm = GridRow_ToEdit[0]["AI_SERIAL_NO"].ToString();
                    model.AI_PUR_DATE_DNK_Itm = GridRow_ToEdit[0]["AI_PUR_DATE"].ToString();
                    model.AI_INS_AMT_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["AI_INS_AMT"]);
                    model.AI_WARRANTY_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["AI_WARRANTY"]);
                    if (!Convert.IsDBNull(GridRow_ToEdit[0]["AI_IMAGE"]))
                    {
                        model.AI_IMAGE_DNK_Itm = (byte[])GridRow_ToEdit[0]["AI_IMAGE"];
                    }
                    model.X_LOC_ID_DNK_Itm = GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "LIVESTOCK")
                {
                    model.LS_NAME_DNK_Itm = GridRow_ToEdit[0]["LS_NAME"].ToString();
                    model.LS_BIRTH_YEAR_DNK_Itm = GridRow_ToEdit[0]["LS_BIRTH_YEAR"].ToString();
                    model.LS_INSURANCE_DNK_Itm = GridRow_ToEdit[0]["LS_INSURANCE"].ToString();
                    model.LS_INSURANCE_ID_DNK_Itm = GridRow_ToEdit[0]["LS_INSURANCE_ID"].ToString();
                    model.LS_INS_POLICY_NO_DNK_Itm = GridRow_ToEdit[0]["LS_INS_POLICY_NO"].ToString();
                    model.LS_INS_AMT_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["LS_INS_AMT"]);
                    model.LS_INS_DATE_DNK_Itm = GridRow_ToEdit[0]["LS_INS_DATE"].ToString();
                    model.X_LOC_ID_DNK_Itm = GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "VEHICLES")
                {
                    model.VI_MAKE_DNK_Itm = GridRow_ToEdit[0]["VI_MAKE"].ToString();
                    model.VI_MODEL_DNK_Itm = GridRow_ToEdit[0]["VI_MODEL"].ToString();
                    model.VI_REG_NO_PATTERN_DNK_Itm = GridRow_ToEdit[0]["VI_REG_NO_PATTERN"].ToString();
                    model.VI_REG_NO_DNK_Itm = GridRow_ToEdit[0]["VI_REG_NO"].ToString();
                    model.VI_REG_DATE_DNK_Itm = GridRow_ToEdit[0]["VI_REG_DATE"].ToString();
                    model.VI_OWNERSHIP_DNK_Itm = GridRow_ToEdit[0]["VI_OWNERSHIP"].ToString();
                    model.VI_OWNERSHIP_AB_ID_DNK_Itm = GridRow_ToEdit[0]["VI_OWNERSHIP_AB_ID"].ToString();
                    model.VI_DOC_RC_BOOK_DNK_Itm = GridRow_ToEdit[0]["VI_DOC_RC_BOOK"].ToString();
                    model.VI_DOC_AFFIDAVIT_DNK_Itm = GridRow_ToEdit[0]["VI_DOC_AFFIDAVIT"].ToString();
                    model.VI_DOC_WILL_DNK_Itm = GridRow_ToEdit[0]["VI_DOC_WILL"].ToString();
                    model.VI_DOC_TRF_LETTER_DNK_Itm = GridRow_ToEdit[0]["VI_DOC_TRF_LETTER"].ToString();
                    model.VI_DOC_FU_LETTER_DNK_Itm = GridRow_ToEdit[0]["VI_DOC_FU_LETTER"].ToString();
                    model.VI_DOC_OTHERS_DNK_Itm = GridRow_ToEdit[0]["VI_DOC_OTHERS"].ToString();
                    model.VI_DOC_NAME_DNK_Itm = GridRow_ToEdit[0]["VI_DOC_NAME"].ToString();
                    model.VI_INSURANCE_ID_DNK_Itm = GridRow_ToEdit[0]["VI_INSURANCE_ID"].ToString();
                    model.VI_INS_POLICY_NO_DNK_Itm = GridRow_ToEdit[0]["VI_INS_POLICY_NO"].ToString();
                    model.VI_INS_EXPIRY_DATE_DNK_Itm = GridRow_ToEdit[0]["VI_INS_EXPIRY_DATE"].ToString();
                    model.X_LOC_ID_DNK_Itm = GridRow_ToEdit[0]["LOC_ID"].ToString();
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "LAND & BUILDING")
                {
                    model.LB_PRO_TYPE_DNK_Itm = GridRow_ToEdit[0]["LB_PRO_TYPE"].ToString();
                    model.LB_PRO_CATEGORY_DNK_Itm = GridRow_ToEdit[0]["LB_PRO_CATEGORY"].ToString();
                    model.LB_PRO_USE_DNK_Itm = GridRow_ToEdit[0]["LB_PRO_USE"].ToString();
                    model.LB_PRO_NAME_DNK_Itm = GridRow_ToEdit[0]["LB_PRO_NAME"].ToString();
                    model.LB_PRO_ADDRESS_DNK_Itm = GridRow_ToEdit[0]["LB_PRO_ADDRESS"].ToString();
                    model.LB_ADDRESS1_DNK_Itm = GridRow_ToEdit[0]["LB_ADDRESS1"].ToString();
                    model.LB_ADDRESS2_DNK_Itm = GridRow_ToEdit[0]["LB_ADDRESS2"].ToString();
                    model.LB_ADDRESS3_DNK_Itm = GridRow_ToEdit[0]["LB_ADDRESS3"].ToString();
                    model.LB_ADDRESS4_DNK_Itm = GridRow_ToEdit[0]["LB_ADDRESS4"].ToString();
                    model.LB_CITY_ID_DNK_Itm = GridRow_ToEdit[0]["LB_CITY_ID"].ToString();
                    model.LB_DISTRICT_ID_DNK_Itm = GridRow_ToEdit[0]["LB_DISTRICT_ID"].ToString();
                    model.LB_STATE_ID_DNK_Itm = GridRow_ToEdit[0]["LB_STATE_ID"].ToString();
                    model.LB_PINCODE_DNK_Itm = GridRow_ToEdit[0]["LB_PINCODE"].ToString();
                    model.LB_OWNERSHIP_DNK_Itm = GridRow_ToEdit[0]["LB_OWNERSHIP"].ToString();
                    model.LB_OWNERSHIP_PARTY_ID_DNK_Itm = GridRow_ToEdit[0]["LB_OWNERSHIP_PARTY_ID"].ToString();
                    model.LB_SURVEY_NO_DNK_Itm = GridRow_ToEdit[0]["LB_SURVEY_NO"].ToString();
                    model.LB_CON_YEAR_DNK_Itm = GridRow_ToEdit[0]["LB_CON_YEAR"].ToString();
                    model.LB_RCC_ROOF_DNK_Itm = GridRow_ToEdit[0]["LB_RCC_ROOF"].ToString();
                    model.LB_PAID_DATE_DNK_Itm = GridRow_ToEdit[0]["LB_PAID_DATE"].ToString();
                    model.LB_PERIOD_FROM_DNK_Itm = GridRow_ToEdit[0]["LB_PERIOD_FROM"].ToString();
                    model.LB_PERIOD_TO_DNK_Itm = GridRow_ToEdit[0]["LB_PERIOD_TO"].ToString();
                    model.LB_DOC_OTHERS_DNK_Itm = GridRow_ToEdit[0]["LB_DOC_OTHERS"].ToString();
                    model.LB_DOC_NAME_DNK_Itm = GridRow_ToEdit[0]["LB_DOC_NAME"].ToString();
                    model.LB_OTHER_DETAIL_DNK_Itm = GridRow_ToEdit[0]["LB_OTHER_DETAIL"].ToString();
                    model.LB_TOT_P_AREA_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["LB_TOT_P_AREA"]);
                    model.LB_CON_AREA_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["LB_CON_AREA"]);
                    model.LB_DEPOSIT_AMT_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["LB_DEPOSIT_AMT"]);
                    model.LB_MONTH_RENT_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_RENT"]);
                    model.LB_MONTH_O_PAYMENTS_DNK_Itm = Convert.ToDouble(GridRow_ToEdit[0]["LB_MONTH_O_PAYMENTS"]);

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
                            if (XROW["LB_REC_ID"].ToString() == model.LB_REC_ID_DNK_Itm)
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
                                Row["LB_REC_ID"] = model.LB_REC_ID_DNK_Itm;
                                EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                            }
                        }
                    }
                    else
                    {
                        DataTable LB_Ext = BASE._L_B_DBOps.GetExtensionDetails(model.LB_REC_ID_DNK_Itm, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift);
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
                            Row["LB_REC_ID"] = model.LB_REC_ID_DNK_Itm;
                            EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row);
                        }
                    }

                    model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm = EDIT_LB_EXTENDED_PROPERTY_TABLE;

                    List<Models.LB_EXTENDED_PROPERTY_TABLE_List> Return_LB_EXTENDED_PROPERTY_TABLE_List = new List<Models.LB_EXTENDED_PROPERTY_TABLE_List>();
                    foreach (DataRow row in model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Rows)
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
                    model.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm = new JavaScriptSerializer().Serialize(Return_LB_EXTENDED_PROPERTY_TABLE_List);

                    DataTable EDIT_LB_DOCS_ARRAY = new DataTable();

                    EDIT_LB_DOCS_ARRAY.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
                    EDIT_LB_DOCS_ARRAY.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

                    if (LB_DOCS_ARRAY != null)
                    {
                        foreach (DataRow XROW in LB_DOCS_ARRAY.Rows)
                        {
                            if (XROW["LB_REC_ID"].ToString() == model.LB_REC_ID_DNK_Itm)
                            {
                                DataRow Row = EDIT_LB_DOCS_ARRAY.NewRow();
                                Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                                Row["LB_REC_ID"] = model.LB_REC_ID_DNK_Itm;
                                EDIT_LB_DOCS_ARRAY.Rows.Add(Row);
                            }
                        }
                    }
                    else
                    {
                        DataTable LB_DOC = BASE._L_B_DBOps.GetDocsDetails(model.LB_REC_ID_DNK_Itm, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift);
                        foreach (DataRow XROW in LB_DOC.Rows)
                        {
                            DataRow Row = EDIT_LB_DOCS_ARRAY.NewRow();
                            Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                            Row["LB_REC_ID"] = model.LB_REC_ID_DNK_Itm;
                            EDIT_LB_DOCS_ARRAY.Rows.Add(Row);
                        }
                    }
                    model.LB_DOCS_ARRAY_DNK_Itm = EDIT_LB_DOCS_ARRAY;
                    List<Models.LB_DOCS_ARRAY_List> return_LB_DOCS_ARRAY = new List<Models.LB_DOCS_ARRAY_List>();
                    foreach (DataRow row in model.LB_DOCS_ARRAY_DNK_Itm.Rows)
                    {
                        Models.LB_DOCS_ARRAY_List nrow = new Models.LB_DOCS_ARRAY_List();
                        nrow.LB_MISC_ID = row["LB_MISC_ID"].ToString();
                        nrow.LB_REC_ID = row["LB_REC_ID"].ToString();
                        return_LB_DOCS_ARRAY.Add(nrow);
                    }
                    model.List_LB_DOCS_ARRAY_DNK_Itm = new JavaScriptSerializer().Serialize(return_LB_DOCS_ARRAY);
                }
                if (GridRow_ToEdit[0]["Item_Profile"].ToString().ToUpper() == "WIP")
                {
                    model.iReference_DNK_Itm = GridRow_ToEdit[0]["REFERENCE"].ToString();
                    model.Ref_RecID_DNK_Itm = GridRow_ToEdit[0]["REF_REC_ID"].ToString();
                    model.iRefType_DNK_Itm = GridRow_ToEdit[0]["WIP_REF_TYPE"].ToString();
                }
                return View(model);
            }
            else if (model.Tag_DNK_Itm == Common.Navigation_Mode._Delete)
            {
                var GridRow_ToDelete = DT_DNK.Select("[Sr.] =" + Grid_RowNo);
                if (MainTag == "_Edit")
                {
                    if (GridRow_ToDelete[0]["Item_Profile"].ToString() == "LAND & BUILDING" || GridRow_ToDelete[0]["Item_Profile"].ToString() == "OTHER ASSETS")
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Insurance Related Assets Cannot be Added/Edited After The Completion of Insurance Audit";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (GridRow_ToDelete[0]["ITEM_VOUCHER_TYPE"].ToString() == "LAND & BUILDING" || GridRow_ToDelete[0]["Item_Profile"].ToString() == "LAND & BUILDING")
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Property Related Expenses Cannot be Added/Edited After The Completion of Insurance Audit";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    string Msg = "";
                    if (GridRow_ToDelete[0]["Item_Profile"].ToString() == "LAND & BUILDING")
                    {
                        Msg = FindLocationUsage(Common.Navigation_Mode._Edit, GridRow_ToDelete[0]["LB_REC_ID"].ToString(), false); //'sold/tf assets not excluded                 
                    }
                    if (Msg.Length > 0)
                    {
                        jsonParam.message = Msg;
                        jsonParam.title = "Edit ~ Donation in Kind";
                        jsonParam.result = false;
                        jsonParam.focusid = "Win_Gift_Item_Grid";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (GridRow_ToDelete[0]["Item_Profile"].ToString() == "WIP" && GridRow_ToDelete[0]["WIP_REF_TYPE"].ToString() == "EXISTING")
                    {
                        object RefId = GridRow_ToDelete[0]["REF_REC_ID"];
                        if (RefId != null)
                        {
                            if (RefId.ToString().Length > 0)
                            {
                                DataTable PROF_TABLE = CommonFunctions.GetReferenceData(BASE, "WIP", GridRow_ToDelete[0]["Item_ID"].ToString(), iTxnM_ID, null, Common_Lib.Common.Navigation_Mode._Delete, RefId.ToString());
                                if (PROF_TABLE.Rows.Count > 0)
                                {
                                    if (BASE.CheckNextYearID(BASE._next_Unaudited_YearID))
                                    {

                                        if (Convert.ToDouble(PROF_TABLE.Rows[0]["Next Year Closing Value"]) < 0)
                                        {
                                            jsonParam.message = "Sorry ! Deletion of Selected Payment Entry creates a Negative Closing Balance in Next Year for " + GridRow_ToDelete[0]["Item_Profile"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                            jsonParam.title = "Error!!";
                                            jsonParam.result = false;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    if (Convert.ToDouble(PROF_TABLE.Rows[0]["Curr Value"]) < 0)
                                    {
                                        jsonParam.message = "Sorry ! Deletion of Selected Payment Entry creates a Negative Closing Balance in Current Year for " + GridRow_ToDelete[0]["Item_Profile"].ToString().ToLower() + " with Original Value " + PROF_TABLE.Rows[0]["Org Value"].ToString();
                                        jsonParam.title = "Error!!";
                                        jsonParam.result = false;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public ActionResult Frm_Voucher_Win_Gift_Item_Window(DonationInKind_Item_Detail model)
        {
            model.Tag_DNK_Itm = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod_DNK_Itm);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag_DNK_Itm == Common_Lib.Common.Navigation_Mode._New || model.Tag_DNK_Itm == Common_Lib.Common.Navigation_Mode._Edit || model.Tag_DNK_Itm == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_ItemList_DNK_Itm))
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Item Name Not Selected. . . !";
                        jsonParam.focusid = "GLookUp_ItemList_DNK_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    //if (string.IsNullOrWhiteSpace(model.Cmd_UOM_DNK_Itm))
                    //{
                    //    jsonParam.title = "Incomplete Information . . .";
                    //    jsonParam.message = "Unit Not Selected. . . !";
                    //    jsonParam.focusid = "Cmd_UOM_DNK_Itm";
                    //    jsonParam.result = false;
                    //    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //}                   
                    //if(model.Txt_Rate_DNK_Itm != null)
                    //{
                    //    if (model.Txt_Rate_DNK_Itm <= 0)
                    //    {
                    //        jsonParam.title = "Incomplete Information . . .";
                    //        jsonParam.message = "Rate cannot be Zero/Negative. . . !";
                    //        jsonParam.focusid = "Txt_Rate_DNK_Itm";
                    //        jsonParam.result = false;
                    //        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    //    }
                    //}

                    if (model.Txt_Amount_DNK_Itm <= 0)
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Amount cannot be Zero/Negative. . . !";
                        jsonParam.focusid = "Txt_Amount_DNK_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Qty_DNK_Itm.ToString().Trim().Contains(".") && model.iProfile_DNK_Itm.ToUpper() == "OTHER ASSETS")
                    {
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.message = "Quantity cannot be partial in case of Movable Assets. . . !";
                        jsonParam.focusid = "Txt_Qty_DNK_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_PurList_DNK_Itm))
                    {
                        jsonParam.title = "Incomplete Information. . .";
                        jsonParam.message = "Purpose Not Selected. . . !";
                        jsonParam.focusid = "GLookUp_PurList_DNK_Itm";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                bool NewProfile = false;
                if (!string.IsNullOrWhiteSpace(model.iProfile_OLD_DNK_Itm))
                {
                    if (model.iProfile_DNK_Itm != model.iProfile_OLD_DNK_Itm)
                    {
                        NewProfile = true;
                    }
                }

                if (model.iVoucher_Type_DNK_Itm.Trim().ToUpper() == "LAND & BUILDING" && model.iProfile_DNK_Itm.ToUpper() != "LAND & BUILDING") //L&B Expense Item
                {
                    DataTable db_Table = (DataTable)BASE._Payment_DBOps.GetLandBuildingNames(model.iTxnM_ID_DNK_Itm ?? "");
                    if (db_Table == null)
                    {
                        jsonParam.title = "Error!!";
                        jsonParam.message = Messages.SomeError;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (db_Table.Rows.Count <= 0)
                    {
                        jsonParam.title = "Add Property First...";
                        jsonParam.message = "No Selectable Property Exists!" + "<br>" + "<br>" + "Please create a Land and Building entry before adding expenses for construction " + "<br>" + "or add a different voucher for expenses if you are using same voucher for property purchase and expenses!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.result = true;
                    jsonParam.popup_title = "Select Property";
                    jsonParam.popup_form_name = "Frm_Property_Select";
                    jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Property_Select/";
                    jsonParam.popup_querystring = "LB_REC_ID=" + Url.Encode(model.LB_REC_ID_DNK_Itm) + "&Txn_M_ID=" + Url.Encode(model.iTxnM_ID_DNK_Itm) + "&FromDNK=" + true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (model.iProfile_DNK_Itm == "WIP")
                {
                    jsonParam.result = true;
                    jsonParam.popup_title = "Reference Type";
                    jsonParam.popup_form_name = "Frm_Reference_Type";
                    jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Reference_Type/";
                    jsonParam.popup_querystring = "Tag=" + Url.Encode(model.Tag_DNK_Itm.ToString()) + "&iRefType=" + Url.Encode(model.iRefType_DNK_Itm) + "&iLed_ID=" + Url.Encode(model.iLed_ID_DNK_Itm) + "&FromDNK=" + true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                switch (model.iProfile_DNK_Itm.ToUpper())
                {
                    case "GOLD":
                    case "SILVER":
                        jsonParam.result = true;
                        jsonParam.popup_title = model.Me_Text_DNK_Itm + " (Gold / Silver Detail)...";
                        jsonParam.popup_form_name = "Frm_GoldSilver_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_GoldSilver_Window/";
                        jsonParam.popup_querystring = "Cmd_Type=" + Url.Encode(model.iProfile_DNK_Itm) +
                            "&BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_DNK_Itm) + "&FromDNK=" + true;
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._New)
                        {
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Url.Encode(model.Tag_DNK_Itm.ToString());
                        }
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit || model.Tag_DNK_Itm == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Url.Encode(Common.Navigation_Mode._New.ToString());
                            }
                            else
                            {
                                if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Url.Encode(Common.Navigation_Mode._Edit.ToString());
                                }
                                else
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Url.Encode(Common.Navigation_Mode._View.ToString());
                                }
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&GS_DESC_MISC_ID=" + Url.Encode(model.GS_DESC_MISC_ID_DNK_Itm) +
                                    "&GS_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_DNK_Itm) +
                                    "&Txt_Weight=" + model.GS_ITEM_WEIGHT_DNK_Itm +
                                    "&Txt_Others=" + Url.Encode(model.Txt_Remarks_DNK_Itm);
                            }
                        }
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    case "OTHER ASSETS":
                        jsonParam.result = true;
                        jsonParam.popup_title = model.Me_Text_DNK_Itm + " (Movable Asset Detail)...";
                        jsonParam.popup_form_name = "Frm_Asset_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Asset_Window/";
                        jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_DNK_Itm) +
                            "&IsGift=" + true + "&FromDNK=" + true;
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._New)
                        {
                            DNK_Asset_Image = null;
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New +
                                //"&Txt_Amt=" + model.Txt_Amount_DNK_Itm +
                                "&Txt_Rate=" + model.Txt_Rate_DNK_Itm +
                                "&Txt_Qty=" + model.Txt_Qty_DNK_Itm;
                            if (IsDate(model.Vdt_DNK_Itm))
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Date=" + Url.Encode(model.Vdt_DNK_Itm.ToString());
                            }
                        }
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit || model.Tag_DNK_Itm == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                }

                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Make=" + Url.Encode(model.AI_MAKE_DNK_Itm) +
                                    "&Txt_Model=" + Url.Encode(model.AI_MODEL_DNK_Itm) +
                                    "&Txt_Serial=" + Url.Encode(model.AI_SERIAL_NO_DNK_Itm) +
                                    "&Txt_Warranty=" + model.AI_WARRANTY_DNK_Itm;
                                DNK_Asset_Image = model.AI_IMAGE_DNK_Itm;
                                if (IsDate(model.AI_PUR_DATE_DNK_Itm))
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&AI_PUR_DATE=" + Url.Encode(model.AI_PUR_DATE_DNK_Itm);
                                }
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Amt=" + model.AI_INS_AMT_DNK_Itm +
                                    "&Txt_Rate=" + model.Txt_Rate_DNK_Itm +
                                    "&Txt_Qty=" + model.Txt_Qty_DNK_Itm +
                                    "&Txt_Others=" + Url.Encode(model.Txt_Remarks_DNK_Itm) +
                                    "&AI_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_DNK_Itm);

                            }
                        }
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);

                    case "LIVESTOCK":
                        jsonParam.result = true;
                        jsonParam.popup_title = model.Me_Text_DNK_Itm + " (Livestock Detail)...";
                        jsonParam.popup_form_name = "Frm_Live_Stock_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Live_Stock_Window/";
                        jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_DNK_Itm) + "&FromDNK=" + true;
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._New)
                        {
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                        }
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit || model.Tag_DNK_Itm == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                }

                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Name=" + Url.Encode(model.LS_NAME_DNK_Itm) +
                                    "&LS_BIRTH_YEAR=" + Url.Encode(model.LS_BIRTH_YEAR_DNK_Itm) +
                                    "&LS_INSURANCE=" + Url.Encode(model.LS_INSURANCE_DNK_Itm) +
                                    "&LS_INS_ID=" + Url.Encode(model.LS_INSURANCE_ID_DNK_Itm) +
                                    "&LS_INS_POLICY_NO=" + Url.Encode(model.LS_INS_POLICY_NO_DNK_Itm) +
                                    "&LS_INS_AMT=" + model.LS_INS_AMT_DNK_Itm;
                                if (IsDate(model.LS_INS_DATE_DNK_Itm))
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&LS_INS_DATE=" + Url.Encode(model.LS_INS_DATE_DNK_Itm);
                                }
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Others=" + Url.Encode(model.Txt_Remarks_DNK_Itm) +
                                    "&LS_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_DNK_Itm);
                            }
                        }
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    case "VEHICLES":
                        jsonParam.result = true;
                        jsonParam.popup_title = model.Me_Text_DNK_Itm + " (Vehicle Detail)...";
                        jsonParam.popup_form_name = "Frm_Vehicles_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Vehicles_Window/";
                        jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.GLookUp_ItemName_DNK_Itm) + "&FromDNK=" + true;

                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._New)
                        {
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                        }
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit || model.Tag_DNK_Itm == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                }

                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Cmd_Make=" + Url.Encode(model.VI_MAKE_DNK_Itm) +
                                    "&VI_MODEL=" + Url.Encode(model.VI_MODEL_DNK_Itm) +
                                    "&VI_REG_NO_PATTERN=" + Url.Encode(model.VI_REG_NO_PATTERN_DNK_Itm) +
                                    "&VI_REG_NO=" + Url.Encode(model.VI_REG_NO_DNK_Itm);

                                if (IsDate(model.VI_REG_DATE_DNK_Itm))
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&VI_REG_DATE=" + Url.Encode(model.VI_REG_DATE_DNK_Itm);
                                }

                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&VI_OWNERSHIP=" + Url.Encode(model.VI_OWNERSHIP_DNK_Itm) +
                                    "&VI_OWNERSHIP_AB_ID=" + Url.Encode(model.VI_OWNERSHIP_AB_ID_DNK_Itm) +
                                    "&VI_DOC_RC_BOOK=" + Url.Encode(model.VI_DOC_RC_BOOK_DNK_Itm) +
                                    "&VI_DOC_AFFIDAVIT=" + Url.Encode(model.VI_DOC_AFFIDAVIT_DNK_Itm) +
                                    "&VI_DOC_WILL=" + Url.Encode(model.VI_DOC_WILL_DNK_Itm) +
                                    "&VI_DOC_TRF_LETTER=" + Url.Encode(model.VI_DOC_TRF_LETTER_DNK_Itm) +
                                    "&VI_DOC_FU_LETTER=" + Url.Encode(model.VI_DOC_FU_LETTER_DNK_Itm) +
                                    "&VI_DOC_OTHERS=" + Url.Encode(model.VI_DOC_OTHERS_DNK_Itm) +
                                    "&VI_DOC_NAME=" + Url.Encode(model.VI_DOC_NAME_DNK_Itm) +
                                    "&VI_INSURANCE_ID=" + Url.Encode(model.VI_INSURANCE_ID_DNK_Itm) +
                                    "&Txt_PolicyNo=" + Url.Encode(model.VI_INS_POLICY_NO_DNK_Itm);

                                if (IsDate(model.VI_INS_EXPIRY_DATE_DNK_Itm))
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&VI_E_DATE=" + Url.Encode(model.VI_INS_EXPIRY_DATE_DNK_Itm);
                                }

                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Others=" + Url.Encode(model.Txt_Remarks_DNK_Itm) +
                                    "&VI_LOC_AL_ID=" + Url.Encode(model.X_LOC_ID_DNK_Itm);
                            }
                        }
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    case "LAND & BUILDING":
                        jsonParam.result = true;
                        jsonParam.popup_title = model.Me_Text_DNK_Itm + " (Land & Building Detail)...";
                        jsonParam.popup_form_name = "Frm_Property_Window";
                        jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Property_Window/";
                        jsonParam.popup_querystring = "IsGift=" + true + "&ITEM_ID=" + Url.Encode(model.GLookUp_ItemList_DNK_Itm) + "&FromDNK=" + true;

                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._New)
                        {
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                        }
                        if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit || model.Tag_DNK_Itm == Common.Navigation_Mode._View)
                        {
                            if (NewProfile)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New;
                            }
                            else
                            {
                                if (model.Tag_DNK_Itm == Common.Navigation_Mode._Edit)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                }

                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&LB_PRO_TYPE=" + Url.Encode(model.LB_PRO_TYPE_DNK_Itm) +
                                    "&LB_PRO_CATEGORY=" + Url.Encode(model.LB_PRO_CATEGORY_DNK_Itm) +
                                    "&LB_PRO_USE=" + Url.Encode(model.LB_PRO_USE_DNK_Itm) +
                                    "&LB_PRO_NAME=" + Url.Encode(model.LB_PRO_NAME_DNK_Itm) +
                                    "&LB_PRO_ADDRESS=" + Url.Encode(model.LB_PRO_ADDRESS_DNK_Itm) +
                                    "&LB_ADDRESS1=" + Url.Encode(model.LB_ADDRESS1_DNK_Itm) +
                                    "&LB_ADDRESS2=" + Url.Encode(model.LB_ADDRESS2_DNK_Itm) +
                                    "&LB_ADDRESS3=" + Url.Encode(model.LB_ADDRESS3_DNK_Itm) +
                                    "&LB_ADDRESS4=" + Url.Encode(model.LB_ADDRESS4_DNK_Itm) +
                                    "&LB_CITY_ID=" + Url.Encode(model.LB_CITY_ID_DNK_Itm) +
                                    "&LB_DISTRICT_ID=" + Url.Encode(model.LB_DISTRICT_ID_DNK_Itm) +
                                    "&LB_STATE_ID=" + Url.Encode(model.LB_STATE_ID_DNK_Itm) +
                                    "&LB_PINCODE=" + Url.Encode(model.LB_PINCODE_DNK_Itm) +
                                    "&LB_OWNERSHIP=" + Url.Encode(model.LB_OWNERSHIP_DNK_Itm) +
                                    "&LB_OWNERSHIP_PARTY_ID=" + Url.Encode(model.LB_OWNERSHIP_PARTY_ID_DNK_Itm) +
                                    "&LB_SURVEY_NO=" + Url.Encode(model.LB_SURVEY_NO_DNK_Itm) +
                                    "&LB_CON_YEAR=" + Url.Encode(model.LB_CON_YEAR_DNK_Itm) +
                                    "&LB_RCC_ROOF=" + Url.Encode(model.LB_RCC_ROOF_DNK_Itm) +
                                    "&LB_PAID_DATE=" + Url.Encode(model.LB_PAID_DATE_DNK_Itm) +
                                    "&LB_PERIOD_FROM=" + Url.Encode(model.LB_PERIOD_FROM_DNK_Itm) +
                                    "&LB_PERIOD_TO=" + Url.Encode(model.LB_PERIOD_TO_DNK_Itm) +
                                    "&LB_DOC_OTHERS=" + Url.Encode(model.LB_DOC_OTHERS_DNK_Itm) +
                                    "&LB_DOC_NAME=" + Url.Encode(model.LB_DOC_NAME_DNK_Itm) +
                                    "&LB_OTHER_DETAIL=" + Url.Encode(model.LB_OTHER_DETAIL_DNK_Itm) +
                                    "&LB_TOT_P_AREA=" + model.LB_TOT_P_AREA_DNK_Itm +
                                    "&LB_CON_AREA=" + model.LB_CON_AREA_DNK_Itm +
                                    "&LB_DEPOSIT_AMT=" + model.LB_DEPOSIT_AMT_DNK_Itm +
                                    "&LB_MONTH_RENT=" + model.LB_MONTH_RENT_DNK_Itm +
                                    "&LB_MONTH_O_PAYMENTS=" + model.LB_MONTH_O_PAYMENTS_DNK_Itm +
                                    "&LB_REC_ID=" + Url.Encode(model.LB_REC_ID_DNK_Itm) +
                                    "&xID=" + Url.Encode(model.LB_REC_ID_DNK_Itm);
                                if (model.List_LB_DOCS_ARRAY_DNK_Itm == null)
                                {
                                    FetchLBDocuments(ref model);
                                    model.List_LB_DOCS_ARRAY_DNK_Itm = new JavaScriptSerializer().Serialize(model.LB_DOCS_ARRAY_DNK_Itm);
                                }
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&List_LB_DOCS_ARRAY=" + Url.Encode(model.List_LB_DOCS_ARRAY_DNK_Itm);
                                if (model.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm == null)
                                {
                                    FetchExtensionData(ref model);
                                    model.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm = new JavaScriptSerializer().Serialize(model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm);
                                }
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&List_LB_EXTENDED_PROPERTY_TABLE=" + Url.Encode(model.List_LB_EXTENDED_PROPERTY_TABLE_DNK_Itm);
                            }
                        }
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
        public void FetchLBDocuments(ref DonationInKind_Item_Detail model)
        {
            model.LB_DOCS_ARRAY_DNK_Itm = new DataTable();
            model.LB_DOCS_ARRAY_DNK_Itm.Columns.Add("LB_MISC_ID", Type.GetType("System.String"));
            model.LB_DOCS_ARRAY_DNK_Itm.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

            var LB_DOC = new Common_Lib.Get_Data(BASE, "SYS", "LAND_BUILDING_EXTENDED_INFO", "SELECT LB_MISC_ID FROM LAND_BUILDING_DOCUMENTS_INFO where LB_REC_ID ='" + model.LB_REC_ID_DNK_Itm + "'");
            foreach (DataRow XROW in LB_DOC._dc_DataTable.Rows)
            {
                DataRow Row = model.LB_DOCS_ARRAY_DNK_Itm.NewRow();
                Row["LB_MISC_ID"] = XROW["LB_MISC_ID"];
                Row["LB_REC_ID"] = model.LB_REC_ID_DNK_Itm;
                model.LB_DOCS_ARRAY_DNK_Itm.Rows.Add(Row);
            }
        }
        public void FetchExtensionData(ref DonationInKind_Item_Detail model)
        {
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm = new DataTable();

            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_SR_NO", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_INS_ID", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_CON_AREA", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_CON_YEAR", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_MOU_DATE", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_VALUE", Type.GetType("System.Double"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"));
            model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Columns.Add("LB_REC_ID", Type.GetType("System.String"));

            var LB_Ext = new Common_Lib.Get_Data(BASE, "SYS", "LAND_BUILDING_EXTENDED_INFO", "SELECT LB_SR_NO, LB_INS_ID, LB_TOT_P_AREA, LB_CON_AREA,LB_CON_YEAR, LB_MOU_DATE, LB_VALUE, LB_OTHER_DETAIL FROM LAND_BUILDING_EXTENDED_INFO where LB_REC_ID ='" + model.LB_REC_ID_DNK_Itm + "'");

            foreach (DataRow XROW in LB_Ext._dc_DataTable.Rows)
            {
                DataRow Row = model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.NewRow();
                Row["LB_MOU_DATE"] = XROW["LB_MOU_DATE"];
                Row["LB_SR_NO"] = XROW["LB_SR_NO"];
                Row["LB_INS_ID"] = XROW["LB_INS_ID"];
                Row["LB_TOT_P_AREA"] = XROW["LB_TOT_P_AREA"];
                Row["LB_CON_AREA"] = XROW["LB_CON_AREA"];
                Row["LB_CON_YEAR"] = XROW["LB_CON_YEAR"];
                Row["LB_VALUE"] = XROW["LB_VALUE"];
                Row["LB_OTHER_DETAIL"] = XROW["LB_OTHER_DETAIL"];
                Row["LB_REC_ID"] = model.LB_REC_ID_DNK_Itm;
                model.LB_EXTENDED_PROPERTY_TABLE_DNK_Itm.Rows.Add(Row);
            }
        }
        public ActionResult LookUp_GetItemList_Itm_Dtel(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetItemList_Itm_Dtel == null || DDRefresh == true)
            {
                RefreshItemList_Itm_Dtel();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetItemList_Itm_Dtel, loadOptions)), "application/json");
        }
        public void RefreshItemList_Itm_Dtel()
        {
            var d1 = BASE._Gift_DBOps.GetItemLedger();
            DataView dview = new DataView((DataTable)d1);
            dview.Sort = "ITEM_NAME";
            GetItemList_Itm_Dtel = DatatableToModel.DataTableto_ItemList_Itm_Dtel(dview.ToTable());
        }
        public ActionResult LookUp_GetPurList_Itm_Dtel(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetPurList_Itm_Dtel == null || DDRefresh == true)
            {
                RefreshPurList_Itm_Dtel();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetPurList_Itm_Dtel, loadOptions)), "application/json");
        }
        public void RefreshPurList_Itm_Dtel()
        {
            DataTable d1 = BASE._Gift_DBOps.GetProjects("PUR_NAME", "PUR_ID");
            DataView dview = new DataView(d1);
            GetPurList_Itm_Dtel = DatatableToModel.DataTableto_PurList_Itm_Dtel(dview.ToTable());
        }
        #endregion
        public ActionResult LookUp_GetPartyList_DNK(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetPartyList_DNK == null || DDRefresh == true)
            {
                RefreshPartyList_DNK();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetPartyList_DNK, loadOptions)), "application/json");
        }
        public void RefreshPartyList_DNK()
        {
            DataTable d1 = BASE._Gift_DBOps.GetParties();
            DataView dview = new DataView(d1);
            dview.Sort = "C_NAME";
            GetPartyList_DNK = DatatableToModel.DataTableto_GetPartyList_DNK(dview.ToTable());
        }

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
            ClearBaseSession("_DNK");
            BASE._SessionDictionary.Remove("Payment_Asset_Image_Payment");
        }
        #endregion
    }
}