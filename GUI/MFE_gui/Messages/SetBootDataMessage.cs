using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class SetBootDataMessage : MfeMessage
    {
        public const int DATA_SIZE_BYTES = 64;

        public UInt32 address;
        public Byte[] lineData = new Byte[DATA_SIZE_BYTES];
        

        public SetBootDataMessage(UInt32 address, byte [] lineData) : base(OPCODE.SET_DATA_LINE)
        {
            this.address = address;
            this.lineData = new byte[lineData.Length];
            lineData.CopyTo(this.lineData, 0);
            this.dataLen = sizeof(UInt32) + lineData.Length;
        }

        public override byte[] parametersToByteArr()
        {
            byte [] b = new byte[dataLen];
            BitConverter.GetBytes(address).CopyTo(b, 0);
            Array.Copy(lineData, 0, b, 4, lineData.Length);
            return b;
        }
    }
}
