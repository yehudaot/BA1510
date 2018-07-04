using System;
using System.Collections.Generic;
using System.Linq;
using atpLib.Infra;

namespace atpLib.Messages.zUp
{
    public abstract class zUpResponse : IResponse
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<IResponse> InheretedClasses = ReflectiveEnumerator.GetEnumerableOfInterface<IResponse>().ToList<IResponse>();
        //private static List<zUpResponse> InheretedClasses = ReflectiveEnumerator.GetEnumerableOfType<zUpResponse>().ToList<zUpResponse>();

        protected string rawResponse {get; set;}
        
        public abstract void parametesFromRawResponse();

        public zUpResponse() { }
        public static zUpResponse newResponse(String command) 
        {
            if (command.Length >= 2)
            {
                foreach (IResponse r in InheretedClasses)
                {
                    if (command.Substring(0, 2).Equals(r.getUniqueIdentifier()))
                    {
                        return (zUpResponse)r;
                    }
                }
            }
            return null;
        }

        public void parseRawResponse(string rawResponse)
        {
            this.rawResponse = rawResponse;
            parametesFromRawResponse();
        }

        public abstract object getUniqueIdentifier();
    }
}

