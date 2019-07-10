using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task CreateDatabase();
    }
}
