using Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        RefreshToken GetToken(string token);
    }
}
