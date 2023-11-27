using Core.Data;
using Core.Entities;
using MongoDB.Bson;
using MongoRepository.Repository;
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
    public partial class PaymentForm : Form
    {
        decimal price;
        string collectionName = "Pay";
        ObjectId _id;
        GenericRepository<Pay> _payRepository;
        GenericRepository<Contract> _contractRepository;
        GenericRepository<Car> _carRepository;
        TimeSpan _timeSpan;
        int width = 1200;
        int height = 800;
        public PaymentForm(ObjectId objectId)
        {
            InitializeComponent();
            _id = objectId;
            _payRepository = new GenericRepository<Pay>(collectionName);
            _contractRepository = new GenericRepository<Contract>("Contract");
            _carRepository = new GenericRepository<Car>("Car");
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {

            var cont = _contractRepository.GetById(_id);
            if (cont != null)
            {
                textBox1.Text = cont.CustomerName;
                textBox2.Text = cont.CustomerIdentityNumber;
                textBox3.Text = cont.CustomerGsmNumber;
                textBox4.Text = cont.CarModel;
                textBox5.Text = cont.CarLicence;
                textBox6.Text = cont.BeginDate.ToString();
                textBox7.Text = DateTime.UtcNow.AddHours(3).ToString();
                _timeSpan = DateTime.UtcNow.AddHours(3) - cont.BeginDate;
                int day = _timeSpan.Days;
                int hour = _timeSpan.Hours;
                textBox8.Text = day + "Day " + hour + "Hour";
                price = cont.CarDailyPrice * day;
                label1.Text = price.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cont = _contractRepository.GetById(_id);
            if (cont != null)
            {
                var carData = _carRepository.GetById(cont.CarID);
                var sellData = new Pay()
                {
                    CustomerID = cont.CustomerID,
                    CustomerName = cont.CustomerName,
                    BeginDate = cont.BeginDate,
                    CarID = cont.CarID,
                    CarLicencePlate = cont.CarLicence,
                    CarModel = cont.CarModel,
                    ContractID = _id,
                    CustomerGsm = cont.CustomerGsmNumber,
                    CustomerIdentity = cont.CustomerIdentityNumber,
                    PaidPrice = price,
                    DeliveryTime = DateTime.UtcNow.AddHours(3)
                };
                _payRepository.Insert(sellData);
                _contractRepository.Delete(_id);
                carData.Situation = CarData.Available;
                _carRepository.Update(carData.CarID, carData);
            }
        }

        private void PaymentForm_SizeChanged(object sender, EventArgs e)
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
