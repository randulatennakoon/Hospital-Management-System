using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_APP_001
{
    public partial class Admin_AppointmentManagement : Form
    {
        // Connection string to connect to the SQL Server database.
        private readonly string connectionString = "Data Source=.;Initial Catalog=HospitalDB;Integrated Security=True";

        // Initializes the form and sets the initial state of controls.
        public Admin_AppointmentManagement()
        {
            InitializeComponent();
            DataGridViewAppoiments.CellContentClick += DataGridViewAppoiments_CellContentClick;
            txtAppointmentID.ReadOnly = true; // Make the Appointment ID field read-only
            LoadAppointments();
            GenerateAppointmentID();
        }

        // Populates the DataGridView with all appointment records from the database.
        private void LoadAppointments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Appointment", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataGridViewAppoiments.DataSource = dt;
            }
        }

        // Generates a new appointment ID by finding the maximum existing ID and incrementing it.
        private void GenerateAppointmentID()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(Appointment_ID), 0) + 1 FROM Appointment", conn);
                int newID = (int)cmd.ExecuteScalar();
                txtAppointmentID.Text = newID.ToString();
            }
        }

        // Adds a new appointment record to the database.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDoctorID.Text) || string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Please enter both Doctor ID and Patient ID.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Appointment (DoctorID, PatientID, Date, Time, Status) " +
                    "VALUES (@DoctorID, @PatientID, @Date, @Time, @Status)", conn);
                cmd.Parameters.AddWithValue("@DoctorID", txtDoctorID.Text.Trim());
                cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text.Trim());
                cmd.Parameters.AddWithValue("@Date", dtpAppointmentDate.Value);
                cmd.Parameters.AddWithValue("@Time", dtpAppointmentTime.Value);
                cmd.Parameters.AddWithValue("@Status", "Pending"); // Default status for new appointments.
                cmd.ExecuteNonQuery();
            }

            LoadAppointments(); // Refresh the data grid.
            GenerateAppointmentID(); // Prepare for next entry.
            MessageBox.Show("Appointment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Updates an existing appointment record.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppointmentID.Text))
            {
                MessageBox.Show("Please select an appointment to update.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Appointment SET DoctorID = @DoctorID, PatientID = @PatientID, Date = @Date, Time = @Time " +
                    "WHERE Appointment_ID = @AppointmentID", conn);
                cmd.Parameters.AddWithValue("@AppointmentID", txtAppointmentID.Text.Trim());
                cmd.Parameters.AddWithValue("@DoctorID", txtDoctorID.Text.Trim());
                cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text.Trim());
                cmd.Parameters.AddWithValue("@Date", dtpAppointmentDate.Value);
                cmd.Parameters.AddWithValue("@Time", dtpAppointmentTime.Text.Trim()); // Note: Use Text property for TimePicker
                int rowsAffected = cmd.ExecuteNonQuery();
                LoadAppointments();
                MessageBox.Show(rowsAffected > 0 ? "Appointment updated." : "No matching appointment found.");
            }
        }

        // Deletes an appointment record from the database after user confirmation.
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppointmentID.Text))
            {
                MessageBox.Show("Please select an appointment to delete.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this appointment?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Appointment WHERE Appointment_ID = @AppointmentID", conn);
                    cmd.Parameters.AddWithValue("@AppointmentID", txtAppointmentID.Text.Trim());
                    int rowsAffected = cmd.ExecuteNonQuery();
                    LoadAppointments();
                    MessageBox.Show(rowsAffected > 0 ? "Appointment deleted." : "No matching appointment found.");
                }

                // Optionally clear fields after deletion
                txtAppointmentID.Clear();
                txtDoctorID.Clear();
                txtPatientID.Clear();
                dtpAppointmentDate.Value = DateTime.Now;
                dtpAppointmentTime.Text = "";
            }
        }

        // Searches for appointments based on the selected filter and search value.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();
            string selectedFilter = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedFilter))
            {
                MessageBox.Show("Please select a filter from the dropdown.");
                return;
            }
            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Please enter a search value.");
                return;
            }

            string query = "";
            // Decide the query based on the selected filter
            if (selectedFilter == "Appointment")
            {
                query = "SELECT * FROM Appointment WHERE Appointment_ID = @SearchValue";
            }
            else if (selectedFilter == "Doctor")
            {
                query = "SELECT * FROM Appointment WHERE DoctorID = @SearchValue";
            }
            else if (selectedFilter == "Patient")
            {
                query = "SELECT * FROM Appointment WHERE PatientID = @SearchValue";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataGridViewAppoiments.DataSource = dt; // Bind the results to the DataGridView
            }
        }

        // Clears all input fields and reloads all appointments.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtDoctorID.Clear();
            txtPatientID.Clear();
            txtAppointmentID.Clear();

            LoadAppointments();
            MessageBox.Show("All fields have been cleared.", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Handles the click event on a cell in the DataGridView to populate the text boxes.
        private void DataGridViewAppoiments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataGridViewAppoiments.Rows[e.RowIndex];
                txtAppointmentID.Text = row.Cells["Appointment_ID"].Value.ToString();
                txtDoctorID.Text = row.Cells["DoctorID"].Value.ToString();
                txtPatientID.Text = row.Cells["PatientID"].Value.ToString();
                dtpAppointmentDate.Value = Convert.ToDateTime(row.Cells["Date"].Value);
                dtpAppointmentTime.Text = row.Cells["Time"].Value.ToString();
            }
        }

        // Populates the Doctor Name, Specialization, and Qualification fields based on the Doctor ID.
        private void txtDoctorID_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT First_Name, Last_Name, Specialization, Qualification FROM Doctor WHERE DoctorID = @DoctorID", conn);
                cmd.Parameters.AddWithValue("@DoctorID", txtDoctorID.Text.Trim());
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // If a doctor is found, set the Name and Specialty text boxes
                        txtDoctorName.Text = reader["First_Name"].ToString() + " " + reader["Last_Name"].ToString();
                        txtSpecialization.Text = reader["Specialization"].ToString();
                        txtQualification.Text = reader["Qualification"].ToString();
                    }
                    else
                    {
                        // If no doctor is found, clear the text boxes
                        txtDoctorName.Clear();
                        txtSpecialization.Clear();
                        txtQualification.Clear();
                    }
                }
            }
        }

        // Populates the Patient Name field based on the Patient ID.
        private void txtPatientID_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT First_Name, Last_Name FROM Patient WHERE PatientID = @PatientID", conn);
                cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text.Trim());
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // If a patient is found, set the Name text box
                        txtPatientName.Text = reader["First_Name"].ToString() + " " + reader["Last_Name"].ToString();
                    }
                    else
                    {
                        // If no patient is found, clear the Name text box
                        txtPatientName.Clear();
                    }
                }
            }
        }

        // Restricts user input to only digits for the Doctor ID text box.
        private void txtDoctorID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Restricts user input to only digits for the Patient ID text box.
        private void txtPatientID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Restricts user input to only digits for the search text box.
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Navigation methods to switch between different admin forms.
        private void ShowAndCloseForm(Form newForm)
        {
            newForm.Show();
            this.Close();
        }

        private void PatientsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_PatientManagement());
        }

        private void DoctorsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_DoctorManagement());
        }

        private void AppointmentsBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void MedRecordsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_MedicalRecords());
        }

        private void InvoicesBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_Invoices());
        }

        private void PaymentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_Payment());
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        // Empty event handlers from the original code.
        private void txtAppointmentID_TextChanged(object sender, EventArgs e) { }
        private void txtSearch_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
    }
}
