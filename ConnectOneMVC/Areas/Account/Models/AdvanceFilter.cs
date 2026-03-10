using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class AdvanceFilter
    {   [Required]
        [Display(Name = "Filter Types:")]
        public string Cmb_FilterTypes_CB { get; set; }     
        [Required]
        [Display(Name = "Filter Criteria:")]
        public string GlookUp_FilterCriteria_CB { get; set; }
        public string Advanced_Filter_Category { get; set; }
        public string Advanced_Filter_RefID { get; set; }        
        public string FilterType { get; set; }
        public string AssetProfile { get; set; }
    }
    [Serializable]
    public class CB_AdvanceFilter
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }
        public string Party { get; set; }
        public string Date { get; set; }
        public string FD_No { get; set; }
        public string FD_Date { get; set; }
        public decimal? FD_Amt { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Use { get; set; }
        public string Name { get; set; }
        public string BIRTHYEAR { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string DESC { get; set; }
        public string AccNo { get; set; }
        public string Bank { get; set; }
        public string Reg_No { get; set; }
        public string Status { get; set; }
        
    }
    [Serializable]
    public class AF_Advance
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }

        public string Party { get; set; }

        public string Date { get; set; }

    }
    [Serializable]
    public class AF_Fd
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }

        public string FD_No { get; set; }

        public string FD_Date { get; set; }
        public decimal? FD_Amt { get; set; }


    }
    [Serializable]
    public class AF_LandAndBuilding
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }
        public string Use { get; set; }

    }
    [Serializable]
    public class AF_LiveStock
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }

        public string Name { get; set; }

        public string BIRTHYEAR { get; set; }

    }
    [Serializable]
    public class AF_Movabale_Assets
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

    }
    [Serializable]
    public class AF_Other_Deposits_Liabilites
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }

        public string Party { get; set; }

        public string Date { get; set; }

    }
    [Serializable]
    public class AF_Silver_Gold
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }

        public string DESC { get; set; }

    }
    [Serializable]
    public class AF_BankAccounts
    {
        public string REC_ID { get; set; }
        public string Item { get; set; }       
        public string AccNo { get; set; }
    }
    [Serializable]
    public class AF_Vechiles
    {
        public string REC_ID { get; set; }
        public string Make { get; set; }
        public string Item { get; set; }
        public string Model { get; set; }
        public string Reg_No { get; set; }
    }
}