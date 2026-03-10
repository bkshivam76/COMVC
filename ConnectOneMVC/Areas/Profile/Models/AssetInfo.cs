using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class AssetInfo
    {
        public string ASSET_TYPE { get; set; }
        public string ITEM_NAME { get; set; }
        public string AI_ITEM_ID { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string Head { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string AI_SERIAL_NO { get; set; }
        public DateTime? AI_PUR_DATE { get; set; }
        public decimal? AI_AMT_FOR_INS { get; set; }
        public decimal? AI_ADJ_FOR_INS { get; set; }
        public decimal? AI_CLOSE_FOR_INS { get; set; }
        public decimal? AI_PUR_AMT { get; set; }
        public decimal? Curr_Value { get; set; }
        public decimal? sale_quantity { get; set; }
        public decimal? Warranty { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Curr_Qty { get; set; }
        public decimal? Rate { get; set; }
        public string AL_LOC_NAME { get; set; }
        public string AI_LOC_AL_ID { get; set; }
        public string AI_OTHER_DETAIL { get; set; }
        public string TR_ID { get; set; }
        public string YearID { get; set; }
        public string ID { get; set; }
        public string QR_Code_ID { get; set; }
        public string Sale_Status { get; set; }
        public string Entry_Type { get; set; }
        public int? Remark_Count { get; set; }
        public string Remark_Status { get; set; }
        public int? Open_Actions { get; set; }
        public int? Crossed_Time_Limit { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public bool Remarks { get; set; }
        public Int32? OTHER_ATTACH_CNT { get; set; }
        public Int32? ALL_ATTACH_CNT { get; set; }
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
        //Added for Audit Icon Filter
        public string iIcon { get; set; }
        public string Special_Ref { get; set; }
    }
}