using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces;

public interface IMongoStore<T>
{
    Task<T?> Find(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate);
    Task Insert(T document);
    Task InsertMany(IEnumerable<T> documents);
    Task<T> Replace(Expression<Func<T, bool>> predicate, T document);
    Task Delete(Expression<Func<T, bool>> predicate);
}