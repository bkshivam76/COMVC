using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Frm_Live_Stock_Window_Look_LocList
    {
        public string Location_Name { get; set; }
        public string AL_ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
        public string Matched_Type { get; set; }
        public string Matched_Name { get; set; }
        public string Matched_Instt { get; set; }
        public decimal? Final_Amount { get; set; }
    }
    [Serializable]
    public class Frm_Live_Stock_Window_Model
    {
        public string Tr_M_ID { get; set; }
        [Display(Name = "Item Name:")]
        public string BE_ItemName { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        [Display(Name = "Livestock Name / Tag:")]
        [Required(ErrorMessage = "Name Can not be blank...!")]
        public string Txt_Name { get; set; }     
        [Display(Name = "Other Details:")]
        public string Txt_Others { get; set; }      
        [Display(Name = "Birth Year:")]
        public string Cmd_Year { get; set; }
        [Display(Name = "Expiry Date:")]
        public DateTime? Txt_I_Date { get; set; }      
        [Display(Name = "Insurance Amount:")]
        public double? Txt_InsAmt { get; set; }
        [Display(Name = "Policy No.:")]
        public string Txt_PolicyNo { get; set; }
        [Display(Name = "Insurance Company Name:")]
        public string Look_InsList { get; set; }
        public string Rad_Insurance { get; set; }
        [Display(Name = "Location:")]
        [Required(ErrorMessage = "Location Not Selected...!")]
        public string Look_LocList { get; set; }
        public List<Return_LocationList> LocationListData{ get; set; }    
        public List<Return_GetInsList> InsuranceListData{ get; set; }
        public List<string> BirthYearData{ get; set; }
    }
}