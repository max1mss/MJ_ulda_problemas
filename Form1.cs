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
using MetroFramework.Components;
using System.Data.SqlClient;
using System.Configuration;

namespace uldis_ladite
{
    public partial class Form1 : Form
    {
        // Deklarējam mainīgos klases līmenī, lai tie būtu globāli pieejami visā klasē
        private double produkta_cena;
        private double PVN_summa;
        private double rekina_summa;
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

        

        

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (AreFieldsFilled())
            {
                SQLiteConnection sqlite_conn;
                sqlite_conn = CreateConnection();

                SQLiteCommand sqlite_cmd;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "INSERT INTO Uldaizmaksas (Vārds, Uzvārds, Vēltījums, Lādītesgarums, Lādītesplatums, Lādītesaugstums, Kokmateriālacena) " +
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
                                   $"Rēķina summa: {rekina_summa:C}\n";





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

                a.WriteLine(mlb_vards.Text + " - " + mtb_vards.Text);
                a.WriteLine(mlb_uzvards.Text + " - " + mtb_uzvards.Text);
                a.WriteLine(mlb_platums.Text + " - " + mtb_platums.Text);
                a.WriteLine(mlb_garums.Text + " - " + mtb_garums.Text);
                a.WriteLine(mlb_augstums.Text + " - " + mtb_augstums.Text);
                a.WriteLine(mlb_veltteksts.Text + " - " + mtb_veltteksts.Text);
                a.WriteLine(mlb_matcena.Text + " - " + mtb_matcena.Text);
                a.WriteLine("Produkta cena" + " - " + produkta_cena);
                a.WriteLine("PVN summa" + " - " + PVN_summa);
                a.WriteLine("Rēķina summa" + " - " + rekina_summa);


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
                sqlite_cmd.CommandText = "INSERT INTO Uldaizmaksas (Vārds, Uzvārds, Vēltījums, Lādītesgarums, Lādītesplatums, Lādītesaugstums, Kokmateriālacena) " +
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

        private void metroButton3_Click_1(object sender, EventArgs e)
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
            sqlite_conn.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Pārbaudam, vai ir iezīmētas rindas
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Atgūstam iezīmēto rindu indeksus
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    // Pārbaudam, vai rinda ir derīga
                    if (!row.IsNewRow)
                    {
                        // Atgūstam iezīmētās rindas ID no datagridview, ja jūsu datu tabula ir saistīta ar datubāzi un ir ID kolonna
                        int id = Convert.ToInt32(row.Cells["ID"].Value); // Aizstājiet "ID" ar savas tabulas primārās atslēgas kolonnas nosaukumu

                        // Izveidojam savienojumu ar datubāzi
                        using (SQLiteConnection sqlite_conn = CreateConnection())
                        {
                            //sqlite_conn.Open();

                            // Izdzēšam iezīmēto ierakstu no datubāzes
                            string deleteQuery = "DELETE FROM Uldaizmaksas WHERE ID = @id"; // Aizstājiet "Uldaizmaksas" ar savas tabulas nosaukumu
                            using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, sqlite_conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    // Izvadām lodziņu, ja lietotājs ievadīja visu info.
                    MessageBox.Show("Veiksmīgi dzēsts no datubāzes!");
                }


                // Atjaunojam datagridview, lai atspoguļotu veiktos izmaiņas datubāzē
                button6_Click(sender, e); // Pielāgojiet šo atsauci, lai tas atbilstu jūsu faktiskajam atjaunošanas pasākumam
            }
            else
            {
                MessageBox.Show("Iezīmējiet rindas, kuras vēlaties dzēst!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=Ulda_ladite.db;Version=3;"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Ulda_ladite where Vārds=@mtb_vards", con);
                cmd.Parameters.AddWithValue("@mtb_vards", int.Parse(mtb_vards.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mtb_veltteksts_Click(object sender, EventArgs e)
        {

        }



        
        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxsearch.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString; 

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM Ulda_ladite WHERE Vārds LIKE @keyword OR Uzvārds LIKE @keyword";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }
    }
    
    
}


//Pamēģināt uztaisīt matemātiku ar komatiem private void button1_Click
// poga - apskatīt datubāzi, atver jaunu logu ar datubāzes pārskatu
// Poga aizvērt
// https://www.youtube.com/watch?v=aer8S1fFbNc&ab_channel=SwiftLearn With SQL | Insert Update Delete and Search
// search 13:52
