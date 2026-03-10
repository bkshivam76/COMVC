using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class BankInfoGrid
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }
        public string BranchId { get; set; }
        public string BA_ACCOUNT_TYPE { get; set; }
        public string BA_ACCOUNT_NO { get; set; }
        public string BA_CUST_NO { get; set; }
        public string OP_AMOUNT { get; set; }
        public string BA_OTHER_DETAIL { get; set; }
        public string BB_IFSC_CODE { get; set; }
        public string BB_MICR_CODE { get; set; }
        public string BI_BANK_PAN_NO { get; set; }
        public string BA_TAN_NO { get; set; }
        public string BA_TEL_NOS { get; set; }
        public string BA_EMAIL_ID { get; set; }
        public string BA_ACCOUNT_NEW { get; set; }
        public DateTime BA_OPEN_DATE { get; set; }
        public DateTime? BA_CLOSE_DATE { get; set; }
        public string Add_By { get; set; }
        public DateTime Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime Action_Date { get; set; }
        public string Remarks { get; set; }
        public string RemarkStatus { get; set; }
        public Int32 OpenActions { get; set; }
        public Int32 CrossedTimeLimit { get; set; }
        public Int32 YearID { get; set; }
        public int? iREQ_ATTACH_COUNT { get; set; }
        public int? iCOMPLETE_ATTACH_COUNT { get; set; }
        public int? iRESPONDED_COUNT { get; set; }
        public int? iREJECTED_COUNT { get; set; }
        public int? iOTHER_ATTACH_CNT { get; set; }
        public int? iALL_ATTACH_CNT { get; set; }


    }
}