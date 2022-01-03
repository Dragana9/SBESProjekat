using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library1
{
    public class Racun
    {

        private long broj;
        private double iznos;
        private double dozvoljeniMinus;
        private bool blokiran;
        private DateTime poslednjaTransakcija;
        private int idKorisnika;
       

        public long Broj { get => broj; set => broj = value; }
        public double Iznos { get => iznos; set => iznos = value; }
        public double DozvoljeniMinus { get => dozvoljeniMinus; set => dozvoljeniMinus = value; }
        public bool Blokiran { get => blokiran; set => blokiran = value; }
        public DateTime PoslednjaTransakcija { get => poslednjaTransakcija; set => poslednjaTransakcija = value; }
        public int IdKorisnika { get => idKorisnika; set => idKorisnika = value; }

        public Racun() { }

        public Racun(long broj, double iznos, double dozvoljeniMinus, bool blokiran, DateTime poslednjaTransakcija, int idKorisnika)
        {
            Broj = broj;
            Iznos = iznos;
            DozvoljeniMinus = dozvoljeniMinus;
            Blokiran = blokiran;
            PoslednjaTransakcija = poslednjaTransakcija;
            IdKorisnika = idKorisnika;
        }
    }
}
