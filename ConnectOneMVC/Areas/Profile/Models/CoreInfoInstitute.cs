using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class CoreInfoInstitute
    {
        public string ID { get; set; }
        public string INS_NAME { get; set; }
        public string CEN_NAME { get; set; }
        public string CEN_UID { get; set; }
        public string CEN_INCHARGE { get; set; }
        public string AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string EditBy { get; set; }
        public DateTime EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public DateTime ActionDate { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        public string TempActionMethod { get; set; }
    }
}