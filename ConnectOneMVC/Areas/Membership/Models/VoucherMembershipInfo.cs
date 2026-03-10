using Common_Lib;
using ConnectOneMVC.Areas.Account.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Voucher_Donation;

namespace ConnectOneMVC.Areas.Membership.Models
{
    [Serializable]
    public class VoucherMembershipInfo
    {
        public string xID_MRR { get; set; }
        public string xMID_MRR { get; set; }
        public string TitleX_MRR { get; set; }
        public DateTime? Txt_V_Date_MRR { get; set; }
        //public string GLookUp_ItemList_Tag { get; set; }
        public string GLookUp_ItemList_MRR { get; set; }
        public string Txt_V_No_MRR { get; set; }
        public string BE_Item_Head_MRR { get; set; }
        [Required(ErrorMessage = "Member Name Not Selected")]
        public string PC_MemberName_MRR { get; set; }
        //public string PC_MemberName_Tag_MRR { get; set; }
        public string Txt_Mem_No_MRR { get; set; }
        public string BE_Add1_MRR { get; set; }
        public string BE_Add2_MRR { get; set; }
        public string BE_Add3_MRR { get; set; }
        public string BE_Add4_MRR { get; set; }
        public string BE_City_MRR { get; set; }
        public string BE_Pincode_MRR { get; set; }
        public string BE_District_MRR { get; set; }
        public string BE_State_MRR { get; set; }
        public string BE_Country_MRR { get; set; }
        public string BE_Tel_Nos_MRR { get; set; }
        public string BE_Mob_No_MRR { get; set; }
        public string BE_Email_No_MRR { get; set; }
        public string BE_Qual_MRR { get; set; }
        public string BE_Occupation_MRR { get; set; }
        public string BE_DOB_MRR { get; set; }
        public string BE_AGE_MRR { get; set; }
        [Required(ErrorMessage = "Centre Name Not Defined...!")]
        public string BE_Cen_Name_MRR { get; set; }
        public string BE_Cen_UID_MRR { get; set; }
        public DateTime? Txt_S_Date_MRR { get; set; }
        public double? Txt_Subs_Fee_MRR { get; set; }
        public double? Txt_Ent_Fee_MRR { get; set; }
        public double? Txt_Adv_Fee_MRR { get; set; }
        [Required(ErrorMessage = "Membership Type Not Selected")]
        public string GLookUp_SubList_MRR { get; set; }
        public string GLookUp_SubList_Text_MRR { get; set; }//Redmine Bug #135455 fixed
        public string GLookUp_SubList_Tag { get; set; }
        public string GridToRefresh { get; set; }
        //[Required(ErrorMessage = "Please enter old membership no")]
        public string Txt_Mem_Old_No_MRR { get; set; }        
        public string Cmb_Period_MRR { get; set; }
        public int? Cmb_Period_SelectedIndexChanged { get; set; }
        //[Required(ErrorMessage = "Wing Name Not Selected")]//Redmine Bug #135952 fixed
        public string GLookUp_WingList_MRR { get; set; }
        public string xWing_Short_MRR { get; set; }
        [Required(ErrorMessage = "Purpose Not Selected")]
        public string GLookUp_PurList_MRR { get; set; }
        public string Txt_Narration_MRR { get; set; }
        public string Txt_Remarks_MRR { get; set; }
        public string Txt_Reference_MRR { get; set; }
        public double Txt_SubTotal_MRR { get; set; }
        public double Txt_CashAmt_MRR { get; set; }
        public double Txt_BankAmt_MRR { get; set; }
        public decimal Txt_DiffAmt_MRR { get; set; }
        public bool Chk_Incompleted { get; set; }
        public bool DocumentGettingReady { get; set; }

        public string ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public Common.Navigation_Mode Tag { get; set; }
        public int? Cmd_Mode_SelectedIndex { get; set; }
        public string cmd_Mode { get; set; }
        //public decimal? Txt_Amount { get; set; }
        public string GLookUp_RefBankList { get; set; }
        public string Txt_Ref_Branch { get; set; }
        public string Txt_Ref_No { get; set; }
        public DateTime? Txt_Ref_Date { get; set; }
        public DateTime? Txt_Ref_CDate { get; set; }
        public string GLookUp_BankList { get; set; }
        public string BE_Bank_Branch { get; set; }
        public string BE_Bank_Acc_No { get; set; }
        //default variables
        public string iVoucher_Type_MRR { get; set; }
        public string iTrans_Type_MRR { get; set; }
        public string iLed_ID_MRR { get; set; }
        public string iProfile_MRR { get; set; }
        //default variables
        public string iSpecific_ItemID { get; set; }
        public int? Cnt_BankAccount { get; set; }

        //public string Edit_MEM_OLD_NO_MRR { get; set; }
        //public string Edit_MEM_NO { get; set; }
        //public string Edit_AB_ID_MRR { get; set; }
        //public string Edit_WING_ID_MRR { get; set; }
        public string Edit_REC_ID_MRR { get; set; }
        public DateTime Add_Time { get; set; }//Add_Time got from Voucher Controller

        //public double? Default_Ent_Fee { get; set; }
        //public double? Default_Subs_Fee { get; set; }
        //public double? Default_Renew_Fee { get; set; }

        public DateTime? Last_Edit_Time_MRR { get; set; }
        //public DateTime? Add_Time_MRR { get; set; }
        public DateTime? Membership_Last_Edit_Time { get; set; }
        public DateTime? Info_LastEditedOn_MRR { get; set; }

        public string Subs_Category_MRR { get; set; }
        public int? Subs_Start_Month_MRR { get; set; }
        public int? Subs_Total_Month_MRR { get; set; }

        public List<VoucherTypeItems> ItemDD_Data { get; set; }
        public List<Return_DonationVocuherPurpose> PurposeDD_Data { get; set; }
        public List<WingList> WingDD_Data { get; set; }
        public List<SubscriptionList> SubscriptionListDD_Data { get; set; }
        public List<MembershipNamesList> MemberDD_Data { get; set; }
        public List<Period_Till_MRR> Cmb_Period_Data { get; set; }
        public string lbl_FeeEff_Text { get; set; }
        public string lbl_Expire_Text { get; set; }
        public bool WingListEnabled { get; set; }
        public string Edit_WING_ID { get; set; }
        public string Edit_AB_ID { get; set; }
    }
    [Serializable]
    public class BankList_MRR
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
    public class RefBank_MRR
    {
        public string BI_ID { get; set; }
        public string BI_BANK_NAME { get; set; }
        public string BI_SHORT_NAME { get; set; }
    }
    [Serializable]
    public class Membership_Grid_Datatable
    {
        [Key]
        public int Sr { get; set; }
        public string Item_ID { get; set; }
        public string Item_Led_ID { get; set; }
        public string Item_Trans_Type { get; set; }
        public string Item_Party_Req { get; set; }
        public string Item_Profile { get; set; }
        public string ItemName { get; set; }
        //public string ITEM_VOUCHER_TYPE { get; set; }
        public string Head { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public decimal? Rate { get; set; }
       // public decimal TDS { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string Pur_ID { get; set; }
        //public string LOC_ID { get; set; }
        //public string CREATION_PROF_REC_ID { get; set; }
    }
    [Serializable]
    public class Membership_Receipt_Cancel
    {
        public string xID { get; set; }
        public string Text { get; set; }
        public string TitleX { get; set; }
        public Common.Navigation_Mode Tag { get; set; }
        public string xName { get; set; }
        public string xMemNo { get; set; }
        public string xCenName { get; set; }
        public string xCenUID { get; set; }
        public string xWing { get; set; }
        public string xReceiptNo { get; set; }
        public string xReceiptDt { get; set; }
        [Required(ErrorMessage = "Please mention Reason")]
        public string MRR_Txt_Remarks_MRR { get; set; }
    }
    [Serializable]
    public class MembershipConfirmationEmail
    {
        public string Status { get; set; }
        public string DateOfConfirmation { get; set; }
        public string Wing { get; set; }
        public string Approver { get; set; }
        public string MemberName { get; set; }
        public string FormViewLink { get; set; }
    }
    //[Serializable]
    //public class MembershipBankPaymentDetails
    //{
    //    public string TempActionMethod { get; set; }
    //    public Common.Navigation_Mode Tag { get; set; }
    //    public int? Cmd_Mode_SelectedIndex { get; set; }
    //    public string cmd_Mode { get; set; }
    //    public decimal? Txt_Amount { get; set; }
    //    public string GLookUp_RefBankList { get; set; }
    //    public string Txt_Ref_Branch { get; set; }
    //    public string Txt_Ref_No { get; set; }
    //    public DateTime? Txt_Ref_Date { get; set; }
    //    public DateTime? Txt_Ref_CDate { get; set; }
    //    public string GLookUp_BankList { get; set; }
    //    public string BE_Bank_Branch { get; set; }
    //    public string BE_Bank_Acc_No { get; set; }
    //}
}