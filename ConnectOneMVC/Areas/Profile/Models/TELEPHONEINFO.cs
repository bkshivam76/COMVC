using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class TELEPHONEINFO
    {
        public string TP_NO { get; set; }
        public int TP_CEN_ID { get; set; }
        public string TP_TELECOM_MISC_ID { get; set; }
        public string TP_CATEGORY { get; set; }
        public string TP_TYPE { get; set; }
        public string TP_OTHER_DETAIL { get; set; }
        public DateTime? REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public int REC_STATUS { get; set; }
        public DateTime? REC_STATUS_ON { get; set; }
        public string REC_STATUS_BY { get; set; }
        public string REC_ID { get; set; }
        public DateTime? TP_CLOSE_DATE { get; set; }
        public string TP_CLOSE_REMARKS { get; set; }

    }
}