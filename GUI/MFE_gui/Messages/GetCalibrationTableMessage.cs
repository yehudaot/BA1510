using atpLib.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui.Messages
{
    public class GetCalibrationTableMessage : MfeMessage
    {
        public CalibrationTable.TableType tableType;
        public GetCalibrationTableMessage(CalibrationTable.TableType tableType) : base(OPCODE.GET_CALIBRATION_TABLE)
        {
            this.tableType = tableType;
        }

        public override byte[] parametersToByteArr()
        {
            return new byte[] { Convert.ToByte(tableType) };
        }
    }
}
