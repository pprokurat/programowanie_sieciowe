using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            FtpClient ftpClient = new FtpClient();
            ftpClient.Check();
            //ftpClient.Connect();
            //ftpClient.ShowDirectory();
            ftpClient.ConnectViaLibrary();

            Console.ReadLine();
        }
    }
}
