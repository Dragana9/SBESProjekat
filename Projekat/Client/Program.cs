using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "markovcert";

            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/WCFService";
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
                                     new X509CertificateEndpointIdentity(srvCert));

            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address),
            EndpointIdentity.CreateUpnIdentity("WCFService"));

            using (WCFClient proxy = new WCFClient(binding, endpointAddress))
            {
                proxy.TestCommunication();
            }

          
            Console.ReadLine();
        }
    }
}
