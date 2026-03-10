using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Membership.Models
{   
        [Serializable]
        public class MRR_Period
        {
            public string Period { get; set; }
            public int SelectedIndex { get; set; }
        }
   
}