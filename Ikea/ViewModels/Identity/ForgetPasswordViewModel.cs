using System.ComponentModel.DataAnnotations;

namespace Ikea.PL.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;
    }
}
