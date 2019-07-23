using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services.Storages.Interfaces
{
    public interface ILocalStorage<T>
    {
        void Store(T obj);

        T Get();

        void Clear();
    }
}
