using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    public class DonationRegisterPeriod
    {
        public DateTime? DonReg_Fromdate { get; set; }
        public DateTime? DonReg_Todate { get; set; }
    }
}