using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class SelectedCenterDetails
    {
        public string CEN_NAME { get; set; }
        public string CEN_ID_Main { get; set; }
        public string CEN_PAD_NO_Main { get; set; }
        public string CEN_ZONE_ID { get; set; }
        public string CEN_ZONE_SUB_ID { get; set; }
    }
}