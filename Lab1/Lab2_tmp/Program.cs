using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;

namespace Lab2_tmp
{
    class Program
    {
        static void Main(string[] args)
        {

            Pop3Client client = new Pop3Client();
            client.Check();


            Console.ReadLine();
        }
    }
}
