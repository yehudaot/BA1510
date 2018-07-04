using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using atpLib.Messages;

namespace atpLib.Devices
{
    public class BinaryTCPDevice : TCPDevice
    {
        public BinaryTCPDevice(string remoteIPAddress) : base(remoteIPAddress) { }
        public BinaryTCPDevice(string remoteIPAddress, int remotePort) : base(remoteIPAddress, remotePort) { }
        public override bool isAlive()
        {
            return isConnected();
        }

        public override IResponse receiveAnswer()
        {
            try
            {
                byte[] header = new byte[BinaryResponse.HEADER_LENTGH];
                log.Info("recieving an answer...");
                int n = socket.Receive(header, BinaryResponse.HEADER_LENTGH, System.Net.Sockets.SocketFlags.None);
                if (n != BinaryResponse.HEADER_LENTGH)
                {
                    // maybe wait for a while?
                    log.Error("error reading header from device!");
                    throw new InvalidDataException();
                }

                BinaryResponse.OP op = (BinaryResponse.OP)BitConverter.ToInt32(header, 0);
                UInt32 dataLen = BitConverter.ToUInt32(header, 4);
                BinaryResponse answer = BinaryResponse.newDeviceAnswer(op);

                if(answer == null)
                {
                    throw new ArgumentNullException("error in response parsing, is the OPCODE missing? {" + op.ToString() + "}");
                }

                log.Info("got answer of type: " + op.ToString());
                n = 0;
                byte[] data = new byte[dataLen];
                log.Info("data len: " + dataLen.ToString());
                while (dataLen - n > 0)
                {
                    n += socket.Receive(data, n, (int)(dataLen - n), System.Net.Sockets.SocketFlags.Partial);
                    log.Info("read " + n.ToString());
                }
                answer.fromByteArray(header, data);
                return answer;
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public override void sendMsg(IMessage message)
        {
            if (socket == null || !socket.Connected)
            {
                throw new DeviceNotConnectedException("tried to send data while the device is not connected!");
            }

            BinaryMessage m = message as BinaryMessage;

            log.Info("sending " + m.opcode.ToString());
            socket.Send(m.asByteArray());
        }

        public override IResponse sendRecieve(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
