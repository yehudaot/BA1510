using System;
using System.IO.Ports;

using atpLib.Messages.zUp;

namespace atpLib.Devices
{
    public class zUpDevice : SerialDevice
    {
        int address {get; set;}
        public zUpDevice(int address, string portName, int baudRate, Parity parity) : base(portName, baudRate, parity) 
        {
            this.address = address;
        }

        public zUpDevice(int address, string portName) : base(portName, 9600, Parity.None)
        {
            this.address = address;
        }

        public override bool connect()
        {
            log.Info("Connecting to zUP device");
            bool ret = base.connect();
            if (ret && port.IsOpen)
            {
                port.NewLine = "\r\n";
                /* send the ADDR message */
                string cmd = ":ADR" + address.ToString("00") + ";";
                //foreach (char c in cmd.ToCharArray()) {
                //    port.Write(c.ToString());
                //    System.Threading.Thread.Sleep(1);
                //}
                //port.Write(cmd);
                port.Write(";;");
            }
            else
            {
                log.Info("Error opening the com port");
            }
            return ret;
        }

        public override void sendMsg(Messages.IMessage message)
        {
            if (port == null || !port.IsOpen)
            {
                throw new DeviceNotConnectedException("tried to send data while the device is not connected!");
            }

            zUpMessage m = message as zUpMessage;
            log.Info("sending " + m.ToString());

            foreach (char c in m.asCharArray())
            {
                port.Write(c.ToString());
                System.Threading.Thread.Sleep(100);
            }

            //port.Write(m.asCharArray(), 0, m.asCharArray().Length);
            System.Threading.Thread.Sleep(100);
        }

        public override Messages.IResponse receiveAnswer()
        {
            //string line = port.ReadExisting();
            string line = port.ReadLine();

            zUpResponse aResp = zUpResponse.newResponse(line);
            aResp.parseRawResponse(line);
            return aResp;
        }

        public override Messages.IResponse sendRecieve(Messages.IMessage message)
        {
            throw new NotImplementedException();
        }

        public override bool isAlive()
        {
            try
            {
                if(!isConnected())
                {
                    return false;
                }
                sendMsg(new zUPSetAddressMessage());
                sendMsg(new zUPGetModelMessage());
                zUpGetModelResponse m = (zUpGetModelResponse)receiveAnswer();
                log.Info("zUP device is alive with model# " + m.model);
                return true;
            }
            catch(Exception ex)
            {
                log.Error("Error while checking if zUP is alive", ex);
                return false;
            }
        }
    }
}
