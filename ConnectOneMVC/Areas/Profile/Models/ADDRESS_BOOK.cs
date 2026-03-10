using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Profile.Models
{
    [Serializable]
    public class EditorsViewModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Do not use digits in the Name.")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "Name must have at least 2 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "'Password' and 'Confirm Password' do not match.")]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^\+\s*1\s*\(\s*[02-9]\d{2}\)\s*\d{3}\s*-\s*\d{4}$", ErrorMessage = "The phone must have a correct USA phone format")]
        public string Phone { get; set; }

        public string Extension { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string Description { get; set; }

        public int Age { get; set; }

        public string Drink { get; set; }

        [Required(ErrorMessage = "City is required")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Do not use digits in the City name.")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "City must have at least 2 symbols")]
        public string City { get; set; }

        public IEnumerable<string> Colors { get; set; }

        public IEnumerable<string> SelectedColors { get; set; }

        public string Color { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "You must agree to the Terms and Conditions")]
        public bool Accepted { get; set; }

    }
    [Serializable]
    public class ADDRESS_BOOK
    {
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Status { get; set; }
        public string ID { get; set; }
        public DateTime? REC_EDIT_ON { get; set; }
    }
    [Serializable]
    public class InsMISC_INFO
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }
}