using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Library1;
using Service;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "markovcert";

            NetTcpBinding binding = new NetTcpBinding();
          //  string address = "net.tcp://localhost:9999/WCFService";
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:9999/WCFService"),
                                     new X509CertificateEndpointIdentity(srvCert));

           // EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
            //EndpointIdentity.CreateUpnIdentity("WCFService"));

            using (WCFClient proxy = new WCFClient(binding, address1))
            {
                while (true)
                {
                   
                    Console.WriteLine("Izaberte opciju koju zelite: \n\n");
                    Console.WriteLine("1. Otvorite racun\n2. Zatvorite racun\n3. Provera stanja\n4. Uplata\n5. Isplata\n6. Opomena\n7. Izadji iz sistema");

                    var opp = Console.ReadLine();

                    if (opp == "1")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Otvaranje racuna============\n\n");
                        Console.WriteLine("Unesite sledece podatke: \n");
                        Console.WriteLine("Broj racuna: \n");
                        var br = Console.ReadLine();
                        Console.WriteLine("Dozvoljeni minus: \n");
                        var dm = Console.ReadLine();
                        Console.WriteLine("ID korisnika: \n");
                        var idk = Console.ReadLine();

                        var retVal = proxy.OtvoriRacun(long.Parse(br), double.Parse(dm), Convert.ToInt32(idk));
                        Console.WriteLine(retVal);

                        
                    }
                    else if (opp == "2")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Zatvaranje racuna============\n\n");
                        Console.WriteLine("Unesite id korisnika ciji racun zelite da zatvorite: \n");
                        Console.WriteLine("ID korisnika: \n");
                        var id = Console.ReadLine();

                        var retVal = proxy.ZatvoriRacun(Convert.ToInt32(id));
                        Console.WriteLine(retVal);
                    }
                    else if(opp == "3")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Provera stanja============\n\n");
                        Console.WriteLine("Unesite korisnicki id: \n");
                        Console.WriteLine("ID korisnika: \n");
                        var id = Console.ReadLine();

                        var retVal = proxy.ProveriStanje(Convert.ToInt32(id));
                        Console.WriteLine(retVal);


                    }
                    else if (opp == "4")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Uplata============\n\n");
                        Console.WriteLine("Unesite broj racuna i iznos koji zelite da uplatite: \n");
                        Console.WriteLine("Broj racuna: \n");
                        var racun = Console.ReadLine();
                        Console.WriteLine("Iznos za uplatu: \n");
                        var uplata = Console.ReadLine();

                        var retVal = proxy.Uplata(Convert.ToInt64(racun), Convert.ToDouble(uplata));
                        Console.WriteLine(retVal);


                    }
                    else if (opp == "5")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Isplata============\n\n");
                        Console.WriteLine("Unesite svoj koirsnicki id i iznos koji zelite da podignete sa racuna: \n");
                        Console.WriteLine("ID korisnika: \n");
                        var id = Console.ReadLine();
                        Console.WriteLine("Iznos za isplatu: \n");
                        var isplata = Console.ReadLine();

                        var retVal = proxy.Isplata(Convert.ToInt32(id), Convert.ToDouble(isplata));
                        Console.WriteLine(retVal);


                    }
                    else if (opp == "6")
                    {
                        Console.Clear();
                        Console.WriteLine("==============Opomena============\n\n");
                        Console.WriteLine("Unesite broj racuna koji zelite da blokirate\n");
                        Console.WriteLine("Broj racuna: \n");
                        var broj = Console.ReadLine();
                       
                        var retVal = proxy.Opomena(Convert.ToInt64(broj));
                        Console.WriteLine(retVal);


                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Izabrali ste pogresnu opciju.");
                    }



                  
                }
                Console.ReadLine();
            }

          
           
        }
    }
}
