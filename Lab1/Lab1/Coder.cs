﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Lab1
{
    class Coder
    {
        public Coder()
        {

        }

        public void CodeText()
        {
            try
            {
                //odczytanie pliku tekstowego
                String text = File.ReadAllText("TestFile.txt");
                Console.WriteLine(text);

                //zakodowanie tekstu w ASCII
                String[] text_asc = new String[text.Length];

                int i = 0;
                foreach (char c in text)
                {
                    Int32 c_int = Convert.ToInt32(c);
                    String c_asc = Convert.ToString(c_int);
                    if (c_int < 100 && c_int >9)
                    {
                        c_asc = "0" + c_asc;
                    }
                    else if (c_int < 10)
                    {
                        c_asc = "00" + c_asc;
                    }
                    text_asc[i] = c_asc;
                    i++;
                }

                //wypisanie na konsoli tekstu w ASCII
                for (i=0; i<text_asc.Length; i++)
                {
                    Console.Write(text_asc[i]+" ");
                }                

                Console.WriteLine();

                //przełożenie tekstu z ASCII na kod binarny
                String text_bin = null;
                for (i = 0; i < text_asc.Length; i++)
                {
                    byte b = Convert.ToByte(text_asc[i]);
                    if (b > 127)
                    {
                        text_bin += Convert.ToString(b, 2);
                    }
                    else if (b <= 127 && b > 63)
                    {
                        text_bin += "0" + Convert.ToString(b, 2);
                    }
                    else if (b <= 63 && b > 31)
                    {
                        text_bin += "00" + Convert.ToString(b, 2);
                    }
                    else if (b <= 31 && b > 15)
                    {
                        text_bin += "000" + Convert.ToString(b, 2);
                    }
                    else if (b <=15 && b > 7)
                    {
                        text_bin += "0000" + Convert.ToString(b, 2);
                    }
                    else if (b <= 7 && b > 3)
                    {
                        text_bin += "00000" + Convert.ToString(b, 2);
                    }
                    else if (b <= 3 && b > 1)
                    {
                        text_bin += "000000" + Convert.ToString(b, 2);
                    }
                    else if (b <=1)
                    {
                        text_bin += "0000000" + Convert.ToString(b, 2);
                    }
                    //text_bin += " ";
                                        
                }          
                
                Console.WriteLine(text_bin);

                //podział kodu binarnego na grupy 24-bitowe
                int arraySize24 = (text_bin.Length / 24) + 1;
                String[] text24groups = new String[arraySize24];
                i = 0;
                foreach (char c in text_bin)
                {
                    text24groups[i] += c;
                    if (text24groups[i].Length%24 == 0 && text24groups[i].Length != 0)
                    {
                        i++;
                    }                    
                }

                //wypisanie na konsoli tablicy grup 24-bitowych
                for (i = 0; i < text24groups.Length; i++)
                {
                    Console.Write(text24groups[i] + " ");
                }

                Console.WriteLine();

                //podział kodu binarnego na grupy 6-bitowe
                int arraySize6 = (text_bin.Length / 6) + 1;
                String[] text6groups = new String[arraySize6];
                i = 0;
                foreach (char c in text_bin)
                {
                    text6groups[i] += c;                    

                    if (text6groups[i].Length % 6 == 0 && text6groups[i].Length != 0)
                    {
                        i++;
                    }
                }

                //wypisanie na konsoli tablicy grup 6-bitowych, dodanie paddingu
                for (i = 0; i < text6groups.Length; i++)
                {
                    if (text6groups[i].Length == 1)
                    {
                        text6groups[i] += "00000";
                    }
                    else if (text6groups[i].Length == 2)
                    {
                        text6groups[i] += "0000";
                    }
                    else if (text6groups[i].Length == 3)
                    {
                        text6groups[i] += "000";
                    }
                    else if (text6groups[i].Length == 4)
                    {
                        text6groups[i] += "00";
                    }
                    else if (text6groups[i].Length == 5)
                    {
                        text6groups[i] += "0";
                    }

                    Console.Write(text6groups[i] + " ");
                }

                Console.WriteLine();

                //zakodowanie tekstu w Base64
                String[] b64output = new String[arraySize6];
                for (i=0; i <= arraySize6; i++)
                {
                    b64output[i]=
                }



            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        


        public void ReadBmp()
        {
            try
            {
                Image image = Image.FromFile(@"test1.bmp");
                var ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);           
                var bmpBytes = ms.ToArray();
                
                Console.WriteLine(bmpBytes.Length);
                
                    for(int i=0; i < bmpBytes.Length; i++)
                    {
                        Console.Write(bmpBytes[i]+" ");
                        if (i % 10 == 0 && i!=0)
                        {
                        Console.WriteLine();  
                        }
                    }
                

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }

    

}
