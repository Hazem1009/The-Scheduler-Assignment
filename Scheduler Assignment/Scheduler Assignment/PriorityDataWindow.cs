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
    public partial class PriorityDataWindow : Form
    {
        private int processesNumber;
        private int insertedNumber = 0;
        private MainWindow mainWindow;
        private List<Process> processList = new List<Process>();

        public PriorityDataWindow(int number, MainWindow main)
        {
            InitializeComponent();
            processesNumber = number;
            mainWindow = main;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float arrivalTime = float.Parse(richTextBox1.Text);
            float burstTime = float.Parse(richTextBox2.Text);
            int priority = int.Parse(richTextBox3.Text);
            insertedNumber++;
            if (insertedNumber == processesNumber) insertButton.Enabled = false;
            Process p = new Process(arrivalTime, burstTime, priority);
            processList.Add(p);
            string[] row = {p.name, p.arrivalTime.ToString(), p.burstTime.ToString(), p.priority.ToString()};
            dataGridView1.Rows.Add(row);
        }

        private void backButton_Click_1(object sender, EventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }
    }
}
