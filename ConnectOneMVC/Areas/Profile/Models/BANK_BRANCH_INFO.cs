using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class BANK_BRANCH_INFO
    {
        public string BB_BRANCH_NAME { get; set; }
        public string BB_IFSC_CODE { get; set; }
        public string BB_MICR_CODE { get; set; }
        public string BB_ID { get; set; }
    }
}