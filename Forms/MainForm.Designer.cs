
namespace Scheduler.Forms
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
            this.components = new System.ComponentModel.Container();
            this.btnExit = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnCust = new System.Windows.Forms.Button();
            this.btnRpt = new System.Windows.Forms.Button();
            this.btnAppt = new System.Windows.Forms.Button();
            this.tmr1000 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(106, 200);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 27);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Navy;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(264, 34);
            this.labelTitle.TabIndex = 9;
            this.labelTitle.Text = "C969 Scheduler Program";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCust
            // 
            this.btnCust.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCust.Location = new System.Drawing.Point(55, 46);
            this.btnCust.Name = "btnCust";
            this.btnCust.Size = new System.Drawing.Size(178, 40);
            this.btnCust.TabIndex = 26;
            this.btnCust.Text = "Customer Records";
            this.btnCust.UseVisualStyleBackColor = true;
            this.btnCust.Click += new System.EventHandler(this.btnCust_Click);
            // 
            // btnRpt
            // 
            this.btnRpt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRpt.Location = new System.Drawing.Point(55, 138);
            this.btnRpt.Name = "btnRpt";
            this.btnRpt.Size = new System.Drawing.Size(178, 40);
            this.btnRpt.TabIndex = 27;
            this.btnRpt.Text = "Reports";
            this.btnRpt.UseVisualStyleBackColor = true;
            this.btnRpt.Click += new System.EventHandler(this.btnRpt_Click);
            // 
            // btnAppt
            // 
            this.btnAppt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppt.Location = new System.Drawing.Point(55, 92);
            this.btnAppt.Name = "btnAppt";
            this.btnAppt.Size = new System.Drawing.Size(178, 40);
            this.btnAppt.TabIndex = 28;
            this.btnAppt.Text = "My Appointments";
            this.btnAppt.UseVisualStyleBackColor = true;
            this.btnAppt.Click += new System.EventHandler(this.btnAppt_Click);
            // 
            // tmr1000
            // 
            this.tmr1000.Enabled = true;
            this.tmr1000.Interval = 1000;
            this.tmr1000.Tick += new System.EventHandler(this.tmr1000_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 248);
            this.Controls.Add(this.btnAppt);
            this.Controls.Add(this.btnRpt);
            this.Controls.Add(this.btnCust);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.labelTitle);
            this.Name = "MainForm";
            this.Text = "Scheduler";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnCust;
        private System.Windows.Forms.Button btnRpt;
        private System.Windows.Forms.Button btnAppt;
        private System.Windows.Forms.Timer tmr1000;
    }
}