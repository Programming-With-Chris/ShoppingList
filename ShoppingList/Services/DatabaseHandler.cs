using ShoppingList.Model;

namespace ShoppingList.Services; 
public class DatabaseHandler
{
    private SQLiteConnection _db;
    private string _pathToDb; 
    public DatabaseHandler()
    {
        _pathToDb = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "shoppinglist_sqlite.db");
        _db = new SQLiteConnection(_pathToDb);
        _db.CreateTable<Item>();
        _db.CreateTable<UserList>(); 
    }

    public DatabaseHandler(string newDBName)
    {
        _pathToDb = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), newDBName);
        _db = new SQLiteConnection(_pathToDb); 
        _db.CreateTable<Item>();
        _db.CreateTable<UserList>(); 
    }

    public List<UserList> GetUserLists()
    {
        var returnLists = _db.Query<UserList>("SELECT * FROM UserLists");
        return returnLists; 
    }

    public void CreateUserList(string name, string targetStore)
    {
        var newUL = new UserList
        {
            Name = name,
            TargetStore = targetStore
        }; 

        _db.Insert(newUL); 
    }

}
