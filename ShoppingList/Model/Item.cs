using SQLite;

namespace ShoppingList.Model;

[Table("Items")] 
public class Item : ObservableObject 
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("category")]
    public string Category { get; set; }

    [Column("aisle")]
    public string Aisle { get; set; }

    [Column("estimated price")]
    public decimal EstimatedPrice { get; set; }

    [ForeignKey(typeof(UserList))]
    [Column("parentid")]
    public int ParentId { get; set; }

    [Column("iscompleted")]
    public bool IsCompleted { get; set; }

    [OneToOne]
    [Column("itemlocationdata")]
    public ItemLocationData LocationData { get; set; }


    public Item()
    { 
    }

    public Item(Item item)
    {
        this.Name = item.Name;
		this.Description = item.Description;
		this.Category = item.Category;
		this.EstimatedPrice = item.EstimatedPrice;
        this.ParentId = item.ParentId; 

    }

    public Item(Item item, ItemLocationData ild)
    {
        this.Name = item.Name;
		this.Description = item.Description;
		this.Category = item.Category;
		this.EstimatedPrice = item.EstimatedPrice;
        this.ParentId = item.ParentId; 

		this.LocationData = ild;
		this.Aisle = ild.Description;
    
    }
}
