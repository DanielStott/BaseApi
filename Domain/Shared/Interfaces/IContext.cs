using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces;

public interface IContext<T> where T : class
{
    IQueryable<T> Entities { get; set; }
    Task BuildTable();
    Task<T> Add(T entity);
    Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
    Task<T> Update(T entity);
}