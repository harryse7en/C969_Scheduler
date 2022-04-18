using MySql.Data.MySqlClient;
using Scheduler.Classes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class AddCustForm : Form
    {
        // ---------- Variables ----------
        private bool editMode = false; // Used to change form to Modify Customer mode
        private Customer customer;
        private int countryId, cityId, addressId;



        // ---------- Form Constructor ----------
        public AddCustForm() // Add Customer mode
        {
            customer = new Customer();
            InitializeComponent();
        }

        public AddCustForm(Customer customer) // Modify Customer mode
        {
            this.customer = customer;
            this.editMode = true;
            InitializeComponent();
            this.Text = "Modify Customer";
            labelTitle.Text = "Modify Customer";
            textName.Text = customer.Name;
            textAddress.Text = customer.Address;
            textAddress2.Text = customer.Address2;
            textCity.Text = customer.City;
            textZip.Text = customer.PostalCode;
            textCountry.Text = customer.Country;
            textPhone.Text = customer.Phone;
        }



        // ---------- Form Functions ----------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            customer.Name = textName.Text;
            customer.Address = textAddress.Text;
            customer.Address2 = textAddress2.Text;
            customer.City = textCity.Text;
            customer.PostalCode = textZip.Text;
            customer.Country = textCountry.Text;
            customer.Phone = textPhone.Text;

            if (doValidate())
            {
                using (MySqlConnection sql = Database.Connect())
                {
                    // Check to see if the country already exists, or create a new record
                    Database.Query = string.Format(
                        "SELECT countryId FROM country WHERE country = '{0}'", customer.Country);
                    MySqlCommand cmd = new MySqlCommand(Database.Query, sql);
                    if (cmd.ExecuteScalar() == null)
                    {
                        string query2 = string.Format(
                            "INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                            "VALUES ('{0}', UTC_TIMESTAMP(), '{1}', UTC_TIMESTAMP(), '{1}')", customer.Country, MainForm.user);
                        MySqlCommand cmd2 = new MySqlCommand(query2, sql);
                        cmd2.ExecuteNonQuery();
                    }
                    countryId = Int32.Parse(cmd.ExecuteScalar().ToString());


                    // Check to see if the city already exists, or create a new record
                    Database.Query = string.Format(
                        "SELECT cityId FROM city WHERE city = '{0}' AND countryId = '{1}'", customer.City, countryId);
                    cmd.CommandText = Database.Query;
                    if (cmd.ExecuteScalar() == null)
                    {
                        string query2 = string.Format(
                            "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                            "VALUES ('{0}', '{1}', UTC_TIMESTAMP(), '{2}', UTC_TIMESTAMP(), '{2}')", customer.City, countryId, MainForm.user);
                        MySqlCommand cmd2 = new MySqlCommand(query2, sql);
                        cmd2.ExecuteNonQuery();
                    }
                    cityId = Int32.Parse(cmd.ExecuteScalar().ToString());


                    // Check to see if the address already exists, or create a new record
                    Database.Query = string.Format(
                        "SELECT addressId FROM address WHERE address = '{0}' AND address2 = '{1}' AND cityId = '{2}'", customer.Address, customer.Address2, cityId);
                    cmd.CommandText = Database.Query;
                    if (cmd.ExecuteScalar() == null)
                    {
                        string query2 = string.Format(
                            "INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)" +
                            " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', UTC_TIMESTAMP(), '{5}', UTC_TIMESTAMP(), '{5}')", customer.Address, customer.Address2, cityId, customer.PostalCode, customer.Phone, MainForm.user);
                        MySqlCommand cmd2 = new MySqlCommand(query2, sql);
                        cmd2.ExecuteNonQuery();
                    }
                    addressId = Int32.Parse(cmd.ExecuteScalar().ToString());


                    if (this.editMode) // Modify Customer mode
                    {
                        // Update customer record
                        Database.Query = string.Format(
                            "UPDATE customer SET customerName = '{0}', addressId = '{1}', lastUpdate = UTC_TIMESTAMP(), lastUpdateBy = '{2}'" +
                            " WHERE customerId = '{3}'", customer.Name, addressId, MainForm.user, customer.customerId);
                        cmd.CommandText = Database.Query;
                        cmd.ExecuteNonQuery();
                    }
                    else // Add Customer mode
                    {
                        // Create new customer record
                        Database.Query = string.Format(
                            "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)" +
                            " VALUES ('{0}', '{1}', 1, UTC_TIMESTAMP(), '{2}', UTC_TIMESTAMP(), '{2}')", customer.Name, addressId, MainForm.user);
                        cmd.CommandText = Database.Query;
                        cmd.ExecuteNonQuery();
                    }
                }
                this.Close();
            }
        }



        // ---------- Functions ----------
        // Exception control: invalid customer data
        private bool doValidate()
        {
            if (customer.Name == "" || customer.Address == "" ||
                customer.City == "" || customer.PostalCode == "" ||
                customer.Country == "" || customer.Phone == "")
            {
                MessageBox.Show("Required fields cannot be empty","ERROR");
                return false;
            }
            return true;
        }
    }
}
