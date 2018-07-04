using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using atpLib.Messages;

namespace atpLib.Devices
{
    public class AsciiSerialDevice : SerialDevice
    {
        const char PROMPT = '%';
        public const int READ_TIMEOUT = 10000;

        public AsciiSerialDevice(string portName) : base (portName) { setTimeout(); }
        public AsciiSerialDevice(string portName, int baudRate, Parity parity) : base(portName, baudRate, parity) { setTimeout(); }

        private void setTimeout()
        {
            port.ReadTimeout = READ_TIMEOUT;
        }

        #region Inhereted Methods
        public override void sendMsg(IMessage message)
        {
            if (port == null || !port.IsOpen)
            {
                throw new DeviceNotConnectedException("tried to send data while the device is not connected!");
            }

            AsciiMessage m = message as AsciiMessage;
            log.Info("sending " + m.ToString());
            try
            {
                port.Write(m.asCharArray(), 0, m.asCharArray().Length);
            } catch(Exception e) {
                log.Error("error while reading data from port: \"" + getName() + "\"");
                throw e;
            }
        }

        public override IResponse receiveAnswer()
        {
            string rawLine = "";
            bool foundPrompt = false;
            try
            {
                while (!foundPrompt)
                {
                    string line = "";
                    while (!line.Contains("\n") || !line.Contains(PROMPT))
                    {
                        line += (char)port.ReadChar();
                    }
                    if (line.Contains(PROMPT))
                    {
                        /* end of answer */
                        rawLine += line.Split(PROMPT)[0];
                        foundPrompt = true;
                        break;
                    }
                    rawLine += line;
                }
            } catch(Exception e)
            {
                if(e is InvalidOperationException)
                {
                    log.Error("error while reading data from port: \"" + getName() + "\"");
                } else if(e is TimeoutException) {
                    log.Error("timeout while reading data from port: \"" + getName() + "\"");
                }
                throw e;
            }

            LinkedList<String> lines = new LinkedList<string>();
            foreach (string s in rawLine.Split('\n'))
            {
                lines.AddLast(s.Trim());
            }
            AsciiResponse aResp = AsciiResponse.newAsciiResponse(lines.First.Value.Split(' ')[0]);
            aResp.parseRawResponse(lines);
            return aResp;
        }

        public override IResponse sendRecieve(IMessage message)
        {
            throw new NotImplementedException();
        }

        public override bool isAlive()
        {
            return isConnected();
        }
        #endregion


    }
}
