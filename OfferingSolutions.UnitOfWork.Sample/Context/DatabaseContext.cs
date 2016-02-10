using OfferingSolutions.UoW.Sample.ExampleModels;
using System.Data.Entity;

namespace OfferingSolutions.UoW.Sample.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Thing> Things { get; set; }
    }
}