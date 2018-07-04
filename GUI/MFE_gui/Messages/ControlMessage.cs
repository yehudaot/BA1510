using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class ControlMessage : MfeMessage
    {
        public enum TxAntenna
        {
            ANT0, 
            ANT1
        }

        public enum Frequency
        {
            LOW,
            HIGH
        }

        public bool txOn;
        public int paGain;
        public TxAntenna txAnt;
        public Frequency frequency;
        public bool reset;
        public UInt16 Identifier;
        public ControlMessage(bool txOn, int paGain, TxAntenna txAnt, Frequency mode, bool reset, UInt16 Identifier) : base(OPCODE.CONTROL)
        {
            this.txOn = txOn;
            this.paGain = paGain;
            this.txAnt = txAnt;
            this.frequency = mode;
            this.reset = reset;
            this.Identifier = Identifier;
        }

        public override byte[] parametersToByteArr()
        {
            byte [] b = new byte[3];
            b[0] = 0;
            b[0] += (byte)(txOn ? 1 : 0);
            b[0] += (byte)(paGain << 1);
            b[0] += (byte)(txAnt == TxAntenna.ANT1 ? 1 << 4 : 0);
            b[0] += (byte)(frequency == Frequency.HIGH ? 1 << 5 : 0);
            b[0] += (byte)(reset ? 1 << 6 : 0);
            int p = 0;
            for(int i=0; i<7; i++)
            {
                p += (((b[0] & (byte)(1 << i)) > 0) ? 1 : 0);
            }
            b[0] += (byte)((p > 0) ? 1 << 7 : 0);
            Array.Copy(BitConverter.GetBytes(Identifier), 0, b, 1, 2);
            return b;
        }
    }
}
