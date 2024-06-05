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
using Bioskop;

namespace Bioskop.Forme
{
    /// <summary>
    /// Interaction logic for formaRezervacija.xaml
    /// </summary>
    public partial class formaRezervacija : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public formaRezervacija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            cbProjekcija.Focus();
            PopuniPadajuceListe();

        }

        private void PopuniPadajuceListe()
        {   
            try
            {
                konekcija.Open();
                string vratiKorisnike = @"Select distinct KorisnikId, ime + ' ' + prezime + ' ' + nickname as imek from Korisnik k join Rezervacija r on(k.KorisnikId=Korisnik)"; //sql upit
                DataTable dtKorisnik = new DataTable();
                SqlDataAdapter daKorisnik = new SqlDataAdapter(vratiKorisnike, konekcija);
                daKorisnik.Fill(dtKorisnik);
                cbKorisnik.ItemsSource = dtKorisnik.DefaultView;//postavljamo data adapter kao izvor podataka
                cbKorisnik.DisplayMemberPath = "imek"; //Podaci koji ce se pojaviti u okviru padajuce liste!!
                cbKorisnik.SelectedValuePath = "KorisnikId"; //Podaci koji ce se uzeti kao rezultat --> OSTALE VREDNOSTI SU BITNE SAMO DA SE PODUDARAJU
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



            try
            {

                //select ProjekcijaId, cast(ProjekcijaId as varchar) + ') '+ NazivFilma +' , '+ Datum +' , '+ Vreme as 'projekcija' from projekcija join film on (Projekcija.Film=film.filmId)
                konekcija.Open();
                string vratiRezervacije = @"select ProjekcijaId, cast(ProjekcijaId as varchar) + ') '+ NazivFilma +' , '+ Datum +' , '+ Vreme as 'projekcija' from projekcija join film on (Projekcija.Film=film.filmId)"; //sql upit
                DataTable dtProjekcija = new DataTable();
                SqlDataAdapter daProjekcija = new SqlDataAdapter(vratiRezervacije, konekcija);
                daProjekcija.Fill(dtProjekcija);
                cbProjekcija.ItemsSource = dtProjekcija.DefaultView;//postavljamo data adapter kao izvor podataka
                cbProjekcija.DisplayMemberPath = "projekcija"; //Podaci koji ce se pojaviti u okviru padajuce liste!!
                cbProjekcija.SelectedValuePath = "ProjekcijaId"; //Podaci koji ce se uzeti kao rezultat --> OSTALE VREDNOSTI SU BITNE SAMO DA SE PODUDARAJU
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

        
        public formaRezervacija(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            cbProjekcija.Focus();
            PopuniPadajuceListe();

            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;

            if (azuriraj && pomocniRed != null)
            {
                if (pomocniRed.DataView.Table.Columns.Contains("ZaposleniId"))
                    cbKorisnik.SelectedValue = pomocniRed["Zaposleni"];
                if (pomocniRed.DataView.Table.Columns.Contains("RezervacijaId"))
                    cbKorisnik.SelectedValue = pomocniRed["Rezervacija"];

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
                cmd.Parameters.Add("@ProjekcijaId", SqlDbType.NVarChar).Value = cbProjekcija.SelectedValue;
                cmd.Parameters.Add("@BrojMesta", SqlDbType.VarChar).Value = txtBrojMesta.Text;
                cmd.Parameters.Add("@KorisnikId", SqlDbType.Int).Value =cbKorisnik.SelectedValue;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@RezervacijaId", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE Rezervacija SET Projekcija=@ProjekcijaId, BrojMesta=@BrojMesta, Korisnik=@KorisnikId WHERE RezervacijaId = @RezervacijaId";
                    //this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Rezervacija (Projekcija, BrojMesta, Korisnik ) VALUES (@ProjekcijaId, @BrojMesta, @KorisnikId);";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Uverite se da ste uneli sve trazene informacije!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
