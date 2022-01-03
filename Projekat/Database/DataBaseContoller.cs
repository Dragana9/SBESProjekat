
using Library1;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DataBaseContoller
    {
        public static SQLiteConnection myConnection;


        public DataBaseContoller()
        {

            try
            {
                myConnection = new SQLiteConnection("Data Source=Baza.sqlite");
                if (!File.Exists("Baza.sqlite"))
                {
                    SQLiteConnection.CreateFile("Baza.sqlite");
                    Console.WriteLine("Database file creates");

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public  void SacuvajRacun(Racun racun)
        {
            try
            {
                myConnection.Open();
                string querry = $"insert into Racuni ('broj','iznos','dozvoljeniMinus','blokiran','poslednjaTransakcija','idKorisnika')" +
                                 $" values (@br,@iz,@dm,@bl,@pt,@idk)";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                cmd.Parameters.AddWithValue("@br", racun.Broj);
                cmd.Parameters.AddWithValue("@iz", racun.Iznos);
                cmd.Parameters.AddWithValue("@dm", racun.DozvoljeniMinus);
                cmd.Parameters.AddWithValue("@bl", racun.Blokiran);
                cmd.Parameters.AddWithValue("@pt", racun.PoslednjaTransakcija); //ovo samo za sada stoji da ne mijenjamo sve, ali ce vjvj biti izbaceno
                cmd.Parameters.AddWithValue("@idk", racun.IdKorisnika);
               
                var result = cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SQLiteException sqle)
            {
                myConnection.Close();
                Console.WriteLine(sqle.Message);
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
            }
        }


        public void IzbrisiRacun(Racun r)
        {
            try
            {
                myConnection.Open();
                string querry = "delete from Racuni where idKorisnika ='" + r.IdKorisnika + "'";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);

                var result = cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SQLiteException sqle)
            {
                myConnection.Close();
                Console.WriteLine(sqle.Message);
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
            }

        }

        public List<Racun> DobaviSveRacune()
        {
            try
            {
                List<Racun> retVal = new List<Racun>();
                myConnection.Open();
                string querry = $"select * from Racuni";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                var result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        retVal.Add(new Racun(Convert.ToInt64(result["broj"]), Convert.ToDouble(result["iznos"]), Convert.ToDouble(result["dozvoljeniMinus"]),
                                                 Convert.ToBoolean(result["blokiran"]), Convert.ToDateTime(result["poslednjaTransakcija"]), Convert.ToInt32(result["idKorisnika"])));
                    }
                }
                myConnection.Close();
                return retVal;
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
                return null;
            }

        }


        public void AzurirajRacunIznos(Racun r,long broj, double noviIznos)
        {
            try
            {
                myConnection.Open();
                string querry = $"UPDATE Racuni SET  broj='{r.Broj}', iznos='{noviIznos}', dozvoljeniMinus='{r.DozvoljeniMinus}', blokiran='{r.Blokiran}', poslednjaTransakcija='{DateTime.Now}', idKorisnika='{r.IdKorisnika}' WHERE broj='{broj}'";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                var result = cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SQLiteException sqle)
            {
                myConnection.Close();
                Console.WriteLine(sqle.Message);
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
            }
        }

        public void AzurirajRacunBlokiran(Racun r, int blokiran)
        {
            try
            {
                myConnection.Open();
                string querry = $"UPDATE Racuni SET  broj='{r.Broj}', iznos='{r.Iznos}', dozvoljeniMinus='{r.DozvoljeniMinus}', blokiran='{blokiran}', poslednjaTransakcija='{r.PoslednjaTransakcija}', idKorisnika='{r.IdKorisnika}' WHERE broj='{r.Broj}'";
                SQLiteCommand cmd = new SQLiteCommand(querry, myConnection);
                var result = cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (SQLiteException sqle)
            {
                myConnection.Close();
                Console.WriteLine(sqle.Message);
            }
            catch (Exception e)
            {
                myConnection.Close();
                Console.WriteLine(e.Message);
            }
        }
    }
}
