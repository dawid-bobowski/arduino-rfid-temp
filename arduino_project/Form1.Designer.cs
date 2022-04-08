
namespace arduino_project
{
    partial class Form1
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
            this.labelLogs = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelLogs
            // 
            this.labelLogs.AutoSize = true;
            this.labelLogs.Location = new System.Drawing.Point(36, 56);
            this.labelLogs.Name = "labelLogs";
            this.labelLogs.Size = new System.Drawing.Size(115, 13);
            this.labelLogs.TabIndex = 1;
            this.labelLogs.Text = "Company thermometer:";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(453, 117);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(443, 91);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxIP.TabIndex = 3;
            this.textBoxIP.Text = "192.168.0.100";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(427, 94);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(20, 13);
            this.labelIP.TabIndex = 4;
            this.labelIP.Text = "IP:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(453, 147);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.Location = new System.Drawing.Point(36, 75);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(356, 290);
            this.listBoxLogs.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 450);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelLogs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelLogs;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ListBox listBoxLogs;
    }
}

