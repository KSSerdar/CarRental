using Core.Data;
using Core.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RentACar.Forms
{
    public partial class ContractForm : Form
    {
        string collectionnameCustomer = "Customer";
        string collectionnameCar = "Car";
        string collectionnameContract = "Contract";
        GenericRepository<Customer> _customerRepository;
        GenericRepository<Car> _carRepository;
        GenericRepository<Contract> _contractRepository;
        ObjectId _id;
        int width = 1200;
        int height = 800;
        public ContractForm()
        {
            InitializeComponent();
            _customerRepository = new GenericRepository<Customer>(collectionnameCustomer);
            _carRepository = new GenericRepository<Car>(collectionnameCar);
            _contractRepository = new GenericRepository<Contract>(collectionnameContract);
        }

        private void ContractForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = _customerRepository.GetAll();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "CustomerID";
            comboBox2.DataSource = _carRepository.Find(c => c.Situation == CarData.Available);
            comboBox2.DisplayMember = "LicencePlate";
            comboBox2.ValueMember = "CarID";
            ContractList();
        }
        private void ContractList()
        {
            var signs = _contractRepository.GetAll();
            dataGridView1.DataSource = signs.ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedcustomer = _customerRepository.FindItem(c => c.Name.Contains(comboBox1.GetItemText(comboBox1.SelectedItem)));
            var selectedcar = _carRepository.FindItem(c => c.LicencePlate.Contains(comboBox2.GetItemText(comboBox2.SelectedItem)));
            if (selectedcustomer != null && selectedcar != null)
            {
                var newcontract = new Contract()
                {
                    BeginDate = DateTime.UtcNow.AddHours(3),
                    CarID = selectedcar.CarID,
                    CustomerID = selectedcustomer.CustomerID,
                    CustomerName = selectedcustomer.Name + " " + selectedcustomer.SurName,
                    CarModel = selectedcar.CarModel,
                    CustomerGsmNumber = selectedcustomer.GsmNumber,
                    CustomerIdentityNumber = selectedcustomer.IdentityNumber,
                    CarLicence = selectedcar.LicencePlate,
                    CarDailyPrice = selectedcar.DailyPrice
                };
                _contractRepository.Insert(newcontract);
                selectedcar.Situation = CarData.Nonavailable;
                _carRepository.Update(selectedcar.CarID, selectedcar);
                ContractList();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PaymentForm paymentForm = new PaymentForm(_id);
            paymentForm.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                _id = ObjectId.Parse(dataGridViewRow.Cells[0].Value.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var name = _contractRepository.Find(c => c.CustomerName.Contains(textBox1.Text));
            dataGridView1.DataSource = name;
        }

        private void ContractForm_SizeChanged(object sender, EventArgs e)
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
