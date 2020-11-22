using System.Collections.Generic;
using Solutis.Model.Base;

namespace Solutis.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long? id);
    }
}
