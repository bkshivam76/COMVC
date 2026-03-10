using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class InternalTransferToCenterList
    {
        public int TO_CEN_ID { get; set; }
        public string TO_ID { get; set; }
        public string TO_CEN_NAME { get; set; }
        public string TO_INCHARGE { get; set; }

        public string TO_PAD_NO { get; set; }
        public string TO_TEL_NO { get; set; }
        public string TO_UID { get; set; }
        public string TO_ZONE { get; set; }
    }
    [Serializable]
    public class InternalTransferCenterList
    {
        public int? TO_CEN_ID { get; set; }
        public string TO_ID { get; set; }
        public string TO_CEN_NAME { get; set; }
        public string TO_INCHARGE { get; set; }

        public string TO_PAD_NO { get; set; }
        public string TO_TEL_NO { get; set; }
        public string TO_UID { get; set; }
        public string TO_ZONE { get; set; }
    }

    }