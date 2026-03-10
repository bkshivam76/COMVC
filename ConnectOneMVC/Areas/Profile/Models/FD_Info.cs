using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class FD_Info
    {
        public string BI_BANK_NAME { get; set; }
        public string BB_BRANCH_NAME { get; set; }
        public string FD_NO { get; set; }
        public DateTime? FD_DATE { get; set; }
        public DateTime? FD_AS_DATE { get; set; }
        public decimal? FD_AMOUNT { get; set; }
        public decimal? FD_INT_RATE { get; set; }
        public string FD_INT_PAY_COND { get; set; }
        public DateTime? FD_MAT_DATE { get; set; }
        public decimal? FD_MAT_AMT { get; set; }
        public string BA_CUST_NO { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
        public string TR_ID { get; set; }
        public string ID { get; set; }
        public string FD_Status { get; set; }
        public DateTime? CLOSE_DATE { get; set; }
        public string Entry_Type { get; set; }
        public decimal? Interest_Recd { get; set; }
        public decimal? TDS_Paid { get; set; }
        public decimal? Nett_Interest { get; set; }
        public string Other_Detail { get; set; }
        public decimal? FD_Less_Maturity { get; set; }
        public int? Remarks { get; set; }
        public string RemarkStatus { get; set; }
        public int? OpenActions { get; set; }
        public int? CrossedTimeLimit { get; set; }
        public int? YearID { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public int? OTHER_ATTACH_CNT { get; set; }
        public int? ALL_ATTACH_CNT { get; set; }
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