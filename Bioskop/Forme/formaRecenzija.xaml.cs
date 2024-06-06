using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Bioskop.Forme
{
    public partial class formaRecenzija : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public formaRecenzija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtKorisnik.Focus();
            PopuniPadajuceListe();
        }
        private void PopuniPadajuceListe()
        { 

          
            List<string> listaOcena = new List<string> 
            {
                "1", "2","3","4","5" 

            };
            cbOcena.ItemsSource = listaOcena; 
        }



            

        public formaRecenzija(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtKorisnik.Focus();
            PopuniPadajuceListe();

            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@Korisnik", SqlDbType.Int).Value = txtKorisnik.Text;
                cmd.Parameters.Add("@Komentar", SqlDbType.NVarChar).Value = txtbKomentar.Text;
                cmd.Parameters.Add("@Ocena", SqlDbType.NVarChar).Value = cbOcena.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@RecenzijaId", SqlDbType.Int).Value =Convert.ToInt32(red["ID"]);
                    cmd.CommandText = @"UPDATE Recenzija SET Korisnik=@Korisnik, Komentar=@Komentar, Ocena=@Ocena WHERE RecenzijaId = @RecenzijaId";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Recenzija (Korisnik, Komentar, Ocena) VALUES (@Korisnik, @Komentar, @Ocena);";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Uverite se da ste popunili sva polja! ", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
