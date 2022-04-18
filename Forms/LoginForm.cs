using MySql.Data.MySqlClient;
using Scheduler.Classes;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Scheduler.Forms
{
    public partial class LoginForm : Form
    {
        // ---------- Form Constructor ----------
        public LoginForm()
        {
            InitializeComponent();
            checkRegion();
            btnLogin.Enabled = false;
            // Connection info for MySql database
            Database.Server = "localhost";
            Database.Port = "3306";
            Database.DbName = "client_schedule";
            Database.Uid = "sqlUser";
            Database.Password = "Passw0rd!";
            using (MySqlConnection sql = Database.Connect())
            {
                if (Database.IsConnected != true)
                {
                    Application.Exit();
                }
            }
        }



        // ---------- Form Events ----------
        private void textPass_TextChanged(object sender, EventArgs e)
        {
            if (textUser.Text.Length > 0 && textPass.Text.Length > 0)
            {
                btnLogin.Enabled = true;
            }
            else
            {
                btnLogin.Enabled = false;
            }
            labelInvalidCred.Visible = false;
        }

        private void textUser_TextChanged(object sender, EventArgs e)
        {
            if (textUser.Text.Length > 0 && textPass.Text.Length > 0)
            {
                btnLogin.Enabled = true;
            }
            else
            {
                btnLogin.Enabled = false;
            }
            labelInvalidCred.Visible = false;
        }



        // ---------- Form Functions ----------
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (MySqlConnection sql = Database.Connect())
            {
                Database.Query = string.Format(
                    "SELECT userId FROM user WHERE userName = '{0}' AND password = '{1}' AND active = 1", textUser.Text, textPass.Text);
                MySqlCommand cmd = new MySqlCommand(Database.Query, sql);
                if (cmd.ExecuteScalar() != null)
                {
                    labelInvalidCred.Visible = false;
                    MainForm mainForm = new MainForm(textUser.Text, Int32.Parse(cmd.ExecuteScalar().ToString()));
                    // ---------- Requirement J - Track user activity by recording timestamps for user log-ins in a .txt file
                    using (StreamWriter sw = File.AppendText("userLog.txt")) // userLog.txt will be created in the project folder under bin -> Debug
                    {
                        sw.WriteLine(System.DateTime.UtcNow.ToString() + "_UTC -- Username \"" + textUser.Text + "\" logged in successfully");
                    }
                    mainForm.Show();
                    this.Hide();
                }
                // ---------- Requirement F.4 - Exception control: incorrect username and password
                else
                {
                    labelInvalidCred.Visible = true;
                    // ---------- Requirement J - Track user activity by recording timestamps for user log-ins in a .txt file
                    using (StreamWriter sw = File.AppendText("userLog.txt")) // userLog.txt will be created in the project folder under bin -> Debug
                    {
                        sw.WriteLine(System.DateTime.UtcNow.ToString() + "_UTC -- Failed login attempt for username \"" + textUser.Text + "\"");
                    }
                }
            }
        }



        // ---------- Functions ----------
        // Log-in form that can determine the user’s location and translate
        // This section was tested by changing the Format selection in Control Panel -> Region -> Format
        private void checkRegion()
        {
            string region = RegionInfo.CurrentRegion.EnglishName;
            if (region == "France") // Format set to French (France)
            {
                this.Text = "Connexion à la base de données";
                labelTitle.Text = "Connexion à la base de données";
                labelUser.Text = "Nom d'utilisateur";
                labelPass.Text = "Le mot de passe";
                btnExit.Text = "Sortir";
                btnLogin.Text = "Connexion";
                labelInvalidCred.Text = "Authentification invalide";
            }
            else if (region == "Spain") // Format set to Spanish (Spain)
            {
                this.Text = "Inicio de sesión en la base de datos";
                labelTitle.Text = "Inicio de sesión en la base de datos";
                labelUser.Text = "Nombre de usuario";
                labelPass.Text = "Clave";
                btnExit.Text = "Salida";
                btnLogin.Text = "Acceso";
                labelInvalidCred.Text = "Credenciales de acceso invalidos";
            }
            else // United States is the default region
            {
                this.Text = "Database Login";
                labelTitle.Text = "Database Login";
                labelUser.Text = "Username";
                labelPass.Text = "Password";
                btnExit.Text = "Exit";
                btnLogin.Text = "Login";
                labelInvalidCred.Text = "Invalid login credentials";
            }
        }
    }
}
