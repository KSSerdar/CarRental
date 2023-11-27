using Core.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RentACar.Forms
{
    public partial class CustomerMainForm : Form
    {
        string collectionname = "Customer";
        GenericRepository<Customer> repository;
        ObjectId _id;
        int width = 1200;
        int height = 800;
        public CustomerMainForm()
        {
            InitializeComponent();
            repository = new GenericRepository<Customer>(collectionname);

        }

        private void CustomerMainForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = repository.GetAll();
        }
        private void ListCustomers()
        {
            var list = repository.GetAll();
            dataGridView1.DataSource = list.ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var customer = repository.Find(c => c.IdentityNumber.Contains(textBox1.Text));
            dataGridView1.DataSource = customer.ToList();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var customer = repository.FindItem(c => c.CustomerID == _id);
            if (customer != null)
            {
                repository.Delete(customer.CustomerID);
                ListCustomers();
            }
            else
            {
                MessageBox.Show("Error");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CustomerAddForm customerAddForm = new CustomerAddForm();
            customerAddForm.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                _id = ObjectId.Parse(dataGridViewRow.Cells[0].Value.ToString());
                var customer = repository.FindItem(c => c.CustomerID == _id);
                textBox2.Text = customer.Name;
                textBox3.Text = customer.SurName;
                textBox4.Text = customer.GsmNumber;
                textBox5.Text = customer.IdentityNumber;
                textBox6.Text = customer.BirthYear;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var customer = repository.FindItem(c => c.CustomerID == _id);
            if (customer != null)
            {
                customer.GsmNumber = textBox4.Text;
                repository.Update(customer.CustomerID, customer);
                ListCustomers();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void CustomerMainForm_SizeChanged(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            Rectangle rectangle = new Rectangle();
            rectangle = Screen.GetBounds(rectangle);
            float a = ((float)rectangle.Width / (float)width);
            float b = ((float)rectangle.Height / (float)height);
            this.Scale(new SizeF(a, b));
        }
    }
}
