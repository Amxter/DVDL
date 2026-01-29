namespace PresentationDVLD
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLicenseApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationaLicenseApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drivingLicensesServicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrivengLicensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intrnationalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accoutSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentUserInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationsToolStripMenuItem,
            this.peToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.accoutSettingsToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // applicationsToolStripMenuItem
            // 
            this.applicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationTypesToolStripMenuItem,
            this.maToolStripMenuItem,
            this.manageApplicationsToolStripMenuItem,
            this.drivingLicensesServicesToolStripMenuItem});
            this.applicationsToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Applications_64;
            this.applicationsToolStripMenuItem.Name = "applicationsToolStripMenuItem";
            resources.ApplyResources(this.applicationsToolStripMenuItem, "applicationsToolStripMenuItem");
            // 
            // applicationTypesToolStripMenuItem
            // 
            this.applicationTypesToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Application_Types_64;
            this.applicationTypesToolStripMenuItem.Name = "applicationTypesToolStripMenuItem";
            resources.ApplyResources(this.applicationTypesToolStripMenuItem, "applicationTypesToolStripMenuItem");
            this.applicationTypesToolStripMenuItem.Click += new System.EventHandler(this.applicationTypesToolStripMenuItem_Click);
            // 
            // maToolStripMenuItem
            // 
            this.maToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Test_Type_64;
            this.maToolStripMenuItem.Name = "maToolStripMenuItem";
            resources.ApplyResources(this.maToolStripMenuItem, "maToolStripMenuItem");
            this.maToolStripMenuItem.Click += new System.EventHandler(this.maToolStripMenuItem_Click);
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLicenseApplicationsToolStripMenuItem,
            this.internationaLicenseApplicationsToolStripMenuItem});
            this.manageApplicationsToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Manage_Applications_64;
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            resources.ApplyResources(this.manageApplicationsToolStripMenuItem, "manageApplicationsToolStripMenuItem");
            // 
            // localDrivingLicenseApplicationsToolStripMenuItem
            // 
            this.localDrivingLicenseApplicationsToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.License_View_32;
            this.localDrivingLicenseApplicationsToolStripMenuItem.Name = "localDrivingLicenseApplicationsToolStripMenuItem";
            resources.ApplyResources(this.localDrivingLicenseApplicationsToolStripMenuItem, "localDrivingLicenseApplicationsToolStripMenuItem");
            this.localDrivingLicenseApplicationsToolStripMenuItem.Click += new System.EventHandler(this.localDrivingLicenseApllicationsToolStripMenuItem_Click);
            // 
            // internationaLicenseApplicationsToolStripMenuItem
            // 
            this.internationaLicenseApplicationsToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.International_32;
            this.internationaLicenseApplicationsToolStripMenuItem.Name = "internationaLicenseApplicationsToolStripMenuItem";
            resources.ApplyResources(this.internationaLicenseApplicationsToolStripMenuItem, "internationaLicenseApplicationsToolStripMenuItem");
            // 
            // drivingLicensesServicesToolStripMenuItem
            // 
            this.drivingLicensesServicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrivengLicensesToolStripMenuItem});
            this.drivingLicensesServicesToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.LocalDriving_License;
            this.drivingLicensesServicesToolStripMenuItem.Name = "drivingLicensesServicesToolStripMenuItem";
            resources.ApplyResources(this.drivingLicensesServicesToolStripMenuItem, "drivingLicensesServicesToolStripMenuItem");
            // 
            // newDrivengLicensesToolStripMenuItem
            // 
            this.newDrivengLicensesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.intrnationalLicenseToolStripMenuItem,
            this.localLicenseToolStripMenuItem});
            this.newDrivengLicensesToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.New_Driving_License_32;
            this.newDrivengLicensesToolStripMenuItem.Name = "newDrivengLicensesToolStripMenuItem";
            resources.ApplyResources(this.newDrivengLicensesToolStripMenuItem, "newDrivengLicensesToolStripMenuItem");
            // 
            // intrnationalLicenseToolStripMenuItem
            // 
            this.intrnationalLicenseToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.International_32;
            this.intrnationalLicenseToolStripMenuItem.Name = "intrnationalLicenseToolStripMenuItem";
            resources.ApplyResources(this.intrnationalLicenseToolStripMenuItem, "intrnationalLicenseToolStripMenuItem");
            // 
            // localLicenseToolStripMenuItem
            // 
            this.localLicenseToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Local_32;
            this.localLicenseToolStripMenuItem.Name = "localLicenseToolStripMenuItem";
            resources.ApplyResources(this.localLicenseToolStripMenuItem, "localLicenseToolStripMenuItem");
            this.localLicenseToolStripMenuItem.Click += new System.EventHandler(this.localLicenseToolStripMenuItem_Click);
            // 
            // peToolStripMenuItem
            // 
            this.peToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.People_64;
            this.peToolStripMenuItem.Name = "peToolStripMenuItem";
            resources.ApplyResources(this.peToolStripMenuItem, "peToolStripMenuItem");
            this.peToolStripMenuItem.Click += new System.EventHandler(this.peToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Users_2_400;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            resources.ApplyResources(this.usersToolStripMenuItem, "usersToolStripMenuItem");
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // accoutSettingsToolStripMenuItem
            // 
            this.accoutSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentUserInfoToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparator1,
            this.signOutToolStripMenuItem});
            resources.ApplyResources(this.accoutSettingsToolStripMenuItem, "accoutSettingsToolStripMenuItem");
            this.accoutSettingsToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.account_settings_64;
            this.accoutSettingsToolStripMenuItem.Name = "accoutSettingsToolStripMenuItem";
            // 
            // currentUserInfoToolStripMenuItem
            // 
            this.currentUserInfoToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.PersonDetails_32;
            this.currentUserInfoToolStripMenuItem.Name = "currentUserInfoToolStripMenuItem";
            resources.ApplyResources(this.currentUserInfoToolStripMenuItem, "currentUserInfoToolStripMenuItem");
            this.currentUserInfoToolStripMenuItem.Click += new System.EventHandler(this.currentUserInfoToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Password_32;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            resources.ApplyResources(this.changePasswordToolStripMenuItem, "changePasswordToolStripMenuItem");
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Image = global::Driving___Vehicle_License_Department.Properties.Resources.sign_out_32__2;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            resources.ApplyResources(this.signOutToolStripMenuItem, "signOutToolStripMenuItem");
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem peToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accoutSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentUserInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLicenseApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationaLicenseApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drivingLicensesServicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDrivengLicensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intrnationalLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localLicenseToolStripMenuItem;
    }
}

