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

namespace Lab3
{
    class SmtpClient
    {
        public SmtpClient()
        {

        }

        string server;
        int port;
        string username;
        string password;
        string subject = "PS LAB LATO 2018";
        string body = "Mail testujacy program, Patryk Prokurat";

        Socket socket;

        public bool started;

        public void Check()
        {
            Configuration();
            Console.WriteLine(server + " " + port + " " + username + " " + password);
        }

        public void Configuration()
        {
            started = true;

            server = ConfigurationManager.AppSettings["server"].ToString();
            port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            username = ConfigurationManager.AppSettings["username"].ToString();
            password = ConfigurationManager.AppSettings["password"].ToString();
            
        }

        public void Connect()
        {
            if (socket != null)
            {
                socket.Close();
            }

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(server, port);
            socket.ReceiveTimeout = 1000;
        }

        public void SendMessage()
        {
            var usernameBytes = System.Text.Encoding.UTF8.GetBytes(username);
            string usernameB64 = System.Convert.ToBase64String(usernameBytes);

            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            string passwordB64 = System.Convert.ToBase64String(passwordBytes);

           

            string outputString;

            
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("1. "+ outputString);
            socket.Send(Encoding.UTF8.GetBytes("EHLO\r\n"));
            
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("2. "+ outputString);
            socket.Send(Encoding.UTF8.GetBytes("AUTH LOGIN\r\n"));

            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("3. " + outputString);
            socket.Send(Encoding.UTF8.GetBytes(usernameB64 + "\r\n"));

            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("4. " + outputString);
            socket.Send(Encoding.UTF8.GetBytes(passwordB64 + "\r\n"));

            //Thread.Sleep(1000);
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("5. " + outputString);
            socket.Send(Encoding.UTF8.GetBytes("mail from:<" + username + "@o2.pl>\r\n"));

            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("6. " + outputString);
            socket.Send(Encoding.UTF8.GetBytes("rcpt to:<prokurat.p@gmail.com>\r\n"));

            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("7. " + outputString);
            socket.Send(Encoding.UTF8.GetBytes("data\r\n"));

            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("8. " + outputString);
            socket.Send(Encoding.UTF8.GetBytes("From: pp32818@o2.pl\r\nTo: prokurat.p@gmail.com\r\nSubject: "+subject+"\r\n"+body+"\r\n.\r\n"));

            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("9. " + outputString);
            socket.Send(Encoding.UTF8.GetBytes("quit\r\n"));

            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("10. " + outputString);

        }

        private string GetCurrentSocketBuffer(Socket currentSocket)
        {
            string currentBytesString = "";

            byte[] recBuffer = new byte[currentSocket.ReceiveBufferSize];
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

    }
}
