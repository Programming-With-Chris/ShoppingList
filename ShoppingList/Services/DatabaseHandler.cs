using ShoppingList.Model;

namespace ShoppingList.Services;
public class DatabaseHandler
{
    private readonly SQLiteConnection _db;
    private readonly string _pathToDb;
    
    
    /// <summary>
    /// Constructor does the following:<br/>
    /// 1. Creates new SQLiteConnection private to DBH class<br/>
    /// 2. Connects<br/>
    /// 3. Creates Item table if non found<br/>
    /// 4. Creates UserList table if non found<br/>
    /// </summary>
    public DatabaseHandler()
    {
        _pathToDb = Path.Combine (
        Environment.GetFolderPath (Environment.SpecialFolder.Personal),
        "database.db3");

        _db = new SQLiteConnection(_pathToDb);
        _db.CreateTable<Item>();
        _db.CreateTable<ItemLocationData>();
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
        Guard.IsNotNullOrEmpty(newDBName, nameof(newDBName)); 
        

        _pathToDb = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), newDBName);
        _db = new SQLiteConnection(_pathToDb);
        _db.CreateTable<Item>();
        _db.CreateTable<ItemLocationData>(); 
        _db.CreateTable<UserList>();
    }

    /// <summary>
    ///  Does the .Table() query on the type passed, then performs a .ToList() on it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> GetAllQuery<T>() where T : new()
    {
        List<T> returnList = _db.Table<T>().ToList();
        return returnList; 
    }
    /// <summary>
    /// Query Performed:
    ///       SELECT * FROM typeof(T).Name (s) + WHERE name = parameter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<T> GetQueryByName<T>(string name) where T : new()
    {

        Guard.IsNotNullOrEmpty(name, nameof(name)); 
        

        List<T> returnList = _db.Query<T>("SELECT * FROM " + typeof(T).Name + "s" + " WHERE name = ?", name);

        return returnList; 
    }

    /// <summary>
    /// Query Performed:
    ///      "SELECT * FROM " + typeof(T).Name + "s" + " WHERE id = '" + id + "'")
    ///  
    ///  If nothing is found, throws a generic exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns>the 0th index of the list that's returned (if you're getting by ID, that will only ever be 1 row, right????</returns>
    /// <exception cref="Exception">If nothing is found, throws a generic exception</exception>
    public T GetQueryById<T>(int id) where T : new()
    {

        List<T> returnList = _db.Query<T>("SELECT * FROM " + typeof(T).Name + "s" + " WHERE id = ?", id);
        if (returnList.Count > 0)
            return returnList[0];

        throw new Exception("Nothing was returned from query on Table = " + typeof(T).Name + " with ID = " + id); 
    }

    /// <summary>
    ///  Query Performed
    ///     "SELECT * FROM " + typeof(T).Name + "s" + " WHERE parentid = '" + id + "'")
    ///   If nothing is found, throws a generic exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">The Parent's ID you're looking for (Foreign Key on the Item Table)</param>
    /// <returns>All Items found belonging to that parent</returns>
    /// <exception cref="Exception"></exception>
    public List<T> GetQueryByParentId<T>(int id) where T : new()
    {

        List<T> returnList = _db.Query<T>("SELECT * FROM " + typeof(T).Name + "s" + " WHERE parentid = ?", id);
        if (returnList.Count > 0)
            return returnList;

        throw new Exception("Nothing was returned from query on Table = " + typeof(T).Name + " with Parent Id = " + id); 
    }

/// <summary>
///  Inserts Object to it's table (as handled by Sqlite-net
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="newData"></param>
/// <returns>Returns the updated object from Sqlite-net (usually updating the autoincrementing id from the table)</returns>
/// <exception cref="Exception"></exception>
    public T Insert<T>(T newData) where T : new()
    {
        Guard.IsNotNull(newData, nameof(newData));
        
        var numOfRowsInserted = _db.Insert(newData);

        if (numOfRowsInserted == 0)
            throw new Exception("Unknown error occured while trying to Insert on Table " + typeof(T).Name + ".");

        return newData;

    }
/// <summary>
/// Deletes from the T table the object you pass it (as determined by sqlite-net)
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="deleteTarget"></param>
/// <exception cref="Exception"></exception>
    public void Delete<T>(T deleteTarget) where T : new()
    {
        Guard.IsNotNull(deleteTarget, nameof(deleteTarget));
        
        var numOfRowsFound = _db.Delete(deleteTarget);

        if (numOfRowsFound == 0)
            throw new Exception("No Rows Found To Delete in Table " + typeof(T).Name + "s."); 
    }

    public void Update<T>(T updateTarget) where T : new()
    {
        Guard.IsNotNull(updateTarget, nameof(updateTarget));  

        var numOfRowsFound = _db.Update(updateTarget);

        if (numOfRowsFound == 0)
            throw new Exception("No Rows Found To Update in Table " + typeof(T).Name + "s."); 
    }
}
