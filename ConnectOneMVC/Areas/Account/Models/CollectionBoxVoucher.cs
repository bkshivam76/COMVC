using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Common_Lib;
using DataAnnotationsExtensions;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class CollectionBoxVoucher
    {
        public string xID { get; set; }
        [Required(ErrorMessage = "Item Is Required...")]
        public string GLookUp_ItemList_CBox { get; set; }
        [Required(ErrorMessage = "Voucher Date Required")]
        public DateTime? Txt_V_Date_CBox { get; set; }
        public string BE_Item_Head_CBox { get; set; }
        public string Txt_V_NO_CBox { get; set; }
        [Required(ErrorMessage = "Select First Person Name..")]
        public string GLookUp_PartyList1_CBox { get; set; }
        [Required(ErrorMessage = "Select Second Person Name..")]
        public string GLookUp_PartyList2_CBox { get; set; }
        [Display(Name ="Mode:")]
        public string Cmd_Mode_CBox { get; set; }
        public string GLookUp_BankList_CBox { get; set; }
        public string BE_Bank_Branch_CBox { get; set; }
        public string BE_Bank_Acc_No_CBox { get; set; }
        public string GLookUp_RefBankList_CBox { get; set; }
        public string Txt_Ref_Branch_CBox { get; set; }
        public string Txt_Ref_No_CBox { get; set; }
        public DateTime? Txt_Ref_Date_CBox { get; set; }
        public DateTime? Txt_Ref_CDate_CBox { get; set; }
        public string Txt_Narration_CBox { get; set; }
        public string Txt_Remarks_CBox { get; set; }
        public string Txt_Reference_CBox { get; set; }
        //[Required(ErrorMessage = "Please Enter Denominations..")] Redmine Bug #134908 fix
        public double? Txt_Amount_CBox { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public string iSpecific_ItemID { get; set; }
        public string iVoucher_Type { get; set; }
        public string iTrans_Type { get; set; }
        public string iLed_ID { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
       // [StringLength(5,ErrorMessage ="* Only 5 Digits Allowed")]
        public int? Txt_2000 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        //  [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_1000 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_500 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_200 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_100 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_50 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_20 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_10 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_5 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_2 { get; set; }
        [Max(99999, ErrorMessage = "* Only 5 Digits Allowed")]
        // [StringLength(5, ErrorMessage = "* Only 5 Digits Allowed")]
        public int? Txt_1 { get; set; }
        public int BE_2000 { get; set; }
        public int BE_1000 { get; set; }
        public int BE_500 { get; set; }
        public int BE_200 { get; set; }
        public int BE_100 { get; set; }
        public int BE_50 { get; set; }
        public int BE_20 { get; set; }
        public int BE_10 { get; set; }
        public int BE_5 { get; set; }
        public int BE_2 { get; set; }
        public int BE_1 { get; set; }
        public string TitleX { get; set; }
        public string Text { get; set; }
        public string PurposeList__CollBox { get; set; }
        public List<VoucherTypeItems> CBox_ItemList { get; set; }
        public List<BankList> CBox_BankList { get; set; }
        public List<RefBank> CBox_RefBankList { get; set; }
        public List<DbOperations.Voucher_Donation.Return_DonationVocuherPurpose> CBox_PurposeList { get; set; }
        public List<CollectionBoxPartyList> CBox_PartyList { get; set; }
        public DateTime? Party1_LastEditedOn { get; set; }
        public DateTime? Party2_LastEditedOn { get; set; }

    }
}