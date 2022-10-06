using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace Kassiere
{
    internal class Repository
    {
        public String StaffEmail { get; set; }
        SqlConnection con = new SqlConnection("Data Source='';Initial Catalog=Kassiere;Integrated Security=True");

        public Boolean checkLogin(String email, String password)
        {
            con.Open();
            SqlCommand query = con.CreateCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "SELECT StaffEmail,StaffPassword FROM Staff WHERE StaffEmail LIKE " +
                "'" + email + "'";
            query.ExecuteNonQuery();
            con.Close();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query);
            da.Fill(dt);
            Boolean isAccessible = false;
            foreach (DataRow row in dt.Rows)
            {
                string StaffPass = (row["StaffPassword"].ToString());
                if (StaffPass == password)
                {
                    isAccessible = true;
                }
            }
               return isAccessible;
        }

        public Staff GetStaff(String StaffEmail)
        {
            String StaffName;
            String StaffID;
            String StaffDOB;
            String StaffPassword;
            String StaffGender;
            String StaffPhoneNumber;
            int StaffSalary;
            con.Open();
            SqlCommand query = con.CreateCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "SELECT * FROM Staff WHERE StaffEmail = '" + StaffEmail + "'";
            query.ExecuteNonQuery();
            con.Close();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                StaffName = (row["StaffName"].ToString());
                StaffID = (row["StaffID"].ToString());
                StaffDOB = (row["StaffDOB"].ToString());
                StaffPassword = (row["StaffPassword"].ToString());
                StaffGender = (row["StaffGender"].ToString());
                StaffPhoneNumber = (row["StaffPhoneNumber"].ToString());
                StaffSalary = (int)row["StaffSalary"];
                Staff s = new Staff(StaffName, StaffID, StaffDOB, StaffPassword, StaffEmail, StaffGender, StaffPhoneNumber, StaffSalary);
                return s;
            }
            return new Staff("", "", "", "", "", "", "", 0);
        }

        public Dictionary<String,Transaction> GetTransaction(Dictionary<String, Transaction> transactionList)
        {
            String TransactionID;
            String TransactionDate;
            String StaffID;
            String ItemID;
            String ItemName;
            int ItemPrice;  
            int ItemStock;
            String ItemCategoryName;
            int Quantity;
            con.Open();
            SqlCommand query = con.CreateCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "SELECT th.TransactionID,StaffID,TransactionDate,td.ItemID,Quantity,i.ItemName,i.ItemPrice,i.ItemQuantity,ic.ItemName AS ItemCategoryName FROM TransactionHeader th JOIN TransactionDetail td ON th.TransactionID = td.TransactionID JOIN Item i ON td.ItemID = i.ItemID JOIN ItemCategory ic ON i.ItemCategoryID = ic.ItemCategoryID";
            query.ExecuteNonQuery();
            con.Close();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<Item,int> itemList = new Dictionary<Item, int>();
                TransactionID = (row["TransactionID"].ToString());
                TransactionDate = (row["TransactionDate"].ToString());
                StaffID = (row["StaffID"].ToString());
                ItemID = (row["ItemID"].ToString());
                Quantity = (int)row["Quantity"];
                ItemName = (row["ItemName"].ToString());
                ItemPrice = (int)row["ItemPrice"];
                ItemStock = (int)row["ItemQuantity"];
                ItemCategoryName = (row["ItemCategoryName"].ToString());
                Transaction tempId;
                Boolean hasId = transactionList.TryGetValue(TransactionID, out tempId);
                if(hasId)
                {   
                    Item t = new Item(ItemID, ItemName, ItemPrice,  ItemCategoryName);
                    tempId.TransactionItem.Add(t, Quantity);
                    transactionList[TransactionID] = tempId;
                }
                else
                {
                    itemList.Add(new Item(ItemID, ItemName, ItemPrice,  ItemCategoryName), Quantity);
                    transactionList.Add(TransactionID, new Transaction(TransactionID , TransactionDate , StaffID , itemList));
                }
            }
            return transactionList;
        }

        public String AddTransaction(Dictionary<Item,int> ItemList , Dictionary<String,Transaction> transactionList, String StaffID , String TransactionDate)
        {
            int id=0;
            foreach(KeyValuePair<String,Transaction> entry in transactionList)
            {
                int max = int.Parse(entry.Value.TransactionID.Substring(2));
                if(max>id)
                {
                    id = max;
                }
            }
            id++;
            String TransactionID = String.Format("TH{0, 0:D3}",id);
            con.Open();
            SqlCommand query = con.CreateCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "INSERT INTO TransactionHeader VALUES('" + TransactionID + "','" + StaffID + "','" + TransactionDate + "')";
            query.ExecuteNonQuery();
            con.Close();
            
            foreach (KeyValuePair<Item, int> entry in ItemList)
            {
                con.Open();
                SqlCommand query2 = con.CreateCommand();
                query2.CommandType = CommandType.Text;
                query2.CommandText = "INSERT INTO TransactionDetail VALUES('" + TransactionID + "','" + entry.Key.ItemID + "'," + entry.Value + ")";
                query2.ExecuteNonQuery();
                con.Close();
            }
            return TransactionID;
        }

        public Item getItem(String itemName)
        {
            con.Open();
            SqlCommand query = con.CreateCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "SELECT i.ItemID, i.ItemName,ItemPrice,ic.ItemName AS ItemCategoryName FROM Item i JOIN ItemCategory ic ON i.ItemCategoryID = ic.ItemCategoryID WHERE i.ItemName = '" + itemName + "'";
            query.ExecuteNonQuery();
            con.Close();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query);
            da.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
                String ItemName = (row["ItemName"].ToString());
                int ItemPrice = (int)row["ItemPrice"];
                String itemID = (row["ItemID"].ToString());
                String ItemCategoryName = (row["ItemCategoryName"].ToString());
                return new Item(itemID, ItemName, ItemPrice, ItemCategoryName);
            }
            return null;
        }

        public DataTable getDisplayItem()
        {
            con.Open();
            SqlCommand query = con.CreateCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "SELECT i.ItemName,ItemPrice,ic.ItemName AS ItemCategoryName, Quantity=0 FROM Item i JOIN ItemCategory ic ON i.ItemCategoryID = ic.ItemCategoryID";
            query.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable getDisplayReport(String date1, String date2)
        {
            con.Open();
            SqlCommand query = con.CreateCommand();
            query.CommandType = CommandType.Text;
            query.CommandText = "SELECT th.TransactionID, TransactionDate, ItemID, Quantity FROM TransactionHeader th " +
                "JOIN TransactionDetail td ON th.TransactionID = td.TransactionID " +
                "WHERE transactiondate >= '" + date1 + "' AND transactiondate<= '" + date2 + "'";

            query.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query);
            da.Fill(dt);
            con.Close();
            return dt;
        }
    }
}
