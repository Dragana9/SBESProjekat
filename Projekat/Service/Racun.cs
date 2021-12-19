using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Racun
    {
        private long broj;
        private double iznos;
        private double dozvoljeniMinus;
        private double blokiran;
        private DateTime poslednjaTransakcija;
        //info kom korisniku pripada?

        public long Broj { get => broj; set => broj = value; }
        public double Iznos { get => iznos; set => iznos = value; }
        public double DozvoljeniMinus { get => dozvoljeniMinus; set => dozvoljeniMinus = value; }
        public double Blokiran { get => blokiran; set => blokiran = value; }
        public DateTime PoslednjaTransakcija { get => poslednjaTransakcija; set => poslednjaTransakcija = value; }

        public Racun() { }

        public Racun(long broj, double iznos, double dozvoljeniMinus, double blokiran, DateTime poslednjaTransakcija)
        {
            broj = broj;
            iznos = iznos;
            dozvoljeniMinus = dozvoljeniMinus;
            blokiran = blokiran;
            poslednjaTransakcija = poslednjaTransakcija;
        }

    }
}
