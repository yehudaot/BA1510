using System;
using System.IO;
using System.IO.Ports;

using atpLib.Messages;
using System.Net.Sockets;

namespace atpLib.Devices
{
    public abstract class IPDevice : Device
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected const int DEFUALT_IP_PORT = 40;
        const int CONNECT_TIMEOUT_MS = 3000;

        public Socket socket;
        protected string remoteAddress;
        protected int remotePort = DEFUALT_IP_PORT;

        public IPDevice(string remoteIPAddress)
        {
            this.remoteAddress = remoteIPAddress;
        }

        public IPDevice(string remoteIPAddress, int remotePort)
        {
            this.remoteAddress = remoteIPAddress;
            this.remotePort = remotePort;
        }

        #region Inhereted Methods
        public override bool connect()
        {
            try
            {
                IAsyncResult res = socket.BeginConnect(remoteAddress, remotePort, null, null);
                if (!res.AsyncWaitHandle.WaitOne(CONNECT_TIMEOUT_MS) || !socket.Connected)
                {
                    socket.Close();
                    throw new TimeoutException("timeout while connecting to " + remoteAddress + ":" + remotePort);
                }
                return true;
            }
            catch (Exception e)
            {
                log.Error("error occured while openning a connection to \"" + remoteAddress + ":" + remotePort + "\"", e);
                if (e is ArgumentNullException || e is ArgumentOutOfRangeException ||
                    e is SocketException || e is ObjectDisposedException || e is NotSupportedException ||
                    e is InvalidOperationException || e is TimeoutException)
                {
                    return false;
                }
                throw e;
            }
        }

        public override void disconnect()
        {
            if (socket != null && socket.Connected)
            {
                socket.Disconnect(false);
                socket = null;
            }
        }

        public override bool isConnected()
        {
            if (socket != null && socket.Connected)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
