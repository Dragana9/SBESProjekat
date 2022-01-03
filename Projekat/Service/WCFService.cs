using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Library1;

namespace Service
{
    public class WCFService : IWCFContract
    {
        public DataBaseContoller db = new DataBaseContoller();

        public string Isplata(int idKorisnika, double isplata)
        {
            List<Racun> sviRacuni = new List<Racun>();
            string retVal;
            bool pronasao = false;
            Racun azurirani = new Racun();
            double trenutniIznos = 0;
            double noviIznos;

            sviRacuni = db.DobaviSveRacune();

            foreach (Racun r in sviRacuni)
            {
                if (r.IdKorisnika == idKorisnika)
                {
                    pronasao = true;
                    trenutniIznos = r.Iznos;
                    azurirani = r;
                    break;


                }

            }

            if (pronasao == true)
            {
                if (azurirani.Blokiran == false)
                {
                    noviIznos = trenutniIznos - isplata;
                    if (noviIznos >= -azurirani.DozvoljeniMinus)
                    {
                        db.AzurirajRacunIznos(azurirani, azurirani.Broj, noviIznos);
                        retVal = "Uspesno izvrsena isplata.";
                        return retVal;
                    }
                    else
                    {
                        retVal = "Nije moguca isplata van dozvoljenog minusa";
                        return retVal;
                    }
                }

            }
           
                if (azurirani.Blokiran == true)
                {
                    retVal = "Racun je blokiran. Nije moguca isplata.";
                    return retVal;
                }
                else
                {
                    retVal = "Nepostojeci id korisnika.";
                    return retVal;
                }
            
        }

        public string Opomena(long broj)
        {
            List<Racun> sviRacuni = new List<Racun>();
            string retVal;
            bool pronasao = false;
            Racun azurirani = new Racun();

            sviRacuni = db.DobaviSveRacune();

            foreach (Racun r in sviRacuni)
            {
                if (r.Broj == broj)
                {
                    pronasao = true;
                    azurirani = r;
                    break;
                
                }
            
            }

            if ( pronasao == true && azurirani.Iznos < 0)
            {
                db.AzurirajRacunBlokiran(azurirani, 1);
                retVal = "Racun korisnika je u minusa.\nUspesno blokiran racun.";
                return retVal;
            }
            else
            {
                retVal = "Nije prekoracen dozvoljeni minus.\n Neuspesno blokiranje racuna. ";
                return retVal;
            }
        }

        public string OtvoriRacun(long broj, double dozvoljeniMinus, int idKorisnika)
        {

            string retVal;

            if (databaseRacuni.dicRacuni.ContainsKey(idKorisnika))
            {

                retVal = "Nije moguce otvoriti racun. Korisnik vec ima racun u banci.";
                return retVal;

            }

            // databaseRacuni.dicRacuni.Add(idKorisnika, new Racun(broj, 0.0, dozvoljeniMinus, false, default, idKorisnika));

            db.SacuvajRacun(new Racun(broj, 0.0, dozvoljeniMinus, Convert.ToBoolean(0), default, idKorisnika));

            retVal = "Racun uspesno otvoren.";
            return retVal;
        
        }

        public string ProveriStanje(int idKorisnika)
        {
            List<Racun> sviRacuni = new List<Racun>();
            string retVal;
            bool pronasao =false;
            double iznos = 0; ;

            sviRacuni = db.DobaviSveRacune();


            foreach (Racun r in sviRacuni)
            {
                if (r.IdKorisnika == idKorisnika)
                {
                    pronasao = true;
                    iznos = r.Iznos;
                    break;

                }

            }

            if (pronasao == true)
            {
                retVal = $"Stanje racuna: {iznos}";
            }
            else
            {
                retVal = "Nepostojeci korisnicki ID.";
            }

            return retVal;

        }

        public string Uplata(long racun, double uplata)
        {
            List<Racun> sviRacuni = new List<Racun>();
            string retVal ="";
            bool pronasao = false;
            Racun azurirani = new Racun();
            double trenutniIznos=0;
            double noviIznos;

            sviRacuni = db.DobaviSveRacune();

            foreach (Racun r in sviRacuni)
            {
                if (r.Broj == racun)
                {
                    pronasao = true;
                    trenutniIznos = r.Iznos;
                    azurirani = r;
                    break;
                    
                
                }
            
            }

            if (pronasao == true)
            {
                noviIznos = trenutniIznos + uplata;
                db.AzurirajRacunIznos(azurirani, racun, noviIznos);
                if(noviIznos >= 0 && azurirani.Blokiran == true)
                {
                    db.AzurirajRacunBlokiran(azurirani, 0);
                    retVal = "Odblokiran racun.";
                }
                retVal += "Uspesno izvrsena uplata.";
                return retVal;

            }
            else
            {
                retVal = "Netacan broj racuna.";
                return retVal;
            }


        }

        public string ZatvoriRacun(int idKorisnika)
        {
            List<Racun> sviRacuni = new List<Racun>();
            string retVal;
            bool pronasao = false;
            Racun zatvoreni = new Racun() ;
            sviRacuni = db.DobaviSveRacune();

            foreach (Racun r in sviRacuni)
            {
                if (r.IdKorisnika == idKorisnika)
                {
                    pronasao = true;
                    zatvoreni = r;
                    break;
                
                }
            
            }
            if (pronasao == true)
            {
                db.IzbrisiRacun(zatvoreni);
                retVal = "Uspesno zatvoren racun";
                return retVal;
            }
            else
            {
                retVal = "Neuspesno zatvaranje racuna. Nije pronadjen otvoren racun sa unetim korisnickim id-em.";
                return retVal;
            }

        }
    }
}
