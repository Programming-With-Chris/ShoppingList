namespace ShoppingList.Services;
public class UserListService
{
    readonly DatabaseHandler _db = new(); 
    
    public UserListService()
    {
        _db = new DatabaseHandler();
    }

    public UserListService(DatabaseHandler db)
    {
        Guard.IsNotNull(db, nameof(db));

        _db = db;
    }

    public List<UserList> GetUserLists()
    {
        var returnLists = _db.GetAllQuery<UserList>(); 
        return returnLists; 
    }

    public List<UserList> GetUserListByName(string name)
    {
        Guard.IsNotNullOrEmpty(name, nameof(name));

        var returnLists = _db.GetQueryByName<UserList>(name);
        return returnLists; 
    }

    public UserList GetUserListById(UserList ul)
    {
        Guard.IsNotNull(ul, nameof(ul));

        var returnLists = _db.GetQueryById<UserList>(ul.Id);
        return returnLists; 
    }

    public UserList CreateUserList(UserList newlist)
    {
        Guard.IsNotNull(newlist, nameof(newlist));

        return _db.Insert(newlist); 

    }

    public void DeleteUserList(UserList newlist)
    {
        Guard.IsNotNull(newlist, nameof(newlist));

        _db.Delete(newlist);
    }
}


