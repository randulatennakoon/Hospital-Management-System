using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_APP_001
{
    // A form for administrators to manage patient records.
    public partial class Admin_PatientManagement : Form
    {
        // Initializes the form and sets the initial state of controls.
        public Admin_PatientManagement()
        {
            InitializeComponent();
            txtPatientID.ReadOnly = true;
            PopulatePatientsDataGridView();
            dgvAdminPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        // Adds a new patient record to the database.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddressNumber.Text) ||
                string.IsNullOrWhiteSpace(txtAddressCity.Text) ||
                string.IsNullOrWhiteSpace(txtAddressStreet.Text) ||
                string.IsNullOrWhiteSpace(cmbBloodGroup.Text) ||
                string.IsNullOrWhiteSpace(cmbGender.Text))
            {
                MessageBox.Show("All fields must be filled.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            DateTime dob = dtpDOB.Value;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string number = txtAddressNumber.Text;
            string city = txtAddressCity.Text;
            string street = txtAddressStreet.Text;
            DateTime doa = dtpDOA.Value;
            string bloodGroup = cmbBloodGroup.Text;
            string gender = cmbGender.Text;

            Database db = new Database();
            SqlConnection connection = db.GetConnection();

            // Checks if the email already exists in the database.
            string emailCheckQuery = "SELECT COUNT(*) FROM Patient WHERE Email = @Email";
            SqlCommand emailCheckCommand = new SqlCommand(emailCheckQuery, connection);
            emailCheckCommand.Parameters.AddWithValue("@Email", email);

            connection.Open();
            int emailCount = (int)emailCheckCommand.ExecuteScalar();
            connection.Close();

            if (emailCount > 0)
            {
                MessageBox.Show("A patient with this email already exists.", "Duplicate Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Inserts a new patient record.
            connection.Open();

            string insertQuery = "INSERT INTO Patient (First_Name, Last_Name, Date_Of_Birth, Email, Phone_No, Number, City, Street, Date_Of_Arrival, Blood_Group, Gender) VALUES (@FirstName, @LastName, @DOB, @Email, @Phone, @Number, @City, @Street, @DOA, @BloodGroup, @Gender)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@FirstName", firstName);
            insertCommand.Parameters.AddWithValue("@LastName", lastName);
            insertCommand.Parameters.AddWithValue("@DOB", dob);
            insertCommand.Parameters.AddWithValue("@Email", email);
            insertCommand.Parameters.AddWithValue("@Phone", phone);
            insertCommand.Parameters.AddWithValue("@Number", number);
            insertCommand.Parameters.AddWithValue("@City", city);
            insertCommand.Parameters.AddWithValue("@Street", street);
            insertCommand.Parameters.AddWithValue("@DOA", doa);
            insertCommand.Parameters.AddWithValue("@BloodGroup", bloodGroup);
            insertCommand.Parameters.AddWithValue("@Gender", gender);

            insertCommand.ExecuteNonQuery();

            connection.Close();
            MessageBox.Show("Patient added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            PopulatePatientsDataGridView();
            btnClear_Click(sender, e);
        }

        // Updates an existing patient record.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Please select a patient to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtAddressNumber.Text) ||
                string.IsNullOrWhiteSpace(txtAddressCity.Text) ||
                string.IsNullOrWhiteSpace(txtAddressStreet.Text) ||
                string.IsNullOrWhiteSpace(cmbBloodGroup.Text) ||
                string.IsNullOrWhiteSpace(cmbGender.Text))
            {
                MessageBox.Show("All fields must be filled for update.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string patientId = txtPatientID.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            DateTime dob = dtpDOB.Value;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string number = txtAddressNumber.Text;
            string city = txtAddressCity.Text;
            string street = txtAddressStreet.Text;
            DateTime doa = dtpDOA.Value;
            string bloodGroup = cmbBloodGroup.Text;
            string gender = cmbGender.Text;

            Database db = new Database();
            SqlConnection connection = db.GetConnection();

            // Updates the patient record.
            string updateQuery = "UPDATE Patient SET First_Name = @FirstName, Last_Name = @LastName, Date_Of_Birth = @DOB, Email = @Email, Phone_No = @Phone, Number = @Number, City = @City, Street = @Street, Date_Of_Arrival = @DOA, Blood_Group = @BloodGroup, Gender = @Gender WHERE PatientID = @PatientID";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

            updateCommand.Parameters.AddWithValue("@FirstName", firstName);
            updateCommand.Parameters.AddWithValue("@LastName", lastName);
            updateCommand.Parameters.AddWithValue("@DOB", dob);
            updateCommand.Parameters.AddWithValue("@Email", email);
            updateCommand.Parameters.AddWithValue("@Phone", phone);
            updateCommand.Parameters.AddWithValue("@Number", number);
            updateCommand.Parameters.AddWithValue("@City", city);
            updateCommand.Parameters.AddWithValue("@Street", street);
            updateCommand.Parameters.AddWithValue("@DOA", doa);
            updateCommand.Parameters.AddWithValue("@BloodGroup", bloodGroup);
            updateCommand.Parameters.AddWithValue("@Gender", gender);
            updateCommand.Parameters.AddWithValue("@PatientID", patientId);

            connection.Open();
            int rowsAffected = updateCommand.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopulatePatientsDataGridView();
            }
            else
            {
                MessageBox.Show("Patient update failed. Please check the Patient ID.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Deletes a selected patient record.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Please select a patient to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this patient record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string patientId = txtPatientID.Text;
                Database db = new Database();
                SqlConnection connection = db.GetConnection();

                string deleteQuery = "DELETE FROM Patient WHERE PatientID = @PatientID";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@PatientID", patientId);

                connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Patient deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PopulatePatientsDataGridView();
                    btnClear_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Patient deletion failed. Please check the Patient ID.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Clears all input fields.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPatientID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddressNumber.Text = "";
            txtAddressCity.Text = "";
            txtAddressStreet.Text = "";
            txtSearch.Text = "";
            cmbBloodGroup.Text = "Blood Group";
            cmbGender.Text = "Gender";
            dtpDOB.Value = DateTime.Now;
            dtpDOA.Value = DateTime.Now;
            PopulatePatientsDataGridView();
        }

        // Searches for patients by first name, last name, or email.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            DataTable dt = new DataTable();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulatePatientsDataGridView();
                return;
            }

            string searchQuery = "SELECT * FROM Patient WHERE First_Name LIKE @SearchTerm OR Last_Name LIKE @SearchTerm OR Email LIKE @SearchTerm";
            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);
            searchCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            adapter.Fill(dt);
            connection.Close();

            dgvAdminPatients.DataSource = dt;
        }

        // Populates the DataGridView with patient data.
        private void PopulatePatientsDataGridView()
        {
            Database db = new Database();
            SqlConnection connection = db.GetConnection();

            connection.Open();

            string query = "SELECT * FROM Patient";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvAdminPatients.DataSource = dt;

            connection.Close();
        }

        // Fills the input fields when a DataGridView row is clicked.
        private void dgvAdminPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvAdminPatients.Rows[e.RowIndex];

                txtPatientID.Text = row.Cells["PatientID"].Value.ToString();
                txtFirstName.Text = row.Cells["First_Name"].Value.ToString();
                txtLastName.Text = row.Cells["Last_Name"].Value.ToString();
                dtpDOB.Value = Convert.ToDateTime(row.Cells["Date_Of_Birth"].Value);
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["Phone_No"].Value.ToString();
                txtAddressNumber.Text = row.Cells["Number"].Value.ToString();
                txtAddressCity.Text = row.Cells["City"].Value.ToString();
                txtAddressStreet.Text = row.Cells["Street"].Value.ToString();
                dtpDOA.Value = Convert.ToDateTime(row.Cells["Date_Of_Arrival"].Value);
                cmbBloodGroup.Text = row.Cells["Blood_Group"].Value.ToString();
                cmbGender.Text = row.Cells["Gender"].Value.ToString();
            }
        }

        // Hides the current form and shows a new one.
        private void ShowAndCloseForm(Form newForm)
        {
            newForm.Show();
            this.Close();
        }

        // Handles click event for the Patients button.
        private void PatientsBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        // Navigates to the Doctor Management form.
        private void DoctorsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_DoctorManagement());
        }

        // Navigates to the Appointment Management form.
        private void AppointmentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_AppointmentManagement());
        }

        // Navigates to the Medical Records form.
        private void MedRecordsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_MedicalRecords());
        }

        // Navigates to the Invoices form.
        private void InvoicesBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_Invoices());
        }

        // Navigates to the Payments form.
        private void PaymentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_Payment());
        }

        // Logs out the user and shows the login form.
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

        private void txtPatientID_TextChanged(object sender, EventArgs e) { }
        private void txtFirstName_TextChanged(object sender, EventArgs e) { }
        private void txtLastName_TextChanged(object sender, EventArgs e) { }
        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbBloodGroup_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtpDOA_ValueChanged(object sender, EventArgs e) { }
        private void dtpDOB_ValueChanged(object sender, EventArgs e) { }
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtAddressNumber_TextChanged(object sender, EventArgs e) { }
        private void txtAddressCity_TextChanged(object sender, EventArgs e) { }
        private void txtAddressStreet_TextChanged(object sender, EventArgs e) { }
        private void txtSearch_TextChanged(object sender, EventArgs e) { }
    }
}