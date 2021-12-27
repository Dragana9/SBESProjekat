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

        public void OtvoriRacun()
        {
            try
            {

                factory.OtvoriRacun() ;
            }
            catch (Exception e)
            {
                Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
            }
        }

        public void TestCommunication()
        {
            try
            {
               
                factory.TestCommunication();
            }
            catch (Exception e)
            {
                Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
            }
        }

        public void ZatvoriRacun()
        {
            try
            {

                factory.ZatvoriRacun();
            }
            catch (Exception e)
            {
                Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
            }
        }
    }
}
