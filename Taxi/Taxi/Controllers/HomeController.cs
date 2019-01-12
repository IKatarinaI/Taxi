using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Taxi.Models;

namespace Taxi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Korisnik korisnik = Session["korisnik"] as Korisnik;

            korisnik = new Korisnik();
            Session["korisnik"] = korisnik;

            return View("Index");
        }

        [HttpPost]
        public ActionResult Registracija(string user, string pass, string ime, string prezime, string pol, string jmbg, string telefon, string email)
        {
            Pol p = Pol.Muski;
            switch (pol)
            {
                case "MUSKI":
                    p = Pol.Muski;
                    break;
                case "ZENSKI":
                    p = Pol.Zenski;
                    break;
            }

            Korisnik m = new Musterija(user, pass, ime, prezime, p, jmbg, telefon, email, Uloge.Musterija);

            if (PostojeciKorisnici.ListaMusterija != null)
            {
                foreach (Korisnik k in PostojeciKorisnici.ListaMusterija)
                {
                    if (k.Korisnicko_ime == m.Korisnicko_ime)
                    {
                        return View("Greska");
                    }
                }
            }

            PostojeciKorisnici.ListaKorisnika.Add(m);
            PostojeciKorisnici.ListaMusterija.Add(m as Musterija);
            SacuvajKorisnika(PostojeciKorisnici.ListaKorisnika);

            return View("Index");
        }

        [HttpPost]
        public ActionResult Logovanje(string user, string pass)
        {
            foreach (Musterija m in PostojeciKorisnici.ListaMusterija)
            {
                if (m.Korisnicko_ime == user && m.Lozinka != pass)
                {
                    return View("Greska");
                }
                else if (m.Korisnicko_ime == user && m.Lozinka == pass)
                {
                    m.Ulogovan = true;
                    Session["korisnik"] = m;
                    return View("Musterija", m);
                }
            }

            foreach (Dispecer d in PostojeciKorisnici.ListaDispecera)
            {
                if (d.Korisnicko_ime == user && d.Lozinka != pass)
                {
                    return View("Greska");
                }
                else if (d.Korisnicko_ime == user && d.Lozinka == pass)
                {
                    d.Ulogovan = true;
                    Session["korisnik"] = d;
                    return View("Dispecer", d);
                }
            }

            foreach (Vozac v in PostojeciKorisnici.ListaVozaca)
            {
                if (v.Korisnicko_ime == user && v.Lozinka != pass)
                {
                    return View("Greska");
                }
                else if (v.Korisnicko_ime == user && v.Lozinka == pass)
                {
                    v.Ulogovan = true;
                    Session["korisnik"] = v;
                    return View("Vozac", v);
                }
            }

            return View("Greska");
        }

        // dispecer
        [HttpPost]
        public ActionResult KreirajVozaca(string user, string pass, string ime, string prezime, string pol, string jmbg, string telefon, string email, string godisteAuto, string regAuto, string brAuto, string tipAuto)
        {
            TipAutomobila ti = TipAutomobila.KOMBI;

            switch (tipAuto)
            {
                case "KOMBI":
                    ti = TipAutomobila.KOMBI;
                    break;
                case "PUTNICKI":
                    ti = TipAutomobila.PUTNICKI;
                    break;
            }

            Pol p = Pol.Muski;
            if (pol.Equals("MUSKI"))
                p = Pol.Muski;
            else
                p = Pol.Zenski;

            Vozac v = new Vozac(user, pass, ime, prezime, p, jmbg, telefon, email, Uloge.Vozac, ulica, broj, mesto, postanski_broj);

            Automobil a = new Automobil(v, godiste, reg, taxiBroj, ti);

            v.Automobil = a;

            foreach (Vozac v1 in PostojeciKorisnici.ListaVozaca)
            {
                if (v1.Korisnicko_ime == v.Korisnicko_ime)
                {
                    return View("KorisnikPostoji");
                }
            }

            PostojeciKorisnici.ListaVozaca.Add(v);

            foreach (Korisnik k in PostojeciKorisnici.ListaKorisnika)
            {
                if (k.Korisnicko_ime == v.Korisnicko_ime)
                {
                    return View("KorisnikPostoji");
                }
            }


            PostojeciKorisnici.ListaKorisnika.Add(v);

            Sacuvaj(PostojeciKorisnici.ListaKorisnika);
        }

        private void SacuvajKorisnika(List<Korisnik> korisnici)
        {
            string filename = @"C:\Users\Katarina\Desktop\Taxi\Taxi\Taxi\Taxi\korisnici.xml";
            XmlWriter writer = null;
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;

                writer = XmlWriter.Create(filename, settings);
                writer.WriteStartElement("Ulogovani");
                foreach (Korisnik m in korisnici)
                {
                    if (m.Uloga == Uloge.Vozac)
                    {
                        foreach (Vozac vo in PostojeciKorisnici.ListaVozaca)
                        {
                            if (vo.Korisnicko_ime == m.Korisnicko_ime)
                            {
                                writer.WriteStartElement("Korisnik");
                                writer.WriteElementString("Ime", vo.Ime);
                                writer.WriteElementString("Prezime", vo.Prezime);
                                writer.WriteElementString("Jmbg", vo.Jmbg.ToString());
                                writer.WriteElementString("KorisnickoIme", vo.Korisnicko_ime);
                                writer.WriteElementString("Lozinka", vo.Lozinka);
                                writer.WriteElementString("Pol", vo.Pol.ToString());
                                writer.WriteElementString("E-Mail", vo.Email);
                                writer.WriteElementString("BrojTelefona", vo.Kontakt_telefon);
                                writer.WriteElementString("Uloga", vo.Uloga.ToString());
                                writer.WriteStartElement("Automobil");
                                writer.WriteElementString("Godiste", vo.Automobil.GodisteAutomobila.ToString());
                                writer.WriteElementString("Registracija", vo.Automobil.BrojRegistarskeOznake.ToString());
                                writer.WriteElementString("TaxiBroj", vo.Automobil.BrojTaksiVozila.ToString());
                                writer.WriteElementString("TipVozila", vo.Automobil.Tip.ToString());
                                writer.WriteEndElement();
                                writer.WriteStartElement("Adresa");
                                writer.WriteElementString("NazivUlice", vo.Lokacija.Adresa.Ulica);
                                writer.WriteElementString("BrojUlice", vo.Lokacija.Adresa.Broj.ToString());
                                writer.WriteElementString("Grad", vo.Lokacija.Adresa.Mesto);
                                writer.WriteElementString("PostanskiBroj", vo.Lokacija.Adresa.PostanskiBroj.ToString());
                                writer.WriteElementString("X", vo.Lokacija.KoordinataX.ToString());
                                writer.WriteElementString("Y", vo.Lokacija.KoordinataY.ToString());
                                writer.WriteEndElement();
                                writer.WriteEndElement();
                            }
                        }
                    }
                    else
                    {
                        writer.WriteStartElement("Korisnik");
                        writer.WriteElementString("Ime", m.Ime);
                        writer.WriteElementString("Prezime", m.Prezime);
                        writer.WriteElementString("Jmbg", m.Jmbg.ToString());
                        writer.WriteElementString("KorisnickoIme", m.Korisnicko_ime);
                        writer.WriteElementString("Lozinka", m.Lozinka);
                        writer.WriteElementString("Pol", m.Pol.ToString());
                        writer.WriteElementString("E-Mail", m.Email);
                        writer.WriteElementString("BrojTelefona", m.Kontakt_telefon);
                        writer.WriteElementString("Uloga", m.Uloga.ToString());
                        writer.WriteEndElement();
                    }
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private void SacuvajVoznju(List<Voznja> voznje)
        {
            string filename = @"C:\Users\Katarina\Desktop\Taxi\Taxi\Taxi\Taxi\voznje.xml";
            XmlWriter writer = null;
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;

                writer = XmlWriter.Create(filename, settings);
                writer.WriteStartElement("Voznje");

                foreach (Voznja v in voznje)
                {

                    if (v.Odrediste == null)
                    {
                        Adresa a = new Adresa("Nepoznata", "0", "Nepoznato", "0");
                        Lokacija l = new Lokacija("0", "0", a);
                        v.Odrediste = l;
                    }
                    if (v.Vozac == null)
                    {
                        Vozac voz = new Vozac("nema", "nema", "nema", "nema", Pol.Muski, "0000", "nema", "nema", Uloge.Vozac, "nema", "nema", "nema", "nema");
                        v.Vozac = voz;
                    }
                    if (v.Dispecer == null)
                    {
                        Dispecer d = new Dispecer("nema", "nema", "nema", "nema", Pol.Muski, "0000", "nema", "nema", Uloge.Dispecer);
                        v.Dispecer = d;
                    }

                    if (v.Komentar == null)
                    {
                        Komentar k = new Komentar("bez opisa", DateTime.MinValue, v, Ocene.neocenjen, v.Vozac);
                        v.Komentar = k;
                    }
                    if (v.Musterija == null)
                    {
                        Musterija m = new Musterija("nema", "nema", "nema", "nema", Pol.Muski, "000", "nema", "nema", Uloge.Musterija);
                        v.Musterija = m;
                    }

                    string odrediste = v.Odrediste.Adresa.Mesto + "_" + v.Odrediste.Adresa.Broj.ToString() + "," + v.Odrediste.Adresa.Mesto + "_" + v.Odrediste.Adresa.PostanskiBroj.ToString();
                    string pocetna = v.LokacijaNaKojuTaksiDolazi.Adresa.Mesto + "_" + v.LokacijaNaKojuTaksiDolazi.Adresa.Broj.ToString() + "," + v.LokacijaNaKojuTaksiDolazi.Adresa.Mesto + "_" + v.LokacijaNaKojuTaksiDolazi.Adresa.PostanskiBroj.ToString();

                    writer.WriteStartElement("Voznja");
                    writer.WriteElementString("DatumPorudzbine", v.Datum_i_vreme.ToString());
                    writer.WriteElementString("LokacijaNaKojuTaksiDolazi", pocetna);
                    writer.WriteElementString("KrajnjaLokacija", odrediste);
                    writer.WriteElementString("TipVozila", v.TipAutomobila.ToString());
                    writer.WriteElementString("MusterijaIme", v.Musterija.Ime);
                    writer.WriteElementString("MusterijaPrezime", v.Musterija.Prezime);
                    writer.WriteElementString("VozacIme", v.Vozac.Ime);
                    writer.WriteElementString("VozacPrezime", v.Vozac.Prezime);
                    writer.WriteElementString("DispecerIme", v.Dispecer.Ime);
                    writer.WriteElementString("DispececrPrezime", v.Dispecer.Prezime);
                    writer.WriteElementString("Status", v.StatusVoznje.ToString());
                    writer.WriteElementString("KomentarOpis", v.Komentar.Opis);
                    writer.WriteElementString("KomentarDatum", v.Komentar.DatumObjave.ToString());
                    writer.WriteElementString("KomentarOcena", v.Komentar.OcenaVoznje.ToString());
                    writer.WriteElementString("Iznos", v.Iznos);
                    writer.WriteEndElement();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
