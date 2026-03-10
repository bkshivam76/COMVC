using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Payment_Frm_Telephone_Select_Model
    {
        public string TempActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }      
        [Display(Name = "Period From:")]
        public DateTime? Txt_Fr_Date { get; set; }
        [Display(Name = "Telephone No.:")]
        [Required]
        public string GLookUp_TeleList { get; set; }
        public string xID { get; set; }
        [Display(Name = "Company:")]
        public string BE_Company { get; set; }
        [Display(Name = "Category:")]
        public string BE_Category { get; set; }
        [Display(Name = "Plan Type:")]
        public string BE_Plantype { get; set; }
        [Display(Name = "Bill No:")]     
        public string Txt_Bill_No { get; set; }
        [Display(Name = "Bill Date:")]      
        public DateTime? Txt_Bill_Date { get; set; }   
        [Display(Name = "Period To:")]
        public DateTime? Txt_To_Date { get; set; }
        public List<Return_Payment_Telephone_Select> TelephoneListData { get; set; }
    }
    [Serializable]
    public class Payment_Grid_Datatable
    {
        [Key]
        public int Sr { get; set; }
        public string Item_ID { get; set; }
        public string Item_Led_ID { get; set; }
        public string Item_Trans_Type { get; set; }
        public string Item_Party_Req { get; set; }
        public string Item_Profile { get; set; }
        public string ItemName { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }
        public string Head { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public decimal? Rate { get; set; }
        public decimal TDS { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string Pur_ID { get; set; }
        public string LOC_ID { get; set; }
        public string CREATION_PROF_REC_ID { get; set; }
        //'---Gold/Silver-----
        public string GS_DESC_MISC_ID { get; set; }
        public decimal? GS_ITEM_WEIGHT { get; set; }
        //'---Other Asset-----
        public string AI_TYPE { get; set; }
        public string AI_MAKE { get; set; }
        public string AI_MODEL { get; set; }
        public string AI_SERIAL_NO { get; set; }
        public string AI_PUR_DATE { get; set; }
        public decimal? AI_WARRANTY { get; set; }
        public byte[] AI_IMAGE { get; set; }
        //'-LIVE STOCK-----
        public string LS_NAME { get; set; }
        public string LS_BIRTH_YEAR { get; set; }
        public string LS_INSURANCE { get; set; }
        public string LS_INSURANCE_ID { get; set; }
        public string LS_INS_POLICY_NO { get; set; }
        public decimal? LS_INS_AMT { get; set; }
        public string LS_INS_DATE { get; set; }
        //  '---Vehicles------
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
        //    '-----Land&Building-----
        public string LB_PRO_TYPE { get; set; }
        public string LB_PRO_CATEGORY { get; set; }
        public string LB_PRO_USE { get; set; }
        public string LB_PRO_NAME { get; set; }
        public string LB_PRO_ADDRESS { get; set; }
        public string LB_OWNERSHIP { get; set; }
        public string LB_OWNERSHIP_PARTY_ID { get; set; }
        public string LB_SURVEY_NO { get; set; }
        public decimal? LB_TOT_P_AREA { get; set; }
        public decimal? LB_CON_AREA { get; set; }
        public string LB_CON_YEAR { get; set; }
        public string LB_RCC_ROOF { get; set; }
        public decimal? LB_DEPOSIT_AMT { get; set; }
        public string LB_PAID_DATE { get; set; }
        public decimal? LB_MONTH_RENT { get; set; }
        public decimal? LB_MONTH_O_PAYMENTS { get; set; }
        public string LB_PERIOD_FROM { get; set; }
        public string LB_PERIOD_TO { get; set; }
        public string LB_DOC_OTHERS { get; set; }
        public string LB_DOC_NAME { get; set; }
        public string LB_OTHER_DETAIL { get; set; }
        public string LB_REC_ID { get; set; }
        public DateTime? LB_REC_EDIT_ON { get; set; }
        public string LB_ADDRESS1 { get; set; }
        public string LB_ADDRESS2 { get; set; }
        public string LB_ADDRESS3 { get; set; }
        public string LB_ADDRESS4 { get; set; }
        public string LB_STATE_ID { get; set; }
        public string LB_DISTRICT_ID { get; set; }
        public string LB_CITY_ID { get; set; }
        public string LB_PINCODE { get; set; }
        //  '---Telephone-----
        public string TP_ID { get; set; }
        public string TP_BILL_NO { get; set; }
        public string TP_BILL_DATE { get; set; }
        public string TP_PERIOD_FROM { get; set; }
        public string TP_PERIOD_TO { get; set; }
        //'--WIP--
        public string REF_REC_ID { get; set; }
        public string REFERENCE { get; set; }
        public string WIP_REF_TYPE { get; set; }
        //'--TDS--
        public string REF_TDS_DED { get; set; }
    }
    [Serializable]
    public class Frm_Voucher_Win_Gen_Pay_Tds_Ded_Out_TDS
    {
        public double TDS_Ded { get; set; }
        public string RefMID { get; set; }
    }
}
