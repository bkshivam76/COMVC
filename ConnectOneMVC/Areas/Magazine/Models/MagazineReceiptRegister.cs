using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Magazine.Models
{
    [Serializable]
    public class MagazineRR_SpeceficPeriod
    {
        public DateTime MagazineRR_Fromdate { get; set; }
        public DateTime MagazineRR_Todate { get; set; }
    }
}