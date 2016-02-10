using OfferingSolutions.UoW.Tests.ExampleModels;
using System.Data.Entity;

namespace OfferingSolutions.UoW.Tests.Context
{
    public class TestDatabaseContext : DbContext
    {
        public DbSet<TestPerson> Persons { get; set; }
    }
}