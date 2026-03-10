using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class Address_Book
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }//Redmine Bug #131387 fixed
        public string Occupation { get; set; }
        public string Education { get; set; }
        public string Passport_No { get; set; }
        public string PAN_No { get; set; }
        public string VAT_TIN_No { get; set; }
        public string GST_TIN_No { get; set; }
        public string CST_TIN_No { get; set; }
        public string TAN_No { get; set; }
        public string UID_No { get; set; }
        public string Service_Tax_Reg_No { get; set; }
        public string Voter_ID { get; set; }
        public string Ration_Card_No { get; set; }
        public string DL_No { get; set; }
        public string Taxpayer_ID { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public string Address_Line4 { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string Resi_Tel_No { get; set; }
        public string Office_Tel_No { get; set; }
        public string Office_Fax_No { get; set; }
        public string Resi_Fax_No { get; set; }
        public string Mobile_No { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public DateTime? Date_of_Birth_Lokik { get; set; }
        public string Blood_Group { get; set; }
        public string Status { get; set; }
        public DateTime? Date_of_Birth_Alokik { get; set; }
        public string BK_Title { get; set; }
        public string BK_PAD_No { get; set; }
        public string Class_At { get; set; }
        public string Centre_Category { get; set; }
        public string Centre_Name { get; set; }
        public string Category { get; set; }
        public string Referene { get; set; }
        public string Remarks { get; set; }
        public string Events { get; set; }
        public string Add_By { get; set; }
        public string Add_Date { get; set; }
        public string Edit_By { get; set; }
        public string Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public string Action_Date { get; set; }
        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public Int32? OTHER_ATTACH_CNT { get; set; }
        public Int32? ALL_ATTACH_CNT { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }

        //Added for Audit Icon Filter
        public Int32? VOUCHING_ACCEPTED_COUNT { get; set; }
        public Int32? VOUCHING_PENDING_COUNT { get; set; }
        public Int32? VOUCHING_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? VOUCHING_REJECTED_COUNT { get; set; }
        public Int32? VOUCHING_TOTAL_COUNT { get; set; }
        public Int32? AUDIT_PENDING_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? AUDIT_REJECTED_COUNT { get; set; }
        public Int32? AUDIT_TOTAL_COUNT { get; set; }
        public string iIcon { get; set; }
        //public bool IS_AUTOVOUCHING { get; set; }
    }
    [Serializable]
    public class Organization_INFO
    {
        public string C_ORG_NAME { get; set; }
        public string C_ORG_ID { get; set; }
    }
    [Serializable]
    public class Title_INFO
    {
        public string C_TITLE_NAME { get; set; }
        public string C_TITLE_ID { get; set; }
    }
    [Serializable]
    public class Country_INFO
    {
        public string R_CO_NAME { get; set; }
        public string R_CO_REC_ID { get; set; }
        public string R_CO_CODE { get; set; }
    }
    [Serializable]
    public class State_INFO
    {
        public string R_ST_REC_ID { get; set; }
        public string R_ST_NAME { get; set; }
        public string R_ST_CODE { get; set; }
    }
    [Serializable]
    public class District_INFO
    {
        public string R_DI_REC_ID { get; set; }
        public string R_DI_NAME { get; set; }
        public string R_DI_CODE { get; set; }
    }
    [Serializable]
    public class City_INFO
    {
        public string R_CI_REC_ID { get; set; }
        public string R_CI_NAME { get; set; }
        public string R_CI_CODE { get; set; }
    }
    [Serializable]
    public class Center_INFO
    {
        public string CEN_NAME { get; set; }
        public string CEN_BK_PAD_NO { get; set; }
        public string CEN_INCHARGE { get; set; }
        public string CEN_ZONE_ID { get; set; }
        public int? CEN_ID { get; set; }
    }
    [Serializable]
    public class CenterCategory_Info
    {
        public int? Index { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class Party_Info
    {
        public string Name { get; set; }
        public string BUILDING { get; set; }
        public string HOUSE_NO { get; set; }
        public string AREA_STREET { get; set; }
        public string DISTRICT { get; set; }
        public string MOBILE { get; set; }
        public string ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
    }
    public class ExcelUploadVariables
    {
        //public string Txt_ProfilePic_FileName { get; set; }
        //public string Txt_ProfilePic_Path { get; set; }
        //public HttpPostedFileBase ProfilePicFile { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public HttpPostedFileBase ExcelUpload_Address { get; set; }
        public HttpPostedFileBase ExcelUpload_Action_items { get; set; }
        public string table_name { get; set; }

    }
}