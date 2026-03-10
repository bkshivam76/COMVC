using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Voucher_Donation;
using static Common_Lib.DbOperations.Vouchers;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class DonationVoucher
    {
        [Required(ErrorMessage = "Mode Is Required...")]
        public string Cmd_Mode_Donation { get; set; }      
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public DateTime? Donor_RecEditOn { get; set; }
        public DateTime? DepositBank_RecEditOn { get; set; }    
       
        [Required(ErrorMessage = "Item Name Not Selected...!")]
        public string GLookUp_ItemList_Donation { get; set; } //item id    
        public string GLookUp_BankList_Donation { get; set; }     //deposited bank id

        [Required(ErrorMessage = "Amount not Selected...!")]
        public double? Txt_Amount_Donation { get; set; }
        public string GLookUp_RefBankList_Donation { get; set; }      // reference bank id
        public string Txt_Ref_Branch_Donation { get; set; }
        public string Txt_Ref_No_Donation { get; set; }
        public DateTime? Txt_Ref_Date_Donation { get; set; }//refence date

        [Required(ErrorMessage = "Purpose not Selected...!")]
        public string GLookUp_PurList_Donation { get; set; } //puprpose id
        public int? Txt_Slip_No_Donation { get; set; }
        public DateTime? Txt_Ref_CDate_Donation { get; set; } //clearing date
        public int? Txt_Slip_Count_Donation { get; set; }
        [Required(ErrorMessage = "Donor Not Selected...!")]
        public string GLookUp_PartyList_Donation { get; set; } //donor id    
        public string Txt_V_NO_Donation { get; set; }
        [Required(ErrorMessage = "Voucher Date Not Selected...!")]
        public DateTime? Txt_V_Date_Donation { get; set; }
        public string Txt_Narration_Donation { get; set; }
        public string Txt_Remarks_Donation { get; set; }
        public string Txt_Reference_Donation { get; set; }
        public string xID { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string BE_Receipt_No_Donation { get; set; }
        public string iTrans_Type { get; set; }
        public string iLed_ID { get; set; }        
        public string BE_Bank_Acc_No_Donation { get; set; }       
        public string iVoucher_Type { get; set; }
        public string iSpecific_ItemID { get; set; }
        public string TitleX { get; set; }
        public string Text { get; set; }
        public string BE_Add1_Donation { get; set; }
        public string BE_City_Donation { get; set; }
        public string BE_District_Donation { get; set; }
        public string BE_State_Donation { get; set; }
        public string BE_Country_Donation { get; set; }
        public string SelectedBankID { get; set; }
        public string SelectedRefBankID { get; set; }
        public int SelectedDepositSlipNo { get; set; }
        public string BE_PAN_No_Donation { get; set; }
        public string BE_AADHAAR_No_Donation { get; set; }
        public string BE_ID_No_Donation { get; set; }
        public string BE_ID_No_Label_Donation { get; set; }
        public List<Return_DonationVoucherItemList> ItemData { get; set; }
        public List<Return_DonationVoucherPartyList> PartyData { get; set; }
        public List<Return_GetBankAccounts> BankData { get; set; }
        public List<Return_ReferenceBankList> RefBankData { get; set; }
        public List<Return_DonationVocuherPurpose> PurposeData { get; set; }


        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_DnR { get; set; }
        public string DnR_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_DnR { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_DnR { get; set; }
    }
    [Serializable]
    public class ForeignDonationV 
    {
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string ActionMethod { get; set; }

        [Required(ErrorMessage = "Item Not Selected...!")]
        public string GLookUp_ItemList_Donation_F { get; set; }
        public string BE_Item_Head_Donation_F { get; set; }

        [Required(ErrorMessage = "Voucher Date Not Selected...!")]
        public DateTime? Txt_V_Date_Donation_F { get; set; }
        public string Txt_V_NO_Donation_F { get; set; }
        public string BE_Receipt_No_Donation_F { get; set; }

        [Required(ErrorMessage = "Donor Not Selected...!")]
        public string GLookUp_PartyList_Donation_F { get; set; }
        public string BE_Add1_Donation_F { get; set; }
        public string BE_City_Donation_F { get; set; }
        public string BE_District_Donation_F { get; set; }
        public string BE_State_Donation_F { get; set; }
        public string BE_Country_Donation_F { get; set; }
        public string BE_PAN_No_Donation_F { get; set; }
        public string BE_TAXATION_No_Donation_F { get; set; }
        public string BE_Passport_No_Donation_F { get; set; }
        public string Cmd_Mode_Donation_F { get; set; }
        public string GLookUp_RefBankList_Donation_F { get; set; }
        public string Txt_Ref_Branch_Donation_F { get; set; }
        public string Txt_Ref_No_Donation_F { get; set; }
        public DateTime? Txt_Ref_Date_Donation_F { get; set; }
        public string Txt_Co_Bank_Donation_F { get; set; }
        public string Txt_Co_Branch_Donation_F { get; set; }
        
        [Required(ErrorMessage = "Enter Amount...!")]
        public decimal? Txt_Foreign_Amt_Donation_F { get; set; }
        [Required(ErrorMessage = "Enter Currency Rate...!")]
        public decimal? Txt_Cur_Rate_Donation_F { get; set; }
        public decimal? Txt_INR_Amt_Donation_F { get; set; }
        public decimal? Txt_Bank_Charges_Donation_F { get; set; }
        public decimal? Txt_Amount_Donation_F { get; set; }

        [Required(ErrorMessage = "Purpose Not Selected...!")]
        public string GLookUp_PurList_Donation_F { get; set; }
        public string Txt_Narration_Donation_F { get; set; }
        public string Txt_Remarks_Donation_F { get; set; }
        public string Txt_Reference_Donation_F { get; set; }       
        public string GLookUp_BankList_Donation_F { get; set; }
        [Required(ErrorMessage = "Category Not Selected...!")]
        public string GLookUp_CatList_Donation_F { get; set; }
        [Required(ErrorMessage = "Currency Not Selected...!")]
        public string GLookUp_CurList_Donation_F { get; set; }
        public string BE_Bank_Acc_No_Donation_F { get; set; }   //Deposited Branch name
        public string iVoucher_Type_F { get; set; }
        public string iLed_ID_F { get; set; }
        public string iTrans_Type_F { get; set; }
        public string iSpecific_ItemID_F { get; set; }
        public DateTime? LastEditedOn_F { get; set; }
        public DateTime? Info_LastEditedOn_F { get; set; }
        public DateTime? Txt_Ref_CDate_Donation_F { get; set; }
        public DateTime? PartyList_REC_EDIT_ON_F { get; set; }
        public DateTime? BankList_REC_EDIT_ON_F { get; set; }
        public string TitleX_F { get; set; }
        public string Text_F { get; set; }
        public string xID_F { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_DnF { get; set; }
        public string DnF_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_DnF { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_DnF { get; set; }
    }
    [Serializable]
    public class CategoryList 
    {
        public string CAT_NAME { get; set; }
        public string CAT_ID { get; set; }
    }
    [Serializable]
    public class CurrencyList 
    {
        public string CUR_NAME { get; set; }
        public string CUR_CODE { get; set; }
        public string CUR_SYMBOL { get; set; }
        public string CUR_ID { get; set; }

    }
}