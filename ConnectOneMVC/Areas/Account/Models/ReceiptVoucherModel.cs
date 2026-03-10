using Common_Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class ReceiptVoucherModel
    {
        public string iVoucher_Type { get; set; }
        public string iTrans_Type { get; set; }
        public string iLed_ID { get; set; }
        public string iSpecific_ItemID { get; set; }
        public string iParty_Req { get; set; }
        public string iProfile { get; set; }
        public string iCond_Ledger_ID { get; set; }
        public string iMinValue { get; set; }
        public string iMaxValue { get; set; }
        public string iTDS_CODE { get; set; }
        public string iLink_ID { get; set; }
        public string iOffSet_ID { get; set; }
        public string iOffSet_Item { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public string SelectedBankID { get; set; }
        public string SelectedRefBankID { get; set; }
        public int SelectedDepositSlipNo { get; set; }
        public string Rpt_GLookUp_ItemList { get; set; }
        public DateTime? Rpt_Txt_V_Date { get; set; }
        public string Rpt_BE_Item_Head { get; set; }
        public string Rpt_Txt_V_NO { get; set; }
        public string Rpt_GLookUp_PartyList1 { get; set; }
        public string Rpt_BE_City { get; set; }
        public string Rpt_BE_PAN_No { get; set; }
        public string Rpt_GLookUp_Adjustment { get; set; }
        public int? Rpt_RAD_Receipt { get; set; }
        public double? Rpt_Txt_Out_Standing { get; set; }
        public double? Rpt_Txt_Diff { get; set; }
        [Required(ErrorMessage = "The Mode is required")]
        public string Rpt_Cmd_Mode { get; set; }
        public double? Rpt_Txt_Amount { get; set; }
        public string Rpt_GLookUp_RefBankList { get; set; }
        public string Rpt_Txt_Ref_Branch { get; set; }
        public string Rpt_Txt_Ref_No { get; set; }
        public DateTime? Rpt_Txt_Ref_CDate { get; set; }
        public DateTime? Rpt_Txt_Ref_Date { get; set; }
        public string Rpt_GLookUp_BankList { get; set; }
        public string Rpt_BE_Bank_Branch { get; set; }
        public string Rpt_BE_Bank_Acc_No { get; set; }
        public string Rpt_GLookUp_PurList { get; set; }
        public int? Rpt_Txt_Slip_No { get; set; }
        public int? Rpt_Txt_Slip_Count { get; set; }
        public string Rpt_Txt_Narration { get; set; }
        public string Rpt_Txt_Reference { get; set; }
        public string Rpt_Txt_Remarks { get; set; }
        public string xID { get; set; }
        public string xMID { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string ActionMethod { get; set; }
        public string lbl_Ref_No_Tag { get; set; }
        public double REF_OUTSTAND_NEXT_YEAR { get; set; }
        public DateTime? Bank_REC_EDIT_ON { get; set; }
        public DateTime? PartyList_REC_EDIT_ON { get; set; }
        public bool IsAdjustmentEnable { get; set; }
        public string TitleX { get; set; }
        public string Me_Text { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_Rpt { get; set; }
        public string Rpt_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_Rpt { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_Rpt { get; set; }
        public List<ReceiptPurposeList> PurposeList_Data_Rpt { get; set; }
        public List<VoucherTypeItems> ItemList_DD_Data_Rpt { get; set; }
        public List<DbOperations.Voucher_Receipt.Return_RefBank> RefBankList_DD_Data_Rpt { get; set; }
        public List<BankList> DepBankList_DD_Data_Rpt { get; set; }
        public List<ReceiptAdjustmentList> PaymentList_DD_Data_Rpt { get; set; }
        public List<ReceiptPartyList> PartyList_DD_Data_Rpt { get; set; }
    }
    [Serializable]
    public class VoucherTypeItems
    {
        public string ITEMID { get; set; }
        public string LED_NAME { get; set; }
        public string ITEM_PARTY_REQ { get; set; }
        public string ITEM_TRANS_TYPE { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
        public string ITEMNAME { get; set; }
        public string ITEM_PROFILE { get; set; }
        public string ITEM_CON_LED_ID { get; set; }
        public int? ITEM_CON_MIN_VALUE { get; set; }
        public int? ITEM_CON_MAX_VALUE { get; set; }
        public string ITEM_TDS_CODE { get; set; }
        public string ITEM_LINK_REC_ID { get; set; }
        public string ITEM_OFFSET_REC_ID { get; set; }
        public string ITEM_OFFSET_NAME { get; set; }
        public string ITEM_ID { get; set; }


    }
    [Serializable]
    public class ReceiptPurposeList
    {
        public string PUR_ID { get; set; }
        public string PUR_NAME { get; set; }

    }
    [Serializable]
    public class ReceiptAdjustmentList
    {
        public string REF_ID { get; set; }
        public decimal? REF_AMT { get; set; }
        public decimal? REF_OUTSTAND { get; set; }

        public DateTime? REF_DATE { get; set; }
        public decimal? REF_ADDITION { get; set; }
        public decimal? REF_ADJUSTED { get; set; }
        public decimal? REF_REFUND { get; set; }
        public decimal? REF_OUTSTAND_NEXT_YEAR { get; set; }
        public string REF_ITEM { get; set; }
        public string REF_ITEM_ID { get; set; }
        public string REF_PURPOSE { get; set; }
        public string REF_OTHER_DETAIL { get; set; }

    }
    [Serializable]
    public class ReceiptAdjustmentDroDownParameter
    {
        public string xParty_ID { get; set; }
        public string iLink_ID { get; set; }
        public string xMID { get; set; }
        public string filterName { get; set; }
        public bool IsVisible { get; set; }

    }
}