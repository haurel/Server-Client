namespace CLIENT
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
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.textBoxConnect = new System.Windows.Forms.TextBox();
            this.btnConenct = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.listBoxOnline = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBoxMessage.Enabled = false;
            this.richTextBoxMessage.Location = new System.Drawing.Point(160, 31);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.ReadOnly = true;
            this.richTextBoxMessage.Size = new System.Drawing.Size(400, 256);
            this.richTextBoxMessage.TabIndex = 0;
            this.richTextBoxMessage.Text = "";
            // 
            // textBoxConnect
            // 
            this.textBoxConnect.Location = new System.Drawing.Point(160, 10);
            this.textBoxConnect.Name = "textBoxConnect";
            this.textBoxConnect.Size = new System.Drawing.Size(319, 20);
            this.textBoxConnect.TabIndex = 1;
            // 
            // btnConenct
            // 
            this.btnConenct.Location = new System.Drawing.Point(485, 8);
            this.btnConenct.Name = "btnConenct";
            this.btnConenct.Size = new System.Drawing.Size(75, 23);
            this.btnConenct.TabIndex = 2;
            this.btnConenct.Text = "Connect";
            this.btnConenct.UseVisualStyleBackColor = true;
            this.btnConenct.Click += new System.EventHandler(this.btnConenct_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(485, 288);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(160, 290);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(319, 20);
            this.textBoxSend.TabIndex = 4;
            this.textBoxSend.TextChanged += new System.EventHandler(this.textBoxSend_TextChanged);
            // 
            // listBoxOnline
            // 
            this.listBoxOnline.FormattingEnabled = true;
            this.listBoxOnline.Location = new System.Drawing.Point(12, 12);
            this.listBoxOnline.Name = "listBoxOnline";
            this.listBoxOnline.Size = new System.Drawing.Size(120, 277);
            this.listBoxOnline.TabIndex = 5;
            this.listBoxOnline.SelectedIndexChanged += new System.EventHandler(this.listBoxOnline_SelectedIndexChanged);
            this.listBoxOnline.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxOnline_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mesaj catre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "&&&&";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 318);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxOnline);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConenct);
            this.Controls.Add(this.textBoxConnect);
            this.Controls.Add(this.richTextBoxMessage);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(588, 357);
            this.MinimumSize = new System.Drawing.Size(588, 357);
            this.Name = "Form1";
            this.Text = "Chat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.RichTextBox richTextBoxMessage;
        public System.Windows.Forms.TextBox textBoxConnect;
        public System.Windows.Forms.ListBox listBoxOnline;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnConenct;
        public System.Windows.Forms.Button btnSend;
        public System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Label label2;
    }
}

