using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IStorageHelper<T>
    {
        Task Save(T obj);

        Task<T> Load();

        void Clear();
    }
}
