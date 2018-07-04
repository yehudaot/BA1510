using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class ChangeModeMessage : MfeMessage
    {
        public enum Mode
        {
            Operational = 0,
            Technician = 1,
            Maintanance = 2,
        }

        Mode mode;
        public ChangeModeMessage(Mode mode) : base(OPCODE.CHANGE_MODE)
        {
            this.mode = mode;
        }

        public override byte[] parametersToByteArr()
        {
            return new byte[] { Convert.ToByte(mode)};
        }
    }
}
