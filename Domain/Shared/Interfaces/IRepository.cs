namespace Domain.Shared.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<T> Get(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
    }
}