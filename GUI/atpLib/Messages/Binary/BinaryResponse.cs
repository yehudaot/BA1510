using System;
using System.Collections.Generic;
using System.Linq;

using atpLib.Infra;

namespace atpLib.Messages
{
    public abstract class BinaryResponse : IResponse
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<IResponse> InheretedClasses = ReflectiveEnumerator.GetEnumerableOfInterface<IResponse>().ToList<IResponse>();
        
        public static int HEADER_LENTGH = 8;
        public class OP
        {
            public static int GET_STATUS = BinaryMessage.OP.GET_STATUS;

            protected int value;
            public OP(int value)
            {
                this.value = value;
            }
            public static implicit operator OP(int value)
            {
                return new OP(value);
            }
            public static implicit operator int(OP op)
            {
                return op.value;
            }
            public override bool Equals(object obj)
            {
                var item = obj as OP;

                if (item == null)
                {
                    return false;
                }

                return this.value.Equals(item.value);
            }
            public override int GetHashCode()
            {
                return this.value.GetHashCode();
            }
            public override string ToString()
            {
                return value.ToString();
            }
            public string ToString(string format)
            {
                return value.ToString(format);
            }
        }

        public OP opcode {get; set;}
        public uint dataLen { get; set; }
        protected byte[] rawData {get; set;}
        
        public abstract void parametesFromData();

        public BinaryResponse() { }
        public static BinaryResponse newDeviceAnswer(OP opcode) 
        {
            foreach (IResponse r in InheretedClasses)
            {
                if (opcode.Equals(r.getUniqueIdentifier()))
                {
                    return (BinaryResponse)r;
                }
            }
            return null;
        }

        /* default implementation, should be overidden by specific device implementation */
        public virtual void fromByteArray(Byte[] arr)
        {
            if (arr.Length < HEADER_LENTGH)
            {
                throw new Panic("Byte array is not in the minimum lenght allowed!");
            }

            opcode = (OP)BitConverter.ToInt32(arr, 0);
            dataLen = (uint)BitConverter.ToInt32(arr, 4);
            rawData = new byte[dataLen];
            Array.Copy(arr, HEADER_LENTGH, rawData, 0, dataLen);
            parametesFromData();
        }

        /* default implementation, should be overidden by specific device implementation */
        public virtual void fromByteArray(Byte[] header, Byte[] data)
        {
            if (header.Length < HEADER_LENTGH)
            {
                throw new Panic("Byte array is not in the minimum lenght allowed! (" + HEADER_LENTGH + ")");
            }

            opcode = (OP)BitConverter.ToInt32(header, 0);
            dataLen = (uint)BitConverter.ToInt32(header, 4);
            this.rawData = new byte[dataLen];
            Array.Copy(data, 0, this.rawData, 0, dataLen);
            parametesFromData();
        }

        public abstract object getUniqueIdentifier();
    }
}

