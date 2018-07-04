using System;
using System.Collections.Generic;
using System.Text;

namespace atpLib.Messages.zUp
{
    public abstract class zUpMessage : IMessage
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string command { get; set; }

        public zUpMessage(string command)
        {
            this.command = command;
        }

        public abstract String[] parametersToStringArray();

        public Char[] asCharArray()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(":");
            sb.Append(command);
            List<String> stringArr = new List<string>();
            stringArr.AddRange(parametersToStringArray());
            foreach (string s in stringArr)
            {
                sb.Append(s);
            }
            sb.Append(";");
            return sb.ToString().ToCharArray();
        }
    }
}
