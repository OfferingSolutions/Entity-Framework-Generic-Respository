using OfferingSolutions.UoW.Sample.Context;
using OfferingSolutions.UoW.Sample.ExampleModels;
using OfferingSolutions.UoW.Structure.RepositoryContext;

namespace OfferingSolutions.UoW.Sample.ExampleCustomRepositories
{
    public class ThingRepository : RepositoryContextImpl<Thing>, IThingRepository
    {
        public ThingRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
