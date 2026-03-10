using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class LiveStockInfo
    {
        public string ID { get; set; }
        public string ITEM_NAME { get; set; }
        public string LS_ITEM_ID { get; set; }
        public string LivestockName { get; set; }
        public string BirthYear { get; set; }
        public string Insurance { get; set; }
        public string INSURANCE_ID { get; set; }
        public string INSURANCE_COMPANY { get; set; }
        public string LS_INS_POLICY_NO { get; set; }
        public DateTime? LS_INS_DATE{ get; set; }
        public double LS_INS_AMT { get; set; }
        public double LS_AMT { get; set; }
        public double CurrValue { get; set; }
        public string AL_LOC_NAME { get; set; }
        public string LS_LOC_AL_ID { get; set; }
        public string LS_OTHER_DETAIL { get; set; }
        public string SaleStatus { get; set; }
        public string YearID { get; set; }
        public string TR_ID { get; set; }
        public string EntryType { get; set; }
        public Int32 RemarkCount { get; set; }
        public string RemarkStatus { get; set; }
        public Int32 OpenActions { get; set; }
        public Int32 CrossedTimeLimit { get; set; }
        public bool Chk_Incompleted { get; set; }
        public string AddBy { get; set; }
        public DateTime? AddDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        public string TempActionMethod { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public int? OTHER_ATTACH_CNT { get; set; }
        public int? ALL_ATTACH_CNT { get; set; }
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