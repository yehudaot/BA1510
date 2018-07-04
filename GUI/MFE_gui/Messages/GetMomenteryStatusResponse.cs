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
    public class GetMomenteryStatusResponse : MfeResponse
    {
        public UInt16 ttiCounter;
        public UInt16 temperature;
        public UInt16 powerAmplifierCurrent;
        public Byte powerAmplifierGain;
        public ChangeModeMessage.Mode mode;
        public ControlMessage.TxAntenna antennaState;
        public ControlMessage.Frequency frequency;

        public GetMomenteryStatusResponse() : base()
        {
            
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.MOMENTERY_STATUS);
        }

        public override void parametesFromData()
        {
            ttiCounter = BitConverter.ToUInt16(rawData, 0);
            temperature = BitConverter.ToUInt16(rawData, 2);
            powerAmplifierCurrent = BitConverter.ToUInt16(rawData, 4);
            powerAmplifierGain = rawData[6];
            mode = (ChangeModeMessage.Mode)rawData[7];
            antennaState = (ControlMessage.TxAntenna)rawData[8];
            frequency = (ControlMessage.Frequency)rawData[9];
        }

    }
}
