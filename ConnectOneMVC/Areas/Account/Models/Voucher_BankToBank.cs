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
    public class Voucher_BankToBank
    {
        [Required(ErrorMessage = "Item Name Not Selected...!")]
        public string GLookUp_ItemList_B2B { get; set; }
        [Required(ErrorMessage = "Voucher Date Not Selected...!")]
        public DateTime? Txt_V_Date_B2B { get; set; }
        public string BE_Item_Head_B2B { get; set; }
        public string xID_B2B { get; set; }
        public string Txt_V_NO_B2B { get; set; }    
        [Required(ErrorMessage = "Bank Name Not Selected...!")]
        public string GLookUp_BankList1_B2B { get; set; }
        [Required(ErrorMessage = "Bank Name Not Selected...!")]
        public string GLookUp_BankList2_B2B { get; set; }
        public string iTrans_Type_B2B { get; set; }
        public string iLed_ID_B2B { get; set; }
        public string iVoucher_Type_B2B { get; set; }
        public string BE_Bank1_Acc_No_Tag_B2B { get; set; }
        [Required(ErrorMessage = "Mode Not Selected...!")]
        public string Cmd_Mode_B2B { get; set; }
        [Required(ErrorMessage = "Ref No Not Selected...!")]
        public string Txt_Ref_No_B2B { get; set; }
        [Required(ErrorMessage = "Cheque Date Not Selected...!")]
        public DateTime? Txt_Ref_Date_B2B { get; set; }
        public DateTime? Txt_Ref_CDate_B2B { get; set; }
        [Required(ErrorMessage = "Amount Not Selected...!")]
        public double? Txt_Amount_B2B { get; set; }
        public string Txt_Narration_B2B { get; set; }
        public string Txt_Remarks_B2B { get; set; }
        public string Txt_Reference_B2B { get; set; } 
        public string BE_Bank1_Branch_B2B { get; set; }
        public string BE_Bank2_Branch_B2B { get; set; }
        public string BE_Bank1_Acc_No_B2B { get; set; }
        public string BE_Bank2_Acc_No_B2B { get; set; }       
        public string REC_EDIT_ON_Bank1_B2B { get; set; }
        public string REC_EDIT_ON_Bank2_B2B { get; set; }    
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public string TitleX_B2B { get; set; }  
        public DateTime? LastEditedOn_B2B { get; set; }
        public string PurposeID_B2B { get; set; }

        //FCRA or Special Voucher References
        //Listbox result variable to use in controller(FCRA)
        public int splVchrRefsCount_B2B { get; set; }
        public string B2B_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_B2B { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_B2B { get; set; }
        public List<VoucherTypeItems> B2B_Item_Data { get; set; }
        public List<BankList> Bank1_Data_B2B { get; set; }
        public List<BankList> Bank2_Data_B2B { get; set; }
        public List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose> PurposeList_B2B { get; set; }
    }
}