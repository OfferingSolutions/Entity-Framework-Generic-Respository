using OfferingSolutions.UoW.Structure.ContextBase;
using System.Data.Entity;

namespace OfferingSolutions.UoW.Structure.UnitOfWorkContext
{
    public class OsUnitOfWorkContext : ContextBaseImpl, IOsUnitOfWorkContext
    {
        public OsUnitOfWorkContext(DbContext databaseContext)
            : base(databaseContext)
        {

        }
    }
}