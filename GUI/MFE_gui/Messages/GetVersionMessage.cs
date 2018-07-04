using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class GetVersionMessage : MfeMessage
    {
        public GetVersionMessage() : base(OPCODE.VERSION_REQUEST)
        {

        }

        public override byte[] parametersToByteArr()
        {
            return new byte[] { };
        }
    }
}
