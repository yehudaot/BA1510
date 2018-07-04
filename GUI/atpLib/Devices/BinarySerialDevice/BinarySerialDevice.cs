using System;
using System.IO;
using System.IO.Ports;
using atpLib.Messages;

namespace atpLib.Devices
{
    public class BinarySerialDevice : SerialDevice
    {
        public BinarySerialDevice(string portName) : base (portName) {}
        public BinarySerialDevice(string portName, int baudRate, Parity parity) : base(portName, baudRate, parity) { }

        public override IResponse receiveAnswer()
        {
            byte[] header = new byte[BinaryResponse.HEADER_LENTGH];
            log.Info("recieving an answer...");
            int n = port.Read(header, 0, BinaryResponse.HEADER_LENTGH);
            if (n != BinaryResponse.HEADER_LENTGH)
            {
                // maybe wait for a while?
                log.Info("error reading header from device!");
                throw new InvalidDataException();
            }

            BinaryResponse.OP op = (BinaryResponse.OP)BitConverter.ToInt32(header, 0);
            UInt32 dataLen = BitConverter.ToUInt32(header, 4);
            BinaryResponse answer = BinaryResponse.newDeviceAnswer(op);

            log.Info("got answer of type: " + op.ToString());
            if (dataLen != 0)
            {
                byte[] data = new byte[dataLen];
                n = port.Read(data, 0, (int)dataLen);
                if (n != dataLen)
                {
                    log.Info("could not read all the data, maybe add a loop?!");
                    throw new NotImplementedException();
                }
                answer.fromByteArray(header, data);
            }
            return answer;
        }

        public override IResponse sendRecieve(Messages.IMessage message)
        {
            throw new NotImplementedException();
        }

        public override void sendMsg(IMessage message)
        {
            if (port == null || !port.IsOpen)
            {
                throw new DeviceNotConnectedException("tried to send data while the device is not connected!");
            }

            BinaryMessage m = message as BinaryMessage;

            log.Info("sending " + m.opcode.ToString());
            port.Write(m.asByteArray(), 0, m.asByteArray().Length); 
        }

        public override bool isAlive()
        {
            return isConnected();
        }
    }
}
