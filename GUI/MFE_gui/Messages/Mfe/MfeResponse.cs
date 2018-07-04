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
    /// This is a per project decleration of the different opcodes supported by the undelying device.
    /// </summary>
    public abstract class MfeResponse : BinaryResponse
    {

        public class OPCODE : BinaryResponse.OP
        {
            public static int ACK                   = 0x80;
            public static int RAW_STATUS            = 0x81;
            public static int VERSION               = 0x82;
            public static int GET_CALIBRATION_TABLE = 0x85;
            public static int SET_DATA_LINE         = 0x86;
            public static int GET_DATA_LINE         = 0x87;
            public static int MOMENTERY_STATUS      = 0x89;

            public OPCODE(int value) : base(value)
            {

            }
        }

        public MfeResponse() : base()
        {

        }

        public override void fromByteArray(Byte[] arr)
        {
            opcode = (OP)arr[0];
            /* strip opcode and crc */
            rawData = new byte[arr.Length - 2];
            Array.Copy(arr, 1, rawData, 0, arr.Length - 2);
            parametesFromData();
        }
    }
}
