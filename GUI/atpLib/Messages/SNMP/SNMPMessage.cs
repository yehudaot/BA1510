using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atpLib.Messages.SNMP
{
    public class SNMPMessage : IMessage
    {
        public class OP
        {
            public static int GET_REQUEST = 0;
            public static int SET_REQUEST = 0;

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

        public OP opcode { get; set; }
        public string OID { get; set; }
    }
}
