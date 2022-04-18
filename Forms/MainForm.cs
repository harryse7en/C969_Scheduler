
// ********   APPOINTMENT SCHEDULER   ********
//         by harryse7en, 18-Mar-2022
// This Windows application was created during a college course to meet requirements of a basic Scheduler using Visual Studio 2022.
// This application represents the final solution to pass the course.


using MySql.Data.MySqlClient;
using Scheduler.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class MainForm : Form
    {
        // ---------- Variables ----------
        public static int userId;
        public static string user;
        public static List<Reminder> listReminders = new List<Reminder>();



        // ---------- Form Constructor ----------
        public MainForm(string user, int userId)
        {
            InitializeComponent();
            MainForm.user = user;
            MainForm.userId = userId;
            Database.dt = new DataTable();
        }



        // ---------- Form Events ----------
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Selects all upcoming appointments to build Reminder list
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = string.Format(
                    "SELECT appointmentId, customer.customerName, type, start, end FROM appointment" +
                    " INNER JOIN customer ON appointment.customerId = customer.customerId" +
                    " WHERE userId = {0} AND start >= '{1}'", MainForm.userId, System.DateTime.Now.AddMinutes(-15).ToUniversalTime().ToString("yyyy-MM-dd HH:mm"));
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(Database.Query, sql)
                };
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Reminder rem = new Reminder
                    {
                        ID = row.Field<int>("appointmentId"),
                        Name = row.Field<string>("customerName"),
                        Type = row.Field<string>("type"),
                        RemindTime = row.Field<DateTime>("start").AddMinutes(-15).ToLocalTime(),
                        StartTime = row.Field<DateTime>("start").ToLocalTime(),
                        EndTime = row.Field<DateTime>("end").ToLocalTime(),
                        Triggered = false
                    };

                    if (!listReminders.Contains(rem))
                    {
                        listReminders.Add(rem);
                    }
                }
            }
        }

        private void tmr1000_Tick(object sender, EventArgs e)
        {
            // Alerts 15 minutes in advance of an appointment
            listReminders.ForEach(rem => {  // Lamba used here to iterate through the reminders list instead of using a foreach loop
                if (System.DateTime.Now >= rem.EndTime && !rem.Triggered)
                {
                    rem.Triggered = true;
                }
                else if (System.DateTime.Now >= rem.RemindTime && System.DateTime.Now < rem.RemindTime.AddMinutes(15) && !rem.Triggered)
                {
                    rem.Triggered = true;
                    MessageBox.Show(string.Format("You have an upcoming appointment\n\nType: {0}\nCustomer Name: {1}\nTime: {2}", rem.Type, rem.Name, rem.StartTime), "REMINDER");
                }
                else if (System.DateTime.Now >= rem.RemindTime.AddMinutes(15) && !rem.Triggered)
                {
                    rem.Triggered = true;
                    MessageBox.Show(string.Format("There is currently an appointment in progress\n\nType: {0}\nCustomer Name: {1}\nTime: {2}", rem.Type, rem.Name, rem.StartTime), "REMINDER");
                }
            }); // End of lambda
        }



        // ---------- Form Functions ----------
        private void btnAppt_Click(object sender, EventArgs e)
        {
            // Appointments
            ApptForm apptForm = new ApptForm();
            apptForm.Show();
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            // Customer Records
            CustForm custForm = new CustForm();
            custForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRpt_Click(object sender, EventArgs e)
        {
            // Reports
            ReportForm rptForm = new ReportForm();
            rptForm.Show();
        }
    }
}
