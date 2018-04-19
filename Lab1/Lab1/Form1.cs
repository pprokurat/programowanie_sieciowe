using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        TextCoder textCoder = new TextCoder();
        ImageCoder imageCoder = new ImageCoder();
        TextDecoder decoder = new TextDecoder();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textCoder.CodeText();
            inputTextBox.Text = textCoder.text;
            outputB64textBox.Text = textCoder.base64output;

            decoder.DecodeText();
            inputB64textBox.Text = decoder.base64input;
            outputTextBox.Text = decoder.text_output;

            imageCoder.CodeImage();
            pictureBox1.Image = imageCoder.image;
            imageOutputB64textBox.Text = imageCoder.base64outputImg;

        }
    }
}
