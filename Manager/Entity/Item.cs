using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Entity
{
    public class Item
    {
        public Item(int rowId, int amount, string location, double unitPrice, string date)
        {
            RowId = rowId;
            Amount = amount;
            Location = location;
            UnitPrice = unitPrice;
            Date = date;
        }
        public Item()
        {

        }
        public int RowId { get; set; }
        public int Amount { get; set; }
        public string Location { get; set; }
        public double UnitPrice { get; set; }
        public string Date { get; set; }
    }
}
