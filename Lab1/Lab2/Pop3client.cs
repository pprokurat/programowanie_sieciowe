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

namespace Lab2
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

        Thread pop3thread;
        Socket socket;

        public bool started;
        
        public struct MailInfo
        {
            public int mailID;
            public string title;
        }

        List<MailInfo> mailInfo;

        public void Configuration()
        {
            mailInfo = new List<MailInfo>();
        
            started = true;

            server = ConfigurationManager.AppSettings["server"].ToString();
            port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            username = ConfigurationManager.AppSettings["username"].ToString();
            password = ConfigurationManager.AppSettings["password"].ToString();
            time = Convert.ToInt32(ConfigurationManager.AppSettings["time"]);

            if (pop3thread == null)
            {
                pop3thread = new Thread(this.POP3Thread);
                pop3thread.Start();
            }

        }

        public void Check()
        {
            Configuration();
            Console.WriteLine(server + " " + port + " " + username + " " + password + " " + time);
        }

        public void Connect()
        {
            if (socket != null)
            {
                socket.Close();
            }

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(server, port);
            socket.ReceiveTimeout = 5000;

            socket.Send(Encoding.UTF8.GetBytes("user " + username + "\r\n"));
            socket.Send(Encoding.UTF8.GetBytes("pass " + password + "\r\n"));

            string outputString = GetCurrentSocketInfo(socket);
        }

        private string GetCurrentSocketInfo(Socket currentSocket)
        {
            StringBuilder builder = new StringBuilder();


            int read = 0;


            string currentBytesString = "";

            byte[] currentBytes = new byte[currentSocket.ReceiveBufferSize];
            var canRead = true;

            do
            {

                canRead = currentSocket.Poll(3000000, SelectMode.SelectRead);

                if (canRead)
                {
                    try
                    {
                        read = currentSocket.Receive(currentBytes);
                        currentBytesString += new string(Encoding.UTF8.GetChars(currentBytes)).Replace("\0", string.Empty);
                    }
                    catch
                    {
                        Console.WriteLine("Error: lost connection with mail server. Attempting to reconnect...");
                    }
                }

            } while (read > 0 && canRead);

            return currentBytesString;

        }

        private void NewMessage(int id)
        {
            socket.Send(Encoding.UTF8.GetBytes("retr " + id + "\r\n"));
            string outputString = GetCurrentSocketInfo(socket);


            string[] words = outputString.Split('\n');

            int i = 0;

            do
                i++;
            while (!words[i].Contains("Subject"));

            string[] values = words[i].Split(':');

            MailInfo currentMailInfo = new MailInfo();
            currentMailInfo.mailID = id;
            currentMailInfo.title = values[1];

            mailInfo.Add(currentMailInfo);

            Console.WriteLine("You received a new message: '" + currentMailInfo.title + "'");

        }

        public void GetMailList()
        {
            Connect();
            socket.Send(Encoding.UTF8.GetBytes("uidl\r\n"));

            string outputBuff = GetCurrentSocketInfo(socket);

            string[] words = outputBuff.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return;

            if (words[0] == "+OK")
            {

                for (int i = 1; i < words.Length; i = i + 2)
                {
                        if (words[i][0] == 0 || words[i][0] == '.')
                            return;

                        int mailID = Convert.ToInt32(words[i]);

                        int idx = mailInfo.FindIndex(x => x.mailID == mailID);


                        if (idx == -1)
                        {
                            NewMessage(mailID);
                        }                 
                }
            }
        }

        

        private void POP3Thread()
        {
            while (started)
            {
                GetMailList();
                Thread.Sleep(time * 1000);
            }
        }

    }
}
