using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    public class Membership_wow
    {
        
    }

    [Serializable]
    public class Subscribers_wow
    {
        public string SI_NAME { get; set; }
        public string SI_CATEGORY { get; set; }
        public Int32? SI_START_MONTH { get; set; }
        public Int32? SI_TOTAL_MONTH { get; set; }        
        public string SI_REC_ID { get; set; }
    }
    [Serializable]
    public class WingsList_wow
    {
        public string NAME { get; set; }
        
        public string WING_SHORT_MS { get; set; }
       
        public string ID { get; set; }
        

    }
}