using System;

namespace atpLib.Infra
{
    public class Panic : Exception
    {
        public Panic()
        {
        }

        public Panic(string message)
            : base(message)
        {
        }

        public Panic(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
