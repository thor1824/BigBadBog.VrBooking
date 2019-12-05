using System.Collections.Generic;

namespace VrBooking.Core.DomainServices
{
    public interface IRepository<T>
    {
        T Create(T entity);

        T Read(long id);

        IEnumerable<T> ReadAll();

        T Update(T entity);

        T Delete(T entity);

    }
}
