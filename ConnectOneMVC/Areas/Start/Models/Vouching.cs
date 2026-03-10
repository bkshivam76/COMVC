using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using System.ComponentModel.DataAnnotations;
namespace ConnectOneMVC.Areas.Start.Models
{
    public class VouchingAuditModel
    {
        public string Curr_Attachment_File_Name { get; set; }
        public string VF_ENTRY_ID { get; set; }
        public string VF_Cash_Donation_Multiple_Cash_Donations_Same_Day_Same_Party { get; set; }
        public Boolean VF_Cash_Donation_Correct_Name_Instt { get; set; }
        public string VF_Cash_Donation_Donation_Type { get; set; }
        public Boolean VF_Cash_Donation_Signed { get; set; }
        public string VF_Vouching_Status { get; set; }

        public string VF_Instt_Code { get; set; }
        public string VF_Category { get; set; }
        public List<Common_Lib.DbOperations.DonationRegister.Return_DonationRegister> DonationData;
    }
    public class VouchingDashboard
    {
        public string Category { get; set; }
        public string Instt { get; set; }
        public Int32 Financial_Year { get; set; }
        public string Entries_Available { get; set; }
        public string PhotoUrl { get; set; }
    }
}