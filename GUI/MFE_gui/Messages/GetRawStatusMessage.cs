using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class GetRawStatusMessage : MfeMessage
    {
        public GetRawStatusMessage() : base(OPCODE.RAW_STATUS_REQ)
        {

        }

        public override byte[] parametersToByteArr()
        {
            return new byte[] { };
        }
    }
}
