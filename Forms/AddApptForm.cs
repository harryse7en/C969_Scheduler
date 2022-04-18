using MySql.Data.MySqlClient;
using Scheduler.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class AddApptForm : Form
    {
        // ---------- Variables ----------
        private bool editMode = false; // Used to change form to Modify Appointment mode
        private Appointment appt;
        private DataTable dt;



        // ---------- Form Constructor ----------
        public AddApptForm() // Add Appointment mode
        {
            appt = new Appointment();
            InitializeComponent();
        }

        public AddApptForm(Appointment appt) // Modify Appointment mode
        {
            this.appt = appt;
            this.editMode = true;
            InitializeComponent();
            this.Text = "Modify Appointment";
            labelTitle.Text = "Modify Appointment";
            comboName.SelectedIndex = comboName.FindString(appt.Name);
            comboType.SelectedItem = appt.Type;
            textTitle.Text = appt.Title;
            textDesc.Text = appt.Description;
            textLocation.Text = appt.Location;
            textContact.Text = appt.Contact;
            textUrl.Text = appt.Url;
            dtpStart.Value = appt.Start;
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "M/d/yy hh:mm tt";
            dtpEnd.Value = appt.End;
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "M/d/yy hh:mm tt";
        }



        // ---------- Form Events ----------
        private void AddApptForm_Load(object sender, EventArgs e)
        {
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = string.Format(
                    "SELECT customerId, customerName FROM customer WHERE active = 1");
                MySqlDataAdapter da = new MySqlDataAdapter
                {
                    SelectCommand = new MySqlCommand(Database.Query, sql)
                };
                dt = new DataTable();
                da.Fill(dt);
                comboName.DataSource = dt;
                comboName.DisplayMember = "customerName";
                comboName.SelectedIndex = comboName.FindString(appt.Name);
            }

            List<string> types = new List<string>
            {
                "Brainstorm",
                "Consultation",
                "Discussion",
                "Evaluation",
                "Inspection",
                "Presentation",
                "Risk Analysis",
                "Scrum",
                "Team Update",
                "Training"
            };
            comboType.DataSource = types;
            comboName.SelectedIndex = 0;
            comboType.SelectedIndex = 0;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            dtpEnd.Value = dtpStart.Value.AddMinutes(15);
        }




        // ---------- Form Functions ----------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            appt.Name = dt.Rows[comboName.SelectedIndex].Field<string>("customerName");
            appt.customerId = dt.Rows[comboName.SelectedIndex].Field<int>("customerId");
            appt.Type = comboType.SelectedItem.ToString();
            appt.Title = textTitle.Text;
            appt.Description = textDesc.Text;
            appt.Location = textLocation.Text;
            appt.Contact = textContact.Text;
            appt.Url = textUrl.Text;
            appt.Start = dtpStart.Value;
            appt.End = dtpEnd.Value;

            if (doValidate())
            {
                using (MySqlConnection sql = Database.Connect())
                {
                    MySqlCommand cmd = new MySqlCommand("", sql);
                    if (this.editMode) // Modify Appointment mode
                    {
                        // Update appointment record
                        Database.Query = string.Format(
                            "UPDATE appointment SET customerId = '{0}', title = '{1}', description = '{2}', location = '{3}', contact = '{4}'," +
                            " type = '{5}', url = '{6}', start = '{7}', end = '{8}', lastUpdate = UTC_TIMESTAMP(), lastUpdateBy = '{9}', userId = '{10}'" +
                            " WHERE appointmentId = '{11}'", appt.customerId, appt.Title, appt.Description, appt.Location, appt.Contact,
                            appt.Type, appt.Url, appt.Start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"), appt.End.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"), MainForm.user, MainForm.userId, appt.appointmentId);
                        cmd.CommandText = Database.Query;
                        cmd.ExecuteNonQuery();
                        bool remFound = false;
                        foreach (Reminder rem in MainForm.listReminders)
                        {
                            if (rem.ID == appt.appointmentId)
                            {
                                rem.RemindTime = appt.Start.AddMinutes(-15);
                                rem.StartTime = appt.Start;
                                rem.EndTime = appt.End;
                                rem.Triggered = false;
                                remFound = true;
                            }
                        }
                        if (!remFound)
                        {
                            Reminder rem = new Reminder();
                            rem.ID = appt.appointmentId;
                            rem.Name = appt.Name;
                            rem.Type = appt.Type;
                            rem.RemindTime = appt.Start.AddMinutes(-15);
                            rem.StartTime = appt.Start;
                            rem.EndTime = appt.End;
                            rem.Triggered = false;
                            MainForm.listReminders.Add(rem);
                        }
                    }
                    else // Add Appointment mode
                    {
                        // Create new appointment record
                        Database.Query = string.Format(
                            "INSERT INTO appointment (customerId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy, userId)" +
                            " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', UTC_TIMESTAMP(), '{9}', UTC_TIMESTAMP(), '{9}', '{10}')", appt.customerId, appt.Title, appt.Description,
                            appt.Location, appt.Contact, appt.Type, appt.Url, appt.Start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"), appt.End.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"), MainForm.user, MainForm.userId);
                        cmd.CommandText = Database.Query;
                        cmd.ExecuteNonQuery();
                        Reminder rem = new Reminder();
                        rem.Name = appt.Name;
                        rem.Type = appt.Type;
                        rem.RemindTime = appt.Start.AddMinutes(-15);
                        rem.StartTime = appt.Start;
                        rem.EndTime = appt.End;
                        rem.Triggered = false;
                        Database.Query = "SELECT LAST_INSERT_ID() FROM appointment LIMIT 1";
                        cmd.CommandText = Database.Query;
                        rem.ID = Int32.Parse(cmd.ExecuteScalar().ToString());
                        MainForm.listReminders.Add(rem);
                    }
                }
                this.Close();
            }
        }



        // ---------- Functions ----------
        private bool doValidate()
        {
            if (appt.Name == "" || appt.Type == "")
            {
                MessageBox.Show("Required fields cannot be empty", "ERROR");
                return false;
            }

            if (appt.End < appt.Start)
            {
                MessageBox.Show("Appointment end time cannot be earlier than start time", "ERROR");
                return false;
            }

            // Exception control: appointment outside business hours
            if (appt.Start.ToLocalTime().Hour < 6 || appt.End.ToLocalTime().Hour > 17 ||
                appt.Start.ToLocalTime().Hour > 17)
            {
                MessageBox.Show("Appointment cannot be booked outside of business hours.  Please book the appointment between 6 AM and 6 PM", "ERROR");
                return false;
            }

            // Exception control: overlapping appointments
            // Check existing records to ensure no overlap will occur
            using (MySqlConnection sql = Database.Connect())
            {
                MySqlCommand cmd = new MySqlCommand("", sql);
                if (this.editMode) // Modify Appointment mode
                {
                    // Update appointment record
                    Database.Query = string.Format(
                        "SELECT * FROM appointment WHERE userId = '{0}' AND start >= '{1}' AND end <= '{2}' AND appointmentId <> '{3}'",
                        MainForm.userId, appt.Start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"), appt.End.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"), appt.appointmentId);
                    cmd.CommandText = Database.Query;
                    if (cmd.ExecuteScalar() != null)
                    {
                        MessageBox.Show("This appointment will overlap with a previous appointment. Please change the start or end date to avoid overlapping", "ERROR");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else // Add Appointment mode
                {
                    // Create new appointment record
                    Database.Query = string.Format(
                        "SELECT * FROM appointment WHERE userId = '{0}' AND start >= '{1}' AND end <= '{2}'",
                        MainForm.userId, appt.Start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"), appt.End.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"));
                    cmd.CommandText = Database.Query;
                    cmd.ExecuteNonQuery();
                    if (cmd.ExecuteScalar() != null)
                    {
                        MessageBox.Show("This appointment will overlap with a previous appointment. Please change the start or end date to avoid overlapping", "ERROR");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
