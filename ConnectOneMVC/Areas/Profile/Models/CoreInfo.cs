using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class CoreInfo
    {
        public string xCen_Name { get; set; }
        public string xCen_Pad_No { get; set; }
        public string xCen_UID { get; set; }
        public string xCen_AccType { get; set; }
        public string xCen_SubZone { get; set; }
        public string xCen_Zone { get; set; }
        public string xCen_Incharge { get; set; }
        public string xCen_In_Padno { get; set; }
        public string xCen_DOS { get; set; }
        public string xCen_Res_Person { get; set; }
        public string xCen_Res_TelNo { get; set; }
        public string Support_Edit_On { get; set; }
        public byte[] ImgCentre { get; set; }
        public byte[] ImgIncharge { get; set; }
        //contact detail
        public string xCen_B_Name { get; set; }
        public string xCen_Add1 { get; set; }
        public string xCen_Add2 { get; set; }
        public string xCen_Add3 { get; set; }
        public string xCen_Add4 { get; set; }
        public string xCen_City { get; set; }
        public string xCen_Pin { get; set; }
        public string xCen_District { get; set; }
        public string xCen_State { get; set; }
        public string xCen_Country { get; set; }
        public string xCen_Tel { get; set; }
        public string xCen_Fax { get; set; }
        public string xCen_Mob { get; set; }
        public string xCen_Email { get; set; }
        public string xCen_Website { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
        public List<CoreInfoInstitute> CoreInfoInstitute { get; set; }
        public List<CoreInfoLocation> CoreInfoLocation { get; set; }
        public DateTime LastEditedOn { get; set; }
    }
    [Serializable]
    public class LookUp_GetPersonsList_Info
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
    }
}