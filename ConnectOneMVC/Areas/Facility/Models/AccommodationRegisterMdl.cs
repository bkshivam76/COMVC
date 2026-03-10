using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    public class AccommodationRegisterMdl
    {

    }

    [Serializable]
    public class printparams
    {
        public string reg_No { get; set; }
        public string EventName { get; set; }
        public string PersonName { get; set; }
        public string Place { get; set; }
        public DateTime? arrivalDt { get; set; }
        public DateTime? DepartureDt { get; set; }
        public string NoOfPersons { get; set; }
        public string Building { get; set; }
        public string RoomNo { get; set; }
        public string NoofPerson2 { get; set; }
        public string slipIssuedBy { get; set; }
        public string centerName { get; set; }
        public string printOption { get; set; }
        public int slipCount { get; set; }
        public string maleCount { get; set; }
        public string femaleCount { get; set; }
        public DataTable dt_SelectedSlipsData { get; set; }
        public List<names_respidsList> namesAndrespIdsList { get; set; }
        public string selectedRespID {get; set;}
        public string PrintFromscreen { get; set; }
    }

    [Serializable]
    public class AccommodationEditedData
    {
        public CurrentAllottmentData data { get; set; }
        public string key { get; set; }
        public string type { get; set; }
        public OldData OldData { get; set; }
    }
    [Serializable]
    public class CurrentAllottmentData_Short
    {
        public string CURRENT_ALLOTMENT { get; set; }
    }

    [Serializable]
    public class AccommodationEditedData_Short
    {
        public CurrentAllottmentData_Short data { get; set; }
        public string key { get; set; }
        public string type { get; set; }
        public AccomShortSelectedRoomsData OldData { get; set; }
    }
    [Serializable]
    public class AccomShortSelectedRoomsData
    {
        public string AL_LOC_NAME { get; set; }
        public string SP_SERVICE_PLACE_NAME { get; set; }
        public string REC_ID { get; set; }
        public string SP_REC_ID { get; set; }
        public Int32 AL_MAXCAPACITY { get; set; }        
    }
    [Serializable]
    public class AccommodationOriginalData
    {
        public OldData originalDataMdl { get; set; }
    }
    [Serializable]
    public class CurrentAllottmentData
    {
        public int CurrentAllottment { get; set; }
    }
    [Serializable]
    public class OldData
    {
        public string Building { get; set; }
        public string Room { get; set; }
        public string REC_ID { get; set; }
        public Int32 Allowed { get; set; }        
        public Int32 Allotted { get; set; }        
        public string CurrentAllottment { get; set; }        
    }
    public class names_respidsList
    {
        public string names { get; set; }
        public string respids { get; set; }
    }
} 