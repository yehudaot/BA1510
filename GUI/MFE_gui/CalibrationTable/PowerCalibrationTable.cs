using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui
{
    public class PowerCalibrationTable : CalibrationTable
    {
        public UInt16[] PA_GAIN_VALUES = new UInt16[TABLE_SIZE_BYTES / 2];

        public override byte[] asByteArray()
        {
            byte[] b = new byte[TABLE_SIZE_BYTES];
            for(int i=0; i< TABLE_SIZE_BYTES; i+=2)
            {
                BitConverter.GetBytes(PA_GAIN_VALUES[i/2]).CopyTo(b, i);
            }
            return b;
        }

        public override void fromByteArray(byte[] rawData)
        {
            for (int i = 0; i < TABLE_SIZE_BYTES; i += 2)
            {
                PA_GAIN_VALUES[i / 2] = BitConverter.ToUInt16(rawData, i);
            }


         
        }
    }
}
