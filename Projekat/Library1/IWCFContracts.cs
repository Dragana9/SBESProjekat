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
        string OtvoriRacun(long broj, double dozvoljeniMinus, int idKorisnika);

        [OperationContract]
        string ZatvoriRacun(int idKorisnika);

        [OperationContract]
        string ProveriStanje(int idKorisnika);

        [OperationContract]
        string Uplata(long racun, double uplata);
        [OperationContract]
        string Isplata(int idKorisnika, double uplata);

        [OperationContract]
        string Opomena(long broj);

    }
}
