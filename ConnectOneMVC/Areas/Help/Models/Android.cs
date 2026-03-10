using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    public class SparcHomePAge
    {
  public DataRow[] NonActiveUser { get; set; }
  public DataRow[] ActiveUser { get; set; }
  public DataTable Projects { get; set; }
    }
    public class C1HomePAge
    {
        public DataRow[] NonActiveUser { get; set; }
        public DataRow[] ActiveUser { get; set; }
        public DataTable Projects { get; set; }
    }
}