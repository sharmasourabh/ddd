using System;
using System.Collections.Generic;
using System.Text;

namespace RevenueRecognition.Domain
{
    public interface GenericRepository<T, IEquatable>
    {
        T findOne(IEquatable id);

        T save(T entity);

        T update(T entity);

        void delete(IEquatable id);

        void delete(T entity);    
    }
}
