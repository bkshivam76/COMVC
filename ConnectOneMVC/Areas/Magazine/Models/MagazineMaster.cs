using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ConnectOneMVC.Areas.Magazine.Models
{
    [Serializable]
    public class MagazineMaster
    {
        public List<MagazineList> showMagazineList{ get; set; }
        public List<MagazineSubType> showMagazineSubType { get; set; }
        public List<MagazineSubFees> showMagazineSubFees { get; set; }
        public List<MagazineIssues> showMagazineIssues { get; set; }
    }
    [Serializable]
    public class MagazineList
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name Can not be blank...!")]
        public string Name { get; set; }
        [Display(Name = "Short Name : ")]
        [Required(ErrorMessage = "Short Name Can not be blank...!")]
        public string ShortName { get; set; }
        [Display(Name = "Language : ")]
        [Required(ErrorMessage = "Language not Selected...!")]
        public string Language { get; set; }

        [Display(Name = "Publish On : ")]
        [Required(ErrorMessage = "Publish On not Selected...!")]
        public string PublishOn { get; set; }
        [Display(Name = "Magazine Regd. : ")]
        public string MagazineRegd { get; set; }
        [Display(Name = "Postal Regd. No. : ")]
        public string PostalRegdNo { get; set; }

        [Display(Name = "Membership Start No.")]
        public int MembershipStart { get; set; }
        [Display(Name = "Foreign Subscriptions")]
        public string Foreign { get; set; }
        public string ID { get; set; }
        public string LangName { get; set; }
        public string LangID { get; set; }
        public string PublishName { get; set; }
        public string PublishID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int status_Action { get; set; }

        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
    }
    [Serializable]
    public class MagazineSubType
    {
        [Display(Name = "Sr.")]
        public string Sr { get; set; }
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Name Can not be blank...!")]
        public string Type { get; set; }
        [Display(Name = "Short Name")]
        [Required(ErrorMessage = "Short Name Can not be blank...!")]
        public string ShortNameSub { get; set; }
        [Display(Name = "Start Month")]
        [Required(ErrorMessage = "Start Month not Selected...!")]
        public string StartMonth { get; set; }
        [Display(Name = "St_Month")]
        public string St_Month { get; set; }
       // [Display(Name = "Min.Months")]
        public int MinMonths { get; set; }
        [Display(Name = "Fixed Period")]
        public string FixedPeriod { get; set; }
        [Display(Name = "Period wise Fee")]
        public string PeriodwiseFee { get; set; }      
        public string SubTypeID { get; set; }
        public string MagListID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int status_Action { get; set; }


        public bool Default { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
    }
    [Serializable]
    public class MagazineSubFees
    {
        [Required(ErrorMessage = "D a t e  Can not   B l a n k . . . !")]
        [Display(Name = "Effective Date")]
        public string EffectiveDate { get; set; }
        [Display(Name = "Indian Fee")]
        public decimal IndianFee { get; set; }
        [Display(Name = "Foreign Fee")]
        public decimal ForeignFee { get; set; }      
        public string SubFeesID { get; set; }
        public string MagSubTypeID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int status_Action { get; set; }


        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
    }
    [Serializable]
    public class MagazineIssues
    {      
        [Display(Name = "Magazine")]
        public string Magazine { get; set; }
        [Required(ErrorMessage = "Issue D a t e  Can not   B l a n k . . . !")]
        [Display(Name = "Issue Date")]
        public string IssueDate { get; set; }
        [Display(Name = "Part No")]
        public int PartNo { get; set; }
        [Display(Name = "Vol.No.")]
        public int VolNo { get; set; }
        [Display(Name = "Issue.No.")]
        public int IssueNo { get; set; }
        [Display(Name = "Reg Seed")]
        public int RegSeed { get; set; }
        [Display(Name = "Per Copy Weight")]
        public decimal PerCopyWeight { get; set; }
        [Display(Name = "Bundle Max Fgn")]
        public decimal BundleMaxFgn { get; set; }
        [Display(Name = "Bundle Max")]
        public decimal BundleMax { get; set; }
        [Display(Name = "Per Copy Weight1")]
        public decimal PerCopyWeight1 { get; set; }
        [Display(Name = "Copies for Auto")]
        public int CopiesforAuto { get; set; }
        [Display(Name = "Copies for Fgn")]
        public int CopiesforFgn { get; set; }
        [Display(Name = "Reg Exp")]
        public decimal RegExp { get; set; }
        [Display(Name = "Reg Fgn Exp")]
        public decimal RegFgnExp { get; set; }
        public string IssueID { get; set; }
        public string MagListID { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public string TempActionMethod { get; set; }
        public int status_Action { get; set; }
        public bool Default { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
    }
}