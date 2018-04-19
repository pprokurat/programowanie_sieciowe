using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Lab1
{
    class ImageCoder
    {
        public String base64outputImg = null;
        public Image image = null;

        public ImageCoder()
        {

        }

        public void CodeImage()
        {
            try
            {
                //odczytanie obrazka
                image = Image.FromFile(@"test1c.bmp");
                var ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                var bmpBytes = ms.ToArray();

                //Console.WriteLine(bmpBytes.Length);
                int i = 0;
                for (i = 0; i < bmpBytes.Length; i++)
                {
                    Console.Write(bmpBytes[i] + " ");

                    //if (i % 10 == 0 && i != 0)
                    if (i == bmpBytes.Length - 1)
                    {
                        Console.WriteLine();
                    }
                }

                //zamiana na ASCII
                String[] text_asc = new String[bmpBytes.Length];

                for (i = 0; i < bmpBytes.Length; i++)
                {
                    Int32 c_int = Convert.ToInt32(bmpBytes[i]);
                    String c_asc = Convert.ToString(c_int);
                    if (c_int < 100 && c_int > 9)
                    {
                        c_asc = "0" + c_asc;
                    }
                    else if (c_int < 10)
                    {
                        c_asc = "00" + c_asc;
                    }
                    text_asc[i] = c_asc;
                }

                //wypisanie na konsoli tekstu w ASCII
                for (i = 0; i < text_asc.Length; i++)
                {
                    Console.Write(text_asc[i] + " ");
                }

                Console.WriteLine();

                //przełożenie tekstu z ASCII na kod binarny
                String bmp_bin = null;
                for (i = 0; i < text_asc.Length; i++)
                {
                    byte b = Convert.ToByte(text_asc[i]);
                    if (b > 127)
                    {
                        bmp_bin += Convert.ToString(b, 2);
                    }
                    else if (b <= 127 && b > 63)
                    {
                        bmp_bin += "0" + Convert.ToString(b, 2);
                    }
                    else if (b <= 63 && b > 31)
                    {
                        bmp_bin += "00" + Convert.ToString(b, 2);
                    }
                    else if (b <= 31 && b > 15)
                    {
                        bmp_bin += "000" + Convert.ToString(b, 2);
                    }
                    else if (b <= 15 && b > 7)
                    {
                        bmp_bin += "0000" + Convert.ToString(b, 2);
                    }
                    else if (b <= 7 && b > 3)
                    {
                        bmp_bin += "00000" + Convert.ToString(b, 2);
                    }
                    else if (b <= 3 && b > 1)
                    {
                        bmp_bin += "000000" + Convert.ToString(b, 2);
                    }
                    else if (b <= 1)
                    {
                        bmp_bin += "0000000" + Convert.ToString(b, 2);
                    }

                }

                Console.WriteLine(bmp_bin);

                //podział kodu binarnego na grupy 24-bitowe
                int arraySize24 = (bmp_bin.Length / 24) + 1;
                String[] text24groups = new String[arraySize24];
                i = 0;
                foreach (char c in bmp_bin)
                {
                    text24groups[i] += c;
                    if (text24groups[i].Length % 24 == 0 && text24groups[i].Length != 0)
                    {
                        i++;
                    }
                }

                ////dodanie paddingu
                int padSize = 0;
                //for (i = 0; i < text24groups.Length; i++)
                //{
                //    if (text24groups[i].Length != 24)
                //    {
                //        int k = text24groups[i].Length;
                //        padSize = 24 - k;
                //        for (int j = 0; j < padSize; j++)
                //        {
                //            text24groups[i] += "0";
                //        }
                //    }
                //}

                //wypisanie na konsoli tablicy grup 24-bitowych
                for (i = 0; i < text24groups.Length; i++)
                {
                    Console.Write(text24groups[i] + " ");
                }

                Console.WriteLine();

                String text24groups_string = null;
                for (i = 0; i < text24groups.Length; i++)
                {
                    text24groups_string += text24groups[i];
                }

                //podział kodu binarnego na grupy 6-bitowe
                int arraySize6 = (text24groups_string.Length / 6);
                String[] text6groups = new String[arraySize6];
                i = 0;
                foreach (char c in text24groups_string)
                {
                    text6groups[i] += c;

                    if (text6groups[i].Length % 6 == 0 && text6groups[i].Length != 0)
                    {
                        i++;
                    }
                }

                //wypisanie na konsoli tablicy grup 6-bitowych

                for (i = 0; i < arraySize6; i++)
                {
                    Console.Write(text6groups[i] + " ");
                }

                Console.WriteLine();

                //zakodowanie tekstu w Base64
                Byte[] byteArray = new Byte[arraySize6];
                for (i = 0; i < arraySize6; i++)
                {
                    byteArray[i] = Convert.ToByte(text6groups[i], 2);

                    if (i == arraySize6 - 1 && (padSize > 6 && padSize <= 12 && padSize != 0))
                    {
                        byteArray[i] = 64;
                    }

                    if (i == arraySize6 - 2 && padSize <= 6 && padSize != 0)
                    {
                        byteArray[i] = 64;
                    }
                }

                base64outputImg = null;
                for (i = 0; i < arraySize6; i++)
                {
                    if (byteArray[i] == 0)
                    {
                        base64outputImg += "A";
                    }
                    else if (byteArray[i] == 1)
                    {
                        base64outputImg += "B";
                    }
                    else if (byteArray[i] == 2)
                    {
                        base64outputImg += "C";
                    }
                    else if (byteArray[i] == 3)
                    {
                        base64outputImg += "D";
                    }
                    else if (byteArray[i] == 4)
                    {
                        base64outputImg += "E";
                    }
                    else if (byteArray[i] == 5)
                    {
                        base64outputImg += "F";
                    }
                    else if (byteArray[i] == 6)
                    {
                        base64outputImg += "G";
                    }
                    else if (byteArray[i] == 7)
                    {
                        base64outputImg += "H";
                    }
                    else if (byteArray[i] == 8)
                    {
                        base64outputImg += "I";
                    }
                    else if (byteArray[i] == 9)
                    {
                        base64outputImg += "J";
                    }
                    else if (byteArray[i] == 10)
                    {
                        base64outputImg += "K";
                    }
                    else if (byteArray[i] == 11)
                    {
                        base64outputImg += "L";
                    }
                    else if (byteArray[i] == 12)
                    {
                        base64outputImg += "M";
                    }
                    else if (byteArray[i] == 13)
                    {
                        base64outputImg += "N";
                    }
                    else if (byteArray[i] == 14)
                    {
                        base64outputImg += "O";
                    }
                    else if (byteArray[i] == 15)
                    {
                        base64outputImg += "P";
                    }
                    else if (byteArray[i] == 16)
                    {
                        base64outputImg += "Q";
                    }
                    else if (byteArray[i] == 17)
                    {
                        base64outputImg += "R";
                    }
                    else if (byteArray[i] == 18)
                    {
                        base64outputImg += "S";
                    }
                    else if (byteArray[i] == 19)
                    {
                        base64outputImg += "T";
                    }
                    else if (byteArray[i] == 20)
                    {
                        base64outputImg += "U";
                    }
                    else if (byteArray[i] == 21)
                    {
                        base64outputImg += "V";
                    }
                    else if (byteArray[i] == 22)
                    {
                        base64outputImg += "W";
                    }
                    else if (byteArray[i] == 23)
                    {
                        base64outputImg += "X";
                    }
                    else if (byteArray[i] == 24)
                    {
                        base64outputImg += "Y";
                    }
                    else if (byteArray[i] == 25)
                    {
                        base64outputImg += "Z";
                    }
                    else if (byteArray[i] == 26)
                    {
                        base64outputImg += "a";
                    }
                    else if (byteArray[i] == 27)
                    {
                        base64outputImg += "b";
                    }
                    else if (byteArray[i] == 28)
                    {
                        base64outputImg += "c";
                    }
                    else if (byteArray[i] == 29)
                    {
                        base64outputImg += "d";
                    }
                    else if (byteArray[i] == 30)
                    {
                        base64outputImg += "e";
                    }
                    else if (byteArray[i] == 31)
                    {
                        base64outputImg += "f";
                    }
                    else if (byteArray[i] == 32)
                    {
                        base64outputImg += "g";
                    }
                    else if (byteArray[i] == 33)
                    {
                        base64outputImg += "h";
                    }
                    else if (byteArray[i] == 34)
                    {
                        base64outputImg += "i";
                    }
                    else if (byteArray[i] == 35)
                    {
                        base64outputImg += "j";
                    }
                    else if (byteArray[i] == 36)
                    {
                        base64outputImg += "k";
                    }
                    else if (byteArray[i] == 37)
                    {
                        base64outputImg += "l";
                    }
                    else if (byteArray[i] == 38)
                    {
                        base64outputImg += "m";
                    }
                    else if (byteArray[i] == 39)
                    {
                        base64outputImg += "n";
                    }
                    else if (byteArray[i] == 40)
                    {
                        base64outputImg += "o";
                    }
                    else if (byteArray[i] == 41)
                    {
                        base64outputImg += "p";
                    }
                    else if (byteArray[i] == 42)
                    {
                        base64outputImg += "q";
                    }
                    else if (byteArray[i] == 43)
                    {
                        base64outputImg += "r";
                    }
                    else if (byteArray[i] == 44)
                    {
                        base64outputImg += "s";
                    }
                    else if (byteArray[i] == 45)
                    {
                        base64outputImg += "t";
                    }
                    else if (byteArray[i] == 46)
                    {
                        base64outputImg += "u";
                    }
                    else if (byteArray[i] == 47)
                    {
                        base64outputImg += "v";
                    }
                    else if (byteArray[i] == 48)
                    {
                        base64outputImg += "w";
                    }
                    else if (byteArray[i] == 49)
                    {
                        base64outputImg += "x";
                    }
                    else if (byteArray[i] == 50)
                    {
                        base64outputImg += "y";
                    }
                    else if (byteArray[i] == 51)
                    {
                        base64outputImg += "z";
                    }
                    else if (byteArray[i] == 52)
                    {
                        base64outputImg += "0";
                    }
                    else if (byteArray[i] == 53)
                    {
                        base64outputImg += "1";
                    }
                    else if (byteArray[i] == 54)
                    {
                        base64outputImg += "2";
                    }
                    else if (byteArray[i] == 55)
                    {
                        base64outputImg += "3";
                    }
                    else if (byteArray[i] == 56)
                    {
                        base64outputImg += "4";
                    }
                    else if (byteArray[i] == 57)
                    {
                        base64outputImg += "5";
                    }
                    else if (byteArray[i] == 58)
                    {
                        base64outputImg += "6";
                    }
                    else if (byteArray[i] == 59)
                    {
                        base64outputImg += "7";
                    }
                    else if (byteArray[i] == 60)
                    {
                        base64outputImg += "8";
                    }
                    else if (byteArray[i] == 61)
                    {
                        base64outputImg += "9";
                    }
                    else if (byteArray[i] == 62)
                    {
                        base64outputImg += "+";
                    }
                    else if (byteArray[i] == 63)
                    {
                        base64outputImg += "/";
                    }
                    else if (byteArray[i] == 64)
                    {
                        base64outputImg += "=";
                    }

                }

                Console.WriteLine(base64outputImg);

                File.WriteAllText("tekst.b64", base64outputImg);

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
