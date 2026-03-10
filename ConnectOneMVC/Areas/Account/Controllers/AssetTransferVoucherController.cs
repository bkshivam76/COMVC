
using Common_Lib;
using Common_Lib.RealTimeService;
using ConnectOneMVC.Areas.Account.Models;
using ConnectOneMVC.Areas.Profile.Models;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Helper;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConnectOneMVC.Models;
using System.Text.RegularExpressions;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    public class AssetTransferVoucherController : BaseController
    {
        #region "Start--> Default Variables"        
        public List<VoucherTypeItems> ItemList_AsetTrans
        {
            get { return (List<VoucherTypeItems>)GetBaseSession("ItemList_AsetTrans_ATVou"); }
            set { SetBaseSession("ItemList_AsetTrans_ATVou", value); }
        }
        public List<AssetTransfer_FR_Ins_List> GetFromCenterList_AsetTrans
        {
            get { return (List<AssetTransfer_FR_Ins_List>)GetBaseSession("GetFromCenterList_AsetTrans_ATVou"); }
            set { SetBaseSession("GetFromCenterList_AsetTrans_ATVou", value); }
        }
        public List<AssetTransfer_TO_Ins_List> GetToCenterList_AsetTrans
        {
            get { return (List<AssetTransfer_TO_Ins_List>)GetBaseSession("GetToCenterList_AsetTrans_ATVou"); }
            set { SetBaseSession("GetToCenterList_AsetTrans_ATVou", value); }
        }
        public List<AssetTransfer_PurList> GetPurList_AsetTrans
        {
            get { return (List<AssetTransfer_PurList>)GetBaseSession("GetPurList_AsetTrans_ATVou"); }
            set { SetBaseSession("GetPurList_AsetTrans_ATVou", value); }
        }
        public DataTable ASSET_TABLE_AsetTrans
        {
            get { return (DataTable)GetBaseSession("ASSET_TABLE_AsetTrans_ATVou"); }
            set { SetBaseSession("ASSET_TABLE_AsetTrans_ATVou", value); }
        }
        public List<AssetTransfer_AssetList> AssetList_Data_AsetTrans
        {
            get { return (List<AssetTransfer_AssetList>)GetBaseSession("AssetList_Data_AsetTrans_ATVou"); }
            set { SetBaseSession("AssetList_Data_AsetTrans_ATVou", value); }
        }
        public List<DbOperations.Return_LB_Owners> GetOwnerList_AsetTrans
        {
            get { return (List<DbOperations.Return_LB_Owners>)GetBaseSession("GetOwnerList_AsetTrans_ATVou"); }
            set { SetBaseSession("GetOwnerList_AsetTrans_ATVou", value); }
        }
        public List<AssetTransfer_LocationList> GetLocList_AsetTrans
        {
            get { return (List<AssetTransfer_LocationList>)GetBaseSession("GetLocList_AsetTrans_ATVou"); }
            set { SetBaseSession("GetLocList_AsetTrans_ATVou", value); }
        }
        public DataTable Pending_Grid_Data_AsetTrans
        {
            get { return (DataTable)GetBaseSession("Pending_Grid_Data_AsetTrans_PendingATVou"); }
            set { SetBaseSession("Pending_Grid_Data_AsetTrans_PendingATVou", value); }
        }
        public List<Lookup_Cen_List_AsetTrans> PendingCenterDDList
        {
            get { return (List<Lookup_Cen_List_AsetTrans>)GetBaseSession("PendingCenterDDList_PendingATVou"); }
            set { SetBaseSession("PendingCenterDDList_PendingATVou", value); }
        }
        #endregion

        #region "Start--> Procedures" (Default Page Action Method GET: Account/AssetTransferVoucher)

        [HttpGet]
        public ActionResult Frm_Voucher_Win_Asset_Transfer(string Tag = "", string xID1 = "", string xID2 = "", string xMID = "", string Info_LastEditedOn = null, string Info_MaxEditedOn = null, string iSpecific_ItemID = null, string GridToRefresh = "CashBookListGrid")
        {
            AssetTransferVoucher model = new AssetTransferVoucher();
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            model.ActionMethod = model.Tag.ToString();
            bool HasRight = true;
            if (model.Tag == Common.Navigation_Mode._New && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, "Add") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._New_From_Selection && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, "Add") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._Edit && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, "Update") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._Delete && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, "Delete") == false)
            {
                HasRight = false;
            }
            else if (model.Tag == Common.Navigation_Mode._View && CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, "View") == false)
            {
                HasRight = false;
            }
            if (HasRight == false)
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove
            }
            ViewBag.OpenUserType = BASE._open_User_Type;
            ViewData["Open_Institute_Name"] = BASE._open_Ins_Name;
            model.USE_CROSS_REF = false;
            if (!string.IsNullOrWhiteSpace(Info_LastEditedOn))
            {
                model.Info_LastEditedOn = Convert.ToDateTime(Info_LastEditedOn);
            }
            if (!string.IsNullOrWhiteSpace(Info_MaxEditedOn))
            {
                model.Info_MaxEditedOn = Convert.ToDateTime(Info_MaxEditedOn);
            }
            model.xID1_AsetTrans = xID1;
            model.xID2 = xID2;
            model.xMID = xMID;
            ViewBag.GridToRefresh = GridToRefresh;
            model.TitleX = "Asset Transfer";
            model.TextEdit1Text_AsetTrans = BASE._open_Ins_Name;
            model.TextEdit1Tag_AsetTrans = "";
            RefreshItemList_AsetTrans(model.USE_CROSS_REF);

            //Special Voucher References (FCRA Related) Code
            model.SpecialReferenceList_Data_Astr = BASE._Voucher_DBOps.GetSplVoucherRefsList(ClientScreen.Accounts_Voucher_CashBank, model.Tag);
            model.splVchrRefsCount_Astr = model.SpecialReferenceList_Data_Astr.Count();

            if (ItemList_AsetTrans.Count == 1) 
            {
                model.iTrans_Type = ItemList_AsetTrans[0].ITEM_TRANS_TYPE;
                RefreshGetFromCenterList(model.iTrans_Type);
                RefreshGetToCenterList(model.iTrans_Type);
            }
            Refresh_GetPurList_AsetTrans();
            Refresh_GetOwnerList_AsetTrans();
            Refresh_GetLocList_AsetTrans();
            DataTable HQ_DT = BASE._AssetTransfer_DBOps.GetHQCenters();
            if (HQ_DT != null)
            {
                foreach (DataRow xRow in HQ_DT.Rows)
                {
                    model.HQ_IDs += "'" + xRow["HQ_CEN_ID"].ToString() + "',";
                }
            }
            if (!string.IsNullOrEmpty(model.HQ_IDs) && (model.HQ_IDs.Trim().Length > 0))
            {
                model.HQ_IDs = model.HQ_IDs.Trim().EndsWith(",") ? model.HQ_IDs.Trim().Substring(0, model.HQ_IDs.Trim().Length - 1) : model.HQ_IDs.Trim();
            }
            else if (model.HQ_IDs is null)
            {
                model.HQ_IDs = "''";
            }
            else if (model.HQ_IDs.Trim().Length == 0)
            {
                model.HQ_IDs = "''";
            }
            if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                model.Me_Text = "New ~ " + model.TitleX;
                model.Txt_V_NO_AsetTrans = "";
            }
            if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._View)
            {
                model.Me_Text = "Edit ~ " + model.TitleX;
                DataTable d1 = BASE._AssetTransfer_DBOps.GetMasterRecord(model.xMID);
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }

                //FCRA Related or Special Voucher References Related onEditGet dbfunction call              
                var SpecialReference_Data = BASE._Voucher_DBOps.GetSplVchrRefsOnEdit(xMID);
                if (SpecialReference_Data.Rows.Count > 0)
                {
                    model.SpecialReference_Get_SelectedValue_Astr = SpecialReference_Data.AsEnumerable().Select(r => r.Field<string>("TR_VOUCHER_REF")).ToArray();
                }

                DataTable d2 = BASE._AssetTransfer_DBOps.GetPurposeRecord(model.xMID);
                DataTable d3 = BASE._AssetTransfer_DBOps.GetRecord(model.xMID, 1);
                DataTable d4 = BASE._AssetTransfer_DBOps.GetRecord(model.xMID, 2);
                if (d1 == null || d2 == null || d3 == null || d4 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                model.Txt_V_Date_AsetTrans = Convert.ToDateTime(d3.Rows[0]["TR_DATE"]);
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
                            string message = Messages.RecordChanged("Current Transfer", viewstr);
                            return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('Dynamic_Content_popup','" + message + "','Record Already Changed!!','" + GridToRefresh + "');</script>");
                        }
                    }
                }               
                Data_Binding(d1, d2, d3, d4, ref model);
            }
            if (model.Tag == Common.Navigation_Mode._Delete)
            {
                model.Me_Text = "Delete ~ " + model.TitleX;
            }
            if (model.Tag == Common.Navigation_Mode._View)
            {
                model.Me_Text = "View ~ " + model.TitleX;
            }
            // Check Pending Asset Transfer Entries
            if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection)
            {
                model.USE_CROSS_REF = false;
                DataSet d1 = BASE._AssetTransfer_DBOps.GetUnMatchedList(1, Convert.ToInt32(null));
                if (d1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                DataTable P1 = d1.Tables[0];
                if (P1 == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('" + Messages.SomeError + "','Error!!');</script>");
                }
                if (P1.Rows.Count > 0)
                {
                    model.pending_list = true;
                }
                else
                {
                    model.pending_list = false;
                }
            }
            else
            {
                model.USE_CROSS_REF = false;
            }
            if (model.Tag == Common.Navigation_Mode._New_From_Selection && model.USE_CROSS_REF == false)
            {
                model.iSpecific_ItemID = iSpecific_ItemID;
            }
            return View(model);
        }

        private void Data_Binding(DataTable d1, DataTable d2, DataTable d3, DataTable d4, ref AssetTransferVoucher model)
        {
            model.xID1_AsetTrans = d3.Rows[0]["REC_ID"].ToString();
            model.xID2 = d4.Rows.Count>0?d4.Rows[0]["REC_ID"].ToString():"";

            if (d3.Rows[0]["TR_TYPE"].ToString() == "CREDIT")
            {
                model.USE_CROSS_REF = true;
                RefreshItemList_AsetTrans(model.USE_CROSS_REF);
                model.USE_CROSS_REF = false;
            }
            model.LastEditedOn = Convert.ToDateTime(d3.Rows[0]["REC_EDIT_ON"]);
            model.Txt_V_NO_AsetTrans = d3.Rows[0]["TR_VNO"].ToString();
            if (!Convert.IsDBNull(d3.Rows[0]["TR_ITEM_ID"]))
            {
                if (d3.Rows[0]["TR_ITEM_ID"].ToString().Length > 0)
                {
                    model.GLookUp_ItemList_AsetTrans = d3.Rows[0]["TR_ITEM_ID"].ToString();
                    if (ItemList_AsetTrans.Count > 0)
                    {
                        model.iTrans_Type = ItemList_AsetTrans.Where(x => x.ITEMID == d3.Rows[0]["TR_ITEM_ID"].ToString()).FirstOrDefault().ITEM_TRANS_TYPE;
                    }
                }
            }
            RefreshGetFromCenterList(model.iTrans_Type);
            RefreshGetToCenterList(model.iTrans_Type);
            string Tr_AB_ID_1 = "";
            string Tr_AB_ID_2 = "";
            if (d3.Rows[0]["TR_TYPE"].ToString().ToUpper() == "DEBIT")
            {
                Tr_AB_ID_1 = d3.Rows[0]["Tr_AB_ID_1"].ToString();
                Tr_AB_ID_2 = d3.Rows[0]["Tr_AB_ID_2"].ToString();
            }
            else
            {
                Tr_AB_ID_1 = d3.Rows[0]["Tr_AB_ID_2"].ToString();
                Tr_AB_ID_2 = d3.Rows[0]["Tr_AB_ID_1"].ToString();
            }
            if (Tr_AB_ID_1.Length > 0)
            {
                model.GLookUp_ToCen_List_AsetTrans = Tr_AB_ID_1;
            }
            int iFR_CEN_ID = 0;
            if (Tr_AB_ID_2.Length > 0)
            {
                model.GLookUp_FrCen_List_AsetTrans = Tr_AB_ID_2;
                if (GetFromCenterList_AsetTrans.Count > 0)
                {
                    iFR_CEN_ID = GetFromCenterList_AsetTrans.Where(x => x.FR_ID == Tr_AB_ID_2).FirstOrDefault().FR_CEN_ID;
                }
            }
            model.Cmb_Asset_Type_AsetTrans = d1.Rows[0]["TR_SALE_TYPE"].ToString();
            Get_Asset_Items("", model.USE_CROSS_REF, model.Cmb_Asset_Type_AsetTrans, model.ActionMethod, iFR_CEN_ID.ToString(), model.CROSS_M_ID, model.xMID, model.iTrans_Type);
            if (d4.Rows.Count>0&&!Convert.IsDBNull(d4.Rows[0]["TR_REF_OTHERS"]))
            {
                if (d4.Rows[0]["TR_REF_OTHERS"].ToString().Length > 0)
                {
                    model.GLookUp_AssetList_AsetTrans = d4.Rows[0]["TR_REF_OTHERS"].ToString();
                }
            }
            model.Txt_Qty_AsetTrans = Convert.ToDouble(d1.Rows[0]["TR_SALE_QTY"]);
            model.Txt_SaleAmt_AsetTrans = Convert.ToDouble(d1.Rows[0]["TR_SALE_AMT"]);
            if (!Convert.IsDBNull(d2.Rows[0]["TR_PURPOSE_MISC_ID"]))
            {
                if (d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString().Length > 0)
                {
                    model.GLookUp_PurList_AsetTrans = d2.Rows[0]["TR_PURPOSE_MISC_ID"].ToString();
                }
            }
            model.Txt_Narration_AsetTrans = d3.Rows[0]["TR_NARRATION"].ToString();
            model.Txt_Remarks_AsetTrans = d3.Rows[0]["TR_REMARKS"].ToString();
            model.Txt_Reference_AsetTrans = d3.Rows[0]["TR_REFERENCE"].ToString();
        }
        #endregion

        #region Pending    

        public ActionResult Frm_Asset_Transfer_Pending()
        {
            ViewBag.open_Cen_Rec_ID = BASE._open_Cen_Rec_ID;
            ViewBag.Open_Cen_ID = BASE._open_Cen_ID;
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            Pending_Grid_Data_AsetTrans = new DataTable();
            if (BASE._open_Cen_ID == 4216)
            {
                FillCentres();
            }
            else
            {
                GetPendingAssetTransferData(BASE._open_Cen_ID);
            }
            return PartialView(Pending_Grid_Data_AsetTrans);
        }
        public void GetPendingAssetTransferData(int TO_CEN_ID = 0)
        {
            DataSet p1;
            if (BASE._open_Cen_ID == 4216)
            {
                p1 = BASE._AssetTransfer_DBOps.GetUnMatchedList(0, TO_CEN_ID);
            }
            else
            {
                p1 = BASE._AssetTransfer_DBOps.GetUnMatchedList(0, Convert.ToInt32(null));
            }         
            Pending_Grid_Data_AsetTrans = p1.Tables[0];
        }
        public ActionResult AssetTransferCustomDataAction(string key)
        {
            string itstr = "";
            if (key != null)
            {
                DataRow[] row = Pending_Grid_Data_AsetTrans.Select("[ID] ='" + key+"'");

                if (row.Length > 0)
                {
                    itstr = row[0]["ID"] + "![" + row[0]["M_ID"] + "![" + row[0]["ITEM_ID"] + "![" + row[0]["REC_EDIT_ON"] + "![" + row[0]["Date"] + "![" + row[0]["CEN_ID"] + "![" + row[0]["Asset Type"] + "![" + row[0]["Amount"] + "![" + row[0]["ASSET_REF_ID"] + "![" + row[0]["ASSET_ITEM_ID"] + "![" + row[0]["Qty / Weight"] + "![" + row[0]["PUR_ID"];
                }
            }      
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult AssetTransfer_Pending_Grid(string command, int TO_CEN_ID = 0)
        {
            if (Pending_Grid_Data_AsetTrans == null || command == "REFRESH")
            {
                GetPendingAssetTransferData(TO_CEN_ID);
            }
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            return PartialView(Pending_Grid_Data_AsetTrans);
        }
        #endregion
        [HttpPost]
        public ActionResult Frm_AssetTransferVoucher_Window(AssetTransferVoucher model)
        {
            model.Tag = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), model.ActionMethod);
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._New_From_Selection || model.Tag == Common.Navigation_Mode._Edit)
                {
                    if (model.Cmb_Asset_Type_AsetTrans.ToUpper() == "MOVABLE ASSETS" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "LAND & BUILDING")
                    {
                        // Movable Assets or Property
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Sorry! Addition / Changes cannot be done after the completion of Insurance Audit";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                //// -----------------------------+
                //// Start : Check if entry already changed 
                //// -----------------------------+

                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._Delete || model.Tag == Common.Navigation_Mode._Edit)
                    {
                        // Bug #5697
                        if (model.Info_MaxEditedOn != null)
                        {
                            if (model.Info_MaxEditedOn != DateTime.MinValue)    // Record has been opened on basis of this being a last edited record for referred asset
                            {
                                DateTime Lastest_MaxEdit_On = Convert.ToDateTime(BASE._AssetDBOps.Get_Asset_Ref_MaxEditOn(model.GLookUp_AssetList_AsetTrans));
                                if (IsDate(Lastest_MaxEdit_On.ToString()))
                                {
                                    if (Lastest_MaxEdit_On > model.Info_MaxEditedOn)
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
                    if (model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        DataTable assetTrf_DbOps = BASE._AssetTransfer_DBOps.GetRecord(model.xMID, 1);
                        if (assetTrf_DbOps == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (assetTrf_DbOps.Rows.Count == 0)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Transfer");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.LastEditedOn != Convert.ToDateTime(assetTrf_DbOps.Rows[0]["REC_EDIT_ON"]))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Transfer");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            jsonParam.refreshgrid = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // ED/L 
                        object MaxValue = 0;
                        MaxValue = BASE._AssetTransfer_DBOps.GetStatus(model.xID1_AsetTrans);
                        if (MaxValue == null)
                        {
                            jsonParam.message = "Entry Not Found...!";
                            jsonParam.result = false;
                            jsonParam.title = "Information...";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue == (int)Common.Record_Status._Locked)
                        {
                            jsonParam.message = "Locked Entry cannot be Edited / Deleted...!" + "<br><br>" + "Note:" + "<br>" + "-------" + "<br>" + "Drop your Request to Madhuban for Unlock this Entry," + "<br>" + "If you really want to do some action...!";
                            jsonParam.title = "Information...";
                            jsonParam.refreshgrid = true;
                            jsonParam.result = false;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // Special Checks
                        DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(model.xID1_AsetTrans);
                        string xCross_Ref_Id = "";
                        if (Status == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Status.Rows.Count > 0)  //  checks for record existence here 
                        {
                            if (!Convert.IsDBNull(Status.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                            {
                                xCross_Ref_Id = Status.Rows[0]["TR_TRF_CROSS_REF_ID"].ToString();
                            }
                        }
                        if (model.Tag == Common.Navigation_Mode._Delete)
                        {
                            int _RowHandle = 0;
                            if (Val(model.iTrans_Type) == 2)
                            {
                                _RowHandle = 1;
                            }
                            if (model.iTrans_Type == "CREDIT" && !(Convert.IsDBNull(xCross_Ref_Id)))
                            {
                                bool isProperty = false;
                                if (xCross_Ref_Id.Length > 0)
                                {
                                    foreach (DataRow cRow in BASE._Voucher_DBOps.GetAssetItemID(model.xMID).Rows)   //  Get Actual Item IDs from Selected Transaction
                                    {
                                        string xTemp_ItemID = cRow[0].ToString();
                                        DataTable ProfileTable = BASE._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID);    // Gets Asset Profile
                                        string xTemp_AssetProfile = ProfileTable.Rows[0]["ITEM_PROFILE"].ToString();
                                        string xTemp_AssetID = "";
                                        switch (xTemp_AssetProfile)
                                        {
                                            case "GOLD":
                                            case "SILVER":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, model.xMID);
                                                break;
                                            case "OTHER ASSETS":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, model.xMID);
                                                break;
                                            case "LIVESTOCK":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, model.xMID);
                                                break;
                                            case "VEHICLES":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, model.xMID);
                                                break;
                                            case "LAND & BUILDING":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, model.xMID);
                                                isProperty = true;
                                                break;
                                            case "FIXED DEPOSITS":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.FD_INFO, model.xMID);
                                                break;
                                            case "OTHER DEPOSITS":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.DEPOSITS_INFO, model.xMID);
                                                break;
                                            case "OTHER LIABILITIES":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.LIABILITIES_INFO, model.xMID);
                                                break;
                                            case "ADVANCES":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.ADVANCES_INFO, model.xMID);
                                                break;
                                            case "OPENING":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.OTHER_PROFILE_INFO, model.xMID);
                                                break;
                                            case "WIP":
                                                xTemp_AssetID = BASE._Voucher_DBOps.GetAssetRecID(Tables.WIP_INFO, model.xMID);
                                                break;
                                        }
                                        if (xTemp_AssetID.Length > 0)
                                        {
                                            DataTable SaleRecord = BASE._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID);
                                            if (SaleRecord != null)
                                            {
                                                if (SaleRecord.Rows.Count > 0)
                                                {
                                                    jsonParam.message = Messages.DependencyChanged("Selected Entry contains a asset which was already sold ");
                                                    jsonParam.title = "Referred Record Already Changed!!";
                                                    jsonParam.result = false;
                                                    jsonParam.refreshgrid = true;
                                                    jsonParam.closeform = true;
                                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            DataTable AssetTrfRecord = BASE._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, 0, xTemp_AssetID);    // checks if the referred property for constt items has been transferred 

                                            if (AssetTrfRecord.Rows.Count > 0)
                                            {
                                                if (AssetTrfRecord.Rows.Count > 0)
                                                {
                                                    jsonParam.message = Messages.DependencyChanged("Selected Entry contains a asset which was already Transfered ");
                                                    jsonParam.title = "Referred Record Already Changed!!";
                                                    jsonParam.result = false;
                                                    jsonParam.refreshgrid = true;
                                                    jsonParam.closeform = true;
                                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            // Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                            DataTable ReferenceRecord = BASE._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID);
                                            if (ReferenceRecord != null)
                                            {
                                                if (ReferenceRecord.Rows.Count > 0)
                                                {
                                                    jsonParam.message = Messages.DependencyChanged("Selected Entry contains a asset which was referred in a Dependent Entry");
                                                    jsonParam.title = "Referred Record Already Changed!!";
                                                    jsonParam.result = false;
                                                    jsonParam.refreshgrid = true;
                                                    jsonParam.closeform = true;
                                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (!Convert.IsDBNull(xCross_Ref_Id))
                            {
                                if (xCross_Ref_Id.Length > 0)
                                {
                                    jsonParam.message = "Matched Asset Transfer cannot be Deleted...!";
                                    jsonParam.title = "Referred Record Already Matched!!";
                                    jsonParam.result = false;
                                    jsonParam.refreshgrid = true;
                                    jsonParam.closeform = true;
                                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }

                //// -----------------------------+
                //// End : Check if entry already changed 
                //// -----------------------------+
                if (model.Tag == Common.Navigation_Mode._New || model.Tag == Common.Navigation_Mode._Edit || model.Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (string.IsNullOrWhiteSpace(model.GLookUp_ItemList_AsetTrans))
                    {
                        jsonParam.message = "Item Name Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_ItemList_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()) == false)
                    {
                        jsonParam.message = "Date Incorrect / Blank...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_V_Date_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()) == true)
                    {
                        if (Convert.ToDateTime(model.Txt_V_Date_AsetTrans) < Convert.ToDateTime(BASE._open_Year_Sdt) || Convert.ToDateTime(model.Txt_V_Date_AsetTrans) > Convert.ToDateTime(BASE._open_Year_Edt))
                        {
                            jsonParam.message = "Date not as per Financial Year...!";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Txt_V_Date_AsetTrans";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_FrCen_List_AsetTrans))
                    {
                        jsonParam.message = "From Centre Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_FrCen_List_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_ToCen_List_AsetTrans))
                    {
                        jsonParam.message = "To Centre Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_ToCen_List_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.GLookUp_FrCen_List_AsetTrans == model.GLookUp_ToCen_List_AsetTrans)
                    {
                        jsonParam.message = "Both Centre are Same...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_ToCen_List_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.Cmb_Asset_Type_AsetTrans))
                    {
                        jsonParam.message = "Asset Type Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Cmb_Asset_Type_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_AssetList_AsetTrans))
                    {
                        jsonParam.message = "Asset Item Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_AssetList_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Qty_AsetTrans <= 0 || model.Txt_Qty_AsetTrans==null)
                    {
                        jsonParam.message = "Qty. cannot be Zero / Negative / Blank...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Qty_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_Qty_AsetTrans > model.Txt_CurQty_AsetTrans)
                    {
                        jsonParam.message = "Qty. cannot be greater than " + model.Txt_CurQty_AsetTrans + "...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_Qty_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Txt_SaleAmt_AsetTrans < 0)
                    {
                        jsonParam.message = "Amount cannot be Negative...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_SaleAmt_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    if (model.USE_CROSS_REF && model.OwnershipRequire)
                    {
                        if (string.IsNullOrWhiteSpace(model.Look_OwnList_AsetTrans) || model.Look_OwnList_AsetTrans.Trim().Length == 0)
                        {
                            if (model.Cmb_Asset_Type_AsetTrans.ToUpper() == "ADVANCES" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "OTHER DEPOSITS" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "OTHER LIABILITIES")
                            { jsonParam.message = "Party Name Not Selected...!"; }
                            else
                            { jsonParam.message = "Owner Name Not Selected...!"; }
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Look_OwnList_AsetTrans";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (!(model.Cmb_Asset_Type_AsetTrans.ToUpper() == "LAND & BUILDING" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "FD" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "ADVANCES" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "OTHER LIABILITIES" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "OTHER DEPOSITS" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "OPENING" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "WIP"))
                    {
                        if (model.iTrans_Type == "CREDIT" && (string.IsNullOrWhiteSpace(model.Look_LocList_AsetTrans) || model.Look_LocList_AsetTrans.Trim().Length == 0))
                        {
                            jsonParam.message = "Location Not Selected...!";
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "Look_LocList_AsetTrans";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Cmb_Asset_Type_AsetTrans.ToUpper() == "LAND & BUILDING")
                    {
                        if (model.USE_CROSS_REF == true) 
                        {
                            if (string.IsNullOrWhiteSpace(model.Cmd_PUse_AsetTrans))
                            {
                                jsonParam.message = "Use Of Property Not Selected...!";
                                jsonParam.result = false;
                                jsonParam.title = "Incomplete Information . . .";
                                jsonParam.focusid = "Cmd_PUse_AsetTrans";
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        object MaxValue = 0;
                        MaxValue = BASE._L_B_Voucher_DBOps.CheckDuplicatePropertyName((int)model.Tag, "", model.Property_Name ?? string.Empty, Convert.ToInt32(model.iTO_CEN_ID));
                        if (MaxValue == null)
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue > 0)
                        {
                            jsonParam.message = "Property with same name already Exists in Receiving Center...!" + "<br>" + "<br>" + "Please alter Property name before Transferring the same.";
                            jsonParam.result = false;
                            jsonParam.title = "Duplicate Information . . .";
                            jsonParam.focusid = "GLookUp_AssetList_AsetTrans";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (model.Cmd_PUse_AsetTrans.ToUpper() == "MAIN CENTRE")
                        {
                            jsonParam.message = "Main Center cannot be transferred...! " + "<br>" + "<br>" + "Please alter Property Use before Transferring the same.";
                            jsonParam.result = false;
                            jsonParam.title = "Duplicate Information . . .";
                            jsonParam.focusid = "GLookUp_AssetList_AsetTrans";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        // Checking Duplicate Location....
                        Object MaxValue_Loc = 0;
                        MaxValue_Loc = BASE._AssetLocDBOps.GetRecordCountByName(model.Property_Name.Trim() ?? string.Empty, ClientScreen.Profile_LandAndBuilding, model.BE_To_Pad_No_AsetTrans);
                        if (MaxValue_Loc.CheckNull())
                        {
                            jsonParam.message = Messages.SomeError;
                            jsonParam.title = "Error!!";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if ((int)MaxValue_Loc != 0 && model.Tag == Common.Navigation_Mode._New)
                        {
                            jsonParam.message = "Location With Same Name Already Available in Receiving Center...!";
                            jsonParam.result = false;
                            jsonParam.title = "Duplicate Information . . .";
                            jsonParam.focusid = "GLookUp_AssetList_AsetTrans";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.GLookUp_PurList_AsetTrans))
                    {
                        jsonParam.message = "Purpose Not Selected...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "GLookUp_PurList_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                if (model.Tag == Common.Navigation_Mode._Delete
                            || model.Tag == Common.Navigation_Mode._New
                            || model.Tag == Common.Navigation_Mode._Edit
                            || model.Tag == Common.Navigation_Mode._New_From_Selection)
                {
                    if (model.Cmb_Asset_Type_AsetTrans.ToUpper() == "LAND & BUILDING")    // Property
                    {
                        string UsageMessage = FindLocationUsage_AsetTrans(model.GLookUp_AssetList_AsetTrans, true); //  exclude sold / tf assets
                        if (UsageMessage.Length > 0)
                        {
                            jsonParam.message = UsageMessage;
                            jsonParam.result = false;
                            jsonParam.title = "Incomplete Information . . .";
                            jsonParam.focusid = "GLookUp_AssetList_AsetTrans";
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.Tag == Common.Navigation_Mode._New
                            || model.Tag == Common.Navigation_Mode._New_From_Selection
                            || model.Tag == Common.Navigation_Mode._Edit)
                {
                    Param_GetAssetMaxTxnDate inparam = new Param_GetAssetMaxTxnDate();
                    inparam.Creation_Date = Convert.ToDateTime(model.AssetList_REF_CREATION_DATE.CheckNull() ? BASE._open_Year_Sdt : model.AssetList_REF_CREATION_DATE);
                    inparam.Asset_RecID = model.GLookUp_AssetList_AsetTrans;
                    inparam.YearID = BASE._open_Year_ID;
                    inparam.Tr_M_ID = model.xMID ?? "";
                    DateTime? MxDate = (DateTime?)BASE._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam);
                    if (MxDate.CheckNull())
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    if (Convert.ToDateTime(model.Txt_V_Date_AsetTrans) < MxDate)
                    {
                        jsonParam.message = "Voucher Date cannot be less than previous transaction on same asset dated " + Convert.ToDateTime(MxDate).ToLongDateString() + "  ...!";
                        jsonParam.result = false;
                        jsonParam.title = "Incomplete Information . . .";
                        jsonParam.focusid = "Txt_V_Date_AsetTrans";
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                }
                //// ================================================Dependencies ==============================================
                if (BASE.AllowMultiuser())
                {
                    if (model.Tag == Common.Navigation_Mode._New
                               || model.Tag == Common.Navigation_Mode._Edit
                               || model.Tag == Common.Navigation_Mode._New_From_Selection
                               || model.Tag == Common.Navigation_Mode._Delete)
                    {
                        Get_Asset_Items(model.GLookUp_AssetList_AsetTrans, model.USE_CROSS_REF, model.Cmb_Asset_Type_AsetTrans, model.ActionMethod, model.iFR_CEN_ID, model.CROSS_M_ID, model.xMID, model.iTrans_Type);
                        var Asset_Table_List = AssetList_Data_AsetTrans;
                        int cnt2 = Asset_Table_List.Count();//ASSET_TABLE.Rows.Count;
                        DateTime oldEditOn = Convert.ToDateTime(model.AssetList_REC_EDIT_ON);
                        if (cnt2 <= 0)  //if the user - selected asset is not qualified for sale anymore ,as the same has been changed by other user
                        {
                            jsonParam.message = Messages.DependencyChanged("Profile Assets");
                            jsonParam.title = "Referred Record Already Deleted!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            DateTime newEditOn = Convert.ToDateTime(Asset_Table_List[0].REC_EDIT_ON);// Convert.ToDateTime(ASSET_TABLE.Rows[0]["REC_EDIT_ON"]);
                            if (oldEditOn != newEditOn)
                            {
                                jsonParam.message = Messages.DependencyChanged("Profile Assets");
                                jsonParam.title = "Referred Record Already Changed!!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        if (Convert.ToDouble(Asset_Table_List[0].REF_QTY) < model.Txt_Qty_AsetTrans)     //if the weight/qty remaining is less then the weight/qty demanded for sale
                        {
                            jsonParam.message = Messages.DependencyChanged("Asset List");
                            jsonParam.title = "weight/qty remaining is less then the weight/qty demanded for Transfer !!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDouble(Asset_Table_List[0].REF_AMT) != model.AssetList_REF_AMT)
                        {
                            jsonParam.message = Messages.DependencyChanged("Asset List");
                            jsonParam.title = "Value Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    if (model.Tag == Common.Navigation_Mode._New
                              || model.Tag == Common.Navigation_Mode._Edit
                              || model.Tag == Common.Navigation_Mode._New_From_Selection)   // Asset Location Dependency Check #Ref U41
                    {
                        if (model.Cmb_Asset_Type_AsetTrans.ToLower() != "fd" && model.Cmb_Asset_Type_AsetTrans.ToUpper() != "ADVANCES" && model.Cmb_Asset_Type_AsetTrans.ToUpper() != "OTHER LIABILITIES" && model.Cmb_Asset_Type_AsetTrans.ToUpper() != "OTHER DEPOSITS" && model.Cmb_Asset_Type_AsetTrans.ToUpper() != "OPENING" && model.Cmb_Asset_Type_AsetTrans.ToUpper() != "WIP")
                        {
                            if (string.IsNullOrWhiteSpace(model.Look_LocList_AsetTrans) == false)
                            {
                                DataTable d2 = BASE._AssetTransfer_DBOps.GetAssetLocationByID(model.Look_LocList_AsetTrans);
                                if (d2 != null)
                                {
                                    DateTime Old_EditOn = Convert.ToDateTime(model.LocList_REC_EDIT_ON);
                                    if (d2.Rows.Count <= 0)
                                    {
                                        jsonParam.message = Messages.DependencyChanged("Asset Location");
                                        jsonParam.title = "Referred Record Already Deleted!!";
                                        jsonParam.result = false;
                                        jsonParam.refreshgrid = true;
                                        jsonParam.closeform = true;
                                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        DateTime NewEditOn = Convert.ToDateTime(d2.Rows[0]["REC_EDIT_ON"]);
                                        if (NewEditOn != Old_EditOn)
                                        {
                                            jsonParam.message = Messages.DependencyChanged("Asset Location");
                                            jsonParam.title = "Referred Record Already Changed!!";
                                            jsonParam.result = false;
                                            jsonParam.refreshgrid = true;
                                            jsonParam.closeform = true;
                                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                string Status_Action = ((int)Common.Record_Status._Completed).ToString();
                if (model.Tag.ToString() == "_Delete")
                {
                    Status_Action = ((int)Common.Record_Status._Deleted).ToString();
                }
                string Tr_AB_ID_1 = "";
                string Tr_AB_ID_2 = "";
                string Dr_Led_id = "";
                string Cr_Led_id = "";
                if ((model.iTrans_Type.ToUpper() == "DEBIT"))
                {
                    if (model.Cmb_Asset_Type_AsetTrans == "OTHER LIABILITIES") {
                        Cr_Led_id = model.iLed_ID;  // Transfer Item Ledger
                        Dr_Led_id = model.aLed_ID;  // Asset Item Ledger
                        Tr_AB_ID_1 = model.GLookUp_ToCen_List_AsetTrans;
                        Tr_AB_ID_2 = model.GLookUp_FrCen_List_AsetTrans;
                    }
                    else
                    {
                        Dr_Led_id = model.iLed_ID;  // Transfer Item Ledger
                        Cr_Led_id = model.aLed_ID;  // Asset Item Ledger
                        Tr_AB_ID_1 = model.GLookUp_ToCen_List_AsetTrans;
                        Tr_AB_ID_2 = model.GLookUp_FrCen_List_AsetTrans;
                    }
                }
                else
                {
                    if (model.Cmb_Asset_Type_AsetTrans == "OTHER LIABILITIES")
                    {
                        Dr_Led_id = model.iLed_ID;  // Transfer Item Ledger
                        Cr_Led_id = model.aLed_ID;  // Asset Item Ledger
                        Tr_AB_ID_1 = model.GLookUp_FrCen_List_AsetTrans;
                        Tr_AB_ID_2 = model.GLookUp_ToCen_List_AsetTrans;
                    }
                    else
                    {
                        Cr_Led_id = model.iLed_ID;  // Transfer Item Ledger
                        Dr_Led_id = model.aLed_ID;  // Asset Item Ledger
                        Tr_AB_ID_1 = model.GLookUp_FrCen_List_AsetTrans;
                        Tr_AB_ID_2 = model.GLookUp_ToCen_List_AsetTrans;
                    }
                }
                //// 'Transfer To entry has been changed by other centre
                if (string.IsNullOrWhiteSpace(model.CROSS_M_ID)==false)
                {
                    // Dim AssetTf As DataTable = BASE._AssetTransfer_DBOps.GetRecord(CROSS_M_ID, 1)
                    DataTable AssetTf = BASE._AssetTransfer_DBOps.GetUnMatchedList(0, 0, model.CROSS_M_ID).Tables[0];   // Bug 5092 fixed
                    if (AssetTf == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    DateTime oldEditOn = Convert.ToDateTime(model.FR_REC_EDIT_ON);
                    if (AssetTf.Rows.Count == 0)
                    {
                        jsonParam.message = "Transfer To Entry has been deleted in other centre";
                        jsonParam.title = "Information...!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = true;
                        jsonParam.closeform = true;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        DateTime NewEditOn = Convert.ToDateTime(AssetTf.Rows[0]["REC_EDIT_ON"]);
                        if (NewEditOn != oldEditOn)
                        {
                            jsonParam.message = "Transfer To Entry has been Changed in other centre";
                            jsonParam.title = "Information...!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            jsonParam.closeform = true;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                if (model.Tag == Common.Navigation_Mode._New
                            || model.Tag == Common.Navigation_Mode._New_From_Selection) // new
                {
                    model.xMID = System.Guid.NewGuid().ToString();
                    model.xID1_AsetTrans = System.Guid.NewGuid().ToString();
                    model.xID2 = System.Guid.NewGuid().ToString();
                    string xAssetRefID = System.Guid.NewGuid().ToString();                    
                    string xCROSS_REF_ID = null;
                    if (model.USE_CROSS_REF)
                    {
                        xCROSS_REF_ID = "'" + model.CROSS_M_ID + "'";
                        xAssetRefID = xAssetRefID;
                    }
                    else
                    {                       
                        xAssetRefID = model.GLookUp_AssetList_AsetTrans;
                    }
                    // #Ref AN41
                    if (BASE.AllowMultiuser())
                    {
                        if (model.iTrans_Type.ToString() == "CREDIT")
                        {
                            DataTable assetTfFrom = (DataTable)BASE._Voucher_DBOps.GetAdjustments(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, xCROSS_REF_ID, false, BASE._open_Year_ID);
                            if (assetTfFrom == null)
                            {
                                jsonParam.message = Messages.SomeError;
                                jsonParam.title = "Error!!";
                                jsonParam.result = false;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                            if (assetTfFrom.Rows.Count > 0)     // A/A
                            {
                                jsonParam.message = Messages.DependencyChanged("Asset Transfer From Entry");
                                jsonParam.title = "Referred Record Already Changed !!";
                                jsonParam.result = false;
                                jsonParam.refreshgrid = true;
                                jsonParam.closeform = true;
                                return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }                   
                    // MASTER ENTRY              
                    Param_Txn_Insert_VoucherAssetTransfer InNewParam = new Param_Txn_Insert_VoucherAssetTransfer();
                    Parameter_InsertMasterInfo_VoucherAssetTransfer InMinfo = new Parameter_InsertMasterInfo_VoucherAssetTransfer();
                    InMinfo.TxnCode = (int)Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer;
                    InMinfo.VNo = model.Txt_V_NO_AsetTrans ?? string.Empty;
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        InMinfo.TDate = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMinfo.TDate = model.Txt_V_Date_AsetTrans.ToString();
                    }
                    InMinfo.SubTotal = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InMinfo.Cash = 0;
                    InMinfo.Bank = 0;
                    InMinfo.AssetRef_ID = xAssetRefID;
                    InMinfo.AssetTrf_Amt = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InMinfo.AssetTrf_Qty = Convert.ToDouble(model.Txt_Qty_AsetTrans);
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        InMinfo.AssetTrf_Date = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InMinfo.AssetTrf_Date = model.Txt_V_Date_AsetTrans.ToString();
                    }
                    InMinfo.AssetTrf_Type = model.Cmb_Asset_Type_AsetTrans;
                    InMinfo.Status_Action = Status_Action;
                    InMinfo.RecID = model.xMID;
                    InNewParam.param_InsertMaster = InMinfo;
                    // Transaction info
                    // 1st Entry
                    Parameter_Insert_VoucherAssetTransfer InParam1 = new Parameter_Insert_VoucherAssetTransfer();
                    InParam1.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer;
                    InParam1.VNo = model.Txt_V_NO_AsetTrans ?? string.Empty;
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        InParam1.TDate = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam1.TDate = model.Txt_V_Date_AsetTrans.ToString();
                    }
                    InParam1.ItemID = model.GLookUp_ItemList_AsetTrans;
                    InParam1.Type = model.iTrans_Type;
                    if (model.iTrans_Type == "DEBIT")
                    {
                        InParam1.Cr_Led_ID = "";
                    }
                    else
                    {
                        InParam1.Cr_Led_ID = Cr_Led_id;
                    }

                    if (model.iTrans_Type == "CREDIT")
                    {
                        InParam1.Dr_Led_ID = "";
                    }
                    else
                    {
                        InParam1.Dr_Led_ID = Dr_Led_id;
                    }
                    InParam1.Sub_Cr_Led_ID = "";
                    InParam1.Sub_Dr_Led_ID = "";
                    InParam1.Mode = "JOURNAL";
                    InParam1.Amount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InParam1.AB_ID_1 = Tr_AB_ID_1;
                    InParam1.AB_ID_2 = Tr_AB_ID_2;
                    InParam1.Narration = model.Txt_Narration_AsetTrans ?? string.Empty;
                    InParam1.Remarks = model.Txt_Remarks_AsetTrans ?? string.Empty;
                    InParam1.Reference = model.Txt_Reference_AsetTrans ?? string.Empty;
                    InParam1.Tr_M_ID = model.xMID;
                    InParam1.TxnSrNo = 1;
                    InParam1.AssetTrf_Qty = Convert.ToDouble(model.Txt_Qty_AsetTrans);
                    InParam1.AssetTrf_RefItemID = xAssetRefID;
                    InParam1.Status_Action =Status_Action;
                    InParam1.RecID = model.xID1_AsetTrans;
                    InParam1.Cross_Ref_ID = xCROSS_REF_ID;

                    InNewParam.param_InsertTxnInfo1 = InParam1;
                    // Transaction info
                    // 2nd Entry
                    Parameter_Insert_VoucherAssetTransfer InParam2 = new Parameter_Insert_VoucherAssetTransfer();
                    InParam2.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer;
                    InParam2.VNo = model.Txt_V_NO_AsetTrans ?? string.Empty;
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        InParam2.TDate = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam2.TDate = model.Txt_V_Date_AsetTrans.ToString();
                    }

                    InParam2.ItemID = model.aItem_ID;
                    if (model.iTrans_Type == "DEBIT")
                    {
                        InParam2.Type = "CREDIT";
                    }
                    else
                    {
                        InParam2.Type = "DEBIT";
                    }

                    if (model.iTrans_Type == "DEBIT")
                    {
                        InParam2.Cr_Led_ID = Cr_Led_id;
                    }
                    else
                    {
                        InParam2.Cr_Led_ID = "";
                    }

                    if (model.iTrans_Type == "CREDIT")
                    {
                        InParam2.Dr_Led_ID = Dr_Led_id;
                    }
                    else
                    {
                        InParam2.Dr_Led_ID = "";
                    }

                    InParam2.Sub_Cr_Led_ID = "";
                    InParam2.Sub_Dr_Led_ID = "";
                    InParam2.Mode = "JOURNAL";
                    InParam2.Amount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InParam2.AB_ID_1 = Tr_AB_ID_1;
                    InParam2.AB_ID_2 = Tr_AB_ID_2;
                    InParam2.Narration = model.Txt_Narration_AsetTrans ?? string.Empty;
                    InParam2.Remarks = model.Txt_Remarks_AsetTrans ?? string.Empty;
                    InParam2.Reference = model.Txt_Reference_AsetTrans ?? string.Empty;
                    InParam2.Tr_M_ID = model.xMID;
                    InParam2.TxnSrNo = 2;
                    InParam2.AssetTrf_Qty = Convert.ToDouble(model.Txt_Qty_AsetTrans);
                    InParam2.AssetTrf_RefItemID = xAssetRefID;
                    InParam2.Status_Action = Status_Action.ToString();
                    InParam2.RecID = model.xID2;
                    InParam2.Cross_Ref_ID = xCROSS_REF_ID;
                    InNewParam.param_InsertTxnInfo2 = InParam2;
                    // PAYMENT  
                    Parameter_InsertAandLPayment_VoucherAssetTransfer InPay = new Parameter_InsertAandLPayment_VoucherAssetTransfer();
                    InPay.TxnMID = model.xMID;
                    InPay.Type = "TRANSFER";
                    InPay.SrNo = "1";
                    InPay.RefID = xAssetRefID;
                    InPay.RefAmount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InPay.Status_Action = Status_Action;
                    InPay.RecID = System.Guid.NewGuid().ToString();

                    InNewParam.param_InsertAandLPayment = InPay;
                    // purpose()
                    Parameter_InsertPurpose_VoucherAssetTransfer InPurpose = new Parameter_InsertPurpose_VoucherAssetTransfer();
                    InPurpose.TxnID = model.xMID;
                    InPurpose.PurposeID = model.GLookUp_PurList_AsetTrans;
                    InPurpose.Amount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InPurpose.Status_Action = Status_Action;
                    InPurpose.RecID = System.Guid.NewGuid().ToString();

                    InNewParam.param_InsertPurpose = InPurpose;
                    if (model.USE_CROSS_REF)
                    {
                        // {1}.Update Cross Reference
                        Param_VoucherAssetTransfer_Update_CrossReference UpCrossRef = new Param_VoucherAssetTransfer_Update_CrossReference();
                        UpCrossRef.Cross_Ref_ID = model.xMID;
                        UpCrossRef.RecID = model.CROSS_M_ID;

                        InNewParam.param_UpdateCrossRef = UpCrossRef;
                        // {2}.Insert Profile Entry
                        if (model.Cmb_Asset_Type_AsetTrans.ToUpper() == "VEHICLES")
                        {
                            if (model.TextEdit1Tag_AsetTrans == "INCHARGE")
                            {

                            }
                        }

                        // Insert Profile
                        Parameter_Insert_Profile ParamProfile = new Parameter_Insert_Profile();
                        ParamProfile.AssetType = model.Cmb_Asset_Type_AsetTrans;
                        ParamProfile.AssetRefID = model.GLookUp_AssetList_AsetTrans;
                        ParamProfile.AssetNewID = xAssetRefID;
                        ParamProfile.AssetLocID = model.Look_LocList_AsetTrans;
                        ParamProfile.AssetOwner = model.TextEdit1Tag_AsetTrans;
                        ParamProfile.AssetOwnerID = model.Look_OwnList_AsetTrans;
                        ParamProfile.AssetUse = model.Cmd_PUse_AsetTrans;
                        ParamProfile.AssetQty = Convert.ToDouble(model.Txt_Qty_AsetTrans);
                        ParamProfile.AssetAmt = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                        ParamProfile.CenID = BASE._open_Cen_ID;
                        ParamProfile.TrID = model.xMID;
                        //  Dim InsertResult As DataTable = BASE._AssetTransfer_DBOps.Insert_ProfileBySP(, , , , , , , , , , )
                        InNewParam.param_InsertProfile = ParamProfile;


                    }

                    //FCRA Insert Process
                    if (model.Astr_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.Astr_SplVchrReferenceSelected.Split(',');
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

                    if (!(bool)BASE._AssetTransfer_DBOps.Insert_AssetTransfer_Txn(InNewParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = Messages.SaveSuccess;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, CashbookGridPK = model.xMID+model.xID1_AsetTrans }, JsonRequestBehavior.AllowGet);
                }
                Param_Txn_Update_VoucherAssetTransfer EditParam = new Param_Txn_Update_VoucherAssetTransfer();
                if (model.Tag == Common.Navigation_Mode._Edit) // edit
                {
                    // MASTER ENTRY
                    Parameter_UpdateMasterInfo_VoucherAssetTransfer UpMaster = new Parameter_UpdateMasterInfo_VoucherAssetTransfer();
                    UpMaster.VNo = model.Txt_V_NO_AsetTrans ?? string.Empty;
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        UpMaster.TDate = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMaster.TDate = model.Txt_V_Date_AsetTrans.ToString();
                    }

                    UpMaster.SubTotal = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    UpMaster.Cash = 0;
                    UpMaster.Bank = 0;
                    UpMaster.AssetRef_ID = model.GLookUp_AssetList_AsetTrans;
                    UpMaster.AssetTrf_Amt = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    UpMaster.AssetTrf_Qty = Convert.ToDouble(model.Txt_Qty_AsetTrans);
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        UpMaster.AssetTrf_Date = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        UpMaster.AssetTrf_Date = model.Txt_V_Date_AsetTrans.ToString();
                    }

                    UpMaster.AssetTrf_Type = model.Cmb_Asset_Type_AsetTrans;
                    // UpMaster.Status_Action = Status_Action
                    UpMaster.RecID = model.xMID;

                    EditParam.param_UpdateMaster = UpMaster;

                    EditParam.MID_DeleteItems = model.xMID;

                    EditParam.MID_DeletePayment = model.xMID;

                    EditParam.MID_DeletePurpose = model.xMID;

                    EditParam.MID_Delete = model.xMID;
                    // Transaction info
                    // 1st Entry
                    Parameter_Insert_VoucherAssetTransfer InParam1 = new Parameter_Insert_VoucherAssetTransfer();
                    InParam1.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer;
                    InParam1.VNo = model.Txt_V_NO_AsetTrans ?? string.Empty;
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        InParam1.TDate = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam1.TDate = model.Txt_V_Date_AsetTrans.ToString();
                    }

                    InParam1.ItemID = model.GLookUp_ItemList_AsetTrans;
                    InParam1.Type = model.iTrans_Type;
                    if (model.iTrans_Type == "DEBIT")
                    {
                        InParam1.Cr_Led_ID = "";
                    }
                    else
                    {
                        InParam1.Cr_Led_ID = Cr_Led_id;
                    }

                    if (model.iTrans_Type == "CREDIT")
                    {
                        InParam1.Dr_Led_ID = "";
                    }
                    else
                    {
                        InParam1.Dr_Led_ID = Dr_Led_id;
                    }

                    InParam1.Sub_Cr_Led_ID = "";
                    InParam1.Sub_Dr_Led_ID = "";
                    InParam1.Mode = "JOURNAL";
                    InParam1.Amount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InParam1.AB_ID_1 = Tr_AB_ID_1;
                    InParam1.AB_ID_2 = Tr_AB_ID_2;
                    InParam1.Narration = model.Txt_Narration_AsetTrans ?? string.Empty;
                    InParam1.Remarks = model.Txt_Remarks_AsetTrans ?? string.Empty;
                    InParam1.Reference = model.Txt_Reference_AsetTrans ?? string.Empty;
                    InParam1.Tr_M_ID = model.xMID;
                    InParam1.TxnSrNo = 1;
                    InParam1.AssetTrf_Qty = Convert.ToDouble(model.Txt_Qty_AsetTrans);
                    InParam1.AssetTrf_RefItemID = model.GLookUp_AssetList_AsetTrans;
                    InParam1.Status_Action =Status_Action;
                    InParam1.RecID = model.xID1_AsetTrans;

                    EditParam.param_InsertTxnInfo1 = InParam1;
                    // Transaction info
                    // 2nd Entry
                    Parameter_Insert_VoucherAssetTransfer InParam2 = new Parameter_Insert_VoucherAssetTransfer();
                    InParam2.TransCode = (int)Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer;
                    InParam2.VNo = model.Txt_V_NO_AsetTrans ?? string.Empty;
                    if (IsDate(model.Txt_V_Date_AsetTrans.ToString()))
                    {
                        InParam2.TDate = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    }
                    else
                    {
                        InParam2.TDate = model.Txt_V_Date_AsetTrans.ToString();
                    }
                    InParam2.ItemID = model.aItem_ID;
                    if (model.iTrans_Type == "DEBIT")
                    {
                        InParam2.Type = "CREDIT";
                    }
                    else
                    {
                        InParam2.Type = "DEBIT";
                    }

                    if (model.iTrans_Type == "DEBIT")
                    {
                        InParam2.Cr_Led_ID = Cr_Led_id;
                    }
                    else
                    {
                        InParam2.Cr_Led_ID = "";
                    }

                    if (model.iTrans_Type == "CREDIT")
                    {
                        InParam2.Dr_Led_ID = Dr_Led_id;
                    }
                    else
                    {
                        InParam2.Dr_Led_ID = "";
                    }

                    InParam2.Sub_Cr_Led_ID = "";
                    InParam2.Sub_Dr_Led_ID = "";
                    InParam2.Mode = "JOURNAL";
                    InParam2.Amount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InParam2.AB_ID_1 = Tr_AB_ID_1;
                    InParam2.AB_ID_2 = Tr_AB_ID_2;
                    InParam2.Narration = model.Txt_Narration_AsetTrans ?? string.Empty;
                    InParam2.Remarks = model.Txt_Remarks_AsetTrans ?? string.Empty;
                    InParam2.Reference = model.Txt_Reference_AsetTrans ?? string.Empty;
                    InParam2.Tr_M_ID = model.xMID;
                    InParam2.TxnSrNo = 2;
                    InParam2.AssetTrf_Qty = Convert.ToDouble(model.Txt_Qty_AsetTrans);
                    InParam2.AssetTrf_RefItemID = model.GLookUp_AssetList_AsetTrans;
                    InParam2.Status_Action = Status_Action;
                    InParam2.RecID = model.xID2;

                    EditParam.param_InsertTxnInfo2 = InParam2;
                    // PAYMENT  
                    Parameter_InsertAandLPayment_VoucherAssetTransfer InPay = new Parameter_InsertAandLPayment_VoucherAssetTransfer();
                    InPay.TxnMID = model.xMID;
                    InPay.Type = "TRANSFER";
                    InPay.SrNo = "1";
                    InPay.RefID = model.GLookUp_AssetList_AsetTrans;
                    InPay.RefAmount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InPay.Status_Action = Status_Action;
                    InPay.RecID = System.Guid.NewGuid().ToString();

                    EditParam.param_InsertAandLPayment = InPay;
                    // purpose()
                    Parameter_InsertPurpose_VoucherAssetTransfer InPurpose = new Parameter_InsertPurpose_VoucherAssetTransfer();
                    InPurpose.TxnID = model.xMID;
                    InPurpose.PurposeID = model.GLookUp_PurList_AsetTrans;
                    InPurpose.Amount = Convert.ToDouble(model.Txt_SaleAmt_AsetTrans);
                    InPurpose.Status_Action =Status_Action;
                    InPurpose.RecID = System.Guid.NewGuid().ToString();

                    EditParam.param_InsertPurpose = InPurpose;

                    //FCRA Update Process               
                    if (model.Astr_SplVchrReferenceSelected != null)
                    {
                        var SplVchrRefsSplit = model.Astr_SplVchrReferenceSelected.Split(',');
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
                    if (!(bool)BASE._AssetTransfer_DBOps.Update_AssetTransfer_Txn(EditParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }
                    jsonParam.message = "Updated Successfully";
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam, CashbookGridPK = model.xMID + model.xID1_AsetTrans }, JsonRequestBehavior.AllowGet);
                }

                Param_Txn_Delete_VoucherAssetTransfer DelParam = new Param_Txn_Delete_VoucherAssetTransfer();
                //  BOOKMARK : SAVE_CLICK - DELETION BEGINS
                if (model.Tag == Common.Navigation_Mode._Delete)     // DELETE
                {
                    if (model.Cmb_Asset_Type_AsetTrans.ToUpper() == "MOVABLE ASSETS" || model.Cmb_Asset_Type_AsetTrans.ToUpper() == "LAND & BUILDING")    // Movable Assets or Property
                    {
                        if (BASE.IsInsuranceAudited())
                        {
                            jsonParam.message = "Sorry! Deletion cannot be done after the completion of Insurance Audit";
                            jsonParam.title = "Information..";
                            jsonParam.result = false;
                            return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    DelParam.MID_DeletePayment = model.xMID;

                    DelParam.MID_DeletePurpose = model.xMID;

                    if (model.iTrans_Type == "CREDIT")  // Asset creation happens only in case of "Transfer From" entry 'Bug #5535
                    {
                        DelParam.MID_DeleteGS = model.xMID;
                        DelParam.MID_DeleteAssets = model.xMID;
                        DelParam.MID_DeleteLS = model.xMID;
                        DelParam.MID_DeleteVehicle = model.xMID;
                        DelParam.MID_DeleteFD = model.xMID;
                        DelParam.DeleteComplexBuilding = model.GLookUp_AssetList_AsetTrans;
                        DelParam.DelExtInfo = model.GLookUp_AssetList_AsetTrans;
                        DelParam.DelDocInfo = model.GLookUp_AssetList_AsetTrans;
                        DelParam.MID_DeleteLB = model.xMID;
                        DelParam.MID_DeleteAdvance = model.xMID;
                        DelParam.MID_DeleteDeposit = model.xMID;
                        DelParam.MID_DeleteLiability = model.xMID;
                        DelParam.MID_DeleteWIP = model.xMID;
                    }

                    DelParam.MID_Delete = model.xMID;
                    DelParam.MID_DeleteMaster = model.xMID;
                    DelParam.Txn_Date = Convert.ToDateTime(model.Txt_V_Date_AsetTrans).ToString(BASE._Server_Date_Format_Short);
                    if (!(bool)BASE._AssetTransfer_DBOps.Delete_AssetTransfer_Txn(DelParam))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                    }

                    jsonParam.message = Messages.DeleteSuccess;
                    jsonParam.title = model.TitleX;
                    jsonParam.result = true;
                    jsonParam.closeform = true;
                    return Json(new { jsonParam }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    message = "",
                    result = true
                }, JsonRequestBehavior.AllowGet) ;
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

        public string FindLocationUsage_AsetTrans(string PropertyID = "", bool Exclude_Sold_TF = true)
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
                    Message = "Property being transferred in this Voucher is being used in Another Page as Location. . . !" + "<br><br>" + "Name : " + UsedPage;
                    break;
                }
            }
            return Message;
        }

     

        #region "Start--> LookupEdit Events"
        public void RefreshItemList_AsetTrans(bool USE_CROSS_REF = false)
        {
            DataTable d1 = BASE._AssetTransfer_DBOps.GetLedgerItems(BASE.Is_HQ_Centre);
            DataView dview = new DataView(d1);
            if (USE_CROSS_REF == false)
            {
                dview.RowFilter = "[ITEM_TRANS_TYPE]='DEBIT'";
            }
            dview.Sort = "ITEM_NAME";
            var data = DatatableToModel.DataTabletoVoucherCollectionLookUp_GetItemList(dview.ToTable());
            ItemList_AsetTrans = data;
        }
        [HttpGet]
        public ActionResult AssetTransfer_LookUp_GetItemList(DataSourceLoadOptions loadOptions, bool USE_CROSS_REF = false, bool DDRefresh = false)
        {
            if (ItemList_AsetTrans == null || DDRefresh == true)
            {
                RefreshItemList_AsetTrans(USE_CROSS_REF);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(ItemList_AsetTrans, loadOptions)), "application/json");
        }
        public void RefreshGetFromCenterList(string iTrans_Type)
        {
            DataTable D1 = null;
            if (iTrans_Type == "DEBIT")
            {
                D1 = BASE._AssetTransfer_DBOps.GetFrCenterList("", "", "'" + BASE._open_Cen_Rec_ID + "'", "");
            }
            else
            {
                D1 = BASE._AssetTransfer_DBOps.GetFrCenterList("", "", "", "'" + BASE._open_Cen_Rec_ID + "'");
            }
            if (D1 != null)
            {
                DataView dview = new DataView(D1);
                dview.Sort = "FR_CEN_NAME,FR_UID";
                var data = DatatableToModel.DataTabletoVoucherAssetTransferLookUp_GetFRCenterList(dview.ToTable());
                GetFromCenterList_AsetTrans = data;
            }
        }
        public ActionResult AssetTransfer_LookUp_GetFromCenterList(DataSourceLoadOptions loadOptions, string iTrans_Type = "", bool DDRefresh = false)
        {
            if (GetFromCenterList_AsetTrans == null || DDRefresh == true)
            {
                RefreshGetFromCenterList(iTrans_Type);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetFromCenterList_AsetTrans, loadOptions)), "application/json");
        }
        public void RefreshGetToCenterList(string iTrans_Type = "")
        {
            DataTable D1 = null;
            if (iTrans_Type == "DEBIT")
            {
                D1 = BASE._AssetTransfer_DBOps.GetToCenterList("", "", "", "'" + BASE._open_Cen_Rec_ID + "'");
            }
            else
            {
                D1 = BASE._AssetTransfer_DBOps.GetToCenterList("", "", "'" + BASE._open_Cen_Rec_ID + "'", "");
            }
            if (D1 != null)
            {
                DataView dview = new DataView(D1);
                dview.Sort = "TO_CEN_NAME,TO_UID";
                var data = DatatableToModel.DataTabletoVoucherAssetTransferLookUp_GetTOCenterList(dview.ToTable());
                GetToCenterList_AsetTrans = data;
            }
        }
        [HttpGet]
        public ActionResult AssetTransfer_LookUp_GetToCenterList(DataSourceLoadOptions loadOptions, string iTrans_Type = "", bool DDRefresh = false) /*string selectData = "", string IsReadOnly = "", string CROSS_M_ID = "", string iFR_CEN_ID = "", string USE_CROSS_REF = "", string Cmb_Asset_Type_SelectedIndex = "", string Cmb_Asset_Type_SelectedValue = "")*/
        {
            if (GetToCenterList_AsetTrans == null || DDRefresh == true)
            {
                RefreshGetToCenterList(iTrans_Type);
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetToCenterList_AsetTrans, loadOptions)), "application/json");
        }
        public void Refresh_GetLocList_AsetTrans()
        {
            DataTable d2 = BASE._AssetTransfer_DBOps.GetAssetLocations(BASE._open_Cen_ID);
            DataView dview = new DataView(d2);
            var data = DatatableToModel.DataTabletoVoucherAssetTransferLookUp_GetLocationList(dview.ToTable());
            GetLocList_AsetTrans = data;
        }
        public ActionResult AssetTransfer_LookUp_GetLocList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetLocList_AsetTrans == null || DDRefresh == true)
            {
                Refresh_GetLocList_AsetTrans();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetLocList_AsetTrans, loadOptions)), "application/json");
        }
        public void Refresh_GetPurList_AsetTrans()
        {
            DataTable d1 = BASE._AssetTransfer_DBOps.GetPurposes();
            DataView dview = new DataView(d1);
            if (d1 != null)
            {
                var data = DatatableToModel.DataTabletoVoucherAssetTransferLookUp_GetPurList(dview.ToTable());
                GetPurList_AsetTrans = data;
            }
        }
        public ActionResult AssetTransfer_LookUp_GetPurList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetPurList_AsetTrans == null || DDRefresh == true)
            {
                Refresh_GetPurList_AsetTrans();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetPurList_AsetTrans, loadOptions)), "application/json");
        }
        public void Refresh_GetOwnerList_AsetTrans()
        {
            GetOwnerList_AsetTrans = BASE._L_B_Voucher_DBOps.GetOwners();
        }
        public ActionResult AssetTransfer_LookUp_GetOwnerList(DataSourceLoadOptions loadOptions, bool DDRefresh = false)
        {
            if (GetOwnerList_AsetTrans == null || DDRefresh == true)
            {
                Refresh_GetOwnerList_AsetTrans();
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(GetOwnerList_AsetTrans, loadOptions)), "application/json");
        }
        public void FillCentres()
        {
            DataTable Centres = BASE._AssetTransfer_DBOps.GetToCenterList("", "", "", "");
            DataView dview = new DataView(Centres);
            DataRow ROW;
            ROW = Centres.NewRow();
            ROW["TO_CEN_NAME"] = "All Centres";
            ROW["TO_CEN_ID"] = 0;
            dview.Table.Rows.InsertAt(ROW, 0);
            dview.Sort = "TO_CEN_NAME";
            PendingCenterDDList = DatatableToModel.DataTableto_Cen_List_Pending(dview.ToTable());
        }
        public ActionResult LookUp_Cen_List_AsetTrans(DataSourceLoadOptions loadOptions)
        {
            PendingCenterDDList = PendingCenterDDList ?? new List<Lookup_Cen_List_AsetTrans>();
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(PendingCenterDDList, loadOptions)), "application/json");
        }
        public ActionResult GLookUp_Get_AssetList_AsetTrans(DataSourceLoadOptions loadOptions)
        {
            if (AssetList_Data_AsetTrans == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<AssetTransfer_AssetList>(), loadOptions)), "application/json");
            }
            else
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(AssetList_Data_AsetTrans, loadOptions)), "application/json");
            }
        }

        #endregion

        public ActionResult Get_Asset_Items(string AssetRecID = "", bool USE_CROSS_REF = false, string Cmb_Asset_Type_SelectedValue = "", string Tag = "", string iFR_CEN_ID = "", string CROSS_M_ID = "", string xMID = "", string iTransType = "")
        {
            Param_Get_AssetTf_Asset_Listing AssetParam = new Param_Get_AssetTf_Asset_Listing();
            AssetParam.Next_YearID = BASE._next_Unaudited_YearID;
            AssetParam.Prev_YearId = BASE._prev_Unaudited_YearID;
            int xCen_ID = BASE._open_Cen_ID;
            var ActionMethod = (Common.Navigation_Mode)Enum.Parse(typeof(Common.Navigation_Mode), Tag);
            if (USE_CROSS_REF)
            {
                xCen_ID = Convert.ToInt32(iFR_CEN_ID);
                AssetParam.TR_M_ID = CROSS_M_ID;   //  TO ALLOW ASSETS AND ITS VALUES TO BE SHOWN WITHOUT EFFECT OF 'TRANSFER TO' VOUCHER IN SENDING CENTRE
                DataTable d1 = BASE._AssetTransfer_DBOps.GetUnAuditedFinancialYearOfTransferorCentre(xCen_ID);
                if (d1.Rows.Count > 0)
                {
                    if (Convert.ToInt32(d1.Rows[0]["COD_YEAR_ID"]) > BASE._open_Year_ID)
                    {
                        AssetParam.Next_YearID = Convert.ToInt32(d1.Rows[0]["COD_YEAR_ID"]);
                    }
                    else
                    {
                        AssetParam.Prev_YearId = Convert.ToInt32(d1.Rows[0]["COD_YEAR_ID"]);
                    }
                }
                if (d1.Rows.Count > 1)
                {
                    if (Convert.ToInt32(d1.Rows[1]["COD_YEAR_ID"]) > BASE._open_Year_ID)
                    {
                        AssetParam.Next_YearID = Convert.ToInt32(d1.Rows[1]["COD_YEAR_ID"]);
                    }
                    else
                    {
                        AssetParam.Prev_YearId = Convert.ToInt32(d1.Rows[1]["COD_YEAR_ID"]);
                    }
                }
            }
            AssetParam.Cen_Id = xCen_ID;
            if ((ActionMethod == Common.Navigation_Mode._Edit || ActionMethod == Common.Navigation_Mode._Delete || ActionMethod == Common.Navigation_Mode._View) && iTransType == "DEBIT")
            {
                AssetParam.TR_M_ID = xMID;
            }
            if (Cmb_Asset_Type_SelectedValue.ToUpper() == "GOLD")
            {
                AssetParam.Asset_Profile = AssetProfiles.GOLD;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "SILVER")
            {
                AssetParam.Asset_Profile = AssetProfiles.SILVER;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "VEHICLES")
            {
                AssetParam.Asset_Profile = AssetProfiles.VEHICLES;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "LIVESTOCK")
            {
                AssetParam.Asset_Profile = AssetProfiles.LIVESTOCK;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "MOVABLE ASSETS")
            {
                AssetParam.Asset_Profile = AssetProfiles.OTHER_ASSETS;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "LAND & BUILDING")
            {
                AssetParam.Asset_Profile = AssetProfiles.LAND_BUILDING;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "FD")
            {
                AssetParam.Asset_Profile = AssetProfiles.FD;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "ADVANCES")
            {
                AssetParam.Asset_Profile = AssetProfiles.ADVANCES;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "OTHER DEPOSITS")
            {
                AssetParam.Asset_Profile = AssetProfiles.OTHER_DEPOSITS;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "OTHER LIABILITIES")
            {
                AssetParam.Asset_Profile = AssetProfiles.OTHER_LIABILITIES;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "OPENING")
            {
                AssetParam.Asset_Profile = AssetProfiles.OTHER_OPENING_BALANCES;
            }
            else if (Cmb_Asset_Type_SelectedValue.ToUpper() == "WIP")
            {
                AssetParam.Asset_Profile = AssetProfiles.WIP;
            }
            if (AssetRecID != null && AssetRecID != "")
            {
                AssetParam.Asset_RecID = AssetRecID;
            }
            DataTable ASSET_TABLE = BASE._AssetTransfer_DBOps.Get_AssetTf_Asset_Listing(AssetParam);
            AssetList_Data_AsetTrans = DatatableToModel.DataTabletoVoucherAssetTransferLookUp_GetAssetList(ASSET_TABLE);
            return Json(new { message = "", result = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AssetTransfer_Export_Options()
        {
            if (!CheckRights(BASE, ClientScreen.Accounts_Voucher_AssetTransfer, "Export"))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('popup_Asset_Transfer_ListPreview_Window','Not Allowed','No Rights');</script>");
            }
            return PartialView();
        }
        public bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public int Val(string sInput)
        {
            string sOutput = "0";
            MatchCollection oMatches = Regex.Matches(sInput, "\\d+");
            foreach (Match oMatch in oMatches)
            {
                sOutput += oMatch.ToString();
            }
            return Convert.ToInt32(sOutput);
        }
        public void SessionClear()
        {
            ClearBaseSession("_ATVou");
        }
        public void SessionClear_pending()
        {
            ClearBaseSession("_PendingATVou");
        }

    }
}