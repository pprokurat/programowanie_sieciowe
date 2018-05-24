using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab2_tmp
{
    class Pop3Client
    {
        public Pop3Client()
        {

        }

        string server;
        int port;
        string username;
        string password;
        int time;

        Socket socket;
        Thread pop3Thread;

        public bool started;

        public void Configuration()
        {
            started = true;

            server = ConfigurationManager.AppSettings["server"].ToString();
            port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            username = ConfigurationManager.AppSettings["username"].ToString();
            password = ConfigurationManager.AppSettings["password"].ToString();
            time = Convert.ToInt32(ConfigurationManager.AppSettings["time"]);

            if (pop3Thread == null)
            {
                pop3Thread = new Thread(this.POP3Thread);
                pop3Thread.Start();
            }
        }

        public void Check()
        {
            Configuration();
            Console.WriteLine(server + " " + port + " " + username + " " + password + " " + time);
        }

        private void NewSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(server, port);

            socket.Send(Encoding.UTF8.GetBytes("user " + username + "\r\n"));
            socket.Send(Encoding.UTF8.GetBytes("pass " + password + "\r\n"));            
        }

        private string GetCurrentSocketBuffer(Socket currentSocket)
        {
            string currentBytesString = "";

            byte[] recBuffer = new byte[socket.ReceiveBufferSize];

            try
            {
                socket.Receive(recBuffer);
            }
            catch
            {
                Console.WriteLine("Error: lost connection.");
            }

            currentBytesString += new string(Encoding.UTF8.GetChars(recBuffer)).Replace("\0", string.Empty);

            return currentBytesString;
        }

        public void GetMailList()
        {
            NewSocket();
            string buffer = GetCurrentSocketBuffer(socket);
            Console.WriteLine(buffer);
        }

        private void POP3Thread()
        {

            Console.WriteLine("Client started");

            while (started)
            {
                GetMailList();
                Thread.Sleep(time * 1000);
            }
        }
    }
}
