using Core.Entities;
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
    public partial class PurchaseHistory : Form
    {
        GenericRepository<Pay> _repository;
        public PurchaseHistory()
        {
            InitializeComponent();
            _repository = new GenericRepository<Pay>("Pay");
        }

        private void PurchaseHistory_Load(object sender, EventArgs e)
        {
            var list = _repository.GetAll();
            dataGridView1.DataSource = list;
        }
    }
}
