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
    public class GetCalibrationTableResponse : MfeResponse
    {
        public CalibrationTable table;
        public GetCalibrationTableResponse() : base()
        {
            
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.GET_CALIBRATION_TABLE);
        }

        public override void parametesFromData()
        {
           switch(rawData[0])
            {
                case (byte)CalibrationTable.TableType.General:
                    {
                        table = new GeneralCalibrationTable();
                        table.fromByteArray(rawData.Skip(1).ToArray());
                        break;
                    }
                case (byte)CalibrationTable.TableType.Low:
                case (byte)CalibrationTable.TableType.High:
                    {
                        table = new PowerCalibrationTable();
                        table.fromByteArray(rawData.Skip(1).ToArray());
                        break;
                    }
                default:
                    {
                        throw new FormatException("error in table type!");
                    }
            }
        }

    }
}
