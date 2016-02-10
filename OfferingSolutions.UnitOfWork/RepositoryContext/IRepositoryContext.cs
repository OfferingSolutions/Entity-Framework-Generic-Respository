using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.UoW.Structure.RepositoryContext
{
    public interface IRepositoryContext<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<IQueryable<T>> GetAllASync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetSingleById(int id);

        Task<T> GetSingleByIdASync(int id);

        T GetSingle(Expression<Func<T, bool>> predicate, string includeProperties = "");

        Task<T> GetSingleASync(Expression<Func<T, bool>> predicate, string includeProperties = "");

        void Add(T toAdd);

        void AddOrUpdate(T toAddOrUpdate);

        void Update(T toUpdate);

        void Delete(int id);

        void Delete(T entity);

        int Save();

        Task<int> SaveASync();
    }
}