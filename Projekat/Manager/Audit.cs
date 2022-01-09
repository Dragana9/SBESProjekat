using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class Audit : IDisposable
    {
        
        private static EventLog customLog = null;
        const string SourceName = "SecurityManager.Audit";
        const string LogName = "MySecTest";

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
                customLog = new EventLog(LogName,
                    Environment.MachineName, SourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }


        public static void AuthenticationSuccess(string userName)
        {
            //TO DO

            if (customLog != null)
            {
                string UserAuthenticationSuccess =
                    AuditEvents.AuthenticationSuccess;
                string message = String.Format(UserAuthenticationSuccess,
                    userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.AuthenticationSuccess));
            }
        }


        public static void UplataSuccess(string userName)
        {
            //TO DO

            if (customLog != null)
            {
                string UserUplataSuccess = AuditEvents.UplataSuccess;
                string message = String.Format(UserUplataSuccess, userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.UplataSuccess));
            }
        }

        public static void UplataFailed(string userName)
        {
            //TO DO

            if (customLog != null)
            {
                string UserUplataFailed = AuditEvents.UplataFailed;
                string message = String.Format(UserUplataFailed, userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.UplataFailed));
            }
        }


        public static void IsplataSuccess(string userName)
        {
            //TO DO

            if (customLog != null)
            {
                string UserIsplataSuccess = AuditEvents.IsplataSuccess;
                string message = String.Format(UserIsplataSuccess, userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.IsplataSuccess));
            }
        }


        public static void IsplataFailed(string userName)
        {
            //TO DO

            if (customLog != null)
            {
                string UserIsplataFailed = AuditEvents.IsplataFailed;
                string message = String.Format(UserIsplataFailed, userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
                    (int)AuditEventTypes.IsplataFailed));
            }
        }




        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }



    }

}
