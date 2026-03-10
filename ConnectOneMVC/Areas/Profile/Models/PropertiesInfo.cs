using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class PropertiesInfo
    {
        public string Category { get; set; }
        public string LB_PRO_NAME { get; set; }
        public string Address { get; set; } 
        public string Use_of_Property { get; set; }
        public string Ownership { get; set; }
        public string Owner { get; set; }
        public string LB_SURVEY_NO { get; set; }
        public decimal LB_TOT_P_AREA { get; set; }
        public string Construction_Year { get; set; }
        public string RCC_Roof { get; set; }
        public decimal Deposit_Amount { get; set; }
        public DateTime? Paid_Date { get; set; }
        public decimal Monthly_Rent { get; set; }
        public decimal Other_Monthly_Payments { get; set; }
        public DateTime? Period_From { get; set; }
        public DateTime? Period_To { get; set; }
        public string Other_Detail { get; set; }
        public string Entry_Type { get; set; }
        public decimal LB_CON_AREA { get; set; }
        public string YearID { get; set; }
        public string Type { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
        public string ID { get; set; }
        public string TR_ID { get; set; }
        public string RemarkStatus { get; set; }
        public string Crossed_Time_Limit { get; set; }
        public string Open_Actions { get; set; }
        public decimal Opening_Value { get; set; }
        public decimal Curr_Value { get; set; }
        public string Sale_Status { get; set; }
        public int? RemarkCount { get; set; }
        public bool? Remarks { get; set; }
        public int? REQ_ATTACH_COUNT { get; set; }
        public int? COMPLETE_ATTACH_COUNT { get; set; }
        public int? RESPONDED_COUNT { get; set; }
        public int? REJECTED_COUNT { get; set; }
        public int? OTHER_ATTACH_CNT { get; set; }
        public int? ALL_ATTACH_CNT { get; set; }
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
        public string Special_Ref { get; set; }
        public string TYPE_CHANGE_LOG { get; set; }

    }
    [Serializable]
    public class PropertyWindowProfile
    {
        public string TitleX { get; set; }
        public string SubTitleX { get; set; }      
        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string YearID { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }   
        public string TempActionMethod { get; set; }
        public string LB_TR_ID { get; set; }
        public bool Txt_LB_Amount_ReadOnly { get; set; }      
        public DateTime? Owner_Rec_Edit_On { get; set; }
        public string X_ITEM_ID { get; set; }
        public string xID { get; set; }
        public bool MOU_Property_Select { get; set; }


        [Required(ErrorMessage = "Please mention Property Category")]
        public string Cmd_PCategory_LBW { get; set; }
        [Required(ErrorMessage = "Please mention Property Type")]
        public string Cmd_PType_LBW { get; set; }
        [Required(ErrorMessage = "Please mention Property Use")]
        public string Cmd_PUse_LBW { get; set; }       
        [Required(ErrorMessage = "Please mention Property Name")]
        public string Txt_PName_LBW { get; set; }
        public string Txt_Address_LBW { get; set; }
        public string Txt_Add_LBW { get; set; }
        [Required(ErrorMessage = "Please mention Ownership")]
        public string Cmd_Ownership_LBW { get; set; }   
        public string Look_OwnList_LBW { get; set; }
        public string TextEdit1_LBW { get; set; }         
        public string Txt_SNo_LBW { get; set; }
        [Display(Name = "Total Plot Area (Sq.Ft):")]
        //[RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Tot_Area_LBW { get; set; }
        [Display(Name = "Constructed Area (Sq.Ft):")]
        //[RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Con_Area_LBW { get; set; }
        public string Cmd_Con_Year_LBW { get; set; }
        public string Cmd_RccType_LBW { get; set; }
        public bool Chk_OtherDoc_LBW { get; set; }
        public string Txt_OtherDoc_LBW { get; set; }
        public string Txt_Remarks_LBW { get; set; }
        public double? Txt_Dep_Amt_LBW { get; set; }
        public double? Txt_Mon_Rent_LBW { get; set; }
        public double? Txt_LB_Amount_LBW { get; set; }
        public double? Txt_Other_Payments_LBW{ get; set; }     
        public DateTime? Txt_PaidDate_LBW { get; set; }
        public DateTime? Txt_F_Date_LBW { get; set; }
        public DateTime? Txt_T_Date_LBW { get; set; }
        public List<Documents> Document { get; set; } 
    
      
       
        public string Txt_R_Add1_LBW { get; set; }
        public string Txt_R_Add2_LBW { get; set; }
        public string Txt_R_Add3_LBW { get; set; }
        public string Txt_R_Add4_LBW { get; set; }
        public string GLookUp_CityList_LBW { get; set; }
        public string GLookUp_DistrictList_LBW { get; set; }
        public string GLookUp_StateList_LBW { get; set; }
        public string Txt_R_Pincode_LBW { get; set; }
        public bool isCallForPropertyTypeChange { get; set; }
        public string propertyType_PreviousValue { get; set; }
        public string LB_ORG_REC_ID { get; set; }

    }
    [Serializable]
    public class Documents
    {
        public string ID { get; set; }

        public bool? Selected { get; set; }
    }
    
    [Serializable]
    public class Documents_List_Info
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }
    [Serializable]
    public class Cmd_Con_Year_Bind_Info
    {
        public string Index { get; set; }
        public string Value { get; set; }
    }
    [Serializable]
    public class Property_Window_Grid
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
        public int? Sr_LBW { get; set; }
        [Required(ErrorMessage = "Institution Is Required")]
        public string Look_InsList_LBW { get; set; }

        [Display(Name = "Total Plot Area (Sq.Ft):")]
        //[RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Ext_Tot_Area_LBW { get; set; }
        [Display(Name = "Constructed Area (Sq.Ft.):")]
       //[RegularExpression(@"\d[0-9]+", ErrorMessage = "Only numerics are allowed")]
        public double? Txt_Ext_Con_Area_LBW { get; set; }
        [Display(Name = "Construction Year:")]        
        public int? Cmd_Ext_Con_Year_LBW { get; set; }
        [Display(Name = "M.O.U. Date:")]
        [Required(ErrorMessage = "M.O.U Date Is Required")]
        public DateTime? Txt_MOU_Date_LBW { get; set; }
        public string Txt_Others_LBW { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }      
        public double? Txt_LB_Ext_Amount_LBW { get; set; }
        public string Institution { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
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