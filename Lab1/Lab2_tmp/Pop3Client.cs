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

        public class MailInfo
        {
            public int mailID;
            public string subject;
        }

        List<MailInfo> mailInfo = new List<MailInfo>();

        int mailCount = 0;
        int totalMailCount = 0;

        public void Check()
        {
            Configuration();
            Console.WriteLine(server + " " + port + " " + username + " " + password + " " + time);
        }

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

        private void POP3Thread()
        {

            Console.WriteLine("Client started");

            while (started)
            {
                GetMailList();
                Thread.Sleep(time * 1000);
                Console.WriteLine("\n");
            }
        }

        public void GetMailList()
        {
            NewSocket();
            socket.Send(Encoding.UTF8.GetBytes("uidl\r\n"));

            Thread.Sleep(1000);

            string buffer = GetCurrentSocketBuffer(socket);
            Console.WriteLine("buffer: "+buffer);

            string[] lines = buffer.Split('\n');

            int counter = 0;                                   

            foreach (string line in lines)
            {
                string[] words = line.Split(' ');
                if (words[0]!="+OK" && words[0]!="+ERR" && words.Length > 1)
                {
                    //Console.WriteLine(words[0]);
                    counter++;
                    NewMessage(counter);
                }                
            }

            int messageCount = mailInfo.Count;
            for (int i = 0; i < messageCount; ++i)
            {
                MailInfo mailInf = mailInfo[i];
                Console.WriteLine("Id: " + mailInf.mailID +
                    "\nTemat: " + mailInf.subject);

                mailCount = i;
            }

            if (mailCount > totalMailCount)
            {
                Console.WriteLine("Nowa wiadomosc!\n");
                totalMailCount = mailCount;
            }
                

            mailInfo.Clear();

            //try
            //{
            //    foreach (MailInfo mailInf in mailInfo)
            //    {
            //        mailInfo.Remove(mailInf);
            //    }
            //}
            //catch
            //{
            //    Console.WriteLine("error");
            //}

            //Console.WriteLine("\n\n");

            //if (counter > messageCount)
            //    Console.WriteLine("Nowa wiadomosc!\n");

            //messageCount = counter;
            //Console.WriteLine("Ilosc wiadomosci: "+messageCount);
        }

        private void NewSocket()
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
        }

        private void NewMessage(int id)
        {
            socket.Send(Encoding.UTF8.GetBytes("retr " + id + "\r\n"));

            Thread.Sleep(1000);

            string outputString = GetCurrentSocketBuffer(socket);
            //Console.WriteLine("output: " + outputString);

            string[] lines = outputString.Split('\n');

            for (int i = 0; i<lines.Length; i++)
            {
                if (lines[i].Contains("Subject"))
                {
                    mailInfo.Add(new MailInfo { mailID = id, subject = lines[i] });
                    //Console.WriteLine(lines[i]);
                    break;
                }                    
            }                          
            
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
               
        public void Exit()
        {
            if (pop3Thread != null)
            {

                pop3Thread.Abort();

                if (socket != null)
                    socket.Close();
            }
        }


    }
}
