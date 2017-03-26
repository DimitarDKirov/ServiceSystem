using System.Linq;
using ServiceSystem.Data.Common.Models;

namespace ServiceSystem.Data.Common.Contracts
{
    public interface IEfDbRepository<T> : IEfDbRepository<T, int>
        where T : BaseModel<int>
    {
    }

    public interface IEfDbRepository<T, in TKey>
        where T : BaseModel<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(TKey id);

        T Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Update(T entity);

        void Save();
    }
}
