using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Model
{
    [Table("ItemLocationDatas")]
    public class ItemLocationData
    {
        [AutoIncrement, PrimaryKey]
        [Column("id")]
        public int Id { get; set; }
       
        [Column("baynumber")]
        public string BayNumber { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("number")]
        public string Number { get; set; }
        
        [Column("numberoffacing")]
        public string NumberOfFacing { get; set; }
       
        [Column("side")]
        public string Side { get; set; }
        
        [Column("shelfnumber")]
        public string ShelfNumber { get; set; }
        
        [Column("shelfpositioninbay")]
        public string ShelfPositionInBay { get; set; }

        [ForeignKey(typeof(Item))]
        [Column("parentid")]
        public int ParentId { get; set; }

    }
}
