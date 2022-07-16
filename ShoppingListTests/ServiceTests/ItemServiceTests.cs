namespace ShoppingListTests.ServiceTests;

public class ItemServiceTest
{
    ItemService itemService; 
    DatabaseHandler _db; 
    
    [SetUp]
    public void Setup()
    {
        _db = new DatabaseHandler("itemlistservice.db");
        itemService = new ItemService(_db);
    }

    [Test, Order(1)]
    public void CreateItemTest()
    {
        Item newItem1 = new()
        {
            Name = "testitem1",
            Description = "lets test new item 1",
            Category = "bbq",
            Aisle = "2A",
            EstimatedPrice = 15.00m
        }; 
        
        Item newItem2 = new()
        {
            Name = "testitem2",
            Description = "lets test new item 2",
            Category = "vegtables",
            Aisle = "8C",
            EstimatedPrice = 5.00m
        }; 
        
        Item newItem3 = new()
        {
            Name = "testitem3",
            Description = "lets test new item 3",
            Category = "meat",
            Aisle = "5A",
            EstimatedPrice = 10.00m
        }; 


        itemService.CreateItem(newItem1); 
        itemService.CreateItem(newItem2); 
        itemService.CreateItem(newItem3); 
    }


    [Test, Order(2)]
    public void GetAllItemsTest() 
    {
        var items = itemService.GetAllItems(); 
        Assert.Pass("Return GetAllItems Not Null", items is not null);
        Assert.Pass("Return GetItems[0] is testitem1", items[0].Name == "testitem1"); 
        Assert.Pass("Return GetItems[1] is testitem2", items[1].Name == "testitem2"); 
        Assert.Pass("Return GetItems[2] is testitem3", items[2].Name == "testitem3"); 
    }

    [Test, Order(3)]
    public void DeleteUserListTest()
    {
        Item newItem1 = new()
        {
            Name = "testitem1",
            Description = "lets test new item 1",
            Category = "bbq",
            Aisle = "2A",
            EstimatedPrice = 15.00m
        }; 
        
        Item newItem2 = new()
        {
            Name = "testitem2",
            Description = "lets test new item 2",
            Category = "vegtables",
            Aisle = "8C",
            EstimatedPrice = 5.00m
        }; 
        
        Item newItem3 = new()
        {
            Name = "testitem3",
            Description = "lets test new item 3",
            Category = "meat",
            Aisle = "5A",
            EstimatedPrice = 10.00m
        }; 

        List<Item> itemList1 = itemService.GetItemByName(newItem1.Name); 
        List<Item> itemList2 = itemService.GetItemByName(newItem2.Name); 
        List<Item> itemList3= itemService.GetItemByName(newItem3.Name); 

        itemService.DeleteItem(itemList1[0]);
        itemService.DeleteItem(itemList2[0]);
        itemService.DeleteItem(itemList3[0]);
    }
    
    
}

