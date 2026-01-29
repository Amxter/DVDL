namespace Driving___Vehicle_License_Department.People
{
    partial class FindPerson
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
            this.filterPerson1 = new Driving___Vehicle_License_Department.People.User_Controls.FilterPerson();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filterPerson1
            // 
            this.filterPerson1.FilterEnabled = true;
            this.filterPerson1.Location = new System.Drawing.Point(-1, 89);
            this.filterPerson1.Name = "filterPerson1";
            this.filterPerson1.ShowAddPerson = true;
            this.filterPerson1.Size = new System.Drawing.Size(846, 441);
            this.filterPerson1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(306, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find Person";
            // 
            // button2
            // 
            this.button2.Image = global::Driving___Vehicle_License_Department.Properties.Resources.Close_321;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(736, 520);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 41);
            this.button2.TabIndex = 30;
            this.button2.Text = "        Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FindPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 569);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterPerson1);
            this.Name = "FindPerson";
            this.Text = "Find Person";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private User_Controls.FilterPerson filterPerson1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}