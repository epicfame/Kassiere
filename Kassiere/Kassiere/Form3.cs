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
    public partial class Form3 : Form
    {
        Repository r = new Repository();
        Staff s;
        public Form3(String StaffEmail)
        {
            InitializeComponent();
            tampilLaporan.Enabled = false;
            s = r.GetStaff(StaffEmail);
            StaffIDDisplay.Text = s.StaffName;
        }

        private void membuatOrder_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String date1 = this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");
            String date2 = this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");
            dataGridView1.DataSource = s.ShowReport(r, date1, date2);
        }
    }
}
