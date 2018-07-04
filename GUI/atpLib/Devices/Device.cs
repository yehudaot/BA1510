using System;

using atpLib.Messages;
using System.Threading;

namespace atpLib.Devices
{
    public class DeviceNotInitException : Exception
    {
        public DeviceNotInitException()
        {
        }

        public DeviceNotInitException(string message)
            : base(message)
        {
        }

        public DeviceNotInitException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class DeviceNotConnectedException : Exception
    {
        public DeviceNotConnectedException()
        {
        }

        public DeviceNotConnectedException(string message)
            : base(message)
        {
        }

        public DeviceNotConnectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class NullDeviceException : Exception
    {
        public NullDeviceException()
        {
        }

        public NullDeviceException(string message)
            : base(message)
        {
        }

        public NullDeviceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    //public class DeviceReadTimeout : Exception
    //{
    //    public DeviceReadTimeout()
    //    {
    //    }

    //    public DeviceReadTimeout(string message)
    //        : base(message)
    //    {
    //    }

    //    public DeviceReadTimeout(string message, Exception inner)
    //        : base(message, inner)
    //    {
    //    }
    //}

    public abstract class Device
    {
        public class AsyncMessageToken
        {
            public Guid Value { get; set; }
            public AsyncMessageToken(Guid value)
            {
                this.Value = value;
            }
        }

        public bool SupportsAsyncOperations { get; protected set; }
        #region abstract functions
        public abstract bool connect();
        public abstract void disconnect();
        public abstract bool isConnected();
        public abstract bool isAlive();
        public abstract void sendMsg(IMessage message);
        public virtual IResponse receiveAnswer(CancellationToken ct) { return null; }
        public abstract IResponse receiveAnswer();
        public virtual IResponse sendRecieve(IMessage message)
        {
            throw new NotImplementedException();
        }
        public virtual AsyncMessageToken sendAsyncMsg(IMessage message)
        {
            throw new NotImplementedException();
        }
        public virtual IResponse receiveAsyncAnswer(AsyncMessageToken messageToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
