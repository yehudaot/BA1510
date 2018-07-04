using System;

using atpLib.Infra;

namespace atpLib.Messages
{
    public abstract class BinaryMessage : IMessage
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static int HEADER_LENTGH = 8;

        public class OP
        {
            public static int GET_STATUS = 0;

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
        }
        
        public OP opcode {get; set;}
        public int dataLen { get; set; }
        
        public abstract byte[] parametersToByteArr();

        public BinaryMessage(OP opcode)
        {
            dataLen = 0;
            this.opcode = opcode;
        }

        /* default implementation, should be overidden by specific device implementation */
        public virtual Byte[] asByteArray()
        {
            Byte[] b = new byte[HEADER_LENTGH + dataLen];
            if (BitConverter.GetBytes((int)opcode).Length > 4)
            {
                throw new Panic("Error in size of OPCODE!");
            }
            BitConverter.GetBytes((int)opcode).CopyTo(b, 0);
            BitConverter.GetBytes((int)dataLen).CopyTo(b, 4);
            Byte[] bparams = parametersToByteArr();
            if (bparams.Length != dataLen)
            {
                throw new Panic("Error in paramaters length");
            }
            Array.Copy(bparams, 0, b, HEADER_LENTGH, dataLen); 
            return b;
        }
    }
}
