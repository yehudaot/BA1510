using atpLib.Devices;
using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static mfe_gui.Messages.MfeResponse;

namespace mfe_gui.Messages
{
    public class SetBootDataResponse : MfeResponse
    {
        public enum Status {
            OK = 0,
            ERR = 1,
        }

        public UInt32 address;
        public Status status;

        public SetBootDataResponse() : base()
        {
            
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.SET_DATA_LINE);
        }

        public override void parametesFromData()
        {
            address = BitConverter.ToUInt32(rawData, 0);
            status = (Status)rawData[sizeof(UInt32)];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Address: {0}\n", address);
            sb.AppendFormat("\n");
            return sb.ToString();
        }

    }
}
