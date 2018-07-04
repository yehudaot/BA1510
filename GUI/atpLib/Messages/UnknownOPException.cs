using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atpLib.Messagess
{
    public class UnknownOPException : Exception
    {
        public UnknownOPException()
            : base()
        {
        }

        public UnknownOPException(string message)
            : base(message)
        {
        }

        public UnknownOPException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
