using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class Center_Ins_List
    {

        public string INS_SHORT { get; set; }
        public string CEN_NAME_X { get; set; }
        public string INS_NAME { get; set; }
        public string INS_ID { get; set; }
        public string CEN_UID { get; set; }
        public string CEN_PAD_NO { get; set; }
        public string CEN_ACC_TYPE_ID { get; set; }
        public string CEN_ID { get; set; }
        public string COD_YEAR_ID { get; set; }
        public string COD_YEAR_NAME { get; set; }
        public string COD_YEAR_SDT { get; set; }
        public string COD_YEAR_EDT { get; set; }
        public string CEN_REC_ID { get; set; }
        public string IS_VOLUME{ get; set; }
        [Required]
        public string UserName { get; set; }
        //[Required]
        public string Password { get; set; }
        public string CEN_ZONE_ID { get; set; }
        public string CEN_ZONE_SUB_ID { get; set; }
        public string AuditStatus { get; set; }
        public string CEN_ID_MAIN { get; set; }
        public string REC_ID { get; set; }
        public string CEN_NAME { get; set; }
        public string CEN_PAD_NO_MAIN { get; set; }
        public DateTime? CEN_CANCELLATION_DATE { get; set; }//Redmine Task #135146 & Task #135145 completed

        public string DEVICE_TOKEN { get; set; }
        public string ANDROID_ID { get; set; }
        public string RedirectToAndroid { get; set; }
    }
    [Serializable]
    public class Auditor_Ins_List
    {
        public string INS_SHORT { get; set; }
        public string INS_NAME { get; set; }
        public string INS_ID { get; set; }
    }

}