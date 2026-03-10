using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    public class ViewFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public double Size { get; set; }
        public string CreationDate { get; set; }
        public string ModifyDate { get; set; }
    }
}