using System.Linq;
using VlogRoom.Data.Models.Contracts;

namespace VlogRoom.Data.Repository
{
    public interface IEfRepository<T> where T : class, IDeletable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAndDeleted { get; }

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}