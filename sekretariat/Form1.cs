using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sekretariat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool czy_zweryfikowano = false;

        public void losowanie_obrazka(string s)
        {
            if (s == "weryfikacja")
            {
                MessageBox.Show("nieprawidłowa weryfikacja");
            }
            string[] paths = Directory.GetFiles(@".\res", "*.png");
            List<string> images = paths.ToList();
            Random random = new Random();
            pictureBox1.ImageLocation = paths[random.Next(0, images.Count - 1)];
        }
        public void weryfikacja(string dobrykod)
        {
            if (textBox3.Text == dobrykod) { czy_zweryfikowano = true; }
            else { losowanie_obrazka("weryfikacja"); }
        }
        private void label5_Click(object sender, EventArgs e)
        {
            losowanie_obrazka("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string x = System.IO.Path.GetFileName(pictureBox1.ImageLocation);
            if (x == "6.png") { weryfikacja("befbd"); }
            else if (x == "7.png") { weryfikacja("re7gb3"); }
            else if (x == "5.png") { weryfikacja("x3deb"); }
            else if (x == "4.png") { weryfikacja("cg5dd"); }
            else if (x == "3.png") { weryfikacja("74853"); }
            else if (x == "2.png") { weryfikacja("b5nmm"); }
            else if (x == "1.png") { weryfikacja("mxyxw"); }
            if (textBox1.Text == "a" && textBox2.Text == "a" && czy_zweryfikowano)
            { 
                var myForm = new Form2();
                this.Hide();
                myForm.ShowDialog();
                this.Close();


            }
        }
        }
}
