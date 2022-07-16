namespace ShoppingListTests.ServiceTests
{
    public class DatabaseHandlerTests 
    {
        DatabaseHandler _db; 
        [SetUp]
        public void Setup()
        {
            _db = new DatabaseHandler("databasehandlertest.db"); 
        }

        [Test, Order(1)]
        public void InsertTest()
        {
            _db.Insert<UserList>(new UserList("testlist1", "walmart"));
        }

        [Test, Order(2)]
        public void ContructorTests()
        {

            Assert.Pass("DB Not Null", _db is not null);
        }

        [Test, Order(2)]
        public void GetAllQueryTest()
        {
            List<UserList> ulList = _db.GetAllQuery<UserList>(); 
            Assert.IsNotNull(ulList);
            Assert.IsTrue(ulList[0].Name == "testlist1"); 
            Assert.IsTrue(ulList[0].TargetStore == "walmart"); 
        }

        [Test, Order(2)]
        public void GetQueryByNameTest()
        {
            List<UserList> ul = _db.GetQueryByName<UserList>("testlist1"); 

            Assert.IsNotNull(ul);
            Assert.IsTrue(ul[0].Name == "testlist1"); 
            Assert.IsTrue(ul[0].TargetStore == "walmart"); 

        }


        [Test, Order(2)]
        public void GetQueryByIdTest()
        {
            List<UserList> ulList = _db.GetQueryByName<UserList>("testlist1"); 
            UserList ul = _db.GetQueryById<UserList>(ulList[0].Id); 

            Assert.IsNotNull(ul);
            Assert.IsTrue(ul.Name == "testlist1"); 
            Assert.IsTrue(ul.TargetStore == "walmart"); 

        }

        [Test, Order(10)]
        public void DeleteTest()
        {            
            List<UserList> ulList = _db.GetQueryByName<UserList>("testlist1");
            _db.Delete(ulList[0]); 
        }


    }
}
