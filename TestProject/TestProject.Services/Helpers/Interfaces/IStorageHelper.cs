using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IStorageHelper<T>
    {
        Task Save(int id);

        Task<T> Retrieve();

        void Clear();
    }
}
