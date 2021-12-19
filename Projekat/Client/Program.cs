using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/WCFService";

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
