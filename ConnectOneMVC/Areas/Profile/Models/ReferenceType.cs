using System;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class ReferenceType
    {
        public long SNo { get; set; }
        public string LED_ID { get; set; }
        public string WIP_Ledger { get; set; }
        public string Reference { get; set; }
        public decimal Opening { get; set; }
        public decimal Addition { get; set; }
        public decimal Deduction { get; set; }
        public decimal Closing { get; set; }
        public decimal Next_Year_Closing_Value { get; set; }
        public DateTime Date_of_Creation { get; set; }
        public int YearID { get; set; }
        public string TR_ID { get; set; }
        public string Entry_Type { get; set; }
        public int RemarkCount { get; set; }
        public string RemarkStatus { get; set; }
        public int OpenActions { get; set; }
        public int CrossedTimeLimit { get; set; }
        public string Add_By { get; set; }
        public DateTime Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime Action_Date { get; set; }
        public string ID { get; set; }
    }

 
}