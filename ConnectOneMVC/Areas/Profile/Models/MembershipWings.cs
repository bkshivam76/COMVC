using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common_Lib;

namespace ConnectOneMVC.Areas.Profile.Models
{
    public class Exist_Wing_Members
    {
        public string Cmb_ListBy_Membership_Ww { get; set; }

        public string GLookUp_WingList_Membership_Ww { get; set; }
        public List<WingsList_ww> WingList_Data_Membership_Ww { get; set; }

        public string Txt_Str_Membership_Ww { get; set; }

        public string Look_SubsList_Membership_Ww { get; set; }
        public List<Subscribers_ww> SubsList_Data_Membership_Ww { get; set; }

        public List<DbOperations.Membership.Return_ExistingWingMembership> GridData_Membership_Ww { get; set; }
    }

    [Serializable]
    public class Subscribers_ww
    {
        public string SI_NAME { get; set; }
        public string SI_CATEGORY { get; set; }
        public Int32? SI_START_MONTH { get; set; }
        public Int32? SI_TOTAL_MONTH { get; set; }
        public string SI_REC_ID { get; set; }
    }
    [Serializable]
    public class WingsList_ww
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string WING_SHORT_MS { get; set; }
    }
}