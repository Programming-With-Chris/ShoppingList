namespace ShoppingListTests.ServiceTests
{
    public class DatabaseHandlerTests 
    {
        DatabaseHandler _db; 
        [SetUp]
        public void Setup()
        {
            _db = new DatabaseHandler("test.db");
        }

        [Test]
        public void ContructorTests()
        {

            Assert.Pass("DB Not Null", _db is not null);
        }


        // Probably shouldn't have a test that adds a row to a table every time....?
        [Test]
        public void DBUserListTest()
        {
            _db.CreateUserList("list-test", "kroger");

            List<UserList> uls = _db.GetUserLists();

            foreach (UserList ul in uls)
            {
                Assert.Pass("ul had data", ul?.Name == "list-test" && ul?.TargetStore == "kroger"); 
            }
            
        }
    }
}
