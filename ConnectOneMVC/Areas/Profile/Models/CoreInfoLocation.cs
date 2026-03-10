using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class CoreInfoLocation
    {
        public string ID { get; set; }

        public string Location_Name {get;set;}
    public string Other_Detail {get;set;}
    public string System { get; set; }
public string Matched_Type { get; set; }
        public string Matched_Name { get; set; }
        public string Matched_Instt { get; set; }
        public string YEARID { get; set; }
        public string ORG_REC { get; set; }
        public string Add_By { get; set; }
        public DateTime Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime Action_Date { get; set; }
        public Int32 Capacity { get; set; }
        public string Ac_nonAc { get; set; }
        public string Category { get; set; }
        public string roomfloor { get; set; }
    }
}