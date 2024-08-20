namespace Client
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInputIP = new System.Windows.Forms.TextBox();
            this.txtInputPort = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP ADDR :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "PORT NO :";
            // 
            // txtInputIP
            // 
            this.txtInputIP.Location = new System.Drawing.Point(86, 17);
            this.txtInputIP.Name = "txtInputIP";
            this.txtInputIP.Size = new System.Drawing.Size(231, 21);
            this.txtInputIP.TabIndex = 2;
            this.txtInputIP.Text = "localhost";
            this.txtInputIP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputKeyDown);
            // 
            // txtInputPort
            // 
            this.txtInputPort.Location = new System.Drawing.Point(86, 54);
            this.txtInputPort.Name = "txtInputPort";
            this.txtInputPort.Size = new System.Drawing.Size(100, 21);
            this.txtInputPort.TabIndex = 3;
            this.txtInputPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputKeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(242, 57);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
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
            this.Text = "CLIENT SETTING";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInputIP;
        private System.Windows.Forms.TextBox txtInputPort;
        private System.Windows.Forms.Button btnOK;
    }
}