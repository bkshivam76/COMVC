using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using ConnectOneMVC.Models;
using DevExpress.Web.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class WIPFinalizationController : BaseController
    {
        // GET: Account/WIPFinalization
        #region Global Variables
        public DataTable DT_WIPFinal
        {
            get { return (DataTable)GetBaseSession("DT_WIPFinal"); }
            set { SetBaseSession("DT_WIPFinal", value); }
        }
        public List<Common_Lib.DbOperations.Return_WIP_Ledger> GetWIPLedgerList
        {
            get { return (List<Common_Lib.DbOperations.Return_WIP_Ledger>)GetBaseSession("GetWIPLedgerList_WIPFinal"); }
            set { SetBaseSession("GetWIPLedgerList_WIPFinal", value); }
        }
        public DataTable Reference_Grid_ExportData
        {
            get { return (DataTable)GetBaseSession("Reference_Grid_ExportData_WIPFinal"); }
            set { SetBaseSession("Reference_Grid_ExportData_WIPFinal", value); }
        }
        public DataTable Select_Asset_ExportData
        {
            get { return (DataTable)GetBaseSession("Select_Asset_ExportData_WIPFinal"); }
            set { SetBaseSession("Select_Asset_ExportData_WIPFinal", value); }
        }
        public List<WIP_FinalizedAssetList> WIP_FinalizedAssetList
        {
            get { return (List<WIP_FinalizedAssetList>)GetBaseSession("WIP_FinalizedAssetList_WIPFinal"); }
            set { SetBaseSession("WIP_FinalizedAssetList_WIPFinal", value); }
        }
        public double? Txt_TotalAmount
        {
            get { return (double?)GetBaseSession("Txt_TotalAmount_WIPFinal"); }
            set { SetBaseSession("Txt_TotalAmount_WIPFinal", value); }
        }
        public List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose> PurposeList_WIPFnl
        {
            get
            {
                return (List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose>)GetBaseSession("PurposeList_WIPFinal");
            }
            set
            {
                SetBaseSession("PurposeList_WIPFinal", value);
            }
        }
        #endregion
        #region WIP_Finalization
        [HttpGet]
        public ActionResult Frm_Voucher_Win_WIP_Finalization(string Tag, string xID, string xMID, string Info_LastEditedOn, string iSpecific_ItemID, string GridToRefresh = "CashBookListGrid")
        {
            WIP_Finalization model = new WIP_Finalization();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = model.Tag.ToString();
            var i = 0;
            string[] Rights = { "Add", "Add", "Update", "View", "Delete" };
            Common.Navigation_Mode[] AM = { Common.Navigation_Mode._New, Common.Navigation_Mode._New_From_Selection, Common.Navigation_Mode._Edit, Common.Navigation_Mode._View, Common.Navigation_Mode._Delete };
            for (i = 0; i < Rights.Length; i++)
            {
                if (!CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_WIP_Finalization, Rights[i]) && model.Tag == AM[i])
                {
                    return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
                }
            }
            ViewBag.GridToRefresh = GridToRefresh;
            model.xID = xID;
            model.xMID = xMID;
            model.TitleX = "WIP Finalization";
            model.Me_Text = "New ~ " + model.TitleX;
            model.Rad_AssetType_WIPF = 0;
            model.AI_Type_WIPF = "ASSET";
            RefreshWIPLedgerList_WIPF();

            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_WIPF = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_WIPF = model.SpecialReferenceList_Data_WIPF.Count();

            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._View || model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Me_Text = model.ActionMethod.Substring(1) + " ~ " + model.TitleX;

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_WIPF = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                DataTable d1 = BASE._WIP_Finalization_DBOps.GetMasterRecord(model.xMID);
                DataTable d2 = BASE._WIP_Finalization_DBOps.GetRecord(model.xMID);
                DataTable d3 = BASE._WIP_Finalization_DBOps.GetFinalizedAmounts(model.xMID);
                if (d1 == null || d2 == null)
                {
                    string message = Messages.SomeError;
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + message + "','Error!!');</script>");
                }
                Data_Binding(d1, d2, d3, ref model);
            }
            model.Saving_From_Asset_Window = false;
            model.Saving_From_Select_Asset = false;
            model.PurposeID_WIPF = BASE._WIP_Finalization_DBOps.GetPurposeID(xMID);
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_WIP_Finalization_Window(WIP_Finalization model)
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
                if (model.Saving_From_Asset_Window == false && model.Saving_From_Select_Asset == false)
                {
                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection || model.Tag == Common.Navigation_Mode._Edit)
                    {
                        if (string.IsNullOrWhiteSpace(model.GLookUp_WIPLedgerList_WIPF) || model.GLookUp_WIPLedgerList_WIPF.Trim().Length == 0)
                        {
                            jsonParam.message = "WIP Ledger Not Selected. . .!";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "GLookUp_WIPLedgerList_WIPF";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_V_Date_WIPF.ToString()) == false)
                        {
                            jsonParam.message = "Date Incorrect / Blank. . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_V_Date_WIPF";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (IsDate(model.Txt_V_Date_WIPF.ToString()) == true)
                        {
                            // 1    //2
                            if (model.Txt_V_Date_WIPF < BASE._open_Year_Sdt || model.Txt_V_Date_WIPF > BASE._open_Year_Edt)
                            {
                                jsonParam.message = "Date not as per Financial Year...!";
                                jsonParam.result = false;
                                jsonParam.title = "Incomplete Information . . .";
                                jsonParam.focusid = "Txt_V_Date_WIPF";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (string.IsNullOrWhiteSpace(model.GLookUp_FinalizedAssetList_WIPF) || model.GLookUp_FinalizedAssetList_WIPF.Trim().Length == 0)
                        {
                            jsonParam.message = "Finalized Asset Not Selected. . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "GLookUp_FinalizedAssetList_WIPF";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_TotalAmount_WIPF <= 0)
                        {
                            jsonParam.message = "Amount cannot be Zero / Negative. . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_TotalAmount_WIPF";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (string.IsNullOrWhiteSpace(model.PurposeID_WIPF))
                        {
                            jsonParam.message = "Purpose is Required. . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "PurposeID_WIPF";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection || model.Tag == Common.Navigation_Mode._Edit)
                    {
                        //'check wip creation date                       
                        for (int I = 0; I < Reference_Grid_ExportData.Rows.Count; I++)     //Insert one row for each reference in txn_info
                        {
                            if (Convert.ToDouble(Reference_Grid_ExportData.Rows[I]["Finalized Amount"]) > 0)
                            {
                                DataTable PROFILE_TABLE = CommonFunctions.GetReferenceData(BASE, "WIP", model.Txn_Cr_ItemId_WIPF, model.xMID, "", model.Tag, (string)Reference_Grid_ExportData.Rows[I]["Profile_WIP_RecID"]);
                                if (PROFILE_TABLE != null && PROFILE_TABLE.Rows.Count > 0)
                                {
                                    DateTime CreationDate = IsDate(PROFILE_TABLE.Rows[0]["REF_CREATION_DATE"].ToString()) == false ? BASE._open_Year_Sdt : Convert.ToDateTime(PROFILE_TABLE.Rows[0]["REF_CREATION_DATE"]);
                                    if (model.Txt_V_Date_WIPF < CreationDate)
                                    {
                                        jsonParam.message = "Current Reference Voucher Date cannot be less than Creation Voucher dated " + CreationDate.ToLongDateString() + " for " + Reference_Grid_ExportData.Rows[I]["Reference"] + ". . . !";
                                        jsonParam.result = false;
                                        jsonParam.title = "Incomplete Information . . .";
                                        jsonParam.focusid = "Txt_V_Date_WIPF";
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }

                        if (model.Rad_AssetType_WIPF == 0)  //New Asset 
                        {                           
                            jsonParam.result = true;
                            jsonParam.popup_title = model.Me_Text + " (Movable Asset Detail)...";
                            jsonParam.popup_form_name = "Frm_Asset_Window";
                            jsonParam.popup_form_path = "/Account/ProfilePaymentVoucher/Frm_Asset_Window/";
                            jsonParam.popup_querystring = "BE_ItemName=" + Url.Encode(model.Finalized_Asset_text);    
                            DateTime? xDate = null;
                            if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                            {
                                xDate = Convert.ToDateTime(Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short));
                            }
                            else
                            {
                                xDate = model.Txt_V_Date_WIPF;
                            }
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Date=" + Url.Encode(xDate.ToString());

                            if (model.Tag == Common.Navigation_Mode._New)
                            {
                                jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._New + "&Txt_Make=--" +
                                "&Txt_Model=--"+"&Txt_Amt=" + model.Txt_TotalAmount_WIPF;                              
                            }
                            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._View)
                            {
                                if (model.Tag == Common.Navigation_Mode._Edit)
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._Edit;
                                }
                                else
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring + "&Tag=" + Common.Navigation_Mode._View;
                                }
                                if (model.Asset_Item_ID_WIPF == model.GLookUp_FinalizedAssetList_WIPF)  //Same item getting updated                                                                                                
                                {
                                    if (DT_WIPFinal.Rows.Count > 0)
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Amt=" + model.Txt_TotalAmount_WIPF +
                                            "&Txt_Make=" + Url.Encode(DT_WIPFinal.Rows[0]["AI_MAKE"].ToString()) +
                                            "&Txt_Model=" + Url.Encode(DT_WIPFinal.Rows[0]["AI_MODEL"].ToString()) +
                                            "&Txt_Serial=" + Url.Encode(DT_WIPFinal.Rows[0]["AI_SERIAL_NO"].ToString()) +
                                            "&Txt_Warranty=" + Url.Encode(DT_WIPFinal.Rows[0]["AI_WARRANTY"].ToString()) +
                                            "&AI_PUR_DATE=" + Url.Encode(model.Txt_V_Date_WIPF.ToString()) +
                                            "&Txt_Rate=" + Url.Encode(DT_WIPFinal.Rows[0]["ai_rate"].ToString()) +
                                            "&Txt_Qty=" + Url.Encode(DT_WIPFinal.Rows[0]["QTY"].ToString()) +
                                            "&Txt_Others=" + Url.Encode(DT_WIPFinal.Rows[0]["AI_OTHER_DETAIL"].ToString()) +
                                            "&AI_LOC_AL_ID=" + Url.Encode(DT_WIPFinal.Rows[0]["AI_LOC_AL_ID"].ToString());
                                    }
                                    else 
                                    {
                                        jsonParam.popup_querystring = jsonParam.popup_querystring + "&Txt_Make=--" +
                                            "&Txt_Model=--";
                                    }
                                }
                                else     //item has been changed by user
                                {
                                    jsonParam.popup_querystring = jsonParam.popup_querystring +"&Txt_Make=--" +                                
                                        "&Txt_Model=--" + "&Txt_Amt=" + model.Txt_TotalAmount_WIPF;
                                }
                            }                              
                            jsonParam.popup_querystring = jsonParam.popup_querystring + "&Asset_Window_FromWIPFinalization=" + true;                               
                            return Json(new { jsonParam, Asset_Window = true }, JsonRequestBehavior.AllowGet);                            
                        }
                        else
                        {                            
                            jsonParam.result = true;
                            jsonParam.popup_title = "Select Asset";
                            jsonParam.popup_form_name = "Frm_WIPFinal_Select_Asset";
                            jsonParam.popup_form_path = "/Account/WIPFinalization/Frm_WIPFinal_Select_Asset/";
                            jsonParam.popup_querystring = "FinalizedAsset_Item_ID=" + Url.Encode(model.GLookUp_FinalizedAssetList_WIPF) +
                                "&Tr_M_ID=" + Url.Encode(model.xMID);
                            return Json(new { jsonParam, Select_Asset = true }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection || model.Tag == Common.Navigation_Mode._Edit)
                {
                    if ((model.Rad_AssetType_WIPF == 0 ? "" : model.Existing_Asset_RecID_Profile ?? "").Length > 0)
                    {
                        DataTable PROFILE_TABLE1 = CommonFunctions.GetReferenceData(BASE, "OTHER ASSETS", model.GLookUp_FinalizedAssetList_WIPF, model.xMID, "", model.Tag, model.Existing_Asset_RecID_Profile);
                        DateTime CreationDate1 = IsDate(PROFILE_TABLE1.Rows[0]["REF_CREATION_DATE"].ToString()) == false ? BASE._open_Year_Sdt : Convert.ToDateTime(PROFILE_TABLE1.Rows[0]["REF_CREATION_DATE"]);
                        Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate inparam = new Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate();
                        inparam.Creation_Date = IsDate(CreationDate1.ToString()) == false ? BASE._open_Year_Sdt : CreationDate1;
                        inparam.Asset_RecID = model.Existing_Asset_RecID_Profile;
                        inparam.YearID = BASE._open_Year_ID;
                        inparam.Tr_M_ID = model.xMID;
                        var MxDate = BASE._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam);
                        if (MxDate == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.result = false;
                            jsonParam.title = "Error";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Txt_V_Date_WIPF < Convert.ToDateTime(MxDate))
                        {
                            jsonParam.message = "Finalization Voucher Date cannot be less than previous transaction on Referenced asset dated " + Convert.ToDateTime(MxDate).ToLongDateString() + " . . . !";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_V_Date_WIPF";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                Common_Lib.RealTimeService.Param_Txn_Insert_Voucher_WIPFinalization InNewParam = new Common_Lib.RealTimeService.Param_Txn_Insert_Voucher_WIPFinalization();
                int J;
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection) //new
                {
                    model.xMID = System.Guid.NewGuid().ToString();
                    Common_Lib.RealTimeService.Param_InsertMasterInfo_Voucher_WIPFinalization InMInfo = new Common_Lib.RealTimeService.Param_InsertMasterInfo_Voucher_WIPFinalization();
                    InMInfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization;
                    InMInfo.VNo = model.Txt_V_NO_WIPF ?? "";
                    if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                    {
                        InMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMInfo.TDate = model.Txt_V_Date_WIPF.ToString();
                    }
                    InMInfo.TotalAmount = model.Txt_TotalAmount_WIPF;
                    InMInfo.Status_Action = Status_Action;
                    
                    InMInfo.RecID = model.xMID;
                    InNewParam.param_InsertMaster = InMInfo;
                
                    if (model.Rad_AssetType_WIPF == 0)  //New Asset 
                    {
                        //Insert New Asset in Profile
                        Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets InParam = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets();
                        InParam.AssetType = model.AI_Type_WIPF ?? "";
                        InParam.ItemID = model.GLookUp_FinalizedAssetList_WIPF ?? "";
                        InParam.Make = model.AI_MAKE_WIPF ?? "";
                        InParam.Model = model.AI_MODEL_WIPF ?? "";
                        InParam.SrNo = model.AI_SERIAL_NO_WIPF ?? "";
                        InParam.Rate = Convert.ToDouble(model.AI_RATE_WIPF);
                        InParam.InsAmount = Convert.ToDouble(model.Txt_TotalAmount_WIPF);

                        if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                        {
                            InParam.PurchaseDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.PurchaseDate = model.Txt_V_Date_WIPF.ToString();
                        }

                        InParam.PurchaseAmount = model.AI_Type_WIPF == "ASSET" ? model.Txt_TotalAmount_WIPF : 0;
                        InParam.Warranty = Convert.ToDouble(model.AI_WARRANTY_WIPF);
                        InParam.Quantity = Convert.ToDouble(model.QTY_WIPF);
                        InParam.LocationId = model.X_LOC_ID_WIPF ?? "";
                        InParam.OtherDetails = model.Other_Details_WIPF ?? "";
                        InParam.TxnID = model.xMID ?? "";
                        InParam.TxnSrNo = 1;  
                        InParam.Status_Action = Status_Action;
                        InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_WIP_Finalization;

                        InNewParam.inAssets_Insert = InParam;
                    }

                    Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization[] InsertReferences = new Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization[Reference_Grid_ExportData.Rows.Count];

                    int cnt = 0;
                    //Credit References
                    for (J = 0; J < Reference_Grid_ExportData.Rows.Count; J++)        //Insert one row for each reference in txn_info
                    {
                        if (Convert.ToDouble(Reference_Grid_ExportData.Rows[J]["Finalized Amount"]) > 0)
                        {
                            Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization InParamCr = new Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization();
                            InParamCr.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization;
                            InParamCr.VNo = model.Txt_V_NO_WIPF ?? "";

                            if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                            {
                                InParamCr.TDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParamCr.TDate = model.Txt_V_Date_WIPF.ToString();
                            }

                            InParamCr.ItemID = model.Txn_Cr_ItemId_WIPF;
                            InParamCr.Type = "CREDIT";
                            InParamCr.Dr_Led_ID = "";
                            InParamCr.Cr_Led_ID = model.GLookUp_WIPLedgerList_WIPF;
                            InParamCr.Finalized_Amount = Convert.ToDouble(Reference_Grid_ExportData.Rows[J]["Finalized Amount"]);
                            InParamCr.Mode = "JOURNAL";
                            InParamCr.Narration = model.Txt_Narration_WIPF ?? "";
                            InParamCr.Reference = model.Txt_Reference_WIPF ?? "";
                            InParamCr.CrossRefID = Reference_Grid_ExportData.Rows[J]["Profile_WIP_RecID"].ToString();
                            InParamCr.MasterTxnID = model.xMID ?? "";
                            InParamCr.SrNo = 1.ToString();
                            InParamCr.Status_Action = Status_Action;
                            InParamCr.RecID = Guid.NewGuid().ToString();
                            InsertReferences[cnt] = InParamCr;
                            cnt += 1;
                        }
                    }

                    InNewParam.InsertCr = InsertReferences;

                    //Debit asset ledger
                    Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization InParamDr = new Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization();
                    InParamDr.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization;
                    InParamDr.VNo = model.Txt_V_NO_WIPF??"";

                    if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                    {
                        InParamDr.TDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParamDr.TDate = model.Txt_V_Date_WIPF.ToString();
                    }

                    InParamDr.ItemID = model.GLookUp_FinalizedAssetList_WIPF;
                    InParamDr.Type = "DEBIT";

                    if (Convert.ToDouble(model.Txt_TotalAmount_WIPF) > model.iMinValue_WIPF && Convert.ToDouble(model.Txt_TotalAmount_WIPF) <= model.iMaxValue_WIPF)
                    {
                        InParamDr.Dr_Led_ID = model.iCond_Ledger_ID_WIPF;
                    }
                    else
                    {
                        InParamDr.Dr_Led_ID = model.AI_LED_ID_WIPF;
                    }

                    if (model.Rad_AssetType_WIPF == 1)
                    {
                        InParamDr.Dr_Led_ID = model.AI_LED_ID_WIPF;
                    }

                    InParamDr.Cr_Led_ID = "";
                    InParamDr.Finalized_Amount = model.Txt_TotalAmount_WIPF;
                    InParamDr.Mode = "JOURNAL";
                    InParamDr.Narration = model.Txt_Narration_WIPF ?? "";
                    InParamDr.Reference = model.Txt_Reference_WIPF ?? "";
                    InParamDr.CrossRefID = model.Rad_AssetType_WIPF == 0 ? "" : model.Existing_Asset_RecID_Profile??"";     //Profile_asset_rec_id
                    InParamDr.MasterTxnID = model.xMID;
                    InParamDr.SrNo = 2.ToString();
                    InParamDr.Status_Action = Status_Action;
                    InParamDr.RecID = Guid.NewGuid().ToString();
                    //InParamDr.PurposeID = model.PurposeID_WIPF;
                    InNewParam.InsertDr = InParamDr;
                    InNewParam.PurposeID = model.PurposeID_WIPF;

                    //FCRA Insert Process
                    if (model.WIPF_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.WIPF_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._WIP_Finalization_DBOps.Insert_WIP_Finalization_Txn(InNewParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    model.xID = string.IsNullOrWhiteSpace(model.xID) ? InParamDr.RecID : model.xID;
                    jsonParam.message = Common_Lib.Messages.SaveSuccess;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, CashbookGridPK = model.xMID+model.xID }, JsonRequestBehavior.AllowGet);
                }
                Common_Lib.RealTimeService.Param_Txn_Update_Voucher_WIPFinalization EditParam = new Common_Lib.RealTimeService.Param_Txn_Update_Voucher_WIPFinalization();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Edit) //Edit
                {
                    Common_Lib.RealTimeService.Parameter_UpdateMaster_Voucher_WIPFinalization UpMInfo = new Common_Lib.RealTimeService.Parameter_UpdateMaster_Voucher_WIPFinalization();
                    UpMInfo.VNo = model.Txt_V_NO_WIPF ?? "";

                    if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                    {
                        UpMInfo.TDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMInfo.TDate = model.Txt_V_Date_WIPF.ToString();
                    }

                    UpMInfo.Finalized_Amount =model.Txt_TotalAmount_WIPF;

                    UpMInfo.RecID = model.xMID ?? "";

                    EditParam.Udpate_Master = UpMInfo;

                    EditParam.MID_DeleteAssets = model.xMID ?? "";


                    if (model.Rad_AssetType_WIPF == 0) //New Asset
                    { //Insert New Asset in Profile
                        Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets InParam = new Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets();
                        InParam.AssetType = model.AI_Type_WIPF ?? "";
                        InParam.ItemID = model.GLookUp_FinalizedAssetList_WIPF ?? "";
                        InParam.Make = model.AI_MAKE_WIPF ?? "";  //xfrm.Txt_Make.Text
                        InParam.Model = model.AI_MODEL_WIPF ?? "";  //xfrm.Txt_Model.Text
                        InParam.SrNo = model.AI_SERIAL_NO_WIPF ?? "";
                        InParam.Rate = Convert.ToDouble(model.AI_RATE_WIPF);
                        InParam.InsAmount =model.Txt_TotalAmount_WIPF;

                        if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                        {
                            InParam.PurchaseDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                        }
                        else
                        {
                            InParam.PurchaseDate = model.Txt_V_Date_WIPF.ToString();
                        }

                        InParam.PurchaseAmount = model.AI_Type_WIPF == "ASSET" ? model.Txt_TotalAmount_WIPF : 0; //'IIf(InParam.AssetType.ToUpper = "ASSET", IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")), 0) 'Bug #5046 fix, http://pm.bkinfo.in/issues/5345#note-12
                        InParam.Warranty = Convert.ToDouble(model.AI_WARRANTY_WIPF);
                        InParam.Quantity = Convert.ToDouble(model.QTY_WIPF);
                        InParam.LocationId = model.X_LOC_ID_WIPF;
                        InParam.OtherDetails = model.Other_Details_WIPF;
                        InParam.TxnID = model.xMID ?? "";
                        InParam.TxnSrNo = 1;
                        InParam.Status_Action = Status_Action;
                        InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_WIP_Finalization;

                        EditParam.inAssets_Insert = InParam;
                    }

                    //Delete Txn Info
                    EditParam.MID_Delete = model.xMID;

                    Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization[] InsertReferences = new Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization[Reference_Grid_ExportData.Rows.Count];
                    
                    int cnt = 0;
                    //Credit References
                    for (J = 0; J < Reference_Grid_ExportData.Rows.Count; J++) //Insert one row for each reference in txn_info
                    {
                        if (Convert.ToDouble(Reference_Grid_ExportData.Rows[J]["Finalized Amount"]) > 0)
                        {
                            Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization InParamCr = new Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization();
                            InParamCr.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization;
                            InParamCr.VNo = model.Txt_V_NO_WIPF ?? "";
                            if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                            {
                                InParamCr.TDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                            }
                            else
                            {
                                InParamCr.TDate = model.Txt_V_Date_WIPF.ToString();
                            }

                            InParamCr.ItemID = model.Txn_Cr_ItemId_WIPF;
                            InParamCr.Type = "CREDIT";
                            InParamCr.Dr_Led_ID = "";
                            InParamCr.Cr_Led_ID = model.GLookUp_WIPLedgerList_WIPF;
                            InParamCr.Finalized_Amount = Convert.ToDouble(Reference_Grid_ExportData.Rows[J]["Finalized Amount"]);
                            InParamCr.Mode = "JOURNAL";
                            InParamCr.Narration = model.Txt_Narration_WIPF ?? "";
                            InParamCr.Reference = model.Txt_Reference_WIPF ?? "";
                            InParamCr.CrossRefID = Reference_Grid_ExportData.Rows[J]["Profile_WIP_RecID"].ToString();
                            InParamCr.MasterTxnID = model.xMID ?? "";
                            InParamCr.SrNo = 1.ToString();
                            InParamCr.Status_Action = Status_Action;
                            InParamCr.RecID = Guid.NewGuid().ToString();

                            InsertReferences[cnt] = InParamCr;
                            cnt += 1;
                        }
                    }

                    EditParam.InsertCr = InsertReferences;

                    //Debit asset ledger
                    Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization InParamDr = new Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization();
                    InParamDr.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization;
                    InParamDr.VNo = model.Txt_V_NO_WIPF ?? "";
                    if (IsDate(model.Txt_V_Date_WIPF.ToString()))
                    {
                        InParamDr.TDate = Convert.ToDateTime(model.Txt_V_Date_WIPF).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParamDr.TDate = model.Txt_V_Date_WIPF.ToString();
                    }
                    InParamDr.ItemID = model.GLookUp_FinalizedAssetList_WIPF ?? "";
                    InParamDr.Type = "DEBIT";
                    if (model.Txt_TotalAmount_WIPF > model.iMinValue_WIPF && model.Txt_TotalAmount_WIPF <= model.iMaxValue_WIPF)
                    {
                        InParamDr.Dr_Led_ID = model.iCond_Ledger_ID_WIPF ?? "";
                    }
                    else
                    {
                        InParamDr.Dr_Led_ID = model.AI_LED_ID_WIPF ?? "";
                    }

                    if (model.Rad_AssetType_WIPF == 1)
                    {
                        InParamDr.Dr_Led_ID = model.AI_LED_ID_WIPF ?? "";
                    }

                    InParamDr.Cr_Led_ID = "";
                    InParamDr.Finalized_Amount = model.Txt_TotalAmount_WIPF;
                    InParamDr.Mode = "JOURNAL";
                    InParamDr.Narration = model.Txt_Narration_WIPF ?? "";
                    InParamDr.Reference = model.Txt_Reference_WIPF ?? "";
                    InParamDr.CrossRefID = model.Rad_AssetType_WIPF == 0 ? "" : model.Existing_Asset_RecID_Profile??""; //Profile_asset_rec_id
                    InParamDr.MasterTxnID = model.xMID ?? "";
                    InParamDr.SrNo = 2.ToString();
                    InParamDr.Status_Action = Status_Action;
                    
                    InParamDr.RecID = Guid.NewGuid().ToString();

                    EditParam.InsertDr = InParamDr;
                    EditParam.PurposeID = model.PurposeID_WIPF;

                    //FCRA Update Process               
                    if (model.WIPF_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.WIPF_SplVchrReferenceSelected.Split(',');
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

                    if (!BASE._WIP_Finalization_DBOps.Update_WIP_Finalization_Txn(EditParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    model.xID = InParamDr.RecID;
                    jsonParam.message = Common_Lib.Messages.UpdateSuccess;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam,CashbookGridPK = model.xMID + model.xID }, JsonRequestBehavior.AllowGet);
                }

                Common_Lib.RealTimeService.Param_Txn_Delete_Voucher_WIPFinalization DelParam = new Common_Lib.RealTimeService.Param_Txn_Delete_Voucher_WIPFinalization();
                if (model.Tag == Common_Lib.Common.Navigation_Mode._Delete)     //DELETE
                {
                    //PROFILE ENTRIES DELETE
                    DelParam.MID_DeleteAssets = model.xMID ?? "";
                    DelParam.MID_Delete = model.xMID ?? "";
                    DelParam.MID_DeleteMaster = model.xMID ?? "";

                    if (!BASE._WIP_Finalization_DBOps.Delete_WIP_Finalization_Txn(DelParam))
                    {
                        jsonParam.message = Common_Lib.Messages.SomeError;
                        jsonParam.title = model.TitleX;
                        jsonParam.result = false;
                        jsonParam.closeform = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Common_Lib.Messages.DeleteSuccess;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                jsonParam.message =Messages.SomeError;
                jsonParam.title = "Error..";
                jsonParam.result = false;
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
        public void Ledger_Outstanding_References(string WIP_LED_ID = "", string ActionMethod = "", string xMID = "")
        {
            Common_Lib.RealTimeService.Param_Get_WIP_Outstanding_References Param = new Common_Lib.RealTimeService.Param_Get_WIP_Outstanding_References();
            Param.Prev_YearId = BASE._prev_Unaudited_YearID;
            Param.Next_YearID = BASE._next_Unaudited_YearID;
            Param.WIP_LED_ID = WIP_LED_ID;
            if (ActionMethod == "_Edit" || ActionMethod == "_View" || ActionMethod == "_Delete")
            {
                Param.TR_M_ID = xMID;
            }
            Reference_Grid_ExportData = BASE._WIP_Finalization_DBOps.Get_WIP_Outstanding_References(Param);         
        }
        public ActionResult Frm_WIPFinal_Reference_Listing_Grid(string WIP_LED_ID = null, string ActionMethod = "", string xMID = "", string command = "")
        {
            if (command == "REFRESH")
            {
                if (string.IsNullOrEmpty(WIP_LED_ID))
                {
                    Reference_Grid_ExportData = null;
                }
                else
                {
                    Ledger_Outstanding_References(WIP_LED_ID, ActionMethod, xMID);
                }
                Sub_Amt_Calculation();
            }
            ViewData["TotalAmount"] = Txt_TotalAmount;
            return PartialView(Reference_Grid_ExportData);
        }
        public ActionResult WIPFin_Ref_Listing_CustomDataAction(int? key)
        {
            string itstr = "";
            if (key != null)
            {
                DataRow[] row = Reference_Grid_ExportData.Select("[Sr.] =" + key);

                if (row.Length > 0)
                {
                    itstr = row[0]["Sr."] + "![" + row[0]["WIP_LED_ID"] + "![" + row[0]["Reference"] + "![" + row[0]["OPENING"] + "![" + row[0]["WIP_Amount"] + "![" + row[0]["Next_Year_WIP_Amount"] + "![" + row[0]["Finalized Amount"] +
                        "![" + row[0]["Date of Creation"] + "![" + row[0]["Profile_WIP_RecID"];
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public void Data_Binding(DataTable d1, DataTable d2, DataTable d3, ref WIP_Finalization model)
        {
            DateTime? xDate = null;
            xDate = Convert.ToDateTime(d1.Rows[0]["TR_DATE"].ToString());
            model.Txt_V_Date_WIPF = xDate;
            model.Txt_V_NO_WIPF = d1.Rows[0]["TR_VNO"].ToString();
            DT_WIPFinal = BASE._WIP_Finalization_DBOps.GetAssetList(model.xMID);
            if (DT_WIPFinal.Rows.Count > 0)
            {
                model.Rad_AssetType_WIPF = 0;
                model.Asset_Item_ID_WIPF = DT_WIPFinal.Rows[0]["AI_ITEM_ID"].ToString();
            }
            else
            {
                model.Rad_AssetType_WIPF = 1;
            }
            if (d2.Rows.Count > 0)
            {
                DataRow[] DataRowDr = d2.Select("TR_TYPE='DEBIT'");
                DataRow[] DataRowCr = d2.Select("TR_TYPE='CREDIT'");
                foreach (DataRow Row in DataRowCr)
                {
                    if (Row["TR_CR_LED_ID"].ToString().Length > 0)
                    {
                        model.GLookUp_WIPLedgerList_WIPF = Row["TR_CR_LED_ID"].ToString();
                        break;
                    }
                }
                if (DataRowDr[0]["TR_ITEM_ID"].ToString().Length > 0)
                {
                    model.GLookUp_FinalizedAssetList_WIPF = DataRowDr[0]["TR_ITEM_ID"].ToString();
                }
            }
            RefreshFinalizedAssetList_WIPF(model.GLookUp_WIPLedgerList_WIPF);
            Ledger_Outstanding_References(model.GLookUp_WIPLedgerList_WIPF, model.ActionMethod, model.xMID);
            if (Reference_Grid_ExportData.Rows.Count > 0)
            {
                for (int i = 0; i < Reference_Grid_ExportData.Rows.Count; i++)
                {
                    foreach (DataRow XRow in d3.Rows)
                    {
                        if (Reference_Grid_ExportData.Rows[i].Field<string>("Profile_WIP_RecID") == XRow["TR_TRF_CROSS_REF_ID"].ToString())
                        {
                            Reference_Grid_ExportData.Rows[i]["Finalized Amount"] = Convert.ToDecimal(XRow["TR_AMOUNT"]);
                        }
                    }
                }
            }
            model.Txt_Narration_WIPF = d2.Rows[0]["TR_NARRATION"].ToString();
            model.Txt_Reference_WIPF = d2.Rows[0]["TR_REFERENCE"].ToString();
            Sub_Amt_Calculation();
            model.Txt_TotalAmount_WIPF = Txt_TotalAmount??0;
        }
        #endregion
        #region Lookup Functions
        public ActionResult LookUp_GetFinalizedAssetList_WIPF(DataSourceLoadOptions loadOptions, bool DDRefresh = false, string WIP_Led_ID = "")
        {
            if (WIP_FinalizedAssetList == null || DDRefresh == true)
            {
                RefreshFinalizedAssetList_WIPF(WIP_Led_ID);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(WIP_FinalizedAssetList, loadOptions)), "application/json");
        }
        public ActionResult LookUp_GetWIPLedgerList_WIPF(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetWIPLedgerList == null || DDRefresh == true)
            {
                RefreshWIPLedgerList_WIPF();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetWIPLedgerList, loadOptions)), "application/json");
        }
        public void RefreshFinalizedAssetList_WIPF(string WIP_Led_ID = "")
        {
            DataTable d1 = BASE._WIP_Finalization_DBOps.GetListOfFinalizedAssets(BASE._open_Ins_ID, WIP_Led_ID);
            if (d1 == null)
            {
                WIP_FinalizedAssetList = new List<WIP_FinalizedAssetList>();
            }
            else
            {
                DataView dview = new DataView(d1);
                var data = DatatableToModel.DataTableto_GetFinalizedAssetList(dview.ToTable());
                WIP_FinalizedAssetList = data;
            }
        }
        public void RefreshWIPLedgerList_WIPF()
        {
            var d1 = BASE._WIP_Finalization_DBOps.GetListOfWIPLedgers(BASE.Is_HQ_Centre);
            if (d1 == null)
            {
                GetWIPLedgerList = new List<DbOperations.Return_WIP_Ledger>();
            }
            else
            {
                d1 = d1.OrderBy(x => x.WIP_LEDGER).ToList();
                GetWIPLedgerList = d1;
            }
        }

        #region Lookup_GetPurposeList
        public ActionResult LookUp_GetPurposeList(DataSourceLoadOptions loadOptions)
        {
            List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose> PurposeList = null;
            if (PurposeList_WIPFnl == null)
            {
                PurposeList = BASE._Donation_DBOps.GetPurposes();
                PurposeList_WIPFnl = PurposeList;
            }
            PurposeList = PurposeList_WIPFnl;
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PurposeList, loadOptions)), "application/json");
        }
        #endregion
        #endregion

        #region Item Detail
        [HttpGet]
        public ActionResult Frm_Voucher_WIP_Item_Detail(string ActionMethod = "", string xMID = "", int Sr = 0)
        {
            WIPFinal_Item_Detail model = new WIPFinal_Item_Detail();
            model.Tag_ItemDetail_WIPF = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), "_" + ActionMethod, true);
            if (Reference_Grid_ExportData.Rows.Count > 0)
            {
                if (model.Tag_ItemDetail_WIPF == Common.Navigation_Mode._View)
                {
                    model.Title_ItemDetail_WIPF = "View ~ Item Detail";
                    model.ActionMethod_ItemDetail_WIPF = "_View";
                }
                else
                {
                    model.Title_ItemDetail_WIPF = "Edit ~ Item Detail";
                    model.ActionMethod_ItemDetail_WIPF = "_Edit";
                }
                model.iTxnM_ID_ItemDetail_WIPF = xMID;
                model.Grid_PK = Sr;
                DataRow[] row = Reference_Grid_ExportData.Select("[Sr.] =" + Sr);
                if (row.Length > 0)
                {
                    model.Txt_Reference_ItemDetail_WIPF = Convert.ToString(row[0]["Reference"]);
                    model.Txt_Amount_ItemDetail_WIPF = Convert.ToString(row[0]["WIP_Amount"]);
                    model.Next_year_WIP_Amount_ItemDetail_WIPF = Convert.ToString(row[0]["Next_Year_WIP_Amount"]);
                    model.Txt_Finalized_Amount_ItemDetail_WIPF = Convert.ToDecimal(row[0]["Finalized Amount"]);
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Frm_Voucher_WIP_Item_Detail(WIPFinal_Item_Detail model)
        {
            model.Tag_ItemDetail_WIPF = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod_ItemDetail_WIPF);
            Return_Json_Param jsonparam = new Return_Json_Param();
            if (model.Tag_ItemDetail_WIPF == Common_Lib.Common.Navigation_Mode._Edit)
            {
                if (model.Txt_Finalized_Amount_ItemDetail_WIPF < 0)
                {
                    jsonparam.message = "Amount cannot be Negative...!";
                    jsonparam.title = "Incomplete Information...";
                    jsonparam.result = false;
                    jsonparam.focusid = "Txt_Finalized_Amount_ItemDetail_WIPF";
                    return Json(new { jsonparam }, JsonRequestBehavior.AllowGet);
                }
                if (model.Txt_Finalized_Amount_ItemDetail_WIPF > Convert.ToDecimal(model.Txt_Amount_ItemDetail_WIPF))
                {
                    jsonparam.message = "Finalized Amount Should Be Less Than Or Equal To WIP Amount...!";
                    jsonparam.title = "Incomplete Information...";
                    jsonparam.result = false;
                    jsonparam.focusid = "Txt_Finalized_Amount_ItemDetail_WIPF";
                    return Json(new { jsonparam }, JsonRequestBehavior.AllowGet);
                }
                if (model.Next_year_WIP_Amount_ItemDetail_WIPF.Length > 0)
                {
                    if (model.Txt_Finalized_Amount_ItemDetail_WIPF > Convert.ToDecimal(model.Next_year_WIP_Amount_ItemDetail_WIPF))
                    {
                        jsonparam.message = "Finalized Amount Should Be Less Than Or Equal To WIP Amount in Next Year (" + model.Next_year_WIP_Amount_ItemDetail_WIPF + ")...!";
                        jsonparam.title = "Incomplete Information...";
                        jsonparam.result = false;
                        jsonparam.focusid = "Txt_Finalized_Amount_ItemDetail_WIPF";
                        return Json(new { jsonparam }, JsonRequestBehavior.AllowGet);
                    }
                }
                DataRow[] row = Reference_Grid_ExportData.Select("[Sr.] =" + model.Grid_PK);
                row[0]["Finalized Amount"] = model.Txt_Finalized_Amount_ItemDetail_WIPF;
                Sub_Amt_Calculation();
                jsonparam.result = true;
                return Json(new { jsonparam }, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        #endregion

        #region Select Asset
        public ActionResult Frm_WIPFinal_Select_Asset(string FinalizedAsset_Item_ID = "", string Tr_M_ID = "")
        {
            WIP_Final_Select_Asset model = new WIP_Final_Select_Asset();            
            model.FinalAsset_Item_ID_Fin_Sel_Asset = FinalizedAsset_Item_ID;
            model.Tr_M_ID_Fin_Sel_Asset = Tr_M_ID;
            GetSelectAssetGridData(FinalizedAsset_Item_ID, Tr_M_ID);
            if (Select_Asset_ExportData == null)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('WIP_Select_Asset_Popup','" + Messages.SomeError + "','Error');</script>");
            }
            if (Select_Asset_ExportData.Rows.Count <= 0)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('WIP_Select_Asset_Popup','No Selectable Asset Exists !','Information...');</script>");
            }
            return View(model);
        }
        public void GetSelectAssetGridData(string FinalizedAsset_Item_ID = "", string Tr_M_ID = "")
        {
            Common_Lib.RealTimeService.Param_GetExistingAssetListing Param = new Common_Lib.RealTimeService.Param_GetExistingAssetListing();
            Param.Item_ID = FinalizedAsset_Item_ID;
            Param.Prev_YearId = BASE._prev_Unaudited_YearID;
            Param.Next_YearID = BASE._next_Unaudited_YearID;
            Param.TR_M_ID = Tr_M_ID;
            Select_Asset_ExportData = BASE._WIP_Finalization_DBOps.GetExistingAssetListing(Param);
        }
        public ActionResult Frm_WIPFinal_Select_Asset_Grid(string FinalAsset_Item_ID = "", string Tr_M_ID = "",string command="")
        {
            if (command == "REFRESH")
            {
                GetSelectAssetGridData(FinalAsset_Item_ID, Tr_M_ID);
            }
            return View("Frm_WIPFinal_Select_Asset_Grid", Select_Asset_ExportData);
        }

        public ActionResult WIPFinal_Select_Asset_CustomDataAction(string key)
        {
            string itstr = "";
            if (!string.IsNullOrWhiteSpace(key))
            {
                DataRow[] row = Select_Asset_ExportData.Select("[REC_ID] ='" + key+"'");
                if (row.Count() > 0)
                {
                    itstr =""+ row[0]["REC_EDIT_ON"];
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        #endregion

        #region Misc 

        public void Sub_Amt_Calculation()
        {
            double xAmt = 0;
            if (Reference_Grid_ExportData != null)
            {
                Reference_Grid_ExportData.DefaultView.Sort = "";
                Reference_Grid_ExportData.DefaultView.Sort = "Sr. ASC";
                for (int i = 0; i < Reference_Grid_ExportData.Rows.Count; i++)
                {
                    xAmt += Convert.ToDouble(Reference_Grid_ExportData.Rows[i]["Finalized Amount"]);
                }
            }
            Txt_TotalAmount = xAmt;
        }
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
            ClearBaseSession("_WIPFinal");
            BASE._SessionDictionary.Remove("Payment_Asset_Image_Payment");
            //Session.Remove("WIPInfo_detailGrid_Data");
        }
        #endregion
    }
}