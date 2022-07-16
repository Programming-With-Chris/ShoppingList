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

    public List<T> GetAllQuery<T>() where T : new()
    {
        List<T> returnList = _db.Table<T>().ToList();
        return returnList; 
    }

    public List<T> GetQueryByName<T>(string name) where T : new()
    {
        List<T> returnList = _db.Query<T>("SELECT * FROM " + typeof(T).Name + "s" + " WHERE name = '" + name + "'");

        return returnList; 
    }

    public T GetQueryById<T>(int id) where T : new()
    {
        
        List<T> returnList = _db.Query<T>("SELECT * FROM " + typeof(T).Name + "s" + " WHERE id = '" + id + "'");
        if (returnList.Count > 0)
            return returnList[0];

        throw new Exception("Nothing was returned from query on Table = " + typeof(T).Name + " with ID = " + id); 
    }

    public void Insert<T>(T newData) where T : new()
    {
        _db.Insert(newData);
    }
}
