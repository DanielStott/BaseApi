using System.Linq;
using System.Threading.Tasks;

namespace Domain.Shared.Interfaces
{
    public interface IContext<T> where T : class
    {
        public IQueryable<T> Entities { get; set; }

        Task<T> Add(T entity);
    }
}