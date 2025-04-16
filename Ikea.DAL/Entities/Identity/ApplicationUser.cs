using Microsoft.AspNetCore.Identity;

namespace Ikea.DAL.Entities.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public bool IsAgree { get; set; }
    }
}
