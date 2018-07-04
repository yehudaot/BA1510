using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mfe_gui
{
    public class GeneralCalibrationTable : CalibrationTable
    {
        public UInt16 CAL_TBL_VERSION;
        public UInt16 SERIAL_NUM;
        public UInt16 TEMP_MULT;
        public UInt16 FWD_MULT;
        public UInt16 REV_MULT;
        public UInt16 INP_PWR_MULT;
        public UInt16 PWR_CURRENT_MULT;
        public UInt16 PRE_AMP_MULT;
        public UInt16 ISENSE_PA1_MULT;
        public UInt16 ISENSE_PA2_MULT;
        public UInt16 TX_ON_TIMING_USEC;
        public UInt16 TX_OFF_TIMING_USEC;
        public UInt16 PA_ON_TIMING_USEC;
        public UInt16 PA_OFF_TIMING_USEC;
        public UInt16 ANT_SEL_TIMING_USEC;
        public UInt16 FWD_SAMP_TIMING_USEC;
        public UInt16 REV_SAMP_TIMING_USEC;
        public UInt16 INP_PWR_SAMP_TIMING_USEC;

        public override byte[] asByteArray()
        {
            byte[] b = new byte[TABLE_SIZE_BYTES];
            BitConverter.GetBytes(CAL_TBL_VERSION).CopyTo(b, 0);
            BitConverter.GetBytes(SERIAL_NUM).CopyTo(b, 2);
            BitConverter.GetBytes(TEMP_MULT).CopyTo(b, 4);
            BitConverter.GetBytes(FWD_MULT).CopyTo(b, 6);
            BitConverter.GetBytes(REV_MULT).CopyTo(b, 8);
            BitConverter.GetBytes(INP_PWR_MULT).CopyTo(b, 10);
            BitConverter.GetBytes(PWR_CURRENT_MULT).CopyTo(b, 12);
            BitConverter.GetBytes(PRE_AMP_MULT).CopyTo(b, 14);
            BitConverter.GetBytes(ISENSE_PA1_MULT).CopyTo(b, 16);
            BitConverter.GetBytes(ISENSE_PA2_MULT).CopyTo(b, 18);
            BitConverter.GetBytes(TX_ON_TIMING_USEC).CopyTo(b, 20);
            BitConverter.GetBytes(TX_OFF_TIMING_USEC).CopyTo(b, 22);
            BitConverter.GetBytes(PA_ON_TIMING_USEC).CopyTo(b, 24);
            BitConverter.GetBytes(PA_OFF_TIMING_USEC).CopyTo(b, 26);
            BitConverter.GetBytes(ANT_SEL_TIMING_USEC).CopyTo(b, 28);
            BitConverter.GetBytes(FWD_SAMP_TIMING_USEC).CopyTo(b, 30);
            BitConverter.GetBytes(REV_SAMP_TIMING_USEC).CopyTo(b, 32);
            BitConverter.GetBytes(INP_PWR_SAMP_TIMING_USEC).CopyTo(b, 34);
            return b;
        }

        public override void fromByteArray(byte [] rawData)
        {
            CAL_TBL_VERSION = BitConverter.ToUInt16(rawData, 0);
            SERIAL_NUM = BitConverter.ToUInt16(rawData, 2);
            TEMP_MULT = BitConverter.ToUInt16(rawData, 4);
            FWD_MULT = BitConverter.ToUInt16(rawData, 6);
            REV_MULT = BitConverter.ToUInt16(rawData, 8);
            INP_PWR_MULT = BitConverter.ToUInt16(rawData, 10);
            PWR_CURRENT_MULT = BitConverter.ToUInt16(rawData, 12);
            PRE_AMP_MULT = BitConverter.ToUInt16(rawData, 14);
            ISENSE_PA1_MULT = BitConverter.ToUInt16(rawData, 16);
            ISENSE_PA2_MULT = BitConverter.ToUInt16(rawData, 18);
            TX_ON_TIMING_USEC = BitConverter.ToUInt16(rawData, 20);
            TX_OFF_TIMING_USEC = BitConverter.ToUInt16(rawData, 22);
            PA_ON_TIMING_USEC = BitConverter.ToUInt16(rawData, 24);
            PA_OFF_TIMING_USEC = BitConverter.ToUInt16(rawData, 26);
            ANT_SEL_TIMING_USEC = BitConverter.ToUInt16(rawData, 28);
            FWD_SAMP_TIMING_USEC = BitConverter.ToUInt16(rawData, 30);
            REV_SAMP_TIMING_USEC = BitConverter.ToUInt16(rawData, 32);
            INP_PWR_SAMP_TIMING_USEC = BitConverter.ToUInt16(rawData, 34);
        }
    }
}
