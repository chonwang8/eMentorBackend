using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T Get(Guid id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
