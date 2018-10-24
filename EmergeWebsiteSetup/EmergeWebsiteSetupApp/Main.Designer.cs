namespace EmergeImportApp
{
    partial class Main
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.WebSiteNameTextBox = new System.Windows.Forms.TextBox();
            this.PackagePathTextBox = new System.Windows.Forms.TextBox();
            this.ProcessImportButton = new System.Windows.Forms.Button();
            this.SelectPackageButton = new System.Windows.Forms.Button();
            this.OpenPackageDialog = new System.Windows.Forms.OpenFileDialog();
            this.WebsiteGroupBox = new System.Windows.Forms.GroupBox();
            this.WebsitePathBrowseButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.WebsitePathTextBox = new System.Windows.Forms.TextBox();
            this.DomainTextBox = new System.Windows.Forms.TextBox();
            this.WebsiteDisplayNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.DatabaseGroupBox = new System.Windows.Forms.GroupBox();
            this.DatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.DBServerName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ProgressBox = new System.Windows.Forms.TextBox();
            this.WebsiteGroupBox.SuspendLayout();
            this.DatabaseGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Site Code Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Import Package Path";
            // 
            // WebSiteNameTextBox
            // 
            this.WebSiteNameTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.WebSiteNameTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.WebSiteNameTextBox.Location = new System.Drawing.Point(152, 23);
            this.WebSiteNameTextBox.Name = "WebSiteNameTextBox";
            this.WebSiteNameTextBox.Size = new System.Drawing.Size(311, 20);
            this.WebSiteNameTextBox.TabIndex = 1;
            // 
            // PackagePathTextBox
            // 
            this.PackagePathTextBox.Enabled = false;
            this.PackagePathTextBox.Location = new System.Drawing.Point(152, 213);
            this.PackagePathTextBox.Multiline = true;
            this.PackagePathTextBox.Name = "PackagePathTextBox";
            this.PackagePathTextBox.Size = new System.Drawing.Size(238, 47);
            this.PackagePathTextBox.TabIndex = 3;
            // 
            // ProcessImportButton
            // 
            this.ProcessImportButton.Enabled = false;
            this.ProcessImportButton.Location = new System.Drawing.Point(18, 334);
            this.ProcessImportButton.Name = "ProcessImportButton";
            this.ProcessImportButton.Size = new System.Drawing.Size(188, 33);
            this.ProcessImportButton.TabIndex = 10;
            this.ProcessImportButton.Text = "Start Import";
            this.ProcessImportButton.UseVisualStyleBackColor = true;
            this.ProcessImportButton.Click += new System.EventHandler(this.ProcessImportButton_Click);
            // 
            // SelectPackageButton
            // 
            this.SelectPackageButton.Location = new System.Drawing.Point(396, 213);
            this.SelectPackageButton.Name = "SelectPackageButton";
            this.SelectPackageButton.Size = new System.Drawing.Size(67, 32);
            this.SelectPackageButton.TabIndex = 5;
            this.SelectPackageButton.Text = "Browse....";
            this.SelectPackageButton.UseVisualStyleBackColor = true;
            this.SelectPackageButton.Click += new System.EventHandler(this.SelectPackageButton_Click);
            // 
            // OpenPackageDialog
            // 
            this.OpenPackageDialog.Title = "Select Package";
            // 
            // WebsiteGroupBox
            // 
            this.WebsiteGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.WebsiteGroupBox.Controls.Add(this.WebsitePathBrowseButton);
            this.WebsiteGroupBox.Controls.Add(this.label7);
            this.WebsiteGroupBox.Controls.Add(this.label5);
            this.WebsiteGroupBox.Controls.Add(this.WebsitePathTextBox);
            this.WebsiteGroupBox.Controls.Add(this.DomainTextBox);
            this.WebsiteGroupBox.Controls.Add(this.WebsiteDisplayNameTextBox);
            this.WebsiteGroupBox.Controls.Add(this.label2);
            this.WebsiteGroupBox.Controls.Add(this.SelectPackageButton);
            this.WebsiteGroupBox.Controls.Add(this.label3);
            this.WebsiteGroupBox.Controls.Add(this.label4);
            this.WebsiteGroupBox.Controls.Add(this.PackagePathTextBox);
            this.WebsiteGroupBox.Controls.Add(this.WebSiteNameTextBox);
            this.WebsiteGroupBox.Location = new System.Drawing.Point(12, 41);
            this.WebsiteGroupBox.Name = "WebsiteGroupBox";
            this.WebsiteGroupBox.Size = new System.Drawing.Size(477, 273);
            this.WebsiteGroupBox.TabIndex = 6;
            this.WebsiteGroupBox.TabStop = false;
            // 
            // WebsitePathBrowseButton
            // 
            this.WebsitePathBrowseButton.Location = new System.Drawing.Point(396, 150);
            this.WebsitePathBrowseButton.Name = "WebsitePathBrowseButton";
            this.WebsitePathBrowseButton.Size = new System.Drawing.Size(67, 32);
            this.WebsitePathBrowseButton.TabIndex = 4;
            this.WebsitePathBrowseButton.Text = "Browse....";
            this.WebsitePathBrowseButton.UseVisualStyleBackColor = true;
            this.WebsitePathBrowseButton.Click += new System.EventHandler(this.WebsitePathBrowseButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Site Domain";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Website Path";
            // 
            // WebsitePathTextBox
            // 
            this.WebsitePathTextBox.Enabled = false;
            this.WebsitePathTextBox.Location = new System.Drawing.Point(152, 150);
            this.WebsitePathTextBox.Multiline = true;
            this.WebsitePathTextBox.Name = "WebsitePathTextBox";
            this.WebsitePathTextBox.Size = new System.Drawing.Size(238, 46);
            this.WebsitePathTextBox.TabIndex = 9;
            // 
            // DomainTextBox
            // 
            this.DomainTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DomainTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.DomainTextBox.Location = new System.Drawing.Point(152, 111);
            this.DomainTextBox.Name = "DomainTextBox";
            this.DomainTextBox.Size = new System.Drawing.Size(311, 20);
            this.DomainTextBox.TabIndex = 3;
            // 
            // WebsiteDisplayNameTextBox
            // 
            this.WebsiteDisplayNameTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.WebsiteDisplayNameTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.WebsiteDisplayNameTextBox.Location = new System.Drawing.Point(152, 66);
            this.WebsiteDisplayNameTextBox.Name = "WebsiteDisplayNameTextBox";
            this.WebsiteDisplayNameTextBox.Size = new System.Drawing.Size(311, 20);
            this.WebsiteDisplayNameTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Site Display Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(442, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Import Utility";
            // 
            // FolderBrowserDialog
            // 
            this.FolderBrowserDialog.SelectedPath = "D:\\EMERGE\\ImportExport\\Websites";
            // 
            // DatabaseGroupBox
            // 
            this.DatabaseGroupBox.Controls.Add(this.DatabaseNameTextBox);
            this.DatabaseGroupBox.Controls.Add(this.PasswordTextBox);
            this.DatabaseGroupBox.Controls.Add(this.UserNameTextBox);
            this.DatabaseGroupBox.Controls.Add(this.DBServerName);
            this.DatabaseGroupBox.Controls.Add(this.label10);
            this.DatabaseGroupBox.Controls.Add(this.label9);
            this.DatabaseGroupBox.Controls.Add(this.label8);
            this.DatabaseGroupBox.Controls.Add(this.label6);
            this.DatabaseGroupBox.Location = new System.Drawing.Point(495, 41);
            this.DatabaseGroupBox.Name = "DatabaseGroupBox";
            this.DatabaseGroupBox.Size = new System.Drawing.Size(477, 273);
            this.DatabaseGroupBox.TabIndex = 8;
            this.DatabaseGroupBox.TabStop = false;
            // 
            // DatabaseNameTextBox
            // 
            this.DatabaseNameTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DatabaseNameTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.DatabaseNameTextBox.Location = new System.Drawing.Point(152, 149);
            this.DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            this.DatabaseNameTextBox.Size = new System.Drawing.Size(311, 20);
            this.DatabaseNameTextBox.TabIndex = 9;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.PasswordTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.PasswordTextBox.Location = new System.Drawing.Point(152, 109);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(311, 20);
            this.PasswordTextBox.TabIndex = 8;
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.UserNameTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.UserNameTextBox.Location = new System.Drawing.Point(152, 72);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(311, 20);
            this.UserNameTextBox.TabIndex = 7;
            // 
            // DBServerName
            // 
            this.DBServerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DBServerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.DBServerName.Location = new System.Drawing.Point(152, 31);
            this.DBServerName.Name = "DBServerName";
            this.DBServerName.Size = new System.Drawing.Size(311, 20);
            this.DBServerName.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Database Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Password";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "User Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Database Server Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ProgressBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 386);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(956, 227);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Progress....";
            // 
            // ProgressBox
            // 
            this.ProgressBox.Location = new System.Drawing.Point(6, 19);
            this.ProgressBox.Multiline = true;
            this.ProgressBox.Name = "ProgressBox";
            this.ProgressBox.ReadOnly = true;
            this.ProgressBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ProgressBox.Size = new System.Drawing.Size(940, 202);
            this.ProgressBox.TabIndex = 0;
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(980, 642);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.DatabaseGroupBox);
            this.Controls.Add(this.ProcessImportButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WebsiteGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.WebsiteGroupBox.ResumeLayout(false);
            this.WebsiteGroupBox.PerformLayout();
            this.DatabaseGroupBox.ResumeLayout(false);
            this.DatabaseGroupBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox WebSiteNameTextBox;
        private System.Windows.Forms.TextBox PackagePathTextBox;
        private System.Windows.Forms.Button ProcessImportButton;
        private System.Windows.Forms.Button SelectPackageButton;
        private System.Windows.Forms.OpenFileDialog OpenPackageDialog;
        private System.Windows.Forms.GroupBox WebsiteGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox WebsitePathTextBox;
        private System.Windows.Forms.TextBox DomainTextBox;
        private System.Windows.Forms.TextBox WebsiteDisplayNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Button WebsitePathBrowseButton;
        private System.Windows.Forms.GroupBox DatabaseGroupBox;
        private System.Windows.Forms.TextBox DatabaseNameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox DBServerName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox ProgressBox;
    }
}

