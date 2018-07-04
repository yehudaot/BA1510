using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atpLib.Messages;

namespace atpLib.Devices
{
    public abstract class PseudoDevice : Device
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string deviceName;
        bool connected = false;

        public PseudoDevice(string deviceName)
        {
            this.deviceName = deviceName;
        }
        public override bool connect()
        {
            log.Info("Connected to pseudo device: " + deviceName);
            connected = true;
            return true;
        }
        public override void disconnect()
        {
            log.Info("Disconnected from pseudo device: " + deviceName);
            connected = false;
        }
        public override bool isAlive()
        {
            return isConnected();
        }
        public override bool isConnected()
        {
            return connected;
        }
    }
}
