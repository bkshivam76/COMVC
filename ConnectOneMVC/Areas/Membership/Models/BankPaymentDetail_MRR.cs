using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Membership.Models
{
    [Serializable]
    public class BankPaymentDetail_MRR
    {
        public int Sr { get; set; }

        [Display(Name = "Bank Name:")]
        [Required]
        public string GLookUp_RefBankList_MRR { get; set; }
      
        [Display(Name = "Branch Name:")]
        //[Required(ErrorMessage = "Branch Name Not Specified")]
        public string Txt_Ref_Branch_MRR { get; set; }

        [Display(Name = "Cheque No.:")]
        [Required(ErrorMessage = "Cheque No. Not Specified")]
        public string Txt_Ref_No_MRR { get; set; }
        [Display(Name = "Cheque Date:")]
        [Required]
        public DateTime? Txt_Ref_Date_MRR { get; set; }
        [Display(Name = "Clearing Date:")]
        public DateTime? Txt_Ref_CDate_MRR { get; set; }
        [Display(Name = "Mode of Payment:")]
        [Required]
        public string Cmd_Mode_MRR { get; set; }
       
        [Display(Name = "Bank Name:")]
        [Required]
        public string GLookUp_BankList_MRR { get; set; }
        [Display(Name = "Branch Name:")]
        public string BE_Bank_Branch_MRR { get; set; }
        [Display(Name = "A/c. No.:")]
        //[Required]
        public string BE_Bank_Acc_No_MRR { get; set; }
        [Display(Name = "Amount:")]
        [Required]
        public double? Txt_Amount_MRR { get; set; }
        public DateTime? Bank_Last_Edit_On { get; set; }


        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public string REF_BANK_NAME { get; set; }
        public string DEP_BANK_NAME { get; set; }
        //public DateTime? Edit_Time { get; set; }
        public double? Payment_Credit_Amt { get; set; }
        public string iBank_ID { get; set; }
        public string iRef_BANK_ID { get; set; }
        public List<BankList_MRR> DepBankDD_Data { get; set; }
        public List<Return_RefBank> RefBankDD_Data { get; set; }
    }
    [Serializable]
    public class PaymentBankDetail_Grid_Datatable_MRR
    {
        public Int32? Sr { get; set; }
        public double? Amount { get; set; }
        public string Mode { get; set; }
        public string Ref_No { get; set; }
        public DateTime? Ref_Date { get; set; }
        public DateTime? Ref_CDate { get; set; }
        public string REF_BANK_NAME { get; set; }
        public string Branch { get; set; }       
        public string ID { get; set; }
        public string DEP_BANK_NAME { get; set; }
        public string DEP_BRANCH_NAME { get; set; }
        public string Acc_No { get; set; }
        //public string Ref_Acc_No { get; set; }
        public string DEP_BANK_ID { get; set; }
        public DateTime Edit_Time { get; set; }



    }
}