using Microsoft.AspNetCore.Identity;

namespace HRMData.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual Employee Employee { get; set; } = null!;
    }
}
