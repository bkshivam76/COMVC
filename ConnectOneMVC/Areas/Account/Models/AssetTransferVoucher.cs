using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common_Lib;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class AssetTransferVoucher
    {
        public string iVoucher_Type { get; set; }
        public string iRef_ItemID { get; set; }
        public string iTrans_Type { get; set; }
        public string iAsset_Type { get; set; }
        public string iLed_ID { get; set; }
        public string iCon_Led_ID { get; set; }
        public double iMinValue { get; set; }
        public double iMaxValue { get; set; }

        public string aItem_ID { get; set; }
        public string aLed_ID { get; set; }

        public string iSpecific_ItemID { get; set; }
        public DateTime FR_REC_EDIT_ON { get; set; }
        public string iTO_CEN_ID { get; set; }
        public string iFR_CEN_ID { get; set; }
        public string HQ_IDs { get; set; }

        public bool USE_CROSS_REF { get; set; }
        public string CROSS_REF_ID { get; set; }
        public string CROSS_M_ID { get; set; }

        public string iParty_Req { get; set; }
        public string iProfile { get; set; }

        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime Info_MaxEditedOn { get; set; }
        public bool OwnershipRequire { get; set; }
        public string Property_Name { get; set; }

        public string xID1_AsetTrans { get; set; }
        public string xID2 { get; set; }
        public string xMID { get; set; }
        [Required(ErrorMessage = "Item Is Required")]
        public string GLookUp_ItemList_AsetTrans { get; set; }
        [Required(ErrorMessage = "Voucher Date Required")]
        public DateTime? Txt_V_Date_AsetTrans { get; set; }
        public string BE_Item_Head_AsetTrans { get; set; }
        public string Txt_V_NO_AsetTrans { get; set; }
        [Required(ErrorMessage = "From Center Required")]
        public string GLookUp_FrCen_List_AsetTrans { get; set; }
        public string BE_Fr_Incharge_AsetTrans { get; set; }
        public string BE_Fr_Institute_AsetTrans { get; set; }
        public string BE_Fr_Pad_No_AsetTrans { get; set; }
        public string BE_Fr_Tel_No_AsetTrans { get; set; }
        public string BE_Fr_UID_AsetTrans { get; set; }
        public string BE_Fr_Zone_AsetTrans { get; set; }

        [Required(ErrorMessage = "To Center Required")]
        public string GLookUp_ToCen_List_AsetTrans { get; set; }
        public string BE_To_Incharge_AsetTrans { get; set; }
        public string BE_To_Institute_AsetTrans { get; set; }
        public string BE_To_Pad_No_AsetTrans { get; set; }
        public string BE_To_Tel_No_AsetTrans { get; set; }
        public string BE_To_UID_AsetTrans { get; set; }
        public string BE_To_Zone_AsetTrans { get; set; }

        [Required(ErrorMessage = "Asset Type Required")]
        public string Cmb_Asset_Type_AsetTrans { get; set; }
        public string Cmd_PUse_AsetTrans { get; set; }
        [Required(ErrorMessage = "Purpose Required")]
        public string GLookUp_PurList_AsetTrans { get; set; }
        [Required(ErrorMessage = "Asset Name Required")]
        public string GLookUp_AssetList_AsetTrans { get; set; }
        public string Look_LocList_AsetTrans { get; set; }
        public string Look_OwnList_AsetTrans { get; set; }
        public string Txt_Desc_AsetTrans { get; set; }
        public double? Txt_CurQty_AsetTrans { get; set; }
        public double? Txt_Qty_AsetTrans { get; set; }
        public double? Txt_SaleAmt_AsetTrans { get; set; }

        public string Txt_Narration_AsetTrans { get; set; }
        public string Txt_Remarks_AsetTrans { get; set; }
        public string Txt_Reference_AsetTrans { get; set; }    
  
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }    
        public string ActionMethod { get; set; }       
        public string TextEdit1Text_AsetTrans { get; set; }
        public string TextEdit1Tag_AsetTrans { get; set; }
        public double? Txt_SaleAmtTag_AsetTrans { get; set; }

        public DateTime? AssetList_REF_CREATION_DATE { get; set; }
        public DateTime? AssetList_REC_EDIT_ON { get; set; }     
        public double? AssetList_REF_AMT { get; set; }
        public DateTime? LocList_REC_EDIT_ON { get; set; }     
     
        public string TitleX { get; set; }
        public string Me_Text { get; set; }
        public bool pending_list { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_Astr { get; set; }
        public string Astr_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_Astr { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_Astr { get; set; }
    }
    [Serializable]
    public class AssetTransfer_FR_Ins_List
    {
        public string FR_CEN_NAME { get; set; }
        public string FR_PAD_NO { get; set; }
        public string FR_UID { get; set; }
        public string FR_INCHARGE { get; set; }
        public string FR_ZONE { get; set; }
        public int FR_CEN_ID { get; set; }
        public string FR_ID { get; set; }
        public string FR_TEL_NO { get; set; }
        public string FR_INSTITUTE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_TO_Ins_List
    {
        public string TO_CEN_NAME { get; set; }
        public string TO_PAD_NO { get; set; }
        public string TO_UID { get; set; }
        public string TO_INCHARGE { get; set; }
        public string TO_ZONE { get; set; }
        public int TO_CEN_ID { get; set; }
        public string TO_ID { get; set; }
        public string TO_TEL_NO { get; set; }
        public string TO_INSTITUTE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_LocationList
    {
        public string LocationName { get; set; }
        public string AL_ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public string MatchedType { get; set; }
        public string MatchedName { get; set; }
        public string MatchedInstt { get; set; }
        public decimal? Final_Amount { get; set; }
    }
    [Serializable]
    public class AssetTransfer_PurList
    {
        public string PUR_ID { get; set; }
        public string PUR_NAME { get; set; }
    }
    [Serializable]
    public class AssetTransfer_OwnerList
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class AssetTransfer_AssetList_GOLD
    {
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_LOC_ID { get; set; }
        public string REF_OWNERSHIP { get; set; }
        public string REF_OWNERSHIP_ID { get; set; }
        public string REF_USE { get; set; }
        public decimal? REF_AMT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_AssetList_SILVER
    {
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public decimal? SALE_QTY { get; set; }
        public string REF_LOC_ID { get; set; }
        public decimal REF_AMT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_AssetList_VEHICLES
    {
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public decimal? SALE_QTY { get; set; }
        public string REF_OWNERSHIP { get; set; }
        public string REF_OWNERSHIP_ID { get; set; }
        public string REF_USE { get; set; }
        public string REF_LOC_ID { get; set; }
        public decimal? REF_AMT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_AssetList_LIVESTOCK
    {
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public decimal SALE_QTY { get; set; }
        public string REF_LOC_ID { get; set; }
        public decimal REF_AMT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_AssetList_MOVABLE_ASEETS
    {
        public string AI_TYPE { get; set; }
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
        public int? ITEM_CON_MIN_VALUE { get; set; }
        public int? ITEM_CON_MAX_VALUE { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public decimal? SALE_QTY { get; set; }
        public string REF_LOC_ID { get; set; }
        public decimal? REF_AMT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public string AI_TYPE1 { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_AssetList_LAND_AND_BUILDING
    {
        public string REF_NAME { get; set; }
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public decimal? SALE_QTY { get; set; }
        public string REF_OWNERSHIP { get; set; }
        public string REF_OWNERSHIP_ID { get; set; }
        public string REF_USE { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
    [Serializable]
    public class AssetTransfer_AssetList_FD
    {
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public int? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public DateTime? FD_DATE { get; set; }
        public DateTime? FD_AS_DATE { get; set; }
        public decimal? REF_AMT { get; set; }
        public decimal? FD_INT_RATE { get; set; }
        public string FD_INT_PAY_COND { get; set; }
        public DateTime? FD_MAT_DATE { get; set; }
        public decimal? FD_MAT_AMT { get; set; }
        public string BA_CUST_NO { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public string FD_Status { get; set; }
        public int? YearID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
    [Serializable]
    public class Lookup_Cen_List_AsetTrans
    {
        public int? TO_CEN_ID { get; set; }
        public string TO_ID { get; set; }
        public string TO_CEN_NAME { get; set; }
        public string TO_INCHARGE { get; set; }

        public string TO_PAD_NO { get; set; }
        public string TO_TEL_NO { get; set; }
        public string TO_UID { get; set; }
        public string TO_ZONE { get; set; }
    }
    [Serializable]
    public class PendingAssetTransferInfo
    {
        public String ITEM_ID { get; set; }
        public String Description { get; set; }
        public String Centre_Name{ get; set; }
        public String Incharge { get; set; }
        public String Contact_No{ get; set; }
        public String Centre_UID{ get; set; } 
        public String No{ get; set; }
        public String CEN_ID { get; set; }
        public DateTime Date { get; set; }
        public String Asset_Type{ get; set; } 
        public String ASSET_ITEM_ID { get; set; }
        public String Asset { get; set; }
        public Decimal Qty_Weight{ get; set; } 
        public Decimal Amount { get; set; }
        public String ASSET_REF_ID { get; set; }
        public String Purpose { get; set; }
        public String PUR_ID { get; set; }
        public String ID { get; set; }
        public String M_ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
        public String Narration { get; set; } 

    }
    [Serializable]
    public class PendingAssetTransferItemList
    {
        public string ItemList { get; set; }
        public string ITem_ID { get; set; }
        public string iTransType { get; set; }
        public string Led_Name { get; set; }
    }

    [Serializable]
    public class AssetTransfer_AssetList
    {
        public string AI_TYPE { get; set; }
        public string AI_TYPE1 { get; set; }
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public DateTime? FD_DATE { get; set; }
        public DateTime? FD_AS_DATE { get; set; }
        public decimal? FD_INT_RATE { get; set; }
        public string FD_INT_PAY_COND { get; set; }
        public DateTime? FD_MAT_DATE { get; set; }
        public decimal? FD_MAT_AMT { get; set; }
        public string BA_CUST_NO { get; set; }
        public string FD_Status { get; set; }
        public int? YearID { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
        public int? ITEM_CON_MIN_VALUE { get; set; }
        public int? ITEM_CON_MAX_VALUE { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public decimal? SALE_QTY { get; set; }
        public string REF_LOC_ID { get; set; }
        public string REF_OWNERSHIP { get; set; }
        public string REF_OWNERSHIP_ID { get; set; }
        public string REF_NAME { get; set; }
        public string REF_USE { get; set; }
        public decimal? REF_AMT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }
    }
}