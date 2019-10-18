using TestProject.API.Context;
using TestProject.API.Entities;
using TestProject.API.Repositories.Interfaces;

namespace TestProject.API.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TodoListContext context) : base(context)
        {
        }
    }
}
