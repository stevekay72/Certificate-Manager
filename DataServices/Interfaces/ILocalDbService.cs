using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataServices.Interfaces
{
    public interface ILocalDbService
    {
        void Delete<T>(Expression<Func<T, bool>> query);
        void Insert<T>(T item);
        void Upsert<T>(T item);
        int Count<T>();
        int Count<T>(Expression<Func<T, bool>> query);
        IEnumerable<T> All<T>();
        IEnumerable<T> Query<T>(Expression<Func<T, bool>> query);
        void Update<T>(T item);
    }
}
