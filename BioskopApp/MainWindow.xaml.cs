using Bioskop.Forme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Bioskop
{

    public partial class MainWindow : Window
    {
        string ucitanaTabela;
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        #region SELECT
        static string filmSelect = @"Select FilmId as 'ID', NazivFilma as 'Naziv', Zanr as Žanr, Reziser as Režiser, cast(Trajanje as varchar) + ' ' + 'minuta' as 'Trajanje', cast(GodinaIzlaska as varchar) + ' ' + 'godine' as 'Godina izlaska' from Film";
        static string korisnikSelect = @"Select KorisnikId as 'ID', Ime, Prezime, Nickname, Pol ,BrojTelefona as 'Broj telefona', Email as 'E-Mail' from Korisnik";
        static string placanjeSelect = @" Select PlacanjeID as 'ID',cast(Iznos as varchar) + ' ' + 'RSD' as 'Cenas', NacinPlacanja as 'Način', Rezervacija as 'Za rezervaciju:', DatumPlacanja, Ime + ' ' +Left(Prezime,1) +'.' as 'Naplatio:' from Placanje p join Zaposleni z on (p.Zaposleni=z.ZaposleniId)";
        static string projekcijaSelect = @"Select ProjekcijaId as 'ID', NazivFilma as 'Film', cast(Cena as varchar) + ' RSD' as 'Cena','Broj' + ' ' +  Sala as 'Sala', Datum, Vreme + ' h' as 'Vreme' From projekcija p join film f on(p.film=f.FilmId);";
        static string recenzijaSelect = @"Select RecenzijaId as 'ID', Ocena, Komentar , Nickname from Recenzija r join Korisnik k on(r.Korisnik=k.KorisnikId)";
        static string rezervacijaSelect = @"select  RezervacijaId as 'ID', nazivFilma as 'film' ,Datum + ' , ' + Vreme + ' h' as 'Projekcija', BrojMesta as 'Br Mesta', Ime + ' ' + Prezime  as 'Rezervisao:', Nickname as 'Nick' from Rezervacija r join Korisnik k on (r.Korisnik=k.KorisnikId) join Projekcija p on (r.Projekcija=p.ProjekcijaId) join Film f on(p.film=f.FilmId);";
        static string zaposleniSelect = @"Select ZaposleniId as 'ID',Ime + ' ' + Prezime as 'Puno ime', JMBG, Adresa, BrojTelefona as 'Telefon', Email as 'E-Mail', RadnoMesto as 'Radno mesto' from Zaposleni";
        #endregion

        #region DELETE
        string filmDelete = @"Delete from film where FilmId=";
        string korisnikDelete = @"Delete from Korisnik where KorisnikId=";
        string placanjeDelete = @"Delete from Placanje Where PlacanjeId=";
        string projekcijaDelete = @"Delete from Projekcija Where ProjekcijaId=";
        string recenzijaDelete = @"Delete from Recenzija Where RecenzijaId=";
        string rezervacijaDelete = @"Delete from Rezervacija Where RezervacijaId=";
        string zaposleniDelete = @"Delete from Zaposleni Where ZaposleniId=";
        #endregion

        #region USLOV
        string selectUslovFilm = @"select * from Film where FilmId=";
        string selectUslovKorisnik = @"Select * from Korisnik where KorisnikId=";
        string selectUslovZaposleni = @"select * from Zaposleni where ZaposleniId=";
        string selectUslovPlacanje = @"Select * from Placanje where PlacanjeId="; 
        string selectUslovProjekcija = @"Select * from Projekcija where ProjekcijaId=";
        string selectUslovRecenzija = @"select * from Recenzija where RecenzijaId=";
        string selectUslovRezervacija = @"select * from Rezervacija where RezervacijaId=";
 
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            UcitajPodatke(dataGridCentralni, filmSelect);
            konekcija = kon.KreirajKonekciju();

        }

        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {

            try
            {
                konekcija = kon.KreirajKonekciju();
                konekcija.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(dt);
                if (grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspelo ucitavanje podataka", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnFilm_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, filmSelect);
        }

        private void btnProjekcija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, projekcijaSelect);
        }

        private void btnPlacanje_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, placanjeSelect);
        }

        private void btn_ClickRezervacija(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, rezervacijaSelect);
        }

        private void btnKorisnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, korisnikSelect);
        }

        private void btnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, zaposleniSelect);
        }

        private void btnRecenzija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, recenzijaSelect);
        }

        private void DodajNovog_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(filmSelect))
            {
                prozor = new formaFilm();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, filmSelect);
            }
            else if (ucitanaTabela.Equals(korisnikSelect))
            {
                prozor = new formaKorisnik();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, korisnikSelect);
            }
            else if (ucitanaTabela.Equals(placanjeSelect))
            {
                prozor = new formaPlacanje();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, placanjeSelect);
            }
            else if (ucitanaTabela.Equals(projekcijaSelect))
            {
                prozor = new formaProjekcija();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, projekcijaSelect);
            }
            else if (ucitanaTabela.Equals(recenzijaSelect))
            {
                prozor = new formaRecenzija();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, recenzijaSelect);
            }
            else if (ucitanaTabela.Equals(rezervacijaSelect))
            {
                prozor = new formaRezervacija();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela.Equals(zaposleniSelect))
            {
                prozor = new formaZaposleni();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
        }

        void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                MessageBoxResult resultat = MessageBox.Show("Da li ste sigurni da želite da obrišete?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUpit + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste odabrali red koji želite da obrisete!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci koji bi oštetili druge tabele", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }

        private void ObrisiPostojeceg_Click(object sender, RoutedEventArgs e)
        {

            if (ucitanaTabela.Equals(filmSelect))
            {
                ObrisiZapis(dataGridCentralni, filmDelete);
                UcitajPodatke(dataGridCentralni, filmSelect);
            }
            else if (ucitanaTabela.Equals(korisnikSelect))
            {
                ObrisiZapis(dataGridCentralni, korisnikDelete);
                UcitajPodatke(dataGridCentralni, korisnikSelect);
            }
            else if (ucitanaTabela.Equals(placanjeSelect))
            {
                ObrisiZapis(dataGridCentralni, placanjeDelete);
                UcitajPodatke(dataGridCentralni, placanjeSelect);
            }
            else if (ucitanaTabela.Equals(recenzijaSelect))
            {
                ObrisiZapis(dataGridCentralni, recenzijaDelete);
                UcitajPodatke(dataGridCentralni, recenzijaSelect);
            }
            else if (ucitanaTabela.Equals(projekcijaSelect))
            {
                ObrisiZapis(dataGridCentralni, projekcijaDelete);
                UcitajPodatke(dataGridCentralni, projekcijaSelect);
            }
            else if (ucitanaTabela.Equals(rezervacijaSelect))
            {
                ObrisiZapis(dataGridCentralni, rezervacijaDelete);
                UcitajPodatke(dataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela.Equals(zaposleniSelect))
            {
                ObrisiZapis(dataGridCentralni, zaposleniDelete);
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
        }

        private void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                pomocniRed = red;
                SqlCommand komanda = new SqlCommand
                {
                    Connection = konekcija
                };
                komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                komanda.CommandText = selectUslov + "@id";
                SqlDataReader citac = komanda.ExecuteReader();
                komanda.Dispose();
             while (citac.Read())
                {
                    if (ucitanaTabela.Equals(filmSelect, StringComparison.Ordinal))
                    {
                        formaFilm prozorFilm = new formaFilm(azuriraj, red);
                        prozorFilm.txtNazivFilma.Text = citac["NazivFilma"].ToString();
                        prozorFilm.txtZanr.Text = citac["Zanr"].ToString();
                        prozorFilm.txtReziser.Text = citac["Reziser"].ToString();
                        prozorFilm.txtGodinaIzlaska.Text = citac["GodinaIzlaska"].ToString();
                        prozorFilm.txtTrajanje.Text = citac["Trajanje"].ToString();
                        prozorFilm.ShowDialog();
                    }


                    else if (ucitanaTabela.Equals(korisnikSelect, StringComparison.Ordinal))
                    {
                        formaKorisnik prozorKorisnik = new formaKorisnik(azuriraj, red);
                        prozorKorisnik.txtIme.Text = citac["Ime"].ToString();
                        prozorKorisnik.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorKorisnik.txtNickname.Text = citac["Nickname"].ToString();
                        prozorKorisnik.txtTelefon.Text = citac["BrojTelefona"].ToString();
                        prozorKorisnik.txtEmail.Text = citac["Email"].ToString();
                        if (citac["Pol"].ToString() == "M")
                        {
                            prozorKorisnik.cbxMusko.IsChecked = true;
                            prozorKorisnik.cbxZensko.IsChecked = false;
                        }
                        else
                        {
                            prozorKorisnik.cbxMusko.IsChecked = false;
                            prozorKorisnik.cbxZensko.IsChecked = true;
                        }

                        prozorKorisnik.ShowDialog();
                    }

                    else if (ucitanaTabela.Equals(zaposleniSelect, StringComparison.Ordinal))
                    {
                        formaZaposleni prozorZaposleni = new formaZaposleni(azuriraj, pomocniRed);
                        prozorZaposleni.txtIme.Text = citac["Ime"].ToString();
                        prozorZaposleni.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorZaposleni.txtJMBG.Text = citac["JMBG"].ToString();
                        prozorZaposleni.txtAdresa.Text = citac["Adresa"].ToString();
                        prozorZaposleni.txtEmail.Text = citac["Email"].ToString();
                        prozorZaposleni.txtTelefon.Text = citac["BrojTelefona"].ToString();
                        prozorZaposleni.txtRadnoMesto.Text = citac["RadnoMesto"].ToString();
                        prozorZaposleni.ShowDialog();
                    }

                    

                    else if (ucitanaTabela.Equals(placanjeSelect, StringComparison.Ordinal))
                    {
                        formaPlacanje prozorPlacanje = new formaPlacanje(azuriraj, pomocniRed);
                        prozorPlacanje.txtIznos.Text = citac["Iznos"].ToString();
                        prozorPlacanje.cbNacinPlacanja.SelectedValue = citac["NacinPlacanja"].ToString();
                        prozorPlacanje.cbRezervacija.SelectedValue = citac["Rezervacija"].ToString();
                        prozorPlacanje.cbZaposleni.SelectedValue = citac["Zaposleni"].ToString();
                        prozorPlacanje.ShowDialog();
                    }

                    
                    else if (ucitanaTabela.Equals(projekcijaSelect, StringComparison.Ordinal))
                    {
                        formaProjekcija prozorProjekcija = new formaProjekcija(azuriraj, pomocniRed);
                        prozorProjekcija.cbFilm.SelectedValue = citac["Film"].ToString();
                        prozorProjekcija.txtCena.Text = citac["Cena"].ToString();
                        prozorProjekcija.txtSala.Text = citac["Sala"].ToString();
                        prozorProjekcija.pcDatum.Text = citac["Datum"].ToString();
                        prozorProjekcija.txtVreme.Text = citac["Vreme"].ToString();
                        
                        prozorProjekcija.ShowDialog();
                    }

                    
                    else if (ucitanaTabela.Equals(recenzijaSelect, StringComparison.Ordinal))
                    {
                        formaRecenzija prozorRecenzija = new formaRecenzija(azuriraj, pomocniRed);
                        prozorRecenzija.cbOcena.SelectedValue = citac["Ocena"].ToString();
                        prozorRecenzija.txtKorisnik.Text = citac["Korisnik"].ToString();
                        prozorRecenzija.txtbKomentar.Text = citac["Komentar"].ToString();
                        prozorRecenzija.ShowDialog();
                    }
                    
                    
                    else if (ucitanaTabela.Equals(rezervacijaSelect, StringComparison.Ordinal))
                    {
                        formaRezervacija prozorRezervacija = new formaRezervacija(azuriraj, pomocniRed);
                        prozorRezervacija.cbProjekcija.SelectedValue = citac["Projekcija"].ToString();
                        prozorRezervacija.txtBrojMesta.Text = citac["BrojMesta"].ToString();
                        prozorRezervacija.cbKorisnik.SelectedValue = citac["Korisnik"].ToString();
                        prozorRezervacija.ShowDialog();
                    }


                    
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }


        private void IzmeniPostojeceg_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(filmSelect)) 
            {
                PopuniFormu(dataGridCentralni, selectUslovFilm);
                UcitajPodatke(dataGridCentralni, filmSelect);
            }
            else if (ucitanaTabela.Equals(korisnikSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovKorisnik);
                UcitajPodatke(dataGridCentralni, korisnikSelect);
            }
            else if (ucitanaTabela.Equals(placanjeSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovPlacanje);
                UcitajPodatke(dataGridCentralni, placanjeSelect);
            }
            else if (ucitanaTabela.Equals(projekcijaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovProjekcija);
                UcitajPodatke(dataGridCentralni, projekcijaSelect);
            }
            else if (ucitanaTabela.Equals(recenzijaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovRecenzija);
                UcitajPodatke(dataGridCentralni, recenzijaSelect);
            }
            else if (ucitanaTabela.Equals(rezervacijaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovRezervacija);
                UcitajPodatke(dataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela.Equals(zaposleniSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovZaposleni);
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
        }
    }
}

