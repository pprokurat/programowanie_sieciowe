using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Lab1
{
    class Decoder
    {
        public Decoder()
        {
            
        }

        public void DecodeText()
        {
            try
            {
                //wczytanie pliku b64
                String base64input = File.ReadAllText("tekst.b64");
                Console.WriteLine(base64input);

                //odczytanie łańcucha base64
                int arraySize6 = base64input.Length;
                Byte[] byteArray = new Byte[arraySize6];
                
                int i = 0;
                foreach (char c in base64input)
                {
                    bool result = c.Equals('A');                    
                    if (result == true)
                    {
                        byteArray[i] = 0;
                    }
                    result = c.Equals('B');
                    if (result == true)
                    {
                        byteArray[i] = 1;
                    }
                    result = c.Equals('C');
                    if (result == true)
                    {
                        byteArray[i] = 2;
                    }
                    result = c.Equals('D');
                    if (result == true)
                    {
                        byteArray[i] = 3;
                    }
                    result = c.Equals('E');
                    if (result == true)
                    {
                        byteArray[i] = 4;
                    }
                    result = c.Equals('F');
                    if (result == true)
                    {
                        byteArray[i] = 5;
                    }
                    result = c.Equals('G');
                    if (result == true)
                    {
                        byteArray[i] = 6;
                    }
                    result = c.Equals('H');
                    if (result == true)
                    {
                        byteArray[i] = 7;
                    }
                    result = c.Equals('I');
                    if (result == true)
                    {
                        byteArray[i] = 8;
                    }
                    result = c.Equals('J');
                    if (result == true)
                    {
                        byteArray[i] = 9;
                    }
                    result = c.Equals('K');
                    if (result == true)
                    {
                        byteArray[i] = 10;
                    }
                    result = c.Equals('L');
                    if (result == true)
                    {
                        byteArray[i] = 11;
                    }
                    result = c.Equals('M');
                    if (result == true)
                    {
                        byteArray[i] = 12;
                    }
                    result = c.Equals('N');
                    if (result == true)
                    {
                        byteArray[i] = 13;
                    }
                    result = c.Equals('O');
                    if (result == true)
                    {
                        byteArray[i] = 14;
                    }
                    result = c.Equals('P');
                    if (result == true)
                    {
                        byteArray[i] = 15;
                    }
                    result = c.Equals('Q');
                    if (result == true)
                    {
                        byteArray[i] = 16;
                    }
                    result = c.Equals('R');
                    if (result == true)
                    {
                        byteArray[i] = 17;
                    }
                    result = c.Equals('S');
                    if (result == true)
                    {
                        byteArray[i] = 18;
                    }
                    result = c.Equals('T');
                    if (result == true)
                    {
                        byteArray[i] = 19;
                    }
                    result = c.Equals('U');
                    if (result == true)
                    {
                        byteArray[i] = 20;
                    }
                    result = c.Equals('V');
                    if (result == true)
                    {
                        byteArray[i] = 21;
                    }
                    result = c.Equals('W');
                    if (result == true)
                    {
                        byteArray[i] = 22;
                    }
                    result = c.Equals('X');
                    if (result == true)
                    {
                        byteArray[i] = 23;
                    }
                    result = c.Equals('Y');
                    if (result == true)
                    {
                        byteArray[i] = 24;
                    }
                    result = c.Equals('Z');
                    if (result == true)
                    {
                        byteArray[i] = 25;
                    }
                    result = c.Equals('a');
                    if (result == true)
                    {
                        byteArray[i] = 26;
                    }
                    result = c.Equals('b');
                    if (result == true)
                    {
                        byteArray[i] = 27;
                    }
                    result = c.Equals('c');
                    if (result == true)
                    {
                        byteArray[i] = 28;
                    }
                    result = c.Equals('d');
                    if (result == true)
                    {
                        byteArray[i] = 29;
                    }
                    result = c.Equals('e');
                    if (result == true)
                    {
                        byteArray[i] = 30;
                    }
                    result = c.Equals('f');
                    if (result == true)
                    {
                        byteArray[i] = 31;
                    }
                    result = c.Equals('g');
                    if (result == true)
                    {
                        byteArray[i] = 32;
                    }
                    result = c.Equals('h');
                    if (result == true)
                    {
                        byteArray[i] = 33;
                    }
                    result = c.Equals('i');
                    if (result == true)
                    {
                        byteArray[i] = 34;
                    }
                    result = c.Equals('j');
                    if (result == true)
                    {
                        byteArray[i] = 35;
                    }
                    result = c.Equals('k');
                    if (result == true)
                    {
                        byteArray[i] = 36;
                    }
                    result = c.Equals('l');
                    if (result == true)
                    {
                        byteArray[i] = 37;
                    }
                    result = c.Equals('m');
                    if (result == true)
                    {
                        byteArray[i] = 38;
                    }
                    result = c.Equals('n');
                    if (result == true)
                    {
                        byteArray[i] = 39;
                    }
                    result = c.Equals('o');
                    if (result == true)
                    {
                        byteArray[i] = 40;
                    }
                    result = c.Equals('p');
                    if (result == true)
                    {
                        byteArray[i] = 41;
                    }
                    result = c.Equals('q');
                    if (result == true)
                    {
                        byteArray[i] = 42;
                    }
                    result = c.Equals('r');
                    if (result == true)
                    {
                        byteArray[i] = 43;
                    }
                    result = c.Equals('s');
                    if (result == true)
                    {
                        byteArray[i] = 44;
                    }
                    result = c.Equals('t');
                    if (result == true)
                    {
                        byteArray[i] = 45;
                    }
                    result = c.Equals('u');
                    if (result == true)
                    {
                        byteArray[i] = 46;
                    }
                    result = c.Equals('v');
                    if (result == true)
                    {
                        byteArray[i] = 47;
                    }
                    result = c.Equals('w');
                    if (result == true)
                    {
                        byteArray[i] = 48;
                    }
                    result = c.Equals('x');
                    if (result == true)
                    {
                        byteArray[i] = 49;
                    }
                    result = c.Equals('y');
                    if (result == true)
                    {
                        byteArray[i] = 50;
                    }
                    result = c.Equals('z');
                    if (result == true)
                    {
                        byteArray[i] = 51;
                    }
                    result = c.Equals('0');
                    if (result == true)
                    {
                        byteArray[i] = 52;
                    }
                    result = c.Equals('1');
                    if (result == true)
                    {
                        byteArray[i] = 53;
                    }
                    result = c.Equals('2');
                    if (result == true)
                    {
                        byteArray[i] = 54;
                    }
                    result = c.Equals('3');
                    if (result == true)
                    {
                        byteArray[i] = 55;
                    }
                    result = c.Equals('4');
                    if (result == true)
                    {
                        byteArray[i] = 56;
                    }
                    result = c.Equals('5');
                    if (result == true)
                    {
                        byteArray[i] = 57;
                    }
                    result = c.Equals('6');
                    if (result == true)
                    {
                        byteArray[i] = 58;
                    }
                    result = c.Equals('7');
                    if (result == true)
                    {
                        byteArray[i] = 59;
                    }
                    result = c.Equals('8');
                    if (result == true)
                    {
                        byteArray[i] = 60;
                    }
                    result = c.Equals('9');
                    if (result == true)
                    {
                        byteArray[i] = 61;
                    }
                    result = c.Equals('+');
                    if (result == true)
                    {
                        byteArray[i] = 62;
                    }
                    result = c.Equals('/');
                    if (result == true)
                    {
                        byteArray[i] = 63;
                    }
                    result = c.Equals('=');
                    if (result == true)
                    {
                        byteArray[i] = 64;
                    }

                    i++;
                }

                
                //zamiana na grupy 6-bitowe
                String text6groups = null;
                for (i = 0; i < arraySize6; i++)
                {
                    //text_bin += Convert.ToString(byteArray[i], 2) + " ";

                    if (byteArray[i] > 31)
                    {
                        text6groups += Convert.ToString(byteArray[i], 2) + " ";
                    }
                    else if (byteArray[i] <= 31 && byteArray[i] > 15)
                    {
                        text6groups += "0" + Convert.ToString(byteArray[i], 2) + " ";
                    }
                    else if (byteArray[i] <= 15 && byteArray[i] > 7)
                    {
                        text6groups += "00" + Convert.ToString(byteArray[i], 2) + " ";
                    }
                    else if (byteArray[i] <= 7 && byteArray[i] > 3)
                    {
                        text6groups += "000" + Convert.ToString(byteArray[i], 2) + " ";
                    }
                    else if (byteArray[i] <= 3 && byteArray[i] > 1)
                    {
                        text6groups += "0000" + Convert.ToString(byteArray[i], 2) + " ";
                    }
                    else if (byteArray[i] <= 1)
                    {
                        text6groups += "00000" + Convert.ToString(byteArray[i], 2) + " ";
                    }

                }
                Console.WriteLine(text6groups);

                //wypisanie w kodzie binarnym
                String text_bin = null;
                for (i = 0; i < arraySize6; i++)
                {
                    if (byteArray[i] > 31)
                    {
                        text_bin += Convert.ToString(byteArray[i], 2);
                    }
                    else if (byteArray[i] <= 31 && byteArray[i] > 15)
                    {
                        text_bin += "0" + Convert.ToString(byteArray[i], 2);
                    }
                    else if (byteArray[i] <= 15 && byteArray[i] > 7)
                    {
                        text_bin += "00" + Convert.ToString(byteArray[i], 2);
                    }
                    else if (byteArray[i] <= 7 && byteArray[i] > 3)
                    {
                        text_bin += "000" + Convert.ToString(byteArray[i], 2);
                    }
                    else if (byteArray[i] <= 3 && byteArray[i] > 1)
                    {
                        text_bin += "0000" + Convert.ToString(byteArray[i], 2);
                    }
                    else if (byteArray[i] <= 1)
                    {
                        text_bin += "00000" + Convert.ToString(byteArray[i], 2);
                    }                    
                }

                Console.WriteLine(text_bin);

                //podział kodu binarnego na grupy 8-bitowe
                int arraySize8 = (text_bin.Length / 8) + 1;
                String[] bin_array = new String[arraySize8];
                i = 0;
                foreach (char c in text_bin)
                {                    
                    bin_array[i] += c;

                    if (bin_array[i].Length % 8 == 0 && bin_array[i].Length != 0)
                    {
                        i++;                        
                    }
                }

                //wypisanie grup 8-bitowych
                String binaryOutput = null;
                for (i = 0; i < arraySize8; i++)
                {
                    if (bin_array[i].Length == 1)
                    {
                        bin_array[i] += "0000000";
                    }
                    else if (bin_array[i].Length == 2)
                    {
                        bin_array[i] += "000000";
                    }
                    else if (bin_array[i].Length == 3)
                    {
                        bin_array[i] += "00000";
                    }
                    else if (bin_array[i].Length == 4)
                    {
                        bin_array[i] += "0000";
                    }
                    else if (bin_array[i].Length == 5)
                    {
                        bin_array[i] += "000";
                    }
                    else if (bin_array[i].Length == 6)
                    {
                        bin_array[i] += "00";
                    }
                    else if (bin_array[i].Length == 7)
                    {
                        bin_array[i] += "0";
                    }

                    binaryOutput += bin_array[i] + " ";
                }

                Console.WriteLine(binaryOutput);

                //zamiana na ASCII
                Byte[] asc_numbers = new Byte[arraySize8];
                for (i = 0; i < arraySize8; i++)
                {
                    asc_numbers[i] = Convert.ToByte(bin_array[i], 2);
                }

                String asc_numbers_str = null;            
                for (i = 0; i < arraySize8; i++)
                {
                    asc_numbers_str += asc_numbers[i] + " ";
                }

                //Console.WriteLine(asc_numbers_str);

                //wypisanie w ASCII
                String[] asc_output = new String[arraySize8];
                String asc_output_str = null;
                for (i = 0; i < arraySize8; i++)
                {
                    asc_output[i] = Convert.ToString(asc_numbers[i]);
                    if (asc_numbers[i] < 100 && asc_numbers[i] > 9)
                    {
                        asc_output[i] = "0" + asc_numbers[i];
                    }
                    else if (asc_numbers[i] < 10)
                    {
                        asc_output[i] = "00" + asc_numbers[i];
                    }
                    asc_output_str += asc_output[i] + " ";
                }

                Console.WriteLine(asc_output_str);

                String text_output = null;
                for (i = 0; i < arraySize8; i++)
                {
                    Int32 c_int = Convert.ToInt32(asc_output[i]);
                    char c = (char) c_int;
                    text_output += c.ToString();
                }

                Console.WriteLine(text_output);

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    }
}
