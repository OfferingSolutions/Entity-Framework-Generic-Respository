using System;
using System.Data.Entity;
using OfferingSolutions.UoW.Structure.RepositoryBase;

namespace OfferingSolutions.UoW.Structure.RepositoryService
{
    internal interface IRepositoryService
    {
        DbContext DbContext { get; set; }

        IRepositoryBase<T> GetGenericRepository<T>() where T : class;

        T GetCustomRepository<T>(Func<DbContext, object> factory = null) where T : class;
    }
}