using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common_Lib;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class PersonnelMaster
    {

        public string Name { get; set; }
        public string Gender { get; set; }

        public string Personnel_Type { get; set; }
        public string Skill_Type { get; set; }
        public string Aadhaar_No { get; set; }
        public string PAN_No { get; set; }
        public DateTime DOB { get; set; }
        public string PF_No { get; set; }
        public int  Dept_Name { get; set; }
        public int Contractor_Name { get; set; }
        public string Payment_Mode { get; set; }
        public DateTime Joining_Date { get; set; }
        public DateTime Leaving_Date { get; set; }
        public string Contact_No { get; set; }
        public string Other_Details { get; set; }
        public DateTime REC_ADD_ON { get; set; }
        public string REC_ADD_BY { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
        public string REC_EDIT_BY { get; set; }
        public int REC_ID { get; set; }

        public string Designation { get; set; }

       
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        public string TempActionMethod { get; set; }
    }
}