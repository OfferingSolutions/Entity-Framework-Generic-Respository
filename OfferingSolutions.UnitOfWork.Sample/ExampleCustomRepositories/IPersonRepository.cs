using OfferingSolutions.UoW.Sample.ExampleModels;
using OfferingSolutions.UoW.Structure.RepositoryContext;

namespace OfferingSolutions.UoW.Sample.ExampleCustomRepositories
{
    public interface IPersonRepository : IRepositoryContext<Person>
    {
        void MyNewFunction(int id);
    }
}