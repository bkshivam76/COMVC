using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Magazine.Models
{
    [Serializable]
    public class MagazineReciptReginfo
    {
        public string BE_Stats { get; set; }
        public string BE_View_Period { get; set; }
        public string Cmb_View { get; set; }
        
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        //public string Todate { get; set; }
        public string Receipt_NO { get; set; }
        public string Voucher_No { get; set; }
        public string Entry { get; set; }
        public string Magazine { get; set; }
        public string TagName { get; set; }
        public string Member_Name { get; set; }
        public string City { get; set; }
        public string Membership_ID { get; set; }
        public string Old_ID { get; set; }
        public string Category { get; set; }
        public string Subs_Type { get; set; }
        public string Period { get; set; }
        public string Other_Details { get; set; }
        public string Add_By { get; set; }
        public string Edit_By { get; set; }
        public string Action_By { get; set; }
        public string ID { get; set; }
        public DateTime? Add_Date { get; set; }
        public DateTime? Edit_Date { get; set; }
        public DateTime? Action_Date { get; set; }
        public string Action_Status { get; set; }
        public string Receipt_ID { get; set; }
        public string MS_REC_ID { get; set; }
        public string MM_MEMBER_ID { get; set; }
        public int? Copies { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Start_Date { get; set; }
        public string Ref_Date { get; set; }
        public string Ref_No { get; set; }
        public decimal? Cash_Amount { get; set; }
        public decimal? Bank_Amount { get; set; }
        public decimal? Total_Amount { get; set; }
        public DateTime? xFr_Date { get; set; }
        public DateTime? xTo_Date { get; set; }
        public string GlookMagListID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string MM_MEMBER_TYPE { get; set; }
        public string Membership_No { get; set; }
        public string Membership { get; set; }
        public string Amount { get; set; }
        public DateTime? AddDate { get; set; }
        public string xDate { get; set; }
        public string xTemp_ID { get; set; }
        public string MemDate { get; set; }
        public string vDate { get; set; }
        public string xTemp_5 { get; set; }
        public string xTemp_6 { get; set; }
        public string flag { get; set; }
        public string Receipt_No { get; set; }
        public string xTemp0 { get; set; }
        public string xTemp_4 { get; set; }
        public string xTemp_1 { get; set; }
        public string xTemp_7 { get; set; }
        public string PromptMsg { get; set; }
        public string org_RegId { get; set; }

        public string TempActionMethod { get; set; }
        public string xID { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string Txt_Name { get; set; }
        public string Txt_Remark1 { get; set; }
        public string Txt_Remark2 { get; set; }
        public string Txt_R_Add1 { get; set; }
        public string Txt_R_Add2 { get; set; }
        public string Txt_R_Add3 { get; set; }
        public string Txt_R_Add4 { get; set; }
        public string Txt_R_Other_City { get; set; }
        public bool Txt_R_Other_City_Readonly { get; set; }
        public string GLookUp_RCountryList { get; set; }
        public string GLookUp_RCountryLis_Properties_Tag { get; set; }
        public string GLookUp_RStateList { get; set; }
        public string GLookUp_RStateList_Properties_Tag { get; set; }
        public string GLookUp_RDistrictList { get; set; }
        public string GLookUp_RDistrictList_Properties_Tag { get; set; }
        public string PC_City_Name { get; set; }
        public string PC_City_Tag { get; set; }
        public bool PC_City_Name_Read_only { get; set; }
        public string _Member { get; set; }
        public string Rad_city_OthCity { get; set; }
        public string Txt_R_Pincode { get; set; }
        public string Txt_R_Tel_1 { get; set; }
        public string Txt_Mob_1 { get; set; }
        public string Txt_Mob_2 { get; set; }
        public string Txt_Email1 { get; set; }
        public string message { get; set; }
        public string result { get; set; }
        public string tempStateCode { get; set; }
        public string tempCountryCode { get; set; }
        public string CityRadio { get; set; }
        public string OthCityRadio { get; set; }
        public string CityOpcityclass { get; set; }
        public string Org_RecID { get; set; }
        public string SubCityID { get; set; }

    }
    [Serializable]
    public class PCMember_List
    {
        public string Magazine { get; set; }
        public string MEM_NAME { get; set; }
        public string MM_MS_OLD_ID { get; set; }
        public string MM_CITY { get; set; }
        public string MEM_CEN_NAME { get; set; }
        public string MEM_CEN_UID { get; set; }
        public string MEMBER_ID { get; set; }
        public string MEM_CEN_INCHARGE { get; set; }
        public string CC_MEMBER_TYPE { get; set; }
        public string MEM_ADDRESS { get; set; }
        public string MM_STATUS { get; set; }
        public string MM_MEMBER_TYPE { get; set; }
        public string MEM_ID { get; set; }
        public string MAG_ID { get; set; }
        public string MEM_COUNTRY { get; set; }
        public string MEM_ADD_1 { get; set; }
        public string MEM_ADD_2 { get; set; }
        public string MEM_ADD_3 { get; set; }
        public string MEM_ADD_4 { get; set; }
        public string MEM_ADD_5 { get; set; }
        public string MEM_ADD_6 { get; set; }
        public string MEM_PINCODE { get; set; }
        public string MEM_CEN_ID { get; set; }
        public string MM_MS_ID { get; set; }
        public string MM_OTHER_DETAIL { get; set; }
        public string MM_REC_ID { get; set; }
        public string MM_MS_START_DATE { get; set; }
        public string MM_AUTO_RENEWAL { get; set; }
        public string MM_CC_APPLICABLE { get; set; }
        public string MM_CC_SPONSORED { get; set; }
        public string MM_CC_MS_ID { get; set; }

    }
    [Serializable]
    public class GLookUp_RefBankList
    {
        public string BI_SHORT_NAME { get; set; }
        public string BI_BANK_NAME { get; set; }
        public string BI_ID { get; set; }
    }
    [Serializable]
    public class GLookUp_BankList
    {
        public string BANK_ACC_NO { get; set; }
        public string BI_BANK_NAME { get; set; }
        public string BANK_BRANCH { get; set; }
        public string ID { get; set; }
        public string BANK_ID { get; set; }
    }
    [Serializable]
    public class GLookUp_PurList
    {
        public string PUR_NAME { get; set; }
        public string PUR_ID { get; set; }
    }
    [Serializable]
    public class GLoopUpMagaList
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string Publish_On { get; set; }
        public string Mag_Short_Name { get; set; }
        public string ID { get; set; }
    }
    [Serializable]
    public class GLookUp_CC_Member_List
    {
        public string Joint_ID { get; set; }
        public string MEM_ID { get; set; }
        public string OLD_ID { get; set; }
        public string ID { get; set; }
    }
    [Serializable]
    public class CancelledList
    {
        public string NAME { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Remarks { get; set; }
        public string Cancelled_on { get; set; }
    }
    [Serializable]
    public class Subscriptioninfo
    {
        public string ResCmb_CC_MemType { get; set; }
        public DateTime VDate { get; set; }
        public string Txn_Date { get; set; }
        public string xTemp_ID { get; set; }
        public string MS_RecId { get; set; }
        public string xMID { get; set; }
        public string ID { get; set; }
        public string Cmb_SearchType { get; set; }
        public string Txt_MS_ID { get; set; }
        public string Txt_MS_Old_ID { get; set; }
        public string BE_STATUS { get; set; }
        public string BE_MagazineName{ get; set; }
        public string BE_CCMemberName { get; set; }
        public string BE_Language { get; set; }
        public string BE_Publish_On { get; set; }
        public string BE_Name { get; set; }
        public string BE_Address_1 { get; set; }
        public string BE_Address_2 { get; set; }
        public string BE_Address_3 { get; set; }
        public string BE_Address_4 { get; set; }
        public string BE_Address_5 { get; set; }
        public string BE_Address_6 { get; set; }
        public string RAD_Category { get; set; }
        public string Cmb_MemType { get; set; }
        public string MS_No { get; set; }
        public string PC_MemberName { get; set; }
        public string PC_MemberNameTag { get; set; }
        public DateTime? Txt_MS_Date { get; set; }
        public bool Chk_ConCenter { get; set; }
        public string Cmb_CC_MemType { get; set; }
        public bool Chk_Sponsored { get; set; }
        public string xMS_Rec_ID { get; set; }
        public string Status_Action { get; set; }
        public string lblCurrActivity { get; set; }
        public string Lbl_ActiveSubsPeriod { get; set; }
        public string lblDispatchCount { get; set; }
        public double? BE_OpAdvDue { get; set; }
        public string ME_OtherDetails { get; set; }
        public double? Lbl_SubsCurrTxn { get; set; }
        public double? Lbl_SubsDuringYear { get; set; }
        public double? Lbl_PmtCurrTxn { get; set; }
        public double? Lbl_Pmt_Total { get; set; }
        public double? Lbl_OtherCurrTxn { get; set; }
        public double? Lbl_Other_Total { get; set; }
        public string Txt_Nxt_Issue_Date { get; set; }
        public string NAME { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Remarks { get; set; }
        public string Cancelled_on { get; set; }
        public string ActionMethodName { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string Email_Id { get; set; }
        public string REC_ID { get; set; }
        public string Membership { get; set; }
        public string MAG_ID { get; set; }
        public int GridRow_Index { get; set; }
        public int Grid_Count { get; set; }
        public string Mem_ID { get; set; }

    }
    [Serializable]
    public class Subscriptiondetails_Model
    {
        public int? Sr { get; set; }
        public DateTime? Txt_Subs_Date { get; set; }
        public string GLookUp_STypeList { get; set; }
        public string GLookUp_DTypeList { get; set; }
        public string GLookUp_PurList { get; set; }
        public int Txt_Free { get; set; }
        public int Txt_Copies { get; set; }
        public DateTime? Txt_Fr_Date { get; set; }
        public DateTime? Txt_To_Date { get; set; }
        public double? BE_SubAmt { get; set; }     
        public double? BE_ExtraAmt { get; set; }
        public int Txt_Issues { get; set; }
        public string Rad_DisOnCC { get; set; }
        public double? BE_TotalAmt { get; set; }
        public string Txt_Narration { get; set; }
        public string Txt_Reference { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string SubscriptionType { get; set; }
        public string Subs_Short_Name { get; set; }
        public int Subs_Start_Month { get; set; }
        public int Subs_Min_Months { get; set; }
        public string Subs_Fixed_Period { get; set; }
        public string Subs_PeriodWiseFeeCalc { get; set; }
        public string DispatchName { get; set; }
        public string PUR_ID { get; set; }
        public string PUR_NAME { get; set; }
        public string Membership { get; set; }
        public string BE_Purpose { get; set; }
        public string VoucherType { get; set; }
        public string MagazineID { get; set; }
        public double? DT_Charges { get; set; }
        public double? MSTF_Indian_Fee { get; set; }
        public double? MSTF_Foreign_Fee { get; set; }
        public int CurrYearIssueCount { get; set; }
        public string Tr_M_ID { get; set; }
    }
    [Serializable]
    public class PaymentDetails_Model
    {
        public int? Sr { get; set; }
        public string Cmd_Mode { get; set; }
        public double? Txt_Amount { get; set; }
        public DateTime? Txt_Pmt_Date { get; set; }
        public string GLookUp_RefBankList { get; set; }
        public string Txt_Ref_Branch { get; set; }
        public string Txt_Ref_No { get; set; }
        public DateTime? Txt_Ref_Date { get; set; }
        public DateTime? Txt_Ref_CDate { get; set; }
        public double? GLookUp_BankList { get; set; }
        public string GLookUp_PurList { get; set; }
        public string Txt_Narration { get; set; }
        public string Txt_Reference { get; set; }
        public string xID { get; set; }
        public string BE_Purpose { get; set; }
        public string BE_Bank_ID { get; set; }
        public string BE_Bank_Name { get; set; }
        public string BE_Bank_Branch { get; set; }
        public string RefBank_Name { get; set; }
        public string Ref_Bank_Name { get; set; }
        public string BE_Bank_Acc_No { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
    }
    [Serializable]
    public class Payment_Window_Grid
    {
        public int? Sr { get; set; }
        public string Mode { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Pmt_Date { get; set; }
        public string Deposited_Bank_ID { get; set; }
        public string Deposited_Bank { get; set; }
        public string Deposited_Branch { get; set; }
        public string Deposited_Ac_No { get; set; }
        public string Ref_No { get; set; }
        public DateTime? Ref_Date { get; set; }
        public DateTime? Ref_Clearing_Date{ get; set; }
        public string Ref_Branch { get; set; }
        public string RefBank_Name { get; set; }
        public string Ref_Bank_ID { get; set; }
        public string Pur_ID { get; set; }
        public string Narration { get; set; }
        public string Reference { get; set; }
        public string Tr_M_ID { get; set; }
        public bool UPDATED { get; set; }
        public bool REC_GENERATED { get; set; }
        public int? TR_CODE { get; set; }
        public string Type { get; set; }
        public string REC_ID { get; set; }
        public DateTime? REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public string RECEIPT_NO { get; set; }
        public string RECEIPT_ID { get; set; }  
    }
    [Serializable]
    public class Subscription_Window_Grid
    {
        public int? Sr { get; set; }
        public string SubsType { get; set; }
        public string Subs_ID { get; set; }             
        public DateTime? SubsDate { get; set; }
        public DateTime? FrDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Copies { get; set; }
        public int Free { get; set; }
        public decimal? SubAmt { get; set; }
        public decimal? TotalAmt { get; set; }
        public string DispType { get; set; }
        public string Dis_ID { get; set; }
        public decimal? DispAmt { get; set; }
        public string DisponCC { get; set; }
        public string Pur_ID { get; set; }
        public string Narration { get; set; }
        public string Reference { get; set; }
        public string Tr_M_ID { get; set; }
        public string Type { get; set; }
        public bool UPDATED { get; set; }
        public bool REC_GENERATED { get; set; }
        public int? TR_CODE { get; set; }
        public string REC_ID { get; set; }
        public string REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public string REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public string RECEIPT_NO { get; set; }
        public string RECEIPT_ID { get; set; }
        public bool IS_LIFE { get; set; }
        public decimal? CURR_YEAR_INCOME { get; set; }
        public int Txtissues { get; set; }
    }

    [Serializable]
    public class LedgerList
    {
        public string Date { get; set; }
        public string Membership { get; set; }
        public string Subscription { get; set; }
        public string Payment { get; set; }
        public string BalanceDue { get; set; }
        public string MemberName { get; set; }
        public string ID { get; set; }
        public string Opening { get; set; }
    }
    [Serializable]
    public class AccntList
    {
        public string Name { get; set; }
        public string Led_Id{ get; set; }
        public string Led_Name { get; set; }
        public string Date { get; set; }
        public string Dr { get; set; }
        public string Cr { get; set; }
    }
    [Serializable]
    public class DispatchList
    {
        public string Dispatch_Member_ID { get; set; }
        public string MemberID { get; set; }
        public string MemberOldID { get; set; }
        public string Member { get; set; }
        public string Magazine { get; set; }
        public string Address { get; set; }
        public string TotalCopies { get; set; }
        public string DispatchedCopies { get; set; }
        public string Status { get; set; }
        public string MemberStatus { get; set; }
        public string MII_ISSUE_DATE { get; set; }
        public string DISP_DONE_MODE { get; set; }
        public string MAG_REC_ID { get; set; }
        public string ExpiryStatus { get; set; }
        public string REGION { get; set; }
        public string RMS { get; set; }
        public string PSO { get; set; }
        public string CONTACT_NO { get; set; }
        public string CURRTIME { get; set; }
        public string Issue { get; set; }
        public string ISSUE_MEMBER  { get; set; }
        public string Date { get; set; }
        public string DispatchMode { get; set; }
        public string DispatchStatus { get; set; }
        public string Remarks { get; set; }
        public string Copies { get; set; }
        public string ISSUE_ID { get; set; }
        public string MEM_ID { get; set; }
    }
    [Serializable]
    public class RelatedList
    {
        public string ID { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
        public string MembershipId { get; set; }
        public string MemberName { get; set; }
        public string Magazine { get; set; }
        public string COPIES { get; set; }
        public string Period { get; set; }
        public string MembershipOldId { get; set; }
        public string AddressLane1 { get; set; }
        public string AddressLane2 { get; set; }
        public string AddressLane3 { get; set; }
        public string AddressLane4 { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string TelNos { get; set; }
        public string Membertype { get; set; }
        public string Disponcc { get; set; }
        public string StartDate { get; set; }
        public string Due { get; set; }
        public string Advance { get; set; }
        public string Category { get; set; }
        public string EntryType { get; set; }
        public string MM_MI_ID { get; set; }
        public string MM_MEMBER_ID { get; set; }
        public string MMB_CC_DISPATCH { get; set; }
        public string MM_CC_MS_ID { get; set; }
        public string MM_CC_SPONSORED { get; set; }
        public string MM_MS_NO1 { get; set; }
        public string Subscription { get; set; }
        public string DateofSubs { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string NoofCopies { get; set; }
        public string SubsAmt { get; set; }
        public string DispAmt { get; set; }
        public string TotalAmt { get; set; }
        public string DispatchType { get; set; }
        public string DispatchedOn { get; set; }
        public string LifeMember { get; set; }
    }
    [Serializable]
    public class IssueList
    {
        public string ID { get; set; }
        public string Issue { get; set; }

    }
    [Serializable]
    public class CityList
    {
        public string CityName { get; set; }
        public string City_REC_ID { get; set; }
        public string StateName { get; set; }
        public string State_REC_ID { get; set; }
        public string DistrictName { get; set; }
        public string PinCode { get; set; }
        public string CountryName { get; set; }
        public string Coutry_REC_ID { get; set; }
    }
}