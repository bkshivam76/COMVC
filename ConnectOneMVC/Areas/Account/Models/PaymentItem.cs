using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using Common_Lib;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class PaymentItem
    {
        [Display(Name = "Item Name")]
        [Required]
        public string GLookUp_ItemList { get; set; }
        [Display(Name = "Head")]
        public string BE_Item_Head { get; set; }
        [Display(Name = "Qty")]
        public decimal? Txt_Qty { get; set; }
        [Display(Name = "Unit")]
        public string Cmb_Unit { get; set; }
        [Display(Name = "Rate")]
        public decimal? Txt_Rate { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public decimal? Txt_Amt { get; set; }
        public string GLookUp_PartyList { get; set; }
        [Display(Name = "Section")]
        public string BE_TDS_Section { get; set; }

        [Display(Name = "TDS Rate")]
        public string BE_TDS_Rate { get; set; }
        [Display(Name = "TDS Deducted")]
        public decimal? TXT_TDS { get; set; }
        [Display(Name = "Remarks")]
        public string Txt_Remarks { get; set; }
        public Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int Sr { get; set; }
        public int ID { get; set; } 
        public List<Return_Items> ItemDataList { get; set; }
        public List<Return_Party> PartyDataList { get; set; }
        public List<Return_Purpose> PurposeDataList { get; set; }

        //----------------------------------------------------------------------------------------------------------------------------------------------
        public string iVoucher_Type { get; set; }
        public string iTrans_Type { get; set; }
        public string iLed_ID { get; set; }
        public string iLed_Type { get; set; }
        public string iCon_Led_Type { get; set; }
        public string iSpecific_ItemID { get; set; }
        public string iParty_Req { get; set; }
        public string iProfile { get; set; }
        public string iProfile_OLD { get; set; }
        public string iTDS_CODE { get; set; }
        public bool iSpecific_Allow { get; set; }
        public string iPur_ID { get; set; }
        public string iCond_Ledger_ID { get; set; }
        public decimal? iMinValue { get; set; }
        public decimal? iMaxValue { get; set; }
        public int Cnt_BankAccount { get; set; }
        public string Vno { get; set; }
        public DateTime? Vdt { get; set; }
        public bool Calc_Allow { get; set; }
        public string iPartyID { get; set; }
        public string iTxnM_ID { get; set; }
        public string iReference { get; set; }
        public string iRefType { get; set; }
        public bool AllowAmountEdit { get; set; }
        public bool Delete_Action { get; set; }
        public double? Payment_Credit_Amt { get; set; }
        public DateTime? Txt_V_Date { get; set; }
   
     
 
        public string LB_REC_ID { get; set; }

        public string X_LOC_ID { get; set; }
        public string GS_DESC_MISC_ID { get; set; }
        public decimal GS_ITEM_WEIGHT { get; set; }
        public string AI_TYPE { get; set; }
        public string AI_MAKE { get; set; }
        public string AI_MODEL { get; set; }
        public string AI_SERIAL_NO { get; set; }
        public string AI_PUR_DATE { get; set; }
        public decimal? AI_WARRANTY { get; set; }
        public byte[] AI_IMAGE { get; set; }
        public string LS_NAME { get; set; }
        public string LS_BIRTH_YEAR { get; set; }
        public string LS_INSURANCE { get; set; }
        public string LS_INSURANCE_ID { get; set; }
        public string LS_INS_POLICY_NO { get; set; }
        public string LS_INS_DATE { get; set; }
        public decimal? LS_INS_AMT { get; set; }
        public string VI_MAKE { get; set; }
        public string VI_MODEL { get; set; }
        public string VI_REG_NO_PATTERN { get; set; }
        public string VI_REG_NO { get; set; }
        public string VI_REG_DATE { get; set; }
        public string VI_OWNERSHIP { get; set; }
        public string VI_OWNERSHIP_AB_ID { get; set; }
        public string VI_DOC_RC_BOOK { get; set; }
        public string VI_DOC_AFFIDAVIT { get; set; }
        public string VI_DOC_WILL { get; set; }
        public string VI_DOC_TRF_LETTER { get; set; }
        public string VI_DOC_FU_LETTER { get; set; }
        public string VI_DOC_OTHERS { get; set; }
        public string VI_DOC_NAME { get; set; }
        public string VI_INSURANCE_ID { get; set; }
        public string VI_INS_POLICY_NO { get; set; }
        public string VI_INS_EXPIRY_DATE { get; set; }
        public string LB_PRO_TYPE { get; set; }
        public string LB_PRO_CATEGORY { get; set; }
        public string LB_PRO_USE { get; set; }
        public string LB_PRO_NAME { get; set; }
        public string LB_PRO_ADDRESS { get; set; }
        public string LB_ADDRESS1 { get; set; }
        public string LB_ADDRESS2 { get; set; }
        public string LB_ADDRESS3 { get; set; }
        public string LB_ADDRESS4 { get; set; }
        public string LB_STATE_ID { get; set; }
        public string LB_DISTRICT_ID { get; set; }
        public string LB_CITY_ID { get; set; }
        public string LB_PINCODE { get; set; }
        public string LB_OWNERSHIP { get; set; }
        public string LB_OWNERSHIP_PARTY_ID { get; set; }
        public string LB_SURVEY_NO { get; set; }
        public string LB_CON_YEAR { get; set; }
        public string LB_RCC_ROOF { get; set; }
        public string LB_PAID_DATE { get; set; }
        public string LB_PERIOD_FROM { get; set; }
        public string LB_PERIOD_TO { get; set; }
        public string LB_DOC_OTHERS { get; set; }
        public string LB_DOC_NAME { get; set; }
        public string LB_OTHER_DETAIL { get; set; }
        public decimal? LB_TOT_P_AREA { get; set; }
        public decimal? LB_CON_AREA { get; set; }
        public decimal? LB_DEPOSIT_AMT { get; set; }
        public decimal? LB_MONTH_RENT { get; set; }
        public decimal? LB_MONTH_O_PAYMENTS { get; set; }
        public DataTable LB_DOCS_ARRAY { get; set; }

        //public List<LB_DOCS_ARRAY_List> List_LB_DOCS_ARRAY { get; set; }
        public string List_LB_DOCS_ARRAY { get; set; }

        public DataTable LB_EXTENDED_PROPERTY_TABLE { get; set; }
        //public List<LB_EXTENDED_PROPERTY_TABLE_List> List_LB_EXTENDED_PROPERTY_TABLE { get; set; }
        public string List_LB_EXTENDED_PROPERTY_TABLE { get; set; }
        public DateTime? LB_REC_EDIT_ON { get; set; }
        public string X_TP_ID { get; set; }
        public string TP_BILL_NO { get; set; }
        public string TP_BILL_DATE { get; set; }
        public string TP_PERIOD_FROM { get; set; }
        public string TP_PERIOD_TO { get; set; }
        public string Ref_RecID { get; set; }
        public ArrayList TDS_Deduction_List { get; set; }

        [Required]
        [Display(Name = "Purpose")]
        public string GLookUp_PurList { get; set; }
        public string Item_Name { get; set; }  
    }
    [Serializable]
    public class Items
    {
        public string ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public string HEAD { get; set; }
        public string PARTY { get; set; }

        public string ITEM_NAME { get; set; }
        public string LED_NAME { get; set; }
        public string LED_TYPE { get; set; }
        public string ITEM_TRANS_TYPE { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
        public string ITEM_PARTY_REQ { get; set; }
        public string ITEM_PROFILE { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
        public int ITEM_CON_MIN_VALUE { get; set; }
        public int ITEM_CON_MAX_VALUE { get; set; }
        public string CON_LED_TYPE { get; set; }
        public string ITEM_TDS_CODE { get; set; }
        public string ITEM_ID { get; set; }
        public int TDS_RATE { get; set; }

    }
    [Serializable]
    public class Purpose
    {
        public string PUR_ID { get; set; }
        public string PUR_NAME { get; set; }
    }
    [Serializable]
    public class Party
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string PAN { get; set; }
    }
    [Serializable]
    public class PaymentVoucherPartyList
    {
        public string C_NAME { get; set; }
        public string C_PAN_NO { get; set; }
        public string C_CITY { get; set; }
        public string C_ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
    }
    [Serializable]
    public class BankList
    {
        public string BB_BRANCH_ID { get; set; }
        public string BANK_NAME { get; set; }
        public string BI_SHORT_NAME { get; set; }
        public string BANK_BRANCH { get; set; }
        public string BANK_ACC_NO { get; set; }
        public string BA_ID { get; set; }
        public string BANK_ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
    }
    [Serializable]
    public class RefBank
    {
        public string BI_ID { get; set; }
        public string BI_BANK_NAME { get; set; }
        public string BI_SHORT_NAME { get; set; }
    }
    [Serializable]
    public class AdvancesPartyList
    {
        public string NAME { get; set; }
        public string Status { get; set; }
        public string Occupation { get; set; }
        public string ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
    }
}