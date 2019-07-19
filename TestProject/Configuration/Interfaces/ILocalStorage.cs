using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Configurations.Interfaces
{
    public interface ILocalStorage<T>
    {
        void Store(T obj);

        T Get();

        void Clear();
    }
}
