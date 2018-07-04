using Lextm.SharpSnmpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static atpLib.Messages.SNMP.SNMPMessage;

namespace atpLib.Messages.SNMP
{
    public class SNMPGetResponse : SNMPResponse
    {
        public SNMPGetResponse(IList<Variable> Variables) : base(Variables)
        {

        }

        public override object getUniqueIdentifier()
        {
            return new OP(OP.GET_REQUEST);
        }
    }
}
