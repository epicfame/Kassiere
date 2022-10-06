using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Kassiere
{
    internal class Staff
    {
        public String StaffName { get; set; }
        public String StaffID { get; set; }
        public String StaffDOB { get; set; }
        public String StaffPassword { get; set; }
        public String StaffEmail { get; set; }
        public String StaffGender { get; set; }
        public String StaffPhoneNumber { get; set; }
        public int StaffSalary { get; set; }
        public Staff(String StaffName , String StaffID, String StaffDOB, String StaffPassword, String StaffEmail , String StaffGender, String StaffPhoneNumber, int StaffSalary)
        {
            this.StaffName = StaffName ;
            this.StaffID = StaffID ;
            this.StaffDOB = StaffDOB ;
            this.StaffPassword = StaffPassword ;
            this.StaffEmail = StaffEmail ;
            this.StaffGender = StaffGender ;
            this.StaffPhoneNumber = StaffPhoneNumber ;
            this.StaffSalary = StaffSalary ;
        }


        public Dictionary<String, Transaction> MakeOrder(Repository r , Dictionary<Item, int> itemList , Dictionary<String, Transaction> transactionList, Staff s)
        {
            String TransactionDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            String TransactionID = r.AddTransaction(itemList, transactionList, s.StaffID, TransactionDate);
            Transaction t = new Transaction(TransactionID, TransactionDate, s.StaffID, itemList);
            transactionList.Add(t.TransactionID, t);
            return transactionList;
        }

        public DataTable ShowReport(Repository r, String date1, String date2) //get report from when to when
        {
            return r.getDisplayReport(date1, date2);
        }

    }
}
