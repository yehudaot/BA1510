using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atpLib.Messages;
using atpLib.Infra;

namespace mfe_gui.Messages
{
    /// <summary>
    /// This is a per project decleration of the different opcodes supported by the underlying device.
    /// </summary>
    public abstract class MfeMessage : BinaryMessage
    {
        public class OPCODE : BinaryMessage.OP
        {
            public static int CONTROL               = 0;
            public static int RAW_STATUS_REQ        = 1;
            public static int VERSION_REQUEST       = 2;
            public static int CHANGE_MODE           = 3;
            public static int SET_CALIBRATION_TABLE = 4;
            public static int GET_CALIBRATION_TABLE = 5;
            public static int SET_DATA_LINE         = 6;
            public static int GET_DATA_LINE         = 7;
            public static int FINISH_UPDATE_PROCESS = 8;
            public static int MOMENTERY_STATUS_REQ  = 9;

            public OPCODE(int value) : base(value) { }
        }

        public MfeMessage(OP opcode) : base(opcode) { }

        public override Byte[] asByteArray()
        {
            Byte[] bparams = parametersToByteArr();
            Byte[] b = new byte[sizeof(byte) + bparams.Length];
            b[0] = (byte)opcode;
            Array.Copy(bparams, 0, b, sizeof(byte), bparams.Length);
            return b;
        }
    }
}
