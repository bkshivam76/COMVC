using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class InternalTransferRegister
    {
        public List<Return_InternalTransferRegister_Posted> GridView1{get;set;}
        public List<Return_InternalTransferRegister_Pending> GridView2 { get; set; }  
        public string xID { get; set; }
    }
    [Serializable]
    public class Return_InternalTransferRegister_Pending
    {
        public string ActionStatus { get; set; }
        public DateTime EditDate { get; set; }
        public string EditBy { get; set; }
        public DateTime AddDate { get; set; }
        public string AddBy { get; set; }
        public string MID { get; set; }
        public string ID { get; set; }
        public string Purpose { get; set; }
        public DateTime? RefDate { get; set; }
        public string RefNo { get; set; }

        public string ActionBy { get; set; }
        public string Bank_AC_No { get; set; }
        public string Bank_Name { get; set; }
        public decimal? Amount { get; set; }
        public string Mode { get; set; }
        public DateTime? xDate { get; set; }
        public string Description { get; set; }
        public string Sub_Zone { get; set; }
        public string Zone { get; set; }
        public string No { get; set; }
        public string Center_UID { get; set; }
        public string Centre_Name { get; set; }
        public string Status { get; set; }
        public string Branch_Name { get; set; }
        public DateTime ActionDate { get; set; }
        public string CEN_ID { get; set; }
        public string ITEM_ID { get; set; }
        public string BI_ID { get; set; }
        public string Incharge { get; set; }
        public string ContactNo { get; set; }
        public string PUR_ID { get; set; }
        public string REF_BI_ID{ get; set; }
        public string Ref_Branch { get; set; }
        public string Ref_Others { get; set; }
        public string Ref_Bank_AccNo { get; set; }
        public string Narration { get; set; }
    }
    [Serializable]
    public class InternalTransfer_Matching
    {
       public string Matched { get; set; }
        public string Entering_Centre { get; set; }
        public string Item { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Mode { get; set; }
        public string RefNo { get; set; }
        public string Receiving_Center { get; set; }
        public string Paying_Center { get; set; }
        public string REC_ID { get; set; }


    }
}