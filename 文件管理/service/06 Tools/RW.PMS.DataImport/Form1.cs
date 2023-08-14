namespace RW.PMS.DataImport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormImportProjectInfo prj = new FormImportProjectInfo();
            prj.ShowDialog();
        }
    }
}