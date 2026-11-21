using Microsoft.AspNetCore.Identity;

namespace WebApi.NET.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
