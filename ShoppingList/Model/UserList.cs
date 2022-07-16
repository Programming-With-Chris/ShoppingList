using SQLiteNetExtensions.Attributes;

namespace ShoppingList.Model;


[Table("UserLists")]
public class UserList
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("targetStore")]
    public string TargetStore { get; set; }

    [OneToMany]
    public List<Item> Items { get; set; }


    public UserList()
    {
        Items = new(); 
    }

    public UserList(string name, string targetStore)
    {
        Name = name;
        TargetStore = targetStore;
        Items = new(); 
    }
}
