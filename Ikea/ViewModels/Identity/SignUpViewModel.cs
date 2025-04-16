
using System.ComponentModel.DataAnnotations;



namespace Ikea.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is Required")]
        public string LName { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
       
        public string Password { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password doesn't match with Password")]
        
        public string ConfirmPassword { get; set; } = string.Empty;
        
        public bool IsAgree { get; set; }

    }
}
