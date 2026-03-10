using DataAnnotationsExtensions;
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
    public class Voucher_Win_Cash
    {
        public string RecID { get; set; }
        public string Txt_V_NO_CDW { get; set; }
        [Required(ErrorMessage = Common_Lib.Messages.CommonTooltips_DialogResults.VchDateIncorrect)] //"Voucher Date Not Selected...!"
        public DateTime? Txt_V_Date_CDW { get; set; }

        [Required(ErrorMessage = "Bank Name Not Selected...!")]
        public string GLookUp_BankList_CDW { get; set; }
        [Required(ErrorMessage = Common_Lib.Messages.CommonTooltips_DialogResults.ItemNotSelected)] //"Item Name Not Selected...!"
        public string GLookUp_ItemList_CDW { get; set; }
        public string iTrans_Type { get; set; }
        public string LED_ID { get; set; }
        public string VOUCHER_TYPE { get; set; }
        public string iMode { get; set; }
        public string Txt_Ref_No_CDW { get; set; }
        public DateTime? Txt_Ref_Date_CDW { get; set; }
        public DateTime? Txt_Ref_CDate_CDW { get; set; }
        public double? Txt_Amount_CDW { get; set; }
        public string Txt_Narration_CDW { get; set; }
        public string Txt_Remarks_CDW { get; set; }
        public string Txt_Reference_CDW { get; set; }
        public DateTime? REC_EDIT_ON_Bank_CDW { get; set; }
        public string BE_Bank_Acc_No_Tag_CDW { get; set; }     
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public DateTime? LastEditedOn { get; set; }
      
        public string PurposeID_CDW { get; set; }

        public List<BankList> BankListData { get; set; }
        public List<VoucherTypeItems> ItemListData { get; set; }
        public List<Return_DonationVocuherPurpose> PurposeListData { get; set; }

        //FCRA or Special Voucher References
        
        //Listbox result variable to use in controller(FCRA)
      
        public int splVchrRefsCount { get; set; }    
        public string cdw_SplVchrReferenceSelected { get; set; }
        public string[] SpecialReference_Get_SelectedValue_CDW { get; set; }
        public List<Return_SplVchrRefsList> SpecialReferenceList_Data_CDW { get; set; }
          
        public int Txt_2000_CDW { get; set; }     
        public int Txt_1000_CDW { get; set; }
        public int Txt_500_CDW { get; set; }  
        public int Txt_200_CDW { get; set; }    
        public int Txt_100_CDW { get; set; }  
        public int Txt_50_CDW { get; set; }      
        public int Txt_20_CDW { get; set; }  
        public int Txt_10_CDW { get; set; }      
        public int Txt_5_CDW { get; set; }   
        public int Txt_2_CDW { get; set; }
        public int Txt_1_CDW { get; set; }
    }
}