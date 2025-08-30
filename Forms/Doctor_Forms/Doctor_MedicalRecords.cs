using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace HMS_APP_001
{
    // A form for doctors to manage medical records.
    public partial class Doctor_MedicalRecords : Form
    {
        // Initializes the form, sets initial control states, and populates the records view.
        public Doctor_MedicalRecords()
        {
            InitializeComponent();
            txtMedicalRecordID.ReadOnly = true;
            txtPatientID.ReadOnly = true;
            txtDoctorID.ReadOnly = true;
            PopulateMedicalRecordsDataGridView();
        }
        // Adds a new medical record to the database.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppointmentID.Text) ||
                string.IsNullOrWhiteSpace(txtPatientID.Text) ||
                string.IsNullOrWhiteSpace(txtDiagnosis.Text) ||
                string.IsNullOrWhiteSpace(txtMedication.Text))
            {
                MessageBox.Show("Please fill all required fields: Appointment ID, Patient ID, Diagnosis, and Medication.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            connection.Open();

            // Verifies the appointment ID and patient ID.
            string appointmentCheckQuery = "SELECT COUNT(*) FROM Appointment WHERE Appointment_ID = @AppointmentID AND PatientID = @PatientID AND DoctorID = @DoctorID";
            SqlCommand appointmentCheckCommand = new SqlCommand(appointmentCheckQuery, connection);
            appointmentCheckCommand.Parameters.AddWithValue("@AppointmentID", txtAppointmentID.Text);
            appointmentCheckCommand.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
            appointmentCheckCommand.Parameters.AddWithValue("@DoctorID", LoggedInUser.DoctorID);
            int appointmentCount = (int)appointmentCheckCommand.ExecuteScalar();
            if (appointmentCount == 0)
            {
                MessageBox.Show("Invalid Appointment ID or Patient ID, or it does not belong to your account.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return;
            }

            // Checks for an existing medical record for the same appointment.
            string recordCheckQuery = "SELECT COUNT(*) FROM Medical_Records WHERE Appointment_ID = @AppointmentID";
            SqlCommand recordCheckCommand = new SqlCommand(recordCheckQuery, connection);
            recordCheckCommand.Parameters.AddWithValue("@AppointmentID", txtAppointmentID.Text);
            int recordCount = (int)recordCheckCommand.ExecuteScalar();
            if (recordCount > 0)
            {
                MessageBox.Show("A medical record for this appointment already exists. Use the Update button to modify it.", "Duplicate Record", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                return;
            }

            // Inserts the new medical record into the database.
            string insertQuery = "INSERT INTO Medical_Records (Medicine, Diagnosis, Prescription_Status, Status, Special_Notes, PatientID, Appointment_ID, DoctorID) VALUES (@Medicine, @Diagnosis, @PrescriptionStatus, @PatientStatus, @SpecialNotes, @PatientID, @AppointmentID, @DoctorID)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@Medicine", txtMedication.Text);
            insertCommand.Parameters.AddWithValue("@Diagnosis", txtDiagnosis.Text);
            insertCommand.Parameters.AddWithValue("@PrescriptionStatus", cmbPrescriptionStatus.Text);
            insertCommand.Parameters.AddWithValue("@PatientStatus", cmbPatientStatus.Text);
            insertCommand.Parameters.AddWithValue("@SpecialNotes", txtSpecialNotes.Text);
            insertCommand.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
            insertCommand.Parameters.AddWithValue("@AppointmentID", txtAppointmentID.Text);
            insertCommand.Parameters.AddWithValue("@DoctorID", LoggedInUser.DoctorID);

            insertCommand.ExecuteNonQuery();
            MessageBox.Show("Medical record added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            PopulateMedicalRecordsDataGridView();
            ClearInputs();
        }

        // Updates an existing medical record.
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedicalRecordID.Text))
            {
                MessageBox.Show("Please select a medical record to update.", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            connection.Open();

            string updateQuery = "UPDATE Medical_Records SET Medicine = @Medicine, Diagnosis = @Diagnosis, Prescription_Status = @PrescriptionStatus, Status = @PatientStatus, Special_Notes = @SpecialNotes WHERE Medical_ID = @MedicalID AND DoctorID = @DoctorID";
            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
            updateCommand.Parameters.AddWithValue("@Medicine", txtMedication.Text);
            updateCommand.Parameters.AddWithValue("@Diagnosis", txtDiagnosis.Text);
            updateCommand.Parameters.AddWithValue("@PrescriptionStatus", cmbPrescriptionStatus.Text);
            updateCommand.Parameters.AddWithValue("@PatientStatus", cmbPatientStatus.Text);
            updateCommand.Parameters.AddWithValue("@SpecialNotes", txtSpecialNotes.Text);
            updateCommand.Parameters.AddWithValue("@MedicalID", txtMedicalRecordID.Text);
            updateCommand.Parameters.AddWithValue("@DoctorID", LoggedInUser.DoctorID);

            int rowsAffected = updateCommand.ExecuteNonQuery();
            connection.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Medical record updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update medical record. Make sure this record belongs to your account.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            PopulateMedicalRecordsDataGridView();
            ClearInputs();
        }

        // Clears all input fields and reloads all appointments.
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            PopulateMedicalRecordsDataGridView();
        }

        // Populates the DataGridView with medical records for the logged-in doctor.
        private void PopulateMedicalRecordsDataGridView()
        {
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            connection.Open();

            string query = "SELECT * FROM Medical_Records WHERE DoctorID = @DoctorID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DoctorID", LoggedInUser.DoctorID);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvDoctorMedicalRecords.DataSource = dt;
            dgvDoctorMedicalRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connection.Close();
        }

        // Fills the input fields when a DataGridView row is clicked.
        private void dgvDoctorMedicalRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDoctorMedicalRecords.Rows[e.RowIndex];

                txtMedicalRecordID.Text = row.Cells["Medical_ID"].Value.ToString();
                txtMedication.Text = row.Cells["Medicine"].Value.ToString();
                txtDiagnosis.Text = row.Cells["Diagnosis"].Value.ToString();
                cmbPrescriptionStatus.Text = row.Cells["Prescription_Status"].Value.ToString();
                cmbPatientStatus.Text = row.Cells["Status"].Value.ToString();
                txtSpecialNotes.Text = row.Cells["Special_Notes"].Value.ToString();
                txtPatientID.Text = row.Cells["PatientID"].Value.ToString();
                txtAppointmentID.Text = row.Cells["Appointment_ID"].Value.ToString();
                txtDoctorID.Text = row.Cells["DoctorID"].Value.ToString();
            }
        }

        // Fills Doctor ID and Patient ID when Appointment ID changes.
        private void txtAppointmentID_TextChanged(object sender, EventArgs e)
        {
            string appointmentID = txtAppointmentID.Text.Trim();

            if (string.IsNullOrWhiteSpace(appointmentID))
            {
                txtDoctorID.Text = "";
                txtPatientID.Text = "";
                return;
            }

            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            string query = "SELECT DoctorID, PatientID FROM Appointment WHERE Appointment_ID = @AppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppointmentID", appointmentID);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                txtDoctorID.Text = reader["DoctorID"].ToString();
                txtPatientID.Text = reader["PatientID"].ToString();
            }
            else
            {
                txtDoctorID.Text = "";
                txtPatientID.Text = "";
            }

            reader.Close();
            connection.Close();
        }

        // Clears all input fields.
        private void ClearInputs()
        {
            txtMedicalRecordID.Text = "";
            txtAppointmentID.Text = "";
            txtPatientID.Text = "";
            txtDiagnosis.Text = "";
            txtMedication.Text = "";
            txtSpecialNotes.Text = "";
            cmbPrescriptionStatus.SelectedIndex = -1;
            cmbPatientStatus.SelectedIndex = -1;
            txtSearch.Text = "";
            txtDoctorID.Text = "";
        }

        // Searches for medical records by Medical_ID.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            DataTable dt = new DataTable();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulateMedicalRecordsDataGridView();
                return;
            }

            // Search query to find records by Medical_ID for the logged-in doctor.
            string searchQuery = "SELECT * FROM Medical_Records WHERE Medical_ID LIKE @SearchTerm AND DoctorID = @DoctorID";
            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);
            searchCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
            searchCommand.Parameters.AddWithValue("@DoctorID", LoggedInUser.DoctorID);

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            adapter.Fill(dt);
            connection.Close();

            dgvDoctorMedicalRecords.DataSource = dt;
        }

        // Generates and saves a PDF prescription for the selected record.
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedicalRecordID.Text))
            {
                MessageBox.Show("Please select a medical record to print.", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = "MedicalRecord_" + txtMedicalRecordID.Text + ".pdf";
            string filePath = System.IO.Path.Combine(documentsPath, fileName);

            Document doc = new Document(PageSize.A5, 40, 40, 40, 40);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            Paragraph header = new Paragraph("Prescription",
                new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
            header.Alignment = Element.ALIGN_CENTER;
            doc.Add(header);
            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph("Medical Record ID: " + txtMedicalRecordID.Text));
            doc.Add(new Paragraph("Appointment ID: " + txtAppointmentID.Text));
            doc.Add(new Paragraph("Doctor ID: " + txtDoctorID.Text));
            doc.Add(new Paragraph("Patient ID: " + txtPatientID.Text));
            doc.Add(new Paragraph("Diagnosis: " + txtDiagnosis.Text));
            doc.Add(new Paragraph("Medication: " + txtMedication.Text));
            doc.Add(new Paragraph("Prescription Status: " + cmbPrescriptionStatus.Text));
            doc.Add(new Paragraph("Patient Status: " + cmbPatientStatus.Text));
            doc.Add(new Paragraph("Special Notes: " + txtSpecialNotes.Text));

            doc.Close();

            MessageBox.Show("Prescription PDF saved successfully in Documents folder.\nFile: " + filePath,
                "PDF Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Navigation method to show a new form and close the current one.
        private void ShowAndCloseForm(Form newForm)
        {
            newForm.Show();
            this.Close();
        }

        // Navigates to the Appointments form.
        private void AppointmentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Doctor_AppointmentManagement());
        }

        // Handles click event for the Medical Records button.
        private void MedRecordsBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
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

        private void txtMedicalRecordID_TextChanged(object sender, EventArgs e) { }
        private void txtDoctorID_TextChanged(object sender, EventArgs e) { }
        private void txtPatientID_TextChanged(object sender, EventArgs e) { }
        private void cmbPrescriptionStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbPatientStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtDiagnosis_TextChanged(object sender, EventArgs e) { }
        private void txtMedication_TextChanged(object sender, EventArgs e) { }
        private void txtSpecialNotes_TextChanged(object sender, EventArgs e) { }
        private void txtSearch_TextChanged(object sender, EventArgs e) { }
    }
}