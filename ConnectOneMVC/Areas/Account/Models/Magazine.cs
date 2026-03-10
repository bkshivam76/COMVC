using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectOneMVC.Areas.Account.Models
{
    public class Magazine
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Language { get; set; }
        public string PublishOn { get; set; }
        public string MagazineRegdNo { get; set; }
        public string PostalRegdNo { get; set; }
        public int MembershipStartNo { get; set; }
        public bool ForeignSubscriptionsCount { get; set; }
        public string AddBy { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }
    }
}