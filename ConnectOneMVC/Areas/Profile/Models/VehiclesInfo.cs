using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class VehiclesInfo
    {

        public string ITEM_NAME { get; set; }
        public string VI_ITEM_ID { get; set; }
        public string MAKE { get; set; }
        public string Model { get; set; }
        public string VI_REG_NO { get; set; }
        public DateTime? Date_of_First_Registration { get; set; }
        public decimal Opening_Value { get; set; }
        public decimal Curr_Value { get; set; }

        public string Ownership { get; set; }
        public string VI_DOC_RC_BOOK { get; set; }
        public string Affidavit { get; set; }
        public string Will { get; set; }
        public string Transfer_Lettter { get; set; }
        public string Free_Use_Letter { get; set; }
        public string Other_Documents { get; set; }
        public string Insurance_Company { get; set; }
        public string INSURANCE_ID { get; set; }
        public string VI_INS_POLICY_NO { get; set; }
        public DateTime? Expiry_Date { get; set; }

        public string VI_OTHER_DETAIL { get; set; }

        public string AL_LOC_AL_ID { get; set; }

        public string AL_LOC_Name { get; set; }
        public string YEAR_ID { get; set; }
        public string TR_ID { get; set; }
        public string ID { get; set; }
        public string Sale_Status { get; set; }
        public string Entry_Type { get; set; }

        public int? Remark_Count { get; set; }

        public string Remark_Status { get; set; }

        public int? Open_Actions { get; set; }

        public int Crossed_Time_Limit { get; set; }

        public string Add_By { get; set; }

        public string Edit_By { get; set; }

        public DateTime Add_Date { get; set; }

        public DateTime Edit_Date { get; set; }

        public string Action_Status { get; set; }

        public string Action_By { get; set; }

        public DateTime Action_Date { get; set; }
        public string Entry_Status { get; set; }

        public bool Remarks { get; set; }
        public int? REQ_ATTACH_COUNT { get; set; }
        public int? COMPLETE_ATTACH_COUNT { get; set; }
        public int? RESPONDED_COUNT { get; set; }
        public int? REJECTED_COUNT { get; set; }
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