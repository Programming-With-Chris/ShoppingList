using SQLiteNetExtensions.Attributes;

namespace ShoppingList.Model;


[Table("UserLists")]
public class UserList : ObservableObject 
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("targetStore")]
    public string TargetStore { get; set; }

    private decimal _totalPrice; 

    public decimal TotalPrice { 
	    get 
	    {
            _totalPrice = 0m;
	        foreach(var item in Items)
            {
                _totalPrice += item.EstimatedPrice;
	        }
            return _totalPrice;
	    } 
    }

    
    [OneToMany]
    public List<Item> Items { get; set; }

    public enum ListType
    {
        WeeklyGrocery,
        ForAParty,
        OutOfSnacks
    }

    [Column("type")]
    public ListType Type { get; set; }

    [Column("creation_dt")]
    public DateTime CreationDate { get; set; }

    [Column("archive_dt")]
    public DateTime? ArchiveDate { get; set; }

    public UserList()
    {
        Items = new(); 
    }

    public UserList(UserList ul)
    {
        this.Id = ul.Id;
        this.Name = ul.Name;
        this.TargetStore = ul.TargetStore;
        this.Items = ul.Items;
    }

    public UserList(string name, string targetStore)
    {
        Name = name;
        TargetStore = targetStore;
        Items = new(); 
    }
}
