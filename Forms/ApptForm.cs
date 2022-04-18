using MySql.Data.MySqlClient;
using Scheduler.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class ApptForm : Form
    {
        // ---------- Variables ----------
        private string queryStart, queryEnd;



        // ---------- Form Constructor ----------
        public ApptForm()
        {
            InitializeComponent();
            radioAll.Checked = true;
        }



        // ---------- Form Events ----------
        private void ApptForm_Activated(object sender, EventArgs e)
        {
            loadData();
        }

        private void ApptForm_Load(object sender, EventArgs e)
        {
            radioAll.Checked = true;
            loadData();
        }

        private void calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            loadData();
        }

        private void radioAll_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void radioDay_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void radioMonth_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void radioWeek_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }



        // ---------- Form Functions ----------
        // Add, update, and delete appointments
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddApptForm addApptForm = new AddApptForm();
            addApptForm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgAppt.CurrentRow.Selected)
            {
                using (MySqlConnection sql = Database.Connect())
                {
                    Database.Query = string.Format(
                        "DELETE FROM appointment WHERE appointmentId = '{0}'", dgAppt.CurrentRow.Cells["appointmentId"].Value);
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
            if (dgAppt.CurrentRow.Selected)
            {
                Appointment appt = new Appointment
                {
                    appointmentId = Int32.Parse(dgAppt.CurrentRow.Cells["appointmentId"].Value.ToString()),
                    customerId = Int32.Parse(dgAppt.CurrentRow.Cells["customerId"].Value.ToString()),
                    Name = dgAppt.CurrentRow.Cells["customerName"].Value.ToString(),
                    Type = dgAppt.CurrentRow.Cells["type"].Value.ToString(),
                    Title = dgAppt.CurrentRow.Cells["title"].Value.ToString(),
                    Description = dgAppt.CurrentRow.Cells["description"].Value.ToString(),
                    Location = dgAppt.CurrentRow.Cells["location"].Value.ToString(),
                    Contact = dgAppt.CurrentRow.Cells["contact"].Value.ToString(),
                    Url = dgAppt.CurrentRow.Cells["url"].Value.ToString(),
                    Start = DateTime.Parse(dgAppt.CurrentRow.Cells["start"].Value.ToString()),
                    End = DateTime.Parse(dgAppt.CurrentRow.Cells["end"].Value.ToString())
                };
                AddApptForm addApptForm = new AddApptForm(appt);
                addApptForm.Show();
            }
            else
            {
                MessageBox.Show("No record selected", "ERROR");
            }
        }



        // ---------- Functions ----------
        private void loadData()
        {
            // View the calendar by month and by week
            if (radioMonth.Checked)
            {
                viewByMonth();
            }
            else if (radioWeek.Checked)
            {
                viewByWeek();
            }
            else if (radioDay.Checked)
            {
                viewByDay();
            }
            else
            {
                viewAll();
            }
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = string.Format(
                    "SELECT appointmentId, appointment.customerId, customer.customerName, type, title, description, location, contact, url, start, end FROM appointment" +
                    " INNER JOIN customer ON appointment.customerId = customer.customerId" +
                    " WHERE userId = {0} AND (start BETWEEN '{1}' AND '{2}' OR end BETWEEN '{1}' AND '{2}')", MainForm.userId, queryStart, queryEnd);
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(Database.Query, sql)
                };
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    // Adjust appointment times based on user time zones and daylight-saving time
                    row.SetField<DateTime>("start", row.Field<DateTime>("start").ToLocalTime());
                    row.SetField<DateTime>("end", row.Field<DateTime>("end").ToLocalTime());
                }

                dgAppt.DataSource = dt;
                dgAppt.Columns["customerName"].HeaderText = "Customer Name";
                dgAppt.Columns["type"].HeaderText = "Appt. Type";
                dgAppt.Columns["title"].HeaderText = "Title";
                dgAppt.Columns["start"].HeaderText = "Start Time";
                dgAppt.Columns["end"].HeaderText = "End Time";
                dgAppt.Columns["appointmentId"].Visible = false;
                dgAppt.Columns["customerId"].Visible = false;
                dgAppt.Columns["description"].Visible = false;
                dgAppt.Columns["location"].Visible = false;
                dgAppt.Columns["contact"].Visible = false;
                dgAppt.Columns["url"].Visible = false;
                dgAppt.Columns["start"].DefaultCellStyle.Format = "M'/'d'/'yy hh:mm tt";
                dgAppt.Columns["end"].DefaultCellStyle.Format = "M'/'d'/'yy hh:mm tt";
                dgAppt.AutoResizeColumns();
            }
        }

        private void viewAll()
        {
            queryStart = "2000-01-01 00:00:00";
            queryEnd = "2099-12-31 23:59:59";
            calendar.RemoveAllBoldedDates();
            calendar.UpdateBoldedDates();
        }

        private void viewByDay()
        {
            DateTime _dtSel = calendar.SelectionStart.ToUniversalTime();
            queryStart = _dtSel.ToString("yyyy-MM-dd HH:mm");
            queryEnd = _dtSel.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm");
            calendar.RemoveAllBoldedDates();
            calendar.AddBoldedDate(_dtSel.ToLocalTime());
            calendar.UpdateBoldedDates();
        }

        private void viewByMonth()
        {
            DateTime _dtSel = calendar.SelectionStart.ToUniversalTime();
            DateTime _dtTemp = _dtSel.AddDays(-_dtSel.Day + 1);
            int lastDay;
            switch (_dtSel.Month)
            {
                case 2:
                    lastDay = 28;
                    break;
                case 4:
                    lastDay = 30;
                    break;
                case 6:
                    lastDay = 30;
                    break;
                case 9:
                    lastDay = 30;
                    break;
                case 11:
                    lastDay = 30;
                    break;
                default:
                    lastDay = 31;
                    break;
            }
            queryStart = _dtTemp.ToString("yyyy-MM-dd HH:mm");
            queryEnd = _dtTemp.AddDays(lastDay).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm");
            calendar.RemoveAllBoldedDates();
            for (int i = 0; i < lastDay; i++)
            {
                calendar.AddBoldedDate(_dtTemp.ToLocalTime().AddDays(i));
            }
            calendar.UpdateBoldedDates();
        }

        private void viewByWeek()
        {
            DateTime _dtSel = calendar.SelectionStart.ToUniversalTime();
            int dow = (int)_dtSel.DayOfWeek;
            DateTime _dtTemp = _dtSel.AddDays(-dow);
            queryStart = _dtTemp.ToString("yyyy-MM-dd HH:mm");
            queryEnd = _dtTemp.AddDays(8).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm");
            calendar.RemoveAllBoldedDates();
            for (int i = 0; i < 7; i++)
            {
                calendar.AddBoldedDate(_dtTemp.ToLocalTime().AddDays(i));
            }
            calendar.UpdateBoldedDates();
        }
    }
}
