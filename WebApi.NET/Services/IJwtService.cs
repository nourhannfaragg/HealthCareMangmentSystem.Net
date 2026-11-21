using WebApi.NET.Models;

namespace WebApi.NET.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
