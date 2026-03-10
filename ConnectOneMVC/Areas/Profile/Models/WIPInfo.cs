using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class WIPInfo
    {
        public string ID { get; set; }        
        public string LED_ID { get; set; }
        [Required(ErrorMessage = "Ledger Name Not Selected..!")]
        public string GLookUp_WIP_LedgerList_WIPWindow { get; set; }
        [Required(ErrorMessage = "Please enter relevant reference which you may remember while converting WIP to assets  . . . !")]
        public string Reference_WIPWindow { get; set; }
        [Required(ErrorMessage = "Opening Value cannot be Zero / Negative. . . !")]
        public double? Opening_WIPWindow { get; set; }      
        public DateTime? LastEditedOn { get; set; }
        public int YearID { get; set; }
        public string xID { get; set; }
        public string TR_ID { get; set; } 
        public bool IsWIPCarriedFwd { get; set; }   
        public DateTime EditDate { get; set; }   
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
    }

    [Serializable]
    public class WIP_Txn_Report 
    {
        public string ID { get; set; }
        public double Opening { get; set; }
        public string LED_ID { get; set; }
        public string WIP_Ledger{ get; set; }
        public string Reference { get; set; }
        public DateTime Date_of_Creation { get; set; }
        public string WIP_PopupID { get; set; }
        public string  CallingFrom { get; set; }
        public string EntryType { get; set; }

        public List<WIP_Txn_Report> GridData { get; set; }       
    }
    [Serializable]
    public class WIP_Txn_Report_Grid 
    {
        public string Voucher { get; set; }
        public DateTime? Date { get; set; }
        public string ItemName { get; set; }
        public string Party { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public string Balance { get; set; }
    }
}