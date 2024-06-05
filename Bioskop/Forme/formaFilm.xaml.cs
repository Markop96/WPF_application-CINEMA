using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Bioskop.Forme
{

    public partial class formaFilm : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public formaFilm()
        {
            InitializeComponent();
            txtNazivFilma.Focus();
            konekcija = kon.KreirajKonekciju();
        }

        public formaFilm(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtNazivFilma.Focus();
            konekcija = kon.KreirajKonekciju();
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
                
                cmd.Parameters.Add("@NazivFilma", SqlDbType.NChar).Value = txtNazivFilma.Text;
                cmd.Parameters.Add("@Zanr", SqlDbType.NVarChar).Value = txtZanr.Text;
                cmd.Parameters.Add("@Reziser", SqlDbType.NVarChar).Value = txtReziser.Text;
                cmd.Parameters.Add("@GodinaIzlaska", SqlDbType.NChar).Value = txtGodinaIzlaska.Text;
                cmd.Parameters.Add("@Trajanje", SqlDbType.NChar).Value = txtTrajanje.Text;
                
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Film SET NazivFilma=@NazivFilma, Zanr=@Zanr, Reziser=@Reziser, GodinaIzlaska=@GodinaIzlaska, Trajanje=@Trajanje WHERE FilmId = @Id";
                    //this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Film (NazivFilma, Zanr, Reziser, GodinaIzlaska, Trajanje) VALUES (@NazivFilma, @Zanr, @Reziser, @GodinaIzlaska, @Trajanje) ";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Uverite se da ste uneli sve trazene informacije! " +
                    "Sva polja osim režiser i žanr MORAJU biti popunjena!" +
                    "Godina mora imati 4 cifre!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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

