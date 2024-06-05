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
    /// <summary>
    /// Interaction logic for formaProjekcija.xaml
    /// </summary>
    public partial class formaProjekcija : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public formaProjekcija()
        {
            InitializeComponent();
            txtCena.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }



        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiFilmove = @"select NazivFilma,FilmId FROM Film";
                DataTable dtFilm = new DataTable();
                SqlDataAdapter daFilm = new SqlDataAdapter(vratiFilmove, konekcija);
                daFilm.Fill(dtFilm);
                cbFilm.ItemsSource = dtFilm.DefaultView;
                cbFilm.DisplayMemberPath = "NazivFilma";
                cbFilm.SelectedValuePath = "FilmId";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Padajuće liste nisu popunjene! " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null && konekcija.State == ConnectionState.Open)
                {
                    konekcija.Close();
                }
            }
        }

        public formaProjekcija(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();

            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;

            if (azuriraj && pomocniRed != null)
            {
                if (pomocniRed.DataView.Table.Columns.Contains("FilmId"))
                    cbFilm.SelectedValue = pomocniRed["FilmId"];

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
                

                cmd.Parameters.Add("@Cena", SqlDbType.NVarChar).Value = txtCena.Text; //prvi type je kako je predstavljen u bazi, drugi unutar textbox-a
                cmd.Parameters.Add("@Datum", SqlDbType.NVarChar).Value = pcDatum.Text;                
                cmd.Parameters.Add("@Sala", SqlDbType.VarChar).Value = txtSala.Text;
                cmd.Parameters.Add("@Vreme", SqlDbType.NVarChar).Value = txtVreme.Text;
                cmd.Parameters.Add("@FilmId", SqlDbType.NVarChar).Value = cbFilm.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ProjekcijaId", SqlDbType.Int).Value = Convert.ToInt32(red["ID"]);
                    cmd.CommandText = @"UPDATE Projekcija SET  Cena=@Cena, Datum=@Datum, Sala=@Sala, Vreme=@Vreme, Film=@FilmId WHERE ProjekcijaId = @ProjekcijaId";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Projekcija ( Cena, Datum, Sala, Vreme, Film) VALUES (@Cena, @Datum, @Sala, @Vreme, @FilmId);";
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
                this.Close();
            }
        }

        
        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}