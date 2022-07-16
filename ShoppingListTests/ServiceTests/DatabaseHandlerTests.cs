namespace ShoppingListTests.ServiceTests
{
    public class DatabaseHandlerTests 
    {
        DatabaseHandler _db; 
        [SetUp]
        public void Setup()
        {
            _db = new DatabaseHandler("test1.db"); 
        }

        [Test]
        public void ContructorTests()
        {

            Assert.Pass("DB Not Null", _db is not null);
        }

        [Test]
        public void GetAllQueryTest()
        {
            List<UserList> ulList = _db.GetAllQuery<UserList>(); 
            Assert.IsNotNull(ulList);
            Assert.IsTrue(ulList[0].Name == "list-test"); 
            Assert.IsTrue(ulList[0].TargetStore == "kroger"); 
        }

        [Test]
        public void GetQueryByNameTest()
        {
            List<UserList> ul = _db.GetQueryByName<UserList>("list-test"); 

            Assert.IsNotNull(ul);
            Assert.IsTrue(ul[0].Name == "list-test"); 
            Assert.IsTrue(ul[0].TargetStore == "kroger"); 

        }


        [Test]
        public void GetQueryByIdTest()
        {
            UserList ul = _db.GetQueryById<UserList>(1); 

            Assert.IsNotNull(ul);
            Assert.IsTrue(ul.Name == "list-test"); 
            Assert.IsTrue(ul.TargetStore == "kroger"); 

        }




    }
}
