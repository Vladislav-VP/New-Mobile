using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entities;

namespace TestProject.Services.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> UserExists(string userName);
    }
}
