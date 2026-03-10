using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Reports.Models
{
    [Serializable]
    public class TrialBalanceModel
    {
        public string SVR_DD_TB_Report { get; set; }
        public List<SVR_List> SVR_List { get; set; }
        public List<Common_Lib.DbOperations.Reports_All.TrialBalanceReport> Grid_Data { get; set; }
    }

    [Serializable]
    public class SVR_List
    {
        public string Spl_Vouch_Ref { get; set; }
    }
}