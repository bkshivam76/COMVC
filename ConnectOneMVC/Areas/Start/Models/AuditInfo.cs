using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Start.Models
{
    public class AuditInfo
    {
        public DataTable grdGradings { get; set; }
        public DataTable grdAuditHistory { get; set; }
        public DataTable grdAllocations { get; set; }
    }
}