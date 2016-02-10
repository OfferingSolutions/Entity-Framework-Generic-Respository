using OfferingSolutions.UoW.Structure.RepositoryBase;
using System;
using System.Threading.Tasks;

namespace OfferingSolutions.UoW.Structure.UnitOfWork
{
    internal interface IOsUnitOfWork : IDisposable
    {
        IRepositoryBase<T> GetRepository<T>() where T : class;

        int Save();

        Task<int> SaveASync();
    }
}