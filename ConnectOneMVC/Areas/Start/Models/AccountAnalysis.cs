using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Start.Models
{
    public class AccountAnalysis
    {
        public DataTable Catalog { get; set; }
        public DataTable MainTable { get; set; }
    }
}