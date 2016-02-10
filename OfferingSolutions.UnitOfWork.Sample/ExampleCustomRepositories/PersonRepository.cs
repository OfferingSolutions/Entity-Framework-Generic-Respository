using OfferingSolutions.UoW.Sample.Context;
using OfferingSolutions.UoW.Sample.ExampleModels;
using OfferingSolutions.UoW.Structure.RepositoryContext;

namespace OfferingSolutions.UoW.Sample.ExampleCustomRepositories
{
    public class PersonRepository : RepositoryContextImpl<Person>, IPersonRepository
    {
        public PersonRepository(DatabaseContext dbContext)
            : base(dbContext)
        {

        }

        public void MyNewFunction(int id)
        {
            //Do Something
        }

        public override void Add(Person toAdd)
        {
            MyAdditionalAddFunction();
            base.Add(toAdd);
        }

        private void MyAdditionalAddFunction()
        {
            //Do something else...
        }
    }
}
