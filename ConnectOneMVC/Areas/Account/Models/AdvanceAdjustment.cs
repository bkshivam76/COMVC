using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class AdvanceAdjustment
    {
        public int Sr { get; set; }            
        
        [Display(Name = "Item Name:")]
        [Required]
        public string BE_ItemName { get; set; }
        [Display(Name = "Purpose:")]
        public string BE_Purpose { get; set; }
        [Display(Name = "Other Details:")]
        public string BE_Other_Detail { get; set; }
        [Display(Name = "Advance:")]
        public double BE_Adv_Amt { get; set; }
        [Display(Name = "Adjusted:")]
        public double BE_Adjust_Amt { get; set; }
        [Display(Name = "Refund:")]
        public double BE_Refund_Amt { get; set; }
        [Display(Name = "Out-Standing:")]
        public double BE_OS_Amt { get; set; }
        [Display(Name = "Given Date:")]
        public DateTime? BE_Given_Date { get; set; }
        [Display(Name = "Payment:")]
        [Required]
        public double? Txt_Amount { get; set; }       
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }     
        public double? Next_Year_Balance { get; set; }
        public double? Payment_Credit_Amt { get; set; }

    }

    [Serializable]
    public class AdvanceAdjustment_Grid_Datatable
    {
        public Int64 Sr { get; set; }
        public string GLookUp_PartyList1 { get; set; }
        public double Txt_Amount { get; set; }
        public string BE_ItemName { get; set; }
        public string BE_Purpose { get; set; }
        public string BE_Other_Detail { get; set; }

        public double BE_Adv_Amt { get; set; }
        public double BE_Adjust_Amt { get; set; }
        public double BE_Refund_Amt { get; set; }
        public double BE_OS_Amt { get; set; }
        public double Next_Year_Balance { get; set; }
        public DateTime? BE_Given_Date { get; set; }

        //public int Sr { get; set; }
        public DateTime? GivenDate { get; set; }
        public string Item { get; set; }
        public string AI_ITEM_ID { get; set; }
        public string OFFSET_ID { get; set; }
        public decimal? Advance { get; set; }
        public decimal? Addition { get; set; }
        public decimal? Adjusted { get; set; }
        public decimal? Refund { get; set; }
        public decimal? Out_Standing { get; set; }
        public decimal? Next_Year_Out_Standing { get; set; }
        public decimal? Payment { get; set; }
        public string Purpose { get; set; }
        public string Other_Details { get; set; }
        public string AI_ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public DateTime? REF_CREATION_DATE { get; set; }


    }
}