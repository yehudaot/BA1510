using atpLib.Scenarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atpLib.Devices;
using mfe_gui.Messages;
using System.IO;
using System.Threading;
using static atpLib.Devices.Device;

namespace mfe_gui.Scenarios
{
    public class CommTestScenario : SingleDeviceScenario
    {
        public class CommTestScenarioException : Exception
        {

        }

        int nTimes;
        int waitTime;


        public CommTestScenario(string name, Device device, int nTimes, int waitTime) : base(name, device)
        {
            this.nTimes = nTimes;
            this.waitTime = waitTime;
        }

        protected override ScenarioResult internalRun(CancellationToken ct)
        {
            try
            {
                int t;
                for(t = 0; t< nTimes; t++)
                {
                    MfeMessage m;
                    if((t & 1) == 0)
                    {
                        //m = new GetMomenteryStatusMessage();
                        m = new ControlMessage(true, 1, ControlMessage.TxAntenna.ANT0, ControlMessage.Frequency.HIGH, false, (ushort)t, false);
                    } else
                    {
                        //m = new GetVersionMessage();
                        m = new ControlMessage(true, 1, ControlMessage.TxAntenna.ANT1, ControlMessage.Frequency.HIGH, false, (ushort)t, false);

                    }
                    device.sendMsg(m);
                    //MfeResponse resp = (MfeResponse)device.receiveAnswer(new System.Threading.CancellationToken());
                    //if (resp == null)
                    //{
                    //    throw new CommTestScenarioException();
                    //}
                    Thread.Sleep(waitTime);
                }
            } catch (Exception)
            {
                throw new CommTestScenarioException();
            }
            return new ScenarioResult(ScenarioResult.RunResult.Pass);
        }
    }
}
