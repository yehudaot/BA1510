using System;
using System.IO;
using System.IO.Ports;
using atpLib.Messages;
using atpLib.Devices;
using mfe_gui.CRC;
using System.Collections;
using System.Threading;
using atpLib.Messagess;

namespace mfe_gui.Devices
{
    public class RFC1662Exception : Exception
    {

    }
    class RFC1662BinarySerialDevice : SerialDevice
    {
        const int CHAR_FLAG = 0x7E;
        const int CHAR_FLAG_XORED = 0x5E;
        const int CHAR_ESCAPE = 0x7D;
        const int CHAR_ESCAPE_XORED = 0x5D;

        ICRC crc;

        public RFC1662BinarySerialDevice(string portName, ICRC crc) : base(portName) { this.crc = crc; }
        public RFC1662BinarySerialDevice(string portName, ICRC crc, int baudRate, Parity parity) : base(portName, baudRate, parity) { this.crc = crc; }

        byte [] prepare_data_to_send(byte[] data)
        {
            int data_replaced_lentgh = 0;
            byte[] dataWithFcs = new byte[data.Length + crc.crcLength()];
            byte[] buff = new byte[dataWithFcs.Length * 2];
            byte[] dest;

            Array.Copy(data, dataWithFcs, data.Length);
            //calc crc
            crc.attachEnd(dataWithFcs, data.Length);
            //replace all escape chars and flag chars if any
            data_replaced_lentgh = replaceFlagAndEscape(dataWithFcs, buff, dataWithFcs.Length);
            //add flagging
            buff[0] = 0x7e;
            buff[data_replaced_lentgh] = 0x7e;
            data_replaced_lentgh++; /* add enough space for the trailing flag */
            dest = new byte[data_replaced_lentgh];
            Array.Copy(buff, dest, data_replaced_lentgh);
            return dest;
        }

        byte[] restore_received_data(byte[] data)
        {
            int restored_data_size = 0;

            //the received data is received without the start and end FLAGS
            restored_data_size = restoreFlagAndEscape(data, data, data.Length);
            //calc fcs
            if (crc.check(data, restored_data_size))
            {
                byte[] dest = new byte[restored_data_size];
                Array.Copy(data, dest, restored_data_size);
                return dest;
            }
            else
            {
                return null;
            }
        }

        private int replaceFlagAndEscape(byte[] data, byte[] dest, int datalen)
        {
            int datai = 0;
            int desti = 1;
            for (datai = 0; datai < datalen; datai++)
            {
                if (data[datai] == CHAR_FLAG)
                {
                    dest[desti++] = CHAR_ESCAPE;
                    dest[desti++] = CHAR_FLAG_XORED;
                }
                else if (data[datai] == CHAR_ESCAPE)
                {
                    dest[desti++] = CHAR_ESCAPE;
                    dest[desti++] = CHAR_ESCAPE_XORED;
                }
                else
                {
                    dest[desti++] = data[datai];
                }
            }
            return desti; //return the size of the new data string
        }

        private int restoreFlagAndEscape(byte[] data, byte[] dest, int datalen)
        {
            int datai = 0;
            int desti = 0;
            for (datai = 0; datai < datalen; datai++)
            {
                if (data[datai] == CHAR_ESCAPE)
                {
                    datai++;
                    if (data[datai] == CHAR_FLAG_XORED)
                    {
                        dest[desti++] = CHAR_FLAG;
                    }
                    else if (data[datai] == CHAR_ESCAPE_XORED)
                    {
                        dest[desti++] = CHAR_ESCAPE;
                    }
                    else
                    {
                        //count this as an error on the stream!
                    }
                }
                else
                {
                    dest[desti++] = data[datai];
                }
            }
            return desti; //return the size of the new data string
        }

        public override IResponse receiveAnswer() { throw new NotImplementedException(); }
        
        public override IResponse receiveAnswer(CancellationToken ct)
        {
            Queue dataQ = new Queue();
            Byte [] b = new byte[1];
            log.Info("recieving an answer...");
            int n = 0;
            bool fullMessage = false;
            port.ReadTimeout = 1000;
            
            while (!fullMessage)
            {
                /* check the cancellation token */
                ct.ThrowIfCancellationRequested();

                try
                {
                    n = port.Read(b, 0, 1);
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
            log.Info("got: " + BitConverter.ToString(data).Replace("-", " "));

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
            if (port == null || !port.IsOpen)
            {
                throw new DeviceNotConnectedException("tried to send data while the device is not connected!");
            }

            BinaryMessage m = message as BinaryMessage;

            log.Info("sending " + m.opcode.ToString());
            
            byte [] src = m.asByteArray();
            byte [] dest = prepare_data_to_send(src);
            log.Info("array is: " + BitConverter.ToString(dest).Replace("-", " "));
            /* flush input data */
            port.ReadExisting();
            port.Write(dest, 0, dest.Length);
        }

        public override bool isAlive()
        {
            return isConnected();
        }
    }
}
