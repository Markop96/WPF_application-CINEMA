using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Bioskop.Forme
{
    public partial class formaKorisnik : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public formaKorisnik()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }

        public formaKorisnik(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtNickname.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;

        }

        private void Cekirano(object sender, RoutedEventArgs e)
        {
            if (sender == cbxMusko && cbxMusko.IsChecked == true)
            {
                cbxZensko.IsChecked = false;
            }
            else if (sender == cbxZensko && cbxZensko.IsChecked == true)
            {
                cbxMusko.IsChecked = false;
            }
        }

        private void Odcekirano(object sender, RoutedEventArgs e)
        {

            if (cbxMusko.IsChecked == false && cbxZensko.IsChecked == false)
            {
                if (sender == cbxMusko)
                {
                    cbxZensko.IsChecked = true;
                }
                else
                {
                    cbxMusko.IsChecked = true;
                }
            }
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
                cmd.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@Nickname", SqlDbType.NVarChar).Value = txtNickname.Text;
                cmd.Parameters.Add("@BrojTelefona", SqlDbType.NVarChar).Value = txtTelefon.Text;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@Pol", SqlDbType.Char).Value = cbxMusko.IsChecked == true ? 'M' : 'Z';

                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Korisnik SET Ime=@Ime, Prezime=@Prezime, Nickname=@Nickname, Email=@Email, BrojTelefona=@BrojTelefona, Pol=@Pol WHERE KorisnikId = @Id";
                    this.pomocniRed = null;
                    
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Korisnik (Ime, Prezime, Nickname, Email, BrojTelefona, Pol) VALUES (@Ime, @Prezime, @Nickname, @Email, @BrojTelefona, @Pol);";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Uverite se da ste uneli SVE VREDNOSTI (Ime,Prezime,Nickname), Nicname mora biti jedinstven za svakog korisnika, cekirajte pol! ", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
