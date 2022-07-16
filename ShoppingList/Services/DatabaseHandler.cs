using ShoppingList.Model;

namespace ShoppingList.Services;
public class DatabaseHandler
{
    private SQLiteConnection _db;
    private string _pathToDb;
    
    
    /// <summary>
    /// Constructor does the following:<br/>
    /// 1. Creates new SQLiteConnection private to DBH class<br/>
    /// 2. Connects<br/>
    /// 3. Creates Item table if non found<br/>
    /// 4. Creates UserList table if non found<br/>
    /// </summary>
    public DatabaseHandler()
    {
        _pathToDb = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "shoppinglist_sqlite.db");
        _db = new SQLiteConnection(_pathToDb);
        _db.CreateTable<Item>();
        _db.CreateTable<UserList>();
    }

    /// <summary>
    /// Constructor does the following:<br/>
    /// 1. Creates new SQLiteConnection private to DBH class at <paramref name="newDBName"/><br/>
    /// 2. Connects<br/>
    /// 3. Creates Item table if non found<br/>
    /// 4. Creates UserList table if non found<br/>
    /// </summary>
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
    public T Insert<T>(T newData) where T : new()
    {
        var numOfRowsInserted = _db.Insert(newData);

        if (numOfRowsInserted == 0)
            throw new Exception("Unknown error occured while trying to Insert on Table " + typeof(T).Name + ".");

        return newData;

    }

    public void Delete<T>(T deleteTarget) where T : new()
    {
        var numOfRowsFound = _db.Delete(deleteTarget);

        if (numOfRowsFound == 0)
            throw new Exception("No Rows Found To Delete in Table " + typeof(T).Name + "s."); 
    }
}
