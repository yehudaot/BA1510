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
    public class GetVersionResponse : MfeResponse
    {
        public byte day;
        public byte month;
        public UInt16 year;
        public UInt16 version;
        public UInt16 serailNumber;

        public GetVersionResponse() : base()
        {
            
        }

        public override object getUniqueIdentifier()
        {
            return new OPCODE(OPCODE.VERSION);
        }

        public override void parametesFromData()
        {
            day = rawData[0];
            month = rawData[1];
            year = BitConverter.ToUInt16(rawData, 2);
            version = BitConverter.ToUInt16(rawData, 4);
            serailNumber = BitConverter.ToUInt16(rawData, 6);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Version: {0}.{1}\n", (version & 0xff).ToString("X"), ((version >> 8) & 0xff).ToString("X"));
            sb.AppendFormat("Date: {0}/{1}/{2}\n", day, month, year);
            sb.AppendFormat("Serial: {0}\n", serailNumber.ToString("X"));
            return sb.ToString();
        }

    }
}
