using MySql.Data.MySqlClient;
using Scheduler.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class ReportForm : Form
    {
        // ---------- Form Constructor ----------
        public ReportForm()
        {
            InitializeComponent();
        }



        // ---------- Form Events ----------
        private void ReportForm_Load(object sender, EventArgs e)
        {
            textRpt.SelectionTabs = new int[] { 150, 300, 450, 600 };
        }



        // ---------- Form Functions ----------
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // Reports: number of appointment types by month
        private void btnRpt1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = "SELECT year(start) as year, month(start) as month, type, COUNT(type) as count" +
                    " FROM appointment GROUP BY year(start), MONTH(start), type" +
                    " ORDER BY year(start) ASC, MONTH(start) ASC, type ASC";
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(Database.Query, sql)
                };
                DataTable dt = new DataTable();
                da.Fill(dt);
                textRpt.Text = "Appointment types shown by year & month:\n\nYear-Month\tTotal\tAppointment type\n";
                textRpt.Text += "-----------------------------------------------------------------\n";
                foreach (DataRow row in dt.Rows)
                {
                    textRpt.Text += row.Field<int>("year").ToString() + "-";
                    textRpt.Text += CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(row.Field<int>("month")) + "\t";
                    textRpt.Text += row.Field<Int64>("count").ToString() + "\t";
                    textRpt.Text += row.Field<string>("type") + "\n";
                }
                textRpt.Text += "-----------------------------------------------------------------\nReport finished";
            }
        }

        // Reports: schedule for each consultant
        private void btnRpt2_Click(object sender, EventArgs e)
        {
            List<string> users = new List<string>();
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = "SELECT DISTINCT userName FROM appointment INNER JOIN user ON appointment.userId = user.userId" +
                    " WHERE start >= UTC_TIMESTAMP() ORDER BY userName ASC";
                MySqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = Database.Query;
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add((string)reader["userName"]);
                }
                reader.Close();
                Database.Query = "SELECT userName, type, start, end FROM appointment INNER JOIN user ON appointment.userId = user.userId" +
                    " WHERE start >= UTC_TIMESTAMP() ORDER BY userName ASC, start ASC";
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(Database.Query, sql)
                };
                DataTable dt = new DataTable();
                da.Fill(dt);
                textRpt.Text = "Upcoming schedule for each consultant:\n";
                textRpt.Text += "-----------------------------------------------------------------\n\n";

                users.ForEach(user => {  // Lamba used here to iterate through the users (consultants) list instead of using a foreach loop
                    textRpt.Text += "Consultant: " + user + "\n";
                    textRpt.Text += "Start\tEnd\tType\n";
                    textRpt.Text += "-----------------------------------------------------------------\n";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.Field<string>("userName") == user)
                        {
                            textRpt.Text += row.Field<DateTime>("start").ToLocalTime().ToString("M/dd/yy HH:mm tt") + "\t";
                            textRpt.Text += row.Field<DateTime>("end").ToLocalTime().ToString("M/dd/yy HH:mm tt") + "\t";
                            textRpt.Text += row.Field<string>("type") + "\n";
                        }
                    }
                    textRpt.Text += "\n";
                }); // End of lambda
                textRpt.Text += "-----------------------------------------------------------------\nReport finished";
            }
        }

        // Reports: additional report of your choice
        private void btnRpt3_Click(object sender, EventArgs e)
        {
            int completed, pending;
            List<string> users = new List<string>();
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = "SELECT DISTINCT userName FROM appointment INNER JOIN user ON appointment.userId = user.userId";
                MySqlCommand cmd = sql.CreateCommand();
                cmd.CommandText = Database.Query;
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add((string)reader["userName"]);
                }
                reader.Close();
                Database.Query = "SELECT userName, start, end FROM appointment INNER JOIN user ON appointment.userId = user.userId" +
                    " ORDER BY userName ASC";
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(Database.Query, sql)
                };
                DataTable dt = new DataTable();
                da.Fill(dt);
                textRpt.Text = "Total appointments for each consultant:\n";
                textRpt.Text += "-----------------------------------------------------------------\n\n";
                users.ForEach(user => {  // Lamba used here to iterate through the users (consultants) list instead of using a foreach loop
                    completed = 0;
                    pending = 0;
                    textRpt.Text += "Consultant:\t" + user + "\n";
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.Field<string>("userName") == user)
                        {
                            if (System.DateTime.UtcNow >= row.Field<DateTime>("end"))
                            {
                                completed++;
                            }
                            else
                            {
                                pending++;
                            }
                        }
                    }
                    textRpt.Text += "Completed:\t" + completed + "\n";
                    textRpt.Text += "Pending:\t" + pending + "\n";
                    textRpt.Text += "Total:\t" + (completed + pending) + "\n\n";
                }); // End of lambda
                textRpt.Text += "-----------------------------------------------------------------\nReport finished";
            }
        }
    }
}
