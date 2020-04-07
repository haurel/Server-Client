using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENT
{
    
    public partial class Form1 : Form
    {
        Client client;
        public string currentNameClient = null;
        public string sendMessageTo = null;
        public Form1()
        {
            InitializeComponent();
            btnSend.Enabled = false;
            textBoxSend.Enabled = false;
        }

        private void btnConenct_Click(object sender, EventArgs e)
        {
            currentNameClient = textBoxConnect.Text;
            client = new Client(currentNameClient, this);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.richTextBoxMessage.AppendText("me: " + textBoxSend.Text + Environment.NewLine);

            if(textBoxSend.Text.Length > 0)
            client.SendMessage(textBoxSend.Text, label2.Text, currentNameClient);
            textBoxSend.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null)
            {
                client.Disconnect();
            }
        }

        private void listBoxOnline_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void listBoxOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = listBoxOnline.SelectedItem.ToString();
        }

        private void textBoxSend_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSend.Text.Length > 0) btnSend.Enabled = true;
            else btnSend.Enabled = false;
        }

        private void richTextBoxMessage_TextChanged(object sender, EventArgs e)
        {
            string lastLine;
            if (richTextBoxMessage.Lines.Any())
            {
                lastLine = richTextBoxMessage.Lines[richTextBoxMessage.Lines.Length - 1];
                if (lastLine == "ERROR")
                {
                    client = null;
                }
            }

        }
    }
}