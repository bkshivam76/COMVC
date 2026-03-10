using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class BANK_INFO
    {
        public string BI_BANK_NAME { get; set; }
        public string BI_BANK_PAN_NO { get; set; }
        public string REC_ID { get; set; }
    }
    [Serializable]
    public class Telecom_INFO
    {
        public string MISC_NAME { get; set; }
        public string MISC_ID { get; set; }
    }
    [Serializable]
    public class Item_INFO_Vehicle
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }
    [Serializable]
    public class Item_INFO
    {
        public string NAME { get; set; }
        public string ID { get; set; }
    }
}