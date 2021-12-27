using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Library1
{
    [ServiceContract]
    public interface IWCFContract
    {
        [OperationContract]
        void TestCommunication();

        [OperationContract]
        void OtvoriRacun();

        [OperationContract]
        void ZatvoriRacun();

    }
}
