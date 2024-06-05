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

    public partial class formaZaposleni : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public formaZaposleni()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }

        public formaZaposleni(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtJMBG.Focus();
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

                cmd.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@Adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                cmd.Parameters.Add("@BrojTelefona", SqlDbType.NChar).Value = txtTelefon.Text;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@RadnoMesto", SqlDbType.NVarChar).Value = txtRadnoMesto.Text;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Zaposleni SET Ime=@Ime, Prezime=@Prezime, JMBG=@JMBG, Adresa=@Adresa, BrojTelefona=@BrojTelefona, Email=@Email, RadnoMesto=@RadnoMesto WHERE ZaposleniId=@id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Zaposleni (Ime, Prezime, JMBG, Adresa, BrojTelefona, Email, RadnoMesto) VALUES (@Ime, @Prezime, @JMBG, @Adresa, @BrojTelefona, @Email, @RadnoMesto);";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Uverite se da ste uneli sve tražene informacije! " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null && konekcija.State == ConnectionState.Open)
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
