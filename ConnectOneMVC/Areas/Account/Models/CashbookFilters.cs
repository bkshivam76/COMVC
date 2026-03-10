using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    public class CashbookFilters
    {
        public string CB_VouchingStatus { get; set; }
        public string CB_VouchedBy { get; set; }
        public string CB_ReviewedBy { get; set; }
        public string[] CB_Zone { get; set; }
        public string[] CB_SubZone { get; set; }
        public string[] CB_State { get; set; }
        public string[] CB_UID { get; set; }
        public string[] CB_EntryScreen { get; set; }
        public string[] CB_Head { get; set; }
        public string[] CB_Item { get; set; }
        public int? CB_Amount1 { get; set; }
        public int? CB_Amount2 { get; set; }
        public string CB_Mode { get; set; }
        public string CB_Type { get; set; }
        public string[] CB_Purpose { get; set; }
        public string CB_Narration { get; set; }
        public string CB_RejectReason { get; set; }
        public int? CB_ReviewdCount1 { get; set; }
        public int? CB_ReviewdCount2 { get; set; }
        public string CB_Document { get; set; }
        public string CB_DocumentCategory { get; set; }
        public DateTime? CB_DocumentFromDate { get; set; }
        public DateTime? CB_DocumentToDate { get; set; }
        public string CB_DocumentDescription { get; set; }
        public string CB_VouchingCategory { get; set; }
        public string CB_DataScope { get; set; }
        public string CB_Poolsize { get; set; }  
        public bool? CB_Include_Audited_Period { get; set; }
}
}