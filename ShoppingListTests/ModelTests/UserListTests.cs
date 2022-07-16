namespace ShoppingListTests.ModelTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ContructorTests()
        {
            UserList ul = new UserList("list1", "walmart");
            Assert.Pass("UserList Constructor", ul.Name == "list1" && ul.TargetStore == "walmart");
        }
    }
}