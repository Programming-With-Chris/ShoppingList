using SQLite;

namespace ShoppingList.Model;

[Table("Items")] 
public class Item
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

}
