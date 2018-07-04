using System;
using System.Collections.Generic;
using System.Text;

namespace atpLib.Messages
{
    public abstract class AsciiMessage : IMessage
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string command { get; set; }

        public AsciiMessage(string command)
        {
            this.command = command;
        }

        public abstract String[] parametersToStringArray();

        public Char[] asCharArray()
        {
            StringBuilder sb = new StringBuilder();
            List<String> stringArr = new List<string>();
            stringArr.Add(command);
            stringArr.AddRange(parametersToStringArray());
            foreach (string s in stringArr)
            {
                sb.Append(s);
                sb.Append(" ");
            }
            sb.Remove(sb.Length-1, 1);
            sb.Append("\r\n");
            return sb.ToString().ToCharArray();
        }
    }
}
