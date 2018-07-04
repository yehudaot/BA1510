using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atpLib.Messages.SNMP
{
    public class SNMPGetMessage : SNMPMessage
    {
        

        public SNMPGetMessage() : base()
        {

        }

        public SNMPGetMessage(string OID) : base()
        {
            this.OID = OID;
            this.opcode = OP.GET_REQUEST;
        }

    }
}
