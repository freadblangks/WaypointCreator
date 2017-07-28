#region

using System;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

using WaypointCreator.Properties;

#endregion

namespace WaypointCreator
{
    public partial class LoginForm : Form
    {
        #region Constructors and Destructors

        public LoginForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Settings.Default.UsingDB = false;
            Settings.Default.Save();
            LoadWaypointViewer();
        }

        private void LoadWaypointViewer()
        {
            var waypointForm = new WaypointForm();
            waypointForm.Show();
            Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Load values from settings
            HostTextBox.Text = Settings.Default.host;
            UserNameTextBox.Text = Settings.Default.username;
            PasswordTextBox.Text = Settings.Default.password;
            DatabaseTextBox.Text = Settings.Default.database;
            PortTextBox.Text = Settings.Default.port;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection())
            {
                var builder = new MySqlConnectionStringBuilder
                              {
                                  Server = HostTextBox.Text,
                                  Port = PortTextBox.Text.ToUint(),
                                  UserID = UserNameTextBox.Text,
                                  Password = PasswordTextBox.Text,
                                  Database = DatabaseTextBox.Text
                              };

                conn.ConnectionString = builder.ConnectionString;
                try
                {
                    // Try db connection.
                    conn.Open();

                    // If db connection success, save values to settings.
                    if (chkBox_SaveValues.Checked)
                    {
                        Settings.Default.host = HostTextBox.Text;
                        Settings.Default.username = UserNameTextBox.Text;
                        Settings.Default.password = PasswordTextBox.Text;
                        Settings.Default.database = DatabaseTextBox.Text;
                        Settings.Default.port = PortTextBox.Text;
                        Settings.Default.UsingDB = true;
                        Settings.Default.Save();
                    }

                    LoadWaypointViewer();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error Connecting to Database please re-enter login information." + Environment.NewLine + ex.Message);
                }
            }
        }

        #endregion
    }
}