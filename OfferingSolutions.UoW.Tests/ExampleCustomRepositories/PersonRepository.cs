using OfferingSolutions.UoW.Structure.RepositoryContext;
using OfferingSolutions.UoW.Tests.ExampleModels;
using System.Data.Entity;

namespace OfferingSolutions.UoW.Tests.ExampleCustomRepositories
{
    public class PersonRepository : RepositoryContextImpl<TestPerson>, IPersonRepository
    {
        public PersonRepository(DbContext dbContext)
            : base(dbContext)
        {

        }

        public void MyNewFunction(int id)
        {
            //Do Something
        }
    }
}
