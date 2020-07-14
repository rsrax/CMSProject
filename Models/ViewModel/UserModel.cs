using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using CMSProject.Models.DB;

namespace CMSProject.Models.ViewModel
{

    public class UserSignUpView
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "Username:")]
        [Required(ErrorMessage = "Username is required.")]
        [Remote("doesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists.")]
        public string Username { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Display(Name = "Gender:")]
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Display(Name = "Birthdate:")]
        [Required(ErrorMessage = "Birthdate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Display(Name = "E-Mail:")]
        [Required(ErrorMessage = "E-Mail is required.")]
        [DataType(DataType.EmailAddress)]
        [Remote("doesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "Email already exists.")]
        public string Email { get; set; }
    }

    public class UserLoginView
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please enter the Username!")]
        [Display(Name = "Username:")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Please enter the Password!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }

    public class UserProfileView
    {
        [Key]
        public int UserProfileID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Please enter a Username!")]
        //[Remote("doesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists.")]
        [Display(Name = "Username:")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a Password!")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter a First Name!")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a Last Name!")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please select a Gender!")]
        [Display(Name = "Gender:")]
        public string Gender { get; set; }

        [Display(Name = "Birthdate:")]
        [Required(ErrorMessage = "Birthdate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }

        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Display(Name = "E-Mail:")]
        [Required(ErrorMessage = "E-Mail is required.")]
        //[Remote("doesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "Email already exists.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class CustomerProfileView
    {
        [Key]
        public int UserProfileID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Please enter a Username!")]
        //[Remote("doesUserNameExist", "Account", HttpMethod = "POST", ErrorMessage = "User name already exists.")]
        [Display(Name = "Username:")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a Password!")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter a First Name!")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a Last Name!")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please select a Gender!")]
        [Display(Name = "Gender:")]
        public string Gender { get; set; }

        [Display(Name = "Birthdate:")]
        [Required(ErrorMessage = "Birthdate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }

        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Display(Name = "E-Mail:")]
        [Required(ErrorMessage = "E-Mail is required.")]
        //[Remote("doesEmailExist", "Account", HttpMethod = "POST", ErrorMessage = "Email already exists.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Active { get; set; }
    }

    public class Branches
    {
        [Key]
        public int BranchID { get; set; }
    }
}