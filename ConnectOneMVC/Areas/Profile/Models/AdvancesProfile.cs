using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class AdvancesProfile
    {
        public string ITEM_NAME { get; set; }
        public string AI_ITEM_ID { get; set; }
        public string AI_PARTY_ID { get; set; }
        public string PARTY_NAME { get; set; }
        public DateTime? AI_ADV_DATE { get; set; }
        public decimal Advance { get; set; }
        public decimal Addition { get; set; }
        public decimal Adjusted { get; set; }
        public decimal Refund { get; set; }
        public decimal OutStanding { get; set; }
        public string Reason { get; set; }
        public string AI_OTHER_DETAIL { get; set; }
        public string TR_ID { get; set; }
        public Int32 YearID { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public Int32 RemarkCount { get; set; }
        public string RemarkStatus { get; set; }
        public Int32 OpenActions { get; set; }
        public Int32 CrossedTimeLimit { get; set; }
        public string AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string EditBy { get; set; }
        public DateTime EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }
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