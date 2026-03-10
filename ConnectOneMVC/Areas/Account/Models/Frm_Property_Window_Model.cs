using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Frm_Property_Window_Model
    {
        public string xID { get; set; }
        public DateTime? Txt_F_Date { get; set; }
        public DateTime? Txt_T_Date { get; set; }   
        public DateTime? Txt_PaidDate { get; set; }    
        public string TextEdit1 { get; set; }
        public string Cmd_Con_Year { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string ITEM_ID { get; set; }
        [Required(ErrorMessage = "Please mention Property Type")]
        public string Cmd_PType { get; set; }
        [Required(ErrorMessage = "Please mention Property Category")]
        public string Cmd_PCategory { get; set; }
        public string Cmd_PUse_Text { get; set; }
        [Required(ErrorMessage = "Please mention Property Use")]
        public string Cmd_PUse { get; set; }
        [Required(ErrorMessage = "Please mention Property Name")]
        public string Txt_PName { get; set; }
        public string Txt_Address { get; set; }
     
        public string Txt_Add { get; set; }
    
        public string Txt_R_Add1 { get; set; }
        public string Txt_R_Add2 { get; set; }
        public string Txt_R_Add3 { get; set; }
        public string Txt_R_Add4 { get; set; }
        public string Txt_R_Pincode { get; set; }
    
        public string GLookUp_CityList { get; set; }
    
        public string GLookUp_DistrictList { get; set; }
     
        public string GLookUp_StateList { get; set; }
        [Required(ErrorMessage = "Please mention Ownership")]
        public string Cmd_Ownership { get; set; }        
        public string Look_OwnList { get; set; }
        public string Txt_SNo { get; set; }
        [Display(Name = "Total Plot Area (Sq.Ft):")]
        [RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Tot_Area { get; set; }
        [Display(Name = "Constructed Area (Sq.Ft):")]
        //[RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Con_Area { get; set; }
        public string Cmd_RccType { get; set; }        
        public double? Txt_Dep_Amt { get; set; }       
        public double? Txt_Mon_Rent { get; set; }       
        public double? Txt_Other_Payments { get; set; }       
        public bool Chk_OtherDoc { get; set; }
        public string Txt_OtherDoc { get; set; }        
        public string Txt_Remarks_Pw { get; set; }
        public List<Documents> Document { get; set; }
        public bool IsGift { get; set; }
        public bool IsJV { get; set; }     
        public DataTable LB_EXTENDED_PROPERTY_TABLE { get; set; }
        public DataTable LB_DOCS_ARRAY { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethodPropertyWindow { get; set; }
        public List<Cmd_Con_Year_Bind_Info> PuseListData { get; set; }
        public List<Return_LB_Documents> PPDocumentList { get; set; }
        public List<string> Cmd_Con_Year_Bind { get; set; }
        public List<Return_LB_Owners> OwnerListData { get; set; }
        public List<Return_StateList> StateListData { get; set; }
        public List<Return_DistrictList> DistrictListData { get; set; }
        public List<Return_CityList> CityListData { get; set; }
    }
    [Serializable]
    public class Documents
    {
        public string ID { get; set; }
        public bool? Selected { get; set; }
    }
    [Serializable]
    public class LookUp_GetOwnList_Info
    {
        public String Name { get; set; }
        public String Organization { get; set; }
        public String Status { get; set; }
        public String ID { get; set; }
    }
    [Serializable]
    public class Documents_List_Info
    {
        public String Name { get; set; }
        public String ID { get; set; }
    }
    [Serializable]
    public class Cmd_Con_Year_Bind_Info
    {
        public string Index { get; set; }
        public string Value { get; set; }
    }
    [Serializable]
    public class Property_Window_ExtendedProperty_Grid
    {
        public int? Sr { get; set; }
        public string Institution { get; set; }
        public string Ins_ID { get; set; }
        public double? Total_Plot_Area { get; set; }
        public double? Constructed_Area { get; set; }
        public string Construction_Year { get; set; }
        public string M_O_U_Date { get; set; }
        public double? Value { get; set; }
        public string Other_Detail { get; set; }
    }
    [Serializable]
    public class Frm_Property_Window_Ext_Model
    {
        public int? Sr { get; set; }
        [Required(ErrorMessage ="Institute Is Required")]
        public string Look_InsList { get; set; }

        [Display(Name = "Total Plot Area (Sq.Ft):")]
        //[RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Ext_Tot_Area { get; set; }
        [Display(Name = "Constructed Area (Sq.Ft.):")]
        //[RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Ext_Con_Area { get; set; }
        [Display(Name = "Construction Year:")]     
        public int? Cmd_Ext_Con_Year { get; set; }
        [Display(Name = "M.O.U. Date:")]
        [Required(ErrorMessage ="M.O.U Date Is Required")]
        public DateTime? Txt_MOU_Date { get; set; }
        public string Txt_Others { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public double? xAmt { get; set; }
        public double? Txt_Ext_Pro_Opening_Value { get; set; }
        public string Institution { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public List<int> Cmd_Ext_Con_Year_Bind { get; set; }
        public List<Return_LB_Institution> InstituteListData { get; set; }
    }
    [Serializable]
    public class LookUp_GetInsList_Info
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Short_Name { get; set; }
    }
    [Serializable]
    public class LB_EXTENDED_PROPERTY_TABLE_List
    {
        public string LB_MOU_DATE { get; set; }
        public string LB_SR_NO { get; set; }
        public string LB_INS_ID { get; set; }
        public string LB_TOT_P_AREA { get; set; }
        public string LB_CON_AREA { get; set; }
        public string LB_CON_YEAR { get; set; }
        public string LB_VALUE { get; set; }
        public string LB_OTHER_DETAIL { get; set; }
        public string LB_REC_ID { get; set; }

    }
    [Serializable]
    public class LB_DOCS_ARRAY_List
    {
        public string LB_MISC_ID { get; set; }
        public string LB_REC_ID { get; set; }
    }
}