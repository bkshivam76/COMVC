using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    public class PartyMerge
    {
        public string Merged_Party_ID_AB { get; set; }
        public string Merged_Party_AB { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public string GLookUp_Party_AB { get; set; }
        public string GLookUp_Party_Name { get; set; }
        public bool ReplicateParty_PartyMerge { get; set; }
    }
}