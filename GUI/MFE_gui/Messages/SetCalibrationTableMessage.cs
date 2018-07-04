using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class SetCalibrationTableMessage : MfeMessage
    {
        public CalibrationTable table;
        public CalibrationTable.TableType tableType;
        public SetCalibrationTableMessage(CalibrationTable table, CalibrationTable.TableType tableType) : base(OPCODE.SET_CALIBRATION_TABLE)
        {
            this.table = table;
            this.tableType = tableType;
        }

        public override byte[] parametersToByteArr()
        {
            byte[] t = table.asByteArray();
            byte[] b = new byte[1 + t.Length];
            b[0] = (byte)(tableType);
            Array.Copy(t, 0, b, 1, t.Length);
            return b;
        }
    }
}
