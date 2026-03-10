using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    public class RoomsVisualization
    {
    }
    [Serializable]
    public class RoomsVisualizationData
    {        
        public Int32 AL_CEN_ID { get; set; }
        public string AL_LOC_MAIN { get; set; }
        public string AL_LOC_NAME { get; set; }
        public Int16 AL_MAXCAPACITY { get; set; }
        public string AL_AC_OR_NON_AC { get; set; }
        public string AL_CATEGORY { get; set; }
        public string SP_SERVICE_PLACE_NAME { get; set; }
        public string SP_SERVICE_PLACE_TYPE { get; set; }
        public string SP_REC_ID { get; set; }
        public string ALI_REC_ID { get; set; }        
    }
}