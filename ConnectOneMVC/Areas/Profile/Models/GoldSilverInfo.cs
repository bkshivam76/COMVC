using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class GoldSilverInfo
    {
        public string GS_ITEM_ID { get; set; }
        public string GS_DESC_MISC_ID { get; set; }
        public string GS_LOC_AL_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string MISC_NAME { get; set; }
        public decimal GS_ITEM_WEIGHT { get; set; }
        public string GS_AMT { get; set; }
        public string AL_LOC_NAME { get; set; }
        public string GS_OTHER_DETAIL { get; set; }
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
        public string RemarkCount { get; set; }
        public string RemarkStatus { get; set; }
        public decimal Rate_per_Gram { get; set; }
        public string CrossedTimeLimit { get; set; }
        public Int32? OpenActions { get; set; }
        //public string ITEM_NAME { get; set; }
        //public string MISC_NAME { get; set; }
        //public string GS_ITEM_WEIGHT { get; set; }
        //public string GS_OTHER_DETAIL { get; set; }
        public decimal Curr_Weight { get; set; }
        public decimal Curr_Value { get; set; }
        //public string GS_AMT { get; set; }
        public string Entry_Type { get; set; }
        //public string Action_Status { get; set; }
        public string Type { get; set; }
        public string Sale_Status { get; set; }
        public bool Remarks { get; set; }
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