using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfferingSolutions.UoW.Structure.UnitOfWorkContext;
using OfferingSolutions.UoW.Tests.Context;
using OfferingSolutions.UoW.Tests.ExampleCustomRepositories;
using OfferingSolutions.UoW.Tests.ExampleModels;

namespace OfferingSolutions.UoW.Tests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestInitialize]
        public void TestInit()
        {
            InitializeDataBase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestDatabaseContext context = new TestDatabaseContext();
            context.Database.Delete();
        }

        [TestMethod]
        public void GenericRepo_Add_Entry_Exists()
        {
            List<TestPerson> testPersons;
            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.Add(new TestPerson());

                unitOfWorkContext.Save();

                testPersons = unitOfWorkContext.GetAll<TestPerson>().ToList();
            }

            Assert.IsTrue(testPersons.Count == 1);
        }

        [TestMethod]
        public void GenericRepo_Add_Without_Save_Entry_Does_Not_Exists()
        {
            List<TestPerson> testPersons;
            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.Add(new TestPerson());

                testPersons = unitOfWorkContext.GetAll<TestPerson>().ToList();
            }

            Assert.IsTrue(testPersons.Count == 0);
        }

        [TestMethod]
        public void GenericRepo_AddOrUpdate_Add_Entry_Exists()
        {
            List<TestPerson> testPersons;
            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.AddOrUpdate(new TestPerson { Age = 28, Name = "Fabian" });

                unitOfWorkContext.Save();

                testPersons = unitOfWorkContext.GetAll<TestPerson>().ToList();
            }

            Assert.IsTrue(testPersons.Count == 1);
        }

        [TestMethod]
        public void GenericRepo_AddOrUpdate_Update_Entry_Is_Updated()
        {
            TestPerson findByUpdated;

            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.AddOrUpdate(new TestPerson { Age = 28, Name = "Fabian" });
                unitOfWorkContext.Save();

                TestPerson findBy = unitOfWorkContext.GetSingle<TestPerson>(x => x.Name == "Fabian");
                findBy.Name = "Claudio";

                unitOfWorkContext.AddOrUpdate(findBy);
                unitOfWorkContext.Save();

                findByUpdated = unitOfWorkContext.GetSingle<TestPerson>(x => x.Name == "Claudio");
            }

            Assert.IsNotNull(findByUpdated);
            Assert.IsTrue(findByUpdated.Name == "Claudio");
        }

        [TestMethod]
        public void GenericRepo_Added_TestPersons_Get_Ordered_Ascending()
        {
            List<TestPerson> testPersons;

            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Zebra" });
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Adam" });
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Fabian" });
                unitOfWorkContext.Save();

                testPersons = unitOfWorkContext.GetAll<TestPerson>(orderBy: q => q.OrderBy(x => x.Name)).ToList();
            }

            Assert.IsNotNull(testPersons);
            Assert.IsTrue(testPersons[0].Name == "Adam");
            Assert.IsTrue(testPersons[1].Name == "Fabian");
            Assert.IsTrue(testPersons[2].Name == "Zebra");
        }

        [TestMethod]
        public void GenericRepo_Added_TestPersons_Get_Ordered_Descending()
        {
            List<TestPerson> testPersons;

            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Zebra" });
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Adam" });
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Fabian" });
                unitOfWorkContext.Save();

                testPersons = unitOfWorkContext.GetAll<TestPerson>(orderBy: q => q.OrderByDescending(x => x.Name)).ToList();
            }

            Assert.IsNotNull(testPersons);
            Assert.IsTrue(testPersons[2].Name == "Adam");
            Assert.IsTrue(testPersons[1].Name == "Fabian");
            Assert.IsTrue(testPersons[0].Name == "Zebra");
        }

        [TestMethod]
        public void GenericRepo_Update_Entry_Is_Updated()
        {
            TestPerson findByUpdated;

            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Fabian" });
                unitOfWorkContext.Save();

                TestPerson findBy = unitOfWorkContext.GetSingle<TestPerson>(x => x.Name == "Fabian");
                findBy.Name = "Claudio";

                unitOfWorkContext.Update(findBy);
                unitOfWorkContext.Save();

                findByUpdated = unitOfWorkContext.GetSingle<TestPerson>(x => x.Name == "Claudio");
            }

            Assert.IsNotNull(findByUpdated);
            Assert.IsTrue(findByUpdated.Name == "Claudio");
        }

        [TestMethod]
        public void GenericRepo_Delete_Entry_With_Entry_Is_Deleted()
        {
            List<TestPerson> testPersons;

            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Fabian" });
                unitOfWorkContext.Save();

                TestPerson findBy = unitOfWorkContext.GetSingle<TestPerson>(x => x.Name == "Fabian");

                unitOfWorkContext.Delete(findBy);
                unitOfWorkContext.Save();

                testPersons = unitOfWorkContext.GetAll<TestPerson>().ToList();
            }

            Assert.IsTrue(testPersons.Count == 0);
        }

        [TestMethod]
        public void GenericRepo_Delete_Entry_With_Id_Is_Deleted()
        {
            List<TestPerson> testPersons;

            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                unitOfWorkContext.Add(new TestPerson { Age = 28, Name = "Fabian" });
                unitOfWorkContext.Save();

                TestPerson findBy = unitOfWorkContext.GetSingle<TestPerson>(x => x.Name == "Fabian");

                unitOfWorkContext.Delete<TestPerson>(findBy.Id);
                unitOfWorkContext.Save();

                testPersons = unitOfWorkContext.GetAll<TestPerson>().ToList();
            }

            Assert.IsTrue(testPersons.Count == 0);
        }

        [TestMethod]
        public void GenericRepo_Stopwatch_Generate_Generic_Repo()
        {
            Stopwatch stopwatch = new Stopwatch();
            TimeSpan timeSpan1, timeSpan2;
            using (IOsUnitOfWorkContext unitOfWorkContext = new OsUnitOfWorkContext(new TestDatabaseContext()))
            {
                stopwatch.Start();
                unitOfWorkContext.GetAll<TestPerson>();
                stopwatch.Stop();
                timeSpan1 = stopwatch.Elapsed;
                stopwatch.Reset();

                stopwatch.Start();
                unitOfWorkContext.GetAll<TestPerson>();
                stopwatch.Stop();
                timeSpan2 = stopwatch.Elapsed;
                stopwatch.Reset();
            }

            Console.WriteLine(timeSpan1);
            Console.WriteLine(timeSpan2);
            Assert.IsTrue(timeSpan2 < timeSpan1);
        }

        [TestMethod]
        public void CustomRepo_Add_Entry_Exists()
        {
            List<TestPerson> testPersons;
            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.Add(new TestPerson());

                personRepository.Save();

                testPersons = personRepository.GetAll().ToList();
            }

            Assert.IsTrue(testPersons.Count == 1);
        }

        [TestMethod]
        public void CustomRepo_Add_Without_Save_Entry_Does_Not_Exists()
        {
            List<TestPerson> testPersons;
            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.Add(new TestPerson());

                testPersons = personRepository.GetAll().ToList();
            }

            Assert.IsTrue(testPersons.Count == 0);
        }

        [TestMethod]
        public void CustomRepo_AddOrUpdate_Add_Entry_Exists()
        {
            List<TestPerson> testPersons;
            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.AddOrUpdate(new TestPerson { Age = 28, Name = "Fabian" });

                personRepository.Save();

                testPersons = personRepository.GetAll().ToList();
            }

            Assert.IsTrue(testPersons.Count == 1);
        }

        [TestMethod]
        public void CustomRepo_AddOrUpdate_Update_Entry_Is_Updated()
        {
            TestPerson findByUpdated;

            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.AddOrUpdate(new TestPerson { Age = 28, Name = "Fabian" });
                personRepository.Save();

                TestPerson findBy = personRepository.GetSingle(x => x.Name == "Fabian");
                findBy.Name = "Claudio";

                personRepository.AddOrUpdate(findBy);
                personRepository.Save();

                findByUpdated = personRepository.GetSingle(x => x.Name == "Claudio");
            }

            Assert.IsNotNull(findByUpdated);
            Assert.IsTrue(findByUpdated.Name == "Claudio");
        }

        [TestMethod]
        public void CustomRepo_Added_TestPersons_Get_Ordered_Ascending()
        {
            List<TestPerson> testPersons;

            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.Add(new TestPerson { Age = 28, Name = "Zebra" });
                personRepository.Add(new TestPerson { Age = 28, Name = "Adam" });
                personRepository.Add(new TestPerson { Age = 28, Name = "Fabian" });
                personRepository.Save();

                testPersons = personRepository.GetAll(orderBy: q => q.OrderBy(x => x.Name)).ToList();
            }

            Assert.IsNotNull(testPersons);
            Assert.IsTrue(testPersons[0].Name == "Adam");
            Assert.IsTrue(testPersons[1].Name == "Fabian");
            Assert.IsTrue(testPersons[2].Name == "Zebra");
        }

        [TestMethod]
        public void CustomRepo_Added_TestPersons_Get_Ordered_Descending()
        {
            List<TestPerson> testPersons;

            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.Add(new TestPerson { Age = 28, Name = "Zebra" });
                personRepository.Add(new TestPerson { Age = 28, Name = "Adam" });
                personRepository.Add(new TestPerson { Age = 28, Name = "Fabian" });
                personRepository.Save();

                testPersons = personRepository.GetAll(orderBy: q => q.OrderByDescending(x => x.Name)).ToList();
            }

            Assert.IsNotNull(testPersons);
            Assert.IsTrue(testPersons[2].Name == "Adam");
            Assert.IsTrue(testPersons[1].Name == "Fabian");
            Assert.IsTrue(testPersons[0].Name == "Zebra");
        }

        [TestMethod]
        public void CustomRepo_Update_Entry_Is_Updated()
        {
            TestPerson findByUpdated;

            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.Add(new TestPerson { Age = 28, Name = "Fabian" });
                personRepository.Save();

                TestPerson findBy = personRepository.GetSingle(x => x.Name == "Fabian");
                findBy.Name = "Claudio";

                personRepository.Update(findBy);
                personRepository.Save();

                findByUpdated = personRepository.GetSingle(x => x.Name == "Claudio");
            }

            Assert.IsNotNull(findByUpdated);
            Assert.IsTrue(findByUpdated.Name == "Claudio");
        }

        [TestMethod]
        public void CustomRepo_Delete_Entry_With_Entry_Is_Deleted()
        {
            List<TestPerson> testPersons;

            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.Add(new TestPerson { Age = 28, Name = "Fabian" });
                personRepository.Save();

                TestPerson findBy = personRepository.GetSingle(x => x.Name == "Fabian");

                personRepository.Delete(findBy);
                personRepository.Save();

                testPersons = personRepository.GetAll().ToList();
            }

            Assert.IsTrue(testPersons.Count == 0);
        }

        [TestMethod]
        public void CustomRepo_Delete_Entry_With_Id_Is_Deleted()
        {
            List<TestPerson> testPersons;

            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                personRepository.Add(new TestPerson { Age = 28, Name = "Fabian" });
                personRepository.Save();

                TestPerson findBy = personRepository.GetSingle(x => x.Name == "Fabian");

                personRepository.Delete(findBy.Id);
                personRepository.Save();

                testPersons = personRepository.GetAll().ToList();
            }

            Assert.IsTrue(testPersons.Count == 0);
        }

        [TestMethod]
        public void CustomRepo_Stopwatch_Generate_Custom_Repo()
        {
            Stopwatch stopwatch = new Stopwatch();
            TimeSpan timeSpan1, timeSpan2;
            using (IPersonRepository personRepository = new PersonRepository(new TestDatabaseContext()))
            {
                stopwatch.Start();
                personRepository.GetAll();
                stopwatch.Stop();
                timeSpan1 = stopwatch.Elapsed;
                stopwatch.Reset();

                stopwatch.Start();
                personRepository.GetAll();
                stopwatch.Stop();
                timeSpan2 = stopwatch.Elapsed;
                stopwatch.Reset();
            }

            Console.WriteLine(timeSpan1);
            Console.WriteLine(timeSpan2);
            Assert.IsTrue(timeSpan2 < timeSpan1);
        }

        private void InitializeDataBase()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TestDatabaseContext>());

            TestDatabaseContext context = new TestDatabaseContext();
            context.Database.Initialize(false);
        }
    }
}
