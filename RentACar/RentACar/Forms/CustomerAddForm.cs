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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar.Forms
{
    public partial class CustomerAddForm : Form
    {

        string collectionname = "Customer";
        private event EventHandler<KeyPressEventArgs> myEvent;
        GenericRepository<Customer> repository;
        ObjectId _id;
        int width = 1200;
        int height = 800;
        public CustomerAddForm()
        {
            InitializeComponent();
            repository = new GenericRepository<Customer>(collectionname);
        }
        private void ListCustomers()
        {
            var repository = new GenericRepository<Customer>(collectionname);
            dataGridView1.DataSource = repository.GetAll();
        }
        private async void button1_Click(object sender, EventArgs e)
        {

            long identitynum = long.Parse(textBox1.Text);
            try
            {
                using (TcSorgula.KPSPublicSoapClient service = new TcSorgula.KPSPublicSoapClient(TcSorgula.KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap))
                {
                    var situation = await service.TCKimlikNoDogrulaAsync(identitynum, textBox2.Text, textBox3.Text, Convert.ToInt32(textBox4.Text));

                    if (situation.Body.TCKimlikNoDogrulaResult)
                    {
                        var newCustomer = new Customer()
                        {
                            Name = textBox2.Text,
                            SurName = textBox3.Text,
                            IdentityNumber = textBox1.Text,
                            BirthYear = textBox4.Text,
                            GsmNumber = textBox5.Text
                        };
                        repository.Insert(newCustomer);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Girdiginiz Bilgilere Ait Bir Vatandas Bulunmamakta");
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void HandleNumeric(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Sadece Rakam Girilebilir");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            myEvent += HandleNumeric;
            OnButtonPressed(e);
            myEvent -= HandleNumeric;

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            myEvent += HandleNumeric;
            OnButtonPressed(e);
            myEvent -= HandleNumeric;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            myEvent += HandleNumeric;
            OnButtonPressed(e);
            myEvent -= HandleNumeric;

        }
        private void OnButtonPressed(KeyPressEventArgs e)
        {
            myEvent?.Invoke(this, e);
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            ListCustomers();
        }

        private void CustomerAddForm_SizeChanged(object sender, EventArgs e)
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
