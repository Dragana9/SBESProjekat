using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Manager;
using Library1;
using System.Security.Cryptography.X509Certificates;

namespace Client
{
    public class WCFClient : ChannelFactory<IWCFContract>, IWCFContract, IDisposable
    {
        IWCFContract factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {

            string cltCertCN = Formatter1.ParseName(WindowsIdentity.GetCurrent().Name);

            //validacija
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);


            factory = this.CreateChannel();
        }

        public string OtvoriRacun(long broj, double dozvoljeniMinus, int idKorisnika)
        {
            string retVal = "";
            try
            {

                retVal = factory.OtvoriRacun(broj, dozvoljeniMinus, idKorisnika);
                return retVal;

            }
            catch (Exception e)
            {

                Console.WriteLine("[OtvoriRacun] ERROR = {0}", e.Message);
                return retVal;
            }
        }

        public string ProveriStanje(int idKorisnika)
        {
            string retVal = "";
            try
            {

                retVal = factory.ProveriStanje(idKorisnika);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("[ProveriStanja] ERROR = {0}", e.Message);
                return retVal;
            }
        }

        public string ZatvoriRacun(int idKorisnika)
        {
            string retVal = "";
            try
            {

                retVal = factory.ZatvoriRacun(idKorisnika);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("[ZatvoriRacun] ERROR = {0}", e.Message);
                return retVal;
            }
        }

        public string Uplata(long racun, double uplata)
        {
            string retVal = "";
            try
            {

                retVal = factory.Uplata(racun, uplata);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("[Uplata] ERROR = {0}", e.Message);
                return retVal;
            }
        }

        public string Isplata(int idKorisnika, double uplata)
        {
            string retVal = "";
            try
            {

                retVal = factory.Isplata(idKorisnika, uplata);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("[Isplata] ERROR = {0}", e.Message);
                return retVal;
            }
        }


        public string Opomena(long broj)
        {
            string retVal = "";
            try
            {

                retVal = factory.Opomena(broj);
                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("[Opomena] ERROR = {0}", e.Message);
                return retVal;
            }
        }
    }
}
