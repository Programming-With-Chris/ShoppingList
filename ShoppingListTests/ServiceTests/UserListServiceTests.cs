namespace ShoppingListTests.ServiceTests;

public class UserListServiceTest
{
    UserListService uls;
    DatabaseHandler _db; 

    /// <summary>
    /// DB content for tests:
    ///    UserList ul1 = new UserList("testlist1", "walmart"); 
    ///    UserList ul2 = new UserList("testlist2", "kroger"); 
    ///    UserList ul3 = new UserList("testlist3", "target");
    /// 
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _db = new DatabaseHandler("userlistservicetest.db");
        uls = new UserListService(_db);
    }

    [Test, Order(1)]
    public void CreateUserListTest()
    {
        UserList ul1 = new("testlist1", "walmart");
        Item newItem1 = new()
        {
            Name = "testitem1",
            Description = "lets test new item 1",
            Category = "bbq",
            Aisle = "2A",
            EstimatedPrice = 15.00m
        }; 
        ul1.Items.Add(newItem1);
        

        UserList ul2 = new("testlist2", "kroger");

        UserList ul3 = new("testlist3", "target");

        uls.CreateUserList(ul1); 
        uls.CreateUserList(ul2); 
        uls.CreateUserList(ul3); 
    }


    [Test, Order(2)]
    public void GetUserListsTest() 
    {
        var lists = uls.GetUserLists(); 
        Assert.Pass("Return GetUserLists Not Null", lists is not null);
        Assert.Pass("Return GetUserLists[0] is testlist1", lists[0].Name == "testlist1"); 
        Assert.Pass("Return GetUserLists[1] is testlist2", lists[1].Name == "testlist2"); 
        Assert.Pass("Return GetUserLists[2] is testlist3", lists[2].Name == "testlist3"); 
        Assert.Pass("Return GetUserLists Not Null", lists is not null);
    }

    [Test, Order(3)]
    public void DeleteUserListTest()
    {
        UserList ul1 = new("testlist1", "walmart");

        UserList ul2 = new("testlist2", "kroger");

        UserList ul3 = new("testlist3", "target");

        List<UserList> ul1List = uls.GetUserListByName(ul1.Name); 
        List<UserList> ul2List = uls.GetUserListByName(ul2.Name); 
        List<UserList> ul3List = uls.GetUserListByName(ul3.Name);

        uls.DeleteUserList(ul1List[0]);
        uls.DeleteUserList(ul2List[0]);
        uls.DeleteUserList(ul3List[0]); 
    }
    
    
}

