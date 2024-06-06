using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Bioskop.Forme
{
    public partial class formaPlacanje : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public formaPlacanje()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        //padajuca lista unutar forme!!!

        
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiZaposleni = @"select distinct ZaposleniId ,  ime+ ' ' +prezime as 'ime' from Zaposleni z left join Placanje p on (z.ZaposleniId=p.Zaposleni)"; //sql upit
                DataTable dtZaposleni = new DataTable();
                SqlDataAdapter daZaposleni = new SqlDataAdapter(vratiZaposleni, konekcija);
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;//postavljamo data adapter kao izvor podataka
                cbZaposleni.DisplayMemberPath = "ime"; //Podaci koji ce se pojaviti u okviru padajuce liste!!
                cbZaposleni.SelectedValuePath = "ZaposleniId"; //Podaci koji ce se uzeti kao rezultat --> OSTALE VREDNOSTI SU BITNE SAMO DA SE PODUDARAJU
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
            List<string> ListaNacinaPlacanja = new List<string> 
            {
                "Kes", 
                "Kartica",
            };
            cbNacinPlacanja.ItemsSource = ListaNacinaPlacanja; 

            try
            {
                konekcija.Open();
                string vratiRezervacije = @"select RezervacijaId, nazivfilma + ' ,cena: ' + isnull(cast((brojmesta*Cena) as varchar),'nije definisana ') +'RSD, za:' + ime + ' ' + prezime + '('+ Nickname+')'as 'rezervacija' from Rezervacija r join Projekcija p on (r.Projekcija=p.ProjekcijaId) join Film f on (p.Film=f.FilmId) join Korisnik k on (r.Korisnik=k.KorisnikId)"; //sql upit
                DataTable dtRezervacija = new DataTable();
                SqlDataAdapter daRezervacija = new SqlDataAdapter(vratiRezervacije, konekcija);
                daRezervacija.Fill(dtRezervacija);
                cbRezervacija.ItemsSource = dtRezervacija.DefaultView;
                cbRezervacija.DisplayMemberPath = "rezervacija";
                cbRezervacija.SelectedValuePath = "RezervacijaId"; 
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

        public formaPlacanje(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
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

                cmd.Parameters.Add("@NacinPlacanja", SqlDbType.NVarChar).Value = cbNacinPlacanja.SelectedValue;
                cmd.Parameters.Add("@Iznos", SqlDbType.Decimal).Value = txtIznos.Text;
                cmd.Parameters.Add("@RezervacijaId", SqlDbType.NVarChar).Value = cbRezervacija.SelectedValue;
                cmd.Parameters.Add("@ZaposleniId", SqlDbType.Int).Value = cbZaposleni.SelectedValue; 

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@PlacanjeId", SqlDbType.Int).Value = Convert.ToInt32(red["ID"]);
                    cmd.CommandText = @"UPDATE Placanje SET  NacinPlacanja=@NacinPlacanja, Iznos=@Iznos, Rezervacija=@RezervacijaId, Zaposleni=@ZaposleniId WHERE PlacanjeId = @PlacanjeId";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"INSERT INTO Placanje ( NacinPlacanja, Iznos, Rezervacija, Zaposleni) VALUES (@NacinPlacanja, @Iznos, @RezervacijaId, @ZaposleniId);";
                }

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Uverite se da ste uneli sve tražene informacije, nacin placanja moze biti 'KES' ili 'Kartica', neophodo je prethodno se uveriti za koju rezervaciju se vrsi placanje! " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
