using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Summary
    {
    public string Title { get; set; }
        public double Sr { get; set; }
        public string Description { get; set; }
        public double O_BALANCE { get; set; }
        public double R_BALANCE { get; set; }
        public double P_BALANCE { get; set; }
        public double C_BALANCE { get; set; }
    }
}