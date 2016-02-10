# OfferingSolutions UnitOfWork with Entity Framework

[![NuGet Downloads](https://img.shields.io/nuget/dt/OfferingSolutions.UnitOfWork.Structure .svg)](https://www.nuget.org/packages/OfferingSolutions.UnitOfWork.Structure/) [![NuGet Version](https://img.shields.io/nuget/v/OfferingSolutions.UnitOfWork.Structure .svg)](https://www.nuget.org/packages/OfferingSolutions.UnitOfWork.Structure/)

Offering you a complete abstraction of the UnitOfWork-Pattern with the basic CRUD-Operations, the Repository Pattern and extended functions like CustomRepositores all in one small lib. Made for the Entity Framework.

See the Sample-Project how this works. 

See Nuget to load this package:

https://www.nuget.org/packages/OfferingSolutions.UnitOfWork.Structure/

<pre>Install-Package OfferingSolutions.UnitOfWork.Structure</pre>

Have fun. Hope this helps :)

<pre>
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
</pre>

or you can make repositories for each entity

<pre>
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
</pre>
