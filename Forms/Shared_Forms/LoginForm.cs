using System; // Provides access to fundamental classes and base types.
using System.Data.SqlClient; // Facilitates interaction with SQL Server.
using System.Windows.Forms; // Enables the creation of Windows-based applications.

namespace HMS_APP_001
{
    // Represents the login form for the application.
    public partial class LoginForm : Form
    {
        // Initializes the form's components.
        public LoginForm()
        {
            InitializeComponent();
        }

        // Handles the click event for the login button.
        private void btnLogin_Click(object sender, EventArgs e)
        {   // Validates that both User ID and Password fields are not empty.
            if (string.IsNullOrWhiteSpace(txtUserID.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a User ID and Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Retrieves the User ID and Password from text fields.
            string UserID = txtUserID.Text;
            string password = txtPassword.Text;
            int userId;

            // Validates that the User ID is a numerical value.
            if (!int.TryParse(UserID, out userId))
            {
                MessageBox.Show("User ID must be a number.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Establishes a database connection.
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            connection.Open();

            // SQL query to verify credentials for both Admins and Doctors.
            string query = @"
                SELECT 'Admin' AS UserType FROM Admin WHERE AdminID = @userID AND Password = @password
                UNION ALL
                SELECT 'Doctor' AS UserType FROM Doctor WHERE DoctorID = @userID AND Password = @password
            ";

            // Executes the query with parameters to prevent SQL injection.
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", userId);
            command.Parameters.AddWithValue("@password", password);

            // Retrieves the user's role if credentials are valid.
            object result = command.ExecuteScalar();

            // Directs the user to the appropriate form based on their role.
            if (result != null)
            {
                string userType = result.ToString();
                if (userType == "Admin")
                {
                    Admin_PatientManagement adminForm = new Admin_PatientManagement();
                    adminForm.Show();
                    this.Hide();
                }
                else if (userType == "Doctor")
                {
                    LoggedInUser.DoctorID = userId;
                    Doctor_AppointmentManagement doctorForm = new Doctor_AppointmentManagement();
                    doctorForm.Show();
                    this.Hide();
                }
            }
            // Displays an error if login fails.
            else
            {
                MessageBox.Show("Invalid User ID or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Closes the database connection.
            connection.Close();
        }

        // Unused event handlers for text changes.
        private void txtUserID_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
    }
}