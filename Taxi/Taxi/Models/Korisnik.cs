using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Models
{
    public enum Pol
    {
        Muski,
        Zenski
    }

    public enum Uloge
    {
        Musterija,
        Dispecer,
        Vozac
    }

    public class Korisnik
    {
        public string Korisnicko_ime { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public string Jmbg { get; set; }
        public string Kontakt_telefon { get; set; }
        public string Email { get; set; }
        public Uloge Uloga { get; set; }
        public bool Ulogovan { get; set; }
        public bool Filter { get; set; } = false;
        public bool Sortiranje { get; set; } = false;
        public bool Pretrazivanje { get; set; } = false;
        public List<Voznja> listaVoznja { get; set; }
        public List<Voznja> Filtrirane { get; set; }
        public List<Voznja> Sortirane { get; set; }
        public List<Voznja> Pretrazene { get; set; }

        public Korisnik()
        {
            listaVoznja = new List<Voznja>();
            Ulogovan = false;
            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }

        public Korisnik(string korisnicko_ime, string lozinka, string ime, string prezime, Pol pol, string jmbg, string kontakt_telefon, string email, Uloge uloga)
        {
            Korisnicko_ime = korisnicko_ime;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Jmbg = jmbg;
            Kontakt_telefon = kontakt_telefon;
            Email = email;
            Uloga = uloga;
            Ulogovan = false;
            listaVoznja = new List<Voznja>();
            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }
    }

    public class PostojeciKorisnici
    {
        public static List<Musterija> ListaMusterija { get; set; }
        public static List<Vozac> ListaVozaca { get; set; }
        public static List<Dispecer> ListaDispecera { get; set; }
        public static List<Korisnik> ListaKorisnika { get; set; }
        public static List<Voznja> ListaSvihVoznji { get; set; }

        public PostojeciKorisnici()
        {
            ListaMusterija = new List<Musterija>();
            ListaVozaca = new List<Vozac>();
            ListaDispecera = new List<Dispecer>();
            ListaKorisnika = new List<Korisnik>();
            ListaSvihVoznji = new List<Voznja>();
        }
    }

    [Serializable]
    public class Dispecer : Korisnik
    {
        public bool TraziVozac { get; set; } = false;
        public bool TraziMusteriju { get; set; } = false;
        public List<Voznja> NadjeniVozaci { get; set; }
        public List<Voznja> NadjeneMusterije { get; set; }

        public Dispecer(string korisnicko_ime, string lozinka, string ime, string prezime, Pol pol, string jmbg, string kontakt_telefon,
        string email, Uloge uloga) : base(korisnicko_ime, lozinka, ime, prezime, pol, jmbg, kontakt_telefon, email, uloga)
        {
            Korisnicko_ime = korisnicko_ime;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Jmbg = jmbg;
            Kontakt_telefon = kontakt_telefon;
            Email = email;
            Uloga = uloga;
            Ulogovan = false;
            listaVoznja = new List<Voznja>();
            NadjeniVozaci = new List<Voznja>();
            NadjeneMusterije = new List<Voznja>();
            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }

        public Dispecer()
        {
            listaVoznja = new List<Voznja>();
            Ulogovan = false;
            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            NadjeniVozaci = new List<Voznja>();
            NadjeneMusterije = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }
    }

    [Serializable]
    public class Musterija : Korisnik
    {
        public Musterija(string korisnicko_ime, string lozinka, string ime, string prezime, Pol pol, string jmbg, string kontakt_telefon,
       string email, Uloge uloga) : base(korisnicko_ime, lozinka, ime, prezime, pol, jmbg, kontakt_telefon, email, uloga)
        {
            Korisnicko_ime = korisnicko_ime;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Jmbg = jmbg;
            Kontakt_telefon = kontakt_telefon;
            Email = email;
            Uloga = uloga;
            Ulogovan = false;
            listaVoznja = new List<Voznja>();
            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }

        public Musterija()
        {
            listaVoznja = new List<Voznja>();
            Ulogovan = false;
            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }
    }

    [Serializable]
    public class Vozac : Korisnik
    {
        public Automobil Automobil { get; set; }
        public Lokacija Lokacija { get; set; }
        public bool Zauzet { get; set; } = false;

        public Vozac()
        {
            listaVoznja = new List<Models.Voznja>();
        }

        public Vozac(Automobil a, Lokacija l)
        {
            a = new Automobil();
            l = new Lokacija();
            listaVoznja = new List<Models.Voznja>();
            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }

        public Vozac(string korisnicko_ime, string lozinka, string ime, string prezime, Pol pol, string jmbg, string kontakt_telefon, string email, Uloge uloga, string ulica, string broj, string mesto, string postanski_broj) : base(korisnicko_ime, lozinka, ime, prezime, pol, jmbg, kontakt_telefon, email, uloga)
        {
            Korisnicko_ime = korisnicko_ime;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Jmbg = jmbg;
            Kontakt_telefon = kontakt_telefon;
            Email = email;
            Uloga = uloga;
            listaVoznja = new List<Models.Voznja>();

            Adresa a = new Adresa();
            a.Ulica = ulica;
            a.Broj = broj;
            a.PostanskiBroj = postanski_broj;
            a.Mesto = mesto;
            Lokacija l = new Lokacija("2", "2", a);
            Lokacija = l;

            Filtrirane = new List<Voznja>();
            Sortirane = new List<Voznja>();
            Pretrazene = new List<Voznja>();
        }
    }
}