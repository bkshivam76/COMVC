using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class FD
    {
        public bool RowFlag1 { get; set; }
        public string SubTitleX { get; set; }
        public string xID { get; set; }
        
        public string BankList { get; set; }
        public string Lbl_Branches { get; set; }
        public string Lbl_AccountNumber { get; set; }
        public string Txt_No { get; set; }

        public DateTime? Txt_Date { get; set; }

        public Double Lbl_timePeriod { get; set; }
        public DateTime? Txt_As_Date { get; set; }
        public string Txt_Amount { get; set; }
        public string txt_Rate { get; set; }
        public string Cmd_Type { get; set; }
        public double? Txt_Mat_Amount { get; set; }
        public DateTime? Txt_Mat_Date { get; set; }
        public string Lbl_Period { get; set; }

        public string Txt_Remarks { get; set; }
        public string ID { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public string REC_EDIT_ON_Bank { get; set; }
        
        public string AddBy { get; set; }
        public DateTime? AddDate { get; set; }
        public string EdiBy { get; set; }
        public DateTime? EditDate { get; set; }
        public string ActionStatus { get; set; }
        public string ActionBy { get; set; }
        public string ActionDate { get; set; }
        public string Remarks { get; set; }
        public string RemarkStatus { get; set; }
        public string OpenActions { get; set; }
        public string CrossedTimeLimit { get; set; }
        public string YearID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public bool Chk_Incompleted { get; set; }
    }
    [Serializable]
    public class FD_Bank
    {
        public string Name { get; set; }
        public string Branch { get; set; }
        public string BA_CUST_NO { get; set; }
        public string ID { get; set; }
        public DateTime? RecEditOn { get; set; }
    }

}