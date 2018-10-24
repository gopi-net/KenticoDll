namespace EmergeLicenseGenerator
{
    partial class LicenseGenerator
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
            this.components = new System.ComponentModel.Container();
            this.TextboxDomainName = new System.Windows.Forms.TextBox();
            this.LabelDomainName = new System.Windows.Forms.Label();
            this.LabelModuleNames = new System.Windows.Forms.Label();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.LGErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.LabelExpirationpolicy = new System.Windows.Forms.Label();
            this.CboExpirationpolicy = new System.Windows.Forms.ComboBox();
            this.LabelExpiredOn = new System.Windows.Forms.Label();
            this.dateTimeExpiredon = new System.Windows.Forms.DateTimePicker();
            this.labelError = new System.Windows.Forms.Label();
            this.TextboxKey = new System.Windows.Forms.TextBox();
            this.LabelLicensekey = new System.Windows.Forms.Label();
            this.CheckedBoxListModuleNames = new System.Windows.Forms.CheckedListBox();
            this.cmdGetLicenseDetails = new System.Windows.Forms.Button();
            this.labelMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LGErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // TextboxDomainName
            // 
            this.TextboxDomainName.Location = new System.Drawing.Point(157, 46);
            this.TextboxDomainName.Name = "TextboxDomainName";
            this.TextboxDomainName.Size = new System.Drawing.Size(422, 20);
            this.TextboxDomainName.TabIndex = 0;
            // 
            // LabelDomainName
            // 
            this.LabelDomainName.AutoSize = true;
            this.LabelDomainName.Location = new System.Drawing.Point(65, 46);
            this.LabelDomainName.Name = "LabelDomainName";
            this.LabelDomainName.Size = new System.Drawing.Size(75, 13);
            this.LabelDomainName.TabIndex = 1;
            this.LabelDomainName.Text = "Domain name:";
            // 
            // LabelModuleNames
            // 
            this.LabelModuleNames.AutoSize = true;
            this.LabelModuleNames.Location = new System.Drawing.Point(65, 80);
            this.LabelModuleNames.Name = "LabelModuleNames";
            this.LabelModuleNames.Size = new System.Drawing.Size(79, 13);
            this.LabelModuleNames.TabIndex = 2;
            this.LabelModuleNames.Text = "Module names:";
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Location = new System.Drawing.Point(595, 355);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(130, 23);
            this.cmdGenerate.TabIndex = 4;
            this.cmdGenerate.Text = "Generate New License";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // LGErrorProvider
            // 
            this.LGErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.LGErrorProvider.ContainerControl = this;
            // 
            // LabelExpirationpolicy
            // 
            this.LabelExpirationpolicy.AutoSize = true;
            this.LabelExpirationpolicy.Location = new System.Drawing.Point(65, 183);
            this.LabelExpirationpolicy.Name = "LabelExpirationpolicy";
            this.LabelExpirationpolicy.Size = new System.Drawing.Size(86, 13);
            this.LabelExpirationpolicy.TabIndex = 6;
            this.LabelExpirationpolicy.Text = "Expiration policy:";
            // 
            // CboExpirationpolicy
            // 
            this.CboExpirationpolicy.FormattingEnabled = true;
            this.CboExpirationpolicy.Items.AddRange(new object[] {
            "Unlimited",
            "For Fixed Time (Please enter date in the Expired on field)"});
            this.CboExpirationpolicy.Location = new System.Drawing.Point(157, 183);
            this.CboExpirationpolicy.Name = "CboExpirationpolicy";
            this.CboExpirationpolicy.Size = new System.Drawing.Size(568, 21);
            this.CboExpirationpolicy.TabIndex = 7;
            this.CboExpirationpolicy.SelectedIndexChanged += new System.EventHandler(this.CboExpirationpolicy_SelectedIndexChanged);
            // 
            // LabelExpiredOn
            // 
            this.LabelExpiredOn.AutoSize = true;
            this.LabelExpiredOn.Location = new System.Drawing.Point(65, 213);
            this.LabelExpiredOn.Name = "LabelExpiredOn";
            this.LabelExpiredOn.Size = new System.Drawing.Size(60, 13);
            this.LabelExpiredOn.TabIndex = 8;
            this.LabelExpiredOn.Text = "Expired on:";
            this.LabelExpiredOn.Visible = false;
            // 
            // dateTimeExpiredon
            // 
            this.dateTimeExpiredon.Location = new System.Drawing.Point(157, 213);
            this.dateTimeExpiredon.Name = "dateTimeExpiredon";
            this.dateTimeExpiredon.Size = new System.Drawing.Size(214, 20);
            this.dateTimeExpiredon.TabIndex = 9;
            this.dateTimeExpiredon.Visible = false;
            // 
            // labelError
            // 
            this.labelError.AutoEllipsis = true;
            this.labelError.AutoSize = true;
            this.labelError.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.labelError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(73, 360);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(0, 13);
            this.labelError.TabIndex = 10;
            // 
            // TextboxKey
            // 
            this.TextboxKey.Location = new System.Drawing.Point(157, 242);
            this.TextboxKey.Multiline = true;
            this.TextboxKey.Name = "TextboxKey";
            this.TextboxKey.ReadOnly = true;
            this.TextboxKey.Size = new System.Drawing.Size(568, 80);
            this.TextboxKey.TabIndex = 11;
            this.TextboxKey.Visible = false;
            // 
            // LabelLicensekey
            // 
            this.LabelLicensekey.AutoSize = true;
            this.LabelLicensekey.Location = new System.Drawing.Point(65, 245);
            this.LabelLicensekey.Name = "LabelLicensekey";
            this.LabelLicensekey.Size = new System.Drawing.Size(81, 13);
            this.LabelLicensekey.TabIndex = 12;
            this.LabelLicensekey.Text = "Generated Key:";
            this.LabelLicensekey.Visible = false;
            // 
            // CheckedBoxListModuleNames
            // 
            this.CheckedBoxListModuleNames.CheckOnClick = true;
            this.CheckedBoxListModuleNames.FormattingEnabled = true;
            this.CheckedBoxListModuleNames.Location = new System.Drawing.Point(157, 80);
            this.CheckedBoxListModuleNames.Name = "CheckedBoxListModuleNames";
            this.CheckedBoxListModuleNames.Size = new System.Drawing.Size(568, 94);
            this.CheckedBoxListModuleNames.TabIndex = 15;
            // 
            // cmdGetLicenseDetails
            // 
            this.cmdGetLicenseDetails.Location = new System.Drawing.Point(598, 46);
            this.cmdGetLicenseDetails.Name = "cmdGetLicenseDetails";
            this.cmdGetLicenseDetails.Size = new System.Drawing.Size(127, 23);
            this.cmdGetLicenseDetails.TabIndex = 16;
            this.cmdGetLicenseDetails.Text = "Get License Details";
            this.cmdGetLicenseDetails.UseVisualStyleBackColor = true;
            this.cmdGetLicenseDetails.Click += new System.EventHandler(this.cmdGetLicenseDetails_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.AutoEllipsis = true;
            this.labelMessage.AutoSize = true;
            this.labelMessage.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.ForeColor = System.Drawing.Color.Green;
            this.labelMessage.Location = new System.Drawing.Point(67, 360);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 13);
            this.labelMessage.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(769, 80);
            this.label1.TabIndex = 18;
            // 
            // LicenseGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 422);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.cmdGetLicenseDetails);
            this.Controls.Add(this.CheckedBoxListModuleNames);
            this.Controls.Add(this.LabelLicensekey);
            this.Controls.Add(this.TextboxKey);
            this.Controls.Add(this.dateTimeExpiredon);
            this.Controls.Add(this.LabelExpiredOn);
            this.Controls.Add(this.CboExpirationpolicy);
            this.Controls.Add(this.LabelExpirationpolicy);
            this.Controls.Add(this.cmdGenerate);
            this.Controls.Add(this.LabelModuleNames);
            this.Controls.Add(this.LabelDomainName);
            this.Controls.Add(this.TextboxDomainName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseGenerator";
            this.Text = "Emerge License Generator";
            ((System.ComponentModel.ISupportInitialize)(this.LGErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.TextBox TextboxDomainName;
        private System.Windows.Forms.Label LabelDomainName;
        private System.Windows.Forms.Label LabelModuleNames;
        private System.Windows.Forms.Button cmdGenerate;
        private System.Windows.Forms.ErrorProvider LGErrorProvider;
        private System.Windows.Forms.ComboBox CboExpirationpolicy;
        private System.Windows.Forms.Label LabelExpirationpolicy;
        private System.Windows.Forms.DateTimePicker dateTimeExpiredon;
        private System.Windows.Forms.Label LabelExpiredOn;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.TextBox TextboxKey;
        private System.Windows.Forms.Label LabelLicensekey;
        private System.Windows.Forms.CheckedListBox CheckedBoxListModuleNames;
        private System.Windows.Forms.Button cmdGetLicenseDetails;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label label1;
    }
}

