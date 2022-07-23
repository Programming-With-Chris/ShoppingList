﻿using SQLite;

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

    [Ignore]
    public ItemLocationData LocationData { get; set; }
}
