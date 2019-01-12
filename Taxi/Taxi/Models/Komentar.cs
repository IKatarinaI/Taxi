using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.Models
{
    public enum Ocene
    {
        neocenjen = 0,
        veomaLose = 1,
        lose = 2,
        dobro = 3,
        veomaDobro = 4,
        odlicno = 5
    }

    public class Komentar
    {
        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public Voznja Voznja { get; set; }
        public Ocene OcenaVoznje { get; set; }
        public Korisnik Korisnik { get; set; }

        public Komentar() { }

        public Komentar(string opis, DateTime datumObjave, Voznja voznja, Ocene ocenaVoznje, Korisnik k)
        {
            Opis = opis;
            DatumObjave = datumObjave;
            Voznja = voznja;
            OcenaVoznje = ocenaVoznje;
            Korisnik = k;
        }
    }
}