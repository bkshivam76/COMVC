using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using static Common_Lib.DbOperations.ClientUserInfo;
using ConnectOneMVC.Models;

namespace ConnectOneMVC.Areas.Options.Models
{
    [Serializable]
    public class Model_UserRegisterWindow : AllRights
    {
      
        [Display(Name = "Personnel Name :")]        
        public string clientpersonnelName { get; set; }

        public int? clientpersonnelID { get; set; }

        [Display(Name = "Sewa Department :")]
        
        public string clientsewaDepartment { get; set; }


        [Display(Name = "Username :")]
        [Required(ErrorMessage = "Username cannot be blank...!")]
        [StringLength(255)]
        [RegularExpression("^[A-Za-z0-9_-]{1,255}$", ErrorMessage = "Only alphaNumeric, hyphen and underscore without Spaces are allowed")]        
        public string  clientUsername { get; set; }

        [Display(Name = "Mobile No :")]
        public string clientMobileNo { get; set; }

        [Display(Name = "Email ID :")]
        public string clientEmailID { get; set; }

      
        [Display(Name = "Groups Mapped :")]
        public string clientselectGroups { get; set; }

        public int[] clientGroupID { get; set; }

        public string selectGroups { get; set; }

        //[Display(Name = "IsAdministrator :")]
        [Required(ErrorMessage = "is administrator cannot be blank...!")]
        public bool? clientIsAdminstrator { get; set; }
        public bool? clientIsSelfPostedOnly { get; set; }
        public string REC_ID { get; set; }
        public string ActionMethod { get; set; }

        public List<Return_GetRegister> Grid_Data { get; set; }


        //Password and Confirm password is added for User Creation Screen
        [Display(Name = "Password : ")]
        [Required(ErrorMessage = "Password cannot be blank...!")]
        //[StringLength(30, ErrorMessage = "Password cannot be less than 8 characters...!", MinimumLength = 8)]
        public string Password_UserRegister { get; set; }

        [Display(Name = "Confirm Password : ")]
        //[Required(ErrorMessage = "Confirm Password. Cannot be blank...!")]
        [Compare("Password_UserRegister", ErrorMessage = "Password and Confirm Password are not Matching..!")]
        public string Confirm_Password_UserRegister { get; set; }

    }
}