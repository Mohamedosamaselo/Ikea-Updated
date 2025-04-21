using System.ComponentModel.DataAnnotations;

namespace Ikea.PL.ViewModels.Identity
{
    public class SignInViewModel
    {
        [Required(ErrorMessage ="Email Is Required ")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = "";
        //[MinLength(5)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = "";
        public bool RememberMe { get; set; }
    }
}
