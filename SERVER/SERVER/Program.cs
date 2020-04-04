using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SERVER
{
    static class Headers
    {
        static public string user = "%";
        static public string message = "#";
        static public string diconnect = "^";
        static public string doneUser = "!";
        static public string doneMessage = "*";
        static public string doneDisconnect = "&";
    }

    public class ClientHandler
    {
        public TcpClient clientSocket = null;
        public string name = null;
        public NetworkStream networkStream = null;
        

        public ClientHandler(TcpClient _client, string _name)
        {
            this.clientSocket = _client;
            this.name = _name;
            this.networkStream = _client.GetStream();
            Console.WriteLine(this.name + " has connected");
        }
    }

    public class Server
    {
        int port = 8000;
        TcpListener server = null;
        NetworkStream nsServer = null;
        List<ClientHandler> clients;


        public Server()
        {
            clients = new List<ClientHandler>();
            Console.WriteLine("Server started ...");
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        
        void Run()
        {
            server = new TcpListener(port);
            server.Start();

            while(true)
            {
                TcpClient client = server.AcceptTcpClient();
                
                Thread thread = new Thread(() => ServerReceiveMessage(client));
                thread.Start();
            }
        }
        
        private void ServerReceiveMessage(TcpClient client)
        {
            nsServer = client.GetStream();

            
            while (true)
            {
                NetworkStream _ns = client.GetStream();
                string receive = GetNetworkStreamMessage(_ns);
                //Console.WriteLine("Receive=GetNetworkStream - " + receive);
                if (receive.StartsWith(Headers.user))
                {
                    string name = receive.Substring(1, receive.IndexOf(Headers.doneUser));
                    name = name.Replace(Headers.doneUser, String.Empty);
                    ClientHandler clientHandler = new ClientHandler(client, name);
                    clients.Add(clientHandler);
                    UpdateClientsList(clients);
                }



                if (receive.StartsWith(Headers.message))
                {
                    string _msg = "";
                    string _from = "";
                    string _to = "";
                    string _names = "";
                    Console.WriteLine("Line95 - " + receive);
                    receive = receive.Substring(1, receive.Length - 1);
                    Console.WriteLine("Line97 - " + receive);
                    if (receive.StartsWith(Headers.user))
                    {
                        _names = receive.Substring(1, receive.IndexOf(Headers.doneUser));
                        //Console.WriteLine(_names);
                        String[] splited = _names.Split(' ');
                        _from = splited[0];
                        _to = splited[1].Replace(Headers.doneUser, String.Empty);
                        _msg = receive.Substring(receive.IndexOf(Headers.doneUser));

                        _msg = _msg.Replace(Headers.doneUser, String.Empty);
                        _msg = _msg.Replace(Headers.doneMessage, String.Empty);
                        Console.WriteLine("From: " + _from + " to " + _to + " message: " + _msg);
                    }

                    foreach (var c in clients)
                    {
                        if (c.name == _to)
                        {
                            //Console.WriteLine("Mesaj de la " + _from + " catre: " + _to);

                            string send = Headers.message + Headers.user + _from + Headers.doneUser +
                                _msg + Headers.doneMessage;
                            byte[] sendMsg = Encoding.UTF8.GetBytes(send);

                            NetworkStream ns = c.clientSocket.GetStream();
                            ns.Write(sendMsg, 0, sendMsg.Length);
                            ns.Flush();
                        }
                    }
                }

                if(receive.StartsWith(Headers.diconnect))
                {
                    string disconnectUser = receive.Substring(1, receive.IndexOf(Headers.doneDisconnect));
                    disconnectUser = disconnectUser.Replace(Headers.doneDisconnect, String.Empty);

                    
                    for (int i = 0; i < clients.Count; i++)
                    {
                        if (disconnectUser == clients[i].name)
                        {
                            Console.WriteLine("S-a deconectat: " + disconnectUser);
                            clients.RemoveAt(i);
                            break;
                        }
                    }

                    if (clients.Count > 0)
                    {
                        foreach (var user in clients)
                        {
                            string send = Headers.diconnect + disconnectUser + Headers.doneDisconnect;
                            byte[] sendDisconnected = Encoding.UTF8.GetBytes(send);
                            NetworkStream ns = user.clientSocket.GetStream();
                            ns.Write(sendDisconnected, 0, sendDisconnected.Length);
                            ns.Flush();
                        }
                    }
                }
            }
        }

        private string GetNetworkStreamMessage(NetworkStream _ns)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (_ns.DataAvailable)
                {
                    int read = _ns.ReadByte();
                    if (read > 0)
                    {
                        sb.Append((char)read);
                    }
                    else break;
                }
                else if (sb.ToString().Length > 0) break;
            }
            return sb.ToString();
        }

        void UpdateClientsList(List<ClientHandler> _clients)
        {
            string send = "";
            foreach (var c in _clients)
            {
                send = Headers.user;
                foreach (var cc in clients)
                {
                    send += cc.name + " ";
                }
                send += Headers.doneUser;
                Console.WriteLine("Send to: " + c.name + " online list: " + send);

                if (c.clientSocket.Connected)
                {
                    NetworkStream ns = c.clientSocket.GetStream();
                    byte[] sendStream = Encoding.UTF8.GetBytes(send);
                    ns.Write(sendStream, 0, sendStream.Length);
                    ns.Flush();
                }
                else Console.WriteLine(c.name + " error");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            new Server();
        }
    }
}
