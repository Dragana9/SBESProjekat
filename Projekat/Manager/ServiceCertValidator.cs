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
  public  class ServiceCertValidator: X509CertificateValidator
    {
        /// <summary>
		/// Implementation of a custom certificate validation on the service side.
		/// Service should consider certificate valid if its issuer is the same as the issuer of the service.
		/// If validation fails, throw an exception with an adequate message.
		/// </summary>
		/// <param name="certificate"> certificate to be validate </param>
		public override void Validate(X509Certificate2 certificate)
        {
            bool test = true;

            /// This will take service's certificate from storage
            /// paramerar je klijentski 
            /// iz storage vadimo serverski
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine,
                Formatter1.ParseName(WindowsIdentity.GetCurrent().Name));

            // razliciti issueri
            if (!certificate.Issuer.Equals(srvCert.Issuer))
            {
                test = false;
                throw new Exception("Certificate is not from the valid issuer.");
            }
            if (test )
            {
                Audit.AuthenticationSuccess(Formatter1.ParseName(certificate.SubjectName.Name));
            }
            else
            {
                Audit.AuthenticationFailed(Formatter1.ParseName(certificate.SubjectName.Name));
            }
        }
    }
}
