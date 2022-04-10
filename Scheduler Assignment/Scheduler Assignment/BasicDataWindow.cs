using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler_Assignment
{
    public partial class BasicDataWindow : Form
    {
        private int processesNumber;
        private int insertedNumber = 0;

        public BasicDataWindow(int number)
        {
            InitializeComponent();
            processesNumber = number;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int arrivalTime = int.Parse(richTextBox1.Text);
            int burstTime = int.Parse(richTextBox2.Text);
            insertedNumber++;
            if (insertedNumber == processesNumber) button1.Enabled = false;
            Process p = new Process(arrivalTime, burstTime);
            string[] row = {p.name, p.arrivalTime.ToString(), p.burstTime.ToString()};
            dataGridView1.Rows.Add(row);
        }
    }
}
