using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Entity;

namespace TestProject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUserByName(string userName);
    }
}
