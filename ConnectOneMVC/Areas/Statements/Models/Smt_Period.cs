using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Statements.Models
{
    [Serializable]
    public class Smt_Period
    {
        public DateTime? Smt_Fromdate { get; set; }
        public DateTime? Smt_Todate { get; set; }

    }
}