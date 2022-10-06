using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassiere
{
    internal class Transaction
    {
        public String TransactionID { get; set; }
        public String TransactionDate { get; set; }
        public String StaffID { get; set; }
        public Dictionary<Item, int> TransactionItem { get; set; }
        public Transaction(String TransactionID , String TransactionDate , String StaffID , Dictionary<Item, int> TransactionItem)
        {
            this.TransactionID = TransactionID;
            this.TransactionDate = TransactionDate;
            this.StaffID = StaffID;
            this.TransactionItem = TransactionItem;
        }

    }
}
