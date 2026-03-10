using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Payment_Frm_Vehicles_Window_Model
    {
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        [Display(Name = "Item Name:")]
        public string BE_ItemName { get; set; }
        public bool? Chk_Affidavit { get; set; }
        public bool? Chk_FU_Letter { get; set; }
        public bool? Chk_OtherDoc { get; set; }
        public bool? Chk_RC_Book { get; set; }
        public bool? Chk_Trf_Letter { get; set; }
        public bool? Chk_Will { get; set; }

        [Display(Name = "Make / Company:")]
        public string Cmd_Make { get; set; }
        [Display(Name = "Model:")]
        public string Cmd_Model { get; set; }
        [Required(ErrorMessage = "Cannot Be Empty..")]
        public string Cmd_Ownership { get; set; }
        [Display(Name = "Insurance Company Name:")]
        public string InsList { get; set; }
        [Display(Name = "Location:")]
        [Required(ErrorMessage = "Location required.")]
        public string LocList { get; set; }
        [Required(ErrorMessage = "Owner/Incharge Name required.")]
        public string OwnList { get; set; }
        [Required(ErrorMessage = "Registration Pattern required.")]
        public string RAD_RegPattern { get; set; }
        public string TextEdit1 { get; set; }
        [Display(Name = "Expiry Date:")]
        public DateTime? Txt_E_Date { get; set; }
        public string Txt_OtherDoc { get; set; }
        [Display(Name = "Other Details:")]
        public string Txt_Others { get; set; }
        [Display(Name = "Policy No:")]
        public string Txt_PolicyNo { get; set; }
        public DateTime? Txt_RegDate { get; set; }
        public string Txt_RegNo { get; set; }
        public string Tr_M_ID { get; set; }
        public List<Return_GetInsList> InsuranceListData { get; set; }
        public List<Return_LocationList> LocationListData { get; set; }
        public List<Return_Vehicles_OwnerList> OwnerListData { get; set; }
        public List<Return_Vehicles_MakeList> MakeListData { get; set; }
        public List<Return_Vehicles_ModelList> ModelListData { get; set; }

    }
    [Serializable]
    public class PaymentVoucher_Frm_Vehicles_Window_AssetLocations_Model
    {
        public string Location_Name { get; set; }
        public string AL_ID { get; set; }
        public DateTime REC_EDIT_ON { get; set; }
        public string Matched_Type { get; set; }
        public string Matched_Name { get; set; }
        public string Matched_Instt { get; set; }
        public decimal? Final_Amount { get; set; }
    }
}