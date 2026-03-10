using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class SaleAssetModel
    {
        public string iVoucher_Type { get; set; }
        public string iRef_ItemID { get; set; }
        public string iTrans_Type { get; set; }
        public string iAsset_Type { get; set; }
        public string iLed_ID { get; set; }
        public string iCon_Led_ID { get; set; }
        public string iSpecific_ItemID { get; set; }
        public int? Cnt_BankAccount { get; set; }
        public string iParty_Req { get; set; }
        public string iProfile { get; set; }
        public string iTDS_CODE { get; set; }
        public string iOffSet_ID { get; set; }
        public string iOffSet_Item { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime? Info_MaxEditedOn { get; set; }
        public string SaleProfit_ItemID { get; set; }
        public string SaleLoss_ItemID { get; set; }
        public string SaleProfit_Loss_LedID { get; set; }
        //Global variables till here

        public string REC_EDIT_ON_Party { get; set; }
        public string REC_EDIT_ON_Bank { get; set; }
        public string REC_EDIT_ON_AssetName { get; set; }
        public string ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string xMID { get; set; }
        public string xID { get; set; }
        public string GLookUp_ItemList_SaleA { get; set; }
        [Required(ErrorMessage = "Date Incorrect/Blank...!")]
        public DateTime? Txt_V_Date_SaleA { get; set; }
        public string BE_Item_Head_SaleA { get; set; }
        public string Txt_V_NO_SaleA { get; set; }
        public string GLookUp_PartyList1_SaleA { get; set; }
        public string BE_City_SaleA { get; set; }
        public string Cmb_Asset_Type_SaleA { get; set; }
        public string GLookUp_AssetList_SaleA { get; set; }
        public string GLookUp_AssetList_Text_SaleA { get; set; }
        public string Txt_Desc_SaleA { get; set; }
        public double? Txt_CurQty_SaleA { get; set; }
        [Required(ErrorMessage = "Payment Mode is required...!")]
        public string Cmd_Mode_SaleA { get; set; }
        public DateTime? Txt_SDate_SaleA { get; set; }
        public double? Txt_Qty_SaleA { get; set; }
        public double? Txt_SaleAmt_SaleA { get; set; }
        public double? Txt_Amount_SaleA { get; set; }
        public double? Txt_Diff_SaleA { get; set; }
        public string GLookUp_RefBankList_SaleA { get; set; }
        public string Txt_Ref_Branch_SaleA { get; set; }
        public string Txt_Ref_No_SaleA { get; set; }
        public DateTime? Txt_Ref_Date_SaleA { get; set; }
        public DateTime? Txt_Ref_CDate_SaleA { get; set; }
        public string GLookUp_BankList_SaleA { get; set; }
        public string BE_Bank_Branch_SaleA { get; set; }
        public string BE_Bank_Acc_No_SaleA { get; set; }
        public string GLookUp_PurList_SaleA { get; set; }
        public string Txt_Narration_SaleA { get; set; }
        public string Txt_Remarks_SaleA { get; set; }
        public string Txt_Reference_SaleA { get; set; }
        public string lbl_Ref_No_Tag { get; set; }
        public string BE_Bank_Acc_No_Tag { get; set; }
        public DateTime? REF_CREATION_or_Purchase_Date { get; set; }
        public double? REF_AMT { get; set; }
        public double? REF_QTY { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_SaleA { get; set; }
        public string SaleA_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_SaleA { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_SaleA { get; set; }        

    }
    [Serializable]
    public class SaleAssetBankNameList
    {
        public string BANK_NAME { get; set; }
        public string BI_SHORT_NAME { get; set; }
        public string BANK_BRANCH { get; set; }
        public string BANK_ACC_NO { get; set; }
        public string BA_ID { get; set; }
        public string BANK_ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
    }
    [Serializable]
    public class SaleTransferItemList
    {
        public string ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string LED_NAME { get; set; }
        public string ITEM_TRANS_TYPE { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
        public string ITEM_PARTY_REQ { get; set; }
        public string ITEM_PROFILE { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
        public string ITEM_CON_MIN_VALUE { get; set; }
        public string ITEM_CON_MAX_VALUE { get; set; }
        public string ITEM_TDS_CODE { get; set; }
        public string ITEM_LINK_REC_ID { get; set; }
        public string ITEM_OFFSET_REC_ID { get; set; }
        public string ITEM_OFFSET_NAME { get; set; }
    }
    [Serializable]
    public class SaleAssetPurposeList
    {
        public string PUR_NAME { get; set; }
        public string PUR_ID { get; set; }
    }
    [Serializable]
    public class SaleAssetPartyList
    {
        public string C_ID { get; set; }
        public string C_NAME { get; set; }
       public string C_CITY { get; set; } 
        public string C_PAN_NO { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }


    }
    [Serializable]
    public class SaleAsset_AssetList
    {
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public decimal? REF_QTY { get; set; }
        public string REF_DESC { get; set; }
        public string REF_MISC_ID { get; set; }
        public string REF_LED_ID { get; set; }
        public string REF_TRANS_TYPE { get; set; }
        public string REF_ID { get; set; }
        public string SALE_QTY { get; set; }        
        public decimal? REF_AMT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }

        public string ITEM_CON_LED_ID { get; set; }        
        public string AI_TYPE { get; set; }
    }
}
