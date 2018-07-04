using atpLib.Devices;
using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static mfe_gui.Messages.ControlMessage;
using static mfe_gui.Messages.MfeResponse;

namespace mfe_gui.Messages
{
    public class GetMomenteryStatusResponse : MfeResponse
    {
        public UInt16 ttiCounter;
        public byte mode;
        public bool txOn;
        public int paGain;
        public TxAntenna txAnt;
        public Frequency frequency;
        public UInt16 preAmpPower1;
        public UInt16 preAmpPower2;
        public UInt16 preAmpPower3;
        public UInt16 preAmpPower4;
        public UInt16 reversePower1;
        public UInt16 reversePower2;
        public UInt16 reversePower3;
        public UInt16 reversePower4;
         public bool DontUpdate;

        public GetMomenteryStatusResponse() : base()
        {
            
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.MOMENTERY_STATUS);
        }

        public override void parametesFromData()
        {
            int bits = (int)rawData[0];
            txOn = ((bits & 0) != 0) ? true : false;
            paGain = (bits & 3 << 1) >> 1;
            txAnt = ((bits & 1 << 4) != 0) ? TxAntenna.ANT1 : TxAntenna.ANT0;
            frequency = ((bits & 1 << 5) != 0) ? Frequency.HIGH : Frequency.LOW;
            DontUpdate = ((bits & 1 << 7) != 0) ? true : false;
            
            ttiCounter = BitConverter.ToUInt16(rawData, 1);
            mode = rawData[3];
            preAmpPower1 = BitConverter.ToUInt16(rawData, 4);
            preAmpPower2 = BitConverter.ToUInt16(rawData, 6);
            preAmpPower3 = BitConverter.ToUInt16(rawData, 8);
            preAmpPower4 = BitConverter.ToUInt16(rawData, 10);

            reversePower1 = BitConverter.ToUInt16(rawData, 12);
            reversePower2 = BitConverter.ToUInt16(rawData, 14);
            reversePower3 = BitConverter.ToUInt16(rawData, 16);
            reversePower4 = BitConverter.ToUInt16(rawData, 18);
         }

    }
}
