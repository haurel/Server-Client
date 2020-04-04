using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CLIENT
{
    class Client
    {
        string hostname = "localhost";
        int port = 8000;

        TcpClient client = null;
        NetworkStream nsCliet = null;
        string name = null;
        bool connected = false;
        public Thread threadClient = null;
        private Form1 chat;
        
        public Client(string _name, Form1 _chat)
        {
            this.chat = _chat;
            try
            {
                this.client = new TcpClient(hostname, port);
                this.nsCliet = client.GetStream();
                this.name = _name;

                this.connected = true;
                
                this.chat.textBoxConnect.Enabled = false;
                this.chat.btnConenct.Enabled = false;
                this.chat.textBoxSend.Enabled = true;
                SendConnection();

                threadClient = new Thread(() => ListenToServer());
                threadClient.Start();
            }
            catch (Exception)
            {
                this.chat.richTextBoxMessage.AppendText("Server Offline!" + Environment.NewLine);
            }
        }

        private void SendConnection()
        {
            string nameSend = Headers.user + this.name + Headers.doneUser;
            byte[] sendStream = Encoding.UTF8.GetBytes(nameSend);
            nsCliet.Write(sendStream, 0, sendStream.Length);
            nsCliet.Flush();
        }

        public void ListenToServer()
        {
            while (true)
            {
                try
                {
                    string message = GetReceiveMessage(nsCliet);
                    string _tempName = "";

                    if (message.StartsWith(Headers.user))
                    {
                        if (this.chat.listBoxOnline.InvokeRequired)
                        {
                            this.chat.listBoxOnline.Invoke((MethodInvoker)delegate
                            {
                                this.chat.listBoxOnline.Items.Clear();
                            });
                        }
                        _tempName = message.Substring(1, message.IndexOf(Headers.doneUser));
                        _tempName = _tempName.Replace(Headers.doneUser, String.Empty);
                        String[] splited = _tempName.Split(' ');
                        if (this.chat.listBoxOnline.InvokeRequired)
                        {
                            this.chat.listBoxOnline.Invoke((MethodInvoker)delegate
                            {
                                foreach (var user in splited)
                                {
                                    this.chat.listBoxOnline.Items.Add(user);
                                }
                            });
                        }
                        else
                        {
                            foreach (var user in splited)
                            {
                                this.chat.listBoxOnline.Items.Add(user);
                            }
                        }
                       
                    }
                    if (message.StartsWith(Headers.message))
                    {
                        string _name = "";
                        string _msg = "";
                        message = message.Substring(1);
                        if (message.StartsWith(Headers.user))
                        {
                            _name = message.Substring(1, message.IndexOf(Headers.doneUser));
                            _name = _name.Replace(Headers.doneUser, String.Empty);

                            _msg = message.Substring(message.IndexOf(Headers.doneUser));
                            _msg = _msg.Replace(Headers.doneUser, String.Empty);
                            _msg = _msg.Replace(Headers.doneMessage, String.Empty);

                            if (this.chat.richTextBoxMessage.InvokeRequired)
                            {
                                this.chat.richTextBoxMessage.Invoke((MethodInvoker)delegate
                                {
                                    this.chat.richTextBoxMessage.AppendText(
                                        _name + ": " + _msg + Environment.NewLine);
                                });
                            }
                            else
                            {
                                this.chat.richTextBoxMessage.AppendText(
                                    _name + ": " + _msg + Environment.NewLine);
                            }
                        }
                    }
                    if (message.StartsWith(Headers.diconnect))
                    {
                        string disconnectUser = message.Substring(1, message.IndexOf(Headers.doneDisconnect));
                        disconnectUser = disconnectUser.Replace(Headers.doneDisconnect, String.Empty);

                        if (this.chat.listBoxOnline.InvokeRequired)
                        {
                            this.chat.listBoxOnline.Invoke((MethodInvoker)delegate
                            {
                                this.chat.listBoxOnline.Items.Remove(disconnectUser);
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    this.chat.richTextBoxMessage.AppendText("Server down!" + Environment.NewLine);
                    this.connected = false;

                    this.chat.textBoxConnect.Enabled = true;
                    this.chat.btnConenct.Enabled = true;
                    this.chat.btnSend.Enabled = false;
                    this.chat.textBoxSend.Enabled = false;
                }
            }
        }

        private string GetReceiveMessage(NetworkStream _ns)
        {
            StringBuilder _receiveMessage = new StringBuilder();
            while (true)
            {
                if (_ns.DataAvailable)
                {
                    int read = _ns.ReadByte();
                    if (read > 0)
                    {
                        _receiveMessage.Append((char)read);
                    }
                    else { break; }
                }
                else if (_receiveMessage.ToString().Length > 0)
                {
                    break;
                }
            }
            return _receiveMessage.ToString();
        }

        public void SendMessage(string _msg, string _to, string _me)
        {
            try
            {
                NetworkStream ns = client.GetStream();
                _msg = Headers.message + Headers.user + _me + " " + _to +
                        Headers.doneUser + _msg + Headers.doneMessage;
                byte[] outStream = Encoding.UTF8.GetBytes(_msg);

                ns.Write(outStream, 0, outStream.Length);
                ns.Flush();
            }
            catch (Exception)
            {
                this.chat.richTextBoxMessage.AppendText("Server down!" + Environment.NewLine);
                this.connected = false;

                this.chat.textBoxConnect.Enabled = true;
                this.chat.btnConenct.Enabled = true;
                this.chat.btnSend.Enabled = false;
                this.chat.textBoxSend.Enabled = false;
            }
        }

        public void Disconnect()
        {
            if (this.client != null)
            {
                if (this.connected)
                {
                    NetworkStream ns = client.GetStream();
                    string _msg = Headers.diconnect + this.name + Headers.doneDisconnect;
                    byte[] outStream = Encoding.UTF8.GetBytes(_msg);
                    ns.Write(outStream, 0, outStream.Length);
                    ns.Flush();
                }
                client.Close();
                threadClient.Abort();
            }
        }

        public TcpClient GetClient()
        {
            return client;
        }
    }
}
