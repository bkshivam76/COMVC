using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    [Serializable]
    public class ActionItemsInfo
    {
       
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; }

        public string Auditor { get; set; } 

        public string Title { get; set; }
        public string Description { get; set; }
        public string Due_On { get; set; }
        public string Centre_Remarks { get; set; }
        public string Close_Remarks { get; set; }
        public string Closed_On { get; set; }
        public string Closed_By { get; set; }
        public string ID { get; set; }
        public int CrossedTimeLimit { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public Int32? OTHER_ATTACH_CNT { get; set; }
        public Int32? ALL_ATTACH_CNT { get; set; }

        //Added for Audit Icon Filter        
        public string iIcon { get; set; }

    }
    [Serializable]
    public class ActionItems_GetTitleList_INFO
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}