namespace HMS_APP_001
{
    partial class Admin_Payment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin_Payment));
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.txtPaymentID = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PaymentsBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DoctorsBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.InvoicesBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MedRecordsBtn = new System.Windows.Forms.Button();
            this.AppointmentsBtn = new System.Windows.Forms.Button();
            this.PatientsBtn = new System.Windows.Forms.Button();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dgvAdminPayments = new System.Windows.Forms.DataGridView();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnProcessPayment = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInvoiceID = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpPaymentTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminPayments)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.Location = new System.Drawing.Point(8, 27);
            this.dtpPaymentDate.Margin = new System.Windows.Forms.Padding(5);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(293, 26);
            this.dtpPaymentDate.TabIndex = 8;
            this.dtpPaymentDate.ValueChanged += new System.EventHandler(this.dtpPaymentDate_ValueChanged);
            // 
            // txtPaymentID
            // 
            this.txtPaymentID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPaymentID.ForeColor = System.Drawing.Color.Black;
            this.txtPaymentID.Location = new System.Drawing.Point(8, 27);
            this.txtPaymentID.Margin = new System.Windows.Forms.Padding(5);
            this.txtPaymentID.Name = "txtPaymentID";
            this.txtPaymentID.Size = new System.Drawing.Size(270, 26);
            this.txtPaymentID.TabIndex = 3;
            this.txtPaymentID.TextChanged += new System.EventHandler(this.txtPaymentID_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox6.Controls.Add(this.txtPaymentID);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(10, 10);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(286, 64);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Payment ID";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.PaymentsBtn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DoctorsBtn);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.InvoicesBtn);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.MedRecordsBtn);
            this.panel1.Controls.Add(this.AppointmentsBtn);
            this.panel1.Controls.Add(this.PatientsBtn);
            this.panel1.Controls.Add(this.LogoutBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 590);
            this.panel1.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(58)))));
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 206);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(219, 43);
            this.panel2.TabIndex = 50;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::HMS_APP_001.Properties.Resources.user;
            this.pictureBox2.Location = new System.Drawing.Point(12, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(58)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(53, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Admin Menu";
            // 
            // PaymentsBtn
            // 
            this.PaymentsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.PaymentsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.PaymentsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PaymentsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PaymentsBtn.ForeColor = System.Drawing.Color.White;
            this.PaymentsBtn.Location = new System.Drawing.Point(0, 485);
            this.PaymentsBtn.Name = "PaymentsBtn";
            this.PaymentsBtn.Size = new System.Drawing.Size(219, 40);
            this.PaymentsBtn.TabIndex = 15;
            this.PaymentsBtn.Text = "Payment";
            this.PaymentsBtn.UseVisualStyleBackColor = false;
            this.PaymentsBtn.Click += new System.EventHandler(this.PaymentsBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(71, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 24);
            this.label1.TabIndex = 48;
            this.label1.Text = "System";
            // 
            // DoctorsBtn
            // 
            this.DoctorsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.DoctorsBtn.FlatAppearance.BorderSize = 0;
            this.DoctorsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.DoctorsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DoctorsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.DoctorsBtn.ForeColor = System.Drawing.Color.White;
            this.DoctorsBtn.Location = new System.Drawing.Point(0, 301);
            this.DoctorsBtn.Name = "DoctorsBtn";
            this.DoctorsBtn.Size = new System.Drawing.Size(219, 40);
            this.DoctorsBtn.TabIndex = 14;
            this.DoctorsBtn.Text = "Doctor Management";
            this.DoctorsBtn.UseVisualStyleBackColor = false;
            this.DoctorsBtn.Click += new System.EventHandler(this.DoctorsBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 24);
            this.label5.TabIndex = 47;
            this.label5.Text = "Hospital Management";
            // 
            // InvoicesBtn
            // 
            this.InvoicesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.InvoicesBtn.FlatAppearance.BorderSize = 0;
            this.InvoicesBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.InvoicesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InvoicesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InvoicesBtn.ForeColor = System.Drawing.Color.White;
            this.InvoicesBtn.Location = new System.Drawing.Point(0, 439);
            this.InvoicesBtn.Name = "InvoicesBtn";
            this.InvoicesBtn.Size = new System.Drawing.Size(219, 40);
            this.InvoicesBtn.TabIndex = 13;
            this.InvoicesBtn.Text = "Invoices";
            this.InvoicesBtn.UseVisualStyleBackColor = false;
            this.InvoicesBtn.Click += new System.EventHandler(this.InvoicesBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HMS_APP_001.Properties.Resources.logo__2D2D30;
            this.pictureBox1.Location = new System.Drawing.Point(59, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // MedRecordsBtn
            // 
            this.MedRecordsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.MedRecordsBtn.FlatAppearance.BorderSize = 0;
            this.MedRecordsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.MedRecordsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MedRecordsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MedRecordsBtn.ForeColor = System.Drawing.Color.White;
            this.MedRecordsBtn.Location = new System.Drawing.Point(0, 393);
            this.MedRecordsBtn.Name = "MedRecordsBtn";
            this.MedRecordsBtn.Size = new System.Drawing.Size(219, 40);
            this.MedRecordsBtn.TabIndex = 12;
            this.MedRecordsBtn.Text = "Medical Records";
            this.MedRecordsBtn.UseVisualStyleBackColor = false;
            this.MedRecordsBtn.Click += new System.EventHandler(this.MedRecordsBtn_Click);
            // 
            // AppointmentsBtn
            // 
            this.AppointmentsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.AppointmentsBtn.FlatAppearance.BorderSize = 0;
            this.AppointmentsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.AppointmentsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AppointmentsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.AppointmentsBtn.ForeColor = System.Drawing.Color.White;
            this.AppointmentsBtn.Location = new System.Drawing.Point(0, 347);
            this.AppointmentsBtn.Name = "AppointmentsBtn";
            this.AppointmentsBtn.Size = new System.Drawing.Size(219, 40);
            this.AppointmentsBtn.TabIndex = 11;
            this.AppointmentsBtn.Text = "Appointment Management";
            this.AppointmentsBtn.UseVisualStyleBackColor = false;
            this.AppointmentsBtn.Click += new System.EventHandler(this.AppointmentsBtn_Click);
            // 
            // PatientsBtn
            // 
            this.PatientsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.PatientsBtn.FlatAppearance.BorderSize = 0;
            this.PatientsBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.PatientsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PatientsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PatientsBtn.ForeColor = System.Drawing.Color.White;
            this.PatientsBtn.Location = new System.Drawing.Point(0, 255);
            this.PatientsBtn.Name = "PatientsBtn";
            this.PatientsBtn.Size = new System.Drawing.Size(219, 40);
            this.PatientsBtn.TabIndex = 10;
            this.PatientsBtn.Text = "Patient Management";
            this.PatientsBtn.UseVisualStyleBackColor = false;
            this.PatientsBtn.Click += new System.EventHandler(this.PatientsBtn_Click);
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.LogoutBtn.FlatAppearance.BorderSize = 0;
            this.LogoutBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.LogoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LogoutBtn.ForeColor = System.Drawing.Color.White;
            this.LogoutBtn.Location = new System.Drawing.Point(0, 531);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(219, 40);
            this.LogoutBtn.TabIndex = 5;
            this.LogoutBtn.Text = "Log Out";
            this.LogoutBtn.UseVisualStyleBackColor = false;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox7.Controls.Add(this.dtpPaymentDate);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(10, 19);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(311, 64);
            this.groupBox7.TabIndex = 34;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Payment Date";
            // 
            // dgvAdminPayments
            // 
            this.dgvAdminPayments.AllowUserToAddRows = false;
            this.dgvAdminPayments.AllowUserToDeleteRows = false;
            this.dgvAdminPayments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdminPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdminPayments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvAdminPayments.Location = new System.Drawing.Point(219, 340);
            this.dgvAdminPayments.Margin = new System.Windows.Forms.Padding(10);
            this.dgvAdminPayments.MultiSelect = false;
            this.dgvAdminPayments.Name = "dgvAdminPayments";
            this.dgvAdminPayments.ShowEditingIcon = false;
            this.dgvAdminPayments.Size = new System.Drawing.Size(653, 250);
            this.dgvAdminPayments.TabIndex = 35;
            this.dgvAdminPayments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdminPayments_CellClick);
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "Card",
            "Online"});
            this.cmbPaymentMethod.Location = new System.Drawing.Point(8, 54);
            this.cmbPaymentMethod.Margin = new System.Windows.Forms.Padding(5);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(142, 28);
            this.cmbPaymentMethod.TabIndex = 5;
            this.cmbPaymentMethod.Text = "Method";
            this.cmbPaymentMethod.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentMethod_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.cmbPaymentMethod);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(10, 149);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 90);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Payment Method";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox8.Controls.Add(this.btnPrint);
            this.groupBox8.Controls.Add(this.txtSearch);
            this.groupBox8.Controls.Add(this.btnSearch);
            this.groupBox8.Controls.Add(this.btnClear);
            this.groupBox8.Controls.Add(this.btnProcessPayment);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox8.ForeColor = System.Drawing.Color.White;
            this.groupBox8.Location = new System.Drawing.Point(10, 185);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(311, 138);
            this.groupBox8.TabIndex = 38;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Action Buttons";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Location = new System.Drawing.Point(109, 63);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(91, 26);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(109, 27);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(192, 26);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(8, 27);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(91, 26);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(210, 63);
            this.btnClear.Margin = new System.Windows.Forms.Padding(5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 26);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnProcessPayment
            // 
            this.btnProcessPayment.BackColor = System.Drawing.SystemColors.Control;
            this.btnProcessPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnProcessPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessPayment.ForeColor = System.Drawing.Color.Black;
            this.btnProcessPayment.Location = new System.Drawing.Point(8, 63);
            this.btnProcessPayment.Margin = new System.Windows.Forms.Padding(5);
            this.btnProcessPayment.Name = "btnProcessPayment";
            this.btnProcessPayment.Size = new System.Drawing.Size(91, 26);
            this.btnProcessPayment.TabIndex = 0;
            this.btnProcessPayment.Text = "Add";
            this.btnProcessPayment.UseVisualStyleBackColor = false;
            this.btnProcessPayment.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox6);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(219, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(306, 340);
            this.panel3.TabIndex = 39;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.txtInvoiceID);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(10, 78);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 64);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoice ID";
            // 
            // txtInvoiceID
            // 
            this.txtInvoiceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtInvoiceID.ForeColor = System.Drawing.Color.Black;
            this.txtInvoiceID.Location = new System.Drawing.Point(8, 27);
            this.txtInvoiceID.Margin = new System.Windows.Forms.Padding(5);
            this.txtInvoiceID.Name = "txtInvoiceID";
            this.txtInvoiceID.Size = new System.Drawing.Size(270, 26);
            this.txtInvoiceID.TabIndex = 3;
            this.txtInvoiceID.TextChanged += new System.EventHandler(this.txtInvoiceID_TextChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox8);
            this.panel4.Controls.Add(this.groupBox7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(523, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(349, 340);
            this.panel4.TabIndex = 40;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.Controls.Add(this.dtpPaymentTime);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(10, 87);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 63);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payment  Time";
            // 
            // dtpPaymentTime
            // 
            this.dtpPaymentTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPaymentTime.Location = new System.Drawing.Point(8, 27);
            this.dtpPaymentTime.Margin = new System.Windows.Forms.Padding(5);
            this.dtpPaymentTime.Name = "dtpPaymentTime";
            this.dtpPaymentTime.ShowUpDown = true;
            this.dtpPaymentTime.Size = new System.Drawing.Size(121, 26);
            this.dtpPaymentTime.TabIndex = 8;
            this.dtpPaymentTime.ValueChanged += new System.EventHandler(this.dtpPaymentTime_ValueChanged);
            // 
            // Admin_Payment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(33)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(872, 590);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvAdminPayments);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Admin_Payment";
            this.Text = "|   ADMIN   | Payment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminPayments)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.TextBox txtPaymentID;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dgvAdminPayments;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button PaymentsBtn;
        private System.Windows.Forms.Button DoctorsBtn;
        private System.Windows.Forms.Button InvoicesBtn;
        private System.Windows.Forms.Button MedRecordsBtn;
        private System.Windows.Forms.Button AppointmentsBtn;
        private System.Windows.Forms.Button PatientsBtn;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnProcessPayment;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInvoiceID;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpPaymentTime;
    }
}