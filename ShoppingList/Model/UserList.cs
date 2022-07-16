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

    [OneToMany("item")]
    public List<Item> Items { get; set; }


    public UserList()
    {

    }

    public UserList(string name, string targetStore)
    {
        Name = name;
        TargetStore = targetStore;
    }
}
