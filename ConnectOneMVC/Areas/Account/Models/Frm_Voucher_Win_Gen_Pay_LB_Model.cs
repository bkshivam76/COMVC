using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Frm_Voucher_Win_Gen_Pay_LB_Model
    {
        public Int64 Sr { get; set; }
        public string i_PartyID { get; set; }
        [Display(Name = "Item Name:")]
        [Required]
        public string BE_ItemName { get; set; }
        [Display(Name = "Purpose:")]
        public string BE_Purpose { get; set; }
        [Display(Name = "Other Details:")]
        public string BE_Other_Detail { get; set; }
        [Display(Name = "Amount:")]
        public double? BE_Adv_Amt { get; set; }
        [Display(Name = "Paid:")]
        public double? BE_Paid_Amt { get; set; }
        [Display(Name = "Out-Standing:")]
        public double? BE_OS_Amt { get; set; }
        public double? Next_Year_Balance { get; set; }
        [Display(Name = "Payment:")]
        public double? Txt_Amount { get; set; }
        [Display(Name = "Given Date:")]
        public DateTime? BE_Given_Date { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public double? Payment_Credit_Amt { get; set; }
    }
    [Serializable]
    public class Frm_Voucher_Win_Gen_Pay_LB_Grid4_Model
    {
        public Int64 Sr { get; set; }
        public DateTime? Date { get; set; }
        public string Item { get; set; }
        public string OFFSET_ID { get; set; }
        public string LI_ITEM_ID { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Addition { get; set; }
        public decimal? Adjusted { get; set; }
        public decimal? Paid { get; set; }
        public decimal? OutStanding { get; set; }
        public decimal? Next_Year_OutStanding { get; set; }
        public decimal? Payment { get; set; }
        public string Purpose { get; set; }
        public string Other_Details { get; set; }
        public string LI_ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }

      

    }
}