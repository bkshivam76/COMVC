using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Models
{
    [Serializable]
    public class KarunaSankalpChartModel
    {
        public string ParticipantName { get; set; }
        public string Participant_AB_ID { get; set; }
        public string ChartSrNo { get; set; }
        public string ChartFrom { get; set; }
        public string ChartTo { get; set; }
        public string BenevolentPledge { get; set; }
        public string EquanimityPledge { get; set; }
        public string PeacePledge { get; set; }
        public string HappinessPledge { get; set; }
        public string RespectPledge { get; set; }
        public string BrotherhoodPledge { get; set; }  
        public string href_Yes { get; set; }  
        public string href_No { get; set; }  
        public string ExceptionMessage { get; set; }  
        public string UserResponse { get; set; } 
        public string id { get; set; }

    }
}