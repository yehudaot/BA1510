using System;
using atpLib.Devices;
using atpLib.Messages.zUp;
using System.Threading;

namespace atpLib.Scenarios.zUp
{
    public class zUpGetCurrentScenario : SingleDeviceScenario
    {

        public zUpGetCurrentScenario(string name, Device device)
            : base(name, device)
        {
        }

        protected override Scenario.ScenarioResult internalRun(CancellationToken ct)
        {
            ScenarioResult.RunResult sResult = ScenarioResult.RunResult.Fail;
            float current = 0;
            try
            {
                device.connect();
                if (!device.isAlive())
                {
                    log.Info("aaa");
                    throw new ScenarioException("zUP device is not alive!");
                }
                device.sendMsg(new zUPSetAddressMessage());
                /* read current consuption voltage */
                device.sendMsg(new zUPGetCurrentMessage());
                string readCurrent = ((zUpGetCurrentResponse)device.receiveAnswer()).current;
                log.Info("Read current of: " + readCurrent);
                current = 0;
                if (!float.TryParse(readCurrent, out current))
                {
                    throw new ScenarioException("Error reading voltage from zUp");
                    
                }
                sResult = ScenarioResult.RunResult.Pass;
            }
            catch (Exception ex)
            {
                log.Error("Error in scenario", ex);
            }
            finally
            {
                device.disconnect();
            }
            return new ScenarioResult(sResult, current);
        }
    }
}
