using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Reflection.Emit;

namespace uldis_ladite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        int darba_samaksa = 15;
        int PVN = 21;


        static SQLiteConnection CreateConnection() // Programmas konnekcija ar SQL datubāzi
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=Ulda_ladite.db; Version=3; New=True; Compress=True;");
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                // Savienojuma kļūdu apstrāde
            }
            return sqlite_conn;
        }

        private void button1_Click(object sender, EventArgs e)
            {
            //Pārveido vēlējumu stringu uz int
            string varda_ga = mtb_veltteksts.Text;
            int gar = varda_ga.Length;

            // Pārbauda vai visas nepieciešamās vērtības ir 
            if (
                int.TryParse(mtb_platums.Text, out int platums) &&
                int.TryParse(mtb_garums.Text, out int garums) &&
                int.TryParse(mtb_augstums.Text, out int augstums) &&
                int.TryParse(mtb_matcena.Text, out int materiala_cena))
            {
                //  saksaita characters tb_veltijums un reizina ar 1.2
                string inputText = mtb_veltteksts.Text;
                int charCount = inputText.Length;
                double multipliedCount = charCount * 1.2;

                // veic matemātiku
                double produkta_cena = (gar * 1.2) + ((platums / 100) * (augstums / 100) * (garums / 100)) / 3 * materiala_cena;
                double PVN_summa = (produkta_cena + darba_samaksa) * PVN / 100;
                double rekina_summa = produkta_cena + darba_samaksa + PVN_summa;

                // Parāda to RichTextBox
                richTextBox1.Text = $"Produkta cena: {produkta_cena:C}\n" +
                                   $"PVN summa: {PVN_summa:C}\n" +
                                   $"Rekina summa: {rekina_summa:C}\n";



          

            }
            else
            {
                // Kļūdas gadījumā programma izvada paziņojumu
                richTextBox1.Text = "Iavadiet lūdzu pareizi.";
            }



            }




        private void button2_Click(object sender, EventArgs e)
        {
            
            using (StreamWriter a = new StreamWriter("Cheks.txt"))
            {
                // Izveidojam jaunu mapi ar failu, kur saglabāsies informācija

                a.WriteLine(mlb_vards.Text + " " + mtb_vards.Text);
                a.WriteLine(mlb_uzvards.Text + " " + mtb_uzvards.Text);
                a.WriteLine(mlb_platums.Text + " " + mtb_platums.Text);
                a.WriteLine(mlb_garums.Text + " " + mtb_garums.Text);
                a.WriteLine(mlb_augstums.Text + " " + mtb_augstums.Text);
                a.WriteLine(mlb_veltteksts.Text + " " + mtb_veltteksts.Text);
                a.WriteLine(mlb_matcena.Text + " " + mtb_matcena.Text);

                
                a.Close();
                MessageBox.Show("Veiksmīgi saglabāts failā!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (AreFieldsFilled())
            {
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();

                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Uldaizmaksas (Vards, Uzvards, Velejums, Laditesgarums, Laditesplatums, Laditesaugstums, Kokmaterialacena) " +
                                         "VALUES('" + mtb_vards.Text + "', '" + mtb_uzvards.Text + "', '" +
                                         mtb_veltteksts.Text + "', '" + mtb_garums.Text + "', '" + mtb_platums.Text + "', '"+mtb_augstums.Text+"', '"+mtb_matcena.Text+"');";
                sqlite_cmd.ExecuteNonQuery();

                // Izvadām lodziņu, ja lietotājs ievadīja visu info.
                MessageBox.Show("Jūs veiksmīgi saglabājāt informāciju datubāzē!");
            }
            else
            {
                MessageBox.Show("Lūdzu ievadiet visus datus!");
            }
        }
        private bool AreFieldsFilled()
        {
            // Pārbaudām vai ir aizpildīti visi lodziņi
            return !string.IsNullOrEmpty(mtb_vards.Text) &&
                   !string.IsNullOrEmpty(mtb_uzvards.Text) &&
                   !string.IsNullOrEmpty(mtb_veltteksts.Text) &&
                   !string.IsNullOrEmpty(mtb_garums.Text) &&
                   !string.IsNullOrEmpty(mtb_platums.Text) &&
                   !string.IsNullOrEmpty(mtb_augstums.Text) &&
                   !string.IsNullOrEmpty(mtb_matcena.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Uldaizmaksas";

            DataTable sTable;
            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(sqlite_cmd);
            using (sTable = new DataTable())
            {
                sqlda.Fill(sTable);
                dataGridView1.DataSource = sTable;
            }

            /*if (sTable.Rows.Count > 0)
            {
                lb_vards.Text = sTable.Rows[0]["Vards"].ToString();
            }
            else
            {
                lb_uzvards.Text = "Nav atrasts!";
            }*/
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tb_garums_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_augstums_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_platums_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_velt_teksts_TextChanged(object sender, EventArgs e)
        {

        }

        private void lb_garums_Click(object sender, EventArgs e)
        {

        }

        private void lb_augstums_Click(object sender, EventArgs e)
        {

        }

        private void lb_platums_Click(object sender, EventArgs e)
        {

        }

        private void tb_cena_TextChanged(object sender, EventArgs e)
        {

        }

        private void lb_velt_teksts_Click(object sender, EventArgs e)
        {

        }

        private void lb_cena_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string vards = mtb_vards.Text;
            string uzvards = mtb_uzvards.Text;
            string veltteksts = mtb_veltteksts.Text;

            int platums = int.Parse(mtb_platums.Text);
            int augstums = int.Parse(mtb_augstums.Text);
            int garums = int.Parse(mtb_garums.Text);
            int matcena = int.Parse(mtb_matcena.Text);


            string insertQuery = $"INSERT INTO Uldaizmaksas (Vards, Uzvards, Velejums, Laditesgarums, Laditesplatums, Laditesaugstums, Kokmaterialacena)" +
                $" VALUES ('{vards}', '{uzvards}', '{veltteksts}', '{garums}', '{platums}', '{augstums}', '{matcena}')";
            ExecuteQuery(insertQuery);
            MessageBox.Show("Record inserted successfully.");
        }

        private void ExecuteQuery(string insertQuery)
        {
            throw new NotImplementedException();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //Pārveido vēlējumu stringu uz int
            string varda_ga = mtb_veltteksts.Text;
            int gar = varda_ga.Length;

            // Pārbauda vai visas nepieciešamās vērtības ir 
            if (
                int.TryParse(mlb_platums.Text, out int platums) &&
                int.TryParse(mlb_garums.Text, out int garums) &&
                int.TryParse(mlb_augstums.Text, out int augstums) &&
                int.TryParse(mlb_matcena.Text, out int materiala_cena))
            {
                //  saksaita characters tb_veltijums un reizina ar 1.2
                string inputText = mlb_veltteksts.Text;
                int charCount = inputText.Length;
                double multipliedCount = charCount * 1.2;

                // veic matemātiku
                double produkta_cena = (gar * 1.2) + ((platums / 100) * (augstums / 100) * (garums / 100)) / 3 * materiala_cena;
                double PVN_summa = (produkta_cena + darba_samaksa) * PVN / 100;
                double rekina_summa = produkta_cena + darba_samaksa + PVN_summa;

                // Parāda to RichTextBox
                richTextBox1.Text = $"Produkta cena: {produkta_cena:C}\n" +
                                   $"PVN summa: {PVN_summa:C}\n" +
                                   $"Rekina summa: {rekina_summa:C}\n";





            }
            else
            {
                // Kļūdas gadījumā programma izvada paziņojumu
                richTextBox1.Text = "Iavadiet lūdzu pareizi.";
            }



        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            using (StreamWriter a = new StreamWriter("Cheks.txt"))
            {
                // Izveidojam jaunu mapi ar failu, kur saglabāsies informācija

                a.WriteLine(mlb_vards.Text + " " + mtb_vards.Text);
                a.WriteLine(mlb_uzvards.Text + " " + mtb_uzvards.Text);
                a.WriteLine(mlb_platums.Text + " " + mtb_platums.Text);
                a.WriteLine(mlb_garums.Text + " " + mtb_garums.Text);
                a.WriteLine(mlb_augstums.Text + " " + mtb_augstums.Text);
                a.WriteLine(mlb_veltteksts.Text + " " + mtb_veltteksts.Text);
                a.WriteLine(mlb_matcena.Text + " " + mtb_matcena.Text);


                a.Close();
                MessageBox.Show("Veiksmīgi saglabāts failā!");
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (AreFieldsFilled())
            {
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();

                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Uldaizmaksas (Vards, Uzvards, Velejums, Laditesgarums, Laditesplatums, Laditesaugstums, Kokmaterialacena) " +
                                         "VALUES('" + mtb_vards.Text + "', '" + mtb_uzvards.Text + "', '" +
                                         mtb_veltteksts.Text + "', '" + mtb_garums.Text + "', '" + mtb_platums.Text + "', '" + mtb_augstums.Text + "', '" + mtb_matcena.Text + "');";
                sqlite_cmd.ExecuteNonQuery();

                // Izvadām lodziņu, ja lietotājs ievadīja visu info.
                MessageBox.Show("Jūs veiksmīgi saglabājāt informāciju datubāzē!");
            }
            else
            {
                MessageBox.Show("Lūdzu ievadiet visus datus!");
            }
        }
    }
    
    
}


//Pamēģināt uztaisīt matemātiku ar komatiem private void button1_Click
// poga - apskatīt datubāzi, atver jaunu logu ar datubāzes pārskatu
// Login
// https://www.youtube.com/watch?v=aer8S1fFbNc&ab_channel=SwiftLearn With SQL | Insert Update Delete and Search