using Core.Data;
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


namespace RentACar.Forms
{
    public partial class CarForm : Form
    {
        string collectionname = "Car";
        GenericRepository<Car> repository;
        ObjectId _id;
        int width = 1200;
        int height = 800;
        public CarForm()
        {
            InitializeComponent();
            repository = new GenericRepository<Car>(collectionname);
        }

        private void CarForm_Load(object sender, EventArgs e)
        {
            CarList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var c = new Car()
            {
                CarBrand = textBox1.Text,
                CarModel = textBox2.Text,
                DailyPrice = Convert.ToDecimal(textBox3.Text),
                LicencePlate = textBox10.Text,
                Situation = CarData.Available
            };

            var a = repository.FindItem(b => b.CarModel == c.CarModel);

            if (a == null)
            {
                repository.Insert(c);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox10.Text = "";
                CarList();
            }
            else { MessageBox.Show("Item Already Exist"); }
        }
        private void CarList()
        {

            var list = repository.GetAll();
            dataGridView1.DataSource = list.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var query = repository.GetById(_id);

            if (query != null)
            {
                repository.Delete(_id);
                CarList();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                _id = ObjectId.Parse(dataGridViewRow.Cells[0].Value.ToString());
                var a = repository.FindItem(c => c.CarID == _id);
                textBox6.Text = a.CarBrand;
                textBox9.Text = a.CarBrand;
                textBox11.Text = a.LicencePlate;
                textBox12.Text = a.LicencePlate;
                textBox5.Text = a.CarModel;
                textBox8.Text = a.CarModel;
                textBox4.Text = a.DailyPrice.ToString();
                textBox7.Text = a.DailyPrice.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var query = repository.GetById(_id);
            if (query != null)
            {
                query.CarBrand = textBox9.Text;
                query.CarModel = textBox8.Text;
                query.DailyPrice = Convert.ToDecimal(textBox7.Text);
                query.LicencePlate = textBox12.Text;
                repository.Update(_id, query);
                CarList();
            }
        }

        private void CarForm_SizeChanged(object sender, EventArgs e)
        {
            this.Location=new Point(0,0);
            this.Size=Screen.PrimaryScreen.WorkingArea.Size;
            Rectangle rectangle = new Rectangle();
            rectangle=Screen.GetBounds(rectangle);
            float a = ((float)rectangle.Width / (float)width);
            float b = ((float)rectangle.Height / (float)height);
            this.Scale(new SizeF(a, b));
        }
    }
}
