using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public enum AuditEventTypes
    {
        AuthenticationSuccess = 0,
        UplataSuccess=1,
        UplataFailed=2,
        IsplataSuccess=3,
        IsplataFailed=4,
        AuthenticationFailed=5
    }

    public  class AuditEvents
    {
        private static ResourceManager resourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if (resourceManager == null)
                    {
                        resourceManager = new ResourceManager
                            (typeof(AuditEventFile).ToString(),
                            Assembly.GetExecutingAssembly());
                    }
                    return resourceManager;
                }
            }
        }

        public static string AuthenticationSuccess
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.AuthenticationSuccess.ToString());
            }
        }

        public static string AuthenticationFailed
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.AuthenticationFailed.ToString());
            }
        }

        public static string UplataSuccess
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.UplataSuccess.ToString());
            }
        }

        public static string UplataFailed
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.UplataFailed.ToString());
            }
        }


        public static string IsplataSuccess
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.IsplataSuccess.ToString());
            }
        }


        public static string IsplataFailed
        {
            get
            {
                // TO DO
                return ResourceMgr.GetString(AuditEventTypes.IsplataFailed.ToString());
            }
        }


    }
}
