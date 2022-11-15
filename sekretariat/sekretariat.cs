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

namespace sekretariat
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
			string[] readLines;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            try
            {
                readLines = File.ReadAllLines(@".\Properties\file.txt");
            }
            catch { return; }
            for(int i=0; i< readLines.Count(); i++)
            {
                string[] fields = readLines[i].Split(' ');
                uczniowie.Add(new Uczen(fields[0], fields[1], fields[2]));
            }

            wypisuj(uczniowie);
        }
        List<Uczen> uczniowie = new List<Uczen>();
        
        public void wypisuj(List<Uczen> U)
        {
            richTextBox1.Clear();
            for (int i = 0; i < U.Count(); i++)
            {
                richTextBox1.Text += $"{i} {U[i].imie} {U[i].nazwisko} {U[i].klasa}\n";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Regex imieinazwisko = new Regex("^[A-Z][a-z]{2,50}$");
            Regex klasa = new Regex("^[0-4][a-g]$");

            if (!imieinazwisko.IsMatch(textBox1.Text))
            {
                MessageBox.Show("wprowadziles niepoprawne imie, sprawdz czy rozpoczyna sie od wielkiej litery i nie ma cyfr");
            }
            if (!imieinazwisko.IsMatch(textBox2.Text))
            {
                MessageBox.Show("wprowadziles niepoprawne nazwisko, sprawdz czy rozpoczyna sie od wielkiej litery i nie ma cyfr");
            }
            if (!klasa.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Upewnij sie czy dany uczen chodzi do klasy od 1-4 z litera a-g");
            }
            if (imieinazwisko.IsMatch(textBox1.Text) && imieinazwisko.IsMatch(textBox2.Text) && klasa.IsMatch(textBox3.Text))
            {

                /*using (SqlConnection conn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\student\source\repos\Oliwka420\Zadanie_login_haslo\Zadanie_login_haslo\Database1.mdf; Integrated Security = True"))
                {
                    SqlCommand CmdSql = new SqlCommand($"INSERT INTO 'Tables' VALUES(@ID, {textBox1.Text}, {textBox2.Text}, {textBox2.Text}) ", conn);


                    conn.Open();
                    CmdSql.Parameters.AddWithValue("@ID", 1);
                    CmdSql.Parameters.AddWithValue("@Imie", textBox1.Text);
                    CmdSql.Parameters.AddWithValue("@Nazwisko", textBox2.Text);
                    CmdSql.Parameters.AddWithValue("@Klasa", textBox3.Text);
                    conn.Close();
                }*/

                string connetionString = null;
                string sql = null;
                connetionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = .\Database1.mdf; Integrated Security = True;";

                // Prepare a proper parameterized query 
                sql = "insert into Tables ([Imie], [Nazwisko], [Klasa]) values(@imie,@nazwisko,@klasa)";

                // Create the connection (and be sure to dispose it at the end)
                using (SqlConnection cnn = new SqlConnection(connetionString))
                {
                    try
                    {
                        cnn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        {

                            cmd.Parameters.Add("@imie", SqlDbType.NVarChar).Value = textBox1.Text;
                            cmd.Parameters.Add("@nazwisko", SqlDbType.NVarChar).Value = textBox2.Text;
                            cmd.Parameters.Add("@klasa", SqlDbType.NVarChar).Value = textBox3.Text;

                            int rowsAdded = cmd.ExecuteNonQuery();
                            if (rowsAdded > 0)
                                MessageBox.Show("Row inserted!!");
                            else
                                MessageBox.Show("No row inserted");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR:" + ex.Message);
                    }
                }

                Uczen nowy = new Uczen(textBox1.Text, textBox2.Text, textBox3.Text);
                uczniowie.Add(nowy);
                //Open File
                File.AppendAllText(@".\res\file.txt", nowy.imie + " " + nowy.nazwisko + " " + nowy.klasa + Environment.NewLine);
                MessageBox.Show("pomyslnie dodano");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                wypisuj(uczniowie);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                wypisuj(uczniowie);
                return;
            }
            Regex regex;
            
            if (comboBox2.SelectedIndex == 0)//równe
            {
                regex = new Regex($@"^{textBox4.Text}$");
            }
            else if (comboBox2.SelectedIndex == 1)//rozpoczyna się od
            {
                regex = new Regex($@"^{textBox4.Text}");
            }
            else if (comboBox2.SelectedIndex == 2)//zawiera
            {
                regex = new Regex($@"{textBox4.Text}");
            }
            else { return;  }

            List<Uczen> filtracja = new List<Uczen>();
            foreach(Uczen u in uczniowie)
            {
                if (comboBox1.SelectedIndex == 0 && regex.IsMatch(u.imie))
                {
                    filtracja.Add(u);
                }
                if (comboBox1.SelectedIndex == 1 && regex.IsMatch(u.nazwisko))
                {
                    filtracja.Add(u);
                }
                if (comboBox1.SelectedIndex == 2 && regex.IsMatch(u.klasa))
                {
                    filtracja.Add(u);
                }
                wypisuj(filtracja);
            }
        }
    }
    public class Uczen
    {
        public string imie;
        public string nazwisko;
        public string klasa;

        public Uczen(string imie, string nazwisko, string klasa)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.klasa = klasa;
        }
    }
}
