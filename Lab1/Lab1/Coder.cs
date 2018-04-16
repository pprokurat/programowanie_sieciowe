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
                String text_pad1 = null;
                for (i = 0; i < arraySize6; i++)
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

                    text_pad1 += text6groups[i];
                    Console.Write(text6groups[i] + " ");
                }
                
                Console.WriteLine();
                Console.WriteLine(text_pad1);

                if (text_pad1.Length % 24 != 0)
                {
                    int k = text_pad1.Length % 24;
                    
                    for (i = 0; i < k; i++)
                    {
                        text_pad1 += "0";
                    }
                }

                Console.WriteLine(text_pad1);

                //ponowny podział na grupy 6-bitowe
                arraySize6 = (text_pad1.Length / 6) + 1;
                text6groups = new String[arraySize6];
                i = 0;
                foreach (char c in text_pad1)
                {
                    text6groups[i] += c;

                    if (text6groups[i].Length % 6 == 0 && text6groups[i].Length != 0)
                    {
                        i++;
                    }
                }

                //for (i = 0; i < arraySize6; i++)
                //{
                //    Console.Write(text6groups[i] + " ");
                //}

                //zakodowanie tekstu w Base64
                String tmp = null;
                Byte[] byteArray = new Byte[arraySize6];
                for (i = 0; i < arraySize6; i++)
                {
                    byteArray[i] = Convert.ToByte(text6groups[i],2);
                }
                //for (i = 0; i < arraySize6; i++)
                //{
                //    Console.WriteLine(byteArray[i] + " ");
                //}

                String base64output = null;
                for (i = 0; i < arraySize6; i++)
                {
                    if (byteArray[i] == 0)
                    {
                        base64output += "A";
                    }
                    else if (byteArray[i] == 1)
                    {
                        base64output += "B";
                    }
                    else if (byteArray[i] == 2)
                    {
                        base64output += "C";
                    }
                    else if (byteArray[i] == 3)
                    {
                        base64output += "D";
                    }
                    else if (byteArray[i] == 4)
                    {
                        base64output += "E";
                    }
                    else if (byteArray[i] == 5)
                    {
                        base64output += "F";
                    }
                    else if (byteArray[i] == 6)
                    {
                        base64output += "G";
                    }
                    else if (byteArray[i] == 7)
                    {
                        base64output += "H";
                    }
                    else if (byteArray[i] == 8)
                    {
                        base64output += "I";
                    }
                    else if (byteArray[i] == 9)
                    {
                        base64output += "J";
                    }
                    else if (byteArray[i] == 10)
                    {
                        base64output += "K";
                    }
                    else if (byteArray[i] == 11)
                    {
                        base64output += "L";
                    }
                    else if (byteArray[i] == 12)
                    {
                        base64output += "M";
                    }
                    else if (byteArray[i] == 13)
                    {
                        base64output += "N";
                    }
                    else if (byteArray[i] == 14)
                    {
                        base64output += "O";
                    }
                    else if (byteArray[i] == 15)
                    {
                        base64output += "P";
                    }
                    else if (byteArray[i] == 16)
                    {
                        base64output += "Q";
                    }
                    else if (byteArray[i] == 17)
                    {
                        base64output += "R";
                    }
                    else if (byteArray[i] == 18)
                    {
                        base64output += "S";
                    }
                    else if (byteArray[i] == 19)
                    {
                        base64output += "T";
                    }
                    else if (byteArray[i] == 20)
                    {
                        base64output += "U";
                    }
                    else if (byteArray[i] == 21)
                    {
                        base64output += "V";
                    }
                    else if (byteArray[i] == 22)
                    {
                        base64output += "W";
                    }
                    else if (byteArray[i] == 23)
                    {
                        base64output += "X";
                    }
                    else if (byteArray[i] == 24)
                    {
                        base64output += "Y";
                    }
                    else if (byteArray[i] == 25)
                    {
                        base64output += "Z";
                    }
                    else if (byteArray[i] == 26)
                    {
                        base64output += "a";
                    }
                    else if (byteArray[i] == 27)
                    {
                        base64output += "b";
                    }
                    else if (byteArray[i] == 28)
                    {
                        base64output += "c";
                    }
                    else if (byteArray[i] == 29)
                    {
                        base64output += "d";
                    }
                    else if (byteArray[i] == 30)
                    {
                        base64output += "e";
                    }
                    else if (byteArray[i] == 31)
                    {
                        base64output += "f";
                    }
                    else if (byteArray[i] == 32)
                    {
                        base64output += "g";
                    }
                    else if (byteArray[i] == 33)
                    {
                        base64output += "h";
                    }
                    else if (byteArray[i] == 34)
                    {
                        base64output += "i";
                    }
                    else if (byteArray[i] == 35)
                    {
                        base64output += "j";
                    }
                    else if (byteArray[i] == 36)
                    {
                        base64output += "k";
                    }
                    else if (byteArray[i] == 37)
                    {
                        base64output += "l";
                    }
                    else if (byteArray[i] == 38)
                    {
                        base64output += "m";
                    }
                    else if (byteArray[i] == 39)
                    {
                        base64output += "n";
                    }
                    else if (byteArray[i] == 40)
                    {
                        base64output += "o";
                    }
                    else if (byteArray[i] == 41)
                    {
                        base64output += "p";
                    }
                    else if (byteArray[i] == 42)
                    {
                        base64output += "q";
                    }
                    else if (byteArray[i] == 43)
                    {
                        base64output += "r";
                    }
                    else if (byteArray[i] == 44)
                    {
                        base64output += "s";
                    }
                    else if (byteArray[i] == 45)
                    {
                        base64output += "t";
                    }
                    else if (byteArray[i] == 46)
                    {
                        base64output += "u";
                    }
                    else if (byteArray[i] == 47)
                    {
                        base64output += "v";
                    }
                    else if (byteArray[i] == 48)
                    {
                        base64output += "w";
                    }
                    else if (byteArray[i] == 49)
                    {
                        base64output += "x";
                    }
                    else if (byteArray[i] == 50)
                    {
                        base64output += "y";
                    }
                    else if (byteArray[i] == 51)
                    {
                        base64output += "z";
                    }
                    else if (byteArray[i] == 52)
                    {
                        base64output += "0";
                    }
                    else if (byteArray[i] == 53)
                    {
                        base64output += "1";
                    }
                    else if (byteArray[i] == 54)
                    {
                        base64output += "2";
                    }
                    else if (byteArray[i] == 55)
                    {
                        base64output += "3";
                    }
                    else if (byteArray[i] == 56)
                    {
                        base64output += "4";
                    }
                    else if (byteArray[i] == 57)
                    {
                        base64output += "5";
                    }
                    else if (byteArray[i] == 58)
                    {
                        base64output += "6";
                    }
                    else if (byteArray[i] == 59)
                    {
                        base64output += "7";
                    }
                    else if (byteArray[i] == 60)
                    {
                        base64output += "8";
                    }
                    else if (byteArray[i] == 61)
                    {
                        base64output += "9";
                    }
                    else if (byteArray[i] == 62)
                    {
                        base64output += "+";
                    }
                    else if (byteArray[i] == 63)
                    {
                        base64output += "/";
                    }
                    else if (byteArray[i] == 64)
                    {
                        base64output += "=";
                    }

                }

                Console.WriteLine(base64output);
                




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
