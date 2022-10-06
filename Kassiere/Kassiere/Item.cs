using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassiere
{
    internal class Item
    {
        
        public String ItemID { get; set; }
        public String ItemName { get; set; }
        public int ItemPrice { get; set; }

        public String ItemCategory { get; set; }

        public Item(string itemID, string itemName, int itemPrice, string itemCategory)
        {
            ItemID = itemID;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemCategory = itemCategory;
        }
    }
}
