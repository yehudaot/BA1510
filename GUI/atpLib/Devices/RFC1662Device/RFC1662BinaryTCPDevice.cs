using System;
using System.IO;
using System.IO.Ports;
using atpLib.Messages;
using atpLib.Devices;
using System.Collections;
using System.Threading;
using atpLib.Messagess;
using atpLib.CRC;
using atpLib.Devices.RFC1662Device;

namespace atpLib.Devices
{
    public class RFC1662BinaryTCPDevice : RFC1662BinaryDevice<BinaryTCPDevice>
    {
        public RFC1662BinaryTCPDevice(BinaryTCPDevice device, ICRC crc) : base(device, crc)
        {
        }

        
        public override IResponse receiveAnswer() { throw new NotImplementedException(); }
        
        public override IResponse receiveAnswer(CancellationToken ct)
        {
            Queue dataQ = new Queue();
            Byte [] b = new byte[1];
           // log.Info("recieving an answer...");
            int n = 0;
            bool fullMessage = false;
            device.socket.ReceiveTimeout = 1000;
            
            while (!fullMessage)
            {
                /* check the cancellation token */
                ct.ThrowIfCancellationRequested();

                try
                {
                    n = device.socket.Receive(b, 1, System.Net.Sockets.SocketFlags.None);
                    //n = port.Read(b, 0, 1);
                } catch(TimeoutException)
                {
                    /* this is ok, continue to next iteration */
                    continue;
                }
                                
                if(n > 0)
                {
                    if(b[0] == CHAR_FLAG)
                    {
                        if(dataQ.Count > 1) /* op + crc */
                        {
                            fullMessage = true;
                        } else
                        {
                            dataQ.Clear();
                        }
                    } else
                    {
                        dataQ.Enqueue(b[0]);
                    }
                }
            }

            byte[] data = new byte[dataQ.Count];
            Array.Copy(dataQ.ToArray(), data, dataQ.Count);
          //  log.Info("got: " + BitConverter.ToString(data).Replace("-", " "));

            byte[] strippedData = restore_received_data(data);
            if (strippedData == null)
            {
                log.Error("error in recieved bitstream!");
                throw new RFC1662Exception();
            }

            BinaryResponse.OP op = (BinaryResponse.OP)strippedData[0];
            BinaryResponse answer = BinaryResponse.newDeviceAnswer(op);
            if(answer == null)
            {
                throw new UnknownOPException();
            }
            log.Info("got answer of type: " + op.ToString("X"));
            
            answer.fromByteArray(strippedData);
           
            return answer;
        }

        public override IResponse sendRecieve(IMessage message)
        {
            throw new NotImplementedException();
        }

        public override void sendMsg(IMessage message)
        {
            if (device.socket == null || !device.socket.Connected)
            {
                throw new DeviceNotConnectedException("tried to send data while the device is not connected!");
            }

            BinaryMessage m = message as BinaryMessage;

            log.Info("sending " + m.opcode.ToString());
            
            byte [] src = m.asByteArray();
            byte [] dest = prepare_data_to_send(src);
            //    log.Info("array is: " + BitConverter.ToString(dest).Replace("-", " "));
            /* flush input data */
            device.socket.Send(dest);
        }

        public override bool isAlive()
        {
            return isConnected();
        }
    }
}
