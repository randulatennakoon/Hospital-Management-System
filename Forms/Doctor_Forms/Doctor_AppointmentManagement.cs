using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HMS_APP_001
{
    public partial class Doctor_AppointmentManagement : Form
    {
        // Initializes the form and loads appointments.
        public Doctor_AppointmentManagement()
        {
            InitializeComponent();
            LoadAppointments();
            dtpAppointmentDate.Enabled = false;
            dtpAppointmentTime.Enabled = false;
            dgvDoctorAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Retrieves and displays appointments for the logged-in doctor.
        private void LoadAppointments()
        {
            Database db = new Database();
            SqlConnection con = db.GetConnection();
            string query = @"
                SELECT
                    a.Appointment_ID,
                    a.PatientID,
                    p.First_Name,
                    p.Last_Name,
                    a.Date,
                    a.Time,
                    a.Status
                FROM
                    Appointment a
                INNER JOIN
                    Patient p ON a.PatientID = p.PatientID
                WHERE
                    a.DoctorID = @DoctorID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DoctorID", LoggedInUser.DoctorID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            con.Close();
            dgvDoctorAppointments.DataSource = dt;
        }

        // Updates the status of a selected appointment.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppointmentID.Text))
            {
                MessageBox.Show("Please select an appointment to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string status = cmbStatus.Text;
            int appointmentID = Convert.ToInt32(txtAppointmentID.Text);
            Database db = new Database();
            SqlConnection con = db.GetConnection();
            string query = "UPDATE Appointment SET Status = @Status WHERE Appointment_ID = @AppointmentID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Appointment status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAppointments();
            }
            else
            {
                MessageBox.Show("Failed to update appointment status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clears form fields and reloads all appointments.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAppointmentID.Clear();
            txtPatientID.Clear();
            txtPatientName.Clear();
            cmbStatus.Text = string.Empty;
            txtSearch.Clear();
            dtpAppointmentDate.Value = DateTime.Now;
            dtpAppointmentTime.Value = DateTime.Now;
            LoadAppointments();
        }

        // Populates form fields with data from the clicked row.
        private void dgvDoctorAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDoctorAppointments.Rows[e.RowIndex];
                string appointmentID = row.Cells["Appointment_ID"].Value.ToString();
                string patientID = row.Cells["PatientID"].Value.ToString();
                string patientFirstName = row.Cells["First_Name"].Value.ToString();
                string patientLastName = row.Cells["Last_Name"].Value.ToString();
                DateTime appointmentDate = Convert.ToDateTime(row.Cells["Date"].Value);
                string appointmentTime = row.Cells["Time"].Value.ToString();
                string appointmentStatus = row.Cells["Status"].Value.ToString();
                txtAppointmentID.Text = appointmentID;
                txtPatientID.Text = patientID;
                txtPatientName.Text = patientFirstName + " " + patientLastName;
                dtpAppointmentDate.Value = appointmentDate;
                cmbStatus.Text = appointmentStatus;
                DateTime combinedDateTime = appointmentDate.Date.Add(DateTime.Parse(appointmentTime).TimeOfDay);
                dtpAppointmentTime.Value = combinedDateTime;
            }
        }

        // Handles changes in the search text box.
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadAppointments();
            }
        }

        // Searches for a specific appointment ID.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter an Appointment ID to search.", "Search Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int appointmentID;
            if (!int.TryParse(txtSearch.Text, out appointmentID))
            {
                MessageBox.Show("Appointment ID must be a number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Database db = new Database();
            SqlConnection con = db.GetConnection();
            string query = @"
                SELECT
                    a.Appointment_ID,
                    a.PatientID,
                    p.First_Name,
                    p.Last_Name,
                    a.Date,
                    a.Time,
                    a.Status
                FROM
                    Appointment a
                INNER JOIN
                    Patient p ON a.PatientID = p.PatientID
                WHERE
                    a.DoctorID = @DoctorID AND a.Appointment_ID = @AppointmentID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DoctorID", LoggedInUser.DoctorID);
            cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                dgvDoctorAppointments.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No appointment found with that ID for the logged in doctor.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDoctorAppointments.DataSource = null;
            }
        }

        // Navigation method to show a new form and close the current one.
        private void ShowAndCloseForm(Form newForm)
        {
            newForm.Show();
            this.Close();
        }

        // Handles click event for Appointments button.
        private void AppointmentsBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        // Navigates to the Medical Records form.
        private void MedRecordsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Doctor_MedicalRecords());
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

        // Unused text changed event handlers.
        private void txtAppointmentID_TextChanged(object sender, EventArgs e) { }
        private void txtPatientID_TextChanged(object sender, EventArgs e) { }
        private void txtPatientName_TextChanged(object sender, EventArgs e) { }
        private void dtpAppointmentDate_ValueChanged(object sender, EventArgs e) { }
        private void dtpAppointmentTime_ValueChanged(object sender, EventArgs e) { }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
