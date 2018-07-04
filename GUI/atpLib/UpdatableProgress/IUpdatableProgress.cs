using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atpLib.UpdatableProgress
{
    interface IUpdatableProgress
    {
        void init();
        void reset(int maxValue);
        void update(int value);
    }
}
