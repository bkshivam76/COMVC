using Common_Lib;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Office.Utils;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib.RealTimeService;


namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class SaleAssetController : BaseController
    {

        #region "Start--> Default Variables"

        public List<SaleTransferItemList> SaleA_Item_Data
        {
            get { return (List<SaleTransferItemList>)GetBaseSession("SaleA_Item_Data_SaleAsset"); }
            set { SetBaseSession("SaleA_Item_Data_SaleAsset", value); }
        }
        public List<SaleAssetPartyList> SaleA_GetPartyList_Data
        {
            get { return (List<SaleAssetPartyList>)GetBaseSession("SaleA_GetPartyList_Data_SaleAsset"); }
            set { SetBaseSession("SaleA_GetPartyList_Data_SaleAsset", value); }
        }
        public List<ReferenceBankList> SaleA_RefBankList_Data
        {
            get { return (List<ReferenceBankList>)GetBaseSession("SaleA_RefBankList_Data_SaleAsset"); }
            set { SetBaseSession("SaleA_RefBankList_Data_SaleAsset", value); }
        }
        public List<SaleAssetBankNameList> SaleA_GetBankList_Data
        {
            get { return (List<SaleAssetBankNameList>)GetBaseSession("SaleA_GetBankList_Data_SaleAsset"); }
            set { SetBaseSession("SaleA_GetBankList_Data_SaleAsset", value); }
        }
        public List<SaleAsset_AssetList> SaleA_AssetList_Data
        {
            get { return (List<SaleAsset_AssetList>)GetBaseSession("SaleA_AssetList_Data_SaleAsset"); }
            set { SetBaseSession("SaleA_AssetList_Data_SaleAsset", value); }
        }
        public List<SaleAssetPurposeList> SaleA_PurposeList_Data
        {
            get { return (List<SaleAssetPurposeList>)GetBaseSession("SaleA_PurposeList_Data_SaleAsset"); }
            set { SetBaseSession("SaleA_PurposeList_Data_SaleAsset", value); }
        }

        #endregion
        // GET: Account/SaleAsset
        [HttpGet]
        public ActionResult Frm_Voucher_Win_Sale_Asset(string Tag = null,string iSpecific_ItemID = "",  string xID = "",string xMID = "" , string Info_LastEditedOn = "", string Info_MaxEditedOn = "",string GridToRefresh = "CashBookListGrid")
        {
            try
            {
                var i = 0;
                string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
                string[] AM = { "_New", "_New_From_Selection", "_Edit", "_View", "_Delete" };                
                for (i = 0; i < Rights.Length; i++)
                {
                    if (CheckRights(BASE, ClientScreen.Accounts_Voucher_SaleOfAsset, Rights[i]) == false && Tag == AM[i])
                    {
                        return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
                    }
                }
                ViewBag.GridToRefresh = GridToRefresh;
                ViewData["SaleAset_AddFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "Add");
                ViewData["SaleAset_ListFacilityAddress"] = CheckRights(BASE, ClientScreen.Facility_AddressBook, "List");

                DateTime? dtnull = null;
                SaleAssetModel model = new SaleAssetModel();
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);               
                model.ActionMethod = Tag;
                model.iSpecific_ItemID = iSpecific_ItemID ?? string.Empty;
                model.xMID = xMID;
                model.xID = xID;
                model.Cmd_Mode_SaleA = "CASH";
                LookUp_GetItemList_Refresh();
                LookUp_GetBankList_Refresh();
                LookUp_GetRefBankList_Refresh();
                LookUp_GetPartyList_Refresh();
                LookUp_GetPurposeList_Refresh();

                //Special Voucher References (FCRA Related) Code
                model.SpecialReferenceList_Data_SaleA = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
                model.splVchrRefsCount_SaleA = model.SpecialReferenceList_Data_SaleA.Count();

                if (model.ActionMethod == "_Edit" || model.ActionMethod == "_View" || model.ActionMethod == "_Delete")
                {
                    
                    model.Info_LastEditedOn = Info_LastEditedOn == "" ? dtnull : Convert.ToDateTime(Info_LastEditedOn);
                    model.Info_MaxEditedOn = Info_MaxEditedOn == "" ? dtnull : Convert.ToDateTime(Info_MaxEditedOn);
                    model.xMID = BASE._SaleOfAsset_DBOps.GetMasterID(model.xID).ToString();
                    DataTable d1 = BASE._SaleOfAsset_DBOps.GetMasterRecord(model.xMID.ToString());
                    if (d1 == null)
                    {
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                    }
                    DataTable d2 = BASE._SaleOfAsset_DBOps.GetPurposeRecord(model.xMID);
                    DataTable d3 = BASE._SaleOfAsset_DBOps.GetRecord(model.xMID);
                    if ((d2 == null) || (d3 == null))
                    {
                        return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!');</script>");
                    }
                    DateTime? xDate = null;
                    xDate = Convert.ToDateTime(d3.Rows[0]["TR_DATE"]);
                    model.Txt_V_Date_SaleA = xDate;

                    //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                    var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);                    
                    if (SpecialReference_Data.Rows.Count > 0)
                    {
                        model.SpecialReference_Get_SelectedValue_SaleA = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                    }

                    // -----------------------------+
                    // Start : Check if entry already changed 
                    // -----------------------------+
                    if (BASE.AllowMultiuser())
                    {
                        if ((model.Tag == Common.Navigation_Mode._Edit) || (model.Tag == Common.Navigation_Mode._Delete)
                                    || (model.Tag == Common.Navigation_Mode._View))
                        {
                            string viewstr = "";
                            if (model.Tag == Common.Navigation_Mode._View)
                            {
                                viewstr = "view";
                            }
                            if (model.Info_LastEditedOn != Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]))
                            {
                                string message = Messages.RecordChanged("Current Sale", viewstr);
                                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                            }
                        }
                    }

                    // -----------------------------+
                    // End : Check if entry already changed 
                    // -----------------------------+

                    Data_Binding(d1, d2, d3, ref model);
                }
                if (model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection) //By Item-wise Selection
                {
                    model.GLookUp_ItemList_SaleA = model.iSpecific_ItemID;
                }
                if (!(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper()) &&
                    (!(BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper())))
                {
                    ViewBag.GLookUp_AssetList_SaleA_Columnhide = true;
                }
                model.SaleProfit_ItemID = "F298C1EF-29F4-4B52-BFCD-EC120E9404F9";
                model.SaleLoss_ItemID = "ADA231AD-390F-4665-B866-ACA0DAA43055";
                model.SaleProfit_Loss_LedID = "00060";
                return View(model);
            }

            catch(Exception ex)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + ex.Message + "','Exception Raised');</script>");
            }
        }

        public void Data_Binding(DataTable d1, DataTable d2, DataTable d3, ref SaleAssetModel model)
        {
            model.LastEditedOn = Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]);
            model.Txt_V_NO_SaleA = d3.Rows[0]["TR_VNO"].ToString().HandleEscapeCharacters();
            model.Cmd_Mode_SaleA = d3.Rows[0]["TR_MODE"].ToString();
            
            if (!Convert.IsDBNull(d3.Rows[0]["TR_ITEM_ID"]))
            {
                if (d3.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                {
                    model.GLookUp_ItemList_SaleA = d3.Rows[0]["TR_ITEM_ID"].ToString();
                }
                var ItemID = model.GLookUp_ItemList_SaleA;                
                model.iTrans_Type = SaleA_Item_Data.Where(x => x.ITEM_ID == ItemID).First().ITEM_TRANS_TYPE;
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_AB_ID_1"]))
            {
                if (d3.Rows[0]["TR_AB_ID_1"].ToString().Length > 0)
                {
                    model.GLookUp_PartyList1_SaleA = d3.Rows[0]["TR_AB_ID_1"].ToString();
                }
            }
            model.Cmb_Asset_Type_SaleA = d1.Rows[0]["TR_SALE_TYPE"].ToString().Trim();
            Get_Asset_Items(model.Cmb_Asset_Type_SaleA.ToUpper(), model.ActionMethod, model.xMID);
            model.Txt_SDate_SaleA = Convert.ToDateTime(d1.Rows[0]["TR_SALE_DATE"]);
            model.Txt_SaleAmt_SaleA = Convert.ToDouble(d1.Rows[0]["TR_SALE_AMT"]);
            model.Txt_Amount_SaleA = Convert.ToDouble(d1.Rows[0]["TR_SUB_AMT"]);
            if (!Convert.IsDBNull(d3.Rows[0]["TR_TRF_CROSS_REF_ID"]))
            {
                if (d3.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString().Length > 0)
                {
                    model.GLookUp_AssetList_SaleA = d3.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString();
                }
            }
            model.Txt_Qty_SaleA = Convert.ToDouble(d1.Rows[0]["TR_SALE_QTY"]);
            string Bank_ID = "";
            if (!string.IsNullOrEmpty(model.iTrans_Type) && (model.iTrans_Type.ToUpper() == "DEBIT"))
            {
                if (!Convert.IsDBNull(d3.Rows[0]["TR_SUB_CR_LED_ID"]) && ((Convert.IsDBNull(d3.Rows[0]["TR_CR_LED_ID"]) ? "" : d3.Rows[0]["TR_CR_LED_ID"].ToString()) == "00079"))
                {
                    Bank_ID = d3.Rows[0]["TR_SUB_CR_LED_ID"].ToString();
                }
            }
            else
            {
                if (!Convert.IsDBNull(d3.Rows[0]["TR_SUB_DR_LED_ID"]) && ((Convert.IsDBNull(d3.Rows[0]["TR_DR_LED_ID"]) ? "" : d3.Rows[0]["TR_DR_LED_ID"].ToString()) == "00079"))
                {
                    Bank_ID = d3.Rows[0]["TR_SUB_DR_LED_ID"].ToString();
                }
            }
            if (Bank_ID.Length > 0)
            {
                model.GLookUp_BankList_SaleA = Bank_ID;
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_REF_BANK_ID"]))
            {
                if ((d3.Rows[0]["TR_REF_BANK_ID"].ToString().Length > 0))
                {
                    model.GLookUp_RefBankList_SaleA = d3.Rows[0]["TR_REF_BANK_ID"].ToString();
                }
            }
            model.Txt_Ref_Branch_SaleA = d3.Rows[0]["TR_REF_BRANCH"].ToString().HandleEscapeCharacters();
            model.Txt_Ref_No_SaleA = d3.Rows[0]["TR_REF_NO"].ToString().HandleEscapeCharacters();
            if (!Convert.IsDBNull(d3.Rows[0]["TR_REF_DATE"]))
            {
                model.Txt_Ref_Date_SaleA = Convert.ToDateTime(d3.Rows[0]["TR_REF_DATE"]);
            }
            if (!Convert.IsDBNull(d3.Rows[0]["TR_REF_CDATE"]))
            {
                model.Txt_Ref_CDate_SaleA = Convert.ToDateTime(d3.Rows[0]["TR_REF_CDATE"]);
            }
            if (!Convert.IsDBNull(d2.Rows[0]["TR_PURPOSE_MISC_ID"]))
            {
                if (d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().Length > 0)
                {
                    model.GLookUp_PurList_SaleA = d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().HandleEscapeCharacters();
                }
            }
            model.Txt_Narration_SaleA = d3.Rows[0]["TR_NARRATION"].ToString().HandleEscapeCharacters();
            model.Txt_Remarks_SaleA = d3.Rows[0]["TR_REMARKS"].ToString().HandleEscapeCharacters();
            model.Txt_Reference_SaleA = d3.Rows[0]["TR_REFERENCE"].ToString().HandleEscapeCharacters();
            // RAD_Receipt.DataBindings.Add("SelectedIndex", d1, "TR_RECEIPT_TYPE")
        }

        [HttpPost]
        public ActionResult Frm_Voucher_Win_Sale_Asset(SaleAssetModel model)
        {
            var Me_Text = model.ActionMethod.Substring(1) + " Sale of Asset";
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);

                if ((model.Tag == Common.Navigation_Mode._New) || (model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                            || (model.Tag == Common_Lib.Common.Navigation_Mode._Edit))
                {
                    if (model.Cmb_Asset_Type_SaleA != null)
                    {
                        if ((model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS") || (model.Cmb_Asset_Type_SaleA.ToUpper() == "LAND & BUILDING"))
                        {
                            if (BASE.IsInsuranceAudited())
                            {
                                jsonParam.message = "Sorry! Changes cannot be done after the completion of Insurance Audit";
                                jsonParam.title = "Information";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                // -----------------------------+
                // Start : Check if entry already changed 
                // -----------------------------+
                if (BASE.AllowMultiuser())
                {
                    if (!string.IsNullOrEmpty(model.GLookUp_BankList_SaleA) && (model.GLookUp_BankList_SaleA != ""))
                    {
                        // 'BUG #5004 FIXED
                        // Closed Bank Acc Check #G42
                        string AccNo = BASE._Voucher_DBOps.GetBankAccount(model.GLookUp_BankList_SaleA, string.Empty).ToString();
                        if (AccNo == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                        if (Convert.IsDBNull(AccNo))
                        {
                            AccNo = string.Empty;
                        }

                        if (AccNo.Length > 0)
                        {
                            jsonParam.message = "Entry cannot be Added/Edited/Deleted...!<br>In this entry, Used Bank A / c No.: " + AccNo + " was closed...!!!";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;                            
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if ((model.Tag == Common_Lib.Common.Navigation_Mode._Edit) || (model.Tag == Common_Lib.Common.Navigation_Mode._Delete))
                    {
                        DataTable saleofasset_DbOps = BASE._SaleOfAsset_DBOps.GetRecord(model.xMID);
                        if (saleofasset_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (saleofasset_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Sale");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(saleofasset_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Common_Lib.Messages.RecordChanged("Current Sale");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        object MaxValue = 0;
                        MaxValue = BASE._SaleOfAsset_DBOps.GetStatus(model.xID);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Common_Lib.Common.Record_Status)MaxValue == Common_Lib.Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited/Deleted...!<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,If you really want to do some action...!";
                            jsonParam.title = "Information";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // Special Checks
                        string xTemp_AdvID = BASE._Voucher_DBOps.GetRaisedAdvanceRecID(model.xMID);// Get advance created by current Txn
                        if (xTemp_AdvID.Length > 0)
                        {
                            //DataTable xTemp_RefRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AdvID);// Advance is referred in a transaction                            
                            DataTable xTemp_RefRecord = BASE._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID);// Advance is referred in a transaction                            
                            if (!(xTemp_RefRecord == null))
                            {
                                if (xTemp_RefRecord.Rows.Count > 0)
                                {
                                    DataRow row = xTemp_RefRecord.Rows[0];
                                    jsonParam.message = "Sorry ! Some Adjustments / Refunds were made on " + Convert.ToDateTime(row["TR_DATE"]).ToLongDateString() + " of Rs." + row["TR_AMOUNT"].ToString() + " for the Sale amount Receivable raised by current entry." + "<br>" + "<br>" + " Please delete the record for deleting this Entry.";
                                    jsonParam.title = "Error!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                // -----------------------------+
                // End : Check if entry already changed 
                // -----------------------------+
                
                if ((model.Tag == Common_Lib.Common.Navigation_Mode._New) || (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)
                                || (model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection))
                {
                    if ((string.IsNullOrEmpty(model.GLookUp_ItemList_SaleA) || model.GLookUp_ItemList_SaleA.Trim().Length == 0))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_ItemList_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.Txt_V_Date_SaleA == null || IsDate(model.Txt_V_Date_SaleA.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_V_Date_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if(Convert.ToDateTime(model.Txt_V_Date_SaleA) != null)
                    {
                        // 1                   
                        if (Convert.ToDateTime(model.Txt_V_Date_SaleA) < Convert.ToDateTime(BASE._open_Year_Sdt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_V_Date_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // 2 
                        if (Convert.ToDateTime(model.Txt_V_Date_SaleA) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_V_Date_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    if (model.iParty_Req.ToString().Trim().ToUpper() == "YES")
                    {
                        if ((model.GLookUp_PartyList1_SaleA == null)||(model.GLookUp_PartyList1_SaleA.Trim().Length == 0))
                        {
                            jsonParam.message = "Party Not Selected...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "GLookUp_PartyList1_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((string.IsNullOrEmpty(model.Cmb_Asset_Type_SaleA)) || model.Cmb_Asset_Type_SaleA.Trim().Length == 0 || model.Cmb_Asset_Type_SaleA == "")
                    {
                        jsonParam.message = "Asset Type Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Cmb_Asset_Type_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_AssetList_SaleA) || (model.GLookUp_AssetList_SaleA.ToString().Trim().Length == 0))
                    {
                        jsonParam.message = "Asset Item Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_AssetList_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_SDate_SaleA == null || IsDate(model.Txt_SDate_SaleA.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_SDate_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if(model.Txt_SDate_SaleA != null)
                    {
                        // 1 
                        if (Convert.ToDateTime(model.Txt_SDate_SaleA) < Convert.ToDateTime(BASE._open_Year_Sdt))
                        {                               
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_SDate_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // 2
                        if (Convert.ToDateTime(model.Txt_SDate_SaleA) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.title = "Incomplete Information...";
                            jsonParam.focusid = "Txt_SDate_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // 3                        
                        if (Convert.ToDateTime(model.Txt_SDate_SaleA) > Convert.ToDateTime(model.Txt_V_Date_SaleA))
                        {
                            jsonParam.message = "Sale Date cannot be greater than Voucher Date...!";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_SDate_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // 4
                        DateTime ReferenceCreationDate = Convert.ToDateTime(model.REF_CREATION_or_Purchase_Date) == null ? Convert.ToDateTime(BASE._open_Year_Sdt) : Convert.ToDateTime(model.REF_CREATION_or_Purchase_Date);
                        if (Convert.ToDateTime(model.Txt_SDate_SaleA) < ReferenceCreationDate)
                        {
                            jsonParam.message = "Asset Sale Date cannot be less than Purchase Date...!<br><br>Please Update Voucher Date to Change Sale Date.";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_SDate_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (model.Txt_Qty_SaleA==null || model.Txt_Qty_SaleA <= 0)
                    {
                        jsonParam.message = "Qty. cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Qty_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Qty_SaleA > model.Txt_CurQty_SaleA)
                    {
                        jsonParam.message = "Qty. cannot be greater than " + model.Txt_CurQty_SaleA + "...!";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Qty_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_SaleAmt_SaleA == null || model.Txt_SaleAmt_SaleA <= 0)
                    {
                        jsonParam.message = "Amount cannot be Zero/Negative...!";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_SaleAmt_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    // check property references , as location, before selling 
                    if (model.Cmb_Asset_Type_SaleA.ToUpper() == "LAND & BUILDING")
                    {
                        string Message = FindLocationUsage(model.GLookUp_AssetList_SaleA, true);  // excludes sold/tf assets

                        if (Message.Length > 0)
                        {
                            jsonParam.message = Message;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "GLookUp_AssetList_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Txt_Amount_SaleA > model.Txt_SaleAmt_SaleA)
                    {
                        jsonParam.message = "Amount cannot be greater than " + model.Txt_SaleAmt_SaleA + "...!";
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Amount_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((string.IsNullOrEmpty(model.GLookUp_BankList_SaleA) || model.GLookUp_BankList_SaleA.Trim().Length == 0) && (model.Cmd_Mode_SaleA.ToString().ToUpper() != "CASH"))
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_BankList_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(model.GLookUp_BankList_SaleA) || model.GLookUp_BankList_SaleA.ToString().Trim().Length == 0)
                    {
                        model.GLookUp_BankList_SaleA = string.Empty;
                    }
                    if ((string.IsNullOrEmpty(model.GLookUp_RefBankList_SaleA) || model.GLookUp_RefBankList_SaleA.Trim().Length == 0) && ((model.Cmd_Mode_SaleA.ToString().ToUpper() != "CASH") && (model.Cmd_Mode_SaleA.ToString().ToUpper() != "BANK ACCOUNT")))
                    {
                        jsonParam.message = "Bank Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_RefBankList_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((string.IsNullOrEmpty(model.GLookUp_RefBankList_SaleA)) || (model.GLookUp_RefBankList_SaleA.ToString().Trim().Length == 0))
                    {
                        model.GLookUp_RefBankList_SaleA = string.Empty;
                    }
                    if ((string.IsNullOrEmpty(model.Txt_Ref_Branch_SaleA)) && ((model.Cmd_Mode_SaleA.ToString().ToUpper() != "CASH") && (model.Cmd_Mode_SaleA.ToString().ToUpper() != "BANK ACCOUNT")))
                    {
                        jsonParam.message = "Bank Branch Not Specified...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_Ref_Branch_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((string.IsNullOrEmpty(model.Txt_Ref_No_SaleA)) && ((model.Cmd_Mode_SaleA.ToString().ToUpper() != "CASH") && (model.Cmd_Mode_SaleA.ToString().ToUpper() != "BANK ACCOUNT")))
                    {
                        jsonParam.message = "No. Not Specified...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_Ref_No_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((model.Txt_Ref_Date_SaleA == null || IsDate(model.Txt_Ref_Date_SaleA.ToString()) == false) && ((model.Cmd_Mode_SaleA.ToString().ToUpper() != "CASH") && (model.Cmd_Mode_SaleA.ToString().ToUpper() != "BANK ACCOUNT")))
                    {
                        jsonParam.message = "Date Incorrect/Blank...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "Txt_Ref_Date_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Ref_CDate_SaleA != null)
                    {
                        // 1
                        if (Convert.ToDateTime(model.Txt_Ref_CDate_SaleA) < Convert.ToDateTime(model.Txt_Ref_Date_SaleA))
                        {
                            jsonParam.message = "Clearing Date Cannot be less than Reference Date!!";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_Ref_CDate_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if ((string.IsNullOrEmpty(model.GLookUp_PurList_SaleA)) || (model.GLookUp_PurList_SaleA.ToString().Trim().Length == 0))
                    {
                        jsonParam.message = "Purpose Not Selected...!";
                        jsonParam.title = "Incomplete Information...";
                        jsonParam.focusid = "GLookUp_PurList_SaleA";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if ((model.Tag == Common_Lib.Common.Navigation_Mode._New)
                            || (model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                            || (model.Tag == Common_Lib.Common.Navigation_Mode._Edit))
                    {
                        Param_GetAssetMaxTxnDate inparam = new Param_GetAssetMaxTxnDate();
                        inparam.Creation_Date = Convert.ToDateTime(model.REF_CREATION_or_Purchase_Date==null ? BASE._open_Year_Sdt : model.REF_CREATION_or_Purchase_Date);//redmine bug 133020 fix 
                        inparam.Asset_RecID = model.GLookUp_AssetList_SaleA;
                        inparam.YearID = BASE._open_Year_ID;
                        inparam.Tr_M_ID = model.xMID;
                        DateTime MxDate = Convert.ToDateTime(BASE._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam));
                        if (MxDate == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((Convert.ToDateTime(model.Txt_V_Date_SaleA)) < MxDate)
                        {
                            jsonParam.message = "Voucher Date cannot be less than previous transaction on same asset dated " + MxDate.ToLongDateString() + "  . . . !";
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_V_Date_SaleA";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                // -------------------------// Start Dependencies // ------------------------
                if (BASE.AllowMultiuser())
                {
                    if ((model.Tag == Common_Lib.Common.Navigation_Mode._Delete) || (model.Tag == Common_Lib.Common.Navigation_Mode._Edit))
                    {
                        if (model.Info_MaxEditedOn!=null)
                        {
                            if ((model.Info_MaxEditedOn != DateTime.MinValue))// Record has been opened on basis of this being a last edited record for referred asset
                            {
                                object Lastest_MaxEdit_On = BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(model.GLookUp_AssetList_SaleA);
                                if (IsDate(Lastest_MaxEdit_On.ToString()))//Redmine Bug #133191 fixed
                                {
                                    if (Convert.ToDateTime(Lastest_MaxEdit_On) > model.Info_MaxEditedOn)
                                    {
                                        jsonParam.message = Messages.CustomChanges("Sorry ! Current Voucher is no Longer Latest Edited Voucher referring the Current Asset. Another Record has been Added/Edited in background which refers the same Asset.", "Last Edited Reference Entry");
                                        jsonParam.title = "Referred Record Already Changed!!";
                                        jsonParam.result = false;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                    }
                    if ((model.Tag == Common_Lib.Common.Navigation_Mode._New) || (model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection)
                                || (model.Tag == Common_Lib.Common.Navigation_Mode._Edit))
                    {
                        DateTime OldEditOn;
                        DateTime NewEditOn;
                        if (!string.IsNullOrEmpty(model.GLookUp_BankList_SaleA) && model.GLookUp_BankList_SaleA.ToString().Length > 0)
                        {
                            DataTable d1 = BASE._SaleOfAsset_DBOps.GetBankAccounts(model.GLookUp_BankList_SaleA);
                            if (d1 == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            OldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Bank);
                            if (d1.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Bank Account");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                NewEditOn = Convert.ToDateTime(d1.Rows[0]["REC_EDIT_ON"]);
                                if ((NewEditOn != OldEditOn))
                                {
                                    jsonParam.message = Messages.DependencyChanged("Bank Account");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if ((model.GLookUp_PartyList1_SaleA.ToString().Length > 0))
                        {
                            DataTable add_book = BASE._SaleOfAsset_DBOps.GetParties(model.GLookUp_PartyList1_SaleA);
                            if (add_book == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            OldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_Party);
                            if (add_book.Rows.Count <= 0)
                            {
                                jsonParam.message = Messages.DependencyChanged("Address Book");
                                jsonParam.title = "Referred Record Already Deleted!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                NewEditOn = Convert.ToDateTime(add_book.Rows[0]["REC_EDIT_ON"]);
                                if ((NewEditOn != OldEditOn))
                                {
                                    jsonParam.message = Messages.DependencyChanged("Address Book");
                                    jsonParam.title = "Referred Record Already Changed!!";
                                    jsonParam.result = false;
                                    jsonParam.closeform = true;
                                    jsonParam.refreshgrid = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        // Code to Check Dependencies of Sale Qty for multi user 
                        int cnt2;
                        Common_Lib.RealTimeService.Param_GetAssetListingForSale AssetParam = new Common_Lib.RealTimeService.Param_GetAssetListingForSale();
                        AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                        AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                        if (model.Cmb_Asset_Type_SaleA.ToUpper() == "GOLD")   // Gold
                        {
                            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD;
                        }
                        else if (model.Cmb_Asset_Type_SaleA.ToUpper() == "SILVER")    // Silver
                        {
                            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.SILVER;
                        }
                        else if (model.Cmb_Asset_Type_SaleA.ToUpper() == "VEHICLES")  // VEHICLES
                        {
                            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.VEHICLES;
                        }
                        else if (model.Cmb_Asset_Type_SaleA.ToUpper() == "LIVESTOCK")     // LIVESTOCK
                        {
                            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK;
                        }
                        else if ((model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS"))     // OTHER ASSET
                        {
                            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS;
                        }
                        else if ((model.Cmb_Asset_Type_SaleA.ToUpper() == "LAND & BUILDING"))     // LAND & BUILDING
                        {
                            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING;
                        }
                        if ((model.Tag == Common_Lib.Common.Navigation_Mode._Edit) || (model.Tag == Common_Lib.Common.Navigation_Mode._Delete))
                        {
                            AssetParam.TR_M_ID = model.xMID;
                        }
                        AssetParam.Asset_RecID = model.GLookUp_AssetList_SaleA;
                        DataTable ASSET_TABLE = BASE._SaleOfAsset_DBOps.GetAllProfile_AssetList(AssetParam);    // Fetch asset data as per selection
                        if (ASSET_TABLE == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        cnt2 = ASSET_TABLE.Rows.Count;
                        OldEditOn = Convert.ToDateTime(model.REC_EDIT_ON_AssetName);                        
                        if (cnt2 <= 0)      //  if the user - selected asset is not qualified for sale anymore ,as the same has been changed by other user
                        {
                            jsonParam.message = Messages.DependencyChanged("Profile Assets");
                            jsonParam.title = "Referred Record Already Deleted / Not Available for sale/transfer any more!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            NewEditOn = Convert.ToDateTime(ASSET_TABLE.Rows[0]["REC_EDIT_ON"]);
                            if (OldEditOn != NewEditOn)     // A/E,E/E
                            {
                                jsonParam.message = Messages.DependencyChanged("Profile Assets");
                                jsonParam.title = "Referred Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.closeform = true;
                                jsonParam.refreshgrid = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (double.Parse(ASSET_TABLE.Rows[0]["REF_QTY"].ToString()) < double.Parse(model.Txt_Qty_SaleA.ToString()))     //  if the weight/qty remaining is less then the weight/qty demanded for sale 
                        {
                            jsonParam.message = Messages.DependencyChanged("Asset Value");
                            jsonParam.title = "weight/qty remaining is less then the weight/qty demanded for sale !!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (double.Parse(ASSET_TABLE.Rows[0]["REF_AMT"].ToString()) != Convert.ToDouble(model.REF_AMT))
                        {
                            jsonParam.message = Messages.DependencyChanged("Asset Value");
                            jsonParam.title = "Value Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                // --------------------------------// End Dependencies //------------------------------------------------

                if (string.IsNullOrWhiteSpace(model.Cmd_Mode_SaleA) == true)
                {
                    // Details: This if block is added in web only.
                    jsonParam.message = "Payment Mode is required...!";
                    jsonParam.title = "Incomplete Information...";
                    jsonParam.focusid = "Cmd_Mode_SaleA";
                    jsonParam.result = false;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                if (model.Cmd_Mode_SaleA.ToString().ToUpper() == "CASH")
                {
                    model.GLookUp_BankList_SaleA = string.Empty;
                    model.GLookUp_RefBankList_SaleA = string.Empty;
                    model.Txt_Ref_Branch_SaleA = string.Empty;
                    model.Txt_Ref_No_SaleA = string.Empty;
                    model.Txt_Ref_Date_SaleA = null;
                    model.Txt_Ref_CDate_SaleA = null;
                }
                string Status_Action = string.Empty;
                if (model.ActionMethod == "_Delete")
                {
                    Status_Action = ((int)Common_Lib.Common.Record_Status._Deleted).ToString();
                }
                else
                {
                    Status_Action = ((int)Common_Lib.Common.Record_Status._Completed).ToString();
                }
                // +----JV LEDGER DETAIL----+
                string JV_TRANS_TYPE = string.Empty;
                string JV_Cr_Led_id = string.Empty;
                string JV_Dr_Led_id = string.Empty;
                if ((double.Parse(model.Txt_Diff_SaleA.ToString()) > 0))
                {
                    if ((model.iOffSet_ID.ToString().Length > 0))
                    {
                        DataTable JV_DT = BASE._SaleOfAsset_DBOps.GetItemsListByID(model.iOffSet_ID) as DataTable;
                        if (JV_DT == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (JV_DT.Rows.Count > 0)
                        {
                            JV_TRANS_TYPE = JV_DT.Rows[0]["Item_Trans_Type"].ToString();
                            if (JV_DT.Rows[0]["Item_Trans_Type"].ToString().ToUpper() == "DEBIT")
                            {
                                JV_Dr_Led_id = JV_DT.Rows[0]["ITEM_LED_ID"].ToString();
                            }
                            else
                            {
                                JV_Cr_Led_id = JV_DT.Rows[0]["ITEM_LED_ID"].ToString();
                            }
                        }
                        else
                        {
                            jsonParam.message = "Sale Amount Receivable Item Not Found..!";
                            jsonParam.title = Me_Text;
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        jsonParam.message = "Sale Amount Receivable Item Not Define..!";
                        jsonParam.title = Me_Text;
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                // +----------END-----------+
                string Dr_Led_id = string.Empty;
                string Cr_Led_id = string.Empty;
                string Sub_Dr_Led_ID = string.Empty;
                string Sub_Cr_Led_ID = string.Empty;
                if (model.iTrans_Type.ToUpper() == "DEBIT")
                {
                    if (model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS")     // OTHER ASSET
                    {
                        if (model.iAsset_Type == "ASSET")
                        {
                            Dr_Led_id = model.iLed_ID;
                        }
                        else
                        {
                            Dr_Led_id = model.iCon_Led_ID;
                        }
                    }
                    else
                    {
                        Dr_Led_id = model.iLed_ID;
                    }
                    if (model.Cmd_Mode_SaleA.ToString().ToUpper() == "CASH")
                    {
                        Cr_Led_id = "00080";                //Cash A/c.
                    }
                    else
                    {
                        Cr_Led_id = "00079";                //Bank A/c.
                        Sub_Cr_Led_ID = model.GLookUp_BankList_SaleA;
                    }
                }
                else
                {
                    if (model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS")     // OTHER ASSET
                    {
                        if (model.iAsset_Type.ToUpper() == "ASSET")
                        {
                            Cr_Led_id = model.iLed_ID;
                        }
                        else
                        {
                            Cr_Led_id = model.iCon_Led_ID;
                        }
                    }
                    else
                    {
                        Cr_Led_id = model.iLed_ID;
                    }
                    if (model.Cmd_Mode_SaleA.ToString().ToUpper() == "CASH")
                    {
                        Dr_Led_id = "00080";                        //Cash A/c.
                    }
                    else
                    {
                        Dr_Led_id = "00079";                        //Bank A/c.
                        Sub_Dr_Led_ID = model.GLookUp_BankList_SaleA;
                    }
                }
                Common_Lib.RealTimeService.Param_Txn_Insert_VoucherSaleOfAsset InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_VoucherSaleOfAsset();
                if ((model.Tag == Common_Lib.Common.Navigation_Mode._New) || (model.Tag == Common_Lib.Common.Navigation_Mode._New_From_Selection))  // new
                {
                    model.xMID = System.Guid.NewGuid().ToString();
                    model.xID = System.Guid.NewGuid().ToString();
                    string STR2 = string.Empty;
                    double XCASH = 0.0;
                    double XBANK = 0.0;
                    if (model.Cmd_Mode_SaleA.ToString().ToUpper() == "CASH")
                    {
                        if (model.Txt_Amount_SaleA != null)
                        {
                            XCASH = double.Parse(model.Txt_Amount_SaleA.ToString());
                        }
                    }
                    else
                    {
                        if (model.Txt_Amount_SaleA != null)
                        {
                            XBANK = double.Parse(model.Txt_Amount_SaleA.ToString());
                        }
                    }
                    // MASTER ENTRY
                    Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherSaleOfAsset InMinfo = new Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherSaleOfAsset();
                    InMinfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                    InMinfo.VNo = model.Txt_V_NO_SaleA??string.Empty;
                    if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                    {
                        InMinfo.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMinfo.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                    }
                    InMinfo.PartyID = model.GLookUp_PartyList1_SaleA;                    
                    InMinfo.SubTotal = Convert.ToDouble(model.Txt_Amount_SaleA);
                    InMinfo.Cash = XCASH;
                    InMinfo.Bank = XBANK;
                    InMinfo.Advance = 0;
                    InMinfo.Liability = 0;
                    InMinfo.Credit = 0;
                    InMinfo.TDS = 0;
                    InMinfo.SaleAmt = Convert.ToDouble(model.Txt_SaleAmt_SaleA);
                    InMinfo.SaleQty = Convert.ToDouble(model.Txt_Qty_SaleA);
                    if (IsDate(model.Txt_SDate_SaleA.ToString()))
                    {
                        InMinfo.SaleDate = Convert.ToDateTime(model.Txt_SDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMinfo.SaleDate = string.IsNullOrEmpty(model.Txt_SDate_SaleA.ToString()) ? string.Empty : model.Txt_SDate_SaleA.ToString();
                    }
                    InMinfo.SaleType = model.Cmb_Asset_Type_SaleA;
                    InMinfo.Status_Action = Status_Action;
                    InMinfo.RecID = model.xMID;
                    InNewParam.param_InsertMasterEntry = InMinfo;
                    // Profit/Loss Calculation                
                    decimal BalanceQty = Convert.ToDecimal(model.REF_QTY);
                    decimal BalanceCost = 0;
                    if (BalanceQty != 0)
                    {
                        BalanceCost = ((Convert.ToDecimal(model.REF_AMT) * Convert.ToDecimal(model.Txt_Qty_SaleA)) / BalanceQty);
                    }

                    if (BalanceQty != Convert.ToDecimal(model.Txt_Qty_SaleA))        //  if this is not final sale 
                    {
                        BalanceCost = Math.Round(BalanceCost, 0);
                    }
                    decimal DiffOfSale = Convert.ToDecimal(model.Txt_SaleAmt_SaleA) - BalanceCost;
                    if (model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS")
                    {
                        if (model.iAsset_Type != "ASSET")
                        {
                            DiffOfSale = 0;         //  Insurance Assets wont generate profit/loss
                        }
                    }

                    decimal Receipt_Unposted = Convert.ToDecimal(model.Txt_Amount_SaleA);
                    decimal Vehicle_Unposted = BalanceCost;
                    decimal TxnAmount = 0;
                    if (Convert.ToDecimal(model.Txt_Amount_SaleA) < BalanceCost)
                    {
                        TxnAmount = Convert.ToDecimal(model.Txt_Amount_SaleA);
                    }
                    else
                    {
                        TxnAmount = BalanceCost;
                    }
                    if (model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS")
                    {
                        if (model.iAsset_Type != "ASSET")
                        {
                            TxnAmount = Convert.ToDecimal(model.Txt_Amount_SaleA);
                            Vehicle_Unposted = Convert.ToDecimal(model.Txt_SaleAmt_SaleA);
                        }
                    }
                    // Transaction info
                    Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                    InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                    InParam.VNo = model.Txt_V_NO_SaleA??"";
                    if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                    {
                        InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                    }
                    InParam.ItemID = model.GLookUp_ItemList_SaleA;
                    InParam.Type = model.iTrans_Type;
                    InParam.Cr_Led_ID = Cr_Led_id;
                    InParam.Dr_Led_ID = Dr_Led_id;
                    InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    InParam.Mode = model.Cmd_Mode_SaleA;
                    InParam.Ref_Bank = model.GLookUp_RefBankList_SaleA??string.Empty;
                    InParam.Ref_Branch = model.Txt_Ref_Branch_SaleA ?? string.Empty;
                    InParam.Ref_No = model.Txt_Ref_No_SaleA ?? string.Empty;
                    if (IsDate(model.Txt_Ref_Date_SaleA.ToString()))
                    {
                        InParam.RefDate = Convert.ToDateTime(model.Txt_Ref_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.RefDate = string.IsNullOrEmpty(model.Txt_Ref_Date_SaleA.ToString()) ? string.Empty : model.Txt_Ref_Date_SaleA.ToString();
                    }

                    if (IsDate(model.Txt_Ref_CDate_SaleA.ToString()))
                    {
                        InParam.RefCDate = Convert.ToDateTime(model.Txt_Ref_CDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.RefCDate = string.IsNullOrEmpty(model.Txt_Ref_CDate_SaleA.ToString())? string.Empty : model.Txt_Ref_CDate_SaleA.ToString() ;
                    }
                    InParam.Amount = Convert.ToDouble(TxnAmount);
                    InParam.PartyID = model.GLookUp_PartyList1_SaleA;
                    InParam.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA.ToString() + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA.ToString() + " and Profit (Or Loss) = " + DiffOfSale + "";     //'changes as per pm.bkinfo.in/issues/5308#note-6
                    InParam.Remarks = model.Txt_Remarks_SaleA ?? string.Empty;
                    InParam.Reference = model.Txt_Reference_SaleA ?? string.Empty;
                    InParam.Tr_M_ID = model.xMID;
                    InParam.TxnSrNo = 1;
                    InParam.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                    InParam.Status_Action = Status_Action;
                    InParam.RecID = model.xID;
                    InNewParam.param_InsertTransactionInfo = InParam;
                    Vehicle_Unposted = (Vehicle_Unposted - Convert.ToDecimal(InParam.Amount));
                    Receipt_Unposted = (Receipt_Unposted - Convert.ToDecimal(InParam.Amount));
                    // PAYMENT  
                    Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset InPay = new Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset();
                    InPay.TxnMID = model.xMID;
                    InPay.Type = "SALE";
                    InPay.SrNo = "1";
                    InPay.RefID = model.GLookUp_AssetList_SaleA;
                    InPay.RefAmount = Convert.ToDouble(model.Txt_Amount_SaleA);
                    InPay.Status_Action = Status_Action;
                    InPay.RecID = System.Guid.NewGuid().ToString();
                    InNewParam.param_InsertAandLPayment = InPay;
                    // JV ENTRY IF difference found
                    if ((model.iOffSet_ID.ToString().Length > 0) && (model.Txt_Diff_SaleA > 0))
                    {
                        // CREDIT ENTRY
                        if ((Vehicle_Unposted > 0 && Receipt_Unposted == 0) || (DiffOfSale < 0))
                        {
                            Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InParams = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                            InParams.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                            InParams.VNo = string.IsNullOrEmpty(model.Txt_V_NO_SaleA) ? string.Empty : model.Txt_V_NO_SaleA;
                            if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                            {
                                InParams.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                            }
                            InParams.ItemID = model.GLookUp_ItemList_SaleA;
                            InParams.Type = model.iTrans_Type;
                            InParams.Cr_Led_ID = Cr_Led_id;
                            InParams.Dr_Led_ID = string.Empty;
                            InParams.Sub_Cr_Led_ID = string.Empty;
                            InParams.Sub_Dr_Led_ID = string.Empty;
                            InParams.Mode = model.Cmd_Mode_SaleA;
                            InParams.Ref_Bank = string.Empty;
                            InParams.Ref_Branch = string.Empty;
                            InParams.Ref_No = model.Txt_Ref_No_SaleA ?? string.Empty;
                            if (IsDate(model.Txt_Ref_Date_SaleA.ToString()))
                            {
                                InParams.RefDate = Convert.ToDateTime(model.Txt_Ref_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.RefDate = string.IsNullOrEmpty(model.Txt_Ref_Date_SaleA.ToString()) ? string.Empty : model.Txt_Ref_Date_SaleA.ToString();
                            }
                            if (IsDate(model.Txt_Ref_CDate_SaleA.ToString()))
                            {
                                InParams.RefCDate = Convert.ToDateTime(model.Txt_Ref_CDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParams.RefCDate = string.IsNullOrEmpty(model.Txt_Ref_CDate_SaleA.ToString()) ? string.Empty : model.Txt_Ref_CDate_SaleA.ToString();
                            }
                            if (DiffOfSale > 0)
                            {
                                InParams.Amount = Convert.ToDouble(Vehicle_Unposted);
                            }
                            else
                            {
                                InParams.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);    // Credit Vehicle by Advance Amount
                            }

                            InParams.PartyID = model.GLookUp_PartyList1_SaleA;
                            InParams.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA + " and Profit (Or Loss) = " + DiffOfSale + "";    //changes as per pm.bkinfo.in/issues/5308#note-6
                            InParams.Remarks = model.Txt_Remarks_SaleA ?? string.Empty;
                            InParams.Reference = model.Txt_Reference_SaleA??string.Empty;
                            InParams.Tr_M_ID = model.xMID;
                            InParams.TxnSrNo = 1;
                            InParams.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                            InParams.Status_Action = Status_Action;
                            InParams.RecID = System.Guid.NewGuid().ToString();
                            InNewParam.param_InsertCreditJV = InParams;
                            Vehicle_Unposted = (Vehicle_Unposted - Convert.ToDecimal(InParams.Amount));
                        }
                        // DEBIT ENTRY
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InParam1 = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                        InParam1.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                        InParam1.VNo = model.Txt_V_NO_SaleA??string.Empty;
                        if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                        {
                            InParam1.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam1.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                        }
                        InParam1.ItemID = model.iOffSet_ID;
                        InParam1.Type = JV_TRANS_TYPE;
                        InParam1.Cr_Led_ID = string.Empty;
                        InParam1.Dr_Led_ID = JV_Dr_Led_id;
                        InParam1.Sub_Cr_Led_ID = string.Empty;
                        InParam1.Sub_Dr_Led_ID = string.Empty;
                        InParam1.Mode = model.Cmd_Mode_SaleA;
                        InParam1.Ref_Bank = string.Empty;
                        InParam1.Ref_Branch = string.Empty;
                        InParam1.Ref_No = model.Txt_Ref_No_SaleA??string.Empty;
                        if (IsDate(model.Txt_Ref_Date_SaleA.ToString()))
                        {
                            InParam1.RefDate = Convert.ToDateTime(model.Txt_Ref_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam1.RefDate = string.IsNullOrEmpty(model.Txt_Ref_Date_SaleA.ToString())?string.Empty: model.Txt_Ref_Date_SaleA.ToString();
                        }
                        if (IsDate(model.Txt_Ref_CDate_SaleA.ToString()))
                        {
                            InParam1.RefCDate = Convert.ToDateTime(model.Txt_Ref_CDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam1.RefCDate = string.IsNullOrEmpty(model.Txt_Ref_CDate_SaleA.ToString()) ? string.Empty : model.Txt_Ref_CDate_SaleA.ToString();
                        }
                        InParam1.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InParam1.PartyID = model.GLookUp_PartyList1_SaleA;
                        InParam1.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA + " and Profit (Or Loss) = " + DiffOfSale + "";        //changes as per pm.bkinfo.in/issues/5308#note-6
                        InParam1.Remarks = model.Txt_Remarks_SaleA??string.Empty;
                        InParam1.Reference = model.Txt_Reference_SaleA??string.Empty;
                        InParam1.Tr_M_ID = model.xMID;
                        InParam1.TxnSrNo = 2;
                        InParam1.Cross_Ref_ID = string.Empty;
                        InParam1.Status_Action = Status_Action;
                        InParam1.RecID = System.Guid.NewGuid().ToString();
                        InNewParam.param_InsertDebitJV = InParam1;
                        // PAYMENT - ADJUSTMENT
                        Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset InPmt = new Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset();
                        InPmt.TxnMID = model.xMID;
                        InPmt.Type = "SALE-RECEIVABLE";
                        InPmt.SrNo = "2";
                        InPmt.RefID = model.GLookUp_AssetList_SaleA;
                        InPmt.RefAmount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InPmt.Status_Action = Status_Action;
                        InPmt.RecID = System.Guid.NewGuid().ToString();
                        InNewParam.param_InsertPaymentAdjustment = InPmt;
                        // Profile - Advances
                        Common_Lib.RealTimeService.Parameter_InsertTRID_Advances InPara = new Common_Lib.RealTimeService.Parameter_InsertTRID_Advances();
                        InPara.ItemID = model.iOffSet_ID;
                        InPara.PartyID = model.GLookUp_PartyList1_SaleA;
                        if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                        {
                            InPara.AdvanceDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InPara.AdvanceDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                        }
                        InPara.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InPara.Purpose = model.Txt_Narration_SaleA ?? string.Empty;
                        InPara.Remarks = "Sale " + model.GLookUp_AssetList_Text_SaleA + ": " + model.Txt_Desc_SaleA + ", Sale Date: " + model.Txt_SDate_SaleA.ToString();
                        InPara.TxnID = model.xMID;
                        InPara.Status_Action = Status_Action;
                        InPara.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset;
                        InNewParam.param_InsertProfileAdvances = InPara;
                        // purpose
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset InPurpose_JV = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset();
                        InPurpose_JV.TxnID = model.xMID;
                        InPurpose_JV.PurposeID = model.GLookUp_PurList_SaleA;
                        InPurpose_JV.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InPurpose_JV.Status_Action = Status_Action;
                        InPurpose_JV.RecID = System.Guid.NewGuid().ToString();
                        InPurpose_JV.SrNo = 2;
                        InNewParam.param_InsertPurposeJV = InPurpose_JV;
                    }
                    // Profit/Loss Posting , if Sale Amount <> Balance Cost(Sold Qty)
                    if (DiffOfSale != 0)      // Profit if +ve , else Loss
                    {
                        // Debit jv for Profit/Loss Ledger
                        decimal PL_Unposted = DiffOfSale;
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InProfit_Loss = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                        InProfit_Loss.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                        InProfit_Loss.VNo = model.Txt_V_NO_SaleA??string.Empty;
                        if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                        {
                            InProfit_Loss.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InProfit_Loss.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                        }
                        if (DiffOfSale > 0)     //  Profit
                        {
                            InProfit_Loss.ItemID = model.SaleProfit_ItemID;
                            InProfit_Loss.Type = "CREDIT";
                            InProfit_Loss.Cr_Led_ID = model.SaleProfit_Loss_LedID;
                            if (DiffOfSale >= Receipt_Unposted)
                            {
                                InProfit_Loss.Amount = Convert.ToDouble(Receipt_Unposted);
                                InProfit_Loss.Dr_Led_ID = Dr_Led_id;
                                InProfit_Loss.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                            }
                            else            // Receipt has already been settled, as Receipt Left + cost <= Total Sale Amount                                                
                            {
                                InProfit_Loss.Amount = Convert.ToDouble(DiffOfSale);
                                InProfit_Loss.Dr_Led_ID = string.Empty;
                                InProfit_Loss.Sub_Dr_Led_ID = string.Empty;
                            }
                        }
                        else           // Loss
                        {
                            InProfit_Loss.ItemID = model.SaleLoss_ItemID;
                            InProfit_Loss.Type = "DEBIT";
                            InProfit_Loss.Cr_Led_ID = string.Empty;
                            InProfit_Loss.Dr_Led_ID = model.SaleProfit_Loss_LedID;
                            InProfit_Loss.Amount = (Convert.ToDouble(1 * DiffOfSale) * -1);
                            InProfit_Loss.Sub_Dr_Led_ID = string.Empty;
                        }
                        InProfit_Loss.Sub_Cr_Led_ID = string.Empty;
                        InProfit_Loss.Mode = model.Cmd_Mode_SaleA;
                        InProfit_Loss.Ref_Bank = string.Empty;
                        InProfit_Loss.Ref_Branch = string.Empty;
                        InProfit_Loss.Ref_No = string.Empty;
                        InProfit_Loss.PartyID = model.GLookUp_PartyList1_SaleA;
                        InProfit_Loss.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA.ToString() + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA.ToString() + " and Profit (Or Loss) = " + DiffOfSale + "";       //changes as per pm.bkinfo.in/issues/5308#note-6
                        InProfit_Loss.Remarks = string.IsNullOrEmpty(model.Txt_Remarks_SaleA) ? string.Empty : model.Txt_Remarks_SaleA;
                        InProfit_Loss.Reference = string.IsNullOrEmpty(model.Txt_Reference_SaleA) ? string.Empty : model.Txt_Reference_SaleA;
                        InProfit_Loss.Tr_M_ID = model.xMID;
                        InProfit_Loss.TxnSrNo = 3;
                        InProfit_Loss.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                        InProfit_Loss.Status_Action = Status_Action;
                        InProfit_Loss.RecID = System.Guid.NewGuid().ToString();
                        InNewParam.param_InsertPL = InProfit_Loss;
                        PL_Unposted = (PL_Unposted - Convert.ToDecimal(InProfit_Loss.Amount));
                        if ((PL_Unposted > 0) || (DiffOfSale < 0))
                        {
                            Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InProfit_Loss_2 = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                            InProfit_Loss_2.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                            InProfit_Loss_2.VNo = string.IsNullOrEmpty(model.Txt_V_NO_SaleA) ? string.Empty : model.Txt_V_NO_SaleA;
                            if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                            {
                                InProfit_Loss_2.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InProfit_Loss_2.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                            }
                            InProfit_Loss_2.Cr_Led_ID = model.SaleProfit_Loss_LedID;
                            if (DiffOfSale > 0)     //  Profit
                            {
                                InProfit_Loss_2.ItemID = model.SaleProfit_ItemID;
                                InProfit_Loss_2.Type = "CREDIT";
                                InProfit_Loss_2.Cr_Led_ID = model.SaleProfit_Loss_LedID;
                                InProfit_Loss_2.Amount = Convert.ToDouble(PL_Unposted);
                                InProfit_Loss_2.Dr_Led_ID = "";
                            }
                            else                   // Loss
                            {
                                InProfit_Loss_2.ItemID = model.GLookUp_ItemList_SaleA;
                                InProfit_Loss_2.Type = "CREDIT";
                                InProfit_Loss_2.Cr_Led_ID = Cr_Led_id;
                                InProfit_Loss_2.Dr_Led_ID = string.Empty;
                                InProfit_Loss_2.Amount = (Convert.ToDouble(1 * DiffOfSale) * -1);
                            }
                            InProfit_Loss_2.Sub_Cr_Led_ID = string.Empty;
                            InProfit_Loss_2.Sub_Dr_Led_ID = string.Empty;
                            InProfit_Loss_2.Mode = model.Cmd_Mode_SaleA;
                            InProfit_Loss_2.Ref_Bank = string.Empty;
                            InProfit_Loss_2.Ref_Branch = string.Empty;
                            InProfit_Loss_2.Ref_No = string.Empty;
                            InProfit_Loss_2.PartyID = model.GLookUp_PartyList1_SaleA;
                            InProfit_Loss_2.Narration = model.Txt_Narration_SaleA ?? string.Empty;
                            InProfit_Loss_2.Remarks = model.Txt_Remarks_SaleA ?? string.Empty;
                            InProfit_Loss_2.Reference = model.Txt_Reference_SaleA ?? string.Empty;
                            InProfit_Loss_2.Tr_M_ID = model.xMID;
                            InProfit_Loss_2.TxnSrNo = 3;
                            InProfit_Loss_2.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                            InProfit_Loss_2.Status_Action = Status_Action;
                            InProfit_Loss_2.RecID = System.Guid.NewGuid().ToString();
                            InNewParam.param_InsertPL_2 = InProfit_Loss_2;
                        }
                        // purpose
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset InPurpose_PL = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset();
                        InPurpose_PL.TxnID = model.xMID;
                        InPurpose_PL.PurposeID = model.GLookUp_PurList_SaleA;
                        if (DiffOfSale > 0)
                        {
                            InPurpose_PL.Amount = Convert.ToDouble(DiffOfSale);
                        }
                        else
                        {
                            InPurpose_PL.Amount = (Convert.ToDouble(1 * DiffOfSale) * -1);
                        }
                        InPurpose_PL.Status_Action = Status_Action;
                        InPurpose_PL.RecID = System.Guid.NewGuid().ToString();
                        InPurpose_PL.SrNo = 3;
                        InNewParam.param_InsertPurposePL = InPurpose_PL;
                    }
                    // purpose
                    Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset InPurpose = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset();
                    InPurpose.TxnID = model.xMID;
                    InPurpose.PurposeID = model.GLookUp_PurList_SaleA;
                    InPurpose.Amount = Convert.ToDouble(model.Txt_Amount_SaleA);
                    InPurpose.Status_Action = Status_Action;
                    InPurpose.RecID = System.Guid.NewGuid().ToString();
                    InPurpose.SrNo = 1;

                    InNewParam.param_InsertPurpose = InPurpose;

                    //FCRA Insert Process
                    if (model.SaleA_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.SaleA_SplVchrReferenceSelected.Split(',');
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

                    if (!(bool)(BASE._SaleOfAsset_DBOps.InsertSaleOfAsset_Txn(InNewParam)))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Messages.SaveSuccess;
                    jsonParam.title = "Sale of Asset";
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam, CashbookGridPK = model.xMID + model.xID }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_Update_VoucherSaleOfAsset EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_VoucherSaleOfAsset();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit)       // edit
                {
                    string STR2 = "";
                    double XCASH = 0.0;
                    double XBANK = 0.0;
                    if (model.Cmd_Mode_SaleA.ToUpper() == "CASH")
                    {
                        XCASH = Convert.ToDouble(model.Txt_Amount_SaleA);
                    }
                    else
                    {
                        XBANK = Convert.ToDouble(model.Txt_Amount_SaleA);
                    }
                    // MASTER ENTRY
                    Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherSaleOfAsset UpMaster = new Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherSaleOfAsset();
                    UpMaster.VNo = model.Txt_V_NO_SaleA ?? "";
                    if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                    {
                        UpMaster.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMaster.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                    }

                    UpMaster.PartyID = model.GLookUp_PartyList1_SaleA;
                    UpMaster.SubTotal = Convert.ToDouble(model.Txt_Amount_SaleA);
                    UpMaster.Cash = XCASH;
                    UpMaster.Bank = XBANK;
                    UpMaster.SaleAmt = Convert.ToDouble(model.Txt_SaleAmt_SaleA);
                    UpMaster.SaleQty = Convert.ToDouble(model.Txt_Qty_SaleA);
                    if (IsDate(model.Txt_SDate_SaleA.ToString()))
                    {
                        UpMaster.SaleDate = Convert.ToDateTime(model.Txt_SDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMaster.SaleDate = string.IsNullOrEmpty(model.Txt_SDate_SaleA.ToString()) ? string.Empty : model.Txt_SDate_SaleA.ToString();
                    }
                    UpMaster.SaleType = model.Cmb_Asset_Type_SaleA;
                    UpMaster.RecID = model.xMID;
                    EditParam.param_UpdateMaster = UpMaster;
                    EditParam.Mid_DeleteItems = model.xMID;
                    EditParam.Mid_DeletePayment = model.xMID;
                    EditParam.Mid_DeletePurpose = model.xMID;
                    EditParam.Mid_Delete = model.xMID;
                    // PROFILE                
                    EditParam.Mid_DeleteAdvances = model.xMID;
                    // Profit/Loss Calculation                
                    decimal BalanceQty = Convert.ToDecimal(model.REF_QTY);
                    decimal BalanceCost = 0;
                    if (BalanceQty != 0)
                    {
                        BalanceCost = ((Convert.ToDecimal(model.REF_AMT) * Convert.ToDecimal(model.Txt_Qty_SaleA)) / BalanceQty);
                    }
                    if (BalanceQty != Convert.ToDecimal(model.Txt_Qty_SaleA))   //  if this is not final sale 
                    {
                        BalanceCost = Math.Round(BalanceCost, 0); //'Bug #5098
                    }
                    decimal DiffOfSale = (Convert.ToDecimal(model.Txt_SaleAmt_SaleA) - BalanceCost);
                    if (model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS")
                    {
                        if (model.iAsset_Type != "ASSET")
                        {
                            DiffOfSale = 0;     //  Insurance Assets wont generate profit/loss
                        }
                    }

                    decimal Receipt_Unposted = Convert.ToDecimal(model.Txt_Amount_SaleA);
                    decimal Vehicle_Unposted = BalanceCost;
                    decimal TxnAmount = 0;

                    if (Convert.ToDecimal(model.Txt_Amount_SaleA) < BalanceCost)
                    {
                        TxnAmount = Convert.ToDecimal(model.Txt_Amount_SaleA);
                    }
                    else
                    {
                        TxnAmount = BalanceCost;
                    }
                    if (model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS")
                    {
                        if (model.iAsset_Type != "ASSET")
                        {
                            TxnAmount = Convert.ToDecimal(model.Txt_Amount_SaleA);
                            Vehicle_Unposted = Convert.ToDecimal(model.Txt_SaleAmt_SaleA);
                        }
                    }
                    // Transaction info 1: 
                    // Cash / Bank A/c Dr         Amt Received
                    //                To Vehicles                 Amt Received
                    Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                    InParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                    InParam.VNo = string.IsNullOrEmpty(model.Txt_V_NO_SaleA) ? string.Empty : model.Txt_V_NO_SaleA;
                    if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                    {
                        InParam.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                    }
                    InParam.ItemID = model.GLookUp_ItemList_SaleA;
                    InParam.Type = model.iTrans_Type;
                    InParam.Cr_Led_ID = Cr_Led_id;
                    InParam.Dr_Led_ID = Dr_Led_id;
                    InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID;
                    InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                    InParam.Mode = model.Cmd_Mode_SaleA;
                    InParam.Ref_Bank = model.GLookUp_RefBankList_SaleA;
                    InParam.Ref_Branch = string.IsNullOrEmpty(model.Txt_Ref_Branch_SaleA) ? string.Empty : model.Txt_Ref_Branch_SaleA;
                    InParam.Ref_No = string.IsNullOrEmpty(model.Txt_Ref_No_SaleA) ? string.Empty : model.Txt_Ref_No_SaleA;
                    if (IsDate(model.Txt_Ref_Date_SaleA.ToString()))
                    {
                        InParam.RefDate = Convert.ToDateTime(model.Txt_Ref_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.RefDate = string.IsNullOrEmpty(model.Txt_Ref_Date_SaleA.ToString()) ? string.Empty : model.Txt_Ref_Date_SaleA.ToString();
                    }
                    if (IsDate(model.Txt_Ref_CDate_SaleA.ToString()))
                    {
                        InParam.RefCDate = Convert.ToDateTime(model.Txt_Ref_CDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam.RefCDate = string.IsNullOrEmpty(model.Txt_Ref_CDate_SaleA.ToString()) ? string.Empty : model.Txt_Ref_CDate_SaleA.ToString();
                    }
                    InParam.Amount = Convert.ToDouble(TxnAmount);
                    InParam.PartyID = model.GLookUp_PartyList1_SaleA;
                    InParam.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA.ToString() + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA.ToString() + " and Profit (Or Loss) = " + DiffOfSale + "";     //changes as per pm.bkinfo.in/issues/5308#note-6
                    InParam.Remarks = string.IsNullOrEmpty(model.Txt_Remarks_SaleA) ? string.Empty : model.Txt_Remarks_SaleA.ToString();
                    InParam.Reference = string.IsNullOrEmpty(model.Txt_Reference_SaleA) ? string.Empty : model.Txt_Reference_SaleA.ToString();
                    InParam.Tr_M_ID = model.xMID;
                    InParam.TxnSrNo = 1;
                    InParam.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                    InParam.Status_Action = Status_Action;
                    InParam.RecID = model.xID;
                    EditParam.param_InsertTransactionInfo = InParam;
                    Vehicle_Unposted = (Vehicle_Unposted - Convert.ToDecimal(InParam.Amount));
                    Receipt_Unposted = (Receipt_Unposted - Convert.ToDecimal(InParam.Amount));
                    // PAYMENT  
                    Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset InPay = new Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset();
                    InPay.TxnMID = model.xMID;
                    InPay.Type = "SALE";
                    InPay.SrNo = "1";
                    InPay.RefID = model.GLookUp_AssetList_SaleA;
                    InPay.RefAmount = Convert.ToDouble(model.Txt_Amount_SaleA);
                    InPay.Status_Action = Status_Action;
                    InPay.RecID = System.Guid.NewGuid().ToString();
                    EditParam.param_InsertAandLPayment = InPay;
                    // Transaction Info 2
                    // Advance --         Diff of Sale and Recd
                    //        To Vehicle 39860                Diff of Sale and Recd
                    if ((model.iOffSet_ID.ToString().Length > 0) && (Convert.ToDouble(model.Txt_Diff_SaleA) > 0))
                    {
                        // CREDIT ENTRY
                        if (((Vehicle_Unposted > 0) && (Receipt_Unposted == 0)) || (DiffOfSale < 0))
                        {
                            Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InsParams = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                            InsParams.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                            InsParams.VNo = string.IsNullOrEmpty(model.Txt_V_NO_SaleA) ? string.Empty : model.Txt_V_NO_SaleA;
                            if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                            {
                                InsParams.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InsParams.TDate = model.Txt_V_Date_SaleA.ToString();
                            }
                            InsParams.ItemID = model.GLookUp_ItemList_SaleA;
                            InsParams.Type = model.iTrans_Type;
                            InsParams.Cr_Led_ID = Cr_Led_id;
                            InsParams.Dr_Led_ID = string.Empty;
                            InsParams.Sub_Cr_Led_ID = string.Empty;
                            InsParams.Sub_Dr_Led_ID = string.Empty;
                            InsParams.Mode = model.Cmd_Mode_SaleA;
                            InsParams.Ref_Bank = string.Empty;
                            InsParams.Ref_Branch = string.Empty;
                            InsParams.Ref_No = string.IsNullOrEmpty(model.Txt_Ref_No_SaleA) ? string.Empty : model.Txt_Ref_No_SaleA;
                            if (IsDate(model.Txt_Ref_Date_SaleA.ToString()))
                            {
                                InsParams.RefDate = Convert.ToDateTime(model.Txt_Ref_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InsParams.RefDate = model.Txt_Ref_Date_SaleA.ToString();
                            }
                            if (IsDate(model.Txt_Ref_CDate_SaleA.ToString()))
                            {
                                InsParams.RefCDate = Convert.ToDateTime(model.Txt_Ref_CDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InsParams.RefCDate = string.IsNullOrEmpty(model.Txt_Ref_CDate_SaleA.ToString()) ? string.Empty : model.Txt_Ref_CDate_SaleA.ToString();
                            }
                            if (DiffOfSale > 0)
                            {
                                InsParams.Amount = Convert.ToDouble(Vehicle_Unposted);
                            }
                            else
                            {
                                InsParams.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);       // Credit Vehicle by Advance Amount
                            }
                            InsParams.PartyID = model.GLookUp_PartyList1_SaleA;
                            InsParams.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA.ToString() + " and Profit (Or Loss) = " + DiffOfSale + "";           //changes as per pm.bkinfo.in/issues/5308#note-6
                            InsParams.Remarks = string.IsNullOrEmpty(model.Txt_Remarks_SaleA) ? string.Empty : model.Txt_Remarks_SaleA;
                            InsParams.Reference = string.IsNullOrEmpty(model.Txt_Reference_SaleA) ? string.Empty : model.Txt_Reference_SaleA;
                            InsParams.Tr_M_ID = model.xMID;
                            InsParams.TxnSrNo = 1;
                            InsParams.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                            InsParams.Status_Action = Status_Action;
                            InsParams.RecID = System.Guid.NewGuid().ToString();
                            EditParam.param_InsertCreditJV = InsParams;
                            Vehicle_Unposted = (Vehicle_Unposted - Convert.ToDecimal(InsParams.Amount));
                        }
                        // DEBIT ENTRY
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InsParam = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                        InsParam.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                        InsParam.VNo = string.IsNullOrEmpty(model.Txt_V_NO_SaleA) ? string.Empty : model.Txt_V_NO_SaleA;
                        if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                        {
                            InsParam.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InsParam.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                        }
                        InsParam.ItemID = model.iOffSet_ID;
                        InsParam.Type = JV_TRANS_TYPE;
                        InsParam.Cr_Led_ID = string.Empty;
                        InsParam.Dr_Led_ID = JV_Dr_Led_id;
                        InsParam.Sub_Cr_Led_ID = string.Empty;
                        InsParam.Sub_Dr_Led_ID = string.Empty;
                        InsParam.Mode = model.Cmd_Mode_SaleA;
                        InsParam.Ref_Bank = string.Empty;
                        InsParam.Ref_Branch = string.Empty;
                        InsParam.Ref_No = string.IsNullOrEmpty(model.Txt_Ref_No_SaleA) ? string.Empty : model.Txt_Ref_No_SaleA;
                        if (IsDate(model.Txt_Ref_Date_SaleA.ToString()))
                        {
                            InsParam.RefDate = Convert.ToDateTime(model.Txt_Ref_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InsParam.RefDate = string.IsNullOrEmpty(model.Txt_Ref_Date_SaleA.ToString()) ? string.Empty : model.Txt_Ref_Date_SaleA.ToString();
                        }
                        if (IsDate(model.Txt_Ref_CDate_SaleA.ToString()))
                        {
                            InsParam.RefCDate = Convert.ToDateTime(model.Txt_Ref_CDate_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InsParam.RefCDate = string.IsNullOrEmpty(model.Txt_Ref_CDate_SaleA.ToString()) ? string.Empty : model.Txt_Ref_CDate_SaleA.ToString();
                        }
                        InsParam.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InsParam.PartyID = model.GLookUp_PartyList1_SaleA;
                        InsParam.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA.ToString() + " and Profit (Or Loss) = " + DiffOfSale + "";            //changes as per pm.bkinfo.in/issues/5308#note-6
                        InsParam.Remarks = string.IsNullOrEmpty(model.Txt_Remarks_SaleA) ? string.Empty : model.Txt_Remarks_SaleA;
                        InsParam.Reference = string.IsNullOrEmpty(model.Txt_Reference_SaleA) ? string.Empty : model.Txt_Reference_SaleA;
                        InsParam.Tr_M_ID = model.xMID;
                        InsParam.TxnSrNo = 2;
                        InsParam.Cross_Ref_ID = string.Empty;
                        InsParam.Status_Action = Status_Action;
                        InsParam.RecID = System.Guid.NewGuid().ToString();
                        EditParam.param_InsertDebitJV = InsParam;
                        // PAYMENT - ADJUSTMENT
                        Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset InsPmt = new Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset();
                        InsPmt.TxnMID = model.xMID;
                        InsPmt.Type = "SALE-RECEIVABLE";
                        InsPmt.SrNo = "2";
                        InsPmt.RefID = model.GLookUp_AssetList_SaleA;
                        InsPmt.RefAmount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InsPmt.Status_Action = Status_Action;
                        InsPmt.RecID = System.Guid.NewGuid().ToString();
                        EditParam.param_InsertPaymentAdjustment = InsPmt;
                        // Profile - Advances
                        Common_Lib.RealTimeService.Parameter_InsertTRID_Advances InParam1 = new Common_Lib.RealTimeService.Parameter_InsertTRID_Advances();
                        InParam1.ItemID = model.iOffSet_ID;
                        InParam1.PartyID = model.GLookUp_PartyList1_SaleA;
                        if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                        {
                            InParam1.AdvanceDate = Convert.ToDateTime(model.Txt_V_Date_SaleA).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam1.AdvanceDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                        }
                        InParam1.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InParam1.Purpose = string.IsNullOrEmpty(model.Txt_Narration_SaleA) ? string.Empty : model.Txt_Narration_SaleA;
                        InParam1.Remarks = "Sale " + model.GLookUp_AssetList_Text_SaleA + ": " + model.Txt_Desc_SaleA + ", Sale Date: " + model.Txt_SDate_SaleA;
                        InParam1.TxnID = model.xMID;
                        InParam1.Status_Action = Status_Action;
                        InParam1.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset;
                        EditParam.param_InsertProfileAdvances = InParam1;
                        // purpose
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset InPurp_JV = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset();
                        InPurp_JV.TxnID = model.xMID;
                        InPurp_JV.PurposeID = model.GLookUp_PurList_SaleA;
                        InPurp_JV.Amount = Convert.ToDouble(model.Txt_Diff_SaleA);
                        InPurp_JV.Status_Action = Status_Action;
                        InPurp_JV.RecID = System.Guid.NewGuid().ToString();
                        InPurp_JV.SrNo = 2;
                        EditParam.param_InsertPurposeJV = InPurp_JV;
                    }
                    // Profit/Loss Posting , if Sale Amount <> Balance Cost(Sold Qty)
                    if ((DiffOfSale != 0))
                    {

                        // Debit jv for Profit/Loss Ledger
                        decimal PL_Unposted = DiffOfSale;
                        Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InProfit_Loss = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                        InProfit_Loss.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                        InProfit_Loss.VNo = string.IsNullOrEmpty(model.Txt_V_NO_SaleA) ? string.Empty : model.Txt_V_NO_SaleA;
                        if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                        {
                            InProfit_Loss.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InProfit_Loss.TDate = model.Txt_V_Date_SaleA.ToString();
                        }
                        if (DiffOfSale > 0)     //  Profit
                        {
                            InProfit_Loss.ItemID = model.SaleProfit_ItemID;
                            InProfit_Loss.Type = "CREDIT";
                            InProfit_Loss.Cr_Led_ID = model.SaleProfit_Loss_LedID;
                            if (DiffOfSale >= Receipt_Unposted)
                            {
                                InProfit_Loss.Amount = Convert.ToDouble(Receipt_Unposted);
                                InProfit_Loss.Dr_Led_ID = Dr_Led_id;
                                InProfit_Loss.Sub_Dr_Led_ID = Sub_Dr_Led_ID;
                            }
                            else     // Receipt has already been settled, as Receipt Left + cost <= Total Sale Amount
                            {
                                InProfit_Loss.Amount = Convert.ToDouble(DiffOfSale);
                                InProfit_Loss.Dr_Led_ID = string.Empty;
                                InProfit_Loss.Sub_Dr_Led_ID = string.Empty;
                            }
                        }
                        else   // Loss
                        {
                            InProfit_Loss.ItemID = model.SaleLoss_ItemID;
                            InProfit_Loss.Type = "DEBIT";
                            InProfit_Loss.Cr_Led_ID = string.Empty;
                            InProfit_Loss.Dr_Led_ID = model.SaleProfit_Loss_LedID;
                            InProfit_Loss.Amount = (Convert.ToDouble(1 * DiffOfSale) * -1);
                            InProfit_Loss.Sub_Dr_Led_ID = string.Empty;
                        }
                        InProfit_Loss.Sub_Cr_Led_ID = string.Empty;
                        InProfit_Loss.Mode = model.Cmd_Mode_SaleA;
                        InProfit_Loss.Ref_Bank = string.Empty;
                        InProfit_Loss.Ref_Branch = string.Empty;
                        InProfit_Loss.Ref_No = string.Empty;
                        InProfit_Loss.PartyID = model.GLookUp_PartyList1_SaleA;
                        InProfit_Loss.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA.ToString() + " and Profit (Or Loss) = " + DiffOfSale + "";       //changes as per pm.bkinfo.in/issues/5308#note-6
                        InProfit_Loss.Remarks = string.IsNullOrEmpty(model.Txt_Remarks_SaleA) ? string.Empty : model.Txt_Remarks_SaleA;
                        InProfit_Loss.Reference = string.IsNullOrEmpty(model.Txt_Reference_SaleA) ? string.Empty : model.Txt_Reference_SaleA;
                        InProfit_Loss.Tr_M_ID = model.xMID;
                        InProfit_Loss.TxnSrNo = 3;
                        InProfit_Loss.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                        InProfit_Loss.Status_Action = Status_Action;
                        InProfit_Loss.RecID = System.Guid.NewGuid().ToString();
                        EditParam.param_InsertPL = InProfit_Loss;
                        PL_Unposted = (PL_Unposted - Convert.ToDecimal(InProfit_Loss.Amount));
                        if ((PL_Unposted > 0) || (DiffOfSale < 0))
                        {
                            Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset InProfit_Loss_2 = new Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset();
                            InProfit_Loss_2.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Sale_Asset;
                            InProfit_Loss_2.VNo = model.Txt_V_NO_SaleA ?? string.Empty;
                            if (IsDate(model.Txt_V_Date_SaleA.ToString()))
                            {
                                InProfit_Loss_2.TDate = Convert.ToDateTime(model.Txt_V_Date_SaleA.ToString()).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InProfit_Loss_2.TDate = string.IsNullOrEmpty(model.Txt_V_Date_SaleA.ToString()) ? string.Empty : model.Txt_V_Date_SaleA.ToString();
                            }
                            InProfit_Loss_2.Cr_Led_ID = model.SaleProfit_Loss_LedID;
                            if (DiffOfSale > 0)     //  Profit
                            {
                                InProfit_Loss_2.ItemID = model.SaleProfit_ItemID;
                                InProfit_Loss_2.Type = "CREDIT";
                                InProfit_Loss_2.Cr_Led_ID = model.SaleProfit_Loss_LedID;
                                InProfit_Loss_2.Amount = Convert.ToDouble(PL_Unposted);
                                InProfit_Loss_2.Dr_Led_ID = string.Empty;
                            }
                            else   // Loss                        
                            {
                                InProfit_Loss_2.ItemID = model.GLookUp_ItemList_SaleA;
                                InProfit_Loss_2.Type = "CREDIT";
                                InProfit_Loss_2.Cr_Led_ID = Cr_Led_id;
                                InProfit_Loss_2.Dr_Led_ID = string.Empty;
                                InProfit_Loss_2.Amount = (Convert.ToDouble(1 * DiffOfSale) * -1);
                            }
                            InProfit_Loss_2.Sub_Cr_Led_ID = string.Empty;
                            InProfit_Loss_2.Sub_Dr_Led_ID = string.Empty;
                            InProfit_Loss_2.Mode = model.Cmd_Mode_SaleA;
                            InProfit_Loss_2.Ref_Bank = string.Empty;
                            InProfit_Loss_2.Ref_Branch = string.Empty;
                            InProfit_Loss_2.Ref_No = string.Empty;
                            InProfit_Loss_2.PartyID = model.GLookUp_PartyList1_SaleA;
                            InProfit_Loss_2.Narration = "Sale of " + model.GLookUp_AssetList_Text_SaleA + ". Cost = " + BalanceCost + ", Sale Amount = " + model.Txt_SaleAmt_SaleA.ToString() + " and Profit (Or Loss) = " + DiffOfSale + "";     //changes as per pm.bkinfo.in/issues/5308#note-6
                            InProfit_Loss_2.Remarks = string.IsNullOrEmpty(model.Txt_Remarks_SaleA) ? string.Empty : model.Txt_Remarks_SaleA;
                            InProfit_Loss_2.Reference = string.IsNullOrEmpty(model.Txt_Reference_SaleA) ? string.Empty : model.Txt_Reference_SaleA;
                            InProfit_Loss_2.Tr_M_ID = model.xMID;
                            InProfit_Loss_2.TxnSrNo = 3;
                            InProfit_Loss_2.Cross_Ref_ID = model.GLookUp_AssetList_SaleA;
                            InProfit_Loss_2.Status_Action = Status_Action;
                            InProfit_Loss_2.RecID = System.Guid.NewGuid().ToString();
                            EditParam.param_InsertPL_2 = InProfit_Loss_2;
                        }
                        // purpose
                        Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset InPurpose_PL = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset();
                        InPurpose_PL.TxnID = model.xMID;
                        InPurpose_PL.PurposeID = model.GLookUp_PurList_SaleA;
                        if (DiffOfSale > 0)
                        {
                            InPurpose_PL.Amount = Convert.ToDouble(DiffOfSale);
                        }
                        else
                        {
                            InPurpose_PL.Amount = (Convert.ToDouble(1 * DiffOfSale) * -1);
                        }

                        InPurpose_PL.Status_Action = Status_Action;
                        InPurpose_PL.RecID = System.Guid.NewGuid().ToString();
                        InPurpose_PL.SrNo = 3;
                        EditParam.param_InsertPurposePL = InPurpose_PL;
                    }
                    // purpose
                    Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset InPurp = new Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset();
                    InPurp.TxnID = model.xMID;
                    InPurp.PurposeID = model.GLookUp_PurList_SaleA;
                    InPurp.Amount = Convert.ToDouble(model.Txt_Amount_SaleA);
                    InPurp.Status_Action = Status_Action;
                    InPurp.RecID = System.Guid.NewGuid().ToString();
                    InPurp.SrNo = 1;
                    EditParam.param_InsertPurpose = InPurp;

                    //FCRA Update Process               
                    if (model.SaleA_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.SaleA_SplVchrReferenceSelected.Split(',');
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

                    if (!(bool)(BASE._SaleOfAsset_DBOps.UpdateSaleOfAsset_Txn(EditParam)))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Messages.UpdateSuccess;
                    jsonParam.title = "Sale of Asset";
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam, CashbookGridPK = model.xMID + model.xID }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_Delete_VoucherSaleOfAsset DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_VoucherSaleOfAsset();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)     // DELETE
                {
                    if ((model.Cmb_Asset_Type_SaleA.ToUpper() == "MOVABLE ASSETS") || (model.Cmb_Asset_Type_SaleA.ToUpper() == "LAND & BUILDING"))  // Movable Assets or Property
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Sorry! Changes cannot be done after the completion of Insurance Audit";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DelParam.Mid_DeleteItems = model.xMID;
                    DelParam.Mid_DeletePayment = model.xMID;
                    DelParam.Mid_DeletePurpose = model.xMID;
                    // PROFILE ENTRIES
                    DelParam.Mid_DeleteAdvances = model.xMID;
                    DelParam.Mid_Delete = model.xMID;
                    DelParam.Mid_DeleteMaster = model.xMID;
                    if (!(bool)(BASE._SaleOfAsset_DBOps.DeleteSaleOfAssets_Txn(DelParam)))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.DeleteSuccess;
                    jsonParam.title = "Sale of Asset";
                    jsonParam.result = false;
                    jsonParam.closeform = true;
                    jsonParam.refreshgrid = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message = "";
                jsonParam.title = "";
                jsonParam.result = true;
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
        private string FindLocationUsage(string PropertyID, bool Exclude_Sold_TF = true)
        {
            string Message = "";


            DataTable Locations = BASE._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, PropertyID);
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
                    Message = "Property being sold in this Voucher is being used in Another Page as Location. . . !" + "<br>" + "<br>" + "Name : " + UsedPage;
                    break;
                }
            }
            return Message;
        }
        
        #region "Start--> LookupEdit Events"
        public void LookUp_GetPurposeList_Refresh()
        {
            DataTable d1 = BASE._SaleOfAsset_DBOps.GetPurposes();
            SaleA_PurposeList_Data = DatatableToModel.DataTabletoSaleAssetPurposeList(d1);  
        }
        [HttpGet]
        public ActionResult LookUp_GetPurposeList_SaleAsset(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (SaleA_PurposeList_Data == null || DDRefresh == true)
            {
                LookUp_GetPurposeList_Refresh();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SaleA_PurposeList_Data, loadOptions)), "application/json");
        }
        public void LookUp_GetItemList_Refresh()
        {
            DataTable d1 = BASE._SaleOfAsset_DBOps.GetLedgerItems(BASE.Is_HQ_Centre);                
            DataView dview = new DataView(d1);                
            dview.Sort = "ITEM_NAME";                
            SaleA_Item_Data = DatatableToModel.DataTabletoSaleTransferItemList(dview.ToTable());
        }
        public ActionResult LookUp_GetItemList_SaleAsset(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (SaleA_Item_Data == null || DDRefresh == true)
            {
                LookUp_GetItemList_Refresh();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SaleA_Item_Data, loadOptions)), "application/json");
        }
        public void LookUp_GetRefBankList_Refresh()
        {
            DataTable B2 = BASE._SaleOfAsset_DBOps.GetBanks();            
            DataView dview = new DataView(B2);
            dview.Sort = "BI_BANK_NAME";
            SaleA_RefBankList_Data = DatatableToModel.DataTableToSaleAssetRefBankList(dview.ToTable());
        }
        public ActionResult LookUp_GetRefBankList_SaleAsset(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            // bank
            if (SaleA_RefBankList_Data == null || DDRefresh == true)
            {
                LookUp_GetRefBankList_Refresh();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SaleA_RefBankList_Data, loadOptions)), "application/json");
        }
        public void LookUp_GetBankList_Refresh()
        {
            DataTable BA_Table = BASE._SaleOfAsset_DBOps.GetBankAccounts();            
            string Branch_IDs = "";
            foreach (DataRow xRow in BA_Table.Rows)
            {
                Branch_IDs = (Branch_IDs + ("\'" + (xRow["BA_BRANCH_ID"].ToString() + "\',")));
            }
            if (Branch_IDs.Trim().Length > 0)
            {
                Branch_IDs = (Branch_IDs.Trim().EndsWith(",") ? Branch_IDs.Trim().ToString().Substring(0, (Branch_IDs.Trim().Length - 1)) : Branch_IDs.Trim().ToString());
            }
            if (Branch_IDs.Trim().Length == 0)
            {
                Branch_IDs = "\'\'";
            }
            DataTable BB_Table = BASE._SaleOfAsset_DBOps.GetBranchDetails(Branch_IDs);            
            var BuildData = from B in BB_Table.AsEnumerable()
                            join A in BA_Table.AsEnumerable()
            on B.Field<string>("BB_BRANCH_ID") equals A.Field<string>("BA_BRANCH_ID")
                            select new SaleAssetBankNameList
                            {
                                BANK_NAME = B.Field<string>("Name"),
                                BI_SHORT_NAME = B.Field<string>("BI_SHORT_NAME"),
                                BANK_BRANCH = B.Field<string>("Branch"),
                                BANK_ACC_NO = A.Field<string>("BA_ACCOUNT_NO"),
                                BA_ID = A.Field<string>("ID"),
                                BANK_ID = B.Field<string>("BANK_ID"),
                                REC_EDIT_ON = A.Field<DateTime>("REC_EDIT_ON"),
                            };

            var Final_Data = BuildData.ToList();
            Final_Data = Final_Data.OrderBy(x => x.BANK_NAME).ToList();
            SaleA_GetBankList_Data = Final_Data;
        }
        public ActionResult LookUp_GetBankList_SaleAsset(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (SaleA_GetBankList_Data == null || DDRefresh == true)
            {
                LookUp_GetBankList_Refresh();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SaleA_GetBankList_Data, loadOptions)), "application/json");
        }
        public void LookUp_GetPartyList_Refresh()
        {
            DataTable d1 = BASE._SaleOfAsset_DBOps.GetParties();
            DataRow ROW = d1.NewRow();
            ROW["C_NAME"] = "";
            d1.Rows.Add(ROW);            
            DataView dview = new DataView(d1);
            dview.Sort = "C_NAME";
            SaleA_GetPartyList_Data = DatatableToModel.DataTableToSaleAssetPartyList(dview.ToTable());            
        }
        public ActionResult LookUp_GetPartyList_SaleAsset(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (SaleA_GetPartyList_Data == null || DDRefresh == true)
            {
                LookUp_GetPartyList_Refresh();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SaleA_GetPartyList_Data, loadOptions)), "application/json");
        }
        public ActionResult GLookUp_Get_AssetList(DataSourceLoadOptions loadOptions, string Profile, string ActionMethod, string xMID, bool DDRefresh = false)
        {
            if (DDRefresh == true) 
            {
                Get_Asset_Items(Profile, ActionMethod, xMID);
            }
            if (SaleA_AssetList_Data == null)
            {
                SaleA_AssetList_Data = new List<SaleAsset_AssetList>();
            }               
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SaleA_AssetList_Data, loadOptions)), "application/json");           
        }

        #endregion // "End--> LookupEdit Events"

        [HttpGet]
        public ActionResult Get_Asset_Items(string Profile, string ActionMethod, string xMID)
        {
            if (string.IsNullOrWhiteSpace(Profile) == false)
            {
                Param_GetAssetListingForSale AssetParam = new Param_GetAssetListingForSale();
                AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
                AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
                if (Profile == "GOLD")
                {
                    AssetParam.Asset_Profile = AssetProfiles.GOLD;
                }
                else if (Profile == "SILVER")
                {
                    AssetParam.Asset_Profile = AssetProfiles.SILVER;
                }
                else if (Profile == "VEHICLES")
                {
                    AssetParam.Asset_Profile = AssetProfiles.VEHICLES;
                }
                else if (Profile == "LIVESTOCK")
                {
                    AssetParam.Asset_Profile = AssetProfiles.LIVESTOCK;
                }
                else if (Profile == "MOVABLE ASSETS")
                {
                    AssetParam.Asset_Profile = AssetProfiles.OTHER_ASSETS;
                }
                else if (Profile == "LAND & BUILDING")
                {
                    AssetParam.Asset_Profile = AssetProfiles.LAND_BUILDING;
                }

                if (ActionMethod == "_Edit" || ActionMethod == "_Delete"|| ActionMethod == "_View")
                {
                    AssetParam.TR_M_ID = xMID;
                }

                DataTable ASSET_TABLE = BASE._SaleOfAsset_DBOps.GetAllProfile_AssetList(AssetParam);
                if (ASSET_TABLE == null)
                {
                    return Json(new { message = Messages.SomeError, result = false }, JsonRequestBehavior.AllowGet);
                }
                SaleA_AssetList_Data = DatatableToModel.DataTabletoVoucherSaleAssetLookUp_GetAssetList(ASSET_TABLE);
                
            }
            else
            {
                SaleA_AssetList_Data = new List<SaleAsset_AssetList>();
                
            }
            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);

        }
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
            ClearBaseSession("_SaleAsset");         
        }
    }
}