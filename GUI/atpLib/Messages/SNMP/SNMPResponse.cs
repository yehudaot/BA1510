using Lextm.SharpSnmpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atpLib.Messages.SNMP
{
    public class SNMPResponse : IResponse
    {
        List<Variable> response;

        public SNMPResponse(IList<Variable> Variables)
        {
            foreach (Variable v in Variables)
            {
                response.Add(v);
            }
        }

        public virtual object getUniqueIdentifier()
        {
            throw new NotImplementedException();
        }
    }
}
