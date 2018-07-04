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
    public class RFC1662BinaryDevice<T> : Device where T : Device
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected const int CHAR_FLAG = 0x7E;
        protected const int CHAR_FLAG_XORED = 0x5E;
        protected const int CHAR_ESCAPE = 0x7D;
        protected const int CHAR_ESCAPE_XORED = 0x5D;

        protected ICRC crc;
        public T device { get; protected set; }

        public RFC1662BinaryDevice(T device, ICRC crc) 
        {
            this.device = device;
            this.crc = crc;
        }

        protected byte [] prepare_data_to_send(byte[] data)
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

        protected byte[] restore_received_data(byte[] data)
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

        public override bool connect()
        {
            return device.connect();
        }

        public override void disconnect()
        {
            device.disconnect();
        }

        public override bool isConnected()
        {
            return device.isConnected();
        }

        public override bool isAlive()
        {
            return device.isAlive();
        }

        public override void sendMsg(IMessage message)
        {
            device.sendMsg(message);
        }

        public override IResponse receiveAnswer()
        {
            return device.receiveAnswer();
        }
    }
}
