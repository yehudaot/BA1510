using atpLib.Devices;
using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class AckResponse : MfeResponse
    {
     

        public AckResponse() : base()
        {
            
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.ACK);
        }

        public override void parametesFromData()
        {
            
        }

    }
}
