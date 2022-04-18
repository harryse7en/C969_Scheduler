
namespace Scheduler.Forms
{
    partial class ReportForm
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
            this.btnRpt1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRpt3 = new System.Windows.Forms.Button();
            this.btnRpt2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textRpt = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioDay = new System.Windows.Forms.RadioButton();
            this.radioWeek = new System.Windows.Forms.RadioButton();
            this.radioMonth = new System.Windows.Forms.RadioButton();
            this.radioAll = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRpt1
            // 
            this.btnRpt1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRpt1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRpt1.Location = new System.Drawing.Point(15, 70);
            this.btnRpt1.Name = "btnRpt1";
            this.btnRpt1.Size = new System.Drawing.Size(164, 27);
            this.btnRpt1.TabIndex = 32;
            this.btnRpt1.Text = "Appt. Types by Month";
            this.btnRpt1.UseVisualStyleBackColor = true;
            this.btnRpt1.Click += new System.EventHandler(this.btnRpt1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRpt3);
            this.groupBox1.Controls.Add(this.btnRpt2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnRpt1);
            this.groupBox1.Controls.Add(this.textRpt);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(6, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(849, 493);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reports";
            // 
            // btnRpt3
            // 
            this.btnRpt3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRpt3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRpt3.Location = new System.Drawing.Point(15, 136);
            this.btnRpt3.Name = "btnRpt3";
            this.btnRpt3.Size = new System.Drawing.Size(164, 27);
            this.btnRpt3.TabIndex = 38;
            this.btnRpt3.Text = "Total Appts. by Consultant";
            this.btnRpt3.UseVisualStyleBackColor = true;
            this.btnRpt3.Click += new System.EventHandler(this.btnRpt3_Click);
            // 
            // btnRpt2
            // 
            this.btnRpt2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRpt2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRpt2.Location = new System.Drawing.Point(15, 103);
            this.btnRpt2.Name = "btnRpt2";
            this.btnRpt2.Size = new System.Drawing.Size(164, 27);
            this.btnRpt2.TabIndex = 37;
            this.btnRpt2.Text = "Consultant Schedules";
            this.btnRpt2.UseVisualStyleBackColor = true;
            this.btnRpt2.Click += new System.EventHandler(this.btnRpt2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(12, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 17);
            this.label5.TabIndex = 36;
            this.label5.Text = "Please choose a report:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(658, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 35;
            // 
            // textRpt
            // 
            this.textRpt.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRpt.Location = new System.Drawing.Point(201, 28);
            this.textRpt.Name = "textRpt";
            this.textRpt.ReadOnly = true;
            this.textRpt.Size = new System.Drawing.Size(631, 459);
            this.textRpt.TabIndex = 32;
            this.textRpt.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(51, 460);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 15);
            this.label1.TabIndex = 31;
            this.label1.Text = "(All times shown for local timezone)";
            // 
            // radioDay
            // 
            this.radioDay.AutoSize = true;
            this.radioDay.Location = new System.Drawing.Point(653, 121);
            this.radioDay.Name = "radioDay";
            this.radioDay.Size = new System.Drawing.Size(58, 17);
            this.radioDay.TabIndex = 40;
            this.radioDay.TabStop = true;
            this.radioDay.Text = "by Day";
            this.radioDay.UseVisualStyleBackColor = true;
            // 
            // radioWeek
            // 
            this.radioWeek.AutoSize = true;
            this.radioWeek.Location = new System.Drawing.Point(653, 98);
            this.radioWeek.Name = "radioWeek";
            this.radioWeek.Size = new System.Drawing.Size(68, 17);
            this.radioWeek.TabIndex = 39;
            this.radioWeek.TabStop = true;
            this.radioWeek.Text = "by Week";
            this.radioWeek.UseVisualStyleBackColor = true;
            // 
            // radioMonth
            // 
            this.radioMonth.AutoSize = true;
            this.radioMonth.Location = new System.Drawing.Point(653, 75);
            this.radioMonth.Name = "radioMonth";
            this.radioMonth.Size = new System.Drawing.Size(69, 17);
            this.radioMonth.TabIndex = 38;
            this.radioMonth.TabStop = true;
            this.radioMonth.Text = "by Month";
            this.radioMonth.UseVisualStyleBackColor = true;
            // 
            // radioAll
            // 
            this.radioAll.AutoSize = true;
            this.radioAll.Location = new System.Drawing.Point(653, 52);
            this.radioAll.Name = "radioAll";
            this.radioAll.Size = new System.Drawing.Size(103, 17);
            this.radioAll.TabIndex = 37;
            this.radioAll.TabStop = true;
            this.radioAll.Text = "All Appointments";
            this.radioAll.UseVisualStyleBackColor = true;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 505);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radioDay);
            this.Controls.Add(this.radioWeek);
            this.Controls.Add(this.radioMonth);
            this.Controls.Add(this.radioAll);
            this.Name = "ReportForm";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRpt1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioDay;
        private System.Windows.Forms.RadioButton radioWeek;
        private System.Windows.Forms.RadioButton radioMonth;
        private System.Windows.Forms.RadioButton radioAll;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RichTextBox textRpt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRpt2;
        private System.Windows.Forms.Button btnRpt3;
    }
}