using Common_Lib.RealTimeService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Model_Frm_Voucher_Win_FD
    {
        public string xID { get; set; }
        public string xMID { get; set; }
        public string CreatedFDID { get; set; }
        public string iVoucher_Type { get; set; }
        public string iTrans_Type { get; set; }
        public string iLed_ID { get; set; }
        public string iSpecific_ItemID { get; set; }
        public int? Cnt_BankAccount { get; set; }
        public Common_Lib.Common.FDAction? iAction { get; set; }
        public string FDiAction { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public string TitleX { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string ActionMethod { get; set; }
        public DateTime? GlookUp_BankList_REC_EDIT_ON { get; set; }
        public DateTime? FD_List_REC_EDIT_ON { get; set; }      
        public DateTime? FD_MaturityDate { get; set; }
        public double? FD_MaturityAmt { get; set; }
        public string xID_FDWindow { get; set; }
        public bool GLookUp_BankListEnabled { get; set; }
        public string Look_BankList { get; set; }
        public DateTime? Look_BankList_RecEditOn { get; set; }
        public DateTime? FD_AsDate { get; set; }
        public DateTime? FD_Date { get; set; }
        public string FdListBank_ID { get; set; }
        public string FD_ItemID { get; set; }
        [Required]
        public string FD_GLookUp_ItemList { get; set; }
        public DateTime? FD_Txt_V_Date { get; set; }
        public string FD_BE_Item_Head { get; set; }
        public string FD_Txt_V_NO { get; set; }
        public string GLookUp_FD_List { get; set; }
        public double? FD_Txt_Amount { get; set; }
        public DateTime? FD_TXT_NRC_DATE { get; set; }
        public string FD_TXT_Fd_Bank { get; set; }
        public string FD_Cmd_Mode { get; set; }
        public double? FD_TXT_RENEWAL_MATURITY_AMOUNT { get; set; }
        public string FD_TXT_FD_ACT { get; set; }
        public double? FD_TXT_REC_AMOUNT { get; set; }
        public double? FD_TXT_INTEREST { get; set; }
        [Required(ErrorMessage ="Mode Not Selected...!")]
        public string FD_Cmb_Rec_Mode { get; set; }
        public double? FD_TXT_TDS { get; set; }
        public double? FD_TXT_TDS_PREV { get; set; }
        public string FD_GLookUp_BankList { get; set; }
        public string FD_Txt_Branch { get; set; }
        public string FD_Txt_AccountNo { get; set; }
        public string FD_Txt_Ref_No { get; set; }
        public DateTime? FD_Txt_Ref_Date { get; set; }
        public DateTime? FD_Txt_Ref_CDate { get; set; }
        public string FD_Txt_Narration { get; set; }
        public string FD_Txt_Remarks { get; set; }
        public string FD_Txt_Reference { get; set; }
        public string PurposeID_FD { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_FD { get; set; }
        public string FD_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_FD { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_FD { get; set; }

        public List<FDVoucherItems> ItemData { get; set; }
        public List<FdListItems> FDData { get; set; }
        public List<FDBankList> BankData { get; set; }
        public string FD_RenewFromFDNo { get; set; }
        public string In_UP_FD { get; set; }
        public string In_UP_FDHty { get; set; }
        public string In_UP_RenFDHis { get; set; }
    }
    [Serializable]
    public class Model_FD_Window
    {
        public string TxnID { get; set; }
        public string Status { get; set; }
        public DateTime? MatDate { get; set; }
        public string Fdiaction { get; set; }
        public Common_Lib.Common.FDAction? iAction_window { get; set; }
        public string iRenewFrom { get; set; }
        public string iRenewFDNo { get; set; }
        public string _action { get; set; }
        public string xFdID { get; set; }
        public string xID_FD { get; set; }
        public string TempActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }

        public string Look_BankList_FD { get; set; }
        public string Txt_No_FD { get; set; }
        public DateTime? Txt_Date_FD { get; set; }
        public DateTime? Txt_As_Date_FD { get; set; }
        public double? Txt_Amount_FD { get; set; }
        [Required(ErrorMessage = "Rate Of Interest Is Required...!")]
        public double txt_Rate_FD { get; set; }
        [Required(ErrorMessage ="Interest Payment Condition Not Selected...!")]
        public string Cmd_Type_FD { get; set; }
        public double? Txt_Mat_Amount_FD { get; set; }
        public DateTime? Txt_Mat_Date_FD { get; set; }
        public string Txt_Remarks_FD { get; set; }
        public string Popup_id { get; set; }       

    }
    [Serializable]
    public class FdVoucher
    {
        public string xID { get; set; }
        public string TitleX { get; set; }
        public string PopUpHeaderTitleX { get; set; }
        [Required(ErrorMessage = "Voucher Date is Required")]
        public DateTime? Txt_V_Date { get; set; }
        public DateTime? Txt_Ref_Date { get; set; }
        public DateTime? Txt_Ref_CDate { get; set; }
        public DateTime? TXT_NRC_DATE { get; set; }
        public DateTime? FD_AS_DATE { get; set; }
        public DateTime? FD_List_MATURITY_DATE { get; set; }
        public decimal? FD_List_MATURITY_AMOUNT { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? FD_List_FD_DATE { get; set; }
        public string ItemList { get; set; }
        public string FD_List { get; set; }
        public string Lay_Select_Fd { get; set; }
        public string Lay_View_Fd { get; set; }
        public string Lay_FD_Bank { get; set; }
        public string Lay_Payment_Mode { get; set; }
        public bool Lay_Payment_Mode_Visible { get; set; }
        public string Lay_Renewal_Amount { get; set; }
        [Required(ErrorMessage = "Amount is Required...!")]
        public double? Txt_Amount { get; set; }
        [Required(ErrorMessage = "Payment Mode Not Selected...!")]
        public string Cmd_Mode { get; set; }
        public string Lay_Renewal { get; set; }
        public string FDVoucher_BankList { get; set; }
        public bool BankList_Enabled { get; set; }
        public string Txt_Branch { get; set; }
        public string Txt_AccountNo { get; set; }
        public string Txt_Ref_No { get; set; }
        [Required(ErrorMessage = "Please enter Renewal Amount..!")]
        public double? TXT_RENEWAL_MATURITY_AMOUNT { get; set; }
        public string lblRenewalTitle { get; set; }
        public string xMID { get; set; }
        public string Cmb_Rec_Mode { get; set; }
        [Required(ErrorMessage = "Please enter Received Amount..!")]
        public double? TXT_REC_AMOUNT { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public DateTime? Bank_List_REC_EDIT_ON { get; set; }
        public DateTime? FD_List_REC_EDIT_ON { get; set; }
        public bool Chk_Incompleted { get; set; }
        public string iTrans_Type { get; set; }
        public string Txt_V_NO { get; set; }
        public string Txt_Narration { get; set; }

        public string Txt_Remarks { get; set; }

        public string Txt_Reference { get; set; }
        public string message { get; set; }
        public string TempActionMethod { get; set; }
        public string BE_Item_Head { get; set; }
        public string TXT_Fd_Bank { get; set; }
        public string TXT_FD_ACT { get; set; }
        public double? TXT_INTEREST { get; set; }
        public double? TXT_TDS { get; set; }
        public double? TXT_TDS_PREV { get; set; }
        public Common_Lib.Common.FDAction? iAction { get; set; }
        public string FD_List_BA_ID { get; set; }
        public string FD_List_BANK_BRANCH { get; set; }
        public string FD_List_BANK_CUST_NO { get; set; }
        public string FD_List_FD_NO { get; set; }
        public string ItemID { get; set; }
        public string iVOUCHER_TYPE { get; set; }
        public string iLed_ID { get; set; }
        public string iMode { get; set; }
        public DateTime? VDate { get; set; }
        public string Bank_List { get; set; }
        public DateTime? Ref_Date { get; set; }
        public DateTime? Ref_CDate { get; set; }
        public double? Amount { get; set; }
        public string Status_Action { get; set; }
        public string Dr_Led_id { get; set; }
        public string Cr_Led_id { get; set; }
        public string Sub_Dr_Led_ID { get; set; }
        public string Sub_Cr_Led_ID { get; set; }
        [Display(Name = "Voucher No.:")]
        public string Voch_Lab_No { get; set; }
        [Display(Name = "FD No.:")]
        public string FD_NO { get; set; }
    }
    [Serializable]
    public class FDType
    {
        public string FDActivity { get; set; }
        public int Sr { get; set; }
        public string ITEMID { get; set; }
    }
    [Serializable]
    public class FDVoucherItems
    {
        public string ITEMID { get; set; }
        //public string ITEMNAME { get; set; }
        public string LED_NAME { get; set; }
        public string ITEM_PARTY_REQ { get; set; }

        public string ITEM_TRANS_TYPE { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string ITEM_VOUCHER_TYPE { get; set; }

        public string ITEMNAME { get; set; }
        //public string LED_NAME { get; set}
        //public string ITEM_TRANS_TYPE { get; set}
        //public string ITEM_LED_ID { get; set}
        //public string ITEM_VOUCHER_TYPE { get; set}
        //public string ITEM_PARTY_REQ { get; set}
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
    public class FdListItems
    {
        public string BI_SHORT_NAME { get; set; }
        public string BANK_NAME { get; set; }
        public string BANK_BRANCH { get; set; }
        public string BANK_CUST_NO { get; set; }
        public string BA_ID { get; set; }
        public string FD_ID { get; set; }
        public string FD_NO { get; set; }
        public decimal? FD_AMOUNT { get; set; }
        public decimal? MATURITY_AMOUNT { get; set; }
        public DateTime? MATURITY_DATE { get; set; }
        public DateTime? FD_DATE { get; set; }
        public DateTime? FD_AS_DATE { get; set; }
        public string FD_STATUS { get; set; }
        public decimal? ROI { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
    }
    [Serializable]
    public class FDBankList
    {
        public string BANK_NAME { get; set; }
        public string BI_SHORT_NAME { get; set; }
        public string BANK_BRANCH { get; set; }
        public string BANK_ACC_NO { get; set; }
        public string BA_ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
    }
    [Serializable]
    public class FDBankList_window
    {
        public string Name { get; set; }
        public string BA_CUST_NO { get; set; }
        public string Branch { get; set; }
        public string BANK_ACC_NO { get; set; }
        public string ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }      
    }
}