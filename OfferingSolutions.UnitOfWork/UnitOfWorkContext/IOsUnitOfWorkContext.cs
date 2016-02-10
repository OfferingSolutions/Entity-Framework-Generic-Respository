using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OfferingSolutions.UoW.Structure.UnitOfWorkContext
{
    public interface IOsUnitOfWorkContext : IDisposable
    {
        void Add<T>(T toAdd) where T : class;

        void AddOrUpdate<T>(T toAddOrUpdate) where T : class;

        void Delete<T>(int id) where T : class;

        void Delete<T>(T toDelete) where T : class;

        T GetSingle<T>(Expression<Func<T, bool>> predicate, string includeProperties = "") where T : class;

        Task<T> GetSingleASync<T>(Expression<Func<T, bool>> predicate, string includeProperties = "") where T : class;

        T GetSingleById<T>(int id) where T : class;

        Task<T> GetSingleByIdASync<T>(int id) where T : class;

        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "") where T : class;

        Task<IQueryable<T>> GetAllASync<T>(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "") where T : class;

        int Save();

        Task<int> SaveASync();

        void Update<T>(T toUpdate) where T : class;
    }
}
