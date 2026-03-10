using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class Magazine_Membership_Info
    {
        public string tag{ get; set; }

        public string membershipID{ get; set; }

        public System.DateTime startDate{ get; set; }

        public string memberName{ get; set; }

        public string addressLine1{ get; set; }

        public string addressLine2{ get; set; }

        public string addressLine3{ get; set; }

        public string addressLine4{ get; set; }

        public string pincode{ get; set; }

        public string city{ get; set; }

        public string state{ get; set; }

        public string district{ get; set; }

        public string country{ get; set; }

        public string telNos{ get; set; }

        public string memberType{ get; set; }

        public string status{ get; set; }

        public int cOPIES{ get; set; }

        public decimal openingDue{ get; set; }

        public decimal openingAdvance{ get; set; }

        public decimal openingAccBalance{ get; set; }

        public decimal currDue{ get; set; }

        public decimal currAdvance{ get; set; }

        public string period{ get; set; }

        public string magazine{ get; set; }

        public string membershipOldID{ get; set; }

        public string category{ get; set; }

        public int mM_MS_NO{ get; set; }

        public string id{ get; set; }

        public string addBy{ get; set; }

        public System.DateTime addDate{ get; set; }

        public string editBy{ get; set; }

        public System.DateTime editDate{ get; set; }

        public string actionStatus{ get; set; }

        public string actionBy{ get; set; }

        public System.DateTime actionDate{ get; set; }

        public string entryType{ get; set; }

        public string aB_ID{ get; set; }

        public string connectedto{ get; set; }

        public System.DateTime closedon{ get; set; }

        public string closureRemarks{ get; set; }

        public System.DateTime mMB_PERIOD_TO{ get; set; }

        public string mMB_CC_DISPATCH{ get; set; }
    }
}