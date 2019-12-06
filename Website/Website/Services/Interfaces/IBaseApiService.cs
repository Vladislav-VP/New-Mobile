using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IBaseApiService<TEntity> where TEntity : class
    {
        TEntity FindById(int id);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}
