namespace ShoppingList.Services;
public class ItemService
{
    readonly DatabaseHandler _db = new(); 
    
    public ItemService()
    {
        _db = new DatabaseHandler();
    }

    public ItemService(DatabaseHandler db)
    {
        _db = db;
    }

    public List<Item> GetAllItems()
    {
        var returnLists = _db.GetAllQuery<Item>(); 
        return returnLists; 
    }

    public List<Item> GetItemByName(string name)
    {

        var returnLists = _db.GetQueryByName<Item>(name);
        return returnLists; 
    }

    public Item GetItemById(Item item)
    {
        var returnItem = _db.GetQueryById<Item>(item.Id); 
        return returnItem; 
    }
    public List<Item> GetUserListItems(UserList ul)
    {
        var returnLists = _db.GetQueryById<UserList>(ul.Id);
        return returnLists.Items; 
    }

    public List<Item> GetItemByParentId(UserList ul)
    {
        var returnLists = _db.GetQueryByParentId<Item>(ul.Id);
        return returnLists; 
    }

    public Item CreateItem(Item newItem)
    {
        return _db.Insert(newItem); 

    }

    public void DeleteItem(Item deletedItem)
    {
        _db.Delete(deletedItem);
    }
}


