using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    public class HTMLEmailer
    {
    }
    [Serializable]
    public class DonationReceipt80G
    {
        public string body { get; set; }
        public string donationDataRows { get; set; }
        public string donationDataHeader { get; set; }
        public string instituteName { get; set; }
        public string instituteLogoURL { get; set; }
        public string cenID { get; set; }
    }
}