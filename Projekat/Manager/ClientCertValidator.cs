﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
   public class ClientCertValidator: X509CertificateValidator
    {
        /// <summary>
		/// Implementation of a custom certificate validation on the client side.
		/// Client should consider certificate valid if the given certifiate is not self-signed.
		/// If validation fails, throw an exception with an adequate message.
		/// </summary>
		/// <param name="certificate"> certificate to be validate </param>
		public override void Validate(X509Certificate2 certificate)
        {

            bool test = true;

            

            //parametar Validate metode-serverski sertifikat
            //iz storage kupimo klijentski 
            X509Certificate2 cltCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine,
               Formatter1.ParseName(WindowsIdentity.GetCurrent().Name));

            //issueri nisu isti
            if (!certificate.Issuer.Equals(cltCert.Issuer))
            {
                test = false;
                throw new Exception("Certificate is not from the valid issuer.");
            }

            //serverski self-signed
            if (certificate.Subject.Equals(certificate.Issuer))
            {
                test = false;
                throw new Exception("Certificate is self-issued.");
            }

            if (test)
            {
                try
                {
                    Audit.AuthenticationSuccess(Formatter1.ParseName(certificate.SubjectName.Name));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    Audit.AuthenticationFailed(Formatter1.ParseName(certificate.SubjectName.Name));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            }
    }
}
