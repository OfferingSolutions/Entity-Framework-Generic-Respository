using OfferingSolutions.UoW.Structure.RepositoryContext;
using OfferingSolutions.UoW.Tests.ExampleModels;

namespace OfferingSolutions.UoW.Tests.ExampleCustomRepositories
{
    public interface IPersonRepository : IRepositoryContext<TestPerson>
    {
        void MyNewFunction(int id);
    }
}