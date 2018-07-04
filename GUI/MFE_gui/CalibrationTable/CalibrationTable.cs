using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui
{
    public abstract class CalibrationTable
    {
        public enum TableType
        {
            General = 0,
            Low = 1,
            High = 2,
        }
        public const int TABLE_SIZE_BYTES = 64;
        public abstract byte[] asByteArray();
        public abstract void fromByteArray(byte[] rawData);
    }
}
