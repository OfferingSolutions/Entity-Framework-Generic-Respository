using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OfferingSolutions.UoW.Sample.Context;
using OfferingSolutions.UoW.Sample.ExampleCustomRepositories;
using OfferingSolutions.UoW.Sample.ExampleModels;
using OfferingSolutions.UoW.Structure.UnitOfWorkContext;

namespace OfferingSolutions.UoW.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.InitializeDataBase();

            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new DatabaseContext()))
            {
                Person person = new Person() { Age = 28, Name = "Fabian" };

                //Adding a new Entity, for example "Person"
                unitOfWorkContext.Add(person);

                //Savechanges
                unitOfWorkContext.Save();

                //or...
                unitOfWorkContext.SaveASync();

                // Get all Persons
                List<Person> allPersons = unitOfWorkContext.GetAll<Person>().ToList();

                // Get all Persons with the age of 35
                List<Person> allPersonsOnAge35 = unitOfWorkContext.GetAll<Person>(x => x.Age == 35).ToList();

                // Get all Persons with the age of 35 ordered by Name
                List<Person> allPersonsOnAge35Ordered = unitOfWorkContext.GetAll<Person>(x => x.Age == 35, orderBy: q => q.OrderBy(d => d.Name)).ToList();

                // Get all Persons with the age of 35 ordered by Name and include its properties
                List<Person> allPersonsOnAge35OrderedAndWithThings = unitOfWorkContext.GetAll<Person>(
                    x => x.Age == 35,
                    orderBy: q => q.OrderBy(d => d.Name),
                    includeProperties: "Things").ToList();

                // Get all Persons and include its properties
                List<Person> allPersonsWithThings = unitOfWorkContext.GetAll<Person>(includeProperties: "Things").ToList();

                // Find a single Person with a specific name
                Person findBy = unitOfWorkContext.GetSingle<Person>(x => x.Name == "Fabian");

                // Find a single Person with a specific name and include its siblings
                Person findByWithThings = unitOfWorkContext.GetSingle<Person>(x => x.Name == "Fabian", includeProperties: "Things");

                // Find a person by id 
                unitOfWorkContext.GetSingleById<Person>(6);

                //Update an existing person
                unitOfWorkContext.Update(person);

                //Add or Update a Person
                unitOfWorkContext.AddOrUpdate<Person>(person);

                //Deleting a Person by Id or by entity
                //unitOfWorkContext.Delete<Person>(person.Id);
                unitOfWorkContext.Delete(person);
            }

            program.PerformDatabaseOperations();

            Console.ReadLine();
        }

        private async void PerformDatabaseOperations()
        {
            DatabaseContext databaseContext = new DatabaseContext();
            IPersonRepository personRepository = new PersonRepository(databaseContext);
            IThingRepository thingRepository = new ThingRepository(databaseContext);

            personRepository.Add(new Person());
            thingRepository.Add(new Thing());

            personRepository.Save();

            List<Person> persons = await personRepository.GetAllASync().Result.ToListAsync();

            Console.WriteLine(persons.Count);
            personRepository.MyNewFunction(6);
            await personRepository.SaveASync();
            List<Person> allASync = await personRepository.GetAllASync().Result.ToListAsync();

            thingRepository.Dispose();
            personRepository.Dispose();
        }

        private void InitializeDataBase()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());

            DatabaseContext context = new DatabaseContext();
            context.Database.Initialize(false);
        }
    }
}
