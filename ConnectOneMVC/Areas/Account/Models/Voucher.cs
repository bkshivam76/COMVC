using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class CB_Grid_Model
    {
        public string iTR_VNO { get; set; }
        public DateTime? iTR_DATE { get; set; }
        public string iTR_ITEM_ID { get; set; }
        public string iTR_ITEM { get; set; }
        public string iLED_ID { get; set; }
        public string iTR_HEAD { get; set; }
        public string iTR_SUB_ID { get; set; }
        public string iTR_AB_ID_1 { get; set; }
        public string iTR_PARTY_1 { get; set; }
        public string iTR_CR_ID { get; set; }
        public string iTR_CR_NAME { get; set; }
        public int? iTR_DATE_SERIAL { get; set; }
        public string iTR_DATE_SHOW { get; set; }
        public string iTR_ENTRY { get; set; }
        public decimal? iTR_REC_CASH { get; set; }
        public decimal? iTR_REC_BANK { get; set; }
        //public decimal? REC_BANK01 { get; set; }
        //public decimal? REC_BANK02 { get; set; }
        //public decimal? REC_BANK03 { get; set; }
        //public decimal? REC_BANK04 { get; set; }
        //public decimal? REC_BANK05 { get; set; }
        //public decimal? REC_BANK06 { get; set; }
        //public decimal? REC_BANK07 { get; set; }
        //public decimal? REC_BANK08 { get; set; }
        //public decimal? REC_BANK09 { get; set; }
        //public decimal? REC_BANK10 { get; set; }
        public decimal? iTR_REC_JOURNAL { get; set; }
        public decimal? iTR_REC_TOTAL { get; set; }
        public decimal? iTR_PAY_CASH { get; set; }
        public decimal? iTR_PAY_BANK { get; set; }
        //public decimal? PAY_BANK01 { get; set; }
        //public decimal? PAY_BANK02 { get; set; }
        //public decimal? PAY_BANK03 { get; set; }
        //public decimal? PAY_BANK04 { get; set; }
        //public decimal? PAY_BANK05 { get; set; }
        //public decimal? PAY_BANK06 { get; set; }
        //public decimal? PAY_BANK07 { get; set; }
        //public decimal? PAY_BANK08 { get; set; }
        //public decimal? PAY_BANK09 { get; set; }
        //public decimal? PAY_BANK10 { get; set; }
        public decimal? iTR_PAY_JOURNAL { get; set; }
        public decimal? iTR_PAY_TOTAL { get; set; }
        public string iTR_NARRATION { get; set; }
        public string iTR_ROW_POS { get; set; }
        public string iTR_TYPE { get; set; }
        public int? iTR_CODE { get; set; }
        public string iREC_ID { get; set; }
        public string iTR_M_ID { get; set; }
        public int? iTR_SR_NO { get; set; }
        public string iTR_SORT { get; set; }
        public DateTime? iREC_ADD_ON { get; set; }
        public string iTR_TEMP_ID { get; set; }
        public int? iTR_REF_NO { get; set; }
        public string iACTION_STATUS { get; set; }
        public DateTime? iREC_EDIT_ON { get; set; }
        public DateTime? iREC_STATUS_ON { get; set; }
        public string iREC_ADD_BY { get; set; }
        public string iREC_EDIT_BY { get; set; }
        public string iREC_STATUS_BY { get; set; }
        public string iCross_Ref_ID { get; set; }
        public string iRef_no { get; set; }
        public string Attachment_IDs { get; set; }
        public string iPurpose { get; set; }
        public string Advanced_Filter { get; set; }
        public int Sr { get; set; }
        public int? iREQ_ATTACH_COUNT { get; set; }
        public int? iCOMPLETE_ATTACH_COUNT { get; set; }
        public int? iRESPONDED_COUNT { get; set; }
        public int? iREJECTED_COUNT { get; set; }
        public int? iOTHER_ATTACH_CNT { get; set; }
        public int? iALL_ATTACH_CNT { get; set; }
        public string Grid_PK { get; set; }
        public Int32? VOUCHING_PENDING_COUNT { get; set; }
        public Int32? VOUCHING_ACCEPTED_COUNT { get; set; }
        public Int32? VOUCHING_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? VOUCHING_REJECTED_COUNT { get; set; }
        public Int32? VOUCHING_TOTAL_COUNT { get; set; }
        public Int32? AUDIT_PENDING_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? AUDIT_REJECTED_COUNT { get; set; }
        public Int32? AUDIT_TOTAL_COUNT { get; set; }
        public string iIcon { get; set; }  
        public string SPECIAL_VOUCHER_REFERENCE { get; set; }
        public string BA_ACC_NO { get; set; }
        public string ITEM_TDS_CODE { get; set; }
        public string LED_TYPE { get; set; }
        public string InvNo { get; set; }
    }
    [Serializable]
    public class ReceiptPartyList
    {
        public string C_ID { get; set; }
        public string C_NAME { get; set; }
        public string C_PAN_NO { get; set; }
        public string C_CITY { get; set; }
        public DateTime? REC_Edit_ON { get; set; }

    }
    [Serializable]
    public class CB_SpeceficPeriod
    {
        [Required(ErrorMessage ="From Date Is Required")]
        public DateTime CB_Fromdate { get; set; }
        [Required(ErrorMessage = "To Date Is Required")]
        public DateTime CB_Todate { get; set; }
    }
    [Serializable]
    public class Voucher_SpeceficPeriod
    {
        public DateTime Voucher_Fromdate { get; set; }
        public DateTime Voucher_Todate { get; set; }
    }
    [Serializable]
    public class CollectionBoxPartyList
    {
        public string C_ID { get; set; }
        public string C_NAME { get; set; }
        public string C_PAN_NO { get; set; }
        public string C_CITY { get; set; }
        public string C_OCCUPATION { get; set; }
        public DateTime? REC_Edit_ON { get; set; }

    }
    [Serializable]
    public class JournalPartyList
    {
        public string Name { get; set; }
        public string PAN { get; set; }
        public string C_UID_NO { get; set; }
        public string ID { get; set; }
        public DateTime? REC_Edit_ON { get; set; }
        public string C_OTHER_ID { get; set; }
        public string C_OTHER_ID_LABEL { get; set; }
        public string C_CATEGORY { get; set; }

    }
}