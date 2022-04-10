namespace Scheduler_Assignment
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(richTextBox1.Text, out int result))
            {
                errorProvider1.SetError(label1, "Please enter a valid number");
            }
            else if (int.Parse(richTextBox1.Text) <= 0)
            {
                errorProvider1.SetError(label1, "Number of processes must be positive");
            }
            else if (comboBox1.SelectedItem == null)
            {
                errorProvider1.SetError(label2, "Please select an algorithm");
            }
            else
            {
                BasicDataWindow dataWindow = new BasicDataWindow(int.Parse(richTextBox1.Text));
                dataWindow.Show();
                this.Hide();
            }
        }
    }
}