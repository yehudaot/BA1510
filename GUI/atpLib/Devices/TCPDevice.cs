using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atpLib.Messages;
using System.Net.Sockets;

namespace atpLib.Devices
{
    public abstract class TCPDevice : IPDevice
    {

        public TCPDevice(string remoteIPAddress) : base(remoteIPAddress)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp); // should move to connect?
        }

        public TCPDevice(string remoteIPAddress, int remotePort) : base(remoteIPAddress, remotePort)
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp); // should move to connect? 
        }
    }
}
