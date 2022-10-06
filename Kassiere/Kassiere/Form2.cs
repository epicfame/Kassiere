using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kassiere
{
    public partial class Form2 : Form
    {
        Repository r = new Repository();

        Staff s;
        Dictionary<String,Transaction> transactionList = new Dictionary<String,Transaction>();
        int totalPrice = 0;
        public Form2(String StaffEmail)
        {
            InitializeComponent();
            updateTable();
            initData();
            s = r.GetStaff(StaffEmail);
            StaffIDDisplay.Text = "Welcome back " + s.StaffName + "!";
            membuatOrder.Enabled = false;
        }

        public void initData()
        {
            transactionList = r.GetTransaction(transactionList);
        }

        public void updateTable()
        {
            dataGridView1.DataSource = r.getDisplayItem();
        }

        public void resetData()
        {
            totalPrice = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[3].Value = 0;
            }
            label9.Text = totalPrice.ToString();
            textBox1.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = "0";
            label6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label7.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
            else
            {
                if (textBox1.Text == "")
                    dataGridView1.CurrentRow.Cells[3].Value = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int itemPrice = int.Parse(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            int itemQuantityBefore = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            int itemQuantity = int.Parse(textBox1.Text);
            totalPrice = totalPrice - (itemPrice * itemQuantityBefore);
            dataGridView1.CurrentRow.Cells[3].Value = textBox1.Text;
            totalPrice = totalPrice + (itemPrice * itemQuantity);
            label9.Text = totalPrice.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order Confirm, the total price is " + totalPrice);
            Dictionary<Item,int> itemList = new Dictionary<Item,int>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if(dataGridView1.Rows[i].Cells[3].Value.ToString() != "0")
                {
                    String itemName = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    int itemQuantity = int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    itemList.Add(r.getItem(itemName) , itemQuantity);
                }
            }
            transactionList = s.MakeOrder(r, itemList, transactionList,s);
            label9.Text = totalPrice.ToString();
            textBox1.Text = "0";
            resetData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            groupBox2.Visible = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3(s.StaffEmail);
            form.ShowDialog();
            this.Show();
        }
    }
}
