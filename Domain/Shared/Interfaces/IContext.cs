namespace Domain.Shared.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IContext<T> where T : class
    {
        IQueryable<T> Entities { get; set; }
        void BuildTable();
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
    }
}