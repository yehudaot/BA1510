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
    public class GetRawStatusResponse : MfeResponse
    {
        
        public UInt16 ttiCounter;
        public UInt16 Identifier;
        public UInt16 [] fwdPower;
        public UInt16 [] inputPower;
        public UInt16 temperature;
        public UInt16 powerAmplifierCurrent;
        public bool txOn;
        public int paGain;
        public TxAntenna txAnt;
        public Frequency frequency;
        public int reversePowerStatus;
        public bool DontUpdate;

        public GetRawStatusResponse() : base()
        {
            fwdPower = new UInt16[4];
            inputPower = new UInt16[4];
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.RAW_STATUS);
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
            Identifier = BitConverter.ToUInt16(rawData, 3);
            for (int i = 0; i < 4; i++)
            {
                fwdPower[i] = BitConverter.ToUInt16(rawData, 5 + i * 2);
                inputPower[i] = BitConverter.ToUInt16(rawData, 13 + i * 2);
            }
            temperature = BitConverter.ToUInt16(rawData, 21);
            powerAmplifierCurrent = BitConverter.ToUInt16(rawData, 23);
            reversePowerStatus = rawData[25];
        }

    }
}
