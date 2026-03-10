using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class LiabilitiesInfo
    {

        public string LI_ITEM_ID { get; set; }
        public string LI_PARTY_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string PARTY_NAME { get; set; }
        public string LI_DATE { get; set; }
        public string LI_PAY_DATE { get; set; }
        public string LI_OTHER_DETAIL { get; set; }
        public string YearID { get; set; }
        public string Add_By { get; set; }
        public string Add_Date { get; set; }
        public string Edit_By { get; set; }
        public string Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public string Action_Date { get; set; }
        public string ID { get; set; }
        public string TR_ID { get; set; }
        public string RemarkStatus { get; set; }
        public bool Remarks { get; set; }
        public string CrossedTimeLimit { get; set; }
        public string OpenActions { get; set; }
        public decimal Amount { get; set; }
        public decimal Paid { get; set; }
        public decimal Out_Standing { get; set; }
        public string Type { get; set; }

        public decimal Addition { get; set; }
        public decimal Adjusted { get; set; }
        public int? RemarkCount { get; set; }
        public string Reason { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public Int32? OTHER_ATTACH_CNT { get; set; }
        public Int32? ALL_ATTACH_CNT { get; set; }

        //Added for Audit Icon Filter
        public Int32? VOUCHING_ACCEPTED_COUNT { get; set; }
        public Int32? VOUCHING_PENDING_COUNT { get; set; }
        public Int32? VOUCHING_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? VOUCHING_REJECTED_COUNT { get; set; }
        public Int32? VOUCHING_TOTAL_COUNT { get; set; }
        public Int32? AUDIT_PENDING_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? AUDIT_REJECTED_COUNT { get; set; }
        public Int32? AUDIT_TOTAL_COUNT { get; set; }
        public string iIcon { get; set; }
        public string Special_Ref { get; set; }
    }
}