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
    public class GetBootDataResponse : MfeResponse
    {
        public UInt32 address;
        public byte[] lineData;

        public GetBootDataResponse() : base()
        {
            
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.GET_DATA_LINE);
        }

        public override void parametesFromData()
        {
            address = BitConverter.ToUInt32(rawData, 0);
            lineData = new byte[rawData.Length - sizeof(UInt32)];
            Array.Copy(rawData, 4, lineData, 0, lineData.Length);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Address: {0}\n", address);
            foreach (byte b in lineData)
            {
                sb.AppendFormat("{0}", b.ToString("X"));
            }
            sb.AppendFormat("\n");
            return sb.ToString();
        }

    }
}
