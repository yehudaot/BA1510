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
    public class GetRawStatusResponse : MfeResponse
    {
        public UInt16 ttiCounter;
        public UInt16 Identifier;
        public UInt16 [] fwdPower;
        public UInt16 [] reversePower;
        public UInt16 [] inputPower;
        public UInt16 [] preAmpPower;

        public GetRawStatusResponse() : base()
        {
            fwdPower = new UInt16[4];
            reversePower = new UInt16[4];
            inputPower = new UInt16[4];
            preAmpPower = new UInt16[4];
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.RAW_STATUS);
        }

        public override void parametesFromData()
        {
            ttiCounter = BitConverter.ToUInt16(rawData, 0);
            Identifier = BitConverter.ToUInt16(rawData, 2);
            for(int i=0; i<4; i++)
            {
                fwdPower[i] = BitConverter.ToUInt16(rawData, 4 + i * 2);
                reversePower[i] = BitConverter.ToUInt16(rawData, 12 + i * 2);
                inputPower[i] = BitConverter.ToUInt16(rawData, 20 + i * 2);
                preAmpPower[i] = BitConverter.ToUInt16(rawData, 28 + i * 2);
            }

        }

    }
}
