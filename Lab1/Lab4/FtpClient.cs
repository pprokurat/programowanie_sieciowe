using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using System.Net;
using System.IO;

namespace Lab4
{
    class FtpClient
    {
        public FtpClient()
        {

        }

        string server;
        int port;
        string dataServer;
        int dataServerPort;
        string username;
        string password;
        string directory;

        Socket socket;
        Socket dataSocket;

        public bool started;

        public void Check()
        {
            Configuration();
            //Console.WriteLine(port + " " + server + " " + username + " " + password);
        }

        public void Configuration()
        {
            started = true;

            server = ConfigurationManager.AppSettings["server"].ToString();
            port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            //dataServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["dataServerPort"]);
            username = ConfigurationManager.AppSettings["username"].ToString();
            password = ConfigurationManager.AppSettings["password"].ToString();
            directory = ConfigurationManager.AppSettings["directory"].ToString();

        }

        public void Connect()
        {
            if (socket != null)
            {
                socket.Close();
            }

            string outputString;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(server, port);
            socket.ReceiveTimeout = 1000;

            socket.Send(Encoding.UTF8.GetBytes("user " + username + "\r\n"));

            Thread.Sleep(1000);
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("1. " + outputString);

            socket.Send(Encoding.UTF8.GetBytes("pass " + password + "\r\n"));
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("2. " + outputString);

            if (outputString.Contains("230"))
            {                
                NewDataSocket();
                //Console.WriteLine("Connected successfully to FTP Server.\n");
            }
        }

        public void ShowDirectory()
        {
            string outputString;

            socket.Send(Encoding.UTF8.GetBytes("pwd\r\n"));
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("3. " + outputString);
            
            socket.Send(Encoding.UTF8.GetBytes("list\r\n"));
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("4. " + outputString);

            socket.Send(Encoding.UTF8.GetBytes("cwd " + directory + "\r\n"));
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("5. " + outputString);

            socket.Send(Encoding.UTF8.GetBytes("list\r\n"));
            outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine("6. " + outputString);

            //string output = GetCurrentSocketBuffer(dataSocket);
            //Console.WriteLine(output);                                  


        }

        public void ConnectViaLibrary()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + server + directory);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential(username, password);
            request.KeepAlive = false;
            request.UseBinary = true;
            request.UsePassive = true;

            try
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://"+server + directory);
                req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;


                req.Credentials = new NetworkCredential(username, password);
                req.KeepAlive = false;
                req.UseBinary = true;
                req.UsePassive = true;


                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                Console.WriteLine(reader.ReadToEnd());

                Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);

                reader.Close();
                response.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void NewDataSocket()
        {
            socket.Send(Encoding.UTF8.GetBytes("pasv\r\n"));

            string outputString = GetCurrentSocketBuffer(socket);
            Console.WriteLine(outputString);                       

            String[] numbers = Regex.Split(outputString, @"\D+");
            int idx = 0;

            for (int i = 0; i < numbers.Length; i++)
                if (numbers[i] == "227")
                {
                    idx = i;
                    break;
                }

            dataServer = numbers[idx + 1] + "." + numbers[idx + 2] + "." + numbers[idx + 3] + "." + numbers[idx + 4];
            dataServerPort = Int32.Parse(numbers[idx + 5]) * 256 + Int32.Parse(numbers[idx + 6]);

            //Console.WriteLine(dataServer + " " + dataServerPort);
            
            dataSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            dataSocket.Connect(dataServer, dataServerPort);
            dataSocket.ReceiveTimeout = 2000;            
   
            //outputString = GetCurrentSocketBuffer(dataSocket);

            //Console.WriteLine(outputString);

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
