using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Options.Models
{
    [Serializable]
    public class ChangePasswordModel
    {
        
        [Display(Name = "User: ")]
        public string User_Name { get; set; }


        [Display(Name = "Old Password: ")]
        [Required(ErrorMessage = "Old Password. Can not be blank...!")]
        public string Old_Password { get; set; }

        [Display(Name = "New Password: ")]
        [Required(ErrorMessage = "New Password. Can not be blank...!")]
        [StringLength(100, ErrorMessage = "New Password cannot be less than 8 characters...!",MinimumLength =8)]
        public string New_Password { get; set; }

        [Display(Name = "Confirm Password: ")]
        [Required(ErrorMessage = "Confirm Password. Can not be blank...!")]
        [Compare("New_Password",ErrorMessage = "New / Confirm Password not Matched...!")]
        public string Confirm_Password { get; set; }

    }
}