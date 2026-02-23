namespace DrivingVehicleLicenseDepartment.Applications.Local_Driving_License
{
    partial class LDLApplicationInfo
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
            this.btnClose = new System.Windows.Forms.Button();
            this.localDrivingLicenseApplicationInfo1 = new DrivingVehicleLicenseDepartment.Applications.Application.LocalDrivingLicenseApplicationInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::DrivingVehicleLicenseDepartment.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(774, 374);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // localDrivingLicenseApplicationInfo1
            // 
            this.localDrivingLicenseApplicationInfo1.IsShowLicenseInfoEnabled = false;
            this.localDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(3, 3);
            this.localDrivingLicenseApplicationInfo1.Name = "localDrivingLicenseApplicationInfo1";
            this.localDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(902, 363);
            this.localDrivingLicenseApplicationInfo1.TabIndex = 19;
            // 
            // LDLApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 417);
            this.Controls.Add(this.localDrivingLicenseApplicationInfo1);
            this.Controls.Add(this.btnClose);
            this.Name = "LDLApplicationInfo";
            this.Text = "LDLApplicationInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private Application.LocalDrivingLicenseApplicationInfo localDrivingLicenseApplicationInfo1;
    }
}