using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class BankPaymentDetail
    {
        public int Sr { get; set; }   
        
        [Display(Name = "Bank Name:")]
        [Required]
        public string GLookUp_BankList { get; set; }
        [Display(Name = "Branch Name:")]
        public string BE_Bank_Branch { get; set; }
        [Display(Name = "A/c. No.:")]
        public string BE_Bank_Acc_No { get; set; }
        [Display(Name = "Mode of Payment:")]
        [Required]
        public string Cmd_Mode { get; set; }
        [Display(Name = "Cheque No.:")]
        //[Required]
        public string Txt_Ref_No { get; set; }
        [Display(Name = "Cheque Date:")]
        [Required]
        public DateTime? Txt_Ref_Date { get; set; }
        [Display(Name = "Clearing Date:")]
        public DateTime? Txt_Ref_CDate { get; set; }
        [Display(Name = "Money Transfer (CBS / RTGS / NEFT) Bank:")]
        //[Required]
        public string GLookUp_RefBankList { get; set; }
        [Display(Name = "Ref. A/c. No.:")]
        //[Required]
        public string Txt_Trf_ANo { get; set; }
        [Display(Name = "Amount:")]
        [Required]
        public double? Txt_Amount { get; set; }     
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }       
        public string BANK_NAME { get; set; }
        public string MT_BANK_NAME { get; set; }
        public DateTime? Edit_Time { get; set; }
        public double? Payment_Credit_Amt { get; set; }
        public string iBank_ID { get; set; }
      public string iMT_BANK_ID { get; set; }
        public List<BankList> BankListData { get; set; }
        public List<Return_RefBank> RefBankListData { get; set; }

    }
    [Serializable]
    public class PaymentBankDetail_Grid_Datatable
    {
        public int Sr { get; set; }
        public double? Amount { get; set; }
        public string Mode { get; set; }
        public string Ref_No { get; set; }
        public DateTime? Ref_Date { get; set; }
        public DateTime? Ref_CDate { get; set; }
        public string BANK_NAME { get; set; }
        public string Branch { get; set; }
        public string Acc_No { get; set; }
        public string ID { get; set; }
        public string MT_BANK_Name { get; set; }
        public string Ref_Acc_No { get; set; }
        public string MT_BANK_ID { get; set; }
        public DateTime Edit_Time { get; set; }



    }
}