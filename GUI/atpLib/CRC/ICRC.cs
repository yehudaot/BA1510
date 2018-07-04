using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atpLib.CRC
{
    public interface ICRC
    {
        void attachEnd(byte[] cp, int len);

        void caluclate(byte[] cp, int len, byte[] result);

        bool check(byte[] cp, int len);

        int crcLength();
    }
}
