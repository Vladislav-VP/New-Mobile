using System.Linq;

using DataAccess.Context;
using DataAccess.Repositories.Interfaces;
using Entities;

namespace DataAccess.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(TodoListContext context) : base(context)
        {
        }

        public RefreshToken GetToken(string token)
        {
            RefreshToken refreshToken = _dbSet.SingleOrDefault(t => t.Token == token);
            return refreshToken;
        }
    }
}
