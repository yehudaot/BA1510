using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mfe_gui
{
    public class HexConverter
    {
        public class HexBadCSException : Exception
        {
            public HexBadCSException(string message) : base(message)
            {
            }
        }

        public class HexIlligleLineException : Exception
        {
            public HexIlligleLineException(string message) : base(message)
            {
            }
        }

        public const int COMMA_SIZE = 1;
        public const int BYTE_COUNT_SIZE = 2;
        public const int ADDRESS_SIZE = 4;
        public const int TYPE_SIZE = 2;
        public const int CS_SIZE = 2;

        public const int MIN_HEX_LINE_ASCII_SIZE = COMMA_SIZE + BYTE_COUNT_SIZE + ADDRESS_SIZE + TYPE_SIZE + CS_SIZE;

        //struct line {
        //    UInt32 address;

        //}

        string hexPath;
        Dictionary<UInt32, byte[]> memMap = new Dictionary<uint, byte[]>();
        int readMemOffset = 0;

        public HexConverter(string hexPath)
        {
            this.hexPath = hexPath;
        }

        public void Process()
        {
            int i;
            /* the offset below is added to all addressing, 
               it is updated when a type 2 or 4 line is processed */
            UInt32 globalAddressOffset = 0; 
            string[] content = File.ReadAllLines(hexPath);
            if(content == null)
            {
                throw new FileLoadException("could not open the hex file: " + hexPath);
            }

            /* read a line */
            foreach (string l in content)
            {
                if(l.StartsWith(";"))
                {
                    /* ignore comments */
                    continue;
                }
                if(!l.StartsWith(":"))
                {
                    throw new HexIlligleLineException("Hex lines should start with a :");
                }
                if(l.Length < MIN_HEX_LINE_ASCII_SIZE)
                {
                    throw new HexIlligleLineException("Hex lines should be longer or equal to " + MIN_HEX_LINE_ASCII_SIZE.ToString() + " bytes");
                }

                /* seperate to address & data */
                byte nBytes = Convert.ToByte(l.Substring(1, 2), 16);
                UInt32 address = Convert.ToUInt32(l.Substring(3, 4), 16);
                byte type = Convert.ToByte(l.Substring(7, 2), 16);
                byte cs = Convert.ToByte(l.Substring(l.Length-2, 2), 16);

                byte[] bData = new byte[nBytes];
                for(i = 0; i<nBytes; i++)
                {
                    bData[i] = Convert.ToByte(l.Substring(9 + (i * 2), 2), 16);
                }

                /* verify CS */
                byte calcCS = nBytes;
                calcCS += type;
                foreach (Byte b in BitConverter.GetBytes(address))
                {
                    calcCS += b;
                }

                foreach (Byte b in bData)
                {
                    calcCS += b;
                }

                /* cs is two's comliment */
                if((byte)(calcCS + cs) != 0)
                {
                    throw new HexBadCSException("Calculated CC: " + calcCS.ToString() + " is different than the HEX one: " + cs.ToString());
                }

                if(type == 2 || type == 4)
                {
                    int shiftCount = (type == 2) ? 4 : 16;
                    /* shift the address 4 bits left */
                    UInt32 shiftedAddress = BitConverter.ToUInt16(bData, 0);

                    shiftedAddress <<= shiftCount;
                    globalAddressOffset = shiftedAddress;
                }

                address += globalAddressOffset;
                
                UInt32 address64BytesAligned = address & 0xFFFFFFC0;
                int offset = (int)(address - address64BytesAligned);
                if (offset < 0)
                {
                    throw new InvalidDataException("got a negative offset!");
                }
                else if (offset > 64)
                {
                    throw new InvalidDataException("got offset biffer than 64!");
                }

                if (address64BytesAligned == 0)
                {

                }
                if (!memMap.ContainsKey(address64BytesAligned))
                {

                    /* if not create a new key */
                    byte[] b = new byte[64];
                    Array.Copy(bData, 0, b, offset, nBytes);
                    //memMap.Add(address64BytesAligned, b);
                    memMap[address64BytesAligned] = b;
                } else
                {
                    /* if key exitst add the data to the key */
                    byte[] b;
                    if(!memMap.TryGetValue(address64BytesAligned,  out b))
                    {
                        new KeyNotFoundException("value error when using key " + address64BytesAligned.ToString());
                    }
                    Array.Copy(bData, 0, b, offset, nBytes);
                    memMap[address64BytesAligned] = b;
                }


            }
        }

        public bool getNextLine(bool startFromScratch, out KeyValuePair<UInt32, byte[]> keyval)
        {
            
            if (startFromScratch)
            {
                readMemOffset = 0;
            }
            if(readMemOffset == memMap.Keys.Count)
            {
                keyval = new KeyValuePair<uint, byte[]>(0, null);
                return false;
            }

            UInt32 key = memMap.Keys.ToArray()[readMemOffset];
            byte[] b = memMap[key];
            keyval = new KeyValuePair<uint, byte[]>(key, b);
            readMemOffset++;
            return true;
        }
        public Int32 lineCount()
        {
            if(memMap == null)
            {
                return 0;
            }
            return memMap.Count;
        }
    }
}
