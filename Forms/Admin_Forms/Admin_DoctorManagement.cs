using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_APP_001
{
    public partial class Admin_DoctorManagement : Form
    {
        // This is the constructor for the form.
        public Admin_DoctorManagement()
        {
            InitializeComponent();
            PopulateDoctorsDataGridView();
            txtDoctorID.ReadOnly = true;
        }

        // This method handles the addition of a new doctor.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHonorific.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtSpecialization.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtQualification.Text) ||
                string.IsNullOrWhiteSpace(txtDoctorPassword.Text) ||
                string.IsNullOrWhiteSpace(cmbEmploymentStatus.Text))
            {
                MessageBox.Show("All fields must be filled.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string honorific = txtHonorific.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string specialization = txtSpecialization.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string qualification = txtQualification.Text;
            string password = txtDoctorPassword.Text;
            string employmentStatus = cmbEmploymentStatus.Text;

            Database db = new Database();
            SqlConnection connection = db.GetConnection();

            string emailCheckQuery = "SELECT COUNT(*) FROM Doctor WHERE Email = @Email";
            SqlCommand emailCheckCommand = new SqlCommand(emailCheckQuery, connection);
            emailCheckCommand.Parameters.AddWithValue("@Email", email);

            connection.Open();
            int emailCount = (int)emailCheckCommand.ExecuteScalar();
            connection.Close();

            if (emailCount > 0)
            {
                MessageBox.Show("A doctor with this email already exists.", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            connection.Open();

            string insertQuery = "INSERT INTO Doctor (Honorific, First_Name, Last_Name, Specialization, Phone_No, Email, Qualification, Password, Employment_Status) VALUES (@Honorific, @FirstName, @LastName, @Specialization, @Phone, @Email, @Qualification, @Password, @EmploymentStatus)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@Honorific", honorific);
            insertCommand.Parameters.AddWithValue("@FirstName", firstName);
            insertCommand.Parameters.AddWithValue("@LastName", lastName);
            insertCommand.Parameters.AddWithValue("@Specialization", specialization);
            insertCommand.Parameters.AddWithValue("@Phone", phone);
            insertCommand.Parameters.AddWithValue("@Email", email);
            insertCommand.Parameters.AddWithValue("@Qualification", qualification);
            insertCommand.Parameters.AddWithValue("@Password", password);
            insertCommand.Parameters.AddWithValue("@EmploymentStatus", employmentStatus);

            insertCommand.ExecuteNonQuery();

            connection.Close();
            MessageBox.Show("Doctor added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PopulateDoctorsDataGridView();
            btnClear_Click(sender, e);
        }

        // This method handles the update of an existing doctor's details.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDoctorID.Text))
            {
                MessageBox.Show("Please select a doctor to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHonorific.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtSpecialization.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtQualification.Text) ||
                string.IsNullOrWhiteSpace(txtDoctorPassword.Text) ||
                string.IsNullOrWhiteSpace(cmbEmploymentStatus.Text))
            {
                MessageBox.Show("All fields must be filled for update.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string doctorId = txtDoctorID.Text;
            string honorific = txtHonorific.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string specialization = txtSpecialization.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string qualification = txtQualification.Text;
            string password = txtDoctorPassword.Text;
            string employmentStatus = cmbEmploymentStatus.Text;

            Database db = new Database();
            SqlConnection connection = db.GetConnection();

            string updateQuery = "UPDATE Doctor SET Honorific = @Honorific, First_Name = @FirstName, Last_Name = @LastName, Specialization = @Specialization, Phone_No = @Phone, Email = @Email, Qualification = @Qualification, Password = @Password, Employment_Status = @EmploymentStatus WHERE DoctorID = @DoctorID";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

            updateCommand.Parameters.AddWithValue("@Honorific", honorific);
            updateCommand.Parameters.AddWithValue("@FirstName", firstName);
            updateCommand.Parameters.AddWithValue("@LastName", lastName);
            updateCommand.Parameters.AddWithValue("@Specialization", specialization);
            updateCommand.Parameters.AddWithValue("@Phone", phone);
            updateCommand.Parameters.AddWithValue("@Email", email);
            updateCommand.Parameters.AddWithValue("@Qualification", qualification);
            updateCommand.Parameters.AddWithValue("@Password", password);
            updateCommand.Parameters.AddWithValue("@EmploymentStatus", employmentStatus);
            updateCommand.Parameters.AddWithValue("@DoctorID", doctorId);

            connection.Open();
            int rowsAffected = updateCommand.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Doctor updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulateDoctorsDataGridView();
                btnClear_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Doctor update failed. Please check the Doctor ID.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This method handles the deletion of a doctor record.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDoctorID.Text))
            {
                MessageBox.Show("Please select a doctor to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this doctor record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string doctorId = txtDoctorID.Text;
                Database db = new Database();
                SqlConnection connection = db.GetConnection();

                string deleteQuery = "DELETE FROM Doctor WHERE DoctorID = @DoctorID";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@DoctorID", doctorId);

                connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Doctor deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulateDoctorsDataGridView();
                    btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Doctor deletion failed. Please check the Doctor ID.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // This method clears all input fields on the form.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDoctorID.Text = "";
            txtHonorific.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtSpecialization.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtQualification.Text = "";
            txtDoctorPassword.Text = "";
            txtSearch.Text = "";
            cmbEmploymentStatus.Text = "Employment Status";
            PopulateDoctorsDataGridView();
        }

        // This method searches for doctors based on user input.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            DataTable dt = new DataTable();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulateDoctorsDataGridView();
                return;
            }

            string searchQuery = "SELECT * FROM Doctor WHERE First_Name LIKE @SearchTerm OR Email LIKE @SearchTerm";
            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);
            searchCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            adapter.Fill(dt);
            connection.Close();

            dgvAdminDoctors.DataSource = dt;
        }

        // This method triggers a search as the user types.
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        // This method populates form fields when a cell is clicked in the DataGridView.
        private void dgvAdminDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvAdminDoctors.Rows[e.RowIndex];

                txtDoctorID.Text = row.Cells["DoctorID"].Value.ToString();
                txtHonorific.Text = row.Cells["Honorific"].Value.ToString();
                txtFirstName.Text = row.Cells["First_Name"].Value.ToString();
                txtLastName.Text = row.Cells["Last_Name"].Value.ToString();
                txtSpecialization.Text = row.Cells["Specialization"].Value.ToString();
                txtPhone.Text = row.Cells["Phone_No"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtQualification.Text = row.Cells["Qualification"].Value.ToString();
                txtDoctorPassword.Text = row.Cells["Password"].Value.ToString();
                cmbEmploymentStatus.Text = row.Cells["Employment_Status"].Value.ToString();
            }
        }

        // This method retrieves and displays all doctor records in the DataGridView.
        private void PopulateDoctorsDataGridView()
        {
            Database db = new Database();
            SqlConnection connection = db.GetConnection();

            connection.Open();

            string query = "SELECT * FROM Doctor";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvAdminDoctors.DataSource = dt;
            dgvAdminDoctors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            connection.Close();
        }

        // This method changes the active form.
        private void ShowAndCloseForm(Form newForm)
        {
            newForm.Show();
            this.Close();
        }

        // This method navigates to the patients' management form.
        private void PatientsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_PatientManagement());
        }

        // This method brings the current form to the front.
        private void DoctorsBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        // This method navigates to the appointments management form.
        private void AppointmentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_AppointmentManagement());
        }

        // This method navigates to the medical records form.
        private void MedRecordsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_MedicalRecords());
        }

        // This method navigates to the invoices form.
        private void InvoicesBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_Invoices());
        }

        // This method navigates to the payments form.
        private void PaymentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_Payment());
        }

        // This method logs out the user and shows the login form.
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is LoginForm loginForm)
                {
                    loginForm.Show();
                    loginForm.BringToFront();
                    break;
                }
            }
            this.Close();
        }

        // These methods are placeholders for unused events.
        private void txtDoctorID_TextChanged(object sender, EventArgs e) { }
        private void txtDoctorPassword_TextChanged(object sender, EventArgs e) { }
        private void txtHonorific_TextChanged(object sender, EventArgs e) { }
        private void txtFirstName_TextChanged(object sender, EventArgs e) { }
        private void txtLastName_TextChanged(object sender, EventArgs e) { }
        private void txtSpecialization_TextChanged(object sender, EventArgs e) { }
        private void txtQualification_TextChanged(object sender, EventArgs e) { }
        private void cmbEmploymentStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
    }
}