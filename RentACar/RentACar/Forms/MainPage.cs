using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar.Forms
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CarForm carForm = new CarForm();
            carForm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            CustomerMainForm customerForm = new CustomerMainForm();
            customerForm.ShowDialog();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ContractForm contractForm = new ContractForm();
            contractForm.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PurchaseHistory purchaseHistory = new PurchaseHistory();
            purchaseHistory.ShowDialog();
        }

        private void MainPage_Resize(object sender, EventArgs e)
        {


        }
    }
}
