using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class FinishUpdateMessage : MfeMessage
    {
        public FinishUpdateMessage() : base(OPCODE.FINISH_UPDATE_PROCESS)
        {

        }

        public override byte[] parametersToByteArr()
        {
            return new byte[] { };
        }
    }
}
