using System;
using System.IO;
using System.IO.Ports;

using atpLib.Messages;

namespace atpLib.Devices
{
    public abstract class SerialDevice : Device
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        const int BAUD_RATE = 115200;
        const Parity PARITY_BITS = Parity.None;
        const int DATA_BITS = 8;
        const StopBits STOP_BITS = StopBits.One;
        
        public SerialPort port = null;

        public SerialDevice(string portName)
        {
            port = new SerialPort(portName, BAUD_RATE, PARITY_BITS, DATA_BITS, STOP_BITS);
        }

        public SerialDevice(string portName, int baudRate, Parity parity)
        {
            port = new SerialPort(portName, baudRate, parity, DATA_BITS, STOP_BITS);
        }

        public string getName()
        {
            return (port != null) ? port.PortName : "";
        }

        #region Inhereted Methods
        public override bool connect()
        {
            try
            {
                port.Open();
                return true;
            }
            catch (Exception e) 
            {
                log.Error("error oppening port \"" + getName() + "\"", e);
                if (e is UnauthorizedAccessException || e is ArgumentOutOfRangeException ||
                    e is ArgumentException || e is IOException || e is InvalidOperationException)
                {
                    return false;
                }
                throw e;
            }
        }

        public override void disconnect()
        {
            if (port != null && port.IsOpen)
            {
                port.Close();
            }
        }

        public override bool isConnected()
        {
            if (port != null && port.IsOpen)
            {
                return true;
            }
            return false;
        }

        public void flushRx()
        {
            port.DiscardInBuffer();
        }
        #endregion
    }
}
