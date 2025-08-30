using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace HMS_APP_001
{
    public partial class Admin_Payment : Form
    {
        /// Initializes the form, sets a read-only field, and populates the data grid view.
        public Admin_Payment()
        {
            InitializeComponent();
            txtPaymentID.ReadOnly = true;
            PopulatePaymentsDataGridView();
        }

        /// Handles the click event for adding a new payment record.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceID.Text) ||
                string.IsNullOrWhiteSpace(cmbPaymentMethod.Text))
            {
                MessageBox.Show("All fields must be filled.", "Data Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtInvoiceID.Text, out int invoiceId))
            {
                MessageBox.Show("Invoice ID must be a number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string paymentMethod = cmbPaymentMethod.Text;
            DateTime paymentDate = dtpPaymentDate.Value.Date;
            DateTime paymentTime = dtpPaymentTime.Value;
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            string invoiceCheckQuery = "SELECT COUNT(*) FROM Invoice WHERE Invoice_ID = @InvoiceID";
            SqlCommand invoiceCheckCommand = new SqlCommand(invoiceCheckQuery, connection);
            invoiceCheckCommand.Parameters.AddWithValue("@InvoiceID", invoiceId);
            connection.Open();
            int invoiceCount = (int)invoiceCheckCommand.ExecuteScalar();
            connection.Close();
            if (invoiceCount == 0)
            {
                MessageBox.Show("Invoice ID is invalid. Please enter an existing invoice ID.", "Invalid Invoice ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection.Open();
            string insertQuery = "INSERT INTO Payment (Invoice_ID, Method, Payment_Date, Payment_Time) VALUES (@InvoiceID, @Method, @PaymentDate, @PaymentTime)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@InvoiceID", invoiceId);
            insertCommand.Parameters.AddWithValue("@Method", paymentMethod);
            insertCommand.Parameters.AddWithValue("@PaymentDate", paymentDate);
            insertCommand.Parameters.AddWithValue("@PaymentTime", paymentTime.TimeOfDay);
            insertCommand.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Payment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PopulatePaymentsDataGridView();
            btnClear_Click(sender, e);
        }
        /// Clears all input fields and refreshes the data grid view.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPaymentID.Text = "";
            txtInvoiceID.Text = "";
            cmbPaymentMethod.Text = "Payment Method";
            dtpPaymentDate.Value = DateTime.Now;
            dtpPaymentTime.Value = DateTime.Now;
            PopulatePaymentsDataGridView();
        }

        /// Performs a search based on the payment ID.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            DataTable dt = new DataTable();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulatePaymentsDataGridView();
                return;
            }
            string searchQuery = "SELECT * FROM Payment WHERE Payment_ID LIKE @SearchTerm";
            SqlCommand searchCommand = new SqlCommand(searchQuery, connection);
            searchCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(searchCommand);
            adapter.Fill(dt);
            connection.Close();
            dgvAdminPayments.DataSource = dt;
        }

        /// Populates the input fields with data from the selected row.
        private void dgvAdminPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvAdminPayments.Rows[e.RowIndex];
                txtPaymentID.Text = row.Cells["Payment_ID"].Value.ToString();
                txtInvoiceID.Text = row.Cells["Invoice_ID"].Value.ToString();
                DateTime paymentDate = Convert.ToDateTime(row.Cells["Payment_Date"].Value);
                TimeSpan paymentTime = (TimeSpan)row.Cells["Payment_Time"].Value;
                dtpPaymentDate.Value = paymentDate;
                dtpPaymentTime.Value = paymentDate.Add(paymentTime);
                cmbPaymentMethod.Text = row.Cells["Method"].Value.ToString();
            }
        }

        /// Retrieves and displays all payment data in the grid view.
        private void PopulatePaymentsDataGridView()
        {
            Database db = new Database();
            SqlConnection connection = db.GetConnection();
            connection.Open();
            string query = "SELECT * FROM Payment";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvAdminPayments.DataSource = dt;
            dgvAdminPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connection.Close();
        }
        /// Generates and saves a PDF for the selected payment record.
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPaymentID.Text))
            {
                MessageBox.Show("Please select an Payment to print.", "No Payment Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(documentsPath, "Payment_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
            Document doc = new Document(PageSize.A5, 40, 40, 40, 40);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();
            iTextSharp.text.Font headerFont = FontFactory.GetFont("Helvetica", 16, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font normalFont = FontFactory.GetFont("Helvetica", 12, iTextSharp.text.Font.NORMAL);
            Paragraph header = new Paragraph("PAYMENT", headerFont);
            header.Alignment = Element.ALIGN_CENTER;
            header.SpacingAfter = 20;
            doc.Add(header);
            doc.Add(new Paragraph("Payment ID         : " + txtPaymentID.Text, normalFont));
            doc.Add(new Paragraph("Invoice ID         : " + txtInvoiceID.Text, normalFont));
            doc.Add(new Paragraph("Payment Method   : " + cmbPaymentMethod.Text, normalFont));
            doc.Add(new Paragraph("Payment Date     : " + dtpPaymentDate.Value.ToShortDateString(), normalFont));
            doc.Add(new Paragraph("Payment Time     : " + dtpPaymentTime.Value.ToShortTimeString(), normalFont));
            doc.Close();
            MessageBox.Show("PDF saved to Documents folder:\n" + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ShowAndCloseForm(new Admin_Invoices());
        }
        private void PaymentsBtn_Click(object sender, EventArgs e)
        {
            this.BringToFront();
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

        /// This event handlers are currently not in use.
        private void txtPaymentID_TextChanged(object sender, EventArgs e) { }
        private void txtInvoiceID_TextChanged(object sender, EventArgs e) { }
        private void dtpPaymentDate_ValueChanged(object sender, EventArgs e) { }
        private void dtpPaymentTime_ValueChanged(object sender, EventArgs e) { }
        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtSearch_TextChanged(object sender, EventArgs e) { }
    }
}
