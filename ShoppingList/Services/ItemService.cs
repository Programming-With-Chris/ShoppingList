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
        var itemList = _db.GetQueryByParentId<Item>(ul.Id);

        foreach (Item item in itemList)
        {
            item.LocationData = _db.GetQueryByParentId<ItemLocationData>(item.Id).FirstOrDefault(); 
        }
        return itemList; 
    }

    public List<Item> GetItemByParentId(UserList ul)
    {
        var returnLists = _db.GetQueryByParentId<Item>(ul.Id);
        foreach (Item item in returnLists)
        {
            item.LocationData = _db.GetQueryByParentId<ItemLocationData>(item.Id).FirstOrDefault(); 
        }

        return returnLists; 
    }

    public Item CreateItem(Item newItem)
    {
        var item = _db.Insert(newItem);
        newItem.LocationData.ParentId = item.Id; 
        newItem.Id = item.Id;
        _db.Insert<ItemLocationData>(newItem.LocationData); 
        return newItem; 

    }

    public void DeleteItem(Item deletedItem)
    {
        _db.Delete(deletedItem);
    }

    public void UpdateItem(Item updatedItem)
    {
        _db.Update(updatedItem); 
    }
}


