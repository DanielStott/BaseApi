using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetById(Guid id);
    Task<T> Get(T entity);
    Task<IEnumerable<T>> GetAll();
    Task<T> Add(T entity);
    Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
    Task<T> Update(T entity);
}