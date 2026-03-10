using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    public class AddressBookSmall
    {
        public string Titlex_ABSmall { get; set; }
        public string Lbl_ContactName_ABSmall { get; set; }
        public string Txt_Name_ABSmall { get; set; }
        public string Txt_R_Add1_ABSmall { get; set; }
        public string Txt_R_Add2_ABSmall { get; set; }
        public string Txt_R_Add3_ABSmall { get; set; }
        public string Txt_R_Add4_ABSmall { get; set; }
        public string GLookUp_RCountryList_ABSmall { get; set; }
        public string GLookUp_RStateList_ABSmall { get; set; }
        public string Rad_city_OthCity_ABSmall { get; set; }
        public string PC_City_Name_ABSmall { get; set; }
        public string PC_City_Name_Text_ABSmall { get; set; }
        public string Txt_R_Pincode_ABSmall { get; set; }
        public string Txt_R_Other_City_ABSmall { get; set; }
        public string GLookUp_RDistrictList_ABSmall { get; set; }
        public string Txt_R_Tel_1_ABSmall { get; set; }
        public string Txt_Mob_1_ABSmall { get; set; }
        public string Txt_Email1_ABSmall { get; set; }
        public string Txt_Remark1_ABSmall { get; set; }
        public string Txt_Remark2_ABSmall { get; set; }
        public string xID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public string NewFunctionName { get; set; }
        public string DropDownFunctionName { get; set; }
        public string Party_DataGridID { get; set; }
        public string PopUpId { get; set; }
        public string PostSuccessFunction { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string Org_RecID { get; set; }
        public bool New_Confirm { get; set; }
        public bool Replicate_Confirm { get; set; }
        public bool Edit_Confirm { get; set; }
    }
}