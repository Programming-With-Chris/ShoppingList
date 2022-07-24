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
        Guard.IsNotNull(db, nameof(db));

        _db = db;
    }

    public List<Item> GetAllItems()
    {
        var returnLists = _db.GetAllQuery<Item>(); 
        return returnLists; 
    }

    public List<Item> GetItemByName(string name)
    {
        Guard.IsNotNullOrEmpty(name, nameof(name)); 

        var returnLists = _db.GetQueryByName<Item>(name);
        return returnLists; 
    }

    public Item GetItemById(Item item)
    {
        Guard.IsNotNull(item, nameof(item)); 

        var returnItem = _db.GetQueryById<Item>(item.Id); 
        return returnItem; 
    }
    public List<Item> GetUserListItems(UserList ul)
    {
        Guard.IsNotNull(ul, nameof(ul)); 
        
        var itemList = _db.GetQueryByParentId<Item>(ul.Id);

        foreach (Item item in itemList)
        {
            item.LocationData = _db.GetQueryByParentId<ItemLocationData>(item.Id).FirstOrDefault(); 
        }
        return itemList; 
    }

    public List<Item> GetItemByParentId(UserList ul)
    {
        Guard.IsNotNull(ul, nameof(ul));

        var returnLists = _db.GetQueryByParentId<Item>(ul.Id);
        foreach (Item item in returnLists)
        {
            item.LocationData = _db.GetQueryByParentId<ItemLocationData>(item.Id).FirstOrDefault(); 
        }

        return returnLists; 
    }

    public Item CreateItem(Item newItem)
    {
        Guard.IsNotNull(newItem, nameof(newItem)); 

        var item = _db.Insert(newItem);
        newItem.LocationData.ParentId = item.Id; 
        newItem.Id = item.Id;
        _db.Insert<ItemLocationData>(newItem.LocationData); 
        return newItem; 

    }

    public void DeleteItem(Item deletedItem)
    {
        Guard.IsNotNull(deletedItem, nameof(deletedItem)); 

        _db.Delete(deletedItem);
    }

    public void UpdateItem(Item updatedItem)
    {
        Guard.IsNotNull(updatedItem, nameof(updatedItem));

        _db.Update(updatedItem); 
    }
}


