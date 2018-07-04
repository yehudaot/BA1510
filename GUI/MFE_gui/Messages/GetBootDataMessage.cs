using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class GetBootDataMessage : MfeMessage
    {
        public UInt32 address;
        public GetBootDataMessage(UInt32 address) : base(OPCODE.GET_DATA_LINE)
        {
            this.address = address;
            this.dataLen = sizeof(UInt32);
        }

        public override byte[] parametersToByteArr()
        {
            byte [] b = new byte[sizeof(UInt32)];
            BitConverter.GetBytes(address).CopyTo(b, 0);
            return b;
        }
    }
}
