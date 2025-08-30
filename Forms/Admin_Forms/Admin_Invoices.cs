using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HMS_APP_001
{
    public partial class Admin_Invoices : Form
    {
        /// Initializes the form and populates the data grid view.
        public Admin_Invoices()
        {
            InitializeComponent();
            txtInvoiceID.ReadOnly = true;
            txtPatientID.ReadOnly = true;
            PopulateInvoicesDataGridView();
        }

        /// Handles the click event for adding a new invoice.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedicalRecordID.Text) ||
                string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please fill all required fields: Medical Record ID and Amount.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            string medicalRecordCheckQuery = "SELECT COUNT(*) FROM Medical_Records WHERE Medical_ID = @MedicalID";
            SqlCommand medicalRecordCheckCommand = new SqlCommand(medicalRecordCheckQuery, connection);
            medicalRecordCheckCommand.Parameters.AddWithValue("@MedicalID", txtMedicalRecordID.Text);
            connection.Open();
            int recordCount = (int)medicalRecordCheckCommand.ExecuteScalar();
            connection.Close();
            if (recordCount == 0)
            {
                MessageBox.Show("The Medical Record ID does not exist.", "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string invoiceCheckQuery = "SELECT COUNT(*) FROM Invoice WHERE Medical_ID = @MedicalID";
            SqlCommand invoiceCheckCommand = new SqlCommand(invoiceCheckQuery, connection);
            invoiceCheckCommand.Parameters.AddWithValue("@MedicalID", txtMedicalRecordID.Text);
            connection.Open();
            int invoiceCount = (int)invoiceCheckCommand.ExecuteScalar();
            connection.Close();
            if (invoiceCount > 0)
            {
                MessageBox.Show("An invoice for this medical record already exists.", "Duplicate Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string invoiceDate = dtpInvoiceDate.Value.ToString("yyyy-MM-dd");
            string invoiceTime = dtpInvoiceTime.Value.ToString("HH:mm:ss");
            string medicalID = txtMedicalRecordID.Text;
            string patientID = txtPatientID.Text;
            string amount = txtAmount.Text;
            connection.Open();
            string insertQuery = "INSERT INTO Invoice (Invoice_Date, Invoice_Time, Amount, PatientID, Medical_ID) VALUES (@InvoiceDate, @InvoiceTime, @Amount, @PatientID, @MedicalID)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@InvoiceDate", invoiceDate);
            insertCommand.Parameters.AddWithValue("@InvoiceTime", invoiceTime);
            insertCommand.Parameters.AddWithValue("@Amount", amount);
            insertCommand.Parameters.AddWithValue("@MedicalID", medicalID);
            insertCommand.Parameters.AddWithValue("@PatientID", patientID);
            insertCommand.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Invoice added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PopulateInvoicesDataGridView();
            btnClear_Click(sender, e);
        }
        /// Clears all input fields and refreshes the data grid view.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInvoiceID.Text = "";
            txtMedicalRecordID.Text = "";
            txtPatientID.Text = "";
            txtAmount.Text = "";
            txtsearch.Text = "";
            dtpInvoiceDate.Value = DateTime.Now;
            dtpInvoiceTime.Value = DateTime.Now;
            PopulateInvoicesDataGridView();
        }

        /// Performs a search based on the provided term.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtsearch.Text.Trim();
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            DataTable dt = new DataTable();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulateInvoicesDataGridView();
                return;
            }
            string searchQuery = "SELECT * FROM Invoice WHERE Invoice_ID LIKE @SearchTerm";
            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);
            searchCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            adapter.Fill(dt);
            connection.Close();
            dgvAdminInvoices.DataSource = dt;
        }

        /// Populates the input fields with data from the selected row.
        private void dgvAdminInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvAdminInvoices.Rows[e.RowIndex];
                txtInvoiceID.Text = row.Cells["Invoice_ID"].Value.ToString();
                txtMedicalRecordID.Text = row.Cells["Medical_ID"].Value.ToString();
                txtPatientID.Text = row.Cells["PatientID"].Value.ToString();
                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                dtpInvoiceDate.Value = Convert.ToDateTime(row.Cells["Invoice_Date"].Value);
                TimeSpan time = (TimeSpan)row.Cells["Invoice_Time"].Value;
                dtpInvoiceTime.Value = DateTime.Today.Add(time);
            }
        }

        /// Retrieves and displays all invoice data in the grid view.
        private void PopulateInvoicesDataGridView()
        {
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            connection.Open();
            string query = "SELECT * FROM Invoice";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvAdminInvoices.DataSource = dt;
            dgvAdminInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connection.Close();
        }

        /// Generates a PDF invoice for the selected record.
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceID.Text))
            {
                MessageBox.Show("Please select an invoice to print.", "No Invoice Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = "Invoice_" + txtInvoiceID.Text + ".pdf";
            string filePath = System.IO.Path.Combine(documentsPath, fileName);
            Document doc = new Document(PageSize.A5, 40, 40, 40, 40);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();
            Paragraph header = new Paragraph("INVOICE",
                new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
            header.Alignment = Element.ALIGN_CENTER;
            doc.Add(header);
            doc.Add(new Paragraph("\n"));
            doc.Add(new Paragraph("Invoice ID: " + txtInvoiceID.Text));
            doc.Add(new Paragraph("Medical Record ID: " + txtMedicalRecordID.Text));
            doc.Add(new Paragraph("Patient ID: " + txtPatientID.Text));
            doc.Add(new Paragraph("Invoice Date: " + dtpInvoiceDate.Value.ToString("yyyy-MM-dd")));
            doc.Add(new Paragraph("Invoice Time: " + dtpInvoiceTime.Value.ToString("HH:mm:ss")));
            doc.Add(new Paragraph("Amount: " + txtAmount.Text));
            doc.Add(new Paragraph("\nThank you for your payment."));
            doc.Close();
            MessageBox.Show("Invoice PDF saved successfully in Documents folder.\nFile: " + filePath,
                "PDF Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// Retrieves the patient ID associated with the medical record ID as the text changes.
        private void txtMedicalRecordID_TextChanged(object sender, EventArgs e)
        {
            string medicalID = txtMedicalRecordID.Text.Trim();
            if (string.IsNullOrWhiteSpace(medicalID))
            {
                txtPatientID.Text = "";
                return;
            }
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            string query = "SELECT PatientID FROM Medical_Records WHERE Medical_ID = @MedicalID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MedicalID", medicalID);
            connection.Open();
            object patientIDObj = command.ExecuteScalar();
            connection.Close();
            if (patientIDObj != null)
            {
                txtPatientID.Text = patientIDObj.ToString();
            }
            else
            {
                txtPatientID.Text = "";
            }
        }

        /// Navigates to a new form and closes the current form.
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
            ShowAndCloseForm(new Admin_AppointmentManagement());
        }
        private void MedRecordsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_MedicalRecords());
        }
        private void InvoicesBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }
        private void PaymentsBtn_Click(object sender, EventArgs e)
        {
            ShowAndCloseForm(new Admin_Payment());
        }
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

        /// This event handlers is currently not in use.
        private void txtInvoiceID_TextChanged(object sender, EventArgs e)
        {
        }
        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpInvoiceTime_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPatientID_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
