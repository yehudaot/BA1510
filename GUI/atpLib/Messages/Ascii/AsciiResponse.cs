using System;
using System.Collections.Generic;
using System.Linq;
using atpLib.Infra;

namespace atpLib.Messages
{
    public abstract class AsciiResponse : IResponse
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<IResponse> InheretedClasses = ReflectiveEnumerator.GetEnumerableOfInterface<IResponse>().ToList<IResponse>();

        protected LinkedList<String> rawResponse {get; set;}
        
        public abstract void parametesFromRawResponse();

        public AsciiResponse() {}
        public static AsciiResponse newAsciiResponse(String command) 
        {
            foreach (IResponse r in InheretedClasses)
            {
                if (command.Equals(r.getUniqueIdentifier()))
                {
                      return (AsciiResponse)Activator.CreateInstance(r.GetType());
                }
            }
            return null;
        }

        public void parseRawResponse(LinkedList<String> rawResponse)
        {
            this.rawResponse = new LinkedList<String>(rawResponse);
            parametesFromRawResponse();
        }

        public abstract object getUniqueIdentifier();
    }
}

