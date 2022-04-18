using MySql.Data.MySqlClient;
using Scheduler.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class CustForm : Form
    {
        // ---------- Form Constructor ----------
        public CustForm()
        {
            InitializeComponent();
        }



        // ---------- Form Events ----------
        private void CustForm_Activated(object sender, EventArgs e)
        {
            loadData();
        }
        
        private void CustForm_Load(object sender, EventArgs e)
        {
            loadData();
        }



        // ---------- Form Functions ----------
        // Add, update, and delete customer records
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustForm addCustForm = new AddCustForm();
            addCustForm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgCust.CurrentRow.Selected)
            {
                using (MySqlConnection sql = Database.Connect())
                {
                    Database.Query = string.Format(
                        "DELETE FROM customer WHERE customerId = '{0}'", dgCust.CurrentRow.Cells["customerId"].Value);
                    MySqlCommand cmd = new MySqlCommand(Database.Query, sql);
                    cmd.ExecuteNonQuery();
                }
                loadData();
            }
            else
            {
                MessageBox.Show("No record selected", "ERROR");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgCust.CurrentRow.Selected)
            {
                Customer customer = new Customer
                {
                    customerId = Int32.Parse(dgCust.CurrentRow.Cells["customerId"].Value.ToString()),
                    Name = dgCust.CurrentRow.Cells["customerName"].Value.ToString(),
                    Address = dgCust.CurrentRow.Cells["address"].Value.ToString(),
                    Address2 = dgCust.CurrentRow.Cells["address2"].Value.ToString(),
                    City = dgCust.CurrentRow.Cells["city"].Value.ToString(),
                    PostalCode = dgCust.CurrentRow.Cells["postalCode"].Value.ToString(),
                    Country = dgCust.CurrentRow.Cells["country"].Value.ToString(),
                    Phone = dgCust.CurrentRow.Cells["phone"].Value.ToString()
                };
                AddCustForm addCustForm = new AddCustForm(customer);
                addCustForm.Show();
            }
            else
            {
                MessageBox.Show("No record selected", "ERROR");
            }
        }



        // ---------- Functions ----------
        private void loadData()
        {
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = string.Format(
                    "SELECT customerId, customerName, customer.addressId, address, address2, address.cityId, city, postalCode, country, city.countryId, phone FROM customer" +
                    " INNER JOIN address AS address ON customer.addressId = address.addressId" +
                    " INNER JOIN city AS city ON address.cityId = city.cityId" +
                    " INNER JOIN country AS country ON city.countryId = country.countryId");
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(Database.Query, sql)
                };
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgCust.DataSource = dt;
                dgCust.Columns["customerName"].HeaderText = "Name";
                dgCust.Columns["address"].HeaderText = "Address";
                dgCust.Columns["address2"].HeaderText = "Address 2";
                dgCust.Columns["city"].HeaderText = "City";
                dgCust.Columns["postalCode"].HeaderText = "Postal Code";
                dgCust.Columns["country"].HeaderText = "Country";
                dgCust.Columns["phone"].HeaderText = "Phone";
                dgCust.Columns["customerId"].Visible = false;
                dgCust.Columns["addressId"].Visible = false;
                dgCust.Columns["cityId"].Visible = false;
                dgCust.Columns["countryId"].Visible = false;
                dgCust.AutoResizeColumns();
            }
        }
    }
}
