using Data.Repositories.Interfaces;
using Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
