using OfferingSolutions.UoW.Sample.ExampleModels;
using OfferingSolutions.UoW.Structure.RepositoryContext;

namespace OfferingSolutions.UoW.Sample.ExampleCustomRepositories
{
    public interface IThingRepository : IRepositoryContext<Thing>
    {
    }
}
