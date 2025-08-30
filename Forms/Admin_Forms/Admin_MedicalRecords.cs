using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace HMS_APP_001
{
    public partial class Admin_MedicalRecords : Form
    {
        // This is the constructor for the form.
        public Admin_MedicalRecords()
        {
            InitializeComponent();
            txtMedicalRecordID.ReadOnly = true;
            txtAppointmentID.ReadOnly = true;
            txtDoctorID.ReadOnly = true;
            txtPatientID.ReadOnly = true;
            txtDiagnosis.ReadOnly = true;
            txtMedication.ReadOnly = true;
            txtSpecialNotes.ReadOnly = true;
            cmbPrescriptionStatus.Enabled = false;
            cmbPatientStatus.Enabled = false;
            PopulateMedicalRecordsDataGridView();
        }

        // This method populates medical records data into the DataGridView.
        private void PopulateMedicalRecordsDataGridView()
        {
            Database db = new Database();
            SqlConnection connection = db.GetConnection();

            connection.Open();

            string query = "SELECT * FROM Medical_Records";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvAdminMedicalRecords.DataSource = dt;
            dgvAdminMedicalRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            connection.Close();
        }

        // This method populates form fields when a cell is clicked in the DataGridView.
        private void dgvAdminMedicalRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvAdminMedicalRecords.Rows[e.RowIndex];

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

        // This method handles searching for a medical record.
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

            string searchQuery = "SELECT * FROM Medical_Records WHERE Medical_ID LIKE @SearchTerm";
            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);
            searchCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            adapter.Fill(dt);
            connection.Close();

            dgvAdminMedicalRecords.DataSource = dt;
        }
        // This method clears all input fields.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMedicalRecordID.Text = "";
            txtMedication.Text = "";
            txtDiagnosis.Text = "";
            cmbPrescriptionStatus.Text = "Status";
            cmbPatientStatus.Text = "Status";
            txtSpecialNotes.Text = "";
            txtPatientID.Text = "";
            txtAppointmentID.Text = "";
            txtDoctorID.Text = "";
            txtSearch.Text = "";
            PopulateMedicalRecordsDataGridView();
        }

        // This method handles printing the medical record to a PDF file.
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

        // Navigation button methods.
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

        // This method navigates to the doctors' management form.
        private void DoctorsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_DoctorManagement());
        }

        // This method navigates to the appointments management form.
        private void AppointmentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_AppointmentManagement());
        }

        // This method brings the current form to the front.
        private void MedRecordsBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
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
        private void txtMedicalRecordID_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtAppointmentID_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtDoctorID_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtDoctorName_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPatientID_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPatientName_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbPrescriptionStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbPatientStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtDiagnosis_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMedication_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSpecialNotes_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        }
    }
}