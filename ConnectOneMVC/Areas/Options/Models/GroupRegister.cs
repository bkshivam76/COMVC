using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.ClientUserInfo;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Options.Models
{
    [Serializable]
    public class Model_GroupRegister_Window :AllRights
    {
        [Display(Name = "Group Name:")]
        [Required]
        [StringLength(255)]
        [RegularExpression("^[A-Za-z0-9 _]{1,255}$", ErrorMessage = "Only alphaNumeric Underscore (_) is allowed")]
        public string Grup_Register_GrupName { get; set; }

        [Display(Name = "Group Description:")]
        [StringLength(8000)]
        public string Grup_Register_GrupDescription { get; set; }

        public string ActionMethod { get; set; }

        public int ID { get; set; }
        public string PostSuccessFunction { get; set; }
        public string PopupName { get; set; }

        public List<Return_GetGroupRegister_MainTable> Grup_Register_GridData { get; set; }
    }
    [Serializable]
    public class Model_GroupMapping_Window
    {
        [Display(Name = "Group Name:")]
        [Required (ErrorMessage = "Select Group Name")]
        public int? GrupMapping_GrupName { get; set; }
                   
        public string[] MappedUser { get; set; }

        public string UgFLAG { get; set; }

        // public List<Return_GetRegister> GrupMap_Grid { get; set; }
    }
    [Serializable]
    public class Model_ManagPrivileges_Window
    {
        public string ManPri_UserID { get; set; }
        public string ManPri_Group { get; set; }

        public string PopupID { get; set; }
        public int? ManPri_GroupID { get; set; }
        public List<Common_Lib.DbOperations.ClientUserInfo.Return_GetPrivilegesRegister> GridData { get; set; }
        public int ManPriID { get; set; }
        public string UGFlag { get; set; }
    }
    [Serializable]
    public class Model_AdUserPrivileges_Window
    {
        [Display(Name = "User:")]
        public string AdUserPrivileges_UserID { get; set; }

        public string AdUserPrivileges_UserName { get; set; }
        
        [Display(Name = "Group:")]
        public int? AdUserPrivileges_GroupID { get; set; }

        public string AdUserPrivileges_GroupName { get; set; }        

        [Required]
        [Display(Name = "Entity/Screen:")]
        public int[] AdUserPrivileges_EntityScreenID { get; set; }

        [Required]
        [Display(Name = "Privileges Allowed:")]
        public string AdUserPrivileges_PrivilegesAllowed { get; set; }

        public int ID { get; set; }
        public string ActionMethod { get; set; }

        public string AdUserPrivileges_EntityScreenStr { get; set; } 
        public string PopupID { get; set; }

    }
}