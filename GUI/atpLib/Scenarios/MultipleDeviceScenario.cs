using System;
using System.Collections.Generic;
using atpLib.Devices;
using atpLib.Messages;

namespace atpLib.Scenarios
{
    public static class DeviceListExtention
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void ConnectAll(this LinkedList<Device> devices)
        {
            //TODO: send async using future or something
            foreach (Device d in devices)
            {
                if (!d.connect())
                {
                    log.Error("cannot connect to device " + d.ToString());
                }
            }
        }

        public static void DisconnectAll(this LinkedList<Device> devices)
        {
            //TODO: send async using future or something
            foreach (Device d in devices)
            {
                d.disconnect();
            }
        }

        public static void SendMsgToAll(this LinkedList<Device> devices, IMessage message)
        {
            //TODO: send async using future or something
            foreach (Device d in devices)
            {
                d.sendMsg(message);
            }
        }

        public static Dictionary<Device, Boolean> IsAllConnected(this LinkedList<Device> devices)
        {
            //TODO: receive async using future or something
            Dictionary<Device, Boolean> resp = new Dictionary<Device, Boolean>();
            foreach (Device d in devices)
            {
                try
                {
                    resp.Add(d, d.isConnected());
                }
                catch (TimeoutException)
                {
                    resp.Add(d, false);
                }
            }
            return resp;
        } 

        public static Dictionary<Device, IResponse> ReceiveAnswerFromAll(this LinkedList<Device> devices)
        {
            //TODO: receive async using future or something
            Dictionary<Device, IResponse> resp = new Dictionary<Device, IResponse>();
            foreach (Device d in devices)
            {
                try
                {
                    resp.Add(d, d.receiveAnswer());
                }
                catch (TimeoutException)
                {
                    resp.Add(d, null);
                }
            }
            return resp;
        }
    } 


    /// <summary>
    /// a scenario that runs on multiple devices at the same time
    /// </summary>
    public abstract class MultipleDeviceScenario : Scenario
    {
        protected LinkedList<Device> devices;
        public MultipleDeviceScenario(string name, params Device[] devices) : base(name)
        {
            if (devices == null)
            {
                throw new NullDeviceException("cannot accept null device list!");
            }

            foreach (Device d in devices)
            {
                if (d == null)
                {
                    throw new NullDeviceException("cannot accept null devices!");
                }
            }
            this.devices = new LinkedList<Device>(devices);
        }
    }
}
