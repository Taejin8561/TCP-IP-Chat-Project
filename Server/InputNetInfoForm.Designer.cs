namespace Server
{
    partial class InputNetInfoForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtInputPort = new System.Windows.Forms.TextBox();
            this.txtInputIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(245, 54);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtInputPort
            // 
            this.txtInputPort.Location = new System.Drawing.Point(89, 51);
            this.txtInputPort.Name = "txtInputPort";
            this.txtInputPort.Size = new System.Drawing.Size(100, 21);
            this.txtInputPort.TabIndex = 8;
            this.txtInputPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputKeyDown);
            // 
            // txtInputIP
            // 
            this.txtInputIP.Location = new System.Drawing.Point(89, 14);
            this.txtInputIP.Name = "txtInputIP";
            this.txtInputIP.Size = new System.Drawing.Size(231, 21);
            this.txtInputIP.TabIndex = 7;
            this.txtInputIP.Text = "localhost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "PORT NO :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP ADDR :";
            // 
            // InputNetInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 91);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtInputPort);
            this.Controls.Add(this.txtInputIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "InputNetInfoForm";
            this.Text = "SERVER SETTING";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtInputPort;
        private System.Windows.Forms.TextBox txtInputIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}