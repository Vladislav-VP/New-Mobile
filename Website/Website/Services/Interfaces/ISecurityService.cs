using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISecurityService
    {
        Task<object> GenerateJwtToken(string email, IdentityUser user);
    }
}
