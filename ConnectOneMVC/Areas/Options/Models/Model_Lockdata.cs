using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.DataRestriction;

namespace ConnectOneMVC.Areas.Options.Models
{
    public class Model_Lockdata
    {
        public string PartyID_LockData { get; set; }
        public string BankID_LockData { get; set; }
        public string LedgerID_LockData { get; set; }      
        public DateTime? PeriodFrom_LockData { get; set; }     
        public DateTime? PeriodTo_LockData { get; set; }
        [Required(ErrorMessage = "Lock Type Is Required")]
        public string LockType_LockData { get; set; }
        public string Remarks_LockData { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int RecID_LockData { get; set; }
    }
}