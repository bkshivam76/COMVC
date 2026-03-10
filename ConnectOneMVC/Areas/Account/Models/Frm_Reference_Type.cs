using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;
using System;
using static Common_Lib.DbOperations;
using System.Collections.Generic;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Frm_Reference_Type_Model
    {
        public string Led_ID { get; set; }
        public string Txn_M_ID { get; set; }

        public object Look_InsList { get; set; }
        public object Look_LocList { get; set; }

        public string Ref_Rec_ID { get; set; }
        public string Reference { get; set; }
        public decimal Opening { get; set; }
        public decimal Closing { get; set; }
        public decimal NextYearClosing { get; set; }
        public string Txt_Amt { get; set; }
        public string RefType { get; internal set; }
        public string Specific_ItemID { get; set; }
        public string Tag { get; set; }
    }
    [Serializable]
    public class Frm_WIP_Window_Model
    {
        [Required(ErrorMessage = "Ledger Name Not Selected..!")]
        public string LedID { get; set; }
        public double Amount_WIP { get; set; }      
        [Required(ErrorMessage = "Please enter relevant reference which you may remember while converting WIP to assets  . . . !")]
        public string Reference_WIP { get; set; }
        public string xID { get; set; }
        public string iTxnM_ID { get; set; }
        [Required(ErrorMessage = "Ledger Name Not Selected..!")]
        public string GLookUp_WIP_LedgerList { get; set; }               
        public string TempActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }      
        public string iSpecific_itemID { get; set; }
        public string Ref_RecID { get; set; }
        public string iReftype { get; set; }
        public List<Return_WIP_Ledger> WIPLedgerListData { get; set; }
    }
}